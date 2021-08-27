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
        
        static bool _logFileCreated;
        static int _logCounterLineSave;

        static void Main(string[] args)
        {
            _logFileCreated = false;
            start(args);

            while (true)
            {

            }
        }
        private static void start(string[] args)
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

            //_gameServerManager.CreateConfigFile()

        }

        private static void ExitServer()
        {
            if (debug_Mode) Console.ReadLine();
            System.Environment.Exit(0);
        }

        private static void gameserver_Event_GameServer(GameServerEventParameters gameServerEventParameters)
        {
            
            //para pruebas
            //Console.WriteLine(gameServerEventParameters.GetGameServerEventType + " " +  gameServerEventParameters.GetMessage);

            switch (gameServerEventParameters.GetGameServerEventType)
            {
                case GameServerEventParameters.GameServerEventType.GAMESERVER_LOAD_CONFIG_ERROR:
                    ShowMessage(gameServerEventParameters.GetMessage, null, true);
                    ExitServer();
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_LOAD_CONFIG_OK:
                    CreateLogFile();
                    ShowAndLogMessage("LOAD CONFIG", null, false,true);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_MESSAGE:
                    LogMessage(gameServerEventParameters.GetMessage);
                    ShowMessage(gameServerEventParameters.GetMessage);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_MESSAGE_ERROR:
                    ShowMessage(gameServerEventParameters.GetMessage,null,true);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_STARTING:
                    ShowAndLogMessage("GAME SERVER STARTING", null, false, true);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_START_ERROR:
                    ShowAndLogMessage("GAME SERVER START", null, true, false);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_START_OK:
                    ShowAndLogMessage("GAME SERVER START", null, false, true);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_STOP:
                    ShowAndLogMessage("GAME SERVER STOP", null, false, true);
                    break;
            }
        }

        private static void Logger_Event_Log(string eventMessage)
        {
            //Console.WriteLine(eventMessage);
        }

        private static void CreateLogFile()
        {
            if (!_logFileCreated)
            {
                string fileName = _gameServerManager.GetConfig.serverConfig.serverParameters.logFileName;
                string path = _gameServerManager.GetConfig.serverConfig.serverParameters.logPathFile;
                
                _gameServerManager.CreateLogFile(fileName, path);
                _logFileCreated = true;
            }

        }

        private static void LogMessage(string message)
        {
            if (_logCounterLineSave < C_MAX_LOG_LINES_TO_SAVE)
            {
                _gameServerManager.WriteLog(message);
                _logCounterLineSave++;
            }
            else
            {
                _gameServerManager.WriteAndSaveLog(message);
                _logCounterLineSave = 0;
            }
            //hacer una clase estatica que devuelva el mensaje con sus colores            
        }

        private static void ShowMessage(string message, OutputFormatter outputFormaterParam = null, bool msgError = false, bool msgOk = false)
        {
            MessageParams param = new MessageParams();
            
            Console.WriteLine(message);

            if (outputFormaterParam != null)
            {
                
            }

            if (msgError)
            {

            }
            
            if (msgOk)
            {

            }

        }

        private static void ShowAndLogMessage(string message, OutputFormatter outputFormaterParam = null, bool msgError = false, bool msgOk = false)
        {
            LogMessage(message);
            ShowMessage(message, outputFormaterParam, msgError, msgOk);
        }

    }
}
