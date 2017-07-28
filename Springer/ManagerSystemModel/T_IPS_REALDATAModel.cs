using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 实时数据Model
    /// </summary>
    public class T_IPS_REALDATAModel
    {

        /// <summary>
        /// 主键
        /// </summary>
        public string REALDATAID { get; set; }


        /// <summary>
        /// 经度
        /// </summary>
        public string LONGITUDE { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string LATITUDE { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public string HEIGHT { get; set; }

        /// <summary>
        /// 电量
        /// </summary>
        public string ELECTRIC { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        public string SPEED { get; set; }

        /// <summary>
        /// 方位
        /// </summary>
        public string DIRECTION { get; set; }

        /// <summary>
        /// 最新上报时间
        /// </summary>
        public string SBTIME { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NOTE { get; set; }
        /// <summary>
        /// 护林员ID
        /// </summary>
        public string HID { get; set; }
        /// <summary>
        /// 护林员
        /// </summary>
        public string HNAME { get; set; }
        /// <summary>
        /// 护林员机构
        /// </summary>
        public string ORGNAME { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string ORGNO { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PHONE { get; set; }
    }
}
