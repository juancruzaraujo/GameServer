using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationObjects.Login
{
    [DataContract]
    public class Login
    {
        [DataMember]
        public string user { get; set; }

        [DataMember]
        public string password { get; set; }
    }
}
