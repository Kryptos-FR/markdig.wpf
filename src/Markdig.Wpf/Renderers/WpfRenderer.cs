// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows.Documents;
using Markdig.Renderers.Wpf;
using Markdig.Renderers.Wpf.Inlines;
using Markdig.Syntax;

namespace Markdig.Renderers
{
    public class WpfRenderer : RendererBase
    {
        public WpfRenderer(FlowDocument document)
        {
            Document = document;

            // Default block renderers
            ObjectRenderers.Add(new CodeBlockRenderer());
            //ObjectRenderers.Add(new ListRenderer());
            ObjectRenderers.Add(new HeadingRenderer());
            //ObjectRenderers.Add(new HtmlBlockRenderer());
            ObjectRenderers.Add(new ParagraphRenderer());
            //ObjectRenderers.Add(new QuoteBlockRenderer());
            //ObjectRenderers.Add(new ThematicBreakRenderer());

            // Default inline renderers
            //ObjectRenderers.Add(new AutolinkInlineRenderer());
            ObjectRenderers.Add(new CodeInlineRenderer());
            ObjectRenderers.Add(new DelimiterInlineRenderer());
            ObjectRenderers.Add(new EmphasisInlineRenderer());
            ObjectRenderers.Add(new LineBreakInlineRenderer());
            //ObjectRenderers.Add(new HtmlInlineRenderer());
            //ObjectRenderers.Add(new HtmlEntityInlineRenderer());
            //ObjectRenderers.Add(new LinkInlineRenderer());
            ObjectRenderers.Add(new LiteralInlineRenderer());
        }

        public FlowDocument Document { get; }

        public override object Render(MarkdownObject markdownObject)
        {
            Write(markdownObject);
            return Document;
        }
    }
}
