using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Social.Model
{
    public class PostEntryComments : IEntityModel
    {
        public Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public UserAccountEntry UserAccount { get; set; }

        public Guid? PostEntryId { get; set; }

        [ForeignKey("PostEntryId")]
        public PostEntry PostEntry { get; set; }

        public string Content { get; set; }

        public int LikeNum { get; set; }

        public Guid? ParentCommentId { get; set; }

        [ForeignKey("ParentCommentId ")]
        public PostEntryComments ParentComment { get; set; }

        public List<PostEntryComments> ChildrenComment { get; set; }

        public long TimeStamp { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
