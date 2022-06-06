using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using GameServerFW.config;
using GameUtils;

namespace GameServerFW
{
    public class ConfigManager
    {

        Config _config;

        internal delegate void Delegate_ConfigManager_Event(ConfigManagerEventsParameters configmanagerEventsParameters);
        internal Delegate_ConfigManager_Event Event_ConfigManager;
        private void EventConfigManager(ConfigManagerEventsParameters configmanagerEventsParameters)
        {
            this.Event_ConfigManager(configmanagerEventsParameters);
        }


        internal ConfigManager()
        {

        }

        public Config GetConfig
        {
            get
            {
                return _config;
            }
        }

        public void LoadConfig(string fileName)
        {
            ConfigManagerEventsParameters ev = new ConfigManagerEventsParameters();
            string curConfig = "";
            string message = "";
            
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

                    _config = (Config)serializer.ReadObject(ms);
                    ms.Close();

                    
                    ev.SetEventType(ConfigManagerEventsParameters.ConfigManagerEventType.LOAD_CONFIG_OK);
                    EventConfigManager(ev);
                    return;

                }

                ev.SetEventType(ConfigManagerEventsParameters.ConfigManagerEventType.FILE_NOT_FOUND);
                EventConfigManager(ev);
                return;

            }
            catch(Exception err)
            {
                
                ev.SetEventType(ConfigManagerEventsParameters.ConfigManagerEventType.LOAD_CONFIG_ERROR).SetMessage(err.Message);
                EventConfigManager(ev);
            }
            
        }
    
        /// <summary>
        /// not implemented
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="theClass"></param>
        /// <param name="obj"></param>
        public void CreateCustomConfigFile(string filename,Type theClass,object obj)
        {

        }

        public void CreateConfigFile(string fileName)
        {
            try
            {
                ConfigManagerEventsParameters ev = new ConfigManagerEventsParameters();

                //genera el archivo de configuracion
                JsonUtils jsonUtils = new JsonUtils();
                Config config = SetExampleConfig();

                if (jsonUtils.JsonToFile(fileName,typeof(Config), config))
                {
                    ev.SetEventType(ConfigManagerEventsParameters.ConfigManagerEventType.CREATE_CONFIG_FILE_OK);
                    EventConfigManager(ev);
                    return;

                }
                else
                {
                    ev.SetEventType(ConfigManagerEventsParameters.ConfigManagerEventType.CREATE_CONFIG_FILE_ERROR);
                    ev.SetMessage(jsonUtils.GetErrorMessage);
                    EventConfigManager(ev);
                }

            }
            catch(Exception err)
            {
                
                ConfigManagerEventsParameters ev = new ConfigManagerEventsParameters();
                ev.SetEventType(ConfigManagerEventsParameters.ConfigManagerEventType.CREATE_CONFIG_FILE_ERROR).SetMessage(err.Message);
                EventConfigManager(ev);
            }
        }

        private Config SetExampleConfig()
        {
            Config config = new Config();
            
            ServerConfig serverConfig = new ServerConfig();
            config.serverConfig = serverConfig;

            ServerParameters serverParameters = new ServerParameters();
            config.serverConfig.serverParameters = serverParameters;

            config.serverConfig.serverParameters.serverName = "exampleName";
            config.serverConfig.serverParameters.tcpPortNumber = "1234";
            config.serverConfig.serverParameters.udpPortNumber = "1234";
            config.serverConfig.serverParameters.maxUsers = "100";
            config.serverConfig.serverParameters.recieveTimeOut = "30";
            config.serverConfig.serverParameters.logFileName = "exampleLog.log";
            config.serverConfig.serverParameters.logPathFile = "c:\\";

            DestinyServers destinyServers = new DestinyServers();
            ServerInfo serverInfo = new ServerInfo();

            destinyServers.serverInfo = serverInfo;
            destinyServers.serverInfo.host = "1.1.1.1";
            destinyServers.serverInfo.identifierTag = "example_tag";
            destinyServers.serverInfo.password = "1234pass";
            destinyServers.serverInfo.serverType = "exaple server";
            destinyServers.serverInfo.tcpPort = "4321";
            destinyServers.serverInfo.udpPort = "4321";
            destinyServers.serverInfo.user = "usr";

            List<DestinyServers> lstDestiniServer = new List<DestinyServers>();
            lstDestiniServer.Add(destinyServers);

            config.serverConfig.destinyServers = lstDestiniServer;

            AdminUser adminUser = new AdminUser();
            adminUser.usr = "adm1";
            adminUser.pass = "admPass";

            AdminUsers adminUsers = new AdminUsers();
            adminUsers.adminUser = adminUser;

            List<AdminUsers> lst = new List<AdminUsers>();
            config.serverConfig.serverParameters.adminUsers = lst;
            config.serverConfig.serverParameters.adminUsers.Add(adminUsers);
            
            return config;
        }
    }
}
