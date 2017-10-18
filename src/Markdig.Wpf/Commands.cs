// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System.Windows.Input;

namespace Markdig.Wpf
{
    /// <summary>
    /// List of supported commands.
    /// </summary>
    public static class Commands
    {
        /// <summary>
        /// Routed command for Hyperlink.
        /// </summary>
        public static RoutedCommand Hyperlink { get; } = new RoutedCommand(nameof(Hyperlink), typeof(Commands));
    }
}
