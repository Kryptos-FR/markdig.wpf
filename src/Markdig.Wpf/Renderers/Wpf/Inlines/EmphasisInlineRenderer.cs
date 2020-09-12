// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Documents;

using Markdig.Syntax.Inlines;
using Markdig.Wpf;

namespace Markdig.Renderers.Wpf.Inlines
{
    /// <summary>
    /// A WPF renderer for an <see cref="EmphasisInline"/>.
    /// </summary>
    /// <seealso cref="EmphasisInline" />
    public class EmphasisInlineRenderer : WpfObjectRenderer<EmphasisInline>
    {
        protected override void Write(WpfRenderer renderer, EmphasisInline obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            Span? span = null;

            switch (obj.DelimiterChar)
            {
                case '*':
                case '_':
                    span = obj.DelimiterCount == 2 ? (Span)new Bold() : new Italic();
                    break;
                case '~':
                    span = new Span();
                    span.SetResourceReference(FrameworkContentElement.StyleProperty, obj.DelimiterCount == 2 ? Styles.StrikeThroughStyleKey : Styles.SubscriptStyleKey);
                    break;
                case '^':
                    span = new Span();
                    span.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.SuperscriptStyleKey);
                    break;
                case '+':
                    span = new Span();
                    span.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.InsertedStyleKey);
                    break;
                case '=':
                    span = new Span();
                    span.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.MarkedStyleKey);
                    break;
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
