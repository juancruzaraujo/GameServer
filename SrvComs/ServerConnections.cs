using System;
using System.Collections.Generic;
using System.Linq;
using GameCommunication;

namespace Communica
{
    public class ServerConnections
    {
        private const string C_HOSTINFO_NOT_FOUND = "host info not found";
        private const string C_CLIENTINFO_NOT_FOUND = "client info not found";

        List<HostInfo> _lstHostInfo;
        List<ClientInfo> _lstClientInfo;

        public ServerConnections()
        {
            _lstHostInfo = new List<HostInfo>();
            _lstClientInfo = new List<ClientInfo>();
        }

        public void AddClientInfo(int connectionNumber, string ip, string clientId)
        {
            if (_lstClientInfo == null)
            {
                _lstClientInfo = new List<ClientInfo>();
            }

            
            ClientInfo clientInfo = new ClientInfo(clientId);
            clientInfo.ConnectionNumber = connectionNumber;
            clientInfo.status = CommsInfo.Status.CONNECTED;

            _lstClientInfo.Add(clientInfo);

        }

        private int GetClientInfoIndex(string clientId)
        {
            for (int i=0;i<_lstClientInfo.Count();i++)
            {
                if (_lstClientInfo[i].GetClientId == clientId)
                {
                    return i;
                }
            }

            return -1;
        }

        private int GetClientInfoIndex(int connectionNumber)
        {
            for (int i = 0; i < _lstClientInfo.Count(); i++)
            {
                if (_lstClientInfo[i].ConnectionNumber == connectionNumber)
                {
                    return i;
                }
            }

            return -1;
        }

        public int GetClientInfoConnectionNumber(string clientId)
        {
            int index = GetClientInfoIndex(clientId);

            if (index < 0)
            {
                throw new Exception(C_CLIENTINFO_NOT_FOUND);
            }

            return _lstClientInfo[index].ConnectionNumber;
        }

        public string GetClientInfoId(int connectionNumber)
        {
            int index = GetClientInfoIndex(connectionNumber);

            if (index < 0)
            {
                throw new Exception(C_CLIENTINFO_NOT_FOUND);
            }

            return _lstClientInfo[index].GetClientId;
        }

        public void DeleteClientInfo(string clientId)
        {
            int index = GetClientInfoIndex(clientId);

            if (index < 0)
            {
                throw new Exception(C_CLIENTINFO_NOT_FOUND);
            }

            _lstClientInfo.RemoveAt(index);
        }

        public void DeleteClientInfo(int connectionNumber)
        {
            DeleteClientInfo(GetClientInfoId(connectionNumber));
        }

        public void DeleteHostInfo(string tag)
        {
            int index = GetHostInfoIndex(tag);

            if (index < 0)
            {
                throw new Exception(C_HOSTINFO_NOT_FOUND);
            }

            _lstHostInfo.RemoveAt(index);
        }

        public void DeleteInfo(string clientId,string tag)
        {
            if (clientId !="")
            {
                DeleteClientInfo(clientId);
                return;
            }

            if (tag !="")
            {
                DeleteHostInfo(tag);
                return;
            }

        }

        public void DeleteInfo(int connectionNumber,string tag)
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

        public void AddHostInfo(HostInfo hostInfo)
        {
            _lstHostInfo.Add(hostInfo);
        }

        public void UpdateHostInfo(int connectionNumber, string tag, CommsInfo.Status status = CommsInfo.Status.CONNECTED)
        {

            int index = GetHostInfoIndex(tag);

            if (index >=0  )
            {
                _lstHostInfo[index].ConnectionNumber = connectionNumber;
                _lstHostInfo[index].status = status;
            }
            else
            {
                throw new Exception(C_HOSTINFO_NOT_FOUND);
            }

        }

        private int GetHostInfoIndex(string tag)
        {
            for (int i = 0; i < _lstHostInfo.Count(); i++)
            {
                if (_lstHostInfo[i].GetServerTag == tag)
                {
                    return i;
                }
            }

            return -1;
        }

        public int GetClientsCount
        {
            get
            {
                return _lstClientInfo.Count();
            }
        }

        public int GetConnectedHost
        {
            get
            {
                return _lstHostInfo.Count();
            }
        }

    }
}
