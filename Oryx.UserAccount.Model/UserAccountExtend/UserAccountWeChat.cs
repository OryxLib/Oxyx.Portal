using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oryx.UserAccount.Model.UserAccountExtend
{
    public class UserAccountWeChat
    {
        public Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public UserAccountEntry UserAccount { get; set; }

        public string UnionId { get; set; }

        public List<WeChatAccountOpenIdMapping> OpenIdMapping { get; set; }
    }
}