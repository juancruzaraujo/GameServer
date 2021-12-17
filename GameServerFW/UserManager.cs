using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServerFW.Users;
using GameServerFW.EventParameters;

namespace GameServerFW
{
    public class UserManager
    {
        static UserManager _userManagerInstance;
        List<UserInfo> _lstUserInfo;

        internal delegate void Delegate_UserManager_Events(UserManagerEventsParameters userManagerEventsParameters);
        internal event Delegate_UserManager_Events ConnectionManagerEvents;
        private void UserManagerEvent(UserManagerEventsParameters userManagerEventsParameters)
        {
            this.ConnectionManagerEvents(userManagerEventsParameters);
        }

        private void CreateEvent(UserManagerEventsParameters.UserManagerEventType userManagerEventType,string message = "")
        {
            UserManagerEventsParameters ev = new UserManagerEventsParameters();
            ev.SetEventType(userManagerEventType);
            if (message != "")
            {
                ev.SetMessage(message);
            }

            UserManagerEvent(ev);
        }

        internal UserManager()
        {
            _lstUserInfo = new List<UserInfo>();
        }

        public void NewUser(string ip,string id,int connectionNumber)
        {
            foreach(UserInfo usr in _lstUserInfo)
            {
                if (usr.GetUserId == id)
                {
                    CreateEvent(UserManagerEventsParameters.UserManagerEventType.CREATE_USER_ERROR, "existing user");
                    return;
                }
            }

            UserInfo user = new UserInfo(ip, id, connectionNumber);
            _lstUserInfo.Add(user);

            CreateEvent(UserManagerEventsParameters.UserManagerEventType.CREATE_USER_OK);

            return;
        }

        public string GetUserIp(string id)
        {
            UserInfo usr = GetUser(id);
            if (usr !=null)
            {
                CreateEvent(UserManagerEventsParameters.UserManagerEventType.USER_NOT_FOUND, usr.GetUserIp);
                return usr.GetUserIp;
            }
            
            CreateEvent(UserManagerEventsParameters.UserManagerEventType.USER_NOT_FOUND);

            return "";
        }

        public int GetUserConnectionNumber(string id)
        {
            UserInfo usr = GetUser(id);
            if (usr != null)
            {                
                CreateEvent(UserManagerEventsParameters.UserManagerEventType.USER_FOUND, usr.GetConnectionNumber.ToString());
                return usr.GetConnectionNumber;
            }

            CreateEvent(UserManagerEventsParameters.UserManagerEventType.USER_NOT_FOUND);

            return -1;
        }

        public string GetUserId(int connectionNumber)
        {
            foreach (UserInfo user in _lstUserInfo)
            {
                if (user.GetConnectionNumber == connectionNumber)
                {
                    CreateEvent(UserManagerEventsParameters.UserManagerEventType.USER_FOUND, user.GetUserId);
                    return user.GetUserId;
                }
            }

            CreateEvent(UserManagerEventsParameters.UserManagerEventType.USER_NOT_FOUND);
            return "";
        }

        private UserInfo GetUser(string id)
        {
            foreach(UserInfo user in _lstUserInfo)
            {
                if (user.GetUserId == id)
                {
                    return user;
                }
            }

            return null;
        }

        public void DeleteUser(string id)
        {
            for (int i = 0; i<_lstUserInfo.Count();i++)
            {
                if (_lstUserInfo[i].GetUserId == id)
                {
                    _lstUserInfo.RemoveAt(i);
                    CreateEvent(UserManagerEventsParameters.UserManagerEventType.DELETE_USER_OK);
                    return;
                }
            }
            CreateEvent(UserManagerEventsParameters.UserManagerEventType.DELETE_USER_ERROR);
            return;
        }
       

    }
}
