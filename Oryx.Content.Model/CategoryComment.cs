using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Content.Model
{
    public class CategoryComment : IEntityModel
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Categories Category { get; set; }

        public string Content { get; set; }

        public Guid UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public UserAccountEntry UserAccount { get; set; }

        public Guid? ParentCommentId { get; set; }

        [ForeignKey("ParentCommentId")]
        public virtual CategoryComment ParentComment { get; set; }

        public virtual List<CategoryComment> ChildrenComment { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
