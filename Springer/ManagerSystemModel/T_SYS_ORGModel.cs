using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 组织机构Model
    /// </summary>
    public class T_SYS_ORGModel
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
        /// 机构简称
        /// </summary>
        public string ORGJC { get; set; }
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
        /// 行政区划名称
        /// </summary>
        public string AreaNAME { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 指挥部名称
        /// </summary>
        public string COMMANDNAME { get; set; }
        /// <summary>
        /// 方法
        /// </summary>

        public string opMethod { get; set; }
        /// <summary>
        /// 返回Url
        /// </summary>

        public string returnUrl { get; set; }
        /// <summary>
        /// 卫星热点简称
        /// </summary>
        public string WXJC { get; set; }
        /// <summary>
        /// 气象信息简称
        /// </summary>
        public string WEATHERJC { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string POSTCODE { get; set; }
        /// <summary>
        /// 值班电话
        /// </summary>
        public string DUTYTELL { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string FAX { get; set; }
        /// <summary>
        /// 手机回传参数列表
        /// </summary>
        public string MOBILEPARAMLIST { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string ADDRESS { get; set; }
    }
}
