using Oryx.CommonInterface.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.UserAccount.Model.UserBusinessExtend
{
    public class UserAccountBusinessRole : IEntityModel
    {
        public Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public virtual UserAccountEntry UserAccount { get; set; }

        public string RoleName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
