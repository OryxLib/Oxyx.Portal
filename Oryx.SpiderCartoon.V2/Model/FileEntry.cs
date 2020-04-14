using System;

namespace Oryx.SpiderCartoon.V2.Model
{
    public class FileEntry
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public string ActualPath { get; set; }

        public DateTime CreateTime { get; set; }
    }
}