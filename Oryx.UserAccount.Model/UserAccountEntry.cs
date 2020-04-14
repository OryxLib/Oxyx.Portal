using Oryx.CommonInterface.Interface;
using Oryx.UserAccount.Model.UserAccountExtend;
using Oryx.UserAccount.Model.UserBusinessExtend;
using Oryx.UserAccount.Model.Wallet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oryx.UserAccount.Model
{
    public class UserAccountEntry : IEntityModel
    {
        public Guid Id { get; set; }

        public string NickName { get; set; }

        public string Avatar { get; set; }

        public UserAccountEmail Email { get; set; }

        public UserAccountPhone Phone { get; set; }

        public UserAccountWeChat WeChat { get; set; }

        public UserAccountUserNamePwd UserNamePwd { get; set; }

        public MoneyWallet MoneyWallet { get; set; }

        public RewardPointWallet RewardPointWallet { get; set; }

        public UserAccountProfile Profile { get; set; }

        public UserAccountInviteOrign InviteOrigin { get; set; }

        public List<UserAccountBusinessRole> Roles { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
