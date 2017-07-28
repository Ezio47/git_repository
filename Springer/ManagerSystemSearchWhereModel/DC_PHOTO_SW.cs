using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{

    /// <summary>
    /// 数据中心_照片
    /// </summary>
    public class DC_PHOTO_SW
    {

        /// <summary>
        /// 序号
        /// </summary>
        public string PHOTO_ID { get; set; }
        /// <summary>
        /// 照片类别
        /// </summary>
        public string PHOTOTYPE { get; set; }
        /// <summary>
        /// 所属序号
        /// </summary>
        public string PRID { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
    }
}
