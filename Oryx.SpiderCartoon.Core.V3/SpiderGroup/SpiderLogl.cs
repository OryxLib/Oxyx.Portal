using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.SpiderCartoon.Core.V3.SpiderGroup
{
    public class SpiderLog
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Type { get; set; }

        public int Level { get; set; }

        public string TargetUrl { get; set; }

        public bool IsDownload { get; set; }

        public string DownloadFilePath { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
