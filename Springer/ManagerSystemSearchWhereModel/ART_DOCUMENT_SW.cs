using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 文档内容表条件
    /// </summary>
    public class ART_DOCUMENT_SW
    {

        /// <summary>
        /// 文档序号
        /// </summary>
        public string ARTID { get; set; }
        /// <summary>
        /// 类别序号
        /// </summary>
        public string ARTTYPEID { get; set; }
        /// <summary>
        /// 大类别序号
        /// </summary>
        public string ARTBigTYPEID { get; set; }
        /// <summary>
        /// 所属机构
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 文档标题
        /// </summary>
        public string ARTTITLE { get; set; }
        /// <summary>
        /// 文档标签
        /// </summary>
        public string ARTTAG { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public string ARTCHECKSTATUS { get; set; }

        /// <summary>
        /// 选出记录数据
        /// </summary>
        public string TOPS { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string TIMEBegin { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string TIMEEnd { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }

   
    }
}
