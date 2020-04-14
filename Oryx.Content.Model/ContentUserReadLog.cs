using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Content.Model
{
    public class ContentUserReadLog : IEntityModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserAccountEntry UserAccount { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Categories Categories { get; set; }

        public Guid ContentEntryId { get; set; }

        [ForeignKey("ContentEntryId")]
        public ContentEntry ContentEntry { get; set; }

        public DateTime CreateTime { get; set; }
    }
}