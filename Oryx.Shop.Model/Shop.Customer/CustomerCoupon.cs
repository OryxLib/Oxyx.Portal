using Oryx.Shop.Model.Shop.Coupon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Shop.Model.Shop.Customer
{
    public class CustomerCoupon
    {
        public Guid Id { get; set; }

        public CustomerAccount Customer { get; set; }

        public CouponEntity CouponMapping { get; set; }

        public bool IsUsed { get; set; }

        public bool CanUse { get; set; }
    }
}
