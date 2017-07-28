using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 公用_机构表_村委会
    /// </summary>
    public class T_SYS_ORG_CWH_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string CWHID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 村委会名称
        /// </summary>
        public string CWHNAME { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
        /// <summary>
        /// 所属单位名称
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 所属类型
        /// </summary>
        public string ORGLINKTYPE { get; set; }
        /// <summary>
        /// 自然村名称
        /// </summary>
        public string UNITNAME { get; set; }
        /// <summary>
        /// 自然村组织机构
        /// </summary>
        public string ORGNO { get; set; }
    }
}
