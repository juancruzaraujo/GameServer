using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.Connections
{
    internal class HostInfo : ConnectionInfo
    {
        string _serverTag;

        internal string GetServerTag
        {
            get
            {
                return _serverTag;
            }
        }

        internal HostInfo(string serverTag)
        {
            _serverTag = serverTag;
        }
    }
}
