using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCommunication
{
    public class ClientInfo : CommsInfo
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
