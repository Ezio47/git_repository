using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 值班人
    /// </summary>
    public class DUTY_USER_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DUID { get; set; }
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
        /// 查询开始时间
        /// </summary>
        public string DTBegin { get; set; }
        /// <summary>
        /// 查询结束时间
        /// </summary>
        public string DTEnd { get; set; }
        /// <summary>
        /// 值班类别ID，用于获取该类别下面所有的日期，排班表使用
        /// </summary>
        public string OD_TYPEID { get; set; }
        /// <summary>
        /// 当前单位编码，查询时需查询该单位下面的所有单位
        /// </summary>
        public string curOrgNo { get; set; }
    }
}
