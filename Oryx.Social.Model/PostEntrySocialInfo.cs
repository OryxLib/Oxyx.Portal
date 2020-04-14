using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Social.Model
{
    public class PostEntrySocialInfo
    {
        public Guid Id { get; set; }

        public PostEntry PostEntry { get; set; }

        public int LikeNum { get; set; } 
    }
}
