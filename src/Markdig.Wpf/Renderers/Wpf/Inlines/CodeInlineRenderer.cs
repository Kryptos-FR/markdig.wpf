// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows.Documents;
using Markdig.Syntax.Inlines;
using Markdig.Wpf;

namespace Markdig.Renderers.Wpf.Inlines
{
    public class CodeInlineRenderer : WpfObjectRenderer<CodeInline>
    {
        protected override void Write(WpfRenderer renderer, CodeInline obj)
        {
            var run = new Run(obj.Content);
            run.SetResourceReference(Paragraph.StyleProperty, Styles.CodeStyleKey);
            renderer.WriteInline(run);
        }
    }
}
