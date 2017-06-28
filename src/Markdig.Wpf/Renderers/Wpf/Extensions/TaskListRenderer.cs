// Copyright (c) 2016-2017 Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System.Windows.Controls;
using System.Windows.Documents;
using Markdig.Annotations;
using Markdig.Extensions.TaskLists;
using Markdig.Wpf;

namespace Markdig.Renderers.Wpf.Extensions
{
    public class TaskListRenderer : WpfObjectRenderer<TaskList>
    {
        protected override void Write([NotNull] WpfRenderer renderer, [NotNull] TaskList taskList)
        {
            var checkBox = new CheckBox
            {
                IsEnabled = false,
                IsChecked = taskList.Checked,
            };

            checkBox.SetResourceReference(Paragraph.StyleProperty, Styles.TaskListStyleKey);
            renderer.WriteInline(new InlineUIContainer(checkBox));
        }
    }
}
