using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 预警_卫星云图表
    /// </summary>
   public class YJ_SATELLITECLOUD_Model
    {
       /// <summary>
       /// 云图编号
       /// </summary>
        public string CLOUDID { get; set; }
       /// <summary>
       /// 云图时间
       /// </summary>
        public string CLOUDTIME { get; set; }
       /// <summary>
       /// 云图标题
       /// </summary>
        public string CLOUDNAME { get; set; }
       /// <summary>
       /// 云图文件名（压缩）
       /// </summary>
        public string CLOUDFILENAME { get; set; }

       /// <summary>
        /// 云图文件名（原始）
       /// </summary>
        public string CLOUDORIGIONNAME { get; set; }
    }
}