using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 数据中心_瞭望塔
    /// </summary>
    public class DC_WATCHTOWER_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_WATCHTOWERID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 瞭望塔名称
        /// </summary>
        public string WATCHNAME { get; set; }
        /// <summary>
        /// 基本情况
        /// </summary>
        public string BASICS { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string LINKWAY { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string PHOTO { get; set; }
        /// <summary>
        /// 建设时间
        /// </summary>
        public string BUILDTIME { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public string BULIDAREA { get; set; }
        /// <summary>
        /// 使用情况
        /// </summary>
        public string USAGE { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
         /// <summary>
         /// 纬度
         /// </summary>
         public string WD { get; set; }
         /// <summary>
         /// 经度
         /// </summary>
         public string JD { get; set; }
        /// <summary>
        /// 最高组织机构编码
        /// </summary>
         public string TopORGNO { get; set; }

    }
}
