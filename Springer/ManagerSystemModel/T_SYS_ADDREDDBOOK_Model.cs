using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 用户_通讯录表
    /// </summary>
    public class T_SYS_ADDREDDBOOK_Model
    {
        /// <summary>
        /// 通讯录序号
        /// </summary>
        public string ADID { get; set; }
        /// <summary>
        /// 通讯录类别序号
        /// </summary>
        public string ATID { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string ORGNO { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string ADNAME { get; set; }
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
        public string ORGNAME { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string ATName { get; set; }
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
