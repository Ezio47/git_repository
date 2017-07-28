using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 公用_机构表_联系人
    /// </summary>
    public class T_SYS_ORG_LINK_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string ORGLINK_ID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 所属类型
        /// </summary>
        public string ORGLINKTYPE { get; set; }
        /// <summary>
        /// 所属类型名称
        /// </summary>
        public string ORGLINKTYPEName { get; set; }
        /// <summary>
        /// 单位名称 村委会名称
        /// </summary>
        public string UNITNAME { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 用户职位
        /// </summary>
        public string USERJOB { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string Tell { get; set; }
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
    }
}
