[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)
[![Release](https://img.shields.io/github/tag/the-flx/FlxCs.svg?label=release&logo=github)](https://github.com/the-flx/FlxCs/releases/latest)
[![Unity Engine](https://img.shields.io/badge/unity-2023.1.11f1-black.svg?style=flat&logo=unity)](https://unity3d.com/get-unity/download/archive)
[![.NET](https://img.shields.io/badge/.NET-2.0-blueviolet.svg)](https://learn.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-1-0)
[![Nuget DT](https://img.shields.io/nuget/dt/FlxCs?logo=nuget&logoColor=49A2E6)](https://www.nuget.org/packages/FlxCs/)

# FlxCs
> Rewrite emacs-flx in C#

[![Compile](https://github.com/the-flx/FlxCs/actions/workflows/compile.yml/badge.svg)](https://github.com/the-flx/FlxCs/actions/workflows/compile.yml)

## 🔨 Usage

```cs
Result result = Flx.Score("switch-to-buffer", "stb");

Console.Write(result.score);  // 237
```

## 🛠️ Development

For testing, we use the [Visual Studio][] built-in testing library.

See Microsoft's support page for more information: [Walkthrough: Create and run unit tests for managed code](https://learn.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code).

## 📂 Example

- [Mx][] - M-x for Unity

## ⚜️ License

`FlxCs` is distributed under the terms of the MIT license.

See [`LICENSE`](./LICENSE) for details.


<!-- Links -->

[Mx]: https://github.com/jcs090218/Unity.Mx

[flx]: https://github.com/lewang/flx

[Visual Studio]: https://visualstudio.microsoft.com/
