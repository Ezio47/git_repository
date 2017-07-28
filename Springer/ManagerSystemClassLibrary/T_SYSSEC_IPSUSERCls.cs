using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OAModel;
using Teleware.Utility;
using Teleware.Security;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 系统用户管理类
    /// </summary>
    public class T_SYSSEC_IPSUSERCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Manager(T_SYSSEC_IPSUSER_Model m)
        {
            if (m.opMethod == "Add")
            {
                SystemCls.LogSave("3", "系统用户:" + m.LOGINUSERNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_SYSSEC_USER.Add(m);
                if (msgUser.Success == false)
                    return new Message(msgUser.Success, msgUser.Msg, "");

                //获取新添加的UserID
                DataTable dt = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { LOGINUSERNAME = m.LOGINUSERNAME });
                string UserID = "";
                if (dt.Rows.Count > 0)
                {
                    UserID = dt.Rows[0]["USERID"].ToString();
                }
                dt.Clear();
                dt.Dispose();
                //判断扩展表中是否存在记录
                if (string.IsNullOrEmpty(UserID) == true)
                    return new Message(false, "系统用户不存在", ""); ;//系统用户不存在
                m.USERID = UserID;
                Message msg = new Message(false, "", "");
                //判断扩展表中是否存在，如果存在，则修改，否则添加
                if (BaseDT.T_SYSSEC_IPSUSER.isExists(new T_SYSSEC_IPSUSER_SW { USERID = UserID }))
                {
                    msg = BaseDT.T_SYSSEC_IPSUSER.Mdy(m);
                }
                else
                {
                    msg = BaseDT.T_SYSSEC_IPSUSER.Add(m);
                }
                if (msg.Success == false)
                    return new Message(msg.Success, "系统用户添加成功，但用户扩展信息" + msg.Msg, "");// "系统用户添加成功，但扩展信息保存失败，请修改该用户扩展信息", "");

                //保存角色
                BaseDT.T_SYSSEC_USER_ROLE.Save(new T_SYSSEC_USER_ROLE_Model { ROLEID = m.ROLEIDList, USERID = UserID });

                return new Message(true, "添加成功", m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                SystemCls.LogSave("4", "系统用户:" + m.LOGINUSERNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_SYSSEC_USER.Mdy(m);
                if (msgUser.Success == false)
                    return new Message(msgUser.Success, msgUser.Msg, "");
                Message msg = new Message(false, "", "");
                //判断扩展表中是否存在，如果存在，则修改，否则添加
                if (BaseDT.T_SYSSEC_IPSUSER.isExists(new T_SYSSEC_IPSUSER_SW { USERID = m.USERID }))
                {
                    msg = BaseDT.T_SYSSEC_IPSUSER.Mdy(m);
                }
                else
                {
                    msg = BaseDT.T_SYSSEC_IPSUSER.Add(m);
                }
                if (msg.Success == false)
                    return new Message(msg.Success, "系统用户修改成功，但用户扩展信息" + msg.Msg, "");// "系统用户添加成功，但扩展信息保存失败，请修改该用户扩展信息", "");
                //保存角色
                BaseDT.T_SYSSEC_USER_ROLE.Save(new T_SYSSEC_USER_ROLE_Model { ROLEID = m.ROLEIDList, USERID = m.USERID });

                return new Message(true, "修改成功", m.returnUrl);
            }
            if (m.opMethod == "MdyLastOpTime")
            {
                return BaseDT.T_SYSSEC_IPSUSER.MdyLastOpTime(m);
            }
            if (m.opMethod == "Del")
            {
                SystemCls.LogSave("5", "系统用户:" + m.LOGINUSERNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_SYSSEC_USER.Del(m);
                if (msgUser.Success == false)
                    return new Message(msgUser.Success, msgUser.Msg, "");
                Message msg = BaseDT.T_SYSSEC_IPSUSER.Del(m);
                if (msg.Success == false)
                    return new Message(msg.Success, "系统用户删除成功，但用户扩展信息" + msg.Msg, "");// "系统用户添加成功，但扩展信息保存失败，请修改该用户扩展信息", "");

                //保存角色
                BaseDT.T_SYSSEC_USER_ROLE.Save(new T_SYSSEC_USER_ROLE_Model { ROLEID = "", USERID = m.USERID });

                return new Message(true, "删除成功", m.returnUrl);
            }
            return new Message(false, "无效操作", "");
        }

        #endregion

        #region 根据用户ID获取用户组织编号

        /// <summary>
        /// 根据用户ID获取用户组织编号
        /// </summary>
        /// <param name="UID">用户序号</param>
        /// <returns>参见模型</returns>
        public static string getOrgNoByUID(string UID)
        {
            T_SYSSEC_IPSUSER_Model m = getModel(new T_SYSSEC_IPSUSER_SW { USERID = UID });
            return m.ORGNO;
        }
        #endregion

        #region 根据查询条件获取某一条用户信息记录
        /// <summary>
        /// 根据查询条件获取某一条用户信息记录，用于修改、删除、用户登录验证
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_SYSSEC_IPSUSER_Model getModel(T_SYSSEC_IPSUSER_SW sw)
        {
            DataTable dt = BaseDT.T_SYSSEC_IPSUSER.getDT(sw);
            DataTable dt46 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "46" });//科室
            T_SYSSEC_IPSUSER_Model m = new T_SYSSEC_IPSUSER_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.GID = dt.Rows[i]["GID"].ToString();
                m.USERID = dt.Rows[i]["USERID"].ToString();
                m.SEX = dt.Rows[i]["SEX"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.USERJOB = dt.Rows[i]["USERJOB"].ToString();
                m.ORGNO = dt.Rows[i]["ORGNO"].ToString();
                m.LOGINUSERNAME = dt.Rows[i]["LOGINUSERNAME"].ToString();
                m.USERNAME = dt.Rows[i]["USERNAME"].ToString();
                m.USERPWD = dt.Rows[i]["USERPWD"].ToString();
                m.REGISTERTIME = dt.Rows[i]["REGISTERTIME"].ToString();
                m.LOGINNUM = dt.Rows[i]["LOGINNUM"].ToString();
                m.LOGINIP = dt.Rows[i]["LOGINIP"].ToString();
                m.LASTTIME = dt.Rows[i]["LOGINNUM"].ToString();
                m.NOTE = dt.Rows[i]["NOTE"].ToString();
                m.DEPARTMENT = dt.Rows[i]["DEPARTMENT"].ToString();
                m.DEPARTMENTName = BaseDT.T_SYS_DICT.getName(dt46, dt.Rows[i]["DEPARTMENT"].ToString());
                //扩充字段
            }
            return m;
        }
        #endregion

        #region 获取用户列表（分页）
        /// <summary>
        /// 获取用户列表（分页）
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYSSEC_IPSUSER_Pager_Model> getUserModel(T_SYSSEC_IPSUSER_SW sw, out int total)
        {
            var result = new List<T_SYSSEC_IPSUSER_Pager_Model>();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtSex = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "性别" });//性别
            DataTable dt = BaseDT.T_SYSSEC_IPSUSER.getDT(sw, out total);//用户列表
            DataTable dt46 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "46" });//科室
            string uidList = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(uidList))
                    uidList += dt.Rows[i]["USERID"].ToString();
                else
                    uidList += "," + dt.Rows[i]["USERID"].ToString();
            }
            //获取角色信息
            DataTable dtRole = BaseDT.T_SYSSEC_ROLE.getDT(new T_SYSSEC_ROLE_SW { SYSFLAG = ConfigCls.getSystemFlag() });
            //获取所有用户的角色信息
            DataTable dtUserRole = BaseDT.T_SYSSEC_USER_ROLE.getDT(new T_SYSSEC_USER_ROLE_SW { USERID = uidList });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYSSEC_IPSUSER_Pager_Model m = new T_SYSSEC_IPSUSER_Pager_Model();
                m.USERID = dt.Rows[i]["USERID"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["ORGNO"].ToString());//单位名称
                m.ORGNO = dt.Rows[i]["ORGNO"].ToString();
                m.USERNAME = dt.Rows[i]["USERNAME"].ToString();//用户姓名
                m.USERJOB = dt.Rows[i]["USERJOB"].ToString();//职务
                m.LOGINUSERNAME = dt.Rows[i]["LOGINUSERNAME"].ToString();//登录名
                m.GID = dt.Rows[i]["GID"].ToString();//用户扩展护林员ID
                m.SEXNAME = BaseDT.T_SYS_DICT.getName(dtSex, dt.Rows[i]["SEX"].ToString());//性别名称
                m.PHONE = dt.Rows[i]["PHONE"].ToString();//电话
                m.DEPARTMENT = dt.Rows[i]["DEPARTMENT"].ToString();
                m.DEPARTMENTName = BaseDT.T_SYS_DICT.getName(dt46, dt.Rows[i]["DEPARTMENT"].ToString());
                string roleNameList = "";
                DataRow[] drUR = dtUserRole.Select("USERID=" + dt.Rows[i]["USERID"].ToString());
                for (int k = 0; k < drUR.Length; k++)
                {
                    if (string.IsNullOrEmpty(roleNameList) == false)
                        roleNameList += ",";
                    roleNameList += BaseDT.T_SYSSEC_ROLE.getName(dtRole, drUR[k]["ROLEID"].ToString());
                }
                m.RoleNameList = roleNameList;//注意此处用ROLEIDList代替角色中文名
                result.Add(m);
            }
            dtORG.Clear();
            dtORG.Dispose();
            dtSex.Clear();
            dtSex.Dispose();
            dt.Clear();
            dt.Dispose();
            dtRole.Clear();
            dtRole.Dispose();
            dtUserRole.Clear();
            dtUserRole.Dispose();
            return result;
        }

        /// <summary>
        /// 获取用户列表（分页）
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYSSEC_IPSUSER_Pager_Model> getOAUserModel(T_SYSSEC_IPSUSER_SW sw, out int total)
        {
            var result = new List<T_SYSSEC_IPSUSER_Pager_Model>();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtSex = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "性别" });//性别
            DataTable dt = BaseDT.T_SYSSEC_IPSUSER.getDT2(sw, out total);//用户列表
            DataTable dt46 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "46" });//科室
            string uidList = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(uidList))
                    uidList += dt.Rows[i]["USERID"].ToString();
                else
                    uidList += "," + dt.Rows[i]["USERID"].ToString();
            }
            //获取角色信息
            DataTable dtRole = BaseDT.T_SYSSEC_ROLE.getDT(new T_SYSSEC_ROLE_SW { SYSFLAG = ConfigCls.getSystemFlag() });
            //获取所有用户的角色信息
            DataTable dtUserRole = BaseDT.T_SYSSEC_USER_ROLE.getDT(new T_SYSSEC_USER_ROLE_SW { USERID = uidList });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYSSEC_IPSUSER_Pager_Model m = new T_SYSSEC_IPSUSER_Pager_Model();
                m.USERID = dt.Rows[i]["USERID"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["ORGNO"].ToString());//单位名称
                m.ORGNO = dt.Rows[i]["ORGNO"].ToString();
                m.USERNAME = dt.Rows[i]["USERNAME"].ToString();//用户姓名
                m.USERJOB = dt.Rows[i]["USERJOB"].ToString();//职务
                m.LOGINUSERNAME = dt.Rows[i]["LOGINUSERNAME"].ToString();//登录名
                m.GID = dt.Rows[i]["GID"].ToString();//用户扩展护林员ID
                m.SEXNAME = BaseDT.T_SYS_DICT.getName(dtSex, dt.Rows[i]["SEX"].ToString());//性别名称
                m.PHONE = dt.Rows[i]["PHONE"].ToString();//电话
                m.DEPARTMENT = dt.Rows[i]["DEPARTMENT"].ToString();
                m.DEPARTMENTName = BaseDT.T_SYS_DICT.getName(dt46, dt.Rows[i]["DEPARTMENT"].ToString());
                m.IsOpenOA = dt.Rows[i]["IsOpenOA"].ToString() == "" ? "0" : dt.Rows[i]["IsOpenOA"].ToString();
                string roleNameList = "";
                DataRow[] drUR = dtUserRole.Select("USERID=" + dt.Rows[i]["USERID"].ToString());
                for (int k = 0; k < drUR.Length; k++)
                {
                    if (string.IsNullOrEmpty(roleNameList) == false)
                        roleNameList += ",";
                    roleNameList += BaseDT.T_SYSSEC_ROLE.getName(dtRole, drUR[k]["ROLEID"].ToString());
                }
                m.RoleNameList = roleNameList;//注意此处用ROLEIDList代替角色中文名
                result.Add(m);
            }
            dtORG.Clear();
            dtORG.Dispose();
            dtSex.Clear();
            dtSex.Dispose();
            dt.Clear();
            dt.Dispose();
            dtRole.Clear();
            dtRole.Dispose();
            dtUserRole.Clear();
            dtUserRole.Dispose();
            return result;
        }
        #endregion

        #region 在线用户信息
        /// <summary>
        /// 在线用户信息
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_SYSSEC_IPSUSER_OnLine_Model getUserLineModel(T_SYSSEC_IPSUSER_SW sw)
        {
            var list = getListModel(sw);//获取所有用户列表
            var resultLineIn = new List<T_SYSSEC_IPSUSER_Pager_Model>();//在线人数信息
            var resultLineOut = new List<T_SYSSEC_IPSUSER_Pager_Model>();//离线人员信息
            int LineC = 0;//总人数
            int LineInC = 0;//在线人数
            int LineOutC = 0;//离线人数
            int mDiff = Int32.Parse(ConfigCls.getIsSaveLastOpTime());//时间间隔

            T_SYSSEC_IPSUSER_OnLine_Model Model = new T_SYSSEC_IPSUSER_OnLine_Model();
            if (mDiff > 0)//=0时，不需要判断在线状态
            {
                foreach (var v in list)
                {
                    LineC++;

                    T_SYSSEC_IPSUSER_Pager_Model m = new T_SYSSEC_IPSUSER_Pager_Model();
                    m.USERID = v.USERID;
                    m.ORGNO = v.ORGNO;//单位编码
                    m.ORGNAME = v.ORGNAME;//单位名称
                    m.USERNAME = v.USERNAME;//用户姓名
                    m.USERJOB = v.USERJOB;//职务
                    m.LOGINUSERNAME = v.LOGINUSERNAME;//登录名
                    m.GID = v.GID;//用户扩展护林员ID
                    m.SEXNAME = v.SEXNAME;//性别名称
                    m.PHONE = v.PHONE;//电话
                    m.LASTOPTIME = v.LASTOPTIME;
                    if (string.IsNullOrEmpty(m.LASTOPTIME))//离线
                    {
                        LineOutC++;
                        resultLineOut.Add(m);
                    }
                    else//判断是否小于时间间隔
                    {
                        if (PublicClassLibrary.ClsStr.getMinutesDiff(m.LASTOPTIME, DateTime.Now) <= mDiff)//在线
                        {
                            LineInC++;
                            resultLineIn.Add(m);
                        }
                        else//离线
                        {
                            LineOutC++;
                            resultLineOut.Add(m);

                        }

                    }

                }
            }
            Model.LineCount = LineC.ToString();//总人数
            Model.LineInCount = LineInC.ToString();//在线人数
            Model.LineOutCount = LineOutC.ToString();//离线人数
            Model.LineInUserListModel = resultLineIn;//在线人数信息
            Model.LineOutUserListModel = resultLineOut;//离线人员信息
            return Model;
        }
        #endregion

        #region 获取用户列表
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYSSEC_IPSUSER_Pager_Model> getListModel(T_SYSSEC_IPSUSER_SW sw)
        {
            var result = new List<T_SYSSEC_IPSUSER_Pager_Model>();

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtSex = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "性别" });//性别
            DataTable dt46 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "46" });//科室
            DataTable dt = BaseDT.T_SYSSEC_IPSUSER.getDT(sw);//用户列表

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYSSEC_IPSUSER_Pager_Model m = new T_SYSSEC_IPSUSER_Pager_Model();
                m.USERID = dt.Rows[i]["USERID"].ToString();
                m.ORGNO = dt.Rows[i]["ORGNO"].ToString();//单位编码
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["ORGNO"].ToString());//单位名称
                m.USERNAME = dt.Rows[i]["USERNAME"].ToString();//用户姓名
                m.USERJOB = dt.Rows[i]["USERJOB"].ToString();//职务
                m.LOGINUSERNAME = dt.Rows[i]["LOGINUSERNAME"].ToString();//登录名
                m.GID = dt.Rows[i]["GID"].ToString();//用户扩展护林员ID
                m.SEXNAME = BaseDT.T_SYS_DICT.getName(dtSex, dt.Rows[i]["SEX"].ToString());//性别名称
                m.PHONE = dt.Rows[i]["PHONE"].ToString();//电话
                m.LASTOPTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["LASTOPTIME"].ToString());
                m.DEPARTMENT = dt.Rows[i]["DEPARTMENT"].ToString();
                m.DEPARTMENTName = BaseDT.T_SYS_DICT.getName(dt46, dt.Rows[i]["DEPARTMENT"].ToString());
                result.Add(m);
            }
            dtORG.Clear();
            dtORG.Dispose();
            dtSex.Clear();
            dtSex.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 获取用户组合字符串
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>如张三(红河州)</returns>
        /// <example>
        /// sw.USERID 多用户以ID+英文逗号 单用户用用户序号
        /// sw.curOrgNo 非空查询该组织机构用户，空取所有单位用户
        /// sw.formatUserStr 格式化组合用户信息字符串 将字段信息替换 [userName] 中文名 [orgName]
        /// sw.splitUserStr 多用户之间分隔符,默认为英文逗号
        /// </example>
        public static string getUserCombString(T_SYSSEC_IPSUSER_SW sw)
        {
            var result = new List<T_SYSSEC_IPSUSER_Pager_Model>();

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            //DataTable dtSex = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "性别" });//性别
            DataTable dt = BaseDT.T_SYSSEC_IPSUSER.getDT(sw);//用户列表
            string str = "";
            if (string.IsNullOrEmpty(sw.splitUserStr))
                sw.splitUserStr = ",";
            if (string.IsNullOrEmpty(sw.formatUserStr))
                sw.formatUserStr = "[userName]<[orgName]>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                    str += sw.splitUserStr;
                str += sw.formatUserStr
                    .Replace("[userName]", dt.Rows[i]["USERNAME"].ToString())
                    .Replace("[orgName]", BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["ORGNO"].ToString()));
            }
            dtORG.Clear();
            dtORG.Dispose();
            //dtSex.Clear();
            //dtSex.Dispose();
            dt.Clear();
            dt.Dispose();
            return str;
        }
        #endregion

        #region 获取系统用户树形菜单
        /// <summary>
        /// 返回系统用户树形菜单Json 
        /// </summary>
        /// <param name="sw">单位编码为空，默认为当前登录用户所有单位编码，其他条件无</param>
        /// <returns>Json</returns>
        public static string getSystemUserTree(T_SYSSEC_IPSUSER_SW sw)
        {
            //if (string.IsNullOrEmpty(sw.curOrgNo))
            //    sw.curOrgNo = SystemCls.getCurUserOrgNo();//未传值，获取该用户当前单位编码
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtUser = BaseDT.T_SYSSEC_IPSUSER.getDT(new T_SYSSEC_IPSUSER_SW { });//取出用户列表，不用传单位编码
            JArray JA = new JArray();
            //// getTreeChild( dtUser, dtOrg, sw.curOrgNo);
            if (sw.curOrgNo == SystemCls.getCurUserOrgNo())
            {
                JObject root = new JObject
                        {
                        {"id",sw.curOrgNo}
                        ,{"text",BaseDT.T_SYS_ORG.getName(dtOrg,sw.curOrgNo)}
                        ,{"flag","type"}
                        //,{"state", sw.isTreeOpen},
                        };
                root.Add("children", getTreeChild(dtUser, dtOrg, sw.curOrgNo));
                JA.Add(root);
            }
            //JArray JA =  getTreeChild( dtUser, dtOrg, sw.curOrgNo);

            dtOrg.Clear();
            dtOrg.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            return JsonConvert.SerializeObject(JA);

        }

        /// <summary>
        /// 获取子单位
        /// </summary>
        /// <param name="dtUser">用户</param>
        /// <param name="dtOrg">单位</param>
        /// <param name="orgNo">单位编码</param>
        /// <returns>JArray</returns>
        private static JArray getTreeChild(DataTable dtUser, DataTable dtOrg, string orgNo)
        {
            JArray childobjArray = new JArray();
            //JArray childobjArray = new JArray();
            DataRow[] drOrg = null;// = dtOrg.Select("", "");// dt.Select("RATID = " + RATID + "", "ORDERBY");
            if (orgNo.Substring(4, 5) == "00000")//获取所有市的
                drOrg = dtOrg.Select(" SUBSTRING(ORGNO,1,4) = '" + ClsSql.EncodeSql(orgNo.Substring(0, 4)) + "' and substring(orgno,5,5)<>'00000' and substring(orgno,7,3)='000'");
            else if (orgNo.Substring(6, 3) == "000")//获取所有县的
                drOrg = dtOrg.Select(" SUBSTRING(ORGNO,1,6) = '" + ClsSql.EncodeSql(orgNo.Substring(0, 6)) + "' and substring(orgno,7,3)<>'000'");
            //else
            //    drOrg = dtOrg.Select(" ORGNO = '" + ClsSql.EncodeSql(orgNo) + "'");

            getTreeUser(childobjArray, dtUser, orgNo);
            if (drOrg != null)
            {

                for (int i = 0; i < drOrg.Length; i++)
                {
                    string orgID = drOrg[i]["ORGNO"].ToString();

                    JObject root = new JObject
                        {
                        {"id",orgID}
                        ,{"text",drOrg[i]["ORGNAME"].ToString()}
                        ,{"flag","type"}
                        ,{"state", "closed"},
                        };
                    root.Add("children", getTreeChild(dtUser, dtOrg, orgID));
                    childobjArray.Add(root);
                }
            }
            return childobjArray;
        }
        /// <summary>
        /// 获取单位编码下所有用户
        /// </summary>
        /// <param name="childobjArray">JArray</param>
        /// <param name="dtUser">用户</param>
        /// <param name="orgNo">单位编码</param>
        /// <returns>JArray</returns>
        private static JArray getTreeUser(JArray childobjArray, DataTable dtUser, string orgNo)
        {

            //JArray childobjArray = new JArray();
            DataRow[] drUser = dtUser.Select("ORGNO='" + orgNo + "'", "");
            for (int i = 0; i < drUser.Length; i++)
            {
                JObject root1 = new JObject
                        {
                        {"id", drUser[i]["USERID"].ToString()}
                        ,{"text",drUser[i]["USERNAME"].ToString()}  //+"["+drUser[i]["PHONE"].ToString()+"]"}
                        ,{"flag","user"}
                        };
                childobjArray.Add(root1);

            }
            return childobjArray;
        }
        #endregion

        #region 根据用户ID获取用户名称

        /// <summary>
        /// 根据用户ID获取用户用户名称
        /// </summary>
        /// <param name="UID">用户序号</param>
        /// <returns>参见模型</returns>
        public static string getname(string UID)
        {
            T_SYSSEC_IPSUSER_Model m = getModel(new T_SYSSEC_IPSUSER_SW { USERID = UID });
            return m.USERNAME;
        }
        #endregion

        #region 根据用户ID获取用户手机号码

        /// <summary>
        /// 根据用户ID获取用户手机号码
        /// </summary>
        /// <param name="UID">用户序号</param>
        /// <returns>参见模型</returns>
        public static string getPhone(string UID)
        {
            T_SYSSEC_IPSUSER_Model m = getModel(new T_SYSSEC_IPSUSER_SW { USERID = UID });
            return m.PHONE;
        }
        #endregion
    }
}
