using Oryx.Social.Model;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.ViewModel.Social.PostEntry
{
    public class PostEntryOutputViewModel
    {
        public Guid Id { get; set; }

        public string TextContent { get; set; }

        public UserAccountEntry UserAccountEntry { get; set; }

        public string PostEntryTopic { get; set; }

        public List<PostEntryComments> PostEntryCommentList { get; set; }

        public int PostEntryCommentListNum { get; set; }

        public long TimeStamp { get; set; }

        public int Order { get; set; }

        public int LikeSum { get; set; }
    }
}
