using System;
using System.Threading.Tasks;

namespace FM.Bitrate
{
    class Calculator
    {
        public CalculateOptions Options { get; private set; }

        public Calculator(CalculateOptions options)
        {
            Options = options;
        }

        public Task<int> Run()
        {
            var baselinePixelCount = Options.BaselineWidth * Options.BaselineHeight;
            var baselineBitrate = baselinePixelCount * Options.FrameRate * Options.BitsPerPixel / 1000;

            var pixelCount = Options.Width * Options.Height;
            var pixelScale = (double)pixelCount / baselinePixelCount;
            var bitrate = (int)Math.Ceiling(Math.Pow(pixelScale, Options.BitrateExponent) * baselineBitrate);

            bitrate = Math.Min(Math.Max(Options.MinimumBitrate, bitrate), Options.MaximumBitrate);

            Console.Error.WriteLine($"{bitrate} kbps");

            return Task.FromResult(0);
        }
    }
}
