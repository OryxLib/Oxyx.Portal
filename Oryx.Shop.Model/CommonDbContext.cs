using Microsoft.EntityFrameworkCore;
using Oryx.Shop.Model.Shop.AdminUser;
using Oryx.Shop.Model.Shop.Customer;
using Oryx.Shop.Model.Shop.Goods;
using Oryx.Shop.Model.Shop.Orders;
using Oryx.Shop.Model.Shop.PayLog;
using Oryx.Shop.Model.Shop.ShoppingBascket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Shop.Model
{
    public class CommonDbContext : DbContext
    {
        public CommonDbContext(DbContextOptions<CommonDbContext> options) : base(options)
        {

        }

        public DbSet<AdminUserEntity> AdminUesrEntity { get; set; }

        public DbSet<CustomerAccount> CustomerAccount { get; set; }

        public DbSet<CustomerInfo> CustomerInfo { get; set; }

        public DbSet<CustomerYanzhi> CustomerYanzhi { get; set; }

        public DbSet<GoodCategory> GoodCategory { get; set; }

        public DbSet<GoodComments> GoodComments { get; set; }

        public DbSet<GoodEntity> GoodEntity { get; set; }

        public DbSet<GoodOffer> GoodOffer { get; set; }

        public DbSet<OrderEntity> OrderEntity { get; set; }

        public DbSet<OrderExpressInfo> OrderExpressInfo { get; set; }

        public DbSet<OrderGoodMapping> OrderGoodMapping { get; set; }

        public DbSet<PayLogEntity> PayLogEntity { get; set; }

        public DbSet<ShoppingBasketEntity> ShoppingBasketEntity { get; set; }
    }
}
