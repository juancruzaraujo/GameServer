using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sockets;
using ConsoleOutputFormater;
using ShowAndLogMessage;

namespace DummyServer
{
    class Program
    {

        static Sockets.Sockets socketsTCP;
        static Sockets.Sockets socketsUDP;
        static OutputFormatter outputFormater;
        

        static int portNumber;
        static string serverType;
        static bool keepRuning;

        static void Main(string[] args)
        {
            outputFormater = new ConsoleOutputFormater.OutputFormatter();
            OutputFormatterAttributes attr = new OutputFormatterAttributes();
            LoggerMessage msg = LoggerMessage.GetInstance();
            try
            {

                //msg.ShowMessage("Dummy server", attr, LoggerMessage.typeMsg.ERROR);
                //msg.ShowMessage("Dummy server", attr, LoggerMessage.typeMsg.OK);
                //msg.ShowMessage("Dummy server", attr, LoggerMessage.typeMsg.OK);
                //msg.ShowMessage("Dummy server", attr, LoggerMessage.typeMsg.OK);
                //msg.ShowMessage("Dummy server", attr, LoggerMessage.typeMsg.WARNING);

                if (args.Length == 0 || args.Length == 1)
                {
                    //attr.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Red);
                    msg.ShowMessage(" \r\nparams[port number][server type]", null, LoggerMessage.typeMsg.ERROR);

                    System.Environment.Exit(0);
                }

                portNumber = int.Parse(args[0]);
                serverType = args[1];

                socketsTCP = new Sockets.Sockets();
                socketsUDP = new Sockets.Sockets();

                socketsTCP.SetServer(portNumber, Sockets.Sockets.C_DEFALT_CODEPAGE, true, 10);
                socketsUDP.SetServer(portNumber, Sockets.Sockets.C_DEFALT_CODEPAGE, false, 0);

                socketsTCP.Event_Socket += SocketsTCP_Event_Socket;
                socketsUDP.Event_Socket += SocketsUDP_Event_Socket;

                msg.ShowMessage("Dummy server ", null, LoggerMessage.typeMsg.NO_TYPE);
                             
                
                msg.ShowMessage("tcp port " + portNumber, null, LoggerMessage.typeMsg.OK);
                msg.ShowMessage("udp port " + portNumber, null, LoggerMessage.typeMsg.OK);
                msg.ShowMessage("server type  " + serverType, null, LoggerMessage.typeMsg.OK);
                


                socketsTCP.StartServer();
                msg.ShowMessage("tcp start " + serverType, null, LoggerMessage.typeMsg.OK);

                socketsUDP.StartServer();
                msg.ShowMessage("udp start " + serverType, null, LoggerMessage.typeMsg.OK);


                attr.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Red).SetColorBG(OutputFormatterAttributes.TextColorBG.Bright_Green);
                msg.ShowMessage("test test no", attr,LoggerMessage.typeMsg.ERROR); //falla
                msg.ShowMessage("test test si", null, LoggerMessage.typeMsg.ERROR); //no falla
                msg.ShowMessage("no rojo 22222222222222", null);
                msg.ShowMessage("formateado", attr);

                keepRuning = true;

                while(keepRuning)
                {

                }
            }
            catch(Exception err)
            {
                WriteMessage(err.Message);
            }

        }

        private static void SocketsUDP_Event_Socket(EventParameters eventParameters)
        {
            switch(eventParameters.GetEventType)
            {
                case EventParameters.EventType.DATA_IN:
                    DataIn(eventParameters.GetData);
                    socketsUDP.Send(eventParameters.GetConnectionNumber, "MSG UDP RECIVED OK ");
                    break;
            }
        }

        private static void SocketsTCP_Event_Socket(EventParameters eventParameters)
        {
            switch (eventParameters.GetEventType)
            {
                case EventParameters.EventType.ACCEPT_CONNECTION:
                    DataIn("Accept connection from > " + eventParameters.GetClientIp);
                    break;

                case EventParameters.EventType.DATA_IN:
                    DataIn(eventParameters.GetData);
                    socketsTCP.Send(eventParameters.GetConnectionNumber, "MSG TCP RECIVED OK ");
                    break;

                case EventParameters.EventType.ERROR:
                    DataIn("ERROR " + eventParameters.GetData);
                    break;

                
            }
        }

        private static void DataIn(string message)
        {
            WriteMessage(message);
        }


        static void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
