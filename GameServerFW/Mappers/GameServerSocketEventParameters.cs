﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.Mappers
{
    public class GameServerSocketEventParameters
    {

        
        public enum SocketProtocol
        {
            TCP,
            UDP
        }

        int _connectionNumber;
        internal int SetConnectionNumber
        {
            set
            {
                _connectionNumber = value;
            }
        }
        public int GetConnectionNumber
        {
            get
            {
                return _connectionNumber;
            }
        }

        private int _listIndex;
        internal int SetListIndex
        {
            set
            {
                _listIndex = value;
            }
        }
        public int GetListIndex
        {
            get
            {
                return _listIndex;
            }
        }

      
        private bool _listenig;
        internal bool SetListenig
        {
            set
            {
                _listenig = value;
            }
        }
        public bool GetListening
        {
            get
            {
                return _listenig;
            }
        }

        private long _size;
        internal long SetSize
        {
            set
            {
                _size = value;
            }
        }
        public long GetSize
        {
            get
            {
                return _size;
            }
        }

        private string _data;
        internal string SetData
        {
            set
            {
                _data = value;
            }
        }
        public string GetData
        {
            get
            {
                return _data;
            }
        }

        private long _position;
        internal long SetPosition
        {
            set
            {
                _position = value;
            }
        }
        public long GetPosition
        {
            get
            {
                return _position;
            }
        }

        private string _clientIp;
        internal string SetClientIp
        {
            set
            {
                _clientIp = value;
            }
        }
        public string GetClientIp
        {
            get
            {
                return _clientIp + "";
            }
        }

        private string _serverIp;
        internal string SetServerIp
        {
            set
            {
                _serverIp = value;
            }
        }
        public string GetServerIp
        {
            get
            {
                return _serverIp + "";
            }
        }

        private SocketProtocol _socketProtocol;
        internal SocketProtocol SetSocketProtocol
        {
            set
            {
                _socketProtocol = value;
            }
        }
        public SocketProtocol GetSockectProtol
        {
            get
            {
                return _socketProtocol;
            }
        }

        private string _tag;
        internal string SetTag
        {
            set
            {
                _tag = value;
            }
        }

        public string GetTag
        {
            get
            {
                return _tag + "";
            }
        }
    }
}
