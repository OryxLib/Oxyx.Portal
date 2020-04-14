
using System;
using System.Collections.Generic;

namespace Oryx.SpiderCartoon.V2.Model
{
    public class ContentEntry
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Order { get; set; }

        public Categories Category { get; set; }

        public List<FileEntry> MediaResource { get; set; }

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 需付费文章内容
        /// </summary>
        public int Fee { get; set; }
    }
}
