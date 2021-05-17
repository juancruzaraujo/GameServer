using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using GameServerFW;


namespace GameServerGateway
{
    class Program
    {

        static bool keepRuning;
        static GameServerFW.GameServerManager _gameServerManager;

        static void Main(string[] args)
        {
            _gameServerManager = GameServerFW.GameServerManager.GetGameServerInstance();
            _gameServerManager.Event_GameServer += new GameServerManager.Delegate_GameServer_Event(_gameserver_Event_GameServer);
            _gameServerManager.LoadConfig(args);

            _gameServerManager.Test();
            /*
            utils = new Utils();
            outputFormater = new OutputFormater();
            logger = new Logger.Logger("gatewayserverlog_"+ ".txt", Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar);
            logger.Event_Log += Logger_Event_Log;

            Console.WriteLine(logger.WriteLog("Iniciando GatewayServer"));

            config = configManager.GetConfig(args,logger);
            if (config==null)
            {
                Console.WriteLine(logger.WriteLog(utils.ReturnError() + " Load config"));
                logger.SaveLog();
                System.Environment.Exit(0);
            }
            else
            {

                Console.WriteLine(logger.WriteLog(utils.ReturnOk() + " Load config"));
            }

            StartServer();
            StartClientsToServers();

            keepRuning = true;
            logger.SaveLog();



            Console.WriteLine("inicio de tareas");
            while(keepRuning)
            {

            }
            //Console.ReadKey();
            */
            while (true)
            {

            }
        }

        private static void _gameserver_Event_GameServer(GameServerEventParameters gameServerEventParameters)
        {
            Console.WriteLine("evento");
        }

        private static void Logger_Event_Log(string eventMessage)
        {
            //Console.WriteLine(eventMessage);
        }


        /*
        int tcpPort = int.Parse(config.serverConfig.serverParameters.tcpPortNumber);
        int UdpPort = int.Parse(config.serverConfig.serverParameters.udpPortNumber);
        int maxCon = int.Parse(config.serverConfig.serverParameters.MaxUsers);

        gatewayServerTCP = new Sockets.Sockets();
        gatewayServerTCP.SetServer(tcpPort, Sockets.Sockets.C_DEFALT_CODEPAGE, true,maxCon);
        gatewayServerTCP.Event_Socket += GatewayServerTCP_Event_Socket;

        gatewayServerUDP = new Sockets.Sockets();
        gatewayServerUDP.SetServer(UdpPort, Sockets.Sockets.C_DEFALT_CODEPAGE, false, 0);
        gatewayServerUDP.Event_Socket += GetewayServertUDP_Event_Socket;

        gatewayServerTCP.StartServer();
        gatewayServerUDP.StartServer();
        */




        private static void StartClientsToServers()
        {
            /*
            int mapServersCount = config.serverConfig.mapServerList.Count();
            int otherServersCount = config.serverConfig.otherServer.Count();

            cliServersTCP = new Sockets.Sockets();
            cliServersTCP.ClientMode = true;
            cliServersTCP.Event_Socket += CliServersTCP_Event_Socket;

            for (int i= 0;i<mapServersCount;i++)
            {
                int tcpPortNumber = Convert.ToInt32(config.serverConfig.mapServerList[i].mapServerInfo.tcpPort);
                int udpPortNumber = Convert.ToInt32(config.serverConfig.mapServerList[i].mapServerInfo.udpPort);
                string host = config.serverConfig.mapServerList[i].mapServerInfo.host;

                cliServersTCP.ConnectClient(tcpPortNumber, host, C_SERVERS_TIMEOUT);
                Console.WriteLine("ok");
                
            }

            for (int i =0;i<otherServersCount;i++)
            {
                int tcpPortNumber = Convert.ToInt32(config.serverConfig.otherServer[i].otherServerInfo.tcpPort);
                int udpPortNumber = Convert.ToInt32(config.serverConfig.otherServer[i].otherServerInfo.udpPort);

                string host = config.serverConfig.otherServer[i].otherServerInfo.host;

                cliServersTCP.ConnectClient(tcpPortNumber, host, C_SERVERS_TIMEOUT);
                Console.WriteLine("ok");
            }
            
        }
        

        /*
        private static void CliServersTCP_Event_Socket(EventParameters eventParameters)
        {
            /*
            switch (eventParameters.GetEventType)
            {
                case Sockets.EventParameters.EventType.ERROR:
                    Console.WriteLine("ERROR ");
                    break;

                case EventParameters.EventType.TIME_OUT:
                    Console.WriteLine(logger.WriteLog(utils.ReturnError() + " time out " + eventParameters.GetServerIp));
                    break;
                
            }
        }


    private static void GatewayServerTCP_Event_Socket(Sockets.EventParameters eventParameters)
        {
            
            switch(eventParameters.GetEventType)
            {
                case Sockets.EventParameters.EventType.SERVER_START:
                    outputFormater.SetBold(true);
                    Console.WriteLine(logger.WriteLog(utils.ReturnOk() + " start server on TCP port " + outputFormater.FormatText(config.serverConfig.serverParameters.tcpPortNumber, OutputFormater.TextColorFG.Bright_Green, OutputFormater.TextColorBG.Black)));
                    break;
            }
        }

        private static void GetewayServertUDP_Event_Socket(Sockets.EventParameters eventParameters)
        {
            
            switch (eventParameters.GetEventType)
            {
                case Sockets.EventParameters.EventType.SERVER_START:
                    outputFormater.SetBold(true);
                    Console.WriteLine(logger.WriteLog(utils.ReturnOk() + " start server on UDP port " + outputFormater.FormatText(config.serverConfig.serverParameters.udpPortNumber, OutputFormater.TextColorFG.Bright_Green, OutputFormater.TextColorBG.Black)));
                    break;
            }
            
        }
        */
        }
    }
}
