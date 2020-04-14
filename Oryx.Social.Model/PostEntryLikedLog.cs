using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Social.Model
{
    public class PostEntryLikedLog : IEntityModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserAccountEntryId { get; set; }

        [ForeignKey("UserAccountEntryId ")]
        public UserAccountEntry UserAccounetEntry { get; set; }

        public Guid PostEntryId { get; set; }

        [ForeignKey("PostEntryId")]
        public PostEntry PostEntry { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
