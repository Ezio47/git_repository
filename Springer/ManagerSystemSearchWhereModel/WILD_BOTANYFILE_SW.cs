using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 野生植物附件表
    /// </summary>
   public  class WILD_BOTANYFILE_SW
    {
        /// <summary>
        /// 附件序号
        /// </summary>
        public string PESTFILEID { get; set; }
        /// <summary>
        /// 生物物种编码
        /// </summary>
        public string BIOLOGICALTYPECODE { get; set; }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string PESTFILETITLE { get; set; }
        /// <summary>
        /// 附件类别
        /// </summary>
        public string PESTFILETYPE { get; set; }
        /// <summary>
        /// 附件文件名
        /// </summary>
        public string PESTFILENAME { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public string UPLOADTIME { get; set; }
        /// <summary>
        /// 所属用户ID
        /// </summary>
        public string UID { get; set; }
    }
}
