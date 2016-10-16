# Markdig-WPF
A WPF port of [lunet-io/markdig](https://github.com/lunet-io/markdig)

I split the project into two separate parts:
- [a XAML renderer](https://github.com/Kryptos-FR/markdig-wpf/tree/master/src/Markdig.Xaml)
- [a WPF renderer](https://github.com/Kryptos-FR/markdig-wpf/tree/master/src/Markdig.Wpf)

The XAML renderer outputs a string in a similar way as the HTML renderer. This string can then be saved into a file or parsed by an application.

[Markdig.Xaml.SampleApp](https://github.com/Kryptos-FR/markdig-wpf/tree/master/src/Markdig.Xaml.SampleApp) illustrates a way to utilize the parsed XAML at runtime. It should be fine for small documents but might not be the best way for bigger one.

*The WPF renderer is still under developement and has yet to reach a workable state.*

## Features

Supports all standard features from Markdig (i.e. fully CommonMark compliant).

Additionally, the following extensions are supported:

- **Auto-links**
