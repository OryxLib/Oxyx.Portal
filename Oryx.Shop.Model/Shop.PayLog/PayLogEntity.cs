using Oryx.Shop.Model.Shop.Orders;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Shop.Model.Shop.PayLog
{
    public class PayLogEntity
    {
        public int Id { get; set; }

        public string TradeNo { get; set; }

        public OrderEntity Order { get; set; }

        public Guid UserAccountEntryId { get; set; }

        [ForeignKey("UserAccountEntryId ")]
        public UserAccountEntry UserAccountEntry { get; set; }

        public int TotalFee { get; set; }

        public string ProcessResult { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
