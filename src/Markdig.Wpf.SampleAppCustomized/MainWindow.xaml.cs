using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Markdig.Wpf.SampleAppCustomized
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool useExtensions = true;

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

        private void ToggleExtensionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            useExtensions = !useExtensions;
            Viewer.Pipeline = useExtensions ? new MarkdownPipelineBuilder().UseSupportedExtensions().Build() : new MarkdownPipelineBuilder().Build();
        }


    }
}
