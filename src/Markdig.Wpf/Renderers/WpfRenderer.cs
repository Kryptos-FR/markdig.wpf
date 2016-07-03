// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System.Collections.Generic;
using System.Windows.Documents;
using Markdig.Helpers;
using Markdig.Renderers.Wpf;
using Markdig.Renderers.Wpf.Inlines;
using System.Runtime.CompilerServices;
using System;

namespace Markdig.Renderers
{
    public class WpfRenderer : RendererBase
    {
        private readonly Stack<Block> blocks = new Stack<Block>();
        private readonly List<Inline> inlines = new List<Inline>();
        private char[] buffer;

        public WpfRenderer(FlowDocument document)
        {
            buffer = new char[1024];
            Document = document;

            // Default block renderers
            //ObjectRenderers.Add(new CodeBlockRenderer());
            //ObjectRenderers.Add(new ListRenderer());
            //ObjectRenderers.Add(new HeadingRenderer());
            //ObjectRenderers.Add(new HtmlBlockRenderer());
            ObjectRenderers.Add(new ParagraphRenderer());
            //ObjectRenderers.Add(new QuoteBlockRenderer());
            //ObjectRenderers.Add(new ThematicBreakRenderer());

            // Default inline renderers
            //ObjectRenderers.Add(new AutolinkInlineRenderer());
            //ObjectRenderers.Add(new CodeInlineRenderer());
            ObjectRenderers.Add(new DelimiterInlineRenderer());
            //ObjectRenderers.Add(new EmphasisInlineRenderer());
            ObjectRenderers.Add(new LineBreakInlineRenderer());
            //ObjectRenderers.Add(new HtmlInlineRenderer());
            //ObjectRenderers.Add(new HtmlEntityInlineRenderer());
            //ObjectRenderers.Add(new LinkInlineRenderer());
            ObjectRenderers.Add(new LiteralInlineRenderer());
        }

        public FlowDocument Document { get; }

        internal IReadOnlyCollection<Inline> Inlines => inlines;

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
        public void WriteLeafInline(Syntax.LeafBlock leafBlock)
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

        internal void BeginBlock(Block block)
        {
            blocks.Push(block);
        }

        internal void EndBlock()
        {
            var block = blocks.Pop();
            if (blocks.Count == 0)
            {
                // first-level block
                Document.Blocks.Add(block);
            }
            inlines.Clear();
        }

        internal void BeginSpan(Span span)
        {
            // TODO
        }

        internal void EndSpan()
        {
            // TODO
        }

        internal void WriteInline(Inline inline)
        {
            inlines.Add(inline);
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

        internal void WriteText(string text, int offset, int length)
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

    }
}
