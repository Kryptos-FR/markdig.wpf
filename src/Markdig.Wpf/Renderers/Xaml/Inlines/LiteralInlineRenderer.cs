// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Xaml.Inlines
{
    /// <summary>
    /// A XAML renderer for a <see cref="LiteralInline"/>.
    /// </summary>
    /// <seealso cref="Xaml.XamlObjectRenderer{T}" />
    public class LiteralInlineRenderer : XamlObjectRenderer<LiteralInline>
    {
        protected override void Write(XamlRenderer renderer, LiteralInline obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            if (obj.Content.IsEmpty)
                return;

            renderer.Write("<Run");
            renderer.Write(" Text=\"").WriteEscape(ref obj.Content).Write("\"");
            renderer.Write(" />");
        }
    }
}
