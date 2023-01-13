using System;
using System.Windows;

namespace Markdig.Wpf.SampleAppCustomized.Customized
{
    /// <summary>
    /// Usually the image paths in the Markdown must be relative to the application or absolute.
    /// This class utilizes a custom renderer and facilitates changing the root path.
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
            Document = Markdown is null
                     ? null 
                     : Markdig.Wpf.Markdown.ToFlowDocument(Markdown, Pipeline ?? DefaultPipeline, _renderer);
        }
        
        
        private WpfRenderer? _renderer = null;
        public void SetCustomRenderer(bool? state)
        {
            // For obvious contrast, we only provide a custom renderer in 1 of the 3 presentation states
            if (state != true)
            {
                _renderer = null;
                return;
            }
            
            // In some cases the UCRootPath is not updated fast enough, so we force it
            this.GetBindingExpression(UCRootPathProperty)?.UpdateTarget();
            
            var path = UCRootPath;
            if (!string.IsNullOrEmpty(path) && !path.EndsWith("/", StringComparison.Ordinal))
                path = path.Remove(path.LastIndexOf('/') + 1);
            
            _renderer = new WpfRenderer(new LinkInlineRenderer(path), new ColumnScalingTableRenderer());
        }
    }
}
