// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System.Collections.Generic;
using System.Windows.Documents;
using Markdig.Helpers;
using Markdig.Renderers.Wpf;
using Markdig.Renderers.Wpf.Inlines;
using System.Runtime.CompilerServices;
using System;
using Markdig.Annotations;
using System.Linq;

namespace Markdig.Renderers
{
    /// <summary>
    /// WPF renderer for a Markdown <see cref="Syntax.MarkdownDocument"/> object.
    /// </summary>
    /// <seealso cref="Renderers.RendererBase" />
    public class WpfRenderer : RendererBase
    {
        private readonly Stack<object> stack = new Stack<object>();
        private char[] buffer;

        public WpfRenderer(FlowDocument document)
        {
            buffer = new char[1024];
            Document = document;

            // Default block renderers
            ObjectRenderers.Add(new CodeBlockRenderer());
            ObjectRenderers.Add(new ListRenderer());
            ObjectRenderers.Add(new HeadingRenderer());
            ObjectRenderers.Add(new ParagraphRenderer());
            ObjectRenderers.Add(new QuoteBlockRenderer());
            ObjectRenderers.Add(new ThematicBreakRenderer());

            // Default inline renderers
            ObjectRenderers.Add(new AutolinkInlineRenderer());
            ObjectRenderers.Add(new CodeInlineRenderer());
            ObjectRenderers.Add(new DelimiterInlineRenderer());
            ObjectRenderers.Add(new EmphasisInlineRenderer());
            ObjectRenderers.Add(new LineBreakInlineRenderer());
            //ObjectRenderers.Add(new HtmlInlineRenderer());
            //ObjectRenderers.Add(new HtmlEntityInlineRenderer());
            ObjectRenderers.Add(new LinkInlineRenderer());
            ObjectRenderers.Add(new LiteralInlineRenderer());
        }

        public FlowDocument Document { get; }

        /// <inheritdoc/>
        public override object Render(Syntax.MarkdownObject markdownObject)
        {
            Write(markdownObject);
            return Document;
        }

        /// <summary>
        /// Writes the inlines of a leaf inline.
        /// </summary>
        /// <param name="leafBlock">The leaf block.</param>
        /// <returns>This instance</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteLeafInline([NotNull] Syntax.LeafBlock leafBlock)
        {
            if (leafBlock == null) throw new ArgumentNullException(nameof(leafBlock));
            var inline = (Syntax.Inlines.Inline)leafBlock.Inline;
            if (inline != null)
            {
                while (inline != null)
                {
                    Write(inline);
                    inline = inline.NextSibling;
                }
            }
        }

        /// <summary>
        /// Writes the lines of a <see cref="LeafBlock"/>
        /// </summary>
        /// <param name="leafBlock">The leaf block.</param>
        /// <param name="writeEndOfLines">if set to <c>true</c> write end of lines.</param>
        /// <param name="escape">if set to <c>true</c> escape the content for HTML</param>
        /// <param name="softEscape">Only escape &lt; and &amp;</param>
        /// <returns>This instance</returns>
        public void WriteLeafRawLines([NotNull] Syntax.LeafBlock leafBlock)
        {
            if (leafBlock == null) throw new ArgumentNullException(nameof(leafBlock));
            if (leafBlock.Lines.Lines != null)
            {
                var lines = leafBlock.Lines;
                var slices = lines.Lines;
                for (int i = 0; i < lines.Count; i++)
                {
                    WriteText(ref slices[i].Slice);
                    WriteInline(new LineBreak());
                }
            }
        }

        internal void Push(object o)
        {
            stack.Push(o);
        }

        internal void Pop()
        {
            var popped = stack.Pop();
            BlockCollection blocks = null;
            InlineCollection inlines = null;
            ListItemCollection listItems = null;

            if (stack.Count == 0)
            {
                Document.Blocks.Add((Block)popped);
            }
            else
            {
                var top = stack.Peek();

                switch (top)
                {
                    case List list:
                        listItems = list.ListItems;
                        break;
                    case ListItem listItem:
                        blocks = listItem.Blocks;
                        break;
                    case Paragraph paragraph:
                        inlines = paragraph.Inlines;
                        break;
                    case Section section:
                        blocks = section.Blocks;
                        break;
                    default:
                        throw new NotImplementedException();
                }

                if (inlines != null)
                {
                    AddInline(inlines, (Inline)popped);
                }
                else if (listItems != null)
                {
                    listItems.Add((ListItem)popped);
                }
                else
                {
                    blocks.Add((Block)popped);
                }
            }
        }

        internal void WriteInline(Inline inline)
        {
            var top = stack.Peek();
            InlineCollection inlines;

            switch (top)
            {
                case Paragraph para:
                    inlines = para.Inlines;
                    break;
                case Span span:
                    inlines = span.Inlines;
                    break;
                default:
                    throw new NotImplementedException();
            }

            AddInline(inlines, inline);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void WriteText(ref StringSlice slice)
        {
            if (slice.Start > slice.End)
                return;

            WriteText(slice.Text, slice.Start, slice.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void WriteText(string text)
        {
            WriteInline(new Run(text));
        }

        internal void WriteText([CanBeNull] string text, int offset, int length)
        {
            if (text == null)
                return;

            if (offset == 0 && text.Length == length)
            {
                WriteText(text);
            }
            else
            {
                if (length > buffer.Length)
                {
                    buffer = text.ToCharArray();
                    WriteText(new string(buffer, offset, length));
                }
                else
                {
                    text.CopyTo(offset, buffer, 0, length);
                    WriteText(new string(buffer, 0, length));
                }
            }
        }

        private void AddInline(InlineCollection inlines, Inline inline)
        {
            if (!EndsWithSpace(inlines) && !StartsWithSpace(inline))
            {
                inlines.Add(new Run(" "));
            }

            inlines.Add(inline);
        }

        private bool StartsWithSpace(Inline inline)
        {
            if (inline is Run run)
            {
                return run.Text.Length == 0 || run.Text.First().IsWhitespace();
            }
            else if (inline is Span span)
            {
                return StartsWithSpace(span.Inlines.FirstInline);
            }

            return true;
        }

        private bool EndsWithSpace(InlineCollection inlines)
        {
            if (inlines.LastInline is Run run)
            {
                return run.Text.Length == 0 || run.Text.Last().IsWhitespace();
            }
            else if (inlines.LastInline is Span span)
            {
                return EndsWithSpace(span.Inlines);
            }

            return true;
        }
    }
}
