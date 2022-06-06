using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSGateway
{
    class Program
    {
        static GSGatewayManager gsGatewayManager;

        static void Main(string[] args)
        {
            gsGatewayManager = new GSGatewayManager(args);
            gsGatewayManager.StartServer();
        }
    }
}
