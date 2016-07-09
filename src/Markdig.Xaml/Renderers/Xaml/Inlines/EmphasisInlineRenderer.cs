// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Xaml.Inlines
{
    /// <summary>
    /// A XAML renderer for an <see cref="EmphasisInline"/>.
    /// </summary>
    /// <seealso cref="Xaml.XamlObjectRenderer{T}" />
    public class EmphasisInlineRenderer : XamlObjectRenderer<EmphasisInline>
    {
        /// <summary>
        /// Delegates to get the tag associated to an <see cref="EmphasisInline"/> object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The XAML tag associated to this <see cref="EmphasisInline"/> object</returns>
        public delegate string GetTagDelegate(EmphasisInline obj);

        /// <summary>
        /// Initializes a new instance of the <see cref="EmphasisInlineRenderer"/> class.
        /// </summary>
        public EmphasisInlineRenderer()
        {
            GetTag = GetDefaultTag;
        }

        /// <summary>
        /// Gets or sets the GetTag delegate.
        /// </summary>
        public GetTagDelegate GetTag { get; set; }

        protected override void Write(XamlRenderer renderer, EmphasisInline obj)
        {
            var tag = GetTag(obj);
            renderer.Write("<").Write(tag).Write(">");
            renderer.WriteChildren(obj);
            renderer.Write("</").Write(tag).Write(">");
        }

        /// <summary>
        /// Gets the default XAML tag for ** and __ emphasis.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public string GetDefaultTag(EmphasisInline obj)
        {
            if (obj.DelimiterChar == '*' || obj.DelimiterChar == '_')
            {
                return obj.IsDouble ? "Bold" : "Italic";
            }
            return null;
        }
    }
}