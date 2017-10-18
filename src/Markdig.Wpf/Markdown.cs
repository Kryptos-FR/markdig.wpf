// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;
using System.IO;
using System.Windows.Documents;
using Markdig.Annotations;
using Markdig.Renderers;
using Markdig.Syntax;

namespace Markdig.Wpf
{
    /// <summary>
    /// Provides methods for parsing a Markdown string to a syntax tree and converting it to other formats.
    /// </summary>
    public static partial class Markdown
    {
        /// <summary>
        /// Converts a Markdown string to a FlowDocument.
        /// </summary>
        /// <param name="markdown">A Markdown text.</param>
        /// <param name="pipeline">The pipeline used for the conversion.</param>
        /// <returns>The result of the conversion</returns>
        /// <exception cref="System.ArgumentNullException">if markdown variable is null</exception>
        [NotNull]
        public static FlowDocument ToFlowDocument([NotNull] string markdown, MarkdownPipeline pipeline = null)
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

        /// <summary>
        /// Converts a Markdown string to XAML.
        /// </summary>
        /// <param name="markdown">A Markdown text.</param>
        /// <param name="pipeline">The pipeline used for the conversion.</param>
        /// <returns>The result of the conversion</returns>
        /// <exception cref="ArgumentNullException">if markdown variable is null</exception>
        [NotNull]
        public static string ToXaml([NotNull] string markdown, [CanBeNull] MarkdownPipeline pipeline = null)
        {
            if (markdown == null) throw new ArgumentNullException(nameof(markdown));
            var writer = new StringWriter();
            ToXaml(markdown, writer, pipeline);
            return writer.ToString();
        }

        /// <summary>
        /// Converts a Markdown string to XAML and output to the specified writer.
        /// </summary>
        /// <param name="markdown">A Markdown text.</param>
        /// <param name="writer">The destination <see cref="TextWriter"/> that will receive the result of the conversion.</param>
        /// <param name="pipeline">The pipeline used for the conversion.</param>
        /// <returns>The Markdown document that has been parsed</returns>
        /// <exception cref="ArgumentNullException">if reader or writer variable are null</exception>
        public static MarkdownDocument ToXaml([NotNull] string markdown, [NotNull] TextWriter writer,
            [CanBeNull] MarkdownPipeline pipeline = null)
        {
            if (markdown == null) throw new ArgumentNullException(nameof(markdown));
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            pipeline = pipeline ?? new MarkdownPipelineBuilder().Build();

            // We override the renderer with our own writer
            var renderer = new XamlRenderer(writer);
            pipeline.Setup(renderer);

            var document = Markdig.Markdown.Parse(markdown, pipeline);
            renderer.Render(document);
            writer.Flush();

            return document;
        }
    }
}
