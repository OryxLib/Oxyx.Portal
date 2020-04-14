using System;
using System.Collections.Generic;
using System.Text;

namespace Oryxl.DynamicPage.Model
{
    public class FormEntryDetail
    {
        public Guid Id { get; set; }

        public string PropertyName { get; set; }

        public string Type { get; set; }

        public string MappingElement { get; set; }

        public FormEntry FormEntry { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
