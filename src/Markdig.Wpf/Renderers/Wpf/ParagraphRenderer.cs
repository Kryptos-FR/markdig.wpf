// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax;
using System.Windows.Documents;
using Markdig.Annotations;

namespace Markdig.Renderers.Wpf
{
    public class ParagraphRenderer : WpfObjectRenderer<ParagraphBlock>
    {
        /// <inheritdoc/>
        protected override void Write([NotNull] WpfRenderer renderer, [NotNull] ParagraphBlock obj)
        {
            var paragraph = new Paragraph();

            renderer.BeginBlock(paragraph);
            renderer.WriteLeafInline(obj);

            paragraph.Inlines.AddRange(renderer.Inlines);
            renderer.EndBlock();
        } 
    }
}
