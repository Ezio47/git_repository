using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 附件
    /// </summary>
    public class E_File_SW
    {
        /// <summary>
        /// 附件序号
        /// </summary>
        public string EFID { get; set; }
        /// <summary>
        /// 所属邮件序号
        /// </summary>
        public string BYEMAILID { get; set; }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string EMAILFILETITLE { get; set;}
        /// <summary>
        /// 附件大小
        /// </summary>
        public string EMAILFILESIZE { get; set;}
        /// <summary>
        /// 保存文件名
        /// </summary>
        public string EMAILFILENAME { get; set;}
    }
}
