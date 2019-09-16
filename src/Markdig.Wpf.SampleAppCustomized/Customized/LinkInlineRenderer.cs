using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Markdig.Annotations;
using Markdig.Renderers;
using Markdig.Syntax.Inlines;

namespace Markdig.Wpf.SampleAppCustomized.Customized
{
    public class LinkInlineRenderer : Markdig.Renderers.Wpf.Inlines.LinkInlineRenderer
    {
        private string _linkpath;

        public LinkInlineRenderer(string linkpath)
        {
            _linkpath = linkpath;
        }

        protected override void Write(Renderers.WpfRenderer renderer, LinkInline link)
        {
            if (link.IsImage)
                link.Url = _linkpath + link.Url;

            base.Write(renderer, link);
        }
    }
}
