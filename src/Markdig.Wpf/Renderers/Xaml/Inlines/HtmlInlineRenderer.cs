// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Xaml.Inlines
{
    /// <summary>
    /// A XAML renderer for a <see cref="HtmlInline"/>.
    /// </summary>
    /// <seealso cref="Xaml.XamlObjectRenderer{T}" />
    public class HtmlInlineRenderer : XamlObjectRenderer<HtmlInline>
    {
        protected override void Write(XamlRenderer renderer, HtmlInline obj)
        {
            // HTML inlines are not supported
        }
    }
}
