using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Content.Model.SocialExtension
{
    /// <summary>
    /// 话题列表页关联信息
    /// </summary>
    public class ContentExtPostEntryTopic
    {
        public Guid Id { get; set; }

        public Guid LinkId { get; set; }

        /// <summary>
        /// Content or Category
        /// </summary>
        public string LinkType { get; set; }
        public string TopicText { get; set; }
    }
}
