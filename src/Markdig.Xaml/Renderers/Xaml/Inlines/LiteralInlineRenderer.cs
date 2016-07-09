// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Xaml.Inlines
{
    /// <summary>
    /// A XAML renderer for a <see cref="LiteralInline"/>.
    /// </summary>
    /// <seealso cref="Xaml.XamlObjectRenderer{T}" />
    public class LiteralInlineRenderer : XamlObjectRenderer<LiteralInline>
    {
        protected override void Write(XamlRenderer renderer, LiteralInline obj)
        {
            renderer.Write("<Run>");
            renderer.WriteEscape(ref obj.Content);
            renderer.Write("</Run>");
        }
    }
}
