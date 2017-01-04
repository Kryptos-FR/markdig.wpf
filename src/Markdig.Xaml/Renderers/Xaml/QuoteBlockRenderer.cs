// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using Markdig.Annotations;
using Markdig.Syntax;

namespace Markdig.Renderers.Xaml
{
    /// <summary>
    /// A XAML renderer for a <see cref="QuoteBlock"/>.
    /// </summary>
    /// <seealso cref="Xaml.XamlObjectRenderer{T}" />
    public class QuoteBlockRenderer : XamlObjectRenderer<QuoteBlock>
    {
        protected override void Write([NotNull] XamlRenderer renderer, QuoteBlock obj)
        {
            renderer.EnsureLine();

            renderer.Write("<Section");
            // Apply quote block styling
            renderer.Write(" Style=\"{StaticResource {x:Static markdig:Styles.QuoteBlockStyleKey}}\"");
            renderer.WriteLine(">");
            renderer.WriteChildren(obj);
            renderer.WriteLine("</Section>");
        }
    }
}
