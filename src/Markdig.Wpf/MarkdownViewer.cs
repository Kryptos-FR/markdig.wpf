// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Markdig.Wpf
{
    /// <summary>
    /// A markdown viewer control.
    /// </summary>
    public class MarkdownViewer : Control
    {
        protected static readonly MarkdownPipeline DefaultPipeline = new MarkdownPipelineBuilder().UseSupportedExtensions().Build();

        private static readonly DependencyPropertyKey DocumentPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Document), typeof(FlowDocument), typeof(MarkdownViewer), new FrameworkPropertyMetadata());

        /// <summary>
        /// Defines the <see cref="Document"/> property.
        /// </summary>
        public static readonly DependencyProperty DocumentProperty = DocumentPropertyKey.DependencyProperty;

        /// <summary>
        /// Defines the <see cref="Markdown"/> property.
        /// </summary>
        public static readonly DependencyProperty MarkdownProperty =
            DependencyProperty.Register(nameof(Markdown), typeof(string), typeof(MarkdownViewer), new FrameworkPropertyMetadata(MarkdownChanged));

        /// <summary>
        /// Defines the <see cref="Markdown"/> property.
        /// </summary>
        public static readonly DependencyProperty PipelineProperty =
            DependencyProperty.Register(nameof(Pipeline), typeof(MarkdownPipeline), typeof(MarkdownViewer), new FrameworkPropertyMetadata(PipelineChanged));

        /// <summary>
        /// Defines the MarkdownViewer.AnchorName attached property used for in-document linking (e.g. "#my-id")
        /// </summary>
        public static readonly DependencyProperty AnchorNameProperty = DependencyProperty.RegisterAttached(
            "AnchorName", typeof(string), typeof(MarkdownViewer), new PropertyMetadata(default(string)));

        public static void SetAnchorName(DependencyObject element, string value)
        {
            element.SetValue(AnchorNameProperty, value);
        }

        public static string GetAnchorName(DependencyObject element)
        {
            return (string)element.GetValue(AnchorNameProperty);
        }
        static MarkdownViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkdownViewer), new FrameworkPropertyMetadata(typeof(MarkdownViewer)));
        }

        private FlowDocumentScrollViewer? docViewer;

        /// <summary>
        /// Gets the flow document to display.
        /// </summary>
        public FlowDocument? Document
        {
            get { return (FlowDocument)GetValue(DocumentProperty); }
            protected set { SetValue(DocumentPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the markdown to display.
        /// </summary>
        public string? Markdown
        {
            get { return (string)GetValue(MarkdownProperty); }
            set { SetValue(MarkdownProperty, value); }
        }

        /// <summary>
        /// Gets or sets the markdown pipeline to use.
        /// </summary>
        public MarkdownPipeline Pipeline
        {
            get { return (MarkdownPipeline)GetValue(PipelineProperty); }
            set { SetValue(PipelineProperty, value); }
        }

        private static void MarkdownChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (MarkdownViewer)sender;
            control.RefreshDocument();
        }

        private static void PipelineChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (MarkdownViewer)sender;
            control.RefreshDocument();
        }

        public MarkdownViewer()
        {
            CommandBindings.Add(new CommandBinding(Commands.Navigate, NavigateCommandExecuted));
        }

        private void NavigateCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            string url = e.Parameter?.ToString() ?? "";
            if (url.Length > 1 && url[0] == '#')
            {
                string anchorName = url.Substring(1);
                e.Handled = NavigateTo(anchorName);
            }
        }

        protected virtual void RefreshDocument()
        {
            Document = Markdown != null ? Wpf.Markdown.ToFlowDocument(Markdown, Pipeline ?? DefaultPipeline) : null;
        }

        public override void OnApplyTemplate()
        {
            docViewer =  GetTemplateChild("PART_DocViewer") as FlowDocumentScrollViewer;
            
            base.OnApplyTemplate();
        }

        public bool NavigateTo(string anchorName)
        {
            if (Document == null)
                throw new InvalidOperationException("No rendered content found");
            
            foreach (var block in Document.Blocks)
            {
                string blockAnchorName = GetAnchorName(block);
                if (String.Equals(blockAnchorName, anchorName, StringComparison.OrdinalIgnoreCase))
                {
                    return ScrollIntoView(block);
                }
            }

            return false;
        }

        private bool ScrollIntoView(Block block)
        {
            if (docViewer == null)
                return false;
            double top = block.ContentStart.GetCharacterRect(LogicalDirection.Forward).Top;
            var scrollViewer = FindVisualChild<ScrollViewer>(docViewer);
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToVerticalOffset(top);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a visual child of the specific type.
        /// </summary>
        /// <typeparam name="T">The type of child to find.</typeparam>
        /// <param name="element">Where to start the "search".</param>
        /// <returns>The child or null.</returns>
        public static T? FindVisualChild<T>(
            DependencyObject element) where T : class
        {
            if (element is T retVal)
                return retVal;
            
            int childCnt = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childCnt; ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(element, i);

                var result = FindVisualChild<T>(child);
                if (result != null)
                    return result;
            }

            return null;
        }
    }
}
