using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oryx.Shop.Model.Shop.Orders
{
    public class OrderExpressInfo
    {
        public Guid Id { get; set; }

        public Guid OrderEntityId { get; set; }

        [ForeignKey("OrderEntityId")]
        public OrderEntity OrderEntity { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public string CustomerPhone { get; set; }
    }
}
