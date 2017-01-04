// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System.Windows.Documents;
using Markdig.Annotations;
using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Wpf.Inlines
{
    using Inline = System.Windows.Documents.Inline;

    /// <summary>
    /// A WPF renderer for an <see cref="EmphasisInline"/>.
    /// </summary>
    /// <seealso cref="Markdig.Renderers.Wpf.WpfObjectRenderer{Markdig.Syntax.Inlines.EmphasisInline}" />
    public class EmphasisInlineRenderer : WpfObjectRenderer<EmphasisInline>
    {
        /// <summary>
        /// Delegates to get the inline associated to an <see cref="EmphasisInline"/> object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The WPF inline associated to this <see cref="EmphasisInline"/> object</returns>
        public delegate Inline GetInlineDelegate(EmphasisInline obj);

        /// <summary>
        /// Initializes a new instance of the <see cref="EmphasisInlineRenderer"/> class.
        /// </summary>
        public EmphasisInlineRenderer()
        {
            GetInline = GetDefaultInline;
        }

        /// <summary>
        /// Gets or sets the GetInline delegate.
        /// </summary>
        public GetInlineDelegate GetInline { get; set; }

        /// <summary>
        /// Gets the default WPF inline for ** and __ emphasis.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        [CanBeNull]
        public static Inline GetDefaultInline([NotNull] EmphasisInline obj)
        {
            if (obj.DelimiterChar == '*' || obj.DelimiterChar == '_')
            {
                return obj.IsDouble ? (Inline)new Bold() : new Italic();
            }
            return null;
        }

        protected override void Write([NotNull] WpfRenderer renderer, EmphasisInline obj)
        {
            var inline = GetInline(obj);
            // TODO: "render" the inline
            renderer.WriteChildren(obj);
            // TODO: add children to the inline
            // Note: could be a push/pop API with a stack. Pop automatically adds children into the inline and removes it from the stack.
        }
    }
}
