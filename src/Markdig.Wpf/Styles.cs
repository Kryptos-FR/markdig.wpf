// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System.Windows;

namespace Markdig.Wpf
{
    /// <summary>
    /// List of supported styles.
    /// </summary>
    public static class Styles
    {
        /// <summary>
        /// Resource Key for the CodeStyle.
        /// </summary>
        public static ComponentResourceKey CodeStyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(CodeStyleKey));

        /// <summary>
        /// Resource Key for the CodeBlockStyle.
        /// </summary>
        public static ComponentResourceKey CodeBlockStyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(CodeBlockStyleKey));

        /// <summary>
        /// Resource Key for the DocumentStyle.
        /// </summary>
        public static ComponentResourceKey DocumentStyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(DocumentStyleKey));

        /// <summary>
        /// Resource Key for the Heading1Style.
        /// </summary>
        public static ComponentResourceKey Heading1StyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(Heading1StyleKey));

        /// <summary>
        /// Resource Key for the Heading2Style.
        /// </summary>
        public static ComponentResourceKey Heading2StyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(Heading2StyleKey));

        /// <summary>
        /// Resource Key for the Heading3Style.
        /// </summary>
        public static ComponentResourceKey Heading3StyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(Heading3StyleKey));

        /// <summary>
        /// Resource Key for the Heading4Style.
        /// </summary>
        public static ComponentResourceKey Heading4StyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(Heading4StyleKey));

        /// <summary>
        /// Resource Key for the Heading5Style.
        /// </summary>
        public static ComponentResourceKey Heading5StyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(Heading5StyleKey));

        /// <summary>
        /// Resource Key for the Heading6Style.
        /// </summary>
        public static ComponentResourceKey Heading6StyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(Heading6StyleKey));

        /// <summary>
        /// Resource Key for the ImageStyle.
        /// </summary>
        public static ComponentResourceKey ImageStyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(ImageStyleKey));

        /// <summary>
        /// Resource Key for the QuoteBlockStyle.
        /// </summary>
        public static ComponentResourceKey QuoteBlockStyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(QuoteBlockStyleKey));

        /// <summary>
        /// Resource Key for the TableStyle.
        /// </summary>
        public static ComponentResourceKey TableStyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(TableStyleKey));

        /// <summary>
        /// Resource Key for the TableCellStyle.
        /// </summary>
        public static ComponentResourceKey TableCellStyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(TableCellStyleKey));

        /// <summary>
        /// Resource Key for the TableHeaderStyle.
        /// </summary>
        public static ComponentResourceKey TableHeaderStyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(TableHeaderStyleKey));

        /// <summary>
        /// Resource Key for the TaskListStyle.
        /// </summary>
        public static ComponentResourceKey TaskListStyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(TaskListStyleKey));

        /// <summary>
        /// Resource Key for the ThematicBreakStyle.
        /// </summary>
        public static ComponentResourceKey ThematicBreakStyleKey { get; } = new ComponentResourceKey(typeof(Styles), nameof(ThematicBreakStyleKey));
    }
}
