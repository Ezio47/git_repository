using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using ManagerSystemModel;
using ManagerSystemClassLibrary.BaseDT;
using Newtonsoft.Json;
using ManagerSystemClassLibrary;
using ManagerSystemSearchWhereModel;
using ManagerSystem.MVC.HelpCom;

namespace ManagerSystem.MVC.Controllers
{
    /// <summary>
    /// 值班管理
    /// </summary>
    public class OndutyPbController : BaseController
    {
       
        ////排班页面*********************************************************************
        #region 排班页面 叶磊2016年6月21日17时51分32秒
        /// <summary>
        /// 排班
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            pubViewBag("010006", "010006", "");
            OD_ODTYPE_Model m = OD_TYPECls.getModel(new OD_ODTYPE_SW { BYORGNO = SystemCls.getCurUserOrgNo(), isTopOne = "1" });
            ViewBag.id = m.OD_TYPEID;
            ViewBag.Ttitle = m.OD_TYPENAME;
            ViewBag.bigTime = m.OD_DATEBEGIN;
            ViewBag.endTime = m.OD_DATEEND;

            StringBuilder sb = new StringBuilder();
            T_SYSSEC_IPSUSER_SW ts = new T_SYSSEC_IPSUSER_SW();
            ts.curOrgNo = SystemCls.getCurUserOrgNo();//获取当前用户的组织机构编码
            var list1 = T_SYSSEC_IPSUSERCls.getListModel(ts);
            foreach (var item in list1)
            {
                sb.AppendFormat("<option value=\"{0}\">{1}</option>", item.USERID, item.USERNAME);
            }
            ViewBag.ZBR = sb.ToString();//当前单位所有值班人
            ViewBag.create = (SystemCls.isRight("010006001")) ? "1" : "0";
            ViewBag.tocreate = (SystemCls.isRight("010006002")) ? "1" : "0";
            return View();
        }

        #endregion

        #region 排班日期的新建、重置排班 叶磊2016年6月21日17时48分57秒
        /// <summary>
        /// 排班日期的新建、重置排班
        /// </summary>
        /// <returns></returns>
        public ActionResult createOnDuty()
        {
            OD_ODTYPE_Model m = new OD_ODTYPE_Model();
            m.OD_TYPEID = Request.Params["TYPEID"];//值班类别序号
            m.OD_TYPENAME = Request.Params["TYPENAME"];//标题
            m.OD_DATEBEGIN = Request.Params["TIMEBegin"];//开始时间
            m.OD_DATEEND = Request.Params["TIMEEnd"]; //结束时间
            m.op_Method = Request.Params["Method"];//创建方式 Add 新增 Reset 重置
            m.BYORGNO = SystemCls.getCurUserOrgNo();//当前用户单位编码

            //验证
            if (string.IsNullOrEmpty(m.OD_DATEBEGIN) == true || string.IsNullOrEmpty(m.OD_DATEEND) == true)
                return Content(JsonConvert.SerializeObject(new Message(false, "请选择排班日期！", "")), "text/html;charset=UTF-8");
            if (string.IsNullOrEmpty(m.OD_TYPENAME) == true)
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入值班名称！", "")), "text/html;charset=UTF-8");
            if (PublicClassLibrary.ClsSwitch.compDate(m.OD_DATEBEGIN, m.OD_DATEEND, "0") == false)
                return Content(JsonConvert.SerializeObject(new Message(false, "开始时间应小于结束时间！", "")), "text/html;charset=UTF-8");


            Message msgType = OD_TYPECls.Manager(m);//对类别执行管理
            if (msgType.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, msgType.Msg, "")), "text/html;charset=UTF-8");


            m.OD_TYPEID = msgType.Url;//重新赋值类别ID，在新增的情况下为新增序号，修改的时候和原序号相同 后面需返回 以Url方式存在

            if (string.IsNullOrEmpty(m.OD_TYPEID))
            {
                return Content(JsonConvert.SerializeObject(new Message(false, "尚未排班，请先新建排班！！", "")), "text/html;charset=UTF-8");
            }
            m.op_Method = "Add";//将操作方法改为Add
            return Content(JsonConvert.SerializeObject(OD_DATECls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 保存排班信息 叶磊 2016年6月21日17时49分26秒
        /// <summary>
        /// 保存排班信息
        /// </summary>
        /// <returns></returns>
        public ActionResult PbSave()
        {
            OD_USER_Model m = new OD_USER_Model();
            m.ONDUTYUSERID = Request.Params["userID"];//获取当前用户USERID 以逗号分隔
            m.dateBegin = Request.Params["tDate"];//获取值班人的值班日期 开始日期
            m.dateEnd = Request.Params["txtTime"];//编辑行日期表单的值 结束日期
            m.BYORGNO = SystemCls.getCurUserOrgNo();//获取当前用户组织机构编码
            m.ONDUTYUSERTYPE = Request.Params["ondutyType"];//获取值班人的值班类别 1 代表第一班 2 代表第二班3 代表第三班-1代表带班领导（固定）-2代表总带班领导（固定）   
            m.OD_TYPEID = Request.Params["TYPEID"];////值班类别序号
            m.opMethod = "Add";
            if (string.IsNullOrEmpty(m.dateEnd))
                m.dateEnd = m.dateBegin;


            //验证
            if (string.IsNullOrEmpty(m.dateBegin) == true || string.IsNullOrEmpty(m.dateEnd) == true)
                return Content(JsonConvert.SerializeObject(new Message(false, "请选择需要排班的日期！", "")), "text/html;charset=UTF-8");
            if (PublicClassLibrary.ClsSwitch.compDate(m.dateBegin, m.dateEnd, "1") == false)
                return Content(JsonConvert.SerializeObject(new Message(false, "开始时间应小于或等于结束时间！", "")), "text/html;charset=UTF-8");
            //if (string.IsNullOrEmpty(m.ONDUTYUSERID) == true)
            //    return Content(JsonConvert.SerializeObject(new Message(false, "未选择值班人员！", "")), "text/html;charset=UTF-8");


            return Content(JsonConvert.SerializeObject(OD_USERCls.Manager(m)), "text/html;charset=UTF-8");

        }

        #endregion
        
        #region 排班表向下复制功能 叶磊2016年6月21日17时49分47秒
        /// <summary>
        /// 排班表向下复制功能
        /// </summary>
        /// <returns></returns>
        public ActionResult copy()
        {
            OD_USER_Model m = new OD_USER_Model();
            m.BYORGNO = SystemCls.getCurUserOrgNo();//当前用户单位编码
            m.ONDUTYDATE = Request.Params["ONDUTYDATE"];//要复制的值班日期
            m.opMethod = "Copy";
            return Content(JsonConvert.SerializeObject(OD_USERCls.Manager(m)), "text/html;charset=UTF-8");


        }

        #endregion


        #region 查询当前排班人员情况 叶磊2016年6月21日17时50分37秒
        public ActionResult indexQuery()
        {
            string ODTypeID = Request.Params["ODTypeID"];//排序类别ID
            if (string.IsNullOrEmpty(ODTypeID))

                return Content(JsonConvert.SerializeObject(new Message(false, "尚未排班，请新建排班", "")), "text/html;charset=UTF-8");
            var list = OD_DATECls.getListModel(new OD_DATE_SW { OD_TYPEID = ODTypeID });
            var result = OD_USERCls.getListModel(new OD_USER_SW { OD_TYPEID = ODTypeID, BYORGNO = SystemCls.getCurUserOrgNo() });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>日期</th>");
            sb.AppendFormat("<th>星期</th>");
            sb.AppendFormat("<th>早班 </th>");
            sb.AppendFormat("<th>中班</th>");
            sb.AppendFormat("<th>晚班</th>");
            sb.AppendFormat("<th>带班领导</th>");
            sb.AppendFormat("<th>总带班领导</th>");
            sb.AppendFormat("<th>操作</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 1;//table 行数
            var j = 0;
            foreach (var date in list)
            {
                var recordresult = result.Where(p => p.ONDUTYDATE == date.ONDUTYDATE).ToList();
                if (j % 2 == 0)
                {
                    sb.AppendFormat("<tr id=\"" + i + "\" onclick=\"trClick('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')\">"
                    , date.ONDUTYDATE
                    , date.WEEK
                    , getuserIDList("1", recordresult)
                    , getuserIDList("2", recordresult)
                    , getuserIDList("3", recordresult)
                    , getuserIDList("-1", recordresult)
                    , getuserIDList("-2", recordresult)
                    , i
                    );
                }
                else
                {
                    sb.AppendFormat("<tr id=\"" + i + "\" class=\"row1\" onclick=\"trClick('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')\">"
                    , date.ONDUTYDATE
                    , date.WEEK
                    , getuserIDList("1", recordresult)
                    , getuserIDList("2", recordresult)
                    , getuserIDList("3", recordresult)
                    , getuserIDList("-1", recordresult)
                    , getuserIDList("-2", recordresult)
                    , i
                    );
                }
                
                sb.AppendFormat("<td class=\"center\">{0}</td>", date.ONDUTYDATE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", date.WEEK);
                sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("1", recordresult));
                sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("2", recordresult));
                sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("3", recordresult));
                sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("-1", recordresult));
                sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("-2", recordresult));
                sb.AppendFormat("<td style=\"width:40px;\"><a href=\"javascript:void(0)\" onclick=\"copy('{0}','{1}')\"><img src=\"../Images/next3.jpg\" /></a></td>", date.ONDUTYDATE, getuserIDList("-1", recordresult));
                sb.AppendFormat("</tr>");
                i++;
                j++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");

            ////Message ms = new Message(true, sb.ToString(), "");
            //return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        #endregion

        #region 返回用户名组合字符串 叶磊 2016年6月24日15时24分17秒
        /// <summary>
        /// 返回用户名组合字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private string getuserIDList(string str, IList<OD_USER_Model> list)
        {
            string strname = "";
            var str1 = list.Where(p => p.ONDUTYUSERTYPE == str).Select(p => p.ONDUTYUSERID).ToList();
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

        #region table 每行的点击事件 要修改的赋值到修改框里，修改后获取后台值班人ID 前台使用 叶磊2016年6月24日15时24分02秒
        /// <summary>
        /// table 每行的点击事件 要修改的赋值到修改框里，修改后获取后台值班人ID 
        /// </summary>
        /// <returns></returns>
        public ActionResult getUserID()
        {
            var result = OD_USERCls.getListModel(new OD_USER_SW { ONDUTYDATE = Request.Params["ONDUTYDATE"], OD_TYPEID = Request.Params["TYPEID"] });// O_OD_USERCls.GetListModel(ou);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}={1}={2}={3}={4}", getuserIDList("1", result.ToList()), getuserIDList("2", result.ToList()), getuserIDList("3", result.ToList()), getuserIDList("-1", result.ToList()), getuserIDList("-2", result.ToList()));

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        #endregion

        ////*********************************************************************

        ////值班处理页面*********************************************************************

        #region 值班处理首页 叶磊2016年6月22日17时53分46秒
        /// <summary>
        /// 值班处理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult OdutyHandle()
        {
            pubViewBag("010001", "010001", "");
            ViewBag.orgNameTrue = T_SYS_ORGCls.getModel(new T_SYS_ORGSW { ORGNO = SystemCls.getCurUserOrgNo() }).ORGNAME;

            OD_HANDOVER_Model m = OD_HANDOVERCls.getModel(new OD_HANDOVER_SW { BYORGNO = SystemCls.getCurUserOrgNo(), ONDUTYTYPE = "-3", ONDUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now) });//获取日志
            if (m != null)
                ViewBag.dayLog = m.OPCONTENT;
            return View();
        }
        #endregion

        #region 添加领导意见 叶磊 2016年6月23日15时23分49秒
        /// <summary>
        /// 添加领导意见
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveLeadAdd()
        {

            OD_HANDOVER_Model m = new OD_HANDOVER_Model();
            m.opMethod = "Add";
            m.ONDUTYDATE = Request.Params["ONDUTYDATE"];//时间
            m.BYORGNO = SystemCls.getCurUserOrgNo();//单位编码
            m.ONDUTYUSERTYPE = Request.Params["ONDUTYUSERTYPE"];//对应早中晚 那个班次
            m.ONDUTYTYPE = "-2";//-2代表领导意见
            m.ONDUTYUSERID = Request.Params["ONDUTYUSERID"];//领导人ID
            m.OPTIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now);//时间
            m.OPCONTENT = Request.Params["content"];//领导意见
            if (string.IsNullOrEmpty(m.OPCONTENT) == true)
                return Content(JsonConvert.SerializeObject(new Message(false, "领导意见内容为空，请输入对应的意见内容！", "")), "text/html;charset=UTF-8");
            return Content(JsonConvert.SerializeObject(OD_HANDOVERCls.Manager(m)), "text/html;charset=UTF-8");

        }

        #endregion


        #region 获取值班员、日报信息 叶磊 2016年6月24日14时44分13秒
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
            OD_USER_Model m = OD_USERCls.getModel(new OD_USER_SW { ONDUTYUSERID = SystemCls.getUserID(), BYORGNO = curOrgNo, ONDUTYDATE = strDate });
            string zw = "非值班人员";
            switch (m.ONDUTYUSERTYPE)
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
            if (OD_HANDOVERCls.isExists(new OD_HANDOVER_SW { ONDUTYDATE = strDate, BYORGNO = curOrgNo, ONDUTYTYPE = "-3" }) == true)
                sb.AppendFormat(" &nbsp;&nbsp;<input type=button style=\"width:87px;\" value='日报已填写' class=\"btnWriteCss\" onclick=\"$('#ww').window('open')\">");

            else
                sb.AppendFormat(" &nbsp;&nbsp;<input type=button style=\"width:80px;\" value='填写日报' class=\"btnWriteCss\" onclick=\"$('#ww').window('open')\">");

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        #endregion

        #region 添加日报 叶磊2016年6月23日17时26分45秒
        /// <summary>
        /// 添加日报
        /// </summary>
        /// <returns></returns>
        public ActionResult OdutyDailyAdd()
        {
            //ONDUTYDATE, BYORGNO, ONDUTYUSERTYPE, ONDUTYTYPE, ONDUTYUSERID, OPTIME, OPCONTENT
            OD_HANDOVER_Model m = new OD_HANDOVER_Model();
            m.opMethod = "Add";
            m.ONDUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);//日期
            m.BYORGNO = SystemCls.getCurUserOrgNo();//单位编码
            m.ONDUTYUSERTYPE = "0";//日报无班次，可设为默认0
            m.ONDUTYTYPE = "-3";//日报专属标识
            m.ONDUTYUSERID = SystemCls.getUserID();//当前用户ID
            m.OPTIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now);//时间
            m.OPCONTENT = Request.Params["content"];//内容
            if (string.IsNullOrEmpty(m.OPCONTENT) == true)
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入日报内容！", "")), "text/html;charset=UTF-8");
            return Content(JsonConvert.SerializeObject(OD_HANDOVERCls.Manager(m)), "text/html;charset=UTF-8");
        }

        #endregion


        #region 值班处理签到方法 叶磊 2016年6月23日14时25分36秒
        /// <summary>
        /// 值班处理签到方法
        /// </summary>
        /// <returns></returns>
        public ActionResult SignAgien()
        {
            OD_USER_Model m = new OD_USER_Model();
            m.ONDUTYDATE = Request.Params["ondutyDate"];//当前值班日期
            m.BYORGNO = SystemCls.getCurUserOrgNo();//获取当前用户的组织机构编码
            m.ONDUTYUSERID = Request.Params["strUserId"]; //用户ID
            m.ONDUTYUSERTYPE = Request.Params["ondutyType"];//当前用户值班类别
            m.opMethod = "Sign";//签到方法
            Message msSign = OD_USERCls.Manager(m);
            if (msSign.Success == true && m.ONDUTYUSERTYPE != "-1")//签到成功，判断是否迟到 领导签到不需要验证
            {
                if (OD_CLASSCls.isLate(new OD_CLASS_SW { ONDUTYCLASSID = m.ONDUTYUSERTYPE }) == false)//未迟到
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

        #region 交班的时候判断是否有下一班次的接班人，没有禁止交班 叶磊 2016年6月23日17时23分23秒
        /// <summary>
        /// 交班的时候判断是否有下一班次的接班人，没有禁止交班
        /// </summary>
        /// <returns></returns>
        public ActionResult getclassNest()
        {
            OD_HANDOVER_Model m = new OD_HANDOVER_Model();
            m.ONDUTYDATE = Request.Params["dt"];// PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);//日期 
            m.BYORGNO = SystemCls.getCurUserOrgNo();//单位编码
            m.ONDUTYUSERTYPE = Request.Params["dcClass"];//当前班次
            m.ONDUTYTYPE = "-1";//交班信息
            m.ONDUTYUSERID = SystemCls.getUserID();//当前用户ID
            m.OPTIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now);//当前时间
            if (OD_CLASSCls.isEarlyOut(new OD_CLASS_SW { ONDUTYCLASSID = m.ONDUTYUSERTYPE, judgeDate = m.ONDUTYDATE }) == true)
                return Content(JsonConvert.SerializeObject(new Message(false, "未到交班时间，早退,禁止交班！", "")), "text/html;charset=UTF-8");

            var dataList = OD_USERCls.getNextClassListModel(new OD_USER_SW { ISATTENDED = "1", ONDUTYDATE = m.ONDUTYDATE, BYORGNO = m.BYORGNO, ONDUTYUSERTYPE = m.ONDUTYUSERTYPE });
            StringBuilder signSb = new StringBuilder();
            signSb.AppendFormat("<select id=\"s1\">");
            if (dataList.Any())
            {
                foreach (var item in dataList)
                {
                    signSb.AppendFormat("<option value='{0}'>{1}</option>", item.ONDUTYUSERID, item.USERNAME);
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


        #region 交班时获取上交班信息 叶磊2016年6月24日10时37分29秒
        /// <summary>
        /// 交班时获取上交班信息
        /// </summary>
        /// <returns></returns>
        public ActionResult getclassJbsx()
        {
            OD_HANDOVER_SW sw = new OD_HANDOVER_SW();
            sw.ONDUTYDATE = Request.Params["dt"];//值班日期
            sw.BYORGNO = SystemCls.getCurUserOrgNo();//单位编码
            sw.ONDUTYUSERTYPE = Request.Params["dcClass"];//当前班次
            sw.ONDUTYTYPE = "-1";//交班信息
            sw.isGetUPOne = "1";//查询上一班次
            OD_HANDOVER_Model m = OD_HANDOVERCls.getModel(sw);
            if (m == null)
            {
                m.OPCONTENT = "无交班信息";
            }
            m.OPCONTENT = m.OPCONTENT.Replace("<br>", "\n");
            return Content(JsonConvert.SerializeObject(new Message(true, m.OPCONTENT, "")), "text/html;charset=UTF-8");
        }

        #endregion


        #region 交班保存 叶磊2016年6月24日12时14分59秒
        /// <summary>
        /// 交班保存
        /// </summary>
        /// <returns></returns>
        public ActionResult HandOverCreatejb()
        {
            OD_HANDOVER_Model m = new OD_HANDOVER_Model();
            m.ONDUTYDATE = Request.Params["odutyTime"];//值班日期
            m.BYORGNO = SystemCls.getCurUserOrgNo(); ;//组织机构编码
            m.ONDUTYUSERTYPE =Request.Params["ondutyType"];//人员值班类别（早，中，晚）
            m.ONDUTYTYPE= "-1";//值班类别 -1代表交班信息
            m.OPCONTENT = Request.Params["opcontent"].Replace("\r\n", "<br>").Replace("\r", "<br>").Replace("\n", "<br>"); //交班信息
            m.ONDUTYUSERID = Request.Params["strUserId"];//值班人序号
            m.OPTIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now);//操作时间
            m.opMethod="Add";
            //验证
            if (string.IsNullOrEmpty(m.OPCONTENT))
                return Content(JsonConvert.SerializeObject(new Message(false, "交班内容不能为空", "")), "text/html;charset=UTF-8");
            Message msg=OD_HANDOVERCls.Manager(m);//保存交班信息

            //保存接班信息
            m.ONDUTYUSERID = Request.Params["jbrID"];//接班人ID
            m.ONDUTYTYPE = "-4";//代表接班
            m.OPCONTENT = "";//接班信息内容为空
             msg = OD_HANDOVERCls.Manager(m);//保存接班信息
             msg.Msg = "交班成功!";
             return Content(JsonConvert.SerializeObject(msg), "text/html;charset=UTF-8");
        }

        #endregion


        #region 根据当前日期，返回值班情况 叶磊 2016年6月24日14时58分53秒
        /// <summary>
        /// 根据当前日期，返回值班情况
        /// </summary>
        /// <returns></returns>
        public ActionResult getDCInfoByDT()
        {
            string dt = Request.Params["dt"];
            if (string.IsNullOrEmpty(dt))
                dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string[] arr = PublicClassLibrary.ClsStr.getWeeks(dt);
            //OD_ODTYPE_Model mT = OD_TYPECls.getModel(new OD_ODTYPE_SW { BYORGNO = SystemCls.getCurUserOrgNo(), isTopOne = "1" });
            //var listDate = O_ODDATECls.getListModel(new OD_DATE_SW { OD_TYPEID = mT.OD_TYPEID, DTBegin = arr[2], DTEnd = arr[3] });
            //O_ODDATE_Model dm = new O_ODDATE_Model();
            //dm.ONDUTYDATE = arr[2];
            //dm.ONDUTYYEAR = arr[3];
            ////dm.ODTYPEID = 13;
            //O_ODUSER_Model om = new O_ODUSER_Model();
            //om.BigONDUTYDATE = arr[2];
            //om.EndONDUTYDATE = arr[3];

            //om.BYORGNO = SystemCls.getCurUserOrgNo();//获取当前用户组织机构编码
            //om.FLag = "false";//非模糊
            OD_ODTYPE_Model m = OD_TYPECls.getModel(new OD_ODTYPE_SW { BYORGNO = SystemCls.getCurUserOrgNo(), isTopOne = "1" });


            var datalist = OD_DATECls.getListModel(new OD_DATE_SW { OD_TYPEID = m.OD_TYPEID, DTBegin = arr[2], DTEnd = arr[3] });
            // O_ODDATECls.GetDateModelList(new O_ODDATE_Model { ONDUTYDATE = arr[2], ONDUTYYEAR = arr[3] });
            //var result = O_OD_USERCls.GetListModel(new O_ODUSER_Model { BigONDUTYDATE = arr[2], EndONDUTYDATE = arr[3], BYORGNO = SystemCls.getCurUserOrgNo(), FLag = "false" });
            OD_USER_SW sw = new OD_USER_SW();
            sw.DTBegin = arr[2];
            sw.DTEnd = arr[3];
            sw.BYORGNO = SystemCls.getCurUserOrgNo();
            var result = OD_USERCls.getListModel(sw);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding='0' cellspacing='0'>");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th colspan=\"17\" class=\"divTable weekSel\"><a href=\"#\" class=\"border_ty3\" onclick=\"getdaybytype('{0}')\">上周</a>&nbsp;&nbsp;", arr[0]);
            sb.AppendFormat("<a href=\"#\" class=\"border_ty8\" onclick=\"getdaybytype('{0}')\">本周</a>&nbsp;&nbsp;", DateTime.Now.ToString("yyyy-MM-dd"));
            sb.AppendFormat("<a href=\"#\" class=\"border_ty10\" onclick=\"getdaybytype('{0}')\">下周</a></th></tr>", arr[4]);
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>日期</th>");
            sb.AppendFormat("<th>星期</th>");
            sb.AppendFormat("<th>早班 </th>");
            sb.AppendFormat("<th>中班</th>");
            sb.AppendFormat("<th>晚班</th>");
            sb.AppendFormat("<th>带班领导</th>");
            sb.AppendFormat("<th>总带班领导</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody >");
            foreach (var item in datalist)
            {
                if (DateTime.Parse(item.ONDUTYDATE) == DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
                    sb.AppendFormat("<tr class='DefaultRow'>");
                else
                    sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.ONDUTYDATE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.WEEK);
                //sb.AppendFormat("<td class=\"center\">{0}</td>", result.ToList()[0].BYORGNO);
                sb.AppendFormat("<td class=\"left\">{0}</td>", getuser1("1", result.Where(p => p.ONDUTYDATE == item.ONDUTYDATE).ToList()));
                sb.AppendFormat("<td class=\"left\">{0}</td>", getuser1("2", result.Where(p => p.ONDUTYDATE == item.ONDUTYDATE).ToList()));
                sb.AppendFormat("<td class=\"left\">{0}</td>", getuser1("3", result.Where(p => p.ONDUTYDATE == item.ONDUTYDATE).ToList()));
                sb.AppendFormat("<td class=\"left\">{0}</td>", getuser1("-1", result.Where(p => p.ONDUTYDATE == item.ONDUTYDATE).ToList()));
                sb.AppendFormat("<td class=\"left\">{0}</td>", getuser1("-2", result.Where(p => p.ONDUTYDATE == item.ONDUTYDATE).ToList()));
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
            }

            sb.AppendFormat("</tbody></table>");
            Message ms = new Message(true, sb.ToString(), "");
            if (!datalist.Any())
                ms = new Message(false, "该日期无需值班", "");
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");

        }
        private string getuser1(string str, IList<OD_USER_Model> list)
        {
            string strname = "";
            var str1 = list.Where(p => p.ONDUTYUSERTYPE == str).ToList();//;.Select(p => p.USERNAME).ToList();

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


        #region 获取各班次信息 叶磊 2016年6月25日10时56分57秒
        public ActionResult getClass()
        {
            string str = Request.Params["tm"];
            if (string.IsNullOrEmpty(str))
                str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            StringBuilder sb = new StringBuilder();
            string[] arr = PublicClassLibrary.ClsStr.getWeeks(str);
            var listW = OD_CLASSCls.GetListModel(new OD_CLASS_SW { });// O_ODCLASSCls.GetModelList();
            string class1 = "";//用于判断当前班次
            DateTime dtClass = Convert.ToDateTime(str);
            dtClass = dtClass.AddHours(1);
            IEnumerable<OD_CLASS_Model> query = from items in listW orderby items.ONDUTYBEGINTIME descending select items;
            string[] arrClassInfo = new string[4];//用于存放班次信息 为了与后面保持一致，index=0用不到
            foreach (var v in listW)
            {
                DateTime dt1 = DateTime.Parse(dtClass.ToString("yyyy-MM-dd abc:00").Replace("abc", v.ONDUTYBEGINTIME));
                DateTime dt2 = DateTime.Parse(dtClass.ToString("yyyy-MM-dd abc:00").Replace("abc", v.ONDUTYENDTIME));
                if (dt1 > dt2)
                    dt2 = dt2.AddDays(1);
                if (dt1 < dt2)
                {
                    if (dtClass > dt1 && dtClass <= dt2)
                    {
                        class1 = v.ONDUTYCLASSID;
                    }
                }
                else
                {
                    if (dtClass > dt1 && dtClass <= dt1.AddDays(1))
                    {
                        class1 = v.ONDUTYCLASSID;
                    }
                }
                if (v.ONDUTYCLASSID == "1")
                {
                    arrClassInfo[1] = v.ONDUTYCLASSNAME + "(" + v.ONDUTYBEGINTIME + "～" + v.ONDUTYENDTIME + ")";
                }
                else if (v.ONDUTYCLASSID == "2")
                {
                    arrClassInfo[2] = v.ONDUTYCLASSNAME + "(" + v.ONDUTYBEGINTIME + "～" + v.ONDUTYENDTIME + ")";
                }
                else
                {
                    arrClassInfo[3] = v.ONDUTYCLASSNAME + "(" + v.ONDUTYBEGINTIME + "～" + v.ONDUTYENDTIME + ")";
                }
            }
            //Response.Write("&nbsp;&nbsp;&nbsp;当前班次：");
            //Response.Write(class1);
            //Response.Write("<hr>");
            sb.AppendFormat("<table cellpadding='0' cellspacing='0'>");
            string[,] arrC = new string[3, 3];
            if (class1 == "1")//第一班
            {
                arrC[0, 0] = "2";//第一班次
                arrC[0, 1] = dtClass.AddDays(-1).ToString("yyyy-MM-dd");//日期
                arrC[0, 2] = arrClassInfo[2];//班次名称
                arrC[1, 0] = "3";//第二班次
                arrC[1, 1] = dtClass.AddDays(-1).ToString("yyyy-MM-dd");
                arrC[1, 2] = arrClassInfo[3];
                arrC[2, 0] = "1";//第三班次 当前班
                arrC[2, 1] = dtClass.ToString("yyyy-MM-dd");
                arrC[2, 2] = arrClassInfo[1];
            }
            if (class1 == "2")
            {
                arrC[0, 0] = "3";//第一班次
                arrC[0, 1] = dtClass.AddDays(-1).ToString("yyyy-MM-dd");//日期
                arrC[0, 2] = arrClassInfo[3];//班次名称
                arrC[1, 0] = "1";//第二班次
                arrC[1, 1] = dtClass.ToString("yyyy-MM-dd");
                arrC[1, 2] = arrClassInfo[1];
                arrC[2, 0] = "2";//第三班次 当前班
                arrC[2, 1] = dtClass.ToString("yyyy-MM-dd");
                arrC[2, 2] = arrClassInfo[2];
            }
            if (class1 == "3")
            {
                arrC[0, 0] = "1";//第一班次
                arrC[0, 1] = dtClass.ToString("yyyy-MM-dd");//日期
                arrC[0, 2] = arrClassInfo[1];
                arrC[1, 0] = "2";//第二班次
                arrC[1, 1] = dtClass.ToString("yyyy-MM-dd");
                arrC[1, 2] = arrClassInfo[2];
                arrC[2, 0] = "3";//第三班次 当前班
                arrC[2, 1] = dtClass.ToString("yyyy-MM-dd");
                arrC[2, 2] = arrClassInfo[3];
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

        #region 获取各班次详细信息 叶磊 2016年6月25日10时57分34秒
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

            OD_USER_SW sw = new OD_USER_SW();
            sw.DTBegin = dt;
            sw.DTEnd = dt;
            sw.BYORGNO = SystemCls.getCurUserOrgNo();
            var result = OD_USERCls.getListModel(sw);

            string isQD = "0";//是否签到 0非值班人员
            string isLD = "0";//是否领导 0非领导
            string CurUserID = SystemCls.getUserID();//当前登录用户
            var str1 = result.Where(p => p.ONDUTYUSERTYPE == dcClass).ToList();
            string Tmp = "";
            foreach (var v in str1)
            {
                Tmp += (string.IsNullOrEmpty(Tmp)) ? "" : ",";
                Tmp += (v.ISATTENDED != "1") ? "<font color=red>" + v.USERNAME + "</font>" : v.USERNAME;
                if (CurUserID == v.ONDUTYUSERID)
                    isQD = (v.ISATTENDED == "0") ? "1" : "2";//值班人员?未签到:已签到
            }

            string TmpLD = "";//领导
            var str2 = result.Where(p => p.ONDUTYUSERTYPE == "-1").ToList();
            foreach (var v in str2)
            {
                string dd1 = (v.ISATTENDED != "1") ? "<font color=red>" + v.USERNAME + "</font>" : v.USERNAME;
                TmpLD = (string.IsNullOrEmpty(Tmp)) ? dd1 : "," + dd1;
                if (CurUserID == v.ONDUTYUSERID)//登录用户为当前领导
                    isLD = (v.ISATTENDED == "0") ? "1" : "2";//领导 ?未签到:已签到
            }

            sb.AppendFormat("<td valign='top'>");
            sb.AppendFormat("签到信息：{0}", Tmp);

            string jbxx = "";//交班信息
            string ldyj = "";//领导意见
            if (1 == 1)//获取交班信息
            {
                //var HandData = O_HANDOVERCls.GetMoldeList(new O_HANDOVER_Model { ONDUTYTYPE = "-1", ONDUTYUSERTYPE = dcClass, BYORGNO = SystemCls.getCurUserOrgNo(), ONDUTYDATE = dt, FLag = "false" });
                var HandData = OD_HANDOVERCls.getListModel(new OD_HANDOVER_SW { ONDUTYTYPE = "-1", ONDUTYUSERTYPE = dcClass, BYORGNO = SystemCls.getCurUserOrgNo(), ONDUTYDATE = dt });
                foreach (var item in HandData)
                {
                    jbxx = item.OPCONTENT;
                }
            }
            if (1 == 1)////获取领导意见  改动后暂时去掉 ONDUTYUSERTYPE = dcClass 这个条件
            {
                //var HandData = O_HANDOVERCls.GetMoldeList(new O_HANDOVER_Model { ONDUTYTYPE = "-2", ONDUTYUSERTYPE = dcClass, BYORGNO = SystemCls.getCurUserOrgNo(), ONDUTYDATE = dt, FLag = "false" });

                var HandData = OD_HANDOVERCls.getListModel(new OD_HANDOVER_SW { ONDUTYTYPE = "-2", ONDUTYUSERTYPE = dcClass, BYORGNO = SystemCls.getCurUserOrgNo(), ONDUTYDATE = dt });
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
            sb.AppendFormat("</td>");

            return sb.ToString();
        }

        #endregion
        ////*********************************************************************


        ////值班查询页面*********************************************************************
        #region 值班查询首页 叶磊 2016年6月24日15时30分39秒
        /// <summary>
        /// 值班查询首页
        /// </summary>
        /// <returns></returns>
        public ActionResult OdutyPbQuery()
        {
            pubViewBag("010002", "010002", "");
            string strOrgno = SystemCls.getCurUserOrgNo();//获取当前用户的组织机构编码
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = strOrgno, TopORGNO = SystemCls.getCurUserOrgNo(), OnlyGetShiXian = "1" });
            ViewBag.curDate = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);
            return View();
        }
        #endregion

        #region 值班查询功能 叶磊 2016年6月24日15时31分26秒
        /// <summary>
        /// 值班查询功能
        /// </summary>
        /// <returns></returns>
        public ActionResult OdutyListQuery()
        {
            //ViewBag.userModelList = GetUserModelList(new OD_USER_Model { BYORGNO = Request.Params["BYORGNO"], ONDUTYDATE = Request.Params["TTBH"] });
            //Message ms = new Message(true, ViewBag.userModelList, "");
            //return Json(ms);
            return Content(JsonConvert.SerializeObject(new Message(true, GetUserModelList(new OD_USER_Model { BYORGNO = Request.Params["BYORGNO"], ONDUTYDATE = Request.Params["TTBH"] }), "")), "text/html;charset=UTF-8");
        }
        #endregion

        #region 组合值班查询表格 叶磊 2016年6月24日16时11分35秒
        /// <summary>
        /// 组合值班查询表格
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string GetUserModelList(OD_USER_Model o)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr role=\"row\">");
            sb.AppendFormat("<th class=\"center\">单位名称</th>");
            sb.AppendFormat("<th class=\"center\">早班 </th>");
            sb.AppendFormat("<th class=\"center\">中班</th>");
            sb.AppendFormat("<th class=\"center\">晚班</th>");
            sb.AppendFormat("<th class=\"center\">带班领导</th>");
            sb.AppendFormat("<th class=\"center\">总带班领导</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            //获取当前单位下属县级以上单位，只取到县级
            var orglist = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { OnlyGetShiXian = "1", TopORGNO = o.BYORGNO });// new List<T_SYS_ORGModel>();
            //var curorg = o.BYORGNO;
            //if (string.IsNullOrEmpty(curorg))
            //{
            //    curorg = SystemCls.getCurUserOrgNo();
            //}
            //var bo = PublicCls.OrgIsShi(curorg);//市级单位
            //var bb = PublicCls.OrgIsXian(curorg);//县级单位
            //if (bo)
            //{
            //    //市县
            //    orglist = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() }).Where(p => p.ORGNO.StartsWith(curorg.Substring(0, 4)) && p.ORGNO.EndsWith("000")).ToList();
            //}
            //if (bb)
            //{
            //    //县
            //    orglist = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() }).Where(p => p.ORGNO == curorg).ToList();
            //}

            var result = OD_USERCls.getListModel(new OD_USER_SW { ONDUTYDATE = o.ONDUTYDATE });// O_OD_USERCls.GetListModel(o);
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
                        sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("1", list));
                        sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("2", list));
                        sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("3", list));
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
                        sb.AppendFormat("<td class=\"center\">暂无</td>");
                        sb.AppendFormat("<td class=\"center\">暂无</td>");
                        sb.AppendFormat("<td class=\"center\">暂无</td>");
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

        #region 根据值班类别从List获取用户列表 叶磊 2016年6月24日16时11分50秒
        /// <summary>
        /// 根据值班类别从List获取用户列表
        /// </summary>
        /// <param name="str"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private string getuser(string str, IList<OD_USER_Model> list)
        {
            string strname = "";
            var str1 = list.Where(p => p.ONDUTYUSERTYPE == str).Select(p => p.USERNAME).ToList();
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

        ////*********************************************************************

        ////值班统计页面*********************************************************************
        #region 值班统计首页 叶磊 2016年6月24日17时00分08秒
        /// <summary>
        /// 值班统计首页
        /// </summary>
        /// <returns></returns>
        public ActionResult OdutyTongJi()
        {
            pubViewBag("010003", "010003", "");
            ViewBag.dateB = DateTime.Now.ToString("yyyy-MM-01");
            ViewBag.dateE = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);
            return View();
        }
#endregion
        #region 值班统计查询方法 叶磊 2016年6月24日17时00分42秒
        /// <summary>
        /// 值班统计方法
        /// </summary>
        /// <returns></returns>
        public ActionResult OdutyQueryUser()
        {
            OD_USER_SW sw = new OD_USER_SW();
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
        #region 值班统计的查询方法 叶磊 2016年6月24日17时00分58秒
        /// <summary>
        /// 值班统计的查询方法
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string GetQueryUserList(OD_USER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>值班人姓名</th>");
            sb.AppendFormat("<th>早班次数 </th>");
            sb.AppendFormat("<th>中班次数</th>");
            sb.AppendFormat("<th>晚班次数</th>");
            sb.AppendFormat("<th>带班人</th>");
            sb.AppendFormat("<th>带班次数</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody role=\"alert\" aria-live=\"polite\" aria-relevant=\"all\">");
            var result = OD_USERCls.GetDutyCount(sw);// O_OD_USERCls.GetDutyCount(o).ToList();
            int i = 0;
            foreach (var item in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");

                sb.AppendFormat("<td class=\"center\">{0}</td>", item.USERNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.zaobCount);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.zhongbCount);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.wanbCount);
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
        ////*********************************************************************


        ////日报情况查询首页*********************************************************************
        #region 日报情况查询首页 叶磊2016年6月25日09时52分23秒
        /// <summary>
        /// 日报情况查询首页
        /// </summary>
        /// <returns></returns>
        public ActionResult OdutyDaily()
        {
            pubViewBag("010004", "010004", "");
            ViewBag.Date = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);
            return View();
        }
        #endregion

        #region 日报提交查询请求的方法 叶磊2016年6月25日10时21分55秒

        /// <summary>
        /// 日报提交查询请求的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult OdutyDailyQuery()
        {
            //string strTime = Request.Params["TTBH"];//查询日期
            //string userORGON = SystemCls.getCurUserOrgNo();//获取当前用户组织机构编码
            //ViewBag.HandOverModelList = GetHandOverList(new O_HANDOVER_Model { BYORGNO = userORGON, ONDUTYDATE = strTime, ONDUTYTYPE = "-3" });
            OD_HANDOVER_SW sw = new OD_HANDOVER_SW();
            sw.BYORGNO = SystemCls.getCurUserOrgNo();//当前单位编码
            sw.ONDUTYDATE = Request.Params["TTBH"];//查询日期
            sw.ONDUTYTYPE = "-3";//日报
            if(string.IsNullOrEmpty(sw.ONDUTYDATE))
            return Content(JsonConvert.SerializeObject(new Message(false, "请选择查询日期", "")), "text/html;charset=UTF-8");
            string msg = GetHandOverList(sw);
            //Message ms = new Message(true, msg, "");
            return Content(JsonConvert.SerializeObject(new Message(true, msg, "")), "text/html;charset=UTF-8");
        }
        #endregion 

        #region 组合日报查询表格 叶磊2016年6月25日09时53分04秒

        /// <summary>
        /// 组合日报查询表格
        /// </summary>
        /// <param name="ohm"></param>
        /// <returns></returns>
        private string GetHandOverList(OD_HANDOVER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<th colspan=\"2\" class=\"center\"><h3 style=\"color:red;\">" + sw.ONDUTYDATE +PublicClassLibrary.ClsStr.Week(Convert.ToDateTime(sw.ONDUTYDATE)) + "</h3></th>");

            sb.AppendFormat("</thead>");
            //获取当前单位下属县级以上单位，只取到县级

            var orglist = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { OnlyGetShiXian = "1", TopORGNO = sw.BYORGNO });
            
            var result =OD_HANDOVERCls.getListModel( sw);//获取日报集合
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
                            sb.AppendFormat("<td class=\"center \"><span style=\"color:blue;width:20%;\">{0}(上报人：" + StateSwitch.GetUsrNameByUserid(v.ONDUTYUSERID) + ")</span></td>", org.ORGNAME);
                            //if (v.OPCONTENT.Contains("一"))
                            //    v.OPCONTENT = v.OPCONTENT.Insert(v.OPCONTENT.IndexOf("一"), "<br />");
                            //if (v.OPCONTENT.Contains("二"))
                            //    v.OPCONTENT = v.OPCONTENT.Insert(v.OPCONTENT.IndexOf("二") - 1, "<br />");
                            //if (v.OPCONTENT.Contains("三"))
                            //    v.OPCONTENT = v.OPCONTENT.Insert(v.OPCONTENT.IndexOf("三") - 1, "<br />");
                            //if (v.OPCONTENT.Contains("四"))
                            //    v.OPCONTENT = v.OPCONTENT.Insert(v.OPCONTENT.IndexOf("四") - 1, "<br />");
                            //if (v.OPCONTENT.Contains("五"))
                            //    v.OPCONTENT = v.OPCONTENT.Insert(v.OPCONTENT.IndexOf("五") - 1, "<br />");
                            //if (v.OPCONTENT.Contains("六"))
                            //    v.OPCONTENT = v.OPCONTENT.Insert(v.OPCONTENT.IndexOf("六") - 1, "<br />");
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
        ////*********************************************************************

        ////交班查询*********************************************************************
        #region 交班查询首页 叶磊2016年6月25日10时33分20秒
        /// <summary>
        /// 交班查询首页
        /// </summary>
        /// <returns></returns>
        public ActionResult OdutyShift()
        {
            pubViewBag("010005", "010005", "");
            ViewBag.Date = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);
            return View();
        }

        #endregion
        #region 交班查询按钮方法 叶磊2016年6月25日10时33分20秒
        /// <summary>
        /// 交班查询
        /// </summary>
        /// <returns></returns>
        public ActionResult OndutyShiftQuery()
        {
            OD_HANDOVER_SW sw = new OD_HANDOVER_SW();
            sw.BYORGNO = SystemCls.getCurUserOrgNo();//当前单位编码
            sw.ONDUTYDATE = Request.Params["TTBH"];//查询日期
            sw.ONDUTYTYPE = "-1";//交班
            if (string.IsNullOrEmpty(sw.ONDUTYDATE))
                return Content(JsonConvert.SerializeObject(new Message(false, "请选择查询日期", "")), "text/html;charset=UTF-8");
            string msg = GetShiftList(sw);
            //Message ms = new Message(true, msg, "");
            return Content(JsonConvert.SerializeObject(new Message(true, msg, "")), "text/html;charset=UTF-8");


            //string strTime = Request.Params["TTBH"];
            //string userORGON = SystemCls.getCurUserOrgNo();//获取当前用户组织机构编码
            //ViewBag.ShiftModelList = GetShiftList(new O_HANDOVER_Model { FLag = "false", BYORGNO = userORGON, ONDUTYDATE = strTime, ONDUTYTYPE = "-1,-2,-4" });
            //Message ms = new Message(true, ViewBag.ShiftModelList, "");
            ////return Json(ms);
            //return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        #endregion
        #region 交班查询组合表格 叶磊2016年6月25日10时33分20秒
        private string GetShiftList(OD_HANDOVER_SW sw)
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
            var result = OD_HANDOVERCls.getListModel(new OD_HANDOVER_SW { BYORGNO = sw.BYORGNO, ONDUTYDATE = sw.ONDUTYDATE });
            var resultUser = OD_USERCls.getListModel(new OD_USER_SW { BYORGNO = sw.BYORGNO, ONDUTYDATE = sw.ONDUTYDATE });
            int i = 0;
            foreach (var v in result.Where(p => p.ONDUTYTYPE == "-1"))
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
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ONDUTYUSERName);//交班人
                var jv = result.Where(p => p.ONDUTYTYPE == "-4" && p.ONDUTYUSERTYPE == v.ONDUTYUSERTYPE).FirstOrDefault();//接班人
                if (jv != null)
                    sb.AppendFormat("<td class=\"center\">{0}</td>", jv.ONDUTYUSERName);//交班人
                else
                    sb.AppendFormat("<td class=\"USERID\">{0}</td>", "&nbsp;");

                var ldV = resultUser.Where(p => p.ONDUTYUSERTYPE == "-1").FirstOrDefault();//带班领导
                if (ldV != null)
                    sb.AppendFormat("<td class=\"center\">{0}</td>", ldV.USERNAME);
                else
                    sb.AppendFormat("<td class=\"USERID\">{0}</td>", "&nbsp;");
                sb.AppendFormat("<td class=\"USERID\">{0}</td>", v.OPCONTENT);//交班内容

                var ldyjV = result.Where(p => p.ONDUTYTYPE == "-2" && p.ONDUTYUSERTYPE == v.ONDUTYUSERTYPE).FirstOrDefault();//领导意见
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
        ////*********************************************************************

        ////历史值班********************************************************************
        #region 历史值班记录 叶磊2016年6月22日17时54分11秒
        /// <summary>
        /// 历史值班记录
        /// </summary>
        /// <returns></returns>
        public ActionResult Historyduty()
        {
            pubViewBag("010007", "010007", "");
            //获取组织机构下拉框
            string strOrgno = SystemCls.getCurUserOrgNo();//获取当前用户的组织机构编码  
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = strOrgno, TopORGNO = SystemCls.getCurUserOrgNo(), OnlyGetShiXian = "1" });
            //获取标题名称下拉框
            OD_ODTYPE_Model od = new OD_ODTYPE_Model();
            ViewBag.dataList = OD_TYPECls.GetModelList(od);
            return View();
        }

        #endregion

        #region  查询按钮方法 叶磊2016年6月25日10时58分54秒
        /// <summary>
        /// 查询按钮方法
        /// </summary>
        /// <returns></returns>
        public ActionResult HistoryQuery()
        {
            string orgNo = Request.Params["BYORGNO"];
            string typeID = Request.Params["typeID"];
            //string bigTime = ""; string endTime = "";
            //if (string.IsNullOrEmpty(typenameID))
            //    return Json(new Message(false, "", ""));
            //OD_ODTYPE_Model od = new OD_ODTYPE_Model();
            //od.OD_TYPEID = typenameID;
            //var data = OD_TYPECls.GetModelList(od);
            //foreach (var item in data)
            //{
            //    bigTime = item.OD_DATEBEGIN;
            //    endTime = item.OD_DATEEND;
            //}
            //O_ODUSER_Model ou = new O_ODUSER_Model();
            //ou.BigONDUTYDATE = bigTime;
            //ou.EndONDUTYDATE = endTime;
            //ou.BYORGNO = byorgno;
            //ou.FLag = "false";
            if (string.IsNullOrEmpty(typeID))
                return Content(JsonConvert.SerializeObject(new Message(false, "没有查询到值班信息", "")), "text/html;charset=UTF-8");
            string msg = GetUserModelListType(new OD_USER_SW { BYORGNO = orgNo, OD_TYPEID = typeID });

            return Content(JsonConvert.SerializeObject(new Message(true, msg, "")), "text/html;charset=UTF-8");

        }
        private string GetUserModelListType(OD_USER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>单位名称</th>");
            sb.AppendFormat("<th>日期</th>");
            sb.AppendFormat("<th>星期</th>");
            sb.AppendFormat("<th>早班 </th>");
            sb.AppendFormat("<th>中班</th>");
            sb.AppendFormat("<th>晚班</th>");
            sb.AppendFormat("<th>带班领导</th>");
            sb.AppendFormat("<th>总带班领导</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = OD_USERCls.getListModel(sw);// O_OD_USERCls.GetListModel(o);
            if (result.Any())
            {
                int i = 0;
                foreach (var item in result.OrderBy(p => p.ONDUTYDATE).Select(p => p.ONDUTYDATE).Distinct())
                {
                    var recordresult = result.Where(p => p.ONDUTYDATE == item).ToList();
                    if (i % 2 == 0)
                        sb.AppendFormat("<tr>");
                    else
                        sb.AppendFormat("<tr class='row1'>");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", recordresult[0].ORGNAME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", item);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", PublicClassLibrary.ClsStr.Week(item));
                    sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("1", recordresult));
                    sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("2", recordresult));
                    sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("3", recordresult));
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

        #region 根据机构编码获取对应的类型下拉框 叶磊2016年6月23日17时39分28秒
        /// <summary>
        /// 根据机构编码获取对应的类型下拉框
        /// </summary>
        /// <param name="orgNo"></param>
        /// <returns></returns>
        public ActionResult getDutyTYPEByOrgNo(string orgNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<select id=\"tbxOD_TYPE\">");
            var list = OD_TYPECls.getListModel(new OD_ODTYPE_SW { BYORGNO = orgNo });
            foreach (var v in list)
            {
                sb.AppendFormat("<option value=\"{0}\">{1}</option>", v.OD_TYPEID, v.OD_TYPENAME);
            }
            if (list.Any() == false)
            {
                sb.AppendFormat("<option value=\"{0}\">{1}</option>", "", "暂无排班信息");
            }
            sb.AppendFormat("</select>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        #endregion

        ////*********************************************************************

        ////周末值班********************************************************************
        #region 周末值班查询首页 叶磊2016年6月23日17时55分14秒
        /// <summary>
        /// 周末值班查询首页
        /// </summary>
        /// <returns></returns>
        public ActionResult WeekData()
        {
            pubViewBag("010008", "010008", "");
            string strOrgno = SystemCls.getCurUserOrgNo();//获取当前用户的组织机构编码
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = strOrgno, TopORGNO = SystemCls.getCurUserOrgNo(), OnlyGetShiXian = "1" });

            ViewBag.dateB = DateTime.Now.ToString("yyyy-MM-01");
            ViewBag.dateE = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);
            return View();
        }
        #endregion

        #region 查询按钮方法 叶磊2016年6月23日17时55分37秒
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ActionResult weekdataQuery()
        {
            OD_USER_SW sw = new OD_USER_SW();
            sw.DTBegin = Request.Params["BEGTIME"];//开始时间
            sw.DTEnd = Request.Params["ENDTIME"];//结束时间
            sw.curOrgNo = Request.Params["BYORGNO"];

            //验证
            if (string.IsNullOrEmpty(sw.DTBegin) == true || string.IsNullOrEmpty(sw.DTEnd) == true)
                return Content(JsonConvert.SerializeObject(new Message(false, "请选择日期！", "")), "text/html;charset=UTF-8"); 
            if (PublicClassLibrary.ClsSwitch.compDate(sw.DTBegin, sw.DTEnd, "0") == false)
                return Content(JsonConvert.SerializeObject(new Message(false, "开始时间应小于结束时间！", "")), "text/html;charset=UTF-8");

            string msg = GetQueryUserWeek(sw);
            return Content(JsonConvert.SerializeObject(new Message(true, msg, "")), "text/html;charset=UTF-8");
        }

        #endregion

        #region 拼接周末值班次数的数据 叶磊 2016年6月22日17时55分58秒
        /// <summary>
        /// 拼接周末值班次数的数据
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string GetQueryUserWeek(OD_USER_SW sw)
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
            var result = OD_USERCls.getWeekCountListModel(sw).ToList();
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

        ////*********************************************************************

        #region 无用

        //#region 值班处理 上周、下周、本周数据集合
        /////// <summary>
        /////// 值班处理 上周、下周、本周数据集合
        /////// </summary>
        /////// <returns></returns>
        ////public JsonResult GegDutyUserList()
        ////{
        ////    string strBTime = Request.Params["BigTime"];
        ////    string strEndTime = Request.Params["EndTime"];
        ////    OD_DATE_Model dm = new OD_DATE_Model();
        ////    dm.ONDUTYDATE = strBTime;
        ////    dm.ONDUTYYEAR = strEndTime;
        ////    var oneData = OD_TYPECls.GetOneData();
        ////    foreach (var item in oneData)
        ////    {
        ////        dm.ODTYPEID = Convert.ToInt32(item.OD_TYPEID);

        ////    }

        ////    O_ODUSER_Model om = new O_ODUSER_Model();
        ////    om.BigONDUTYDATE = strBTime;
        ////    om.EndONDUTYDATE = strEndTime;

        ////    om.BYORGNO = SystemCls.getCurUserOrgNo();//获取当前用户组织机构编码
        ////    om.FLag = "false";//非模糊
        ////    var datalist = O_ODDATECls.GetDateModelList(dm);
        ////    var result = OD_USERCls.getListModel(new OD_USER_SW { });// O_OD_USERCls.GetListModel(om);
        ////    int i = 0;
        ////    StringBuilder sb = new StringBuilder();
        ////    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
        ////    sb.AppendFormat("<thead>");
        ////    sb.AppendFormat("<tr class='row1'><th style=\"text-align:right;\" colspan=\"7\"><a href=\"javascript:;\"><span onclick=\"getdaybytype(2)\">上周</span></a>&nbsp;&nbsp;");
        ////    sb.AppendFormat("<a href=\"javascript:;\"><span onclick=\"getdaybytype(1)\">本周</span></a>&nbsp;&nbsp;");
        ////    sb.AppendFormat("<a href=\"javascript:;\"><span onclick=\"getdaybytype(3)\">下周</span></a></th></tr>");
        ////    sb.AppendFormat("<tr>");
        ////    sb.AppendFormat("<th class=\"center\">日期</th>");
        ////    sb.AppendFormat("<th class=\"center\">星期</th>");
        ////    sb.AppendFormat("<th class=\"center\">早班 </th>");
        ////    sb.AppendFormat("<th class=\"center\">中班</th>");
        ////    sb.AppendFormat("<th class=\"center\">晚班</th>");
        ////    sb.AppendFormat("<th class=\"center\">带班领导</th>");
        ////    sb.AppendFormat("<th class=\"center\">总带班领导</th>");
        ////    sb.AppendFormat("</tr>");
        ////    sb.AppendFormat("</thead>");
        ////    sb.AppendFormat("<tbody>");
        ////    foreach (var item in datalist)
        ////    {
        ////        if (i % 2 == 0)
        ////            sb.AppendFormat("<tr>");
        ////        else
        ////            sb.AppendFormat("<tr class='row1'>");
        ////        sb.AppendFormat("<td class=\"center\">{0}</td>", item.ONDUTYDATE);
        ////        sb.AppendFormat("<td class=\"center\">{0}</td>", item.WEEK);
        ////        sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("1", result.Where(p => p.ONDUTYDATE == item.ONDUTYDATE).ToList()));
        ////        sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("2", result.Where(p => p.ONDUTYDATE == item.ONDUTYDATE).ToList()));
        ////        sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("3", result.Where(p => p.ONDUTYDATE == item.ONDUTYDATE).ToList()));
        ////        sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("-1", result.Where(p => p.ONDUTYDATE == item.ONDUTYDATE).ToList()));
        ////        sb.AppendFormat("<td class=\"center\">{0}</td>", getuser("-2", result.Where(p => p.ONDUTYDATE == item.ONDUTYDATE).ToList()));
        ////        sb.AppendFormat("</td>");
        ////        sb.AppendFormat("</tr>");
        ////        i++;
        ////    }

        ////    sb.AppendFormat("</tbody></table>");
        ////    Message ms = new Message(true, sb.ToString(), "");
        ////    return Json(ms);
        ////}
        //#endregion

        //#region 值班人员签到方法 ******************无用**************
        ///// <summary>
        ///// 值班人员签到方法
        ///// </summary>
        ///// <returns></returns>
        ////public JsonResult Sign()
        ////{
        ////    T_SYSSEC_IPSUSER_SW ts = new T_SYSSEC_IPSUSER_SW();
        ////    ts.curOrgNo = SystemCls.getCurUserOrgNo();//获取当前用户的组织机构编码
        ////    string ondutyType = Request.Params["ondutyType"];//当前用户值班类别
        ////    string strUserId = SystemCls.getUserID();//当前用户ID
        ////    string ondutyDate = Request.Params["odutyTime"];//当前值班日期            
        ////    //O_ODUSER_Model odm = new O_ODUSER_Model();
        ////    //odm.BYORGNO = ts.curOrgNo;
        ////    //odm.ISATTENDED = "1";
        ////    //odm.ONDUTYDATE = ondutyDate;
        ////    //odm.ONDUTYUSERTYPE = ondutyType;


        ////    O_ODUSER_Model om = new O_ODUSER_Model();
        ////    om.BYORGNO = ts.curOrgNo;
        ////    om.ONDUTYUSERTYPE = ondutyType;
        ////    om.ONDUTYDATE = ondutyDate;
        ////    om.ISATTENDED = "1";
        ////    var dataList = O_OD_USERCls.GetListModel(om).ToList();
        ////    if (dataList.Count <= 0)
        ////    {
        ////        O_HANDOVER_Model ohm = new O_HANDOVER_Model();
        ////        ohm.BYORGNO = ts.curOrgNo;
        ////        ohm.ONDUTYDATE = ondutyDate;
        ////        ohm.ONDUTYTYPE = "-4";//代表接班人
        ////        ohm.ONDUTYUSERTYPE = ondutyType;//值班班次（早，中，晚）
        ////        ohm.ONDUTYUSERID = strUserId;
        ////        ohm.opMethod = "add";
        ////        ohm.OPTIME = "getdate()";//操作时间
        ////        Message msg = O_HANDOVERCls.Manager(ohm);
        ////    }

        ////    om.ONDUTYUSERID = strUserId;
        ////    Message ms = O_OD_USERCls.Sign(om);


        ////    return Json(ms);
        ////}

        //#endregion

        //#region 获取签到数据
        ///// <summary>
        ///// 获取签到数据
        ///// </summary>
        ///// <returns></returns>
        //public JsonResult SignDate()
        //{
        //    T_SYSSEC_IPSUSER_SW ts = new T_SYSSEC_IPSUSER_SW();
        //    ts.curOrgNo = SystemCls.getCurUserOrgNo();//获取当前用户的组织机构编码
        //    string ondutyType = Request.Params["ondutyType"];//当前用户值班类别
        //    string strUserId = SystemCls.getUserID();//当前用户ID
        //    string ondutyDate = Request.Params["odutyTime"];//当前值班日期
        //    O_ODUSER_Model om = new O_ODUSER_Model();
        //    om.BYORGNO = ts.curOrgNo;
        //    om.ONDUTYUSERTYPE = ondutyType;
        //    //om.ONDUTYUSERID = strUserId;
        //    om.ONDUTYDATE = ondutyDate;
        //    //om.ISATTENDED = "1";
        //    om.FLag = "false";
        //    var dataList = O_OD_USERCls.GetListModel(om);
        //    StringBuilder sb = new StringBuilder();
        //    foreach (var item in dataList)
        //    {
        //        if (item.ISATTENDED == "1")
        //            sb.AppendFormat(item.USERNAME + ":已签到,");
        //        if (item.ISATTENDED == "0")
        //            sb.AppendFormat(item.USERNAME + ":未签到,");

        //    }
        //    Message ms = new Message(true, sb.ToString(), "");
        //    return Json(ms);
        //}
        //public JsonResult HandOverData()
        //{
        //    string ondutyUserType = Request.Params["ondutyType"];//人员值班类别
        //    string ONDUTYTYPE = "-1";//值班类别
        //    string byorgno = SystemCls.getCurUserOrgNo(); ;//组织机构编码
        //    string odutyTime = Request.Params["odutyTime"];//值班日期
        //    O_HANDOVER_Model ohm = new O_HANDOVER_Model();//实例化值班交班记录表
        //    ohm.ONDUTYTYPE = ONDUTYTYPE;
        //    ohm.ONDUTYUSERTYPE = ondutyUserType;
        //    ohm.BYORGNO = byorgno;
        //    ohm.ONDUTYDATE = odutyTime;
        //    var HandData = O_HANDOVERCls.GetMoldeList(ohm);
        //    StringBuilder sb = new StringBuilder();
        //    foreach (var item in HandData)
        //    {
        //        sb.AppendFormat(item.OPCONTENT);
        //    }
        //    Message ms = new Message(true, sb.ToString(), "");
        //    return Json(ms);
        //}

        //public JsonResult HandOverCreate()
        //{
        //    string ondutyDate = Request.Params["odutyTime"];//值班日期
        //    string byorgno = SystemCls.getCurUserOrgNo(); ;//组织机构编码
        //    string ondutyUserType = Request.Params["ondutyType"];//人员值班类别（早，中，晚）
        //    string ondutyType = "-1";//值班类别 -1代表交班信息
        //    string opcontent = Request.Params["opcontent"];//交班信息
        //    string ondutyuserID = Request.Params["strUserId"];//值班人序号
        //    O_ODUSER_Model odm = new O_ODUSER_Model();
        //    odm.ONDUTYDATE = ondutyDate;
        //    odm.BYORGNO = byorgno;
        //    odm.ONDUTYUSERID = ondutyuserID;
        //    var ondutyTypeDate = O_OD_USERCls.GetListModel(odm);
        //    foreach (var item in ondutyTypeDate)
        //    {
        //        ondutyUserType = item.ONDUTYUSERTYPE;
        //    }

        //    O_HANDOVER_Model ohm = new O_HANDOVER_Model();
        //    ohm.BYORGNO = byorgno;
        //    ohm.ONDUTYDATE = ondutyDate;
        //    ohm.ONDUTYTYPE = ondutyType;
        //    ohm.ONDUTYUSERID = ondutyuserID;
        //    ohm.ONDUTYUSERTYPE = ondutyUserType;
        //    ohm.OPCONTENT = opcontent;
        //    ohm.OPTIME = "getdate()";//操作时间
        //    ohm.FLag = "false";
        //    var HandData = O_HANDOVERCls.GetMoldeList(ohm);
        //    if (HandData.Any())//交班信息前判断是否存在，存在先删除再添加
        //    {
        //        foreach (var item in HandData)
        //        {
        //            ohm.ODHID = item.ODHID;
        //            ohm.opMethod = "del";
        //            O_HANDOVERCls.Manager(ohm);
        //        }
        //    }
        //    ohm.opMethod = "add";
        //    Message msg = O_HANDOVERCls.Manager(ohm);
        //    return Json(msg);
        //}
        //#endregion


        //#region indexText

        //public ActionResult indexText()
        //{
        //    pubViewBag("010006", "010006", "");
        //    OD_DATE_Model om = new OD_DATE_Model();
        //    var oneData = OD_TYPECls.GetOneData();
        //    foreach (var item in oneData)
        //    {
        //        ViewBag.id = item.OD_TYPEID;
        //        ViewBag.title = item.OD_TYPENAME;
        //        ViewBag.bigTime = item.OD_DATEBEGIN;
        //        ViewBag.endTime = item.OD_DATEEND;
        //        om.ODTYPEID = Convert.ToInt32(item.OD_TYPEID);
        //    }
        //    StringBuilder sb = new StringBuilder();
        //    T_SYSSEC_IPSUSER_SW ts = new T_SYSSEC_IPSUSER_SW();
        //    ts.curOrgNo = SystemCls.getCurUserOrgNo();//获取当前用户的组织机构编码
        //    var list1 = T_SYSSEC_IPSUSERCls.getListMode(ts);
        //    foreach (var item in list1)
        //    {
        //        sb.AppendFormat("<option value=\"{0}\">{1}</option>", item.USERID, item.USERNAME);
        //    }
        //    ViewBag.ZBR = sb.ToString();//当前单位所有值班人

        //    return View();
        //}

        //#endregion



        #endregion

    }
}
