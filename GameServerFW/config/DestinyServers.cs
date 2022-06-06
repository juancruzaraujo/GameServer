using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.config
{
    [DataContract]
    public class DestinyServers
    {
        ServerInfo _serverInfo;

        public DestinyServers()
        {
            _serverInfo = new ServerInfo();
        }

        [DataMember]
        public ServerInfo serverInfo
        {
            get
            {
                return _serverInfo;
            }
            set
            {
                _serverInfo = value;
            }
        }
    }
}
