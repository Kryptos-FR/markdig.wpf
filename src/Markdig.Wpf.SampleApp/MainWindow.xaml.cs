using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using Markdig.Renderers;


namespace Markdig.Wpf.SampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;

            var markdown = File.ReadAllText("Documents/Markdig-readme.md");
            var document = Markdig.Markdown.Parse(markdown);
            Viewer.Document = (FlowDocument)new WpfRenderer(new FlowDocument()).Render(document);
        }

        private static MarkdownPipeline BuildPipeline()
        {
            return new MarkdownPipelineBuilder()
                .UseSupportedExtensions()
                .Build();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var markdown = File.ReadAllText("Documents/Markdig-readme.md");
            Viewer.Document = Markdown.ToFlowDocument(markdown, BuildPipeline());
        }

        private void OpenHyperlink(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Process.Start(e.Parameter.ToString());
        }
    }
}
