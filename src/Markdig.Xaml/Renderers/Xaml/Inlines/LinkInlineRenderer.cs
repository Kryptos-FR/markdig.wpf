// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Xaml.Inlines
{
    /// <summary>
    /// A XAML renderer for a <see cref="LinkInline"/>.
    /// </summary>
    /// <seealso cref="Xaml.XamlObjectRenderer{T}" />
    public class LinkInlineRenderer : XamlObjectRenderer<LinkInline>
    {
        protected override void Write(XamlRenderer renderer, LinkInline obj)
        {
            var url = obj.GetDynamicUrl?.Invoke() ?? obj.Url;

            renderer.Write("<Hyperlink").Write(" NavigateUri=\"").WriteEscapeUrl(url).Write("\">");
            renderer.WriteEscape(obj.Title);
            renderer.Write("</Hyperlink");

            // TODO: support images
        }
    }
}
