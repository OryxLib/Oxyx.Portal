using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.ViewModel.Social.UserRelationship
{
    public class UserRelationshipInfoViewModel
    {
        public Guid UserAccountId { get; set; }

        /// <summary>
        /// 关注数
        /// </summary>
        public int SubcribCount { get; set; }

        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FansCount { get; set; }
    }
}
