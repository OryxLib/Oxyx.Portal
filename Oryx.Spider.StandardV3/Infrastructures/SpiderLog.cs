using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Spider.StandardV3.Infrastructures
{
    public class SpiderLog
    {
        public int Id { get; set; }

        public string ParentUrl { get; set; }

        public string ParentName { get; set; }

        public int Order { get; set; }

        public string TargetUrl { get; set; }

        public bool ReloadSuccess { get; set; } = false;

        public string Type { get; set; }

        public string Html { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
