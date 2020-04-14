using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Social.Model
{
    public class PostEntryFile
    {
        public Guid Id { get; set; }

        public PostEntryFileType Type { get; set; }

        public string ActualPath { get; set; }

        public string Name { get; set; }
        public int Order { get; set; }
        public string Tag { get; set; }
    }

    public enum PostEntryFileType
    {
        Image,
        Voice,
        Video
    }
}
