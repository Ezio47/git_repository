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
    /// 统计分析
    /// </summary>
    public class HUReportController : BaseController
    {
        #region 护林员统计
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public FileResult HUCountExportExcel()
        {
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004001", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            sheet1.SetColumnWidth(0, 50 * 256);
            sheet1.SetColumnWidth(1, 10 * 256);
            sheet1.SetColumnWidth(2, 10 * 256);
            sheet1.SetColumnWidth(3, 10 * 256);
            sheet1.SetColumnWidth(4, 10 * 256);
            sheet1.SetColumnWidth(5, 10 * 256);
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            row = sheet1.CreateRow(1);
            row.CreateCell(0).SetCellValue("单位");
            row.CreateCell(1).SetCellValue("总数");
            row.CreateCell(2).SetCellValue("性别");
            row.CreateCell(4).SetCellValue("固/兼职");
            row.GetCell(0).CellStyle = getCellStyleHead(book);
            row.GetCell(1).CellStyle = getCellStyleHead(book);
            row.GetCell(2).CellStyle = getCellStyleHead(book);
            row.GetCell(4).CellStyle = getCellStyleHead(book);
            row = sheet1.CreateRow(2);
            row.CreateCell(2).SetCellValue("男");
            row.CreateCell(3).SetCellValue("女");
            row.CreateCell(4).SetCellValue("固职");
            row.CreateCell(5).SetCellValue("兼职");
            row.GetCell(2).CellStyle = getCellStyleHead(book);
            row.GetCell(3).CellStyle = getCellStyleHead(book);
            row.GetCell(4).CellStyle = getCellStyleHead(book);
            row.GetCell(5).CellStyle = getCellStyleHead(book);
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 5));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, 3));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 4, 5));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 1, 1));
            T_IPSFR_USER_SW sw = new T_IPSFR_USER_SW();
            sw.TopORGNO = Request.Params["TopORGNO"];
            var list = HUReportCls.getHUCountModel(sw);
            int rowI = 0;
            foreach (var item in list)
            {
                row = sheet1.CreateRow(rowI + 3);
                row.CreateCell(0).SetCellValue(item.ORGName);
                row.CreateCell(1).SetCellValue(item.HUCount);
                row.CreateCell(2).SetCellValue(item.Sex0Count);
                row.CreateCell(3).SetCellValue(item.Sex1Count);
                row.CreateCell(4).SetCellValue(item.Onstate0Count);
                row.CreateCell(5).SetCellValue(item.Onstate1Count);
                for (int i = 0; i < 6; i++)
                {
                    if (i == 0)
                        row.GetCell(0).CellStyle = getCellStyleLeft(book);
                    else
                        row.GetCell(i).CellStyle = getCellStyleCenter(book);
                }
                rowI++;
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = vMenu.MENUNAME + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
        }
        /// <summary>
        /// 护林员统计
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult HUCount()
        {
            pubViewBag("004001", "004001", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            
            //ViewBag.pageList = sb.ToString();
            return View();
        }
        /// <summary>
        /// 护林员统计Echarts
        /// </summary>
        /// <returns></returns>
        public ActionResult HUCountMan()
        {
            pubViewBag("004001", "004001", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }
        #region 返回查询表格Json
        /// <summary>
        /// 返回查询表格
        /// </summary>
        /// <returns></returns>
        public ActionResult getHUCountJson()
        {

            T_IPSFR_USER_SW sw = new T_IPSFR_USER_SW();
            sw.TopORGNO = Request.Params["TopORGNO"];

            var list = HUReportCls.getHUCountModel(sw);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th rowspan=\"2\">单位</th>");
            sb.AppendFormat("        <th rowspan=\"2\">总数</th>");
            sb.AppendFormat("        <th colspan=\"2\">性别</th>");
            sb.AppendFormat("        <th colspan=\"2\">固/兼职</th>");
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>男</th>");
            sb.AppendFormat("        <th>女</th>");
            sb.AppendFormat("        <th>固职</th>");
            sb.AppendFormat("        <th>兼职</th>");
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var item in list)
            {
                i++;
                if (i % 2 == 0)
                {
                    sb.AppendFormat("    <tr>");
                }
                else
                {
                    sb.AppendFormat("    <tr class='row1'>");
                }
               
                sb.AppendFormat("<td class=\"left\">{0}</td>", item.ORGName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.HUCount);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.Sex1Count);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.Sex0Count);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.Onstate0Count);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.Onstate1Count);
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        #endregion


        #endregion

        #region 巡检路线统计导出
        public FileResult PatrolRouteStatExportExcel()
        {
            //string BYORGNO = Request.Params["BYORGNO"];
            //string TIMEBegin = Request.Params["TIMEBegin"];
            //string TIMEEnd = Request.Params["TIMEEnd"];
            //string HNamePhone = Request.Params["HNamePhone"];
         
            //var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004002", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            
                #region 导出统计表
                NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                //添加一个sheet
                NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
                sheet1.IsPrintGridlines = true; //打印时显示网格线
                sheet1.DisplayGridlines = true;//查看时显示网格线
                sheet1.SetColumnWidth(0, 30 * 256);
                sheet1.SetColumnWidth(1, 10 * 256);
                sheet1.SetColumnWidth(2, 10 * 256);
                sheet1.SetColumnWidth(3, 10 * 256);
                sheet1.SetColumnWidth(4, 20 * 256);
                sheet1.SetColumnWidth(5, 10 * 256);
                sheet1.SetColumnWidth(6, 10 * 256);
                sheet1.SetColumnWidth(7, 10 * 256);
                sheet1.SetColumnWidth(8, 20 * 256);
                IRow row = sheet1.CreateRow(0);
                row.CreateCell(0).SetCellValue("巡检路线统计总表");
                row.GetCell(0).CellStyle = getCellStyleTitle(book);
                row = sheet1.CreateRow(1);
                row.CreateCell(0).SetCellValue("单位");
                row.CreateCell(1).SetCellValue("巡检次数(条)");
                row.CreateCell(5).SetCellValue("巡检地点(个)");
                row.GetCell(0).CellStyle = getCellStyleHead(book);
                row.GetCell(1).CellStyle = getCellStyleHead(book);
                row.GetCell(5).CellStyle = getCellStyleHead(book);
                row = sheet1.CreateRow(2);
                row.CreateCell(1).SetCellValue("总数");
                row.CreateCell(2).SetCellValue("巡检");
                row.CreateCell(3).SetCellValue("未巡检");
                row.CreateCell(4).SetCellValue("巡检率(%)");
                row.CreateCell(5).SetCellValue("总数");
                row.CreateCell(6).SetCellValue("完成");
                row.CreateCell(7).SetCellValue("未完成");
                row.CreateCell(8).SetCellValue("完成率(%)");
                row.GetCell(1).CellStyle = getCellStyleHead(book);
                row.GetCell(2).CellStyle = getCellStyleHead(book);
                row.GetCell(3).CellStyle = getCellStyleHead(book);
                row.GetCell(4).CellStyle = getCellStyleHead(book);
                row.GetCell(5).CellStyle = getCellStyleHead(book);
                row.GetCell(6).CellStyle = getCellStyleHead(book);
                row.GetCell(7).CellStyle = getCellStyleHead(book);
                row.GetCell(8).CellStyle = getCellStyleHead(book);
                sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 8));
                sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 1, 4));
                sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 5, 8));
                sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
                //var list = HUReportCls.getPatrolRouteStatModel(new PatrolRouteStat_SW { DateBegin = TIMEBegin, DateEnd = TIMEEnd, orgNo = BYORGNO, TopORGNO = BYORGNO });
                PatrolRouteStat_SW sw = new PatrolRouteStat_SW();
                sw.orgNo = Request.Params["BYORGNO"];
                sw.TopORGNO = Request.Params["BYORGNO"];
                sw.DateBegin = Request.Params["TIMEBegin"];
                sw.DateEnd = Request.Params["TIMEEnd"];
                var list = HUReportCls.getPatrolRouteStatModel(sw);
                int rowI = 0;
                
                foreach (var item in list)
                {
                    row = sheet1.CreateRow(rowI + 3);

                    row.CreateCell(0).SetCellValue(item.ORGName);
                    row.CreateCell(1).SetCellValue(item.LineCount);
                    row.CreateCell(2).SetCellValue(item.LineCount0);
                    row.CreateCell(3).SetCellValue(item.LineCount1);
                    row.CreateCell(4).SetCellValue(item.LineCount2);
                    row.CreateCell(5).SetCellValue(item.PointCount);
                    row.CreateCell(6).SetCellValue(item.PointCount0);
                    row.CreateCell(7).SetCellValue(item.PointCount1);
                    row.CreateCell(8).SetCellValue(item.PointCount2);
                    for (int i = 0; i < 9; i++)
                    {
                        if (i == 0)
                            row.GetCell(0).CellStyle = getCellStyleLeft(book);
                        else
                            row.GetCell(i).CellStyle = getCellStyleCenter(book);
                    }
                    rowI++;
                }
                // 写入到客户端 
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                book.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                string fileName = "巡检路线统计总表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                return File(ms, "application/vnd.ms-excel", fileName);
                #endregion
            
        }

        #endregion

        #region 巡检路线统计总表
       
        public ActionResult PatrolRouteStat()
        {
            pubViewBag("004002", "004002", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.TIMEBegin = DateTime.Now.ToString("yyyy-MM-01");
            ViewBag.TIMEEnd = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(),  TopORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }
        /// <summary>
        /// 巡检路线Echarts
        /// </summary>
        /// <returns></returns>
        public ActionResult PatrolRouteStatMan()
        {
            pubViewBag("004002", "004002", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.TIMEBegin = DateTime.Now.ToString("yyyy-MM-01");
            ViewBag.TIMEEnd = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }
        #endregion

        #region 巡检路线统计Ajax
        /// <summary>
        /// 巡检路线统计Ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult getRouteDetailJson()
        {
            PatrolRouteStat_SW sw = new PatrolRouteStat_SW();
            sw.orgNo = Request.Params["BYORGNO"];
            sw.TopORGNO = Request.Params["BYORGNO"];
            sw.DateBegin = Request.Params["TIMEBegin"];
            sw.DateEnd = Request.Params["TIMEEnd"];


            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th rowspan=\"2\">单位</th>");
            sb.AppendFormat("        <th colspan=\"4\">巡检次数(条)</th>");
            sb.AppendFormat("        <th colspan=\"4\">巡检地点(个)</th>");
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>总数</th>");
            sb.AppendFormat("        <th>巡检</th>");
            sb.AppendFormat("        <th>未巡检</th>");
            sb.AppendFormat("        <th>巡检率(%)</th>");
            sb.AppendFormat("        <th>总数</th>");
            sb.AppendFormat("        <th>完成</th>");
            sb.AppendFormat("        <th>未完成</th>");
            sb.AppendFormat("        <th>完成率(%)</th>");
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var list = HUReportCls.getPatrolRouteStatModel(sw);
            int i = 0;
            foreach (var v in list)
            {
                i++;
                string orgName = v.ORGName;
                string orgNo = v.ORGNo;
                if (i % 2 == 0)
                {
                    sb.AppendFormat("<tr >");
                }
                else
                {
                    sb.AppendFormat("<tr class='row1'>");
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGName + "");//
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LineCount + "");//总
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LineCount0 + "");//完成
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LineCount1 + "");//未完成
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LineCount2 + "");//完成率
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PointCount + "");//总
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PointCount0 + "");//完成
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PointCount1 + "");//未完成
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PointCount2 + "");//完成率
                sb.AppendFormat("</tr>");
            }

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        #endregion


        #region 上报统计导出Excel
        public FileResult ReportCountExportExcel()
        {
            //ViewBag.ExportExcelUrl = string.Format("/HUReport/ReportCountExportExcel?TIMEBegin={0}&TIMEEnd={1}&BYORGNO={2}&HID={3}&SYSTYPEVALUE={4}"
            string BYORGNO = Request.Params["BYORGNO"];
            string TIMEBegin = Request.Params["TIMEBegin"];
            string TIMEEnd = Request.Params["TIMEEnd"];
            string HID = Request.Params["HID"];
            string SYSTYPEVALUE = Request.Params["SYSTYPEVALUE"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004003", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);

            IEnumerable<T_IPSRPT_REPORT_TypeCountModel> typeModel;
            var list = T_IPSRPT_REPORTCls.getModelCount(new T_IPSRPT_REPORT_SW { TopORGNO = BYORGNO, DateBegin = TIMEBegin, DateEnd = TIMEEnd }, out typeModel);
            int typeCount = 0;//计算类别有多少列
            foreach (var v in typeModel)
            {
                typeCount++;
            }
            //设置宽度
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 20 * 256);
            for (int i = 0; i < typeCount; i++)
            {
                sheet1.SetColumnWidth(i + 2, 20 * 256);
            }
            row = sheet1.CreateRow(1);
            if (PublicCls.OrgIsZhen(BYORGNO) == false)
                row.CreateCell(0).SetCellValue("单位");
            else
                row.CreateCell(0).SetCellValue("姓名");
            row.CreateCell(1).SetCellValue("总数");
            int indexType = 2;//从第二列开始
            foreach (var v in typeModel)
            {
                row.CreateCell(indexType).SetCellValue(v.typeName);
                indexType++;
            }
            for (int i = 0; i < typeCount + 2; i++)
            {
                row.GetCell(i).CellStyle = getCellStyleHead(book);
            }
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, typeCount + 1));
            int rowI = 0;//数据行
            foreach (var v in list)//循环获取数据
            {
                row = sheet1.CreateRow(rowI + 2);
                if (string.IsNullOrEmpty(v.ORGName) == false)
                {
                    row.CreateCell(0).SetCellValue(v.ORGName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.HName);
                    row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                row.CreateCell(1).SetCellValue(v.ReportCount);
                row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 2;//类型开始列
                foreach (var vv in v.TypeCountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.typeCount);
                    row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                rowI++;
            }

            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = vMenu.MENUNAME + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
        }

        #endregion

        #region 上报统计
       

        /// <summary>
        /// 上报统计
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult ReportCount()
        {
            pubViewBag("004003", "004003", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.TIMEEnd = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.TIMEBegin = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });

            return View();
        }
        /// <summary>
        /// 上报统计
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportCountMan()
        {
            pubViewBag("004003", "004003", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.TIMEEnd = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.TIMEBegin = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });

            return View();
        }
        #region 护林员上报详情
        private string getReportHidDetailStr(T_IPSRPT_REPORT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table  cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>单位</th>");
            sb.AppendFormat("        <th>姓名</th>");
            sb.AppendFormat("        <th>电话</th>");
            sb.AppendFormat("        <th>上报时间</th>");
            sb.AppendFormat("        <th>处理状态</th>");
            sb.AppendFormat("        <th>描述</th>");
            sb.AppendFormat("        <th>地址</th>");
            sb.AppendFormat("    </tr>");
            var list = T_IPSRPT_REPORTCls.getModelList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.OrgNoName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.HName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PHONE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.REPORTTIME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", (v.MANSTATE == "0") ? "×" : "√");
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.COLLECTNAME);
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.ADDRESS);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        #endregion


        
        #region 返回查询表格Json
        /// <summary>
        /// 返回查询表格
        /// </summary>
        /// <returns></returns>
        public ActionResult getReportCountJson()
        {

            T_IPSRPT_REPORT_SW sw = new T_IPSRPT_REPORT_SW();
            sw.TopORGNO = Request.Params["TopORGNO"];
            sw.DateBegin = Request.Params["DateBegin"];
            sw.DateEnd = Request.Params["DateEnd"];

            IEnumerable<T_IPSRPT_REPORT_TypeCountModel> typeModel;//类别
            var list = T_IPSRPT_REPORTCls.getModelCount(sw, out typeModel);//查询结果

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>单位/姓名</th>");
            sb.AppendFormat("        <th>总数</th>");

            foreach (var v in typeModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.typeName);
            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                i++;
                if (i % 2 == 0)
                {
                    sb.AppendFormat("<tr>");
                }
                else
                {
                    sb.AppendFormat("<tr class='row1'>");
                }
                if (string.IsNullOrEmpty(v.ORGNo))
                {
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.HName, "");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.ORGName, "");
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ReportCount);//总
                var vvList = v.TypeCountModel;
                foreach (var vv in vvList)
                {
                        sb.AppendFormat("<td class=\"center\">{0}</td>", vv.typeCount);
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        #endregion

        #endregion

        #region 采集统计导出Excel
        public FileResult CollectCountExportExcel()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            string TIMEBegin = Request.Params["TIMEBegin"];
            string TIMEEnd = Request.Params["TIMEEnd"];
            string HID = Request.Params["HID"];
            string SYSTYPEVALUE = Request.Params["SYSTYPEVALUE"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004004", SYSFLAG = ConfigCls.getSystemFlag() });
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);

            IEnumerable<T_IPSCOL_COLLECT_TypeCountModel> typeModel;
            var list = T_IPSCOL_COLLECTCls.getModelCount(new T_IPSCOL_COLLECT_SW { TopORGNO = BYORGNO, DateBegin = TIMEBegin, DateEnd = TIMEEnd }, out typeModel);
            int typeCount = 0;//计算类别有多少列
            foreach (var v in typeModel)
            {
                typeCount++;
            }
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 20 * 256);
            for (int i = 0; i < typeCount; i++)
            {
                sheet1.SetColumnWidth(i + 2, 20 * 256);
            }
            row = sheet1.CreateRow(1);
            if (PublicCls.OrgIsZhen(BYORGNO) == false)
                row.CreateCell(0).SetCellValue("单位");
            else
                row.CreateCell(0).SetCellValue("姓名");
            row.CreateCell(1).SetCellValue("总数");
            int indexType = 2;//从第二列开始
            foreach (var v in typeModel)
            {
                row.CreateCell(indexType).SetCellValue(v.typeName);
                indexType++;
            }
            for (int i = 0; i < typeCount + 2; i++)
            {
                row.GetCell(i).CellStyle = getCellStyleHead(book);
            }
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, typeCount + 1));
            int rowI = 0;//数据行
            foreach (var v in list)//循环获取数据
            {
                row = sheet1.CreateRow(rowI + 2);
                if (string.IsNullOrEmpty(v.ORGName) == false)
                {
                    row.CreateCell(0).SetCellValue(v.ORGName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.HName);
                    row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                row.CreateCell(1).SetCellValue(v.CollectCount);
                row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 2;//类型开始列
                foreach (var vv in v.TypeCountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.typeCount);
                    row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                rowI++;
            }

            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = vMenu.MENUNAME + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
        }

        #endregion

        #region 采集统计
        /// <summary>
        /// 查询时，跳转页面
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult CollectCountquery()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            string TIMEBegin = Request.Params["TIMEBegin"];
            string TIMEEnd = Request.Params["TIMEEnd"];
            string HUNM = Request.Params["HUNM"];
            string str = ClsStr.EncryptA01(BYORGNO + "|" + TIMEBegin + "|" + TIMEEnd + "|||", "kkkkkkkk");
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/HUReport/CollectCount?trans=" + str)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 采集统计
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult CollectCount()
        {
            pubViewBag("004004", "004004", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.TIMEEnd = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.TIMEBegin = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(),  TopORGNO = SystemCls.getCurUserOrgNo() });

            return View();
        }
        /// <summary>
        /// 采集统计
        /// </summary>
        /// <returns></returns>
        public ActionResult CollectCountMan()
        {
            pubViewBag("004004", "004004", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.TIMEEnd = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.TIMEBegin = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });

            return View();
        }
        #region 明细
        private string getCollectHidDetailStr(T_IPSCOL_COLLECT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>单位</th>");
            sb.AppendFormat("        <th>姓名</th>");
            sb.AppendFormat("        <th>电话</th>");
            sb.AppendFormat("        <th>采集时间</th>");
            sb.AppendFormat("        <th>处理状态</th>");
            sb.AppendFormat("        <th>描述</th>");
            sb.AppendFormat("        <th>地址</th>");
            sb.AppendFormat("    </tr>");
            var list = T_IPSCOL_COLLECTCls.getModelList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.OrgNoName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.HName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.Phone);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.COLLECTTIME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", (v.MANSTATE == "0") ? "×" : "√");
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.COLLECTNAME);
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.ADDRESS);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        #endregion

        //private string getCollectCountStr(T_IPSCOL_COLLECT_SW sw)

        public ActionResult getCollectCountJson()
        {
            T_IPSCOL_COLLECT_SW sw = new T_IPSCOL_COLLECT_SW();
            sw.TopORGNO = Request.Params["TopORGNO"];
            sw.DateBegin = Request.Params["DateBegin"];
            sw.DateEnd = Request.Params["DateEnd"];
            IEnumerable<T_IPSCOL_COLLECT_TypeCountModel> typeModel;
            var list = T_IPSCOL_COLLECTCls.getModelCount(sw, out typeModel);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>单位/姓名</th>");
            sb.AppendFormat("        <th>总数</th>");
            foreach (var v in typeModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.typeName);
            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                i++;
                if (i % 2 == 0)
                {
                    sb.AppendFormat("<tr>");
                }
                else
                {
                    sb.AppendFormat("<tr class='row1'>");
                }
                
                if (string.IsNullOrEmpty(v.ORGNo))
                {
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.HName, "");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.ORGName, "");
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.CollectCount);//总
                var vvList = v.TypeCountModel;
                foreach (var vv in vvList)
                {
                    //if (string.IsNullOrEmpty(v.HID))
                        sb.AppendFormat("<td class=\"center\">{0}</td>", vv.typeCount);
                    //else
                    //{
                    //    if (vv.typeCount == "0")
                    //        sb.AppendFormat("<td class=\"center\">{0}</td>", vv.typeCount);
                    //    else
                    //        sb.AppendFormat("<td class=\"center\"><a href='/HUReport/CollectCount?trans={1}'>{0}</a></td>"
                    //            , vv.typeCount, ClsStr.EncryptA01(sw.TopORGNO + "|" + sw.DateBegin + "|" + sw.DateEnd + "|" + v.HID + "|" + vv.typeID, "kkkkkkkk"));
                    //}
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            //return sb.ToString();
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        #endregion
    }
}
