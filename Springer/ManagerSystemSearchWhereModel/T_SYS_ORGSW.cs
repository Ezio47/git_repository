using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 组织机构
    /// </summary>
    public class T_SYS_ORGSW
    {
        /// <summary>
        /// 机构编码
        /// </summary>
        public string ORGNO { get; set; }
        /// <summary>
        /// 机构名
        /// </summary>
        public string ORGNAME { get; set; }
        /// <summary>
        /// 机构职责
        /// </summary>
        public string ORGDUTY { get; set; }
        /// <summary>
        /// 领导
        /// </summary>
        public string LEADER { get; set; }
        /// <summary>
        /// 区划编码
        /// </summary>
        public string AREACODE { get; set; }
        /// <summary>
        /// 所属系统标识
        /// </summary>
        public string SYSFLAG { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
        /// <summary>
        /// 顶级单位编码 用于获取该单位及该单位下面所属的单位信息
        /// </summary>

        public string TopORGNO { get; set; }
        /// <summary>
        /// 当前编码，用于下拉框匹配默认选中项
        /// </summary>
        public string CurORGNO { get; set; }
        /// <summary>
        /// 所有县机构编码
        /// </summary>
        public string GetContyORGNOByCity { get; set; }
        /// <summary>
        ///县获取所有乡镇
        /// </summary>
        public string GetXZOrgNOByConty { get; set; }
        /// <summary>
        /// 只获取市
        /// </summary>
        public string OnlyGetShi { get; set; }
        /// <summary>
        /// 只获取市、县
        /// </summary>
        public string OnlyGetShiXian { get; set; }     
        /// <summary>
        /// 只获取县
        /// </summary>
        public string OnlyGetXian { get; set; }
        /// <summary>
        /// 只获取县、乡镇
        /// </summary>
        public string OnlyGetXianXZ { get; set; }
        /// <summary>
        /// 获取市县乡
        /// </summary>
        public string OnlyGetShiXianXZ { get; set; }
        /// <summary>
        /// 数据中心组织机构下拉框全部
        /// </summary>
        public string IsShowAll{ get; set; }
        /// <summary>
        /// 顶级单位市县编码 用于获取该单位及该单位下面县
        /// </summary>
        public string TopSXORGNO { get; set; }
        /// <summary>
        /// 数据中心图标展示顶级组织机构
        /// </summary>
        public string TopEchartORGNO { get; set; }
        /// <summary>
        /// 是否显示村 默认不显示村 0或null不显示 1显示
        /// </summary>
        public string IsEnableCUN { get; set; }
    }
}
