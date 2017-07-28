using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{

    /// <summary>
    /// 数据中心_照片
    /// </summary>
    public class DC_PHOTO_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PHOTO_ID { get; set; }
        /// <summary>
        /// 照片标题
        /// </summary>
        public string PHOTOTITLE { get; set; }
        /// <summary>
        /// 照片文件名称
        /// </summary>
        public string PHOTOFILENAME { get; set; }
        /// <summary>
        /// 照片说明
        /// </summary>
        public string PHOTOEXPLAIN { get; set; }
        /// <summary>
        /// 照片时间
        /// </summary>
        public string PHOTOTIME { get; set; }
        /// <summary>
        /// 照片类别
        /// </summary>
        public string PHOTOTYPE { get; set; }
        /// <summary>
        /// 所属序号
        /// </summary>
        public string PRID { get; set; }
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
