using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oryx.Common.Model.SandBox
{
    public class SandBoxMessage : IEntityModel
    {
        public Guid Id { get; set; }

        public SandBoxMessageType MessageType { get; set; }

        public bool IsRead { get; set; }

        public string RecieveToken { get; set; }

        public string Content { get; set; }

        public Guid FromUserAccountId { get; set; }

        [ForeignKey("FromUserAccountId")]
        public UserAccountEntry FromUserAccount { get; set; }

        public Guid ToUserAccountId { get; set; }

        [ForeignKey("ToUserAccountId")]
        public UserAccountEntry ToUserAccount { get; set; }

        //针对不同消息, 相关跳转链接的参数存储.
        public string AttentionInfo { get; set; }

        public long TimeStamp { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
