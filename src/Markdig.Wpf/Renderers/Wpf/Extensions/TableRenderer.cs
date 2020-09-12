// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows;
using Markdig.Extensions.Tables;
using Markdig.Wpf;
using WpfTable = System.Windows.Documents.Table;
using WpfTableCell = System.Windows.Documents.TableCell;
using WpfTableColumn = System.Windows.Documents.TableColumn;
using WpfTableRow = System.Windows.Documents.TableRow;
using WpfTableRowGroup = System.Windows.Documents.TableRowGroup;

namespace Markdig.Renderers.Wpf.Extensions
{
    public class TableRenderer : WpfObjectRenderer<Table>
    {
        protected override void Write(WpfRenderer renderer, Table table)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (table == null) throw new ArgumentNullException(nameof(table));

            var wpfTable = new WpfTable();

            wpfTable.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.TableStyleKey);

            foreach (var tableColumnDefinition in table.ColumnDefinitions)
            {
                wpfTable.Columns.Add(new WpfTableColumn
                {
                    Width = (tableColumnDefinition?.Width ?? 0) != 0 ?
                        new GridLength(tableColumnDefinition!.Width, GridUnitType.Star) :
                        GridLength.Auto,
                });
            }

            var wpfRowGroup = new WpfTableRowGroup();

            renderer.Push(wpfTable);
            renderer.Push(wpfRowGroup);

            foreach (var rowObj in table)
            {
                var row = (TableRow)rowObj;
                var wpfRow = new WpfTableRow();

                renderer.Push(wpfRow);

                if (row.IsHeader)
                {
                    wpfRow.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.TableHeaderStyleKey);
                }

                for (var i = 0; i < row.Count; i++)
                {
                    var cellObj = row[i];
                    var cell = (TableCell)cellObj;
                    var wpfCell = new WpfTableCell
                    {
                        ColumnSpan = cell.ColumnSpan,
                        RowSpan = cell.RowSpan,
                    };

                    wpfCell.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.TableCellStyleKey);

                    renderer.Push(wpfCell);
                    renderer.Write(cell);
                    renderer.Pop();

                    if (table.ColumnDefinitions.Count > 0)
                    {
                        var columnIndex = cell.ColumnIndex < 0 || cell.ColumnIndex >= table.ColumnDefinitions.Count
                            ? i
                            : cell.ColumnIndex;
                        columnIndex = columnIndex >= table.ColumnDefinitions.Count ? table.ColumnDefinitions.Count - 1 : columnIndex;
                        var alignment = table.ColumnDefinitions[columnIndex].Alignment;
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

            renderer.Pop();
            renderer.Pop();
        }
    }
}
