using GameServerFW.EventParameters;

namespace GameServerFW.Mappers
{
    internal class GameServerEventMapper
    {

        GameServerManager _gameServerManagerInstance;

        public delegate void Delegate_GameServerEventManager_Event(GameServerEventParameters gameServerEventParameters);
        public event Delegate_GameServerEventManager_Event Event_GameServerEventManager;
        private void EventGameServerEventsManager(GameServerEventParameters gameServerEventParameters)
        {
            if (gameServerEventParameters.EventLunched)
            {
                this.Event_GameServerEventManager(gameServerEventParameters);
            }
        }

        internal void ConfigManagerEvent(ConfigManagerEventsParameters eventParameters)
        {

            GameServerEventParameters gameServerEventParameters = new GameServerEventParameters();
            gameServerEventParameters.SetMessage(eventParameters.GetMessage);

            switch (eventParameters.GetEventType)
            {
                case ConfigManagerEventsParameters.ConfigManagerEventType.LOAD_CONFIG_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_LOAD_CONFIG_OK);
                    break;

                case ConfigManagerEventsParameters.ConfigManagerEventType.LOAD_CONFIG_ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_LOAD_CONFIG_ERROR);
                    break;

                case ConfigManagerEventsParameters.ConfigManagerEventType.READ_CONFIG_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_READ_CONFIG_OK);
                    break;

                case ConfigManagerEventsParameters.ConfigManagerEventType.READ_CONFIG_ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_READ_CONFIG_ERROR);
                    break;

                case ConfigManagerEventsParameters.ConfigManagerEventType.CREATE_CONFIG_FILE_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_CONFIG_FILE_OK);
                    break;

                case ConfigManagerEventsParameters.ConfigManagerEventType.CREATE_CONFIG_FILE_ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_CONFIG_FILE_ERROR);
                    break;

                case ConfigManagerEventsParameters.ConfigManagerEventType.FILE_NOT_FOUND:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CONFIG_FILE_NOT_FOUND);
                    break;
            }

            EventGameServerEventsManager(gameServerEventParameters);
        }

        internal void LoggerManagerEvent(LoggerManagerEventsParameters eventMessage)
        {
            GameServerEventParameters gameServerEventParameters = new GameServerEventParameters();
            gameServerEventParameters.SetMessage(eventMessage.GetEventMessage);

            switch (eventMessage.GetEventType)
            {
                case LoggerManagerEventsParameters.LoggerManagerEventType.CREATE_OR_APPEND_LOG_FILE_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_OR_APPEND_LOG_FILE_OK);
                    break;

                case LoggerManagerEventsParameters.LoggerManagerEventType.CREATE_OR_APPEND_LOG_FILE_ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_OR_APPEND_LOG_FILE_ERROR);
                    break;

                case LoggerManagerEventsParameters.LoggerManagerEventType.WRITE_LOG_FILE_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_WRITE_LOG_FILE_OK);
                    break;

                case LoggerManagerEventsParameters.LoggerManagerEventType.WRITE_LOG_FILE_ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_WRITE_LOG_FILE_ERROR);
                    break;

                case LoggerManagerEventsParameters.LoggerManagerEventType.SAVE_LOG_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_SAVE_LOG_OK);
                    break;

                case LoggerManagerEventsParameters.LoggerManagerEventType.SAVE_LOG_ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_SAVE_LOG_ERROR);
                    break;
            }
            EventGameServerEventsManager(gameServerEventParameters);
        }

        internal void ConnectionsManager_ConnectionManagerEvents(Sockets.EventParameters socketEventParameters)
        {

           
            GameServerEventParameters gameServerEventParameters = new GameServerEventParameters();
            GameServerSocketEventParameters gameServerSocketEventParameters = new GameServerSocketEventParameters();

            gameServerSocketEventParameters.SetClientIp = socketEventParameters.GetClientIp;
            gameServerSocketEventParameters.SetConnectionNumber = socketEventParameters.GetConnectionNumber;
            gameServerSocketEventParameters.SetData = socketEventParameters.GetData;
            gameServerSocketEventParameters.SetListenig = socketEventParameters.GetListening;
            gameServerSocketEventParameters.SetListIndex = socketEventParameters.GetListIndex;
            gameServerSocketEventParameters.SetPosition = socketEventParameters.GetPosition;
            gameServerSocketEventParameters.SetServerIp = socketEventParameters.GetServerIp;
            gameServerSocketEventParameters.SetSize = socketEventParameters.GetSize;
            gameServerSocketEventParameters.SetTag = socketEventParameters.GetClientTag;

            if (socketEventParameters.GetProtocol == Sockets.Protocol.ConnectionProtocol.TCP)
            {
                gameServerSocketEventParameters.SetSocketProtocol = GameServerSocketEventParameters.SocketProtocol.TCP;
            }
            if (socketEventParameters.GetProtocol == Sockets.Protocol.ConnectionProtocol.UDP)
            {
                gameServerSocketEventParameters.SetSocketProtocol = GameServerSocketEventParameters.SocketProtocol.UDP;
            }

            gameServerEventParameters.SetSocketEventParameters(gameServerSocketEventParameters);

            switch (socketEventParameters.GetEventType)
            {
                case Sockets.EventParameters.EventType.CLIENT_CONNECTION_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CLIENT_CONNECTION_OK);
                    break;

                case Sockets.EventParameters.EventType.CLIENT_TIME_OUT:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CLIENT_TIME_OUT);
                    break;

                case Sockets.EventParameters.EventType.CONNECTION_LIMIT:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CONNECTION_LIMIT);
                    break;

                case Sockets.EventParameters.EventType.DATA_IN:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_DATA_IN);
                    gameServerEventParameters.SetMessage(socketEventParameters.GetData);
                    break;

                case Sockets.EventParameters.EventType.END_CONNECTION:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_END_CONNECTION);
                    break;

                case Sockets.EventParameters.EventType.ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_SOCKET_ERROR);
                    gameServerEventParameters.SetMessage(socketEventParameters.GetData);
                    break;

                case Sockets.EventParameters.EventType.RECIEVE_TIMEOUT:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_RECIEVE_TIMEOUT);
                    break;

                case Sockets.EventParameters.EventType.SEND_ARRAY_COMPLETE:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_SEND_ARRAY_COMPLETE);
                    break;

                case Sockets.EventParameters.EventType.SERVER_ACCEPT_CONNECTION:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_ACCEPT_CONNECTION);
                    break;

                case Sockets.EventParameters.EventType.SERVER_NEW_CONNECTION:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_NEW_CONNECTION);
                    break;

                case Sockets.EventParameters.EventType.SERVER_START:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_START_SERVER_LISTENING);
                    break;

                case Sockets.EventParameters.EventType.SERVER_STOP:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_STOP_SERVER_LISTENING);
                    break;
            }

            EventGameServerEventsManager(gameServerEventParameters);
            
        }

        internal void UserManager_UserManagerEvents(UserManagerEventsParameters userManagerEventsParameters)
        {
            GameServerEventParameters gameServerEventParameters = new GameServerEventParameters();
            if (gameServerEventParameters.GetMessage != "")
            {
                gameServerEventParameters.SetMessage(gameServerEventParameters.GetMessage);
            }

            switch (userManagerEventsParameters.GetEventType)
            {
                case UserManagerEventsParameters.UserManagerEventType.USER_FOUND:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_USER_FOUND);
                    break;

                case UserManagerEventsParameters.UserManagerEventType.USER_NOT_FOUND:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_USER_NOT_FOUND);
                    break;

                case UserManagerEventsParameters.UserManagerEventType.CREATE_USER_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_USER_OK);
                    break;

                case UserManagerEventsParameters.UserManagerEventType.CREATE_USER_ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_CREATE_USER_ERROR);
                    break;

                case UserManagerEventsParameters.UserManagerEventType.DELETE_USER_OK:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_DELETE_USER_OK);
                    break;

                case UserManagerEventsParameters.UserManagerEventType.DELETE_USER_ERROR:
                    gameServerEventParameters.SetEventType(GameServerEventParameters.GameServerEventType.GAMESERVER_DELETE_USER_ERROR);
                    break;
            }

            EventGameServerEventsManager(gameServerEventParameters);
        }


    }
}
