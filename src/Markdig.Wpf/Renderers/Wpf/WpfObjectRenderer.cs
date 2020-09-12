// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax;

namespace Markdig.Renderers.Wpf
{
    /// <summary>
    /// A base class for WPF rendering <see cref="Block"/> and <see cref="Markdig.Syntax.Inlines.Inline"/> Markdown objects.
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <seealso cref="Markdig.Renderers.IMarkdownObjectRenderer" />
    public abstract class WpfObjectRenderer<TObject> : MarkdownObjectRenderer<WpfRenderer, TObject>
        where TObject : MarkdownObject
    {
    }
}
