using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCommunication
{
    public class HostInfo : CommsInfo
    {
        string _serverTag;

        public string GetServerTag
        {
            get
            {
                return _serverTag;
            }
        }

        public HostInfo(string serverTag) 
        {
            _serverTag = serverTag;
        }

    }

}
