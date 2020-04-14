﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Oryx.CommonDbOperation;
using Oryx.Content.Model;
using Oryx.Shop.Model.Shop.Orders;
using Oryx.Social.Model;
using System;

namespace Oryx.Shop.BusinessApi.Migrations
{
    [DbContext(typeof(CommonDbContext))]
    [Migration("20181023024531_addPostEntryLikedLog")]
    partial class addPostEntryLikedLog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("Oryx.Content.Model.Banners", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<string>("Link");

                    b.Property<int>("Order");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Banners");
                });

            modelBuilder.Entity("Oryx.Content.Model.Categories", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Description");

                    b.Property<int>("Fee");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ParentCategoryId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Oryx.Content.Model.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<Guid?>("ContentEntryId");

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid?>("ParentCommentId");

                    b.Property<Guid?>("UserAccountId");

                    b.HasKey("Id");

                    b.HasIndex("ContentEntryId");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("UserAccountId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Oryx.Content.Model.ContentEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CategoryId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("Fee");

                    b.Property<int>("Order");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("ContentEntry");
                });

            modelBuilder.Entity("Oryx.Content.Model.ContentPayLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CostMoney");

                    b.Property<int>("CostRewardPoints");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("PayContentId");

                    b.Property<int>("PayContentType");

                    b.Property<int>("PayType");

                    b.Property<Guid>("UserAccountId");

                    b.HasKey("Id");

                    b.ToTable("ContentPayLog");
                });

            modelBuilder.Entity("Oryx.Content.Model.ContentUserReadLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CategoryId");

                    b.Property<Guid>("ContentEntryId");

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ContentEntryId");

                    b.HasIndex("UserId");

                    b.ToTable("ContentUserReadLog");
                });

            modelBuilder.Entity("Oryx.Content.Model.FileEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActualPath");

                    b.Property<Guid?>("CategoriesId");

                    b.Property<Guid?>("ContentEntryId");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.Property<string>("Tag");

                    b.HasKey("Id");

                    b.HasIndex("CategoriesId");

                    b.HasIndex("ContentEntryId");

                    b.ToTable("FileEntry");
                });

            modelBuilder.Entity("Oryx.Content.Model.Tags", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CategoriesId");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.HasKey("Id");

                    b.HasIndex("CategoriesId");

                    b.ToTable("Tags");
                });

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

                    b.Property<Guid?>("OrderEntityId");

                    b.HasKey("Id");

                    b.HasIndex("OrderEntityId");

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

                    b.Property<string>("OutTradeNo");

                    b.Property<int>("Status");

                    b.Property<Guid>("UserCoder");

                    b.HasKey("Id");

                    b.HasIndex("CustomerAccountUserCode");

                    b.ToTable("OrderEntity");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Orders.OrderExpressInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustomerAddress");

                    b.Property<string>("CustomerName");

                    b.Property<string>("CustomerPhone");

                    b.Property<Guid>("OrderEntityId");

                    b.HasKey("Id");

                    b.HasIndex("OrderEntityId")
                        .IsUnique();

                    b.ToTable("OrderExpressInfo");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Orders.OrderGoodMapping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid?>("GoodId");

                    b.Property<int>("Number");

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

            modelBuilder.Entity("Oryx.Social.Model.ChatCollection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ChatLogId");

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChatLogId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatCollection");
                });

            modelBuilder.Entity("Oryx.Social.Model.ChatLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.HasKey("Id");

                    b.ToTable("ChatLog");
                });

            modelBuilder.Entity("Oryx.Social.Model.ChatMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ChageLogId");

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("FromUserId");

                    b.Property<bool>("IsReaded");

                    b.Property<string>("MessageContent");

                    b.Property<int>("MsgType");

                    b.Property<long>("TimeStamp");

                    b.Property<Guid>("ToUserId");

                    b.HasKey("Id");

                    b.HasIndex("ChageLogId");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("ChatMessage");
                });

            modelBuilder.Entity("Oryx.Social.Model.PostEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("Order");

                    b.Property<string>("PostEntryTopic");

                    b.Property<string>("TextContent");

                    b.Property<long>("TimeStamp");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("PostEntry");
                });

            modelBuilder.Entity("Oryx.Social.Model.PostEntryComments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("LikeNum");

                    b.Property<Guid?>("ParentCommentId");

                    b.Property<Guid?>("PostEntryId");

                    b.Property<int>("TimeStamp");

                    b.Property<Guid?>("UserAccountId");

                    b.HasKey("Id");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("PostEntryId");

                    b.HasIndex("UserAccountId");

                    b.ToTable("PostEntryComments");
                });

            modelBuilder.Entity("Oryx.Social.Model.PostEntryFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActualPath");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.Property<Guid?>("PostEntryId");

                    b.Property<string>("Tag");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PostEntryId");

                    b.ToTable("PostEntryFile");
                });

            modelBuilder.Entity("Oryx.Social.Model.PostEntryLikedLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("PostEntryId");

                    b.Property<Guid>("UserAccountEntryId");

                    b.HasKey("Id");

                    b.HasIndex("PostEntryId");

                    b.HasIndex("UserAccountEntryId");

                    b.ToTable("PostEntryLikedLog");
                });

            modelBuilder.Entity("Oryx.Social.Model.PostEntrySocialInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LikeNum");

                    b.Property<Guid?>("PostEntryId");

                    b.HasKey("Id");

                    b.HasIndex("PostEntryId");

                    b.ToTable("PostEntrySocialInfo");
                });

            modelBuilder.Entity("Oryx.Social.Model.PostEntryTopic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("PostEntryTopic");
                });

            modelBuilder.Entity("Oryx.Social.Model.PostEntryUserAnchor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NextTimeStamp");

                    b.Property<int>("NextTimeStampOrder");

                    b.Property<int>("PrevTimeStamp");

                    b.Property<int>("PrevTimeStampOrder");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("PostEntryUserAnchor");
                });

            modelBuilder.Entity("Oryx.UserAccount.Model.UserAccountEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid?>("EmailId");

                    b.Property<Guid?>("MoneyWalletId");

                    b.Property<string>("NickName");

                    b.Property<Guid?>("PhoneId");

                    b.Property<Guid?>("ProfileId");

                    b.Property<Guid?>("RewardPointWalletId");

                    b.Property<Guid?>("UserNamePwdId");

                    b.Property<Guid?>("WeChatId");

                    b.HasKey("Id");

                    b.HasIndex("EmailId")
                        .IsUnique();

                    b.HasIndex("MoneyWalletId")
                        .IsUnique();

                    b.HasIndex("PhoneId")
                        .IsUnique();

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.HasIndex("RewardPointWalletId")
                        .IsUnique();

                    b.HasIndex("UserNamePwdId")
                        .IsUnique();

                    b.HasIndex("WeChatId")
                        .IsUnique();

                    b.ToTable("UserAccountEntry");
                });

            modelBuilder.Entity("Oryx.UserAccount.Model.UserAccountExtend.UserAccountEmail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("UserAccountEmail");
                });

            modelBuilder.Entity("Oryx.UserAccount.Model.UserAccountExtend.UserAccountPhone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("UserAccountPhone");
                });

            modelBuilder.Entity("Oryx.UserAccount.Model.UserAccountExtend.UserAccountProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDay");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Location");

                    b.HasKey("Id");

                    b.ToTable("UserAccountProfile");
                });

            modelBuilder.Entity("Oryx.UserAccount.Model.UserAccountExtend.UserAccountUserNamePwd", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("UserAccountUserNamePwd");
                });

            modelBuilder.Entity("Oryx.UserAccount.Model.UserAccountExtend.UserAccountWeChat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("OpenId");

                    b.HasKey("Id");

                    b.ToTable("UserAccountWeChat");
                });

            modelBuilder.Entity("Oryx.UserAccount.Model.Wallet.MoneyWallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<decimal>("Money");

                    b.HasKey("Id");

                    b.ToTable("MoneyWallet");
                });

            modelBuilder.Entity("Oryx.UserAccount.Model.Wallet.RewardPointWallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<decimal>("RewardPoints");

                    b.HasKey("Id");

                    b.ToTable("RewardPointWallet");
                });

            modelBuilder.Entity("Oryx.Content.Model.Categories", b =>
                {
                    b.HasOne("Oryx.Content.Model.Categories", "ParentCategory")
                        .WithMany("ChildrenCategories")
                        .HasForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("Oryx.Content.Model.Comment", b =>
                {
                    b.HasOne("Oryx.Content.Model.ContentEntry", "ContentEntry")
                        .WithMany("Comments")
                        .HasForeignKey("ContentEntryId");

                    b.HasOne("Oryx.Content.Model.Comment", "ParentComment")
                        .WithMany("ChildrenComment")
                        .HasForeignKey("ParentCommentId");

                    b.HasOne("Oryx.UserAccount.Model.UserAccountEntry", "UserAccount")
                        .WithMany()
                        .HasForeignKey("UserAccountId");
                });

            modelBuilder.Entity("Oryx.Content.Model.ContentEntry", b =>
                {
                    b.HasOne("Oryx.Content.Model.Categories", "Category")
                        .WithMany("ContentList")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("Oryx.Content.Model.ContentUserReadLog", b =>
                {
                    b.HasOne("Oryx.Content.Model.Categories", "Categories")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Oryx.Content.Model.ContentEntry", "ContentEntry")
                        .WithMany()
                        .HasForeignKey("ContentEntryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Oryx.UserAccount.Model.UserAccountEntry", "UserAccount")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Oryx.Content.Model.FileEntry", b =>
                {
                    b.HasOne("Oryx.Content.Model.Categories")
                        .WithMany("MediaResource")
                        .HasForeignKey("CategoriesId");

                    b.HasOne("Oryx.Content.Model.ContentEntry")
                        .WithMany("MediaResource")
                        .HasForeignKey("ContentEntryId");
                });

            modelBuilder.Entity("Oryx.Content.Model.Tags", b =>
                {
                    b.HasOne("Oryx.Content.Model.Categories")
                        .WithMany("Tags")
                        .HasForeignKey("CategoriesId");
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Coupon.CouponEntity", b =>
                {
                    b.HasOne("Oryx.Shop.Model.Shop.Orders.OrderEntity")
                        .WithMany("CouponEntity")
                        .HasForeignKey("OrderEntityId");
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
                });

            modelBuilder.Entity("Oryx.Shop.Model.Shop.Orders.OrderExpressInfo", b =>
                {
                    b.HasOne("Oryx.Shop.Model.Shop.Orders.OrderEntity", "OrderEntity")
                        .WithOne("OrderExpressInfo")
                        .HasForeignKey("Oryx.Shop.Model.Shop.Orders.OrderExpressInfo", "OrderEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
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

            modelBuilder.Entity("Oryx.Social.Model.ChatCollection", b =>
                {
                    b.HasOne("Oryx.Social.Model.ChatLog", "ChatLog")
                        .WithMany("ChatCollection")
                        .HasForeignKey("ChatLogId");

                    b.HasOne("Oryx.UserAccount.Model.UserAccountEntry", "UserAccountEntry")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Oryx.Social.Model.ChatMessage", b =>
                {
                    b.HasOne("Oryx.Social.Model.ChatLog", "ChageLog")
                        .WithMany("ChatMessageList")
                        .HasForeignKey("ChageLogId");

                    b.HasOne("Oryx.UserAccount.Model.UserAccountEntry", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Oryx.UserAccount.Model.UserAccountEntry", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Oryx.Social.Model.PostEntryComments", b =>
                {
                    b.HasOne("Oryx.Social.Model.PostEntryComments", "ParentComment")
                        .WithMany("ChildrenComment")
                        .HasForeignKey("ParentCommentId");

                    b.HasOne("Oryx.Social.Model.PostEntry", "PostEntry")
                        .WithMany("PostEntryCommentList")
                        .HasForeignKey("PostEntryId");

                    b.HasOne("Oryx.UserAccount.Model.UserAccountEntry", "UserAccount")
                        .WithMany()
                        .HasForeignKey("UserAccountId");
                });

            modelBuilder.Entity("Oryx.Social.Model.PostEntryFile", b =>
                {
                    b.HasOne("Oryx.Social.Model.PostEntry")
                        .WithMany("PostEntryFileList")
                        .HasForeignKey("PostEntryId");
                });

            modelBuilder.Entity("Oryx.Social.Model.PostEntryLikedLog", b =>
                {
                    b.HasOne("Oryx.Social.Model.PostEntry", "PostEntry")
                        .WithMany()
                        .HasForeignKey("PostEntryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Oryx.UserAccount.Model.UserAccountEntry", "UserAccounetEntry")
                        .WithMany()
                        .HasForeignKey("UserAccountEntryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Oryx.Social.Model.PostEntrySocialInfo", b =>
                {
                    b.HasOne("Oryx.Social.Model.PostEntry", "PostEntry")
                        .WithMany()
                        .HasForeignKey("PostEntryId");
                });

            modelBuilder.Entity("Oryx.UserAccount.Model.UserAccountEntry", b =>
                {
                    b.HasOne("Oryx.UserAccount.Model.UserAccountExtend.UserAccountEmail", "Email")
                        .WithOne("UserAccount")
                        .HasForeignKey("Oryx.UserAccount.Model.UserAccountEntry", "EmailId");

                    b.HasOne("Oryx.UserAccount.Model.Wallet.MoneyWallet", "MoneyWallet")
                        .WithOne("Account")
                        .HasForeignKey("Oryx.UserAccount.Model.UserAccountEntry", "MoneyWalletId");

                    b.HasOne("Oryx.UserAccount.Model.UserAccountExtend.UserAccountPhone", "Phone")
                        .WithOne("UserAccount")
                        .HasForeignKey("Oryx.UserAccount.Model.UserAccountEntry", "PhoneId");

                    b.HasOne("Oryx.UserAccount.Model.UserAccountExtend.UserAccountProfile", "Profile")
                        .WithOne("UserAccount")
                        .HasForeignKey("Oryx.UserAccount.Model.UserAccountEntry", "ProfileId");

                    b.HasOne("Oryx.UserAccount.Model.Wallet.RewardPointWallet", "RewardPointWallet")
                        .WithOne("Account")
                        .HasForeignKey("Oryx.UserAccount.Model.UserAccountEntry", "RewardPointWalletId");

                    b.HasOne("Oryx.UserAccount.Model.UserAccountExtend.UserAccountUserNamePwd", "UserNamePwd")
                        .WithOne("UserAccount")
                        .HasForeignKey("Oryx.UserAccount.Model.UserAccountEntry", "UserNamePwdId");

                    b.HasOne("Oryx.UserAccount.Model.UserAccountExtend.UserAccountWeChat", "WeChat")
                        .WithOne("UserAccount")
                        .HasForeignKey("Oryx.UserAccount.Model.UserAccountEntry", "WeChatId");
                });
#pragma warning restore 612, 618
        }
    }
}
