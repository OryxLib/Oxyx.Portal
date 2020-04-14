using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Content.Model
{
    public class Comment : IEntityModel
    {
        public Guid Id { get; set; }

        public Guid? UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public UserAccountEntry UserAccount { get; set; }

        public string Content { get; set; }

        public Guid? ContentEntryId { get; set; }

        [ForeignKey("ContentEntryId")]
        public ContentEntry ContentEntry { get; set; }

        public Guid? ParentCommentId { get; set; }

        [ForeignKey("ParentCommentId")]
        public Comment ParentComment { get; set; }

        public List<Comment> ChildrenComment { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
