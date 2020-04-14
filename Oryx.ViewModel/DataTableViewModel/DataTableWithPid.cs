using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.ViewModel.DataTableViewModel
{
    public class DataTableWithPid : DataTableAjaxPostModel
    {
        public Guid pid { get; set; }
    }
}
