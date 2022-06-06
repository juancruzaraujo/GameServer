using GameServerFW;
using System.Linq;

namespace GSGateway
{
    internal class ServerConfManager
    {

        const string C_PARAM_LOAD_FILE_CONFIG = "-f";
        const string C_PARAM_CREATE_CONFIG_FILE = "-g";

        static GameServerManager _gameServerManager;

        internal ServerConfManager(GameServerManager gameServerManagerInstance)
        {
            _gameServerManager = gameServerManagerInstance;
        }

        internal bool SetConfig(string[] args)
        {
            bool returnResul = false;

            if (args.Count() == 1 && args[0] == C_PARAM_CREATE_CONFIG_FILE)
            {
                //generar el archivo de conf
                string exampleConfigName = System.Diagnostics.Process.GetCurrentProcess().ProcessName + "_example.json";

                _gameServerManager.configManager.CreateConfigFile(exampleConfigName);

                //ExitServer();
            }
            else if (args.Count() == 2)
            {
                if (args[0] == C_PARAM_LOAD_FILE_CONFIG)
                {
                    _gameServerManager.configManager.LoadConfig(args[1]);
                    returnResul = true;
                }
            }
            else
            {
                //muestro la ayuda propia del prog
                CommandLineParametersHelp.SetGameFW(_gameServerManager);
                CommandLineParametersHelp.ShowHelp("configFile.json", C_PARAM_CREATE_CONFIG_FILE, C_PARAM_LOAD_FILE_CONFIG);
                //ExitServer();
            }


            return returnResul;
        }
    }
}
