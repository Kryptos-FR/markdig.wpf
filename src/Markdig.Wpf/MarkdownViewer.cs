using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Markdig.Wpf
{
    /// <summary>
    /// A markdown viewer control.
    /// </summary>
    public class MarkdownViewer : Control
    {
        private static readonly DependencyPropertyKey DocumentPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(Document),
                typeof(FlowDocument),
                typeof(MarkdownViewer),
                new FrameworkPropertyMetadata());

        /// <summary>
        /// Defines the <see cref="Document"/> property.
        /// </summary>
        public static readonly DependencyProperty DocumentProperty = DocumentPropertyKey.DependencyProperty;

        /// <summary>
        /// Defines the <see cref="Markdown"/> property.
        /// </summary>
        public static readonly DependencyProperty MarkdownProperty =
            DependencyProperty.Register(
                nameof(Markdown),
                typeof(string),
                typeof(MarkdownViewer),
                new FrameworkPropertyMetadata(MarkdownChanged));

        static MarkdownViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(MarkdownViewer),
                new FrameworkPropertyMetadata(typeof(MarkdownViewer)));
        }

        /// <summary>
        /// Gets the flow document to display.
        /// </summary>
        public FlowDocument Document
        {
            get { return (FlowDocument)GetValue(DocumentProperty); }
            private set { SetValue(DocumentPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the markdown to display.
        /// </summary>
        public string Markdown
        {
            get { return (string)GetValue(MarkdownProperty); }
            set { SetValue(MarkdownProperty, value); }
        }

        private static void MarkdownChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var s = (string)e.NewValue;
            var control = (MarkdownViewer)sender;

            if (s != null)
            {
                var pipeline = new MarkdownPipelineBuilder().UseSupportedExtensions().Build();
                control.Document = Wpf.Markdown.ToFlowDocument(s, pipeline);
            }
            else
            {
                control.Document = null;
            }
        }
    }
}
