using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Oryx.Social.Model
{
    public class PostEntryUserAnchor
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        /// <summary>
        /// 前进时间戳
        /// </summary>
        public int PrevTimeStamp { get; set; }

        /// <summary>
        /// 后进时间戳
        /// </summary>
        public int NextTimeStamp { get; set; }

        /// <summary>
        /// 前进时间戳 数序
        /// </summary>
        public int PrevTimeStampOrder { get; set; }

        /// <summary>
        /// 后进时间抽数序
        /// </summary>
        public int NextTimeStampOrder { get; set; }
    }
}