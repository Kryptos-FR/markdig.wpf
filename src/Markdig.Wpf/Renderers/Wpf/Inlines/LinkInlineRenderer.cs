// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

using Markdig.Syntax.Inlines;
using Markdig.Wpf;

namespace Markdig.Renderers.Wpf.Inlines
{
    /// <summary>
    /// A WPF renderer for a <see cref="LinkInline"/>.
    /// </summary>
    /// <seealso cref="Markdig.Renderers.Wpf.WpfObjectRenderer{Markdig.Syntax.Inlines.LinkInline}" />
    public class LinkInlineRenderer : WpfObjectRenderer<LinkInline>
    {
        /// <inheritdoc/>
        protected override void Write(WpfRenderer renderer, LinkInline link)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (link == null) throw new ArgumentNullException(nameof(link));

            var url = link.GetDynamicUrl != null ? link.GetDynamicUrl() ?? link.Url : link.Url;

            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out _))
            {
                url = "#";
            }

            if (link.IsImage)
            {
                var template = new ControlTemplate();
                var image = new FrameworkElementFactory(typeof(Image));
                image.SetValue(Image.SourceProperty, new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute)));
                image.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.ImageStyleKey);
                template.VisualTree = image;

                var btn = new Button()
                {
                    Template = template,
                    Command = Commands.Image,
                    CommandParameter = url
                };

                renderer.WriteInline(new InlineUIContainer(btn));
            }
            else
            {
                var hyperlink = new Hyperlink
                {
                    Command = url.StartsWith("#") ? Commands.Navigate : Commands.Hyperlink,
                    CommandParameter = url,
                    NavigateUri = new Uri(url, UriKind.RelativeOrAbsolute),
                    ToolTip = !string.IsNullOrEmpty(link.Title) ? link.Title : null,
                };
                hyperlink.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.HyperlinkStyleKey);

                renderer.Push(hyperlink);
                renderer.WriteChildren(link);
                renderer.Pop();
            }
        }
    }
}
