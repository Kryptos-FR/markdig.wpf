// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

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
            // TODO: styling
            renderer.Write("<Line Stretch=\"Fill\" Stroke=\"Black\" X2=\"1\" />");
        }
    }
}
