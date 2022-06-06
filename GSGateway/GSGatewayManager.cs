using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServerFW;
using ShowAndLogMessage;
using GameCommunication;



namespace GSGateway
{
    internal class GSGatewayManager
    {
        
        string[] _args;

        static GameServerManager _gameServerManager;
        //static LogInfo _loggerMessage;

        GSCommandManager _gsCommandManager;
        GSConnectionManager _gsConnectionManager;
        

        internal GSGatewayManager(string[] args)
        {
            int lines;
            _args = args;

            _gameServerManager = GameServerManager.GetGameServerInstance();
            _gameServerManager.Event_GameServer += new GameServerManager.Delegate_GameServer_Event(GameserverEventGameServer);

            ServerConfManager serverConfManager = new ServerConfManager(_gameServerManager);
            if (!serverConfManager.SetConfig(_args))
            {
                ExitServer(1);
            }
            string logName = _gameServerManager.configManager.GetConfig.serverConfig.serverParameters.logFileName;
            string logPath = _gameServerManager.configManager.GetConfig.serverConfig.serverParameters.logPathFile;

            //hacer que venga por config la cantidad de lineas.
            if (GameServerManager.Get_debug_mode)
            {
                lines = 1;
            }
            else
            {
                lines = 20;
            }
            _gameServerManager.loggerManager.MaxLogLinesToSave(lines);
            _gameServerManager.loggerManager.CreateLogFile(logName, logPath);
            
            _gsCommandManager = new GSCommandManager();
            _gsConnectionManager = new GSConnectionManager(_gameServerManager);
        }

        internal void StartServer()
        {
            _gameServerManager.loggerManager.ShowAndLogMessage("starting gateway server\r\n");
            _gsConnectionManager.ConnectToServers();
            _gsConnectionManager.StartServerListening();

            while (true) //poner una variable acá
            {

            }

        }

        private  void GameserverEventGameServer(GameServerEventParameters gameServerEventParameters)
        {
            _gameServerManager.loggerManager.ShowAndLogMessage(gameServerEventParameters.GetGameServerEventType.ToString());
            switch (gameServerEventParameters.GetGameServerEventType)
            {
                #region connection events
                //connection events
                case GameServerEventParameters.GameServerEventType.GAMESERVER_CLIENT_CONNECTION_OK:
                case GameServerEventParameters.GameServerEventType.GAMESERVER_CLIENT_TIME_OUT:
                case GameServerEventParameters.GameServerEventType.GAMESERVER_CONNECTION_LIMIT:
                case GameServerEventParameters.GameServerEventType.GAMESERVER_DATA_IN:
                case GameServerEventParameters.GameServerEventType.GAMESERVER_END_CONNECTION:
                case GameServerEventParameters.GameServerEventType.GAMESERVER_SOCKET_ERROR:
                case GameServerEventParameters.GameServerEventType.GAMESERVER_RECIEVE_TIMEOUT:
                case GameServerEventParameters.GameServerEventType.GAMESERVER_SEND_ARRAY_COMPLETE:
                case GameServerEventParameters.GameServerEventType.GAMESERVER_ACCEPT_CONNECTION:
                case GameServerEventParameters.GameServerEventType.GAMESERVER_NEW_CONNECTION:
                case GameServerEventParameters.GameServerEventType.GAMESERVER_START_SERVER_LISTENING:
                case GameServerEventParameters.GameServerEventType.GAMESERVER_STOP_SERVER_LISTENING:
                    _gsConnectionManager.ConnectionEvents(gameServerEventParameters);
                    break;

                #endregion






            }
        }

        private static void ExitServer(int exitCode)
        {
            if (GameServerManager.Get_debug_mode) Console.ReadLine();
            System.Environment.Exit(exitCode);
        }
    }
}
