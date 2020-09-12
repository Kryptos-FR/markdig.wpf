// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;

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
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var tag = GetTag(obj);
            renderer.Write("<").Write(tag);
            switch (obj.DelimiterChar)
            {
                case '*':
                case '_':
                    break;
                case '~':
                    renderer.Write(obj.DelimiterCount == 2
                        ? " Style=\"{StaticResource {x:Static markdig:Styles.StrikeThroughStyleKey}}\""
                        : " Style=\"{StaticResource {x:Static markdig:Styles.SubscriptStyleKey}}\"");
                    break;
                case '^':
                    renderer.Write(" Style=\"{StaticResource {x:Static markdig:Styles.SuperscriptStyleKey}}\"");
                    break;
                case '+':
                    renderer.Write(" Style=\"{StaticResource {x:Static markdig:Styles.InsertedStyleKey}}\"");
                    break;
                case '=':
                    renderer.Write(" Style=\"{StaticResource {x:Static markdig:Styles.MarkedStyleKey}}\"");
                    break;
            }
            renderer.Write(">");
            renderer.WriteChildren(obj);
            renderer.Write("</").Write(tag).Write(">");
        }

        /// <summary>
        /// Gets the default XAML tag for ** and __ emphasis.
        /// </summary>
        /// <param name="emphasis">The emphasis inline object.</param>
        /// <returns></returns>
        public static string GetDefaultTag(EmphasisInline emphasis)
        {
            if (emphasis == null) throw new ArgumentNullException(nameof(emphasis));

            if (emphasis.DelimiterChar == '*' || emphasis.DelimiterChar == '_')
            {
                return emphasis.DelimiterCount == 2 ? "Bold" : "Italic";
            }
            return "Span";
        }
    }
}