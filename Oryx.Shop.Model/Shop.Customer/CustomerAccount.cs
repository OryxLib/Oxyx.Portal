using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Shop.Model.Shop.Customer
{
    public class CustomerAccount : IEntityModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public UserAccountEntry UserAccount { get; set; }

        public string OpenId { get; set; }

        public string NickName { get; set; }

        public string Avatar { get; set; }

        public virtual IList<CustomerInfo> CustomerInfoList { get; set; }

        public virtual IList<CustomerYanzhi> CustomerYanzhiList { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public virtual List<CustomerCoupon> CustomerCoupon { get; set; }
    }
}
