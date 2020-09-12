// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Documents;

using Markdig.Syntax;
using Markdig.Wpf;

namespace Markdig.Renderers.Wpf
{
    public class ParagraphRenderer : WpfObjectRenderer<ParagraphBlock>
    {
        /// <inheritdoc/>
        protected override void Write(WpfRenderer renderer, ParagraphBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var paragraph = new Paragraph();
            paragraph.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.ParagraphStyleKey);

            renderer.Push(paragraph);
            renderer.WriteLeafInline(obj);
            renderer.Pop();
        }
    }
}
