using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServerFW.Connections;
using GameServerFW.config;

namespace GameServerFW
{
    public class ConnectionsManager
    {

        //static ConnectionsManager _connectionsManagerInstance;
        private Server _serverTCP;
        private Server _serverUDP;
        private Client _clientTCP;
        private Client _clientUDP;

        internal delegate void Delegate_Server_Events(Sockets.EventParameters socketEventParameters);
        internal event Delegate_Server_Events ConnectionManagerEvents;
        private void Events(Sockets.EventParameters socketEventParameters)
        {
            this.ConnectionManagerEvents(socketEventParameters);
        }

        internal ConnectionsManager(){}


        public void StartServerTCP(int port, int maxConnections)
        {
            StartServer(port, maxConnections, Protocol.ConnectionProtocol.TCP);
        }

        public void StartServerUDP(int port,int maxConnections)
        {
            StartServer(port, maxConnections, Protocol.ConnectionProtocol.UDP);
        }

        private void StartServer(int port,int maxConnections, Protocol.ConnectionProtocol connectionProtocol)
        {
            if (connectionProtocol == Protocol.ConnectionProtocol.TCP)
            {
                _serverTCP = new Server(port, maxConnections, connectionProtocol);
                _serverTCP.ServerEvents += ServerTCP_ServerEvents;
                _serverTCP.StartServer();
            }

            if (connectionProtocol == Protocol.ConnectionProtocol.UDP)
            {
                _serverUDP = new Server(port, 0, connectionProtocol);
                _serverUDP.ServerEvents += ServerUDP_ServerEvents;
                _serverUDP.StartServer();
            }
            
        }

        private void ServerTCP_ServerEvents(Sockets.EventParameters socketEventParameters)
        {
            Events(socketEventParameters);
        }

        private void ServerUDP_ServerEvents(Sockets.EventParameters socketEventParameters)
        {
            Events(socketEventParameters);
        }

        public void ConnectTCP(string host,int port,string tag)
        {
            Connect(host, port, tag, Protocol.ConnectionProtocol.TCP);
        }

        public void ConnectUDP(string host,int port, string tag)
        {
            Connect(host, port, tag,Protocol.ConnectionProtocol.UDP);
        }

        public void ConnectToServer(ServerInfo serverInfo)
        {
            if (serverInfo.tcpPort !="")
            {
                Connect(serverInfo.host, Convert.ToInt32(serverInfo.tcpPort), serverInfo.identifierTag,Protocol.ConnectionProtocol.TCP);
            }

            if (serverInfo.udpPort != "")
            {
                Connect(serverInfo.host, Convert.ToInt32(serverInfo.tcpPort), serverInfo.identifierTag, Protocol.ConnectionProtocol.UDP);
            }

        }

        private void Connect(string host,int port,string tag,Protocol.ConnectionProtocol connectionProtocol)
        {
            if (connectionProtocol == Protocol.ConnectionProtocol.TCP)
            {
                if (_clientTCP == null)
                {
                    _clientTCP = new Client(connectionProtocol);
                    _clientTCP.ClientEvents += ClientTCP_ClientEvents;
                    
                }

                _clientTCP.Connect(host, port,tag);
                

            }

            if (connectionProtocol == Protocol.ConnectionProtocol.UDP)
            {
                if (_clientUDP == null)
                {
                    _clientUDP = new Client(connectionProtocol);
                    _clientUDP.ClientEvents += ClientUDP_ClientEvents;
                }

                _clientUDP.Connect(host, port,tag);
            }
        }

        private void ClientUDP_ClientEvents(Sockets.EventParameters socketEventParameters)
        {
            Events(socketEventParameters);
        }

        private void ClientTCP_ClientEvents(Sockets.EventParameters socketEventParameters)
        {
            Events(socketEventParameters);
        }

        public void Disconect()
        {

        }

        public void DisconnectTCPClientFromServer(int connectionNumber)
        {
            _serverTCP.DisconnectClient(connectionNumber);
        }

        public void DisconnectAllTCPClientsFromServer()
        {
            _serverTCP.DisconnectAllClients();
        }

        public void SeverSendMessage(int connectionNumber, string message, Protocol.ConnectionProtocol connectionProtocol)
        {
            if (connectionProtocol == Protocol.ConnectionProtocol.TCP)
            {
                _serverTCP.Send(message, connectionNumber);
            }
            
            if (connectionProtocol == Protocol.ConnectionProtocol.UDP)
            {
                _serverUDP.Send(message, connectionNumber);
            }
        }

        public void ServerSendMessageToAll(string message, Protocol.ConnectionProtocol connectionProtocol)
        {
            if (connectionProtocol == Protocol.ConnectionProtocol.TCP)
            {
                _serverTCP.SendToAll(message);
            }

            if (connectionProtocol == Protocol.ConnectionProtocol.UDP)
            {
                _serverUDP.SendToAll(message);
            }
        }

        public void ClientSendMessageToAll(string message,Protocol.ConnectionProtocol connectionProtocol)
        {

        }

        public void ClientSendMessage(string message, int connectionNumber, Protocol.ConnectionProtocol connectionProtocol)
        {
            if (connectionProtocol == Protocol.ConnectionProtocol.TCP)
            {
                //_serverTCP.Send(message, connectionNumber);
                _clientTCP.Send(message, connectionNumber);
            }

            if (connectionProtocol == Protocol.ConnectionProtocol.UDP)
            {
                _clientUDP.Send(message, connectionNumber);
            }
        }
    }
}
