// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;

using Markdig.Syntax;

namespace Markdig.Renderers.Xaml
{
    /// <summary>
    /// A XAML renderer for a <see cref="ThematicBreakBlock"/>.
    /// </summary>
    /// <seealso cref="Xaml.XamlObjectRenderer{T}" />
    public class ThematicBreakRenderer : XamlObjectRenderer<ThematicBreakBlock>
    {
        protected override void Write(XamlRenderer renderer, ThematicBreakBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            renderer.EnsureLine();

            renderer.WriteLine("<Paragraph>");
            renderer.Write("<Line X2=\"1\"");
            // Apply styling
            renderer.Write(" Style=\"{StaticResource {x:Static markdig:Styles.ThematicBreakStyleKey}}\"");
            renderer.WriteLine(" />");
            renderer.WriteLine("</Paragraph>");
        }
    }
}
