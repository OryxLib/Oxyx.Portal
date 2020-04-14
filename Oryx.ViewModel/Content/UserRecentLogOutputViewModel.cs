using Oryx.Content.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.ViewModel.Content
{
    public class UserRecentLogOutputViewModel
    {
        public Categories Categories { get; set; }

        public List<ContentEntry> ContentEntry { get; set; }
    }
}
