using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 临时实时数据Model
    /// </summary>
    public class T_IPS_REALDATATEMPORARYModel
    {


        /// <summary>
        /// 用户userid
        /// </summary>
        public string USERID { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string LONGITUDE { get; set; }

        /// <summary>
        /// 原经度
        /// </summary>
        public string ORILONGITUDE { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string LATITUDE { get; set; }

        /// <summary>
        /// 原纬度
        /// </summary>
        public string ORILATITUDE { get; set; }

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
        /// 备注
        /// </summary>
        public string NOTE { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string ORGNO { get; set; }
        /// <summary>
        /// 上报日期
        /// </summary>
        public string SBDATE { get; set; }
        /// <summary>
        /// 本日开始上报时间
        /// </summary>
        public string SBTIMEBEGIN { get; set; }
        /// <summary>
        /// 巡检总长度
        /// </summary>
        public string PATROLLENGTH { get; set; }



        /// <summary>
        /// 护林员
        /// </summary>
        public string HNAME { get; set; }

        /// <summary>
        /// 护林员机构
        /// </summary>
        public string ORGNAME { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string PHONE { get; set; }

        /// <summary>
        /// 0 表离线 1 表在线
        /// </summary>
        public string HSTATE { get; set; }

        /// <summary>
        /// 出围 0 表未出围 1 表示出围
        /// </summary>
        public string ISOUTRAIL { get; set; }

    }
}
