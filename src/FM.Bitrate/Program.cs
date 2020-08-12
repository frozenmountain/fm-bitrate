using CommandLine;
using CommandLine.Text;
using System;
using System.Threading.Tasks;

namespace FM.Bitrate
{
    class Program
    {
        static void Main(string[] args)
        {
            using var parser = new CommandLine.Parser((settings) =>
            {
                settings.CaseInsensitiveEnumValues = true;
                settings.HelpWriter = null;
            });

            var result = parser.ParseArguments<
                CalculateOptions
            >(args);

            result.MapResult(
                (CalculateOptions options) =>
                {
                    return Task.Run(async () =>
                    {
                        return await new Calculator(options).Run();
                    }).GetAwaiter().GetResult();
                },
                errors =>
                {
                    var helpText = HelpText.AutoBuild(result, 96);
                    helpText.Copyright = "Copyright (C) 2020 Frozen Mountain Software Ltd.";
                    helpText.AddEnumValuesToHelpText = true;
                    Console.Error.Write(helpText);
                    return 1;
                });
        }
    }
}
