using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Content.Model
{
    public class ContentPayLog
    {
        public Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        public PayType PayType { get; set; }

        public PayContentType PayContentType { get; set; }

        public string PayContentId { get; set; }

        public int CostMoney { get; set; }

        public int CostRewardPoints { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public enum PayContentType
    {
        Category,
        ContentEntry
    }

    public enum PayType
    {
        Money,
        RewardPoints,
        Mix
    }
}
