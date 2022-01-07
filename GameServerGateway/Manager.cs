using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServerFW;
using GameServerFW.config;
using GameServerFW.Connections;
using ConsoleOutputFormater;
using ShowAndLogMessage;
using GameCommunication;

namespace GameServerGateway
{
    class Manager
    {
        #if DEBUG
            private static bool debug_Mode = true;
            const int C_MAX_LOG_LINES_TO_SAVE = 0;
        #else
            private static bool debug_Mode = false;
            const int C_MAX_LOG_LINES_TO_SAVE = 20;
        #endif

        
        const string C_PARAM_LOAD_FILE_CONFIG = "-f";
        const string C_PARAM_CREATE_CONFIG_FILE = "-g";
        static GameServerManager _gameServerManager;
        
        static LoggerMessage _loggerMessage;
        
        private static ServerCommands _serverCommands;

        
        
        bool keepRuning;
        bool _logFileCreated;

        int _maxTcpConnections;
        int _tcpPort;
        int _udpPort;
        

        public Manager()
        {
            
            _logFileCreated = false;
            keepRuning = true;

            _loggerMessage = LoggerMessage.GetInstance();

        }

        

        public void StartServer(string[] args)
        {

            _gameServerManager = GameServerFW.GameServerManager.GetGameServerInstance();
            _gameServerManager.Event_GameServer += new GameServerManager.Delegate_GameServer_Event(GameserverEventGameServer);

            _serverCommands = new ServerCommands(_gameServerManager);
            _serverCommands.CreateCommands();

            if (args.Count() == 1 && args[0] == C_PARAM_CREATE_CONFIG_FILE)
            {
                //generar el archivo de conf
                string exampleConfigName = System.Diagnostics.Process.GetCurrentProcess().ProcessName + "_example.json";

                //_gameServerManager.CreateConfigFile(exampleConfigName);
                _gameServerManager.configManager.CreateConfigFile(exampleConfigName);

                ExitServer();
            }
            else if (args.Count() == 2)
            {
                if (args[0] == C_PARAM_LOAD_FILE_CONFIG)
                {
                    _gameServerManager.configManager.LoadConfig(args[1]);
                }
            }
            else
            {
                //muestro la ayuda propia del prog
                CommandLineParametersHelp.ShowHelp("configFile.json", C_PARAM_CREATE_CONFIG_FILE, C_PARAM_LOAD_FILE_CONFIG);
                ExitServer();
            }

            ConnectToDestinyServer();
            StartListening();

            //_gameServerManager.GetPrueba.Test();
            

            while (keepRuning)
            {

            }

        }

        private void ConnectToDestinyServer()
        {
            int count = _gameServerManager.configManager.GetConfig.serverConfig.destinyServers.Count();
            if (count > 0)
            {

                for (int i =0; i<count;i++)
                {
                    string logMessage = "Connecting to " + _gameServerManager.configManager.GetConfig.serverConfig.destinyServers[i].serverInfo.serverType;
                    logMessage = logMessage + " " + _gameServerManager.configManager.GetConfig.serverConfig.destinyServers[i].serverInfo.identifierTag;

                    OutputFormatterAttributes atr = new OutputFormatterAttributes();
                    atr.SetColorFG(OutputFormatterAttributes.TextColorFG.Blue).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);

                    _loggerMessage.ShowAndLogMessage(_loggerMessage.CustomType("INFO",atr) + logMessage);

                    HostInfo hostInfo = new HostInfo(_gameServerManager.configManager.GetConfig.serverConfig.destinyServers[i].serverInfo.identifierTag);
                    hostInfo.Ip = _gameServerManager.configManager.GetConfig.serverConfig.destinyServers[i].serverInfo.host;
                    hostInfo.status = CommsInfo.Status.DISCONNECTED;

                    _serverCommands.AddHostInfo(hostInfo);
                    

                    _gameServerManager.connectionsManager.ConnectToServer(_gameServerManager.configManager.GetConfig.serverConfig.destinyServers[i].serverInfo);
                }

            }
            
        }

        private void StartListening()
        {
            
            _maxTcpConnections = Convert.ToInt32(_gameServerManager.configManager.GetConfig.serverConfig.serverParameters.maxUsers);

            _tcpPort = Convert.ToInt32(_gameServerManager.configManager.GetConfig.serverConfig.serverParameters.tcpPortNumber);
            _udpPort = Convert.ToInt32(_gameServerManager.configManager.GetConfig.serverConfig.serverParameters.udpPortNumber);

            _gameServerManager.connectionsManager.StartServerTCP(_tcpPort, _maxTcpConnections);
            _loggerMessage.ShowAndLogMessage("Listening TCP port " + _tcpPort, null, LoggerMessage.typeMsg.OK);

            _gameServerManager.connectionsManager.StartServerUDP(_tcpPort, _maxTcpConnections);
            _loggerMessage.ShowAndLogMessage("Listening UDP port " + _udpPort, null, LoggerMessage.typeMsg.OK);
        }


        private static void ExitServer()
        {
            if (debug_Mode) Console.ReadLine();
            System.Environment.Exit(0);
        }

        private static void GameserverEventGameServer(GameServerEventParameters gameServerEventParameters)
        {
            OutputFormatterAttributes atr = new OutputFormatterAttributes();

            //Console.WriteLine("=====" + gameServerEventParameters.GetGameServerEventType);

            switch (gameServerEventParameters.GetGameServerEventType)
            {
                case GameServerEventParameters.GameServerEventType.GAMESERVER_LOAD_CONFIG_ERROR:
                    _loggerMessage.ShowMessage(gameServerEventParameters.GetMessage, null, LoggerMessage.typeMsg.ERROR);
                    ExitServer();
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_LOAD_CONFIG_OK:
                    _loggerMessage.CreateLogFile(_gameServerManager);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_CONFIG_FILE_OK:
                    _loggerMessage.ShowMessage("create config example file", null, LoggerMessage.typeMsg.OK);
                    ExitServer();
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_CONFIG_FILE_NOT_FOUND:
                    _loggerMessage.ShowMessage("config file not found", null, LoggerMessage.typeMsg.ERROR);
                    ExitServer();
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_MESSAGE:
                    _loggerMessage.ShowAndLogMessage(gameServerEventParameters.GetMessage);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_MESSAGE_ERROR:
                    _loggerMessage.ShowAndLogMessage(gameServerEventParameters.GetMessage, null, LoggerMessage.typeMsg.ERROR);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_STARTING:
                    _loggerMessage.ShowAndLogMessage("GAME SERVER STARTING", null, LoggerMessage.typeMsg.OK);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_START_ERROR:
                    _loggerMessage.ShowAndLogMessage("GAME SERVER START", null, LoggerMessage.typeMsg.ERROR);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_START_OK:
                    _loggerMessage.ShowAndLogMessage("GAME SERVER START", null, LoggerMessage.typeMsg.OK);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_STOP:
                    _loggerMessage.ShowAndLogMessage("GAME SERVER STOP", null, LoggerMessage.typeMsg.OK);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_OR_APPEND_LOG_FILE_OK:
                    _loggerMessage.ShowAndLogMessage("LOAD CONFIG", null, LoggerMessage.typeMsg.OK);
                    _loggerMessage.ShowAndLogMessage("CREATE OR APPEND LOG FILE", null, LoggerMessage.typeMsg.OK);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_OR_APPEND_LOG_FILE_ERROR:
                    _loggerMessage.ShowMessage("LOAD CONFIG", null, LoggerMessage.typeMsg.OK);
                    _loggerMessage.ShowMessage("CREATE OR APPEND LOG FILE", null, LoggerMessage.typeMsg.ERROR);
                    atr.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Red);
                    _loggerMessage.ShowMessage(gameServerEventParameters.GetMessage, atr);
                    break;

                    //ACÁ SE CONECTA ALGUIEN A MI
                case GameServerEventParameters.GameServerEventType.GAMESERVER_NEW_CONNECTION:
                    _serverCommands.AddClientInfo(gameServerEventParameters.GetSocketEventParameters.GetConnectionNumber, gameServerEventParameters.GetSocketEventParameters.GetClientIp);               
                    _gameServerManager.connectionsManager.SeverSendMessage(gameServerEventParameters.GetSocketEventParameters.GetConnectionNumber, "hola", GameServerFW.Connections.Protocol.ConnectionProtocol.TCP);



                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_SOCKET_ERROR:
                    _loggerMessage.ShowMessage(gameServerEventParameters.GetMessage, null, LoggerMessage.typeMsg.ERROR);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_CLIENT_TIME_OUT:
                    _loggerMessage.ShowAndLogMessage(gameServerEventParameters.GetMessage + " " + gameServerEventParameters.GetSocketEventParameters.GetServerIp + " " + gameServerEventParameters.GetSocketEventParameters.GetTag,null, LoggerMessage.typeMsg.ERROR); 
                    break;

                    //ACÁ YO ME CONECTO A UN SERVIDOR
                case GameServerEventParameters.GameServerEventType.GAMESERVER_CLIENT_CONNECTION_OK:
                    _loggerMessage.ShowAndLogMessage("Connected to " + gameServerEventParameters.GetSocketEventParameters.GetServerIp + " " + gameServerEventParameters.GetSocketEventParameters.GetTag + "\r\n", null, LoggerMessage.typeMsg.OK);
                    _serverCommands.UpdateHostInfo(gameServerEventParameters.GetSocketEventParameters.GetConnectionNumber, gameServerEventParameters.GetSocketEventParameters.GetTag);
                    
                    List<string> lstParams = new List<string>();
                    lstParams.Add(gameServerEventParameters.GetSocketEventParameters.GetConnectionNumber.ToString());
                    lstParams.Add(gameServerEventParameters.GetSocketEventParameters.GetTag);

                    _serverCommands.ExecuteCommand(ServerCommands.C_LOGUIN_TO_SERVER, lstParams); 

                    //HACER PROTOCOLO
                    //crear clase de hots con los datos y estado del host al que me conecte
                    //tiene que tener ademas
                    //la latencia
                    //estado, conectado o desconectado

                    //hacer lib de commandos


                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_END_CONNECTION:
                    _loggerMessage.ShowMessage("end connection =(");
                    _serverCommands.DeleteInfo(gameServerEventParameters.GetSocketEventParameters.GetConnectionNumber, gameServerEventParameters.GetSocketEventParameters.GetTag);
                    break;

                case GameServerEventParameters.GameServerEventType.GAMESERVER_DATA_IN:
                    string tag = gameServerEventParameters.GetSocketEventParameters.GetTag;
                    int connectionNumber = gameServerEventParameters.GetSocketEventParameters.GetConnectionNumber;
                    _serverCommands.GetMessage(gameServerEventParameters.GetMessage, connectionNumber, tag);
                    break;

            }
        }

        

    }

}
