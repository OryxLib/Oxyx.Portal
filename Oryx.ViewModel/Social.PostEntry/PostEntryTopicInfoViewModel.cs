using Oryx.Content.Model;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.ViewModel.Social.PostEntry
{
    public class PostEntryTopicInfoViewModel
    {
        public Guid? LinkedCartoonId { get; set; }

        public string LinkedCartoonName { get; set; }

        public int PostEntryCount { get; set; }

        public IList<GetPostEntryListOutput> PostEntryList { get; set; }

        public List<ContentEntry> NewsList { get; set; }

        public int NewsCount { get; set; }
        public Guid TopicPosterId { get; set; }
        public string TopicPosterName { get; set; }
        public string TopicPosterAvatar { get; set; }
    }
}
