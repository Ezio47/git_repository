using ManagerSystemClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    public class EarlyMonitorController : BaseController
    {
        //
        // GET: /EarlyMonitor/

        /// <summary>
        /// 预警监测地图
        /// </summary>
        /// <returns></returns>
        public ActionResult JcIndex()
        {
            ViewBag.rights = T_SYSSEC_RIGHTCls.getRightStrByUID(new T_SYSSEC_IPSUSER_SW { USERID = SystemCls.getUserID() });
            string method = Request.Params["Method"];
            switch (method)
            {
                case "monitor":
                    ViewBag.loadFunc = "getJcMonitorLonLat()";//电子监控
                    ViewBag.loadtype = "1";
                    pubViewBag("008003", "008003", "电子监控");
                    break;
                default:
                    ViewBag.loadFunc = "getJcCameraLonLat()";//红外相机监测
                    ViewBag.loadtype = "0";
                    pubViewBag("008001", "008001", "红外相机");
                    break;
            }
            return View();
        }



        #region Ajax

        #region 红外相机

        /// <summary>
        /// 获取相机列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCamareaInfo()
        {
            MessageListObject msg = null;
            var sw = new JC_INFRAREDCAMERA_BASICINFO_SW();
            var list = JC_INFRAREDCAMERACls.getListModel(sw);
            if (list.Count() > 0)
            {
                msg = new MessageListObject(true, list);
            }
            return Json(msg);
        }

        /// <summary>
        /// 获取相机图片检索
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPhotoListHtmlAjax()
        {
            Message ms = null;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"sample-table-1\" class=\"table table-striped table-bordered table-hover\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("  <tr> ");
            sb.AppendFormat("  <th>序号</th>");
            sb.AppendFormat("  <th>设备名称</th>");
            sb.AppendFormat("  <th>所属组织</th>");
            sb.AppendFormat("  <th>设备号码</th>");
            sb.AppendFormat("  <th>接收时间</th>");
            sb.AppendFormat("  <th>图片</th>");
            sb.AppendFormat("  <th>状态</th>");
            sb.AppendFormat("  <th>操作</th>");
            sb.AppendFormat("   </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            string txtStartTime = Request.Params["txtStartTime"];//开始时间
            string txtEndTime = Request.Params["txtEndTime"];//结束时间
            string status = Request.Params["status"];//处理结果
            var sw = new JC_INFRAREDCAMERA_PHOTO_SW();
            sw.DateBegin = txtStartTime;
            sw.DateEnd = txtEndTime;
            if (status != "3")//0 未处理1已处理 2 已转为火情 3全部
            {
                sw.MANSTATE = status;
            }
            var list = JC_INFRAREDCAMERACls.getListModelPhoto(sw);
            if (list.Any())
            {
                var imgurl = System.Configuration.ConfigurationManager.AppSettings["ImageUrl"].ToString();
                int i = 0;
                foreach (var item in list)
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>{0}</td>", ++i);
                    sb.AppendFormat("<td>{0}</td>", item.BasicInfoModel.INFRAREDCAMERANAME);
                    sb.AppendFormat("<td>{0}</td>", item.BasicInfoModel.ORGNAME);
                    sb.AppendFormat("<td>{0}</td>", item.tpa);
                    sb.AppendFormat("<td>{0}</td>", item.recvdatetime);
                    sb.AppendFormat("<td><a href='{0}' target=_blank><image src='{0}' style='width:70px;height:70px' title='点击看大图'></a></td>", item.filename.Replace(imgurl, ""));
                    if (item.MANSTATE == "0")//0 未处理 1 为已处理 2 为已转为火情
                    {
                        sb.AppendFormat("<td><a class=\"label label-danger\">未处理</a></td>");
                        sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"removePhoto(" + item.smid + ")\">删除</a>&nbsp;&nbsp;&nbsp;<a href=\"javascript:void(0);\" onClick=\"convertFire('../JCFireInfo/FireHtmlIndex','1'," + item.smid + ")\">处理</a></td>");
                    }
                    else if (item.MANSTATE == "2")
                    {
                        sb.AppendFormat("<td><a class=\"label label-success\">已转为火情</a></td>");
                        sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"removePhoto(" + item.smid + ")\">删除</a></td>");
                    }
                    else
                    {
                        sb.AppendFormat("<td><a class=\"label label-success\">已处理</a></td>");
                        sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"removePhoto(" + item.smid + ")\">删除</a></td>");
                    }

                    //class=\"icon-flag\" 
                    sb.AppendFormat("</tr>");
                }
            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan=\"8\">暂无图片信息</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }


        /// <summary>
        /// 删除相机图片
        /// </summary>
        /// <returns></returns>
        public JsonResult DeletCameraPhoto()
        {
            string smid = Request.Params["smid"];//接收信息id
            var sw = new JC_INFRAREDCAMERA_PHOTO_Model();
            sw.smid = smid;
            sw.opMethod = "Del";
            var ms = JC_INFRAREDCAMERACls.ManagerPhoto(sw);
            return Json(ms);
        }

        #endregion

        #region 电子监控

        /// <summary>
        /// 电子监控设备信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMonitorInfo()
        {
            MessageListObject msg = null;
            var sw = new JC_MONITOR_BASICINFO_SW();
            var list = JC_MONITORCls.getListModel(sw);
            if (list.Count() > 0)
            {
                msg = new MessageListObject(true, list);
            }
            return Json(msg);
        }

        /// <summary>
        /// 电子监控火情监测信息
        /// </summary>
        /// <returns></returns>
        /// <summary>
        public JsonResult GetMonitorListHtmlAjax()
        {
            Message ms = null;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"sample-table-1\" class=\"table table-striped table-bordered table-hover\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("  <tr> ");
            sb.AppendFormat("  <th>序号</th>");
            sb.AppendFormat("  <th>塔台编码</th>");
            sb.AppendFormat("  <th>监控名称</th>");
            sb.AppendFormat("  <th>所属机构</th>");
            sb.AppendFormat("  <th>报警时间</th>");
            sb.AppendFormat("  <th>水平角</th>");
            sb.AppendFormat("  <th>俯仰角</th>");
            sb.AppendFormat("  <th>图片地址</th>");
            sb.AppendFormat("  <th>状态</th>");
            sb.AppendFormat("  <th>操作</th>");
            sb.AppendFormat("   </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            string txtStartTime = Request.Params["txtStartTime"];//开始时间
            string txtEndTime = Request.Params["txtEndTime"];//结束时间
            string status = Request.Params["status"];//处理结果
            var sw = new JC_MONITOR_SW();
            sw.DateBegin = txtStartTime;
            sw.DateEnd = txtEndTime;
            if (status != "3")//0 未处理1已处理 2 已转为火情 3全部
            {
                sw.MANSTATE = status;
            }
            var list = JC_MONITORCls.getListModelMonitor(sw);
            if (list.Any())
            {
                var imgurl = System.Configuration.ConfigurationManager.AppSettings["ImageUrl"].ToString();
                int i = 0;
                foreach (var item in list)
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>{0}</td>", ++i);
                    sb.AppendFormat("<td>{0}</td>", item.BasicInfoModel.TTBH);
                    sb.AppendFormat("<td>{0}</td>", item.BasicInfoModel.EMNAME);
                    sb.AppendFormat("<td>{0}</td>", item.BasicInfoModel.ORGNAME);
                    sb.AppendFormat("<td>{0}</td>", item.IMBTIME);
                    sb.AppendFormat("<td>{0}</td>", item.SPJ);
                    sb.AppendFormat("<td>{0}</td>", item.FYJ);
                    sb.AppendFormat("<td>{0}</td>", item.IMBIMGURL);
                    if (item.MANSTATE == "0")//0 未处理 1 为已处理 2 为已转为火情
                    {
                        sb.AppendFormat("<td><a class=\"label label-danger\">未处理</a></td>");
                        sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"removeMonitor(" + item.IMBID + ")\">删除</a>&nbsp;&nbsp;&nbsp;<a href=\"javascript:void(0);\" onClick=\"convertFire('../JCFireInfo/FireHtmlIndex','4'," + item.IMBID + ")\">处理</a></td>");
                    }
                    else if (item.MANSTATE == "2")
                    {
                        sb.AppendFormat("<td><a class=\"label label-success\">已转为火情</a></td>");
                        sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"removeMonitor(" + item.IMBID + ")\">删除</a></td>");
                    }
                    else
                    {
                        sb.AppendFormat("<td><a class=\"label label-success\">已处理</a></td>");
                        sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"removeMonitor(" + item.IMBID + ")\">删除</a></td>");
                    }

                    //class=\"icon-flag\" 
                    sb.AppendFormat("</tr>");
                }
            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan=\"10\">暂无电子监控信息</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }


        /// <summary>
        /// 删除监测
        /// </summary>
        /// <returns></returns>
        public JsonResult DeletJcMonitor()
        {
            string imid = Request.Params["imid"];//接收监测信息id
            var sw = new JC_MONITOR_Model();
            sw.IMBID = imid;
            sw.opMethod = "Del";
            var ms = JC_MONITORCls.ManagerMonitor(sw);
            return Json(ms);
        }
        #endregion

        #region 火险等级
        /// <summary>
        /// 获取火险等级
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFireLevelList()
        {
            MessageListObject msg = null;
            var sw = new YJ_DANGERCLASS_SW();
            var list = YJ_DANGERCLASSCls.getListModelTop(sw).OrderByDescending(p => Convert.ToInt32(p.DANGERCLASS));
            msg = new MessageListObject(true, list);
            return Json(msg);
        }
        #endregion

        #region 天气情况
        public JsonResult GetWeatherList()
        {
            MessageListObject msg = null;
            var sw = new YJ_WEATHER_SW();
            var list = YJ_WEATHERCls.getNewListModel(sw);
            msg = new MessageListObject(true, list);
            return Json(msg);
        }
        #endregion
        #endregion

    }
}
