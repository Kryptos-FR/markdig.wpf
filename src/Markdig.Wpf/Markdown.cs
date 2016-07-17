// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows.Documents;
using Markdig.Renderers;

namespace Markdig.Wpf
{
    public static partial class Markdown
    {
        /// <summary>
        /// Converts a Markdown string to a FlowDocument.
        /// </summary>
        /// <param name="markdown">A Markdown text.</param>
        /// <param name="pipeline">The pipeline used for the conversion.</param>
        /// <returns>The result of the conversion</returns>
        /// <exception cref="System.ArgumentNullException">if markdown variable is null</exception>
        public static FlowDocument ToFlowDocument(string markdown, MarkdownPipeline pipeline = null)
        {
            if (markdown == null) throw new ArgumentNullException(nameof(markdown));
            pipeline = pipeline ?? new MarkdownPipelineBuilder().Build();

            // We override the renderer with our own writer
            var result = new FlowDocument();
            var renderer = new WpfRenderer(result);
            pipeline.Setup(renderer);

            var document = Markdig.Markdown.Parse(markdown, pipeline);
            renderer.Render(document);

            return result;
        }
    }
}
