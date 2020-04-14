using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.UserAccount.Model.UserAccountExtend
{
    public class WeChatAccountOpenIdMapping
    {
        public Guid Id { get; set; }

        public string UnionId { get; set; }

        public string OpenId { get; set; }

        /// <summary>
        /// 绑定的账号类型 , App, web, 公众号
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 网站名: 例如次元吖
        /// </summary>
        public string Source { get; set; }

        public UserAccountWeChat UserAccountWechat { get; set; }
    }
}
