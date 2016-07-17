using System.Diagnostics;
using Markdig.Renderers;
using System.Windows;
using System.Windows.Documents;

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

            var document = Markdig.Markdown.Parse(Test);
            Viewer.Document = (FlowDocument)new WpfRenderer(new FlowDocument()).Render(document);
        }

        private static readonly string Test = 
@"
*Text en italique*
";

        private void OpenHyperlink(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Process.Start(e.Parameter.ToString());
        }
    }
}
