# Markdig-WPF [![NuGet](https://img.shields.io/nuget/v/Markdig.Wpf.svg?logo=nuget)](https://www.nuget.org/packages/Markdig.wpf/) [![NuGet](https://img.shields.io/nuget/dt/Markdig.Wpf.svg)](https://www.nuget.org/stats/packages/Markdig.Wpf?groupby=Version)
A WPF library for [lunet-io/markdig](https://github.com/lunet-io/markdig)

The project is split into two parts:
- [a WPF renderer](https://github.com/Kryptos-FR/markdig-wpf/blob/master/src/Markdig.Wpf/Renderers/WpfRenderer.cs)
- [a XAML renderer](https://github.com/Kryptos-FR/markdig-wpf/blob/master/src/Markdig.Wpf/Renderers/XamlRenderer.cs)

The WPF renderer allows you to transform markdown text to an equivalent FlowDocument that can then be used in a WPF control. For convenience an implementation of such control is given in [MarkdownViewer](https://github.com/Kryptos-FR/markdig-wpf/blob/master/src/Markdig.Wpf/MarkdownViewer.cs).

The XAML renderer outputs a string in a similar way as the HTML renderer. This string can then be saved into a file or parsed by an application. It is less complete compared to the WPF renderer.

[Markdig.Xaml.SampleApp](https://github.com/Kryptos-FR/markdig-wpf/tree/master/src/Markdig.Xaml.SampleApp) illustrates a way to utilize the parsed XAML at runtime. It should be fine for small documents but might not be the best way for bigger one.


## Features

Supports all standard features from Markdig (i.e. fully CommonMark compliant).

Additionally, the following extensions are supported:
- **Auto-links**
- **Task lists** (WPF renderer only)
- **Tables** (partial support of grid and pipe tables) (WPF renderer only)
- **Extra emphasis**
