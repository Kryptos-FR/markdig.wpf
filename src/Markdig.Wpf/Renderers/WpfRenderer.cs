// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using Markdig.Annotations;
using Markdig.Helpers;
using Markdig.Renderers.Wpf;
using Markdig.Renderers.Wpf.Extensions;
using Markdig.Renderers.Wpf.Inlines;
using Markdig.Syntax;
using Markdig.Wpf;
using Block = System.Windows.Documents.Block;

namespace Markdig.Renderers
{
    /// <summary>
    /// WPF renderer for a Markdown <see cref="MarkdownDocument"/> object.
    /// </summary>
    /// <seealso cref="RendererBase" />
    public class WpfRenderer : RendererBase
    {
        private readonly Stack<IAddChild> stack = new Stack<IAddChild>();
        private char[] buffer;

        public WpfRenderer([NotNull] FlowDocument document)
        {
            buffer = new char[1024];
            Document = document;
            document.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.DocumentStyleKey);
            stack.Push(document);

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
            ObjectRenderers.Add(new HtmlEntityInlineRenderer());
            ObjectRenderers.Add(new LineBreakInlineRenderer());
            ObjectRenderers.Add(new LinkInlineRenderer());
            ObjectRenderers.Add(new LiteralInlineRenderer());

            // Extension renderers
            ObjectRenderers.Add(new TableRenderer());
            ObjectRenderers.Add(new TaskListRenderer());
        }

        public FlowDocument Document { get; }

        /// <inheritdoc/>
        public override object Render([NotNull] MarkdownObject markdownObject)
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
        public void WriteLeafInline([NotNull] LeafBlock leafBlock)
        {
            if (leafBlock == null) throw new ArgumentNullException(nameof(leafBlock));
            var inline = (Syntax.Inlines.Inline)leafBlock.Inline;
            while (inline != null)
            {
                Write(inline);
                inline = inline.NextSibling;
            }
        }

        /// <summary>
        /// Writes the lines of a <see cref="LeafBlock"/>
        /// </summary>
        /// <param name="leafBlock">The leaf block.</param>
        public void WriteLeafRawLines([NotNull] LeafBlock leafBlock)
        {
            if (leafBlock == null) throw new ArgumentNullException(nameof(leafBlock));
            if (leafBlock.Lines.Lines != null)
            {
                var lines = leafBlock.Lines;
                var slices = lines.Lines;
                for (var i = 0; i < lines.Count; i++)
                {
                    if (i != 0)
                        WriteInline(new LineBreak());

                    WriteText(ref slices[i].Slice);
                }
            }
        }

        internal void Push([NotNull] IAddChild o)
        {
            stack.Push(o);
        }

        internal void Pop()
        {
            var popped = stack.Pop();
            stack.Peek().AddChild(popped);
        }

        internal void WriteBlock([NotNull] Block block)
        {
            stack.Peek().AddChild(block);
        }

        internal void WriteInline([NotNull] Inline inline)
        {
            AddInline(stack.Peek(), inline);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void WriteText(ref StringSlice slice)
        {
            if (slice.Start > slice.End)
                return;

            WriteText(slice.Text, slice.Start, slice.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void WriteText([CanBeNull] string text)
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

        private static void AddInline([NotNull] IAddChild parent, [NotNull] Inline inline)
        {
            parent.AddChild(inline);
        }
    }
}
