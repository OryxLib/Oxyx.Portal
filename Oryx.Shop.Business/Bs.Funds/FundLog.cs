using Oryx.Shop.Model.Shop.Orders;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Shop.Business.Bs.Funds
{
    public class FundLog
    {
        public Guid Id { get; set; }

        public Guid FromUserId { get; set; }

        [ForeignKey("FromUserId")]
        public UserAccountEntry FromUser { get; set; }

        public Guid OrderId { get; set; }

        [ForeignKey("OrderId ")]
        public OrderEntity OrderEntity { get; set; }

        public Guid ToUserId { get; set; }

        [ForeignKey("ToUserId")]
        public UserAccountEntry ToUser { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
