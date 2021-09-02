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
            GAMESERVER_STARTING,
            GAMESERVER_START_OK,
            GAMESERVER_START_ERROR,
            GAMESERVER_STOP,

            GAMESERVER_LOAD_CONFIG_OK,
            GAMESERVER_LOAD_CONFIG_ERROR,
            GAMESERVER_READ_CONFIG_OK,
            GAMESERVER_READ_CONFIG_ERROR,
            GAMESERVER_CREATE_CONFIG_FILE_OK,
            GAMESERVER_CREATE_CONFIG_FILE_ERROR,
            GAMESERVER_CONFIG_FILE_NOT_FOUND,

            GAMESERVER_MESSAGE_ERROR,
            GAMESERVER_MESSAGE,

            GAMESERVER_CREATE_OR_APPEND_LOG_FILE_OK,
            GAMESERVER_CREATE_OR_APPEND_LOG_FILE_ERROR,
            GAMESERVER_WRITE_LOG_FILE_OK,
            GAMESERVER_WRITE_LOG_FILE_ERROR,
            GAMESERVER_SAVE_LOG_OK,
            GAMESERVER_SAVE_LOG_ERROR
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
