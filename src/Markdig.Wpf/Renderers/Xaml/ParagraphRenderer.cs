// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using Markdig.Annotations;
using Markdig.Syntax;

namespace Markdig.Renderers.Xaml
{
    /// <summary>
    /// A XAML renderer for a <see cref="ParagraphBlock"/>.
    /// </summary>
    /// <seealso cref="Xaml.XamlObjectRenderer{T}" />
    public class ParagraphRenderer : XamlObjectRenderer<ParagraphBlock>
    {
        protected override void Write([NotNull] XamlRenderer renderer, ParagraphBlock obj)
        {
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
