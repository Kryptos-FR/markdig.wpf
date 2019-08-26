using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Markdig.Wpf.SampleAppCustomized.Customized
{
    /// <summary>
    /// Usually the image paths in the Markdown must be relative to the application or absolute.
    /// This classes make it possible to change the root path.
    /// </summary>
    public class MarkdownViewer : Markdig.Wpf.MarkdownViewer
    {
        /// <summary>
        /// Root path for linking images
        /// </summary>
        public string UCRootPath
        {
            get { return (string)GetValue(UCRootPathProperty); }
            set { SetValue(UCRootPathProperty, value); }
        }

        /// <summary>
        /// Registered DependencyProperty for <see cref="UCRootPath"/>.
        /// </summary>
        public static readonly DependencyProperty UCRootPathProperty =
            DependencyProperty.Register(nameof(UCRootPath), typeof(string), typeof(MarkdownViewer), new PropertyMetadata(string.Empty));


        protected override void RefreshDocument()
        {
            // In some cases the path is not updated fast enough, so we force it
            this.GetBindingExpression(UCRootPathProperty)?.UpdateTarget();

            var path = UCRootPath;
            if (!string.IsNullOrEmpty(path) && !path.EndsWith("/"))
                path = path.Remove(path.LastIndexOf('/') + 1);
            Document = Markdown != null ? Markdig.Wpf.Markdown.ToFlowDocument(Markdown, Pipeline ?? DefaultPipeline, new Customized.WpfRenderer(path)) : null;
        }
    }
}
