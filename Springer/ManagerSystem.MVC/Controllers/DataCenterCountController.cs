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
    /// 数据中心及设施统计
    /// </summary>
    public class DataCenterCountController : BaseController
    {
        #region 队伍统计
        #region 队伍统计导出Excel
        public FileResult ARMYCountExportExcel()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            //string DC_ARMY_ID = Request.Params["DC_ARMY_ID"];
            //string ARMYTYPE = Request.Params["ARMYTYPE"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004005", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            if (string.IsNullOrEmpty(BYORGNO))
                BYORGNO = SystemCls.getCurUserOrgNo();
            IEnumerable<DC_ARMY_TypeCountModel> typeModel;
            var list = DataCenterCountCls.getModelCount(
                new DC_ARMY_SW
                {
                    TopORGNO = BYORGNO,
                }
                , out typeModel);
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
                row.CreateCell(0).SetCellValue("名称");
            row.CreateCell(1).SetCellValue("总数/人数");
            int indexType = 2;//从第二列开始
            foreach (var v in typeModel)
            {
                row.CreateCell(indexType).SetCellValue(v.DICTNAME + "/人数");
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
                    string orgName = v.ORGName;
                    if (PublicCls.OrgIsZhen(BYORGNO) == false)
                    {
                        if (PublicCls.OrgIsShi(v.ORGNo))//统计市，即所有县的
                        {
                        }
                        else if (PublicCls.OrgIsXian(v.ORGNo))//县
                        {
                            orgName = "　　" + orgName;
                        }
                        else
                        {
                            orgName = "　　　　" + orgName;
                        }
                    }
                    row.CreateCell(0).SetCellValue(orgName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.NAME);
                    //row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                row.CreateCell(1).SetCellValue(v.ARMYCount + "/" + v.MEMBERCount + "");
                //row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 2;//类型开始列
                foreach (var vv in v.TypeCountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.ARMYTYPECount + "/" + vv.MEMBERTYPECount + "");
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                rowI++;
            }
            //}
            //else//查询队伍详细信息
            //{
            //    if (BYORGNO.Substring(6, 3) == "xxx")
            //    {
            //        BYORGNO = BYORGNO.Substring(0, 6) + "000";
            //    }
            //    row = sheet1.CreateRow(1);
            //    string[] titleArr = new string[] { "单位", "名称", "编号", "队长", "人数", "队伍类型", "联系方式", "队伍特点" };
            //    for (int i = 0; i < titleArr.Length; i++)
            //    {

            //        row.CreateCell(i).SetCellValue(titleArr[i]);
            //        row.GetCell(i).CellStyle = getCellStyleHead(book);
            //        if (i == 0 || i == 1)
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //        else if (i == 2 || i == 3 || i == 4)
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //        else
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //    }
            //    sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, titleArr.Length - 1));
            //    var list = DC_ARMYCls.getModelList(new DC_ARMY_SW
            //    {
            //        BYORGNO = BYORGNO,
            //        DC_ARMY_ID = DC_ARMY_ID,//队伍ID
            //        ARMYTYPE = ARMYTYPE,//查询队伍类别
            //        ORGNOSXZ = "1",
            //    });

            //    int rowI = 0;//数据行
            //    foreach (var v in list)//循环获取数据
            //    {
            //        row = sheet1.CreateRow(rowI + 2);
            //        for (int i = 0; i < titleArr.Length; i++)
            //        {
            //            switch (i)
            //            {
            //                case 0:
            //                    row.CreateCell(i).SetCellValue(v.ORGName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 1:
            //                    row.CreateCell(i).SetCellValue(v.NAME);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 2:
            //                    row.CreateCell(i).SetCellValue(v.NUMBER);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 3:
            //                    row.CreateCell(i).SetCellValue(v.ARMYLEADER);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 4:
            //                    row.CreateCell(i).SetCellValue(v.ARMYMEMBERCOUNT);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 5:
            //                    row.CreateCell(i).SetCellValue(v.ARMYTYPEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 6:
            //                    row.CreateCell(i).SetCellValue(v.CONTACTS);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 7:
            //                    row.CreateCell(i).SetCellValue(v.ARMYCHARACTER);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //            }
            //        }
            //        rowI++;
            //    }
            //}

            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = vMenu.MENUNAME + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(ms, "application/vnd.ms-excel", fileName);

        }

        #endregion

        #region 队伍统计
        /// <summary>
        /// 队伍统计查询
        /// </summary>
        /// <returns></returns>
        //public ActionResult ArmyCountquery()
        //{
        //    string BYORGNO = Request.Params["BYORGNO"];
        //    string DC_ARMY_ID = Request.Params["DC_ARMY_ID"];
        //    string ARMYTYPE = Request.Params["ARMYTYPE"];
        //    string str = ClsStr.EncryptA01(BYORGNO + "|" + DC_ARMY_ID + "|" + ARMYTYPE, "kkkkkkkk");

        //    return Content(JsonConvert.SerializeObject(new Message(true, "", "/DataCenterCount/ArmyCount?trans=" + str)), "text/html;charset=UTF-8");
        //}
        /// <summary>
        /// 上报统计
        /// </summary>
        /// <returns></returns>
        public ActionResult ArmyCount()
        {
            pubViewBag("004005", "004005", "");
            if (ViewBag.isPageRight == false)
                return View();


            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });

            return View();
        }
        /// <summary>
        /// 队伍详细信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getARMYidDetailStr(DC_ARMY_SW sw)
        {

            string BYORGNO = Request.Params["BYORGNO"];
            sw.BYORGNO = BYORGNO;
            sw.ORGNOSXZ = "1";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>名称</th>");
            sb.AppendFormat("        <th>编号</th>");
            sb.AppendFormat("        <th>队长</th>");
            sb.AppendFormat("        <th>人数</th>");
            sb.AppendFormat("        <th>队伍类型</th>");
            sb.AppendFormat("        <th>联系方式</th>");
            sb.AppendFormat("        <th>队伍特点</th>");
            sb.AppendFormat("    </tr>");
            var list = DC_ARMYCls.getModelList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ARMYLEADER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ARMYMEMBERCOUNT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ARMYTYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.CONTACTS);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ARMYCHARACTER);
                sb.AppendFormat("</tr>");
                i++;

            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");

        }
        /// <summary>
        /// 队伍统计
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getARMYCountStr(DC_ARMY_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.TopORGNO = BYORGNO;
            StringBuilder sb = new StringBuilder();
            // sb.Append(" <div id=\"box\">");
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th >单位/名称</th>");
            sb.AppendFormat("        <th >总数/人数</th>");
            IEnumerable<DC_ARMY_TypeCountModel> typeModel;
            var list = DataCenterCountCls.getModelCount(sw, out typeModel);
            foreach (var v in typeModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.DICTNAME + "/人数");

            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                i++;
                string orgName = v.ORGName;
                string orgNo = v.ORGNo;
                if (string.IsNullOrEmpty(v.ORGNo))
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.NAME, "");
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        sb.AppendFormat("<tr class='{0}'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.ORGNo) == false && v.ORGNo == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.ORGNo))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.ORGNo, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<tr class='{0} row1'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.ORGNo) == false && v.ORGNo == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.ORGNo))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.ORGNo, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                }
                sb.AppendFormat("<td class=\"center\">{0}/{1}</td>", v.ARMYCount, v.MEMBERCount);//总
                var vvList = v.TypeCountModel;
                foreach (var vv in vvList)
                {
                    if (string.IsNullOrEmpty(v.DC_ARMY_ID))
                        sb.AppendFormat("<td class=\"center\">{0}/{1}</td>", vv.ARMYTYPECount, vv.MEMBERTYPECount);
                    else
                    {
                        if (vv.ARMYTYPECount == "0")
                            sb.AppendFormat("<td class=\"center\">{0}/{1}</td>", vv.ARMYTYPECount, vv.MEMBERTYPECount);
                        else
                            sb.AppendFormat("<td class=\"center\"><a href='/DataCenterCount/ArmyCount?trans={1}'>{0}</a></td>"
                                , vv.ARMYTYPECount
                                 , ClsStr.EncryptA01(sw.TopORGNO + "|" + v.DC_ARMY_ID + "|" + vv.DICTVALUE, "kkkkkkkk")
                                );
                    }
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            // sb.AppendFormat("</div>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        #endregion

        #region 队伍统计图标
        public ActionResult ArmyCountMan()
        {
            pubViewBag("004005", "004005", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.armytype = getarmytype(new T_SYS_DICTSW { DICTTYPEID = "26" });
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }
        private string getarmytype(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk1' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        #endregion
        #endregion

        #region 资源统计
        #region 资源统计导出Excel
        /// <summary>
        /// 资源统计导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult RESOURCECountExportExcel()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            //string DC_RESOURCE_NEW_ID = Request.Params["DC_RESOURCE_NEW_ID"];
            //string VALUE = Request.Params["VALUE"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004006", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);

            if (string.IsNullOrEmpty(BYORGNO))
                BYORGNO = SystemCls.getCurUserOrgNo();

            IEnumerable<RESOURCETYPECountModel> RESOURCETYPECountModel;
            IEnumerable<AGETYPECountModel> AGETYPECountModel;
            IEnumerable<ORIGINTYPECountModel> ORIGINTYPECountModel;
            IEnumerable<BURNTYPECountModel> BURNTYPECountModel;
            IEnumerable<TREETYPECountModel> TREETYPECountModel;
            var list = DataCenterCountCls.getRESOURCEModelCount(new DC_RESOURCE_NEW_SW
                {
                    TopORGNO = BYORGNO,
                }, out RESOURCETYPECountModel, out AGETYPECountModel, out ORIGINTYPECountModel, out BURNTYPECountModel, out TREETYPECountModel);

            int typeCount1 = 0;//计算类别有多少列
            int typeCount2 = 0;//计算类别有多少列
            int typeCount3 = 0;//计算类别有多少列
            int typeCount4 = 0;//计算类别有多少列
            int typeCount5 = 0;//计算类别有多少列
            foreach (var v in RESOURCETYPECountModel)
            {
                typeCount1++;
            }
            foreach (var v in AGETYPECountModel)
            {
                typeCount2++;
            }
            foreach (var v in ORIGINTYPECountModel)
            {
                typeCount3++;
            }
            foreach (var v in BURNTYPECountModel)
            {
                typeCount4++;
            }
            foreach (var v in TREETYPECountModel)
            {
                typeCount5++;
            }
            //设置宽度
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 20 * 256);
            for (int i = 0; i < typeCount1; i++)
            {
                sheet1.SetColumnWidth(i + 2, 20 * 256);
            }
            for (int i = 0; i < typeCount2; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1, 20 * 256);
            }
            for (int i = 0; i < typeCount3; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1 + typeCount2, 20 * 256);
            }
            for (int i = 0; i < typeCount4; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1 + typeCount2 + typeCount3, 20 * 256);
            }
            for (int i = 0; i < typeCount5; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1 + typeCount2 + typeCount3 + typeCount4, 20 * 256);
            }
            row = sheet1.CreateRow(1);
            if (PublicCls.OrgIsZhen(BYORGNO) == false)
                row.CreateCell(0).SetCellValue("单位");
            else
                row.CreateCell(0).SetCellValue("名称");
            row.CreateCell(1).SetCellValue("总数(个)/面积(公顷)");
            row.CreateCell(2).SetCellValue("资源类型");
            row.CreateCell(2 + typeCount1).SetCellValue("林龄类别");
            row.CreateCell(2 + typeCount1 + typeCount2).SetCellValue("起源类型");
            row.CreateCell(2 + typeCount1 + typeCount2 + typeCount3).SetCellValue("可燃类型");
            row.CreateCell(2 + typeCount1 + typeCount2 + typeCount3 + typeCount4).SetCellValue("林木类型");
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            row.GetCell(1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1 + typeCount2).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1 + typeCount2 + typeCount3).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1 + typeCount2 + typeCount3 + typeCount4).CellStyle = getCellStyleTitle(book);
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, typeCount1 + typeCount2 + typeCount3 + typeCount4 + typeCount5 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 1, 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, typeCount1 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2 + typeCount1, typeCount1 + 1 + typeCount2));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, typeCount1 + 2 + typeCount2, typeCount1 + 1 + typeCount2 + typeCount3));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, typeCount1 + 2 + typeCount2 + typeCount3, typeCount1 + 1 + typeCount2 + typeCount3 + typeCount4));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, typeCount1 + 2 + typeCount2 + typeCount3 + typeCount4, typeCount1 + 1 + typeCount2 + typeCount3 + typeCount4 + typeCount5));
            row = sheet1.CreateRow(2);
            int indexType = 2;//从第二列开始
            foreach (var v in RESOURCETYPECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.RESOURCETYPENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in AGETYPECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.AGETYPENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in ORIGINTYPECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.ORIGINTYPENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in BURNTYPECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.BURNTYPENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in TREETYPECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.TREETYPENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }

            int rowI = 1;//数据行
            foreach (var v in list)//循环获取数据
            {
                row = sheet1.CreateRow(rowI + 2);
                if (string.IsNullOrEmpty(v.ORGName) == false)
                {
                    string orgName = v.ORGName;
                    if (PublicCls.OrgIsZhen(BYORGNO) == false)
                    {
                        if (PublicCls.OrgIsShi(v.ORGNo))//统计市，即所有县的
                        {
                        }
                        else if (PublicCls.OrgIsXian(v.ORGNo))//县
                        {
                            orgName = "　　" + orgName;
                        }
                        else
                        {
                            orgName = "　　　　" + orgName;
                        }
                    }
                    row.CreateCell(0).SetCellValue(orgName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.NAME);
                    //row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                row.CreateCell(1).SetCellValue(v.RESOURCETYPECount + "/" + v.AREACount + "");
                //row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 2;//类型开始列
                foreach (var vv in v.RESOURCETYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.DICTRESOURCETYPECount + "/" + vv.AREATYPE1Count + "");
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.AGETYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.DICTAGETYPECount + "/" + vv.AREATYPE2Count + "");
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.ORIGINTYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.DICORIGINTYPECount + "/" + vv.AREATYPE3Count + "");
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.BURNTYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.DICTBURNTYPETYPECount + "/" + vv.AREATYPE4Count + "");
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.TREETYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.DICTTREETYPECount + "/" + vv.AREATYPE5Count + "");
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
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

        #region 资源统计
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        //public ActionResult RESOURCECountquery()
        //{
        //    string BYORGNO = Request.Params["BYORGNO"];
        //    string DC_RESOURCE_NEW_ID = Request.Params["DC_RESOURCE_NEW_ID"];
        //    string VALUE = Request.Params["VALUE"];
        //    string str = ClsStr.EncryptA01(BYORGNO + "|" + DC_RESOURCE_NEW_ID + "|" + VALUE, "kkkkkkkk");
        //    return Content(JsonConvert.SerializeObject(new Message(true, "", "/DataCenterCount/RESOURCECount?trans=" + str)), "text/html;charset=UTF-8");
        //}
        /// <summary>
        /// 资源统计页
        /// </summary>
        /// <returns></returns>
        public ActionResult RESOURCECount()
        {
            pubViewBag("004006", "004006", "");
            if (ViewBag.isPageRight == false)
                return View();
            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }
        /// <summary>
        /// 资源统计表格展示
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getRESOURCECountStr(DC_RESOURCE_NEW_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.TopORGNO = BYORGNO;
            StringBuilder sb = new StringBuilder();
            string a = DataCenterCountCls.getCount("28");//资源类型
            string b = DataCenterCountCls.getCount("27");//林龄类别
            string c = DataCenterCountCls.getCount("29");//起源类型
            string d = DataCenterCountCls.getCount("30");//可燃类型
            string e = DataCenterCountCls.getCount("31");//林木类型
            sb.AppendFormat("<table id=\"tb\" style=\"width:140%\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th rowspan=\"2\">单位/名称</th>");
            sb.AppendFormat("        <th rowspan=\"2\" >总数(个)/面积(公顷)</th>");
            sb.AppendFormat("        <th colspan=\"{0}\" >资源类型</th>", int.Parse(a));
            sb.AppendFormat("        <th colspan=\"{0}\" >林龄类别</th>", int.Parse(b));
            sb.AppendFormat("        <th colspan=\"{0}\" >起源类型</th>", int.Parse(c));
            sb.AppendFormat("        <th colspan=\"{0}\" >可燃类型</th>", int.Parse(d));
            sb.AppendFormat("        <th colspan=\"{0}\" >林木类型</th>", int.Parse(e));
            sb.AppendFormat("    </tr>");
            IEnumerable<RESOURCETYPECountModel> RESOURCETYPECountModel;
            IEnumerable<AGETYPECountModel> AGETYPECountModel;
            IEnumerable<ORIGINTYPECountModel> ORIGINTYPECountModel;
            IEnumerable<BURNTYPECountModel> BURNTYPECountModel;
            IEnumerable<TREETYPECountModel> TREETYPECountModel;
            var list = DataCenterCountCls.getRESOURCEModelCount(sw, out RESOURCETYPECountModel, out AGETYPECountModel, out ORIGINTYPECountModel, out BURNTYPECountModel, out TREETYPECountModel);
            sb.AppendFormat("    <tr>");
            foreach (var v in RESOURCETYPECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.RESOURCETYPENAME);

            }
            foreach (var v in AGETYPECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.AGETYPENAME);

            }
            foreach (var v in ORIGINTYPECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.ORIGINTYPENAME);

            }
            foreach (var v in BURNTYPECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.BURNTYPENAME);
            }
            foreach (var v in TREETYPECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.TREETYPENAME);
            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                i++;
                string orgName = v.ORGName;
                string orgNo = v.ORGNo;
                if (string.IsNullOrEmpty(v.ORGNo))
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.NAME, "");
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        sb.AppendFormat("<tr class='{0}'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.ORGNo) == false && v.ORGNo == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.ORGNo))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.ORGNo, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<tr class='{0} row1'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.ORGNo) == false && v.ORGNo == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.ORGNo))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.ORGNo, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    
                }

                string totol = "";
                totol = v.RESOURCETYPECount;
                sb.AppendFormat("<td class=\"center\">{0}/{1}</td>", totol, v.AREACount);//总数
                var vvList1 = v.RESOURCETYPECountModel;
                foreach (var vv in vvList1)
                {
                    sb.AppendFormat("<td class=\"center\">{0}/{1}</td>", vv.DICTRESOURCETYPECount, vv.AREATYPE1Count);
                }
                var vvList2 = v.AGETYPECountModel;
                foreach (var vv in vvList2)
                {
                    sb.AppendFormat("<td class=\"center\">{0}/{1}</td>", vv.DICTAGETYPECount, vv.AREATYPE2Count);
                }

                var vvList3 = v.ORIGINTYPECountModel;
                foreach (var vv in vvList3)
                {
                    sb.AppendFormat("<td class=\"center\">{0}/{1}</td>", vv.DICORIGINTYPECount, vv.AREATYPE3Count);
                }

                var vvList4 = v.BURNTYPECountModel;
                foreach (var vv in vvList4)
                {
                    sb.AppendFormat("<td class=\"center\">{0}/{1}</td>", vv.DICTBURNTYPETYPECount, vv.AREATYPE4Count);
                }

                var vvList5 = v.TREETYPECountModel;
                foreach (var vv in vvList5)
                {
                    sb.AppendFormat("<td class=\"center\">{0}/{1}</td>", vv.DICTTREETYPECount, vv.AREATYPE5Count);
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 资源详细信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getRESOURCEidDetailStr(DC_RESOURCE_NEW_SW sw)
        {

            string BYORGNO = Request.Params["BYORGNO"];
            sw.ORGNOS = BYORGNO;
            sw.ORGNOSXZ = "1";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>名称</th>");
            sb.AppendFormat("        <th>编号</th>");
            sb.AppendFormat("        <th>树种</th>");
            sb.AppendFormat("        <th>资源类型</th>");
            sb.AppendFormat("        <th>林龄类别</th>");
            sb.AppendFormat("        <th>起源类型</th>");
            sb.AppendFormat("        <th>可燃类型</th>");
            sb.AppendFormat("        <th>林木类型</th>");
            sb.AppendFormat("    </tr>");
            var list = DC_RESOURCE_NEWCls.getModelList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {

                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.KINDTYPE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.RESOURCETYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.AGETYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORIGINTYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.BURNTYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.TREETYPEName);
                sb.AppendFormat("</tr>");
                i++;

            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");

        }
        #endregion

        #region 资源统计图型展示
        /// <summary>
        /// 资源统计图型展示
        /// </summary>
        /// <returns></returns>
        public ActionResult RESOURCECountMan()
        {
            pubViewBag("004006", "004006", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.agetype = getAgetype(new T_SYS_DICTSW { DICTTYPEID = "27" });

            ViewBag.resourcetype = getresourcetype(new T_SYS_DICTSW { DICTTYPEID = "28" });

            ViewBag.originttype = getoriginttype(new T_SYS_DICTSW { DICTTYPEID = "29" });

            ViewBag.burntype = getburntype(new T_SYS_DICTSW { DICTTYPEID = "30" });

            ViewBag.treetype = gettreetype(new T_SYS_DICTSW { DICTTYPEID = "31" });

            return View();
        }
        /// <summary>
        /// 林龄
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        private string getAgetype(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk1' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        private string getresourcetype(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk2' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        private string getoriginttype(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk3' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        private string getburntype(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk4' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        private string gettreetype(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk5' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        #endregion
        #endregion

        #region 装备统计
        #region 装备统计导出Excel
        /// <summary>
        /// 装备统计导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult EQUIPCountExportExcel()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            string DC_EQUIP_NEW_ID = Request.Params["DC_EQUIP_NEW_ID"];
            string VALUE = Request.Params["VALUE"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004007", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);

            if (string.IsNullOrEmpty(BYORGNO))
                BYORGNO = SystemCls.getCurUserOrgNo();

            //if (PublicCls.OrgIsZhen(BYORGNO) == false)
            //{
            IEnumerable<EQUIPTYPECountModel> EQUIPTYPECountModel;
            IEnumerable<USESTATECountModel> USESTATECountModel;

            var list = DataCenterCountCls.getEQUIPModelCount(new DC_EQUIP_NEW_SW
            {
                TopORGNO = BYORGNO,
            }, out EQUIPTYPECountModel, out USESTATECountModel);

            int typeCount1 = 0;//计算类别有多少列
            int typeCount2 = 0;//计算类别有多少列
            foreach (var v in EQUIPTYPECountModel)
            {
                typeCount1++;
            }
            foreach (var v in USESTATECountModel)
            {
                typeCount2++;
            }
            //设置宽度
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 20 * 256);
            for (int i = 0; i < typeCount1; i++)
            {
                sheet1.SetColumnWidth(i + 2, 20 * 256);
            }
            for (int i = 0; i < typeCount2; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1, 20 * 256);
            }
            row = sheet1.CreateRow(1);
            if (PublicCls.OrgIsZhen(BYORGNO) == false)
                row.CreateCell(0).SetCellValue("单位");
            else
                row.CreateCell(0).SetCellValue("名称");
            row.CreateCell(1).SetCellValue("总数");
            row.CreateCell(2).SetCellValue("装备类型");
            row.CreateCell(2 + typeCount1).SetCellValue("使用现状");
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            row.GetCell(1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1).CellStyle = getCellStyleTitle(book);
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, typeCount1 + typeCount2 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 1, 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, typeCount1 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2 + typeCount1, typeCount1 + 1 + typeCount2));
            row = sheet1.CreateRow(2);
            int indexType = 2;//从第二列开始
            foreach (var v in EQUIPTYPECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.EQUIPTYPENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in USESTATECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.USESTATENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            int rowI = 1;//数据行
            foreach (var v in list)//循环获取数据
            {
                row = sheet1.CreateRow(rowI + 2);
                if (string.IsNullOrEmpty(v.ORGName) == false)
                {
                    string orgName = v.ORGName;
                    if (PublicCls.OrgIsZhen(BYORGNO) == false)
                    {
                        if (PublicCls.OrgIsShi(v.BYORGNO))//统计市，即所有县的
                        {
                        }
                        else if (PublicCls.OrgIsXian(v.BYORGNO))//县
                        {
                            orgName = "　　" + orgName;
                        }
                        else
                        {
                            orgName = "　　　　" + orgName;
                        }
                    }
                    row.CreateCell(0).SetCellValue(orgName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.NAME);
                    //row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                row.CreateCell(1).SetCellValue(v.EQUIPTYPECount);
                //row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 2;//类型开始列
                foreach (var vv in v.EQUIPTYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.DICTEQUIPTYPECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.USESTATECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.USESTATECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                rowI++;
            }
            //}
            //else//查询装备详细信息
            //{
            //    if (BYORGNO.Substring(6, 3) == "xxx")
            //    {
            //        BYORGNO = BYORGNO.Substring(0, 6) + "000";
            //    }
            //    row = sheet1.CreateRow(1);
            //    string[] titleArr = new string[] { "单位", "名称", "编号", "装备类型", "使用现状", "存储地点" };
            //    for (int i = 0; i < titleArr.Length; i++)
            //    {

            //        row.CreateCell(i).SetCellValue(titleArr[i]);
            //        row.GetCell(i).CellStyle = getCellStyleHead(book);
            //        if (i == 0 || i == 1)
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //        else if (i == 4 || i == 5 || i == 6 || i == 7 || i == 8)
            //            sheet1.SetColumnWidth(i, 25 * 256);//设置宽度
            //        else
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //    }
            //    sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, titleArr.Length - 1));
            //    var list = DC_EQUIP_NEWCls.getModelList(new DC_EQUIP_NEW_SW
            //    {
            //        BYORGNO = BYORGNO,
            //        ORGNOSXZ = "1",
            //    });

            //    int rowI = 0;//数据行
            //    foreach (var v in list)//循环获取数据
            //    {
            //        row = sheet1.CreateRow(rowI + 2);
            //        for (int i = 0; i < titleArr.Length; i++)
            //        {
            //            switch (i)
            //            {
            //                case 0:
            //                    row.CreateCell(i).SetCellValue(v.ORGName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 1:
            //                    row.CreateCell(i).SetCellValue(v.NAME);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 2:
            //                    row.CreateCell(i).SetCellValue(v.NUMBER);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 3:
            //                    row.CreateCell(i).SetCellValue(v.EQUIPTYPEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 4:
            //                    row.CreateCell(i).SetCellValue(v.USESTATEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 5:
            //                    row.CreateCell(i).SetCellValue(v.STOREADDR);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //            }
            //        }
            //        rowI++;
            //    }
            //}
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = vMenu.MENUNAME + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(ms, "application/vnd.ms-excel", fileName);

        }
        #endregion

        #region 装备统计
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        //public ActionResult EQUIPCountquery()
        //{
        //    string BYORGNO = Request.Params["BYORGNO"];
        //    string DC_EQUIP_NEW_ID = Request.Params["DC_EQUIP_NEW_ID"];
        //    string VALUE = Request.Params["VALUE"];
        //    string str = ClsStr.EncryptA01(BYORGNO + "|" + DC_EQUIP_NEW_ID + "|" + VALUE, "kkkkkkkk");
        //    return Content(JsonConvert.SerializeObject(new Message(true, "", "/DataCenterCount/EQUIPCount?trans=" + str)), "text/html;charset=UTF-8");
        //}
        /// <summary>
        /// 装备统计
        /// </summary>
        /// <returns></returns>
        public ActionResult EQUIPCount()
        {
            pubViewBag("004007", "004007", "");
            if (ViewBag.isPageRight == false)
                return View();


            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });

            return View();
        }
        /// <summary>
        /// 装备统计表格
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getEQUIPCountStr(DC_EQUIP_NEW_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.TopORGNO = BYORGNO;
            StringBuilder sb = new StringBuilder();
            string a = DataCenterCountCls.getCount("32");//装备类型
            string b = DataCenterCountCls.getCount("36");//使用现状类别
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th rowspan=\"2\">单位/名称</th>");
            sb.AppendFormat("        <th rowspan=\"2\">总数</th>");
            sb.AppendFormat("        <th colspan=\"{0}\">装备类型</th>", int.Parse(a));
            sb.AppendFormat("        <th colspan=\"{0}\">使用现状</th>", int.Parse(b));
            sb.AppendFormat("    </tr>");
            IEnumerable<EQUIPTYPECountModel> EQUIPTYPECountModel;
            IEnumerable<USESTATECountModel> USESTATECountModel;
            var list = DataCenterCountCls.getEQUIPModelCount(sw, out EQUIPTYPECountModel, out USESTATECountModel);
            sb.AppendFormat("    <tr>");

            foreach (var v in EQUIPTYPECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.EQUIPTYPENAME);

            }
            foreach (var v in USESTATECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.USESTATENAME);

            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                i++;
                string orgName = v.ORGName;
                string orgNo = v.BYORGNO;
                if (string.IsNullOrEmpty(v.BYORGNO))
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.NAME, "");
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        sb.AppendFormat("<tr class='{0}'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<tr class='{0} row1'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    
                }

                string totol = "";
                totol = v.EQUIPTYPECount;
                sb.AppendFormat("<td class=\"center\">{0}</td>", totol);//总数
                var vvList1 = v.EQUIPTYPECountModel;
                foreach (var vv in vvList1)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.DICTEQUIPTYPECount);
                }
                var vvList2 = v.USESTATECountModel;
                foreach (var vv in vvList2)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.USESTATECount);
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        public ActionResult getEQUIPidDetailStr(DC_EQUIP_NEW_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.BYORGNO = BYORGNO;
            sw.ORGNOSXZ = "1";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>名称</th>");
            sb.AppendFormat("        <th>编号</th>");
            sb.AppendFormat("        <th>装备类型</th>");
            sb.AppendFormat("        <th>使用现状</th>");
            sb.AppendFormat("        <th>存储地点</th>");
            sb.AppendFormat("    </tr>");
            var list = DC_EQUIP_NEWCls.getModelList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {

                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.EQUIPTYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.USESTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.STOREADDR);

                sb.AppendFormat("</tr>");
                i++;

            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");

        }
        #endregion

        #region 装备统计图型展示
        /// <summary>
        /// 装备统计图型展示
        /// </summary>
        /// <returns></returns>
        public ActionResult EQUIPCountMan()
        {
            pubViewBag("004007", "004007", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            //ViewBag.equiptype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "32", isShowAll = "1" });
            //ViewBag.usestate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36", isShowAll = "1" });
            ViewBag.equiptype = getequiptype(new T_SYS_DICTSW { DICTTYPEID = "32" });
            ViewBag.usestate = getusestate(new T_SYS_DICTSW { DICTTYPEID = "36" });
            return View();
        }
        /// <summary>
        /// 装备类型
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        private string getequiptype(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk1' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 使用现状
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        private string getusestate(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk2' value='" + v.DICTVALUE + "' checked=\"checked\"/>{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        #endregion
        #endregion

        #region  车辆统计

        #region 车辆统计导出Excel
        public FileResult CARCountExportExcel()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            string DC_CAR_ID = Request.Params["DC_CAR_ID"];
            string CARTYPE = Request.Params["CARTYPE"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004008", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            if (string.IsNullOrEmpty(BYORGNO))
                BYORGNO = SystemCls.getCurUserOrgNo();
            //if (PublicCls.OrgIsZhen(BYORGNO) == false)
            //{
            IEnumerable<CARTYPECountModel> typeModel;
            var list = DataCenterCountCls.getCARModelCount(
                new DC_CAR_SW
                {
                    TopORGNO = BYORGNO,
                }
                , out typeModel);

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
                row.CreateCell(indexType).SetCellValue(v.CARTYPENAME);
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
                    string orgName = v.ORGName;
                    if (PublicCls.OrgIsZhen(BYORGNO) == false)
                    {
                        if (PublicCls.OrgIsShi(v.BYORGNO))//统计市，即所有县的
                        {
                        }
                        else if (PublicCls.OrgIsXian(v.BYORGNO))//县
                        {
                            orgName = "　　" + orgName;
                        }
                        else
                        {
                            orgName = "　　　　" + orgName;
                        }
                    }
                    row.CreateCell(0).SetCellValue(orgName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.NAME);
                    //row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                row.CreateCell(1).SetCellValue(v.CARTYPECount);
                //row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 2;//类型开始列
                foreach (var vv in v.CARTYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.CARTYPECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                rowI++;
            }
            //}
            //else//查询车辆详细信息
            //{
            //    if (BYORGNO.Substring(6, 3) == "xxx")
            //    {
            //        BYORGNO = BYORGNO.Substring(0, 6) + "000";
            //    }
            //    row = sheet1.CreateRow(1);
            //    string[] titleArr = new string[] { "单位", "名称", "编号", "驾驶员", "号牌", "车辆类型", "联系方式", "存储地点" };
            //    for (int i = 0; i < titleArr.Length; i++)
            //    {

            //        row.CreateCell(i).SetCellValue(titleArr[i]);
            //        row.GetCell(i).CellStyle = getCellStyleHead(book);
            //        if (i == 0 || i == 1)
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //        else if (i == 4 || i == 5 || i == 6 || i == 7 || i == 8)
            //            sheet1.SetColumnWidth(i, 25 * 256);//设置宽度
            //        else
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //    }
            //    sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, titleArr.Length - 1));
            //    var list = DC_CARCls.getModelList(new DC_CAR_SW
            //    {
            //        BYORGNO = BYORGNO,
            //        ORGNOSXZ = "1",
            //    });

            //    int rowI = 0;//数据行
            //    foreach (var v in list)//循环获取数据
            //    {
            //        row = sheet1.CreateRow(rowI + 2);
            //        for (int i = 0; i < titleArr.Length; i++)
            //        {
            //            switch (i)
            //            {
            //                case 0:
            //                    row.CreateCell(i).SetCellValue(v.ORGName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 1:
            //                    row.CreateCell(i).SetCellValue(v.NAME);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 2:
            //                    row.CreateCell(i).SetCellValue(v.NUMBER);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 3:
            //                    row.CreateCell(i).SetCellValue(v.DRIVER);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 4:
            //                    row.CreateCell(i).SetCellValue(v.PLATENUM);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 5:
            //                    row.CreateCell(i).SetCellValue(v.CARTYPEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 6:
            //                    row.CreateCell(i).SetCellValue(v.CONTACTS);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 7:
            //                    row.CreateCell(i).SetCellValue(v.STOREADDR);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //            }
            //        }
            //        rowI++;
            //    }
            //}
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = vMenu.MENUNAME + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(ms, "application/vnd.ms-excel", fileName);

        }

        #endregion

        #region  车辆统计
        #region 车辆统计图
        /// <summary>
        /// 车辆统计图
        /// </summary>
        /// <returns></returns>
        public ActionResult CARCountMan()
        {
            pubViewBag("004008", "004008", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }
        #endregion


        /// <summary>
        /// 车辆统计
        /// </summary>
        /// <returns></returns>
        public ActionResult CARCount()
        {
            pubViewBag("004008", "004008", "");
            if (ViewBag.isPageRight == false)
                return View();


            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });

            return View();
        }
        /// <summary>
        /// 车辆统计表格
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getCARCountStr(DC_CAR_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.TopORGNO = BYORGNO;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>单位/名称</th>");
            sb.AppendFormat("        <th>总数</th>");
            IEnumerable<CARTYPECountModel> typeModel;
            var list = DataCenterCountCls.getCARModelCount(sw, out typeModel);
            foreach (var v in typeModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.CARTYPENAME);

            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                i++;
                string orgName = v.ORGName;
                string orgNo = v.BYORGNO;
                if (string.IsNullOrEmpty(v.BYORGNO))
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.NAME, "");
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        sb.AppendFormat("<tr class='{0}'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<tr class='{0} row1'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.CARTYPECount);//总
                var vvList = v.CARTYPECountModel;
                foreach (var vv in vvList)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.CARTYPECount);
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        public ActionResult getCARidDetailStr(DC_CAR_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.BYORGNO = BYORGNO;
            sw.ORGNOSXZ = "1";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>名称</th>");
            sb.AppendFormat("        <th>编号</th>");
            sb.AppendFormat("        <th>驾驶员</th>");
            sb.AppendFormat("        <th>号牌</th>");
            sb.AppendFormat("        <th>车辆类型</th>");
            sb.AppendFormat("        <th>联系方式</th>");
            sb.AppendFormat("        <th>存储地点</th>");
            sb.AppendFormat("    </tr>");
            var list = DC_CARCls.getModelList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {

                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.DRIVER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PLATENUM);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.CARTYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.CONTACTS);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.STOREADDR);
                sb.AppendFormat("</tr>");
                i++;

            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");

        }
        #endregion

        #endregion

        #region 设施统计
        #region 营房

        #region 营房统计导出Excel
        public FileResult CAMPCountExportExcel()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            string DC_UTILITY_CAMP_ID = Request.Params["DC_UTILITY_CAMP_ID"];
            string STRUCTURETYPE = Request.Params["STRUCTURETYPE"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004009", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);


            if (string.IsNullOrEmpty(BYORGNO))
                BYORGNO = SystemCls.getCurUserOrgNo();
            //if (PublicCls.OrgIsZhen(BYORGNO) == false)
            //{
            IEnumerable<STRUCTURETYPECountModel> typeModel;
            var list = DataCenterCountCls.getCAMPModelCount(
                new DC_UTILITY_CAMP_SW
                {
                    TopORGNO = BYORGNO,
                }
                , out typeModel);

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
                row.CreateCell(indexType).SetCellValue(v.STRUCTURETYPENAME);
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
                    string orgName = v.ORGName;
                    if (PublicCls.OrgIsZhen(BYORGNO) == false)
                    {
                        if (PublicCls.OrgIsShi(v.BYORGNO))//统计市，即所有县的
                        {
                        }
                        else if (PublicCls.OrgIsXian(v.BYORGNO))//县
                        {
                            orgName = "　　" + orgName;
                        }
                        else
                        {
                            orgName = "　　　　" + orgName;
                        }
                    }
                    row.CreateCell(0).SetCellValue(orgName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.NAME);
                    //row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                row.CreateCell(1).SetCellValue(v.STRUCTURETYPECount);
                //row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 2;//类型开始列
                foreach (var vv in v.STRUCTURETYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.STRUCTURETYPECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                rowI++;
            }
            //}
            //else//查询营房详细信息
            //{

            //    if (BYORGNO.Substring(6, 3) == "xxx")
            //    {
            //        BYORGNO = BYORGNO.Substring(0, 6) + "000";
            //    }
            //    row = sheet1.CreateRow(1);
            //    string[] titleArr = new string[] { "单位", "名称", "编号", "结构类型", "面积", "楼层", "附属设施" };
            //    for (int i = 0; i < titleArr.Length; i++)
            //    {

            //        row.CreateCell(i).SetCellValue(titleArr[i]);
            //        row.GetCell(i).CellStyle = getCellStyleHead(book);
            //        if (i == 0 || i == 1)
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //        else if (i == 3)
            //            sheet1.SetColumnWidth(i, 25 * 256);//设置宽度
            //        else
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //    }
            //    sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, titleArr.Length - 1));
            //    var list = DC_UTILITY_CAMPCls.getModelList(new DC_UTILITY_CAMP_SW
            //    {
            //        ORGNOS = BYORGNO,
            //        ORGNOSXZ = "1",
            //    });

            //    int rowI = 0;//数据行
            //    foreach (var v in list)//循环获取数据
            //    {
            //        row = sheet1.CreateRow(rowI + 2);
            //        for (int i = 0; i < titleArr.Length; i++)
            //        {
            //            switch (i)
            //            {
            //                case 0:
            //                    row.CreateCell(i).SetCellValue(v.ORGName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 1:
            //                    row.CreateCell(i).SetCellValue(v.NAME);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 2:
            //                    row.CreateCell(i).SetCellValue(v.NUMBER);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 3:
            //                    row.CreateCell(i).SetCellValue(v.STRUCTURETYPEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 4:
            //                    row.CreateCell(i).SetCellValue(v.AREA);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 5:
            //                    row.CreateCell(i).SetCellValue(v.FLOOR);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 6:
            //                    row.CreateCell(i).SetCellValue(v.SUBFACILITIES);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //            }
            //        }
            //        rowI++;
            //    }
            //}
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = vMenu.MENUNAME + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(ms, "application/vnd.ms-excel", fileName);

        }

        #endregion

        #region  营房统计


        /// <summary>
        /// 营房统计查询
        /// </summary>
        /// <returns></returns>
        //public ActionResult CAMPCountquery()
        //{
        //    string BYORGNO = Request.Params["BYORGNO"];
        //    string DC_UTILITY_CAMP_ID = Request.Params["DC_UTILITY_CAMP_ID"];
        //    string STRUCTURETYPE = Request.Params["STRUCTURETYPE"];
        //    string str = ClsStr.EncryptA01(BYORGNO + "|" + DC_UTILITY_CAMP_ID + "|" + STRUCTURETYPE, "kkkkkkkk");
        //    return Content(JsonConvert.SerializeObject(new Message(true, "", "/DataCenterCount/CAMPCount?trans=" + str)), "text/html;charset=UTF-8");
        //}
        /// <summary>
        /// 营房统计
        /// </summary>
        /// <returns></returns>
        public ActionResult CAMPCount()
        {
            pubViewBag("004009", "004009", "");
            if (ViewBag.isPageRight == false)
                return View();


            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });

            return View();
        }
        /// <summary>
        /// 营房统计表格
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getCAMPCountStr(DC_UTILITY_CAMP_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.TopORGNO = BYORGNO;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>单位/名称</th>");
            sb.AppendFormat("        <th>总数</th>");
            IEnumerable<STRUCTURETYPECountModel> typeModel;
            var list = DataCenterCountCls.getCAMPModelCount(sw, out typeModel);
            foreach (var v in typeModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.STRUCTURETYPENAME);

            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                i++;
                string orgName = v.ORGName;
                string orgNo = v.BYORGNO;
                if (string.IsNullOrEmpty(v.BYORGNO))
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.NAME, "");
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        sb.AppendFormat("<tr class='{0}'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<tr class='{0} row1'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                   
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.STRUCTURETYPECount);//总
                var vvList = v.STRUCTURETYPECountModel;
                foreach (var vv in vvList)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.STRUCTURETYPECount);
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        public ActionResult getCAMPidStr(DC_UTILITY_CAMP_SW sw)
        {

            string BYORGNO = Request.Params["BYORGNO"];
            sw.ORGNOS = BYORGNO;
            sw.ORGNOSXZ = "1";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>名称</th>");
            sb.AppendFormat("        <th>编号</th>");
            sb.AppendFormat("        <th>结构类型</th>");
            sb.AppendFormat("        <th>面积</th>");
            sb.AppendFormat("        <th>楼层</th>");
            sb.AppendFormat("        <th>附属设施</th>");
            sb.AppendFormat("    </tr>");
            var list = DC_UTILITY_CAMPCls.getModelList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {

                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.STRUCTURETYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.AREA);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.FLOOR);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.SUBFACILITIES);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");

        }

        #endregion

        #region 营房统计图型展示
        /// <summary>
        /// 营房统计图型展示
        /// </summary>
        /// <returns></returns>
        public ActionResult CampCountMan()
        {
            pubViewBag("004009", "004009", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }
        #endregion
        #endregion

        #region 瞭望台
        #region 瞭望台统计导出Excel
        /// <summary>
        /// 瞭望台统计导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult OVERWATCHCountExportExcel()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            string DC_UTILITY_OVERWATCH_ID = Request.Params["DC_UTILITY_OVERWATCH_ID"];
            string STRUCTURETYPE = Request.Params["STRUCTURETYPE"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004010", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);

            if (string.IsNullOrEmpty(BYORGNO))
                BYORGNO = SystemCls.getCurUserOrgNo();

            //if (PublicCls.OrgIsZhen(BYORGNO) == false)
            //{
            IEnumerable<TYPECountModel> typeModel;
            var list = DataCenterCountCls.getOVERWATCHModelCount(
                new DC_UTILITY_OVERWATCH_SW
                {
                    TopORGNO = BYORGNO,
                }
                , out typeModel);

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
                row.CreateCell(indexType).SetCellValue(v.STRUCTURETYPENAME);
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
                    string orgName = v.ORGName;
                    if (PublicCls.OrgIsZhen(BYORGNO) == false)
                    {
                        if (PublicCls.OrgIsShi(v.BYORGNO))//统计市，即所有县的
                        {
                        }
                        else if (PublicCls.OrgIsXian(v.BYORGNO))//县
                        {
                            orgName = "　　" + orgName;
                        }
                        else
                        {
                            orgName = "　　　　" + orgName;
                        }
                    }
                    row.CreateCell(0).SetCellValue(orgName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.NAME);
                    //row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                row.CreateCell(1).SetCellValue(v.STRUCTURETYPECount);
                //row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 2;//类型开始列
                foreach (var vv in v.TYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.STRUCTURETYPECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                rowI++;
            }
            //}
            //else//查询瞭望台详细信息
            //{
            //    if (BYORGNO.Substring(6, 3) == "xxx")
            //    {
            //        BYORGNO = BYORGNO.Substring(0, 6) + "000";
            //    }
            //    row = sheet1.CreateRow(1);
            //    string[] titleArr = new string[] { "单位", "名称", "编号", "结构类型", "面积", "楼层", "附属设施" };
            //    for (int i = 0; i < titleArr.Length; i++)
            //    {

            //        row.CreateCell(i).SetCellValue(titleArr[i]);
            //        row.GetCell(i).CellStyle = getCellStyleHead(book);
            //        if (i == 0 || i == 1)
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //        else if (i == 3)
            //            sheet1.SetColumnWidth(i, 25 * 256);//设置宽度
            //        else
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //    }
            //    sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, titleArr.Length - 1));
            //    var list = DC_UTILITY_OVERWATCHCls.getModelList(new DC_UTILITY_OVERWATCH_SW
            //    {
            //        ORGNOS = BYORGNO,
            //        ORGNOSXZ = "1",
            //    });

            //    int rowI = 0;//数据行
            //    foreach (var v in list)//循环获取数据
            //    {
            //        row = sheet1.CreateRow(rowI + 2);
            //        for (int i = 0; i < titleArr.Length; i++)
            //        {
            //            switch (i)
            //            {
            //                case 0:
            //                    row.CreateCell(i).SetCellValue(v.ORGName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 1:
            //                    row.CreateCell(i).SetCellValue(v.NAME);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 2:
            //                    row.CreateCell(i).SetCellValue(v.NUMBER);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 3:
            //                    row.CreateCell(i).SetCellValue(v.STRUCTURETYPEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 4:
            //                    row.CreateCell(i).SetCellValue(v.AREA);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 5:
            //                    row.CreateCell(i).SetCellValue(v.FLOOR);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 6:
            //                    row.CreateCell(i).SetCellValue(v.SUBFACILITIES);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //            }
            //        }
            //        rowI++;
            //    }
            //}
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = vMenu.MENUNAME + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(ms, "application/vnd.ms-excel", fileName);


        }
        #endregion

        #region 瞭望台统计
        /// <summary>
        /// 瞭望台统计查询
        /// </summary>
        /// <returns></returns>
        //public ActionResult OVERWATCHCountquery()
        //{
        //    string BYORGNO = Request.Params["BYORGNO"];
        //    string DC_UTILITY_OVERWATCH_ID = Request.Params["DC_UTILITY_OVERWATCH_ID"];
        //    string STRUCTURETYPE = Request.Params["STRUCTURETYPE"];
        //    string str = ClsStr.EncryptA01(BYORGNO + "|" + DC_UTILITY_OVERWATCH_ID + "|" + STRUCTURETYPE, "kkkkkkkk");
        //    return Content(JsonConvert.SerializeObject(new Message(true, "", "/DataCenterCount/OVERWATCHCount?trans=" + str)), "text/html;charset=UTF-8");
        //}
        /// <summary>
        /// 瞭望台统计
        /// </summary>
        /// <returns></returns>
        public ActionResult OVERWATCHCount()
        {
            pubViewBag("004010", "004010", "");
            if (ViewBag.isPageRight == false)
                return View();

            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });

            return View();
        }
        /// <summary>
        /// 瞭望台统计表格
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getOVERWATCHCountStr(DC_UTILITY_OVERWATCH_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.TopORGNO = BYORGNO;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>单位/名称</th>");
            sb.AppendFormat("        <th>总数</th>");
            IEnumerable<TYPECountModel> typeModel;
            var list = DataCenterCountCls.getOVERWATCHModelCount(sw, out typeModel);
            foreach (var v in typeModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.STRUCTURETYPENAME);

            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                i++;
                string orgName = v.ORGName;
                string orgNo = v.BYORGNO;
                if (string.IsNullOrEmpty(v.BYORGNO))
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.NAME, "");
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        sb.AppendFormat("<tr class='{0}'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<tr class='{0} row1'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.STRUCTURETYPECount);//总
                var vvList = v.TYPECountModel;
                foreach (var vv in vvList)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.STRUCTURETYPECount);
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 瞭望台详细
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getOVERWATCHidDetailStr(DC_UTILITY_OVERWATCH_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.ORGNOS = BYORGNO;
            sw.ORGNOSXZ = "1";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>名称</th>");
            sb.AppendFormat("        <th>编号</th>");
            sb.AppendFormat("        <th>结构类型</th>");
            sb.AppendFormat("        <th>面积</th>");
            sb.AppendFormat("        <th>楼层</th>");
            sb.AppendFormat("        <th>附属设施</th>");
            sb.AppendFormat("    </tr>");
            var list = DC_UTILITY_OVERWATCHCls.getModelList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {

                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.STRUCTURETYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.AREA);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.FLOOR);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.SUBFACILITIES);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");

        }
        #endregion

        #region 瞭望台统计图型展示
        /// <summary>
        /// 瞭望台统计图型展示
        /// </summary>
        /// <returns></returns>
        public ActionResult OVERWATCHCountMan()
        {
            pubViewBag("004010", "004010", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }
        #endregion
        #endregion

        #region 隔离带

        #region 隔离带统计导出execl
        /// <summary>
        /// 隔离带统计导出execl
        /// </summary>
        /// <returns></returns>
        public FileResult ISOLATIONSTRIPCountExportExcel()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            //string DC_UTILITY_ISOLATIONSTRIP_ID = Request.Params["DC_UTILITY_ISOLATIONSTRIP_ID"];
            //string VALUE = Request.Params["VALUE"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004011", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            if (string.IsNullOrEmpty(BYORGNO))
                BYORGNO = SystemCls.getCurUserOrgNo();
            //if (PublicCls.OrgIsZhen(BYORGNO) == false)
            //{
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<ISOLATIONTYPECountModel> ISOLATIONTYPECountModel;
            var list = DataCenterCountCls.getISOLATIONSTRIPModelCount(new DC_UTILITY_ISOLATIONSTRIP_SW
            {
                TopORGNO = BYORGNO,
            }, out USESTATECountModel, out MANAGERSTATECountModel, out ISOLATIONTYPECountModel);

            int typeCount1 = 0;//计算类别有多少列
            int typeCount2 = 0;//计算类别有多少列
            int typeCount3 = 0;//计算类别有多少列
            foreach (var v in ISOLATIONTYPECountModel)
            {
                typeCount1++;
            }
            foreach (var v in USESTATECountModel)
            {
                typeCount2++;
            }
            foreach (var v in MANAGERSTATECountModel)
            {
                typeCount3++;
            }
            //设置宽度
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 20 * 256);
            for (int i = 0; i < typeCount1; i++)
            {
                sheet1.SetColumnWidth(i + 2, 20 * 256);
            }
            for (int i = 0; i < typeCount2; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1, 20 * 256);
            }
            for (int i = 0; i < typeCount3; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1 + typeCount2, 20 * 256);
            }
            row = sheet1.CreateRow(1);
            if (PublicCls.OrgIsZhen(BYORGNO) == false)
                row.CreateCell(0).SetCellValue("单位");
            else
                row.CreateCell(0).SetCellValue("名称");
            row.CreateCell(1).SetCellValue("总数(个)/长度(米)");
            row.CreateCell(2).SetCellValue("隔离带类型");
            row.CreateCell(2 + typeCount1).SetCellValue("使用现状");
            row.CreateCell(2 + typeCount1 + typeCount2).SetCellValue("维护类型");
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            row.GetCell(1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1 + typeCount2).CellStyle = getCellStyleTitle(book);
            //row.GetCell(2 + typeCount1 + typeCount2 + typeCount3).CellStyle = getCellStyleTitle(book);
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, typeCount1 + typeCount2 + typeCount3 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 1, 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, typeCount1 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2 + typeCount1, typeCount1 + 1 + typeCount2));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, typeCount1 + 2 + typeCount2, typeCount1 + 1 + typeCount2 + typeCount3));
            row = sheet1.CreateRow(2);
            int indexType = 2;//从第二列开始
            foreach (var v in ISOLATIONTYPECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.ISOLATIONTYPENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in USESTATECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.USESTATENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in MANAGERSTATECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.MANAGERSTATNAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }

            int rowI = 1;//数据行
            foreach (var v in list)//循环获取数据
            {
                row = sheet1.CreateRow(rowI + 2);
                if (string.IsNullOrEmpty(v.ORGName) == false)
                {
                    string orgName = v.ORGName;
                    if (PublicCls.OrgIsZhen(BYORGNO) == false)
                    {
                        if (PublicCls.OrgIsShi(v.BYORGNO))//统计市，即所有县的
                        {
                        }
                        else if (PublicCls.OrgIsXian(v.BYORGNO))//县
                        {
                            orgName = "　　" + orgName;
                        }
                        else
                        {
                            orgName = "　　　　" + orgName;
                        }
                    }
                    row.CreateCell(0).SetCellValue(orgName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.NAME);
                    //row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                row.CreateCell(1).SetCellValue(v.USESTATECount + "/" + v.LENGTHCount + "");
                //row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 2;//类型开始列
                foreach (var vv in v.ISOLATIONTYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.DICTISOLATIONTYPECount + "/" + vv.DICTISOLATIONTYPELENGTHCount + "");
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.USESTATECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.USESTATECount + "/" + vv.USESTATELENGTHCount + "");
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.MANAGERSTATECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.MANAGERSTATCount + "/" + vv.MANAGERSTATLENGTHCount + "");
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                rowI++;
            }
            //}
            //else//查询隔离带详细信息
            //{
            //    if (BYORGNO.Substring(6, 3) == "xxx")
            //    {
            //        BYORGNO = BYORGNO.Substring(0, 6) + "000";
            //    }
            //    row = sheet1.CreateRow(1);
            //    string[] titleArr = new string[] { "单位", "名称", "编号", "隔离带类型", "使用现状", "维护类型" };
            //    for (int i = 0; i < titleArr.Length; i++)
            //    {

            //        row.CreateCell(i).SetCellValue(titleArr[i]);
            //        row.GetCell(i).CellStyle = getCellStyleHead(book);
            //        if (i == 0 || i == 1)
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //        else if (i == 4)
            //            sheet1.SetColumnWidth(i, 25 * 256);//设置宽度
            //        else
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //    }
            //    sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, titleArr.Length - 1));
            //    var list = DC_UTILITY_ISOLATIONSTRIPCls.getModelList(new DC_UTILITY_ISOLATIONSTRIP_SW
            //    {
            //        BYORGNO = BYORGNO,
            //        ORGNOSXZ = "1",
            //    });

            //    int rowI = 0;//数据行
            //    foreach (var v in list)//循环获取数据
            //    {
            //        row = sheet1.CreateRow(rowI + 2);
            //        for (int i = 0; i < titleArr.Length; i++)
            //        {
            //            switch (i)
            //            {
            //                case 0:
            //                    row.CreateCell(i).SetCellValue(v.ORGName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 1:
            //                    row.CreateCell(i).SetCellValue(v.NAME);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 2:
            //                    row.CreateCell(i).SetCellValue(v.NUMBER);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 3:
            //                    row.CreateCell(i).SetCellValue(v.ISOLATIONTYPEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 4:
            //                    row.CreateCell(i).SetCellValue(v.USESTATEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 5:
            //                    row.CreateCell(i).SetCellValue(v.MANAGERSTATEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //            }
            //        }
            //        rowI++;
            //    }
            //}
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = vMenu.MENUNAME + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(ms, "application/vnd.ms-excel", fileName);

        }
        #endregion

        #region 隔离带统计
        public ActionResult ISOLATIONSTRIPCount()
        {
            pubViewBag("004011", "004011", "");
            if (ViewBag.isPageRight == false)
                return View();

            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });

            return View();
        }
        /// <summary>
        /// 隔离带统计表格展示
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getISOLATIONSTRIPCountStr(DC_UTILITY_ISOLATIONSTRIP_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.TopORGNO = BYORGNO;
            StringBuilder sb = new StringBuilder();
            string a = DataCenterCountCls.getCount("36");//使用现状
            string b = DataCenterCountCls.getCount("37");//维护类型
            string c = DataCenterCountCls.getCount("35");//防火通道等级类型
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th rowspan=\"2\">单位/名称</th>");
            sb.AppendFormat("        <th rowspan=\"2\">总数(个)/长度(米)</th>");
            sb.AppendFormat("        <th colspan=\"{0}\">隔离带类型</th>", int.Parse(c));
            sb.AppendFormat("        <th colspan=\"{0}\">使用现状</th>", int.Parse(a));
            sb.AppendFormat("        <th colspan=\"{0}\">维护类型</th>", int.Parse(b));
            sb.AppendFormat("    </tr>");
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<ISOLATIONTYPECountModel> ISOLATIONTYPECountModel;
            var list = DataCenterCountCls.getISOLATIONSTRIPModelCount(sw, out USESTATECountModel, out MANAGERSTATECountModel, out ISOLATIONTYPECountModel);
            sb.AppendFormat("    <tr>");
            foreach (var v in ISOLATIONTYPECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.ISOLATIONTYPENAME);

            }
            foreach (var v in USESTATECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.USESTATENAME);

            }
            foreach (var v in MANAGERSTATECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.MANAGERSTATNAME);

            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                i++;
                string orgName = v.ORGName;
                string orgNo = v.BYORGNO;
                if (string.IsNullOrEmpty(v.BYORGNO))
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.NAME, "");
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        sb.AppendFormat("<tr class='{0}'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<tr class='{0} row1'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                }
                string totol = "";
                totol = v.ISOLATIONTYPECount;
                sb.AppendFormat("<td class=\"center\">{0}/{1}</td>", totol, v.LENGTHCount);//总数
                var vvList3 = v.ISOLATIONTYPECountModel;
                foreach (var vv in vvList3)
                {
                    sb.AppendFormat("<td class=\"center\">{0}/{1}</td>", vv.DICTISOLATIONTYPECount, vv.DICTISOLATIONTYPELENGTHCount);
                }
                var vvList1 = v.USESTATECountModel;
                foreach (var vv in vvList1)
                {
                    sb.AppendFormat("<td class=\"center\">{0}/{1}</td>", vv.USESTATECount, vv.USESTATELENGTHCount);
                }
                var vvList2 = v.MANAGERSTATECountModel;
                foreach (var vv in vvList2)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.MANAGERSTATCount, vv.MANAGERSTATLENGTHCount);
                }

                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 隔离带详细信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getISOLATIONSTRIPidDetailStr(DC_UTILITY_ISOLATIONSTRIP_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.BYORGNO = BYORGNO;
            sw.ORGNOSXZ = "1";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>名称</th>");
            sb.AppendFormat("        <th>编号</th>");
            sb.AppendFormat("        <th>隔离带类型</th>");
            sb.AppendFormat("        <th>使用现状</th>");
            sb.AppendFormat("        <th>维护类型</th>");
            sb.AppendFormat("    </tr>");
            var list = DC_UTILITY_ISOLATIONSTRIPCls.getModelList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {

                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ISOLATIONTYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.USESTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MANAGERSTATEName);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");

        }
        #endregion

        #region 隔离带统计图型展示
        /// <summary>
        /// 隔离带统计图型展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ISOLATIONSTRIPCountMan()
        {
            pubViewBag("004011", "004011", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.isolationtype = getisolationtype(new T_SYS_DICTSW { DICTTYPEID = "35", isShowAll = "1" });
            ViewBag.usestate = getusestate(new T_SYS_DICTSW { DICTTYPEID = "36" });
            ViewBag.managerstate = getmanagerstate(new T_SYS_DICTSW { DICTTYPEID = "37" });
            return View();
        }
        private string getisolationtype(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk1' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        #endregion
        #endregion

        #region 防火通道
        #region 防火通道统计导出Excel
        /// <summary>
        /// 防火通道统计导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult FIRECHANNELCountExportExcel()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            //string DC_UTILITY_FIRECHANNEL_ID = Request.Params["DC_UTILITY_FIRECHANNEL_ID"];
            //string VALUE = Request.Params["VALUE"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004012", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            if (string.IsNullOrEmpty(BYORGNO))
                BYORGNO = SystemCls.getCurUserOrgNo();
            //if (PublicCls.OrgIsZhen(BYORGNO) == false)
            //{
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<FIRECHANNELLEVELTYPECountModel> FIRECHANNELLEVELTYPECountModel;
            IEnumerable<FIRECHANNELUSERTYPECountModel> FIRECHANNELUSERTYPECountModel;
            var list = DataCenterCountCls.getFIRECHANNELModelCount(new DC_UTILITY_FIRECHANNEL_SW
            {
                TopORGNO = BYORGNO,
            }, out USESTATECountModel, out MANAGERSTATECountModel, out FIRECHANNELLEVELTYPECountModel, out FIRECHANNELUSERTYPECountModel);

            int typeCount1 = 0;//计算类别有多少列
            int typeCount2 = 0;//计算类别有多少列
            int typeCount3 = 0;//计算类别有多少列
            int typeCount4 = 0;//计算类别有多少列
            foreach (var v in USESTATECountModel)
            {
                typeCount1++;
            }
            foreach (var v in MANAGERSTATECountModel)
            {
                typeCount2++;
            }
            foreach (var v in FIRECHANNELLEVELTYPECountModel)
            {
                typeCount3++;
            }
            foreach (var v in FIRECHANNELUSERTYPECountModel)
            {
                typeCount4++;
            }
            //设置宽度
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 20 * 256);
            for (int i = 0; i < typeCount1; i++)
            {
                sheet1.SetColumnWidth(i + 2, 20 * 256);
            }
            for (int i = 0; i < typeCount2; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1, 20 * 256);
            }
            for (int i = 0; i < typeCount3; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1 + typeCount2, 20 * 256);
            }
            for (int i = 0; i < typeCount4; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1 + typeCount2 + typeCount3, 20 * 256);
            }
            row = sheet1.CreateRow(1);
            if (PublicCls.OrgIsZhen(BYORGNO) == false)
                row.CreateCell(0).SetCellValue("单位");
            else
                row.CreateCell(0).SetCellValue("名称");
            row.CreateCell(1).SetCellValue("总数");
            row.CreateCell(2).SetCellValue("使用现状");
            row.CreateCell(2 + typeCount1).SetCellValue("维护类型");
            row.CreateCell(2 + typeCount1 + typeCount2).SetCellValue("防火通道等级");
            row.CreateCell(2 + typeCount1 + typeCount2 + typeCount3).SetCellValue("防火通道使用性质");
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            row.GetCell(1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1 + typeCount2).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1 + typeCount2 + typeCount3).CellStyle = getCellStyleTitle(book);
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, typeCount1 + typeCount2 + typeCount3 + typeCount4 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 1, 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, typeCount1 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2 + typeCount1, typeCount1 + 1 + typeCount2));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, typeCount1 + 2 + typeCount2, typeCount1 + 1 + typeCount2 + typeCount3));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, typeCount1 + 2 + typeCount2 + typeCount3, typeCount1 + 1 + typeCount2 + typeCount3 + typeCount4));
            row = sheet1.CreateRow(2);
            int indexType = 2;//从第二列开始
            foreach (var v in USESTATECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.USESTATENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in MANAGERSTATECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.MANAGERSTATNAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in FIRECHANNELLEVELTYPECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.FIRECHANNELLEVELTYPENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in FIRECHANNELUSERTYPECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.FIRECHANNELUSERTYPENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }

            int rowI = 1;//数据行
            foreach (var v in list)//循环获取数据
            {
                row = sheet1.CreateRow(rowI + 2);
                if (string.IsNullOrEmpty(v.ORGName) == false)
                {
                    string orgName = v.ORGName;
                    if (PublicCls.OrgIsZhen(BYORGNO) == false)
                    {
                        if (PublicCls.OrgIsShi(v.BYORGNO))//统计市，即所有县的
                        {
                        }
                        else if (PublicCls.OrgIsXian(v.BYORGNO))//县
                        {
                            orgName = "　　" + orgName;
                        }
                        else
                        {
                            orgName = "　　　　" + orgName;
                        }
                    }
                    row.CreateCell(0).SetCellValue(orgName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.NAME);
                    //row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                row.CreateCell(1).SetCellValue(v.USESTATECount);
                row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 2;//类型开始列
                foreach (var vv in v.USESTATECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.USESTATECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.MANAGERSTATECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.MANAGERSTATCount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.FIRECHANNELLEVELTYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.FIRECHANNELLEVELTYPECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.FIRECHANNELUSERTYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.FIRECHANNELUSERTYPECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                rowI++;
            }
            //}
            //else//查询防火通道详细信息
            //{
            //    if (BYORGNO.Substring(6, 3) == "xxx")
            //    {
            //        BYORGNO = BYORGNO.Substring(0, 6) + "000";
            //    }
            //    row = sheet1.CreateRow(1);
            //    string[] titleArr = new string[] { "单位", "名称", "编号", "使用现状", "维护类型", "防火通道等级类型", "防火通道使用性质" };
            //    for (int i = 0; i < titleArr.Length; i++)
            //    {

            //        row.CreateCell(i).SetCellValue(titleArr[i]);
            //        row.GetCell(i).CellStyle = getCellStyleHead(book);
            //        if (i == 0 || i == 1)
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //        else if (i == 3 || i == 4 || i == 5 || i == 6)
            //            sheet1.SetColumnWidth(i, 25 * 256);//设置宽度
            //        else
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //    }
            //    sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, titleArr.Length - 1));
            //    var list = DC_UTILITY_FIRECHANNELCls.getModelList(new DC_UTILITY_FIRECHANNEL_SW
            //    {
            //        BYORGNO = BYORGNO,
            //        ORGNOSXZ = "1",
            //    });

            //    int rowI = 0;//数据行
            //    foreach (var v in list)//循环获取数据
            //    {
            //        row = sheet1.CreateRow(rowI + 2);
            //        for (int i = 0; i < titleArr.Length; i++)
            //        {
            //            switch (i)
            //            {
            //                case 0:
            //                    row.CreateCell(i).SetCellValue(v.ORGName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 1:
            //                    row.CreateCell(i).SetCellValue(v.NAME);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 2:
            //                    row.CreateCell(i).SetCellValue(v.NUMBER);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 3:
            //                    row.CreateCell(i).SetCellValue(v.USESTATEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 4:
            //                    row.CreateCell(i).SetCellValue(v.MANAGERSTATEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 5:
            //                    row.CreateCell(i).SetCellValue(v.FIRECHANNELLEVELTYPEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 6:
            //                    row.CreateCell(i).SetCellValue(v.FIRECHANNELUSERTYPEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //            }
            //        }
            //        rowI++;
            //    }
            //}
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = vMenu.MENUNAME + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(ms, "application/vnd.ms-excel", fileName);

        }

        #endregion

        #region 防火通道统计
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        //public ActionResult FIRECHANNELCountquery()
        //{
        //    string BYORGNO = Request.Params["BYORGNO"];
        //    string DC_UTILITY_FIRECHANNEL_ID = Request.Params["DC_UTILITY_FIRECHANNEL_ID"];
        //    string VALUE = Request.Params["VALUE"];
        //    string str = ClsStr.EncryptA01(BYORGNO + "|" + DC_UTILITY_FIRECHANNEL_ID + "|" + VALUE, "kkkkkkkk");
        //    return Content(JsonConvert.SerializeObject(new Message(true, "", "/DataCenterCount/FIRECHANNELCount?trans=" + str)), "text/html;charset=UTF-8");
        //}
        /// <summary>
        /// 防火通道统计
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRECHANNELCount()
        {
            pubViewBag("004012", "004012", "");
            if (ViewBag.isPageRight == false)
                return View();
            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }
        /// <summary>
        /// 防火通道统计表格展示
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getFIRECHANNELCountStr(DC_UTILITY_FIRECHANNEL_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.TopORGNO = BYORGNO;
            StringBuilder sb = new StringBuilder();
            string a = DataCenterCountCls.getCount("36");//使用现状
            string b = DataCenterCountCls.getCount("37");//维护类型
            string c = DataCenterCountCls.getCount("38");//防火通道等级类型
            string d = DataCenterCountCls.getCount("39");//防火通道使用性质
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th rowspan=\"2\">单位/名称</th>");
            sb.AppendFormat("        <th rowspan=\"2\">总数</th>");
            sb.AppendFormat("        <th colspan=\"{0}\">使用现状</th>", int.Parse(a));
            sb.AppendFormat("        <th colspan=\"{0}\">维护类型</th>", int.Parse(b));
            sb.AppendFormat("        <th colspan=\"{0}\">防火通道等级类型</th>", int.Parse(c));
            sb.AppendFormat("        <th colspan=\"{0}\">防火通道使用性质</th>", int.Parse(d));
            sb.AppendFormat("    </tr>");
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<FIRECHANNELLEVELTYPECountModel> FIRECHANNELLEVELTYPECountModel;
            IEnumerable<FIRECHANNELUSERTYPECountModel> FIRECHANNELUSERTYPECountModel;
            var list = DataCenterCountCls.getFIRECHANNELModelCount(sw, out USESTATECountModel, out MANAGERSTATECountModel, out FIRECHANNELLEVELTYPECountModel, out FIRECHANNELUSERTYPECountModel);
            sb.AppendFormat("    <tr>");
            foreach (var v in USESTATECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.USESTATENAME);

            }
            foreach (var v in MANAGERSTATECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.MANAGERSTATNAME);

            }
            foreach (var v in FIRECHANNELLEVELTYPECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.FIRECHANNELLEVELTYPENAME);

            }
            foreach (var v in FIRECHANNELUSERTYPECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.FIRECHANNELUSERTYPENAME);
            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                i++;
                string orgName = v.ORGName;
                string orgNo = v.BYORGNO;
                if (string.IsNullOrEmpty(v.BYORGNO))
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.NAME, "");
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        sb.AppendFormat("<tr class='{0}'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<tr class='{0} row1'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }

                }

                string totol = "";
                totol = v.FIRECHANNELUSERTYPECount;
                sb.AppendFormat("<td class=\"center\">{0}</td>", totol);//总数
                var vvList1 = v.USESTATECountModel;
                foreach (var vv in vvList1)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.USESTATECount);
                }
                var vvList2 = v.MANAGERSTATECountModel;
                foreach (var vv in vvList2)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.MANAGERSTATCount);
                }

                var vvList3 = v.FIRECHANNELLEVELTYPECountModel;
                foreach (var vv in vvList3)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.FIRECHANNELLEVELTYPECount);
                }

                var vvList4 = v.FIRECHANNELUSERTYPECountModel;
                foreach (var vv in vvList4)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.FIRECHANNELUSERTYPECount);
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 防火通道详细信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getFIRECHANNELidDetailStr(DC_UTILITY_FIRECHANNEL_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.BYORGNO = BYORGNO;
            sw.ORGNOSXZ = "1";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>名称</th>");
            sb.AppendFormat("        <th>编号</th>");
            sb.AppendFormat("        <th>使用现状</th>");
            sb.AppendFormat("        <th>维护类型</th>");
            sb.AppendFormat("        <th>通道等级类型</th>");
            sb.AppendFormat("        <th>通道使用性质</th>");
            sb.AppendFormat("    </tr>");
            var list = DC_UTILITY_FIRECHANNELCls.getModelList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {

                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.USESTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MANAGERSTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIRECHANNELLEVELTYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIRECHANNELUSERTYPEName);
                sb.AppendFormat("</tr>");
                i++;

            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        #endregion

        #region 防火通道统计图型展示
        /// <summary>
        /// 防火通道统计图型展示
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRECHANNELCountMan()
        {
            pubViewBag("004012", "004012", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.managerstate = getmanagerstate(new T_SYS_DICTSW { DICTTYPEID = "37" });
            ViewBag.fireleveltype = getfireleveltype(new T_SYS_DICTSW { DICTTYPEID = "38" });
            ViewBag.fireusetype = getfireusetype(new T_SYS_DICTSW { DICTTYPEID = "39" });
            ViewBag.usestate = getusestate(new T_SYS_DICTSW { DICTTYPEID = "36" });
            return View();
        }
        private string getfireleveltype(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk1' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        private string getfireusetype(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk3' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        private string getmanagerstate(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk4' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }

        #endregion
        #endregion

        #region 宣传碑牌
        #region 宣传碑牌统计导出
        /// <summary>
        /// 宣传碑牌统计导出
        /// </summary>
        /// <returns></returns>
        public FileResult PROPAGANDASTELECountExportExcel()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            string DC_UTILITY_PROPAGANDASTELE_ID = Request.Params["DC_UTILITY_PROPAGANDASTELE_ID"];
            string VALUE = Request.Params["VALUE"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004013", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            if (string.IsNullOrEmpty(BYORGNO))
                BYORGNO = SystemCls.getCurUserOrgNo();
            //if (PublicCls.OrgIsZhen(BYORGNO) == false)
            //{
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<PROPAGANDASTELETYPECountModel> PROPAGANDASTELETYPECountModel;
            IEnumerable<STRUCTURETYPECountModel> STRUCTURETYPECountModel;
            var list = DataCenterCountCls.getPROPAGANDASTELEModelCount(new DC_UTILITY_PROPAGANDASTELE_SW
            {
                TopORGNO = BYORGNO,
            }, out USESTATECountModel, out MANAGERSTATECountModel, out PROPAGANDASTELETYPECountModel, out STRUCTURETYPECountModel);

            int typeCount1 = 0;//计算类别有多少列
            int typeCount2 = 0;//计算类别有多少列
            int typeCount3 = 0;//计算类别有多少列
            int typeCount4 = 0;//计算类别有多少列
            foreach (var v in PROPAGANDASTELETYPECountModel)
            {
                typeCount1++;
            }
            foreach (var v in USESTATECountModel)
            {
                typeCount2++;
            }
            foreach (var v in MANAGERSTATECountModel)
            {
                typeCount3++;
            }
            foreach (var v in STRUCTURETYPECountModel)
            {
                typeCount4++;
            }
            //设置宽度
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 20 * 256);
            for (int i = 0; i < typeCount1; i++)
            {
                sheet1.SetColumnWidth(i + 2, 20 * 256);
            }
            for (int i = 0; i < typeCount2; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1, 20 * 256);
            }
            for (int i = 0; i < typeCount3; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1 + typeCount2, 20 * 256);
            }
            for (int i = 0; i < typeCount4; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1 + typeCount2 + typeCount3, 20 * 256);
            }
            row = sheet1.CreateRow(1);
            if (PublicCls.OrgIsZhen(BYORGNO) == false)
                row.CreateCell(0).SetCellValue("单位");
            else
                row.CreateCell(0).SetCellValue("名称");
            row.CreateCell(1).SetCellValue("总数");
            row.CreateCell(2).SetCellValue("宣传碑类型");
            row.CreateCell(2 + typeCount1).SetCellValue("使用现状");
            row.CreateCell(2 + typeCount1 + typeCount2).SetCellValue("维护类型");
            row.CreateCell(2 + typeCount1 + typeCount2 + typeCount3).SetCellValue("结构类型");
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            row.GetCell(1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1 + typeCount2).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1 + typeCount2 + typeCount3).CellStyle = getCellStyleTitle(book);
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, typeCount1 + typeCount2 + typeCount3 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 1, 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, typeCount1 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2 + typeCount1, typeCount1 + 1 + typeCount2));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, typeCount1 + 2 + typeCount2, typeCount1 + 1 + typeCount2 + typeCount3));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, typeCount1 + 2 + typeCount2 + typeCount3, typeCount1 + 1 + typeCount2 + typeCount3 + typeCount4));
            row = sheet1.CreateRow(2);
            int indexType = 2;//从第二列开始
            foreach (var v in PROPAGANDASTELETYPECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.PROPAGANDASTELETYPENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in USESTATECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.USESTATENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in MANAGERSTATECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.MANAGERSTATNAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in STRUCTURETYPECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.STRUCTURETYPENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            int rowI = 1;//数据行
            foreach (var v in list)//循环获取数据
            {
                row = sheet1.CreateRow(rowI + 2);
                if (string.IsNullOrEmpty(v.ORGName) == false)
                {
                    string orgName = v.ORGName;
                    if (PublicCls.OrgIsZhen(BYORGNO) == false)
                    {
                        if (PublicCls.OrgIsShi(v.BYORGNO))//统计市，即所有县的
                        {
                        }
                        else if (PublicCls.OrgIsXian(v.BYORGNO))//县
                        {
                            orgName = "　　" + orgName;
                        }
                        else
                        {
                            orgName = "　　　　" + orgName;
                        }
                    }
                    row.CreateCell(0).SetCellValue(orgName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.NAME);
                    //row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                row.CreateCell(1).SetCellValue(v.USESTATECount);
                //row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 2;//类型开始列
                foreach (var vv in v.PROPAGANDASTELETYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.DICTPROPAGANDASTELETYPECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.USESTATECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.USESTATECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.MANAGERSTATECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.MANAGERSTATCount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.STRUCTURETYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.STRUCTURETYPECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                rowI++;
            }
            //}
            //else//查询宣传碑牌详细信息
            //{
            //    if (BYORGNO.Substring(6, 3) == "xxx")
            //    {
            //        BYORGNO = BYORGNO.Substring(0, 6) + "000";
            //    }
            //    row = sheet1.CreateRow(1);
            //    string[] titleArr = new string[] { "单位", "名称", "编号", "宣传碑类型", "结构类型", "使用现状", "维护管理类型", "地址" };
            //    for (int i = 0; i < titleArr.Length; i++)
            //    {

            //        row.CreateCell(i).SetCellValue(titleArr[i]);
            //        row.GetCell(i).CellStyle = getCellStyleHead(book);
            //        if (i == 0 || i == 1)
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //        else if (i == 3 || i == 4 || i == 5 || i == 6)
            //            sheet1.SetColumnWidth(i, 25 * 256);//设置宽度
            //        else
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //    }
            //    sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, titleArr.Length - 1));
            //    var list = DC_UTILITY_PROPAGANDASTELECls.getModelList(new DC_UTILITY_PROPAGANDASTELE_SW
            //    {
            //        BYORGNO = BYORGNO,
            //        ORGNOSXZ = "1",
            //    });

            //    int rowI = 0;//数据行
            //    foreach (var v in list)//循环获取数据
            //    {
            //        row = sheet1.CreateRow(rowI + 2);
            //        for (int i = 0; i < titleArr.Length; i++)
            //        {
            //            switch (i)
            //            {
            //                case 0:
            //                    row.CreateCell(i).SetCellValue(v.ORGName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 1:
            //                    row.CreateCell(i).SetCellValue(v.NAME);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 2:
            //                    row.CreateCell(i).SetCellValue(v.NUMBER);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 3:
            //                    row.CreateCell(i).SetCellValue(v.PROPAGANDASTELETYPEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 4:
            //                    row.CreateCell(i).SetCellValue(v.STRUCTURETYPEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 5:
            //                    row.CreateCell(i).SetCellValue(v.USESTATEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 6:
            //                    row.CreateCell(i).SetCellValue(v.MANAGERSTATEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 7:
            //                    row.CreateCell(i).SetCellValue(v.ADDRESS);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //            }
            //        }
            //        rowI++;
            //    }
            //}
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = vMenu.MENUNAME + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(ms, "application/vnd.ms-excel", fileName);

        }
        #endregion

        #region 宣传碑牌统计
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        //public ActionResult PROPAGANDASTELECountquery()
        //{
        //    string BYORGNO = Request.Params["BYORGNO"];
        //    string DC_UTILITY_PROPAGANDASTELE_ID = Request.Params["DC_UTILITY_PROPAGANDASTELE_ID"];
        //    string VALUE = Request.Params["VALUE"];
        //    string str = ClsStr.EncryptA01(BYORGNO + "|" + DC_UTILITY_PROPAGANDASTELE_ID + "|" + VALUE, "kkkkkkkk");
        //    return Content(JsonConvert.SerializeObject(new Message(true, "", "/DataCenterCount/PROPAGANDASTELECount?trans=" + str)), "text/html;charset=UTF-8");
        //}
        public ActionResult PROPAGANDASTELECount()
        {
            pubViewBag("004013", "004013", "");
            if (ViewBag.isPageRight == false)
                return View();
            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }
        /// <summary>
        /// 宣传碑统计表格展示
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getPROPAGANDASTELECountStr(DC_UTILITY_PROPAGANDASTELE_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.TopORGNO = BYORGNO;
            StringBuilder sb = new StringBuilder();
            string a = DataCenterCountCls.getCount("36");//使用现状
            string b = DataCenterCountCls.getCount("37");//维护类型
            string c = DataCenterCountCls.getCount("40");//宣传碑类型
            string d = DataCenterCountCls.getCount("34");//结构d类型
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th rowspan=\"2\">单位/名称</th>");
            sb.AppendFormat("        <th rowspan=\"2\">总数</th>");
            sb.AppendFormat("        <th colspan=\"{0}\">宣传碑类型</th>", int.Parse(c));
            sb.AppendFormat("        <th colspan=\"{0}\">使用现状</th>", int.Parse(a));
            sb.AppendFormat("        <th colspan=\"{0}\">维护类型</th>", int.Parse(b));
            sb.AppendFormat("        <th colspan=\"{0}\">结构类型</th>", int.Parse(d));
            sb.AppendFormat("    </tr>");
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<PROPAGANDASTELETYPECountModel> PROPAGANDASTELETYPECountModel;
            IEnumerable<STRUCTURETYPECountModel> STRUCTURETYPECountModel;
            var list = DataCenterCountCls.getPROPAGANDASTELEModelCount(sw, out USESTATECountModel, out MANAGERSTATECountModel, out PROPAGANDASTELETYPECountModel, out STRUCTURETYPECountModel);
            sb.AppendFormat("    <tr>");
            foreach (var v in PROPAGANDASTELETYPECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.PROPAGANDASTELETYPENAME);

            }
            foreach (var v in USESTATECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.USESTATENAME);

            }
            foreach (var v in MANAGERSTATECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.MANAGERSTATNAME);

            }
            foreach (var v in STRUCTURETYPECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.STRUCTURETYPENAME);

            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                i++;
                string orgName = v.ORGName;
                string orgNo = v.BYORGNO;
                if (string.IsNullOrEmpty(v.BYORGNO))
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.NAME, "");
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        sb.AppendFormat("<tr class='{0}'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<tr class='{0} row1'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                }
                string totol = "";
                totol = v.PROPAGANDASTELETYPECount;
                sb.AppendFormat("<td class=\"center\">{0}</td>", totol);//总数
                var vvList3 = v.PROPAGANDASTELETYPECountModel;
                foreach (var vv in vvList3)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.DICTPROPAGANDASTELETYPECount);
                }
                var vvList1 = v.USESTATECountModel;
                foreach (var vv in vvList1)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.USESTATECount);
                }
                var vvList2 = v.MANAGERSTATECountModel;
                foreach (var vv in vvList2)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.MANAGERSTATCount);
                }
                var vvList4 = v.STRUCTURETYPECountModel;
                foreach (var vv in vvList4)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.STRUCTURETYPECount);
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 宣传碑牌详细统计
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getPROPAGANDASTELEidDetailStr(DC_UTILITY_PROPAGANDASTELE_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.BYORGNO = BYORGNO;
            sw.ORGNOSXZ = "1";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>名称</th>");
            sb.AppendFormat("        <th>编号</th>");
            sb.AppendFormat("        <th>宣传碑类型</th>");
            sb.AppendFormat("        <th>结构类型</th>");
            sb.AppendFormat("        <th>使用现状</th>");
            sb.AppendFormat("        <th>维护管理类型</th>");
            sb.AppendFormat("        <th>地址</th>");
            sb.AppendFormat("    </tr>");
            var list = DC_UTILITY_PROPAGANDASTELECls.getModelList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {

                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PROPAGANDASTELETYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.STRUCTURETYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.USESTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MANAGERSTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ADDRESS);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");

        }
        #endregion

        #region 宣传碑牌统计图型展示
        /// <summary>
        /// 宣传碑牌图型展示
        /// </summary>
        /// <returns></returns>
        public ActionResult PROPAGANDASTELECountMan()
        {
            pubViewBag("004013", "004013", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.managerstate = getmanagerstate(new T_SYS_DICTSW { DICTTYPEID = "37" });
            ViewBag.structure = getstructure(new T_SYS_DICTSW { DICTTYPEID = "34" });
            ViewBag.usestate = getusestate(new T_SYS_DICTSW { DICTTYPEID = "36" });
            ViewBag.propagandasteletype = getpropagandasteletype(new T_SYS_DICTSW { DICTTYPEID = "40" });
            return View();
        }
        private string getpropagandasteletype(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk1' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        private string getstructure(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk3' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        #endregion

        #endregion

        #region 中继站
        #region 中继站统计导出
        /// <summary>
        /// 中继站统计导出
        /// </summary>
        /// <returns></returns>
        public FileResult RELAYCountExportExcel()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004014", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            if (string.IsNullOrEmpty(BYORGNO))
                BYORGNO = SystemCls.getCurUserOrgNo();
            //if (PublicCls.OrgIsZhen(BYORGNO) == false)
            //{
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<COMMUNICATIONWAYCountModel> COMMUNICATIONWAYCountModel;
            var list = DataCenterCountCls.getRELAYModelCount(new DC_UTILITY_RELAY_SW
            {
                TopORGNO = BYORGNO,
            }, out USESTATECountModel, out MANAGERSTATECountModel, out COMMUNICATIONWAYCountModel);

            int typeCount1 = 0;//计算类别有多少列
            int typeCount2 = 0;//计算类别有多少列
            int typeCount3 = 0;//计算类别有多少列
            foreach (var v in COMMUNICATIONWAYCountModel)
            {
                typeCount1++;
            }
            foreach (var v in USESTATECountModel)
            {
                typeCount2++;
            }
            foreach (var v in MANAGERSTATECountModel)
            {
                typeCount3++;
            }
            //设置宽度
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 20 * 256);
            for (int i = 0; i < typeCount1; i++)
            {
                sheet1.SetColumnWidth(i + 2, 20 * 256);
            }
            for (int i = 0; i < typeCount2; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1, 20 * 256);
            }
            for (int i = 0; i < typeCount3; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1 + typeCount2, 20 * 256);
            }
            row = sheet1.CreateRow(1);
            if (PublicCls.OrgIsZhen(BYORGNO) == false)
                row.CreateCell(0).SetCellValue("单位");
            else
                row.CreateCell(0).SetCellValue("名称");
            row.CreateCell(1).SetCellValue("总数");
            row.CreateCell(2).SetCellValue("通讯方式");
            row.CreateCell(2 + typeCount1).SetCellValue("使用现状");
            row.CreateCell(2 + typeCount1 + typeCount2).SetCellValue("维护类型");
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            row.GetCell(1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1 + typeCount2).CellStyle = getCellStyleTitle(book);
            //row.GetCell(2 + typeCount1 + typeCount2 + typeCount3).CellStyle = getCellStyleTitle(book);
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, typeCount1 + typeCount2 + typeCount3 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 1, 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, typeCount1 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2 + typeCount1, typeCount1 + 1 + typeCount2));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, typeCount1 + 2 + typeCount2, typeCount1 + 1 + typeCount2 + typeCount3));
            row = sheet1.CreateRow(2);
            int indexType = 2;//从第二列开始
            foreach (var v in COMMUNICATIONWAYCountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.COMMUNICATIONWAYNAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in USESTATECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.USESTATENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in MANAGERSTATECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.MANAGERSTATNAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }

            int rowI = 1;//数据行
            foreach (var v in list)//循环获取数据
            {
                row = sheet1.CreateRow(rowI + 2);
                if (string.IsNullOrEmpty(v.ORGName) == false)
                {
                    string orgName = v.ORGName;
                    if (PublicCls.OrgIsZhen(BYORGNO) == false)
                    {
                        if (PublicCls.OrgIsShi(v.BYORGNO))//统计市，即所有县的
                        {
                        }
                        else if (PublicCls.OrgIsXian(v.BYORGNO))//县
                        {
                            orgName = "　　" + orgName;
                        }
                        else
                        {
                            orgName = "　　　　" + orgName;
                        }
                    }
                    row.CreateCell(0).SetCellValue(orgName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.NAME);
                    //row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                row.CreateCell(1).SetCellValue(v.USESTATECount);
                //row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 2;//类型开始列
                foreach (var vv in v.COMMUNICATIONWAYCountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.DICTCOMMUNICATIONWAYCount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.USESTATECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.USESTATECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.MANAGERSTATECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.MANAGERSTATCount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                rowI++;
            }
            //}
            //else//查询中继站详细信息
            //{
            //    if (BYORGNO.Substring(6, 3) == "xxx")
            //    {
            //        BYORGNO = BYORGNO.Substring(0, 6) + "000";
            //    }
            //    row = sheet1.CreateRow(1);
            //    string[] titleArr = new string[] { "单位", "名称", "编号", "型号", "通讯方式", "使用现状", "维护管理类型", "地址" };
            //    for (int i = 0; i < titleArr.Length; i++)
            //    {

            //        row.CreateCell(i).SetCellValue(titleArr[i]);
            //        row.GetCell(i).CellStyle = getCellStyleHead(book);
            //        if (i == 0 || i == 1)
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //        else if (i == 3 || i == 4 || i == 5 || i == 6)
            //            sheet1.SetColumnWidth(i, 25 * 256);//设置宽度
            //        else
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //    }
            //    sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, titleArr.Length - 1));
            //    var list = DC_UTILITY_RELAYCls.getModelList(new DC_UTILITY_RELAY_SW
            //    {
            //        BYORGNO = BYORGNO,
            //        ORGNOSXZ = "1",
            //    });

            //    int rowI = 0;//数据行
            //    foreach (var v in list)//循环获取数据
            //    {
            //        row = sheet1.CreateRow(rowI + 2);
            //        for (int i = 0; i < titleArr.Length; i++)
            //        {
            //            switch (i)
            //            {
            //                case 0:
            //                    row.CreateCell(i).SetCellValue(v.ORGName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 1:
            //                    row.CreateCell(i).SetCellValue(v.NAME);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 2:
            //                    row.CreateCell(i).SetCellValue(v.NUMBER);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 3:
            //                    row.CreateCell(i).SetCellValue(v.MODEL);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 4:
            //                    row.CreateCell(i).SetCellValue(v.COMMUNICATIONWAYName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 5:
            //                    row.CreateCell(i).SetCellValue(v.USESTATEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 6:
            //                    row.CreateCell(i).SetCellValue(v.MANAGERSTATEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 7:
            //                    row.CreateCell(i).SetCellValue(v.ADDRESS);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //            }
            //        }
            //        rowI++;
            //    }
            //}
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = vMenu.MENUNAME + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(ms, "application/vnd.ms-excel", fileName);

        }
        #endregion

        #region 中继站统计
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        //public ActionResult RELAYCountquery()
        //{
        //    string BYORGNO = Request.Params["BYORGNO"];
        //    string DC_UTILITY_RELAY_ID = Request.Params["DC_UTILITY_RELAY_ID"];
        //    string VALUE = Request.Params["VALUE"];
        //    string str = ClsStr.EncryptA01(BYORGNO + "|" + DC_UTILITY_RELAY_ID + "|" + VALUE, "kkkkkkkk");
        //    return Content(JsonConvert.SerializeObject(new Message(true, "", "/DataCenterCount/RELAYCount?trans=" + str)), "text/html;charset=UTF-8");
        //}
        public ActionResult RELAYCount()
        {
            pubViewBag("004014", "004014", "");
            if (ViewBag.isPageRight == false)
                return View();
            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }
        /// <summary>
        /// 宣传碑统计表格展示
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getRELAYCountStr(DC_UTILITY_RELAY_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.TopORGNO = BYORGNO;
            StringBuilder sb = new StringBuilder();
            string a = DataCenterCountCls.getCount("36");//使用现状
            string b = DataCenterCountCls.getCount("37");//维护类型
            string c = DataCenterCountCls.getCount("41");//宣传碑类型
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th rowspan=\"2\">单位/名称</th>");
            sb.AppendFormat("        <th rowspan=\"2\">总数</th>");
            sb.AppendFormat("        <th colspan=\"{0}\">通讯方式</th>", int.Parse(c));
            sb.AppendFormat("        <th colspan=\"{0}\">使用现状</th>", int.Parse(a));
            sb.AppendFormat("        <th colspan=\"{0}\">维护类型</th>", int.Parse(b));
            sb.AppendFormat("    </tr>");
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<COMMUNICATIONWAYCountModel> COMMUNICATIONWAYCountModel;
            var list = DataCenterCountCls.getRELAYModelCount(sw, out USESTATECountModel, out MANAGERSTATECountModel, out COMMUNICATIONWAYCountModel);
            sb.AppendFormat("    <tr>");
            foreach (var v in COMMUNICATIONWAYCountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.COMMUNICATIONWAYNAME);

            }
            foreach (var v in USESTATECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.USESTATENAME);

            }
            foreach (var v in MANAGERSTATECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.MANAGERSTATNAME);

            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                i++;
                string orgName = v.ORGName;
                string orgNo = v.BYORGNO;
                if (string.IsNullOrEmpty(v.BYORGNO))
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.NAME, "");
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        sb.AppendFormat("<tr class='{0}'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<tr class='{0} row1'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                }

                string totol = "";
                totol = v.COMMUNICATIONWAYCount;
                sb.AppendFormat("<td class=\"center\">{0}</td>", totol);//总数
                var vvList3 = v.COMMUNICATIONWAYCountModel;
                foreach (var vv in vvList3)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.DICTCOMMUNICATIONWAYCount);
                }
                var vvList1 = v.USESTATECountModel;
                foreach (var vv in vvList1)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.USESTATECount);
                }
                var vvList2 = v.MANAGERSTATECountModel;
                foreach (var vv in vvList2)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.MANAGERSTATCount);
                }

                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        public ActionResult getRELAYidDetailStr(DC_UTILITY_RELAY_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.BYORGNO = BYORGNO;
            sw.ORGNOSXZ = "1";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>名称</th>");
            sb.AppendFormat("        <th>编号</th>");
            sb.AppendFormat("        <th>型号</th>");
            sb.AppendFormat("        <th>通讯方式</th>");
            sb.AppendFormat("        <th>使用现状</th>");
            sb.AppendFormat("        <th>维护类型</th>");
            sb.AppendFormat("        <th>地址</th>");
            sb.AppendFormat("    </tr>");
            var list = DC_UTILITY_RELAYCls.getModelList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {

                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MODEL);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.COMMUNICATIONWAYName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.USESTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MANAGERSTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ADDRESS);
                sb.AppendFormat("</tr>");
                i++;

            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        #endregion

        #region 中继站统计图型展示
        /// <summary>
        /// 中继站统计图型展示
        /// </summary>
        /// <returns></returns>
        public ActionResult RELAYCountMan()
        {
            pubViewBag("004014", "004014", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.managerstate = getmanagerstate(new T_SYS_DICTSW { DICTTYPEID = "37" });
            ViewBag.communicationway = getcommunicationway(new T_SYS_DICTSW { DICTTYPEID = "41" });
            ViewBag.usestate = getusestate(new T_SYS_DICTSW { DICTTYPEID = "36" });
            return View();
        }
        private string getcommunicationway(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk1' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        #endregion
        #endregion

        #region 监测站
        #region 监测站导出excel
        /// <summary>
        /// 监测站导出excel
        /// </summary>
        /// <returns></returns>
        public FileResult MONITORINGSTATIONCountExportExcel()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            string DC_UTILITY_MONITORINGSTATION_ID = Request.Params["DC_UTILITY_MONITORINGSTATION_ID"];
            string VALUE = Request.Params["VALUE"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004015", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            if (string.IsNullOrEmpty(BYORGNO))
                BYORGNO = SystemCls.getCurUserOrgNo();
            //if (PublicCls.OrgIsZhen(BYORGNO) == false)
            //{
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<TRANSFERMODETYPECountModel> TRANSFERMODETYPECountModel;
            var list = DataCenterCountCls.getMONITORINGSTATIONModelCount(new DC_UTILITY_MONITORINGSTATION_SW
            {
                TopORGNO = BYORGNO,
            }, out USESTATECountModel, out MANAGERSTATECountModel, out TRANSFERMODETYPECountModel);

            int typeCount1 = 0;//计算类别有多少列
            int typeCount2 = 0;//计算类别有多少列
            int typeCount3 = 0;//计算类别有多少列
            foreach (var v in TRANSFERMODETYPECountModel)
            {
                typeCount1++;
            }
            foreach (var v in USESTATECountModel)
            {
                typeCount2++;
            }
            foreach (var v in MANAGERSTATECountModel)
            {
                typeCount3++;
            }
            //设置宽度
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 20 * 256);
            for (int i = 0; i < typeCount1; i++)
            {
                sheet1.SetColumnWidth(i + 2, 20 * 256);
            }
            for (int i = 0; i < typeCount2; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1, 20 * 256);
            }
            for (int i = 0; i < typeCount3; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1 + typeCount2, 20 * 256);
            }
            row = sheet1.CreateRow(1);
            if (PublicCls.OrgIsZhen(BYORGNO) == false)
                row.CreateCell(0).SetCellValue("单位");
            else
                row.CreateCell(0).SetCellValue("名称");
            row.CreateCell(1).SetCellValue("总数");
            row.CreateCell(2).SetCellValue("无线电传输方式");
            row.CreateCell(2 + typeCount1).SetCellValue("使用现状");
            row.CreateCell(2 + typeCount1 + typeCount2).SetCellValue("维护类型");
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            row.GetCell(1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1 + typeCount2).CellStyle = getCellStyleTitle(book);
            //row.GetCell(2 + typeCount1 + typeCount2 + typeCount3).CellStyle = getCellStyleTitle(book);
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, typeCount1 + typeCount2 + typeCount3 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 1, 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, typeCount1 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2 + typeCount1, typeCount1 + 1 + typeCount2));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, typeCount1 + 2 + typeCount2, typeCount1 + 1 + typeCount2 + typeCount3));
            row = sheet1.CreateRow(2);
            int indexType = 2;//从第二列开始
            foreach (var v in TRANSFERMODETYPECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.TRANSFERMODETYPENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in USESTATECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.USESTATENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in MANAGERSTATECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.MANAGERSTATNAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }

            int rowI = 1;//数据行
            foreach (var v in list)//循环获取数据
            {
                row = sheet1.CreateRow(rowI + 2);
                if (string.IsNullOrEmpty(v.ORGName) == false)
                {
                    string orgName = v.ORGName;
                    if (PublicCls.OrgIsZhen(BYORGNO) == false)
                    {
                        if (PublicCls.OrgIsShi(v.BYORGNO))//统计市，即所有县的
                        {
                        }
                        else if (PublicCls.OrgIsXian(v.BYORGNO))//县
                        {
                            orgName = "　　" + orgName;
                        }
                        else
                        {
                            orgName = "　　　　" + orgName;
                        }
                    }
                    row.CreateCell(0).SetCellValue(orgName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.NAME);
                    //row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                row.CreateCell(1).SetCellValue(v.USESTATECount);
                //row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 2;//类型开始列
                foreach (var vv in v.TRANSFERMODETYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.DICTTRANSFERMODETYPECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.USESTATECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.USESTATECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.MANAGERSTATECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.MANAGERSTATCount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                rowI++;
            }
            //}
            //else//查询监测站详细信息
            //{
            //    if (BYORGNO.Substring(6, 3) == "xxx")
            //    {
            //        BYORGNO = BYORGNO.Substring(0, 6) + "000";
            //    }
            //    row = sheet1.CreateRow(1);
            //    string[] titleArr = new string[] { "单位", "名称", "编号", "型号", "传输方式", "使用现状", "维护管理类型", "地址" };
            //    for (int i = 0; i < titleArr.Length; i++)
            //    {

            //        row.CreateCell(i).SetCellValue(titleArr[i]);
            //        row.GetCell(i).CellStyle = getCellStyleHead(book);
            //        if (i == 0 || i == 1)
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //        else if (i == 3 || i == 4 || i == 5 || i == 6)
            //            sheet1.SetColumnWidth(i, 25 * 256);//设置宽度
            //        else
            //            sheet1.SetColumnWidth(i, 20 * 256);//设置宽度
            //    }
            //    sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, titleArr.Length - 1));
            //    var list = DC_UTILITY_MONITORINGSTATIONCls.getModelList(new DC_UTILITY_MONITORINGSTATION_SW
            //    {
            //        BYORGNO = BYORGNO,
            //        ORGNOSXZ = "1",
            //    });

            //    int rowI = 0;//数据行
            //    foreach (var v in list)//循环获取数据
            //    {
            //        row = sheet1.CreateRow(rowI + 2);
            //        for (int i = 0; i < titleArr.Length; i++)
            //        {
            //            switch (i)
            //            {
            //                case 0:
            //                    row.CreateCell(i).SetCellValue(v.ORGName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 1:
            //                    row.CreateCell(i).SetCellValue(v.NAME);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 2:
            //                    row.CreateCell(i).SetCellValue(v.NUMBER);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 3:
            //                    row.CreateCell(i).SetCellValue(v.MODEL);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 4:
            //                    row.CreateCell(i).SetCellValue(v.TRANSFERMODETYPEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 5:
            //                    row.CreateCell(i).SetCellValue(v.USESTATEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 6:
            //                    row.CreateCell(i).SetCellValue(v.MANAGERSTATEName);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //                case 7:
            //                    row.CreateCell(i).SetCellValue(v.ADDRESS);
            //                    row.GetCell(i).CellStyle = getCellStyleCenter(book);
            //                    break;
            //            }
            //        }
            //        rowI++;
            //    }
            //}
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = vMenu.MENUNAME + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(ms, "application/vnd.ms-excel", fileName);

        }
        #endregion

        #region 监测站统计
        ///// <summary>
        ///// 查询
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult MONITORINGSTATIONCountquery()
        //{
        //    string BYORGNO = Request.Params["BYORGNO"];
        //    string DC_UTILITY_MONITORINGSTATION_ID = Request.Params["DC_UTILITY_MONITORINGSTATION_ID"];
        //    string VALUE = Request.Params["VALUE"];
        //    string str = ClsStr.EncryptA01(BYORGNO + "|" + DC_UTILITY_MONITORINGSTATION_ID + "|" + VALUE, "kkkkkkkk");
        //    return Content(JsonConvert.SerializeObject(new Message(true, "", "/DataCenterCount/MONITORINGSTATIONCount?trans=" + str)), "text/html;charset=UTF-8");
        //}
        public ActionResult MONITORINGSTATIONCount()
        {
            pubViewBag("004015", "004015", "");
            if (ViewBag.isPageRight == false)
                return View();
            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }
        /// <summary>
        /// 监测站统计表格展示
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getMONITORINGSTATIONCountStr(DC_UTILITY_MONITORINGSTATION_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.TopORGNO = BYORGNO;
            StringBuilder sb = new StringBuilder();
            string a = DataCenterCountCls.getCount("36");//使用现状
            string b = DataCenterCountCls.getCount("37");//维护类型
            string c = DataCenterCountCls.getCount("42");//无线电传输方式
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th rowspan=\"2\">单位/名称</th>");
            sb.AppendFormat("        <th rowspan=\"2\">总数</th>");
            sb.AppendFormat("        <th colspan=\"{0}\">无线电传输方式</th>", int.Parse(c));
            sb.AppendFormat("        <th colspan=\"{0}\">使用现状</th>", int.Parse(a));
            sb.AppendFormat("        <th colspan=\"{0}\">维护类型</th>", int.Parse(b));
            sb.AppendFormat("    </tr>");
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<TRANSFERMODETYPECountModel> TRANSFERMODETYPECountModel;
            var list = DataCenterCountCls.getMONITORINGSTATIONModelCount(sw, out USESTATECountModel, out MANAGERSTATECountModel, out TRANSFERMODETYPECountModel);
            sb.AppendFormat("    <tr>");
            foreach (var v in TRANSFERMODETYPECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.TRANSFERMODETYPENAME);

            }
            foreach (var v in USESTATECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.USESTATENAME);

            }
            foreach (var v in MANAGERSTATECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.MANAGERSTATNAME);

            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                i++;
                string orgName = v.ORGName;
                string orgNo = v.BYORGNO;

                if (string.IsNullOrEmpty(v.BYORGNO))
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.NAME, "");
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        sb.AppendFormat("<tr class='{0}'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<tr class='{0} row1'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }

                }

                string totol = "";
                totol = v.TRANSFERMODETYPECount;
                sb.AppendFormat("<td class=\"center\">{0}</td>", totol);//总数
                var vvList3 = v.TRANSFERMODETYPECountModel;
                foreach (var vv in vvList3)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.DICTTRANSFERMODETYPECount);
                }
                var vvList1 = v.USESTATECountModel;
                foreach (var vv in vvList1)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.USESTATECount);
                }
                var vvList2 = v.MANAGERSTATECountModel;
                foreach (var vv in vvList2)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.MANAGERSTATCount);
                }

                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 监测站详细信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getMONITORINGSTATIONRidDetailStr(DC_UTILITY_MONITORINGSTATION_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.BYORGNO = BYORGNO;
            sw.ORGNOSXZ = "1";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>名称</th>");
            sb.AppendFormat("        <th>编号</th>");
            sb.AppendFormat("        <th>型号</th>");
            sb.AppendFormat("        <th>传输方式</th>");
            sb.AppendFormat("        <th>使用现状</th>");
            sb.AppendFormat("        <th>维护类型</th>");
            sb.AppendFormat("        <th>地址</th>");
            sb.AppendFormat("    </tr>");
            var list = DC_UTILITY_MONITORINGSTATIONCls.getModelList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {

                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MODEL);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.TRANSFERMODETYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.USESTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MANAGERSTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ADDRESS);
                sb.AppendFormat("</tr>");
                i++;

            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");

        }
        #endregion

        #region 监测站统计图型展示
        /// <summary>
        /// 监测站统计图型展示
        /// </summary>
        /// <returns></returns>
        public ActionResult MONITORINGSTATIONCountMan()
        {
            pubViewBag("004015", "004015", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.managerstate = getmanagerstate(new T_SYS_DICTSW { DICTTYPEID = "37" });
            ViewBag.transfermodetype = gettransfermodetype(new T_SYS_DICTSW { DICTTYPEID = "42" });
            ViewBag.usestate = getusestate(new T_SYS_DICTSW { DICTTYPEID = "36" });
            return View();
        }
        private string gettransfermodetype(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk1' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        #endregion

        #endregion

        #region 因子采集站
        #region 因子采集站导出excel
        /// <summary>
        /// 监测站导出excel
        /// </summary>
        /// <returns></returns>
        public FileResult FACTORCOLLECTSTATIONCountExportExcel()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004016", SYSFLAG = ConfigCls.getSystemFlag() });
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            if (string.IsNullOrEmpty(BYORGNO))
                BYORGNO = SystemCls.getCurUserOrgNo();
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<TRANSFERMODETYPECountModel> TRANSFERMODETYPECountModel;
            var list = DataCenterCountCls.getFACTORCOLLECTSTATIONModelCount(new DC_UTILITY_FACTORCOLLECTSTATION_SW
            {
                TopORGNO = BYORGNO,
            }, out USESTATECountModel, out MANAGERSTATECountModel, out TRANSFERMODETYPECountModel);

            int typeCount1 = 0;//计算类别有多少列
            int typeCount2 = 0;//计算类别有多少列
            int typeCount3 = 0;//计算类别有多少列
            foreach (var v in TRANSFERMODETYPECountModel)
            {
                typeCount1++;
            }
            foreach (var v in USESTATECountModel)
            {
                typeCount2++;
            }
            foreach (var v in MANAGERSTATECountModel)
            {
                typeCount3++;
            }
            //设置宽度
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 20 * 256);
            for (int i = 0; i < typeCount1; i++)
            {
                sheet1.SetColumnWidth(i + 2, 20 * 256);
            }
            for (int i = 0; i < typeCount2; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1, 20 * 256);
            }
            for (int i = 0; i < typeCount3; i++)
            {
                sheet1.SetColumnWidth(i + 2 + typeCount1 + typeCount2, 20 * 256);
            }
            row = sheet1.CreateRow(1);
            if (PublicCls.OrgIsZhen(BYORGNO) == false)
                row.CreateCell(0).SetCellValue("单位");
            else
                row.CreateCell(0).SetCellValue("名称");
            row.CreateCell(1).SetCellValue("总数");
            row.CreateCell(2).SetCellValue("无线电传输方式");
            row.CreateCell(2 + typeCount1).SetCellValue("使用现状");
            row.CreateCell(2 + typeCount1 + typeCount2).SetCellValue("维护类型");
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            row.GetCell(1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1).CellStyle = getCellStyleTitle(book);
            row.GetCell(2 + typeCount1 + typeCount2).CellStyle = getCellStyleTitle(book);
            //row.GetCell(2 + typeCount1 + typeCount2 + typeCount3).CellStyle = getCellStyleTitle(book);
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, typeCount1 + typeCount2 + typeCount3 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 1, 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, typeCount1 + 1));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2 + typeCount1, typeCount1 + 1 + typeCount2));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, typeCount1 + 2 + typeCount2, typeCount1 + 1 + typeCount2 + typeCount3));
            row = sheet1.CreateRow(2);
            int indexType = 2;//从第二列开始
            foreach (var v in TRANSFERMODETYPECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.TRANSFERMODETYPENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in USESTATECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.USESTATENAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }
            foreach (var v in MANAGERSTATECountModel)
            {
                row.CreateCell(indexType).SetCellValue(v.MANAGERSTATNAME);
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                indexType++;
            }

            int rowI = 1;//数据行
            foreach (var v in list)//循环获取数据
            {
                row = sheet1.CreateRow(rowI + 2);
                if (string.IsNullOrEmpty(v.ORGName) == false)
                {
                    string orgName = v.ORGName;
                    if (PublicCls.OrgIsZhen(BYORGNO) == false)
                    {
                        if (PublicCls.OrgIsShi(v.BYORGNO))//统计市，即所有县的
                        {
                        }
                        else if (PublicCls.OrgIsXian(v.BYORGNO))//县
                        {
                            orgName = "　　" + orgName;
                        }
                        else
                        {
                            orgName = "　　　　" + orgName;
                        }
                    }
                    row.CreateCell(0).SetCellValue(orgName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.NAME);
                    //row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                row.CreateCell(1).SetCellValue(v.USESTATECount);
                //row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 2;//类型开始列
                foreach (var vv in v.TRANSFERMODETYPECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.DICTTRANSFERMODETYPECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.USESTATECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.USESTATECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    TypeI++;
                }
                foreach (var vv in v.MANAGERSTATECountModel)
                {
                    row.CreateCell(TypeI).SetCellValue(vv.MANAGERSTATCount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
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

        #region 因子采集站统计
        public ActionResult FACTORCOLLECTSTATIONCount()
        {
            pubViewBag("004016", "004016", "");
            if (ViewBag.isPageRight == false)
                return View();

            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }
        /// <summary>
        /// 因子采集站统计表格展示
        /// </summary> 
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getFACTORCOLLECTSTATIONCountStr(DC_UTILITY_FACTORCOLLECTSTATION_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.TopORGNO = BYORGNO;
            StringBuilder sb = new StringBuilder();
            string a = DataCenterCountCls.getCount("36");//使用现状
            string b = DataCenterCountCls.getCount("37");//维护类型
            string c = DataCenterCountCls.getCount("42");//无线电传输方式
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th rowspan=\"2\">单位/名称</th>");
            sb.AppendFormat("        <th rowspan=\"2\">总数</th>");
            sb.AppendFormat("        <th colspan=\"{0}\">无线电传输方式</th>", int.Parse(c));
            sb.AppendFormat("        <th colspan=\"{0}\">使用现状</th>", int.Parse(a));
            sb.AppendFormat("        <th colspan=\"{0}\">维护类型</th>", int.Parse(b));
            sb.AppendFormat("    </tr>");
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<TRANSFERMODETYPECountModel> TRANSFERMODETYPECountModel;
            var list = DataCenterCountCls.getFACTORCOLLECTSTATIONModelCount(sw, out USESTATECountModel, out MANAGERSTATECountModel, out TRANSFERMODETYPECountModel);
            sb.AppendFormat("    <tr>");
            foreach (var v in TRANSFERMODETYPECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.TRANSFERMODETYPENAME);

            }
            foreach (var v in USESTATECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.USESTATENAME);

            }
            foreach (var v in MANAGERSTATECountModel)
            {
                sb.AppendFormat("        <th>{0}</th>", v.MANAGERSTATNAME);

            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in list)
            {
                string orgName = v.ORGName;
                string orgNo = v.BYORGNO;
                if (string.IsNullOrEmpty(v.BYORGNO))
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.NAME, "");
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        sb.AppendFormat("<tr class='{0}'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<tr class='{0} row1'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                        if ((PublicCls.OrgIsZhen(v.BYORGNO) == false && v.BYORGNO == sw.TopORGNO && v.ORGName.IndexOf("合计") == -1) || PublicCls.OrgIsZhen(v.BYORGNO))
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                        }
                    }
                   
                }
                string totol = "";
                totol = v.TRANSFERMODETYPECount;
                sb.AppendFormat("<td class=\"center\">{0}</td>", totol);//总数
                var vvList3 = v.TRANSFERMODETYPECountModel;
                foreach (var vv in vvList3)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.DICTTRANSFERMODETYPECount);
                }
                var vvList1 = v.USESTATECountModel;
                foreach (var vv in vvList1)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.USESTATECount);
                }
                var vvList2 = v.MANAGERSTATECountModel;
                foreach (var vv in vvList2)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", vv.MANAGERSTATCount);
                }

                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 监测站详细
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getFACTORCOLLECTSTATIONidDetailStr(DC_UTILITY_FACTORCOLLECTSTATION_SW sw)
        {
            string BYORGNO = Request.Params["BYORGNO"];
            sw.BYORGNO = BYORGNO;
            sw.ORGNOSXZ = "1";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>名称</th>");
            sb.AppendFormat("        <th>编号</th>");
            sb.AppendFormat("        <th>型号</th>");
            sb.AppendFormat("        <th>采集内容</th>");
            sb.AppendFormat("        <th>传输方式</th>");
            sb.AppendFormat("        <th>使用现状</th>");
            sb.AppendFormat("        <th>维护类型</th>");
            sb.AppendFormat("        <th>地址</th>");
            sb.AppendFormat("    </tr>");
            var list = DC_UTILITY_FACTORCOLLECTSTATIONCls.getModelList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {

                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MODEL);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.FACTCOLLCONTENT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.TRANSFERMODETYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.USESTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MANAGERSTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ADDRESS);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");

        }
        #endregion

        #region 因子采集站统计图型展示
        /// <summary>
        /// 因子采集站统计图型展示
        /// </summary>
        /// <returns></returns>
        public ActionResult FACTORCOLLECTSTATIONCountMan()
        {
            pubViewBag("004016", "004016", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.managerstate = getmanagerstate(new T_SYS_DICTSW { DICTTYPEID = "37" });
            ViewBag.transfermodetype = gettransfermodetype(new T_SYS_DICTSW { DICTTYPEID = "42" });
            ViewBag.usestate = getusestate(new T_SYS_DICTSW { DICTTYPEID = "36" });
            return View();
        }
        #endregion
        #endregion

        #endregion

        #region 档案统计
        #region 档案导出
        /// <summary>
        /// 档案导出excel
        /// </summary>
        /// <returns></returns>
        public FileResult ArchivalCountExportExcel()
        {
            string BYORGNO = Request.Params["BYORGNO"];
            string TYPE = Request.Params["TYPE"];
            string TIMEBegin = Request.Params["TIMEBegin"];
            string TIMEEnd = Request.Params["TIMEEnd"];
            var vMenu = T_SYS_MENUCls.getModel(new T_SYS_MENU_SW { MENUCODE = "004017", SYSFLAG = ConfigCls.getSystemFlag() });
            //string type1 = "";
            //string type2 = "";
            //string type3 = "";
            //string[] arr = TYPE.Split(',');
            //type1 = arr[0];
            //type2 = arr[1];
            //type3 = arr[2];
            //vMenu.MENUNAME 页面/菜单名称
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue(vMenu.MENUNAME);
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            if (string.IsNullOrEmpty(BYORGNO))
                BYORGNO = SystemCls.getCurUserOrgNo();

            IEnumerable<HOTTYPECountModel> HOTTYPECountModel;
            IEnumerable<FIRELEVELCountModel> FIRELEVELCountModel;

            var list = DataCenterCountCls.getArchivalModelCount(new JC_FIRE_SW
            {
                TopORGNO = BYORGNO,
                BeginTime = TIMEBegin,
                EndTime = TIMEEnd,
            }, out HOTTYPECountModel, out FIRELEVELCountModel);

            int typeCount1 = 0;//计算类别有多少列
            int typeCount2 = 0;//计算类别有多少列
            int typeCount3 = 0;//计算类别有多少列
            if (TYPE == "1")
            {
                foreach (var v in HOTTYPECountModel)
                {
                    typeCount1++;
                }
                typeCount1 = typeCount1 + 1;
            }
            if (TYPE == "2")
            {
                foreach (var v in FIRELEVELCountModel)
                {
                    typeCount2++;
                }
                typeCount2 = typeCount2 + 1;
            }
            if (TYPE == "3")
            {
                typeCount3 = 8;
            }
            //设置宽度
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 20 * 256);
            if (TYPE == "1")
            {
                for (int i = 0; i < typeCount1; i++)
                {
                    sheet1.SetColumnWidth(i + 1, 20 * 256);
                }
            }
            if (TYPE == "2")
            {
                for (int i = 0; i < typeCount2; i++)
                {
                    sheet1.SetColumnWidth(i + 1 + typeCount1, 20 * 256);
                }
            }
            if (TYPE == "3")
            {
                for (int i = 0; i < typeCount2; i++)
                {
                    sheet1.SetColumnWidth(i + 1 + typeCount1 + typeCount2, 20 * 256);
                }
            }
            row = sheet1.CreateRow(1);
            if (PublicCls.OrgIsZhen(BYORGNO) == false)
                row.CreateCell(0).SetCellValue("单位");
            else
                row.CreateCell(0).SetCellValue("名称");
            row.GetCell(0).CellStyle = getCellStyleTitle(book);
            if (TYPE == "1")
            {
                row.CreateCell(1).SetCellValue("热点类别");
                row.GetCell(1).CellStyle = getCellStyleTitle(book);
            }
            if (TYPE == "2")
            {
                row.CreateCell(typeCount1 + 1).SetCellValue("火灾等级");
                row.GetCell(typeCount1 + 1).CellStyle = getCellStyleTitle(book);
            }
            if (TYPE == "3")
            {
                row.CreateCell(typeCount1 + 1 + typeCount2).SetCellValue("火情来源");
                row.GetCell(typeCount1 + 1 + typeCount2).CellStyle = getCellStyleTitle(book);
            }
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, typeCount1 + typeCount2 + 1 + typeCount3));
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
            if (TYPE == "1")
            {
                sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 1, typeCount1));
            }
            if (TYPE == "2")
            {
                sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 1 + typeCount1, typeCount1 + typeCount2));
            }
            if (TYPE == "3")
            {
                sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 1 + typeCount1 + typeCount2, typeCount1 + typeCount2 + typeCount3));
            }
            row = sheet1.CreateRow(2);
            int indexType = 1;//从第二列开始
            if (TYPE == "1")
            {
                row.CreateCell(indexType).SetCellValue("总数");
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                foreach (var v in HOTTYPECountModel)
                {
                    row.CreateCell(indexType + 1).SetCellValue(v.HOTTYPEname);
                    row.GetCell(indexType + 1).CellStyle = getCellStyleHead(book);
                    indexType++;
                }
                indexType = indexType + 1;
            }
            if (TYPE == "2")
            {
                row.CreateCell(indexType).SetCellValue("总数");
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                foreach (var v in FIRELEVELCountModel)
                {
                    row.CreateCell(indexType + 1).SetCellValue(v.FIRELEVELname);
                    row.GetCell(indexType + 1).CellStyle = getCellStyleHead(book);
                    indexType++;
                }
                indexType = indexType + 1;
            }
            if (TYPE == "3")
            {
                row.CreateCell(indexType).SetCellValue("总数");
                row.CreateCell(indexType + 1).SetCellValue("红外相机");
                row.CreateCell(indexType + 2).SetCellValue("电话报警");
                row.CreateCell(indexType + 3).SetCellValue("卫星热点");
                row.CreateCell(indexType + 4).SetCellValue("电子监控");
                row.CreateCell(indexType + 5).SetCellValue("护林员火情");
                row.CreateCell(indexType + 6).SetCellValue("无人机巡护");
                row.CreateCell(indexType + 7).SetCellValue("历史补录");
                row.GetCell(indexType).CellStyle = getCellStyleHead(book);
                row.GetCell(indexType + 1).CellStyle = getCellStyleHead(book);
                row.GetCell(indexType + 2).CellStyle = getCellStyleHead(book);
                row.GetCell(indexType + 3).CellStyle = getCellStyleHead(book);
                row.GetCell(indexType + 4).CellStyle = getCellStyleHead(book);
                row.GetCell(indexType + 5).CellStyle = getCellStyleHead(book);
                row.GetCell(indexType + 6).CellStyle = getCellStyleHead(book);
                row.GetCell(indexType + 7).CellStyle = getCellStyleHead(book);
            }
            int rowI = 1;//数据行
            foreach (var v in list)//循环获取数据
            {
                row = sheet1.CreateRow(rowI + 2);
                if (string.IsNullOrEmpty(v.ORGName) == false)
                {
                    string orgName = v.ORGName;
                    if (PublicCls.OrgIsZhen(BYORGNO) == false)
                    {
                        if (PublicCls.OrgIsShi(v.BYORGNO))//统计市，即所有县的
                        {
                        }
                        else if (PublicCls.OrgIsXian(v.BYORGNO))//县
                        {
                            orgName = "　　" + orgName;
                        }
                        else
                        {
                            orgName = "　　　　" + orgName;
                        }
                    }
                    row.CreateCell(0).SetCellValue(orgName);
                    row.GetCell(0).CellStyle = getCellStyleLeft(book);
                }
                else
                {
                    row.CreateCell(0).SetCellValue(v.NAME);
                    //row.GetCell(0).CellStyle = getCellStyleCenter(book);
                }
                //row.CreateCell(1).SetCellValue(v.);
                //row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int TypeI = 1;//类型开始列
                if (TYPE == "1")
                {
                    row.CreateCell(TypeI).SetCellValue(v.HOTTYPECount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    foreach (var vv in v.HOTTYPECountModel)
                    {
                        row.CreateCell(TypeI + 1).SetCellValue(vv.DictHOTTYPECount);
                        //row.GetCell(TypeI + 1).CellStyle = getCellStyleCenter(book);
                        TypeI++;
                    }
                    TypeI = TypeI + 1;
                }
                if (TYPE == "2")
                {
                    row.CreateCell(TypeI).SetCellValue(v.FIRELEVELCount);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    foreach (var vv in v.FIRELEVELCountModel)
                    {
                        row.CreateCell(TypeI + 1).SetCellValue(vv.DictFIRELEVELCount);
                        //row.GetCell(TypeI + 1).CellStyle = getCellStyleCenter(book);
                        TypeI++;
                    }
                    TypeI = TypeI + 1;
                }
                if (TYPE == "3")
                {
                    row.CreateCell(TypeI).SetCellValue(v.FIREFROMCount);
                    row.CreateCell(TypeI + 1).SetCellValue(v.FIREFROMCount1);
                    row.CreateCell(TypeI + 2).SetCellValue(v.FIREFROMCount2);
                    row.CreateCell(TypeI + 3).SetCellValue(v.FIREFROMCount3);
                    row.CreateCell(TypeI + 4).SetCellValue(v.FIREFROMCount4);
                    row.CreateCell(TypeI + 5).SetCellValue(v.FIREFROMCount5);
                    row.CreateCell(TypeI + 6).SetCellValue(v.FIREFROMCount6);
                    row.CreateCell(TypeI + 7).SetCellValue(v.FIREFROMCount7);
                    //row.GetCell(TypeI).CellStyle = getCellStyleCenter(book);
                    //row.GetCell(TypeI + 1).CellStyle = getCellStyleCenter(book);
                    //row.GetCell(TypeI + 2).CellStyle = getCellStyleCenter(book);
                    //row.GetCell(TypeI + 3).CellStyle = getCellStyleCenter(book);
                    //row.GetCell(TypeI + 4).CellStyle = getCellStyleCenter(book);
                    //row.GetCell(TypeI + 5).CellStyle = getCellStyleCenter(book);
                    //row.GetCell(TypeI + 6).CellStyle = getCellStyleCenter(book);
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

        #region 档案统计
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        //public ActionResult ArchivalCountquery()
        //{
        //    string BYORGNO = Request.Params["BYORGNO"];
        //    string type1 = "";
        //    string type2 = "";
        //    string type3 = "";
        //    string TIMEBegin = Request.Params["TIMEBegin"];
        //    string TIMEEnd = Request.Params["TIMEEnd"];
        //    string TYPE = Request.Params["TYPE"];
        //    string[] arr = TYPE.Split(',');
        //    type1 = arr[0];
        //    type2 = arr[1];
        //    type3 = arr[2];
        //    if (type1 == "0" && type2 == "0" && type3 == "0")
        //    {
        //        return Content(JsonConvert.SerializeObject(new Message(false, "请必须至少选择一种类型查询", "")), "text/html;charset=UTF-8");
        //    }
        //    else
        //    {
        //        //string str = ClsStr.EncryptA01(BYORGNO + "|" + type1 + "|" + type2 + "|" + type3 + "|" + TIMEBegin + "|" + TIMEEnd, "kkkkkkkk");
        //        string str = ClsStr.EncryptA01(BYORGNO + "|" + TYPE + "|" + TIMEBegin + "|" + TIMEEnd, "kkkkkkkk");
        //        return Content(JsonConvert.SerializeObject(new Message(true, "", "/DataCenterCount/ArchivalCount?trans=" + str)), "text/html;charset=UTF-8");
        //    }
        //}
        /// <summary>
        /// 档案统计
        /// </summary>
        /// <returns></returns>
        public ActionResult ArchivalCount()
        {
            pubViewBag("004017", "004017", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.TIMEBegin = DateTime.Now.ToString("yyyy-MM-01");
            ViewBag.TIMEEnd = DateTime.Now.ToString("yyyy-MM-dd");

            //组织机构下拉框
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            return View();
        }
        public ActionResult getArchivalCountStr(JC_FIRE_SW sw)
        {
            sw.BeginTime = Request.Params["TIMEBegin"];
            sw.EndTime = Request.Params["TIMEEnd"];
            sw.TYPE = Request.Params["TYPE"];
            sw.TopORGNO = Request.Params["BYORGNO"];
            StringBuilder sb = new StringBuilder();
            string a = DataCenterCountCls.getCount("10");//热点类别类型
            string b = DataCenterCountCls.getCount("22");//火险等级类别
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th rowspan=\"2\">单位/名称</th>");
            if ((sw.TYPE) == "1")
            {
                sb.AppendFormat("        <th colspan=\"{0}\">热点类别</th>", int.Parse(a) + 1);
            }
            if ((sw.TYPE) == "2")
            {
                sb.AppendFormat("        <th colspan=\"{0}\">火险等级</th>", int.Parse(b) + 1);
            }
            if ((sw.TYPE) == "3")
            {
                sb.AppendFormat("        <th colspan=\"8\">火情来源</th>");
            }
            sb.AppendFormat("    </tr>");
            IEnumerable<HOTTYPECountModel> HOTTYPECountModel;
            IEnumerable<FIRELEVELCountModel> FIRELEVELCountModel;
            var list = DataCenterCountCls.getArchivalModelCount(sw, out HOTTYPECountModel, out FIRELEVELCountModel);
            sb.AppendFormat("    <tr>");
            if ((sw.TYPE) == "1")
            {
                sb.AppendFormat("        <th>总数</th>");
                foreach (var v in HOTTYPECountModel)
                {
                    sb.AppendFormat("        <th>{0}</th>", v.HOTTYPEname);

                }
            }
            if ((sw.TYPE) == "2")
            {
                sb.AppendFormat("        <th>总数</th>");
                foreach (var v in FIRELEVELCountModel)
                {
                    sb.AppendFormat("        <th>{0}</th>", v.FIRELEVELname);

                }
            }
            if ((sw.TYPE) == "3")
            {
                sb.AppendFormat("        <th>总数</th>");
                sb.AppendFormat("        <th>红外相机</th>");
                sb.AppendFormat("        <th>电话报警</th>");
                sb.AppendFormat("        <th>卫星热点</th>");
                sb.AppendFormat("        <th>电子监控</th>");
                sb.AppendFormat("        <th>护林员火情</th>");
                sb.AppendFormat("        <th>无人机巡护</th>");
                sb.AppendFormat("        <th>历史补录</th>");
            }
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");

            foreach (var v in list)
            {
                string orgName = v.ORGName;
                string orgNo = v.BYORGNO;
                if (string.IsNullOrEmpty(v.BYORGNO))
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.NAME, "");
                }
                else
                {
                    sb.AppendFormat("<tr class='{0}'>", PublicCls.getOrgTrClass(sw.TopORGNO, orgNo));
                    if (PublicCls.OrgIsZhen(v.BYORGNO))
                    {
                        sb.AppendFormat("<td class=\"left\" style=\"{1}\"><a href=\"#\" onclick=\"Layer('{2}','{3}')\">{0}</a></td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo), v.BYORGNO, v.ORGName);
                    }
                    else
                    {
                        sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", orgName, PublicCls.getOrgTDNameClass(sw.TopORGNO, orgNo));
                    }
                }
                if ((sw.TYPE) == "1")
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.HOTTYPECount);//总数
                    var vvList1 = v.HOTTYPECountModel;
                    foreach (var vv in vvList1)
                    {
                        sb.AppendFormat("<td class=\"center\">{0}</td>", vv.DictHOTTYPECount);
                    }
                }
                if ((sw.TYPE) == "2")
                {
                    var vvList2 = v.FIRELEVELCountModel;
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIRELEVELCount);//总数
                    foreach (var vv in vvList2)
                    {
                        sb.AppendFormat("<td class=\"center\">{0}</td>", vv.DictFIRELEVELCount);
                    }
                }
                if ((sw.TYPE) == "3")
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIREFROMCount);//总数
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIREFROMCount1);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIREFROMCount2);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIREFROMCount3);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIREFROMCount4);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIREFROMCount5);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIREFROMCount6);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIREFROMCount7);
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取镇的档案详情
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public ActionResult getArchivalidDetailStr(JC_FIRE_SW sw)
        {
            sw.ISOUTFIRE = "1";
            sw.BYORGNO = Request.Params["BYORGNO"];
            sw.BeginTime = Request.Params["TIMEBegin"];
            sw.EndTime = Request.Params["TIMEEnd"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"tb\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr>");
            sb.AppendFormat("        <th>序号</th>");
            sb.AppendFormat("        <th>名称</th>");
            sb.AppendFormat("        <th>火情来源</th>");
            sb.AppendFormat("        <th>热点类别</th>");
            sb.AppendFormat("        <th>火灾等级</th>");
            sb.AppendFormat("        <th>起火时间</th>");
            sb.AppendFormat("        <th>火灾发生地</th>");
            sb.AppendFormat("    </tr>");
            var list = JC_FIRECls.getList(sw);
            int i = 1;
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var v in list)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIRENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIREFROMName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.HOTTYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIRELEVELName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIRETIME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ZQWZ);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        #region 档案统计Echarts
        public ActionResult ArchivalCountMan()
        {
            pubViewBag("004017", "004017", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.hotetype = gethotetype(new T_SYS_DICTSW { DICTTYPEID = "10" });
            ViewBag.firelevel = getfirelevel(new T_SYS_DICTSW { DICTTYPEID = "22" });
            ViewBag.firefrom = getfirefrom(new T_SYS_DICTSW { DICTTYPEID = "99" });
            return View();
        }
        private string gethotetype(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk1' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        private string getfirelevel(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk2' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        private string getfirefrom(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();

            var result = T_SYS_DICTCls.getListModel(sw);

            if (result.Count() > 0)
            {
                foreach (var v in result)
                {
                    sb.AppendFormat("<input type='checkbox' name='chk3' value='" + v.DICTVALUE + "'  checked=\"checked\" />{0}", v.DICTNAME);
                }
            }
            return sb.ToString();
        }
        #endregion
        #endregion
        #endregion
    }
}
