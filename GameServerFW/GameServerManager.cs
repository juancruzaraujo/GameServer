using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;
using Sockets;
using ConsoleOutputFormater;
using GameServerFW.config;
using System.IO;

namespace GameServerFW
{
    public class GameServerManager
    {
        //https://refactoring.guru/es/design-patterns/singleton/csharp/example#example-0

        const int C_SERVERS_TIMEOUT = 5;

        Sockets.Sockets _gatewayServerTCP;
        Sockets.Sockets _gatewayServerUDP;

        Sockets.Sockets _cliServersTCP;
        Sockets.Sockets _cliServersUDP;

        Config _config;
        ConfigManager _configManager = new ConfigManager();
        Utils utils;
        OutputFormater _outputFormater;
        private Logger.Logger _logger;
        static string[] _args;

        static GameServerManager _gameServerInstance;

        
        public delegate void Delegate_GameServer_Event(GameServerEventParameters gameServerEventParameters);
        public event Delegate_GameServer_Event Event_GameServer;
        private void EventGameServer(GameServerEventParameters gameServerEventParameters)
        {
            this.Event_GameServer(gameServerEventParameters);
        }

        private GameServerManager() { }

        public static GameServerManager GetGameServerInstance()
        {
            if (_gameServerInstance == null)
            {
                _gameServerInstance = new GameServerManager();
            }

            
            return _gameServerInstance;
        }
        
        public void LoadConfig(string[] args)
        {

            _args = args;
            utils = new Utils();
            _outputFormater = new OutputFormater();


            //mandar esto a un evento
            //Console.WriteLine(_logger.WriteLog("Iniciando GatewayServer"));

            _config = _configManager.GetConfig(_args, _logger);
            if (_config == null)
            {
                //Console.WriteLine(_logger.WriteLog(utils.ReturnError() + " Load config"));
                //_logger.SaveLog();
                //crear evento de error
                //System.Environment.Exit(0);
            }
            //else
            //{
            //enviar a un evento
            //Console.WriteLine(_logger.WriteLog(utils.ReturnOk() + " Load config"));
            //}
            //salio todo ok,
            _logger = new Logger.Logger(_config.ServerConfig.serverParameters.logFileName, Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar);
            _logger.Event_Log += Logger_Event_Log;


            //StartServer();
            //StartClientsToServers();

            //keepRuning = true;
            _logger.SaveLog();



            //Console.WriteLine("inicio de tareas");

        }

        public string WriteLog(string log)
        {
            return (_logger.WriteLog(log));
        }

        public string WriteAndSaveLog(string log)
        {
            return WriteLog(_logger.WriteAndSaveLog(log));
        }



        public void Test()
        {
            GameServerEventParameters ev = new GameServerEventParameters();

            this.EventGameServer(ev);
            Console.WriteLine("test");
        }

        private void Logger_Event_Log(string eventMessage)
        {
            //Console.WriteLine(eventMessage);
        }
    }
}
