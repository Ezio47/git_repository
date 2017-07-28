using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物照片
    /// </summary>
    public class PEST_PHOTO_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_PHOTOID { get; set; }
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
    }
}
