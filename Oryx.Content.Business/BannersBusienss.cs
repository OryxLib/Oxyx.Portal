using Oryx.Content.DataAccessor;
using Oryx.Content.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Content.Business
{
    public class BannersBusienss
    {
        public readonly DataBannersAccessor BannerDataAccessor;

        public BannersBusienss(DataBannersAccessor _BannerDataAccessor)
        {
            BannerDataAccessor = _BannerDataAccessor;
        }

        public async Task<List<Banners>> GetList()
        {
            var list = await BannerDataAccessor.ListOrderByDescendingAsync<Banners>(0, 3);
            return list.ToList();
        }

        public async Task Add(Model.Banners banner)
        {
            await BannerDataAccessor.Add(banner);
        }

        public async Task Update(Model.Banners banner)
        {
            await BannerDataAccessor.Update(banner);
        }

        public async Task Delete(Guid Id)
        {
            await BannerDataAccessor.Delete<Banners>(x => x.Id == Id);
        }

        public async Task<Banners> GetOne(Guid Id)
        {
            return await BannerDataAccessor.OneAsync<Banners>(x => x.Id == Id);
        }

        public async Task AddOrUpdate(Banners banner)
        {
            await BannerDataAccessor.AddOrUpdate(banner);
        }
    }
}
