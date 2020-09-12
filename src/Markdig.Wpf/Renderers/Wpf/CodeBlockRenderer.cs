// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Documents;
using Markdig.Syntax;
using Markdig.Wpf;

namespace Markdig.Renderers.Wpf
{
    public class CodeBlockRenderer : WpfObjectRenderer<CodeBlock>
    {
        protected override void Write(WpfRenderer renderer, CodeBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));

            var paragraph = new Paragraph();
            paragraph.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeBlockStyleKey);
            renderer.Push(paragraph);
            renderer.WriteLeafRawLines(obj);
            renderer.Pop();
        }
    }
}
