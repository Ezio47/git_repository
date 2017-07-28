using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 预警_火险等级表
    /// </summary>
    public class YJ_DANGERCLASS_SW
    {
        /// <summary>
        /// 机构码
        /// </summary>
        public string BYORGNO { get; set; }

        /// <summary>
        /// 火险等级时间
        /// </summary>
        public string DCDATE { get; set; }

        /// <summary>
        /// 火险等级
        /// </summary>
        public string DANGERCLASS { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string TOWNNAME { get; set; }

    }
}
