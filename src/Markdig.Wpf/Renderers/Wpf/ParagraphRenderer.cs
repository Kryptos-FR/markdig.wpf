// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System.Windows.Documents;
using Markdig.Annotations;
using Markdig.Syntax;

namespace Markdig.Renderers.Wpf
{
    public class ParagraphRenderer : WpfObjectRenderer<ParagraphBlock>
    {
        /// <inheritdoc/>
        protected override void Write([NotNull] WpfRenderer renderer, [NotNull] ParagraphBlock obj)
        {
            var paragraph = new Paragraph();

            renderer.Push(paragraph);
            renderer.WriteLeafInline(obj);
            renderer.Pop();
        }
    }
}
