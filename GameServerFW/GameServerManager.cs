using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;
using Sockets;
using ConsoleOutputFormater;
using System.IO;
using GameServerFW.config;

namespace GameServerFW
{
    public class GameServerManager
    {
        //https://refactoring.guru/es/design-patterns/singleton/csharp/example#example-0

        const int C_SERVERS_TIMEOUT = 5;

        Sockets.Sockets _gatewayServerTCP;
        Sockets.Sockets _gatewayServerUDP;

        Sockets.Sockets _cliServersTCP;
        Sockets.Sockets _cliServersUDP;

        Config _config;
        ConfigManager _configManager;
        Utils utils;
        OutputFormater _outputFormater;
        private Logger.Logger _logger;
        //static string[] _args;

        static GameServerManager _gameServerInstance;

        
        public delegate void Delegate_GameServer_Event(GameServerEventParameters gameServerEventParameters);
        public event Delegate_GameServer_Event Event_GameServer;
        private void EventGameServer(GameServerEventParameters gameServerEventParameters)
        {
            this.Event_GameServer(gameServerEventParameters);
        }

        private GameServerManager() 
        {
            utils = new Utils();
            _outputFormater = new OutputFormater();
            _configManager = new ConfigManager();
            _configManager.Event_ConfigManager += new ConfigManager.Delegate_ConfigManager_Event(ConfigManagerEvent);

            //mandar esto a un evento
            //Console.WriteLine(_logger.WriteLog("Iniciando GatewayServer"));

            _logger = new Logger.Logger();
            _logger.Event_Log += Logger_Event_Log;
        }

        public static GameServerManager GetGameServerInstance()
        {
            if (_gameServerInstance == null)
            {
                _gameServerInstance = new GameServerManager();
            }

            
            return _gameServerInstance;
        }
        
        public void LoadConfig(string fileName)
        {
            _config = _configManager.GetConfig(fileName);
        }

        public void CreateConfigFile(string fileName)
        {
            _configManager.CreateConfigFile(fileName);
        }

        public string WriteLog(string log)
        {
            return (_logger.WriteLog(log));
        }

        public string WriteAndSaveLog(string log)
        {
            return WriteLog(_logger.WriteAndSaveLog(log));
        }

        public void GetConfig()
        {
            
        }

        private void Logger_Event_Log(string eventMessage)
        {
            
        }

        private void ConfigManagerEvent(ConfigManagerEventsParameters eventParameters)
        {

            GameServerEventParameters gameServerEventParameters = new GameServerEventParameters();
            gameServerEventParameters.SetMessage(eventParameters.GetMessage);

            switch (eventParameters.GetEventType)
            {
                case ConfigManagerEventsParameters.ConfigManagerEventType.LOAD_CONFIG_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_LOAD_CONFIG_OK);
                    break;

                case ConfigManagerEventsParameters.ConfigManagerEventType.LOAD_CONFIG_ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_LOAD_CONFIG_ERROR);
                    break;

                case ConfigManagerEventsParameters.ConfigManagerEventType.READ_CONFIG_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_READ_CONFIG_OK);
                    break;

                case ConfigManagerEventsParameters.ConfigManagerEventType.READ_CONFIG_ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_READ_CONFIG_ERROR);
                    break;

                case ConfigManagerEventsParameters.ConfigManagerEventType.CREATE_CONFIG_FILE_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_CONFIG_FILE_OK);
                    break;

                case ConfigManagerEventsParameters.ConfigManagerEventType.CREATE_CONFIG_FILE_ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_CONFIG_FILE_ERROR);
                    break;

                case ConfigManagerEventsParameters.ConfigManagerEventType.FILE_NOT_FOUND:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CONFIG_FILE_NOT_FOUND);
                    break;
            }
        }
    }
}
