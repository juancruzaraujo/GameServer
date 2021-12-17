using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.EventParameters
{
    public class UserManagerEventsParameters
    {
        public enum UserManagerEventType
        {
            CREATE_USER_OK,
            CREATE_USER_ERROR,
            USER_FOUND,
            USER_NOT_FOUND,
            DELETE_USER_OK,
            DELETE_USER_ERROR
        }

        private UserManagerEventType _eventType;
        private string _message;

        internal UserManagerEventsParameters() { }

        internal UserManagerEventsParameters(UserManagerEventType userManagerEventType)
        {
            _eventType = userManagerEventType;
        }

        internal UserManagerEventsParameters SetEventType(UserManagerEventType userManagerEventType)
        {
            _eventType = userManagerEventType;
            return this;
        }

        internal UserManagerEventsParameters SetMessage(string message)
        {
            _message = message;
            return this;
        }

        internal string GetMessage
        {
            get
            {
                return _message;
            }
        }

        internal UserManagerEventType GetEventType
        {
            get
            {
                return _eventType;
            }
        }



    }
}
