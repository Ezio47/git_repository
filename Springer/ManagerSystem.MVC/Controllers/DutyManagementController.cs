using ManagerSystem.MVC.HelpCom;
using ManagerSystemClassLibrary;
using ManagerSystemClassLibrary.DUTY;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    public class DutyManagementController : BaseController
    {
        #region 值班处理
        /// <summary>
        /// 值班处理
        /// </summary>
        /// <returns></returns>
        public ActionResult DutyHandle()
        {
            pubViewBag("022001", "022001", "");
            ViewBag.orgNameTrue = T_SYS_ORGCls.getModel(new T_SYS_ORGSW { ORGNO = SystemCls.getCurUserOrgNo() }).ORGNAME;

            DUTY_HANDOVER_Model m = DUTY_HANDOVERCls.getModel(new DUTY_HANDOVER_SW { BYORGNO = SystemCls.getCurUserOrgNo(), DUTYTYPE = "-3", DUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now) });//获取日志
            if (m.OPCONTENT != null)
            {
                ViewBag.dayLog = m.OPCONTENT;
            }
            else
            {
                var list = JC_FIRECls.GetListModel(new JC_FIRE_SW { BYORGNO = SystemCls.getCurUserOrgNo(), FIRETIME = DateTime.Now.ToString("yyyy-MM-dd") });
                //var list = JC_FIRECls.GetListModel(new JC_FIRE_SW { BYORGNO = "532523000", FIRETIME = "2017-03-24" });
                if (list.Count() > 0)
                {
                    ViewBag.dayLog = "当前有" + list.Count() + "个火情";
                }
            }
            return View();
        } 
        #endregion

        #region 获取各班次信息
        public ActionResult getClass()
        {
            string str = Request.Params["tm"];
            if (string.IsNullOrEmpty(str))
                str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            StringBuilder sb = new StringBuilder();
            string[] arr = PublicClassLibrary.ClsStr.getWeeks(str);
            var listW = DUTY_CLASSCls.GetListModel(new DUTY_CLASS_SW { BYORGNO = SystemCls.getCurUserOrgNo() });// 班次情况
            var id = listW.Select(p => p.DUTYCLASSID).ToList();
            string class1 = "";
            int CurClass = 0;//用于判断当前班次
            DateTime dtClass = Convert.ToDateTime(str);
            dtClass = dtClass.AddHours(1);
            IEnumerable<DUTY_CLASS_Model> query = from items in listW orderby items.DUTYBEGINTIME descending select items;
            string[] arrClassInfo = new string[4];//用于存放班次信息 为了与后面保持一致，index=0用不到
            if (listW.Count() > 0)
            {

                foreach (var v in listW)
                {
                    DateTime dt1 = DateTime.Parse(dtClass.ToString("yyyy-MM-dd abc:00").Replace("abc", v.DUTYBEGINTIME));
                    DateTime dt2 = DateTime.Parse(dtClass.ToString("yyyy-MM-dd abc:00").Replace("abc", v.DUTYENDTIME));
                    if (dt1 > dt2)
                        dt2 = dt2.AddDays(1);
                    if (dt1 < dt2)
                    {
                        if (dtClass > dt1 && dtClass <= dt2)
                        {
                            CurClass = Convert.ToInt32(v.DUTYCLASSID);
                        }
                    }

                    var i = Convert.ToInt32(v.DUTYCLASSID);
                    if (v.DUTYCLASSID == "1")
                    {
                        arrClassInfo[i] = v.DUTYCLASSNAME + "(" + v.DUTYBEGINTIME + "～" + v.DUTYENDTIME + ")";
                        class1 += v.DUTYCLASSID + ",";
                    }
                    else if (v.DUTYCLASSID == "2")
                    {
                        arrClassInfo[i] = v.DUTYCLASSNAME + "(" + v.DUTYBEGINTIME + "～" + v.DUTYENDTIME + ")";
                        class1 += v.DUTYCLASSID + ",";
                    }
                    else
                    {
                        arrClassInfo[i] = v.DUTYCLASSNAME + "(" + v.DUTYBEGINTIME + "～" + v.DUTYENDTIME + ")";
                        class1 += v.DUTYCLASSID + ",";
                    }
                }
            }
            else {
                return Content(JsonConvert.SerializeObject(new Message(false, "", "")), "text/html;charset=UTF-8");
            }
            sb.AppendFormat("<table cellpadding='0' cellspacing='0'>");
            string[,] arrC = new string[3, 3];
            string[] strID = class1.Split(',');//早 中 晚的顺序
            if (strID.Length > 0)
            {
                if (strID.Length == 2)//只有一个班次
                {
                    var userid = Convert.ToInt32(strID[0]);
                    arrC[0, 0] = strID[0];
                    arrC[0, 1] = dtClass.AddDays(-2).ToString("yyyy-MM-dd");//日期
                    arrC[0, 2] = arrClassInfo[userid];//班次名称
                    arrC[1, 0] = strID[0];
                    arrC[1, 1] = dtClass.AddDays(-1).ToString("yyyy-MM-dd");
                    arrC[1, 2] = arrClassInfo[userid];
                    arrC[2, 0] = strID[0];// 当前班
                    arrC[2, 1] = dtClass.ToString("yyyy-MM-dd");
                    arrC[2, 2] = arrClassInfo[userid];
                }
                else if (strID.Length == 3)//两个班次
                {
                    var userid1 = Convert.ToInt32(strID[0]);
                    var userid2 = Convert.ToInt32(strID[1]);
                    if (CurClass == userid1)
                    {
                        arrC[0, 0] = strID[0];//早班  早班  中班
                        arrC[0, 1] = dtClass.AddDays(-1).ToString("yyyy-MM-dd");//日期
                        arrC[0, 2] = arrClassInfo[userid1];//班次名称
                        arrC[1, 0] = strID[1];//中班  晚班  晚班
                        arrC[1, 1] = dtClass.AddDays(-1).ToString("yyyy-MM-dd");
                        arrC[1, 2] = arrClassInfo[userid2];
                        arrC[2, 0] = strID[0];//早班  早班  中班当前班
                        arrC[2, 1] = dtClass.ToString("yyyy-MM-dd");
                        arrC[2, 2] = arrClassInfo[userid1];
                    }
                    else
                    {
                        arrC[0, 0] = strID[1];//中班  晚班  晚班
                        arrC[0, 1] = dtClass.AddDays(-1).ToString("yyyy-MM-dd");//日期
                        arrC[0, 2] = arrClassInfo[userid2];//班次名称
                        arrC[1, 0] = strID[0];//早班  早班  中班
                        arrC[1, 1] = dtClass.ToString("yyyy-MM-dd");
                        arrC[1, 2] = arrClassInfo[userid1];
                        arrC[2, 0] = strID[1];//中班  晚班  晚班 当前班
                        arrC[2, 1] = dtClass.ToString("yyyy-MM-dd");
                        arrC[2, 2] = arrClassInfo[userid2];
                    }
                }
                else
                {
                    //三个班次
                    var userid1 = Convert.ToInt32(strID[0]);
                    var userid2 = Convert.ToInt32(strID[1]);
                    var userid3 = Convert.ToInt32(strID[2]);
                    if (CurClass == userid1)
                    {
                        arrC[0, 0] = strID[1];//中班
                        arrC[0, 1] = dtClass.AddDays(-1).ToString("yyyy-MM-dd");//日期
                        arrC[0, 2] = arrClassInfo[userid2];//班次名称
                        arrC[1, 0] = strID[2];//晚班
                        arrC[1, 1] = dtClass.AddDays(-1).ToString("yyyy-MM-dd");
                        arrC[1, 2] = arrClassInfo[userid3];
                        arrC[2, 0] = strID[0];//早班 当前班
                        arrC[2, 1] = dtClass.ToString("yyyy-MM-dd");
                        arrC[2, 2] = arrClassInfo[userid1];
                    }
                    else if (CurClass == userid2)
                    {
                        arrC[0, 0] = strID[2];//晚班
                        arrC[0, 1] = dtClass.AddDays(-1).ToString("yyyy-MM-dd");//日期
                        arrC[0, 2] = arrClassInfo[userid3];//班次名称
                        arrC[1, 0] = strID[0];//早班
                        arrC[1, 1] = dtClass.ToString("yyyy-MM-dd");
                        arrC[1, 2] = arrClassInfo[userid1];
                        arrC[2, 0] = strID[1];//中班 当前班
                        arrC[2, 1] = dtClass.ToString("yyyy-MM-dd");
                        arrC[2, 2] = arrClassInfo[userid2];
                    }
                    else {
                        arrC[0, 0] = strID[0];//早班
                        arrC[0, 1] = dtClass.ToString("yyyy-MM-dd");//日期
                        arrC[0, 2] = arrClassInfo[userid1];//班次名称
                        arrC[1, 0] = strID[1];//中班
                        arrC[1, 1] = dtClass.ToString("yyyy-MM-dd");
                        arrC[1, 2] = arrClassInfo[userid2];
                        arrC[2, 0] = strID[2];//晚班 当前班
                        arrC[2, 1] = dtClass.ToString("yyyy-MM-dd");
                        arrC[2, 2] = arrClassInfo[userid3];
                    }                
                }
            }

            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th colspan='1' style='width:33.333%'>{0}</th>", arrC[0, 2]);
            sb.AppendFormat("<th colspan='1' style='width:33.333%'>{0}</th>", arrC[1, 2]);
            sb.AppendFormat("<th colspan='1' class='DCThCur' style='width:33.333%'>{0}</th>", arrC[2, 2]);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            //sb.AppendFormat(getClassTHInfo(class1, arrClassInfo));//班次情况

            sb.AppendFormat("<body>");
            sb.AppendFormat("<tr style='height:120px;'>");

            sb.AppendFormat(getClassInfo(false, arrC[0, 0], arrC[0, 1]));//组合第一班次
            sb.AppendFormat(getClassInfo(false, arrC[1, 0], arrC[1, 1]));//组合第二班次
            sb.AppendFormat(getClassInfo(true, arrC[2, 0], arrC[2, 1]));//组合第三班次
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</body>");
            sb.AppendFormat("</table>");
            //sb.AppendFormat("<hr>");
            Message ms = new Message(true, sb.ToString(), "");
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
            // return sb.ToString();
        }

        #endregion

        #region 获取各班次详细信息
        /// <summary>
        /// 获取各班次详细信息
        /// </summary>
        /// <param name="isCurClass"></param>
        /// <param name="dcClass"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string getClassInfo(bool isCurClass, string dcClass, string dt)
        {
            StringBuilder sb = new StringBuilder();
            //var result = O_OD_USERCls.GetListModel(new O_ODUSER_Model { BigONDUTYDATE = dt, EndONDUTYDATE = dt, BYORGNO = SystemCls.getCurUserOrgNo(), FLag = "false" });

            DUTY_USER_SW sw = new DUTY_USER_SW();
            sw.DTBegin = dt;
            sw.DTEnd = dt;
            sw.BYORGNO = SystemCls.getCurUserOrgNo();
            var result = DUTY_USERCls.getListModel(sw);

            string isQD = "0";//是否签到 0非值班人员
            string isLD = "0";//是否领导 0非领导
            string CurUserID = SystemCls.getUserID();//当前登录用户
            var str1 = result.Where(p => p.DUTYUSERTYPE == dcClass).ToList();
            string Tmp = "";
            foreach (var v in str1)
            {
                Tmp += (string.IsNullOrEmpty(Tmp)) ? "" : ",";
                Tmp += (v.ISATTENDED != "1") ? "<font color=red>" + v.USERNAME + "</font>" : v.USERNAME;
                if (CurUserID == v.DUTYUSERID)
                    isQD = (v.ISATTENDED == "0") ? "1" : "2";//值班人员?未签到:已签到
            }

            string TmpLD = "";//领导
            var str2 = result.Where(p => p.DUTYUSERTYPE == "-1").ToList();
            foreach (var v in str2)
            {
                string dd1 = (v.ISATTENDED != "1") ? "<font color=red>" + v.USERNAME + "</font>" : v.USERNAME;
                TmpLD = (string.IsNullOrEmpty(Tmp)) ? dd1 : "," + dd1;
                if (CurUserID == v.DUTYUSERID)//登录用户为当前领导
                    isLD = (v.ISATTENDED == "0") ? "1" : "2";//领导 ?未签到:已签到
            }

            sb.AppendFormat("<td valign='top'>");
            sb.AppendFormat("签到信息：{0}", Tmp);

            string jbxx = "";//交班信息
            string ldyj = "";//领导意见
            if (1 == 1)//获取交班信息
            {
                //var HandData = O_HANDOVERCls.GetMoldeList(new O_HANDOVER_Model { ONDUTYTYPE = "-1", ONDUTYUSERTYPE = dcClass, BYORGNO = SystemCls.getCurUserOrgNo(), ONDUTYDATE = dt, FLag = "false" });
                var HandData = DUTY_HANDOVERCls.getListModel(new DUTY_HANDOVER_SW { DUTYTYPE = "-1", DUTYUSERTYPE = dcClass, BYORGNO = SystemCls.getCurUserOrgNo(), DUTYDATE = dt });
                foreach (var item in HandData)
                {
                    jbxx = item.OPCONTENT;
                }
            }
            if (1 == 1)////获取领导意见  改动后暂时去掉 ONDUTYUSERTYPE = dcClass 这个条件
            {
                //var HandData = O_HANDOVERCls.GetMoldeList(new O_HANDOVER_Model { ONDUTYTYPE = "-2", ONDUTYUSERTYPE = dcClass, BYORGNO = SystemCls.getCurUserOrgNo(), ONDUTYDATE = dt, FLag = "false" });

                var HandData = DUTY_HANDOVERCls.getListModel(new DUTY_HANDOVER_SW { DUTYTYPE = "-2", DUTYUSERTYPE = dcClass, BYORGNO = SystemCls.getCurUserOrgNo(), DUTYDATE = dt });
                foreach (var item in HandData)
                {
                    ldyj = item.OPCONTENT;
                }
            }
            if (isQD != "0")//是否签到
            {
                if (isQD == "1")//0 非值班人员 1未签到 2已签到
                    sb.AppendFormat("<input type=button  value='值班员签到' class=\"btnWriteCss\" style='width:110px;padding:3px 10px;' onclick=\"zbyQD('{0}','{1}','{2}');\">&nbsp;", CurUserID, dt, dcClass);
                else
                {
                    if (string.IsNullOrEmpty(jbxx))//未填写交班信息
                        sb.AppendFormat("<input type=button  value='交班' class=\"btnWriteCss\" style='padding:3px 10px;' onclick=\"JB('{0}','{1}','{2}');\">&nbsp;", CurUserID, dt, dcClass);
                }
            }
            sb.AppendFormat("<br>交班信息:<br>{0}", (string.IsNullOrEmpty(jbxx)) ? "<font color=red>注意：未交班</font>" : jbxx);


            if (!string.IsNullOrEmpty(ldyj))
                sb.AppendFormat("<br>领导意见:<br>{0}", ldyj);
            if (isLD != "0")//值班领导
            {
                if (isLD == "1")//未签到
                    sb.AppendFormat("<input type=button value='领导签到' class=\"btnWriteCss\" style='width:100px;padding:3px 10px;' onclick=\"LDQD('{0}','{1}')\">&nbsp;", CurUserID, dt);
                else//已签到
                {
                    if (string.IsNullOrEmpty(ldyj))//未填写领导意见 
                        sb.AppendFormat("<input type=button value='签写领导意见' class=\"btnWriteCss\" style='width:120px;padding:3px 10px;' onclick=\"LDYJ('{0}','{1}','{2}')\">", CurUserID, dt, dcClass);
                }
            }
            var orgno = SystemCls.getCurUserOrgNo();
            if (PublicCls.OrgIsShi(orgno))//如果是州用户 换成蒙自市的
            {
                orgno = ConfigCls.getConfigValue("ProvincialCapital");
            }

            var m = YJ_WEATHERCls.getModel(new YJ_WEATHER_SW { WEATHERDATE = dt, BYORGNO = orgno });
            if (!string.IsNullOrEmpty(m.THIGH) && !string.IsNullOrEmpty(m.TLOWER) && !string.IsNullOrEmpty(m.P))
            {
                var tmp = m.TLOWER + "~" + m.THIGH;
                sb.AppendFormat("<br>气象信息:<br>{0}", "<font color=red>温度:"+ tmp +",雨量:"+ m.P +"</font>");
            }
            else {
                sb.AppendFormat("<br>气象信息:<br>{0}", "<font color=red>无</font>");
            }
            sb.AppendFormat("</td>");
            
            return sb.ToString();
        }

        #endregion

        #region 添加领导意见
        /// <summary>
        /// 添加领导意见
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveLeadAdd()
        {
            DUTY_HANDOVER_Model m = new DUTY_HANDOVER_Model();
            m.opMethod = "Add";
            m.DUTYDATE = Request.Params["DUTYDATE"];//时间
            m.BYORGNO = SystemCls.getCurUserOrgNo();//单位编码
            m.DUTYUSERTYPE = Request.Params["DUTYUSERTYPE"];//对应早中晚 那个班次
            m.DUTYTYPE = "-2";//-2代表领导意见
            m.DUTYUSERID = Request.Params["DUTYUSERID"];//领导人ID
            m.OPTIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now);//时间
            m.OPCONTENT = Request.Params["content"];//领导意见
            if (string.IsNullOrEmpty(m.OPCONTENT) == true)
                return Content(JsonConvert.SerializeObject(new Message(false, "领导意见内容为空，请输入对应的意见内容！", "")), "text/html;charset=UTF-8");
            return Content(JsonConvert.SerializeObject(DUTY_HANDOVERCls.Manager(m)), "text/html;charset=UTF-8");
        }

        #endregion

        #region 获取值班员、日报信息
        /// <summary>
        /// 获取值班员信息
        /// </summary>
        /// <returns></returns>
        public ActionResult getODUserInfo()
        {
            StringBuilder sb = new StringBuilder();

            ViewBag.trueName = SystemCls.getCookieInfo().trueName;
            sb.AppendFormat("当前用户：{0}", ViewBag.trueName);
            string curOrgNo = SystemCls.getCurUserOrgNo();//获取当前用户的组织机构编码
            sb.AppendFormat("&nbsp;&nbsp;所属单位：{0}", T_SYS_ORGCls.getModel(new T_SYS_ORGSW { ORGNO = curOrgNo }).ORGNAME);

            string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            DUTY_USER_Model m = DUTY_USERCls.getModel(new DUTY_USER_SW { DUTYUSERID = SystemCls.getUserID(), BYORGNO = curOrgNo, DUTYDATE = strDate });
            string zw = "非值班人员";
            switch (m.DUTYUSERTYPE)
            {
                case "-2":
                    zw = "总带班领导";
                    break;
                case "-1":
                    zw = "带班领导";
                    break;
                case "1":
                    zw = "值班员";
                    break;
                case "2":
                    zw = "值班员";
                    break;
                case "3":
                    zw = "值班员";
                    break;
                default:
                    break;
            }
            sb.AppendFormat("&nbsp;&nbsp;值班状态：{0}", zw);
            //判断日志是否添加
            if (DUTY_HANDOVERCls.isExists(new DUTY_HANDOVER_SW { DUTYDATE = strDate, BYORGNO = curOrgNo, DUTYTYPE = "-3" }) == true)
                sb.AppendFormat(" &nbsp;&nbsp;<input type=button style=\"width:87px;\" value='日报已填写' class=\"btnWriteCss\" onclick=\"$('#ww').window('open')\">");

            else
                sb.AppendFormat(" &nbsp;&nbsp;<input type=button style=\"width:80px;\" value='填写日报' class=\"btnWriteCss\" onclick=\"$('#ww').window('open')\">");

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        #endregion

        #region 添加日报
        /// <summary>
        /// 添加日报
        /// </summary>
        /// <returns></returns>
        public ActionResult dutyDailyAdd()
        {
            //ONDUTYDATE, BYORGNO, ONDUTYUSERTYPE, ONDUTYTYPE, ONDUTYUSERID, OPTIME, OPCONTENT
            DUTY_HANDOVER_Model m = new DUTY_HANDOVER_Model();
            m.opMethod = "Add";
            m.DUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);//日期
            m.BYORGNO = SystemCls.getCurUserOrgNo();//单位编码
            m.DUTYUSERTYPE = "0";//日报无班次，可设为默认0
            m.DUTYTYPE = "-3";//日报专属标识
            m.DUTYUSERID = SystemCls.getUserID();//当前用户ID
            m.OPTIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now);//时间
            m.OPCONTENT = Request.Params["content"];//内容
            if (string.IsNullOrEmpty(m.OPCONTENT) == true)
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入日报内容！", "")), "text/html;charset=UTF-8");
            return Content(JsonConvert.SerializeObject(DUTY_HANDOVERCls.Manager(m)), "text/html;charset=UTF-8");
        }

        #endregion

        #region 值班处理签到方法
        /// <summary>
        /// 值班处理签到方法
        /// </summary>
        /// <returns></returns>
        public ActionResult SignAgien()
        {
            DUTY_USER_Model m = new DUTY_USER_Model();
            m.DUTYDATE = Request.Params["ondutyDate"];//当前值班日期
            m.BYORGNO = SystemCls.getCurUserOrgNo();//获取当前用户的组织机构编码
            m.DUTYUSERID = Request.Params["strUserId"]; //用户ID
            m.DUTYUSERTYPE = Request.Params["ondutyType"];//当前用户值班类别
            m.opMethod = "Sign";//签到方法
            Message msSign = DUTY_USERCls.Manager(m);
            if (msSign.Success == true && m.DUTYUSERTYPE != "-1")//签到成功，判断是否迟到 领导签到不需要验证
            {
                if (DUTY_CLASSCls.isLate(new DUTY_CLASS_SW { DUTYCLASSID = m.DUTYUSERTYPE }) == false)//未迟到
                    return Content(JsonConvert.SerializeObject(msSign), "text/html;charset=UTF-8");
                else
                {
                    msSign.Msg = "您已迟到！";
                    return Content(JsonConvert.SerializeObject(msSign), "text/html;charset=UTF-8");
                }

            }
            return Content(JsonConvert.SerializeObject(msSign), "text/html;charset=UTF-8");
        }

        #endregion

        #region 交班的时候判断是否有下一班次的接班人，没有禁止交班
        /// <summary>
        /// 交班的时候判断是否有下一班次的接班人，没有禁止交班
        /// </summary>
        /// <returns></returns>
        public ActionResult getclassNest()
        {
            DUTY_HANDOVER_Model m = new DUTY_HANDOVER_Model();
            m.DUTYDATE = Request.Params["dt"];// PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);//日期 
            m.BYORGNO = SystemCls.getCurUserOrgNo();//单位编码
            m.DUTYUSERTYPE = Request.Params["dcClass"];//当前班次
            m.DUTYTYPE = "-1";//交班信息
            m.DUTYUSERID = SystemCls.getUserID();//当前用户ID
            m.OPTIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now);//当前时间
            if (DUTY_CLASSCls.isEarlyOut(new DUTY_CLASS_SW { DUTYCLASSID = m.DUTYUSERTYPE, judgeDate = m.DUTYDATE }) == true)
                return Content(JsonConvert.SerializeObject(new Message(false, "未到交班时间，早退,禁止交班！", "")), "text/html;charset=UTF-8");

            var dataList = DUTY_USERCls.getNextClassListModel(new DUTY_USER_SW { ISATTENDED = "1", DUTYDATE = m.DUTYDATE, BYORGNO = m.BYORGNO, DUTYUSERTYPE = m.DUTYUSERTYPE });
            StringBuilder signSb = new StringBuilder();
            signSb.AppendFormat("<select id=\"s1\">");
            if (dataList.Any())
            {
                foreach (var item in dataList)
                {
                    signSb.AppendFormat("<option value='{0}'>{1}</option>", item.DUTYUSERID, item.USERNAME);
                }
            }
            else
            {
                signSb.AppendFormat("<option value='{0}'>{1}</option>", "-1", "");
            }

            signSb.AppendFormat("</select>");
            return Content(JsonConvert.SerializeObject(new Message(true, signSb.ToString(), "")), "text/html;charset=UTF-8");
        }

        #endregion

        #region 交班时获取上交班信息
        /// <summary>
        /// 交班时获取上交班信息
        /// </summary>
        /// <returns></returns>
        public ActionResult getclassJbsx()
        {
            DUTY_HANDOVER_SW sw = new DUTY_HANDOVER_SW();
            sw.DUTYDATE = Request.Params["dt"];//值班日期
            sw.BYORGNO = SystemCls.getCurUserOrgNo();//单位编码
            sw.DUTYUSERTYPE = Request.Params["dcClass"];//当前班次
            sw.DUTYTYPE = "-1";//交班信息
            sw.isGetUPOne = "1";//查询上一班次
            DUTY_HANDOVER_Model m = DUTY_HANDOVERCls.getModel(sw);
            if (m.OPCONTENT == null)
            {
                m.OPCONTENT = "无交班信息";
            }
            m.OPCONTENT = m.OPCONTENT.Replace("<br>", "\n");
            return Content(JsonConvert.SerializeObject(new Message(true, m.OPCONTENT, "")), "text/html;charset=UTF-8");
        }

        #endregion

        #region 交班保存
        /// <summary>
        /// 交班保存
        /// </summary>
        /// <returns></returns>
        public ActionResult HandOverCreatejb()
        {
            DUTY_HANDOVER_Model m = new DUTY_HANDOVER_Model();
            m.DUTYDATE = Request.Params["odutyTime"];//值班日期
            m.BYORGNO = SystemCls.getCurUserOrgNo(); ;//组织机构编码
            m.DUTYUSERTYPE = Request.Params["ondutyType"];//人员值班类别（早，中，晚）
            m.DUTYTYPE = "-1";//值班类别 -1代表交班信息
            m.OPCONTENT = Request.Params["opcontent"].Replace("\r\n", "<br>").Replace("\r", "<br>").Replace("\n", "<br>"); //交班信息
            m.DUTYUSERID = Request.Params["strUserId"];//值班人序号
            m.OPTIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now);//操作时间
            m.opMethod = "Add";
            //验证
            if (string.IsNullOrEmpty(m.OPCONTENT))
                return Content(JsonConvert.SerializeObject(new Message(false, "交班内容不能为空", "")), "text/html;charset=UTF-8");
            Message msg = DUTY_HANDOVERCls.Manager(m);//保存交班信息

            //保存接班信息
            m.DUTYUSERID = Request.Params["jbrID"];//接班人ID
            m.DUTYTYPE = "-4";//代表接班
            m.OPCONTENT = "";//接班信息内容为空
            msg = DUTY_HANDOVERCls.Manager(m);//保存接班信息
            msg.Msg = "交班成功!";
            return Content(JsonConvert.SerializeObject(msg), "text/html;charset=UTF-8");
        }

        #endregion

        #region 根据当前日期，返回值班情况
        /// <summary>
        /// 根据当前日期，返回值班情况
        /// </summary>
        /// <returns></returns>
        public ActionResult getDCInfoByDT()
        {
            string dt = Request.Params["dt"];
            int i = 1;
            if (string.IsNullOrEmpty(dt))
                dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string[] arr = PublicClassLibrary.ClsStr.getWeeks(dt);

            var listW = DUTY_CLASSCls.GetListModel(new DUTY_CLASS_SW { BYORGNO = SystemCls.getCurUserOrgNo() });

            DUTY_USER_SW sw = new DUTY_USER_SW();
            sw.DTBegin = arr[2];
            sw.DTEnd = arr[3];
            sw.BYORGNO = SystemCls.getCurUserOrgNo();
            var result = DUTY_USERCls.getListModel(sw);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding='0' cellspacing='0'>");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th colspan=\"17\" class=\"divTable weekSel\"><a href=\"#\" class=\"border_ty3\" onclick=\"getdaybytype('{0}')\">上周</a>&nbsp;&nbsp;", arr[0]);
            sb.AppendFormat("<a href=\"#\" class=\"border_ty8\" onclick=\"getdaybytype('{0}')\">本周</a>&nbsp;&nbsp;", DateTime.Now.ToString("yyyy-MM-dd"));
            sb.AppendFormat("<a href=\"#\" class=\"border_ty10\" onclick=\"getdaybytype('{0}')\">下周</a></th></tr>", arr[4]);
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>日期</th>");
            sb.AppendFormat("<th>星期</th>");
            if (listW.Count() > 0)
            {
                foreach (var item in listW)
                {
                    if (item.DUTYCLASSID == "1")
                    {
                        sb.AppendFormat("<th>" + item.DUTYCLASSNAME + "</th>");
                    }
                    else if (item.DUTYCLASSID == "2")
                    {
                        sb.AppendFormat("<th>" + item.DUTYCLASSNAME + "</th>");
                    }
                    else
                    {
                        sb.AppendFormat("<th>" + item.DUTYCLASSNAME + "</th>");
                    }
                }

            }
            sb.AppendFormat("<th>带班领导</th>");
            sb.AppendFormat("<th>总带班领导</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody >");
            string curdt = DateTime.Now.ToString("yyyy-MM-dd");
            for (DateTime dt1 = Convert.ToDateTime(arr[2]); dt1 <= (Convert.ToDateTime(arr[3])); dt1 = dt1.AddDays(1))
            {
                if (curdt == dt1.ToString("yyyy-MM-dd"))//当前日期突出显示
                    sb.AppendFormat("<tr style=\"background-color:#FFE4E1\">");
                else
                    sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", dt1.ToString("yyyy-MM-dd"));
                sb.AppendFormat("<td class=\"center\">{0}</td>", WEEK(i));
                foreach (var li in listW)
                {
                    sb.AppendFormat("<td class=\"left\">{0}</td>", getuser1(li.DUTYCLASSID, result.Where(p => p.DUTYDATE == dt1.ToString("yyyy-MM-dd")).ToList()));
                }
                sb.AppendFormat("<td class=\"left\">{0}</td>", getuser1("-1", result.Where(p => p.DUTYDATE == dt1.ToString("yyyy-MM-dd")).ToList()));
                sb.AppendFormat("<td class=\"left\">{0}</td>", getuser1("-2", result.Where(p => p.DUTYDATE == dt1.ToString("yyyy-MM-dd")).ToList()));
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
                dt1.AddDays(1);
            }

            sb.AppendFormat("</tbody></table>");
            Message ms = new Message(true, sb.ToString(), "");
            if (!result.Any())
                ms = new Message(false, "该日期无需值班", "");
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");

        }

        public string WEEK(int i)
        {
            string week = "";
            switch (i)
            {
                case 1:
                    week = "星期一";
                    break;
                case 2:
                    week = "星期二";
                    break;
                case 3:
                    week = "星期三";
                    break;
                case 4:
                    week = "星期四";
                    break;
                case 5:
                    week = "星期五";
                    break;
                case 6:
                    week = "星期六";
                    break;
                default:
                    week = "星期日";
                    break;
            }
            return week;
        }

        private string getuser1(string str, IList<DUTY_USER_Model> list)
        {
            string strname = "";
            var str1 = list.Where(p => p.DUTYUSERTYPE == str).ToList();//;.Select(p => p.USERNAME).ToList();

            for (int i = 0; i < str1.Count; i++)
            {
                if (i > 0)
                {
                    strname += ",";
                    strname += (str1[i].ISATTENDED) == "1" ? str1[i].USERNAME : "<font color=red>" + str1[i].USERNAME + "</font>";
                }
                else
                {
                    strname += (str1[i].ISATTENDED) == "1" ? str1[i].USERNAME : "<font color=red>" + str1[i].USERNAME + "</font>";
                }
            }


            return strname;
        }

        #endregion

        #region 值班查询
        /// <summary>
        /// 值班查询
        /// </summary>
        /// <returns></returns>
        public ActionResult DutyQuery()
        {
            pubViewBag("022002", "022002", "");
            string strOrgno = SystemCls.getCurUserOrgNo();//获取当前用户的组织机构编码
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = strOrgno, TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.curDate = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);
            return View();
        } 
        #endregion

        #region 值班查询功能
        /// <summary>
        /// 值班查询功能
        /// </summary>
        /// <returns></returns>
        public ActionResult dutyListQuery()
        {
            return Content(JsonConvert.SerializeObject(new Message(true, GetUserModelList(new DUTY_USER_Model { BYORGNO = Request.Params["BYORGNO"], DUTYDATE = Request.Params["TTBH"] }), "")), "text/html;charset=UTF-8");
        }
        #endregion

        #region 组合值班查询表格
        /// <summary>
        /// 组合值班查询表格
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string GetUserModelList(DUTY_USER_Model o)
        {
            var listW = DUTY_CLASSCls.GetListModel(new DUTY_CLASS_SW { BYORGNO = o.BYORGNO });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr role=\"row\">");
            sb.AppendFormat("<th class=\"center\">单位名称</th>");
            if (listW.Count() > 0)
            {
                foreach (var item in listW)
                {
                    if (item.DUTYCLASSID == "1")
                    {
                        sb.AppendFormat("<th class=\"center\">" + item.DUTYCLASSNAME + "</th>");
                    }
                    else if (item.DUTYCLASSID == "2")
                    {
                        sb.AppendFormat("<th class=\"center\">" + item.DUTYCLASSNAME + "</th>");
                    }
                    else
                    {
                        sb.AppendFormat("<th class=\"center\">" + item.DUTYCLASSNAME + "</th>");
                    }
                }

            }
            //sb.AppendFormat("<th class=\"center\">早班 </th>");
            //sb.AppendFormat("<th class=\"center\">中班</th>");
            //sb.AppendFormat("<th class=\"center\">晚班</th>");
            sb.AppendFormat("<th class=\"center\">带班领导</th>");
            sb.AppendFormat("<th class=\"center\">总带班领导</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");

            IEnumerable<T_SYS_ORGModel>  orglist;
            if (o.BYORGNO.Substring(4, 5) == "00000")//如果是州级别取市县,否则取乡镇
            {
                 orglist = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { OnlyGetShiXian = "1", TopORGNO = o.BYORGNO });// new List<T_SYS_ORGModel>();
            }
            else {
                 orglist = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { TopORGNO = o.BYORGNO });// new List<T_SYS_ORGModel>();
            }
            

            var result = DUTY_USERCls.getListModel(new DUTY_USER_SW { DUTYDATE = o.DUTYDATE });// O_OD_USERCls.GetListModel(o);
            int i = 0;
            if (orglist.Any())
            {
                foreach (var org in orglist)//遍历机构
                {
                    var list = result.Where(p => p.BYORGNO == org.ORGNO).ToList();
                    if (list.Any())
                    {
                        if (i % 2 == 0)
                            sb.AppendFormat("<tr>");
                        else
                            sb.AppendFormat("<tr class='row1'>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", org.ORGNAME);
                        foreach (var li in listW)
                        {
                            sb.AppendFormat("<td class=\"center\">{0}</td>", getuser(li.DUTYCLASSID, list));
                        }
                        //sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("1", list));
                        //sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("2", list));
                        //sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("3", list));
                        sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("-1", list));
                        sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("-2", list));
                        sb.AppendFormat("</tr>");
                    }
                    else
                    {
                        if (i % 2 == 0)
                            sb.AppendFormat("<tr>");
                        else
                            sb.AppendFormat("<tr class='row1'>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", org.ORGNAME);
                        foreach (var li in listW)
                        {
                            sb.AppendFormat("<td class=\"center\">暂无</td>");
                        }
                        sb.AppendFormat("<td class=\"center\">暂无</td>");
                        sb.AppendFormat("<td class=\"center\">暂无</td>");
                        sb.AppendFormat("</tr>");
                    }
                    i++;
                }
            }
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        #endregion

        #region 根据值班类别从List获取用户列表
        /// <summary>
        /// 根据值班类别从List获取用户列表
        /// </summary>
        /// <param name="str"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private string getuser(string str, IList<DUTY_USER_Model> list)
        {
            string strname = "";
            var str1 = list.Where(p => p.DUTYUSERTYPE == str).Select(p => p.USERNAME).ToList();
            if (str1.Count > 0)
            {
                if (str1.Count > 1)
                {
                    for (int i = 0; i < str1.Count; i++)
                    {
                        if (i == str1.Count - 1)
                        {
                            strname += str1[i];
                        }
                        else
                        {
                            strname += str1[i] + ",";
                        }
                    }
                }
                else
                {
                    strname = str1[0];
                }
            }

            return strname;
        }

        #endregion

        #region 值班统计
        /// <summary>
        /// 值班统计
        /// </summary>
        /// <returns></returns>
        public ActionResult DutyStatistics()
        {
            pubViewBag("022003", "022003", "");
            ViewBag.dateB = DateTime.Now.ToString("yyyy-MM-01");
            ViewBag.dateE = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);
            return View();
        } 
        #endregion

        #region 值班统计查询方法
        /// <summary>
        /// 值班统计方法
        /// </summary>
        /// <returns></returns>
        public ActionResult dutyQueryUser()
        {
            DUTY_USER_SW sw = new DUTY_USER_SW();
            sw.BYORGNO = SystemCls.getCurUserOrgNo();
            sw.DTBegin = Request.Params["dateB"];
            sw.DTEnd = Request.Params["dateE"];
            if (string.IsNullOrEmpty(sw.DTBegin))
                return Content(JsonConvert.SerializeObject(new Message(false, "请选择开始时间", "")), "text/html;charset=UTF-8");
            if (string.IsNullOrEmpty(sw.DTEnd))
                return Content(JsonConvert.SerializeObject(new Message(false, "请选择结束时间", "")), "text/html;charset=UTF-8");
            if (PublicClassLibrary.ClsSwitch.compDate(sw.DTBegin, sw.DTEnd, "1") == false)
                return Content(JsonConvert.SerializeObject(new Message(false, "开始时间要小于等于结束时间", "")), "text/html;charset=UTF-8");
            string msg = GetQueryUserList(sw);
            return Content(JsonConvert.SerializeObject(new Message(true, msg, "")), "text/html;charset=UTF-8");
        }

        #endregion

        #region 值班统计的查询方法
        /// <summary>
        /// 值班统计的查询方法
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string GetQueryUserList(DUTY_USER_SW sw)
        {
            var listW = DUTY_CLASSCls.GetListModel(new DUTY_CLASS_SW { BYORGNO = SystemCls.getCurUserOrgNo() });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>值班人姓名</th>");
            if (listW.Count() > 0)
            {
                foreach (var item in listW)
                {
                    if (item.DUTYCLASSID == "1")
                    {
                        sb.AppendFormat("<th>" + item.DUTYCLASSNAME + "次数</th>");
                    }
                    else if (item.DUTYCLASSID == "2")
                    {
                        sb.AppendFormat("<th>" + item.DUTYCLASSNAME + "次数</th>");
                    }
                    else
                    {
                        sb.AppendFormat("<th>" + item.DUTYCLASSNAME + "次数</th>");
                    }
                }

            }
            sb.AppendFormat("<th>带班人</th>");
            sb.AppendFormat("<th>带班次数</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody role=\"alert\" aria-live=\"polite\" aria-relevant=\"all\">");
            var result = DUTY_USERCls.GetDutyCount(sw);// O_OD_USERCls.GetDutyCount(o).ToList();
            int i = 0;
            foreach (var item in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");

                sb.AppendFormat("<td class=\"center\">{0}</td>", item.USERNAME);
                if (listW.Count() > 0)
                {
                    foreach (var li in listW)
                    {
                        if (li.DUTYCLASSID == "1")
                        {
                            sb.AppendFormat("<td class=\"center\">{0}</td>", item.zaobCount);
                        }
                        else if (li.DUTYCLASSID == "2")
                        {
                            sb.AppendFormat("<td class=\"center\">{0}</td>", item.zhongbCount);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"center\">{0}</td>", item.wanbCount);
                        }
                    }

                }
                if (item.daiBCount != "0")
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", item.USERNAME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", item.daiBCount);
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\"></td>");
                    sb.AppendFormat("<td class=\"center\"></td>");
                }
                i++;
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");


            return sb.ToString();
        }
        #endregion

        #region 日报查询
        /// <summary>
        /// 日报查询
        /// </summary>
        /// <returns></returns>
        public ActionResult DailyQuery()
        {
            pubViewBag("022004", "022004", "");
            ViewBag.Date = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);
            return View();
        } 
        #endregion

        #region 日报提交查询请求的方法

        /// <summary>
        /// 日报提交查询请求的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult dutyDailyQuery()
        {
            DUTY_HANDOVER_SW sw = new DUTY_HANDOVER_SW();
            sw.BYORGNO = SystemCls.getCurUserOrgNo();//当前单位编码
            sw.DUTYDATE = Request.Params["TTBH"];//查询日期
            sw.DUTYTYPE = "-3";//日报
            if (string.IsNullOrEmpty(sw.DUTYDATE))
                return Content(JsonConvert.SerializeObject(new Message(false, "请选择查询日期", "")), "text/html;charset=UTF-8");
            string msg = GetHandOverList(sw);
            //Message ms = new Message(true, msg, "");
            return Content(JsonConvert.SerializeObject(new Message(true, msg, "")), "text/html;charset=UTF-8");
        }
        #endregion

        #region 组合日报查询表格

        /// <summary>
        /// 组合日报查询表格
        /// </summary>
        /// <param name="ohm"></param>
        /// <returns></returns>
        private string GetHandOverList(DUTY_HANDOVER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<th colspan=\"2\" class=\"center\"><h3 style=\"color:red;\">" + sw.DUTYDATE + PublicClassLibrary.ClsStr.Week(Convert.ToDateTime(sw.DUTYDATE)) + "</h3></th>");

            sb.AppendFormat("</thead>");

            IEnumerable<T_SYS_ORGModel> orglist;
            if (sw.BYORGNO.Substring(4, 5) == "00000")//如果是州级别取市县,否则取乡镇
            {
                orglist = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { OnlyGetShiXian = "1", TopORGNO = sw.BYORGNO });
            }
            else
            {
                orglist = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { TopORGNO = sw.BYORGNO });
            }

            //var orglist = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { OnlyGetShiXian = "1", TopORGNO = sw.BYORGNO });

            var result = DUTY_HANDOVERCls.getListModel(sw);//获取日报集合
            int i = 0;
            if (orglist.Any())
            {
                foreach (var org in orglist)//遍历机构
                {
                    if (result.Any())
                    {
                        var v = result.Where(p => p.BYORGNO == org.ORGNO).FirstOrDefault();
                        if (i % 2 == 0)
                            sb.AppendFormat("<tr>");
                        else
                            sb.AppendFormat("<tr class='row1'>");
                        if (v != null)
                        {
                            sb.AppendFormat("<td class=\"center \"><span style=\"color:blue;width:20%;\">{0}(上报人：" + StateSwitch.GetUsrNameByUserid(v.DUTYUSERID) + ")</span></td>", org.ORGNAME);
                            sb.AppendFormat("<td style=\" width:80%;\" class=\"left\">{0}</td>", v.OPCONTENT);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"center\"><span style=\"color:blue;width:20%;\">{0}(上报人：无)</span></td>", org.ORGNAME);
                            sb.AppendFormat("</td>");
                            sb.AppendFormat("<td style=\" width:80%;\" class=\"left\">暂无日报内容</td>");
                            sb.AppendFormat("</td>");
                        }
                        sb.AppendFormat("</td>");
                        sb.AppendFormat("</tr>");

                    }
                    else
                    {
                        if (i % 2 == 0)
                            sb.AppendFormat("<tr>");
                        else
                            sb.AppendFormat("<tr class='row1'>");
                        sb.AppendFormat("<td class=\"center\"><span style=\"color:blue;width:20%;\">{0}(上报人：无)</span></td>", org.ORGNAME);
                        sb.AppendFormat("</td>");
                        sb.AppendFormat("<td style=\" width:80%;\" class=\"left\">暂无日报内容</td>");
                        sb.AppendFormat("</td>");
                        sb.AppendFormat("</tr>");
                    }
                    i++;
                }
            }
            sb.AppendFormat("</table>");

            return sb.ToString();
        }

        #endregion

        #region 交班查询
        /// <summary>
        /// 交班查询
        /// </summary>
        /// <returns></returns>
        public ActionResult HandoverQuery()
        {
            pubViewBag("022005", "022005", "");
            ViewBag.Date = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);
            return View();
        } 
        #endregion

        #region 交班查询按钮方法
        /// <summary>
        /// 交班查询
        /// </summary>
        /// <returns></returns>
        public ActionResult dutyHandoverQuery()
        {
            DUTY_HANDOVER_SW sw = new DUTY_HANDOVER_SW();
            sw.BYORGNO = SystemCls.getCurUserOrgNo();//当前单位编码
            sw.DUTYDATE = Request.Params["TTBH"];//查询日期
            sw.DUTYTYPE = "-1";//交班
            if (string.IsNullOrEmpty(sw.DUTYDATE))
                return Content(JsonConvert.SerializeObject(new Message(false, "请选择查询日期", "")), "text/html;charset=UTF-8");
            string msg = GetHandoverList(sw);
            return Content(JsonConvert.SerializeObject(new Message(true, msg, "")), "text/html;charset=UTF-8");

        }
        #endregion

        #region 交班查询组合表格
        private string GetHandoverList(DUTY_HANDOVER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>交班时间</th>");
            sb.AppendFormat("<th>交班人</th>");
            sb.AppendFormat("<th>接班人</th>");
            sb.AppendFormat("<th>带班领导</th>");
            sb.AppendFormat("<th>交班事项</th>");
            sb.AppendFormat("<th>领导意见</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = DUTY_HANDOVERCls.getListModel(new DUTY_HANDOVER_SW { BYORGNO = sw.BYORGNO, DUTYDATE = sw.DUTYDATE });
            var resultUser = DUTY_USERCls.getListModel(new DUTY_USER_SW { BYORGNO = sw.BYORGNO, DUTYDATE = sw.DUTYDATE });
            int i = 0;
            foreach (var v in result.Where(p => p.DUTYTYPE == "-1"))
            //-1代表交班信息：下班次需要处理事项(固定)
            //-2代表领导意见(固定)
            //-3代表日报（固定）
            //-4代表接班人（固定）
            //-5代表上班次交办事项处理情况
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr onclick=\"changeTD(this)\">");
                else
                    sb.AppendFormat("<tr onclick=\"changeTD(this)\" class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.OPTIME);//交班时间
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.DUTYUSERName);//交班人
                var jv = result.Where(p => p.DUTYTYPE == "-4" && p.DUTYUSERTYPE == v.DUTYUSERTYPE).FirstOrDefault();//接班人
                if (jv != null)
                    sb.AppendFormat("<td class=\"center\">{0}</td>", jv.DUTYUSERName);//交班人
                else
                    sb.AppendFormat("<td class=\"USERID\">{0}</td>", "&nbsp;");

                var ldV = resultUser.Where(p => p.DUTYUSERTYPE == "-1").FirstOrDefault();//带班领导
                if (ldV != null)
                    sb.AppendFormat("<td class=\"center\">{0}</td>", ldV.USERNAME);
                else
                    sb.AppendFormat("<td class=\"USERID\">{0}</td>", "&nbsp;");
                sb.AppendFormat("<td class=\"USERID\">{0}</td>", v.OPCONTENT);//交班内容

                var ldyjV = result.Where(p => p.DUTYTYPE == "-2" && p.DUTYUSERTYPE == v.DUTYUSERTYPE).FirstOrDefault();//领导意见
                if (ldyjV != null)
                    sb.AppendFormat("<td class=\"USERID\">{0}</td>", ldyjV.OPCONTENT);//交班内容
                else
                    sb.AppendFormat("<td class=\"USERID\">{0}</td>", "&nbsp;");//交班内容
                sb.AppendFormat("</tr>");
                i++;

            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");

            return sb.ToString();
        }
        #endregion

        #region 排班表
        /// <summary>
        /// 排班表
        /// </summary>
        /// <returns></returns>
        public ActionResult Schedule()
        {
            pubViewBag("022006", "022006", "");

            var result = DUTY_CLASSCls.GetListModel(new DUTY_CLASS_SW { BYORGNO = SystemCls.getCurUserOrgNo() }).Select(p => p.DUTYCLASSID).ToList();

            StringBuilder sb = new StringBuilder();
            T_SYSSEC_IPSUSER_SW ts = new T_SYSSEC_IPSUSER_SW();
            ts.curOrgNo = SystemCls.getCurUserOrgNo();//获取当前用户的组织机构编码
            var list1 = T_SYSSEC_IPSUSERCls.getListModel(ts);
            foreach (var item in list1)
            {
                sb.AppendFormat("<option value=\"{0}\">{1}</option>", item.USERID, item.USERNAME);
            }
            ViewBag.ZBR = sb.ToString();//当前单位所有值班人
            //动态显示班次
            //if (result.Count > 0)
            //{
            //    var a = result.Count;
            //    if (a == 1)
            //    {
            //        if (result[0] == "1")
            //        {
            //            Response.Write("<script>window.onload=function(){document.getElementById('divZao').style.display=''}</script>");
            //        }
            //        else if (result[0] == "2")
            //        {
            //            Response.Write("<script>window.onload=function(){document.getElementById('divZhong').style.display=''}</script>");
            //        }
            //        else
            //        {
            //            Response.Write("<script>window.onload=function(){document.getElementById('divWan').style.display=''}</script>");
            //        }
            //    }
            //    else if (a == 2)
            //    {
            //        if (result[0] == "1" && result[1] == "2")//早班 中班
            //        {
            //            Response.Write("<script>window.onload=function(){document.getElementById('divZao').style.display='';document.getElementById('divZhong').style.display=''}</script>");
            //        }
            //        else if (result[0] == "1" && result[1] == "3")//早班 晚班
            //        {
            //            Response.Write("<script>window.onload=function(){document.getElementById('divZao').style.display='';document.getElementById('divWan').style.display=''}</script>");
            //        }
            //        else
            //        { //中班 晚班
            //            Response.Write("<script>window.onload=function(){document.getElementById('divZhong').style.display='';document.getElementById('divWan').style.display=''}</script>");
            //        }
            //    }
            //    else
            //    {
            //        Response.Write("<script>window.onload=function(){document.getElementById('divZao').style.display='';document.getElementById('divZhong').style.display='';document.getElementById('divWan').style.display=''}</script>");
            //    }
            //}
            //else
            //{
            //    Response.Write("<script>window.onload=function(){alert('请设置班次！');}</script>");
            //}
            var result1 = GetModelList();
            return View(result1);
        }

        private IEnumerable<DUTY_CLASS_Model> GetModelList()
        {
            var result = new List<DUTY_CLASS_Model>();
            var list= DUTY_CLASSCls.GetListModel(new DUTY_CLASS_SW { BYORGNO = SystemCls.getCurUserOrgNo() });
            if (list.Any()) {
                foreach (var item in list){
                    var model = new DUTY_CLASS_Model();
                    model.DUTYCLASSID = item.DUTYCLASSID;
                    result.Add(model);
                }          
            }
            return result;
        }

        /// <summary>
        /// 排班管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ScheduleManager()
        {
            var begin = Request.Params["begin"];
            var end = Request.Params["end"];
            var zaoid = Request.Params["zaoid"];
            var zhongid = Request.Params["zhongid"];
            var wanid = Request.Params["wanid"];
            var daibanid = Request.Params["daibanid"];
            var zongdaibanid = Request.Params["zongdaibanid"];
            var selectdate = Request.Params["selectdate"];
            var method = Request.Params["method"];
            var strid = zaoid + "#" + zhongid + "#" + wanid + "#" + daibanid + "#" + zongdaibanid;
            DUTY_USER_Model m = new DUTY_USER_Model();
            m.DUTYDATE = selectdate;
            m.DUTYUSERID = strid;
            m.dateBegin = begin;
            m.dateEnd = end;
            //m.ZHONGDUTYUSERID = zhongid;
            //m.WANDUTYUSERID = wanid;
            //m.DAIBANDUTYUSERID = daibanid;
            //m.ZONGDAIBANDUTYUSERID = zongdaibanid;
            m.BYORGNO = SystemCls.getCurUserOrgNo();//获取当前用户组织机构编码
            m.opMethod = method;
            //m.DUTYUSERTYPE = "1"; //1 早班 2 中班 3 晚班 -1 带班领导 -2 总带班领导

            return Content(JsonConvert.SerializeObject(DUTY_USERCls.Manager(m)), "text/html;charset=UTF-8");

        }

        /// <summary>
        /// 排班查询
        /// </summary>
        /// <returns></returns>
        public string ScheduleQuery()
        {
            var start = Request.Params["start"];
            var end = Request.Params["end"];

            DUTY_USER_Model m = new DUTY_USER_Model();
            m.dateBegin = start;
            m.dateEnd = end;

            var str = DUTY_USERCls.getJsonStr(m);
            return str;
        } 
        #endregion

        #region 历史排班
        /// <summary>
        /// 历史排班
        /// </summary>
        /// <returns></returns>
        public ActionResult HistoricalDuty()
        {
            pubViewBag("022007", "022007", "");
            string strOrgno = SystemCls.getCurUserOrgNo();//获取当前用户的组织机构编码  
            if (strOrgno.Substring(4, 5) == "00000")//如果是州级别取市县,否则取乡镇
            {
                ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = strOrgno, TopORGNO = SystemCls.getCurUserOrgNo(), OnlyGetShiXian = "1" });
            }
            else
            {
                ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = strOrgno, TopORGNO = SystemCls.getCurUserOrgNo()});
            }
            
            return View();
        } 
        #endregion

        #region  查询按钮方法
        /// <summary>
        /// 查询按钮方法
        /// </summary>
        /// <returns></returns>
        public ActionResult HistoricalQuery()
        {
            string orgNo = Request.Params["BYORGNO"];
            //string typeID = Request.Params["typeID"];

            //if (string.IsNullOrEmpty(typeID))
            //    return Content(JsonConvert.SerializeObject(new Message(false, "没有查询到值班信息", "")), "text/html;charset=UTF-8");
            string msg = GetUserModelListType(new DUTY_USER_SW { BYORGNO = orgNo });

            return Content(JsonConvert.SerializeObject(new Message(true, msg, "")), "text/html;charset=UTF-8");

        }
        private string GetUserModelListType(DUTY_USER_SW sw)
        {
            var listW = DUTY_CLASSCls.GetListModel(new DUTY_CLASS_SW { BYORGNO = sw.BYORGNO });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>单位名称</th>");
            sb.AppendFormat("<th>日期</th>");
            sb.AppendFormat("<th>星期</th>");
            if (listW.Count() > 0)
            {
                foreach (var item in listW)
                {
                    if (item.DUTYCLASSID == "1")
                    {
                        sb.AppendFormat("<th class=\"center\">" + item.DUTYCLASSNAME + "</th>");
                    }
                    else if (item.DUTYCLASSID == "2")
                    {
                        sb.AppendFormat("<th class=\"center\">" + item.DUTYCLASSNAME + "</th>");
                    }
                    else
                    {
                        sb.AppendFormat("<th class=\"center\">" + item.DUTYCLASSNAME + "</th>");
                    }
                }
            }
            sb.AppendFormat("<th>带班领导</th>");
            sb.AppendFormat("<th>总带班领导</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = DUTY_USERCls.getListModel(sw);// O_OD_USERCls.GetListModel(o);
            if (result.Any())
            {
                int i = 0;
                foreach (var item in result.OrderBy(p => p.DUTYDATE).Select(p => p.DUTYDATE).Distinct())
                {
                    var recordresult = result.Where(p => p.DUTYDATE == item).ToList();
                    if (i % 2 == 0)
                        sb.AppendFormat("<tr>");
                    else
                        sb.AppendFormat("<tr class='row1'>");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", recordresult[0].ORGNAME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", item);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", PublicClassLibrary.ClsStr.Week(item));
                    foreach (var li in listW)
                    {
                        sb.AppendFormat("<td class=\"center\">{0}</td>", getuser(li.DUTYCLASSID, recordresult));
                    }
                    sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("-1", recordresult));
                    sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("-2", recordresult));
                    sb.AppendFormat("</td>");
                    sb.AppendFormat("</tr>");
                    i++;
                }

            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan='16'><em>暂无值班信息</em></td>");
                sb.AppendFormat("</tr>");
            }


            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        #endregion

        #region 节假日统计
        /// <summary>
        /// 节假日统计
        /// </summary>
        /// <returns></returns>
        public ActionResult HolidayStatistics()
        {
            pubViewBag("022008", "022008", "");
            string strOrgno = SystemCls.getCurUserOrgNo();//获取当前用户的组织机构编码

            if (strOrgno.Substring(4, 5) == "00000")//如果是州级别取市县,否则取乡镇
            {
                ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = strOrgno, TopORGNO = SystemCls.getCurUserOrgNo(), OnlyGetShiXian = "1" });
            }
            else
            {
                ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = strOrgno, TopORGNO = SystemCls.getCurUserOrgNo() });
            }
           
            ViewBag.dateB = DateTime.Now.ToString("yyyy-MM-01");
            ViewBag.dateE = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);
            return View();
        } 
        #endregion

        #region 查询按钮方法
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ActionResult HolidayQuery()
        {
            DUTY_USER_SW sw = new DUTY_USER_SW();
            sw.DTBegin = Request.Params["BEGTIME"];//开始时间
            sw.DTEnd = Request.Params["ENDTIME"];//结束时间
            sw.curOrgNo = Request.Params["BYORGNO"];

            //验证
            if (string.IsNullOrEmpty(sw.DTBegin) == true || string.IsNullOrEmpty(sw.DTEnd) == true)
                return Content(JsonConvert.SerializeObject(new Message(false, "请选择日期！", "")), "text/html;charset=UTF-8");
            if (PublicClassLibrary.ClsSwitch.compDate(sw.DTBegin, sw.DTEnd, "0") == false)
                return Content(JsonConvert.SerializeObject(new Message(false, "开始时间应小于结束时间！", "")), "text/html;charset=UTF-8");

            string msg = GetQueryUserHoliday(sw);
            return Content(JsonConvert.SerializeObject(new Message(true, msg, "")), "text/html;charset=UTF-8");
        }

        #endregion

        #region 拼接周末值班次数的数据
        /// <summary>
        /// 拼接周末值班次数的数据
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string GetQueryUserHoliday(DUTY_USER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>单位名称</th>");
            sb.AppendFormat("<th>姓名 </th>");
            sb.AppendFormat("<th>值班次数</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody role=\"alert\" aria-live=\"polite\" aria-relevant=\"all\">");
            var result = DUTY_USERCls.getWeekCountListModel(sw).ToList();
            int i = 0;
            foreach (var item in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");

                sb.AppendFormat("<td class=\"center\">{0}</td>", item.ORGNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.USERNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.weekCount);

                i++;
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");


            return sb.ToString();
        }
        #endregion

        #region 值班设置
        /// <summary>
        /// 值班设置
        /// </summary>
        /// <returns></returns>
        public ActionResult DutySetting()
        {
            pubViewBag("022009", "022009", "");
            var ORGNO = SystemCls.getCurUserOrgNo();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat(" <tr>");
            sb.AppendFormat("  <th>值班名称</th>");
            sb.AppendFormat("  <th>值班开始时间</th>");
            sb.AppendFormat("  <th>值班结束时间</th>");
            sb.AppendFormat("  <th></th>");
            sb.AppendFormat(" </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");

            var li = DUTY_CLASSCls.GetListModel(new DUTY_CLASS_SW { BYORGNO = ORGNO });
            foreach (var item in li)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.DUTYCLASSNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.DUTYBEGINTIME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.DUTYENDTIME);
                //sb.AppendFormat("<td class=\"center\">{0}</td>", "<a href='#' onclick='Del(\"{0}\",\"{1}\")' class=\"searchBox_01 LinkDel\">删除</a>", item.DUTYCLASSID,item.BYORGNO);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href='#' onclick='Del(\"{0}\",\"{1}\")' class=\"searchBox_01 LinkDel\">删除</a>", item.DUTYCLASSID, item.BYORGNO);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            ViewBag.list = sb.ToString();
            return View();
        } 
        #endregion

        #region 值班设置管理
        public ActionResult DutySettingManager()
        {
            var DUTYCLASSNAME = Request.Params["DUTYCLASSNAME"];
            var DUTYCLASSID = Request.Params["DUTYCLASSID"];
            var DUTYBEGINTIME = Request.Params["DUTYBEGINTIME"];
            var DUTYENDTIME = Request.Params["DUTYENDTIME"];
            var METHOD = Request.Params["METHOD"];
            var ORGNO = SystemCls.getCurUserOrgNo();

            DUTY_CLASS_Model m = new DUTY_CLASS_Model();

            m.BYORGNO = ORGNO;
            m.DUTYCLASSID = DUTYCLASSID;
            m.DUTYCLASSNAME = DUTYCLASSNAME;
            m.DUTYBEGINTIME = DUTYBEGINTIME;
            m.DUTYENDTIME = DUTYENDTIME;
            m.opMethod = METHOD;

            return Content(JsonConvert.SerializeObject(DUTY_CLASSCls.Manager(m)));
        } 
        #endregion

    }
}
