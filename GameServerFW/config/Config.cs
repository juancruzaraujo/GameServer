using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.config
{
    [DataContract]
    public class Config
    {

        ServerConfig _serverConfig;
        public Config()
        {
            _serverConfig = new ServerConfig();
        }

        [DataMember]
        public ServerConfig serverConfig 
        { 
            get
            {
                return _serverConfig;
            }
            set
            {
                _serverConfig = value;
            }
        }
    }
}
