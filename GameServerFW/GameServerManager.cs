
using ConsoleOutputFormater;
using GameServerFW.Mappers;

namespace GameServerFW
{
    /// <summary>
    /// use GetGameServerInstance to create singleton objet
    /// </summary>
    public class GameServerManager
    {

        #if DEBUG
            private static bool debug_Mode = true;
            const int C_MAX_LOG_LINES_TO_SAVE = 0;
        #else
            private static bool debug_Mode = false;
            const int C_MAX_LOG_LINES_TO_SAVE = 20;
        #endif

        //https://refactoring.guru/es/design-patterns/singleton/csharp/example#example-0

        const int C_SERVERS_TIMEOUT = 5;

        private GameServerEventMapper _gameServerEventParameters;

        private ConfigManager _configManager;

        private Utils utils;

        //private LoggerManager _loggerManager;
        public LoggerManager loggerManager;

        private UserManager _userManager;
        private CommunicationsManager _connectionsManager;

        static GameServerManager _gameServerInstance;

        //public Prueba prueba;

        public delegate void Delegate_GameServer_Event(GameServerEventParameters gameServerEventParameters);
        public event Delegate_GameServer_Event Event_GameServer;
        private void EventGameServer(GameServerEventParameters gameServerEventParameters)
        {
            this.Event_GameServer(gameServerEventParameters);
        }

        private GameServerManager()
        {

            _gameServerEventParameters = new GameServerEventMapper();
            _gameServerEventParameters.Event_GameServerEventManager += GameServerEventParameters_Event_GameServerEventManager;


            _configManager = new ConfigManager();
            _configManager.Event_ConfigManager += new ConfigManager.Delegate_ConfigManager_Event(_gameServerEventParameters.ConfigManagerEvent);

            loggerManager = new LoggerManager();
            loggerManager.Event_Log += new LoggerManager.Delegate_Log_Event(_gameServerEventParameters.LoggerManagerEvent);

            _connectionsManager = new CommunicationsManager();
            connectionsManager.ConnectionManagerEvents += new CommunicationsManager.Delegate_Server_Events(_gameServerEventParameters.ConnectionsManager_ConnectionManagerEvents);

            _userManager = new UserManager();
            _userManager.ConnectionManagerEvents += new UserManager.Delegate_UserManager_Events(_gameServerEventParameters.UserManager_UserManagerEvents);

            //prueba = new Prueba("hola mundo");

        }

        /// <summary>
        /// return true if we are run in debug mode
        /// </summary>
        public static bool Get_debug_mode
        {
            get
            {
                return debug_Mode;
            }
        }

        /*
        public Prueba GetPrueba
        {
            get
            {
                return prueba;
            }
        }
        */

        public ConfigManager configManager
        {
            get
            {
                return _configManager;
            }
        }

        public UserManager userManager
        {
            get
            {
                return _userManager;
            }
        }

        public CommunicationsManager connectionsManager
        {
            get
            {
                return _connectionsManager;
            }
        }


        private void GameServerEventParameters_Event_GameServerEventManager(GameServerEventParameters gameServerEventParameters)
        {            
            EventGameServer(gameServerEventParameters);
        }

        /// <summary>
        /// create a singleton instance
        /// </summary>
        /// <returns></returns>
        public static GameServerManager GetGameServerInstance()
        {
            if (_gameServerInstance == null)
            {
                _gameServerInstance = new GameServerManager();

            }

            return _gameServerInstance;
        }

        

    }
}
