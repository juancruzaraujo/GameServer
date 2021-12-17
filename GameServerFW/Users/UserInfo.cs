
namespace GameServerFW.Users
{
    internal class UserInfo
    {
        private string _ip;
        private string _idUser;
        private int _connectionNumber;

        internal UserInfo(string ip,string idUser,int connectionNumber)
        {
            _ip = ip;
            _idUser = idUser;
            _connectionNumber = connectionNumber;
        }

        internal string GetUserIp
        {
            get
            {
                return _ip;
            }
        }

        internal string GetUserId
        {
            get
            {
                return _idUser;
            }
        }

        internal int GetConnectionNumber
        {
            get
            {
                return _connectionNumber;
            }
        }
    }
}
