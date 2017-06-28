// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System.Windows.Documents;
using Markdig.Annotations;
using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Wpf.Inlines
{
    /// <summary>
    /// A WPF renderer for an <see cref="EmphasisInline"/>.
    /// </summary>
    /// <seealso cref="Markdig.Renderers.Wpf.WpfObjectRenderer{Markdig.Syntax.Inlines.EmphasisInline}" />
    public class EmphasisInlineRenderer : WpfObjectRenderer<EmphasisInline>
    {
        protected override void Write([NotNull] WpfRenderer renderer, [NotNull] EmphasisInline obj)
        {
            Span span = null;

            if (obj.DelimiterChar == '*' || obj.DelimiterChar == '_')
            {
                span = obj.IsDouble ? (Span)new Bold() : new Italic();
            }

            if (span != null)
            {
                renderer.Push(span);
                renderer.WriteChildren(obj);
                renderer.Pop();
            }
            else
            {
                renderer.WriteChildren(obj);
            }
        }
    }
}
