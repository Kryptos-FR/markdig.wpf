// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Xaml.Inlines
{
    /// <summary>
    /// A XAML renderer for a <see cref="CodeInline"/>.
    /// </summary>
    /// <seealso cref="Xaml.XamlObjectRenderer{T}" />
    public class CodeInlineRenderer : XamlObjectRenderer<CodeInline>
    {
        protected override void Write(XamlRenderer renderer, CodeInline obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            renderer.Write("<Run");
            // Apply code styling (see also CodeBlockRenderer)
            renderer.Write(" Style=\"{StaticResource {x:Static markdig:Styles.CodeStyleKey}}\"");
            renderer.Write(" Text=\"").WriteEscape(obj.Content).Write("\"");
            renderer.Write(" />");
        }
    }
}
