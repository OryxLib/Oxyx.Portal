using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Social.Model
{
    /// <summary>
    /// 作为话题的索引
    /// </summary>
    public class PostEntryTopic : IEntityModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid PosterId { get; set; }

        [ForeignKey("PosterId")]
        public UserAccountEntry PosterAccount { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
