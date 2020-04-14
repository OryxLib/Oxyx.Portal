using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using Oryx.Shop.Model;
using Oryx.Shop.Model.Shop.AdminUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Shop.DataAccessor.DA.AdminUser
{
    public class DataAdminAccessor : CommonAccessor
    {
        public DataAdminAccessor(CommonDbContext _dbContext, IDistributedCache _DistributedCache)
            : base(_dbContext, _DistributedCache)
        {
        }

        public async Task<bool> HasUser(string userName, string password)
        {
            var user = await OneAsync<AdminUserEntity>(x => x.UserName == userName && x.Password == password);

            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
