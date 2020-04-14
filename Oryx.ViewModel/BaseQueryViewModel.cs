using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.ViewModel
{
    public class BaseQueryViewModel
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int Size { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 总页码
        /// </summary>
        public int Total { get; set; }
    }
}
