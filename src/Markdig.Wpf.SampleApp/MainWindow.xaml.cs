using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Markdig.Wpf.SampleApp
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
            Viewer.Markdown = File.ReadAllText("Documents/Markdig-readme.md");
        }

        private void OpenHyperlink(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Process.Start(e.Parameter.ToString());
        }

        private void ToggleExtensionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            useExtensions = !useExtensions;
            Viewer.Pipeline = useExtensions ? new MarkdownPipelineBuilder().UseSupportedExtensions().Build() : new MarkdownPipelineBuilder().Build();
        }
    }
}
