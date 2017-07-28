using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{

    /// <summary>
    /// 数据中心_队伍_人员表
    /// </summary>
    public class DC_ARMY_MEMBER_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_ARMY_MEMBER_ID { get; set; }
        /// <summary>
        /// 所属队伍序号
        /// </summary>
        public string DC_ARMY_ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string MEMBERNAME { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string SEX { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string CONTACTS { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string BIRTH { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 性别名称
        /// </summary>
        public string SEXNAME { get; set; }

    }
}
