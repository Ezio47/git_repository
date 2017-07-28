using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 
    /// </summary>
    public class ART_DOCUMENT_Model
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
        /// 添加时间
        /// </summary>
        public string ARTTIME { get; set; }
        /// <summary>
        /// 文档内容
        /// </summary>
        public string ARTCONTENT { get; set; }

        /// <summary>
        /// 添加人序号
        /// </summary>
        public string ARTADDUSERID { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public string ARTCHECKSTATUS { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string ARTCHECKUSERID { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public string ARTCHECKTIME { get; set; }
        /// <summary>
        /// 附件地址
        /// </summary>
        public string PLANFILENAME { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string ARTTYPENAME { get; set; }

        /// <summary>
        /// 添加人名称
        /// </summary>
        public string ARTADDUSERName { get; set; }
        /// <summary>
        /// 审核状态名称
        /// </summary>
        public string ARTCHECKSTATUSName { get; set; }
        /// <summary>
        /// 审核人名称
        /// </summary>
        public string ARTCHECKUSERIDName { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string orgName { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }

    }
}
