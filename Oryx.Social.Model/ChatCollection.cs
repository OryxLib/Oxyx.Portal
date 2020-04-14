using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Social.Model
{
    public class ChatCollection : IEntityModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public UserAccountEntry UserAccountEntry { get; set; }

        public ChatLog ChatLog { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
