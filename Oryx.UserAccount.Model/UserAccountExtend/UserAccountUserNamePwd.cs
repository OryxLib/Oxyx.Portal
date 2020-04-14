using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.UserAccount.Model.UserAccountExtend
{
    public class UserAccountUserNamePwd
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        
        public Guid UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public UserAccountEntry UserAccount { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
