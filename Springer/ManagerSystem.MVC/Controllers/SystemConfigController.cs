﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagerSystemClassLibrary;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using PublicClassLibrary;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using PublicClassLibrary.ThirdDockService;
using log4net;
using Newtonsoft.Json.Linq;


namespace ManagerSystem.MVC.Controllers
{
    public class SystemConfigController : BaseController
    {
        private ILog logs = LogHelper.GetInstance();

        #region 行政区划管理
        /// <summary>
        /// 行政区划管理－增、删、改
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult AREAManager()
        {
            T_ALL_AREA_Model m = new T_ALL_AREA_Model();
            m.AREAID = Request.Params["AREAID"];
            m.AREACODE = Request.Params["AREACODE"];
            m.AREANAME = Request.Params["AREANAME"];
            m.AREAJC = Request.Params["AREAJC"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.opMethod = Request.Params["Method"];
            m.returnUrl = Request.Params["returnUrl"];

            if (string.IsNullOrEmpty(m.returnUrl))
                m.returnUrl = "/SystemConfig/AREAList";
            //默认为添加
            if (string.IsNullOrEmpty(m.opMethod))
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间,请重新输入!", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间,请重新输入!", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.AREACODE))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入或选择区划编码!", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.AREANAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入或选择区划名!", "")), "text/html;charset=UTF-8");
                if (m.AREACODE.Length != 15)
                    return Content(JsonConvert.SerializeObject(new Message(false, "区划编码错误,请重新输入!", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(T_ALL_AREACls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取地区Json字符串
        /// </summary>
        /// <returns></returns>
        public ActionResult getAREAJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            return Content(JsonConvert.SerializeObject(T_ALL_AREACls.getModel(new T_ALL_AREA_SW { AREAID = ID })), "text/html;charset=UTF-8");
        }

        ///<summary>
        ///行政区划列表
        /// </summary>
        ///<returns>参见模型</returns>
        public ActionResult AREAList()
        {
            pubViewBag("007001", "007001", "");
            if (ViewBag.isPageRight == false)
                return View();
            string ID = Request.Params["ID"];//当前页面传递编号
            if (string.IsNullOrEmpty(ID) == true)
                ID = "0";
            ViewBag.T_ID = ID;
            ViewBag.T_UrlReferrer = "/SystemConfig/AREAList?ID=" + ID;
            //导航条
            string navStr = "";
            if (ID != "0" && ID.Length == 15)
            {
                if (ID.Substring(0, 2) != "00")
                {
                    if (ID == ID.Substring(0, 2) + "0000000000000")
                        navStr += "<li class=\"active\">" + T_ALL_AREACls.getNameByID(new T_ALL_AREA_SW { AREACODE = ID.Substring(0, 2) + "0000000000000" }) + "</li>";
                    else
                        navStr += "<li class=\"active\"><a href=\"/SystemConfig/AREAList?ID=" + ID.Substring(0, 2) + "0000000000000" + "\">" + T_ALL_AREACls.getNameByID(new T_ALL_AREA_SW { AREACODE = ID.Substring(0, 2) + "0000000000000" }) + "</a></li>";
                }
                if (ID.Substring(2, 2) != "00")
                {
                    if (ID == ID.Substring(0, 4) + "00000000000")
                        navStr += "<li class=\"active\">" + T_ALL_AREACls.getNameByID(new T_ALL_AREA_SW { AREACODE = ID.Substring(0, 4) + "00000000000" }) + "</li>";
                    else
                        navStr += "<li class=\"active\"><a href=\"/SystemConfig/AREAList?ID=" + ID.Substring(0, 4) + "00000000000" + "\">" + T_ALL_AREACls.getNameByID(new T_ALL_AREA_SW { AREACODE = ID.Substring(0, 4) + "00000000000" }) + "</a></li>";
                }
                if (ID.Substring(4, 2) != "00")
                {
                    if (ID == ID.Substring(0, 6) + "000000000")
                        navStr += "<li class=\"active\">" + T_ALL_AREACls.getNameByID(new T_ALL_AREA_SW { AREACODE = ID.Substring(0, 6) + "000000000" }) + "</li>";
                    else
                        navStr += "<li class=\"active\"><a href=\"/SystemConfig/AREAList?ID=" + ID.Substring(0, 6) + "000000000" + "\">" + T_ALL_AREACls.getNameByID(new T_ALL_AREA_SW { AREACODE = ID.Substring(0, 6) + "000000000" }) + "</a></li>";
                }
                if (ID.Substring(6, 3) != "000")
                {
                    if (ID == ID.Substring(0, 9) + "000000")
                        navStr += "<li class=\"active\">" + T_ALL_AREACls.getNameByID(new T_ALL_AREA_SW { AREACODE = ID.Substring(0, 9) + "000000" }) + "</li>";
                    else
                        navStr += "<li class=\"active\"><a href=\"/SystemConfig/AREAList?ID=" + ID.Substring(0, 9) + "000000" + "\">" + T_ALL_AREACls.getNameByID(new T_ALL_AREA_SW { AREACODE = ID.Substring(0, 9) + "000000" }) + "</a></li>";
                }
            }
            ViewBag.navList = navStr;
            ViewBag.AREAList = getAreaStr(new T_ALL_AREA_SW { SubAREACODE = ID });
            return View();
        }

        private string getAreaStr(T_ALL_AREA_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>区划编码</th><th>区划名称</th><th>简称</th><th>经度</th><th>纬度</th><th></th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = T_ALL_AREACls.getListModel(sw);
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("<tr class=\"{1}\" onclick=\"showValue('{0}')\">", v.AREAID, (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.AREACODE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.AREANAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.AREAJC);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.JD);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.WD);
                sb.AppendFormat("<td class=\" \">");
                if (v.AREACODE.Substring(6, 9) == "000000000")//只有省/市/县才有下属区域
                {
                    sb.AppendFormat("<a href=\"/SystemConfig/AREAList?ID={0}\">下属区划</a>", v.AREACODE);
                }
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        #endregion

        #region 单位管理

        /// <summary>
        /// 组织机构增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult ORGManager()
        {
            T_SYS_ORGModel m = new T_SYS_ORGModel();
            m.ORGJC = Request.Params["ORGJC"];
            m.JD = Request.Params["JD"];
            m.COMMANDNAME = Request.Params["COMMANDNAME"];
            m.WD = Request.Params["WD"];
            m.WXJC = Request.Params["WXJC"];
            m.WEATHERJC = Request.Params["WEATHERJC"];
            m.POSTCODE = Request.Params["POSTCODE"];
            m.DUTYTELL = Request.Params["DUTYTELL"];
            m.FAX = Request.Params["FAX"];
            m.ORGNO = Request.Params["ORGNO"];
            m.ORGNAME = Request.Params["ORGNAME"];
            m.ORGDUTY = Request.Params["ORGDUTY"];
            m.LEADER = Request.Params["LEADER"];
            m.AREACODE = Request.Params["AREACODE"];
            m.SYSFLAG = ConfigCls.getSystemFlag();
            m.opMethod = Request.Params["Method"];
            m.returnUrl = Request.Params["returnUrl"];

            if (string.IsNullOrEmpty(m.returnUrl))
                m.returnUrl = "/SystemConfig/ORGList";
            if (string.IsNullOrEmpty(m.opMethod))
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间,请重新输入!", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间,请重新输入!", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.ORGNO))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入单位编码!", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.ORGNAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入单位名称!", "")), "text/html;charset=UTF-8");
                if (m.ORGNO.Length != 15)
                    return Content(JsonConvert.SerializeObject(new Message(false, "单位编码,请重新输入!", "")), "text/html;charset=UTF-8");
                if (m.AREACODE.Length != 15)
                    return Content(JsonConvert.SerializeObject(new Message(false, "所属行政区划码错误,请重新输入!", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(T_SYS_ORGCls.Manager(m)), "text/html;charset=UTF-8");
        }

        public ActionResult ORGMan()
        {
            pubViewBag("007002", "007002", "");
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
            //如果未传参数,默认为添加
            if (string.IsNullOrEmpty(ViewBag.T_Method))
                ViewBag.T_Method = "Add";
            //ViewBag.RoleChk = T_SYSSEC_ROLECls.getRoleAndUid(new T_SYSSEC_ROLE_SW { USERID = ViewBag.T_USERID });
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
            ViewBag.vdAREANAME = T_ALL_AREACls.getAREANAMESelectOption(new T_ALL_AREA_SW { });
            return View();
        }

        public ActionResult getORGJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            return Content(JsonConvert.SerializeObject(T_SYS_ORGCls.getModel(new T_SYS_ORGSW { ORGNO = ID })), "text/html;charset=UTF-8");
        }

        public ActionResult ORGList()
        {
            pubViewBag("007002", "007002", "");
            if (ViewBag.isPageRight == false)
                return View();
            //string trans = Request.Params["trans"];//传递网页参数           
            ////查询条件
            //string[] arr = new string[3];//存放查询条件的数组 根据实际存放的数据
            //if (string.IsNullOrEmpty(trans) == false)
            //    arr = ClsStr.DecryptA01(trans, "kkkkkkkk").Split('|');
            //if (string.IsNullOrEmpty(arr[0]) == true)
            //    arr[0] = PagerCls.getDefaultPageSize().ToString();//默认记录
            //if (string.IsNullOrEmpty(arr[2]) == true)
            //    arr[2] = SystemCls.getCurUserOrgNo();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });// ipsuM.ORGNAME });
            //ViewBag.ORGNAME = arr[1];//显示查询值 组织机构名
            //ViewBag.ORGNOList = getOrgStr(new T_SYS_ORGSW { TopORGNO = arr[2], ORGNAME = arr[1], SYSFLAG = ConfigCls.getSystemFlag() });
            return View();
        }

        public ActionResult getORGListAjax()
        {
            string ORGNO = Request.Params["ORGNO"];
            string ORGNAME = Request.Params["ORGNAME"];
            string AREACODE = Request.Params["AREACODE"];
            T_SYS_ORGSW sw = new T_SYS_ORGSW { TopORGNO = ORGNO, ORGNAME = ORGNAME, SYSFLAG = ConfigCls.getSystemFlag() };
            var result = T_SYS_ORGCls.getListModel(sw);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>行政区划</th><th>单位编码</th><th>单位名称</th><th>单位职责</th><th>负责人</th><th></th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                string orgName = v.ORGNAME;
                if (PublicCls.OrgIsShi(v.ORGNO))//统计市，即所有县的
                {
                }
                else if (PublicCls.OrgIsXian(v.ORGNO))//县
                {
                    orgName = "　　" + orgName;
                }
                else if (PublicCls.OrgIsZhen(v.ORGNO))
                {
                    orgName = "　　　　　　" + orgName;
                }
                else
                {
                    orgName = "　　　　　　　　" + orgName;
                }
                sb.AppendFormat("<tr class='{0}'>", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", ((sw.curPage - 1) * sw.pageSize + i + 1).ToString());
                string tNo = ClsStr.EncryptA01(v.ORGNO, "kdiekdfd");
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.AreaNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGNO);
                //sb.AppendFormat("<td class=\"left\"><a href=\"/SystemConfig/ORGMan?Method=See&ID={0}&tNo={1}\">{2}</a></td>", v.ORGNO, tNo, orgName);
                sb.AppendFormat("<td class=\"left\"><ahref='#' onclick=\"See('See','{0}','{1}')\">{2}</a></td>", v.ORGNO, tNo, orgName);
                //sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGJC);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGDUTY);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LEADER);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href='#' onclick=\"Mdy('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", v.ORGNO, tNo);
                sb.AppendFormat("&nbsp;<a href='#' onclick='Manager(\"{0}\")' title='删除'  class=\"searchBox_01 LinkDel\" >删除</a>", v.ORGNO);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), "")), "text/html;charset=UTF-8"); ;
        }

        /// <summary>
        /// 组合Table Html代码
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total"></param>
        /// <returns>参见模型</returns>
        private string getOrgStr(T_SYS_ORGSW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>行政区划</th><th>单位编码</th><th>单位名称</th><th>单位职责</th><th>负责人</th><th></th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = T_SYS_ORGCls.getListModel(sw);
            int i = 0;
            foreach (var v in result)
            {
                string orgName = v.ORGNAME;
                if (PublicCls.OrgIsShi(v.ORGNO))//统计市,即所有县的
                {
                }
                else if (PublicCls.OrgIsXian(v.ORGNO))//县
                {
                    orgName = "　　" + orgName;
                }
                else
                {
                    orgName = "　　　　" + orgName;
                }
                sb.AppendFormat("<tr class='{0}'>", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", ((sw.curPage - 1) * sw.pageSize + i + 1).ToString());
                string tNo = ClsStr.EncryptA01(v.ORGNO, "kdiekdfd");
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.AreaNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGNO);
                //sb.AppendFormat("<td class=\"left\"><a href=\"/SystemConfig/ORGMan?Method=See&ID={0}&tNo={1}\">{2}</a></td>", v.ORGNO, tNo, orgName);
                sb.AppendFormat("<td class=\"left\"><ahref='#' onclick=\"See('See','{0}','{1}')\">{2}</a></td>", v.ORGNO, tNo, orgName);
                //sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGJC);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGDUTY);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LEADER);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href='#' onclick=\"Mdy('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", v.ORGNO, tNo);
                sb.AppendFormat("&nbsp;<a href='#' onclick='Manager(\"{0}\")' title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.ORGNO);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        #endregion

        #region 参数管理
        /// <summary>
        /// 参数管理－修改
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult PARAMETERManager()
        {
            Message ms = null;
            T_SYS_PARAMETER_Model m = new T_SYS_PARAMETER_Model();
            string PARAMID = Request.Params["PARAMID"];
            string PARAMFLAG = Request.Params["PARAMFLAG"];
            // string PARANAME = Request.Params["PARANAME"];
            //string PARAMMARK = Request.Params["PARAMMARK"];
            string PARAMVALUE = Request.Params["PARAMVALUE"];
            string Method = Request.Params["Method"];
            if (string.IsNullOrEmpty(Method))
                Method = "Mdy";
            m.PARAMID = PARAMID;
            m.PARAMVALUE = PARAMVALUE;
            m.returnUrl = Request.Params["returnUrl"];
            if (string.IsNullOrEmpty(m.returnUrl))
                m.returnUrl = "/SystemConfig/PARAMETERlist";
            if (string.IsNullOrEmpty(m.PARAMVALUE))
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入参数值!", "")), "text/html;charset=UTF-8");
            m.opMETHOD = Method;
            ms = T_SYS_PARAMETERCls.Manager(m);
            if (ms.Success && Method == "Mdy")
            {
                var mobileNotice = ConfigCls.getConfigValue("mobileParameterService");//配置文件读取 1 为通知 0 不通知手机端
                if (mobileNotice == "1")
                {
                    if (!string.IsNullOrEmpty(PARAMFLAG))
                    {
                        try
                        {
                            new TaskUtil().NotifyRefreshData(PARAMFLAG, m.PARAMVALUE);//服务==》通知手机端修改参数
                        }
                        catch (Exception ex)
                        {
                            logs.Error("调用参数修改通知手机服务错误!", ex);
                        }
                    }
                }
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }

        public ActionResult getPARAMETERJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            return Content(JsonConvert.SerializeObject(T_SYS_PARAMETERCls.getModel(new T_SYS_PARAMETER_SW { PARAMID = ID })), "text/html;charset=UTF-8");
        }

        public ActionResult PARAMETERlist()
        {
            pubViewBag("007003", "007003", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.PARAMETERlist = getParamStr(new T_SYS_PARAMETER_SW { SYSFLAG = ConfigCls.getSystemFlag() });
            return View();
        }

        private string getParamStr(T_SYS_PARAMETER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>参数名称</th><th>KEY</th><th>参数值</th><th>说明</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = T_SYS_PARAMETERCls.getListModel(sw);
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("<tr class='{1}'\" onclick=\"showValue('{0}')\">", v.PARAMID, (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"left\" style=\"padding-left:10px;\">{0}</td>", v.PARAMNAME);
                sb.AppendFormat("<td class=\"left\" style=\"padding-left:10px;\">{0}</td>", v.PARAMFLAG);
                sb.AppendFormat("<td class=\"left\" style=\"padding-left:10px;\">{0}</td>", v.PARAMVALUE);
                sb.AppendFormat("<td class=\"left\" style=\"padding-left:10px;\">{0}</td>", v.PARAMMARK);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        #endregion

        #region 数据字典管理
        /// <summary>
        /// 字典管理
        /// </summary>
        /// <returns></returns>
        public ActionResult DICTManager()
        {
            T_SYS_DICTModel m = new T_SYS_DICTModel();
            string DICTID = Request.Params["DICTID"];
            string DICTTYPEID = Request.Params["DICTTYPEID"];
            string DICTFLAG = Request.Params["DICTFLAG"];
            string DICTNAME = Request.Params["DICTNAME"];
            string DICTVALUE = Request.Params["DICTVALUE"];
            string ORDERBY = Request.Params["ORDERBY"];
            string STANDBY1 = Request.Params["STANDBY1"];
            string STANDBY2 = Request.Params["STANDBY2"];
            string STANDBY3 = Request.Params["STANDBY3"];
            string STANDBY4 = Request.Params["STANDBY4"];
            string Method = Request.Params["Method"];
            if (string.IsNullOrEmpty(Method))
                Method = "Add";
            if (Method != "Del")
            {
                if (string.IsNullOrEmpty(DICTNAME))
                {
                    if (Method == "Add")
                        return Content(JsonConvert.SerializeObject(new Message(false, "请输入字典名!", "")), "text/html;charset=UTF-8");
                    if (Method == "Mdy")
                        return Content(JsonConvert.SerializeObject(new Message(false, "请选择字典名!", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(DICTVALUE))
                {
                    if (Method == "Add")
                        return Content(JsonConvert.SerializeObject(new Message(false, "请输入字典值!", "")), "text/html;charset=UTF-8");
                    if (Method == "Mdy")
                        return Content(JsonConvert.SerializeObject(new Message(false, "请选择字典值!", "")), "text/html;charset=UTF-8");
                }
            }
            m.DICTID = DICTID;
            m.DICTTYPEID = DICTTYPEID;
            //m.DICTFLAG = DICTFLAG;// ClsStr.DecryptA01(trans, "kkkkkkkk");
            m.DICTNAME = DICTNAME;
            //m.SYSFLAG = ConfigCls.getSystemFlag();
            m.DICTVALUE = DICTVALUE;
            m.ORDERBY = ORDERBY;
            m.STANDBY1 = STANDBY1;
            m.STANDBY2 = STANDBY2;
            m.STANDBY3 = STANDBY3;
            m.STANDBY4 = STANDBY4;
            //m.ISMAN = "1";
            m.opMethod = Method;
            Message mm = T_SYS_DICTCls.Manager(m);
            return Content(JsonConvert.SerializeObject(mm), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取字典单条记录信息
        /// </summary>
        /// <returns></returns>
        public ActionResult getDICTJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            return Content(JsonConvert.SerializeObject(T_SYS_DICTCls.getModel(new T_SYS_DICTSW { DICTID = ID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取类别记录信息
        /// </summary>
        /// <returns></returns>
        public ActionResult getDICTTypeJson()
        {
            string ID = Request.Params["ID"];
            return Content(JsonConvert.SerializeObject(T_SYS_DICTCls.getTypeModel(new T_SYS_DICTTYPE_SW { DICTTYPEID = ID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 字典值列表
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult getDICTListJson()
        {
            string trans = Request.Params["trans"];//传递网页参数
            return Content(JsonConvert.SerializeObject(new Message(true, getDictStr(new T_SYS_DICTSW { DICTTYPEID = trans }), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 字典值表格
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        private string getDictStr(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th><th>字典名称</th><th>字典值</th><th>排序</th><th>备用一</th><th>备用二</th><th>备用三</th><th>备用四</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = T_SYS_DICTCls.getListModel(sw);
            int i = 0;
            //bool blnIsMan = false;
            var mType = T_SYS_DICTCls.getTypeModel(new T_SYS_DICTTYPE_SW { DICTTYPEID = sw.DICTTYPEID });
            foreach (var v in result)
            {
                string rowClass = "";
                if (i % 2 != 0)
                    rowClass = " class='row1'";
                if (mType.ISMAN == "1")
                    sb.AppendFormat("<tr {1} onclick=\"showValue('{0}',1)\">", v.DICTID, rowClass);
                else
                    sb.AppendFormat("<tr {1} onclick=\"showValue('{0}',0)\">", v.DICTID, rowClass);
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.DICTNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.DICTVALUE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORDERBY);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.STANDBY1);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.STANDBY2);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.STANDBY3);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.STANDBY4);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// 数剧字典管理列表
        /// </summary>
        /// <returns></returns>
        public ActionResult DICTFLAGList()
        {
            pubViewBag("007004", "007004", "");
            if (ViewBag.isPageRight == false)
                return View();
            return View();
        }

        #region 取出树形菜单
        /// <summary>
        /// 取出树形菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult DictTreeGet()
        {
            T_SYS_DICTTYPE_SW sw = new T_SYS_DICTTYPE_SW();
            if (SystemCls.isRight("007004001") == false)
                sw.ISMAN = "1";
            string result = T_SYS_DICTCls.getTypeTree(sw);
            return Content(result, "application/json");
        }

        #endregion

        #endregion

        #region 机构参数管理
        public ActionResult HlyList()
        {
            pubViewBag("007006", "007006", "");
            if (ViewBag.isPageRight == false)
                return View();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>单位</th><th>呼救号码</th><th>回传频率</th><th>回传开始时间</th><th>回传结束时间</th><th>WEB Service地址</th><th>回传有效日期</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var list = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { });
            foreach (var item in list)
            {
                string orgName = item.ORGNAME;
                var str = item.MOBILEPARAMLIST;
                if (PublicCls.OrgIsShi(item.ORGNO))//统计市,即所有县的
                {
                    sb.AppendFormat("<tr class='danger'  onclick=\"showValue('{0}','{1}')\">", item.ORGNO, item.ORGNAME);
                }
                else if (PublicCls.OrgIsXian(item.ORGNO))//县
                {
                    sb.AppendFormat("<tr class='warning' onclick=\"showValue('{0}','{1}')\">", item.ORGNO, item.ORGNAME);
                    orgName = "&nbsp;&nbsp;&nbsp;&nbsp;" + orgName;
                }
                else
                {
                    sb.AppendFormat("<tr class='' onclick=\"showValue('{0}','{1}')\">", item.ORGNO, item.ORGNAME);
                    orgName = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + orgName;
                }
                if (str != "")
                {
                    sb.AppendFormat("<td class=\"left\">{0}</td>", orgName);
                    string[] arr = str.Split('$');
                    if (arr.Length > 1)
                    {
                        sb.AppendFormat("<td class=\"center\">{0}</td>", arr[0]);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", arr[1]);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", arr[2]);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", arr[3]);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", arr[4]);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", arr[5]);
                    }
                    else
                    {
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "");
                    }
                }
                else
                {
                    sb.AppendFormat("<td class=\"left\">{0}</td>", orgName + "(默认值)");
                    T_SYS_PARAMETER_SW sw = new T_SYS_PARAMETER_SW();
                    var result = T_SYS_PARAMETERCls.getListModel(sw);
                    var SOS_TEL = result.Where(p => p.PARAMFLAG == "SOS_TEL").FirstOrDefault().PARAMVALUE;
                    var FQCY = result.Where(p => p.PARAMFLAG == "FQCY").FirstOrDefault().PARAMVALUE;
                    var STATR_TIME = result.Where(p => p.PARAMFLAG == "STATR_TIME").FirstOrDefault().PARAMVALUE;
                    var END_TIME = result.Where(p => p.PARAMFLAG == "END_TIME").FirstOrDefault().PARAMVALUE;
                    var WEB_SERVICE_URL = result.Where(p => p.PARAMFLAG == "WEB_SERVICE_URL").FirstOrDefault().PARAMVALUE;
                    var TransEanbleDate = result.Where(p => p.PARAMFLAG == "TransEanbleDate").FirstOrDefault().PARAMVALUE;
                    sb.AppendFormat("<td class=\"center\">{0}</td>", SOS_TEL);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", FQCY);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", STATR_TIME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", END_TIME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", WEB_SERVICE_URL);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", TransEanbleDate);
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            ViewBag.pageList = sb.ToString();
            return View();
        }

        public ActionResult getHlyData()
        {
            var orgno = Request.Params["orgno"];
            var model = T_SYS_ORGCls.getModel(new T_SYS_ORGSW { ORGNO = orgno }).MOBILEPARAMLIST;
            if (model != "")
            {
                return Content(JsonConvert.SerializeObject(model), "text/html;charset=UTF-8");
            }
            else
            {
                T_SYS_PARAMETER_SW sw = new T_SYS_PARAMETER_SW();
                var result = T_SYS_PARAMETERCls.getListModel(sw);
                var SOS_TEL = result.Where(p => p.PARAMFLAG == "SOS_TEL").FirstOrDefault().PARAMVALUE;
                var FQCY = result.Where(p => p.PARAMFLAG == "FQCY").FirstOrDefault().PARAMVALUE;
                var STATR_TIME = result.Where(p => p.PARAMFLAG == "STATR_TIME").FirstOrDefault().PARAMVALUE;
                var END_TIME = result.Where(p => p.PARAMFLAG == "END_TIME").FirstOrDefault().PARAMVALUE;
                var WEB_SERVICE_URL = result.Where(p => p.PARAMFLAG == "WEB_SERVICE_URL").FirstOrDefault().PARAMVALUE;
                var TransEanbleDate = result.Where(p => p.PARAMFLAG == "TransEanbleDate").FirstOrDefault().PARAMVALUE;
                var ParameterModel = SOS_TEL + "$" + FQCY + "$" + STATR_TIME + "$" + END_TIME + "$" + WEB_SERVICE_URL + "$" + TransEanbleDate + "$$$$$$$$$$";
                return Content(JsonConvert.SerializeObject(ParameterModel), "text/html;charset=UTF-8");
            }
        }

        public ActionResult HlyUpdate()
        {
            T_SYS_ORGModel m = new T_SYS_ORGModel();
            T_SYS_PARAMETER_SW sw = new T_SYS_PARAMETER_SW();
            var orgno = Request.Params["orgno"];
            var Number = Request.Params["tbxNumber"];
            var Frequence = Request.Params["tbxFrequence"];
            var StartTime = Request.Params["tbxStartTime"];
            var EndTime = Request.Params["tbxEndTime"];
            var Addr = Request.Params["tbxAddr"];
            var Date = Request.Params["tbxDate"];
            var result = T_SYS_PARAMETERCls.getListModel(sw);
            var SOS_TEL = result.Where(p => p.PARAMFLAG == "SOS_TEL").FirstOrDefault().PARAMVALUE;
            var FQCY = result.Where(p => p.PARAMFLAG == "FQCY").FirstOrDefault().PARAMVALUE;
            var STATR_TIME = result.Where(p => p.PARAMFLAG == "STATR_TIME").FirstOrDefault().PARAMVALUE;
            var END_TIME = result.Where(p => p.PARAMFLAG == "END_TIME").FirstOrDefault().PARAMVALUE;
            var WEB_SERVICE_URL = result.Where(p => p.PARAMFLAG == "WEB_SERVICE_URL").FirstOrDefault().PARAMVALUE;
            var TransEanbleDate = result.Where(p => p.PARAMFLAG == "TransEanbleDate").FirstOrDefault().PARAMVALUE;
            if (Number == "" || Number == null)
            {
                Number = SOS_TEL;
            }
            if (Frequence == "" || Frequence == null)
            {
                Frequence = FQCY;
            }
            if (StartTime == "" || StartTime == null)
            {
                StartTime = STATR_TIME;
            }
            if (EndTime == "" || EndTime == null)
            {
                EndTime = END_TIME;
            }
            if (Addr == "" || Addr == null)
            {
                Addr = WEB_SERVICE_URL;
            }
            if (Date == "" || Date == null)
            {
                Date = TransEanbleDate;
            }
            m.ORGNO = orgno;
            m.MOBILEPARAMLIST = Number + "$" + Frequence + "$" + StartTime + "$" + EndTime + "$" + Addr + "$" + Date + "$$$$$$$$$$";
            Message ms = T_SYS_ORGCls.Update(m);
            var list = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { BYORGNO = orgno });
            var phone = "";
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.PHONE))
                {
                    phone += item.PHONE + ",";
                }
            }
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
                            logs.Error("调用参数修改通知手机服务错误!", ex);
                        }
                    }
                }
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }

        public ActionResult Cancel()
        {
            var orgno = Request.Params["orgno"];
            T_SYS_ORGModel m = new T_SYS_ORGModel();
            m.ORGNO = orgno;
            m.MOBILEPARAMLIST = "";
            Message ms = T_SYS_ORGCls.Update(m);
            var list = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { BYORGNO = orgno });
            var phone = "";
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.PHONE))
                {
                    phone += item.PHONE + ",";
                }
            }

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
                            logs.Error("调用参数修改通知手机服务错误!", ex);
                        }
                    }
                }
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        #endregion

        #region 系统更新管理
        /// <summary>
        /// 系统更新
        /// </summary>
        /// <returns></returns>
        public ActionResult SystemUpdate()
        {
            pubViewBag("007007", "007007", "");
            if (ViewBag.isPageRight == false)
                return View();
            return View();
        }

        /// <summary>
        /// 系统更新操作树
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateTree()
        {
            string result = GetUpdateTree();
            return Content(result, "application/json");
        }

        /// <summary>
        /// 获取系统更新操作树
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTree()
        {
            JArray JA = new JArray();
            JObject root1 = new JObject { { "id", "1" }, { "text", "数据库操作" }, { "state", "open" } };
            JObject root2 = new JObject { { "id", "2" }, { "text", "图层管理" }, { "state", "open" } };
            JObject root3 = new JObject { { "id", "3" }, { "text", "护林员巡检路线" }, { "state", "open" } };
            JA.Add(root1);
            JA.Add(root2);
            JA.Add(root3);
            return JsonConvert.SerializeObject(JA);
        }

        #region 更新数据库
        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateDataBase()
        {
            return View();
        }

        /// <summary>
        /// 执行sql语句更新数据库
        /// </summary>
        /// <returns></returns>
        public ActionResult DataBaseManager()
        {
            string sql = Request.Params["Sql"].Replace('\r', ' ');
            return Content(JsonConvert.SerializeObject(SYS_ConfigCls.UpdateDataBase(sql)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 图层管理
        /// <summary>
        /// 图层管理
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateLayer()
        {
            return View();
        }

        /// <summary>
        /// 获取图层列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLayerList()
        {
            string layerName = Request.Params["LayerName"];
            var list = T_SYS_LAYERCls.getListModel(new T_SYS_LAYER_SW { LAYERNAME = layerName });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            string dis = list.Count() <= 0 ? "disabled=\"disabled\"" : "";
            sb.AppendFormat("<th style=\"width:5%\"><input id=\"tbxLayerCodeALL\" name=\"tbxLayerCodeALL\" type=\"checkbox\" class=\"ace\" value=\"ALL\" onclick=\"selectAll(this.value,this.checked)\" {0} /></th>", dis);
            sb.AppendFormat("<th>序号</th><th>图层编号</th><th>图层名称</th><th>图层序号</th><th>默认是否打开</th><th>是否权限控制</th><th>对应权限编码</th><th>火情周边查询</th><th>护林员周边查询</th><th>排序号</th><th>操作</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            int i = 0;
            foreach (var v in list)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxLayerCode" + i + "\" name=\"tbxLayerCode\"  type=\"checkbox\" class=\"ace\" value=\"" + v.LAYERCODE + "\" onclick=\"selectAll(this.value,this.checked)\" />", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.LAYERCODE);
                sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", v.LAYERNAME, PublicCls.getLAYERNameClass(v.LAYERCODE));
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LAYERID);
                sb.AppendFormat("<td class=\"center\">{0}</td>", !string.IsNullOrEmpty(v.ISDEFAULTCH) ? (v.ISDEFAULTCH == "1" ? "是" : "否") : v.ISDEFAULTCH);
                sb.AppendFormat("<td class=\"center\">{0}</td>", !string.IsNullOrEmpty(v.ISACTION) ? (v.ISACTION == "1" ? "是" : "否") : v.ISDEFAULTCH);
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.LAYERRIGHTID);
                sb.AppendFormat("<td class=\"center\">{0}</td>", !string.IsNullOrEmpty(v.ISFIREROUNDDEFAULT) ? (v.ISFIREROUNDDEFAULT == "1" ? "是" : "否") : v.ISFIREROUNDDEFAULT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", !string.IsNullOrEmpty(v.ISFUROUNDDEFAULT) ? (v.ISFUROUNDDEFAULT == "1" ? "是" : "否") : v.ISFUROUNDDEFAULT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORDERBY);
                sb.AppendFormat("<td class\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}')\" title='修改' class=\"searchBox_01 LinkMdy\">修改</a>", v.LAYERCODE);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.LAYERCODE);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取单条图层数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLayerDataJson()
        {
            string layerCode = Request.Params["LayerCode"];
            return Content(JsonConvert.SerializeObject(T_SYS_LAYERCls.getModel(new T_SYS_LAYER_SW { LAYERCODE = layerCode })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 批量操作图层
        /// </summary>
        /// <returns></returns>
        public ActionResult PLUpdateLayer()
        {
            T_SYS_LAYER_Model m = new T_SYS_LAYER_Model();
            m.LAYERCODE = Request.Params["LayerCode"];
            m.PlCZ = Request.Params["PlCZ"];
            m.opMethod = "PLMdy";
            return Content(JsonConvert.SerializeObject(T_SYS_LAYERCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 图层管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult LayerManager()
        {
            T_SYS_LAYER_Model m = new T_SYS_LAYER_Model();
            m.LAYERCODE = Request.Params["LayerCode"];
            m.LAYERNAME = Request.Params["LayerName"];
            m.LAYERID = Request.Params["LayerId"];
            m.ISACTION = Request.Params["IsAction"];
            m.LAYERRIGHTID = Request.Params["LayerRightId"];
            m.ORDERBY = Request.Params["OrderBy"];
            m.ISDEFAULTCH = Request.Params["IsDefaultCh"];
            m.ISFIREROUNDDEFAULT = Request.Params["IsFireRoundDefault"];
            m.ISFUROUNDDEFAULT = Request.Params["IsFuRoundDefault"];
            m.LAYERPICNAME = Request.Params["LayerPicName"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(T_SYS_LAYERCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 护林员巡检路线
        /// <summary>
        /// 护林员巡检路线
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateHlyRoute()
        {
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }

        /// <summary>
        /// 获取护林员ID
        /// </summary>
        /// <returns></returns>
        public ActionResult GetHIdList()
        {
            string orgNo = Request.Params["ORGNO"];
            string type = Request.Params["Type"];
            string hidlist = "";
            var sfrUserList = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { BYORGNO = orgNo, PATROLLENGTH = type, });
            foreach (var r in sfrUserList)
            {
                var count = T_IPSFR_ROUTERAILCls.GetRouteCount(new T_IPSFR_ROUTERAIL_SW { HID = r.HID, ROADTYPE = "0" });
                if (count > 0)
                    hidlist += r.HID + ",";
            }
            if (hidlist.Length > 0)
                hidlist = hidlist.Substring(0, hidlist.Length - 1);
            return Content(JsonConvert.SerializeObject(new Message(true, hidlist, "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 更新护林员路线距离
        /// </summary>
        /// <returns></returns>
        public ActionResult HlyRouteManager()
        {
            T_IPSFR_USER_Model m = new T_IPSFR_USER_Model();
            m.HID = Request.Params["Hid"];
            m.PATROLLENGTH = Request.Params["PatRollLength"];
            m.opMethod = "PATROLLENGTH";
            return Content(JsonConvert.SerializeObject(T_IPSFR_USERCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion
        #endregion
    }
}
