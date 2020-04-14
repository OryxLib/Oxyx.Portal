using Oryx.Content.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.ViewModel.UserInfo
{
    public class UpdateUserInfoViewModel
    {
        public string Avatar { get; set; }

        public string NickName { get; set; }

        public DateTime BirthDay { get; set; }

        public string Location { get; set; }
    }
}
