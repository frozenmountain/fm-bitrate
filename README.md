# FM Bitrate CLI

![build](https://github.com/frozenmountain/fm-bitrate/workflows/build/badge.svg) ![license](https://img.shields.io/badge/License-MIT-yellow.svg) ![release](https://img.shields.io/github/v/release/frozenmountain/fm-bitrate.svg)

The FM Bitrate CLI lets you calculate optimal bitrates for live video streams.

Requires .NET Core 3.1 or newer.

## Building

Use `dotnet publish` to create a single, self-contained file for a specific platform/architecture:

### Windows
```
dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true -o win
```

### macOS
```
dotnet publish -r osx-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true -o osx
```

### Linux
```
dotnet publish -r linux-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true -o linux
```

Alternatively, use `dotnet build` to create a platform-agnostic bundle (the .NET Core runtime must be installed):

```
dotnet build
```

Using this approach will generate a library instead of an executable.

Use `dotnet fmbitrate.dll` instead of `fmbitrate` to run it.

## Usage

```
fmbitrate [verb] [options]
```

### Verbs
```
  calculate  Calculates a bitrate.
```

## analyze

The `calculate` verb calculates an optimal bitrate for live video streaming.

### Usage
```
  -w, --width          Required. The width.

  -h, --height         Required. The height.

  -f, --fps            (Default: 30) The frame-rate.

  -b, --bpp            (Default: 0.05) The bits-per-pixel.

  -e, --bitrate-exp    (Default: 0.75) The exponent to use for scaling the bits-per-pixel around
                       the baseline width and height.

  --baseline-width     (Default: 640) The baseline width for scaling the bits-per-pixel.

  --baseline-height    (Default: 480) The baseline height for scaling the bits-per-pixel.

  --min-bitrate        (Default: 100) The minimum bitrate.

  --max-bitrate        (Default: 100000) The maximum bitrate.
```

## Calculations
The primary inputs to the calculation are the video width, height, frame-rate, and bits-per-pixel.

The `bpp` (bits-per-pixel) value is very important, as it controls the density of information in the bitstream. Higher values will result in higher bitrates which, to a point, produce greater clarity and reduced compression artifacts. A value of 0.05 to 0.10 is typical for low motion videos like web cameras. Higher values, up to 0.20 or even higher, are more useful for high-motion videos, e.g. sports broadcasts, or for recording.

The `min-bitrate` and `max-bitrate` put constraints on the output. This is useful for resource planning or for codecs which document minimum or maximum viable bitrates.

The `bitrate-exp` value, together with the `baseline-width`, and `baseline-height`, are used to determine the logarithmic scale to use. Encoder efficiency typically increases as resolution increases, so instead of applying a linear scale, we use an exponent to gently curve higher-resolution bitrates down and lower-resolution bitrates up. The agressiveness of this curve is determined by the bitrate exponent, and the middle point (where the bpp is applied without scaling) is determined by the baseline width and height. Using 0.75 for the exponent is a common value to use, first put forth by [Ben Waggoner](https://www.amazon.com/Compression-Great-Video-Audio-Master-ebook/dp/B00BEGBYUO).

The baseline bitrate is calculated as follows:

> width * height * fps * bpp / 1000 = bitrate (in kbps)

(The division by 1000 converts from bps to kbps.)

With default settings, an input size of 640x480 (the baseline) will result in 461 kbps:

> 640 * 480 * 30 * 0.05 / 1000 = 461

Going above that baseline to an input size of 1280x720 (720p) will result in 1051 kbps (vs. 1383 kbps using a linear scale where the `bitrate-exp` is 1.0):

> (1280 * 720) / (640 * 480) ^ 0.75 * 461 = 1051

Going below that baseline to an input size of 320x240 will result in 163 kbps (vs. 116 kbps using a linear scale where the `bitrate-exp` is 1.0):

> (320 * 240) / (640 * 480) ^ 0.75 * 461 = 163

## Contact

To learn more, visit [frozenmountain.com](https://www.frozenmountain.com).

For inquiries, contact [sales@frozenmountain.com](mailto:sales@frozenmountain.com).

All contents copyright © Frozen Mountain Software.
