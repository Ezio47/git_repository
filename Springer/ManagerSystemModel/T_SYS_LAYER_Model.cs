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
    public class T_SYS_LAYER_Model
    {
        /// <summary>
        /// 图层编码
        /// </summary>
        public string LAYERCODE { get; set; }
        /// <summary>
        /// 图层名称
        /// </summary>
        public string LAYERNAME { get; set; }
        /// <summary>
        /// 图层编号
        /// </summary>
        public string LAYERID { get; set; }
        /// <summary>
        /// 是否需要权限，0不需要，1需要
        /// </summary>
        public string ISACTION { get; set; }
        /// <summary>
        /// 图层权限编号
        /// </summary>
        public string LAYERRIGHTID { get; set; }
        /// <summary>
        /// 图层排序
        /// </summary>
        public string ORDERBY { get; set; }
        /// <summary>
        /// 火点周边查询默认图层
        /// </summary>
        public string ISFIREROUNDDEFAULT { get; set; }
        /// <summary>
        /// 护林员周边查询默认图层
        /// </summary>
        public string ISFUROUNDDEFAULT { get; set; }
    }
}
