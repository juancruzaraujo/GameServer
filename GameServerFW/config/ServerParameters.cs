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
        string _serverName;
        string _tcpPortNumber;
        string _udpPortNumber;
        string _maxUsers;
        string _recieveTimeOut;
        string _logFileName;
        string _logPathFile;
        List<AdminUsers> _adminUsers;

        public ServerParameters()
        {
            _serverName = "";
            _tcpPortNumber = "";
            _udpPortNumber = "";
            _maxUsers = "";
            _recieveTimeOut = "";
            _logFileName = "";
            _logPathFile = "";
            List<AdminUsers> _adminUsers = new List<AdminUsers>();
        }

        [DataMember]
        public string serverName
        {
            get
            {
                return _serverName;
            }
            set
            {
                _serverName = value;
            }
        }

        [DataMember]
        public string tcpPortNumber
        {
            get
            {
                return _tcpPortNumber;
            }
            set
            {
                _tcpPortNumber = value;
            }
        }

        [DataMember]
        public string udpPortNumber
        {
            get
            {
                return _udpPortNumber;
            }
            set
            {
                _udpPortNumber = value;
            }
        }

        [DataMember]
        public string maxUsers
        {
            get
            {
                return _maxUsers;
            }
            set
            {
                _maxUsers = value;
            }
        }
        
        [DataMember]
        public string recieveTimeOut
        {
            get
            {
                return _recieveTimeOut;
            }
            set
            {
                _recieveTimeOut = value;
            }
        }

        [DataMember]
        public string logFileName
        {
            get
            {
                return _logFileName;
            }
            set
            {
                _logFileName = value;
            }
        }

        [DataMember]
        public string logPathFile
        {
            get
            {
                return _logPathFile;
            }
            set
            {
                _logPathFile = value;
            }
        }

        [DataMember]
        public List<AdminUsers> adminUsers
        {
            get
            {
                return _adminUsers;
            }
            set
            {
                _adminUsers = value;
            }
        }

    }
}
