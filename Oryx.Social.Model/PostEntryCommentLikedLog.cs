using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Social.Model
{
    public class PostEntryCommentLikedLog : IEntityModel
    {
        public Guid Id { get; set; }

        public Guid UserAccountEntryId { get; set; }

        [ForeignKey("UserAccountEntryId")]
        public virtual UserAccountEntry UserAccountEntry { get; set; }

        public Guid PostEntryCommentId { get; set; }

        [ForeignKey("PostEntryCommentId")]
        public virtual PostEntryComments PostEntryComments { get; set; }

        public DateTime CreateTime { get; set; }

        public Guid PostEntryId { get; set; }

        [ForeignKey("PostEntryId")]
        public virtual PostEntry PostEntry { get; set; }
    }
}
