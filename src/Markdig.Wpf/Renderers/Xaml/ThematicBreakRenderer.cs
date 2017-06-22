// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using Markdig.Annotations;
using Markdig.Syntax;

namespace Markdig.Renderers.Xaml
{
    /// <summary>
    /// A XAML renderer for a <see cref="ThematicBreakBlock"/>.
    /// </summary>
    /// <seealso cref="Xaml.XamlObjectRenderer{T}" />
    public class ThematicBreakRenderer : XamlObjectRenderer<ThematicBreakBlock>
    {
        protected override void Write([NotNull] XamlRenderer renderer, ThematicBreakBlock obj)
        {
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
