using GameServerFW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using CommunicationObjects.Login;
using CommunicationObjects;

namespace GameServerGateway
{
    internal class GatewayCommands
    {

        GameServerManager _gameServerManager;
       


        private class SendParams
        {
            internal enum Mode
            {
                CLIENT,
                SERVER
            }
            
            internal string messageTag { get; set; }
            internal string messageJson { get; set; }
            internal string strConnectionNumber { get; set; }
            //internal GameServerFW.Connections.Protocol.ConnectionProtocol connectionProtocol { get; set; }
            internal Mode mode { get; set; }
            internal bool msgToAll { get; set; }

            internal SendParams() //whit default values
            {
                mode = Mode.CLIENT;
                //connectionProtocol = GameServerFW.Connections.Protocol.ConnectionProtocol.TCP;
                msgToAll = false;
            }

        }

        internal GatewayCommands(GameServerManager gameServerManager)
        {
            _gameServerManager = gameServerManager;
        }

        internal void LoguinToServer(string mensaje, List<string> lstParameters)
        {
            const int C_CONNECTION_NUMBER = 0;
            const int C_TAG = 1;
            Login login = new Login();

            for (int i = 0; i<_gameServerManager.configManager.GetConfig.serverConfig.destinyServers.Count(); i++)
            {
                string confTag = _gameServerManager.configManager.GetConfig.serverConfig.destinyServers[i].serverInfo.identifierTag;
                if (lstParameters[C_TAG] == confTag)
                {
                    login.user = _gameServerManager.configManager.GetConfig.serverConfig.destinyServers[i].serverInfo.user;
                    login.password = _gameServerManager.configManager.GetConfig.serverConfig.destinyServers[i].serverInfo.password;

                    string json = ClassToJson(typeof(Login), login);
                    SendParams sendParams = new SendParams();
                    sendParams.messageTag = ServerCommands.C_LOGUIN_TO_SERVER;
                    sendParams.messageJson = json;
                    sendParams.strConnectionNumber = lstParameters[C_CONNECTION_NUMBER];
                    
                    Send(sendParams);

                    break;
                }
            }
        }

        
        private string ClassToJson(Type theClass, object obj)
        {

            var stream1 = new MemoryStream();
            var ser = new DataContractJsonSerializer(theClass);
            ser.WriteObject(stream1, obj);

            stream1.Position = 0;
            var sr = new StreamReader(stream1);
                        
            string json = sr.ReadToEnd();
            return json;
        }
        

        /// <summary>
        /// creates the body of the message to send, the parameter that it receives is precisely the body of the message 
        /// </summary>
        /// <param name="sendParams">body of the message</param>
        private void Send(SendParams sendParams)
        {
            
            int connectionNumber = Convert.ToInt32(sendParams.strConnectionNumber);

            BodyMessage bodyMessage = new BodyMessage();
            bodyMessage.messageTag = sendParams.messageTag;
            bodyMessage.messageBody = sendParams.messageJson;

            string bodyMsg = ClassToJson(typeof(BodyMessage), bodyMessage);
            /*
            if (sendParams.connectionProtocol == GameServerFW.Connections.Protocol.ConnectionProtocol.TCP)
            {
                if (sendParams.msgToAll)
                {
                    if (sendParams.mode == SendParams.Mode.CLIENT)
                    {
                        _gameServerManager.connectionsManager.ClientSendMessageToAll(bodyMsg, sendParams.connectionProtocol);
                    }
                    else
                    {
                        _gameServerManager.connectionsManager.ServerSendMessageToAll(bodyMsg, sendParams.connectionProtocol);
                    }
                }
                else
                {
                    if (sendParams.mode == SendParams.Mode.CLIENT)
                    {
                        _gameServerManager.connectionsManager.ClientSendMessage(bodyMsg, connectionNumber, sendParams.connectionProtocol);
                    }
                    else
                    {
                        _gameServerManager.connectionsManager.SeverSendMessage(connectionNumber,bodyMsg, sendParams.connectionProtocol);
                    }

                }
            }
            */

        }

        
    }
}
