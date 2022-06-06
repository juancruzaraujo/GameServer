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

        static Socket socketsTCP;
        static Socket socketsUDP;
        static OutputFormatter outputFormater;
        

        static int portNumber;
        static string serverType;
        static bool keepRuning;
        static int maxCon;
        static LogInfo _msg;


        static void Main(string[] args)
        {
            outputFormater = new ConsoleOutputFormater.OutputFormatter();
            OutputFormatterAttributes attr = new OutputFormatterAttributes();
            _msg = LogInfo.GetInstance();
            try
            {


                if (args.Count() != 3)
                {
                    //attr.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Red);
                    _msg.ShowMessage(" \r\nparams[port number][server type] [max connections]", null, LogInfo.typeMsg.ERROR);

                    System.Environment.Exit(0);
                }

                portNumber = int.Parse(args[0]);
                serverType = args[1];
                maxCon = int.Parse(args[2]);

                socketsTCP = new Socket();
                socketsUDP = new Socket();

                socketsTCP.SetServer(portNumber, Protocol.ConnectionProtocol.TCP, maxCon);
                socketsUDP.SetServer(portNumber, Protocol.ConnectionProtocol.UDP);

                socketsTCP.Event_Socket += SocketsTCP_Event_Socket;
                socketsUDP.Event_Socket += SocketsUDP_Event_Socket;

                _msg.ShowMessage("Dummy server ", null, LogInfo.typeMsg.NO_TYPE);


                _msg.ShowMessage("tcp port " + portNumber, null, LogInfo.typeMsg.OK);
                _msg.ShowMessage("udp port " + portNumber, null, LogInfo.typeMsg.OK);
                _msg.ShowMessage("server type  " + serverType, null, LogInfo.typeMsg.OK);          


                socketsTCP.StartServer();
                _msg.ShowMessage("tcp start " + serverType, null, LogInfo.typeMsg.OK);

                socketsUDP.StartServer();
                _msg.ShowMessage("udp start " + serverType, null, LogInfo.typeMsg.OK);

                OutputFormatterAttributes atrr = new OutputFormatterAttributes();
                attr.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Blue).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
                _msg.ShowMessage(_msg.CustomType("INFO", attr) + " max connections " + maxCon);

                keepRuning = true;

                while(keepRuning)
                {

                }
            }
            catch(Exception err)
            {
                //WriteMessage(err.Message);
                _msg.ShowMessage(err.Message, null, LogInfo.typeMsg.ERROR);
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
                case EventParameters.EventType.SERVER_ACCEPT_CONNECTION:
                    DataIn("Accept connection from > " + eventParameters.GetClientIp + " connection number " + eventParameters.GetConnectionNumber);
                    break;

                case EventParameters.EventType.DATA_IN:
                    DataIn(eventParameters.GetData);
                    socketsTCP.Send(eventParameters.GetConnectionNumber, "MSG TCP RECIVED OK ");
                    break;

                case EventParameters.EventType.ERROR:
                    _msg.ShowMessage(eventParameters.GetData +  " " + eventParameters.GetConnectionNumber, null, LogInfo.typeMsg.ERROR);
                    break;

                case EventParameters.EventType.END_CONNECTION:
                    _msg.ShowMessage("end connection " + eventParameters.GetConnectionNumber, null, LogInfo.typeMsg.OK);
                    break;

                
            }
        }

        private static void DataIn(string message)
        {
            _msg.ShowMessage(message);
        }
    }
}
