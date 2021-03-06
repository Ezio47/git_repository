﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagerSystemClassLibrary;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using ManagerSystemModel.SDEModel;
using PublicClassLibrary;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using PublicClassLibrary.ThirdDockService;
using log4net;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ManagerSystemClassLibrary.SDECLS;
using OAModel;
using ManagerSystem.MVC.HelpCom;
using ManagerSystem.MVC.Models;

namespace ManagerSystem.MVC.Controllers
{
    public class SystemController : BaseController
    {
        private ILog logs = LogHelper.GetInstance();

        #region 面积单位
        private string dic113Name = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "113" }).ToList()[0].DICTNAME;
        #endregion

        #region 主页
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 日志管理
        /// <summary>
        /// 日志管理
        /// </summary>
        /// <returns></returns>
        public ActionResult LogMan()
        {
            pubViewBag("006006", "006006", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.T_Method = Request.Params["Method"];
            string ID = Request.Params["ID"];
            ViewBag.T_ID = ID;
            return View();
        }

        /// <summary>
        /// 根据序号获取Json内容
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult getLogJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            return Content(JsonConvert.SerializeObject(T_SYS_LOGCls.getModel(new T_SYS_LOG_SW { LOGID = ID })), "text/html;charset=UTF-8");
        }


        /// <summary>
        /// 列表
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult LogList()
        {
            pubViewBag("006006", "006006", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdLogType = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTID = "1" });
            ViewBag.vdLogType = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "1" });
            return View();
        }

        public ActionResult getLogListAjax()
        {

            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            string OPERATION = Request.Params["OPERATION"];
            string LogType = Request.Params["LogType"];
            string TIMEBegin = Request.Params["TIMEBegin"];
            string TIMEEnd = Request.Params["TIMEEnd"];

            if (string.IsNullOrEmpty(TIMEBegin) == false) TIMEBegin = TIMEBegin + " 00:00:00";
            if (string.IsNullOrEmpty(TIMEEnd) == false) TIMEEnd = TIMEEnd + " 23:59:59";

            int total = 0;
            T_SYS_LOG_SW sw = new T_SYS_LOG_SW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), OPERATION = OPERATION, LOGTYPE = LogType, TIMEEnd = TIMEEnd, TIMEBegin = TIMEBegin };


            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr><th>序号</th><th>类别</th><th>标题</th><th>操作人</th><th>操作IP</th><th>操作时间</th> </tr></thead>");
            sb.AppendFormat("<tbody>");
            var result = T_SYS_LOGCls.getListPagerModel(sw, out  total);
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", ((sw.curPage - 1) * sw.pageSize + i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOGTYPENAME);
                //sb.AppendFormat("<td class=\"left\"><a href=\"/System/LogMan?Method=See&ID={0}\">{1}</a></td>", v.LOGID, v.OPERATION);
                sb.AppendFormat("<td class=\"left\"><a href='#' onclick=\"See('{0}','See')\">{1}</a></td>", v.LOGID, v.OPERATION);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOGINUSERName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.USERIP);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.OPERATETIME);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");

            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }

        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="sw">查询模型</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        private string getLogStr(T_SYS_LOG_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr><th>序号</th><th>类别</th><th>标题</th><th>操作人</th><th>操作IP</th><th>操作时间</th> </tr></thead>");
            sb.AppendFormat("<tbody>");
            var result = T_SYS_LOGCls.getListPagerModel(sw, out  total);
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", ((sw.curPage - 1) * sw.pageSize + i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOGTYPENAME);
                //sb.AppendFormat("<td class=\"left\"><a href=\"/System/LogMan?Method=See&ID={0}\">{1}</a></td>", v.LOGID, v.OPERATION);
                sb.AppendFormat("<td class=\"left\"><a href='#' onclick=\"See('{0}','See')\">{1}</a></td>", v.LOGID, v.OPERATION);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOGINUSERName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.USERIP);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.OPERATETIME);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        #endregion

        #region 通知公告管理

        [ValidateInput(false)]
        public ActionResult NoticeManager()
        {
            string INFOID = Request.Params["ID"];
            string INFOTITLE = Request.Params["INFOTITLE"];
            string INFOCONTENT = Request.Params["INFOCONTENT"];
            string LABLE = Request.Params["LABLE"];
            string FBTIME = Request.Params["FBTIME"];
            string Method = Request.Params["Method"];
            string returnUrl = Request.Params["returnUrl"];
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/System/NoticeList";
            //默认为添加
            T_SYS_NOTICE_Model m = new T_SYS_NOTICE_Model();
            if (string.IsNullOrEmpty(Method))
                Method = "Add";
            if (string.IsNullOrEmpty(INFOTITLE))
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入标题!", "")), "text/html;charset=UTF-8");
            if (Method == "Add")
                m.NUM = "0";
            m.FBTIME = FBTIME;
            m.INFOID = INFOID;
            m.INFOTITLE = INFOTITLE;
            m.INFOCONTENT = INFOCONTENT;
            m.INFOURL = "";
            m.LABLE = LABLE;
            m.INFOTYPE = "1";
            m.INFOUSERID = SystemCls.getUserID();
            m.SYSFLAG = ConfigCls.getSystemFlag();
            m.opMethod = Method;
            m.returnUrl = returnUrl;
            return Content(JsonConvert.SerializeObject(T_SYS_NOTICECls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 管理
        /// </summary>
        /// <returns>参见模型</returns>
        [ValidateInput(false)]
        public ActionResult NoticeMan()
        {
            pubViewBag("006005", "006005", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.T_Method = Request.Params["Method"];
            //如果未传参数，默认为添加
            if (string.IsNullOrEmpty(ViewBag.T_Method))
                ViewBag.T_Method = "Add";
            string ID = Request.Params["ID"];
            ViewBag.T_ID = ID;
            return View();
        }

        /// <summary>
        /// 根据序号获取内容
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult getNoticeJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            return Content(JsonConvert.SerializeObject(T_SYS_NOTICECls.getModel(new T_SYS_NOTICE_SW { INFOID = ID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查询时，跳转页面
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult NoticeListQuery()
        {
            string PageSize = Request.Params["PageSize"];
            string Page = Request.Params["Page"];
            string INFOTITLE = Request.Params["INFOTITLE"];
            string str = ClsStr.EncryptA01(PageSize + "|" + INFOTITLE, "kkkkkkkk");
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/System/NoticeList?trans=" + str + "&page=" + Page)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult NoticeList()
        {
            pubViewBag("006005", "006005", "");
            if (ViewBag.isPageRight == false)
            {
                return View();
            }
            string page = Request.Params["page"];//当前页数
            string trans = Request.Params["trans"];//传递网页参数
            if (string.IsNullOrEmpty(page)) { page = "1"; }
            //查询条件
            string[] arr = new string[3];//存放查询条件的数组 根据实际存放的数据
            if (string.IsNullOrEmpty(trans) == false)
            {
                arr = ClsStr.DecryptA01(trans, "kkkkkkkk").Split('|');
            }
            if (string.IsNullOrEmpty(arr[0]) == true)
            {
                arr[0] = PagerCls.getDefaultPageSize().ToString();//默认记录数
            }
            ViewBag.INFOTITLE = arr[1];//显示查询值 用户名
            //列表
            int total = 0;
            ViewBag.UserList = getNoticeStr(new T_SYS_NOTICE_SW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), INFOTITLE = arr[1], INFOTYPE = "1" }, out total);
            //分页信息 需使用上面的total
            ViewBag.PagerInfo = PagerCls.getPagerInfo(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), rowCount = total, url = "/System/NoticeList?trans=" + trans });
            return View();
        }

        private string getNoticeStr(T_SYS_NOTICE_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"sample-table-2\" class=\"table table-striped table-bordered table-hover dataTable\" aria-describedby=\"sample-table-2_info\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr role=\"row\">");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" rowspan=\"1\" colspan=\"1\" aria-label=\"\" style=\"width: 1px;\">序号</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\" aria-label=\"Price: activate to sort column ascending\" style=\"width: 5px;\">标题</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\" aria-label=\"Price: activate to sort column ascending\" style=\"width: 5px;\">发布时间</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\" aria-label=\"Clicks: activate to sort column ascending\" style=\"width: 5px;\">发布人</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" rowspan=\"1\" colspan=\"1\" aria-label=\"\" style=\"width: 20px;\"></th>");
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody role=\"alert\" aria-live=\"polite\" aria-relevant=\"all\">");
            var result = T_SYS_NOTICECls.getListPagerModel(sw, out total);
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", ((sw.curPage - 1) * sw.pageSize + i + 1).ToString());
                sb.AppendFormat("<td class=\"left  sorting_1\"><a href=\"/System/NoticeMan?Method=See&ID={0}\">{1}</a></td>", v.INFOID, v.INFOTITLE);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.FBTIME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.LOGINUSERNAME);
                sb.AppendFormat("    <td class=\" \">");
                sb.AppendFormat("        <div class=\"visible-md visible-lg hidden-sm hidden-xs action-buttons\">");
                sb.AppendFormat("            <a class=\"green\" href=\"/System/NoticeMan?Method=Mdy&ID={0}\">", v.INFOID);
                sb.AppendFormat("                <i class=\"icon-pencil bigger-130\"></i>");
                sb.AppendFormat("            </a>");
                sb.AppendFormat("            <a class=\"red\" href=\"/System/NoticeMan?Method=Del&ID={0}\" onclick=\"return confirm('确实要删除该内容吗?')\">", v.INFOID);
                sb.AppendFormat("                <i class=\"icon-trash bigger-130\"></i>");
                sb.AppendFormat("            </a>");
                sb.AppendFormat("        </div>");
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        #endregion

        #region 护林员管理
        /// <summary>
        /// 护林员增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult FRUserManager()
        {
            string BIRTH = Request.Params["BIRTH"];
            string BYORGNO = Request.Params["BYORGNO"];
            string HID = Request.Params["ID"];
            string HNAME = Request.Params["HNAME"];
            string ONSTATE = Request.Params["ONSTATE"];
            string PHONE = Request.Params["PHONE"];
            string SEX = Request.Params["SEX"];
            string ISENABLE = Request.Params["ISENABLE"];
            string SN = Request.Params["SN"];
            string Method = Request.Params["Method"];
            string returnUrl = Request.Params["returnUrl"];
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/System/FRUserList";
            //默认为添加
            if (string.IsNullOrEmpty(Method)) { Method = "Add"; }
            if (Method == "Add" || Method == "Mdy")
            {
                if (string.IsNullOrEmpty(HNAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入护林员姓名!", "")), "text/html;charset=UTF-8");
            }
            if (Method == "Enable" || Method == "UnEnable" || Method == "Del")
            {
                if (string.IsNullOrEmpty(HID))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请选择要操作的护林员!", "")), "text/html;charset=UTF-8");
            }
            T_IPSFR_USER_Model m = new T_IPSFR_USER_Model();
            m.BIRTH = BIRTH;
            m.BYORGNO = BYORGNO;
            m.HID = HID;
            m.HNAME = HNAME;
            m.ONSTATE = ONSTATE;
            m.PHONE = PHONE;
            m.SEX = SEX;
            m.SN = SN;
            m.ISENABLE = ISENABLE;
            m.opMethod = Method;
            m.returnUrl = returnUrl;
            var ms = T_IPSFR_USERCls.Manager(m);
            //更新三维责任区和责任路线
            if (ms.Success && (m.opMethod == "Mdy" || m.opMethod == "Del"))
            {
                //修改责任线
                var m1 = new TD_DUTYROUTE_Model();
                m1.OBJECTID = m.HID;
                m1.NAME = m.HNAME;
                m1.TELEPHONE = m.PHONE;
                var Org = T_SYS_ORGCls.getModel(new T_SYS_ORGSW { ORGNO = m.BYORGNO });
                m1.ORGNAME = Org.ORGNAME;
                m1.opMethod = m.opMethod;
                DC_DUTYROUTECls.Manager(m1);
                //修改责任面
                var m2 = new TD_DUTYAREA_Model();
                m2.OBJECTID = m.HID;
                m2.NAME = m.HNAME;
                m2.TELEPHONE = m.PHONE;
                m2.ORGNAME = Org.ORGNAME;
                m2.opMethod = m.opMethod;
                DC_DUTYAREACls.Manager(m2);
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 校验手机号码
        /// </summary>
        /// <returns></returns>
        public JsonResult ValidatePhone()
        {
            var phone = Request.Params["tbxPHONE"];
            var method = Request.Params["method"];
            var id = Request.Params["ID"];
            Message ms = null;
            var result = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW());
            if (method == "Add" || string.IsNullOrEmpty(method))
            {
                var recod = result.Where(p => p.PHONE.Trim().Contains(phone.Trim()));
                if (!recod.Any())
                {
                    ms = new Message(true, "手机号码不重复", "");
                }
                else
                {
                    ms = new Message(false, "手机号码重复", "");
                }
            }
            else if (method == "Mdy")
            {
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(phone))
                {
                    var phoneNo = result.Where(p => p.HID == id).FirstOrDefault().PHONE.Trim();
                    if (!string.IsNullOrEmpty(phoneNo) && phone.Trim() == phoneNo)
                    {
                        ms = new Message(true, "手机号码不重复", "");
                    }
                    else
                    {
                        var recod = result.Where(p => p.PHONE.Contains(phone));
                        if (!recod.Any())
                        {
                            ms = new Message(true, "手机号码不重复", "");
                        }
                        else
                        {
                            ms = new Message(false, "手机号码重复", "");
                        }
                    }
                }
            }
            return Json(ms);
        }

        /// <summary>
        /// 护林员管理
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult FRUserMan()
        {
            pubViewBag("006004", "006004", "");
            if (ViewBag.isPageRight == false)
                return View();
            string ID = Request.Params["ID"];
            ViewBag.T_ID = ID;
            if (string.IsNullOrEmpty(ID) == false)
            {
                if (string.IsNullOrEmpty(ViewBag.T_ID))
                    return Redirect("/System/Error?ID=2");//参数错误  
                string otid = Request.Params["tNo"];
                if (string.IsNullOrEmpty(otid))
                    return Redirect("/System/Error?ID=3");//参数错误 
                if (ClsStr.EncryptA01(ViewBag.T_ID, "kdiekdfd") != otid)
                    return Redirect("/System/Error?ID=4");//参数错误 
            }
            ViewBag.T_Method = Request.Params["Method"];
            //如果未传参数，默认为添加
            if (string.IsNullOrEmpty(ViewBag.T_Method)) { ViewBag.T_Method = "Add"; }
            //角色复选框
            //ViewBag.RoleChk = T_SYSSEC_ROLECls.getRoleAndUid(new T_SYSSEC_ROLE_SW { USERID = ViewBag.T_USERID });
            ViewBag.vdSex = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTFLAG = "性别" });
            ViewBag.vdONSTATE = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTFLAG = "固兼职状态" });
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo(), IsEnableCUN = "1" });
            ViewBag.vdISENABLE = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTFLAG = "启用状态", DICTVALUE = "1" });
            return View();
        }

        /// <summary>
        /// 根据序号获取内容
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult getFRUserJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            return Content(JsonConvert.SerializeObject(T_IPSFR_USERCls.getModel(new T_IPSFR_USER_SW { HID = ID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 护林员列表
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult FRUserList()
        {
            pubViewBag("006004", "006004", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo(), IsEnableCUN = "1" });// ipsuM.ORGNAME });
            ViewBag.vdISENABLE = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTFLAG = "启用状态", isShowAll = "1" });
            return View();
        }
        /// <summary>
        /// Ajax调用 返回护林员列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getFRUserListAjax()
        {

            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            string HNAME = Request.Params["HNAME"];
            string BYORGNO = Request.Params["BYORGNO"];
            string ISENABLE = Request.Params["ISENABLE"];

            int total = 0;
            T_IPSFR_USER_SW sw = new T_IPSFR_USER_SW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), PhoneHname = HNAME, BYORGNO = BYORGNO, ISENABLE = ISENABLE };

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th><input name=\"tbxRightID\" id=\"tbxRightID\" type=\"checkbox\"  onclick=\"selectall(this.value,this.checked)\"/></th>");
            //sb.AppendFormat("        <th>行政区划</th>");
            sb.AppendFormat("        <th>单位</th>");
            sb.AppendFormat("        <th>姓名</th>");
            sb.AppendFormat("        <th>终端编号</th>");
            sb.AppendFormat("        <th>手机号码</th>");
            sb.AppendFormat("        <th>性别</th>");
            sb.AppendFormat("        <th>出生日期</th>");
            sb.AppendFormat("        <th>固\\兼职	</th>");
            sb.AppendFormat("        <th>状态	</th>");
            sb.AppendFormat("        <th>巡检距离(米)</th>");
            sb.AppendFormat("        <th></th>");
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = T_IPSFR_USERCls.getListPagerModel(sw, out total);
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr onclick=\"setColor(this)\">");
                else
                    sb.AppendFormat("<tr class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center\"><input name=\"chk\" id=\"chk\" type=\"checkbox\" value=\"{1}\" />{0}</td>", ((sw.curPage - 1) * sw.pageSize + i + 1).ToString(), v.HID);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGNAME);
                string tNo = ClsStr.EncryptA01(v.HID, "kdiekdfd");
                //sb.AppendFormat("<td class=\"center\"><a href=\"/System/FRUserMan?Method=See&ID={0}&tNo={1}\" title='查看详细信息'>{2}</a></td>", v.HID, tNo, v.HNAME);
                sb.AppendFormat("<td class=\"center\"><a href='#' onclick=\"See('See','{0}','{1}')\"  title='查看详细信息'>{2}</a></td>", v.HID, tNo, v.HNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.SN);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PHONE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.SEXNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.BIRTH);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ONSTATENAME);
                if (v.ISENABLE == "0")
                {
                    sb.AppendFormat("<td class=\"center\"><font color=red>{0}</font></td>", v.ISENABLENAME);
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.ISENABLENAME);
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PATROLLENGTH);
                sb.AppendFormat("    <td class=\" \">");
                sb.AppendFormat("&nbsp;<a href=\"#\" onclick=\"GetPhoto('{0}')\" title='图片' class=\"searchBox_01 LinkPhoto\">图片</a>", v.HID);
                //sb.AppendFormat("            <a href=\"/System/FRUserMan?Method=Mdy&ID={0}&tNo={1}\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", v.HID, tNo);
                sb.AppendFormat("            <a href='#' onclick=\"Mdy('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", v.HID, tNo);
                string color = "";
                color = (v.isExitsLine == "0") ? " red" : "green";
                sb.AppendFormat("            <a style=\"color:{2}\" onclick=\"RouteManage('{0}','{1}')\" title='路线管理' class=\"searchBox_01 LinkRoute\">路线管理</a>", v.HID, tNo, color);
                color = (v.isExitsRail == "0") ? "red" : "green";
                sb.AppendFormat("            <a style=\"color:{2}\"   onclick=\"FenceManage('{0}','{1}')\"  title='责任区管理' class=\"searchBox_01 LinkFence\">责任区管理</a>", v.HID, tNo, color);
                sb.AppendFormat("&nbsp;<a href='#' onclick='Manager({0})' class=\"searchBox_01 LinkDel\">删除</a>", v.HID);
                if (SystemCls.isRight("006004007") == true)
                {
                    if (v.MOBILEPARAMLIST == "" || v.MOBILEPARAMLIST == null)
                    {
                        //sb.AppendFormat("            <a  href=\"/System/SetParameter?Method=Update&ID={0}&MOBILEPARAMLIST={1}&orgno={2}\" class=\"searchBox_01 LinkParameter\" >参数设置</a>", v.HID, v.MOBILEPARAMLIST, v.BYORGNO);
                    }
                    else
                    {
                        sb.AppendFormat("            <a style=\"color:orange\"  href=\"/System/SetParameter?Method=Update&ID={0}&MOBILEPARAMLIST={1}&orgno={2}\" class=\"searchBox_01 LinkParameter\">参数修改</a>", v.HID, v.MOBILEPARAMLIST, v.BYORGNO);
                    }
                }
                //sb.AppendFormat("            <a class=\"red\" href=\"/System/FRUserMan?Method=Del&ID={0}&tNo={1}\" onclick=\"return confirm('确实要删除该内容吗?')\" title='删除'>", v.HID, tNo);
                //sb.AppendFormat("                <i class=\"icon-trash bigger-130\"></i>");
                //sb.AppendFormat("            </a>");
                //sb.AppendFormat("        </div>");
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");

            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }


        #endregion

        #region 护林员参数设置页面
        public ActionResult SetParameter()
        {
            pubViewBag("006004", "006004", "");
            if (ViewBag.isPageRight == false) { return View(); }
            ViewBag.Method = Request.Params["Method"];
            ViewBag.ID = Request.Params["ID"];
            ViewBag.MOBILEPARAMLIST = Request.Params["MOBILEPARAMLIST"];
            ViewBag.ORGNO = Request.Params["orgno"];
            return View();
        }

        /// <summary>
        ///获取护林员参数数据
        /// </summary>
        /// <returns></returns>
        public ActionResult getHlyParameter()
        {
            var model = "";
            var ID = Request.Params["ID"];
            var MOBILEPARAMLIST = Request.Params["MOBILEPARAMLIST"];
            var orgno = Request.Params["ORGNO"];
            var ORGMOBILEPARAMLIST = T_SYS_ORGCls.getModel(new T_SYS_ORGSW { ORGNO = orgno }).MOBILEPARAMLIST;
            if (MOBILEPARAMLIST != "")//从user表取
            {
                model = T_IPSFR_USERCls.getModel(new T_IPSFR_USER_SW { HID = ID }).MOBILEPARAMLIST;
            }
            else if (ORGMOBILEPARAMLIST != "")//从org表取
            {
                model = ORGMOBILEPARAMLIST;
            }
            else//获取默认值
            {
                var result = T_SYS_PARAMETERCls.getListModel(new T_SYS_PARAMETER_SW());
                var SOS_TEL = result.Where(p => p.PARAMFLAG == "SOS_TEL").FirstOrDefault().PARAMVALUE;
                var FQCY = result.Where(p => p.PARAMFLAG == "FQCY").FirstOrDefault().PARAMVALUE;
                var STATR_TIME = result.Where(p => p.PARAMFLAG == "STATR_TIME").FirstOrDefault().PARAMVALUE;
                var END_TIME = result.Where(p => p.PARAMFLAG == "END_TIME").FirstOrDefault().PARAMVALUE;
                var WEB_SERVICE_URL = result.Where(p => p.PARAMFLAG == "WEB_SERVICE_URL").FirstOrDefault().PARAMVALUE;
                var TransEanbleDate = result.Where(p => p.PARAMFLAG == "TransEanbleDate").FirstOrDefault().PARAMVALUE;
                model = SOS_TEL + "$" + FQCY + "$" + STATR_TIME + "$" + END_TIME + "$" + WEB_SERVICE_URL + "$" + TransEanbleDate + "$$$$$$$$$$";
            }
            return Content(JsonConvert.SerializeObject(model), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 保存护林员参数数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Save()
        {
            T_IPSFR_USER_Model m = new T_IPSFR_USER_Model();
            Message ms = null;
            var ID = Request.Params["ID"];
            var Method = Request.Params["Method"];
            var Number = Request.Params["tbxNumber"];
            var Frequence = Request.Params["tbxFrequence"];
            var StartTime = Request.Params["tbxStartTime"];
            var EndTime = Request.Params["tbxEndTime"];
            var Addr = Request.Params["tbxAddr"];
            var Date = Request.Params["tbxDate"];
            var phone = T_IPSFR_USERCls.getModel(new T_IPSFR_USER_SW { HID = ID }).PHONE;
            if (string.IsNullOrEmpty(Method))
            {
                Method = "Cancle";
                m.MOBILEPARAMLIST = "";
            }
            else
            {
                m.MOBILEPARAMLIST = Number + "$" + Frequence + "$" + StartTime + "$" + EndTime + "$" + Addr + "$" + Date + "$$$$$$$$$$";
            }
            m.HID = ID;
            m.opMethod = Method;
            ms = T_IPSFR_USERCls.Manager(m);
            if (ms.Success)
            {
                var mobileNotice = ConfigCls.getConfigValue("mobileParameterService");//配置文件读取 1 为通知 0 不通知手机端
                if (mobileNotice == "1")
                {
                    if (!string.IsNullOrEmpty(phone))
                    {
                        try
                        {
                            new TaskUtil().NotifyRefreshData("userParams", phone);//服务==》通知手机端修改参数
                        }
                        catch (Exception ex)
                        {
                            logs.Error("调用参数修改通知手机服务错误", ex);
                        }
                    }
                }
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        #endregion

        #region 根据单位列表获取所有护林员ID
        /// <summary>
        /// 根据单位列表获取所有护林员
        /// </summary>
        /// <returns></returns>
        public ActionResult getFRUIDListByOrgs()
        {
            string orgs = Request.Params["orgs"];
            var list = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { Orgs = orgs, ISENABLE = "1" });
            string str = "";
            foreach (var v in list)
            {
                if (str != "") { str += ","; }
                str += v.HID;
            }
            return Content(JsonConvert.SerializeObject(new Message(true, str, "")), "text/html;charset=UTF-8");
        }
        #endregion

        #region 护林员导入时先读取数据
        /// <summary>
        /// 护林员导入时先读取数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult FRUserListBrowse()
        {
            pubViewBag("006004", "006004", "");
            var result = new List<T_IPSFR_USER_Model>();

            if (Request.Params["savePath"] != "" && Request.Params["savePath"] != null)//文件模板导入
            {
                string filePath = Server.UrlDecode(Request.Params["savePath"]);
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
                int rowCount = sheet.LastRowNum;
                for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                {

                    IRow row = sheet.GetRow(i);
                    string[] arr = new string[9];
                    for (int k = 0; k < arr.Length; k++)
                    {
                        if (k != 8)
                            arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                        else
                            arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                    }
                    T_IPSFR_USER_Model m = new T_IPSFR_USER_Model();
                    //所属县	所属乡镇	所属乡村	姓名	终端编号	手机号码	性别	固兼职	出生日期
                    if (string.IsNullOrEmpty(arr[2]) == false)
                    {
                        m.ORGNAME = arr[2];
                    }
                    if (string.IsNullOrEmpty(arr[1]) == false)
                    {
                        m.ORGXZNAME = arr[1];
                    }
                    if (string.IsNullOrEmpty(arr[0]) == false)
                    {
                        m.ORGXSNAME = arr[0];
                    }

                    m.HNAME = arr[3];
                    m.SN = arr[4];
                    m.PHONE = arr[5];
                    if (string.IsNullOrEmpty(arr[6]))//性别
                    {
                        m.SEX = "0";
                    }
                    else
                    {
                        m.SEX = (arr[6] == "男") ? "0" : "1";
                    }
                    m.SEXNAME = arr[6];
                    if (string.IsNullOrEmpty(arr[7]))//是否固职
                    {
                        m.ONSTATE = "1";
                    }
                    else
                    {
                        m.ONSTATE = (arr[7] == "固职") ? "1" : "2";
                    }
                    m.ONSTATENAME = arr[7];
                    m.BIRTH = arr[8];
                    if (m.BIRTH == "9999-12-31")
                    {
                        m.BIRTH = "1900-01-01";
                    }
                    m.ISENABLE = "1";
                    result.Add(m);
                }
            }
            StringBuilder sb = new StringBuilder();
            #region 数据表
            sb.AppendFormat("<table id=\"FRUserListBrowse\" cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th colspan=\"10\">护林员导入浏览</th></tr>");
            sb.AppendFormat("<tr><th style=\"width:10%;\">所属县</th><th style=\"width:10%;\">所属乡镇</th><th style=\"width:10%;\">所属乡村</th><th style=\"width:10%;\">姓名</th><th style=\"width:10%;\">终端编号</th><th style=\"width:10%;\">手机号码</th><th style=\"width:5%;\">性别</th><th style=\"width:5%;\">固兼职</th><th style=\"width:10%;\">出生日期</th><th style=\"width:20%;\">状态</th></tr>");
            sb.AppendFormat("</thead>");
            #endregion
            int j = 0;
            if (result.Any())
            {

                foreach (var item in result)
                {
                    #region 表身
                    sb.AppendFormat("<tbody id=\"tcontent\" >");
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxORGXS" + j + "\" name=\"tbxORGXS\" type=\"text\" style=\"width:98%;\" class=\"center\" value=\"" + item.ORGXSNAME + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxORGXZ" + j + "\" name=\"tbxORGXZ\" type=\"text\" style=\"width:98%;\" class=\"center\" value=\"" + item.ORGXZNAME + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxORGCUN" + j + "\" name=\"tbxORGCUN\" type=\"text\" style=\"width:98%;\" class=\"center\" value=\"" + item.ORGNAME + "\" />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxHNAME" + j + "\" name=\"tbxHNAME\" style=\"width:98%;\" type=\"text\" class=\"center\" value=\"" + item.HNAME + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxSN" + j + "\" name=\"tbxSN\" style=\"width:98%;\" type=\"text\" class=\"center\" value=\"" + item.SN + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxPHONE" + j + "\" name=\"tbxPHONE\" style=\"width:98%;\" type=\"text\" class=\"center\" value=\"" + item.PHONE + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxSEXNAME" + j + "\" name=\"tbxSEXNAME\" style=\"width:98%;\" type=\"text\" class=\"center\" value=\"" + item.SEXNAME + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxONSTATENAME" + j + "\" name=\"tbxONSTATENAME\" style=\"width:98%;\" type=\"text\" class=\"center\" value=\"" + item.ONSTATENAME + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxBIRTH" + j + "\" name=\"tbxBIRTH\" type=\"text\" style=\"width:98%;\" class=\"Wdate\" onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd'})\"   value=\"" + Convert.ToDateTime(item.BIRTH).ToString("yyyy-MM-dd") + "\" />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxState" + j + "\" name=\"tbxState\" style=\"width:98%;\" type=\"text\" class=\"center\" />");
                    j++;
                    #endregion
                }
                sb.AppendFormat("</tbody>");
                sb.AppendFormat("</table>");
            }
            #endregion
            ViewBag.pageList = sb.ToString();
            ViewBag.row = j;
            return View();
        }
        #endregion

        #region 护林员上传
        [HttpPost]
        public ActionResult FRUserListBrowse(FormCollection form)
        {
            pubViewBag("006004", "006004", "");
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                //扩展名
                string extension = System.IO.Path.GetExtension(File.FileName);//
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串
                    string name = DateTime.Now.ToString("护林员导入-yyyyMMddHHmmss") + extension;
                    if (!FileType.Contains(extension))
                    {
                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件!');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content(@"<script>alert('上传文件超过4M，不能上传!');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/HRExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        var EncodeSavepath = Server.UrlEncode(savePath);//编码 
                        return Content("<script>window.location.href='FRUserListBrowse?savePath=" + EncodeSavepath + "'</script>");
                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传!');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的护林员表格!');history.go(-1);</script>");
                }
            }

            return View();
        }
        #endregion

        #region 护林员导入管理
        /// <summary>
        /// 护林员导入管理
        /// </summary>
        /// <returns></returns>
        public ActionResult FRUserUpload()
        {
            T_IPSFR_USER_Model m = new T_IPSFR_USER_Model();
            string State = Request.Params["State"];
            string BYORGNOSXName = Request.Params["BYORGNOSXName"];
            string BYORGNOCUNName = Request.Params["BYORGNOCUNName"];
            string BYORGNOSZName = Request.Params["BYORGNOSZName"];
            if (string.IsNullOrEmpty(BYORGNOCUNName) == false)
            {
                m.BYORGNO = T_SYS_ORGCls.getCodeByName(BYORGNOCUNName);
                if (string.IsNullOrEmpty(m.BYORGNO))
                {
                    return Content(JsonConvert.SerializeObject(new Message(false, "乡村名称错误或者该乡村未录入组织机构!", "")), "text/html;charset=UTF-8");
                }
            }
            else if (string.IsNullOrEmpty(BYORGNOSZName) == false)
            {
                m.BYORGNO = T_SYS_ORGCls.getCodeByName(BYORGNOSZName);
                if (string.IsNullOrEmpty(m.BYORGNO))
                {
                    return Content(JsonConvert.SerializeObject(new Message(false, "乡镇名称错误或者该乡镇未录入组织机构!", "")), "text/html;charset=UTF-8");
                }
            }
            else
            {
                m.BYORGNO = T_SYS_ORGCls.getCodeByName(BYORGNOSXName);
                if (string.IsNullOrEmpty(m.BYORGNO))
                {
                    return Content(JsonConvert.SerializeObject(new Message(false, "县市名称错误或者该县市未录入组织机构!", "")), "text/html;charset=UTF-8");
                }
            }
            string ONSTATENAME = Request.Params["ONSTATENAME"];
            if (string.IsNullOrEmpty(ONSTATENAME))
            {
                m.ONSTATE = "1";
            }
            else
            {
                m.ONSTATE = (ONSTATENAME == "固职") ? "1" : "2";
            }
            m.HNAME = Request.Params["HNAME"];
            //m.ONSTATE = Request.Params["ONSTATE"];
            m.PHONE = Request.Params["PHONE"];
            if (SpringerCommonValidate.IsHandset(m.PHONE) == false)
            {
                return Content(JsonConvert.SerializeObject(new Message(false, "手机号码格式错误,请重新输入!", "")), "text/html;charset=UTF-8");
            }
            string SEXNAME = Request.Params["SEXNAME"];
            if (string.IsNullOrEmpty(SEXNAME))
            {
                m.SEX = "0";
            }
            else
            {
                m.SEX = (SEXNAME == "男") ? "0" : "1";
            }
            m.ISENABLE = Request.Params["ISENABLE"];
            m.SN = Request.Params["SN"];
            m.opMethod = "Add";
            m.BIRTH = Request.Params["BIRTH"];
            if (string.IsNullOrEmpty(m.HNAME))
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入护林员姓名!", "")), "text/html;charset=UTF-8");
            if (State == "成功")
            {
                return Content(JsonConvert.SerializeObject(new Message(true, "成功", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(T_IPSFR_USERCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 线路/围栏管理
        public ActionResult TestMap()
        {
            return View();
        }
        public ActionResult TestMap1()
        {
            return View();
        }

        /// <summary>
        /// 护林员线管理
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult FRUserRotMan()
        {
            pubViewBag("006004", "006004", "");
            if (ViewBag.isPageRight == false) { return View(); }
            string id = Request.Params["ID"];
            string tNo = Request.Params["tNo"];
            //id加密
            string idstr = ClsStr.EncryptA01(id, "kdiekdfd");
            if (idstr != tNo) { return View(); }
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// 护林员围栏管理
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult FRUserRaiMan()
        {
            pubViewBag("006004", "006004", "");
            if (ViewBag.isPageRight == false) { return View(); }
            string id = Request.Params["ID"];
            string tNo = Request.Params["tNo"];
            //id加密
            string idstr = ClsStr.EncryptA01(id, "kdiekdfd");
            if (idstr != tNo) { return View(); }
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// 获取护林员线路管理坐标点
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult GetFRUserRots()
        {
            string id = Request.Params["id"];//护林员id
            string type = Request.Params["type"];//点类型
            if (string.IsNullOrEmpty(id)) { return Json(new Message(false, "护林员id参数传递错误", "")); }
            var sw = new T_IPSFR_ROUTERAIL_SW();
            sw.HID = id;
            sw.ROADTYPE = type;
            var list = T_IPSFR_ROUTERAILCls.getModelList(sw);
            var ValuesList = list.Select(p => p.LINEARAEID).Distinct();
            var DataModel = new MutipileLineAndPolyModel();
            var data = new List<IEnumerable<T_IPSFR_ROUTERAIL_Model>>();
            if (ValuesList.Any())
            {
                foreach (var value in ValuesList)
                {
                    var RecordList = list.Where(p => p.LINEARAEID == value);
                    if (RecordList.Any())
                    {
                        data.Add(RecordList);
                    }
                }
            }
            DataModel.DataList = data;
            return Json(new MessageObject(true, DataModel));
        }

        /// <summary>
        /// 获取护林员采集线路
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRoadList()
        {
            string id = Request.Params["id"];//护林员id
            string type = Request.Params["type"];//点类型
            //护林员线路采集id获取
            var roadsw = new T_IPSCOL_COLLECT_SW();
            roadsw.SYSTYPEVALUE = type;
            roadsw.HID = id;
            var roadlist = T_IPSCOL_COLLECTCls.getModelList(roadsw).OrderBy(p => p.COLLECTTIME);
            return Json(new MessageListObject(true, roadlist));
        }

        /// <summary>
        /// 保存护林员线坐标点
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult SaveFRUserRot()
        {
            string id = Request.Params["id"];//护林员id
            string pointstr = Request.Params["points"];//采集点
            string type = Request.Params["type"];//采集类型，0是责任线，1是责任面
            string length = Request.Params["length"];//护林员id
            string jd = "";
            string wd = "";
            string dx = "";
            string dy = "";
            string line = "";
            string line1 = "";
            string polygon = "";
            string polygon1 = "";
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pointstr)) { return Json(new Message(false, "参数传递错误", "")); }
            var m = new T_IPSFR_ROUTERAIL_Model();
            m.HID = id;
            m.longitLatitList = pointstr;
            m.opMethod = "AddBatch";
            m.ROADTYPE = type;
            m.longitLatitList = m.longitLatitList.Replace("|", ", , |");
            var ms = T_IPSFR_ROUTERAILCls.Manager(m);
            var m6 = new T_IPSFR_USER_Model();
            m6.HID = id;
            m6.opMethod = "PATROLLENGTH";
            m6.PATROLLENGTH = length;
            var ms1 = T_IPSFR_USERCls.Manager(m6);
            //入三维空间库
            #region 责任线
            if (type == "0")
            {
                m.longitLatitList = m.longitLatitList.Replace(",,", "");
                if (!string.IsNullOrEmpty(m.longitLatitList))
                {
                    var result = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { HID = id }).FirstOrDefault();//获取护林员信息
                    var m1 = new TD_DUTYROUTE_Model();//三维责任路线模型
                    m1.opMethod = m.opMethod;
                    m1.NAME = result.HNAME;
                    m1.OBJECTID = result.HID;
                    m1.ORGNAME = result.ORGNAME;
                    m1.TELEPHONE = result.PHONE;
                    string[] arr1 = m.longitLatitList.Split(';');
                    for (int j = 0; j < arr1.Length - 1; j++)
                    {
                        string[] arr = arr1[j].Split('|');
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(arr[i]))
                            {
                                string[] brr = arr[i].Split(',');
                                double[] drr = ClsPositionTrans.GpsTransform(double.Parse(brr[1]), double.Parse(brr[0]), ConfigCls.getSDELonLatTransform());//坐标系转换
                                wd = drr[0].ToString();
                                jd = drr[1].ToString();
                            }
                            if (i == arr.Length - 1)//最后一条记录
                            {
                                line += jd + " " + wd + "|";
                            }
                            else
                            {
                                line += jd + " " + wd + ",";
                            }
                        }
                        string[] arr2 = line.Split('|');
                        if (j == arr1.Length - 1)//最后一条记录
                        {
                            line1 += "";
                        }
                        else
                        {
                            line1 += "(" + arr2[j].ToString() + "),";
                        }
                        #region 中心点获取
                        if (arr.Length % 2 == 0)
                        {
                            string[] crr = arr[arr.Length / 2].Split(',');
                            double[] drr = ClsPositionTrans.GpsTransform(double.Parse(crr[1]), double.Parse(crr[0]), ConfigCls.getSDELonLatTransform());
                            m1.DISPLAY_X = drr[1].ToString();
                            m1.DISPLAY_Y = drr[0].ToString();
                        }
                        else
                        {
                            string[] crr = arr[(arr.Length + 1) / 2].Split(',');
                            double[] drr = ClsPositionTrans.GpsTransform(double.Parse(crr[1]), double.Parse(crr[0]), ConfigCls.getSDELonLatTransform());//中心点偏移
                            m1.DISPLAY_X = drr[1].ToString();
                            m1.DISPLAY_Y = drr[0].ToString();
                        }
                        #endregion
                    }
                    if (line1 != "")
                    {
                        line1 = line1.Substring(0, line1.LastIndexOf(","));
                    }
                    m1.Shape = "geometry::STGeomFromText('MULTILINESTRING(" + line1 + ")',4326).MakeValid()";
                    DC_DUTYROUTECls.Manager(m1);
                }
            }
            #endregion

            #region 责任面
            else
            {
                m.longitLatitList = m.longitLatitList.Replace(",,", "");
                if (!string.IsNullOrEmpty(m.longitLatitList))
                {
                    var result = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { HID = id }).FirstOrDefault();//获取护林员信息
                    var m2 = new TD_DUTYAREA_Model();//三维责任区模型
                    m2.opMethod = m.opMethod;
                    m2.NAME = result.HNAME;
                    m2.OBJECTID = result.HID;
                    m2.ORGNAME = result.ORGNAME;
                    m2.TELEPHONE = result.PHONE;
                    string[] arr1 = m.longitLatitList.Split(';');
                    for (int j = 0; j < arr1.Length; j++)
                    {
                        string[] arr = arr1[j].Split('|');
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(arr[i]))
                            {
                                string[] brr = arr[i].Split(',');
                                double[] drr = ClsPositionTrans.GpsTransform(double.Parse(brr[1]), double.Parse(brr[0]), ConfigCls.getSDELonLatTransform());//坐标系转换
                                wd = drr[0].ToString();
                                jd = drr[1].ToString();
                            }
                            dx += jd + ",";
                            dy += wd + ",";
                            if (i == arr.Length - 1)//最后一条记录
                            {
                                polygon += jd + " " + wd + "|";
                            }
                            else
                            {
                                polygon += jd + " " + wd + ",";
                            }
                        }
                        string[] arr2 = polygon.Split('|');
                        if (j == arr1.Length - 1)//最后一条记录
                        {
                            polygon1 += "";
                        }
                        else
                        {
                            polygon1 += "(" + arr2[j].ToString() + "),";
                        }
                        #region 中心点获取
                        string[] crr = dx.Split(',');
                        string[] jrr = dy.Split(',');
                        var jdbegin = crr[0];
                        var wdbegin = jrr[0];
                        var jdend = crr[crr.Length - 2];
                        var wdend = jrr[jrr.Length - 2];
                        //if (jdbegin.Trim() != jdend.Trim() || wdbegin.Trim() != wdend.Trim())
                        //    polygon = polygon + "," + jdbegin + " " + wdbegin;
                        var jdmax = crr.Where(p => p != "").Select(p => float.Parse(p)).Max();
                        var jdmin = crr.Where(p => p != "").Select(p => float.Parse(p)).Min();
                        var wdmax = jrr.Where(p => p != "").Select(p => float.Parse(p)).Max();
                        var wdmin = jrr.Where(p => p != "").Select(p => float.Parse(p)).Min();
                        m2.DISPLAY_X = ((jdmax + jdmin) / 2).ToString();
                        m2.DISPLAY_Y = ((wdmax + wdmin) / 2).ToString();
                        #endregion
                    }
                    if (polygon1 != "")
                    {
                        polygon1 = polygon1.Substring(0, polygon1.LastIndexOf(","));
                    }
                    m2.Shape = "geometry::STGeomFromText('MULTIPolygon((" + polygon1 + "))',4326).MakeValid()";
                    DC_DUTYAREACls.Manager(m2);
                }
            }
            #endregion
            return Json(ms);
        }

        /// <summary>
        /// 删除点(线)
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult DeleteFRUserRot()
        {
            string hid = Request.Params["hid"];//hid
            string rodetype = Request.Params["rodetype"];//rodetype
            string type = Request.Params["type"];//rodetype
            if (string.IsNullOrEmpty(hid))
            {
                return Json(new Message(false, "hid参数传递错误", ""));
            }
            if (string.IsNullOrEmpty(rodetype))
            {
                return Json(new Message(false, "rodetype参数传递错误", ""));
            }

            var m = new T_IPSFR_ROUTERAIL_Model();
            m.HID = hid;
            m.ROADTYPE = rodetype;
            m.opMethod = "DelBatch";
            var ms = T_IPSFR_ROUTERAILCls.Manager(m);
            if (ms.Success)
            {
                if (type == "0")//删除线
                {
                    var m1 = new TD_DUTYROUTE_Model();
                    m1.OBJECTID = m.HID;
                    m1.opMethod = m.opMethod;
                    DC_DUTYROUTECls.Manager(m1);
                }
                else  //删除面
                {
                    var m2 = new TD_DUTYAREA_Model();
                    m2.OBJECTID = m.HID;
                    m2.opMethod = m.opMethod;
                    DC_DUTYAREACls.Manager(m2);
                }
            }
            return Json(ms);
        }

        #endregion

        #region 权限管理
        /// <summary>
        /// 权限管理－增、删、改
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult RightManager()
        {
            T_SYSSEC_RIGHT_Model m = new T_SYSSEC_RIGHT_Model();
            m.RIGHTID = Request.Params["RIGHTID"];
            m.RIGHTNAME = Request.Params["RIGHTNAME"];
            m.SYSFLAG = Request.Params["SYSFLAG"];
            m.ORDERBY = Request.Params["ORDERBY"];
            m.opMethod = Request.Params["Method"];
            m.returnUrl = Request.Params["returnUrl"];

            //默认为添加
            if (string.IsNullOrEmpty(m.opMethod))
                m.opMethod = "Add";
            if (string.IsNullOrEmpty(m.RIGHTID))
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入或选择权限编号!", "")), "text/html;charset=UTF-8");
            if (string.IsNullOrEmpty(m.RIGHTNAME))
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入或选择权限名称!", "")), "text/html;charset=UTF-8");
            return Content(JsonConvert.SerializeObject(T_SYSSEC_RIGHTCls.Manager(m)), "text/html;charset=UTF-8");
        }

        public ActionResult getRightJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            return Content(JsonConvert.SerializeObject(T_SYSSEC_RIGHTCls.getModel(new T_SYSSEC_RIGHT_SW { RIGHTID = ID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 权限列表
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult RightList()
        {
            pubViewBag("006003", "006003", "");
            if (ViewBag.isPageRight == false)
                return View();
            //string ID = Request.Params["ID"];//当前页面传递编号
            //string navStr = "";
            //if (string.IsNullOrEmpty(ID) == true)
            //    ID = "0";
            //else
            //{
            //    int len = ID.Length / 3;
            //    if (len >= 1)
            //    {
            //        for (int i = 0; i < len; i++)
            //        {
            //            if (i != len - 1)
            //                navStr += "<li class=\"active\"><a href=\"/System/RightList?ID=" + ID.Substring(0, (i + 1) * 3) + "\">" + T_SYSSEC_RIGHTCls.getNameByID(new T_SYSSEC_RIGHT_SW { RIGHTID = ID.Substring(0, (i + 1) * 3), SYSFLAG = ConfigCls.getSystemFlag() }) + "</a></li>";
            //            else
            //                navStr += "<li class=\"active\">" + T_SYSSEC_RIGHTCls.getNameByID(new T_SYSSEC_RIGHT_SW { RIGHTID = ID, SYSFLAG = ConfigCls.getSystemFlag() }) + "</li>";
            //        }
            //    }
            //}
            //ViewBag.T_ID = ID;
            //ViewBag.T_UrlReferrer = "/System/RightList?ID=" + ID;
            //ViewBag.navList = navStr;
            //ViewBag.RightList = getRightStr(new T_SYSSEC_RIGHT_SW { SubRightID = ID, SYSFLAG = ConfigCls.getSystemFlag() });
            return View();
        }

        #region 取出树形菜单
        /// <summary>
        /// 取出树形菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult RightTreeGet()
        {
            T_SYSSEC_RIGHT_SW sw = new T_SYSSEC_RIGHT_SW();
            string result = T_SYSSEC_RIGHTCls.getTypeTree(sw);
            return Content(result, "application/json");
        }

        #endregion

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult getRightListJson()
        {
            string trans = Request.Params["trans"];//传递网页参数
            return Content(JsonConvert.SerializeObject(new Message(true, getRightStr(new T_SYSSEC_RIGHT_SW { SubRightID = Request.Params["rightID"] }), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        private string getRightStr(T_SYSSEC_RIGHT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr><th>序号</th><th>权限编号</th><th>权限名称</th><th>系统标识符</th><th>排序号</th></tr></thead>");
            sb.AppendFormat("<tbody>");
            var result = T_SYSSEC_RIGHTCls.getListModel(sw);
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr onclick=\"showValue('{0}')\">", v.RIGHTID);
                else
                    sb.AppendFormat("<tr class='row1' onclick=\"showValue('{0}')\">", v.RIGHTID);
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.RIGHTID);
                if (v.SYSFLAG != ConfigCls.getSystemFlag())
                    sb.AppendFormat("<td class=\"center\"><font color=red>{0}</font></td>", v.RIGHTNAME);
                else
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.RIGHTNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.SYSFLAG);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORDERBY);
                //sb.AppendFormat("<td class=\" \">");
                //sb.AppendFormat("<a href=\"/System/RightList?ID={0}\">子权限管理</a>", v.RIGHTID);
                //sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        #endregion

        #region 角色管理
        /// <summary>
        /// 角色管理－增、删、改
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult RoleManager()
        {
            T_SYSSEC_ROLE_Model m = new T_SYSSEC_ROLE_Model();
            m.ROLEID = Request.Params["ID"];
            m.ROLENAME = Request.Params["ROLENAME"];
            m.ROLENOTE = Request.Params["ROLENOTE"];
            m.ROLELEVEL = Request.Params["ROLELEVEL"];
            m.ORDERBY = Request.Params["ORDERBY"];
            m.SYSFLAG = ConfigCls.getSystemFlag();
            m.rightIDList = Request.Params["RightIDList"];//权限列表
            m.opMethod = Request.Params["Method"];
            m.returnUrl = Request.Params["returnUrl"];
            if (string.IsNullOrEmpty(m.returnUrl))
                m.returnUrl = "/System/RoleList";
            //默认为添加
            if (string.IsNullOrEmpty(m.opMethod))
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.ROLENAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入角色名称!", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(T_SYSSEC_ROLECls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取Json字符串
        /// </summary>
        /// <returns></returns>
        public ActionResult getRoleJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID)) { ID = "0"; }
            return Content(JsonConvert.SerializeObject(T_SYSSEC_ROLECls.getModel(new T_SYSSEC_ROLE_SW { ROLEID = ID, SYSFLAG = ConfigCls.getSystemFlag() })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 角色管理
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleMan()
        {
            pubViewBag("006002", "006002", "");
            if (ViewBag.isPageRight == false)
                return View();
            //用户序号
            ViewBag.T_ID = Request.Params["ID"];
            //操作方法　Add Mdy Del
            ViewBag.T_Method = Request.Params["Method"];
            //如果未传参数，默认为添加
            if (string.IsNullOrEmpty(ViewBag.T_Method))
                ViewBag.T_Method = "Add";
            ViewBag.T_UrlReferrer = "/System/RoleList";
            ViewBag.RightChk = getRoleRightStr(new T_SYSSEC_ROLE_RIGHT_SW { ROLEID = ViewBag.T_ID });
            return View();
        }

        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        private string getRoleRightStr(T_SYSSEC_ROLE_RIGHT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            var result = T_SYSSEC_RIGHTCls.getRightByRoleModel(sw);
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr><th style='width:120px;'>一级</th><th style='width:150px;'>二级</th><th>三级</th></tr></thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in result)
            {
                int i = 0;
                foreach (var v2 in v.subModel)
                {
                    sb.AppendFormat("<tr>");
                    if (i == 0)//需要加rowspan
                    {
                        sb.AppendFormat("\r\n <td class=\"left\" rowspan=\"{0}\">", v.subModel.Count().ToString());
                        sb.AppendFormat("\r\n <label><input name=\"tbxRightID\" id=\"tbxRightID{0}\" type=\"checkbox\" class=\"ace\" value=\"{0}\" {2}=\"true\" onclick=\"selectall(this.value,this.checked)\"/><span class=\"lbl\">{1}</span></label>", v.RIGHTID, v.RIGHTNAME, (v.isCheck == "1") ? " checked" : "");
                        sb.AppendFormat("\r\n </td>");
                    }
                    sb.AppendFormat("\r\n <td class=\"left\" >");
                    sb.AppendFormat("\r\n <label><input name=\"tbxRightID\" id=\"tbxRightID{0}\" type=\"checkbox\" class=\"ace\" value=\"{0}\" {2}=\"true\" onclick=\"selectall(this.value,this.checked)\"/><span class=\"lbl\">{1}</span></label>", v2.RIGHTID, v2.RIGHTNAME, (v2.isCheck == "1") ? " checked" : "");
                    sb.AppendFormat("\r\n </td>");
                    sb.AppendFormat("\r\n <td class=\"left\" >");
                    foreach (var v3 in v2.subModel)
                    {
                        sb.AppendFormat("\r\n <label><input name=\"tbxRightID\" id=\"tbxRightID{0}\" type=\"checkbox\" class=\"ace\" value=\"{0}\" {2}=\"true\" onclick=\"selectall(this.value,this.checked)\"/><span class=\"lbl\">{1}</span></label>", v3.RIGHTID, v3.RIGHTNAME, (v3.isCheck == "1") ? " checked" : "");
                    }
                    sb.AppendFormat("\r\n </td>");
                    sb.AppendFormat("</tr>");
                    i++;
                }
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// 角色管理列表
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleList()
        {
            pubViewBag("006002", "006002", "");
            if (ViewBag.isPageRight == false) { return View(); }
            return View();
        }

        /// <summary>
        /// 获取列表查询页面
        /// </summary>
        /// <returns></returns>
        public ActionResult getRoleListAjax()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr><th>序号</th><th>角色名称</th><th>备注</th><th>级别</th><th>排序号</th><th>操作</th></tr></thead>");
            sb.AppendFormat("<tbody>");
            var result = T_SYSSEC_ROLECls.getListModel();
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                //sb.AppendFormat("<td class=\"center\"><a href=\"/System/RoleMan?Method=See&ID={0}\">{1}</a></td>", v.ROLEID, v.ROLENAME);
                sb.AppendFormat("<td class=\"center\"><a  href='#' onclick=\"See('See','{0}')\">{1}</a></td>", v.ROLEID, v.ROLENAME);
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.ROLENOTE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ROLELEVEL);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORDERBY);
                sb.AppendFormat("<td class=\" \">");
                //sb.AppendFormat("<a href=\"/System/RoleMan?Method=Mdy&ID={0}\" class=\"searchBox_01 LinkMdy\">修改</a>", v.ROLEID);
                sb.AppendFormat("<a href='#' onclick=\"Mdy('Mdy','{0}')\"  class=\"searchBox_01 LinkMdy\">修改</a>", v.ROLEID);
                sb.AppendFormat("&nbsp;<a href='#' onclick='Manager({0})' class=\"searchBox_01 LinkDel\">删除</a>", v.ROLEID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), "")), "text/html;charset=UTF-8"); ;
        }
        #endregion

        #region 用户管理
        /// <summary>
        /// 用户管理－增、删、改
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult UserManager()
        {
            string USERID = Request.Params["USERID"];
            string LOGINUSERNAME = Request.Params["LOGINUSERNAME"];
            string USERNAME = Request.Params["USERNAME"];
            string USERPWD = Request.Params["USERPWD"];
            string USERPWD1 = Request.Params["USERPWD1"];
            string SEX = Request.Params["SEX"];
            string ORGNO = Request.Params["ORGNO"];
            string PHONE = Request.Params["PHONE"];
            string USERJOB = Request.Params["USERJOB"];
            string NOTE = Request.Params["NOTE"];
            string Method = Request.Params["Method"];
            string returnUrl = Request.Params["returnUrl"];
            string ROLEID = Request.Params["ROLEID"];//角色列表
            string DEPARTMENT = Request.Params["DEPARTMENT"];//科室           

            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/System/UserList";
            //默认为添加
            if (string.IsNullOrEmpty(Method))
                Method = "Add";
            if (Method != "Del")
            {
                if (string.IsNullOrEmpty(LOGINUSERNAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入用户名!", "")), "text/html;charset=UTF-8");
                if (Method == "Add")
                {
                    if (string.IsNullOrEmpty(USERPWD))
                        return Content(JsonConvert.SerializeObject(new Message(false, "请输入密码!", "")), "text/html;charset=UTF-8");
                    if (USERPWD != USERPWD1)
                        return Content(JsonConvert.SerializeObject(new Message(false, "两次密码输入不一致!", "")), "text/html;charset=UTF-8");
                }
                if (Method == "Mdy")
                {
                    if (string.IsNullOrEmpty(USERPWD) == false && string.IsNullOrEmpty(USERPWD1))
                        return Content(JsonConvert.SerializeObject(new Message(false, "请输入确认密码!", "")), "text/html;charset=UTF-8");
                    if (string.IsNullOrEmpty(USERPWD) && string.IsNullOrEmpty(USERPWD1) == false)
                        return Content(JsonConvert.SerializeObject(new Message(false, "请先输入密码之后再确认密码!", "")), "text/html;charset=UTF-8");
                    if (USERPWD != USERPWD1)
                        return Content(JsonConvert.SerializeObject(new Message(false, "两次密码输入不一致!", "")), "text/html;charset=UTF-8");
                }
            }
            T_SYSSEC_IPSUSER_Model m = new T_SYSSEC_IPSUSER_Model();
            m.USERID = USERID;
            m.LOGINUSERNAME = LOGINUSERNAME;
            m.USERNAME = USERNAME;
            m.USERPWD = USERPWD;
            m.SEX = SEX;
            m.ORGNO = ORGNO;
            m.PHONE = PHONE;
            m.USERJOB = USERJOB;
            m.NOTE = NOTE;
            m.opMethod = Method;
            m.returnUrl = returnUrl;
            m.ROLEIDList = ROLEID;
            m.DEPARTMENT = DEPARTMENT;
            return Content(JsonConvert.SerializeObject(T_SYSSEC_IPSUSERCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取用户Json字符串
        /// </summary>
        /// <returns></returns>
        public ActionResult getUserJson()
        {
            string UID = Request.Params["USERID"];
            if (string.IsNullOrEmpty(UID))
                UID = "0";
            return Content(JsonConvert.SerializeObject(T_SYSSEC_IPSUSERCls.getModel(new T_SYSSEC_IPSUSER_SW { USERID = UID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult UserMan()
        {
            pubViewBag("006001", "006001", "");
            if (ViewBag.isPageRight == false)
                return View();
            //用户序号
            ViewBag.T_USERID = Request.Params["USERID"];
            //操作方法　Add Mdy Del
            ViewBag.T_Method = Request.Params["Method"];
            ViewBag.T_UrlReferrer = "/System/UserList";
            //如果未传参数，默认为添加
            if (string.IsNullOrEmpty(ViewBag.T_Method))
                ViewBag.T_Method = "Add";
            //角色复选框
            ViewBag.RoleChk = getRoleUId(new T_SYSSEC_ROLE_SW { USERID = ViewBag.T_USERID });
            ViewBag.vdSex = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTFLAG = "性别" });
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            string sysorgFlag = "1";//1 州  2 市县 3 乡镇
            var bxz = PublicCls.OrgIsZhen(SystemCls.getCurUserOrgNo());//乡镇
            var bsx = PublicCls.OrgIsXian(SystemCls.getCurUserOrgNo());//市县
            if (bxz)
            {
                sysorgFlag = "3";
            }
            else if (bsx)
            {
                sysorgFlag = "2";
            }
            ViewBag.depart = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "46", STANDBY1 = sysorgFlag });
            return View();
        }

        /// <summary>
        /// 由单位获取相应科室
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetOrgByDepartByFlag()
        {
            string orgNo = Request.Params["orgNo"];
            string showAll = Request.Params["showAll"];
            string sysorgFlag = "1";
            var bxz = PublicCls.OrgIsZhen(orgNo);//乡镇
            var bsx = PublicCls.OrgIsXian(orgNo);//市县
            if (bxz)
            {
                sysorgFlag = "3";
            }
            else if (bsx)
            {
                sysorgFlag = "2";
            }
            ViewBag.depart = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "46", STANDBY1 = sysorgFlag, isShowAll = showAll });
            return ViewBag.depart;
        }

        /// <summary>
        /// 获取所有角色及该用户拥有的角色
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        private string getRoleUId(T_SYSSEC_ROLE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            var result = T_SYSSEC_ROLECls.getRoleAndUidModel(sw);
            foreach (var v in result)
            {
                string chk = (v.isCheck == "1") ? " checked" : "";
                sb.AppendFormat("<label><input name=\"tbxROLEID\" type=\"checkbox\" class=\"ace\" value=\"{0}\" {2}/><span class=\"lbl\">{1}</span></label>", v.ROLEID, v.ROLENAME, chk);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 用户管理列表
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult UserList()
        {
            pubViewBag("006001", "006001", "");
            if (ViewBag.isPageRight == false) { return View(); }
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.depart = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "46", isShowAll = "1" });
            return View();
        }

        /// <summary>
        /// 获取列表查询页面
        /// </summary>
        /// <returns></returns>
        public ActionResult getUserListAjax()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string LoginUserName = Request.Params["LoginUserName"];
            string UserName = Request.Params["UserName"];
            string DEPARTMENT = Request.Params["DEPARTMENT"];
            string ORGNO = Request.Params["ORGNO"];
            string Page = Request.Params["Page"];
            int total = 0;
            T_SYSSEC_IPSUSER_SW sw = new T_SYSSEC_IPSUSER_SW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), LOGINUSERNAME = LoginUserName, USERNAME = UserName, DEPARTMENT = DEPARTMENT, ORGNO = ORGNO };
            var result = T_SYSSEC_IPSUSERCls.getUserModel(sw, out total);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th><th>单位</th><th>职务</th><th>姓名</th><th>登录名</th><th>科室</th><th>性别</th><th>电话</th><th>角色</th><th>操作</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", ((sw.curPage - 1) * sw.pageSize + i + 1).ToString());
                sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.ORGNAME, PublicCls.getOrgTDNameClass(sw.ORGNO, v.ORGNO));
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.USERJOB);
                //sb.AppendFormat("<td class=\"center\"><a href=\"/System/UserMan?Method=See&USERID={1}\">{0}</a></td>", v.USERNAME, v.USERID);
                sb.AppendFormat("<td class=\"center\"><a href='#' onclick=\"See('{1}','See')\" >{0}</a></td>", v.USERNAME, v.USERID);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOGINUSERNAME);
                if (string.IsNullOrEmpty(v.GID))
                    sb.AppendFormat("<td colspan=\"2\" class=\"center\">{0}</td>", "<font color=red>非本系统用户</font>");
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.DEPARTMENTName);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.SEXNAME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.PHONE);
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.RoleNameList);
                sb.AppendFormat("<td class=\"center\">");
                // sb.AppendFormat("<a href=\"/System/UserMan?Method=Mdy&USERID={0}\" class=\"searchBox_01 LinkMdy\">修改</a>", v.USERID);
                sb.AppendFormat("<a href='#' onclick=\"Mdy('{0}','Mdy')\"  class=\"searchBox_01 LinkMdy\">修改</a>", v.USERID);
                sb.AppendFormat("&nbsp;<a href='#' onclick='Manager(\"{0}\")' class=\"searchBox_01 LinkDel\">删除</a>", v.USERID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }

        #endregion

        #region OA部门关联
        /// <summary>
        /// OA部门信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OADeptInfo()
        {
            pubViewBag("006014", "006014", "");
            if (ViewBag.isPageRight == false) { return View(); }
            ViewBag.SysOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.DeptInfo = getOADeptStr("");
            ViewBag.IsOpenOA = ConfigCls.getIsTongBuOA();
            return View();
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        private static string getOADeptStr(string orgNo)
        {
            string sysorgNo = "";
            if (string.IsNullOrEmpty(orgNo))
                sysorgNo = SystemCls.getCurUserOrgNo();
            else
                sysorgNo = orgNo;

            string sysorgFlag = "1";
            var bxc = PublicCls.OrgIsCun(sysorgNo); //村
            var bxz = PublicCls.OrgIsZhen(sysorgNo);//乡镇
            var bsx = PublicCls.OrgIsXian(sysorgNo);//市县
            if (bxc)
                sysorgFlag = "4";
            if (bxz)
                sysorgFlag = "3";
            else if (bsx)
                sysorgFlag = "2";
            List<T_SYS_DICTModel> _list = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "46", STANDBY1 = sysorgFlag }).ToList();
            StringBuilder sb = new StringBuilder();
            string Options = "<option></option>";
            if (ConfigCls.getIsTongBuOA() == "1")
            {
                if (HttpCommon.CheckUrlVisit(ConfigCls.getOAWebServiseAddress()))
                    Options = Options + OACls.GetDeptOption();
            }
            for (int i = 0; i < _list.Count(); i++)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\">{0}<input id=\"sysdept" + i + "\" type=\"hidden\" value=\"{1}\"  /></td>", _list[i].DICTNAME, _list[i].DICTVALUE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<select id=\"tbxOADept" + i + "\" name=\"tbxOADept" + i + "\" onclick=\"OADeptChange()\">" + Options + "</select>");
                sb.AppendFormat("</tr>");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 行政单位发生变化
        /// </summary>
        /// <returns></returns>
        public ActionResult ORGNOChange()
        {
            StringBuilder sb = new StringBuilder();
            string orgNo = Request.Params["orgNo"];
            ViewBag.SysOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo(), CurORGNO = orgNo });
            sb.AppendFormat(getOADeptStr(orgNo));
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查找已关联的OA部门ID
        /// </summary>
        /// <returns></returns>
        public ActionResult FindOADept()
        {
            if (ConfigCls.getIsTongBuOA() == "1")
            {
                string sysORGNO = Request.Params["sysORGNO"];
                string sysDeptIdList = Request.Params["sysDeptIdList"]; //英文逗号分割的部门ID集合
                return Content(JsonConvert.SerializeObject(new Message(true, OACls.FindOADeptBySysDept(sysORGNO, sysDeptIdList), "")), "text/html;charset=UTF-8");
            }
            else
                return Content(JsonConvert.SerializeObject(new Message(true, "", "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 保存部门对应关系
        /// </summary>
        /// <returns></returns>
        public ActionResult OADeptManager()
        {
            if (ConfigCls.getIsTongBuOA() == "1")
            {
                string SysORGNO = Request.Params["SysORGNO"];
                string SysDeptID = Request.Params["SysDeptID"];
                string OADeptID = Request.Params["OADeptID"];
                T_SYS_Dept_OADept_Model m = new T_SYS_Dept_OADept_Model();
                m.SysORGNO = SysORGNO;
                m.SysDeptID = SysDeptID;
                m.OADeptID = OADeptID;
                return Content(JsonConvert.SerializeObject(OACls.DeptMap(m)));
            }
            else
                return Content(JsonConvert.SerializeObject(new Message(false, "暂无操作权限,若要使用,请联系系统管理员!", "")), "text/html;charset=UTF-8");
        }
        #endregion

        #region OA账号管理
        /// <summary>
        /// OA账号管理
        /// </summary>
        /// <returns></returns>
        public ActionResult OAUserList()
        {
            pubViewBag("006015", "006015", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.OAPWD = ConfigCls.getOAPwd();
            ViewBag.Open = (SystemCls.isRight("006015001")) ? "1" : "0"; //是否拥有开通OA账户权限
            ViewBag.Close = (SystemCls.isRight("006015002")) ? "1" : "0";
            return View();
        }

        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOAUserList()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string UserName = Request.Params["UserName"];
            string ORGNO = Request.Params["ORGNO"];
            string IsOpenOA = Request.Params["IsOpenOA"];
            string Page = Request.Params["Page"];
            int total = 0;
            T_SYSSEC_IPSUSER_SW sw = new T_SYSSEC_IPSUSER_SW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), USERNAME = UserName, ORGNO = ORGNO, IsOpenOA = IsOpenOA };
            List<T_SYSSEC_IPSUSER_Pager_Model> result = T_SYSSEC_IPSUSERCls.getOAUserModel(sw, out total).ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"OAUserTable\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            string dis = result.Count <= 0 ? "disabled=\"disabled\"" : "";
            sb.AppendFormat("<th style=\"width:5%\"><input id=\"tbxUserIDALL\" name=\"tbxUserIDALL\" type=\"checkbox\" class=\"ace\" value=\"ALL\" onclick=\"selectall(this.value,this.checked)\" {0} /></th>", dis);
            sb.AppendFormat("<th>序号</th><th style=\"width:15%\">单位</th><th style=\"width:15%\">科室</th><th style=\"width:15%\">姓名</th><th style=\"width:15%\">登录名</th><th>是否开通OA</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0, j = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("<tr class=\"{0}\">", i % 2 == 0 ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxUserID" + j + "\" name=\"tbxUserID\"  type=\"checkbox\" class=\"ace\" value=\"" + v.USERID + "\" onclick=\"selectall(this.value,this.checked)\" />");
                sb.AppendFormat("<td class=\"center\">{0}</td>", ((sw.curPage - 1) * sw.pageSize + i + 1).ToString());
                sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.ORGNAME, PublicCls.getOrgTDNameClass(sw.ORGNO, v.ORGNO));
                if (string.IsNullOrEmpty(v.GID))
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<font color=red>非本系统用户</font>");
                else
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.DEPARTMENTName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.USERNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOGINUSERNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.IsOpenOA == "1" ? "是" : "否");
                if (v.IsOpenOA == "1")
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"button\" value=\"初始化密码\" class=\"searchBox_01 LinkMdy\" onclick=\"InitPwd('" + v.USERID + "','" + ConfigCls.getOAPwd() + "')\" />");
                else
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"button\" value=\"初始化密码\" class=\"searchBox_01 LinkMdy\" disabled=\"disabled\" style=\"background-color:gray;\" />");
                sb.AppendFormat("</tr>");
                i++;
                j++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// OA账号开通
        /// </summary>
        /// <returns></returns>
        public ActionResult OAUserOpen()
        {
            if (ConfigCls.getIsTongBuOA() == "1")
            {
                if (HttpCommon.CheckUrlVisit(ConfigCls.getOAWebServiseAddress()))
                {
                    string ORGNO = Request.Params["ORGNO"];
                    string UserIDList = Request.Params["UserIDList"];
                    List<USERModel> mlist = new List<USERModel>();
                    if (UserIDList.Length > 0)
                    {
                        string[] useridArray = UserIDList.Split(',');
                        foreach (var userid in useridArray)
                        {
                            T_SYSSEC_IPSUSER_Model m = T_SYSSEC_IPSUSERCls.getModel(new T_SYSSEC_IPSUSER_SW { USERID = userid });
                            if (!string.IsNullOrEmpty(m.ORGNO) && !string.IsNullOrEmpty(m.DEPARTMENT))
                            {
                                string[] deptInfo = OACls.GetDeptInfo(m.ORGNO, m.DEPARTMENT);
                                //if (!Array.Exists(deptInfo, string.IsNullOrEmpty))
                                if (!string.IsNullOrEmpty(deptInfo[0]))
                                {
                                    USERModel om = new USERModel();
                                    om.USERID = m.USERID;
                                    om.DEPTID = deptInfo[0];
                                    om.USERNAME = m.USERNAME;
                                    om.PASSWORD = ConfigCls.getOAPwd();
                                    om.LOGONID = m.LOGINUSERNAME;
                                    om.SEX = m.SEX == "0" ? "男" : "女";
                                    om.SUPERID = deptInfo[1];
                                    om.MOBILE = m.PHONE;
                                    om.STATUS = "A";
                                    om.STATUSTIME = DateTime.Now;
                                    om.TITLE = m.USERJOB;
                                    om.ORGANISEID = deptInfo[2];
                                    om.IMAGEURL = "1";
                                    om.OWNERID = "1";
                                    om.ENTRYTIME = DateTime.Now;
                                    om.NETACL = "2";
                                    mlist.Add(om);
                                }
                            }
                        }
                    }
                    return Content(JsonConvert.SerializeObject(OACls.OpenUsers(mlist)), "text/html;charset=UTF-8");
                }
                else
                    return Content(JsonConvert.SerializeObject(new Message(false, "OA服务不通,请联系系统管理员!", "")), "text/html;charset=UTF-8");
            }
            else
                return Content(JsonConvert.SerializeObject(new Message(false, "暂无操作权限,若要使用,请联系系统管理员!", "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// OA账号禁用
        /// </summary>
        /// <returns></returns>
        public ActionResult OAUserClose()
        {
            if (ConfigCls.getIsTongBuOA() == "1")
            {
                if (HttpCommon.CheckUrlVisit(ConfigCls.getOAWebServiseAddress()))
                {
                    string UserIDList = Request.Params["UserIDList"];
                    return Content(JsonConvert.SerializeObject(OACls.CloseUsers(UserIDList)), "text/html;charset=UTF-8");
                }
                else
                    return Content(JsonConvert.SerializeObject(new Message(false, "OA服务不通,请联系系统管理员!", "")), "text/html;charset=UTF-8");
            }
            else
                return Content(JsonConvert.SerializeObject(new Message(false, "暂无操作权限,若要使用,请联系系统管理员!", "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 初始化OA账户密码
        /// </summary>
        /// <returns></returns>
        public ActionResult OAUserInitPwd()
        {
            if (ConfigCls.getIsTongBuOA() == "1")
            {
                if (HttpCommon.CheckUrlVisit(ConfigCls.getOAWebServiseAddress()))
                {
                    string UserId = Request.Params["UserId"];
                    string Pwd = Request.Params["Pwd"];
                    return Content(JsonConvert.SerializeObject(OACls.InitPwd(UserId, Pwd)), "text/html;charset=UTF-8");
                }
                else
                    return Content(JsonConvert.SerializeObject(new Message(false, "OA服务不通,请联系系统管理员!", "")), "text/html;charset=UTF-8");
            }
            else
            {
                return Content(JsonConvert.SerializeObject(new Message(false, "暂无操作权限,若要使用,请联系系统管理员!", "")), "text/html;charset=UTF-8");
            }
        }
        #endregion

        #region 用户登录
        /// <summary>
        /// 登陆页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            ViewBag.Title = ConfigCls.getSystemName();
            if (string.IsNullOrEmpty(SystemCls.getUserID()) == false)
                ViewBag.logined = "<script language=\"javascript\">window.location.href = '" + ConfigCls.getLoginRedirectUrl() + "';</script>";
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult UserLogin()
        {
            string userName = Request.Params["userName"];
            string pwd = Request.Params["pwd"];
            string State = Request.Params["State"];
            if (string.IsNullOrEmpty(userName))
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入用户名!", "")), "text/html;charset=UTF-8");
            if (userName == "请输入你的用户名!")
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入用户名!", "")), "text/html;charset=UTF-8");
            if (string.IsNullOrEmpty(pwd))
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入密码!", "")), "text/html;charset=UTF-8");
            T_SYSSEC_IPSUSER_Model tsuM = T_SYSSEC_IPSUSERCls.getModel(new T_SYSSEC_IPSUSER_SW { LOGINUSERNAME = userName });
            if (tsuM == null)
                return Content(JsonConvert.SerializeObject(new Message(false, "用户名不存在!", "")), "text/html;charset=UTF-8");
            if (string.IsNullOrEmpty(tsuM.USERID) == true)
                return Content(JsonConvert.SerializeObject(new Message(false, "用户名不存在!", "")), "text/html;charset=UTF-8");
            if (tsuM.USERPWD != ClsStr.getSystemManMd5(pwd))
                return Content(JsonConvert.SerializeObject(new Message(false, "密码错误!", "")), "text/html;charset=UTF-8");
            CookieModel cookieM = new CookieModel();
            cookieM.UID = tsuM.USERID;
            cookieM.userName = tsuM.LOGINUSERNAME;
            cookieM.trueName = tsuM.USERNAME;
            cookieM.SaveType = State;
            SystemCls.SaveLoginState(cookieM);
            #region
            //cookieM.ID= 
            //cookieM.ID = omm.ID;// o.ID.ToString();
            //cookieM.UserName = omm.UserName;// o.UserName.ToString();
            //cookieM.PassWord = omm.PassWord;// o.PassWord.ToString();
            //cookieM.SaveType = State;
            //HomeCls.SaveLoginState(cookieM);
            //omm.LastLoginIP = System.Web.HttpContext.Current.Request.ServerVariables["Remote_Host"].ToString();
            ////System.Web.HttpContext.Current.Session["YeleiIDCusername"] = HomeCls.setCookieStr(cookieM);
            ////_cookie.Values.Add("ManagerSystemCookie", HomeCls.setCookieStr(cookieM));
            ////Response.Cookies.Add(_cookie);
            ////O_MASTER_Model oMM = new O_MASTER_Model();
            ////oMM.ID = omm.ID;// o.ID;
            ////oMM.LastLoginIP = System.Web.HttpContext.Current.Request.ServerVariables["Remote_Host"].ToString();
            //m.O_MASTER_SaveLoginInfo(omm);
            //return new ModelAndView(new RedirectView("house.action?method=findHouseList")); 
            // System.Web.HttpContext.Current.Response.Redirect("/Home/Index");
            //return RedirectToAction("Index", "Home");
            //ScriptManager.RegisterStartupScript(this.p, typeof(object), "", "<script>window.location.href='/Home/Index';</script>", false);            
            //if (returnUrl != null && returnUrl.Trim() != "")
            //    return Redirect(returnUrl);
            //return RedirectToAction("Index", "Home");
            #endregion
            return Content(JsonConvert.SerializeObject(new Message(true, "验证成功!", ConfigCls.getLoginRedirectUrl())), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            SystemCls.ClearLoginState();
            return View();
        }
        #endregion

        #region 通讯录类别管理
        /// <summary>
        /// 类别管理－增、删、改
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult ADTYPEManager()
        {
            T_SYS_ADDREDDTYPE_Model m = new T_SYS_ADDREDDTYPE_Model();
            m.ATID = Request.Params["ATID"];
            m.RATID = Request.Params["RATID"];
            m.RTNAME = Request.Params["RTNAME"];
            m.ORDERBY = Request.Params["ORDERBY"];
            m.opMethod = Request.Params["Method"];
            m.returnUrl = Request.Params["returnUrl"];

            if (string.IsNullOrEmpty(m.returnUrl))
                m.returnUrl = "/SystemConfig/AddressTypeList";
            //默认为添加
            if (string.IsNullOrEmpty(m.opMethod))
                m.opMethod = "Add";
            if (m.opMethod == "Mdy" || m.opMethod == "Del")
            {
                if (string.IsNullOrEmpty(m.ATID))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请选择要操作的记录!", "")), "text/html;charset=UTF-8");
            }
            if (string.IsNullOrEmpty(m.ORDERBY))
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入排序号!", "")), "text/html;charset=UTF-8");
            if (string.IsNullOrEmpty(m.RTNAME))
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入名称!", "")), "text/html;charset=UTF-8");
            return Content(JsonConvert.SerializeObject(T_SYS_ADDREDDTYPECls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取类别Json字符串
        /// </summary>
        /// <returns></returns>
        public ActionResult getADTYPEJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            return Content(JsonConvert.SerializeObject(T_SYS_ADDREDDTYPECls.getModel(new T_SYS_ADDREDDTYPE_SW { ATID = ID })), "text/html;charset=UTF-8");
        }
        #endregion

        #region 通讯录管理
        /// <summary>
        /// 取出树形菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult AddressTreeget()
        {
            string rid = Request.Params["ID"];
            string nameForMat = "{ADNAME}[{USERJOB}] [电话：{PHONE}] 排序号：[{ORDERBY}]";
            if (string.IsNullOrEmpty(rid))
            {
                string result = T_SYS_ADDREDDBOOKCls.getTypeTree(new T_SYS_ADDREDDTYPE_SW { treeNameShowUserType = nameForMat, isTreeOpen = "0" });
                return Content(result, "application/json");
            }
            else
                return Content("", "application/json");
        }

        /// <summary>
        /// 通讯录管理－增、删、改
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult ADDREDDBOOKManger()
        {
            T_SYS_ADDREDDBOOK_Model m = new T_SYS_ADDREDDBOOK_Model();
            m.ADID = Request.Params["ADID"];
            m.ATID = Request.Params["ATID"];
            m.ADNAME = Request.Params["ADNAME"];
            m.USERJOB = Request.Params["USERJOB"];
            m.PHONE = Request.Params["PHONE"];
            m.Tell = Request.Params["Tell"];
            m.ORDERBY = Request.Params["ORDERBY"];
            m.opMethod = Request.Params["Method"];

            if (string.IsNullOrEmpty(m.ORDERBY))
                m.ORDERBY = "0";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.ADNAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入姓名!", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.PHONE))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入手机号码!", "")), "text/html;charset=UTF-8");
            }
            if (m.opMethod == "Add")
            {
                if (string.IsNullOrEmpty(m.ATID))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请点击要添加的类别!", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(T_SYS_ADDREDDBOOKCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取通讯录模型
        /// </summary>
        /// <returns></returns>
        public ActionResult GetADDREDDBOOKJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            return Content(JsonConvert.SerializeObject(T_SYS_ADDREDDBOOKCls.getModel(new T_SYS_ADDREDDBOOK_SW { ADID = ID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 通讯录列表列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ADDREDDBOOKList()
        {
            pubViewBag("006009", "006009", "");
            return View();
        }
        #endregion

        #region 预案管理

        #region 预案文件上传
        public JsonResult FirePlanUpload()
        {
            Message ms = null;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string Path = "";
            string[] arr = hfc[0].FileName.Split('.');
            if (string.IsNullOrEmpty(hfc[0].FileName))
                return Json(new Message(false, "请选择附件!", ""));
            if (arr[arr.Length - 1].ToLower() == "exe")
                return Json(new Message(false, "禁止上传exe文件!", ""));
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss.") + arr[arr.Length - 1];
            string ipath = "~/UploadFile/FirePlan/";//相对路径 
            string PhyPath = Server.MapPath(ipath);
            if (!Directory.Exists(PhyPath))//判断文件夹是否已经存在
            {
                Directory.CreateDirectory(PhyPath);//创建文件夹
            }
            Path = "/UploadFile/FirePlan/" + filename;// hfc[i].FileName;
            string PhysicalPath = Server.MapPath(Path);
            hfc[0].SaveAs(PhysicalPath);
            ms = new Message(true, filename, "");
            return Json(ms);
        }
        #endregion

        #region 预案管理增删改
        /// <summary>
        /// 预案管理增删改
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult FirePlanManager()
        {
            string JC_FIRE_PLANID = Request.Params["id"];
            string BYORGNO = Request.Params["BYORGNO"];
            string FIRELEVEL = Request.Params["FIRELEVEL"];
            string PLANTITLE = Request.Params["PLANTITLE"];
            string PLANCONTENT = Request.Params["PLANCONTENT"];
            string PLANFILENAME = Request.Params["PLANFILENAME"];
            string Method = Request.Params["Method"];
            string returnUrl = Request.Params["returnUrl"];
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/System/FirePlanList";
            //默认为添加
            if (string.IsNullOrEmpty(Method))
            {
                Method = "Add";
            }
            if (Method != "Del")
            {
                if (string.IsNullOrEmpty(PLANTITLE))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入标题!", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(BYORGNO))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请选择单位!", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(FIRELEVEL))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请选择火灾级别!", "")), "text/html;charset=UTF-8");
            }
            JC_FIRE_PLAN_Model m = new JC_FIRE_PLAN_Model();
            m.JC_FIRE_PLANID = JC_FIRE_PLANID;
            m.BYORGNO = BYORGNO;
            m.FIRELEVEL = FIRELEVEL;
            m.PLANTITLE = PLANTITLE;
            m.PLANCONTENT = PLANCONTENT;
            m.PLANFILENAME = PLANFILENAME;
            m.opMethod = Method;
            m.returnUrl = returnUrl;
            return Content(JsonConvert.SerializeObject(JC_FIRE_PLANCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 获取预案详细内容
        /// <summary>
        /// 获取预案详细内容
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult getFirePlanJson()
        {
            string id = Request.Params["id"];
            if (string.IsNullOrEmpty(id))
                id = "0";
            return Content(JsonConvert.SerializeObject(JC_FIRE_PLANCls.getModel(new JC_FIRE_PLAN_SW { JC_FIRE_PLANID = id })), "text/html;charset=UTF-8");
        }
        #endregion

        #region 预案管理页面
        /// <summary>
        /// 预案管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult FirePlanMan()
        {
            pubViewBag("006011", "006011", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.vdFireLevel = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "22" });
            ViewBag.T_Method = Request.Params["Method"];
            //如果未传参数，默认为添加
            if (string.IsNullOrEmpty(ViewBag.T_Method))
                ViewBag.T_Method = "Add";
            ViewBag.id = Request.Params["id"];
            return View();
        }
        #endregion

        #region 预案查看页面
        /// <summary>
        /// 预案查看页面
        /// </summary>
        /// <returns></returns>
        public ActionResult FirePlanShow()
        {
            pubViewBag("006011", "006011", "");

            if (ViewBag.isPageRight == false)
                return View();
            string ID = Request.Params["ID"];
            StringBuilder sb = new StringBuilder();
            var v = JC_FIRE_PLANCls.getModel(new JC_FIRE_PLAN_SW { JC_FIRE_PLANID = ID });
            sb.AppendFormat("<div class=' showArtTitle'>{0}</div>", v.PLANTITLE);
            sb.AppendFormat("<div class=' showArtSmallTitle'>");
            //sb.AppendFormat("类别:[{0}]&nbsp;", v.ARTTYPENAME);
            //sb.AppendFormat("录入：[{0}]&nbsp;", v.ARTADDUSERName);
            //sb.AppendFormat("添加时间：{0}", v.ARTTIME);
            sb.AppendFormat("单位：{0}", v.BYORGNOName);
            sb.AppendFormat("&nbsp;火灾级别：{0}", v.FIRELEVELName);
            sb.AppendFormat("</div>");
            string file = "";
            if (string.IsNullOrEmpty(v.PLANFILENAME) == false)
                file = "<br><a href='" + v.PLANFILENAME + "'>查看预案</a>";
            sb.AppendFormat("<div class=' showArtContent'>{0}</div>", v.PLANCONTENT);
            sb.AppendFormat("<div class=' showArtFile'>预案下载：<br><a href='/UploadFile/FirePlan/{0}'>{1}</a></div>", v.PLANFILENAME, v.PLANTITLE);

            ViewBag.Content = sb.ToString();
            return View();
        }
        #endregion



        #region 预案列表页面
        /// <summary>
        /// 预案列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult FirePlanList()
        {
            pubViewBag("006011", "006011", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.vdFireLevel = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "22", isShowAll = "1" });

            return View();
        }
        #endregion
        #region 预案Ajax列表
        /// <summary>
        /// 预案Ajax列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getFirePlanListAjax()
        {

            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            string BYORGNO = Request.Params["BYORGNO"];
            string FIRELEVEL = Request.Params["FIRELEVEL"];

            int total = 0;
            JC_FIRE_PLAN_SW sw = new JC_FIRE_PLAN_SW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), BYORGNO = BYORGNO, FIRELEVEL = FIRELEVEL };
            // T_SYSSEC_IPSUSER_SW sw1 = new T_SYSSEC_IPSUSER_SW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), LOGINUSERNAME = LoginUserName, USERNAME = UserName, DEPARTMENT = DEPARTMENT, ORGNO = ORGNO };

            var result = JC_FIRE_PLANCls.getModelList(sw, out total);


            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style='width:10%;'>序号</th>");
            sb.AppendFormat("<th style='width:10%;'>单位名称</th>");
            sb.AppendFormat("<th style='width:10%;'>火灾级别</th>");
            sb.AppendFormat("<th style='width:60%;'>预案标题</th>");
            sb.AppendFormat("<th style='width:10%;'>管理</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center  \">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center  \">{0}</td>", v.BYORGNOName);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", v.FIRELEVELName);
                sb.AppendFormat("<td class=\"left  \"><a href=\"/System/FirePlanShow?ID={1}\" target='_blank'>{0}</a></td>", v.PLANTITLE, v.JC_FIRE_PLANID);
                sb.AppendFormat("<td class=\"center  \">");
                // sb.AppendFormat("<a href='/System/FirePlanMan?Method=Mdy&id={0}' class=\"searchBox_01 LinkMdy\">修改</a>", v.JC_FIRE_PLANID);
                sb.AppendFormat("<a href='#' onclick=\"Mdy('{0}','Mdy')\" class=\"searchBox_01 LinkMdy\">修改</a>", v.JC_FIRE_PLANID);
                //sb.AppendFormat("&nbsp;<a href='/System/FirePlanManager?Method=Del&id={0}'>删除</a>", v.JC_FIRE_PLANID);
                sb.AppendFormat("&nbsp;<a href='#' onclick='Manager({0})' class=\"searchBox_01 LinkDel\">删除</a>", v.JC_FIRE_PLANID);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");

            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }


        #endregion


        #endregion

        #region 短信模板管理

        #region 获取单条记录
        public ActionResult getDCSMSJson()
        {
            string YJ_DCSMS_TMPID = Request.Params["YJ_DCSMS_TMPID"];
            if (string.IsNullOrEmpty(YJ_DCSMS_TMPID))
                YJ_DCSMS_TMPID = "0";
            return Content(JsonConvert.SerializeObject(YJ_DCSMS_TMPCls.getModel(new YJ_DCSMS_TMP_SW { YJ_DCSMS_TMPID = YJ_DCSMS_TMPID })), "text/html;charset=UTF-8");
        }
        #endregion

        #region 人员选择树形菜单
        public ActionResult TreeTXLUSERGet()
        {
            string result = T_SYS_ADDREDDBOOKCls.getTypeTree(new T_SYS_ADDREDDTYPE_SW { treeIDShowUserType = "{ADID}", treeNameShowUserType = "{ADNAME}[{USERJOB}]", treeIsShowTypeID = "0" });//  T_SYS_ADDREDDTYPECls.get DC_TYPECls.getEQUIPTree(new DC_TYPE_SW { });
            return Content(result, "application/json");
        }
        #endregion

        #region 预案管理页面 DCSMSTmpMan()
        public ActionResult DCSMSTmpMan()
        {
            pubViewBag("006010", "006010", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.YJ_DCSMS_TMPID = Request.Params["YJ_DCSMS_TMPID"];
            ViewBag.T_Method = Request.Params["Method"];
            //如果未传参数，默认为添加
            if (string.IsNullOrEmpty(ViewBag.T_Method))
                ViewBag.T_Method = "Add";
            ViewBag.vdDANGERCLASS = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "24", isShowAll = "0" });
            return View();
        }
        #endregion

        #region 预案管理增删改 DCSMSManager()
        /// <summary>
        /// 预案管理增删改
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult DCSMSManager()
        {
            YJ_DCSMS_TMP_Model m = new YJ_DCSMS_TMP_Model();
            m.YJ_DCSMS_TMPID = Request.Params["YJ_DCSMS_TMPID"];
            m.SMSGROUPNAME = Request.Params["SMSGROUPNAME"];
            m.SMSGROUPTYPE = Request.Params["SMSGROUPTYPE"];
            m.DANGERCLASS = Request.Params["DANGERCLASS"];
            m.TMPCONTENT = Request.Params["TMPCONTENT"];
            m.SMSSENDUSERLIST = Request.Params["SMSSENDUSERLIST"];
            m.ORDERBY = Request.Params["ORDERBY"];
            m.ISENABLE = Request.Params["ISENABLE"];
            m.opMethod = Request.Params["Method"];
            m.returnUrl = Request.Params["returnUrl"];
            if (m.opMethod == "MdyISENABLE")
            {
                if (string.IsNullOrEmpty(m.YJ_DCSMS_TMPID))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请选择要操作的记录!", "")), "text/html;charset=UTF-8");
            }
            if (m.opMethod != "Del" && m.opMethod != "MdyISENABLE")
            {
                if (string.IsNullOrEmpty(m.SMSGROUPNAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入标题名称!", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.TMPCONTENT))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入短信模板内容!", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(YJ_DCSMS_TMPCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 主页面 DCSMSTmpList()
        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DCSMSTmpList()
        {
            pubViewBag("006010", "006010", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdDANGERCLASS = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "24", isShowAll = "1" });
            return View();
        }

        #endregion

        #region 短信模板列表
        /// <summary>
        /// 短信模板列表
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult getDCSMSListJson()
        {
            var list = YJ_DCSMS_TMPCls.GetListModel(new YJ_DCSMS_TMP_SW { DANGERCLASS = Request.Params["FIRELEVEL"] });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>火险等级</th>");
            sb.AppendFormat("<th>模板标题</th>");
            sb.AppendFormat("<th>号码来源</th>");
            sb.AppendFormat("<th>是否启用</th>");
            sb.AppendFormat("<th>排序号</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                var vm = v.dicModel;
                if (i % 2 == 0)
                    sb.AppendFormat("<tr onclick=\"showValue('{0}')\">", v.YJ_DCSMS_TMPID);
                else
                    sb.AppendFormat("<tr class='row1' onclick=\"showValue('{0}')\">", v.YJ_DCSMS_TMPID);
                sb.AppendFormat("<td class=\"center\"><input name=\"chk\" id=\"chk\" type=\"checkbox\" value=\"{0}\" /></td>", v.YJ_DCSMS_TMPID);
                sb.AppendFormat("<td class=\"center\" style='background:{1};'>{0}</td>", v.FIRELEVELName, vm.STANDBY1);
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.SMSGROUPNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.SMSGROUPTYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", (v.ISENABLE == "1") ? v.ISENABLEName : "<font color='red'>" + v.ISENABLEName + "</font>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORDERBY);
                sb.AppendFormat("    <td class=\" \">");
                //sb.AppendFormat("            <a href=\"/System/DCSMSTmpMan?YJ_DCSMS_TMPID={0}&Method=Mdy\" class=\"searchBox_01 LinkMdy\">修改</a>", v.YJ_DCSMS_TMPID);
                sb.AppendFormat("            <a href='#' onclick=\"Mdy('{0}','Mdy')\" class=\"searchBox_01 LinkMdy\">修改</a>", v.YJ_DCSMS_TMPID);
                //sb.AppendFormat("            <a href=\"/SystemConfig/AREAList?ID={0}\">删除</a>", "");
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        #endregion
        #endregion

        #region 首页滚动
        public ActionResult marqueeIndexInfo()
        {
            pubViewBag("000000", "000000", "");
            return View();
        }
        #endregion

        #region 预警响应相关工作表
        #region 增删改
        /// <summary>
        /// 增删改
        /// </summary>
        /// <returns></returns>
        public ActionResult YJ_XY_WORKManger()
        {
            YJ_XY_WORK_Model m = new YJ_XY_WORK_Model();
            m.YJXYID = Request.Params["YJXYID"];
            m.DANGERCLASS = Request.Params["DANGERCLASS"];
            m.YJXYDEPT = Request.Params["YJXYDEPT"];
            m.YJXYCONTENT = Request.Params["YJXYCONTENT"];
            m.opMethod = Request.Params["Method"];
            m.returnUrl = Request.Params["returnUrl"];
            return Content(JsonConvert.SerializeObject(YJ_XY_WORKCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <returns></returns>
        public ActionResult getYJ_XY_WORKJson()
        {
            string DANGERCLASS = Request.Params["DANGERCLASS"];
            string YJXYDEPT = Request.Params["YJXYDEPT"];
            return Content(JsonConvert.SerializeObject(YJ_XY_WORKCls.getModel(new YJ_XY_WORK_SW { DANGERCLASS = DANGERCLASS, YJXYDEPT = YJXYDEPT })), "text/html;charset=UTF-8");
        }
        #endregion

        #region 预警响应工作
        /// <summary>
        /// 预警响应工作
        /// </summary>
        /// <returns></returns>
        public ActionResult yjxyWorkList()
        {
            pubViewBag("006012", "006012", "");
            return View();
        }
        #endregion

        #region 获取表格列表Json
        /// <summary>
        /// 获取表格列表Json
        /// </summary>
        /// <returns></returns>
        public ActionResult getYJ_XY_WORKTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" width=\"{0}px\" cellspacing=\"0\">", (280 * 6 + 120).ToString());
            var result = YJ_XY_WORKCls.getModelListMan(new YJ_XY_WORK_SW { });
            var resultClass = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW
            {
                DICTTYPEID = "24",
                DICTVALUE = T_SYS_PARAMETERCls.getModel(new T_SYS_PARAMETER_SW { PARAMFLAG = "YJ_XY_WORKClass" }).PARAMVALUE
            });//火情预警等级
            var resultDept = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "25" });//预警响应部门
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style='width:120px;'>火险等级</th>");
            foreach (var v in resultDept)
            {
                sb.AppendFormat("<th style='width:280px;'>{0}</th>", v.DICTNAME);
            }
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in resultClass)
            {
                sb.AppendFormat("<tr  style='background:{0};color:#000000'>", v.STANDBY1);
                sb.AppendFormat("<td   style='color:#000000'>{0}</td>", v.DICTNAME);
                foreach (var x in resultDept)
                {
                    var jbs = result.Where(p => p.DANGERCLASS == v.DICTVALUE && p.YJXYDEPT == x.DICTVALUE).FirstOrDefault();
                    sb.AppendFormat("<td onclick=\"show('{0}','{1}')\" valign='top'>", v.DICTVALUE, x.DICTVALUE);
                    sb.AppendFormat("{0}", jbs.YJXYCONTENT);
                    sb.AppendFormat("</td>");
                }
                sb.AppendFormat("</tr>");
            }
            //int i = 0;
            //foreach (var v in result)
            //{
            //    if (i % 2 == 0)
            //        sb.AppendFormat("<tr onclick=\"show('{0}','{1}','{2}')\" style='background:{3};color:#000000'>", v.YJXYID, v.DANGERCLASS, v.YJXYDEPT,v.dicModel.STANDBY1);
            //    else
            //        sb.AppendFormat("<tr class='row1'  onclick=\"show('{0}','{1}','{2}')\" style='background:{3};color:#000000'>", v.YJXYID, v.DANGERCLASS, v.YJXYDEPT, v.dicModel.STANDBY1);
            //    sb.AppendFormat("<td class=\"center  \">{0}</td>", (i + 1).ToString());
            //    sb.AppendFormat("<td class=\"  \">{0}</td>", v.DANGERCLASSName);
            //    sb.AppendFormat("<td class=\"  \">{0}</td>", v.YJXYDEPTName);
            //    sb.AppendFormat("<td class=\"left  \">{0}</td>", v.YJXYCONTENT);
            //    sb.AppendFormat("</tr>");
            //    i++;
            //}
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        #endregion

        #region 主页面 yjxyWorkShow()
        public ActionResult yjxyWorkShow()
        {

            pubViewBag("006012", "006012", "");
            ViewBag.isPageRight = true;
            return View();
        }
        #endregion
        #endregion

        #region 组织机构图管理

        #region 管理
        /// <summary>
        /// 管理
        /// </summary>
        /// <returns></returns>
        public ActionResult OrgMapList()
        {
            pubViewBag("006013", "006013", "");
            if (ViewBag.isPageRight == false) { return View(); }
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.dict45 = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "45", isShowAll = "1" });
            ViewBag.dict45Add = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "45" });
            return View();
        }
        #endregion

        #region 获取查看和修改前的数据
        /// <summary>
        /// 获取查看和修改前的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrgLinkjson()
        {
            string ORGLINK_ID = Request.Params["ORGLINK_ID"];
            //if (string.IsNullOrEmpty(ID))
            //    ID = "0";
            return Content(JsonConvert.SerializeObject(T_SYS_ORG_LINKCls.getModel(new T_SYS_ORG_LINK_SW { ORGLINK_ID = ORGLINK_ID })), "text/html;charset=UTF-8");
        }

        #endregion

        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult OrgLinkManager()
        {
            string type = Request.Params["Type"]; // 1:村委会 2：自然村
            bool IsZhen = false;
            T_SYS_ORG_LINK_Model m = new T_SYS_ORG_LINK_Model();
            m.BYORGNO = Request.Params["BYORGNO"];
            m.opMethod = Request.Params["Method"];
            m.ORGLINKTYPE = Request.Params["ORGLINKTYPE"];
            m.ORGLINK_ID = Request.Params["ORGLINK_ID"];
            m.UNITNAME = Request.Params["UNITNAME"];
            m.NAME = Request.Params["NAME"];
            m.USERJOB = Request.Params["USERJOB"];
            m.PHONE = Request.Params["PHONE"];
            m.Tell = Request.Params["Tell"];
            m.ORDERBY = Request.Params["ORDERBY"];
            if (PublicCls.OrgIsZhen(m.BYORGNO) == true)
            {
                if (string.IsNullOrEmpty(m.ORGLINKTYPE) == false)
                {
                    IsZhen = true;
                    m.BYORGNO = m.ORGLINKTYPE;
                }
                if (string.IsNullOrEmpty(m.UNITNAME)) { m.ORGLINKTYPE = "1"; }//村委会 
                else { m.ORGLINKTYPE = "2"; }//自然村 
            }
            if (string.IsNullOrEmpty(m.opMethod) == true) { m.opMethod = "Add"; }
            if (m.opMethod != "Del")
            {
                if (type == "2")
                {
                    if (IsZhen == true)
                    {
                        if (string.IsNullOrEmpty(m.UNITNAME))
                        {
                            return Content(JsonConvert.SerializeObject(new Message(false, "请输入自然村名称!", "")), "text/html;charset=UTF-8");
                        }
                    }
                }
                if (string.IsNullOrEmpty(m.NAME))
                {
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入姓名!", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.PHONE))
                {
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入手机号码!", "")), "text/html;charset=UTF-8");
                }
            }
            return Content(JsonConvert.SerializeObject(T_SYS_ORG_LINKCls.Manager(m)), "text/html;charset=UTF-8");
        }

        #endregion

        #region 查询通讯录列表
        /// <summary>
        /// 查询通讯录列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getOrgLinklist()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            string ORGLINKTYPE = Request.Params["ORGLINKTYPE"];
            string keys = Request.Params["keys"];
            StringBuilder sb = new StringBuilder();
            int i = 0;
            int j = 0;
            int orderby = 1;

            #region 显示市、县
            if (PublicCls.OrgIsShi(BYORGNO) == true || PublicCls.OrgIsXian(BYORGNO) == true)
            {
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<thead>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<th>序号</th>");
                if (ORGLINKTYPE == "3")//相关单位
                {
                    sb.AppendFormat("<th>单位名称</th>");
                    sb.AppendFormat("<th>职位</th>");
                    sb.AppendFormat("<th>联系人</th>");
                }
                else
                {
                    sb.AppendFormat("<th>职位</th>");
                    sb.AppendFormat("<th>姓名</th>");
                }
                sb.AppendFormat("<th>手机</th>");
                sb.AppendFormat("<th>电话</th>");
                sb.AppendFormat("<th>排序号</th>");
                sb.AppendFormat("<th></th>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("</thead>");
                sb.AppendFormat("<tbody>");
                var result = T_SYS_ORG_LINKCls.getModelList(new T_SYS_ORG_LINK_SW { ORGNO = BYORGNO, ORGLINKTYPE = ORGLINKTYPE, keys = keys });
                foreach (var v in result)
                {
                    sb.AppendFormat("<tr class='{0}' onclick=\"LinkOnclik('{1}','{2}','{3}','{4}','{5}','{6}','{7}')\">"
                        , (i % 2 == 0) ? "" : "row1", v.ORGLINK_ID, v.UNITNAME, v.NAME, v.USERJOB, v.PHONE, v.Tell, v.ORDERBY);
                    if (string.IsNullOrEmpty(v.ORDERBY) == false) { orderby = Convert.ToInt16(v.ORDERBY) + 1; }
                    sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                    if (ORGLINKTYPE == "3")//相关单位
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.UNITNAME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.USERJOB);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.PHONE);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.Tell);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORDERBY);
                    sb.AppendFormat("    <td class=\" \">");
                    sb.AppendFormat("    </td>");
                    sb.AppendFormat("</tr>");
                    i++;
                }
            }
            #endregion

            #region 显示镇，村委会
            else
            {
                #region 村委会成员
                if (ORGLINKTYPE != "")
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                    sb.AppendFormat("<thead>");
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<th colspan=\"100\" class=\"Center\">{0}", "村委会成员");
                    sb.AppendFormat("</th>");
                    sb.AppendFormat("</tr>");
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<th>序号</th>");
                    sb.AppendFormat("<th>职位</th>");
                    sb.AppendFormat("<th>联系人</th>");
                    sb.AppendFormat("<th>手机</th>");
                    sb.AppendFormat("<th>电话</th>");
                    sb.AppendFormat("<th>排序号</th>");
                    sb.AppendFormat("<th></th>");
                    sb.AppendFormat("</tr>");
                    sb.AppendFormat("</thead>");
                    sb.AppendFormat("<tbody>");
                    var tempresult = T_SYS_ORG_LINKCls.getModelList(new T_SYS_ORG_LINK_SW { ORGNO = BYORGNO, ORGLINKTYPE = ORGLINKTYPE, keys = keys });
                    if (ORGLINKTYPE != "")//下属村委会
                    {
                        tempresult = T_SYS_ORG_LINKCls.getModelList(new T_SYS_ORG_LINK_SW { ORGNO = ORGLINKTYPE, keys = keys });
                    }
                    tempresult = tempresult.Where(p => p.UNITNAME == "").ToList();
                    foreach (var v in tempresult)
                    {
                        sb.AppendFormat("<tr class='{0}' onclick=\"LinkOnclik2('{1}','{2}','{3}','{4}','{5}','{6}')\">", (i % 2 == 0) ? "" : "row1", v.ORGLINK_ID, v.NAME, v.USERJOB, v.PHONE, v.Tell, v.ORDERBY);
                        if (string.IsNullOrEmpty(v.ORDERBY) == false) { orderby = Convert.ToInt16(v.ORDERBY) + 1; }
                        sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.USERJOB);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.PHONE);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.Tell);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORDERBY);
                        sb.AppendFormat("<td class=\" \">   </td>");
                        sb.AppendFormat("</tr>");
                        i++;
                    }
                    sb.AppendFormat("<tr class=\"{0}\">", (j % 2 == 0) ? "" : "row1");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                    //bool ShowUnit = false;
                    //if ((PublicCls.OrgIsShi(BYORGNO) == true || PublicCls.OrgIsXian(BYORGNO) == true) && ORGLINKTYPE == "3") { ShowUnit = true; }
                    //if (PublicCls.OrgIsZhen(BYORGNO) && ORGLINKTYPE != "") { ShowUnit = true; }
                    //if (ShowUnit == true)
                    //    sb.AppendFormat("<td class=\"left\">{0}</td>", "<input type=\"text\" id=\"tbxLinkUNITNAME2\"/>");
                    //else
                    //    sb.AppendFormat("{0}", "<input type=\"hidden\" id=\"tbxLinkUNITNAME2\"/>");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"tbxLinkUSERJOB2\"/>");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"tbxLinkNAME2\" id=\"tbxLinkNAME2\" />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"tbxLinkPHONE2\"/>");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"tbxLinkTell2\"/>");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"tbxLinkORDERBY2\" value=\"" + orderby.ToString() + "\"/>");
                    sb.AppendFormat("    <td class=\" \">");
                    sb.AppendFormat("{0}", "<input type=\"hidden\" id=\"tbxLinkORGLINK_ID2\"/>");
                    sb.AppendFormat("{0}", "<input type=\"button\" class=\"btnAddCss\" value=\"添加\" onclick=\"ManagerLink2('Add','1')\"/>");
                    sb.AppendFormat("&nbsp;{0}", "<input type=\"button\" class=\"btnMdyCss\"  id=\"btnLinkMdy2\" style=\"display:none;\" value=\"修改\" onclick=\"ManagerLink2('Mdy','1')\" />");
                    sb.AppendFormat("&nbsp;{0}", "<input type=\"button\" class=\"btnDelCss\"  id=\"btnLinkDel2\" style=\"display:none;\"  value=\"删除\" onclick=\"ManagerLink2('Del','1')\" />");
                    sb.AppendFormat("    </td>");
                    sb.AppendFormat("</tr>");
                    j++;
                    sb.AppendFormat("</tbody>");
                    sb.AppendFormat("</table>");
                }
                #endregion

                #region 下属自然村
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<thead>");
                if (ORGLINKTYPE != "")
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<th colspan=\"100\" class=\"Center\">{0}</th>", "下属自然村成员");
                    sb.AppendFormat("</tr>");
                }
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<th>序号</th>");
                if (ORGLINKTYPE != "")//下属村委会
                {
                    sb.AppendFormat("<th>下属自然村</th>");
                    sb.AppendFormat("<th>职位</th>");
                    sb.AppendFormat("<th>联系人</th>");
                }
                else
                {
                    sb.AppendFormat("<th>职位</th>");
                    sb.AppendFormat("<th>姓名</th>");
                }
                sb.AppendFormat("<th>手机</th>");
                sb.AppendFormat("<th>电话</th>");
                sb.AppendFormat("<th>排序号</th>");
                sb.AppendFormat("<th></th>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("</thead>");
                sb.AppendFormat("<tbody>");
                var result = T_SYS_ORG_LINKCls.getModelList(new T_SYS_ORG_LINK_SW { ORGNO = BYORGNO, ORGLINKTYPE = ORGLINKTYPE, keys = keys }).OrderBy(o => o.ORDERBY).ToList();
                if (ORGLINKTYPE != "")//下属村委会
                {
                    result = T_SYS_ORG_LINKCls.getModelList(new T_SYS_ORG_LINK_SW { ORGNO = ORGLINKTYPE, keys = keys }).OrderBy(o => o.ORDERBY).ToList();
                    result = result.Where(p => p.UNITNAME != "").ToList();
                }
                foreach (var v in result)
                {
                    sb.AppendFormat("<tr class='{0}' onclick=\"LinkOnclik('{1}','{2}','{3}','{4}','{5}','{6}','{7}')\">", (i % 2 == 0) ? "" : "row1", v.ORGLINK_ID, v.UNITNAME, v.NAME, v.USERJOB, v.PHONE, v.Tell, v.ORDERBY);
                    if (string.IsNullOrEmpty(v.ORDERBY) == false) { orderby = Convert.ToInt16(v.ORDERBY) + 1; }
                    sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                    if (ORGLINKTYPE != "")
                    {
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.UNITNAME);
                    }
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.USERJOB);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.PHONE);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.Tell);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORDERBY);
                    sb.AppendFormat("<td class=\" \">    </td>");
                    sb.AppendFormat("</tr>");
                    i++;
                }
                #endregion
            }
            #endregion

            #region
            sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
            sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
            bool isShowUnit = false;
            if ((PublicCls.OrgIsShi(BYORGNO) == true || PublicCls.OrgIsXian(BYORGNO) == true) && ORGLINKTYPE == "3") { isShowUnit = true; }
            if (PublicCls.OrgIsZhen(BYORGNO) && ORGLINKTYPE != "") { isShowUnit = true; }
            if (isShowUnit == true)
                sb.AppendFormat("<td class=\"left\">{0}</td>", "<input type=\"text\" id=\"tbxLinkUNITNAME\" name=\"tbxLinkUNITNAME\" />");
            else
                sb.AppendFormat("{0}", "<input type=\"hidden\" id=\"tbxLinkUNITNAME\" name=\"tbxLinkUNITNAME\" />");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"tbxLinkUSERJOB\" name=\"tbxLinkUSERJOB\" />");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"tbxLinkNAME\" name=\"tbxLinkNAME\" />");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"tbxLinkPHONE\" name=\"tbxLinkPHONE\" />");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"tbxLinkTell\"/ name=\"tbxLinkTell\" >");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"tbxLinkORDERBY\" name=\"tbxLinkORDERBY\" value=\"" + orderby.ToString() + "\"/>");
            sb.AppendFormat("    <td class=\" \">");
            sb.AppendFormat("{0}", "<input type=\"hidden\" id=\"tbxLinkORGLINK_ID\"  id=\"tbxLinkORGLINK_ID\" />");
            sb.AppendFormat("{0}", "<input type=\"button\" class=\"btnAddCss\" value=\"添加\" onclick=\"ManagerLink('Add','2')\"/>");
            sb.AppendFormat("&nbsp;{0}", "<input type=\"button\" class=\"btnMdyCss\"  id=\"btnLinkMdy\" style=\"display:none;\" value=\"修改\" onclick=\"ManagerLink('Mdy','2')\" />");
            sb.AppendFormat("&nbsp;{0}", "<input type=\"button\" class=\"btnDelCss\"  id=\"btnLinkDel\" style=\"display:none;\"  value=\"删除\" onclick=\"ManagerLink('Del','2')\" />");
            sb.AppendFormat("    </td>");
            sb.AppendFormat("</tr>");
            i++;
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        #endregion

        #region 村委会管理
        public ActionResult ManagerCWH()
        {
            T_SYS_ORG_CWH_Model m = new T_SYS_ORG_CWH_Model();
            m.CWHID = Request.Params["CWHID"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.CWHNAME = Request.Params["CWHNAME"];
            m.ORDERBY = Request.Params["ORDERBY"];
            m.opMethod = Request.Params["Method"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
            {
                m.opMethod = "Add";
            }
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.CWHNAME))
                {
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入村委会名称!", "")), "text/html;charset=UTF-8");
                }
            }
            return Content(JsonConvert.SerializeObject(T_SYS_ORG_CWHCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 村委会列表
        /// <summary>
        /// 村委会列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getOrgCWHlist()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>村委会名称</th>");
            sb.AppendFormat("<th>排序号</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = T_SYS_ORG_CWHCls.getModelList(new T_SYS_ORG_CWH_SW { BYORGNO = BYORGNO });
            int orderby = 1;
            foreach (var v in result)
            {
                sb.AppendFormat("<tr class='{3}' onclick=\"CWHOnclik('{0}','{1}','{2}')\">", v.CWHID, v.CWHNAME, v.ORDERBY, (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.CWHNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORDERBY);
                if (string.IsNullOrEmpty(v.ORDERBY) == false)
                    orderby = Convert.ToInt16(v.ORDERBY) + 1;
                sb.AppendFormat("    <td class=\" \">");
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            if (i % 2 == 0)
                sb.AppendFormat("<tr>");
            else
                sb.AppendFormat("<tr class='row1'>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
            sb.AppendFormat("<td class=\"left\">{0}</td>", "<input type=\"text\" id=\"CWHNAME\"/>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"CWHORDERBY\" value=\"" + orderby.ToString() + "\"/>");
            sb.AppendFormat("    <td class=\" \">");
            sb.AppendFormat("{0}", "<input type=\"hidden\" id=\"CWHID\"/>");
            sb.AppendFormat("{0}", "<input type=\"button\" class=\"btnAddCss\" value=\"添加\" onclick=\"ManagerCWH('Add')\" />");
            sb.AppendFormat("{0}", "<input type=\"button\" class=\"btnMdyCss\" id=\"btnCWHMdy\" style=\"display:none;\" value=\"修改\" onclick=\"ManagerCWH('Mdy')\"  />");
            sb.AppendFormat("{0}", "<input type=\"button\" class=\"btnDelCss\" id=\"btnCWHDel\" style=\"display:none;\"  value=\"删除\" onclick=\"ManagerCWH('Del')\" />");
            sb.AppendFormat("    </td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), "")), "text/html;charset=UTF-8"); ;
        }
        #endregion

        #region 组织机构图查看页面
        public ActionResult OrgMapShow()
        {
            pubViewBag("011012", "011012", "");
            if (ViewBag.isPageRight == false)
                return View();
            return View();
        }
        #endregion

        #region 组织机构图查看
        /// <summary>
        /// 查询-弹出列表展示
        /// </summary>
        /// <returns></returns>
        public ActionResult getOrgShow()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            string nr = Request.Params["nr"];
            string TypeID = Request.Params["TypeID"];
            string chwID = Request.Params["chwID"];
            if (string.IsNullOrEmpty(BYORGNO)) { BYORGNO = ConfigCls.getTopOrgCode() + "00000000000"; }
            StringBuilder sb = new StringBuilder();

            #region 市级，显示市、县、乡
            if (PublicCls.OrgIsShi(BYORGNO))
            {
                //if (string.IsNullOrEmpty(nr) == false)//显示市列表
                //{
                //    sb.AppendFormat(getnr(BYORGNO, "nr", TypeID));
                //}
                //else
                //{
                #region 显示州领导
                sb.AppendFormat(getnr(BYORGNO, "nr", TypeID));
                #endregion

                #region 显示市列表
                var result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<thead>");
                var shi = result.Where(p => p.ORGNO.Substring(4, 5) == "00000").FirstOrDefault();
                //获取市名称
                //sb.AppendFormat("<tr>");                  
                //sb.AppendFormat("<th colspan=\"100\" class=\"left\"><font color=red>说明：点击下方的<b>[{0}]</b>可以查看领导、办公室、指挥部等信息</font></th>", shi.ORGNAME + shi.COMMANDNAME);
                //sb.AppendFormat("</tr>");
                //获取市名称
                //sb.AppendFormat("<tr>");                   
                //sb.AppendFormat("<th colspan=\"100\" class=\"Center OrgLinkOrgName\"><a href=\"#\" onclick=\"query('{1}','nr','')\">{0}</a></th>", shi.ORGNAME + shi.COMMANDNAME, BYORGNO);
                //sb.AppendFormat("</tr>");
                sb.AppendFormat("</thead>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("<tr>");
                var orgX = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), GetContyORGNOByCity = BYORGNO });
                var xianList = result.Where(p => p.ORGNO.Substring(6, 3) == "000" && p.ORGNO.Substring(4, 5) != "00000");
                foreach (var v in xianList)
                {
                    sb.AppendFormat("<td class=\"center\"><a href=\"#\" onclick=\"query('{1}','','')\">{0}</a></td>", v.ORGNAME, v.ORGNO);
                }
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                foreach (var v in xianList)
                {
                    var orgZ = result.Where(p => p.ORGNO.Substring(0, 6) == v.ORGNO.Substring(0, 6) && p.ORGNO.Substring(6, 3) != "000");
                    sb.AppendFormat("<td class=\"center\">{0}个</td>", orgZ.Count().ToString());
                }
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                foreach (var v in xianList)
                {
                    var orgZ = result.Where(p => p.ORGNO.Substring(0, 6) == v.ORGNO.Substring(0, 6) && p.ORGNO.Substring(6, 3) != "000");
                    sb.AppendFormat("<td class=\"left\" style=\"vertical-align:top;\">");
                    foreach (var vZ in orgZ)
                    {
                        sb.AppendFormat("<a href=\"#\" onclick=\"query('{1}','','')\">{0}</a><br>", vZ.ORGNAME, vZ.ORGNO);
                    }
                    sb.AppendFormat("</td>");
                }
                sb.AppendFormat("</tr>");
                sb.AppendFormat("</tbody>");
                sb.AppendFormat("</table>");

                #endregion
                //}
            }
            #endregion

            #region 县级，显示乡、村委会
            else if (PublicCls.OrgIsXian(BYORGNO))
            {
                //if (string.IsNullOrEmpty(nr) == false)//显示市列表
                //{
                //    sb.AppendFormat(getnr(BYORGNO, "nr", TypeID));
                //}
                //else
                //{
                #region 显示市领导
                sb.AppendFormat(getnr(BYORGNO, "nr", TypeID));
                #endregion

                #region 显示县列表
                var result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
                var shi = result.Where(p => p.ORGNO == BYORGNO).FirstOrDefault();
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<thead>");
                //获取市名称
                //sb.AppendFormat("<tr>");                  
                //sb.AppendFormat("<th colspan=\"100\" class=\"left\"><font color=red>说明：点击下方的<b>[{0}]</b>可以查看领导、办公室、指挥部等信息</font></th>", shi.ORGNAME + shi.COMMANDNAME);
                //sb.AppendFormat("</tr>");
                //获取市名称
                //sb.AppendFormat("<tr>");                  
                //sb.AppendFormat("<th colspan=\"100\" class=\"Center OrgLinkOrgName\"><a href=\"#\" onclick=\"query('{1}','nr','')\">{0}</a>", shi.ORGNAME + shi.COMMANDNAME, BYORGNO);
                //sb.AppendFormat("&nbsp;[<a href=\"#\" onclick=\"query('{0}','','')\">返回上级</a>]", PublicCls.getShiIncOrgNo(BYORGNO) + "00000");
                //sb.AppendFormat("</th>");
                //sb.AppendFormat("</tr>");
                sb.AppendFormat("</thead>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("<tr>");
                var xianList = result.Where(p => p.ORGNO.Substring(6, 3) != "000" && p.ORGNO.Substring(0, 6) == BYORGNO.Substring(0, 6));
                foreach (var v in xianList)
                {
                    sb.AppendFormat("<td class=\"center\"><a href=\"#\" onclick=\"query('{1}','','')\">{0}</a></td>", v.ORGNAME, v.ORGNO);
                }
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                var resultCWH = T_SYS_ORG_CWHCls.getModelList(new T_SYS_ORG_CWH_SW { BYORGNO = BYORGNO });
                foreach (var v in xianList)
                {
                    var orgZ = resultCWH.Where(p => p.BYORGNO == v.ORGNO);
                    sb.AppendFormat("<td class=\"center\">{0}个</td>", orgZ.Count().ToString());
                }
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                foreach (var v in xianList)
                {
                    var orgCWH = resultCWH.Where(p => p.BYORGNO == v.ORGNO);
                    sb.AppendFormat("<td class=\"left\" style=\"vertical-align:top;\">");
                    foreach (var vZ in orgCWH)
                    {
                        sb.AppendFormat("<a href=\"#\" onclick=\"query('{1}','','','{2}')\">{0}</a><br>", vZ.CWHNAME, v.ORGNO, vZ.CWHID);
                    }
                    sb.AppendFormat("</td>");
                }
                sb.AppendFormat("</tr>");
                sb.AppendFormat("</tbody>");
                sb.AppendFormat("</table>");
                #endregion
                //}
            }
            #endregion

            #region 镇级，显示村委会
            else if (PublicCls.OrgIsZhen(BYORGNO))
            {
                //if (string.IsNullOrEmpty(nr) == false)//显示市列表
                //{
                //    sb.AppendFormat(getnr(BYORGNO, "nr", TypeID));
                //}
                //else
                //{
                #region 显示镇领导
                sb.AppendFormat(getnr(BYORGNO, "nr", TypeID));
                #endregion

                #region 显示乡镇列表
                var result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<thead>");
                var shi = result.Where(p => p.ORGNO == BYORGNO).FirstOrDefault();
                //获取市名称
                //sb.AppendFormat("<tr>");                  
                //sb.AppendFormat("<th colspan=\"100\" class=\"left\"><font color=red>说明：点击下方的<b>[{0}]</b>可以查看乡镇领导等信息</font></th>", shi.ORGNAME + shi.COMMANDNAME);
                //sb.AppendFormat("</tr>");
                //获取市名称
                //sb.AppendFormat("<tr>");                   
                //sb.AppendFormat("<th colspan=\"100\" class=\"Center OrgLinkOrgName\"><a href=\"#\" onclick=\"query('{1}','nr','')\">{0}</a>", shi.ORGNAME + shi.COMMANDNAME, BYORGNO);
                //sb.AppendFormat("&nbsp;[<a href=\"#\" onclick=\"query('{0}','','')\">返回上级</a>]" , PublicCls.getXianIncOrgNo(BYORGNO) + "000");
                //sb.AppendFormat("</th>");
                //sb.AppendFormat("</tr>"); 
                sb.AppendFormat("</thead>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("<tr>");
                var resultCWH = T_SYS_ORG_CWHCls.getModelList(new T_SYS_ORG_CWH_SW { BYORGNO = BYORGNO });
                foreach (var v in resultCWH)
                {
                    chwID = (string.IsNullOrEmpty(chwID)) ? v.CWHID : chwID;
                    if (chwID != v.CWHID)
                    {
                        sb.AppendFormat("<td class=\"center\"><a href=\"#\" onclick=\"query('{1}','','','{2}')\">{0}</a></td>", v.CWHNAME, BYORGNO, v.CWHID);
                    }
                    else
                    {
                        sb.AppendFormat("<td class=\"center OrgLinkCur\"><a href=\"#\" onclick=\"query('{1}','','','{2}')\"><b>{0}</b></a></td>", v.CWHNAME, BYORGNO, v.CWHID);
                    }
                }
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\" colspan=\"100\">");
                sb.AppendFormat(getLinkTableByCWH(chwID));
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("</tbody>");
                sb.AppendFormat("</table>");
                #endregion
                //}
            }
            #endregion

            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), "")), "text/html;charset=UTF-8"); ;
        }
        #endregion

        #region 某单位详细内容查看
        /// <summary>
        /// 某单位详细内容查看
        /// </summary>
        /// <param name="BYORGNO">组织机构编码</param>
        /// <param name="nr"></param>
        /// <param name="typeID">类型</param>
        /// <returns></returns>
        private string getnr(string BYORGNO, string nr, string typeID)
        {
            StringBuilder sb = new StringBuilder();
            var result = T_SYS_ORG_LINKCls.getDict45(new T_SYS_ORG_LINK_SW { ORGNO = BYORGNO });
            var resultOrg = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            T_SYS_ORGModel shi = resultOrg.Where(p => p.ORGNO == BYORGNO).FirstOrDefault();
            //获取市名称
            //sb.AppendFormat("<tr>");           
            //sb.AppendFormat("<th colspan=\"100\" class=\"left\"><font color=red>说明：再次点击下方的<b>[{0}]</b>返回</font></th>", shi.ORGNAME + shi.COMMANDNAME);
            //sb.AppendFormat("</tr>");
            //获取市名称
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th colspan=\"100\" class=\"Center OrgLinkOrgName\"><a href=\"#\" >{0}</a>", shi.ORGNAME + shi.COMMANDNAME);
            if (!PublicCls.OrgIsShi(shi.ORGNO))
            {
                if (PublicCls.OrgIsZhen(shi.ORGNO))
                {
                    sb.AppendFormat("&nbsp;[<a href=\"#\" onclick=\"query('{0}','','')\">返回上级</a>]", PublicCls.getXianIncOrgNo(BYORGNO) + "000000000");
                }
                else
                {
                    sb.AppendFormat("&nbsp;[<a href=\"#\" onclick=\"query('{0}','','')\">返回上级</a>]", PublicCls.getShiIncOrgNo(BYORGNO) + "00000000000");
                }
            }
            sb.AppendFormat("</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            if (PublicCls.OrgIsZhen(BYORGNO) == false)//如果是镇，显示镇机构下所有联系人,而不需要显示类别
            {
                sb.AppendFormat("<tr>");
                foreach (var v in result)
                {
                    typeID = (string.IsNullOrEmpty(typeID)) ? v.DICTVALUE : typeID;
                    sb.AppendFormat("<td class=\"center {4}\"><a href=\"#\" onclick=\"query('{1}','{2}','{3}')\">{0}</a></td>", v.DICTNAME, BYORGNO, nr, v.DICTVALUE, (typeID == v.DICTVALUE) ? "OrgLinkCur" : "");
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td colspan=\"100\">");
            sb.AppendFormat(getLinkTableByOrgnoType(BYORGNO, typeID));
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        #endregion

        #region 根据机构编码和类型获取联系人表格信息
        /// <summary>
        /// 根据机构编码和类型获取联系人表格信息
        /// </summary>
        /// <param name="BYORGNO">机构编码</param>
        /// <param name="TypeID">类型</param>
        /// <returns></returns>
        private string getLinkTableByOrgnoType(string BYORGNO, string TypeID)
        {
            if (PublicCls.OrgIsZhen(BYORGNO) == true)//如果是镇，显示该镇所有联系方式，而不分领导、相关单位等
                TypeID = "";
            StringBuilder sb = new StringBuilder();
            List<T_SYS_ORG_LINK_Model> result = T_SYS_ORG_LINKCls.getModelList(new T_SYS_ORG_LINK_SW { ORGNO = BYORGNO, ORGLINKTYPE = TypeID }).ToList();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            if (TypeID == "3")
            {
                sb.AppendFormat("<th>单位名称</th>");
                sb.AppendFormat("<th>职位</th>");
                sb.AppendFormat("<th>联系人</th>");
            }
            else if (TypeID == "4")
            {
                //sb.AppendFormat("<th>村委会名称</th>");
                sb.AppendFormat("<th>职位</th>");
                sb.AppendFormat("<th>联系人</th>");
            }
            else
            {
                sb.AppendFormat("");
                sb.AppendFormat("<th>职位</th>");
                sb.AppendFormat("<th>姓名</th>");
            }
            sb.AppendFormat("<th>手机</th>");
            sb.AppendFormat("<th>电话</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0) { sb.AppendFormat("<tr>"); }
                else { sb.AppendFormat("<tr class='row1'>"); }
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                if (TypeID == "3") { sb.AppendFormat("<td class=\"center\">{0}</td>", v.UNITNAME); }
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.USERJOB);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PHONE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.Tell);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        #endregion

        #region 根据村委会序号获取联系人表格信息
        /// <summary>
        /// 根据村委会序号获取联系人表格信息
        /// </summary>
        /// <param name="CWHID">村委会序号</param>
        /// <returns></returns>
        private string getLinkTableByCWH(string CWHID)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(CWHID) == false)
            {
                int i = 0;
                var result = T_SYS_ORG_LINKCls.getModelList(new T_SYS_ORG_LINK_SW { ORGNO = CWHID });
                if (result.Where(p => p.ORGLINKTYPE == "1").Count() > 0)
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                    sb.AppendFormat("<thead>");
                    sb.AppendFormat("<tr><th colspan=\"100\">村委会</th></tr>");
                    sb.AppendFormat("</thead>");
                    sb.AppendFormat("<tbody>");
                    sb.AppendFormat("<tr><th>序号</th><th>职位</th><th>姓名</th><th>手机</th><th>电话</th></tr>");
                    sb.AppendFormat("<tr>");
                    foreach (var v in result.Where(p => p.ORGLINKTYPE == "1"))
                    {
                        if (i % 2 == 0)
                            sb.AppendFormat("<tr>");
                        else
                            sb.AppendFormat("<tr class='row1'>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.USERJOB);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.PHONE);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.Tell);
                        sb.AppendFormat("</tr>");
                        i++;
                    }
                    sb.AppendFormat("</tbody>");
                    sb.AppendFormat("</table>");
                }
                if (result.Where(p => p.ORGLINKTYPE == "2").Count() > 0)
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                    sb.AppendFormat("<thead>");
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<th colspan=\"100\">下属自然村</th>");
                    sb.AppendFormat("</tr>");
                    sb.AppendFormat("</thead>");
                    sb.AppendFormat("<tbody>");
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<th>序号</th>");
                    sb.AppendFormat("<th>村名</th>");
                    sb.AppendFormat("<th>职位</th>");
                    sb.AppendFormat("<th>联系人</th>");
                    sb.AppendFormat("<th>手机</th>");
                    sb.AppendFormat("<th>电话</th>");
                    sb.AppendFormat("</tr>");
                    i = 0;
                    foreach (var v in result.Where(p => p.ORGLINKTYPE == "2"))
                    {
                        if (i % 2 == 0)
                            sb.AppendFormat("<tr>");
                        else
                            sb.AppendFormat("<tr class='row1'>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.UNITNAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.USERJOB);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.PHONE);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", v.Tell);
                        sb.AppendFormat("</tr>");
                        i++;
                    }
                    sb.AppendFormat("</tbody>");
                    sb.AppendFormat("</table>");
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 根据机构编码获取对应的类型下拉框
        /// <summary>
        /// 根据机构编码获取对应的类型下拉框
        /// </summary>
        /// <param name="orgNo">机构编码</param>
        /// <returns></returns>
        public ActionResult getORGLINKTYPEByOrgNo(string orgNo)
        {
            StringBuilder sb = new StringBuilder();
            string orgType = "0";
            sb.AppendFormat("<select id=\"tbxORGLINKTYPE\" name=\"tbxORGLINKTYPE\" onchange=\"query()\">");
            if (PublicCls.OrgIsZhen(orgNo))
            {
                var list = T_SYS_ORG_CWHCls.getModelList(new T_SYS_ORG_CWH_SW { BYORGNO = orgNo });
                sb.AppendFormat("<option value=\"{0}\">{1}</option>", "", "乡镇领导");
                foreach (var v in list)
                {
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", v.CWHID, v.CWHNAME);
                }
                orgType = "1";
            }
            else
            {
                sb.AppendFormat(T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "45" }));
            }
            sb.AppendFormat("</select>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), orgType)), "text/html;charset=UTF-8");
        }

        #endregion

        #region 组织机构图上传

        [HttpPost]
        public ActionResult OrgUpload(FormCollection form)
        {
            pubViewBag("006013", "006013", "");
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                //扩展名
                string extension = System.IO.Path.GetExtension(File.FileName);//
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串
                    string name = DateTime.Now.ToString("组织机构图-yyyyMMddHHmmss") + extension;
                    if (!FileType.Contains(extension))
                    {
                        //ViewBag.error = "文件类型不对，只能导入xls和xlsx格式的文件";
                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件!');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        //ViewBag.error = "上传文件超过4M，不能上传";
                        //return View();
                        return Content(@"<script>alert('上传文件超过4M，不能上传!');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/HRExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        OrgUpload(savePath);
                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传!');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的护林员表格!');history.go(-1);</script>");
                }
            }
            return Content("<script>alert('导入成功!');window.location.href='OrgMapList';</script>");
        }
        #endregion

        #region 组织机构图上传
        /// <summary>
        /// 组织机构图上传
        /// </summary>
        /// <param name="filePath">文件路径</param>
        private static void OrgUpload(string filePath)
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
                string[] arr = new string[9];
                for (int k = 0; k < arr.Length; k++)
                {
                    arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString().Trim();//循环获取每一单元格内容
                }
                OrgMapUpload_Model m = new OrgMapUpload_Model();
                //县名称	乡镇名称	村委会名称(为空代表乡镇领导)	自然村名称(空代表村委员会人员）	联系人	职位	手机	电话	排序号
                m.县名称 = arr[0];
                m.乡镇名称 = arr[1];
                m.村委会名称 = arr[2];
                m.自然村名称 = arr[3];
                m.联系人 = arr[4];
                m.职位 = arr[5];
                m.手机 = arr[6];
                m.电话 = arr[7];
                m.排序号 = arr[8];
                T_SYS_ORG_CWHCls.CWHUploadAdd(m);
            }
        }
        #endregion

        #endregion

        #region 菜单管理
        /// <summary>
        /// 菜单管理的增删改查
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuManager()
        {
            T_SYS_MENU_Model m = new T_SYS_MENU_Model();
            m.MENUCODE = Request.Params["MENUCODE"];
            m.MENUNAME = Request.Params["MENUNAME"];
            m.MENUURL = Request.Params["MENUURL"];
            m.MENUICO = Request.Params["MENUICO"];
            m.LICLASS = Request.Params["LICLASS"];
            m.ORDERBY = Request.Params["ORDERBY"];
            m.MENURIGHTFLAG = Request.Params["MENURIGHTFLAG"];
            m.SYSFLAG = Request.Params["SYSFLAG"];
            m.MENUOPENMETHOD = Request.Params["MENUOPENMETHOD"];
            m.MENULINKMODE = Request.Params["MENULINKMODE"];
            m.MENUDROWMTHOD = Request.Params["MENUDROWMTHOD"];
            m.ISTOPMENU = Request.Params["ISTOPMENU"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(T_SYS_MENUCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 菜单管理列表
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuList()
        {
            pubViewBag("006016", "006016", "");
            if (ViewBag.isPageRight == false)
                return View();
            string menucode = Request.Params["MENUCODE"];//当前页面传递编号
            string navStr = "";
            if (!string.IsNullOrEmpty(menucode))
            {
                int len = menucode.Length / 3;
                if (len >= 1)
                {
                    for (int i = 0; i < len; i++)
                    {
                        if (i != len - 1)
                            navStr += "<li class=\"active\"><a href=\"/System/MenuList?MENUCODE=" + menucode.Substring(0, (i + 1) * 3) + "\" >" + T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = menucode.Substring(0, (i + 1) * 3), SYSFLAG = ConfigCls.getSystemFlag() }) + "</a></li>";
                        else
                            navStr += "<li class=\"active\">" + T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = menucode, SYSFLAG = ConfigCls.getSystemFlag() }) + "</li>";
                    }
                }
            }
            ViewBag.MENUCODE = menucode;
            ViewBag.navList = navStr;
            if (string.IsNullOrEmpty(menucode))
                ViewBag.MenuList = getMENUStr(new T_SYS_MENU_SW { IsGetTopCode = true });
            else
                ViewBag.MenuList = getMENUStr(new T_SYS_MENU_SW { MENUCODE = menucode, ChildCODELength = menucode.Length + 3 });
            return View();
        }

        /// <summary>
        /// 菜单管理列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuListQuery()
        {
            string MENUCODE = Request.Params["MENUCODE"];//当前页面传递编号
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(MENUCODE))
                sb.AppendFormat(getMENUStr(new T_SYS_MENU_SW { IsGetTopCode = true }));
            else
                sb.AppendFormat(getMENUStr(new T_SYS_MENU_SW { MENUCODE = MENUCODE, ChildCODELength = MENUCODE.Length + 3 }));
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取菜单管理列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        private string getMENUStr(T_SYS_MENU_SW sw)
        {
            List<T_SYS_MENU_Model> result = T_SYS_MENUCls.getListModel(sw).ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"MenuTable\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr>");
            sb.AppendFormat("<th>序号</th><th>编码</th><th>名称</th><th>显示样式</th><th>权限标识</th><th>系统标识</th><th>打开方式</th><th>关联子模块</th><th>下拉方式</th><th>是否为顶部菜单</th><th>排序号</th><th></th></tr></thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("<tr class='{0}'>", i % 2 == 0 ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"/System/MenuList?MENUCODE={0}\" >{1}</a>", v.MENUCODE, v.MENUCODE);
                sb.AppendFormat("</td>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MENUNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LICLASS);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MENURIGHTFLAG);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.SYSFLAG);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MENUOPENMETHODName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MENULINKMODE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MENUDROWMTHODName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ISTOPMENUName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORDERBY);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href='#' onclick=\"Manager('{0}','Mdy')\"  class=\"searchBox_01 LinkMdy\">修改</a>", v.MENUCODE);
                sb.AppendFormat("&nbsp; <a href='#' onclick=\"Manager('{0}','Del')\"  class=\"searchBox_01 LinkDel\">删除</a>", v.MENUCODE);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// 菜单管理
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuMan()
        {
            string Method = Request.Params["Method"];
            string MENUCODE = Request.Params["MENUCODE"];
            T_SYS_MENU_Model model = new T_SYS_MENU_Model();
            if (!string.IsNullOrEmpty(MENUCODE))
            {
                if (Method == "Mdy")
                    model = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = MENUCODE });
            }
            ViewBag.MENUOPENMETHOD = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "201", DICTVALUE = model.MENUOPENMETHOD });
            ViewBag.MENUDROWMTHOD = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "202", DICTVALUE = model.MENUDROWMTHOD });
            ViewBag.ISTOPMENU = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "203", DICTVALUE = model.ISTOPMENU });
            ViewBag.MENUOPENMETHOD = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "201", DICTVALUE = model.MENUOPENMETHOD });
            ViewBag.MENUDROWMTHOD = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "202", DICTVALUE = model.MENUDROWMTHOD });
            ViewBag.ISTOPMENU = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "203", DICTVALUE = model.ISTOPMENU });
            ViewBag.MENUOPENMETHOD = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "201", DICTVALUE = model.MENUOPENMETHOD });
            ViewBag.MENUDROWMTHOD = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "202", DICTVALUE = model.MENUDROWMTHOD });
            ViewBag.ISTOPMENU = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "203", DICTVALUE = model.ISTOPMENU });
            ViewBag.Method = Method;
            return View(model);
        }

        #endregion

        #region 任务管理
        /// <summary>
        /// 任务管理增删改
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskManager()
        {
            var hlylist = Request.Params["HLYLIST"];
            TASK_INFO_Model m = new TASK_INFO_Model();
            m.TASK_INFOID = Request.Params["TASK_INFOID"];////删除 修改用
            m.BYORGNO = Request.Params["BYORGNO"];
            m.TASKTITLE = Request.Params["NAME"];
            m.TASKTYPE = Request.Params["TYPE"];
            m.TASKLEVEL = Request.Params["LEVEL"];
            m.TASKSTAUTS = Request.Params["STATUS"];//0新建 1已审核 2已下发 3已反馈 4已结束
            m.TASKSTARTTIME = Request.Params["STARTTIME"];//任务发起时间
            m.TASKBEGINTIME = Request.Params["BEGINTIME"];
            m.TASKENDTIME = Request.Params["ENDTIME"];
            m.TASKCONTENT = Request.Params["CONTENT"];
            m.opMethod = Request.Params["METHOD"];

            TASK_FEEDBACK_Model m1 = new TASK_FEEDBACK_Model();
            m1.HID = hlylist;
            m1.TASK_INFOID = Request.Params["TASK_INFOID"];//删除 修改用
            m1.RECEIVETIME = Request.Params["STARTTIME"];//接收时间=下发时间

            if (m.opMethod == "Mdy")
            {
                var OHID = TASK_FEEDBACKCls.getModelList(new TASK_FEEDBACK_SW { TASK_INFOID = m1.TASK_INFOID }).Where(p => p.FEEDBACKSTATUS != "-1").Select(p => p.HID);//获取护林员(去除任务已经取消的护林员)
                var arrHly = hlylist.Split(',').Where(p => p != null && p != "");
                bool isUpdate = OHID.Except(arrHly).Count() > 0;
                bool isAdd = arrHly.Except(OHID).Count() > 0;

                if (isUpdate)
                {
                    var diffOUpdate = OHID.Except(arrHly);//删除的需改变状态
                    m1.OHID = string.Join(",", diffOUpdate);
                }
                else
                {
                    m1.OHID = "";
                }

                if (isAdd)
                {
                    var diffNAdd = arrHly.Except(OHID);//新增加的
                    m1.NHID = string.Join(",", diffNAdd);
                }
                else
                {
                    m1.NHID = "";
                }
            }
            TASK_TURNOVER_Model m2 = new TASK_TURNOVER_Model();
            //m2.TASK_INFOID = Request.Params["TASK_INFOID"];
            m2.OPUID = SystemCls.getUserID();//操作人
            m2.OPTIME = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            m2.OPSTATUS = Request.Params["STATUS"];
            m2.OPTITLE = Request.Params["OPTITLE"];
            m2.OPIP = ClsHtml.GetIP();
            var ms = TASK_INFOCls.Manager(m, m1, m2);
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 任务管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskList()
        {
            pubViewBag("006017", "006017", "");
            ViewBag.TASKTYPE = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "401" });
            ViewBag.TASKLEVEL = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "402" });
            ViewBag.isAdd = (SystemCls.isRight("006017001")) ? "1" : "0";
            return View();
        }

        public JsonResult GetHidOrg(string hid)
        {
            string strorg = SystemCls.getHUserOrg(hid);
            return Json(strorg);
        }

        public JsonResult GetInterval()
        {
            return Json("success");
        }

        /// <summary>
        /// 获取需修改的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTaskjson()
        {
            string TASK_INFOID = Request.Params["TASK_INFOID"];
            return Content(JsonConvert.SerializeObject(TASK_INFOCls.getModel(new TASK_INFO_SW { TASK_INFOID = TASK_INFOID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getTaskList()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string TASKTITLE = Request.Params["TASKTITLE"];
            string STATUS = Request.Params["STATUS"];
            string BYORGNO = Request.Params["BYORGNO"];

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>单位</th>");
            sb.AppendFormat("<th>任务名称</th>");
            sb.AppendFormat("<th>任务类型</th>");
            sb.AppendFormat("<th>任务级别</th>");
            sb.AppendFormat("<th>任务开始时间</th>");
            sb.AppendFormat("<th>任务结束时间</th>");
            sb.AppendFormat("<th>状态</th>");
            sb.AppendFormat("<th>操作</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = TASK_INFOCls.getModelList(new TASK_INFO_SW { TASKTITLE = TASKTITLE, TASKSTAUTS = STATUS, BYORGNO = BYORGNO, curPage = int.Parse(page), pageSize = int.Parse(PageSize) }, out total);
            foreach (var s in result)
            {
                var type = T_SYS_DICTCls.getModel(new T_SYS_DICTSW { DICTTYPEID = "401", DICTVALUE = s.TASKTYPE }).DICTNAME;
                var level = T_SYS_DICTCls.getModel(new T_SYS_DICTSW { DICTTYPEID = "402", DICTVALUE = s.TASKLEVEL }).DICTNAME;
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center  \">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center  \">{0}</td>", s.ORGNAME);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", s.TASKTITLE);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", type);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", level);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", Convert.ToDateTime(s.TASKBEGINTIME).ToString("yyyy-MM-dd hh:mm:ss"));
                sb.AppendFormat("<td class=\"center  \">{0}</td>", Convert.ToDateTime(s.TASKENDTIME).ToString("yyyy-MM-dd hh:mm:ss"));
                if (s.TASKSTAUTS == "0")
                {
                    sb.AppendFormat("<td class=\"center  \">{0}</td>", "新增");
                }
                else if (s.TASKSTAUTS == "2")
                {
                    sb.AppendFormat("<td class=\"center  \">{0}</td>", "已下发");
                }
                else if (s.TASKSTAUTS == "3")
                {
                    sb.AppendFormat("<td class=\"center  \">{0}</td>", "已反馈");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center  \">{0}</td>", "已结束");
                }
                sb.AppendFormat("    <td class=\" \">");
                if (SystemCls.isRight("006017002") == true) //查看
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.TASK_INFOID);
                }
                if (SystemCls.isRight("006017003") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('{0}','Mdy','{1}')\" title='编辑'  class=\"searchBox_01 LinkMdy\">修改</a>", s.TASK_INFOID, page);
                if (SystemCls.isRight("006017004") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('{0}','Del','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.TASK_INFOID, page);
                if (SystemCls.isRight("006017004") == true)
                    if (s.TASKSTAUTS != "4")
                        sb.AppendFormat("<a href=\"#\" onclick=\"Manager('{0}','End','{1}')\" title='结束' class=\"searchBox_01 LinkDel\">结束</a>", s.TASK_INFOID, page);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }

        /// <summary>
        /// 获取护林员下拉列表
        /// </summary>
        /// <returns></returns>
        public ActionResult HlyGet()
        {
            string str = Request.Params["phonehname"];
            string ID = Request.Params["id"];
            //string hidlist = Request.Params["hid"];//护林员id集合
            string result = T_IPSFR_USERCls.getTreeByTask(new T_IPSFR_USER_SW { PhoneHname = str }, ID);
            return Content(result, "application/json");
        }

        /// <summary>
        /// 查看页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SeeDetail()
        {
            string ID = Request.Params["ID"];
            var m = TASK_INFOCls.getModel(new TASK_INFO_SW { TASK_INFOID = ID });
            var result = TASK_FEEDBACKCls.getModelList(new TASK_FEEDBACK_SW { TASK_INFOID = ID, }).Where(p => p.FEEDBACKSTATUS != "-1");//查询任务没有取消的护林员
            string hname = "";
            foreach (var item in result)
            {
                hname += item.HNAME + ",";
            }
            hname = hname.Substring(0, hname.Length - 1);
            StringBuilder sb = new StringBuilder();
            var type = T_SYS_DICTCls.getModel(new T_SYS_DICTSW { DICTTYPEID = "401", DICTVALUE = m.TASKTYPE }).DICTNAME;
            var level = T_SYS_DICTCls.getModel(new T_SYS_DICTSW { DICTTYPEID = "402", DICTVALUE = m.TASKLEVEL }).DICTNAME;
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"text-align:left\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th colspan=\"4\">任务信息</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">单位名称:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ORGNAME);
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">任务名称:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.TASKTITLE);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">任务类型:</td>");
            sb.AppendFormat("<td >{0}</td>", type);
            sb.AppendFormat("<td class=\"tdField\">任务级别:</td>");
            sb.AppendFormat("<td >{0}</td>", level);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">任务开始时间:</td>");
            sb.AppendFormat("<td >{0}</td>", Convert.ToDateTime(m.TASKBEGINTIME).ToString("yyyy-MM-dd hh:mm:ss"));
            sb.AppendFormat("<td class=\"tdField\">任务结束时间:</td>");
            sb.AppendFormat("<td >{0}</td>", Convert.ToDateTime(m.TASKENDTIME).ToString("yyyy-MM-dd hh:mm:ss"));
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">任务发起时间:</td>");
            sb.AppendFormat("<td >{0}</td>", Convert.ToDateTime(m.TASKBEGINTIME).ToString("yyyy-MM-dd hh:mm:ss"));
            sb.AppendFormat("<td class=\"tdField\">是否下发:</td>");
            if (m.TASKSTAUTS == "2" || m.TASKSTAUTS == "3" || m.TASKSTAUTS == "4")
                sb.AppendFormat("<td><input type=\"checkbox\" checked=\"checked\" disabled=\"disabled\"></td>");
            else
                sb.AppendFormat("<td><input type=\"checkbox\" disabled=\"disabled\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">指派护林员:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", hname);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">任务内容:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.TASKCONTENT);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tbody>");
            sb.AppendFormat("</table>");

            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th colspan=\"4\">护林员反馈信息</th></tr>");
            sb.AppendFormat("<tr><th>护林员姓名</th><th>接受时间</th><th>反馈时间</th><th>反馈内容</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            if (result.Count() > 0)
            {
                foreach (var item in result)
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", item.HNAME);
                    if (item.ACCEPTTIME != "1900/1/1 0:00:00")
                        sb.AppendFormat("<td class=\"center\">{0}</td>", Convert.ToDateTime(item.ACCEPTTIME).ToString("yyyy-MM-dd hh:mm:ss"));
                    else
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "");
                    if (item.FEEDBACKTIME != "1900/1/1 0:00:00")
                        sb.AppendFormat("<td class=\"center\">{0}</td>", Convert.ToDateTime(item.FEEDBACKTIME).ToString("yyyy-MM-dd hh:mm:ss"));
                    else
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "");
                    sb.AppendFormat("<td class=\"left\">{0}</td>", item.FEEDBACKCONTENT);
                    sb.AppendFormat("</tr>");
                }
            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\">无护林员反馈信息!</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeDetail = sb.ToString();
            return View();
        }

        /// <summary>
        /// 获取组织机构
        /// </summary>
        /// <returns></returns>
        public ActionResult OrgTreeget()
        {
            string orgno = Request.Params["orgno"];
            if (string.IsNullOrEmpty(orgno))
                orgno = SystemCls.getCurUserOrgNo();
            var str = T_SYS_ORGCls.getORGTree(new T_SYS_ORGSW { ORGNO = orgno });
            return Content(str.ToString(), "application/json");
        }

        /// <summary>
        /// 仅仅获取单独的市县镇
        /// </summary>
        /// <returns></returns>
        public ActionResult OnlyOrgTreeget()
        {
            string orgno = Request.Params["orgno"];
            if (string.IsNullOrEmpty(orgno))
                orgno = SystemCls.getCurUserOrgNo();
            var str = T_SYS_ORGCls.getOnlyORGTree(new T_SYS_ORGSW { ORGNO = orgno });
            return Content(str.ToString(), "application/json");
        }
        #endregion

        #region 无人机管理
        public ActionResult UavManager()
        {
            JC_UAV_Model m = new JC_UAV_Model();
            m.BYORGNO = Request.Params["BYORGNO"];
            m.UAVNAME = Request.Params["NAME"];
            m.UAVEQUIPNAME = Request.Params["EQUIPNAME"];
            m.UAVID = Request.Params["UAVID"];
            m.opMethod = Request.Params["METHOD"];
            var ms = JC_UAVCls.Manager(m);
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取需修改的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUavjson()
        {
            string UAVID = Request.Params["UAVID"];
            return Content(JsonConvert.SerializeObject(JC_UAVCls.getModel(new JC_UAV_SW { UAVID = UAVID })), "text/html;charset=UTF-8");
        }

        public ActionResult UavList()
        {
            pubViewBag("006018", "006018", "");
            ViewBag.isAdd = (SystemCls.isRight("006018001")) ? "1" : "0";
            return View();
        }

        /// <summary>
        /// 获取无人机列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getUavList()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string NAME = Request.Params["NAME"];
            string BYORGNO = Request.Params["BYORGNO"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>单位</th>");
            sb.AppendFormat("<th>无人机名称</th>");
            sb.AppendFormat("<th>设备名称</th>");
            sb.AppendFormat("<th>操作</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total;
            var result = JC_UAVCls.getModelList(new JC_UAV_SW { UAVNAME = NAME, BYORGNO = BYORGNO, curPage = int.Parse(page), pageSize = int.Parse(PageSize) }, out total);
            foreach (var s in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center  \">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center  \">{0}</td>", s.ORGNAME);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", s.UAVNAME);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", s.UAVEQUIPNAME);
                sb.AppendFormat("    <td class=\" \">");
                if (SystemCls.isRight("006018002") == true) //查看
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.UAVID);
                }
                if (SystemCls.isRight("006018003") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('{0}','Mdy','{1}')\" title='编辑'  class=\"searchBox_01 LinkMdy\">修改</a>", s.UAVID, page);
                if (SystemCls.isRight("006018004") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('{0}','Del','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.UAVID, page);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }

            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }

        /// <summary>
        /// 查看无人机信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SeeUav()
        {
            string ID = Request.Params["ID"];
            var m = JC_UAVCls.getModel(new JC_UAV_SW { UAVID = ID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"text-align:left\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th colspan=\"6\">无人机信息</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" >单位名称:</td>");
            sb.AppendFormat("<td >{0}</td>", m.ORGNAME);
            sb.AppendFormat("<td class=\"tdField\" >无人机名称:</td>");
            sb.AppendFormat("<td >{0}</td>", m.UAVNAME);
            sb.AppendFormat("<td  class=\"tdField\" >设备名称:</td>");
            sb.AppendFormat("<td >{0}</td>", m.UAVEQUIPNAME);
            sb.AppendFormat("</tr>");
            //sb.AppendFormat("<tr>");
            //sb.AppendFormat("<td  class=\"tdField\" style=\"width:15%\">设备名称:</td>");
            //sb.AppendFormat("<td colspan=\"3\" style=\"width:35%\">{0}</td>", m.UAVEQUIPNAME);
            //sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            ViewBag.uav = sb.ToString();
            return View();
        }
        #endregion

        #region 生物分类管理
        /// <summary>
        /// 生物分类
        /// </summary>
        /// <returns></returns>
        public ActionResult BiologicalTypeList()
        {
            pubViewBag("006025", "006025", "");
            if (ViewBag.isPageRight == false)
                return View();
            return View();
        }

        /// <summary>
        /// 取出树形菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult BiologicalTypeTreeGet()
        {
            string idcode = Request.Params["id"];
            string result = T_SYS_BIOLOGICALTYPECls.GetTypeTree(new T_SYS_BIOLOGICALTYPE_SW { BIOLOCODE = idcode });
            return Content(result, "application/json");
        }

        /// <summary>
        /// 分类分别下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult BiologicalTypeOptionGet()
        {
            string code = Request.Params["BioloCode"];
            string option = T_SYS_BIOLOGICALTYPECls.GetSelectOption(new T_SYS_BIOLOGICALTYPE_SW { CurCODE = code });
            return Content(JsonConvert.SerializeObject(new Message(true, option, "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 类别改变自动生成类别编码
        /// </summary>
        /// <returns></returns>
        public ActionResult BiologicalTypeChange()
        {
            string code = Request.Params["BioloCode"];
            string new_code = "";
            if (!string.IsNullOrEmpty(code))
            {
                if (PublicCls.BioCodeIsZHong(code))
                {
                    new_code = GeneralZhongCode(code);
                }
                else
                {
                    List<T_SYS_BIOLOGICALTYPE_Model> templist = T_SYS_BIOLOGICALTYPECls.getListModel(new T_SYS_BIOLOGICALTYPE_SW { BIOLOCODE = code, IsOnlyGetChild = true }).ToList();
                    if (templist.Count > 0)
                    {
                        new_code = templist[templist.Count - 1].BIOLOCODE;
                        if (PublicCls.BioCodeIsJie(new_code))
                            new_code = GeneralJieCode(new_code);
                        else if (PublicCls.BioCodeIsMen(new_code))
                            new_code = GeneralMenCode(new_code);
                        else if (PublicCls.BioCodeIsGang(new_code))
                            new_code = GeneralGangCode(new_code);
                        else if (PublicCls.BioCodeIsMu(new_code))
                            new_code = GeneralMuCode(new_code);
                        else if (PublicCls.BioCodeIsKe(new_code))
                            new_code = GeneralKeCode(new_code);
                        else if (PublicCls.BioCodeIsShu(new_code))
                            new_code = GeneralShuCode(new_code);
                        else if (PublicCls.BioCodeIsZHong(new_code))
                            new_code = GeneralZhongCode(new_code);
                    }
                    else
                    {
                        new_code = code;
                        if (PublicCls.BioCodeIsJie(new_code))
                            new_code = GeneralMenCode(new_code);
                        else if (PublicCls.BioCodeIsMen(new_code))
                            new_code = GeneralGangCode(new_code);
                        else if (PublicCls.BioCodeIsGang(new_code))
                            new_code = GeneralMuCode(new_code);
                        else if (PublicCls.BioCodeIsMu(new_code))
                            new_code = GeneralKeCode(new_code);
                        else if (PublicCls.BioCodeIsKe(new_code))
                            new_code = GeneralShuCode(new_code);
                        else if (PublicCls.BioCodeIsShu(new_code))
                            new_code = GeneralZhongCode(new_code);
                    }
                }
            }
            else
                new_code = "01000000000000";
            return Content(JsonConvert.SerializeObject(new Message(true, new_code, "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取生物分类模型
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBiologicalTypeJson()
        {
            string code = Request.Params["BioloCode"];
            return Content(JsonConvert.SerializeObject(T_SYS_BIOLOGICALTYPECls.getModel(new T_SYS_BIOLOGICALTYPE_SW { BIOLOCODE = code })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 生物分类管理
        /// </summary>
        /// <returns></returns>
        public ActionResult BiologicalTypeManager()
        {
            T_SYS_BIOLOGICALTYPE_Model m = new T_SYS_BIOLOGICALTYPE_Model();
            m.BIOLOTYPE = Request.Params["BioloType"];
            m.BIOLOCODE = Request.Params["BioloCode"];
            m.BIOLONAME = Request.Params["BioloName"];
            m.BIOLOENNAME = Request.Params["BioEnName"];
            m.ORDERBY = Request.Params["ORDERBY"];
            m.opMethod = Request.Params["Method"];
            if (string.IsNullOrEmpty(m.ORDERBY))
                m.ORDERBY = "0";
            if (m.opMethod != "Del")
            {
                if (m.BIOLOCODE.Substring(0, 2) == "00")
                    return Content(JsonConvert.SerializeObject(new Message(false, "界级编码不能输入00!", "")), "text/html;charset=UTF-8");
                if (m.BIOLOCODE == "00000000000000")
                    return Content(JsonConvert.SerializeObject(new Message(false, "7级编码不能同时输入00!", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(T_SYS_BIOLOGICALTYPECls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 本地化生物关联
        /// <summary>
        /// 本地化生物关联
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalPestJoin()
        {
            pubViewBag("006019", "006019", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.vdSearType = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "123", isShowAll = "1" });
            return View();
        }

        /// <summary>
        /// 本地化生物关联--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalPestJoinQuery()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            string orgNO = Request.Params["ORGNO"];
            string searchType = Request.Params["SEARCHTYPE"];
            int total = 0;
            PEST_LOCALPESTJOIN_SW sw = new PEST_LOCALPESTJOIN_SW { CurPage = int.Parse(Page), PageSize = int.Parse(PageSize), BYORGNO = orgNO, SEARCHTYPE = searchType };
            var result = PEST_LOCALPESTJOINCls.getListModel(sw, out total).Where(a => a.PESTBYCODENAME != "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr><th>序号</th><th>单位</th><th>调查类型</th><th>科</th><th>属</th><th>种</th><th>操作</th></tr></thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGNONAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.SEARCHTYPENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PESTKENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PESTSHUNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PESTBYCODENAME);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href='#' onclick=\"manager('prop','{0}')\" style=\"width:120px\" class=\"searchBox_01 LinkMdy\">属性管理</a>", v.PESTBYCODE);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8");
        }

        #region 本地化管理
        /// <summary>
        /// 本地化生物管理
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalPestJoinMan()
        {
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.vdSearType = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "123" });
            return View();
        }

        /// <summary>
        /// 获取种级生物列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPestBioZhongList()
        {
            StringBuilder sb = new StringBuilder();
            var result = T_SYS_BIOLOGICALTYPECls.getZhongListModel(new T_SYS_BIOLOGICALTYPE_SW { IsOnlyGetZhong = true });
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("{0}{1}", "<input id=\"tbxBioCode" + i + "\" name=\"tbxBioCode\"  type=\"checkbox\" class=\"ace\" value=\"" + v.BIOLOCODE + "\" onclick=\"selectall(this.value,this.checked)\" />", v.BIOLOKENAME + "-" + v.BIOLOSHUNAME + "-" + v.BIOLONAME);
                if (i != result.Count() - 1)
                    sb.AppendFormat("<br /><br />");
                i++;
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取已关联的种级生物列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPestJoinBioZhongList()
        {
            string orgNO = Request.Params["ORGNO"];
            string searchType = Request.Params["SEARCHTYPE"];
            StringBuilder sb = new StringBuilder();
            var result = PEST_LOCALPESTJOINCls.getListModel(new PEST_LOCALPESTJOIN_SW { BYORGNO = orgNO, SEARCHTYPE = searchType });
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("{0}{1}", "<input id=\"tbxJoinBioCode" + i + "\" name=\"tbxJoinBioCode\"  type=\"checkbox\" class=\"ace\" value=\"" + v.PESTBYCODE + "\" onclick=\"selectall(this.value,this.checked)\" />", v.PESTKENAME + "-" + v.PESTSHUNAME + "-" + v.PESTBYCODENAME);
                if (i != result.Count() - 1)
                    sb.AppendFormat("<br /><br />");
                i++;
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 本地化树种关联数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalPestJoinManager()
        {
            PEST_LOCALPESTJOIN_Model m = new PEST_LOCALPESTJOIN_Model();
            m.BYORGNO = Request.Params["ORGNO"];
            m.PESTBYCODE = Request.Params["BIOCODE"];
            m.SEARCHTYPE = Request.Params["SEARCHTYPE"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(PEST_LOCALPESTJOINCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        /// <summary>
        /// 属性管理
        /// </summary>
        /// <returns></returns>
        public ActionResult PropTotalMan()
        {
            return View();
        }

        #region 属性管理
        /// <summary>
        /// 属性管理
        /// </summary>
        /// <returns></returns>
        public ActionResult PropMan()
        {
            string bioCode = Request.Params["BioCode"];
            string bioName = T_SYS_BIOLOGICALTYPECls.getName(bioCode);
            ViewBag.BioCode = bioCode;
            ViewBag.BioName = bioName;
            List<T_SYS_DICTModel> _dic124List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "124" }).ToList();
            ViewBag.dic124Value = T_SYS_DICTCls.getDicValueStr(_dic124List);
            ViewBag.dic124Count = _dic124List.Count;
            ViewBag.vdQurantine = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "125" });
            ViewBag.vdRisk = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "126" });
            return View();
        }

        /// <summary>
        /// 获取基本属性
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProp()
        {
            string bioCode = Request.Params["Biocode"];
            PEST_PESTPROP_Model m = PEST_PESTPROPCls.getModel(new PEST_PESTPROP_SW { BIOLOGICALTYPECODE = bioCode });
            return Content(JsonConvert.SerializeObject(m), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取动态属性
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDyProp()
        {
            string bioCode = Request.Params["Biocode"];
            List<T_SYS_DICTModel> _dic124List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "124" }).ToList();
            List<PEST_PESTDYNAMICPROP_Model> _templist = PEST_PESTDYNAMICPROPCls.getListModel(new PEST_PESTDYNAMICPROP_SW { BIOLOGICALTYPECODE = bioCode }).ToList();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _dic124List.Count; i++)
            {
                string content = "";
                PEST_PESTDYNAMICPROP_Model dm = _templist.Find(a => a.DYNAMICPROPCODE == _dic124List[i].DICTVALUE);
                if (dm != null && dm.DYNAMICPROPCONTENT != null)
                    content = dm.DYNAMICPROPCONTENT;
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">{0}：</td>", _dic124List[i].DICTNAME);
                sb.AppendFormat("<td colspan=\"3\" style=\"height:50px;\"><textarea id=\"tbx" + _dic124List[i].DICTVALUE + "\" style=\"width:99%; height: 100%\">" + content + "</textarea></td>");
                sb.AppendFormat("</tr>");
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 属性数据-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult PropManager()
        {
            PEST_PESTPROP_Model m = new PEST_PESTPROP_Model();
            m.BIOLOGICALTYPECODE = Request.Params["Biocode"];
            m.QUARANTINE = Request.Params["Qurantine"];
            m.RISK = Request.Params["Risk"];
            var ms = PEST_PESTPROPCls.Manager(m);
            if (ms.Success)
            {
                PEST_PESTDYNAMICPROP_Model dm = new PEST_PESTDYNAMICPROP_Model();
                dm.BIOLOGICALTYPECODE = m.BIOLOGICALTYPECODE;
                dm.DYNAMICPROPCODE = Request.Params["RropCode"];
                dm.DYNAMICPROPCONTENT = Request.Params["PropContent"];
                PEST_PESTDYNAMICPROPCls.Manager(dm);
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        #endregion

        #region 附件管理
        /// <summary>
        /// 附件管理
        /// </summary>
        /// <returns></returns>
        public ActionResult InfoFileMan()
        {
            string bioCode = Request.Params["BioCode"];
            string bioName = T_SYS_BIOLOGICALTYPECls.getName(bioCode);
            ViewBag.BioCode = bioCode;
            ViewBag.BioName = bioName;
            return View();
        }

        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFiles()
        {
            string bioCode = Request.Params["BioCode"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr><th>序号</th><th>生物名称</th><th>照片名称</th><th>上传时间</th><th>缩略图</th></tr></thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = PEST_PESTINFOFILECls.getModelList(new PEST_PESTINFOFILE_SW { BIOLOGICALTYPECODE = bioCode });
            foreach (var s in result)
            {
                sb.AppendFormat("<tr class=\"{4}\" onclick=\"showValues('{0}','{1}','{2}','{3}')\">", s.PESTFILEID, s.BIOLOGICALTYPECODE, s.PESTFILETITLE, s.PESTFILENAME, (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.BIOLOGICALTYPENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PESTFILETITLE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.UPLOADTIME);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{0}\" alt=\"alttext\" title=\"{1}\" height =\"35px\" wideth=\"35px\"/></a>", s.PESTFILENAME, s.PESTFILETITLE);
                sb.AppendFormat("</td>");
                sb.AppendFormat("<td class=\"center\"></td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 附件图片上传
        /// </summary>
        /// <returns></returns>
        public JsonResult InfoFileUpload()
        {
            string pestFileId = Request.Params["PestFileId"];
            string bioCode = Request.Params["BioCode"];
            string pestFileTitle = Request.Params["PestFileTitle"];
            string pestFileName = Request.Params["PestFileName"];
            string Method = Request.Params["Method"];
            string UID = SystemCls.getCookieInfo().UID;
            Message ms = null;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string[] arr = hfc[0].FileName.Split('.');
            string type = arr[arr.Length - 1].ToLower();
            if (Method == "Mdy" && string.IsNullOrEmpty(hfc[0].FileName))
            {
                PEST_PESTINFOFILE_Model m = new PEST_PESTINFOFILE_Model();
                m.PESTFILEID = pestFileId;
                m.BIOLOGICALTYPECODE = bioCode;
                m.PESTFILETITLE = pestFileTitle;
                m.UID = UID;
                m.opMethod = "MdyTP";
                ms = PEST_PESTINFOFILECls.Manager(m);
            }
            else
            {
                if (string.IsNullOrEmpty(pestFileTitle))
                    return Json(new Message(false, "请输入附件名!", ""));
                if (string.IsNullOrEmpty(hfc[0].FileName))
                    return Json(new Message(false, "请选择上传图片!", ""));
                if (type != "jpg" && type != "jpeg" && type != "bmp" && type != "gif" && type != "png")
                    return Json(new Message(false, "禁止上传非图片文件!", ""));
                if (hfc.Count > 0)
                {
                    string ipath = "~/UploadFile/PESTINFOFILE/";//相对路径
                    string phyPath = Server.MapPath(ipath);
                    if (!Directory.Exists(phyPath))//判断文件夹是否已经存在
                        Directory.CreateDirectory(phyPath);//创建文件夹
                    PEST_PESTINFOFILE_Model m = new PEST_PESTINFOFILE_Model();
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        m.PESTFILEID = pestFileId;
                        m.BIOLOGICALTYPECODE = bioCode;
                        m.PESTFILETITLE = pestFileTitle;
                        m.UPLOADTIME = DateTime.Now.ToString();
                        m.PESTFILENAME = "/UploadFile/PESTINFOFILE/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + type;
                        m.UID = UID;
                        string PhysicalPath = Server.MapPath(m.PESTFILENAME);
                        hfc[i].SaveAs(PhysicalPath);
                        m.opMethod = Method;
                    }
                    ms = PEST_PESTINFOFILECls.Manager(m);
                }
            }
            return Json(ms);
        }

        /// <summary>
        /// 附件管理
        /// </summary>
        /// <returns></returns>
        public ActionResult InfoFileManager()
        {
            PEST_PESTINFOFILE_Model m = new PEST_PESTINFOFILE_Model();
            m.PESTFILEID = Request.Params["PestFileId"];
            m.BIOLOGICALTYPECODE = Request.Params["BioCode"];
            m.PESTFILETITLE = Request.Params["PestFileTitle"];
            m.PESTFILENAME = Request.Params["PestFileName"];
            m.UPLOADTIME = DateTime.Now.ToString();
            m.UID = SystemCls.getCookieInfo().UID;
            m.opMethod = Request.Params["Method"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod == "Del")
            {
                string file = Server.MapPath(m.PESTFILENAME);
                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);
            }
            else
            {
                if (string.IsNullOrEmpty(m.PESTFILETITLE))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入附件名称!", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(PEST_PESTINFOFILECls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #endregion

        #region 本地化树种关联
        /// <summary>
        /// 本地化树种管理
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalTreeJoin()
        {
            pubViewBag("006020", "006020", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.Save = (SystemCls.isRight("006020001")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 本地树种管理--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalTreeJoinQuery()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            string OrgNo = Request.Params["ORGNO"];
            int total = 0;
            PEST_LOCALTREESPECIES_SW sw = new PEST_LOCALTREESPECIES_SW { CurPage = int.Parse(Page), PageSize = int.Parse(PageSize), BYORGNO = OrgNo };
            List<PEST_LOCALTREESPECIES_Model> _templist = PEST_LOCALTREESPECIESCls.getListModel(sw, out total).Where(a => a.TSPNAME != "").ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"localTreeTable\"  cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>单位</th><th>科</th><th>属</th><th>种</th><th>树种面积<br />(" + dic113Name + ")</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            for (int i = 0; i < _templist.Count; i++)
            {
                string area = !string.IsNullOrEmpty(_templist[i].TSPAREA) ? string.Format("{0:0.00}", float.Parse(_templist[i].TSPAREA)) : "";
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}{1}</td>", _templist[i].ORGNONAME, "<input id=\"tbx" + i + "\"  type=\"hidden\"  value=\"" + _templist[i].PEST_LOCALTREESPECIESID + "\"  />");
                sb.AppendFormat("<td class=\"center\">{0}</td>", _templist[i].TSPKENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _templist[i].TSPSHUNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _templist[i].TSPNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxAREA" + i + "\"  type=\"text\"  value=\"" + area + "\" style=\"width:150px;\" class=\"center\"  />");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 本地树种管理
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalTreeJoinMan()
        {
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }

        /// <summary>
        /// 获取种级树种列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTreeBioZhongList()
        {
            StringBuilder sb = new StringBuilder();
            var result = T_SYS_BIOLOGICALTYPECls.getZhongListModel(new T_SYS_BIOLOGICALTYPE_SW { BIOLOCODE = "02000000000000", IsOnlyGetZhong = true });
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("{0}{1}", "<input id=\"tbxBioCode" + i + "\" name=\"tbxBioCode\"  type=\"checkbox\" class=\"ace\" value=\"" + v.BIOLOCODE + "\" onclick=\"selectall(this.value,this.checked)\" />", v.BIOLOKENAME + "-" + v.BIOLOSHUNAME + "-" + v.BIOLONAME);
                if (i != result.Count() - 1)
                    sb.AppendFormat("<br /><br />");
                i++;
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取已关联的树种生物列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTreeJoinBioZhongList()
        {
            string orgNO = Request.Params["ORGNO"];
            StringBuilder sb = new StringBuilder();
            var result = PEST_LOCALTREESPECIESCls.getListModel(new PEST_LOCALTREESPECIES_SW { BYORGNO = orgNO });
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("{0}{1}", "<input id=\"tbxJoinBioCode" + i + "\" name=\"tbxJoinBioCode\"  type=\"checkbox\" class=\"ace\" value=\"" + v.TSPCODE + "\" onclick=\"selectall(this.value,this.checked)\" />", v.TSPKENAME + "-" + v.TSPSHUNAME + "-" + v.TSPNAME);
                if (i != result.Count() - 1)
                    sb.AppendFormat("<br /><br />");
                i++;
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 本地树种管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalTreeJoinManager()
        {
            PEST_LOCALTREESPECIES_Model m = new PEST_LOCALTREESPECIES_Model();
            m.PEST_LOCALTREESPECIESID = Request.Params["Id"];
            m.BYORGNO = Request.Params["ORGNO"];
            m.TSPCODE = Request.Params["TSPCODE"];
            m.TSPAREA = Request.Params["TSPAREA"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(PEST_LOCALTREESPECIESCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 树种有害生物关联
        /// <summary>
        /// 树种有害生物关联
        /// </summary>
        /// <returns></returns>
        public ActionResult PestTreeJoin()
        {
            pubViewBag("006022", "006022", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdTREESPECIES = PEST_LOCALTREESPECIESCls.getSelectOption(new PEST_LOCALTREESPECIES_SW { IsShowAll = "1" });
            return View();
        }

        /// <summary>
        /// 树种有害生物--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult PestTreeJoinQuery()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            string TreeSpecies = Request.Params["TreeSpecies"];
            int total = 0;
            PEST_TREESPECIES_PEST_SW sw = new PEST_TREESPECIES_PEST_SW { CurPage = int.Parse(Page), PageSize = int.Parse(PageSize), TREESPECIESCODE = TreeSpecies };
            var result = PEST_TREESPECIES_PESTCls.getListModel(sw, out total).Where(a => a.TREESPECIESNAME != "" && a.PESTBYNAME != "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>树种名称</th><th>有害生物名称</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("<tr class='{0}'>", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", ((sw.CurPage - 1) * sw.PageSize + i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.TREESPECIESNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PESTBYNAME);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 树种有害生物关联
        /// </summary>
        /// <returns></returns>
        public ActionResult PestTreeJoinMan()
        {
            ViewBag.vdTREESPECIES = PEST_LOCALTREESPECIESCls.getSelectOption(new PEST_LOCALTREESPECIES_SW());
            return View();
        }

        /// <summary>
        /// 获取有害生物列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPestTreeList()
        {
            StringBuilder sb = new StringBuilder();
            var result = PEST_LOCALPESTJOINCls.getListModel(new PEST_LOCALPESTJOIN_SW());
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("{0}{1}", "<input id=\"tbxBioCode" + i + "\" name=\"tbxBioCode\"  type=\"checkbox\" class=\"ace\" value=\"" + v.PESTBYCODE + "\" onclick=\"selectall(this.value,this.checked)\" />", v.PESTKENAME + "-" + v.PESTSHUNAME + "-" + v.PESTBYCODENAME);
                if (i != result.Count() - 1)
                    sb.AppendFormat("<br /><br />");
                i++;
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取已关联的有害生物列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPestTreeJoinList()
        {
            string TreeSpecies = Request.Params["TreeSpecies"];
            StringBuilder sb = new StringBuilder();
            var result = PEST_TREESPECIES_PESTCls.getListModel(new PEST_TREESPECIES_PEST_SW { TREESPECIESCODE = TreeSpecies });
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("{0}{1}", "<input id=\"tbxJoinBioCode" + i + "\" name=\"tbxJoinBioCode\"  type=\"checkbox\" class=\"ace\" value=\"" + v.PESTBYCODE + "\" onclick=\"selectall(this.value,this.checked)\" />", v.PESTKENAME + "-" + v.PESTSHUNAME + "-" + v.PESTBYNAME);
                if (i != result.Count() - 1)
                    sb.AppendFormat("<br /><br />");
                i++;
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 树种有害生物关联-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult PestTreeJoinManager()
        {
            PEST_TREESPECIES_PEST_Model m = new PEST_TREESPECIES_PEST_Model();
            m.TREESPECIESCODE = Request.Params["TreeSpeciesCode"];
            m.PESTBYCODE = Request.Params["PestCode"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(PEST_TREESPECIES_PESTCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 有害生物危害等级导入-市
        /// <summary>
        /// 有害生物危害等级导入
        /// </summary>
        /// <returns></returns>
        public ActionResult PestHarmClassImport()
        {
            pubViewBag("006021", "006021", "");
            if (ViewBag.isPageRight == false)
                return View();
            string dcDate = string.IsNullOrEmpty(Request.Params["DcDate"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request.Params["DcDate"];
            string savePath = Request.Params["SavePath"];
            ViewBag.DcDate = dcDate;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"harmClassTable\"  cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>区域</th><th>经度</th><th>纬度</th><th>危害等级</th><th>日期</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            if (string.IsNullOrEmpty(savePath))
            {
                var areaList = T_ALL_AREACls.getListModel2(new T_ALL_AREA_SW());
                var harmClassList = PEST_HARMCLASSCls.getListModel(new PEST_HARMCLASS_SW { DCDATE = dcDate });
                foreach (var item in areaList)
                {
                    if (PublicCls.OrgIsXian(item.AREACODE) == true && !item.AREANAME.Contains("林业局"))//统计市，即所有县的
                    {
                        PEST_HARMCLASS_Model model = harmClassList.Where(a => a.BYORGNO == item.AREACODE).FirstOrDefault();
                        string str = model != null && !string.IsNullOrEmpty(model.HARMCLASS) ? model.HARMCLASS : "";
                        sb.AppendFormat("<tr class='danger'>");
                        sb.AppendFormat("<td>{0}{1}</td>",
                            "<input type=\"text\" class=\"center\" style=\"width:95%;\"  id=\"" + "tbxAREANAME_" + i + "\" value=\"" + item.AREANAME + "\"  readonly=\"readonly\" />",
                            "<input type=\"hidden\"  id=\"" + "tbxAREACODE_" + i + "\" value=\"" + item.AREACODE + "\"   />");
                        sb.AppendFormat("<td>{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:95%;\"  id=\"" + "tbxJD_" + i + "\" value=\"" + item.JD + "\"  />");
                        sb.AppendFormat("<td>{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:95%;\"  id=\"" + "tbxWD_" + i + "\" value=\"" + item.WD + "\"  />");
                        sb.AppendFormat("<td>{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:95%;\"  id=\"" + "tbxHarmClass_" + i + "\" value=\"" + str + "\"  />");
                        sb.AppendFormat("<td>{0}</td>", "<input type=\"text\" class=\"Wdate\"  style=\"width:95%;\"  id=\"" + "tbxDcDate_" + i + "\"  value=\"" + Convert.ToDateTime(dcDate).ToString("yyyy-MM-dd") + "\"    onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd'})\"  />");
                        sb.AppendFormat("</tr>");
                        i++;
                    }
                }
            }
            else
            {
                string filePath = Server.UrlDecode(savePath);
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
                ISheet sheet = hssfworkbook.GetSheetAt(0);
                int rowCount = sheet.LastRowNum;
                for (int j = (sheet.FirstRowNum + 2); j < rowCount; j++)
                {
                    IRow row = sheet.GetRow(j);
                    string[] arr = new string[2];
                    arr[0] = row.GetCell(0) == null ? "" : row.GetCell(0).ToString();
                    arr[1] = row.GetCell(1) == null ? "" : row.GetCell(1).ToString();
                    var areaModel = T_ALL_AREACls.getModel2(new T_ALL_AREA_SW { AREAJC = arr[0] });
                    sb.AppendFormat("<tr class='danger'>");
                    sb.AppendFormat("<td>{0}{1}</td>",
                        "<input type=\"text\" class=\"center\" style=\"width:95%;\"  id=\"" + "tbxAREANAME_" + i + "\" value=\"" + areaModel.AREANAME + "\"  readonly=\"readonly\" />",
                        "<input type=\"hidden\"  id=\"" + "tbxAREACODE_" + i + "\" value=\"" + areaModel.AREACODE + "\"   />");
                    sb.AppendFormat("<td>{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:95%;\"  id=\"" + "tbxJD_" + i + "\" value=\"" + areaModel.JD + "\"  />");
                    sb.AppendFormat("<td>{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:95%;\"  id=\"" + "tbxWD_" + i + "\" value=\"" + areaModel.WD + "\"  />");
                    sb.AppendFormat("<td>{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:95%;\"  id=\"" + "tbxHarmClass_" + i + "\" value=\"" + arr[1] + "\" />");
                    sb.AppendFormat("<td>{0}</td>", "<input type=\"text\" class=\"Wdate\"  style=\"width:95%;\"  id=\"" + "tbxDcDate_" + i + "\"  value=\"" + Convert.ToDateTime(dcDate).ToString("yyyy-MM-dd") + "\"    onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd'})\"  />");
                    sb.AppendFormat("</tr>");
                    i++;
                }
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            ViewBag.tableList = sb.ToString();
            List<T_SYS_DICTModel> dic126list = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "126" }).ToList();
            ViewBag.dic126Value = T_SYS_DICTCls.getDicValueStr(dic126list);
            return View();
        }

        /// <summary>
        /// 火险等级上传
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PestHarmClassImport(FormCollection form)
        {
            string date = Request.Form["tbxDcDate"];
            if (date == "")
                return Content("<script>alert('请选择要导入的日期!');window.location.href='PestHarmClassImport';</script>");
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                string extension = System.IO.Path.GetExtension(File.FileName);
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串
                    string name = DateTime.Now.ToString("有害生物危害等级导入-yyyyMMddHHmmss!") + extension;
                    if (!FileType.Contains(extension))
                    {
                        return Content(@"<script>alert('文件类型不对,只能导入xls和xlsx格式的文件!');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content(@"<script>alert('上传文件超过4M,不能上传!');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/PESTHARMCLASSExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        var EncodeSavepath = Server.UrlEncode(savePath);//编码                       
                        return Content(@"<script>window.location.href='PestHarmClassImport?DcDate=" + date + "&SavePath=" + EncodeSavepath + "'</script>");
                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误,请确认后再上传!');history.go(-1);</script>");
                    }
                }
                else
                    return Content(@"<script>alert('请选择需要导入的有害生物危害等级文件!');history.go(-1);</script>");
            }
            return View();
        }

        /// <summary>
        /// 有害生物危害等级保存
        /// </summary>
        /// <returns></returns>
        public ActionResult PestHarmClassImportManager()
        {
            PEST_HARMCLASS_Model m = new PEST_HARMCLASS_Model();
            m.DCDATE = Request.Params["DcDate"];
            m.BYORGNO = Request.Params["ByOrgno"];
            m.TOWNNAME = Request.Params["TownName"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.HARMCLASS = Request.Params["HarmClass"];
            return Content(JsonConvert.SerializeObject(PEST_HARMCLASSCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 有害生物危害等级导入-市、县、乡镇
        /// <summary>
        /// 有害生物危害等级导入
        /// </summary>
        /// <returns></returns>
        public ActionResult PestHarmClassImportNew()
        {
            pubViewBag("006021", "006021", "");
            if (ViewBag.isPageRight == false)
                return View();
            StringBuilder sb = new StringBuilder();
            var result = PEST_HARMCLASSCls.getTopListModel(new PEST_HARMCLASS_SW() { });
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>单位</th><th>经度</th><th>纬度</th><th>时间</th><th>危害等级</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var item in result)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"left\" style=\"{1}\" >{0}</td>", item.TOWNNAME, PublicCls.getOrgTDNameClass(item.BYORGNO, item.BYORGNO));
                sb.AppendFormat("<td>{0}</td>", item.JD);
                sb.AppendFormat("<td>{0}</td>", item.WD);
                sb.AppendFormat("<td>{0}</td>", item.DCDATE);
                sb.AppendFormat("<td>{0}</td>", item.HARMCLASS);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            ViewBag.TableList = sb.ToString();
            return View();
        }

        /// <summary>
        /// 火险等级文件导入
        /// </summary>
        /// <returns></returns>
        public JsonResult ImportPestHarmClass()
        {
            Message ms = null;
            //接收上传后的文件
            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["Filedata"];
            //判断上传的文件是否为空
            if (file != null)
            {
                string ipath = System.Configuration.ConfigurationManager.AppSettings["PESTHARMCLASSTxtPath"].ToString();//相对路径
                string PhysicalPath = Server.MapPath(ipath + "\\");
                if (!Directory.Exists(PhysicalPath))//判断文件夹是否已经存在
                {
                    Directory.CreateDirectory(PhysicalPath);//创建文件夹
                }
                //保存路径
                string type = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);
                string newName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + "." + type;
                try
                {
                    string path = PhysicalPath + newName;
                    //保存文件
                    file.SaveAs(path);
                    bool bo = ReadTxt(path);//读取txt 文件 入库
                    if (bo)
                    {
                        ms = new Message(true, "上传成功!", "");
                    }
                    else
                    {
                        ms = new Message(false, "上传失败,检查导入文件格式!", "");
                    }
                }
                catch (Exception)
                {
                    ms = new Message(false, "上传出错!", "");
                }
            }
            return Json(ms);
        }
        #endregion

        #region 本地化动物关联
        /// <summary>
        /// 本地化动物关联页面
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalAnimalJoin()
        {
            pubViewBag("006027", "006027", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.Save = (SystemCls.isRight("006027001")) ? 1 : 0;
            return View();
        }
        public ActionResult LocalAnimalJoinQuery()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            string orgNO = Request.Params["ORGNO"];
            int total = 0;
            WILD_LOCALANIMAL_SW sw = new WILD_LOCALANIMAL_SW { CurPage = int.Parse(Page), PageSize = int.Parse(PageSize), BYORGNO = orgNO };
            var result = WILD_LOCALANIMALCls.getListModel(sw, out total);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr><th>序号</th><th>单位</th><th>科</th><th>属</th><th>种</th><th>操作</th></tr></thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGNONAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PESTKENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PESTSHUNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.BIOLOGICALTYPECODENAME);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href='#' onclick=\"manager('prop','{0}')\" style=\"width:120px\" class=\"searchBox_01 LinkMdy\">属性管理</a>", v.BIOLOGICALTYPECODE);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8");
        }

        #region 野生动物本地化管理
        /// <summary>
        /// 野生动物本地化管理
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalAnimalJoinMan()
        {
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });

            return View();
        }

        /// <summary>
        /// 获取种级野生动物列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAnimalZhongList()
        {
            StringBuilder sb = new StringBuilder();
            var result = T_SYS_BIOLOGICALTYPECls.getZhongListModel(new T_SYS_BIOLOGICALTYPE_SW { IsOnlyGetZhong = true, BIOLOCODE = "01000000000000" });
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("{0}{1}", "<input id=\"tbxBioCode" + i + "\" name=\"tbxBioCode\"  type=\"checkbox\" class=\"ace\" value=\"" + v.BIOLOCODE + "\" onclick=\"selectall(this.value,this.checked)\" />", v.BIOLOKENAME + "-" + v.BIOLOSHUNAME + "-" + v.BIOLONAME);
                if (i != result.Count() - 1)
                    sb.AppendFormat("<br /><br />");
                i++;
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取已关联的野生动物列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAnimalJoinZhongList()
        {
            string orgNO = Request.Params["ORGNO"];
            StringBuilder sb = new StringBuilder();
            var result = WILD_LOCALANIMALCls.getListModel(new WILD_LOCALANIMAL_SW { ORGNO = orgNO });
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("{0}{1}", "<input id=\"tbxJoinBioCode" + i + "\" name=\"tbxJoinBioCode\"  type=\"checkbox\" class=\"ace\" value=\"" + v.BIOLOGICALTYPECODE + "\" onclick=\"selectall(this.value,this.checked)\" />", v.PESTKENAME + "-" + v.PESTSHUNAME + "-" + v.BIOLOGICALTYPECODENAME);
                if (i != result.Count() - 1)
                    sb.AppendFormat("<br /><br />");
                i++;
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 本地化野生动植物关联数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalAnimalJoinManager()
        {
            WILD_LOCALANIMAL_Model m = new WILD_LOCALANIMAL_Model();
            m.BYORGNO = Request.Params["ORGNO"];
            m.BIOLOGICALTYPECODE = Request.Params["BIOLOGICALTYPECODE"];
            m.WILD_LOCALANIMALID = Request.Params["WILD_LOCALANIMALID"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(WILD_LOCALANIMALCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 野生动物属性管理
        /// <summary>
        /// 野生动物属性管理
        /// </summary>
        /// <returns></returns>
        public ActionResult WILD_ANIMALPROPMan()
        {
            string bioCode = Request.Params["BioCode"];
            string bioName = T_SYS_BIOLOGICALTYPECls.getName(bioCode);
            ViewBag.BioCode = bioCode;
            ViewBag.BioName = bioName;
            List<T_SYS_DICTModel> _dic128List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "128" }).ToList();
            ViewBag.dic124Value = T_SYS_DICTCls.getDicValueStr(_dic128List);
            ViewBag.dic124Count = _dic128List.Count;
            ViewBag.PROTECTIONLEVEL = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "127" });
            ViewBag.LIVINGSTATUS = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "129" });
            return View();
        }

        /// <summary>
        /// 获取基本属性
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWILD_ANIMALProp()
        {
            string bioCode = Request.Params["Biocode"];
            WILD_ANIMALPROP_Model m = WILD_ANIMALPROPCls.getModel(new WILD_ANIMALPROP_SW { BIOLOGICALTYPECODE = bioCode });
            return Content(JsonConvert.SerializeObject(m), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取动态属性
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDyWILD_ANIMALProp()
        {
            string bioCode = Request.Params["Biocode"];
            List<T_SYS_DICTModel> _dic128List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "128" }).ToList();
            List<WILD_ANIMALDYNAMICPROP_Model> _templist = WILD_ANIMALDYNAMICPROPCls.getListModel(new WILD_ANIMALDYNAMICPROP_SW { BIOLOGICALTYPECODE = bioCode }).ToList();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _dic128List.Count; i++)
            {
                string content = "";
                WILD_ANIMALDYNAMICPROP_Model dm = _templist.Find(a => a.DYNAMICPROPCODE == _dic128List[i].DICTVALUE);
                if (dm != null && dm.DYNAMICPROPCONTENT != null)
                    content = dm.DYNAMICPROPCONTENT;
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">{0}：</td>", _dic128List[i].DICTNAME);
                sb.AppendFormat("<td colspan=\"3\" style=\"height:50px;\"><textarea id=\"tbx" + _dic128List[i].DICTVALUE + "\" style=\"width:99%; height: 100%\">" + content + "</textarea></td>");
                sb.AppendFormat("</tr>");
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 属性数据-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult WILD_ANIMALPROPManager()
        {
            WILD_ANIMALPROP_Model m = new WILD_ANIMALPROP_Model();
            m.BIOLOGICALTYPECODE = Request.Params["Biocode"];
            m.PROTECTIONLEVELCODE = Request.Params["PROTECTIONLEVELCODE"];
            m.LIVINGSTATUSCODE = Request.Params["LIVINGSTATUSCODE"];
            var ms = WILD_ANIMALPROPCls.Manager(m);
            if (ms.Success)
            {
                WILD_ANIMALDYNAMICPROP_Model dm = new WILD_ANIMALDYNAMICPROP_Model();
                dm.BIOLOGICALTYPECODE = m.BIOLOGICALTYPECODE;
                dm.DYNAMICPROPCODE = Request.Params["RropCode"];
                dm.DYNAMICPROPCONTENT = Request.Params["PropContent"];
                WILD_ANIMALDYNAMICPROPCls.Manager(dm);
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        #endregion

        #region 附件管理
        /// <summary>
        /// 附件管理
        /// </summary>
        /// <returns></returns>
        public ActionResult WILD_ANIMALInfoFileMan()
        {
            string bioCode = Request.Params["BioCode"];
            string bioName = T_SYS_BIOLOGICALTYPECls.getName(bioCode);
            ViewBag.BioCode = bioCode;
            ViewBag.BioName = bioName;
            return View();
        }

        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWILD_ANIMALFiles()
        {
            string bioCode = Request.Params["BioCode"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr><th>序号</th><th>生物名称</th><th>附件名称</th><th>上传时间</th><th>缩略图</th></tr></thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = WILD_ANIMALFILECls.getModelList(new WILD_ANIMALFILE_SW { BIOLOGICALTYPECODE = bioCode });
            foreach (var s in result)
            {
                sb.AppendFormat("<tr class=\"{4}\" onclick=\"showValues('{0}','{1}','{2}','{3}')\">", s.PESTFILEID, s.BIOLOGICALTYPECODE, s.PESTFILETITLE, s.PESTFILENAME, (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.BIOLOGICALTYPENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PESTFILETITLE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.UPLOADTIME);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{0}\" alt=\"alttext\" title=\"{1}\" height =\"35px\" wideth=\"35px\"/></a>", s.PESTFILENAME, s.PESTFILETITLE);
                sb.AppendFormat("</td>");
                sb.AppendFormat("<td class=\"center\"></td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 附件图片上传
        /// </summary>
        /// <returns></returns>
        public JsonResult WILD_ANIMALFileUpload()
        {
            string pestFileId = Request.Params["PestFileId"];
            string bioCode = Request.Params["BioCode"];
            string pestFileTitle = Request.Params["PestFileTitle"];
            string pestFileName = Request.Params["PestFileName"];
            string Method = Request.Params["Method"];
            string UID = SystemCls.getCookieInfo().UID;
            Message ms = null;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string[] arr = hfc[0].FileName.Split('.');
            string type = arr[arr.Length - 1].ToLower();
            if (Method == "Mdy" && string.IsNullOrEmpty(hfc[0].FileName))
            {
                WILD_ANIMALFILE_Model m = new WILD_ANIMALFILE_Model();
                m.PESTFILEID = pestFileId;
                m.BIOLOGICALTYPECODE = bioCode;
                m.PESTFILETITLE = pestFileTitle;
                m.UID = UID;
                m.opMethod = "MdyTP";
                ms = WILD_ANIMALFILECls.Manager(m);
            }
            else
            {
                if (string.IsNullOrEmpty(pestFileTitle))
                    return Json(new Message(false, "请输入附件名!", ""));
                if (string.IsNullOrEmpty(hfc[0].FileName))
                    return Json(new Message(false, "请选择上传图片!", ""));
                if (type != "jpg" && type != "jpeg" && type != "bmp" && type != "gif" && type != "png")
                    return Json(new Message(false, "禁止上传非图片文件!", ""));
                if (hfc.Count > 0)
                {
                    string ipath = "~/UploadFile/WILD_ANIMALFILE/";//相对路径
                    string phyPath = Server.MapPath(ipath);
                    if (!Directory.Exists(phyPath))//判断文件夹是否已经存在
                        Directory.CreateDirectory(phyPath);//创建文件夹
                    WILD_ANIMALFILE_Model m = new WILD_ANIMALFILE_Model();
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        m.PESTFILEID = pestFileId;
                        m.BIOLOGICALTYPECODE = bioCode;
                        m.PESTFILETITLE = pestFileTitle;
                        m.UPLOADTIME = DateTime.Now.ToString();
                        m.PESTFILENAME = "/UploadFile/WILD_ANIMALFILE/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + type;
                        m.UID = UID;
                        string PhysicalPath = Server.MapPath(m.PESTFILENAME);
                        hfc[i].SaveAs(PhysicalPath);
                        m.opMethod = Method;
                    }
                    ms = WILD_ANIMALFILECls.Manager(m);
                }
            }
            return Json(ms);
        }

        /// <summary>
        /// 附件管理
        /// </summary>
        /// <returns></returns>
        public ActionResult WILD_ANIMALFileManager()
        {
            WILD_ANIMALFILE_Model m = new WILD_ANIMALFILE_Model();
            m.PESTFILEID = Request.Params["PestFileId"];
            m.BIOLOGICALTYPECODE = Request.Params["BioCode"];
            m.PESTFILETITLE = Request.Params["PestFileTitle"];
            m.PESTFILENAME = Request.Params["PestFileName"];
            m.UPLOADTIME = DateTime.Now.ToString();
            m.UID = SystemCls.getCookieInfo().UID;
            m.opMethod = Request.Params["Method"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod == "Del")
            {
                string file = Server.MapPath(m.PESTFILENAME);
                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);
            }
            else
            {
                if (string.IsNullOrEmpty(m.PESTFILETITLE))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入附件名称!", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(WILD_ANIMALFILECls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 基本属性页面及附件整合
        /// <summary>
        /// 基本属性页面及附件整合
        /// </summary>
        /// <returns></returns>
        public ActionResult WILD_ANIMALPROPTotalMan()
        {
            return View();
        }
        #endregion
        #endregion

        #region 本地化植物关联
        /// <summary>
        /// 本地化植物关联页面
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalBotanyJoin()
        {
            pubViewBag("006028", "006028", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.Save = (SystemCls.isRight("006028001")) ? 1 : 0;
            return View();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalBotanyJoinQuery()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            string orgNO = Request.Params["ORGNO"];
            int total = 0;
            WILD_LOCALBOTANY_SW sw = new WILD_LOCALBOTANY_SW { CurPage = int.Parse(Page), PageSize = int.Parse(PageSize), BYORGNO = orgNO };
            var result = WILD_LOCALBOTANYCls.getListModel(sw, out total);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr><th>序号</th><th>单位</th><th>科</th><th>属</th><th>种</th><th>操作</th></tr></thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGNONAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PESTKENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PESTSHUNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.BIOLOGICALTYPECODENAME);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href='#' onclick=\"manager('prop','{0}')\" style=\"width:120px\" class=\"searchBox_01 LinkMdy\">属性管理</a>", v.BIOLOGICALTYPECODE);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8");
        }


        #region 野生植物本地化管理
        /// <summary>
        /// 野生植物本地化管理
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalBotanyJoinMan()
        {
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });

            return View();
        }

        /// <summary>
        /// 获取种级野生植物列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBotanyZhongList()
        {
            StringBuilder sb = new StringBuilder();
            var result = T_SYS_BIOLOGICALTYPECls.getZhongListModel(new T_SYS_BIOLOGICALTYPE_SW { IsOnlyGetZhong = true, BIOLOCODE = "02000000000000" });
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("{0}{1}", "<input id=\"tbxBioCode" + i + "\" name=\"tbxBioCode\"  type=\"checkbox\" class=\"ace\" value=\"" + v.BIOLOCODE + "\" onclick=\"selectall(this.value,this.checked)\" />", v.BIOLOKENAME + "-" + v.BIOLOSHUNAME + "-" + v.BIOLONAME);
                if (i != result.Count() - 1)
                    sb.AppendFormat("<br /><br />");
                i++;
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取已关联的野生植物列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBotanyJoinZhongList()
        {
            string orgNO = Request.Params["ORGNO"];
            StringBuilder sb = new StringBuilder();
            var result = WILD_LOCALBOTANYCls.getListModel(new WILD_LOCALBOTANY_SW { ORGNO = orgNO });
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("{0}{1}", "<input id=\"tbxJoinBioCode" + i + "\" name=\"tbxJoinBioCode\"  type=\"checkbox\" class=\"ace\" value=\"" + v.BIOLOGICALTYPECODE + "\" onclick=\"selectall(this.value,this.checked)\" />", v.PESTKENAME + "-" + v.PESTSHUNAME + "-" + v.BIOLOGICALTYPECODENAME);
                if (i != result.Count() - 1)
                    sb.AppendFormat("<br /><br />");
                i++;
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 本地化野生植物关联数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalBotanyJoinManager()
        {
            WILD_LOCALBOTANY_Model m = new WILD_LOCALBOTANY_Model();
            m.BYORGNO = Request.Params["ORGNO"];
            m.BIOLOGICALTYPECODE = Request.Params["BIOLOGICALTYPECODE"];
            m.WILD_LOCALBOTANYID = Request.Params["WILD_LOCALBOTANYID"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(WILD_LOCALBOTANYCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 野生植物属性管理
        /// <summary>
        /// 野生植物属性管理
        /// </summary>
        /// <returns></returns>
        public ActionResult WILD_BOTANYPROPMan()
        {
            string bioCode = Request.Params["BioCode"];
            string bioName = T_SYS_BIOLOGICALTYPECls.getName(bioCode);
            ViewBag.BioCode = bioCode;
            ViewBag.BioName = bioName;
            List<T_SYS_DICTModel> _dic130List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "130" }).ToList();
            ViewBag.dic124Value = T_SYS_DICTCls.getDicValueStr(_dic130List);
            ViewBag.dic124Count = _dic130List.Count;
            ViewBag.PROTECTIONLEVEL = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "127" });
            ViewBag.LIVINGSTATUS = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "129" });
            return View();
        }

        /// <summary>
        /// 获取基本属性
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWILD_BOTANYProp()
        {
            string bioCode = Request.Params["Biocode"];
            WILD_BOTANYPROP_Model m = WILD_BOTANYPROPCls.getModel(new WILD_BOTANYPROP_SW { BIOLOGICALTYPECODE = bioCode });
            return Content(JsonConvert.SerializeObject(m), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取动态属性
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDyWILD_BOTANYProp()
        {
            string bioCode = Request.Params["Biocode"];
            List<T_SYS_DICTModel> _dic130List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "130" }).ToList();
            List<WILD_BOTANYDYNAMICPROP_Model> _templist = WILD_BOTANYDYNAMICPROPCls.getListModel(new WILD_BOTANYDYNAMICPROP_SW { BIOLOGICALTYPECODE = bioCode }).ToList();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _dic130List.Count; i++)
            {
                string content = "";
                WILD_BOTANYDYNAMICPROP_Model dm = _templist.Find(a => a.DYNAMICPROPCODE == _dic130List[i].DICTVALUE);
                if (dm != null && dm.DYNAMICPROPCONTENT != null)
                    content = dm.DYNAMICPROPCONTENT;
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">{0}：</td>", _dic130List[i].DICTNAME);
                sb.AppendFormat("<td colspan=\"3\" style=\"height:50px;\"><textarea id=\"tbx" + _dic130List[i].DICTVALUE + "\" style=\"width:99%; height: 100%\">" + content + "</textarea></td>");
                sb.AppendFormat("</tr>");
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 属性数据-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult WILD_BOTANYPROPManager()
        {
            WILD_BOTANYPROP_Model m = new WILD_BOTANYPROP_Model();
            m.BIOLOGICALTYPECODE = Request.Params["Biocode"];
            m.PROTECTIONLEVELCODE = Request.Params["PROTECTIONLEVELCODE"];
            m.LIVINGSTATUSCODE = Request.Params["LIVINGSTATUSCODE"];
            var ms = WILD_BOTANYPROPCls.Manager(m);
            if (ms.Success)
            {
                WILD_BOTANYDYNAMICPROP_Model dm = new WILD_BOTANYDYNAMICPROP_Model();
                dm.BIOLOGICALTYPECODE = m.BIOLOGICALTYPECODE;
                dm.DYNAMICPROPCODE = Request.Params["RropCode"];
                dm.DYNAMICPROPCONTENT = Request.Params["PropContent"];
                WILD_BOTANYDYNAMICPROPCls.Manager(dm);
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        #endregion

        #region 附件管理
        /// <summary>
        /// 附件管理
        /// </summary>
        /// <returns></returns>
        public ActionResult WILD_BOTANYInfoFileMan()
        {
            string bioCode = Request.Params["BioCode"];
            string bioName = T_SYS_BIOLOGICALTYPECls.getName(bioCode);
            ViewBag.BioCode = bioCode;
            ViewBag.BioName = bioName;
            return View();
        }

        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWILD_BOTANYFiles()
        {
            string bioCode = Request.Params["BioCode"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr><th>序号</th><th>生物名称</th><th>附件名称</th><th>上传时间</th><th>缩略图</th></tr></thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = WILD_BOTANYFILECls.getModelList(new WILD_BOTANYFILE_SW { BIOLOGICALTYPECODE = bioCode });
            foreach (var s in result)
            {
                sb.AppendFormat("<tr class=\"{4}\" onclick=\"showValues('{0}','{1}','{2}','{3}')\">", s.PESTFILEID, s.BIOLOGICALTYPECODE, s.PESTFILETITLE, s.PESTFILENAME, (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.BIOLOGICALTYPENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PESTFILETITLE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.UPLOADTIME);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{0}\" alt=\"alttext\" title=\"{1}\" height =\"35px\" wideth=\"35px\"/></a>", s.PESTFILENAME, s.PESTFILETITLE);
                sb.AppendFormat("</td>");
                sb.AppendFormat("<td class=\"center\"></td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 附件图片上传
        /// </summary>
        /// <returns></returns>
        public JsonResult WILD_BOTANYFILEUpload()
        {
            string pestFileId = Request.Params["PestFileId"];
            string bioCode = Request.Params["BioCode"];
            string pestFileTitle = Request.Params["PestFileTitle"];
            string pestFileName = Request.Params["PestFileName"];
            string Method = Request.Params["Method"];
            string UID = SystemCls.getCookieInfo().UID;
            Message ms = null;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string[] arr = hfc[0].FileName.Split('.');
            string type = arr[arr.Length - 1].ToLower();
            if (Method == "Mdy" && string.IsNullOrEmpty(hfc[0].FileName))
            {
                WILD_BOTANYFILE_Model m = new WILD_BOTANYFILE_Model();
                m.PESTFILEID = pestFileId;
                m.BIOLOGICALTYPECODE = bioCode;
                m.PESTFILETITLE = pestFileTitle;
                m.UID = UID;
                m.opMethod = "MdyTP";
                ms = WILD_BOTANYFILECls.Manager(m);
            }
            else
            {
                if (string.IsNullOrEmpty(pestFileTitle))
                    return Json(new Message(false, "请输入附件名!", ""));
                if (string.IsNullOrEmpty(hfc[0].FileName))
                    return Json(new Message(false, "请选择上传图片!", ""));
                if (type != "jpg" && type != "jpeg" && type != "bmp" && type != "gif" && type != "png")
                    return Json(new Message(false, "禁止上传非图片文件!", ""));
                if (hfc.Count > 0)
                {
                    string ipath = "~/UploadFile/WILD_BOTANYFILE/";//相对路径
                    string phyPath = Server.MapPath(ipath);
                    if (!Directory.Exists(phyPath))//判断文件夹是否已经存在
                        Directory.CreateDirectory(phyPath);//创建文件夹
                    WILD_BOTANYFILE_Model m = new WILD_BOTANYFILE_Model();
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        m.PESTFILEID = pestFileId;
                        m.BIOLOGICALTYPECODE = bioCode;
                        m.PESTFILETITLE = pestFileTitle;
                        m.UPLOADTIME = DateTime.Now.ToString();
                        m.PESTFILENAME = "/UploadFile/WILD_BOTANYFILE/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + type;
                        m.UID = UID;
                        string PhysicalPath = Server.MapPath(m.PESTFILENAME);
                        hfc[i].SaveAs(PhysicalPath);
                        m.opMethod = Method;
                    }
                    ms = WILD_BOTANYFILECls.Manager(m);
                }
            }
            return Json(ms);
        }

        /// <summary>
        /// 附件管理
        /// </summary>
        /// <returns></returns>
        public ActionResult WILD_BOTANYFILEManager()
        {
            WILD_BOTANYFILE_Model m = new WILD_BOTANYFILE_Model();
            m.PESTFILEID = Request.Params["PestFileId"];
            m.BIOLOGICALTYPECODE = Request.Params["BioCode"];
            m.PESTFILETITLE = Request.Params["PestFileTitle"];
            m.PESTFILENAME = Request.Params["PestFileName"];
            m.UPLOADTIME = DateTime.Now.ToString();
            m.UID = SystemCls.getCookieInfo().UID;
            m.opMethod = Request.Params["Method"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod == "Del")
            {
                string file = Server.MapPath(m.PESTFILENAME);
                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);
            }
            else
            {
                if (string.IsNullOrEmpty(m.PESTFILETITLE))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入附件名称!", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(WILD_BOTANYFILECls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 基本属性页面及附件整合
        /// <summary>
        /// 基本属性页面及附件整合
        /// </summary>
        /// <returns></returns>
        public ActionResult WILD_BotanyPROPTotalMan()
        {
            return View();
        }
        #endregion
        #endregion

        #region Private
        /// <summary>
        /// 生成界级编码
        /// </summary>
        /// <param name="code">code</param>
        /// <returns></returns>
        private static string GeneralJieCode(string code)
        {
            int temp = int.Parse(code.Substring(0, 2)) + 1;
            if (temp.ToString().Length < 2)
                code = "0" + temp.ToString() + code.Substring(2, 12);
            else if (temp.ToString().Length > 2)
                code = temp.ToString().Substring(0, 2) + code.Substring(2, 12);
            else
                code = temp.ToString() + code.Substring(2, 12);
            return code;
        }

        /// <summary>
        /// 生成门级编码
        /// </summary>
        /// <param name="code">code</param>
        /// <returns></returns>
        private static string GeneralMenCode(string code)
        {
            int temp = int.Parse(code.Substring(2, 2)) + 1;
            if (temp.ToString().Length < 2)
                code = code.Substring(0, 2) + "0" + temp.ToString() + code.Substring(4, 10);
            else if (temp.ToString().Length > 2)
                code = code.Substring(0, 2) + temp.ToString().Substring(0, 2) + code.Substring(4, 10);
            else
                code = code.Substring(0, 2) + temp.ToString() + code.Substring(4, 10);
            return code;
        }

        /// <summary>
        /// 生成纲级编码
        /// </summary>
        /// <param name="code">code</param>
        /// <returns></returns>
        private static string GeneralGangCode(string code)
        {
            int temp = int.Parse(code.Substring(4, 2)) + 1;
            if (temp.ToString().Length < 2)
                code = code.Substring(0, 4) + "0" + temp.ToString() + code.Substring(6, 8);
            else if (temp.ToString().Length > 2)
                code = code.Substring(0, 4) + temp.ToString().Substring(0, 2) + code.Substring(6, 8);
            else
                code = code.Substring(0, 4) + temp.ToString() + code.Substring(6, 8);
            return code;
        }

        /// <summary>
        /// 生成目级编码
        /// </summary>
        /// <param name="code">code</param>
        /// <returns></returns>
        private static string GeneralMuCode(string code)
        {
            int temp = int.Parse(code.Substring(6, 2)) + 1;
            if (temp.ToString().Length < 2)
                code = code.Substring(0, 6) + "0" + temp.ToString() + code.Substring(8, 6);
            else if (temp.ToString().Length > 2)
                code = code.Substring(0, 6) + temp.ToString().Substring(0, 2) + code.Substring(8, 6);
            else
                code = code.Substring(0, 6) + temp.ToString() + code.Substring(8, 6);
            return code;
        }

        /// <summary>
        /// 生成科级编码
        /// </summary>
        /// <param name="code">code</param>
        /// <returns></returns>
        private static string GeneralKeCode(string code)
        {
            int temp = int.Parse(code.Substring(8, 2)) + 1;
            if (temp.ToString().Length < 2)
                code = code.Substring(0, 8) + "0" + temp.ToString() + code.Substring(10, 4);
            else if (temp.ToString().Length > 2)
                code = code.Substring(0, 8) + temp.ToString().Substring(0, 2) + code.Substring(10, 4);
            else
                code = code.Substring(0, 8) + temp.ToString() + code.Substring(10, 4);
            return code;
        }

        /// <summary>
        /// 生成属级编码
        /// </summary>
        /// <param name="code">code</param>
        /// <returns></returns>
        private static string GeneralShuCode(string code)
        {
            int temp = int.Parse(code.Substring(10, 2)) + 1;
            if (temp.ToString().Length < 2)
                code = code.Substring(0, 10) + "0" + temp.ToString() + code.Substring(12, 2);
            else if (temp.ToString().Length > 2)
                code = code.Substring(0, 10) + temp.ToString().Substring(0, 2) + code.Substring(12, 2);
            else
                code = code.Substring(0, 10) + temp.ToString() + code.Substring(12, 2);
            return code;
        }

        /// <summary>
        /// 生成种级编码
        /// </summary>
        /// <param name="code">code</param>
        /// <returns></returns>
        private static string GeneralZhongCode(string code)
        {
            List<T_SYS_BIOLOGICALTYPE_Model> templist = T_SYS_BIOLOGICALTYPECls.getListModel(new T_SYS_BIOLOGICALTYPE_SW { BIOLOCODE = code, IsOnlyGetZhong = true }).ToList();
            if (templist.Count > 0)
            {
                code = templist[templist.Count - 1].BIOLOCODE;
                int temp = int.Parse(code.Substring(12, 2)) + 1;
                if (temp.ToString().Length < 2)
                    code = code.Substring(0, 12) + "0" + temp.ToString();
                else if (temp.ToString().Length > 2)
                    code = code.Substring(0, 12) + temp.ToString().Substring(0, 2);
                else
                    code = code.Substring(0, 12) + temp.ToString();
            }
            else
                code = code.Substring(0, 12) + "01";
            return code;
        }

        /// <summary>
        /// 读取危害等级txt文件并入库
        /// </summary>
        /// <param name="path"></param>
        public bool ReadTxt(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            string dt = "";
            bool bo = false;
            int row = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(line.ToString()))
                {
                    var linelist = line.ToString().Split(' ').Where(p => !string.IsNullOrEmpty(p)).ToArray();//空格分隔
                    if (linelist.Length == 1 && row == 0)//第一行
                    {
                        string time = linelist[0];
                        if (!string.IsNullOrEmpty(time))//20171023
                        {
                            if (time.Length != 8)
                            {
                                bo = false;
                                break;
                            }
                            else
                            {
                                dt = time.Substring(0, 4) + "-" + time.Substring(4, 2) + "-" + time.Substring(6, 2) + " 00:00:00.000";
                                var ss = PEST_HARMCLASSCls.DeleteHarmClassData(new PEST_HARMCLASS_Model { DCDATE = dt });//根据火险等级时间删除
                            }
                        }
                        else
                        {
                            bo = false;
                            break;
                        }
                    }
                    else if (linelist.Length == 0)
                    {
                        bo = false;
                        break;
                    }
                    else
                    {
                        var model = new PEST_HARMCLASS_Model();
                        model.DCDATE = dt;
                        var list = T_ALL_AREACls.getListModel(new T_ALL_AREA_SW());
                        for (int i = 0; i < linelist.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(linelist[i]))
                            {
                                var str = linelist[i].ToString();
                                if (i == 0)
                                {
                                    model.TOWNNAME = str;//乡镇名称
                                    var record = list.Where(p => p.AREAJC.Trim() == str.Trim()).FirstOrDefault();
                                    if (record != null)
                                    {
                                        model.BYORGNO = record.AREACODE;//机构编码
                                    }
                                }
                                else if (i == 1)
                                {
                                    model.JD = str;//经度
                                }
                                else if (i == 2)
                                {
                                    model.WD = str;//纬度
                                }
                                else if (i == 3)
                                {
                                    model.HARMCLASS = str;//火险等级
                                }
                            }
                        }
                        var ms = PEST_HARMCLASSCls.ImportData(model);
                        var flag = System.Configuration.ConfigurationManager.AppSettings["IsInsertSDE"].ToString();//是否更新空间数据库火险等级
                        if (flag == "1")
                        {
                            var m = new PEST_HARMCLASS_Model();
                            m.Name = model.TOWNNAME.Trim();
                            m.DValue = model.HARMCLASS.Trim();
                            var mm = PEST_HARMCLASSCls.UpdateAceHarmClass(m);
                        }
                        bo = ms.Success;
                    }
                }
                ++row;
            }
            return bo;
        }
        #endregion
    }
}
