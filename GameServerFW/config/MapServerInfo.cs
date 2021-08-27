using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.config
{
    [DataContract]
    public class MapServerInfo
    {
        [DataMember]
        public string mapServerType { get; set; }

        [DataMember]
        public string host { get; set; }

        [DataMember]
        public string tcpPort { get; set; }

        [DataMember]
        public string udpPort { get; set; }
    }
}
