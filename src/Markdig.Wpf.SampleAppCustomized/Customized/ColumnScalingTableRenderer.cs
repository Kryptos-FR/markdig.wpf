using System;
using System.Linq;
using System.Windows;
using Markdig.Renderers.Wpf;
using TableColumnAlign = Markdig.Extensions.Tables.TableColumnAlign;
using Table = Markdig.Extensions.Tables.Table;
using TableCell = Markdig.Extensions.Tables.TableCell;
using TableRow = Markdig.Extensions.Tables.TableRow;
using WpfDocs = System.Windows.Documents;


namespace Markdig.Wpf.SampleAppCustomized.Customized
{
    public class ColumnScalingTableRenderer : WpfObjectRenderer<Table>
    {
        protected override void Write(Renderers.WpfRenderer renderer, Table table)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (table == null) throw new ArgumentNullException(nameof(table));

            var wpfTable = new WpfDocs.Table();

            wpfTable.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.TableStyleKey);

            #region Customization

            var columnValueList = new System.Collections.Generic.List<string>?[table.ColumnDefinitions.Count];

            #endregion
            
            foreach (var tableColumnDefinition in table.ColumnDefinitions)
            {
                wpfTable.Columns.Add(new WpfDocs.TableColumn
                {
                    Width = (tableColumnDefinition?.Width ?? 0) != 0 ?
                        new GridLength(tableColumnDefinition!.Width, GridUnitType.Star) :
                        GridLength.Auto,
                });
            }

            var wpfRowGroup = new WpfDocs.TableRowGroup();

            renderer.Push(wpfTable);
            renderer.Push(wpfRowGroup);

            foreach (var rowObj in table)
            {
                var row = (TableRow)rowObj;
                var wpfRow = new WpfDocs.TableRow();

                renderer.Push(wpfRow);

                if (row.IsHeader)
                {
                    wpfRow.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.TableHeaderStyleKey);
                }

                for (var i = 0; i < row.Count; i++)
                {
                    #region Customization

                    if (i < columnValueList.Length - 1 && columnValueList[i] is null)
                        columnValueList[i] = new(row.Count);

                    #endregion
                    
                    var cellObj = row[i];
                    var cell = (TableCell)cellObj;
                    var wpfCell = new WpfDocs.TableCell
                    {
                        ColumnSpan = cell.ColumnSpan,
                        RowSpan = cell.RowSpan,
                    };
                    
                    wpfCell.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.TableCellStyleKey);
                    
                    renderer.Push(wpfCell);
                    renderer.Write(cell);
                    renderer.Pop();

                    var txt = new WpfDocs.TextRange(wpfCell.ContentStart, wpfCell.ContentEnd).Text;
                    columnValueList[i]?.Add(txt);
                    
                    if (table.ColumnDefinitions.Count > 0)
                    {
                        var columnIndex = cell.ColumnIndex < 0 || cell.ColumnIndex >= table.ColumnDefinitions.Count
                            ? i
                            : cell.ColumnIndex;
                        columnIndex = columnIndex >= table.ColumnDefinitions.Count ? table.ColumnDefinitions.Count - 1 : columnIndex;
                        var alignment = table.ColumnDefinitions[columnIndex].Alignment;


                        #region Customization
                        
                        if (row.IsHeader)
                            alignment = TableColumnAlign.Center;

                        #endregion
                        
                        if (alignment.HasValue)
                        {
                            switch (alignment)
                            {
                                case TableColumnAlign.Center:
                                    wpfCell.TextAlignment = TextAlignment.Center;
                                    break;
                                case TableColumnAlign.Right:
                                    wpfCell.TextAlignment = TextAlignment.Right;
                                    break;
                                case TableColumnAlign.Left:
                                    wpfCell.TextAlignment = TextAlignment.Left;
                                    break;
                            }
                        }
                    }
                }

                renderer.Pop();
            }

            #region Customization

            var weights = columnValueList.Select(x => x?.Max(y => y.Length) ?? 0.0).ToArray();
            var sum = weights.Sum();
            for (int i = 0; i < weights.Length; i++)
            {
                var col = wpfTable.Columns[i];
                var weight = weights[i];
                col.Width = weight == 0
                          ? new GridLength(0, GridUnitType.Pixel)
                          : new GridLength(weight / sum * 100.0, GridUnitType.Star);
            }

            #endregion
            
            renderer.Pop();
            renderer.Pop();
        }

        
    }
}