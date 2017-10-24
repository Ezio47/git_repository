using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel.SDEModel
{
    /// <summary>
    /// 三维--病虫害
    /// </summary>
    public class BINGCHONGHAI_Model
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string OBJECTID_1 { get; set; }
        /// <summary>
        /// 空间库ID 数据关联用
        /// </summary>
        public string OBJECTID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string CATEGORY { get; set; }
        /// <summary>
        /// 中心X坐标
        /// </summary>
        public string DISPLAY_Y { get; set; }
        /// <summary>
        /// 中心Y坐标
        /// </summary>
        public string DISPLAY_X { get; set; }
        /// <summary>
        /// 空间数据shape字段
        /// </summary>
        public string Shape { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string opMethod { get; set; }
    }
}
