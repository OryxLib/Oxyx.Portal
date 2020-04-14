using System;
using System.Collections.Generic;
using System.Text;

namespace Oryxl.DynamicPage.Model
{
    public class FormEntry
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public List<FormEntryDetail> ForEntryList { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        //public string 
    }
}
