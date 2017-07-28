using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 公用_机构表_联系人
    /// </summary>
    public class T_SYS_ORG_LINK_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string ORGLINK_ID { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string ORGNO { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 所属类型
        /// </summary>
        public string ORGLINKTYPE { get; set; }
        /// <summary>
        /// 自然村名称
        /// </summary>
        public string UNITNAME { get; set; }
        /// <summary>
        /// 关键字查询(名称 用户职位 手机号码 办公电话)
        /// </summary>
        public string keys { get; set; }
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
        /// 排序编号
        /// </summary>
        public string  ORDERBY { get; set; }
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
