using Oryx.DataPlatform.Model;
using Oryx.DataPlatForm.DataAccessor;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Oryx.DataPlatForm.Business
{
    public class DataOperation
    {
        public readonly StoreMapInfoAccessor StoreMapInfoAccessor;

        public DataOperation(StoreMapInfoAccessor storeMapInfoAccessor)
        {
            StoreMapInfoAccessor = storeMapInfoAccessor;
        }

        public async Task<object> GetStoreMapInfo(string district)
        {
            return await StoreMapInfoAccessor.All<StoreMapInfo>().Where(x => x.District == district).ToListAsync();
        }

        public async Task<object> GetStoreMapInfo()
        {
            return await StoreMapInfoAccessor.All<StoreMapInfo>().ToListAsync();
        }
    }
}