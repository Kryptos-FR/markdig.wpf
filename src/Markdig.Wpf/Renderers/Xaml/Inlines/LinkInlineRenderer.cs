// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;

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
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var url = obj.GetDynamicUrl?.Invoke() ?? obj.Url;

            if (obj.IsImage)
            {
                renderer.Write("<Image");
                // Add image styling
                renderer.Write(" Style=\"{StaticResource {x:Static markdig:Styles.ImageStyleKey}}\"");
                renderer.WriteLine(">");
                renderer.WriteLine("<Image.Source>");
                renderer.Write("<BitmapImage");
                renderer.Write(" UriSource=\"").WriteEscapeUrl(url).Write("\"");
                renderer.WriteLine(" />");
                renderer.WriteLine("</Image.Source>");
                renderer.WriteLine("</Image>");
            }
            else
            {
                renderer.Write("<Hyperlink");
                renderer.Write(" Style=\"{StaticResource {x:Static markdig:Styles.HyperlinkStyleKey}}\"");
                renderer.Write(" Command=\"{x:Static markdig:Commands.Hyperlink}\"");
                renderer.Write(" CommandParameter=\"").WriteEscapeUrl(url).Write("\"");
                if (!string.IsNullOrEmpty(obj.Title))
                    renderer.Write(" ToolTip=\"").Write(obj.Title).Write("\"");
                renderer.WriteLine(">");
                renderer.WriteChildren(obj);
                renderer.EnsureLine();
                renderer.WriteLine("</Hyperlink>");
            }
        }
    }
}
