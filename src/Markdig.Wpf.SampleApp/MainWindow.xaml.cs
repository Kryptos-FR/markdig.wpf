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

            var foo = FindResource(Styles.Heading1StyleKey);

            var document = Markdig.Markdown.Parse(Properties.Resources.spec);
            Viewer.Document = (FlowDocument)new WpfRenderer(new FlowDocument()).Render(document);
        }

        private void OpenHyperlink(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Process.Start(e.Parameter.ToString());
        }
    }
}
