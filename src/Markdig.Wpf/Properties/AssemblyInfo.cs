// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System.Reflection;
using System.Resources;
using System.Windows;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Markdig.Wpf")]
[assembly: AssemblyDescription("A WPF library for lunet-io/markdig")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("Nicolas Musset")]
[assembly: AssemblyProduct("Markdig.Wpf")]
[assembly: AssemblyCopyright("Copyright © Nicolas Musset 2016-2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en")]

[assembly: AssemblyVersion(Markdig.Wpf.Markdown.Version)]
[assembly: AssemblyFileVersion(Markdig.Wpf.Markdown.Version)]
[assembly: ThemeInfo(ResourceDictionaryLocation.SourceAssembly, ResourceDictionaryLocation.SourceAssembly)]

namespace Markdig.Wpf
{
    public static partial class Markdown
    {
        /// <summary>
        /// Version of this library.
        /// </summary>
        public const string Version = "0.2.5";
    }
}