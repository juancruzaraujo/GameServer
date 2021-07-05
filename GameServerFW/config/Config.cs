using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.config
{
    [DataContract]
    class Config
    {
        [DataMember]
        public ServerConfig serverConfig { get; set; }

        /*
        public Config()
        {
            serverConfig = new ServerConfig();
        }*/
        
    }
}
