using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandLine;
using CommandLine.Text;

namespace Basilisk.LegacyConverter
{
    public class Options
    {
        [Option('i', "input-file", HelpText = "Legacy library file to be converted")]
        public string ExplicitInputFile { get; set; }

        [ValueList(typeof(List<string>), MaximumElements = 1)]
        public IList<string> ImplicitInputFiles { get; set; }

        [Option('o', "output-file", Required = true, HelpText = "Path to generated output library")]
        public string OutputFile { get; set; }

        [Option('m', "mode", Required = true, HelpText = "Output mode (Xml or Json)")]
        public OutputMode OutputMode { get; set; }

        public bool HasMultipleInputFiles
        {
            get
            {
                return
                    ImplicitInputFiles != null && ImplicitInputFiles.Any() &&
                    (!String.IsNullOrEmpty(ExplicitInputFile) || ImplicitInputFiles.Count > 1);
            }
        }

        public string InputFile
        {
            get
            {
                if (HasMultipleInputFiles)
                {
                    throw new InvalidOperationException("Multiple output files specified");
                }
                if (!String.IsNullOrEmpty(ExplicitInputFile))
                {
                    return ExplicitInputFile;
                }
                else if (ImplicitInputFiles != null && ImplicitInputFiles.Any())
                {
                    return ImplicitInputFiles.Single();
                }
                else
                {
                    return null;
                }
            }
        }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
