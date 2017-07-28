using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
   /// <summary>
   /// 热点图片Model
   /// </summary>
    public class T_RDXXZLModel
    {
        /// <summary>
        /// 图片格式
        /// </summary>
        public string LX { get; set; }
        /// <summary>
        /// 热点发生时间
        /// </summary>

        public DateTime CRSJ { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public byte[] FJ { get; set; }
        /// <summary>
        /// 文件编号
        /// </summary>
        public string FJBH { get; set; }
    }
}
