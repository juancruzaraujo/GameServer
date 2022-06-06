using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameServerFW.config
{
    [DataContract]
    public class AdminUsers
    {
        public AdminUser _adminUser;

        public AdminUsers()
        {
            _adminUser = new AdminUser();
        }

        [DataMember]
        public AdminUser adminUser
        {
            get
            {
                return _adminUser;
            }
            set
            {
                _adminUser = value;
            }
        }
    }
}
