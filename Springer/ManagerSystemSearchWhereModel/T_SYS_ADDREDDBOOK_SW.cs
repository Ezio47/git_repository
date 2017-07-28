using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{

    /// <summary>
    /// 用户_通讯录表
    /// </summary>
    public class T_SYS_ADDREDDBOOK_SW
    {
        /// <summary>
        /// 通讯录序号
        /// </summary>
        public string ADID { get; set; }
        /// <summary>
        /// 通讯录父序号
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
        /// 手机号码
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        /// 包含下级单位编码的查询条件
        /// </summary>
        public string TopORGNO { get; set; }
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
