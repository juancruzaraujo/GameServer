using GameServerFW.Mappers;

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
            GAMESERVER_SAVE_LOG_ERROR,

            GAMESERVER_CLIENT_CONNECTION_OK,
            GAMESERVER_CLIENT_TIME_OUT,
            GAMESERVER_CONNECTION_LIMIT,
            GAMESERVER_DATA_IN,
            GAMESERVER_END_CONNECTION,
            GAMESERVER_SOCKET_ERROR,
            GAMESERVER_RECIEVE_TIMEOUT,
            GAMESERVER_SEDN_ARRAY_COMPLETE,
            GAMESERVER_ACCEPT_CONNECTION,
            GAMESERVER_NEW_CONNECTION,
            GAMESERVER_START_SERVER_LISTENING,
            GAMESERVER_STOP_SERVER_LISTENING,

            GAMESERVER_CREATE_USER_OK,
            GAMESERVER_CREATE_USER_ERROR,
            GAMESERVER_USER_FOUND,
            GAMESERVER_USER_NOT_FOUND,
            GAMESERVER_DELETE_USER_OK,
            GAMESERVER_DELETE_USER_ERROR

        }

        private GameServerEventType _gameServerEventType;
        private string _message;
        private GameServerSocketEventParameters _socketEventParameters;
        private bool _eventLaunched;

        public GameServerEventParameters(){ _eventLaunched = false; }

        public GameServerEventParameters(GameServerEventType gameServerEventType)
        {
            _gameServerEventType = gameServerEventType;
        }

        internal GameServerEventParameters SetEventType(GameServerEventType gameServerEventType)
        {
            _eventLaunched = true;
            _gameServerEventType = gameServerEventType;
            return this;
        }

        internal GameServerEventParameters SetMessage(string message)
        {
            _message = message;
            return this;
        }

        internal GameServerEventParameters SetSocketEventParameters(GameServerSocketEventParameters socketEventParameters)
        {
            _socketEventParameters = socketEventParameters;
            return this;
        }


        public GameServerSocketEventParameters GetSocketEventParameters
        {
            get
            {
                return _socketEventParameters;
            }
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

        /// <summary>
        /// if any event was set this takes true value, otherwise it continues with false value
        /// </summary>
        public bool EventLunched
        {
            get
            {
                return _eventLaunched;
            }
        }
    }
}
