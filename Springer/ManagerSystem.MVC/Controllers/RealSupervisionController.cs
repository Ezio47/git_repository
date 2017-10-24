using ManagerSystem.MVC.HelpCom;
using ManagerSystem.MVC.Models;
using ManagerSystemClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    /// <summary>
    /// 巡查监控
    /// </summary>
    public class RealSupervisionController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 浮动窗口
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult FloatIndex()
        {
            return View();
        }

        /// <summary>
        /// 二维浮动窗口
        /// </summary>
        /// <returns></returns>
        public ActionResult Float2DIndex()
        {
            var reportMenuList = new List<MenuTypeModel>();//数据上报菜单
            var collectMenuList = new List<MenuTypeModel>();//数据采集
            var jgMenuList = new List<MenuTypeModel>();//实时监管
            var hlyMenuList = new List<MenuTypeModel>();//护林员菜单
            var hlyList = T_SYS_MENUCls.getT_SYS_MENUModel(new T_SYS_MENU_SW { MENUCODE = "030", SYSFLAG = ConfigCls.getSystemFlag() }).FirstOrDefault();//二维护林员
            if (hlyList != null)
            {
                foreach (var item in hlyList.subMenuModel)
                {
                    if (item.MENUCODE.Substring(0, 4) == "0301")//实时监管
                    {
                        var jgmodel = new MenuTypeModel();
                        jgmodel.LICLASS = item.LICLASS;
                        jgmodel.MENUNAME = item.MENUNAME;
                        jgMenuList.Add(jgmodel);
                    }
                    else if (item.MENUCODE.Substring(0, 4) == "0302") //数据上报
                    {
                        var reportmodel = new MenuTypeModel();//数据上报
                        reportmodel.DICTTYPEID = "5";
                        reportmodel.LICLASS = item.LICLASS;
                        reportmodel.MENUNAME = item.MENUNAME;
                        reportmodel.DICTVALUE = item.MENUURL.Substring(item.MENUURL.Length - 1, 1);
                        reportMenuList.Add(reportmodel);
                    }
                    else if (item.MENUCODE.Substring(0, 4) == "0303") //数据采集
                    {
                        var collectmodel = new MenuTypeModel();
                        collectmodel.DICTTYPEID = "4";
                        collectmodel.LICLASS = item.LICLASS;
                        collectmodel.MENUNAME = item.MENUNAME;
                        collectmodel.DICTVALUE = item.MENUURL.Substring(item.MENUURL.Length - 1, 1);
                        collectMenuList.Add(collectmodel);
                    }
                    else if (item.MENUCODE.Substring(0, 4) == "0300") //护林员
                    { 
                        var hlymodel = new MenuTypeModel();
                        hlymodel.LICLASS = item.LICLASS;
                        hlymodel.MENUNAME = item.MENUNAME;
                        hlyMenuList.Add(hlymodel);
                    }
                }
            }
            ViewBag.NewcollectList = collectMenuList;//数据采集项目获取
            ViewBag.Newreportlist = reportMenuList;//数据上报项目获取
            ViewBag.Newjglist = jgMenuList;//实时监管
            ViewBag.Newhlylist = hlyMenuList;//护林员
            return View();
        }

        /// <summary>
        /// 浮动窗口
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult Float3DIndex()
        {
            return View();
        }

        /// <summary>
        /// 图层浮动
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult MapLayerFloatIndex()
        {
            return View();
        }

        #region Ajax事件

        #region 面板
        /// <summary>
        /// 获取实时定位数据Ajax
        /// </summary>
        /// <param name="orgname"></param>
        /// <returns>参见模型</returns>
        [HttpPost]
        public JsonResult GetRealAjax(string uidstr)
        {
            // string uidstr = Request.Params["uid"];//护林员ID
            string maptype = Request.Params["maptype"];
            //获取最新护林员当前坐标点
            var sw = new T_IPS_REALDATATEMPORARYSW();
            sw.USERID = uidstr;
            if (!string.IsNullOrEmpty(maptype))
            {
                sw.MapType = maptype;//地图类型
            }
            if (string.IsNullOrEmpty(uidstr))
            {
                sw.ORGNO = SystemCls.getCurUserOrgNo();
                if (sw.ORGNO.Trim().EndsWith("000") || sw.ORGNO.Trim().EndsWith("0000"))
                {
                    sw.ORGNO = "111111111";//市县级别不用首页定位
                    return Json(null);
                }
            }
            var model = T_IPS_REALDATATEMPORARYCls.getTopOneModelList(sw);//选取最新的一条记录
            return Json(model);
        }

        /// <summary>
        /// 获取护林员详细信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetIPSUserAjax(string userid)
        {
            T_IPSFR_USER_SW info = new T_IPSFR_USER_SW();
            Message msg = new Message(false, "", "");
            info.HID = userid;
            var model = T_IPSFR_USERCls.getModel(info);//获取护林员详细信息
            if (model != null)
            {
                string enablestr = model.ISENABLE == "1" ? "启用" : "禁用";
                string imageurl = "../Images/photo.png";
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(" <br />");
                sb.AppendFormat("<div class=\"divTable\" style=\"margin:10px 5px;\">");
                sb.AppendFormat("<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\"  align=\"center\" style=\"width:100%;height:200px\">");
                sb.AppendFormat("<tr  align=\"center\" height=\"30\">");
                sb.AppendFormat("<td>姓名：</td><td>{0}</td>", model.HNAME);
                sb.AppendFormat("<td>性别：</td><td>{0}</td>", model.SEXNAME);
                sb.AppendFormat("<td colspan=\"2\" rowspan=\"3\"><div id=\"photodiv\"  onclick=\"imgshow('photodiv')\"><img  id=\"photoimg\" src=\"{0}\" title=\"{1}\"照片  style=\"width:150px;height:100px\"></div></td>", imageurl, model.HNAME);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr  align=\"center\" height=\"30\">");
                sb.AppendFormat("<td>电话号码：</td><td>{0}</td>", model.PHONE);
                sb.AppendFormat("<td>出生日期：</td><td>{0}</td>", model.BIRTH);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr  align=\"center\" height=\"30\">");
                sb.AppendFormat("<td>设备号：</td><td>{0}</td>", model.SN);
                sb.AppendFormat("<td>固/兼职：</td><td>{0}</td>", model.ONSTATENAME);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr  align=\"center\" height=\"30\">");
                sb.AppendFormat("<td>是否启用：</td><td>{0}</td>", model.ISENABLENAME);
                sb.AppendFormat("<td>所属机构：</td><td colspan=\"3\">{0}</td>", model.ORGNAME);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("</table>");
                sb.AppendFormat("</div>");
                msg = new Message(true, sb.ToString(), "");
            }
            return Json(msg);
        }

        /// <summary>
        /// 获取实时轨迹中历史轨迹点
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public JsonResult GetRealDataAjax(string phone, string time)
        {
            string uid = Request.Params["uid"];//护林员id
            string maptype = Request.Params["maptype"];
            MessageListObject msg = null;
            var sw = new T_IPS_REALDATASW();
            sw.PHONE = phone.Trim();
            sw.searchDate = DateTime.Now.ToString("yyyy-MM-dd");
            sw.HID = uid;
            if (!string.IsNullOrEmpty(maptype))
            {
                sw.MapType = maptype;//地图类型
            }
            if (!string.IsNullOrEmpty(time))
            {
                sw.DateBegin = Convert.ToDateTime(time).AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                sw.DateEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            var list = T_IPS_REALDATACls.getModelList(sw);
            if (list.Count() > 0)
            {
                msg = new MessageListObject(true, list);
            }
            return Json(msg);
        }

        /// <summary>
        /// 历史轨迹列表Html
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public JsonResult GetHisGJAjax(string uid, string starttime, string endtime)
        {
            string maptype = Request.Params["maptype"];
            // 用户ID列表（sw.USERID逗号分隔）
            /// 开始日期（sw.DateBegin年月日）
            /// 结束日期（sw.DateEnd年月日）
            Message ms = null;
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(starttime) || string.IsNullOrEmpty(endtime))
            {
                ms = new Message(false, "参数错误!", "");
                return Json(ms);
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr>");
            sb.AppendFormat("<th> <i class=\"icon-time bigger-110 hidden-480\"></i>时间");
            sb.AppendFormat("</th>");
            sb.AppendFormat("<th>操作");
            sb.AppendFormat("</th>");
            sb.AppendFormat("</tr></thead>");
            sb.AppendFormat("<tbody>");
            var sw = new T_IPS_REALDATATEMPORARYSW();
            sw.USERID = uid;
            sw.DateBegin = starttime;
            sw.DateEnd = endtime;
            if (!string.IsNullOrEmpty(maptype))
            {
                sw.MapType = maptype;
            }
            var list = T_IPS_REALDATATEMPORARYCls.getModelList(sw);
            if (list.Count() > 0)
            {
                int i = 0;
                foreach (var item in list.OrderByDescending(e => e.SBDATE))
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>{0}</td>", item.SBDATE);
                    sb.AppendFormat("<td><a href='javascript:void(0)' onClick=\"hisgjPlay('" + item.USERID + "','" + item.SBDATE + "','" + i.ToString() + "')\">轨迹回放</a><p id=\"divplay_" + i.ToString() + "\" style=\"display:none;\"><a id=\"playst_" + i.ToString() + "\" onClick=\"pauseAndStart('" + i.ToString() + "')\">暂停</a>|<a onClick=\"drawOver()\">完成</a></p></td>");
                    sb.AppendFormat("</tr>");
                    ++i;
                }
            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan=\"2\">暂无历史轨迹</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }

        /// <summary>
        /// 获取历史经纬度(历史轨迹回放)
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public JsonResult GetHisLogLatAjax(string uid, string time)
        {
            MessageListObject ms = null;
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(time))
            {
                ms = new MessageListObject(false, null);
                return Json(ms);
            }
            string maptype = Request.Params["maptype"];//地图类型
            string starttime = Request.Params["starttime"];//开始时分
            string endtime = Request.Params["endtime"];//结束时分
            var model = new T_IPS_REALDATASW();
            model.HID = uid;
            //model.searchDate = time;
            if (!string.IsNullOrEmpty(starttime.Trim()) && !string.IsNullOrEmpty(endtime.Trim()))//时分参数
            {
                model.DateBegin = time + " " + starttime + ":00";
                model.DateEnd = time + " " + endtime + ":00";
                if (endtime.Trim() == "23:59")
                {
                    model.DateEnd = time + " " + endtime + ":59";
                }
            }
            if (!string.IsNullOrEmpty(maptype))
            {
                model.MapType = maptype;
            }
            var list = T_IPS_REALDATACls.getModelList(model).ToList();
            if (list.Any())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (i != 0)
                    {
                        bool bo = ISBetweenInDistance(list[i - 1].LONGITUDE, list[i - 1].LATITUDE, list[i].LONGITUDE, list[i].LATITUDE);//去除在参数设置范围内的经纬度
                        if (bo)
                        {
                            list.Remove(list[i]);
                            --i;
                        }
                    }
                }
                ms = new MessageListObject(true, list);
            }
            return Json(ms);
        }

        /// <summary>
        /// 实时点名Html
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public JsonResult GetRealCallAjax(string uid)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>单位</th>");
            sb.AppendFormat("<th>姓名</th>");
            sb.AppendFormat("<th>电话号码</th>");
            sb.AppendFormat("<th>最后上传时间</th>");
            sb.AppendFormat("<th>固/兼职</th>");
            //sb.AppendFormat("<th> <i class=\"icon-time bigger-110 hidden-480\"></i>点名时间</th>");
            sb.AppendFormat("<th>状态</th>");
            sb.AppendFormat("</tr></thead>");
            sb.AppendFormat("<tbody>");
            Message ms = null;
            int FRUserCount = 0;
            int FRUserOnLineCount = 0;
            if (string.IsNullOrEmpty(uid))
            {
                ms = new Message(false, "", "");
                return Json(ms);
            }
            var sw = new T_IPS_REALDATATEMPORARYSW();
            sw.USERID = uid;
            var list = T_IPS_REALDATATEMPORARYCls.getFROnLineByUID(sw, out FRUserCount, out FRUserOnLineCount);
            if (list.Any())
            {
                int i = 0;
                foreach (var item in list)
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>{0}</td>", ++i);
                    sb.AppendFormat("<td>{0}</td>", item.ORGNAME);
                    sb.AppendFormat("<td>{0}</td>", item.HNAME);
                    sb.AppendFormat("<td>{0}</td>", item.PHONE);
                    sb.AppendFormat("<td>{0}</td>", item.SBTIME);
                    sb.AppendFormat("<td>{0}</td>", item.ONSTATENAME);
                    if (item.isOnLine == "1")
                    {
                        sb.AppendFormat("<td><span class=\"label label-sm label-success\">在线</span></td>");
                    }
                    else
                    {
                        sb.AppendFormat("<td><span class=\"label label-sm label-grey\">离线</span></td>");
                    }
                    sb.AppendFormat("</tr>");
                }
            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan=\"7\">暂无点名信息</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string str = "(" + FRUserOnLineCount.ToString() + "/" + FRUserCount.ToString() + ")";
            ms = new Message(true, sb.ToString(), str);
            return Json(ms);
        }

        #endregion

        #region 电量

        /// <summary>
        /// 电量查询Html
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public JsonResult GetElectricAjax(string uid, string startTime, string endTime)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>单位</th>");
            sb.AppendFormat("<th>姓名</th>");
            sb.AppendFormat("<th>电话号码</th>");
            sb.AppendFormat("<th>时间</th>");
            sb.AppendFormat("<th>电量</th>");
            sb.AppendFormat("</tr></thead>");
            sb.AppendFormat("<tbody>");
            Message ms = null;
            if (string.IsNullOrEmpty(uid))
            {
                ms = new Message(false, "", "");
                return Json(ms);
            }
            var sw = new T_IPS_REALDATASW();
            sw.HID = uid;
            sw.DateBegin = startTime + " 0:00:00";
            sw.DateEnd = endTime + " 23:59:59";
            var list = T_IPS_REALDATACls.getElectricModelList(sw).Take(500);//获取电量列表
            if (list.Any())
            {
                if (list.Count() <= 500)
                {
                    int i = 0;
                    foreach (var item in list)
                    {
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td>{0}</td>", ++i);
                        sb.AppendFormat("<td>{0}</td>", item.ORGNAME);
                        sb.AppendFormat("<td>{0}</td>", item.HNAME);
                        sb.AppendFormat("<td>{0}</td>", item.PHONE);
                        sb.AppendFormat("<td>{0}</td>", item.SBTIME);
                        sb.AppendFormat("<td>{0}</td>", item.ELECTRIC);
                        sb.AppendFormat("</tr>");
                    }
                }
                else
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td colspan=\"6\">暂无电量信息</td>");
                    sb.AppendFormat("</tr>");
                    ms = new Message(false, sb.ToString(), "检索的数据量过大,请缩短时间间隔或减少查询护林员数量!");
                    return Json(ms);
                }
            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan=\"6\">暂无电量信息</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }

        #endregion

        #region 报警信息

        /// <summary>
        /// 报警信息(一键报警)
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAlarmListAjax()
        {
            string state = Request.Params["state"];//处理状态
            string starttime = Request.Params["starttime"];//开始时间
            string endtime = Request.Params["endtime"];//开始时间
            MessageListObject ms = null;
            var sw = new T_IPS_ALARM_SW();
            sw.orgNo = SystemCls.getCurUserOrgNo();
            sw.MANSTATE = state;
            sw.DateBegin = starttime;
            sw.DateEnd = endtime;
            sw.orgNo = SystemCls.getCurUserOrgNo();
            var list = T_IPS_ALARMCls.getModelList(sw);
            if (list.Any())
            {
                ms = new MessageListObject(true, list);
            }
            return Json(ms);
        }

        /// <summary>
        /// 检索结果报警定位
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLocaAlarmAjax()
        {
            string alarmid = Request.Params["alarmid"];
            MessageObject ms = null;
            if (string.IsNullOrEmpty(alarmid))
            {
                ms = new MessageObject(false, null);
                return Json(ms);
            }
            var sw = new T_IPS_ALARM_SW();
            sw.ALARMID = alarmid;
            var model = T_IPS_ALARMCls.getModel(sw);
            ms = new MessageObject(true, model);
            return Json(ms);
        }

        /// <summary>
        /// 获取一键报警信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAlarmInfoAjax()
        {
            MessageObject ms = null;
            string alarmid = Request.Params["alarmid"];
            var sw = new T_IPS_ALARM_SW();
            sw.ALARMID = alarmid;
            var model = T_IPS_ALARMCls.getModel(sw);
            if (model != null)
            {
                ms = new MessageObject(true, model);
            }
            else
            {
                ms = new MessageObject(false, null);
            }
            return Json(ms);
        }

        /// <summary>
        /// 保存更新报警信息
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveAlarmInfoAjax()
        {
            string alarmid = Request.Params["alarmid"];
            string bjcontent = Request.Params["bjcontent"];
            string tbresult = Request.Params["tbresult"];
            Message ms = null;
            if (string.IsNullOrEmpty(alarmid))
            {
                ms = new Message(false, "alarmid参数传递错误!", "");
                return Json(ms);
            }
            var m = new T_IPS_ALARM_Model();
            m.opMethod = "Man";
            m.ALARMID = alarmid;
            m.MANRESULT = tbresult;
            m.ALARMCONTENT = bjcontent;
            m.MANUSERID = SystemCls.getUserID();
            ms = T_IPS_ALARMCls.Manager(m);
            return Json(ms);
        }

        /// <summary>
        /// 检索报警信息列表Html
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAlarmAjax()
        {

            Message ms = null;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr> ");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>报警单位</th>");
            sb.AppendFormat("<th>报警人</th>");
            sb.AppendFormat("<th>电话号码</th>");
            sb.AppendFormat("<th>报警时间</th>");
            sb.AppendFormat("<th>状态</th>");
            sb.AppendFormat("<th>操作</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            string txtAlarmStartTime = Request.Params["txtAlarmStartTime"];//开始时间
            string txtAlarmEndTime = Request.Params["txtAlarmEndTime"];//结束时间
            string status = Request.Params["status"];//处理结果
            var sw = new T_IPS_ALARM_SW();
            sw.DateBegin = txtAlarmStartTime;
            sw.DateEnd = txtAlarmEndTime;
            sw.orgNo = SystemCls.getCurUserOrgNo();
            if (status != "2")//0 未处理1已处理 2全部
            {
                sw.MANSTATE = status;
            }
            var list = T_IPS_ALARMCls.getModelList(sw);
            if (list.Any())
            {
                int i = 0;
                foreach (var item in list)
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>{0}</td>", ++i);
                    sb.AppendFormat("<td>{0}</td>", item.OrgNoName);
                    sb.AppendFormat("<td>{0}</td>", item.HName);
                    sb.AppendFormat("<td>{0}</td>", item.PHONE);
                    sb.AppendFormat("<td>{0}</td>", item.ALARMTIME);
                    if (item.MANSTATE == "0")
                    {
                        sb.AppendFormat("<td><a class=\"label label-danger\">未处理</a></td>");
                    }
                    else
                    {
                        sb.AppendFormat("<td><a class=\"label label-success\">已处理</a></td>");
                    }
                    sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"getLocaAlarm(" + item.ALARMID + ")\">定位</a></td>");
                    sb.AppendFormat("</tr>");
                }
            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan=\"7\">暂无报警信息</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }

        /// <summary>
        /// 删除报警点
        /// </summary>
        /// <returns></returns>
        public JsonResult DelteAlarmAjax()
        {
            string alarmid = Request.Params["alarmid"];
            Message ms = null;
            if (string.IsNullOrEmpty(alarmid))
            {
                ms = new Message(false, "报警主键ALAEMID传参失败!", "");
                return Json(ms);
            }
            var m = new T_IPS_ALARM_Model();
            m.opMethod = "Del";
            m.ALARMID = alarmid;
            ms = T_IPS_ALARMCls.Manager(m);
            return Json(ms);
        }

        #endregion

        #region 热点

        /// <summary>
        /// 获取目前最新未处理热点
        /// </summary>
        /// <returns></returns>
        public JsonResult GetHotPontListAjax()
        {
            MessageListObject ms = null;
            string state = Request.Params["state"];
            var sw = new T_IPS_HOTS_SW();
            sw.MANSTATE = state;
            var list = T_IPS_HOTSCls.getModelList(sw);
            if (list.Any())
            {
                ms = new MessageListObject(true, list);
            }
            return Json(ms);
        }

        /// <summary>
        /// 获取热点信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetHotPoinInfoAJax()
        {
            MessageObject ms = null;
            string hotid = Request.Params["hotid"];
            if (string.IsNullOrEmpty(hotid))
            {
                ms = new MessageObject(false, null);
                return Json(ms);
            }
            var sw = new T_IPS_HOTS_SW();
            sw.HOTSID = hotid;
            var model = T_IPS_HOTSCls.getModel(sw);
            if (model != null)
            {
                ms = new MessageObject(true, model);
            }
            else
            {
                ms = new MessageObject(false, null);
            }
            return Json(ms);
        }

        /// <summary>
        /// 保存热点信息
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveHotPointInfoAjax()
        {
            Message ms = null;
            string hotid = Request.Params["hotid"];
            string hotresult = Request.Params["hotresult"];
            if (string.IsNullOrEmpty(hotid))
            {
                ms = new Message(false, "hotid参数传递错误!", "");
                return Json(ms);
            }
            var m = new T_IPS_HOTS_Model();
            m.opMethod = "Man";
            m.HOTSID = hotid;
            m.MANRESULT = hotresult;
            m.MANUSERID = SystemCls.getUserID();
            ms = T_IPS_HOTSCls.Manager(m);
            var jctype = System.Configuration.ConfigurationManager.AppSettings["ISJcFire"].ToString();
            if (jctype.Trim() == "1")//增加至监测火情表
            {
                var list = JC_FIRECls.GetListModel(new JC_FIRE_SW() { FIREFROMID = m.HOTSID });
                if (!list.Any())
                {
                    if (ms.Success)
                    {
                        //红外相机 = 1,
                        //卫星热点 = 2,
                        //人工报警 = 3,
                        //电子报警 = 4,
                        //护林员火情上报 = 5
                        var sw = new T_IPS_HOTS_SW();
                        sw.HOTSID = hotid;
                        var record = T_IPS_HOTSCls.getModel(sw);
                        if (record != null)
                        {
                            JC_FIRE_Model model = new JC_FIRE_Model();
                            model.opMethod = "Add";
                            model.FIREFROMID = m.HOTSID;
                            model.FIREFROM = "2";//卫星热点
                            model.FIRENAME = record.WXBH + record.ZQWZ + ClsSwitch.SwitTM(record.FXSJ) + "卫星热点火情";
                            model.FIRETIME = record.FXSJ;
                            model.JD = record.JD;
                            model.WD = record.WD;
                            model.ZQWZ = record.ZQWZ;
                            model.WXBH = record.WXBH;
                            model.DQRDBH = record.DQRDBH;
                            model.RSMJ = record.XS;
                            model.DL = record.DL;
                            model.YY = record.YY;
                            model.JXHQSJ = record.JXHQSJ;
                            model.RECEIVETIME = record.SBSJ;
                            var mm = JC_FIRECls.Manager(model);
                        }
                    }
                }
            }
            return Json(ms);
        }

        /// <summary>
        /// 获取热点列表Html
        /// </summary>
        /// <returns></returns>
        public JsonResult GetHotPointAjax()
        {
            Message ms = null;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>编号</th>");
            sb.AppendFormat("<th>热点区域</th>");
            sb.AppendFormat("<th>接收时间</th>");
            sb.AppendFormat("<th>像素</th>");
            sb.AppendFormat("<th>烟云</th>");
            sb.AppendFormat("<th>连续</th>");
            sb.AppendFormat("<th>状态</th>");
            sb.AppendFormat("<th>操作</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            string txtHotStartTime = Request.Params["txtHotStartTime"];//开始时间
            string txtHotEndTime = Request.Params["txtHotEndTime"];//结束时间
            string hotstatus = Request.Params["hotstatus"];//处理结果
            var sw = new T_IPS_HOTS_SW();
            sw.DateBegin = txtHotStartTime;
            sw.DateEnd = txtHotEndTime;
            if (hotstatus != "2")//0 未处理1已处理 2全部
            {
                sw.MANSTATE = hotstatus;
            }
            var list = T_IPS_HOTSCls.getModelList(sw);
            if (list.Any())
            {
                int i = 0;
                foreach (var item in list)
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>{0}</td>", ++i);
                    sb.AppendFormat("<td>{0}</td>", item.DQRDBH);
                    sb.AppendFormat("<td>{0}</td>", item.ZQWZ);
                    sb.AppendFormat("<td>{0}</td>", item.FXSJ);
                    sb.AppendFormat("<td>{0}</td>", item.XS);
                    sb.AppendFormat("<td>{0}</td>", item.YY);
                    sb.AppendFormat("<td>{0}</td>", (item.JXHQSJ == "无") ? "非连续" : "连续");
                    if (item.MANSTATE == "0")
                    {
                        sb.AppendFormat("<td><a class=\"label label-danger\">未处理</a></td>");
                    }
                    else
                    {
                        sb.AppendFormat("<td><a class=\"label label-success\">已处理</a></td>");
                    }
                    sb.AppendFormat("<td><a  href=\"javascript:void(0);\" onClick=\"getLocaHot(" + item.HOTSID + ")\">定位</a></td>");
                    sb.AppendFormat("</tr>");
                }
            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan=\"8\">暂无热点信息</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }

        /// <summary>
        /// 删除热点
        /// </summary>
        /// <returns></returns>
        public JsonResult DelteHotAjax()
        {
            Message ms = null;
            string hotid = Request.Params["hotid"];
            if (string.IsNullOrEmpty(hotid))
            {
                ms = new Message(false, "hotid热点id传参失败!", "");
                return Json(ms);
            }
            var m = new T_IPS_HOTS_Model();
            m.opMethod = "Del";
            m.HOTSID = hotid;
            ms = T_IPS_HOTSCls.Manager(m);
            return Json(ms);
        }

        /// <summary>
        /// 获取热点图片
        /// </summary>
        /// <param name="fjbh">文件编号（热点）</param>
        /// <returns></returns>
        public ActionResult GetPicture(string fjbh)
        {
            // fjbh = "61f4932ba5404cf3a30a8de646f08373";
            var info = T_RDXXZLCls.getModel(fjbh);
            if (info.FJ != null)
            {
                MemoryStream stream = new MemoryStream(info.FJ);
                System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                ImageResult result = new ImageResult(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                return result;
            }
            return View();
        }

        #endregion

        #endregion

        #region Private
        /// <summary>
        /// 是否在系统参数设定距离之间 true 为设定的范围之类 false 为设定范围之外
        /// </summary>
        /// <param name="lng1"></param>
        /// <param name="lat1"></param>
        /// <param name="lng2"></param>
        /// <param name="lat2"></param>
        /// <returns></returns>
        public bool ISBetweenInDistance(string lng1, string lat1, string lng2, string lat2)
        {
            bool bo = false;
            if (string.IsNullOrEmpty(lng1) || string.IsNullOrEmpty(lat1) || string.IsNullOrEmpty(lng2) || string.IsNullOrEmpty(lat2))
            {
                bo = false;
            }
            else
            {
                var paramodel = T_SYS_PARAMETERCls.getModel(new T_SYS_PARAMETER_SW { PARAMFLAG = "HisTraceDistiance", SYSFLAG = ConfigCls.getSystemFlag() });
                if (paramodel != null)
                {
                    var dis = MapComHelpr.DistanceOfTwoPoints(Convert.ToDouble(lng1), Convert.ToDouble(lat1), Convert.ToDouble(lng2), Convert.ToDouble(lat2), GaussSphere.WGS84);
                    var paradis = Convert.ToDouble(paramodel.PARAMVALUE);//系统参数读取的数值
                    if (dis < paradis)
                    {
                        bo = true;
                    }
                    else
                    {
                        bo = false;
                    }
                }
            }
            return bo;
        }
        #endregion
    }
}
