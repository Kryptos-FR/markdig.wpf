// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Xaml.Inlines
{
    /// <summary>
    /// A XAML renderer for a <see cref="LineBreakInline"/>.
    /// </summary>
    /// <seealso cref="Xaml.XamlObjectRenderer{T}" />
    public class LineBreakInlineRenderer : XamlObjectRenderer<LineBreakInline>
    {
        protected override void Write(XamlRenderer renderer, LineBreakInline obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            if (obj.IsHard)
            {
                renderer.WriteLine("<LineBreak />");
            }
            else
            {
                renderer.WriteLine();
            }
        }
    }
}
