using CommandLine;

namespace FM.Bitrate
{
    [Verb("calculate", HelpText = "Calculates a bitrate.")]
    class CalculateOptions
    {
        [Option('w', "width", Required = true, HelpText = "The width.")]
        public int Width { get; set; }

        [Option('h', "height", Required = true, HelpText = "The height.")]
        public int Height { get; set; }

        [Option('f', "fps", Required = false, Default = 30.0, HelpText = "The frame-rate.")]
        public double FrameRate { get; set; }

        [Option('b', "bpp", Required = false, Default = 0.05, HelpText = "The bits-per-pixel.")]
        public double BitsPerPixel { get; set; }

        [Option("min-bitrate", Required = false, Default = 100, HelpText = "The minimum bitrate.")]
        public int MinimumBitrate { get; set; }

        [Option("max-bitrate", Required = false, Default = 100000, HelpText = "The maximum bitrate.")]
        public int MaximumBitrate { get; set; }

        [Option("bitrate-exp", Required = false, Default = 0.75, HelpText = "The exponent to use for scaling the bits-per-pixel around the baseline width and height.")]
        public double BitrateExponent { get; set; }

        [Option("baseline-width", Required = false, Default = 640, HelpText = "The baseline width for scaling the bits-per-pixel.")]
        public int BaselineWidth { get; set; }

        [Option("baseline-height", Required = false, Default = 480, HelpText = "The baseline height for scaling the bits-per-pixel.")]
        public int BaselineHeight { get; set; }
    }
}
