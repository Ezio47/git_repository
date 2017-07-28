using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 行政区划管理
    /// </summary>
    public class T_ALL_AREA_SW
    {
        /// <summary>
        /// 行政区划码
        /// </summary>
        public string AREACODE { get; set;}
        /// <summary>
        /// 行政区划名
        /// </summary>
        public string AREANAME { get; set;}
        /// <summary>
        /// 行政区划id
        /// </summary>
        public string AREAID { get; set; }

        /// <summary>
        /// 行政区划名简称
        /// </summary>
        public string AREAJC { get; set; }
        /// <summary>
        /// 行政区划码截取
        /// </summary>
        public string SubAREACODE { get; set; }
        /// <summary>
        /// 当前行政区划码
        /// </summary>
        public string CurAREACODE { get; set; }
        /// <summary>
        /// 市获取所有县
        /// </summary>
        public string GetContyORGNOByCity { get; set; }
        /// <summary>
        ///获取乡镇
        /// </summary>
        public string GetXZOrgNOByConty { get; set; }
        /// <summary>
        /// 获取市县
        /// </summary>
        public string OnlyGetShiXian { get; set; }
        /// <summary>
        /// 最高行政区划
        /// </summary>
        public string TOPAREACODE { get; set; }
    }
}
