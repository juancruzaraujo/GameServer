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
        private ServerConnections _serverConnections;

        CommandsManager _commandsManager;
        GameServerManager _gameServerManager;

        //aca manejar los comandos que vengan en conection manager
        internal ServerCommands(GameServerManager gameServerManager)
        {
            _gameServerManager = gameServerManager;
            _commandsManager = new CommandsManager();
            _serverConnections = new ServerConnections();


            Borrame();

        }

        private string CreateClientId()
        {
            return DateTime.Now.ToString("ddMMyyyyHHmmssfff");
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
        private void test(string mensaje, List<string> parameters)
        {
            Console.WriteLine("hola " + parameters[0]);
        }

        internal void GetMessage(string message,int connectionNumber,string hostTag)
        {
            Console.WriteLine(message + " from connectionNumber => " + connectionNumber + " or hostTag => " + hostTag);

            _gameServerManager.connectionsManager.SeverSendMessage(connectionNumber, "<<=OK=>>", GameServerFW.Connections.Protocol.ConnectionProtocol.TCP);
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
