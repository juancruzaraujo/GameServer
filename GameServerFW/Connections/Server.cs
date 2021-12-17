using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sockets;

namespace GameServerFW.Connections
{
    internal class Server
    {
        Socket _serverTCP;
        Socket _serverUDP;
        Protocol.ConnectionProtocol _connectionProtocol;


        internal delegate void Delegate_Server_Events(Sockets.EventParameters socketEventParameters);
        internal event Delegate_Server_Events ServerEvents;
        private void Events(Sockets.EventParameters socketEventParameters)
        {
            this.ServerEvents(socketEventParameters);
        }


        internal Server(int port, int maxConnections, Protocol.ConnectionProtocol connectionProtocol)
        {
            _connectionProtocol = connectionProtocol;

            if (connectionProtocol == Protocol.ConnectionProtocol.TCP)
            {
                _serverTCP = new Socket();
                _serverTCP.SetServer(port, Sockets.Protocol.ConnectionProtocol.TCP, maxConnections);
                _serverTCP.Event_Socket += ServerTCP_Event_Socket;
            }

            if (connectionProtocol == Protocol.ConnectionProtocol.UDP)
            {
                _serverUDP = new Socket();
                _serverUDP.SetServer(port, Sockets.Protocol.ConnectionProtocol.UDP);
                _serverUDP.Event_Socket += ServerUDP_Event_Socket;
            }

        }

        private void ServerTCP_Event_Socket(Sockets.EventParameters eventParameters)
        {
            Events(eventParameters);
        }

        private void ServerUDP_Event_Socket(Sockets.EventParameters eventParameters)
        {
            Events(eventParameters);
        }

        internal void StartServer()
        {
            if (_connectionProtocol == Protocol.ConnectionProtocol.TCP)
            {
                _serverTCP.StartServer();
            }

            if (_connectionProtocol == Protocol.ConnectionProtocol.UDP)
            {
                _serverUDP.StartServer();
            }
        }

        internal void StopServer()
        {
            if (_connectionProtocol == Protocol.ConnectionProtocol.TCP)
            {
                _serverTCP.KillServer();
            }

            if (_connectionProtocol == Protocol.ConnectionProtocol.UDP)
            {
                _serverUDP.KillServer();
            }
        }

        /// <summary>
        /// only for tcp connections
        /// </summary>
        internal void DisconnectAllClients()
        {            
            _serverTCP.DisconnectAllConnectedClientsToMe();   
        }

        /// <summary>
        /// only for tcp connections
        /// </summary>
        internal void DisconnectClient(int connecttionNumber)
        {
            _serverTCP.DisconnectConnectedClientToMe(connecttionNumber);
            
        }

        internal void SendToAll(string message)
        {
            if (_connectionProtocol == Protocol.ConnectionProtocol.TCP)
            {
                _serverTCP.SendAll(message);
            }

            if (_connectionProtocol == Protocol.ConnectionProtocol.UDP)
            {
                _serverUDP.SendAll(message);
            }
        }

        internal void Send(string message, int connectionNumber)
        {
            if (_connectionProtocol == Protocol.ConnectionProtocol.TCP)
            {
                _serverTCP.Send(connectionNumber, message);
            }

            if (_connectionProtocol == Protocol.ConnectionProtocol.UDP)
            {
                _serverUDP.Send(connectionNumber, message);
            }
        }
    }

}
