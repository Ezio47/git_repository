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
namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 护林员管理
    /// </summary>
    public class T_IPSFR_USERCls
    {
        #region 增、删、改

        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(T_IPSFR_USER_Model m)
        {
            if (m.opMethod == "Add")
            {
                SystemCls.LogSave("3", "护林员:" + m.HNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_IPSFR_USER.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                SystemCls.LogSave("4", "护林员:" + m.HNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_IPSFR_USER.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "Update")
            {
                Message msg = BaseDT.T_IPSFR_USER.UpdateHlyParameter(m);
                return new Message(msg.Success, msg.Msg, m.returnUrl);
            }
            if (m.opMethod == "Cancle")
            {
                Message msg = BaseDT.T_IPSFR_USER.UpdateHlyParameter(m);
                return new Message(msg.Success, msg.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {
                SystemCls.LogSave("5", "护林员:" + m.HNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_IPSFR_USER.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Enable")
            {
                SystemCls.LogSave("5", "护林员:" + m.HNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_IPSFR_USER.Enable(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "UnEnable")
            {
                SystemCls.LogSave("5", "护林员:" + m.HNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_IPSFR_USER.UnEnable(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "PATROLLENGTH")
            {
                SystemCls.LogSave("5", "护林员:" + m.HNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_IPSFR_USER.PATROLLENGTHMdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");
        }

        /// <summary>
        /// 根据查询条件获取某一条用户信息记录，用于修改、删除、用户登录验证
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_IPSFR_USER_Model getModel(T_IPSFR_USER_SW sw)
        {
            DataTable dt = BaseDT.T_IPSFR_USER.getDT(sw);
            T_IPSFR_USER_Model m = new T_IPSFR_USER_Model();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtSex = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "性别" });//性别
            DataTable dtOnState = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "固兼职状态" });
            DataTable dtIsEnable = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "启用状态" });
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段 HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
                m.HID = dt.Rows[i]["HID"].ToString();
                m.HNAME = dt.Rows[i]["HNAME"].ToString();
                m.SN = dt.Rows[i]["SN"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.SEX = dt.Rows[i]["SEX"].ToString();
                if (string.IsNullOrEmpty(dt.Rows[i]["BIRTH"].ToString()) == false)
                    m.BIRTH = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BIRTH"].ToString());
                if (m.BIRTH == "1900-01-01")
                    m.BIRTH = "";
                m.ONSTATE = dt.Rows[i]["ONSTATE"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["BYORGNO"].ToString());
                m.SEXNAME = BaseDT.T_SYS_DICT.getName(dtSex, dt.Rows[i]["SEX"].ToString());
                m.ISENABLE = dt.Rows[i]["ISENABLE"].ToString();
                m.MOBILEPARAMLIST = dt.Rows[i]["MOBILEPARAMLIST"].ToString();
                m.ISENABLENAME = BaseDT.T_SYS_DICT.getName(dtIsEnable, dt.Rows[i]["ISENABLE"].ToString());
                m.ONSTATENAME = BaseDT.T_SYS_DICT.getName(dtOnState, dt.Rows[i]["ONSTATE"].ToString());
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dtSex.Clear();
            dtSex.Dispose();
            dtOnState.Clear();
            dtOnState.Dispose();
            dtIsEnable.Clear();
            dtIsEnable.Dispose();
            return m;
        }
        #endregion

        #region 获取模型
        /// <summary>
        /// 获取模型集合
        /// </summary>
        /// <param name="sw">sw</param>
        /// <param name="total">total</param>
        /// <returns></returns>
        public static IEnumerable<T_IPSFR_USER_Pager_Model> getListPagerModel(T_IPSFR_USER_SW sw, out int total)
        {
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(),IsEnableCUN="1" });//获取单位
            DataTable dtSex = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "性别" });//性别
            DataTable dtOnState = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "固兼职状态" });
            DataTable dtIsEnable = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "启用状态" });
            DataTable dt = BaseDT.T_IPSFR_USER.getDT(sw, out total);//列表
            var result = new List<T_IPSFR_USER_Pager_Model>();
            string hidList = "0";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                hidList += ",";
                hidList += dt.Rows[i]["HID"].ToString();
            }
            DataTable dtRoute = BaseDT.T_IPSFR_ROUTERAIL.getDT(new T_IPSFR_ROUTERAIL_SW { HID = hidList });//线路/围栏
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_IPSFR_USER_Pager_Model m = new T_IPSFR_USER_Pager_Model();
                m.HID = dt.Rows[i]["HID"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["BYORGNO"].ToString());
                m.HNAME = dt.Rows[i]["HNAME"].ToString();
                m.SN = dt.Rows[i]["SN"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.SEXNAME = BaseDT.T_SYS_DICT.getName(dtSex, dt.Rows[i]["SEX"].ToString());
                m.BIRTH = ClsSwitch.SwitDate(dt.Rows[i]["BIRTH"].ToString());//转换成标准格式日期
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ONSTATENAME = BaseDT.T_SYS_DICT.getName(dtOnState, dt.Rows[i]["ONSTATE"].ToString());
                m.ISENABLE = dt.Rows[i]["ISENABLE"].ToString();
                m.ISENABLENAME = BaseDT.T_SYS_DICT.getName(dtIsEnable, dt.Rows[i]["ISENABLE"].ToString());
                m.MOBILEPARAMLIST = dt.Rows[i]["MOBILEPARAMLIST"].ToString();

                m.isExitsLine = (BaseDT.T_IPSFR_ROUTERAIL.getCountByHidRoadtype(dtRoute, m.HID, "0") == "0") ? "0" : "1";
                m.isExitsRail = (BaseDT.T_IPSFR_ROUTERAIL.getCountByHidRoadtype(dtRoute, m.HID, "1") == "0") ? "0" : "1";
                result.Add(m);
            }
            dtORG.Clear();
            dtORG.Dispose();
            dtOnState.Clear();
            dtOnState.Dispose();
            dtSex.Clear();
            dtSex.Dispose();
            dt.Clear();
            dt.Dispose();
            dtIsEnable.Clear();
            dtIsEnable.Dispose();
            dtRoute.Clear();
            dtRoute.Dispose();
            return result;
        }

        /// <summary>
        /// 获取护林员列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<T_IPSFR_USER_Model> getListModel(T_IPSFR_USER_SW sw)
        {
            var result = new List<T_IPSFR_USER_Model>();
            DataTable dt = BaseDT.T_IPSFR_USER.getDT(sw);
            DataTable dtSex = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "性别" });//性别
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var m = new T_IPSFR_USER_Model();
                m.HID = dt.Rows[i]["HID"].ToString();
                m.HNAME = dt.Rows[i]["HNAME"].ToString();
                m.SN = dt.Rows[i]["SN"].ToString();
                m.SEX = dt.Rows[i]["SEX"].ToString();
                m.SEXNAME = BaseDT.T_SYS_DICT.getName(dtSex, dt.Rows[i]["SEX"].ToString());
                m.BIRTH = dt.Rows[i]["BIRTH"].ToString();
                m.ONSTATE = dt.Rows[i]["ONSTATE"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ISENABLE = dt.Rows[i]["ISENABLE"].ToString();
                m.ORGNAME = dt.Rows[i]["ORGNAME"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                result.Add(m);
            }
            return result;
        }
        #endregion

        #region 在线用户信息
        /// <summary>
        /// 在线用户信息
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_IPSFR_USER_OnLine_Model getUserLineModel(T_IPSFR_USER_SW sw)
        {
            string curUserOrg = SystemCls.getCurUserOrgNo();//获取当前登录用户的机构编码

            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = curUserOrg, SYSFLAG = ConfigCls.getSystemFlag() });//获取当前登录用户有权限查看的单位

            DataTable dtFRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", BYORGNO = curUserOrg });//获取所有有权限查看的护林员
            //当前在线的护林员
            DataTable dtRealTmp = BaseDT.T_IPS_REALDATATEMPORARY.getOnLineDtByOrgno(new T_IPS_REALDATATEMPORARYSW { ORGNO = curUserOrg });
            //FRUserC = dtFRUser.Compute("count(BYORGNO)", "substring(BYORGNO,1,6)='" + orgNo.Substring(0, 6) + "'").ToString();

            //    FRUserC = dtFRUser.Compute("count(BYORGNO)", "substring(BYORGNO,1,4)='" + orgNo.Substring(0, 4) + "'").ToString();
            //    FRUserCurC = dtRealTmp.Compute("count(BYORGNO)", "substring(BYORGNO,1,4)='" + orgNo.Substring(0, 4) + "'").ToString(); 
            T_IPSFR_USER_OnLine_Model Model = new T_IPSFR_USER_OnLine_Model();
            Model.LineCount = dtFRUser.Rows.Count.ToString();
            Model.LineInCount = dtRealTmp.Rows.Count.ToString();
            Model.LineOutCount = (dtFRUser.Rows.Count - dtRealTmp.Rows.Count).ToString();
            Model.LineOutRouteCount = dtRealTmp.Select("ISOUTRAIL=1").Count().ToString();//rRealTmp[0]["ISOUTRAIL"].ToString()

            return Model;
        }
        #endregion

        #region 在离线出围护林员
        /// <summary>
        /// 获取护林员hid集合
        /// </summary>
        /// <param name="orgno">机构</param>
        /// <param name="flag">类型 0 在线 1 离线 2 出围</param>
        /// <returns></returns>
        public static string GetHids(string orgno, string flag)
        {
            string hidstr = "";
            //获取所有有权限查看的护林员
            DataTable dtFRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", BYORGNO = orgno });
            //当前在线的护林员
            DataTable dtRealTmp = BaseDT.T_IPS_REALDATATEMPORARY.getOnLineDtByOrgno(new T_IPS_REALDATATEMPORARYSW { ORGNO = orgno });
            if (flag == "0")//在线
            {
                if (dtRealTmp != null)
                {
                    for (int i = 0; i < dtRealTmp.Rows.Count; i++)
                    {
                        if (dtRealTmp.Rows[i]["HID"] != null)//查询条件
                        {
                            hidstr += dtRealTmp.Rows[i]["HID"].ToString() + ",";
                        }
                    }
                }
            }
            else if (flag == "1")//离线
            {

                var userTotalList = new List<string>();//护林员数List
                var userOnLineList = new List<string>();//护林员在线数List
                if (dtFRUser != null)//护林员
                {
                    for (int i = 0; i < dtFRUser.Rows.Count; i++)
                    {
                        if (dtFRUser.Rows[i]["HID"] != null)//查询条件
                        {
                            userTotalList.Add(dtFRUser.Rows[i]["HID"].ToString());
                        }
                    }
                }
                if (dtRealTmp != null)//在线
                {
                    for (int i = 0; i < dtRealTmp.Rows.Count; i++)
                    {
                        if (dtRealTmp.Rows[i]["HID"] != null)//查询条件
                        {
                            userOnLineList.Add(dtFRUser.Rows[i]["HID"].ToString());
                        }
                    }
                }

                //获取两个数据源的差集 
                var hidlist = userTotalList.Except(userOnLineList);
                if (hidlist.Any())
                {
                    foreach (var hid in hidlist)
                    {
                        hidstr += hid + ",";
                    }
                }
            }
            else if (flag == "2")//出围
            {
                var drArr = dtRealTmp.Select("ISOUTRAIL=1");
                if (drArr.Count() > 0)
                {
                    foreach (DataRow dr in drArr)
                    {
                        hidstr += dr["HID"].ToString() + ",";
                    }
                }
            }
            return hidstr.TrimEnd(',');
        }

        #endregion

        #region 护林员树形菜单

        #region 组合护林员子单位菜单 getTreeChild
        /// <summary>
        /// 组合护林员子单位菜单
        /// </summary>
        /// <param name="dtOrg">单位DataTable</param>
        /// <param name="dtFRUser">护林员DataTable</param>
        /// <param name="dtRealTmp">临时表DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <returns>JArray字符串</returns>
        private static JArray getTreeChild(DataTable dtOrg, DataTable dtFRUser, DataTable dtRealTmp, string orgNo)
        {
            JArray childArray = new JArray();

            if (orgNo.Substring(4, 5) == "00000")//获取所有市下属的县
            {
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + orgNo.Substring(0, 4) + "' AND ORGNO<>'" + orgNo + "' and SUBSTRING(ORGNO,7,3)='000'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {

                    JObject root = new JObject
                     {
                         {"id",""},//ORGNO
                         {"text",drOrg[i]["ORGNAME"].ToString()+getOnLineHRUser(dtFRUser,dtRealTmp,drOrg[i]["ORGNO"].ToString())}
                     };
                    root.Add("children", getTreeChild(dtOrg, dtFRUser, dtRealTmp, drOrg[i]["ORGNO"].ToString()));//继续获取镇
                    childArray.Add(root);
                }
                return childArray;
            }
            else if (orgNo.Substring(6, 3) == "000")//获取所有县下属的乡
            {
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + orgNo.Substring(0, 6) + "' AND ORGNO<>'" + orgNo + "'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {

                    JObject root = new JObject
                     {
                         {"id",""},//ORGNO
                         {"text",drOrg[i]["ORGNAME"].ToString()+getOnLineHRUser(dtFRUser,dtRealTmp,drOrg[i]["ORGNO"].ToString())} 
                     };
                    root.Add("children", getTreeChild(dtOrg, dtFRUser, dtRealTmp, drOrg[i]["ORGNO"].ToString()));//继续获取护林员
                    childArray.Add(root);
                }
                return childArray;
            }
            else//获取护林员的
            {
                DataRow[] drFRUser = dtFRUser.Select("BYORGNO = '" + orgNo + "'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drFRUser.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<font");// color='{0}' title='{1}' onclick='getLonLat(\"{2}\")'>{3}[{4}]</font>");
                    //string tmp = drFRUser[i]["HNAME"].ToString() + "[" + drFRUser[i]["PHONE"].ToString() + "]";
                    DataRow[] drRealTmp = dtRealTmp.Select("HID='" + drFRUser[i]["HID"].ToString() + "'");
                    if (drRealTmp.Length == 0)//在在线判断时间段内未有记录，离线
                        //tmp = "<font color=red title='离线 点击快速定位' onclick='getLonLat(\"" + drFRUser[i]["HID"].ToString() + "\")'>" + tmp + "</font>";
                        sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getOutLineColor(), "离线 点击快速定位");
                    else
                    {
                        if (drRealTmp[0]["ISOUTRAIL"].ToString() == "0")//在线未出围
                            sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getInLineColor(), "在线 点击快速定位");
                        //tmp = "<font color=green title='在线 点击快速定位' onclick='getLonLat(\"" + drFRUser[i]["HID"].ToString() + "\")'>" + tmp + "</font>";
                        else//在线出围 ISOUTRAIL＝1 中间表中数据
                            sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getOutRailColor(), "出围(责任区) 点击快速定位");
                        //tmp = "<font color=green title='出围 点击快速定位' onclick='getLonLat(\"" + drFRUser[i]["HID"].ToString() + "\")'>" + tmp + "</font>";
                    }
                    sb.AppendFormat(" onclick='getLonLat(\"{0}\")'>", drFRUser[i]["HID"].ToString());
                    sb.AppendFormat("{0}[{1}]</font>", drFRUser[i]["HNAME"].ToString(), drFRUser[i]["PHONE"].ToString());
                    JObject root = new JObject
                     {
                         {"id",drFRUser[i]["HID"].ToString()} ,
                         {"text",sb.ToString()} 
                     };
                    //root.Add("children", getTreeChild(dtOrg, dtFRUser, drFRUser[i]["ORGNO"].ToString()));//继续获取护林员
                    childArray.Add(root);
                }
                return childArray;
            }
            //return childArray;
        }

        #endregion

        #region 根据机构编码获取该机构下在线/总人数 getOnLineHRUser
        /// <summary>
        /// 根据机构编码获取该机构下在线/总人数
        /// </summary>
        /// <param name="dtFRUser">护林员DataTable</param>
        /// <param name="dtRealTmp">DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <returns>在线/总人数</returns>
        private static string getOnLineHRUser(DataTable dtFRUser, DataTable dtRealTmp, string orgNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("(");
            string FRUserC = "0";//总人数
            string FRUserCurC = "0";//在线人数

            if (orgNo.Substring(4, 5) == "00000")//获取所有市的
            {
                FRUserC = dtFRUser.Compute("count(BYORGNO)", "substring(BYORGNO,1,4)='" + orgNo.Substring(0, 4) + "'").ToString();
                FRUserCurC = dtRealTmp.Compute("count(BYORGNO)", "substring(BYORGNO,1,4)='" + orgNo.Substring(0, 4) + "'").ToString();
            }
            else if (orgNo.Substring(6, 3) == "000")//获取所有县的
            {

                FRUserC = dtFRUser.Compute("count(BYORGNO)", "substring(BYORGNO,1,6)='" + orgNo.Substring(0, 6) + "'").ToString();
                FRUserCurC = dtRealTmp.Compute("count(BYORGNO)", "substring(BYORGNO,1,6)='" + orgNo.Substring(0, 6) + "'").ToString();
            }
            else
            {

                FRUserC = dtFRUser.Compute("count(BYORGNO)", "BYORGNO='" + orgNo + "'").ToString();
                FRUserCurC = dtRealTmp.Compute("count(BYORGNO)", "BYORGNO='" + orgNo + "'").ToString();
            }
            sb.AppendFormat("{0}/{1}", FRUserCurC, FRUserC);
            sb.AppendFormat(")");
            return sb.ToString();
        }

        #endregion

        #region 组合护林员树形菜单 getTree(T_IPSFR_USER_SW sw)
        /// <summary>
        /// 组合护林员树形菜单
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string getTree(T_IPSFR_USER_SW sw)
        {

            JArray jObjects = new JArray();
            string curUserOrg = SystemCls.getCurUserOrgNo();//获取当前登录用户的机构编码
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = curUserOrg, SYSFLAG = ConfigCls.getSystemFlag() });//获取当前登录用户有权限查看的单位

            DataTable dtFRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", BYORGNO = curUserOrg, PHONE = sw.PHONE, PhoneHname = sw.PhoneHname });//获取所有有权限查看的护林员
            //当前在线的护林员
            DataTable dtRealTmp = BaseDT.T_IPS_REALDATATEMPORARY.getOnLineDtByOrgno(new T_IPS_REALDATATEMPORARYSW { ORGNO = curUserOrg });
            DataRow[] drOrg = dtOrg.Select("", "ORGNO");
            if (drOrg.Length > 0)
            {

                JObject root = new JObject
                     {
                         {"id",""},//ORGNO
                         {"text",drOrg[0]["ORGNAME"].ToString()+getOnLineHRUser(dtFRUser,dtRealTmp,drOrg[0]["ORGNO"].ToString())} 
                     };
                root.Add("children", getTreeChild(dtOrg, dtFRUser, dtRealTmp, drOrg[0]["ORGNO"].ToString()));
                jObjects.Add(root);
            }
            dtOrg.Clear();
            dtOrg.Dispose();
            dtFRUser.Clear();
            dtFRUser.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }

        #endregion

        /// <summary>
        /// 组合护林员树形菜单 任务管理
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="OrgNo">组织机构编码</param>
        /// <returns>参见模型</returns>
        public static string getTreeByTask(T_IPSFR_USER_SW sw, string OrgNo)
        {
            JArray jObjects = new JArray();
            string curUserOrg = SystemCls.getCurUserOrgNo();//获取当前登录用户的机构编码
            if (string.IsNullOrEmpty(OrgNo) == false)
                curUserOrg = OrgNo;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = curUserOrg, SYSFLAG = ConfigCls.getSystemFlag(), IsEnableCUN = "1" });//获取当前登录用户有权限查看的单位
            DataTable dtFRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", BYORGNO = curUserOrg, PHONE = sw.PHONE, PhoneHname = sw.PhoneHname });//获取所有有权限查看的护林员
            //当前在线的护林员
            DataTable dtRealTmp = BaseDT.T_IPS_REALDATATEMPORARY.getOnLineDtByOrgno(new T_IPS_REALDATATEMPORARYSW { ORGNO = curUserOrg, PhoneHname = sw.PhoneHname });

            #region 市级用户
            if (PublicCls.OrgIsShi(curUserOrg))
            {
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                if (drOrg.Length > 0)
                {
                    JObject root = new JObject
                     {
                         {"id",drOrg[0]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrg[0]["ORGNAME"].ToString()} ,
                         {"treeType","org"} ,
                     };
                    JArray childArray = new JArray();
                    #region 护林员
                    DataRow[] drFRUser = dtFRUser.Select("BYORGNO = '" + curUserOrg + "'", "");
                    for (int i = 0; i < drFRUser.Length; i++)
                    {
                        JObject rootC = new JObject { { "id", drFRUser[i]["HID"].ToString() }, { "text", drFRUser[i]["HNAME"].ToString() }, { "treeType", "hly" } };
                        childArray.Add(rootC);
                    }
                    #endregion

                    #region 组织机构
                    DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + curUserOrg.Substring(0, 4) + "' AND ORGNO<>'" + curUserOrg + "' and SUBSTRING(ORGNO,7,3)='000'", "");//获取所有县且〈〉市的
                    for (int i = 0; i < drOrgC.Length; i++)
                    {
                        JObject rootC = new JObject
                     {
                         {"id",drOrgC[i]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrgC[i]["ORGNAME"].ToString()},
                         {"state","closed"},
                         {"treeType","org"} 
                     };
                        childArray.Add(rootC);
                    }
                    #endregion
                    
                    root.Add("children", childArray);
                    jObjects.Add(root);
                }
            }
            #endregion

            #region 县级用户
            else if (PublicCls.OrgIsXian(curUserOrg))
            {
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                if (drOrg.Length > 0)
                {
                    JObject root = new JObject
                     {
                         {"id",drOrg[0]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrg[0]["ORGNAME"].ToString()}, 
                         {"state","closed"} ,
                         {"treeType","org"} 
                     };
                    JArray jObjectsC = new JArray();
                    #region 护林员
                    DataRow[] drFRUser = dtFRUser.Select("BYORGNO = '" + curUserOrg + "'", "");
                    for (int i = 0; i < drFRUser.Length; i++)
                    {
                        JObject rootC = new JObject { { "id", drFRUser[i]["HID"].ToString() }, { "text", drFRUser[i]["HNAME"].ToString() }, { "treeType", "hly" } };
                        jObjects.Add(rootC);
                    }
                    #endregion

                    #region 组织机构
                    //DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + curUserOrg.Substring(0, 6) + "' AND ORGNO<>'" + curUserOrg + "'", ""); //获取所有县且〈〉市的
                    DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,6) = " + curUserOrg.Substring(0, 6) + " AND SUBSTRING(ORGNO,10,6) =" + curUserOrg.Substring(9, 6) + " AND ORGNO<>" + curUserOrg + "", "");
                    for (int i = 0; i < drOrgC.Length; i++)
                    {
                        JObject rootC = new JObject
                        {
                         {"id",drOrgC[i]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrgC[i]["ORGNAME"].ToString()},
                         {"state","closed"} ,
                         {"treeType","org"} 
                       };
                        //root.Add("children", getTreeChild(dtOrg, dtFRUser, dtRealTmp, drOrg[i]["ORGNO"].ToString()));//继续获取护林员
                        jObjectsC.Add(rootC);
                        if (string.IsNullOrEmpty(OrgNo) == false)//异步加载，不显示县名
                        {
                            jObjects.Add(rootC);
                        }
                    }
                    #endregion
                    
                    if (string.IsNullOrEmpty(OrgNo))//县级用户登录
                    {
                        jObjects.Add(root);
                        root.Add("children", jObjectsC);
                    }
                }
            }
            #endregion

            #region 乡镇用户
            else if (PublicCls.OrgIsZhen(curUserOrg))
            {
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                if (drOrg.Length > 0)
                {
                    JObject root = new JObject
                     {
                         {"id",drOrg[0]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrg[0]["ORGNAME"].ToString()},
                         {"state","closed"} ,
                         {"treeType","org"} 
                     };                   
                    JArray jObjectsC = new JArray();
                    #region 护林员
                    DataRow[] drFRUser = dtFRUser.Select("BYORGNO = '" + curUserOrg + "'", "");//获取所有县且〈〉市的
                    for (int i = 0; i < drFRUser.Length; i++)
                    {
                        JObject rootC = new JObject { { "id", drFRUser[i]["HID"].ToString() }, { "text", drFRUser[i]["HNAME"].ToString() }, { "treeType", "hly" } };
                        jObjectsC.Add(rootC);
                        if (string.IsNullOrEmpty(OrgNo) == false)//异步加载，不显示乡镇名
                        {
                            jObjects.Add(rootC);
                        }
                    }
                    #endregion

                    #region 组织机构
                    DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,9) = " + curUserOrg.Substring(0, 9) + " AND ORGNO<>" + curUserOrg + "", "");
                    for (int i = 0; i < drOrgC.Length; i++)
                    {
                        JObject rootC = new JObject
                        {
                         {"id",drOrgC[i]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrgC[i]["ORGNAME"].ToString()+getOnLineHRUser(dtFRUser,dtRealTmp,drOrgC[i]["ORGNO"].ToString())},
                         {"state","closed"} ,
                         {"treeType","org"} 
                       };
                        //root.Add("children", getTreeChild(dtOrg, dtFRUser, dtRealTmp, drOrg[i]["ORGNO"].ToString()));//继续获取护林员
                        jObjectsC.Add(rootC);
                        if (string.IsNullOrEmpty(OrgNo) == false)//异步加载，不显示县名
                        {
                            jObjects.Add(rootC);

                        }
                    }
                    #endregion

                    if (string.IsNullOrEmpty(OrgNo))//乡镇级用户登录
                    {
                        jObjects.Add(root);
                        root.Add("children", jObjectsC);
                    }
                }
            }
            #endregion

            #region 村用戶
            else if (PublicCls.OrgIsCun(curUserOrg))
            {
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                if (drOrg.Length > 0)
                {
                    JObject root = new JObject
                     {
                         {"id",drOrg[0]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrg[0]["ORGNAME"].ToString()+getOnLineHRUser(dtFRUser,dtRealTmp,drOrg[0]["ORGNO"].ToString())},
                         {"treeType","org"} 
                     };
                    JArray jObjectsC = new JArray();

                    #region 护林员
                    DataRow[] drFRUser = dtFRUser.Select("BYORGNO = '" + curUserOrg + "'", "");//获取所有县且〈〉市的
                    for (int i = 0; i < drFRUser.Length; i++)
                    {
                        JObject rootC = new JObject { { "id", drFRUser[i]["HID"].ToString() }, { "text", drFRUser[i]["HNAME"].ToString() }, { "treeType", "hly" } };
                        jObjectsC.Add(rootC);
                        if (string.IsNullOrEmpty(OrgNo) == false)//异步加载，不显示乡镇名
                        {
                            jObjects.Add(rootC);
                        }
                    }
                    #endregion

                    if (string.IsNullOrEmpty(OrgNo))//村用户登录
                    {
                        jObjects.Add(root);
                        root.Add("children", jObjectsC);
                    }
                }
            }
            #endregion

            dtOrg.Clear();
            dtOrg.Dispose();
            dtFRUser.Clear();
            dtFRUser.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }

        #region 组合护林员树形菜单 getTree(T_IPSFR_USER_SW sw,string OrgNo)
        /// <summary>
        /// 组合护林员树形菜单
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="OrgNo">组织机构编码</param>
        /// <returns>参见模型</returns>
        public static string getTree(T_IPSFR_USER_SW sw, string OrgNo)
        {
            JArray jObjects = new JArray();
            string curUserOrg = SystemCls.getCurUserOrgNo();//获取当前登录用户的机构编码
            if (string.IsNullOrEmpty(OrgNo) == false)
                curUserOrg = OrgNo;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = curUserOrg, SYSFLAG = ConfigCls.getSystemFlag(),IsEnableCUN="1"});//获取当前登录用户有权限查看的单位
            DataTable dtFRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", BYORGNO = curUserOrg, PHONE = sw.PHONE, PhoneHname = sw.PhoneHname });//获取所有有权限查看的护林员
            //当前在线的护林员
            DataTable dtRealTmp = BaseDT.T_IPS_REALDATATEMPORARY.getOnLineDtByOrgno(new T_IPS_REALDATATEMPORARYSW { ORGNO = curUserOrg, PhoneHname = sw.PhoneHname });

            #region 市级用户
            if (PublicCls.OrgIsShi(curUserOrg))
            {
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                if (drOrg.Length > 0)
                {
                    JObject root = new JObject
                     {
                         {"id",drOrg[0]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrg[0]["ORGNAME"].ToString()+getOnLineHRUser(dtFRUser,dtRealTmp,drOrg[0]["ORGNO"].ToString())} ,
                         {"treeType","org"} ,
                     };
                    JArray childArray = new JArray();

                    #region 护林员
                    DataRow[] drFRUser = dtFRUser.Select("BYORGNO = '" + curUserOrg + "'", "");
                    for (int i = 0; i < drFRUser.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<font");
                        DataRow[] drRealTmp = dtRealTmp.Select("HID='" + drFRUser[i]["HID"].ToString() + "'");
                        if (drRealTmp.Length == 0)//在在线判断时间段内未有记录，离线
                            sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getOutLineColor(), "离线 点击快速定位");
                        else
                        {
                            if (drRealTmp[0]["ISOUTRAIL"].ToString() == "0")//在线未出围
                                sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getInLineColor(), "在线 点击快速定位");
                            else//在线出围 ISOUTRAIL＝1 中间表中数据
                                sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getOutRailColor(), "出围(责任区) 点击快速定位");
                        }
                        sb.AppendFormat(" onclick='getLonLat(\"{0}\")'>", drFRUser[i]["HID"].ToString());
                        sb.AppendFormat("{0}[{1}]</font>", drFRUser[i]["HNAME"].ToString(), drFRUser[i]["PHONE"].ToString());
                        JObject rootC = new JObject { { "id", drFRUser[i]["HID"].ToString() }, { "text", sb.ToString() }, { "treeType", "hly" } };
                        //root.Add("children", getTreeChild(dtOrg, dtFRUser, drFRUser[i]["ORGNO"].ToString()));//继续获取护林员
                        childArray.Add(rootC);
                    }
                    #endregion

                    #region 组织机构
                    DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + curUserOrg.Substring(0, 4) + "' AND ORGNO<>'" + curUserOrg + "' and SUBSTRING(ORGNO,7,3)='000'", "");//获取所有县且〈〉市的
                    for (int i = 0; i < drOrgC.Length; i++)
                    {
                        JObject rootC = new JObject
                     {
                         {"id",drOrgC[i]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrgC[i]["ORGNAME"].ToString()+getOnLineHRUser(dtFRUser,dtRealTmp,drOrgC[i]["ORGNO"].ToString())},
                         {"state","closed"},
                         {"treeType","org"} 
                     };
                        childArray.Add(rootC);
                    }
                    #endregion

                    root.Add("children", childArray);
                    jObjects.Add(root);
                }
            }
            #endregion

            #region 县级用户
            else if (PublicCls.OrgIsXian(curUserOrg))
            {
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                if (drOrg.Length > 0)
                {
                    JObject root = new JObject
                     {
                         {"id",drOrg[0]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrg[0]["ORGNAME"].ToString()+getOnLineHRUser(dtFRUser,dtRealTmp,drOrg[0]["ORGNO"].ToString())}, 
                         {"treeType","org"} 
                     };
                    JArray jObjectsC = new JArray();
                    //DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + curUserOrg.Substring(0, 6) + "' AND ORGNO<>'" + curUserOrg + "'", ""); //获取所有县且〈〉市的

                    #region 护林员
                    DataRow[] drFRUser = dtFRUser.Select("BYORGNO = '" + curUserOrg + "'", "");
                    for (int i = 0; i < drFRUser.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<font");
                        DataRow[] drRealTmp = dtRealTmp.Select("HID='" + drFRUser[i]["HID"].ToString() + "'");
                        if (drRealTmp.Length == 0)//在在线判断时间段内未有记录，离线
                            sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getOutLineColor(), "离线 点击快速定位");
                        else
                        {
                            if (drRealTmp[0]["ISOUTRAIL"].ToString() == "0")//在线未出围
                                sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getInLineColor(), "在线 点击快速定位");
                            else//在线出围 ISOUTRAIL＝1 中间表中数据
                                sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getOutRailColor(), "出围(责任区) 点击快速定位");
                        }
                        sb.AppendFormat(" onclick='getLonLat(\"{0}\")'>", drFRUser[i]["HID"].ToString());
                        sb.AppendFormat("{0}[{1}]</font>", drFRUser[i]["HNAME"].ToString(), drFRUser[i]["PHONE"].ToString());
                        JObject rootC = new JObject { { "id", drFRUser[i]["HID"].ToString() }, { "text", sb.ToString() }, { "treeType", "hly" } };
                        //root.Add("children", getTreeChild(dtOrg, dtFRUser, drFRUser[i]["ORGNO"].ToString()));//继续获取护林员
                        jObjects.Add(rootC);
                    }
                    #endregion

                    #region 组织机构
                    DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,6) = " + curUserOrg.Substring(0, 6) + " AND SUBSTRING(ORGNO,10,6) =" + curUserOrg.Substring(9, 6) + " AND ORGNO<>" + curUserOrg + "", "");
                    for (int i = 0; i < drOrgC.Length; i++)
                    {
                        JObject rootC = new JObject
                        {
                         {"id",drOrgC[i]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrgC[i]["ORGNAME"].ToString()+getOnLineHRUser(dtFRUser,dtRealTmp,drOrgC[i]["ORGNO"].ToString())},
                         {"state","closed"} ,
                         {"treeType","org"} 
                       };
                        //root.Add("children", getTreeChild(dtOrg, dtFRUser, dtRealTmp, drOrg[i]["ORGNO"].ToString()));//继续获取护林员
                        jObjectsC.Add(rootC);
                        if (string.IsNullOrEmpty(OrgNo) == false)//异步加载，不显示县名
                        {
                            jObjects.Add(rootC);

                        }
                    }
                    #endregion
                    
                    if (string.IsNullOrEmpty(OrgNo))//县级用户登录
                    {
                        jObjects.Add(root);
                        root.Add("children", jObjectsC);
                    }
                }
            }
            #endregion

            #region 乡镇用户
            else if (PublicCls.OrgIsZhen(curUserOrg))
            {
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                if (drOrg.Length > 0)
                {
                    JObject root = new JObject
                     {
                         {"id",drOrg[0]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrg[0]["ORGNAME"].ToString()+getOnLineHRUser(dtFRUser,dtRealTmp,drOrg[0]["ORGNO"].ToString())},
                         {"treeType","org"} 
                     };
                    JArray jObjectsC = new JArray();

                    #region 护林员
                    DataRow[] drFRUser = dtFRUser.Select("BYORGNO = '" + curUserOrg + "'", "");//获取所有县且〈〉市的
                    for (int i = 0; i < drFRUser.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<font");// color='{0}' title='{1}' onclick='getLonLat(\"{2}\")'>{3}[{4}]</font>");
                        //string tmp = drFRUser[i]["HNAME"].ToString() + "[" + drFRUser[i]["PHONE"].ToString() + "]";
                        DataRow[] drRealTmp = dtRealTmp.Select("HID='" + drFRUser[i]["HID"].ToString() + "'");
                        if (drRealTmp.Length == 0)//在在线判断时间段内未有记录，离线
                            //tmp = "<font color=red title='离线 点击快速定位' onclick='getLonLat(\"" + drFRUser[i]["HID"].ToString() + "\")'>" + tmp + "</font>";
                            sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getOutLineColor(), "离线 点击快速定位");
                        else
                        {
                            if (drRealTmp[0]["ISOUTRAIL"].ToString() == "0")//在线未出围
                                sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getInLineColor(), "在线 点击快速定位");
                            //tmp = "<font color=green title='在线 点击快速定位' onclick='getLonLat(\"" + drFRUser[i]["HID"].ToString() + "\")'>" + tmp + "</font>";
                            else//在线出围 ISOUTRAIL＝1 中间表中数据
                                sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getOutRailColor(), "出围(责任区) 点击快速定位");
                            //tmp = "<font color=green title='出围 点击快速定位' onclick='getLonLat(\"" + drFRUser[i]["HID"].ToString() + "\")'>" + tmp + "</font>";
                        }
                        sb.AppendFormat(" onclick='getLonLat(\"{0}\")'>", drFRUser[i]["HID"].ToString());
                        sb.AppendFormat("{0}[{1}]</font>", drFRUser[i]["HNAME"].ToString(), drFRUser[i]["PHONE"].ToString());
                        JObject rootC = new JObject { { "id", drFRUser[i]["HID"].ToString() }, { "text", sb.ToString() }, { "treeType", "hly" } };
                        //root.Add("children", getTreeChild(dtOrg, dtFRUser, drFRUser[i]["ORGNO"].ToString()));//继续获取护林员
                        jObjectsC.Add(rootC);
                        if (string.IsNullOrEmpty(OrgNo) == false)//异步加载，不显示乡镇名
                        {
                            jObjects.Add(rootC);
                        }
                    }
                    #endregion

                    #region 组织机构
                    DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,9) = " + curUserOrg.Substring(0, 9) + " AND ORGNO<>" + curUserOrg + "", "");
                    for (int i = 0; i < drOrgC.Length; i++)
                    {
                        JObject rootC = new JObject
                        {
                         {"id",drOrgC[i]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrgC[i]["ORGNAME"].ToString()+getOnLineHRUser(dtFRUser,dtRealTmp,drOrgC[i]["ORGNO"].ToString())},
                         {"state","closed"} ,
                         {"treeType","org"} 
                       };
                        //root.Add("children", getTreeChild(dtOrg, dtFRUser, dtRealTmp, drOrg[i]["ORGNO"].ToString()));//继续获取护林员
                        jObjectsC.Add(rootC);
                        if (string.IsNullOrEmpty(OrgNo) == false)//异步加载，不显示县名
                        {
                            jObjects.Add(rootC);

                        }
                    }
                    #endregion
                               
                    if (string.IsNullOrEmpty(OrgNo))//乡镇级用户登录
                    {
                        jObjects.Add(root);
                        root.Add("children", jObjectsC);
                    }
                }
            }
            #endregion

            #region 村用戶
            else if (PublicCls.OrgIsCun(curUserOrg))
            {
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                if (drOrg.Length > 0)
                {
                    JObject root = new JObject
                     {
                         {"id",drOrg[0]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrg[0]["ORGNAME"].ToString()+getOnLineHRUser(dtFRUser,dtRealTmp,drOrg[0]["ORGNO"].ToString())},
                         {"treeType","org"} 
                     };
                    JArray jObjectsC = new JArray();

                    #region 护林员
                    DataRow[] drFRUser = dtFRUser.Select("BYORGNO = '" + curUserOrg + "'", "");//获取所有县且〈〉市的
                    for (int i = 0; i < drFRUser.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<font");// color='{0}' title='{1}' onclick='getLonLat(\"{2}\")'>{3}[{4}]</font>");
                        //string tmp = drFRUser[i]["HNAME"].ToString() + "[" + drFRUser[i]["PHONE"].ToString() + "]";
                        DataRow[] drRealTmp = dtRealTmp.Select("HID='" + drFRUser[i]["HID"].ToString() + "'");
                        if (drRealTmp.Length == 0)//在在线判断时间段内未有记录，离线
                            //tmp = "<font color=red title='离线 点击快速定位' onclick='getLonLat(\"" + drFRUser[i]["HID"].ToString() + "\")'>" + tmp + "</font>";
                            sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getOutLineColor(), "离线 点击快速定位");
                        else
                        {
                            if (drRealTmp[0]["ISOUTRAIL"].ToString() == "0")//在线未出围
                                sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getInLineColor(), "在线 点击快速定位");
                            //tmp = "<font color=green title='在线 点击快速定位' onclick='getLonLat(\"" + drFRUser[i]["HID"].ToString() + "\")'>" + tmp + "</font>";
                            else//在线出围 ISOUTRAIL＝1 中间表中数据
                                sb.AppendFormat(" color='{0}' title='{1}'", ConfigCls.getOutRailColor(), "出围(责任区) 点击快速定位");
                            //tmp = "<font color=green title='出围 点击快速定位' onclick='getLonLat(\"" + drFRUser[i]["HID"].ToString() + "\")'>" + tmp + "</font>";
                        }
                        sb.AppendFormat(" onclick='getLonLat(\"{0}\")'>", drFRUser[i]["HID"].ToString());
                        sb.AppendFormat("{0}[{1}]</font>", drFRUser[i]["HNAME"].ToString(), drFRUser[i]["PHONE"].ToString());
                        JObject rootC = new JObject { { "id", drFRUser[i]["HID"].ToString() }, { "text", sb.ToString() }, { "treeType", "hly" } };
                        //root.Add("children", getTreeChild(dtOrg, dtFRUser, drFRUser[i]["ORGNO"].ToString()));//继续获取护林员
                        jObjectsC.Add(rootC);
                        if (string.IsNullOrEmpty(OrgNo) == false)//异步加载，不显示乡镇名
                        {
                            jObjects.Add(rootC);
                        }
                    }
                    #endregion

                    //#region 组织机构
                    //DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,9) = " + curUserOrg.Substring(0, 9) + " AND ORGNO<>" + curUserOrg + "", "");
                    //for (int i = 0; i < drOrgC.Length; i++)
                    //{
                    //    JObject rootC = new JObject
                    //    {
                    //     {"id",drOrgC[i]["ORGNO"].ToString()},//ORGNO
                    //     {"text",drOrgC[i]["ORGNAME"].ToString()+getOnLineHRUser(dtFRUser,dtRealTmp,drOrgC[i]["ORGNO"].ToString())},
                    //     {"state","closed"} ,
                    //     {"treeType","org"} 
                    //   };
                    //    //root.Add("children", getTreeChild(dtOrg, dtFRUser, dtRealTmp, drOrg[i]["ORGNO"].ToString()));//继续获取护林员
                    //    jObjectsC.Add(rootC);
                    //    if (string.IsNullOrEmpty(OrgNo) == false)//异步加载，不显示县名
                    //    {
                    //        jObjects.Add(rootC);

                    //    }
                    //}
                    //#endregion
                               
                    if (string.IsNullOrEmpty(OrgNo))//村用户登录
                    {
                        jObjects.Add(root);
                        root.Add("children", jObjectsC);
                    }
                }
            }
            #endregion

            dtOrg.Clear();
            dtOrg.Dispose();
            dtFRUser.Clear();
            dtFRUser.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }

        #endregion

        #endregion

        #region 系统用户人员树形菜单
        /// <summary>
        /// 组合护林员子单位菜单
        /// </summary>
        /// <param name="dtOrg">单位DataTable</param>
        /// <param name="dtUser">护林员DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <returns>JArray字符串</returns>
        private static JArray getTreeUserChild(DataTable dtOrg, DataTable dtUser, string orgNo)
        {
            JArray childArray = new JArray();
            if (orgNo.Substring(4, 5) == "00000")//获取所有市下属的人员
            {
                DataRow[] drUser = dtUser.Select("ORGNO = '" + orgNo + "'", "");
                for (int i = 0; i < drUser.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0}", drUser[i]["USERNAME"].ToString());
                    JObject root = new JObject { { "id", drUser[i]["USERID"].ToString() }, { "text", sb.ToString() } };
                    childArray.Add(root);
                }
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + orgNo.Substring(0, 4) + "' AND ORGNO<>'" + orgNo + "' and SUBSTRING(ORGNO,7,3)='000'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {
                    JObject root = new JObject { { "id", "" }, { "text", drOrg[i]["ORGNAME"].ToString() } };
                    root.Add("children", getTreeUserChild(dtOrg, dtUser, drOrg[i]["ORGNO"].ToString()));
                    childArray.Add(root);
                }
                return childArray;
            }
            else if (orgNo.Substring(6, 3) == "000")//获取所有县下属的乡
            {
                DataRow[] drUser = dtUser.Select("ORGNO = '" + orgNo + "'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drUser.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0}", drUser[i]["USERNAME"].ToString());
                    JObject root = new JObject { { "id", drUser[i]["USERID"].ToString() }, { "text", sb.ToString() } };
                    childArray.Add(root);
                }
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + orgNo.Substring(0, 6) + "' AND ORGNO<>'" + orgNo + "'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {
                    JObject root = new JObject { { "id", "" }, { "text", drOrg[i]["ORGNAME"].ToString() } };
                    root.Add("children", getTreeUserChild(dtOrg, dtUser, drOrg[i]["ORGNO"].ToString()));//继续获取用户
                    childArray.Add(root);
                }
                return childArray;
            }
            else
            {
                DataRow[] drUser = dtUser.Select("ORGNO = '" + orgNo + "'", "");//获取乡镇下的人员
                for (int i = 0; i < drUser.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0}", drUser[i]["USERNAME"].ToString());
                    JObject root = new JObject { { "id", drUser[i]["USERID"].ToString() }, { "text", sb.ToString() } };
                    childArray.Add(root);
                }
                return childArray;
            }
        }

        /// <summary>
        /// 系统用户组合人员树形菜单
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string getUserTree(T_IPSFR_USER_SW sw)
        {
            JArray jObjects = new JArray();
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            //DataTable dtUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", BYORGNO = curUserOrg, PHONE = sw.PHONE, PhoneHname = sw.PhoneHname });//获取所有有权限查看的护林员
            DataTable dtUser = BaseDT.T_SYSSEC_IPSUSER.getDT(new T_SYSSEC_IPSUSER_SW { });
            //获取所有人员
            DataRow[] drOrg = dtOrg.Select("", "ORGNO");
            if (drOrg.Length > 0)
            {
                JObject root = new JObject { { "id", "" }, { "text", drOrg[0]["ORGNAME"].ToString() } };
                root.Add("children", getTreeUserChild(dtOrg, dtUser, drOrg[0]["ORGNO"].ToString()));
                jObjects.Add(root);
            }
            dtOrg.Clear();
            dtOrg.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }
        #endregion

        #region 数据中心-护林员-树形菜单
        /// <summary>
        /// 数据中心-护林员-树形子菜单
        /// </summary>
        /// <param name="dtOrg"></param>
        /// <param name="dtFRUser"></param>
        /// <param name="orgNo"></param>
        /// <returns></returns>
        public static JArray getDatacenterTreeChild(DataTable dtOrg, DataTable dtFRUser, string orgNo)
        {
            JArray childArray = new JArray();

            if (orgNo.Substring(4, 5) == "00000")//获取所有市下属的县
            {
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + orgNo.Substring(0, 4) + "' AND ORGNO<>'" + orgNo + "' and SUBSTRING(ORGNO,7,3)='000'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {

                    JObject root = new JObject
                     {
                         {"id",drOrg[i]["ORGNO"].ToString()},
                         {"text",drOrg[i]["ORGNAME"].ToString()},
                         {"type","2"},
                         {"flag","0"}
                     };
                    root.Add("children", getDatacenterTreeChild(dtOrg, dtFRUser, drOrg[i]["ORGNO"].ToString()));//继续获取镇
                    childArray.Add(root);
                }
                return childArray;
            }
            else if (orgNo.Substring(6, 3) == "000")//获取所有县下属的乡
            {
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + orgNo.Substring(0, 6) + "' AND ORGNO<>'" + orgNo + "'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {

                    JObject root = new JObject
                     {
                         {"id",drOrg[i]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrg[i]["ORGNAME"].ToString()},
                         {"type","2"},
                         {"flag","0"}
                     };
                    root.Add("children", getDatacenterTreeChild(dtOrg, dtFRUser, drOrg[i]["ORGNO"].ToString()));//继续获取护林员
                    childArray.Add(root);
                }
                return childArray;
            }
            else//获取护林员的
            {
                DataRow[] drFRUser = dtFRUser.Select("BYORGNO = '" + orgNo + "'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drFRUser.Length; i++)
                {
                    JObject root = new JObject
                     {
                         {"id",drFRUser[i]["HID"].ToString()} ,
                         {"text",drFRUser[i]["HNAME"].ToString()},
                         {"type","2"},
                         {"flag","1"}

                     };
                    childArray.Add(root);
                }
                return childArray;
            }
        }

        #endregion

        #region 通讯录
        /// <summary>
        /// 通讯录树形菜单tree json字符串
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getTXLUserTree(T_SYS_ADDREDDBOOK_SW sw)
        {
            JArray jObjects = new JArray();
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });

            DataTable dtUser = BaseDT.T_SYS_ADDREDDBOOK.getDT(new T_SYS_ADDREDDBOOK_SW { });//通讯录
            //获取所有人员
            DataRow[] drOrg = dtOrg.Select("", "ORGNO");
            if (drOrg.Length > 0)
            {
                JObject root = new JObject
                     {
                         {"id",""},//ORGNO
                         {"text",drOrg[0]["ORGNAME"].ToString()} 
                     };
                root.Add("children", getTreeTXLUserChild(dtOrg, dtUser, drOrg[0]["ORGNO"].ToString()));
                jObjects.Add(root);
            }

            dtOrg.Clear();
            dtOrg.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }

        /// <summary>
        /// 通讯录json
        /// </summary>
        /// <param name="dtOrg"></param>
        /// <param name="dtUser"></param>
        /// <param name="orgNo"></param>
        /// <returns></returns>
        private static JArray getTreeTXLUserChild(DataTable dtOrg, DataTable dtUser, string orgNo)
        {
            JArray childArray = new JArray();
            if (orgNo.Substring(4, 5) == "00000")//获取所有市下属的县
            {
                DataRow[] drUser = dtUser.Select("ORGNO = '" + orgNo + "'", "");
                for (int i = 0; i < drUser.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0}", drUser[i]["ADNAME"].ToString());
                    JObject root = new JObject
                     {
                         {"id",drUser[i]["ADID"].ToString()} ,
                         {"text",sb.ToString()} 
                     };

                    childArray.Add(root);
                }
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + orgNo.Substring(0, 4) + "' AND ORGNO<>'" + orgNo + "' and SUBSTRING(ORGNO,7,3)='000'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {

                    JObject root = new JObject
                     {
                         {"id",""},//ORGNO
                         {"text",drOrg[i]["ORGNAME"].ToString()}
                     };
                    root.Add("children", getTreeTXLUserChild(dtOrg, dtUser, drOrg[i]["ORGNO"].ToString()));
                    childArray.Add(root);
                }
                return childArray;
            }
            else if (orgNo.Substring(6, 3) == "000")//获取所有县下属的乡
            {
                DataRow[] drUser = dtUser.Select("ORGNO = '" + orgNo + "'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drUser.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0}", drUser[i]["ADNAME"].ToString());
                    JObject root = new JObject
                     {
                         {"id",drUser[i]["ADID"].ToString()} ,
                         {"text",sb.ToString()} 
                     };

                    childArray.Add(root);
                }
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + orgNo.Substring(0, 6) + "' AND ORGNO<>'" + orgNo + "'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {

                    JObject root = new JObject
                     {
                         {"id",""},//ORGNO
                         {"text",drOrg[i]["ORGNAME"].ToString()} 
                     };
                    root.Add("children", getTreeTXLUserChild(dtOrg, dtUser, drOrg[i]["ORGNO"].ToString()));//继续获取用户
                    childArray.Add(root);
                }
                return childArray;
            }
            else
            {
                DataRow[] drUser = dtUser.Select("ORGNO = '" + orgNo + "'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drUser.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0}", drUser[i]["ADNAME"].ToString());
                    JObject root = new JObject
                     {
                         {"id",drUser[i]["ADID"].ToString()} ,
                         {"text",sb.ToString()} 
                     };

                    childArray.Add(root);
                }
                return childArray;
            }
        }
        #endregion

        #region 护林员上传
        /// <summary>
        /// 护林员上传
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void HRUserUpload(string filePath)
        {
            HSSFWorkbook hssfworkbook;
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);

            //DataTable table = new DataTable();
            //IRow headerRow = sheet.GetRow(0);//第一行为标题行
            //int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells
            int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1

            //handling header.
            //for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            //{
            //    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
            //    table.Columns.Add(column);
            //}
            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {

                IRow row = sheet.GetRow(i);
                string[] arr = new string[8];
                for (int k = 0; k < arr.Length; k++)
                {
                    if (k != 7)
                        arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                    else
                        arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                }
                T_IPSFR_USER_Model m = new T_IPSFR_USER_Model();
                //所属县	所属乡镇	姓名	终端编号	手机号码	性别	固兼职	出生日期
                if (string.IsNullOrEmpty(arr[2]) || string.IsNullOrEmpty(arr[4]) || string.IsNullOrEmpty(arr[1]))
                {
                    continue;
                }
                m.HNAME = arr[2];
                m.SN = arr[3];
                m.PHONE = arr[4];
                if (string.IsNullOrEmpty(arr[5]))//性别
                {
                    m.SEX = "0";
                }
                else
                {
                    m.SEX = (arr[5] == "男") ? "0" : "1";
                }
                if (string.IsNullOrEmpty(arr[6]))//是否固职
                {
                    m.SEX = "1";
                }
                else
                {
                    m.ONSTATE = (arr[6] == "固职") ? "1" : "2";
                }
                m.BIRTH = arr[7];
                if (m.BIRTH == "9999-12-31")
                    m.BIRTH = "1900-01-01";

                m.BYORGNO = BaseDT.T_SYS_ORG.getCodeByName(arr[1]);

                m.ISENABLE = "1";
                BaseDT.T_IPSFR_USER.Add(m);
                //sb.AppendFormat("INSERT INTO  T_IPSFR_USER(HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO)");
                //sb.AppendFormat("VALUES(");
                //sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.HNAME));
                //sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SN));
                //sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PHONE));
                //sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SEX));
                //sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BIRTH));
                //sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ONSTATE));
                //sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
                //sb.AppendFormat(")");
                string a = row.GetCell(0).ToString();
                string a1 = row.GetCell(1).ToString();
                string a2 = row.GetCell(2).ToString();


                //DataRow dataRow = table.NewRow();

                //if (row != null)
                //{
                //    for (int j = row.FirstCellNum; j < cellCount; j++)
                //    {
                //        //if (row.GetCell(j) != null)
                //        //dataRow[j] = GetCellValue(row.GetCell(j));
                //    }
                //}

                //table.Rows.Add(dataRow);

            }
            // return table;

        }
        #endregion

        #region 获取所有用户
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getuser(T_SYSSEC_IPSUSER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.T_SYSSEC_USER.getDT(sw);
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string userid = dt.Rows[i]["USERID"].ToString();
                string username = dt.Rows[i]["USERNAME"].ToString();
                username = "-" + username;
                sb.AppendFormat("<option value=\"{0}\">{1}</option>", userid, username);
            }
            dt.Clear();
            dt.Dispose();
            return sb.ToString();
        }
        #endregion

        #region 短信树形菜单

        #region 组合护林员子单位菜单 getFRUserTreeChild
        /// <summary>
        /// 组合护林员子单位菜单
        /// </summary>
        /// <param name="dtOrg">单位DataTable</param>
        /// <param name="dtFRUser">护林员DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <returns>JArray字符串</returns>
        private static JArray getFRUserTreeChild(DataTable dtOrg, DataTable dtFRUser, string orgNo)
        {
            JArray childArray = new JArray();

            if (orgNo.Substring(4, 5) == "00000")//获取所有市下属的县
            {
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + orgNo.Substring(0, 4) + "' AND ORGNO<>'" + orgNo + "' and SUBSTRING(ORGNO,7,3)='000'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {

                    JObject root = new JObject
                     {
                         {"id",""},//ORGNO
                         {"text",drOrg[i]["ORGNAME"].ToString()},
                         {"state","closed"}
                     };
                    root.Add("children", getFRUserTreeChild(dtOrg, dtFRUser, drOrg[i]["ORGNO"].ToString()));//继续获取镇
                    childArray.Add(root);
                }
                return childArray;
            }
            else if (orgNo.Substring(6, 3) == "000")//获取所有县下属的乡
            {
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + orgNo.Substring(0, 6) + "' AND ORGNO<>'" + orgNo + "'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {

                    JObject root = new JObject
                     {
                         {"id",""},//ORGNO
                         {"text",drOrg[i]["ORGNAME"].ToString()}, 
                         {"state","closed"}
                     };
                    root.Add("children", getFRUserTreeChild(dtOrg, dtFRUser, drOrg[i]["ORGNO"].ToString()));//继续获取护林员
                    childArray.Add(root);
                }
                return childArray;
            }
            else//获取护林员的
            {
                DataRow[] drFRUser = dtFRUser.Select("BYORGNO = '" + orgNo + "'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drFRUser.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<font title={0}>", drFRUser[i]["PHONE"].ToString());
                    sb.AppendFormat("{0}</font>", drFRUser[i]["HNAME"].ToString());
                    JObject root = new JObject
                     {
                         {"id",drFRUser[i]["PHONE"].ToString()} ,
                         {"text",sb.ToString()},
                         {"flag", drFRUser[i]["HNAME"].ToString()}
                     };
                    childArray.Add(root);
                }
                return childArray;
            }
        }

        #endregion

        #region 组合组织机构人员子单位菜单 getTreeSMSUserChild
        /// <summary>
        /// 组合护林员子单位菜单
        /// </summary>
        /// <param name="dtOrg">单位DataTable</param>
        /// <param name="dtLink">组织机构DataTable</param>
        /// <param name="dtVillagecommittee">自然村DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <returns>JArray字符串</returns>
        private static JArray getTreeSMSUserChild(DataTable dtOrg, DataTable dtLink, DataTable dtVillagecommittee, string orgNo)
        {
            JArray childArray = new JArray();
            if (orgNo.Length == 9 && orgNo.Substring(4, 5) == "00000")//获取所有市下属的县
            {
                DataRow[] drLink = dtLink.Select("BYORGNO = '" + orgNo + "'", "");
                for (int i = 0; i < drLink.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<font title={0}>", drLink[i]["PHONE"].ToString());
                    sb.AppendFormat("{0}[{1}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString());
                    JObject root = new JObject { 
                        { "id", drLink[i]["PHONE"].ToString() },
                        { "text",sb.ToString() } ,
                        { "flag",drLink[i]["NAME"].ToString() } 
                    };
                    childArray.Add(root);
                }
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + orgNo.Substring(0, 4) + "' AND ORGNO<>'" + orgNo + "' and SUBSTRING(ORGNO,7,3)='000'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {

                    JObject root = new JObject
                     {
                         {"id",""},//ORGNO
                         {"text",drOrg[i]["ORGNAME"].ToString()},
                         {"state","closed"}
                     };
                    root.Add("children", getTreeSMSUserChild(dtOrg, dtLink, dtVillagecommittee, drOrg[i]["ORGNO"].ToString()));//继续获取镇
                    childArray.Add(root);
                }
                return childArray;
            }
            else if (orgNo.Length == 9 && orgNo.Substring(6, 3) == "000")//获取所有县下属的乡
            {
                DataRow[] drLink = dtLink.Select("BYORGNO = '" + orgNo + "'", "");
                for (int i = 0; i < drLink.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<font title={0}>", drLink[i]["PHONE"].ToString());
                    sb.AppendFormat("{0}[{1}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString());
                    JObject root = new JObject { 
                        { "id", drLink[i]["PHONE"].ToString() },
                        { "text", sb.ToString()} ,
                        { "flag", drLink[i]["NAME"].ToString()} 
                    };
                    childArray.Add(root);
                }
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + orgNo.Substring(0, 6) + "' AND ORGNO<>'" + orgNo + "'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {
                    JObject root = new JObject
                     {
                         {"id",""},//ORGNO
                         {"text",drOrg[i]["ORGNAME"].ToString()},
                         {"state","closed"}
                     };
                    root.Add("children", getTreeSMSUserChild(dtOrg, dtLink, dtVillagecommittee, drOrg[i]["ORGNO"].ToString()));//继续获取自然村村委会
                    childArray.Add(root);
                }
                return childArray;
            }
            else if (orgNo.Length == 9 && orgNo.Substring(6, 3) != "000")//获取乡下面的村委会
            {

                DataRow[] drLink = dtLink.Select("BYORGNO = '" + orgNo + "'", "");
                for (int i = 0; i < drLink.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<font title={0}>", drLink[i]["PHONE"].ToString());
                    sb.AppendFormat("{0}[{1}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString());
                    JObject root = new JObject { 
                        { "id", drLink[i]["PHONE"].ToString() },
                        { "text", sb.ToString()} ,
                        { "flag", drLink[i]["NAME"].ToString()} 
                    };
                    childArray.Add(root);
                }

                DataRow[] drVillagecommittee = dtVillagecommittee.Select("BYORGNO = '" + orgNo + "'", "");//获取乡下面的村委会
                if (drVillagecommittee.Length > 0)
                {
                    for (int i = 0; i < drVillagecommittee.Length; i++)
                    {
                        JObject root = new JObject
                     {
                         {"id",""},//ORGNO
                         {"text",drVillagecommittee[i]["CWHNAME"].ToString()},
                         {"state","closed"}
                     };
                        root.Add("children", getTreeSMSUserChild(dtOrg, dtLink, dtVillagecommittee, drVillagecommittee[i]["CWHID"].ToString()));//继续获取自然村下面的人员
                        childArray.Add(root);
                    }
                }

                return childArray;
            }

            else
            {
                DataRow[] drLink = dtLink.Select("BYORGNO = '" + orgNo + "'", "");
                if (drLink.Length > 0)
                {
                    for (int i = 0; i < drLink.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<font title={0}>", drLink[i]["PHONE"].ToString());
                        if (string.IsNullOrEmpty(drLink[i]["UNITNAME"].ToString()) == false)
                        {
                            sb.AppendFormat("{0}[{1}][{2}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString(), drLink[i]["UNITNAME"].ToString());
                        }
                        else
                        {
                            sb.AppendFormat("{0}[{1}][{2}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString(), "--");
                        }
                        JObject root = new JObject
                     {
                         {"id",drLink[i]["PHONE"].ToString()},//ORGNO
                         { "text", sb.ToString()} ,
                         { "flag", drLink[i]["NAME"].ToString()}
                     };
                        childArray.Add(root);
                    }
                }
                return childArray;
            }


        }

        #endregion

        #region 短信发送组合人员树形菜单
        /// <summary>
        /// 短信发送组合人员树形菜单
        /// </summary>
        /// <param name="OrgNo">组织机构码</param>
        /// <param name="type">type</param>
        /// <returns>参见模型</returns>
        public static string getSMSUserTree(string OrgNo, string type)
        {
            JArray jObjects = new JArray();
            string curUserOrg = SystemCls.getCurUserOrgNo();//获取当前登录用户的机构编码
            if (string.IsNullOrEmpty(OrgNo) == false)
                curUserOrg = OrgNo;
            bool orgflag = false;//组织机构标志
            bool hlyflag = false;//护林员标志
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = curUserOrg, SYSFLAG = ConfigCls.getSystemFlag() });//获取当前登录用户有权限查看的单位
            DataTable dtLink = BaseDT.T_SYS_ORG_LINK.getDT(new T_SYS_ORG_LINK_SW { });
            DataTable dtVillagecommittee = BaseDT.T_SYS_ORG_CWH.getDT(new T_SYS_ORG_CWH_SW { BYORGNO = OrgNo });
            DataTable dtFRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", BYORGNO = curUserOrg, });//获取所有有权限查看的护林员
            if (type == "0")
            {
                orgflag = true;
                hlyflag = true;
            }
            else if (type == "1")
            {
                orgflag = true;
            }
            else if (type == "2")
            {
                hlyflag = true;
            }
            //获取所有人员
            DataRow[] drOrg = dtOrg.Select("", "ORGNO");
            if (orgflag == true)
            {
                JObject root = new JObject 
                { 
                 { "id", "" }, 
                 { "text", "组织机构成员" } 
                };
                JArray childArrayroot = new JArray();
                JObject root1 = new JObject 
                { 
                 { "id", "" }, 
                 { "text", drOrg[0]["ORGNAME"].ToString() } ,
                 {"state","closed"}
                };
                childArrayroot.Add(root1);
                root.Add("children", childArrayroot);
                root1.Add("children", getTreeSMSUserChild(dtOrg, dtLink, dtVillagecommittee, drOrg[0]["ORGNO"].ToString()));
                jObjects.Add(root);
            }
            if (hlyflag == true)
            {
                JObject root2 = new JObject 
                { 
                 { "id", ""}, 
                 { "text", "护林员" } 
                };
                JArray childArrayroot2 = new JArray();
                JObject root3 = new JObject 
                { 
                 { "id", "" }, 
                 { "text", drOrg[0]["ORGNAME"].ToString() } ,
                 {"state","closed"}
                };
                childArrayroot2.Add(root3);
                root2.Add("children", childArrayroot2);
                root3.Add("children", getFRUserTreeChild(dtOrg, dtFRUser, drOrg[0]["ORGNO"].ToString()));
                jObjects.Add(root2);
            }

            dtOrg.Clear();
            dtOrg.Dispose();
            dtLink.Clear();
            dtLink.Dispose();
            dtFRUser.Clear();
            dtFRUser.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }
        #endregion

        #region 短信发送组合人员树形菜单测试
        /// <summary>
        /// 短信发送组合人员树形菜单测试
        /// </summary>
        /// <param name="OrgNo">组织机构码</param>
        /// <param name="type">type</param>
        /// <returns>参见模型</returns>
        public static string gettestUserTree(string OrgNo, string type)
        {
            JArray jObjects = new JArray();
            //string[] arr ;
            string treetype = "";
            string curUserOrg = SystemCls.getCurUserOrgNo();//获取当前登录用户的机构编码
            if (string.IsNullOrEmpty(OrgNo) == false)
            {
                string[] arr = OrgNo.Split('|');
                curUserOrg = arr[0];
                treetype = arr[1];
            }
            //curUserOrg = OrgNo;
            bool orgflag = false;//组织机构标志
            bool hlyflag = false;//护林员标志
            var dtOrg = new DataTable();
            if (PublicCls.OrgIsShi(curUserOrg) || PublicCls.OrgIsXian(curUserOrg) || PublicCls.OrgIsZhen(curUserOrg))
            {
                dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = curUserOrg, SYSFLAG = ConfigCls.getSystemFlag() });//获取当前登录用户有权限查看的单位
            }
            DataTable dtLink = BaseDT.T_SYS_ORG_LINK.getDT(new T_SYS_ORG_LINK_SW { });
            DataTable dtVillagecommittee = BaseDT.T_SYS_ORG_CWH.getDT(new T_SYS_ORG_CWH_SW { });
            DataTable dtFRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", BYORGNO = curUserOrg, });//获取所有有权限查看的护林员
            if (type == "0")
            {
                orgflag = true;
                hlyflag = true;
            }
            else if (type == "1")
            {
                orgflag = true;
            }
            else if (type == "2")
            {
                hlyflag = true;
            }
            //获取所有人员
            if (orgflag == true)
            {
                JObject root = new JObject 
                 { 
                  { "id", "1"+"|" + "1" }, 
                   { "text", "组织机构成员" } 
                 };
                #region 市级用户
                if (PublicCls.OrgIsShi(curUserOrg))
                {
                    DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                    JArray childArrayroot = new JArray();
                    JObject root1 = new JObject 
                   {   
                   { "id", drOrg[0]["ORGNO"].ToString() + "|" + "1"}, 
                   { "text", drOrg[0]["ORGNAME"].ToString()},
                   {"treeType","1"},
                   { "flag","" }, 
                   {"state","closed"}
                   };
                    childArrayroot.Add(root1);
                    root.Add("children", childArrayroot);
                    JArray childArray = new JArray();
                    DataRow[] drLink = dtLink.Select("BYORGNO = '" + curUserOrg + "'", "");
                    for (int i = 0; i < drLink.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<font title={0}>", drLink[i]["PHONE"].ToString());
                        sb.AppendFormat("{0}[{1}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString());
                        JObject roota = new JObject 
                        { 
                        { "id", drLink[i]["PHONE"].ToString()},
                        { "text",sb.ToString() } ,
                        { "flag",drLink[i]["NAME"].ToString() }, 
                        { "phone",drLink[i]["PHONE"].ToString() }
                        };
                        childArray.Add(roota);
                    }
                    DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + curUserOrg.Substring(0, 4) + "' AND ORGNO<>'" + curUserOrg + "' and SUBSTRING(ORGNO,7,9)='000000000'", "");//获取所有县且〈〉市的
                    for (int i = 0; i < drOrgC.Length; i++)
                    {
                        JObject rootb = new JObject
                     {
                         {"id",drOrgC[i]["ORGNO"].ToString() + "|" + "1"},//ORGNO
                         {"text",drOrgC[i]["ORGNAME"].ToString()},
                         {"flag","" }, 
                         {"state","closed"},
                     };
                        childArray.Add(rootb);
                    }
                    root1.Add("children", childArray);
                    jObjects.Add(root);
                }

                #endregion

                #region 县级用户
                else if (PublicCls.OrgIsXian(curUserOrg))
                {
                    DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                    JArray childArrayroot = new JArray();
                    JObject root1 = new JObject 
                  { 
                   { "id", drOrg[0]["ORGNO"].ToString() + "|" + "1"}, 
                   { "text", drOrg[0]["ORGNAME"].ToString() } ,
                   { "flag","" }, 
                   {"state","closed"}
                  };
                    childArrayroot.Add(root1);
                    root.Add("children", childArrayroot);
                    JArray jObjectsC = new JArray();
                    DataRow[] drLink = dtLink.Select("BYORGNO = '" + curUserOrg + "'", "");
                    for (int i = 0; i < drLink.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<font title={0}>", drLink[i]["PHONE"].ToString());
                        sb.AppendFormat("{0}[{1}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString());
                        JObject rootc = new JObject
                        { 
                           { "id",drLink[i]["PHONE"].ToString()},
                           { "text",sb.ToString() } ,
                           { "flag",drLink[i]["NAME"].ToString() }, 
                           { "phone",drLink[i]["PHONE"].ToString() }
                        };
                        jObjectsC.Add(rootc);
                        if (string.IsNullOrEmpty(OrgNo) == false && treetype == "1")//异步加载，不显示县名
                        {
                            jObjects.Add(rootc);
                        }
                    }
                    DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + curUserOrg.Substring(0, 6) + "' AND ORGNO<>'" + curUserOrg + "' and SUBSTRING(ORGNO,10,6)='000000'", ""); //获取所有镇且〈〉县的
                    for (int i = 0; i < drOrgC.Length; i++)
                    {
                        JObject rootd = new JObject
                        {
                         {"id",drOrgC[i]["ORGNO"].ToString() + "|" + "1"},//ORGNO
                         {"text",drOrgC[i]["ORGNAME"].ToString()},
                         {"flag","" }, 
                         {"state","closed"} ,
                       };
                        jObjectsC.Add(rootd);
                        if (string.IsNullOrEmpty(OrgNo) == false && treetype == "1")//异步加载，不显示县名
                        {
                            jObjects.Add(rootd);
                        }
                    }
                    if (string.IsNullOrEmpty(OrgNo))//县级用户登录
                    {
                        jObjects.Add(root);
                        root1.Add("children", jObjectsC);
                    }
                }
                #endregion

                #region 乡镇级用户
                else if (PublicCls.OrgIsZhen(curUserOrg))
                {
                    DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                    JArray childArrayroot = new JArray();
                    JObject root1 = new JObject 
                  { 
                   { "id", drOrg[0]["ORGNO"].ToString() + "|" + "1"}, 
                   { "text", drOrg[0]["ORGNAME"].ToString() } ,
                   { "flag","" }, 
                   {"treeType","1"},
                  };
                    childArrayroot.Add(root1);
                    root.Add("children", childArrayroot);
                    JArray childArray = new JArray();
                    DataRow[] drLink = dtLink.Select("BYORGNO = '" + curUserOrg + "'", "");
                    for (int i = 0; i < drLink.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<font title={0}>", drLink[i]["PHONE"].ToString());
                        sb.AppendFormat("{0}[{1}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString());
                        JObject rootf = new JObject
                        { 
                           { "id", drLink[i]["PHONE"].ToString()},
                           { "text",sb.ToString() } ,
                           { "flag",drLink[i]["NAME"].ToString() }, 
                           { "phone",drLink[i]["PHONE"].ToString() }
                        };
                        childArray.Add(rootf);
                        if (string.IsNullOrEmpty(OrgNo) == false && treetype == "1")//异步加载
                        {
                            jObjects.Add(rootf);
                        }
                    }
                    DataRow[] drVillagecommittee = dtVillagecommittee.Select("BYORGNO = '" + curUserOrg + "'", "");//获取乡下面的村委会
                    for (int i = 0; i < drVillagecommittee.Length; i++)
                    {
                        JObject rootg = new JObject
                     {
                         {"id",drVillagecommittee[i]["CWHID"].ToString() + "|" + "1"},//ORGNO
                         {"text",drVillagecommittee[i]["CWHNAME"].ToString()},
                         {"state","closed"}
                     };
                        childArray.Add(rootg);
                        //JArray childArray1 = new JArray();
                        //DataRow[] drLink1 = dtLink.Select("BYORGNO = '" + drVillagecommittee[i]["CWHID"].ToString() + "'", "");//获取村委会下面的人员
                        //for (int j = 0; j < drLink1.Length; j++)
                        //{
                        //    StringBuilder sb = new StringBuilder();
                        //    sb.AppendFormat("<font title={0}>", drLink1[j]["PHONE"].ToString());
                        //    if (string.IsNullOrEmpty(drLink1[j]["UNITNAME"].ToString()) == false)
                        //    {
                        //        sb.AppendFormat("{0}[{1}][{2}] </font>", drLink1[j]["NAME"].ToString(), drLink1[j]["USERJOB"].ToString(), drLink1[j]["UNITNAME"].ToString());
                        //    }
                        //    else
                        //    {
                        //        sb.AppendFormat("{0}[{1}][{2}] </font>", drLink1[j]["NAME"].ToString(), drLink1[j]["USERJOB"].ToString(), "--");
                        //    }
                        //    JObject rooth = new JObject
                        // {
                        //     {"id",drLink1[j]["BYORGNO"].ToString()},//ORGNO
                        //     { "text", sb.ToString()} ,
                        //     { "flag", drLink1[j]["NAME"].ToString()},
                        //     { "phone",drLink1[j]["PHONE"].ToString() }
                        // };
                        //    childArray1.Add(rooth);
                        //}
                        //rootg.Add("children", childArray1);
                        if (string.IsNullOrEmpty(OrgNo) == false && treetype == "1")//异步加载
                        {
                            jObjects.Add(rootg);
                        }
                    }
                    if (string.IsNullOrEmpty(OrgNo))//乡镇用户登录
                    {
                        jObjects.Add(root);
                        root1.Add("children", childArray);
                    }
                }
                else
                {
                    JArray childArray = new JArray();
                    DataRow[] drLink = dtLink.Select("BYORGNO = '" + curUserOrg + "'", "");
                    for (int i = 0; i < drLink.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<font title={0}>", drLink[i]["PHONE"].ToString());
                        if (string.IsNullOrEmpty(drLink[i]["UNITNAME"].ToString()) == false)
                        {
                            sb.AppendFormat("{0}[{1}][{2}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString(), drLink[i]["UNITNAME"].ToString());
                        }
                        else
                        {
                            sb.AppendFormat("{0}[{1}][{2}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString(), "--");
                        }
                        JObject rootf = new JObject
                        { 
                           { "id", drLink[i]["PHONE"].ToString()},
                           { "text",sb.ToString() } ,
                           { "flag",drLink[i]["NAME"].ToString() }, 
                           { "phone",drLink[i]["PHONE"].ToString() }
                        };
                        childArray.Add(rootf);
                        if (string.IsNullOrEmpty(OrgNo) == false && treetype == "1")//异步加载
                        {
                            jObjects.Add(rootf);
                        }
                    }
                }
                #endregion
            }
            if (hlyflag == true)
            {
                JObject root2 = new JObject 
                { 
                 { "id", "2"+"|" + "2"}, 
                 { "text", "护林员" } 
                };
                #region 市级用户
                if (PublicCls.OrgIsShi(curUserOrg))
                {
                    DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                    JArray childArrayroot = new JArray();
                    JObject root3 = new JObject 
                   {   
                   { "id", drOrg[0]["ORGNO"].ToString() + "|" + "2"}, 
                   { "text", drOrg[0]["ORGNAME"].ToString()},
                   {"treeType","2"},
                 
                   {"state","closed"}
                   };
                    childArrayroot.Add(root3);
                    root2.Add("children", childArrayroot);
                    JArray childArray = new JArray();
                    DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + curUserOrg.Substring(0, 4) + "' AND ORGNO<>'" + curUserOrg + "' and SUBSTRING(ORGNO,7,9)='000000000'", "");//获取所有县且〈〉市的
                    for (int i = 0; i < drOrgC.Length; i++)
                    {
                        JObject rooth = new JObject
                     {
                         {"id",drOrgC[i]["ORGNO"].ToString() + "|" + "2"},//ORGNO
                         {"text",drOrgC[i]["ORGNAME"].ToString()},
                         {"treeType","2"},
                         {"flag","" }, 
                         {"state","closed"},
                     };
                        childArray.Add(rooth);
                    }
                    root3.Add("children", childArray);
                    jObjects.Add(root2);
                }
                #endregion

                #region 县级用户
                else if (PublicCls.OrgIsXian(curUserOrg))
                {
                    DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                    JArray childArrayroot = new JArray();
                    JObject root3 = new JObject 
                   {   
                   { "id", drOrg[0]["ORGNO"].ToString() + "|" + "2"}, 
                   { "text", drOrg[0]["ORGNAME"].ToString()},
                   {"treeType","2"},
              
                   {"state","closed"}
                   };
                    childArrayroot.Add(root3);
                    root2.Add("children", childArrayroot);
                    JArray jObjectsC = new JArray();
                    DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + curUserOrg.Substring(0, 6) + "' AND ORGNO<>'" + curUserOrg + "' and SUBSTRING(ORGNO,10,6)='000000'", ""); //获取所有镇且〈〉县的
                    for (int i = 0; i < drOrgC.Length; i++)
                    {
                        JObject rooti = new JObject
                        {
                         {"id",drOrgC[i]["ORGNO"].ToString() + "|" + "2"},//ORGNO
                         {"text",drOrgC[i]["ORGNAME"].ToString()},
                         {"treeType","2"},
                         {"state","closed"} ,
                       };
                        jObjectsC.Add(rooti);
                        if (string.IsNullOrEmpty(OrgNo) == false && treetype == "2")//异步加载，不显示县名
                        {
                            jObjects.Add(rooti);
                        }
                    }
                    if (string.IsNullOrEmpty(OrgNo))//县级用户登录
                    {
                        jObjects.Add(root2);
                        root3.Add("children", jObjectsC);
                    }
                }
                #endregion

                #region 乡镇用户
                if (PublicCls.OrgIsZhen(curUserOrg))
                {
                    DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                    JArray childArrayroot = new JArray();
                    JObject root3 = new JObject 
                   {   
                   { "id", drOrg[0]["ORGNO"].ToString() + "|" + "2"}, 
                   { "text", drOrg[0]["ORGNAME"].ToString()},
                   {"treeType","2"},
      
                   {"state","closed"}
                   };
                    childArrayroot.Add(root3);

                    root2.Add("children", childArrayroot);
                    DataRow[] drFRUser = dtFRUser.Select("BYORGNO = '" + curUserOrg + "'", "");//获取乡镇下的护林员
                    JArray jObjectsC = new JArray();
                    for (int i = 0; i < drFRUser.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<font title={0}>", drFRUser[i]["PHONE"].ToString());
                        sb.AppendFormat("{0}</font>", drFRUser[i]["HNAME"].ToString());
                        JObject rootj = new JObject
                     {
                         {"id",drFRUser[i]["PHONE"].ToString()} ,
                         {"text",sb.ToString()},
                         {"flag", drFRUser[i]["HNAME"].ToString()},
                         {"phone",drFRUser[i]["PHONE"].ToString() }
                     };
                        jObjectsC.Add(rootj);
                        if (string.IsNullOrEmpty(OrgNo) == false && treetype == "2")//异步加载，不显示县名
                        {
                            jObjects.Add(rootj);
                        }
                    }
                    if (string.IsNullOrEmpty(OrgNo))//乡镇级用户登录
                    {
                        jObjects.Add(root2);
                        root3.Add("children", jObjectsC);
                    }
                }
                #endregion

            }
            dtOrg.Clear();
            dtOrg.Dispose();
            dtLink.Clear();
            dtLink.Dispose();
            dtFRUser.Clear();
            dtFRUser.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }
        #endregion

        #endregion
    }
}
