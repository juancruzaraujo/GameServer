using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GameServerFW;
using ConsoleOutputFormater;

namespace GameServerGateway
{
    static internal class CommandLineParametersHelp
    {
        static internal void ShowHelp(string fileName, string generateParam, string loadFileParam)
        {
            OutputFormatter outputFormater = new OutputFormatter();

            string message = "";
            char directorySeparatorChar = Path.DirectorySeparatorChar;
            string executableName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

            message = $"run with {generateParam} parameter to generate configuration file or run with path " + directorySeparatorChar + $"{fileName}";
            /*
            ShowMessage(outputFormater.FormatText(message, OutputFormatter.TextColorFG.Bright_White, OutputFormatter.TextColorBG.Black));
            ShowMessage(outputFormater.FormatText("examples", OutputFormatter.TextColorFG.Bright_Green, OutputFormatter.TextColorBG.Black));
            ShowMessage(outputFormater.FormatText($"{executableName} {generateParam}", OutputFormatter.TextColorFG.Bright_Cyan, OutputFormatter.TextColorBG.Black));
            message = $"generates the configuration file {fileName} in the execution directory";
            ShowMessage(outputFormater.FormatText(message, OutputFormatter.TextColorFG.Bright_White, OutputFormatter.TextColorBG.Black));

            ShowMessage(outputFormater.FormatText($"{executableName} {loadFileParam} {fileName}", OutputFormatter.TextColorFG.Bright_Cyan, OutputFormatter.TextColorBG.Black));
            ShowMessage(outputFormater.FormatText($"{executableName} {loadFileParam} [name.json]", OutputFormatter.TextColorFG.Bright_Cyan, OutputFormatter.TextColorBG.Black));
            ShowMessage(outputFormater.FormatText($"{executableName} {loadFileParam} path" + directorySeparatorChar + $"{fileName}", OutputFormatter.TextColorFG.Bright_Cyan, OutputFormatter.TextColorBG.Black));
            message = $"execute {executableName} with this config file";
            ShowMessage(outputFormater.FormatText(message, OutputFormatter.TextColorFG.Bright_White, OutputFormatter.TextColorBG.Black));
            */
        }

        static private void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
