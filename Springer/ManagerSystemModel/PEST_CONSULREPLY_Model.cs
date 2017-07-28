using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物_专家会诊回复表
    /// </summary>
    public class PEST_CONSULREPLY_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_CONSULREPLYID { get; set; }
        /// <summary>
        /// 主题序号
        /// </summary>
        public string PEST_CONSULTATIONID { get; set; }
        /// <summary>
        /// 回复人序号
        /// </summary>
        public string REPLYUID { get; set; }
        /// <summary>
        /// 回复人序号
        /// </summary>
        public string REPLYUSERANME { get; set; }
        /// <summary>
        /// 回复时间
        /// </summary>
        public string REPLYTIME { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string REPLYCONTENT { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
    }
}
