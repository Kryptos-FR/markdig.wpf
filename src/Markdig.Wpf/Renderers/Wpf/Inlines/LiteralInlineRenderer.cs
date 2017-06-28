// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using Markdig.Annotations;
using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Wpf.Inlines
{
    /// <summary>
    /// A WPF renderer for a <see cref="LiteralInline"/>.
    /// </summary>
    /// <seealso cref="Markdig.Renderers.Wpf.WpfObjectRenderer{Markdig.Syntax.Inlines.LiteralInline}" />
    public class LiteralInlineRenderer : WpfObjectRenderer<LiteralInline>
    {
        /// <inheritdoc/>
        protected override void Write([NotNull] WpfRenderer renderer, [NotNull] LiteralInline obj)
        {
            if (obj.Content.IsEmpty)
                return;

            renderer.WriteText(ref obj.Content);
        }
    }
}
