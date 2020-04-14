using Oryx.Shop.Model.Shop.Goods;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Shop.Model.Shop.Orders
{
    public class OrderGoodMapping
    {
        public Guid Id { get; set; }

        public virtual OrderEntity Order { get; set; }

        public virtual GoodEntity Good { get; set; }

        public int Number { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
