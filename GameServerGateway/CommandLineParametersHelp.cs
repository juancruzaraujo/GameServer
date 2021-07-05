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
            OutputFormater outputFormater = new OutputFormater();
            string aux = "";
            string message = "";
            char directorySeparatorChar = Path.DirectorySeparatorChar;
            string executableName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

            message = $"run with {generateParam} parameter to generate configuration file or run with path " + directorySeparatorChar + $"{fileName}";
            outputFormater.ResetAttributes();
            ShowMessage(outputFormater.FormatText(message, OutputFormater.TextColorFG.Bright_White, OutputFormater.TextColorBG.Black));
            ShowMessage(outputFormater.FormatText("examples", OutputFormater.TextColorFG.Bright_Green, OutputFormater.TextColorBG.Black));
            ShowMessage(outputFormater.FormatText($"{executableName} {generateParam}", OutputFormater.TextColorFG.Bright_Cyan, OutputFormater.TextColorBG.Black));
            message = $"generates the configuration file {fileName} in the execution directory";
            ShowMessage(outputFormater.FormatText(message, OutputFormater.TextColorFG.Bright_White, OutputFormater.TextColorBG.Black));

            ShowMessage(outputFormater.FormatText($"{executableName} {loadFileParam} {fileName}", OutputFormater.TextColorFG.Bright_Cyan, OutputFormater.TextColorBG.Black));
            ShowMessage(outputFormater.FormatText($"{executableName} {loadFileParam} [name.json]", OutputFormater.TextColorFG.Bright_Cyan, OutputFormater.TextColorBG.Black));
            ShowMessage(outputFormater.FormatText($"{executableName} {loadFileParam} path" + directorySeparatorChar + $"{fileName}", OutputFormater.TextColorFG.Bright_Cyan, OutputFormater.TextColorBG.Black));
            message = $"execute {executableName} with this config file";
            ShowMessage(outputFormater.FormatText(message, OutputFormater.TextColorFG.Bright_White, OutputFormater.TextColorBG.Black));
        }

        static private void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
