using Oryx.CommonInterface.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.UserAccount.Model.UserAccountExtend
{
    public class UserAccountLoginLog : IEntityModel
    {
        public Guid Id { get; set; }

        public Guid UserAccountEntryId { get; set; }

        [ForeignKey("UserAccountEntryId ")]
        public UserAccountEntry UserAccountEntry { get; set; }

        public string IP { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
