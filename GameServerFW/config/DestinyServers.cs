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
        [DataMember]
        public ServerInfo serverInfo { get; set; }
    }
}
