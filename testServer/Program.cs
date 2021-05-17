using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ConsoleOutputFormater;
using Sockets;

namespace testServer
{
    class Program
    {

        static Sockets.Sockets obSocket;
        //static OutputFormater outputFormater;

        static void Main(string[] args)
        {
            
            
            SetSocket();

            while(true)
            {

            }
        }

        static void SetSocket()
        {
            obSocket = new Sockets.Sockets();
            obSocket.Event_Socket += new Sockets.Sockets.Delegate_Socket_Event(EvSockets);

            obSocket.ServerMode = true;
            obSocket.SetServer(1492, Sockets.Sockets.C_DEFALT_CODEPAGE, true, 10);
            obSocket.StartServer();
        }

        static void EvSockets(EventParameters eventParameters)
        {
            switch (eventParameters.GetEventType)
            {
                case EventParameters.EventType.NEW_CONNECTION:

                    int numCon = eventParameters.GetConnectionNumber;
                    OutputFormater outputFormater = new OutputFormater();

                    outputFormater.SetBold(true).SetUnderline(true);
                    Send(numCon, outputFormater.FormatText("HOLA MUNDO", OutputFormater.TextColorFG.Bright_Green, OutputFormater.TextColorBG.Bright_White));
                    //Send(numCon, "\x1b[1;4;92;107mHola Mundo\x1b[0m");
                    //obSocket.Send("HELLO THERE MY FRIEND!\n\r", eventParameters.GetListIndex);

                    /*
                    Send(eventParameters.GetConnectionNumber, "\x1b[1m bold.\x1b[0m  \r\n ");
                    Send(eventParameters.GetConnectionNumber, "\x1b[2m no bold.\x1b[0m  \r\n ");
                    Send(eventParameters.GetConnectionNumber, "\x1b[3m italic.\x1b[0m  \r\n ");         //funciona
                    Send(eventParameters.GetConnectionNumber, "\x1b[4m subrayado.\x1b[0m  \r\n ");      //funciona 
                    Send(eventParameters.GetConnectionNumber, "\x1b[5m parpadeo.\x1b[0m  \r\n ");
                    Send(eventParameters.GetConnectionNumber, "\x1b[6m reversed text.\x1b[0m  \r\n ");
                    Send(eventParameters.GetConnectionNumber, "\x1b[8m invisible text.\x1b[0m  \r\n ");  //funciona
                    Send(eventParameters.GetConnectionNumber, "\x1b[K invisible text.\x1b[0m  \r\n ");
                    */

                   

                    /*
                    Send(numCon, "============================================================!\n\r \n\r");
                    
                    Send(numCon, "\x1b[0m normal!\x1b[0m\n\r");
                    Send(numCon, "\x1b[1m Bold or increased intensity\x1b[0m\n\r");

                    for (int i =0;i<108; i++ )
                    {
                        Send(numCon, "\x1b[" + i + "m  EL VALOR DE i QUE PASE ES " + i + " \x1b[0m\n\r");
                    }

                    Send(eventParameters.GetConnectionNumber, "\n\rNORMAL!\n\r");

                    Send(numCon, "\x1b[0;34;42mBold,Fondo verde, letra azul\x1b[0m\n\r");
                    Send(numCon, "\x1b[2;94;104mDecrased,Fondo verde, letra azul\x1b[0m\n\r");
                    Send(numCon, "\x1b[1;3;9;21;32;44mNormal,Fondo azul, letra verde\x1b[0m\n\r");
                    Send(numCon, "\x1b[0;3;9;21;37;42mNormal,Fondo azul, letra verde\x1b[0m\n\r");
                    Send(numCon, "\x1b[1;21;106mPrueba Solo fondo 106\x1b[0m\n\r");
                    */
                         //[tipo,tipo,fondo,letra

                    //obSocket.DisconnectConnectedClientToMe(eventParameters.GetConnectionNumber); //Disconnect(eventParameters.GetConnectionNumber);
                    break;

                case EventParameters.EventType.ERROR:
                    Console.WriteLine(eventParameters.GetData);
                    System.Environment.Exit(eventParameters.GetErrorCode);
                    break;
            }

            
        }

        private static void Send(int conNumber, string text)
        {
            Console.Write(text);
            obSocket.Send(conNumber, text);
        }

    }
}
