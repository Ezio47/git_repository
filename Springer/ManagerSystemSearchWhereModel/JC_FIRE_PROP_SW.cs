using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{

    /// <summary>
    /// 监测_火情属性表
    /// </summary>
    public class JC_FIRE_PROP_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string JC_FIRE_PROPID { get; set; }
        /// <summary>
        /// 火灾序号
        /// </summary>
        public string JCFID { get; set; }

        /// <summary>
        /// 火灾级别
        /// </summary>
        public string FIRELEVEL { get; set; }
    }
}
