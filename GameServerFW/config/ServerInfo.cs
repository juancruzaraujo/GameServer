using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.config
{
    [DataContract]
    public class ServerInfo
    {
        [DataMember]
        public string host { get; set; }

        [DataMember]
        public string tcpPort { get; set; }

        [DataMember]
        public string udpPort { get; set; }

        [DataMember]
        public string serverType { get; set; }

        //https://stackoverflow.com/questions/25570712/is-it-possible-to-wrap-json-in-json-field-like-a-string
        [DataMember]
        public string identifierTag { get; set; }

        //"user": "user",
        //"password" : "password"

        [DataMember]
        public string user { get; set; }
        
        [DataMember]
        public string password { get; set; }
    }
}
