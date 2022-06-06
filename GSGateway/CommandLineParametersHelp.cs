using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GameServerFW;
using ConsoleOutputFormater;
using ShowAndLogMessage;

namespace GSGateway
{
    static internal class CommandLineParametersHelp
    {
        static GameServerManager _gameServerManager;

        static internal void SetGameFW(GameServerManager gameServerInstance)
        {
            _gameServerManager = gameServerInstance;
        }

        static internal void ShowHelp(string fileName, string generateParam, string loadFileParam)
        {
            OutputFormatter outputFormater = new OutputFormatter();
            LogInfo loggerMessage = LogInfo.GetInstance();
            OutputFormatterAttributes formatAttributes = new OutputFormatterAttributes();

            string message = "";
            char directorySeparatorChar = Path.DirectorySeparatorChar;
            string executableName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

            message = $"run with {generateParam} parameter to generate configuration file or run with path " + directorySeparatorChar + $"{fileName}";
            
            formatAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_White).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
            _gameServerManager.loggerManager.ShowAndLogMessage(message, formatAttributes);

            formatAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Green).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
            _gameServerManager.loggerManager.ShowAndLogMessage("examples", formatAttributes);

            formatAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Cyan).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
            _gameServerManager.loggerManager.ShowAndLogMessage($"{executableName} {generateParam}", formatAttributes);

            message = $"generates the configuration file {fileName} in the execution directory";
            formatAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_White).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
            _gameServerManager.loggerManager.ShowAndLogMessage(message, formatAttributes);

            formatAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Cyan).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
            _gameServerManager.loggerManager.ShowAndLogMessage($"{executableName} {loadFileParam} {fileName}", formatAttributes);
            _gameServerManager.loggerManager.ShowAndLogMessage($"{executableName} {loadFileParam} [name.json]", formatAttributes);
            _gameServerManager.loggerManager.ShowAndLogMessage($"{executableName} {loadFileParam} path" + directorySeparatorChar + $"{fileName}", formatAttributes);

            formatAttributes.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_White).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
            message = $"execute {executableName} with this config file";
            _gameServerManager.loggerManager.ShowAndLogMessage(message, formatAttributes);
        }
    }
}
