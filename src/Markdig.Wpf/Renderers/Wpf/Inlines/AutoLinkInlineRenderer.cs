// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Documents;
using Markdig.Syntax.Inlines;
using Markdig.Wpf;

namespace Markdig.Renderers.Wpf.Inlines
{
    /// <summary>
    /// A WPF renderer for a <see cref="AutolinkInline"/>.
    /// </summary>
    /// <seealso cref="Markdig.Renderers.Wpf.WpfObjectRenderer{Markdig.Syntax.Inlines.AutolinkInline}" />
    public class AutolinkInlineRenderer : WpfObjectRenderer<AutolinkInline>
    {
        /// <inheritdoc/>
        protected override void Write(WpfRenderer renderer, AutolinkInline link)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (link == null) throw new ArgumentNullException(nameof(link));

            var url = link.Url;
            if (link.IsEmail)
            {
                url = "mailto:" + url;
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                url = "#";
            }

            var hyperlink = new Hyperlink
            {
                Command = Commands.Hyperlink,
                CommandParameter = url,
                NavigateUri = new Uri(url, UriKind.RelativeOrAbsolute),
                ToolTip = link.Url,
            };

            hyperlink.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.HyperlinkStyleKey);

            renderer.Push(hyperlink);
            renderer.WriteText(link.Url);
            renderer.Pop();
        }
    }
}
