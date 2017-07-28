using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ManagerSystem.MVC.Controllers
{
    public class OfficeManagerController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 在线编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult OfficeEditIndex()
        {
            Page page = new Page();
            string controlOutput = string.Empty;
            PageOffice.PageOfficeCtrl pc = new PageOffice.PageOfficeCtrl();
            pc.SaveFilePage = "/OfficeManager/SaveDoc";//处理文件保存
            pc.ServerPage = "/pageoffice/server.aspx";//设置授权页面

            pc.WebOpen("/UploadFile/MBDoc/firereportmb_sc.doc", PageOffice.OpenModeType.docAdmin, "s-yayn");
            page.Controls.Add(pc);
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    Server.Execute(page, htw, false);
                    controlOutput = sb.ToString();
                }
            }
            ViewBag.EditorHtml = controlOutput;

            return View();

        }

        /// <summary>
        /// Aspx写法载入office模板
        /// </summary>
        /// <returns></returns>
        public ActionResult OfficeEditAspxIndex()
        {
            #region 火情报告模板自动填充内容
            PageOffice.WordWriter.WordDocument doc = new PageOffice.WordWriter.WordDocument();
            doc.OpenDataRegion("PO_Year").Value = DateTime.Now.Year.ToString();
            doc.OpenDataRegion("PO_Month").Value = DateTime.Now.Month.ToString();
            doc.OpenDataRegion("PO_Day").Value = DateTime.Now.Day.ToString();
            doc.OpenDataRegion("PO_Hour").Value = DateTime.Now.Hour.ToString();
            #endregion
            ViewData["doc"] = doc;
            return View();
        }

        /// <summary>
        /// 在线保存
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveDoc()
        {
            string filePath = System.Configuration.ConfigurationManager.AppSettings["FireReportPath"].ToString();
            string fullpath = Server.MapPath(filePath);
            if (!Directory.Exists(fullpath))
            {
                Directory.CreateDirectory(fullpath);
            }
            PageOffice.FileSaver fs = new PageOffice.FileSaver();
            filePath += "/" + "sc_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fs.FileExtName;
            fs.SaveToFile(Server.MapPath(filePath));
            fs.Close();
            return null;
            //return Content("<script> alert('已保存')</script>");
            //return Content("alert('文档已保存');", "application/x-javascript");
        }
    }
}
