// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax;

namespace Markdig.Renderers.Xaml
{
    /// <summary>
    /// A XAML renderer for a <see cref="QuoteBlock"/>.
    /// </summary>
    /// <seealso cref="Xaml.XamlObjectRenderer{T}" />
    public class QuoteBlockRenderer : XamlObjectRenderer<QuoteBlock>
    {
        protected override void Write(XamlRenderer renderer, QuoteBlock obj)
        {
            renderer.EnsureLine();

            // TODO: apply quote block styling
            renderer.Write("<Paragraph>");
            renderer.WriteChildren(obj);
            renderer.WriteLine("</Paragraph>");
        }
    }
}
