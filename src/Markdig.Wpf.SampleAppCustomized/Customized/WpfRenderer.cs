namespace Markdig.Wpf.SampleAppCustomized.Customized
{
    public class WpfRenderer : Markdig.Renderers.WpfRenderer
    {
        private string _linkpath;

        /// <summary>
        /// Initializes the WPF renderer
        /// </summary>
        /// <param name="linkpath">image path for the custom LinkInlineRenderer</param>
        public WpfRenderer(string linkpath) : base()
        {
            _linkpath = linkpath;
        }

        /// <summary>
        /// Load first the custom renderer's
        /// </summary>
        protected override void LoadRenderers()
        {
            ObjectRenderers.Add(new LinkInlineRenderer(_linkpath));
            base.LoadRenderers();
        }
    }
}
