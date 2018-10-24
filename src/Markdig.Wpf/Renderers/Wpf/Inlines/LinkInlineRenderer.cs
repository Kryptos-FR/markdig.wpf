// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Markdig.Annotations;
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
        protected override void Write([NotNull] WpfRenderer renderer, [NotNull] LinkInline link)
        {
            var url = link.GetDynamicUrl != null ? link.GetDynamicUrl() ?? link.Url : link.Url;

            if (!Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                url = "#";
            }

            if (link.IsImage)
            {
                var image = new Image
                {
                    Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute))
                };

                image.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.ImageStyleKey);

                renderer.WriteInline(new InlineUIContainer(image));
            }
            else
            {
                var hyperlink = new Hyperlink
                {
                    Command = Commands.Hyperlink,
                    CommandParameter = url,
                    NavigateUri = new Uri(url, UriKind.RelativeOrAbsolute),
                    ToolTip = link.Title != string.Empty ? link.Title : null,
                };

                hyperlink.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.HyperlinkStyleKey);

                renderer.Push(hyperlink);
                renderer.WriteChildren(link);
                renderer.Pop();
            }
        }
    }
}
