using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using GameServerFW;
using GameServerFW.config;
using ConsoleOutputFormater;
using ShowAndLogMessage;

namespace GameServerGateway
{
    class Program
    {

        #if DEBUG
            private static bool debug_Mode = true;
            const int C_MAX_LOG_LINES_TO_SAVE = 0;
        #else
            private /*static*/ bool debug_Mode = false;
            const int C_MAX_LOG_LINES_TO_SAVE = 20;
        #endif

        const string C_PARAM_LOAD_FILE_CONFIG = "-f";
        const string C_PARAM_CREATE_CONFIG_FILE = "-g";
         

        static bool keepRuning;
        static GameServerManager _gameServerManager;
        //static FormatterOutput _formatterOutput;
        static LoggerMessage _loggerMessage;
        

        static bool _logFileCreated;

        static void Main(string[] args)
        {
            //-f GatewayServerConfig.json

            _logFileCreated = false;
            _loggerMessage = LoggerMessage.GetInstance();

            Start(args);

            

            while (true)
            {

            }
        }
        private static void Start(string[] args)
        {

            _gameServerManager = GameServerFW.GameServerManager.GetGameServerInstance();
            _gameServerManager.Event_GameServer += new GameServerManager.Delegate_GameServer_Event(gameserver_Event_GameServer);

            if (args.Count() == 1 && args[0] == C_PARAM_CREATE_CONFIG_FILE)
            {
                //generar el archivo de conf
                string exampleConfigName = System.Diagnostics.Process.GetCurrentProcess().ProcessName + "_example.json";
                _gameServerManager.CreateConfigFile(exampleConfigName);
                ExitServer();
            }
            else if (args.Count() == 2)
            {
                if (args[0] == C_PARAM_LOAD_FILE_CONFIG)
                {
                    _gameServerManager.LoadConfig(args[1]);
                }
            }
            else
            {
                //muestro la ayuda propia del prog
                CommandLineParametersHelp.ShowHelp("configFile.json", C_PARAM_CREATE_CONFIG_FILE, C_PARAM_LOAD_FILE_CONFIG);
                ExitServer();
            }


        }

        private static void ExitServer()
        {
            if (debug_Mode) Console.ReadLine();
            System.Environment.Exit(0);
        }

        private static void gameserver_Event_GameServer(GameServerEventParameters gameServerEventParameters)
        {
            OutputFormatterAttributes atr = new OutputFormatterAttributes();

            switch (gameServerEventParameters.GetGameServerEventType)
            {
                case GameServerEventParameters.GameServerEventType.GAMESERVER_LOAD_CONFIG_ERROR:
                    _loggerMessage.ShowMessage(gameServerEventParameters.GetMessage, null, LoggerMessage.C_ERROR);
                    ExitServer();
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_LOAD_CONFIG_OK:
                    _loggerMessage.CreateLogFile(_gameServerManager);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_MESSAGE:
                    _loggerMessage.ShowAndLogMessage(gameServerEventParameters.GetMessage);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_MESSAGE_ERROR:
                    _loggerMessage.ShowAndLogMessage(gameServerEventParameters.GetMessage,null, LoggerMessage.C_ERROR);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_STARTING:
                    _loggerMessage.ShowAndLogMessage("GAME SERVER STARTING", null, LoggerMessage.C_OK);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_START_ERROR:
                    _loggerMessage.ShowAndLogMessage("GAME SERVER START", null, LoggerMessage.C_ERROR);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_START_OK:
                    _loggerMessage.ShowAndLogMessage("GAME SERVER START", null, LoggerMessage.C_OK);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_STOP:
                    _loggerMessage.ShowAndLogMessage("GAME SERVER STOP", null, LoggerMessage.C_OK);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_OR_APPEND_LOG_FILE_OK:
                    _loggerMessage.ShowAndLogMessage("LOAD CONFIG", null, LoggerMessage.C_OK);
                    _loggerMessage.ShowAndLogMessage("CREATE OR APPEND LOG FILE", null, LoggerMessage.C_OK);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_OR_APPEND_LOG_FILE_ERROR:
                    _loggerMessage.ShowMessage("LOAD CONFIG", null, LoggerMessage.C_OK);
                    _loggerMessage.ShowMessage("CREATE OR APPEND LOG FILE", null, LoggerMessage.C_ERROR);
                    atr.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Red);
                    _loggerMessage.ShowMessage(gameServerEventParameters.GetMessage,atr); 
                    break;
   
            }
        }

    }
}
