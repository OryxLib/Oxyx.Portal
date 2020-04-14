using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.SpiderCore.SpiderQueryModel
{
    public class SpiderResultDicionary
    {
        public SpiderResultDicionary Parent { get; set; }

        /// <summary>
        /// 查询名
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// QueryTarget 查询结果列表
        /// </summary>
        public List<SpiderQueryResult> QueryResult { get; set; }
    }
}
