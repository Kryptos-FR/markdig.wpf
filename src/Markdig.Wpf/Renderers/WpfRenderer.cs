// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System.Windows.Documents;
using Markdig.Helpers;
using Markdig.Renderers.Wpf;
using Markdig.Renderers.Wpf.Inlines;
using System.Runtime.CompilerServices;

namespace Markdig.Renderers
{
    public class WpfRenderer : RendererBase
    {
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
            //ObjectRenderers.Add(new ParagraphRenderer());
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

        public WpfRenderer WriteBlock(Block block)
        {
            // TODO
            return this;
        }

        public WpfRenderer WriteInline(Inline inline)
        {
            // TODO
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WpfRenderer WriteText(ref StringSlice slice)
        {
            if (slice.Start > slice.End)
                return this;

            return WriteText(slice.Text, slice.Start, slice.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WpfRenderer WriteText(string text)
        {
            return WriteInline(new Run(text));
        }

        public WpfRenderer WriteText(string text, int offset, int length)
        {
            if (text == null)
                return this;
            
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
            return this;
        }

        /// <inheritdoc/>
        public override object Render(Syntax.MarkdownObject markdownObject)
        {
            Write(markdownObject);
            return Document;
        }
    }
}
