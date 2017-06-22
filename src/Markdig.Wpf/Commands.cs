using System.Windows.Input;

namespace Markdig.Wpf
{
    public static class Commands
    {
        public static RoutedCommand Hyperlink { get; } = new RoutedCommand(nameof(Hyperlink), typeof(Commands));
    }
}
