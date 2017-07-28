using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物_专家会诊回复表
    /// </summary>
    public class PEST_CONSULREPLY_SW
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
        /// 回复时间
        /// </summary>
        public string REPLYTIME { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string REPLYCONTENT { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurPage { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }
    }
}
