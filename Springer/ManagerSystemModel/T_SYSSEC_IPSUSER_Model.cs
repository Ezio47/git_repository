using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 系统用户Model
    /// </summary>
    public class T_SYSSEC_IPSUSER_Model
    {
        /// <summary>
        /// 用户名ID	
        /// </summary>
        public string USERID { get; set; }
        /// <summary>
        /// 登录名	
        /// </summary>
        public string LOGINUSERNAME { get; set; }
        /// <summary>
        /// 用户姓名	
        /// </summary>
        public string USERNAME { get; set; }
        /// <summary>
        /// 用户密码	
        /// </summary>
        public string USERPWD { get; set; }
        /// <summary>
        /// 注册时间	
        /// </summary>
        public string REGISTERTIME { get; set; }
        /// <summary>
        /// 登录次数	
        /// </summary>
        public string LOGINNUM { get; set; }
        /// <summary>
        /// 最后登录IP	
        /// </summary>
        public string LOGINIP { get; set; }
        /// <summary>
        /// 最后登录时间	
        /// </summary>
        public string LASTTIME { get; set; }
        /// <summary>
        /// 备注	
        /// </summary>
        public string NOTE { get; set; }
        /// <summary>
        /// 最后操作时间
        /// </summary>
        public string LASTOPTIME { get; set; }
        /// <summary>
        /// 护林员专用 管理用户ID	
        /// </summary>
        public string GID { get; set; }
        /// <summary>
        ///护林员专用 性别	
        /// </summary>
        public string SEX { get; set; }
        /// <summary>
        ///护林员专用 性别名称
        /// </summary>
        public string SEXNAME { get; set; }
        /// <summary>
        ///护林员专用 手机号码	
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        /// 护林员专用 用户职位	
        /// </summary>
        public string USERJOB { get; set; }

        /// <summary>
        ///  组织机构编码	
        /// </summary>
        public string ORGNO { get; set; }
        /// <summary>
        ///  组织机构名称	
        /// </summary>
        public string ORGNAME { get; set; }
        /// <summary>
        ///  区划编码	
        /// </summary>
        public string AREACODE { get; set; }
        /// <summary>
        ///  区划名称	
        /// </summary>
        public string AREANAME { get; set; }

        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 该用户拥有的角色列表
        /// </summary>
        public string ROLEIDList { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string DEPARTMENT { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string DEPARTMENTName { get; set; }
        /// <summary>
        /// 是否开通OA 1：开通；0：未开通
        /// </summary>
        public string IsOpenOA { get; set; }
    }

    /// <summary>
    /// 在线用户信息
    /// </summary>
    public class T_SYSSEC_IPSUSER_OnLine_Model
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
        /// 在线用户信息
        /// </summary>
        public IEnumerable<T_SYSSEC_IPSUSER_Pager_Model> LineInUserListModel { get; set; }
        /// <summary>
        /// 离线用户信息
        /// </summary>
        public IEnumerable<T_SYSSEC_IPSUSER_Pager_Model> LineOutUserListModel { get; set; }
    }

    /// <summary>
    /// 系统用户管理分页Model
    /// </summary>
    public class T_SYSSEC_IPSUSER_Pager_Model
    {
        /// <summary>
        /// 用户名ID	
        /// </summary>
        public string USERID { get; set; }
        /// <summary>
        /// 登录名	
        /// </summary>
        public string LOGINUSERNAME { get; set; }
        /// <summary>
        /// 用户姓名	
        /// </summary>
        public string USERNAME { get; set; }
        /// <summary>
        /// 护林员专用 管理用户ID	
        /// </summary>
        public string GID { get; set; }
        /// <summary>
        ///性别名称
        /// </summary>
        public string SEXNAME { get; set; }
        /// <summary>
        ///手机号码	
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        ///  用户职位	
        /// </summary>
        public string USERJOB { get; set; }
        /// <summary>
        ///  组织机构编码	
        /// </summary>
        public string ORGNO { get; set; }

        /// <summary>
        ///  单位名称	
        /// </summary>
        public string ORGNAME { get; set; }
        /// <summary>
        /// 该用户拥有的角色列表
        /// </summary>
        public string RoleNameList { get; set; }
        /// <summary>
        /// 最后操作时间
        /// </summary>
        public string LASTOPTIME { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string DEPARTMENT { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string DEPARTMENTName { get; set; }
        /// <summary>
        /// 是否开通OA 1：开通；0：未开通
        /// </summary>
        public string IsOpenOA { get; set; }
    }
}
