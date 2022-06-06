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
        string _host;
        string _tcpPort;
        string _udpPort;
        string _serverType;
        string _identifierTag;
        string _user;
        string _password;

        public ServerInfo()
        {
            _host = "";
            _tcpPort = "";
            _udpPort = "";
            _serverType = "";
            _identifierTag = "";
            _user = "";
            _password = "";
        }

        [DataMember]
        public string host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
            }
        }

        [DataMember]
        public string tcpPort
        {
            get
            {
                return _tcpPort;
            }
            set
            {
                _tcpPort = value;
            }
        }

        [DataMember]
        public string udpPort
        {
            get
            {
                return _udpPort;
            }
            set
            {
                _udpPort = value;
            }
        }

        [DataMember]
        public string serverType
        {
            get
            {
                return _serverType;
            }
            set
            {
                _serverType = value;
            }

        }

        //https://stackoverflow.com/questions/25570712/is-it-possible-to-wrap-json-in-json-field-like-a-string
        [DataMember]
        public string identifierTag
        {
            get
            {
                return _identifierTag;
            }
            set
            {
                _identifierTag = value;
            }
        }


        [DataMember]
        public string user
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }

        }
        
        [DataMember]
        public string password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value; 
            }
        }
    }
}
