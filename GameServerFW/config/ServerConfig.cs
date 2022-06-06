using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.config
{
    [DataContract]
    public class ServerConfig
    {
        ServerParameters _serverParameters;
        List<DestinyServers> _lstDestinyServer;
        public ServerConfig()
        {
            _serverParameters = new ServerParameters();
            _lstDestinyServer = new List<DestinyServers>();
        }


        [DataMember]
        public ServerParameters serverParameters
        { 
            get
            {
                return _serverParameters;
            }
            set
            {
                _serverParameters = value;
            }
        }

        [DataMember]
        public List<DestinyServers> destinyServers
        {
            get
            {
                return _lstDestinyServer;
            }
            set
            {
                _lstDestinyServer = value;
            }
        }

    }
}
