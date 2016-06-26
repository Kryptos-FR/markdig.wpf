// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax;

namespace Markdig.Renderers.Wpf
{
    public abstract class WpfObjectRenderer<TObject> : MarkdownObjectRenderer<WpfRenderer, TObject>
        where TObject : MarkdownObject
    {
    }
}
