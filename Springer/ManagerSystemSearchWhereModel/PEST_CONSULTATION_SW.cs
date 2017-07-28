using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物专家会诊主题表
    /// </summary>
    public class PEST_CONSULTATION_SW
    {
        /// <summary>
        /// 主题序号
        /// </summary>
        public string PEST_CONSULTATIONID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string CONSULTITLE { get; set; }
        /// <summary>
        /// 提问人手机号码
        /// </summary>
        public string CONSULPHONE { get; set; }
        /// <summary>
        /// 提问时间
        /// </summary>
        public string CONSULTIME { get; set; }
        /// <summary>
        /// 提问内容
        /// </summary>
        public string CONSULCONTENT { get; set; }
        /// <summary>
        /// 提问开始时间
        /// </summary>
        public string CONSULSTARTTIME { get; set; }
        /// <summary>
        /// 结束结束时间
        /// </summary>
        public string CONSULENDTIME { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurPage { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }
    }
}
