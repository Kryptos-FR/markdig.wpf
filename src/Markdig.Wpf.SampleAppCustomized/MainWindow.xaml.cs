using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Markdig.Wpf.SampleAppCustomized
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool? _presentationState = false;

        /// <summary>
        /// Represents the IsChecked 3-State ToggleButton that changes the presentation
        /// </summary>
        public bool? PresentationState
        {
            get => _presentationState; 
            set => ToggleExtensions(value);
        }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var path = "../../../../../Documents/Markdig-readme.md";
            Viewer.UCRootPath = path;
            Viewer.Markdown = File.ReadAllText(path);
        }

        private void OpenHyperlink(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Process.Start(e.Parameter.ToString());
        }

        private void ClickOnImage(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MessageBox.Show($"URL: {e.Parameter}");
        }

        private void ToggleExtensions(bool? newValue)
        {
            _presentationState = newValue;
            
            // Setting the custom renderer first since the pipeline property will trigger refresh
            Viewer.SetCustomRenderer(newValue);
            Viewer.Pipeline = newValue switch
            {
                // Even if we include a custom renderer, we still have to enable the applicable
                // Pipeline extensions that handle that Markdig type.
                // In this case, UsePipeTables() enables the use of ColumnScalingTableRenderer
                true => new MarkdownPipelineBuilder().UsePipeTables().Build(),
                false => new MarkdownPipelineBuilder().UseSupportedExtensions().Build(),
                _ => new MarkdownPipelineBuilder().Build()
            };
        }

    }
}
