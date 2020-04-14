using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.ViewModel.Social.PostEntry
{
    public class PostEntryCommentViewModel
    {
        //public Guid? Id { get; set; }

        public string Content { get; set; }

        public Guid? ParentCommentId { get; set; }

        public Guid? PostId { get; set; }
    }
}
