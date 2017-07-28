using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 值班班次
    /// </summary>
    public class DUTY_CLASS_SW
    {
        /// <summary>
        /// 组织机构编码
        /// </summary>
        public string BYORGNO { get; set; }

        /// <summary>
        /// 值班班次
        /// </summary>
        public string DUTYCLASSID { get; set; }

        /// <summary>
        /// 值班名称
        /// </summary>
        public string DUTYCLASSNAME { get; set; }

        /// <summary>
        /// 值班开始时间
        /// </summary>
        public string DUTYBEGINTIME { get; set; }

        /// <summary>
        /// 值班结束时间
        /// </summary>
        public string DUTYENDTIME { get; set; }

        /// <summary>
        /// 判断时间
        /// </summary>
        public string curTime { get; set; }
        /// <summary>
        /// 要判断的班次日期
        /// </summary>
        public string judgeDate { get; set; }
    }
}
