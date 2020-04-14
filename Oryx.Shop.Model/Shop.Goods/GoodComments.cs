using Oryx.CommonInterface.Interface; 
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Shop.Model.Shop.Goods
{
    public class GoodComments : IEntityModel
    {
        public Guid Id { get; set; }

        public Guid UserAccountEntryId { get; set; }

        [ForeignKey("UserAccountEntryId ")]
        public UserAccountEntry UserAccountEntry { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
