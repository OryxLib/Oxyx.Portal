using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Social.Model
{
    public class ChatMessage : IEntityModel
    {
        public Guid Id { get; set; }

        public ChatLog ChageLog { get; set; }

        public Guid FromUserId { get; set; }

        [ForeignKey("FromUserId")]
        public UserAccountEntry FromUser { get; set; }

        public Guid ToUserId { get; set; }

        [ForeignKey("ToUserId")]
        public UserAccountEntry ToUser { get; set; }

        public string MessageContent { get; set; }

        public bool IsReaded { get; set; } = false;

        public long TimeStamp { get; set; }

        public DateTime CreateTime { get; set; }

        public MessageType MsgType { get; set; }
    }

    public enum MessageType
    {
        Text,
        Voice,
        Video,
        Image,
        Emoji,
        Gift,
        RedPacket,
        Money
    }
}
