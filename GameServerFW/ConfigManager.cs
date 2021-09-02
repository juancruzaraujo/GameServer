using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using GameServerFW.config;

namespace GameServerFW
{
    internal class ConfigManager
    {

        public delegate void Delegate_ConfigManager_Event(ConfigManagerEventsParameters configmanagerEventsParameters);
        public Delegate_ConfigManager_Event Event_ConfigManager;
        private void EventConfigManager(ConfigManagerEventsParameters configmanagerEventsParameters)
        {
            this.Event_ConfigManager(configmanagerEventsParameters);
        }


        internal Config GetConfig(string fileName)
        {

            string curConfig = "";
            string message = "";
            //string aux = "";
            Config config;
            char directorySeparatorChar = Path.DirectorySeparatorChar;

            try
            {
                if ((fileName.Contains(directorySeparatorChar)))
                {
                    curConfig = fileName;
                }
                else
                {
                    curConfig = Directory.GetCurrentDirectory() + directorySeparatorChar + fileName;
                }

                if (File.Exists(curConfig))
                {
                    //https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/how-to-serialize-and-deserialize-json-data
                    string jsonConfig = File.ReadAllText(curConfig);

                    var serializer = new DataContractJsonSerializer(typeof(Config));
                    var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonConfig));

                    config = (Config)serializer.ReadObject(ms);
                    ms.Close();

                    //ConfigManagerEventsParameters ev = new ConfigManagerEventsParameters();
                    //ev.SetEventType(ConfigManagerEventsParameters.ConfigManagerEventType.LOAD_CONFIG_OK);
                    //EventConfigManager(ev);

                    return config;
                }
                else
                {
                    ConfigManagerEventsParameters ev = new ConfigManagerEventsParameters();
                    ev.SetEventType(ConfigManagerEventsParameters.ConfigManagerEventType.FILE_NOT_FOUND);
                    EventConfigManager(ev);
                }

                return null;
            }
            catch(Exception err)
            {
                
                ConfigManagerEventsParameters ev = new ConfigManagerEventsParameters();
                ev.SetEventType(ConfigManagerEventsParameters.ConfigManagerEventType.LOAD_CONFIG_ERROR).SetMessage(err.Message);
                EventConfigManager(ev);
                return null;
            }
            
        }
    
        internal void CreateConfigFile(string fileName)
        {
            try
            {
                //genera el archivo de configuracion
                File.WriteAllBytes(fileName, Resource1.configExample);
                
                ConfigManagerEventsParameters ev = new ConfigManagerEventsParameters();
                ev.SetEventType(ConfigManagerEventsParameters.ConfigManagerEventType.CREATE_CONFIG_FILE_OK);
                EventConfigManager(ev);
            }
            catch(Exception err)
            {
                
                ConfigManagerEventsParameters ev = new ConfigManagerEventsParameters();
                ev.SetEventType(ConfigManagerEventsParameters.ConfigManagerEventType.CREATE_CONFIG_FILE_ERROR).SetMessage(err.Message);
                EventConfigManager(ev);
            }
        }
    }
}
