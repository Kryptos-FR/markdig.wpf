// Copyright (c) 2016 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.IO;

namespace Markdig.Xaml.ConsoleApp
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            using (var stream = File.OpenRead("Documents/Markdig-readme.md"))
            {
                using (var reader = new StreamReader(stream))
                {
                    var markdown = reader.ReadToEnd();
                    var xaml = Wpf.Markdown.ToXaml(markdown);
                    // Do whatever you want with the produced XAML...
                }
            }
        }
    }
}
