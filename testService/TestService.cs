using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Sockets;

namespace testService
{
    public partial class TestService : ServiceBase
    {

        Sockets.Sockets obSocket;

        public TestService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            obSocket = new Sockets.Sockets();
            obSocket.Event_Socket += new Sockets.Sockets.Delegate_Socket_Event(EvSockets);

            obSocket.ServerMode = true;
            obSocket.SetServer(1492, Sockets.Sockets.C_DEFALT_CODEPAGE, true, 10);
            obSocket.StartServer();
        }

        protected override void OnStop()
        {
        }

        private void EvSockets(EventParameters eventParameters)
        {
            switch (eventParameters.GetEventType)
            {
                case EventParameters.EventType.NEW_CONNECTION:
                    obSocket.Send("HELLO THERE MY FRIEND!\n\r",eventParameters.GetListIndex);
                    obSocket.Disconnect(eventParameters.GetConnectionNumber);
                    break;
            }
        }
        
    }
}
