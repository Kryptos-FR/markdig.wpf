// Copyright (c) Nicolas Musset. All rights reserved.
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

        /// <summary>
        /// Routed command for Images.
        /// </summary>
        public static RoutedCommand Image { get; } = new RoutedCommand(nameof(Image), typeof(Commands));

        /// <summary>
        /// Routed command for navigating to a heading in a document. Command parameter contains the heading id
        /// </summary>
        public static RoutedCommand Navigate { get; } = new RoutedCommand(nameof(Navigate), typeof(Commands));
    }
}
