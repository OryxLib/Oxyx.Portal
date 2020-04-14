using Oryxl.DynamicPage.Accessor;
using Oryxl.DynamicPage.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryxl.DynamicPage.Business
{
    public class FormEntryBusiness
    {
        private FormEntryAccessor formEntryAccessor;
        public FormEntryBusiness(FormEntryAccessor _formEntryAccessor)
        {
            formEntryAccessor = _formEntryAccessor;
        }

        public async Task AddFormEntry(FormEntry formEntry)
        {
            await formEntryAccessor.Add(formEntry);
        }

        public async Task<FormEntry> GetFormEntry(string name)
        {
            return await formEntryAccessor.OneAsync<FormEntry>(x => x.Name == name);
        }
    }
}
