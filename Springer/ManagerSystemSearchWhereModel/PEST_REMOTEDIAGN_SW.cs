using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物_远程诊断表
    /// </summary>
    public class PEST_REMOTEDIAGN_SW
    {
        /// <summary>
        /// 远程诊断序号
        /// </summary>
        public string PEST_REMOTEDIAGNID { get; set; }
        /// <summary>
        /// 诊断主题
        /// </summary>
        public string DIAGNTITLE { get; set; }
        /// <summary>
        /// 诊断内容
        /// </summary>
        public string DIAGNCONTENT { get; set; }
        /// <summary>
        /// 诊断时间
        /// </summary>
        public string DIAGNTIME { get; set; }
        /// <summary>
        /// 诊断参与专家
        /// </summary>
        public string DIAGNEXPERTS { get; set; }
        /// <summary>
        /// 诊断状态
        /// </summary>
        public string DIAGNSTATUS { get; set; }
        /// <summary>
        /// 诊断结论
        /// </summary>
        public string DIAGNRESULT { get; set; }
        /// <summary>
        /// 发起人序号
        /// </summary>
        public string DIAGNSPONSERUID { get; set; }
        /// <summary>
        /// 发起时间
        /// </summary>
        public string DIAGNSPONSERTIME { get; set; }
        /// <summary>
        /// 诊断开始时间
        /// </summary>
        public string DIAGNSTARTTIME { get; set; }
        /// <summary>
        /// 诊断结束时间
        /// </summary>
        public string DIAGNENDTIME { get; set; }
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
