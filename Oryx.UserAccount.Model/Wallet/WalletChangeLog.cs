using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.UserAccount.Model.Wallet
{
    public class WalletChangeLog
    {
        public Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public UserAccountEntry Account { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreateTime { get; set; }

        public ChangeType ChangeType { get; set; }
    }

    public enum ChangeType
    {
        Money,
        RewardPoints
    }
}
