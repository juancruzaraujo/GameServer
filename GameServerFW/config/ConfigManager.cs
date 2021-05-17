using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleOutputFormater;
using System.IO;
using System.Runtime.Serialization.Json;

namespace GameServerFW.config
{
    class ConfigManager
    {

        public delegate void Delegate_ConfigManager_Event(string message);
        public Delegate_ConfigManager_Event Event_ConfigManager;
        private void EventConfigManager(string message)
        {
            this.Event_ConfigManager(message);
        }

        internal Config GetConfig(string[] args,Logger.Logger logger)
        {
            string curConfig = "";
            OutputFormater outputFormater = new OutputFormater();
            string message = "";
            string aux = "";
            Config config;
            char directorySeparatorChar = Path.DirectorySeparatorChar;
           

            try
            {

                if (args.Length > 0) 
                {
                    if (args[0] == "g") //si es g genero el archivo de config
                    {
                        //genera el archivo de configuracion
                        File.WriteAllBytes("GatewayServerConfig.json", Resource1.configExample);
                        EventConfigManager(logger.WriteLog(outputFormater.FormatText("creating file ", OutputFormater.TextColorFG.Bright_White, OutputFormater.TextColorBG.Black)));
                        aux = outputFormater.FormatText("OK", OutputFormater.TextColorFG.Bright_Green, OutputFormater.TextColorBG.Black);
                        EventConfigManager(logger.WriteLog("[ " + aux + " ]"));
                    }
                    else
                    {
                        
                        if ((args[0].Contains(directorySeparatorChar)))
                        {
                            curConfig = args[0];
                        }
                        else
                        {
                            curConfig = Directory.GetCurrentDirectory() + directorySeparatorChar + args[0];
                        }

                        if (File.Exists(curConfig))
                        {
                            
                            //https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/how-to-serialize-and-deserialize-json-data
                            string jsonConfig = File.ReadAllText(curConfig);

                            var serializer = new DataContractJsonSerializer(typeof(Config));
                            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonConfig));

                            config = (Config)serializer.ReadObject(ms);
                            ms.Close();
                            return config;
                        }
                        else
                        {
                            message = "FILE NOT FOUND";
                            aux = outputFormater.FormatText(message, OutputFormater.TextColorFG.Bright_Red, OutputFormater.TextColorBG.Black);
                            EventConfigManager(logger.WriteLog("[ " + aux + " ]"));
                        }
                    }

                }
                else
                {

                    outputFormater.SetBold(true);
                    message = "ERROR";
                    aux = outputFormater.FormatText(message, OutputFormater.TextColorFG.Bright_Red, OutputFormater.TextColorBG.Black);
                    EventConfigManager("[ " + aux + " ]");
                    message = "run with g parameter to generate configuration file or run with path " + directorySeparatorChar + "config.json";
                    outputFormater.ResetAttributes();
                    EventConfigManager(outputFormater.FormatText(message, OutputFormater.TextColorFG.Bright_White, OutputFormater.TextColorBG.Black));
                    EventConfigManager(outputFormater.FormatText("examples", OutputFormater.TextColorFG.Bright_Green, OutputFormater.TextColorBG.Black));
                    EventConfigManager(outputFormater.FormatText("GameServerGateway g", OutputFormater.TextColorFG.Bright_Cyan, OutputFormater.TextColorBG.Black));
                    message = "generates the configuration file in the execution directory";
                    EventConfigManager(outputFormater.FormatText(message, OutputFormater.TextColorFG.Bright_White, OutputFormater.TextColorBG.Black));

                    EventConfigManager(outputFormater.FormatText("GameServerGateway GatewayServerConfig.json", OutputFormater.TextColorFG.Bright_Cyan, OutputFormater.TextColorBG.Black));
                    EventConfigManager(outputFormater.FormatText("GameServerGateway [name.json]", OutputFormater.TextColorFG.Bright_Cyan, OutputFormater.TextColorBG.Black));
                    EventConfigManager(outputFormater.FormatText("GameServerGateway path" + directorySeparatorChar + "GatewayServerConfig.json", OutputFormater.TextColorFG.Bright_Cyan, OutputFormater.TextColorBG.Black));
                    message = "execute GameServerGateway with this config file";
                    EventConfigManager(outputFormater.FormatText(message, OutputFormater.TextColorFG.Bright_White, OutputFormater.TextColorBG.Black));

                }

                return null;
            }
            catch (Exception err)
            {
                outputFormater.SetBold(true);
                EventConfigManager(logger.WriteLog(outputFormater.FormatText(err.Message, OutputFormater.TextColorFG.Bright_Red, OutputFormater.TextColorBG.Black)));
                return null;
            }
        }
    }
}
