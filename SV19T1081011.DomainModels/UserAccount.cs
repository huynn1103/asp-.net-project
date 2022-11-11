using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081011.DomainModels
{
    /// <summary>
    /// 
    /// </summary>
    public class UserAccount
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public string GroupName { get; set; }
        public DateTime RegisteredTime { get; set; }
        public bool IsLocked { get; set; }
    }
}
