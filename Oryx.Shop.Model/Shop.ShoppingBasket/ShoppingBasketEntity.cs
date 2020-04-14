using Oryx.Shop.Model.Shop.Goods;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Shop.Model.Shop.ShoppingBascket
{
    public class ShoppingBasketEntity
    {
        public Guid Id { get; set; }

        public GoodEntity Good { get; set; }

        public Guid UserAccountEntryId { get; set; }

        [ForeignKey("UserAccountEntryId ")]
        public UserAccountEntry UserAccountEntry { get; set; }

        public int Amount { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
