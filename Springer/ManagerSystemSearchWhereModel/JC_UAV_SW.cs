using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 无人机
    /// </summary>
    public class JC_UAV_SW
    {
        /// <summary>
        /// 无人机序号
        /// </summary>
        public string UAVID { get; set; }

        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }

        /// <summary>
        /// 无人机名称
        /// </summary>
        public string UAVNAME { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string UAVEQUIPNAME { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
    }
}
