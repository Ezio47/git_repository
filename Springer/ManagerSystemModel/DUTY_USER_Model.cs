using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 值班统计
    /// </summary>
    public class DUTY_USER_DUCKCOUNT_Model
    {
        /// <summary>
        /// 早班次数
        /// </summary>
        public string zaobCount { get; set; }
        /// <summary>
        /// 中班次数
        /// </summary>
        public string zhongbCount { get; set; }
        /// <summary>
        /// 晚班次数
        /// </summary>
        public string wanbCount { get; set; }
        /// <summary>
        /// 带班次数
        /// </summary>
        public string daiBCount { get; set; }
        /// <summary>
        /// 间隔查询值班日期开始时间字段
        /// </summary>
        public string BigONDUTYDATE { get; set; }
        /// <summary>
        /// 间隔查询值班日期结束时间字段
        /// </summary>
        public string EndONDUTYDATE { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string USERNAME { get; set; }
    }

    /// <summary>
    /// 值班人员
    /// </summary>
    public class DUTY_USER_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DUID { get; set; }
        /// <summary>
        /// 值班类别ID
        /// </summary>
        public string DUTY_TYPEID { get; set; }
        /// <summary>
        /// 值班日期
        /// </summary>
        public string DUTYDATE { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 人员值班类别
        /// </summary>
        public string DUTYUSERTYPE { get; set; }
        /// <summary>
        /// 值班人序号
        /// </summary>
        public string DUTYUSERID { get; set; }

        /// <summary>
        /// 是否签到
        /// </summary>
        public string ISATTENDED { get; set; }
        /// <summary>
        /// 签到时间
        /// </summary>
        public string ATTENDEDTIME { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string USERNAME { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ORGNAME { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string dateBegin { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string dateEnd { get; set; }

        /// <summary>
        /// 操作方法 
        /// </summary>
        public string opMethod { get; set; }

        /// <summary>
        /// 周末值班统计人数
        /// </summary>
        public string weekCount { get; set; }
    }
}
