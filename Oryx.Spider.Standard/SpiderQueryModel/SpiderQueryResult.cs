using Oryx.SpiderCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.SpiderCore.SpiderQueryModel
{
    public class SpiderQueryResult : ISpiderQueryResult
    {
        /// <summary>
        /// 结果Key
        /// </summary>
        public string KeyName { get; set; }

        public string Path { get; set; }

        public string QueryValue
        {
            get
            {
                return string.Join("", QueryResult);
            }
        }

        /// <summary>
        /// 结果Value 列表
        /// </summary>
        public List<string> QueryResult { get; set; }
    }
}
