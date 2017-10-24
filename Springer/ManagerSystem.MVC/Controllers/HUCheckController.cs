using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using ManagerSystemClassLibrary;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using PublicClassLibrary;
using Newtonsoft.Json;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ManagerSystem.MVC.Controllers
{
    /// <summary>
    /// 护林员考核
    /// </summary>
    public class HUCheckController : BaseController
    {
        #region 未巡统计

        #region 导出Excel
        public FileResult OmitCheckCountExportExcel()
        {
            PatrolRouteStat_SW sw = new PatrolRouteStat_SW();
            sw.orgNo = Request.Params["BYORGNO"];
            sw.TopORGNO = Request.Params["BYORGNO"];
            sw.DateBegin = Request.Params["TIMEBegin"];
            sw.DateEnd = Request.Params["TIMEEnd"];
            var list = HUReportCls.getPatrolRouteStatModel(sw);
 
                NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
                sheet1.IsPrintGridlines = true; //打印时显示网格线
                sheet1.DisplayGridlines = true;//查看时显示网格线
                sheet1.SetColumnWidth(0, 30 * 256);
                sheet1.SetColumnWidth(1, 10 * 256);
                sheet1.SetColumnWidth(2, 10 * 256);
                sheet1.SetColumnWidth(3, 10 * 256);
                sheet1.SetColumnWidth(4, 10 * 256);
                IRow row = sheet1.CreateRow(0);
                row.CreateCell(0).SetCellValue("未巡统计表");
                row.GetCell(0).CellStyle = getCellStyleTitle(book);
                row = sheet1.CreateRow(1);
                row.CreateCell(0).SetCellValue("单位/姓名");
                row.CreateCell(1).SetCellValue("应巡");
                row.CreateCell(2).SetCellValue("已巡");
                row.CreateCell(3).SetCellValue("未巡");
                row.CreateCell(4).SetCellValue("完成率");
                row.GetCell(0).CellStyle = getCellStyleHead(book);
                row.GetCell(1).CellStyle = getCellStyleHead(book);
                row.GetCell(2).CellStyle = getCellStyleHead(book);
                row.GetCell(3).CellStyle = getCellStyleHead(book);
                row.GetCell(4).CellStyle = getCellStyleHead(book);
                sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 4));
               
                int rowI = 0;
                foreach (var v in list)
                {
                    row = sheet1.CreateRow(rowI + 2);

                    row.CreateCell(0).SetCellValue(v.ORGName);
                    row.CreateCell(1).SetCellValue(v.PointCount);
                    row.CreateCell(2).SetCellValue(v.PointCount0);
                    row.CreateCell(3).SetCellValue(v.PointCount1);
                    row.CreateCell(4).SetCellValue(v.PointCount2);
                    rowI++;
                }
                // 写入到客户端 
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                book.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                string fileName = "未巡统计表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                return File(ms, "application/vnd.ms-excel", fileName);
            
        }

        #endregion
         

        #region 未巡统计
        /// <summary>
        /// 未巡统计
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult OmitCheckCount()
        {
            pubViewBag("005002", "005002", "");
            if (ViewBag.isPageRight == false)
                return View();

            ViewBag.TIMEBegin = DateTime.Now.ToString("yyyy-MM-01");
            ViewBag.TIMEEnd = DateTime.Now.ToString("yyyy-MM-dd");

            //显示查询值 用户名
            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });

            return View();
        }
        #endregion
        /// <summary>
        /// 出勤查询Json
        /// </summary>
        /// <returns></returns>
        public ActionResult getOmitCheckCountJson()
        {

            PatrolRouteStat_SW sw = new PatrolRouteStat_SW();
            sw.orgNo = Request.Params["BYORGNO"];
            sw.TopORGNO = Request.Params["BYORGNO"];
            sw.DateBegin = Request.Params["TIMEBegin"];
            sw.DateEnd = Request.Params["TIMEEnd"];
            var list = HUReportCls.getPatrolRouteStatModel(sw);


            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>单位</th>");
            sb.AppendFormat("        <th>应巡总数</th>");
            sb.AppendFormat("        <th>已完成</th>");
            sb.AppendFormat("        <th>未完成</th>");
            sb.AppendFormat("        <th>完成率(%)</th>");
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                i++;
                string orgName = v.ORGName;
                string orgNo = v.ORGNo;
                if (i % 2 == 0)
                {
                    sb.AppendFormat("<tr>");
                }
                else
                {
                    sb.AppendFormat("<tr class='row1'>");
                }
             
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGName + "");//
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PointCount + "");//总
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PointCount0 + "");//完成
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PointCount1 + "");//未完成
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PointCount2 + "");//完成率
                sb.AppendFormat("</tr>");
            }

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }



        #endregion

        #region 护林员考勤统计
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public FileResult CheckInCountExportExcel()
        {
            HUCheckINCount_SW sw = new HUCheckINCount_SW();
            sw.ORGNO = Request.Params["BYORGNO"];
            sw.DateBegin = Request.Params["TIMEBegin"];
            sw.DateEnd = Request.Params["TIMEEnd"];
            var list = HUCheckCls.getCheckInModel(sw);

            DateTime dt1 = Convert.ToDateTime(sw.DateBegin);
            DateTime dt2 = Convert.ToDateTime(sw.DateEnd);
            int days = ClsStr.getDateDiff(sw.DateBegin, sw.DateEnd) + 1;//日期包含天数

            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 10 * 256);
            for (int i = 0; i < days; i++)
            {
                sheet1.SetColumnWidth(i + 2, 10 * 256);
            }
            sheet1.SetColumnWidth(days + 2, 20 * 256);
            sheet1.SetColumnWidth(days + 3, 20 * 256);
            sheet1.SetColumnWidth(days + 4, 20 * 256);
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue("考勤统计表");
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            row = sheet1.CreateRow(1);
            row.CreateCell(0).SetCellValue("单位/姓名");
            row.CreateCell(1).SetCellValue("考勤人数");
            row.CreateCell(2).SetCellValue("日期（日）");
            row.CreateCell(days + 2).SetCellValue("总出勤（天数）");
            row.CreateCell(days + 3).SetCellValue("已出勤（天数）");
            row.CreateCell(days + 4).SetCellValue("出勤率");
            row.GetCell(0).CellStyle = getCellStyleHead(book);
            row.GetCell(1).CellStyle = getCellStyleHead(book);
            row.GetCell(2).CellStyle = getCellStyleHead(book);
            row.GetCell(days + 2).CellStyle = getCellStyleHead(book);
            row.GetCell(days + 3).CellStyle = getCellStyleHead(book);
            row.GetCell(days + 4).CellStyle = getCellStyleHead(book);
            row = sheet1.CreateRow(2);
            for (int i = 0; i < days; i++)
            {
                DateTime tm = dt1.AddDays(i);
                row.CreateCell(i + 2).SetCellValue(tm.ToString("dd"));
                row.GetCell(i + 2).CellStyle = getCellStyleHead(book);
            }
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, days + 4));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, days + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 1, 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, days + 2 + 2, days + 2 + 2));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, days + 0 + 2, days + 0 + 2));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, days + 1 + 2, days + 1 + 2));
            int rowI = 0;
            foreach (var v in list)
            {
                row = sheet1.CreateRow(rowI + 3);

                row.CreateCell(0).SetCellValue(v.ORGName);
                row.CreateCell(1).SetCellValue(v.HUCount);
                string[] arr = v.DayCountList.Split(',');
                for (int i = 0; i < days; i++)
                {
                    row.CreateCell(i + 2).SetCellValue(arr[i]);
                }
                row.CreateCell(days + 2).SetCellValue(v.daysC);
                row.CreateCell(days + 3).SetCellValue(v.daysOK);
                row.CreateCell(days + 4).SetCellValue(v.daysPer);
                rowI++;
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = "考勤统计表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
        }
         /// <summary>
         /// 出勤查询Json
         /// </summary>
         /// <returns></returns>
        public ActionResult getHHUCheckINCountJson()
        {
            HUCheckINCount_SW sw = new HUCheckINCount_SW();
            sw.ORGNO = Request.Params["BYORGNO"];
            sw.DateBegin = Request.Params["TIMEBegin"];
            sw.DateEnd = Request.Params["TIMEEnd"];
            var list = HUCheckCls.getCheckInModel(sw);

            StringBuilder sb = new StringBuilder();
            DateTime dt1 = Convert.ToDateTime(sw.DateBegin);
            DateTime dt2 = Convert.ToDateTime(sw.DateEnd);
            int days = ClsStr.getDateDiff(sw.DateBegin, sw.DateEnd) + 1;//日期包含天数
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th rowspan=\"2\">单位<br>(姓名)</th>");
            sb.AppendFormat("        <th rowspan=\"2\">考勤<br>人数</th>");
            sb.AppendFormat("        <th colspan=\"{1}\">{0}</th>", "日期(日)", days.ToString());
            sb.AppendFormat("        <th rowspan=\"2\">总出勤<br>(天数)</th>");
            sb.AppendFormat("        <th rowspan=\"2\">已出勤<br>(天数)</th>");
            sb.AppendFormat("        <th rowspan=\"2\">出勤率</th>");
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("    <tr>");
            for (DateTime tm = dt1; tm <= dt2; tm = tm.AddDays(1))
            {
                sb.AppendFormat("        <th>{0}</th>", tm.ToString("dd"));
            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            //var list = HUCheckCls.getCheckInModel(new HUCheckINCount_SW { DateBegin = arr[1], DateEnd = arr[2], ORGNO = arr[0], HUNM = arr[3] });
            int j = 0;
            foreach (var v in list)
            {
                j++;
                string orgName = v.ORGName;
                string orgNo = v.ORGNo;
                if (j % 2 == 0)
                {
                    sb.AppendFormat("<tr>");
                }
                else
                {
                    sb.AppendFormat("<tr class='row1'>");
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.HUCount);//总
                string[] arr1 = v.DayCountList.Split(',');
                for (int i = 0; i < days; i++)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", arr1[i]);
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.daysC);//总
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.daysOK);//完成
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.daysPer);//完成率
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 考勤统计
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult HUCheckINCount()
        {
            pubViewBag("005001", "005001", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.TIMEEnd = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.TIMEBegin = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(),  TopORGNO = SystemCls.getCurUserOrgNo() });
            
            return View();
        }
        #endregion

        #region 怠工统计

        #region 导出Excel
        public FileResult SabotageCountExportExcel()
        {

            SabotageCount_SW sw = new SabotageCount_SW();
            sw.TopORGNO = Request.Params["BYORGNO"];
            sw.DateBegin = Request.Params["TIMEBegin"];
            sw.DateEnd = Request.Params["TIMEEnd"];
            var list = HUCheckCls.getSabotageCountModel(sw);

            DateTime dt1 = Convert.ToDateTime(sw.DateBegin);
            DateTime dt2 = Convert.ToDateTime(sw.DateEnd);
            int days = ClsStr.getDateDiff(sw.DateBegin, sw.DateEnd) + 1;//日期包含天数

            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 10 * 256);
            for (int i = 0; i < days; i++)
            {
                sheet1.SetColumnWidth(i + 2, 10 * 256);
            }
            sheet1.SetColumnWidth(days + 2, 20 * 256);
            sheet1.SetColumnWidth(days + 3, 20 * 256);
            sheet1.SetColumnWidth(days + 4, 20 * 256);
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue("怠工统计表");
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            row = sheet1.CreateRow(1);
            row.CreateCell(0).SetCellValue("单位/姓名");
            row.CreateCell(1).SetCellValue("总数");
            row.CreateCell(days + 2).SetCellValue("正常");
            row.CreateCell(days + 3).SetCellValue("怠工");
            row.CreateCell(days + 4).SetCellValue("完成率");
            row.GetCell(0).CellStyle = getCellStyleHead(book);
            row.GetCell(1).CellStyle = getCellStyleHead(book);
            row.GetCell(days + 2).CellStyle = getCellStyleHead(book);
            row.GetCell(days + 3).CellStyle = getCellStyleHead(book);
            row.GetCell(days + 4).CellStyle = getCellStyleHead(book);
            row = sheet1.CreateRow(2);
            for (int i = 0; i < days; i++)
            {
                DateTime tm = dt1.AddDays(i);
                row.CreateCell(i + 2).SetCellValue(tm.ToString("dd"));
                row.GetCell(i + 2).CellStyle = getCellStyleHead(book);
            }
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, days + 4));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, days + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 1, 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, days + 2 + 2, days + 2 + 2));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, days + 0 + 2, days + 0 + 2));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, days + 1 + 2, days + 1 + 2));
            int rowI = 0;
            foreach (var v in list)
            {
                row = sheet1.CreateRow(rowI + 3);

                row.CreateCell(0).SetCellValue(v.ORGName);
                row.CreateCell(1).SetCellValue(v.Count);
                string[] arr = v.DayCountList.Split(',');
                for (int i = 0; i < days; i++)
                {
                    row.CreateCell(i + 2).SetCellValue(arr[i]);
                }
                row.CreateCell(days + 2).SetCellValue(v.Count0);
                row.CreateCell(days + 3).SetCellValue(v.Count1);
                row.CreateCell(days + 4).SetCellValue(v.CountPer);
                rowI++;
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = "怠工统计表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
             
        }

        #endregion


        #region 怠工统计 SabotageCount()
        /// <summary>
        /// 怠工统计
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult SabotageCount()
        {
            pubViewBag("005003", "005003", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.TIMEBegin = DateTime.Now.ToString("yyyy-MM-01");
            ViewBag.TIMEEnd = DateTime.Now.ToString("yyyy-MM-dd");
            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });

            return View();
        }
        #endregion

        #region 怠工统计Json  getSabotageCountJson()
        /// <summary>
        /// 怠工统计Json
        /// </summary>
        /// <returns></returns>
        public ActionResult getSabotageCountJson()
        {

            SabotageCount_SW sw = new SabotageCount_SW();
            //sw.ORGNO = Request.Params["BYORGNO"];
            sw.TopORGNO = Request.Params["BYORGNO"];
            sw.DateBegin = Request.Params["TIMEBegin"];
            sw.DateEnd = Request.Params["TIMEEnd"];


            DateTime dt1 = Convert.ToDateTime(sw.DateBegin);
            DateTime dt2 = Convert.ToDateTime(sw.DateEnd);
            int days = ClsStr.getDateDiff(sw.DateBegin, sw.DateEnd) + 1;//日期包含天数
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th rowspan=\"2\">单位<br>(姓名)</th>");
            sb.AppendFormat("        <th rowspan=\"2\">总数</th>");
            sb.AppendFormat("        <th colspan=\"{1}\">{0}</th>", "日期(日)", days.ToString());
            sb.AppendFormat("        <th rowspan=\"2\">正常</th>");
            sb.AppendFormat("        <th rowspan=\"2\">怠工</th>");
            sb.AppendFormat("        <th rowspan=\"2\">完成率(%)</th>");
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("    <tr>");
            for (DateTime tm = dt1; tm <= dt2; tm = tm.AddDays(1))
            {
                sb.AppendFormat("        <th>{0}</th>", tm.ToString("dd"));
            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var list = HUCheckCls.getSabotageCountModel(sw);
            int j = 0;
            foreach (var v in list)
            {
                string orgName = v.ORGName;
                string orgNo = v.ORGNo;
                //bool isHU = false;
                //if (PublicCls.OrgIsZhen(sw.TopORGNO) == true && orgName.Contains("合计") == false)
                //{
                //    isHU = true;
                //}
                j++;
                if (j % 2 == 0)
                {
                    sb.AppendFormat("<tr>");
                }
                else
                {
                    sb.AppendFormat("<tr class='row1'>");
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGName + "");//
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.Count + "");//总
                string[] arr1 = v.DayCountList.Split(',');
                for (int i = 0; i < days; i++)
                {
                    //if (isHU == true)
                    //{
                        //arr1[i] = (arr1[i] == "0") == true ? "正常" : "";
                    //}
                    sb.AppendFormat("<td class=\"center\">{0}</td>", arr1[i]);
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.Count0 + "");//完成
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.Count1 + "");//未完成
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.CountPer + "");//完成率
                sb.AppendFormat("</tr>");
            }

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        #endregion
        #endregion

        #region 出围统计 导出Excel
        /// <summary>
        /// 导出 
        /// </summary>
        /// <returns></returns>
        public FileResult OutRaiLExportExcel()
        {
            OutRaiLCount_SW sw = new OutRaiLCount_SW();
            sw.TopORGNO = Request.Params["BYORGNO"];
            sw.DateBegin = Request.Params["TIMEBegin"];
            sw.DateEnd = Request.Params["TIMEEnd"];
            var list = HUCheckCls.getOutRaiLCountModel(sw);

            DateTime dt1 = Convert.ToDateTime(sw.DateBegin);
            DateTime dt2 = Convert.ToDateTime(sw.DateEnd);
            int days = ClsStr.getDateDiff(sw.DateBegin, sw.DateEnd) + 1;//日期包含天数

            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 10 * 256);
            for (int i = 0; i < days; i++)
            {
                sheet1.SetColumnWidth(i + 2, 10 * 256);
            }
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue("出围统计表");
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            row = sheet1.CreateRow(1);
            row.CreateCell(0).SetCellValue("单位/姓名");
            row.CreateCell(1).SetCellValue("出围合计");
            row.CreateCell(2).SetCellValue("日期（日）");
            row.GetCell(0).CellStyle = getCellStyleHead(book);
            row.GetCell(1).CellStyle = getCellStyleHead(book);
            row.GetCell(2).CellStyle = getCellStyleHead(book);
            row = sheet1.CreateRow(2);
            for (int i = 0; i < days; i++)
            {
                DateTime tm = dt1.AddDays(i);
                row.CreateCell(i + 2).SetCellValue(tm.ToString("dd"));
                row.GetCell(i + 2).CellStyle = getCellStyleHead(book);
            }
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, days + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, days -2));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 1, 1));
            int rowI = 0;
            foreach (var v in list)
            {
                row = sheet1.CreateRow(rowI + 3);

                row.CreateCell(0).SetCellValue(v.ORGName);
                row.CreateCell(1).SetCellValue(v.Count);
                string[] arr = v.DayCountList.Split(',');
                for (int i = 0; i < days; i++)
                {
                    row.CreateCell(i + 2).SetCellValue(arr[i]);
                }
                rowI++;
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = "出围统计表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
        }
        #endregion


        #region 出围统计
        /// <summary>
        /// 出围统计
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult OutRaiLCount()
        {
            pubViewBag("005004", "005004", "");
            if (ViewBag.isPageRight == false)
                return View();

            ViewBag.TIMEBegin = DateTime.Now.ToString("yyyy-MM-01");
            ViewBag.TIMEEnd = DateTime.Now.ToString("yyyy-MM-dd");

            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() }); ;
            return View();
        }
        #endregion

        #region 出围查询Json  getOutRaiLCountJson()
        /// <summary>
        /// 出围查询Json
        /// </summary>
        /// <returns></returns>
        public ActionResult getOutRaiLCountJson()
        {
            OutRaiLCount_SW sw = new OutRaiLCount_SW();
            sw.TopORGNO = Request.Params["BYORGNO"];
            sw.DateBegin = Request.Params["TIMEBegin"];
            sw.DateEnd = Request.Params["TIMEEnd"];
            var list = HUCheckCls.getOutRaiLCountModel(sw);

            StringBuilder sb = new StringBuilder();
            DateTime dt1 = Convert.ToDateTime(sw.DateBegin);
            DateTime dt2 = Convert.ToDateTime(sw.DateEnd);
            int days = ClsStr.getDateDiff(sw.DateBegin, sw.DateEnd) + 1;//日期包含天数
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th rowspan=\"2\">单位<br>(姓名)</th>");
            sb.AppendFormat("        <th rowspan=\"2\">出围合计</th>");
            sb.AppendFormat("        <th colspan=\"{1}\">{0}</th>", "日期(日)", days.ToString());
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("    <tr>");
            for (DateTime tm = dt1; tm <= dt2; tm = tm.AddDays(1))
            {
                sb.AppendFormat("        <th>{0}</th>", tm.ToString("dd"));
            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            //var list = HUCheckCls.getCheckInModel(new HUCheckINCount_SW { DateBegin = arr[1], DateEnd = arr[2], ORGNO = arr[0], HUNM = arr[3] });
            int j = 0;
            foreach (var v in list)
            {
                string orgName = v.ORGName;
                string orgNo = v.ORGNo;
                j++;
                if (j % 2 == 0)
                {
                    sb.AppendFormat("<tr>");
                }
                else
                {
                    sb.AppendFormat("<tr class='row1'>");
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.Count);//总
                string[] arr1 = v.DayCountList.Split(',');
                for (int i = 0; i < days; i++)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", arr1[i]);
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        #endregion
    }
}
