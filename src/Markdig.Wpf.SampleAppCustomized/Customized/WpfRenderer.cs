namespace Markdig.Wpf.SampleAppCustomized.Customized
{
    /// <summary>
    /// Create a custom Renderer instead of using the one in the Markdig.Wpf package 
    /// </summary>
    public class WpfRenderer : Markdig.Renderers.WpfRenderer
    {
        private readonly Renderers.IMarkdownObjectRenderer[] _injections;

        /// <summary>
        /// Initializes the WPF renderer
        /// </summary>
        /// <param name="injectRenderers">custom renderer injection list</param>
        public WpfRenderer(params Renderers.IMarkdownObjectRenderer[] injectRenderers) : base()
        {
            _injections = injectRenderers;
        }

        /// <summary>
        /// Load our custom renderer's before the base versions. By doing this, our renderers can
        /// handle/continue on that specific Markdig type before ever reaching the default renderer
        /// </summary>
        protected override void LoadRenderers()
        {
            if (_injections.Length > 0)
            {
                foreach (var renderer in _injections)
                    ObjectRenderers.Add(renderer); 
            }

            base.LoadRenderers();
        }
    }
}
