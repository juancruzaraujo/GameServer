using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW
{
    internal class ConfigManagerEventsParameters
    {
        internal enum ConfigManagerEventType
        {
            LOAD_CONFIG_OK = 1,
            LOAD_CONFIG_ERROR = 2,
            READ_CONFIG_OK = 3,
            READ_CONFIG_ERROR = 4,
            CREATE_CONFIG_FILE_OK = 5,
            CREATE_CONFIG_FILE_ERROR = 6,
            FILE_NOT_FOUND = 7 
        }

        private ConfigManagerEventType _configManagerEventType;
        private string _message;

        internal ConfigManagerEventsParameters SetEventType(ConfigManagerEventType configManagerEventType)
        {
            _configManagerEventType = configManagerEventType;
            return this;
        }

        internal ConfigManagerEventsParameters SetMessage(string message)
        {
            _message = message;
            return this;
        }

        internal ConfigManagerEventType GetEventType
        {
            get
            {
                return _configManagerEventType;
            }
        }

        internal string GetMessage
        {
            get 
            { 
                return _message;
            }
        }
    }
}
