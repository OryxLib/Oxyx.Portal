using Oryx.UserAccount.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oryx.UserActivity.Model
{
    public class ActivityLog
    {
        public Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public UserAccountEntry UserAccount { get; set; }

        public string Key { get; set; }

        /// <summary>
        /// 用来保存可能存在的文本或其他形式的答案等
        /// </summary>
        public string Text { get; set; }

        public DateTime CreateTime { get; set; }


    }
}
