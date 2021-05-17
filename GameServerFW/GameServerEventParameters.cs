using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW
{
    public class GameServerEventParameters
    {

        private GameServerEventType _gameServerEventType;
        private string _messageError;
        private string _message;
        public enum GameServerEventType
        {
            GAMESERVER_STARTING = 0,
            GAMESERVER_START_OK = 1,
            GAMESERVER_START_ERROR = 2,
            GAMESERVER_LOAD_CONFIG_OK = 3,
            GAMESERVER_LOAD_CONFIG_ERROR = 4,
            GAMESERVER_MESSAGE_ERROR = 5,
            GAMESERVER_MESSAGE = 6,
            GAMESERVER_STOP =7   
        }

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

        internal GameServerEventParameters SetMessageError(string message)
        {
            _messageError = message;
            return this;
        }

        internal GameServerEventParameters SetMessage(string message)
        {
            _message = message;
            return this;
        }

        public string GetErrorMessage()
        {
            return _messageError;
        }

        public string GetMessage()
        {
            return _message;
        }
    }
}
