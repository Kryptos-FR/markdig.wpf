// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Wpf.Inlines
{
    /// <summary>
    /// A WPF renderer for a <see cref="DelimiterInline"/>.
    /// </summary>
    /// <seealso cref="Markdig.Renderers.Wpf.WpfObjectRenderer{Markdig.Syntax.Inlines.DelimiterInline}" />
    public class DelimiterInlineRenderer : WpfObjectRenderer<DelimiterInline>
    {
        /// <inheritdoc/>
        protected override void Write(WpfRenderer renderer, DelimiterInline obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            renderer.WriteText(obj.ToLiteral());
            renderer.WriteChildren(obj);
        }
    }
}
