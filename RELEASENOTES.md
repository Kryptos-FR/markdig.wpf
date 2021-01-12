# Release Notes

## 0.5.0 - 2021/01/12
  - update to [Markdig 0.22.0](https://github.com/xoofx/markdig/blob/master/changelog.md#0220-05-oct-2020)
  - add support for .NET 5 (Windows-only)
  - add support for SourceLink

## 0.4.0 - 2020/09/12
  - drop support for net40 (#41, previously #25)
  - target netcore3.1
  - relax dependency rule to Markdig  (#42, previously #20)
  - add style for paragraph (part of #40)

## 0.3.1 - 2019/11/04
  - update to [Markdig 0.18.0](https://github.com/lunet-io/markdig/blob/master/changelog.md#0180-24-oct-2019)
  - add .NET Core support (thanks Victor Irzak, see PR #35)
  - add .NET code analyzers

## 0.3.0 - 2019/06/16
  - update to [Markdig 0.17.1](https://github.com/lunet-io/markdig/blob/master/changelog.md#0171-04-july-2019)
  - check and test Emoji support (need to be enabled through extension `UseEmojiAndSmiley(bool)`)
  - make some class customizables (thanks Finn Coordts, see PR #33)

## 0.2.8 - 2019/04/12
  - now supports net40 (#25)

## 0.2.7 - 2019/03/08
  - update to [Markdig 0.16.0](https://github.com/lunet-io/markdig/blob/master/changelog.md#0160-25-feb-2019)
  - switch to new csproj format

## 0.2.6 - 2018/12/24
  - update to [Markdig 0.15.5](https://github.com/lunet-io/markdig/blob/master/changelog.md#0155-11-dec-2018)
  - now requires an exact dependency to Markdig (see issue #20)
  - hyperlinks improvements (thanks Jack Griffiths)

## 0.2.5 - 2018/10/14
  - update to [Markdig 0.15.4](https://github.com/lunet-io/markdig/blob/master/changelog.md#0154-07-oct-2018)
  - add HtmlEntityInlineRenderer (thanks Skymirrh)
  - make WpfRenderer methods public (thanks Łukasz Holetzke)

## 0.2.4 - 2018/05/25
  - update to [Markdig 0.15.0](https://github.com/lunet-io/markdig/blob/master/changelog.md#0150-4-apr-2018)
  - fix space issue between inline elements (thanks Jack Griffiths)

## 0.2.3 - 2017/11/27
  - update to [Markdig 0.14.7](https://github.com/lunet-io/markdig/blob/master/changelog.md#0147-25-nov-2017)
  - add support for emphasis extensions (thanks Thomas Freudenberg)

## 0.2.2 - 2017/10/18 
  - update to [Markdig 0.13.3](https://github.com/lunet-io/markdig/blob/master/changelog.md#0133)
  - fix relative URL for images (thanks David Holsgrove) 

## 0.2.1 - 2017/08/29 
  - update to [Markdig 0.13.1](https://github.com/lunet-io/markdig/blob/master/changelog.md#0131)
  - allow to change the MarkdownPipeline on a MarkdownViewer control 

## 0.2.0 - 2017/06/28 
  - update to [Markdig 0.12.3](https://github.com/lunet-io/markdig/blob/master/changelog.md#0123)
  - Markdig.Xaml and markdig.Wpf projects have been merged (thanks @grokys)
  - support for TaskList extension 
  - partial support for Table extensions

## 0.1.3 - 2016/12/23 
  - update to [Markdig 0.10.4](https://github.com/lunet-io/markdig/blob/master/changelog.md#0104)

## 0.1.2 - 2016/10/16
  - update to [Markdig 0.8.2](https://github.com/lunet-io/markdig/blob/master/changelog.md#082)

## 0.1.1 - 2016/07/24
  - add styling to quote block and thematic break.
  - fix spaces not preserved in code inline or literal inline.
  - fix thematic break was rendered outside of a paragraph.
  - fix hyperlink tooltip.

## 0.1.0 - 2016/07/17
  - initial release
