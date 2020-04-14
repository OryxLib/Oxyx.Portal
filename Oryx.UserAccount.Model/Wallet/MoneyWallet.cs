using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.UserAccount.Model.Wallet
{
    public class MoneyWallet
    {
        public Guid Id { get; set; }

        public decimal Money { get; set; }

        public Guid UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public UserAccountEntry Account { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
