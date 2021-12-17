using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCommunication
{
    public class CommsInfo
    {
        public enum Status
        {
            DISCONNECTED,
            CONNECTED
        }
        Status _status;


        public Status status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        int _connectionNumber;
        public int ConnectionNumber
        {
            get
            {
                return _connectionNumber;
            }
            set
            {
                _connectionNumber = value;
            }
        }

        string _ip;
        public string Ip
        {
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
            }
        }

        //https://es.stackoverflow.com/questions/99286/saber-cuando-utilizar-las-palabras-claves-new-y-override
    }
}
