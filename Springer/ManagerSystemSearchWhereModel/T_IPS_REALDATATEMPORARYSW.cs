using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 护林员上报
    /// </summary>
    public class T_IPS_REALDATATEMPORARYSW
    {
        /// <summary>
        /// 用户userid
        /// </summary>
        public string USERID { get; set; }
        /// <summary>
        /// 组织机构码
        /// </summary>
        public string ORGNO { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string DateBegin { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string DateEnd { get; set; }
        /// <summary>
        /// 查询时间 用于点名
        /// </summary>
        public string SearchTime { get; set; }
        /// <summary>
        /// 护林员和电话号码
        /// </summary>
        public string PhoneHname { get; set; }

        /// <summary>
        /// 地图类型
        /// </summary>
        public string MapType { get; set; }
    }
}
