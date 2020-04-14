using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Shop.Model.Shop.AdminUser
{
    public class AdminUserEntity
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
