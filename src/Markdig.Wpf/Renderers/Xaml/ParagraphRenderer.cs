// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;

using Markdig.Syntax;

namespace Markdig.Renderers.Xaml
{
    /// <summary>
    /// A XAML renderer for a <see cref="ParagraphBlock"/>.
    /// </summary>
    /// <seealso cref="Xaml.XamlObjectRenderer{T}" />
    public class ParagraphRenderer : XamlObjectRenderer<ParagraphBlock>
    {
        protected override void Write(XamlRenderer renderer, ParagraphBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            if (!renderer.IsFirstInContainer)
            {
                renderer.EnsureLine();
            }
            renderer.WriteLine("<Paragraph>");
            renderer.WriteLeafInline(obj);
            renderer.EnsureLine();
            renderer.WriteLine("</Paragraph>");
        }
    }
}
