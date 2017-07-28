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
    public class JCCameraController : Controller
    {
        //
        // GET: /JCCamera/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 图片展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowImageIndex()
        {
            var count = System.Configuration.ConfigurationManager.AppSettings["ImageCount"].ToString();
            if (string.IsNullOrEmpty(count))
            {
                count = "10";
            }
            var sw = new JC_INFRAREDCAMERA_PHOTO_SW();
            sw.TopCount = count;
            var imagelist = JC_INFRAREDCAMERACls.getListModelTopPhoto(sw);
            var imgurl = System.Configuration.ConfigurationManager.AppSettings["ImageUrl"].ToString();
            foreach (var item in imagelist)
            {
                item.filename = item.filename.Replace(imgurl, "");
            }
            ViewBag.imagelist = imagelist;
            return View();
        }

        /// <summary>
        /// ajax 获取图片
        /// </summary>
        /// <returns></returns>
        public JsonResult ShowAjaxImageIndex()
        {
            MessageListObject ms = null;
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<li style=\"transform-origin: center 256px 0px; transform: translate(0 px, 0 px) scale(0.4) translateZ(0px); opacity: 0.6; z-index: 8;\">");
            var count = System.Configuration.ConfigurationManager.AppSettings["ImageCount"].ToString();
            if (string.IsNullOrEmpty(count))
            {
                count = "10";
            }
            var sw = new JC_INFRAREDCAMERA_PHOTO_SW();
            sw.TopCount = count;
            var imagelist = JC_INFRAREDCAMERACls.getListModelTopPhoto(sw);
            var imgurl = System.Configuration.ConfigurationManager.AppSettings["ImageUrl"].ToString();
            foreach (var item in imagelist)
            {
                item.filename = item.filename.Replace(imgurl, "");
                //sb.Append("<img src=\"" + item.filename + "\"   class=\"sc-image\">");
                //sb.Append("<div class=\"sc-content\">");
                //sb.Append("<h2>设备: " + item.BasicInfoModel.INFRAREDCAMERANAME + "</h2>");
                //sb.Append("<p>所属单位：【" + item.BasicInfoModel.ORGNAME + "】  手机号码：【" + item.tpa + "】  拍摄地点：【" + item.BasicInfoModel.ADDRESS + "】  接收时间：【" + item.recvdatetime + "】 </p>");
                //sb.Append("</div>>");
                //sb.Append("</li>");

            }
            ms = new MessageListObject(true, imagelist);
            return Json(ms);
        }


        /// <summary>
        /// 图片展示1
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowImagePIndex()
        {
            var count = System.Configuration.ConfigurationManager.AppSettings["ImageCount"].ToString();
            if (string.IsNullOrEmpty(count))
            {
                count = "10";
            }
            var sw = new JC_INFRAREDCAMERA_PHOTO_SW();
            sw.TopCount = count;
            var imagelist = JC_INFRAREDCAMERACls.getListModelTopPhoto(sw);
            var imgurl = System.Configuration.ConfigurationManager.AppSettings["ImageUrl"].ToString();
            foreach (var item in imagelist)
            {
                item.filename = item.filename.Replace(imgurl, "");
            }
            ViewBag.imagelist = imagelist;
            return View();
        }

        /// <summary>
        /// 图片展示2
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowImagePTIndex()
        {
            return View();
        }
    }
}
