﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Oryx.CommonDbOperation;
using System;

namespace Oryx.Shop.BusinessApi.Migrations
{
    [DbContext(typeof(CommonDbContext))]
    [Migration("20180619092141_addCoupon")]
    partial class addCoupon
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("Oryx.Shop.Model.Shop.AdminUser.AdminUserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("AdminUesrEntity");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Coupon.CouponEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Amount");

                    b.Property<string>("CouponType");

                    b.Property<DateTime>("CreateTime");

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("GoodsType");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CouponEntity");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Customer.CustomerAccount", b =>
                {
                    b.Property<Guid>("UserCode")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("NickName");

                    b.Property<string>("OpenId");

                    b.HasKey("UserCode");

                    b.ToTable("CustomerAccount");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Customer.CustomerCoupon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CanUse");

                    b.Property<Guid?>("CouponMappingId");

                    b.Property<Guid?>("CustomerUserCode");

                    b.Property<bool>("IsUsed");

                    b.HasKey("Id");

                    b.HasIndex("CouponMappingId");

                    b.HasIndex("CustomerUserCode");

                    b.ToTable("CustomerCoupon");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Customer.CustomerInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid?>("CustomerUserCode");

                    b.Property<string>("District");

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Province");

                    b.HasKey("Id");

                    b.HasIndex("CustomerUserCode");

                    b.ToTable("CustomerInfo");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Customer.CustomerYanzhi", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid?>("CustomerAccountUserCode");

                    b.Property<string>("PhotoUrl");

                    b.Property<double>("Score");

                    b.HasKey("Id");

                    b.HasIndex("CustomerAccountUserCode");

                    b.ToTable("CustomerYanzhi");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Goods.GoodCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("GoodCategory");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Goods.GoodComments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid?>("CustomerAccountUserCode");

                    b.HasKey("Id");

                    b.HasIndex("CustomerAccountUserCode");

                    b.ToTable("GoodComments");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Goods.GoodEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<string>("CoverPics");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Detail");

                    b.Property<Guid?>("GoodOfferId");

                    b.Property<string>("GoodsType");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("GoodOfferId");

                    b.ToTable("GoodEntity");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Goods.GoodOffer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<double>("EndScore");

                    b.Property<double>("OfferValue");

                    b.Property<double>("StartScore");

                    b.HasKey("Id");

                    b.ToTable("GoodOffer");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Orders.OrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid?>("CustomerAccountUserCode");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name");

                    b.Property<Guid?>("OrderExpressInfoId");

                    b.Property<string>("OutTradeNo");

                    b.Property<int>("Status");

                    b.Property<Guid>("UserCoder");

                    b.HasKey("Id");

                    b.HasIndex("CustomerAccountUserCode");

                    b.HasIndex("OrderExpressInfoId")
                        .IsUnique();

                    b.ToTable("OrderEntity");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Orders.OrderExpressInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustomerAddress");

                    b.Property<string>("CustomerName");

                    b.Property<string>("CustomerPhone");

                    b.HasKey("Id");

                    b.ToTable("OrderExpressInfo");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Orders.OrderGoodMapping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid?>("GoodId");

                    b.Property<Guid?>("OrderId");

                    b.HasKey("Id");

                    b.HasIndex("GoodId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderGoodMapping");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.PayLog.PayLogEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid?>("CustomerUserCode");

                    b.Property<Guid?>("OrderId");

                    b.Property<string>("ProcessResult");

                    b.Property<int>("TotalFee");

                    b.Property<string>("TradeNo");

                    b.HasKey("Id");

                    b.HasIndex("CustomerUserCode");

                    b.HasIndex("OrderId");

                    b.ToTable("PayLogEntity");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.ShoppingBascket.ShoppingBasketEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid?>("CustomerUserCode");

                    b.Property<Guid?>("GoodId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerUserCode");

                    b.HasIndex("GoodId");

                    b.ToTable("ShoppingBasketEntity");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Customer.CustomerCoupon", b =>
                {
                    b.HasOne("Oryx.Shop.Model.Shop.Coupon.CouponEntity", "CouponMapping")
                        .WithMany()
                        .HasForeignKey("CouponMappingId");

                    b.HasOne("Oryx.Shop.Model.Shop.Customer.CustomerAccount", "Customer")
                        .WithMany("CustomerCoupon")
                        .HasForeignKey("CustomerUserCode");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Customer.CustomerInfo", b =>
                {
                    b.HasOne("Oryx.Shop.Model.Shop.Customer.CustomerAccount", "Customer")
                        .WithMany("CustomerInfoList")
                        .HasForeignKey("CustomerUserCode");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Customer.CustomerYanzhi", b =>
                {
                    b.HasOne("Oryx.Shop.Model.Shop.Customer.CustomerAccount", "CustomerAccount")
                        .WithMany("CustomerYanzhiList")
                        .HasForeignKey("CustomerAccountUserCode");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Goods.GoodComments", b =>
                {
                    b.HasOne("Oryx.Shop.Model.Shop.Customer.CustomerAccount", "CustomerAccount")
                        .WithMany()
                        .HasForeignKey("CustomerAccountUserCode");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Goods.GoodEntity", b =>
                {
                    b.HasOne("Oryx.Shop.Model.Shop.Goods.GoodCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("Oryx.Shop.Model.Shop.Goods.GoodOffer", "GoodOffer")
                        .WithMany()
                        .HasForeignKey("GoodOfferId");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Orders.OrderEntity", b =>
                {
                    b.HasOne("Oryx.Shop.Model.Shop.Customer.CustomerAccount", "CustomerAccount")
                        .WithMany()
                        .HasForeignKey("CustomerAccountUserCode");

                    b.HasOne("Oryx.Shop.Model.Shop.Orders.OrderExpressInfo", "OrderExpressInfo")
                        .WithOne("OrderEntity")
                        .HasForeignKey("Oryx.Shop.Model.Shop.Orders.OrderEntity", "OrderExpressInfoId");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Orders.OrderGoodMapping", b =>
                {
                    b.HasOne("Oryx.Shop.Model.Shop.Goods.GoodEntity", "Good")
                        .WithMany()
                        .HasForeignKey("GoodId");

                    b.HasOne("Oryx.Shop.Model.Shop.Orders.OrderEntity", "Order")
                        .WithMany("OrderGoods")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.PayLog.PayLogEntity", b =>
                {
                    b.HasOne("Oryx.Shop.Model.Shop.Customer.CustomerAccount", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerUserCode");

                    b.HasOne("Oryx.Shop.Model.Shop.Orders.OrderEntity", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.ShoppingBascket.ShoppingBasketEntity", b =>
                {
                    b.HasOne("Oryx.Shop.Model.Shop.Customer.CustomerAccount", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerUserCode");

                    b.HasOne("Oryx.Shop.Model.Shop.Goods.GoodEntity", "Good")
                        .WithMany()
                        .HasForeignKey("GoodId");
                });
#pragma warning restore 612, 618
        }
    }
}
