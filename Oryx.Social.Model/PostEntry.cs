using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model;
using Oryx.Utilities.TemplateGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oryx.Social.Model
{
    public class PostEntry : IEntityModel, ISoftDeleteModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [TemplateMetaData(Name = "发帖内容")]
        public string TextContent { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserAccountEntry UserAccountEntry { get; set; }

        public List<PostEntryFile> PostEntryFileList { get; set; }

        public List<PostEntryComments> PostEntryCommentList { get; set; }

        public string PostEntryTopic { get; set; }

        public long TimeStamp { get; set; }

        public int Order { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public bool IsSoftDelete { get; set; }

        public int LikeSum { get; set; }

        public List<PostEntryTag> PostEntryTags { get; set; }
    }
}
