using CommandLine;

namespace FM.Bitrate
{
    [Verb("calculate", HelpText = "Calculates a bitrate.")]
    class CalculateOptions
    {
        [Option('w', "width", Required = true, HelpText = "The video width.")]
        public int Width { get; set; }

        [Option('h', "height", Required = true, HelpText = "The video height.")]
        public int Height { get; set; }

        [Option('f', "frame-rate", Required = false, Default = 30.0, HelpText = "The video frame-rate.")]
        public double FrameRate { get; set; }

        [Option('b', "frame-rate", Required = false, Default = 0.05, HelpText = "The video bits-per-pixel.")]
        public double BitsPerPixel { get; set; }
    }
}
