using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.config
{
    [DataContract]
    class ServerConfig
    {
        
        [DataMember]
        public ServerParameters serverParameters{ get; set; }

        [DataMember]
        public List<MapServerList> mapServerList{ get; set; }

        [DataMember]
        public List<OtherServer> otherServer{ get; set; }

        public ServerConfig()
        {
            serverParameters = new ServerParameters();
            mapServerList = new List<MapServerList>();
            otherServer = new List<OtherServer>();
        }
    }
}
