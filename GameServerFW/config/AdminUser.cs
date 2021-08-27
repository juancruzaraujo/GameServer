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

        [DataMember]
        public string usr { get; set; }

        [DataMember]
        public string pass { get; set; }
    }
}
