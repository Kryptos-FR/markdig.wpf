// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax;
using System.Windows.Documents;
using Markdig.Annotations;
using Markdig.Wpf;

namespace Markdig.Renderers.Wpf
{
    public class QuoteBlockRenderer : WpfObjectRenderer<QuoteBlock>
    {
        /// <inheritdoc/>
        protected override void Write([NotNull] WpfRenderer renderer, [NotNull] QuoteBlock obj)
        {
            var section = new Section();

            renderer.Push(section);
            renderer.WriteChildren(obj);
            section.SetResourceReference(Paragraph.StyleProperty, Styles.QuoteBlockStyleKey);
            renderer.Pop();
        }
    }
}
