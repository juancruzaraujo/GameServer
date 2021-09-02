using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sockets;
using ConsoleOutputFormater;
using System.IO;
using GameServerFW.config;
using GameServerFW.EventParameters;

namespace GameServerFW
{
    /// <summary>
    /// use GetGameServerInstance to create singleton objet
    /// </summary>
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
        
        private LoggerManager _loggerManager;
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
           
            _configManager = new ConfigManager();
            _configManager.Event_ConfigManager += new ConfigManager.Delegate_ConfigManager_Event(ConfigManagerEvent);

            _loggerManager = new LoggerManager();
            _loggerManager.Event_Log += new LoggerManager.Delegate_Log_Event(LoggerManagerEvent);
        }

        /// <summary>
        /// create a singleton instance
        /// </summary>
        /// <returns></returns>
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
            if (_config !=null)
            {
                ConfigManagerEventsParameters ev = new ConfigManagerEventsParameters();
                ev.SetEventType(ConfigManagerEventsParameters.ConfigManagerEventType.LOAD_CONFIG_OK);
                ConfigManagerEvent(ev);
            }
        }

        public void CreateConfigFile(string fileName)
        {
            _configManager.CreateConfigFile(fileName);
        }

        public void CreateLogFile(string fileName,string path)
        {
            _loggerManager.CreateLogFile(fileName, path);
        }

        public string WriteLog(string log)
        {
            return (_loggerManager.WriteLog(log));
        }

        public string WriteAndSaveLog(string log)
        {
            return _loggerManager.WriteAndSaveLog(log);
        }

        public Config GetConfig
        {
            get
            {
                return _config;
            }
        }

        private void LoggerManagerEvent(LoggerManagerEventsParameters eventMessage)
        {
            GameServerEventParameters gameServerEventParameters = new GameServerEventParameters();
            gameServerEventParameters.SetMessage(eventMessage.GetEventMessage);

            switch (eventMessage.GetEventType)
            {
                case LoggerManagerEventsParameters.LoggerManagerEventType.CREATE_OR_APPEND_LOG_FILE_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_OR_APPEND_LOG_FILE_OK);
                    break;

                case LoggerManagerEventsParameters.LoggerManagerEventType.CREATE_OR_APPEND_LOG_FILE_ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_OR_APPEND_LOG_FILE_ERROR);
                    break;

                case LoggerManagerEventsParameters.LoggerManagerEventType.WRITE_LOG_FILE_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_WRITE_LOG_FILE_OK);
                    break;

                case LoggerManagerEventsParameters.LoggerManagerEventType.WRITE_LOG_FILE_ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_WRITE_LOG_FILE_ERROR);
                    break;

                case LoggerManagerEventsParameters.LoggerManagerEventType.SAVE_LOG_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_SAVE_LOG_OK);
                    break;

                case LoggerManagerEventsParameters.LoggerManagerEventType.SAVE_LOG_ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_SAVE_LOG_ERROR);
                    break;
            }
            EventGameServer(gameServerEventParameters);
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

            EventGameServer(gameServerEventParameters);
        }
    }
}
