// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Documents;

using Markdig.Syntax.Inlines;
using Markdig.Wpf;

namespace Markdig.Renderers.Wpf.Inlines
{
    public class CodeInlineRenderer : WpfObjectRenderer<CodeInline>
    {
        protected override void Write(WpfRenderer renderer, CodeInline obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var run = new Run(obj.Content);
            run.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeStyleKey);
            renderer.WriteInline(run);
        }
    }
}
