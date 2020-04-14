using System;
using System.Collections.Generic;
using System.Text;
using Oryx.Social.Model;
using Oryx.UserAccount.Model;

namespace Oryx.ViewModel.Social.PostEntry
{
    public class GetPostEntryListOutput
    {
        public string Content { get; set; }
        public PostEntryFile Voice { get; set; }
        public List<PostEntryFile> ImageList { get; set; }
        public long TimeStamp { get; set; }
        public UserAccountEntry UserInfo { get; set; }
        public string PostEntryTopic { get; set; }
        public int LikeNum { get; set; }
        public int CommentNum { get; set; }
        public bool IsSubscribed { get; set; }
        public Guid Id { get; set; }
    }
}
