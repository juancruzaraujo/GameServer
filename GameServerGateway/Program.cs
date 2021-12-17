using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;


namespace GameServerGateway
{
    class Program
    {

        static Manager manager;
        
        static void Main(string[] args)
        {
            //poner esto en propiedades si no tiene nada
            //-f GatewayServerConfig.json

            manager = new Manager();
            manager.StartServer(args);
        }
        

    }
}
