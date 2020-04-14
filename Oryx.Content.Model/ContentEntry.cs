using Oryx.CommonInterface.Interface;
using Oryx.Content.Model.ContentEntryExtension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oryx.Content.Model
{
    /// <summary>
    /// 当Tags 中包含资讯的时候 为新闻分类的
    /// </summary>
    public class ContentEntry : IEntityModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

        public bool IsTop { get; set; }

        public bool IsFaq { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Categories Category { get; set; }

        public List<FileEntry> MediaResource { get; set; }

        public DateTime CreateTime { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Tags> Tags { get; set; }

        public virtual ContentEntryInfo ContentEntryInfo { get; set; }

        /// <summary>
        /// 需付费文章内容
        /// </summary>
        public int Fee { get; set; }
    }
}
