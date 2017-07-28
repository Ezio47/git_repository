using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 交班记录表
    /// </summary>
    public class DUTY_HANDOVER_SW
    {
        /// <summary>
        /// 值班交班序号
        /// </summary>
        public string DHID { get; set; }
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
        /// 值班类别
        /// </summary>
        public string DUTYTYPE { get; set; }
        /// <summary>
        /// 值班人序号
        /// </summary>
        public string DUTYUSERID { get; set; }
        /// <summary>
        /// 获取上一班次信息 1为是 其他为否 比如根据当前班次获取上一班次交班内容
        /// </summary>
        public string isGetUPOne { get; set; }
    }
}
