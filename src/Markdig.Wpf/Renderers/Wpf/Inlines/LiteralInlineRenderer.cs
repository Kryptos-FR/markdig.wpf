// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Wpf.Inlines
{
    /// <summary>
    /// A WPF renderer for a <see cref="LiteralInline"/>.
    /// </summary>
    /// <seealso cref="Markdig.Renderers.Wpf.WpfObjectRenderer{Markdig.Syntax.Inlines.LiteralInline}" />
    public class LiteralInlineRenderer : WpfObjectRenderer<LiteralInline>
    {
        protected override void Write(WpfRenderer renderer, LiteralInline obj)
        {
            throw new NotImplementedException();
        }
    }
}
