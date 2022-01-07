using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            Borrame();

        }
        internal void Borrame()
        {
            Command command = new Command();
            command.SetExecCommand(test);
            command.SetEnabled(true);
            command.SetName("test");

            _commandsManager.AddCommand(command);

            List<string> lst = new List<string>();
            lst.Add("Joaquina");

            //command.ExecuteCommand("test",lst);
            _commandsManager.ExecuteCommand("test", lst);
        }
        internal void CreateCommands()
        {
            
            Command command = new Command();
            command.SetExecCommand(_gatewayCommands.LoguinToServer);
            command.SetEnabled(true);
            command.SetName(C_LOGUIN_TO_SERVER);
            _commandsManager.AddCommand(command);
            
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

       
        private void test(string mensaje, List<string> parameters)
        {
            Console.WriteLine("hola " + parameters[0]);
        }

        internal void GetMessage(string message,int connectionNumber,string hostTag)
        {
            Console.WriteLine(message + " from connectionNumber => " + connectionNumber + " or hostTag => " + hostTag);

            //_gameServerManager.connectionsManager.SeverSendMessage(connectionNumber, "<<=OK=>>", GameServerFW.Connections.Protocol.ConnectionProtocol.TCP);
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
