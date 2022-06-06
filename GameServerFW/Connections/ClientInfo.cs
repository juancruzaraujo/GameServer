using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.Connections
{
    internal class ClientInfo : ConnectionInfo
    {
        private string _clientId;

        public string GetClientId
        {
            get
            {
                return _clientId;
            }
        }

        public ClientInfo(string clientId)
        {
            _clientId = clientId;
        }

    }
}
