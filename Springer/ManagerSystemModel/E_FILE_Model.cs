using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 附件
    /// </summary>
    public class E_FILE_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string EFID { get; set; }
        /// <summary>
        /// 所属邮件序号
        /// </summary>
        public string BYEMAILID { get; set; }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string EMAILFILETITLE { get; set; }
        /// <summary>
        /// 附件大小
        /// </summary>
        public string EMAILFILESIZE { get; set; }
        /// <summary>
        /// 保存文件名
        /// </summary>
        public string EMAILFILENAME { get; set; }
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
