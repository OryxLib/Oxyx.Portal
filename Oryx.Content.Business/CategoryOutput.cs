using Oryx.Content.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Content.Business
{
    public class CategoryOutput
    {
        public Categories Category { get; set; }  
        public List<CategoryComment> CategoryComments { get; set; }
        public IList<ContentUserReadLog> UserReadLog { get; internal set; }
    }
}
