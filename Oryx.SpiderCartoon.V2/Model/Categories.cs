using System;
using System.Collections.Generic;

namespace Oryx.SpiderCartoon.V2.Model
{
    public class Categories
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ContentStatus Status { get; set; }

        public virtual List<FileEntry> MediaResource { get; set; }

        public string Description { get; set; }

        public Categories ParentCategory { get; set; }

        public virtual List<Categories> ChildrenCategories { get; set; }

        public List<ContentEntry> ContentList { get; set; }

        public List<Tags> Tags { get; set; }

        public DateTime CreateTime { get; set; }

        public int Fee { get; set; }
    }

    public enum ContentStatus
    {
        Normal,
        Close
    }
}
