using Oryx.CommonInterface.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Social.Model
{
    public class ChatLog : IEntityModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 聊天对象集合
        /// </summary>
        public List<ChatCollection> ChatCollection { get; set; }

        public List<ChatMessage> ChatMessageList { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
