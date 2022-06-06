using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.EventParameters
{
    public class ConnectionsManagerEventsParameters
    {
        public enum CommunicationsMangerEventsType
        {
            HOST_INFO_NOT_FOUND,
            CLIENT_INFO_NOT_FOUND
        }

        private CommunicationsMangerEventsType _communicationsManagerEventType;
        private string _eventMessage;

        internal ConnectionsManagerEventsParameters() { }

        internal ConnectionsManagerEventsParameters(CommunicationsMangerEventsType communicationsManagerEventType)
        {
            _communicationsManagerEventType = communicationsManagerEventType;
        }

        internal ConnectionsManagerEventsParameters SetEventType(CommunicationsMangerEventsType communicationsManagerEventType)
        {
            _communicationsManagerEventType = communicationsManagerEventType;
            return this;
        }

        internal ConnectionsManagerEventsParameters SetEventMessage(string message)
        {
            _eventMessage = message;
            return this;
        }

        internal CommunicationsMangerEventsType GetEventType
        {
            get
            {
                return _communicationsManagerEventType;
            }
        }

        internal string GetEventMessage
        {
            get
            {
                return _eventMessage;
            }
        }

    }
}
