// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Wpf.Inlines
{
    /// <summary>
    /// A WPF renderer for a <see cref="DelimiterInline"/>.
    /// </summary>
    /// <seealso cref="Markdig.Renderers.Wpf.WpfObjectRenderer{Markdig.Syntax.Inlines.DelimiterInline}" />
    public class DelimiterInlineRenderer : WpfObjectRenderer<DelimiterInline>
    {
        protected override void Write(WpfRenderer renderer, DelimiterInline obj)
        {
            throw new NotImplementedException();
        }
    }
}
