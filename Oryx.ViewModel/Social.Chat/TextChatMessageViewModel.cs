using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Oryx.Social.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.ViewModel.Social.Chat
{
    public class TextChatMessageViewModel
    {
        [JsonProperty(PropertyName = "type")]
        public MessageType Type { get; set; }

        [JsonProperty(PropertyName = "fromId")]
        public Guid FromId { get; set; }

        [JsonProperty(PropertyName = "toId")]
        public Guid ToId { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
    }
}
