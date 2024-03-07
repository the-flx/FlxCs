[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)
[![Release](https://img.shields.io/github/tag/jcs090218/FlxCs.svg?label=release&logo=github)](https://github.com/jcs090218/FlxCs/releases/latest)
[![Unity Engine](https://img.shields.io/badge/unity-2023.1.11f1-black.svg?style=flat&logo=unity)](https://unity3d.com/get-unity/download/archive)
[![.NET](https://img.shields.io/badge/.NET-2.0-blueviolet.svg)](https://learn.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-1-0)
[![Nuget DT](https://img.shields.io/nuget/dt/FlxCs?logo=nuget&logoColor=49A2E6)](https://www.nuget.org/packages/FlxCs/)

# FlxCs
> Rewrite emacs-flx in C#

[![Compile](https://github.com/jcs090218/FlxCs/actions/workflows/compile.yml/badge.svg)](https://github.com/jcs090218/FlxCs/actions/workflows/compile.yml)

## 🔨 Usage

```cs
Score score = Flx.Score("switch-to-buffer", "stb");

Console.Write(score.score);  // 237
```

## 📂 Example

- [Mx][] - M-x for Unity

## 🔍 See Also

- [flx][] - Original algorithm
- [flx-rs][] - Rewrite emacs-flx in Rust for dynamic modules
- [flx-ts][] - Rewrite emacs-flx in TypeScript, with added support for JavaScript

## ⚜️ License

`FlxCs` is distributed under the terms of the MIT license.

See [LICENSE](./LICENSE) for details.


<!-- Links -->

[Mx]: https://github.com/jcs090218/Unity.Mx

[flx]: https://github.com/lewang/flx
[flx-rs]: https://github.com/jcs090218/flx-rs
[flx-ts]: https://github.com/jcs090218/flx-ts
