using System;
using System.Collections.Generic;
using System.Text;
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

        private const double BitratePowerScale = 0.75;

        public Task<int> Run()
        {
            var baselineWidth = 640;
            var baselineHeight = 480;
            var baselinePixelCount = baselineWidth * baselineHeight;
            var baselineBitrate = baselinePixelCount * Options.FrameRate * Options.BitsPerPixel / 1000;

            var pixelCount = Options.Width * Options.Height;
            var pixelScale = (double)pixelCount / baselinePixelCount;
            var bitrate = (int)Math.Ceiling(Math.Pow(pixelScale, BitratePowerScale) * baselineBitrate);

            bitrate = Math.Min(Math.Max(100, bitrate), 100000);

            Console.Error.WriteLine($"{bitrate}kbps");

            return Task.FromResult(0);
        }
    }
}
