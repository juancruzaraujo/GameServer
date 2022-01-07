using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationObjects
{
    [DataContract]
    public class BodyMessage
    {
        [DataMember]
        public string messageTag { get; set; }

        [DataMember]
        public string message { get; set; }
    }
}
