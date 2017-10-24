using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel.SDEModel
{
    /// <summary>
    /// 三维-野生动物区域分布
    /// </summary>
   public class WILD_ANIMALDISTRIBUTEArea_Model
    {
        /// <summary>
        /// OBJECTID
        /// </summary>
        public string OBJECTID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string TYPE { get; set; }
        /// <summary>
        /// 空间数据shape字段
        /// </summary>
        public string Shape { get; set; }
        /// <summary>
        /// 空间数据线的长度
        /// </summary>
        public string Shape_Leng { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 中心点经度
        /// </summary>
        public string CENTRE_X { get; set; }
        /// <summary>
        /// 中心点纬度
        /// </summary>
        public string CENTRE_Y { get; set; }
    }
}
