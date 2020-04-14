using Oryx.Shop.DataAccessor.DA.AdminUser;
using Oryx.ViewModel.Shop.AdminUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Shop.Business.Bs.AdminUser
{
    public class AdminUserBs
    {
        public DataAdminAccessor AdminAccossor { get; set; }

        public AdminUserBs(DataAdminAccessor _AdminAccessor)
        {
            AdminAccossor = _AdminAccessor;
        }

        public async Task<bool> Login(AdminLoginModel loginModel)
        {
            return await AdminAccossor.HasUser(loginModel.UserName, loginModel.Password);
        }
    }
}
