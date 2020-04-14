using Oryx.CommonInterface.Interface;
using Oryx.Shop.Model.Shop.Coupon;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oryx.Shop.Model.Shop.Orders
{
    public class OrderEntity : IEntityModel
    {
        public Guid Id { get; set; }

        public Guid UserCoder { get; set; }

        public string Name { get; set; }

        public Guid UserAccountEntryId { get; set; }

        [ForeignKey("UserAccountEntryId ")]
        public UserAccountEntry UserAccountEntry { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public bool IsDelete { get; set; } = false;

        public virtual IList<OrderGoodMapping> OrderGoods { get; set; }

        public virtual IList<CouponEntity> CouponEntity { get; set; }

        public OrderExpressInfo OrderExpressInfo { get; set; }

        public string OutTradeNo { get; set; }
    }
}
