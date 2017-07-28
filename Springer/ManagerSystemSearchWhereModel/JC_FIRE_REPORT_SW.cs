using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 火情报告上传模型
    /// </summary>
    public class JC_FIRE_REPORT_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 所属火情id
        /// </summary>
        public string OWERJCFID { get; set; }
        /// <summary>
        /// 报告名称
        /// </summary>
        public string FILENAME { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string FILESIZE { get; set; }
        /// <summary>
        /// 文件相对路径
        /// </summary>
        public string FILEURL { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public string UPLOADTIME { get; set; }
        /// <summary>
        /// 上传人
        /// </summary>
        public string UPLOADUSERID { get; set; }
        /// <summary>
        /// 上传单位
        /// </summary>
        public string UPLOADORGNO { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string opMethod { get; set; }
    }
}
