using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.config
{
    [DataContract]
    public class ServerParameters
    {
        [DataMember]
        public string serverName { get; set; }

        [DataMember]
        public string tcpPortNumber { get; set; }

        [DataMember]
        public string udpPortNumber { get; set; }

        [DataMember]
        public string maxUsers { get; set; }
        
        [DataMember]
        public string recieveTimeOut { get; set; }

        [DataMember]
        public string logFileName { get; set; }

        [DataMember]
        public string logPathFile { get; set; }

        [DataMember]
        public List<AdminUsers> adminUsers { get; set; }

    }
}
