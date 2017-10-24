using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 在线用户信息
    /// </summary>
    public class T_IPSFR_USER_OnLine_Model
    {
            /// <summary>
            /// 总人数
            /// </summary>
            public string LineCount { get; set; }
            /// <summary>
            /// 在线人数
            /// </summary>
            public string LineInCount { get; set; }
            /// <summary>
            /// 离线人数
            /// </summary>
            public string LineOutCount { get; set; }
            /// <summary>
            /// 出围人数
            /// </summary>
            public string LineOutRouteCount { get; set; }
            ///// <summary>
            ///// 在线用户信息
            ///// </summary>
            //public IEnumerable<T_SYSSEC_IPSUSER_Pager_Model> LineInUserListModel { get; set; }
            ///// <summary>
            ///// 离线用户信息
            ///// </summary>
            //public IEnumerable<T_SYSSEC_IPSUSER_Pager_Model> LineOutUserListModel { get; set; }
     
    }
    /// <summary>
    /// 护林员Model
    /// </summary>
   public class T_IPSFR_USER_Model
    {
       /// <summary>
        /// 护林员ID
       /// </summary>
        public string HID { get; set; }
       /// <summary>
        /// 护林员名
       /// </summary>
        public string HNAME { get; set; }
       /// <summary>
        /// 终端编号
       /// </summary>
        public string SN { get; set; }
       /// <summary>
        /// 手机号码
       /// </summary>
        public string PHONE { get; set; }
       /// <summary>
        /// 性别
       /// </summary>
        public string SEX { get; set; }
       /// <summary>
        /// 出生日期
       /// </summary>
        public string BIRTH { get; set; }
       /// <summary>
        /// 固\兼职
       /// </summary>
        public string ONSTATE { get; set; }
       /// <summary>
        /// 所属机构编码
       /// </summary>
        public string BYORGNO { get; set; }
       /// <summary>
        /// 需巡检距离
       /// </summary>

        public string ISENABLE { get; set; }
        /// <summary>
        ///性别名称
        /// </summary>
        public string SEXNAME { get; set; }
        /// <summary>
        ///固\兼职名称
        /// </summary>
        public string ONSTATENAME { get; set; }

       /// <summary>
       /// 最后上报时间
       /// </summary>
        public string SBTIME { get; set; }
        /// <summary>
        /// 巡检路线长度
        /// </summary>
        public string PATROLLENGTH { get; set; }

       /// <summary>
       /// 是否启用
       /// </summary>
        public string ISENABLENAME { get; set; }

       /// <summary>
       /// 是否在线 1在线 0离线
       /// </summary>
        public string isOnLine { get; set; }

        /// <summary>
        ///  组织机构名称	
        /// </summary>
        public string ORGNAME { get; set; }
       /// <summary>
       /// 组织机构县市名称
       /// </summary>
        public string ORGXSNAME { get; set; }
        /// <summary>
        /// 组织机构乡镇名称
        /// </summary>
        public string ORGXZNAME { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 护林员参数
        /// </summary>
        public string MOBILEPARAMLIST { get; set; }
    }
    /// <summary>
    /// 获取护林员分页Model
    /// </summary>
   public class T_IPSFR_USER_Pager_Model
   {
       /// <summary>
       /// 护林员ID
       /// </summary>
       public string HID { get; set; }
       /// <summary>
       /// 护林员名
       /// </summary>
       public string HNAME { get; set; }
       /// <summary>
       /// 终端编号
       /// </summary>
       public string SN { get; set; }
       /// <summary>
       /// 手机号码
       /// </summary>
       public string PHONE { get; set; }
       /// <summary>
       /// 出生日期
       /// </summary>
       public string BIRTH { get; set; }
       /// <summary>
       /// 组织机构编码
       /// </summary>
       public string BYORGNO { get; set; }
       /// <summary>
       ///性别名称
       /// </summary>
       public string SEXNAME { get; set; }
       /// <summary>
       ///固\兼职名称
       /// </summary>
       public string ONSTATENAME { get; set; }
       /// <summary>
       /// 是否启用
       /// </summary>
       public string ISENABLE { get; set; }
       /// <summary>
       /// 是否启用
       /// </summary>
       public string ISENABLENAME { get; set; }
       /// <summary>
       ///  组织机构名称	
       /// </summary>
       public string ORGNAME { get; set; }
       /// <summary>
       /// 是否存在线路
       /// </summary>
       public string isExitsLine { get; set; }
       /// <summary>
       /// 是否存在围栏
       /// </summary>
       public string isExitsRail { get; set; }
       /// <summary>
       /// 护林员参数
       /// </summary>
       public string MOBILEPARAMLIST { get; set; }
       /// <summary>
       /// 巡检距离
       /// </summary>
       public string PATROLLENGTH { get; set; }
   }
}
