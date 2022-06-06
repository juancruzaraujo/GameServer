using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sockets;

namespace GameServerFW.Communications
{
    class Client
    {
        private Socket _clientTCP;
        private Socket _clientUDP;
        private Protocol.ConnectionProtocol _connectionProtocol;

        internal delegate void Delegate_Client_Events(Sockets.EventParameters socketEventParameters);
        internal event Delegate_Client_Events ClientEvents;
        private void Events(Sockets.EventParameters socketEventParameters)
        {
            this.ClientEvents(socketEventParameters);
        }

        internal Client(Protocol.ConnectionProtocol connectionProtocol)
        {
            _connectionProtocol = connectionProtocol;
        }

        internal void Connect(string host,int port, string clientTag)
        {
            CreateClient();

            ConnectionParameters connectionParameters = new ConnectionParameters();
            connectionParameters.SetPort(port)
                .SetHost(host)
                .SetConnectionTag(clientTag);
                

            if (_connectionProtocol == Protocol.ConnectionProtocol.TCP)
            {
                connectionParameters.SetProtocol(Sockets.Protocol.ConnectionProtocol.TCP);
                _clientTCP.ConnectClient(connectionParameters);
            }

            if (_connectionProtocol == Protocol.ConnectionProtocol.UDP)
            {
                connectionParameters.SetProtocol(Sockets.Protocol.ConnectionProtocol.UDP);
                _clientUDP.ConnectClient(connectionParameters);
            }
            
        }

        private void CreateClient()
        {
            if (_connectionProtocol == Protocol.ConnectionProtocol.TCP)
            {
                if (_clientTCP == null)
                {
                    _clientTCP = new Socket();
                    _clientTCP.Event_Socket += ClientTCP_Event_Socket;
                }
            }

            if (_connectionProtocol == Protocol.ConnectionProtocol.UDP)
            {
                if (_clientUDP == null)
                {
                    _clientUDP = new Socket();
                    _clientUDP.Event_Socket += ClientUDP_Event_Socket;
                }
            }
        }
        private void ClientUDP_Event_Socket(Sockets.EventParameters eventParameters)
        {
            Events(eventParameters);
        }

        private void ClientTCP_Event_Socket(Sockets.EventParameters eventParameters)
        {
            Events(eventParameters);
        }

        internal void Send(string message,int connectionNumber)
        {
            if (_connectionProtocol == Protocol.ConnectionProtocol.TCP)
            {
                _clientTCP.Send(connectionNumber, message);
            }

            if (_connectionProtocol == Protocol.ConnectionProtocol.UDP)
            {
                _clientUDP.Send(connectionNumber, message);
            }
        }

        internal void SendAll(string message)
        {
            if (_connectionProtocol == Protocol.ConnectionProtocol.TCP)
            {
                _clientTCP.SendAll(message);
            }

            if (_connectionProtocol == Protocol.ConnectionProtocol.UDP)
            {
                _clientUDP.SendAll(message);
            }
        }
    }
}
