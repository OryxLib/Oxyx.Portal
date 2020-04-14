using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.ViewModel.UserInfo
{
    public class UserInfoOutputViewModel
    {
        public string nickName { get; set; }
        public string avatar { get; set; }
        public string location { get; set; }
        public DateTime birthDay { get; set; }
        public Guid userId { get; set; }
    }
}
