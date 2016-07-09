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
            
            if (obj.IsImage)
            {
                // TODO: add support for image styling
                renderer.Write("<Image");
                renderer.Write(" MaxHeight=\"{Binding RelativeSource={RelativeSource Self}, Path=Source.(BitmapSource.PixelHeight)}\"");
                renderer.Write(" MaxWidth=\"{Binding RelativeSource={RelativeSource Self}, Path=Source.(BitmapSource.PixelWidth)}\"");
                renderer.WriteLine(">");
                renderer.WriteLine("<Image.Source>");
                renderer.Write("<BitmapImage");
                renderer.Write(" UriSource=\"").WriteEscapeUrl(url).Write("\"");
                renderer.Write(" />");
                renderer.WriteLine("</Image.Source>");
                renderer.WriteLine("</Image>");
            }
            else
            {
                // TODO: add support for hyperlink styling (esp. command)
                renderer.Write("<Hyperlink");
                renderer.Write(" NavigateUri=\"").WriteEscapeUrl(url).Write("\"");
                if (!string.IsNullOrEmpty(obj.Title))
                    renderer.Write(" Tooltip=\"").Write(obj.Title).Write("\"");
                renderer.Write(">");
                renderer.WriteChildren(obj);
                renderer.Write("</Hyperlink>"); 
            }
        }
    }
}
