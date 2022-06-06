using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.config
{
    [DataContract]
    public class AdminUser
    {
        string _usr;
        string _pass;
        public AdminUser()
        {
            _usr = "";
            _pass = "";
        }

        [DataMember]
        public string usr
        {
            get
            {
                return _usr;
            }
            set
            {
                _usr = value;
            }
        }

        [DataMember]
        public string pass
        {
            get
            {
                return _pass;
            }
            set
            {
                _pass = value;
            }
        }
    }
}
