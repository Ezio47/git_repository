using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// T_SYS_ORG_EXTENDID_Model
    /// </summary>
    public class T_SYS_ORG_EXTENDID_Model
    {
        /// <summary>
        /// ID
        /// </summary>
        public string T_SYS_ORG_EXTEND { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string ORGNO { get; set; }
        /// <summary>
        /// 图层名称
        /// </summary>
        public string LAYERNAME { get; set; }
        /// <summary>
        /// 公益林图层名称
        /// </summary>
        public string GYLLAYERNAME { get; set; }
        /// <summary>
        /// 等高线图层名称
        /// </summary>
        public string DGXLAYERNAME { get; set; }
    }
}
