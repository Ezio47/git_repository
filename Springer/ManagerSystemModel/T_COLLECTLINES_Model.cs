using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 线图层数据采集Model
    /// </summary>
    public class T_COLLECTLINES_Model
    {
        /// <summary>
        /// 线名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 空间数据格式
        /// </summary>
        public string Shape { get; set; }
        /// <summary>
        /// 0表示建筑物；1表示消防设施；2表示可燃物；
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string opMethod { get; set; }
    }
}
