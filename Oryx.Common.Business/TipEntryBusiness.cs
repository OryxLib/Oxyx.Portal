using Oryx.Common.DataAccessor;
using Oryx.Common.Model;
using Oryx.CommonDbOperation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Common.Business
{
    public class TipEntryBussiness : BaseBusiness<TipEntry>
    {
        public readonly TipEntryDataAccessor tipEntryDataAccessor;
        public TipEntryBussiness(TipEntryDataAccessor commonAccessor)
            : base(commonAccessor)
        {
            tipEntryDataAccessor = commonAccessor;
        }

        public async Task AddOrUpdate(TipEntry tipEntry)
        {
            var _tipEntry = await tipEntryDataAccessor.OneAsync<TipEntry>(x => x.Id == tipEntry.Id);
            if (_tipEntry == null)
            {
                _tipEntry = new TipEntry
                {
                    Id = tipEntry.Id,
                    Content = tipEntry.Content,
                    CreateTime = DateTime.Now,
                    Link = tipEntry.Link,
                    PublishTime = tipEntry.PublishTime,
                    Title = tipEntry.Title
                };
                await tipEntryDataAccessor.Add(_tipEntry);
            }
            else
            {
                _tipEntry = new TipEntry
                {
                    Content = tipEntry.Content,
                    Link = tipEntry.Link,
                    PublishTime = tipEntry.PublishTime,
                    Title = tipEntry.Title
                };
                await tipEntryDataAccessor.Update(_tipEntry);
            }
        }

        public async Task<TipEntry> GetTipeId(Guid Id)
        {
            return await tipEntryDataAccessor.OneAsync<TipEntry>(x => x.Id == Id);
        }

        public async Task CreateOrUpdateTipEntry(TipEntry tipEntry)
        {
            await tipEntryDataAccessor.AddOrUpdate(tipEntry);
        }

        public async Task Delete(Guid Id)
        {
            await tipEntryDataAccessor.Delete<TipEntry>(x => x.Id == Id);
        }
    }
}
