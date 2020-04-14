using Oryx.Social.Model;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.ViewModel.Social.Chat
{
    public class ChatMessageOutputData
    {
        public IEnumerable<UserAccountEntry> UserAccountCollectoin { get; set; }

        public ChatMessage ChatMessage { get; set; }
        public Guid ChatLogId { get; set; }

        public Guid? ToUserId { get; set; }
        public UserAccountEntry ToUserInfo { get; set; }
    }
}
