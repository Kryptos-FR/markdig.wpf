// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;

using Markdig.Syntax;

namespace Markdig.Renderers.Wpf
{
    public class ListRenderer : WpfObjectRenderer<ListBlock>
    {
        protected override void Write(WpfRenderer renderer, ListBlock listBlock)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (listBlock == null) throw new ArgumentNullException(nameof(listBlock));

            var list = new List();

            if (listBlock.IsOrdered)
            {
                list.MarkerStyle = TextMarkerStyle.Decimal;

                if (listBlock.OrderedStart != null && (listBlock.DefaultOrderedStart != listBlock.OrderedStart))
                {
                    list.StartIndex = int.Parse(listBlock.OrderedStart, NumberFormatInfo.InvariantInfo);
                }
            }
            else
            {
                list.MarkerStyle = TextMarkerStyle.Disc;
            }

            renderer.Push(list);

            foreach (var item in listBlock)
            {
                var listItemBlock = (ListItemBlock)item;
                var listItem = new ListItem();
                renderer.Push(listItem);
                renderer.WriteChildren(listItemBlock);
                renderer.Pop();
            }

            renderer.Pop();
        }
    }
}
