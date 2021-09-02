using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GameServerFW;
using ConsoleOutputFormater;
using ShowAndLogMessage;

namespace GameServerGateway
{
    static internal class CommandLineParametersHelp
    {
        static internal void ShowHelp(string fileName, string generateParam, string loadFileParam)
        {
            //OutputFormatter outputFormater = new OutputFormatter();
            LoggerMessage loggerMessage = LoggerMessage.GetInstance();
            OutputFormatterAttributes formatAttributes = new OutputFormatterAttributes();

            string message = "";
            char directorySeparatorChar = Path.DirectorySeparatorChar;
            string executableName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

            message = $"run with {generateParam} parameter to generate configuration file or run with path " + directorySeparatorChar + $"{fileName}";
            formatAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_White).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
            loggerMessage.ShowMessage(message, formatAttributes);

            formatAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Green).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
            loggerMessage.ShowMessage("examples", formatAttributes);

            formatAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Cyan).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
            loggerMessage.ShowMessage($"{executableName} {generateParam}", formatAttributes);
            
            message = $"generates the configuration file {fileName} in the execution directory";
            formatAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_White).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
            loggerMessage.ShowMessage(message, formatAttributes);

            formatAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Cyan).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
            loggerMessage.ShowMessage($"{executableName} {loadFileParam} {fileName}", formatAttributes);
            loggerMessage.ShowMessage($"{executableName} {loadFileParam} [name.json]", formatAttributes);
            loggerMessage.ShowMessage($"{executableName} {loadFileParam} path" + directorySeparatorChar + $"{fileName}", formatAttributes);

            formatAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_White).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
            message = $"execute {executableName} with this config file";
            loggerMessage.ShowMessage(message, formatAttributes);
        }

    }
}
