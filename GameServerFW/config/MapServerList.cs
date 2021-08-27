using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.config
{
    [DataContract]
    public class MapServerList
    {
        [DataMember]
        public MapServerInfo mapServerInfo { get; set; }
        /*
        public MapServerList()
        {
            //mapServerInfo = new MapServerInfo();
            mapServerInfo = new List<MapServerInfo>();
        }*/
    }
}
