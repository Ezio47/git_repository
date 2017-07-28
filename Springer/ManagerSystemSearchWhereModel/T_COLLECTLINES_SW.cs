using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 数据采集路线
    /// </summary>
    public class T_COLLECTLINES_SW
    {
        /// <summary>
        /// objectid
        /// </summary>
        public int OBJECTID { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 几何类型
        /// </summary>
        public string Shape { get; set; }
    }
}
