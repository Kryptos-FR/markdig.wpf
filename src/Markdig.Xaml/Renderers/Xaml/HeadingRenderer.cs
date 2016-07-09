// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax;

namespace Markdig.Renderers.Xaml
{
    /// <summary>
    /// An XAML renderer for a <see cref="HeadingBlock"/>.
    /// </summary>
    /// <seealso cref="Xaml.XamlObjectRenderer{T}" />
    public class HeadingRenderer : XamlObjectRenderer<HeadingBlock>
    {
        protected override void Write(XamlRenderer renderer, HeadingBlock obj)
        {
            // TODO: apply style depending on heading level
            renderer.Write("<Paragraph>");
            renderer.WriteLeafInline(obj);
            renderer.WriteLine("</Paragraph>");
        }
    }
}
