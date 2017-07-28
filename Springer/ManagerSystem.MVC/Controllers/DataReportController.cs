using ManagerSystemClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    /// <summary>
    /// 数据上报
    /// </summary>
    public class DataReportController : BaseController
    {
        //
        // GET: /DataReport/

        public ActionResult Index()
        {
            return View();
        }


        #region Ajax

        /// <summary>
        /// 检索获取数据上报数据
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult GetReportDataAjax()
        {
            MessageListObject ms = null;
            string starttime = Request.Params["starttime"];//开始时间
            string endtime = Request.Params["endtime"];//开始时间
            string type = Request.Params["type"];//类型
            string state = Request.Params["state"];//状态
            //if (string.IsNullOrEmpty(starttime) || string.IsNullOrEmpty(endtime) || string.IsNullOrEmpty(type))
            //{
            //    ms = new MessageListObject(false, null);
            //    return Json(ms);

            //}
            var sw = new T_IPSRPT_REPORT_SW();
            sw.DateBegin = starttime;
            sw.DateEnd = endtime;
            sw.SYSTYPEVALUE = type;
            sw.orgNo = SystemCls.getCurUserOrgNo();//当前单位
            if (state != "2")
            {
                sw.MANSTATE = state;
            }

            var list = T_IPSRPT_REPORTCls.getModelList(sw);
            if (list.Any())
            {
                ms = new MessageListObject(true, list);
            }
            else
            {
                ms = new MessageListObject(false, null);
            }
            return Json(ms);
        }

        /// <summary>
        /// 上报数据信息
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult GetReportDataInfoAjax()
        {
            MessageObject ms = null;
            string reportid = Request.Params["reportid"];
            if (string.IsNullOrEmpty(reportid))
            {
                ms = new MessageObject(false, null);
                return Json(ms);
            }
            string maptype = Request.Params["maptype"];
            var sw = new T_IPSRPT_REPORT_SW();
            sw.REPORTID = reportid;

            if (!string.IsNullOrEmpty(maptype))
            {
                sw.MapType = maptype;//地图类型
            }
            var list = T_IPSRPT_REPORTCls.getModelList(sw);
            if (list.Any())
            {
                var model = list.FirstOrDefault();
                ms = new MessageObject(true, model);
            }
            else
            {
                ms = new MessageObject(false, null);
            }

            return Json(ms);
        }


        /// <summary>
        /// 报存上报信息
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult SaveReportDataAjax()
        {
            Message ms = null;
            string reportid = Request.Params["reportid"];
            string address = Request.Params["address"];
            string describe = Request.Params["describe"];
            string result = Request.Params["result"];

            if (string.IsNullOrEmpty(reportid))
            {
                ms = new Message(false, "reportid上报id传参失败", "");
                return Json(ms);
            }
            var m = new T_IPSRPT_REPORT_Model();
            m.REPORTID = reportid;
            m.opMethod = "Man";
            m.MANUSERID = SystemCls.getUserID();
            m.ADDRESS = address.Trim();
            m.COLLECTNAME = describe.Trim();
            m.MANRESULT = result.Trim();
            ms = T_IPSRPT_REPORTCls.Manager(m);
            var jctype = System.Configuration.ConfigurationManager.AppSettings["ISJcFire"].ToString();//是否插入火情监测表
            if (jctype.Trim() == "1")//增加至监测火情表
            {
                var list = JC_FIRECls.GetListModel(new JC_FIRE_SW() { FIREFROMID = m.REPORTID });
                if (!list.Any())
                {
                    if (ms.Success)
                    {
                        //红外相机 = 1,
                        //卫星热点 = 2,
                        //人工报警 = 3,
                        //电子报警 = 4,
                        //护林员火情 = 5
                        var sw = new T_IPSRPT_REPORT_SW();
                        sw.REPORTID = reportid;
                        var record = T_IPSRPT_REPORTCls.getModel(sw);
                        if (record != null)
                        {
                            if (record.SYSTYPEVALUE == "1")//1 为火情
                            {
                                JC_FIRE_Model model = new JC_FIRE_Model();
                                model.opMethod = "Add";
                                model.FIREFROMID = m.REPORTID;
                                model.FIREFROM = "5";//护林员火情上报
                                model.FIRENAME = ClsSwitch.SwitTM(record.REPORTTIME) + "护林员报警火情";
                                model.FIRETIME = record.REPORTTIME;
                                var recordmodel = T_IPSRPT_REPORTCls.getDetailModelList(new T_IPSRPT_REPORT_SW() { REPORTID = record.REPORTID }).FirstOrDefault();//获取数据详细
                                if (recordmodel != null)
                                {
                                    model.JD = recordmodel.LONGITUDE;
                                    model.WD = recordmodel.LATITUDE;
                                }
                                model.BYORGNO = record.OrgNo;
                                model.ZQWZ = record.ADDRESS;
                                model.RECEIVETIME = record.MANTIME;
                                var mm = JC_FIRECls.Manager(model);
                            }
                        }
                    }
                }
            }
            return Json(ms);
        }


        /// <summary>
        /// 获取检索上报数据Html
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult GetReportDataListAjax()
        {
            Message ms = null;
            string typename = "";
            string person = Request.Params["person"];
            string starttime = Request.Params["starttime"];
            string endtime = Request.Params["endtime"];
            string state = Request.Params["state"];
            string tid = Request.Params["tid"];

            string type = Request.Params["type"];

            var dicsw = new T_SYS_DICTSW();
            dicsw.DICTTYPEID = "5";
            dicsw.DICTVALUE = tid;
            var model = T_SYS_DICTCls.getModel(dicsw);
            typename = model.DICTNAME;

            StringBuilder sb = new StringBuilder();
            if (type == "Skyline")
            {
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<thead>");
                sb.AppendFormat("  <tr> ");
                sb.AppendFormat("  <th>序号</th>"); ;
                sb.AppendFormat("  <th>上报人</th>");
                // sb.AppendFormat("  <th>电话号码</th>");
                sb.AppendFormat("  <th>上报时间</th>");
                sb.AppendFormat("  <th>操作</th>");
            }
            else
            {
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<thead>");
                sb.AppendFormat("  <tr> ");
                sb.AppendFormat("  <th>序号</th>");
                sb.AppendFormat("  <th>数据类型</th>");
                sb.AppendFormat("  <th>上报人</th>");
                sb.AppendFormat("  <th>电话号码</th>");
                sb.AppendFormat("  <th>上报时间</th>");
                sb.AppendFormat("  <th>状态</th>");
                sb.AppendFormat("  <th>操作</th>");
            }
            sb.AppendFormat("   </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");

            var sw = new T_IPSRPT_REPORT_SW();
            sw.DateBegin = starttime;
            sw.DateEnd = endtime;
            sw.SYSTYPEVALUE = tid;
            sw.UnionHUser = false;
            if (!string.IsNullOrEmpty(person))
            {
                sw.HUserName = person;
                sw.UnionHUser = true;
            }
            if (state != "2")
            {
                sw.MANSTATE = state;
            }
            var cuurorg = SystemCls.getCurUserOrgNo();
            if (!string.IsNullOrEmpty(cuurorg))
            {
                sw.orgNo = cuurorg;
            }
            var list = T_IPSRPT_REPORTCls.getModelList(sw);

            if (list.Any())
            {
                var url = System.Configuration.ConfigurationManager.AppSettings["SkylineUrl"].ToString();
                var personPopurl = url + @"/SkylineManger/PersonDetailIndex";
                var reportViewPopurl = url + @"/SkylineManger/DataReportDetailViewIndex";
                int i = 0;
                foreach (var item in list)
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>{0}</td>", ++i);
                    if (type == "Skyline")
                    {
                        sb.AppendFormat("<td><a href=\"javascript:void(0);\" title=\"人员信息\" onclick=\"PopUrlReport('" + personPopurl + "'," + item.HID + ")\">{0}</a></td>", item.HName);
                        //  sb.AppendFormat("<td>{0}</td>", item.PHONE);
                        sb.AppendFormat("<td title=\"{0}\">{1}</td>", item.REPORTTIME, Convert.ToDateTime(item.REPORTTIME).ToString("MM-dd HH:mm"));
                    }
                    else
                    {
                        sb.AppendFormat("<td>{0}</td>", typename);
                        sb.AppendFormat("<td>{0}</td>", item.HName);
                        sb.AppendFormat("<td>{0}</td>", item.PHONE);
                        sb.AppendFormat("<td>{0}</td>", item.REPORTTIME);
                        if (item.MANSTATE == "0")
                        {
                            sb.AppendFormat("<td><a class=\"label label-danger\">未处理</a></td>");
                        }
                        else
                        {
                            sb.AppendFormat("<td><a class=\"label label-success\">已处理</a></td>");
                        }
                    }
                    //class=\"icon-flag\" 
                    if (type == "Skyline")
                    {
                        //sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"getLocaReport(" + item.REPORTID + ")\">定位</a>"
                        //   + "&nbsp;<a href=\"javascript:void(0);\" onClick=\"getReportView('" + reportViewPopurl + "'," + item.REPORTID + ")\">查看</a>&nbsp;<a href=\"javascript:void(0);\">对讲</a></td>");
                        sb.AppendFormat("<td  class=\"center\"> <a href=\"javascript:void(0);\" onClick=\"getLocaReport(" + item.REPORTID + ")\" class=\"dw option\"></a><a href=\"javascript:void(0);\" onClick=\"getReportView('" + reportViewPopurl + "'," + item.REPORTID + ")\" class=\"ck option\"></a><a href=\"javascript:void(0);\" class=\"dj option\"></a></td>");
                    }
                    else
                    {
                        sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"getLocaReport(" + item.REPORTID + ")\">定位</a></td>");
                    }
                    sb.AppendFormat("</tr>");
                }
            }
            else
            {
                if (type == "Skyline")
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td colspan=\"4\">暂无信息</td>");
                    sb.AppendFormat("</tr>");
                }
                else
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td colspan=\"7\">暂无信息</td>");
                    sb.AppendFormat("</tr>");
                }
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");

            ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }

        /// <summary>
        /// 删除上报数据 
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult DeleteReportDataAjax()
        {
            Message ms = null;
            string reportid = Request.Params["reportid"];
            if (string.IsNullOrEmpty(reportid))
            {
                ms = new Message(false, "reportid参数传递错误", "");
                return Json(ms);
            }
            var m = new T_IPSRPT_REPORT_Model();
            m.opMethod = "Del";
            m.REPORTID = reportid;
            ms = T_IPSRPT_REPORTCls.Manager(m);
            return Json(ms);

        }
        #endregion
    }
}
