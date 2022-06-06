using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Sockets;
using ShowAndLogMessage;
using ConsoleOutputFormater;

namespace DummyClients
{
    class Program
    {
        static Socket clientTCP;
        static Socket clientUDP;
        static LogInfo msg;
        static int port;
        static string host;
        static int maxCon;
        static string msgType;

        static void Main(string[] args)
        {

            //ejecutar con parámetros
            //ip, puerto, cantidad conexiones
            //DummyClient 200.1.1.10 1987 10

            msg = LogInfo.GetInstance();

            OutputFormatterAttributes attr = new OutputFormatterAttributes();
            attr.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Blue).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
            msgType = msg.CustomType("INFO", attr);

            //la cantidad de argumentos es mala
            if (args.Count() != 3)
            {
                
                msg.ShowMessage(msgType + " ejecutar con los paramteros: IP, numero de puerto, cantidad maxima de conexiones.");
                msg.ShowMessage(msgType + " DummyClients 127.0.0.1 8080 10");
                
                Console.ReadKey();
            }

            host = args[0];
            port = int.Parse(args[1]);
            maxCon = int.Parse(args[2]);

            clientTCP = new Socket();
            clientTCP.Event_Socket += ClientTCP_Event_Socket;

            clientUDP = new Socket();
            clientUDP.Event_Socket += ClientUDP_Event_Socket;

            msg.ShowMessage("start client", null, LogInfo.typeMsg.OK);

            for (int i =0;i<maxCon;i++)
            {
                msg.ShowMessage(msgType + " Connectinig " + i);
                ConnectionParameters connection = new ConnectionParameters();
                connection.SetPort(port)
                    .SetHost(host)
                    .SetConnectionTag("client_" + i);
                clientTCP.ConnectClient(connection);
            }

            msg.ShowMessage(msgType + " press enter to exit");
            Console.ReadKey();
            System.Environment.Exit(0);
        }

       

        private static void ClientUDP_Event_Socket(EventParameters eventParameters)
        {
            
        }

        private static void ClientTCP_Event_Socket(EventParameters eventParameters)
        {
            switch(eventParameters.GetEventType)
            {
                case EventParameters.EventType.CLIENT_CONNECTION_OK:
                    msg.ShowMessage("connection to " + host + " connection number " + eventParameters.GetConnectionNumber, null, LogInfo.typeMsg.OK);
                    clientTCP.Send(eventParameters.GetConnectionNumber, "hola mundo");
                    break;

                case EventParameters.EventType.DATA_IN:
                    msg.ShowMessage(msgType + " " + eventParameters.GetData);
                    //clientTCP.Disconnect(eventParameters.GetConnectionNumber);
                    //Thread.Sleep(500);
                    //clientTCP.ConnectClient(port, host);
                    break;

                case EventParameters.EventType.ERROR:
                    msg.ShowMessage(eventParameters.GetData, null, LogInfo.typeMsg.ERROR);
                    break;
            }
        }
    }
}
