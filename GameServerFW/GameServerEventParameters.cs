using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW
{
    public class GameServerEventParameters
    {

        
        public enum GameServerEventType
        {
            GAMESERVER_STARTING = 0,
            GAMESERVER_START_OK = 1,
            GAMESERVER_START_ERROR = 2,
            GAMESERVER_STOP = 3,

            GAMESERVER_LOAD_CONFIG_OK = 4,
            GAMESERVER_LOAD_CONFIG_ERROR = 5,
            GAMESERVER_READ_CONFIG_OK = 6,
            GAMESERVER_READ_CONFIG_ERROR = 7,
            GAMESERVER_CREATE_CONFIG_FILE_OK = 8,
            GAMESERVER_CREATE_CONFIG_FILE_ERROR = 9,
            GAMESERVER_CONFIG_FILE_NOT_FOUND = 10,

            GAMESERVER_MESSAGE_ERROR = 11,
            GAMESERVER_MESSAGE = 12,
        }

        private GameServerEventType _gameServerEventType;
        private string _message;

        public GameServerEventParameters(){}

        public GameServerEventParameters(GameServerEventType gameServerEventType)
        {
            _gameServerEventType = gameServerEventType;
        }

        internal GameServerEventParameters SetEventType(GameServerEventType gameServerEventType)
        {
            _gameServerEventType = gameServerEventType;
            return this;
        }

        internal GameServerEventParameters SetMessage(string message)
        {
            _message = message;
            return this;
        }

        public string GetMessage
        {
            get
            {
                return _message;
            }
        }

        public GameServerEventType GetGameServerEventType
        {
            get
            {
                return _gameServerEventType;
            }
        }
    }
}
