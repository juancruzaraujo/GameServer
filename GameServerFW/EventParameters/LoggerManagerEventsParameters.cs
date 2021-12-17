using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.EventParameters
{
    public class LoggerManagerEventsParameters
    {
        public enum LoggerManagerEventType
        {
            CREATE_OR_APPEND_LOG_FILE_OK,
            CREATE_OR_APPEND_LOG_FILE_ERROR,
            WRITE_LOG_FILE_OK,
            WRITE_LOG_FILE_ERROR,
            SAVE_LOG_OK,
            SAVE_LOG_ERROR,

        }

        private LoggerManagerEventType _loggerManagerEventType;
        private string _eventMessage;

        internal LoggerManagerEventsParameters() { }

        internal LoggerManagerEventsParameters(LoggerManagerEventType loggerManagerEventType)
        {
            _loggerManagerEventType = loggerManagerEventType;
        }

        internal LoggerManagerEventsParameters SetEventType(LoggerManagerEventType loggerManagerEventType)
        {
            _loggerManagerEventType = loggerManagerEventType;
            return this;
        }

        internal LoggerManagerEventsParameters SetEventMessage(string eventMessage)
        {
            _eventMessage = eventMessage;
            return this;
        }

        internal string GetEventMessage
        {
            get
            {
                return _eventMessage;
            }
        }

        internal LoggerManagerEventType GetEventType
        {
            get
            {
                return _loggerManagerEventType;
            }
        }


    }
}
