using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleOutputFormater;
using GameServerFW;


namespace GSGateway
{
    
    internal class GSConnectionManager
    {
        static GameServerManager _gameServerManager;
        //static LogInfo _loggerMessage;
        int _maxTcpConnections;
        int _tcpPort;
        int _udpPort;

        int countDestinyClients;

        internal GSConnectionManager(GameServerManager gameServerManagerInstance)
        {
            _gameServerManager = gameServerManagerInstance;
        }

        internal void StartServerListening()
        {
            _maxTcpConnections = Convert.ToInt32(_gameServerManager.configManager.GetConfig.serverConfig.serverParameters.maxUsers);

            _tcpPort = Convert.ToInt32(_gameServerManager.configManager.GetConfig.serverConfig.serverParameters.tcpPortNumber);
            _udpPort = Convert.ToInt32(_gameServerManager.configManager.GetConfig.serverConfig.serverParameters.udpPortNumber);

            _gameServerManager.connectionsManager.StartServerTCP(_tcpPort, _maxTcpConnections);
            _gameServerManager.loggerManager.ShowAndLogMessage("Listening TCP port " + _tcpPort,null,ShowAndLogMessage.LogInfo.typeMsg.OK);

            _gameServerManager.connectionsManager.StartServerUDP(_tcpPort, _maxTcpConnections);
            _gameServerManager.loggerManager.ShowAndLogMessage("Listening UDP port " + _tcpPort,null, ShowAndLogMessage.LogInfo.typeMsg.OK);
        }

        internal void ConnectToServers()
        {
            countDestinyClients = _gameServerManager.configManager.GetConfig.serverConfig.destinyServers.Count();
            for (int i=0; i<countDestinyClients;i++)
            {
                string host = _gameServerManager.configManager.GetConfig.serverConfig.destinyServers[i].serverInfo.host;
                string port = _gameServerManager.configManager.GetConfig.serverConfig.destinyServers[i].serverInfo.tcpPort;
                string tag = _gameServerManager.configManager.GetConfig.serverConfig.destinyServers[i].serverInfo.identifierTag;
                int portNumber = Convert.ToInt32(port);

                _gameServerManager.connectionsManager.ConnectTCP(host, portNumber, tag);
            }
        }

        internal void ConnectionEvents(GameServerEventParameters gameServerEventParameters)
        {

            OutputFormatterAttributes atr = new OutputFormatterAttributes();
            atr.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Cyan).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
            ShowAndLogMessage.LogInfo logInfo = ShowAndLogMessage.LogInfo.GetInstance();

            OutputFormatterAttributes atrEvenText = new OutputFormatterAttributes();
            atrEvenText.SetColorFG(OutputFormatterAttributes.TextColorFG.Yellow).SetColorBG(OutputFormatterAttributes.TextColorBG.Bright_Yellow);

            string eventText = OutputFormatter.FormatText(gameServerEventParameters.GetGameServerEventType.ToString(), atrEvenText);

            _gameServerManager.loggerManager.ShowMessage(logInfo.CustomType("INFO", atr) + " " + eventText, null);
        }
    }
}
