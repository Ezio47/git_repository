using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel.LogicModel
{
    /// <summary>
    /// 护林员周边火点
    /// </summary>
    public class HlyAreaDataSW
    {
        /// <summary>
        /// 周边距离
        /// </summary>
        public string AREA { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string DATETIME { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }

        /// <summary>
        /// 地图类型
        /// </summary>
        public string MapType { get; set; }

    }
}
