using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServerFW.Connections;
using GameServerFW.EventParameters;

namespace GameServerFW
{
    internal class ConnectionsManager
    {
        private const string C_HOSTINFO_NOT_FOUND = "host info not found";
        private const string C_CLIENTINFO_NOT_FOUND = "client info not found";

        List<HostInfo> _lstHostInfo;
        List<ClientInfo> _lstClientInfo;

        internal delegate void Delegate_Server_Events(ConnectionsManagerEventsParameters connectionsMangerEventsParameters);
        internal event Delegate_Server_Events ConnectionsManagerEvents;
        private void Events(ConnectionsManagerEventsParameters connectionsMangerEventsParameters)
        {
            this.ConnectionsManagerEvents(connectionsMangerEventsParameters);
        }


        internal ConnectionsManager()
        {
            _lstHostInfo = new List<HostInfo>();
            _lstClientInfo = new List<ClientInfo>();
        }

        internal void AddClientInfo(int connectionNumber, string ip, string clientId)
        {
            if (_lstClientInfo == null)
            {
                _lstClientInfo = new List<ClientInfo>();
            }


            ClientInfo clientInfo = new ClientInfo(clientId);
            clientInfo.ConnectionNumber = connectionNumber;
            clientInfo.status = ConnectionInfo.Status.CONNECTED;

            _lstClientInfo.Add(clientInfo);

        }

        private int GetClientInfoIndex(string clientId)
        {

            int res = -1;
            for (int i = 0; i < _lstClientInfo.Count(); i++)
            {
                if (_lstClientInfo[i].GetClientId == clientId)
                {
                    res = i;
                    return res;
                }
            }

            if (res == -1)
            {
                ConnectionsManagerEventsParameters ev = new ConnectionsManagerEventsParameters(ConnectionsManagerEventsParameters.CommunicationsMangerEventsType.CLIENT_INFO_NOT_FOUND);
                ev.SetEventMessage(C_CLIENTINFO_NOT_FOUND);
                Events(ev);
            }

            return res;
        }

        private int GetClientInfoIndex(int connectionNumber)
        {
            int res = -1;
            for (int i = 0; i < _lstClientInfo.Count(); i++)
            {
                if (_lstClientInfo[i].ConnectionNumber == connectionNumber)
                {
                    res = i;
                    return res;
                }
            }

            if (res == -1)
            {
                ConnectionsManagerEventsParameters ev = new ConnectionsManagerEventsParameters(ConnectionsManagerEventsParameters.CommunicationsMangerEventsType.CLIENT_INFO_NOT_FOUND);
                ev.SetEventMessage(C_CLIENTINFO_NOT_FOUND);
                Events(ev);
            }


            return res;
        }

        public int GetClientInfoConnectionNumber(string clientId)
        {
            int index = GetClientInfoIndex(clientId);

            if (index < 0)
            {
                ConnectionsManagerEventsParameters ev = new ConnectionsManagerEventsParameters(ConnectionsManagerEventsParameters.CommunicationsMangerEventsType.CLIENT_INFO_NOT_FOUND);
                ev.SetEventMessage(C_CLIENTINFO_NOT_FOUND);
                Events(ev);
            }

            return _lstClientInfo[index].ConnectionNumber;
        }

        internal string GetClientInfoId(int connectionNumber)
        {
            int index = GetClientInfoIndex(connectionNumber);

            if (index < 0)
            {
                ConnectionsManagerEventsParameters ev = new ConnectionsManagerEventsParameters(ConnectionsManagerEventsParameters.CommunicationsMangerEventsType.CLIENT_INFO_NOT_FOUND);
                ev.SetEventMessage(C_CLIENTINFO_NOT_FOUND);
                Events(ev);
            }

            return _lstClientInfo[index].GetClientId;
        }


        internal void DeleteClientInfo(string clientId)
        {
            int index = GetClientInfoIndex(clientId);

            if (index < 0)
            {
                ConnectionsManagerEventsParameters ev = new ConnectionsManagerEventsParameters(ConnectionsManagerEventsParameters.CommunicationsMangerEventsType.CLIENT_INFO_NOT_FOUND);
                ev.SetEventMessage(C_CLIENTINFO_NOT_FOUND);
                Events(ev);
            }

            _lstClientInfo.RemoveAt(index);
        }

        internal void DeleteClientInfo(int connectionNumber)
        {
            DeleteClientInfo(GetClientInfoId(connectionNumber));
        }

        internal void DeleteHostInfo(string tag)
        {
            int index = GetHostInfoIndex(tag);

            if (index < 0)
            {
                ConnectionsManagerEventsParameters ev = new ConnectionsManagerEventsParameters(ConnectionsManagerEventsParameters.CommunicationsMangerEventsType.HOST_INFO_NOT_FOUND);
                ev.SetEventMessage(C_HOSTINFO_NOT_FOUND);
                Events(ev);
            }

            _lstHostInfo.RemoveAt(index);
        }

        internal void DeleteInfo(string clientId, string tag)
        {
            if (clientId != "")
            {
                DeleteClientInfo(clientId);
                return;
            }

            if (tag != "")
            {
                DeleteHostInfo(tag);
                return;
            }

        }

        internal void DeleteInfo(int connectionNumber, string tag)
        {
            if (connectionNumber >= 0)
            {
                DeleteClientInfo(connectionNumber);
                return;
            }

            if (tag != "")
            {
                DeleteInfo("", tag);
            }

        }

        internal void AddHostInfo(HostInfo hostInfo)
        {
            _lstHostInfo.Add(hostInfo);
        }

        internal void UpdateHostInfo(int connectionNumber, string tag, ConnectionInfo.Status status = ConnectionInfo.Status.CONNECTED)
        {

            int index = GetHostInfoIndex(tag);

            if (index >= 0)
            {
                _lstHostInfo[index].ConnectionNumber = connectionNumber;
                _lstHostInfo[index].status = status;
            }
            else
            {
                ConnectionsManagerEventsParameters ev = new ConnectionsManagerEventsParameters(ConnectionsManagerEventsParameters.CommunicationsMangerEventsType.HOST_INFO_NOT_FOUND);
                ev.SetEventMessage(C_HOSTINFO_NOT_FOUND);
                Events(ev);
            }

        }

        private int GetHostInfoIndex(string tag)
        {
            int res = -1;
            for (int i = 0; i < _lstHostInfo.Count(); i++)
            {
                if (_lstHostInfo[i].GetServerTag == tag)
                {
                    res = i;
                    return res;
                }
            }

            if (res == -1)
            {
                ConnectionsManagerEventsParameters ev = new ConnectionsManagerEventsParameters(ConnectionsManagerEventsParameters.CommunicationsMangerEventsType.HOST_INFO_NOT_FOUND);
                ev.SetEventMessage(C_HOSTINFO_NOT_FOUND);
                Events(ev);
            }

            return res;
        }

        internal int GetClientsCount
        {
            get
            {
                return _lstClientInfo.Count();
            }
        }

        internal int GetConnectedHost
        {
            get
            {
                return _lstHostInfo.Count();
            }
        }

    }
}

