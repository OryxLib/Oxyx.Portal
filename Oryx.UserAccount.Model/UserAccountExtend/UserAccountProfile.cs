using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.UserAccount.Model.UserAccountExtend
{
    public class UserAccountProfile
    {
        public Guid Id { get; set; }

        public string Location { get; set; }

        public DateTime BirthDay { get; set; }

        public Guid UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public UserAccountEntry UserAccount { get; set; }

        public DateTime CreateTime { get; set; }
        public string TrueName { get; set; }
    }
}
