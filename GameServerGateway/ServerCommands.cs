using System;
using System.Collections.Generic;
using GameCommunication;
using GameServerFW;
using SrvComs; 


namespace GameServerGateway
{
    internal class ServerCommands
    {
        internal const string C_LOGUIN_TO_SERVER = "LoguinToServer";

        private ServerConnections _serverConnections;

        CommandsManager _commandsManager;
        GatewayCommands _gatewayCommands;

        //aca manejar los comandos que vengan en conection manager
        internal ServerCommands(GameServerManager gameServerManager)
        {
            _commandsManager = new CommandsManager();
            _serverConnections = new ServerConnections();
            _gatewayCommands = new GatewayCommands(gameServerManager);

        }
        
        /// <summary>
        /// 
        /// </summary>
        internal void CreateCommands()
        {
            
            Command command = new Command();
            command.SetExecCommand(_gatewayCommands.LoguinToServer);
            command.SetEnabled(true);
            command.SetName(C_LOGUIN_TO_SERVER);
            _commandsManager.AddCommand(command);

            //acá agrego el resto de los comandos
            
        }

        internal void ExecuteCommand(string name, List<string> commandParameters)
        {
            _commandsManager.ExecuteCommand(name, commandParameters);
        }

        internal void ExecuteCommand(string name, string commanParameter)
        {
            List<string> lstParam = new List<string>();
            lstParam.Add(commanParameter);

            ExecuteCommand(name, lstParam);
        }


        private string CreateClientId()
        {
            return DateTime.Now.ToString("ddMMyyyyHHmmssfff");
        }


        internal void GetMessage(string message,int connectionNumber,string hostTag)
        {
            //acá ver tema de seguridad, para que nadie pueda mandar un comando sin permiso
            //o modificar cosas
            //Console.WriteLine(message + " from connectionNumber => " + connectionNumber + " or hostTag => " + hostTag);
        }

        internal void AddHostInfo(HostInfo hostInfo)
        {
            _serverConnections.AddHostInfo(hostInfo);
        }

        internal void AddClientInfo(int connectionNumber,string ip)
        {
            _serverConnections.AddClientInfo(connectionNumber,ip, CreateClientId());
        }

        internal void UpdateHostInfo(int connectionNumber,string tag)
        {
            _serverConnections.UpdateHostInfo(connectionNumber, tag);
        }

        internal void DeleteInfo(int connectionNumber, string tag)
        {
            _serverConnections.DeleteInfo(connectionNumber, tag);
        }


    }
}
