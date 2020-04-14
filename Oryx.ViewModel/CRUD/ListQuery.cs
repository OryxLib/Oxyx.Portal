using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.ViewModel.CRUD
{
    public class ListQuery
    {
        public int PageIndex { get; set; }

        public string QueryKey { get; set; }

        public List<ListQuerySort> SortList { get; set; }
    }

    public class ListQuerySort
    {
        public string Key { get; set; }

        public string Directory { get; set; }
    }
}
