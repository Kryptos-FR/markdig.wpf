// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

// Some parts taken from https://github.com/lunet-io/markdig
// Copyright (c) Alexandre Mutel. All rights reserved.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

using Markdig.Helpers;
using Markdig.Renderers.Xaml;
using Markdig.Renderers.Xaml.Inlines;
using Markdig.Syntax;


namespace Markdig.Renderers
{
    /// <summary>
    /// XAML renderer for a Markdown <see cref="MarkdownDocument"/> object.
    /// </summary>
    /// <seealso cref="Renderers.TextRendererBase{T}" />
    public class XamlRenderer : TextRendererBase<XamlRenderer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XamlRenderer"/> class.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public XamlRenderer(TextWriter writer)
            : base(writer)
        {
            // Default block renderers
            ObjectRenderers.Add(new CodeBlockRenderer());
            ObjectRenderers.Add(new ListRenderer());
            ObjectRenderers.Add(new HeadingRenderer());
            ObjectRenderers.Add(new HtmlBlockRenderer());
            ObjectRenderers.Add(new ParagraphRenderer());
            ObjectRenderers.Add(new QuoteBlockRenderer());
            ObjectRenderers.Add(new ThematicBreakRenderer());

            // Default inline renderers
            ObjectRenderers.Add(new AutolinkInlineRenderer());
            ObjectRenderers.Add(new CodeInlineRenderer());
            ObjectRenderers.Add(new DelimiterInlineRenderer());
            ObjectRenderers.Add(new EmphasisInlineRenderer());
            ObjectRenderers.Add(new LineBreakInlineRenderer());
            ObjectRenderers.Add(new HtmlInlineRenderer());
            ObjectRenderers.Add(new HtmlEntityInlineRenderer());
            ObjectRenderers.Add(new LinkInlineRenderer());
            ObjectRenderers.Add(new LiteralInlineRenderer());

            EnableHtmlEscape = true;
        }

        public bool EnableHtmlEscape { get; set; }

        public override object Render(MarkdownObject markdownObject)
        {
            object result;
            if (markdownObject is MarkdownDocument)
            {
                Write("<FlowDocument");
                Write(" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"");
                Write(" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"");
                Write(" xmlns:markdig=\"clr-namespace:Markdig.Wpf;assembly=Markdig.Wpf\"");
                Write(" Style=\"{StaticResource {x:Static markdig:Styles.DocumentStyleKey}}\"");
                WriteLine(">");
                result = base.Render(markdownObject);
                Write("</FlowDocument>");
            }
            else
            {
                result = base.Render(markdownObject);
            }

            return result;
        }

        /// <summary>
        /// Writes the content escaped for XAML.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>This instance</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XamlRenderer WriteEscape(string? content)
        {
            if (string.IsNullOrEmpty(content))
                return this;

            WriteEscape(content, 0, content.Length);
            return this;
        }

        /// <summary>
        /// Writes the content escaped for XAML.
        /// </summary>
        /// <param name="slice">The slice.</param>
        /// <param name="softEscape">Only escape &lt; and &amp;</param>
        /// <returns>This instance</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XamlRenderer WriteEscape(ref StringSlice slice, bool softEscape = false)
        {
            if (slice.Start > slice.End)
            {
                return this;
            }
            return WriteEscape(slice.Text, slice.Start, slice.Length, softEscape);
        }

        /// <summary>
        /// Writes the content escaped for XAML.
        /// </summary>
        /// <param name="slice">The slice.</param>
        /// <param name="softEscape">Only escape &lt; and &amp;</param>
        /// <returns>This instance</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XamlRenderer WriteEscape(StringSlice slice, bool softEscape = false)
        {
            return WriteEscape(ref slice, softEscape);
        }

        /// <summary>
        /// Writes the content escaped for XAML.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <param name="softEscape">Only escape &lt; and &amp;</param>
        /// <returns>This instance</returns>
        public XamlRenderer WriteEscape(string? content, int offset, int length, bool softEscape = false)
        {
            if (string.IsNullOrEmpty(content) || length == 0)
                return this;

            var end = offset + length;
            var previousOffset = offset;
            for (; offset < end; offset++)
            {
                switch (content[offset])
                {
                    case '<':
                        Write(content, previousOffset, offset - previousOffset);
                        if (EnableHtmlEscape)
                        {
                            Write("&lt;");
                        }
                        previousOffset = offset + 1;
                        break;

                    case '>':
                        if (!softEscape)
                        {
                            Write(content, previousOffset, offset - previousOffset);
                            if (EnableHtmlEscape)
                            {
                                Write("&gt;");
                            }
                            previousOffset = offset + 1;
                        }
                        break;

                    case '&':
                        Write(content, previousOffset, offset - previousOffset);
                        if (EnableHtmlEscape)
                        {
                            Write("&amp;");
                        }
                        previousOffset = offset + 1;
                        break;

                    case '"':
                        if (!softEscape)
                        {
                            Write(content, previousOffset, offset - previousOffset);
                            if (EnableHtmlEscape)
                            {
                                Write("&quot;");
                            }
                            previousOffset = offset + 1;
                        }
                        break;
                }
            }

            Write(content, previousOffset, end - previousOffset);
            return this;
        }

        /// <summary>
        /// Writes the URL escaped for XAML.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>This instance</returns>
        public XamlRenderer WriteEscapeUrl(string? content)
        {
            if (content == null)
                return this;

            var previousPosition = 0;
            var length = content.Length;

            for (var i = 0; i < length; i++)
            {
                var c = content[i];

                if (c < 128)
                {
                    var escape = HtmlHelper.EscapeUrlCharacter(c);
                    if (escape != null)
                    {
                        Write(content, previousPosition, i - previousPosition);
                        previousPosition = i + 1;
                        Write(escape);
                    }
                }
                else
                {
                    Write(content, previousPosition, i - previousPosition);
                    previousPosition = i + 1;

                    byte[] bytes;
                    if (c >= '\ud800' && c <= '\udfff' && previousPosition < length)
                    {
                        bytes = Encoding.UTF8.GetBytes(new[] { c, content[previousPosition] });
                        // Skip next char as it is decoded above
                        i++;
                        previousPosition = i + 1;
                    }
                    else
                    {
                        bytes = Encoding.UTF8.GetBytes(new[] { c });
                    }

                    foreach (var t in bytes)
                    {
                        Write($"%{t:X2}");
                    }
                }
            }

            Write(content, previousPosition, length - previousPosition);
            return this;
        }

        /// <summary>
        /// Writes the lines of a <see cref="LeafBlock"/>
        /// </summary>
        /// <param name="leafBlock">The leaf block.</param>
        /// <param name="writeEndOfLines">if set to <c>true</c> write end of lines.</param>
        /// <param name="escape">if set to <c>true</c> escape the content for XAML</param>
        /// <param name="softEscape">Only escape &lt; and &amp;</param>
        /// <returns>This instance</returns>
        public XamlRenderer WriteLeafRawLines(LeafBlock leafBlock, bool writeEndOfLines, bool escape, bool softEscape = false)
        {
            if (leafBlock == null) throw new ArgumentNullException(nameof(leafBlock));
            if (leafBlock.Lines.Lines != null)
            {
                var lines = leafBlock.Lines;
                var slices = lines.Lines;
                for (var i = 0; i < lines.Count; i++)
                {
                    if (!writeEndOfLines && i > 0)
                    {
                        WriteLine();
                    }
                    if (escape)
                    {
                        WriteEscape(ref slices[i].Slice, softEscape);
                    }
                    else
                    {
                        Write(ref slices[i].Slice);
                    }
                    if (writeEndOfLines)
                    {
                        WriteLine();
                    }
                }
            }
            return this;
        }
    }
}
