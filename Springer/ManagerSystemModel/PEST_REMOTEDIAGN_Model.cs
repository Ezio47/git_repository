using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物_远程诊断表
    /// </summary>
    public class PEST_REMOTEDIAGN_Model
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
        /// 诊断状态名称
        /// </summary>
        public string DIAGNSTATUSName { get; set; }
        /// <summary>
        /// 诊断结论
        /// </summary>
        public string DIAGNRESULT { get; set; }
        /// <summary>
        /// 发起人序号
        /// </summary>
        public string DIAGNSPONSERUID { get; set; }
        /// <summary>
        /// 发起人名称
        /// </summary>
        public string DIAGNSPONSERNAME { get; set; }
        /// <summary>
        /// 发起时间
        /// </summary>
        public string DIAGNSPONSERTIME { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
    }
}
