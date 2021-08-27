using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sockets;
using ConsoleOutputFormater;

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

            try
            {
                
                if (args.Length == 0 || args.Length == 1)
                {
                    string errMessage = "[ " + outputFormater.FormatText("ERROR", OutputFormatter.TextColorFG.Bright_Red, OutputFormatter.TextColorBG.Black) + " ]";
                    errMessage = errMessage + " \r\nparams [port number] [server type]";
                    WriteMessage(errMessage);
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

                WriteMessage("[ " + outputFormater.FormatText("OK", OutputFormatter.TextColorFG.Bright_Green, OutputFormatter.TextColorBG.Black) + " ] Dummy Server");
                WriteMessage("[ " + outputFormater.FormatText("OK", OutputFormatter.TextColorFG.Bright_Green, OutputFormatter.TextColorBG.Black) + " ] tcp port " + portNumber);
                WriteMessage("[ " + outputFormater.FormatText("OK", OutputFormatter.TextColorFG.Bright_Green, OutputFormatter.TextColorBG.Black) + " ] udt port " + portNumber);
                WriteMessage("[ " + outputFormater.FormatText("OK", OutputFormatter.TextColorFG.Bright_Green, OutputFormatter.TextColorBG.Black) + " ] server type " + serverType);


                socketsTCP.StartServer();
                WriteMessage("[ " + outputFormater.FormatText("OK", OutputFormatter.TextColorFG.Bright_Green, OutputFormatter.TextColorBG.Black) + " ] tcp start " + serverType);

                socketsUDP.StartServer();
                WriteMessage("[ " + outputFormater.FormatText("OK", OutputFormatter.TextColorFG.Bright_Green, OutputFormatter.TextColorBG.Black) + " ] udp start " + serverType);

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
