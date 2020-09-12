// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Wpf.Inlines
{
    /// <summary>
    /// A WPF renderer for a <see cref="HtmlEntityInline"/>.
    /// </summary>
    /// <seealso cref="Markdig.Renderers.Wpf.WpfObjectRenderer{Markdig.Syntax.Inlines.HtmlEntityInline}" />
    public class HtmlEntityInlineRenderer : WpfObjectRenderer<HtmlEntityInline>
    {
        protected override void Write(WpfRenderer renderer, HtmlEntityInline obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var transcoded = obj.Transcoded;
            renderer.WriteText(ref transcoded);
        }
    }
}
