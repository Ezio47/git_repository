using System;
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
using Omu.ValueInjecter;
using System.Text;
using ManagerSystem.MVC.Models;
using ManagerSystemModel.SDEModel;
using ManagerSystem.MVC.HelpCom;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace ManagerSystem.MVC.Controllers
{
    /// <summary>
    /// 火情档案管理和报表
    /// </summary>
    public class FIRERECORDController : BaseController
    {
        #region 火情档案管理
        /// <summary>
        /// 火情档案管理--数据增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_FIREINFOManager()
        {
            FIRERECORD_FIREINFO_Model m = new FIRERECORD_FIREINFO_Model();
            m.JCFID = Request.Params["JCFID"];
            m.FRFIID = Request.Params["FRFIID"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.FIRECODE = Request.Params["FIRECODE"];
            m.FIREADDRESSCOUNTY = Request.Params["FIREADDRESSCOUNTY"];
            m.FIREADDRESSTOWNS = Request.Params["FIREADDRESSTOWNS"];
            // m.FIREADDRESSCOUNTY = T_SYS_ORGCls.getorgname(Request.Params["FIREADDRESSCOUNTY"]);//根据组织机构编码获取组织机构名
            // m.FIREADDRESSTOWNS = T_SYS_ORGCls.getorgname(Request.Params["FIREADDRESSTOWNS"]);
            m.FIREADDRESSVILLAGES = Request.Params["FIREADDRESSVILLAGES"];
            m.FIREADDRESS = Request.Params["FIREADDRESS"];
            m.FIRETIME = Request.Params["FIRETIME"];
            m.FIREENDTIME = Request.Params["FIREENDTIME"];
            m.FIRERECINFO000 = Request.Params["FIRERECINFO000"];
            m.FIRERECINFO001 = Request.Params["FIRERECINFO001"];
            m.FIRERECINFO020 = Request.Params["FIRERECINFO020"];
            m.FIRERECINFO021 = Request.Params["FIRERECINFO021"];
            m.FIRERECINFO030 = Request.Params["FIRERECINFO030"];
            m.FIRERECINFO031 = Request.Params["FIRERECINFO031"];
            m.FIRERECINFO032 = Request.Params["FIRERECINFO032"];
            m.FIRERECINFO040 = Request.Params["FIRERECINFO040"];
            m.FIRERECINFO041 = Request.Params["FIRERECINFO041"];
            m.FIRERECINFO050 = Request.Params["FIRERECINFO050"];
            m.FIRERECINFO051 = Request.Params["FIRERECINFO051"];
            m.FIRERECINFO060 = Request.Params["FIRERECINFO060"];
            m.FIRERECINFO061 = Request.Params["FIRERECINFO061"];
            m.FIRERECINFO070 = Request.Params["FIRERECINFO070"];
            m.FIRERECINFO071 = Request.Params["FIRERECINFO071"];
            m.FIRERECINFO072 = Request.Params["FIRERECINFO072"];
            m.FIRERECINFO080 = Request.Params["FIRERECINFO080"];
            m.FIRERECINFO081 = Request.Params["FIRERECINFO081"];
            m.FIRERECINFO082 = Request.Params["FIRERECINFO082"];
            m.FIRERECINFO090 = Request.Params["FIRERECINFO090"];
            m.FIRERECINFO100 = Request.Params["FIRERECINFO100"];
            m.FIRERECINFO110 = Request.Params["FIRERECINFO110"];
            m.FIRERECINFO111 = Request.Params["FIRERECINFO111"];
            m.FIRERECINFO120 = Request.Params["FIRERECINFO120"];
            m.FIRERECINFO130 = Request.Params["FIRERECINFO130"];
            m.FIRERECINFO140 = Request.Params["FIRERECINFO140"];
            m.FIRERECINFO150 = Request.Params["FIRERECINFO150"];
            m.FIRERECINFO160 = Request.Params["FIRERECINFO160"];
            m.FIRELOSEAREA = Request.Params["FIRELOSEAREA"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.opMethod = Request.Params["Method"];
            m.Shape = "geometry::STGeomFromText('POINT(" + m.JD + " " + m.WD + ")',4326)";
            return Content(JsonConvert.SerializeObject(FIRERECORD_FIREINFOCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 火情档案
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_FIREINFOMan()
        {
            string Method = Request.Params["Method"];
            string FRFIID = Request.Params["FRFIID"];
            string JCFID = Request.Params["JCFID"];
            FIRERECORD_FIREINFO_Model model = new FIRERECORD_FIREINFO_Model();
            if (!string.IsNullOrEmpty(JCFID))
            {
                if (Method == "Mdy" || Method == "See1")
                {
                    model = FIRERECORD_FIREINFOCls.getModel(new FIRERECORD_FIREINFO_SW { JCFID = JCFID });
                }
            }
            ViewBag.FIRERECINFO000 = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "301", DICTVALUE = model.FIRERECINFO000 });//火灾等级
            ViewBag.FIRERECINFO001 = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "304", DICTVALUE = model.FIRERECINFO001 });//火灾种类
            ViewBag.FIRERECINFO080 = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "203", DICTVALUE = model.FIRERECINFO080 });//火案查处是否已经处理
            ViewBag.FIRERECINFO140 = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "203", DICTVALUE = model.FIRERECINFO140 });//火源是否查明
            ViewBag.FIRERECINFO160 = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "302", DICTVALUE = model.FIRERECINFO160, STANDBY1InName = "1" });//火源
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), OnlyGetShi = SystemCls.getCurUserOrgNo(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.FIREADDRESSCOUNTY = T_SYS_ORGCls.getSelectOptionByORGNO(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = model.FIREADDRESSCOUNTY });
            ViewBag.FIREADDRESSTOWNS = T_SYS_ORGCls.getSelectOptionByORGNO(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = model.FIREADDRESSTOWNS });
            ViewBag.Method = Method;
            ViewBag.FRFIID = FRFIID;
            ViewBag.JCFID = JCFID;
            ViewBag.StartTime = ClsSwitch.SwitTM(DateTime.Now.ToString());
            ViewBag.EndTime = ClsSwitch.SwitTM(DateTime.Now.ToString());
            // ViewBag.isAdd = (SystemCls.isRight("011002002")) ? "1" : "0";
            return View(model);
        }

        /// <summary>
        /// 火情档案数据查看
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_FIREINFOSee1()
        {
            string JCFID = Request.Params["JCFID"];
            FIRERECORD_FIREINFO_Model m = FIRERECORD_FIREINFOCls.getModel(new FIRERECORD_FIREINFO_SW { JCFID = JCFID });
            JC_FIRE_Model s = JC_FIRECls.getModel(new JC_FIRE_SW { JCFID = JCFID });
            StringBuilder sb = new StringBuilder();
            string FIREADDRESSCOUNTY = T_SYS_ORGCls.getorgname(m.FIREADDRESSCOUNTY);//根据组织机构获取县组织名称
            string FIREADDRESSTOWNS = T_SYS_ORGCls.getorgname(m.FIREADDRESSTOWNS);//根据组织机构获取乡组织名称
            m.FIRERECINFO001 = T_SYS_DICTCls.getDicName(new T_SYS_DICTSW { DICTTYPEID = "304", DICTVALUE = (m.FIRERECINFO001) });//获取火灾等级名称
            m.FIRERECINFO000 = T_SYS_DICTCls.getDicName(new T_SYS_DICTSW { DICTTYPEID = "301", DICTVALUE = (m.FIRERECINFO000) });//获取火灾种类名称
            m.FIRERECINFO080 = T_SYS_DICTCls.getDicName(new T_SYS_DICTSW { DICTTYPEID = "203", DICTVALUE = (m.FIRERECINFO080) });//火案查处是否已经处理
            m.FIRERECINFO140 = T_SYS_DICTCls.getDicName(new T_SYS_DICTSW { DICTTYPEID = "203", DICTVALUE = (m.FIRERECINFO140) });//火源是否查明
            sb.AppendFormat("<div class=\"divMan\" >");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\"style=\"text-align:left\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td colspan=\"6\"style=\" height:40px;\" ><h1 style=\" width:82px;  height: 28px;line-height: 28px;color: #22a306;border: 1px solid #35b719; border-radius: 12px;background: url(../images/ico/firereport_icon.png) 7px 6px no-repeat; padding-left: 28px;font-size: 15px;\">基 本 信 息 </h1></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>起火地点:</td>");
            sb.AppendFormat("<td  colspan=\"2\">{0}</td>", FIREADDRESSCOUNTY + FIREADDRESSTOWNS + m.FIREADDRESSVILLAGES);
            sb.AppendFormat("<td>详细地址:</td>");
            sb.AppendFormat("<td colspan=\"2\">{0}</td>", m.FIREADDRESS);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td style=\" width:15%;\">火灾编号:</td>");
            sb.AppendFormat("<td style=\" width:14%;\">{0}</td>", m.FIRECODE);
            sb.AppendFormat("<td style=\" width:15%;\">起火时间:</td>");
            sb.AppendFormat("<td style=\" width:16%;\">{0}</td>", m.FIRETIME);
            sb.AppendFormat("<td style=\" width:15%;\">灭火时间:</td>");
            sb.AppendFormat("<td style=\" width:16%;\">{0}</td>", m.FIREENDTIME);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>火灾等级:</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO001);
            sb.AppendFormat("<td>火灾种类:</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO000);
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>已查明火源:</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO140);
            sb.AppendFormat("<td>火源:</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO150);
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td colspan=\"6\" style=\" height:40px;\"><h1 style=\" width:82px;  height: 28px;line-height: 28px;color: #22a306;border: 1px solid #35b719; border-radius: 12px;background: url(../images/ico/firereport_icon.png) 7px 6px no-repeat; padding-left: 28px;font-size: 15px;\">火 场 信 息 </h1></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>受害森林面积(公顷):</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRELOSEAREA);
            sb.AppendFormat("<td>火场总面积(公顷):</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO020);
            sb.AppendFormat("<td>有林地面积(公顷):</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO021);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>原始林受灾面积(公顷):</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO030);
            sb.AppendFormat("<td>次生林成灾面积(公顷):</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO031);
            sb.AppendFormat("<td>人工林成灾面积(公顷):</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO032);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>成林蓄积损失(立方米):</td>");
            sb.AppendFormat("<td >{0}</td>", m.FIRERECINFO040);
            sb.AppendFormat("<td>幼林株数损失(万株):</td>");
            sb.AppendFormat("<td >{0}</td>", m.FIRERECINFO041);
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>林龄:</td>");
            sb.AppendFormat("<td >{0}</td>", m.FIRERECINFO051);
            sb.AppendFormat("<td>林分组成:</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO050);
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td colspan=\"6\"  style=\" height:40px;\"><h1 style=\" width:82px;  height: 28px;line-height: 28px;color: #22a306;border: 1px solid #35b719; border-radius: 12px;background: url(../images/ico/firereport_icon.png) 7px 6px no-repeat; padding-left: 28px;font-size: 15px;\">扑 救 信 息 </h1></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>火场指挥员姓名:</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO060);
            sb.AppendFormat("<td>火场指挥员职务:</td>");
            sb.AppendFormat("<td >{0}</td>", m.FIRERECINFO061);
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>火案查处已处理:</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO080);
            sb.AppendFormat("<td>林政处罚人数(人):</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO081);
            sb.AppendFormat("<td>刑事处罚人数(人):</td>");
            sb.AppendFormat("<td >{0}</td>", m.FIRERECINFO082);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>人员伤亡轻伤人数(人):</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO070);
            sb.AppendFormat("<td>人员伤亡重伤人数(人):</td>");
            sb.AppendFormat("<td >{0}</td>", m.FIRERECINFO071);
            sb.AppendFormat("<td>人员伤亡亡(人):</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO072);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>扑火经费(万元):</td>");
            sb.AppendFormat("<td >{0}</td>", m.FIRERECINFO130);
            sb.AppendFormat("<td>其他损失折款(万元):</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO090);
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>出动扑火人工(工日):</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO100);
            sb.AppendFormat("<td>出动飞机(架次):</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO120);
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>出动车辆合计(台):</td>");
            sb.AppendFormat("<td >{0}</td>", m.FIRERECINFO111);
            sb.AppendFormat("<td>其中汽车(台):</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIRERECINFO110);
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>经度:</td>");
            sb.AppendFormat("<td>{0}</td>", s.JD);
            sb.AppendFormat("<td>纬度:</td>");
            sb.AppendFormat("<td>{0}</td>", s.WD);
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.See1 = sb.ToString();
            return View();
        }

        /// <summary>
        /// 异步加载县
        /// </summary>
        /// <returns></returns>
        public ActionResult loadXIAN()
        {
            StringBuilder sb = new StringBuilder();
            string ORGNO = Request.Params["ORGNO"];
            sb.AppendFormat(T_SYS_ORGCls.getSelectOptionByORGNO(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopSXORGNO = ORGNO, CurORGNO = ORGNO }));
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 异步加载乡镇
        /// </summary>
        /// <returns></returns>
        public ActionResult loadXZ()
        {
            StringBuilder sb = new StringBuilder();
            string SHICODE = Request.Params["SHICODE"];
            //sb.AppendFormat(T_SYS_ORGCls.getSelectOptionByORGNO(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), GetXZOrgNOByConty = SHICODE,CurORGNO = SHICODE }));
            sb.AppendFormat(T_SYS_ORGCls.getSelectOptionByORGNO(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), GetXZOrgNOByConty = SHICODE, CurORGNO = SHICODE, TopORGNO = SHICODE }));
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_FIREINFOjson()
        {
            string JCFID = Request.Params["JCFID"];
            return Content(JsonConvert.SerializeObject(FIRERECORD_FIREINFOCls.getModel(new FIRERECORD_FIREINFO_SW { JCFID = JCFID })), "text/html;charset=UTF-8");
        }
        #endregion

        #region 林火1表月报表
        /// <summary>
        /// 林火1表月报表
        /// </summary>
        /// <returns></returns>
        public ActionResult MONTHLYREPORT()
        {
            pubViewBag("041001", "041001", "林火1表月报表");
            if (ViewBag.isPageRight == false)
                return View();
            if (PublicCls.OrgIsShi(SystemCls.getCurUserOrgNo()))//如果是州级获取市县,否则取乡镇
                ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo(), TopORGNO = SystemCls.getCurUserOrgNo(), OnlyGetShiXian = "1" });
            else
                ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo(), TopORGNO = SystemCls.getCurUserOrgNo(), OnlyGetShiXianXZ = "1", GetXZOrgNOByConty = SystemCls.getCurUserOrgNo(), });
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM");
            return View();
        }
        /// <summary>
        /// 林火1表月报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult MONTHLYREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string Time = Request.Params["Time"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            string nowFireTime;
            string nowfireEndTime;
            string fireTime;
            string fireEndTime;
            LYReportQueryTerm(Time, out nowFireTime, out nowfireEndTime, out fireTime, out fireEndTime);

            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result;
            List<FIRERECORD_FIREINFO_Model> _monthlyreportList;
            List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList;
            LYReportQueryData(ORGNO, nowFireTime, nowfireEndTime, fireTime, fireEndTime, out result, out _monthlyreportList, out _nowmonthlyreportList);
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"MONTHLYREPORTTable\"  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            string headname = "地&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;级</br></br>或&nbsp;&nbsp;县&nbsp;&nbsp;级</br></br>名&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;称";
            sb.AppendFormat(HEADER(headname));
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");

            #region 第一行列号
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat(COLUMN());
            sb.AppendFormat("</tr>");
            #endregion

            #region 一至本月累计
            sb.AppendFormat("<tr class=\"center\" >");
            sb.AppendFormat("<td  class=\"center\">一至本月累计</td>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", _monthlyreportList.Count);
            sb.AppendFormat(TOTAL(_monthlyreportList));
            #endregion

            #region 本月合计
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td  class=\"center\">本月合计</td>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList.Count);
            sb.AppendFormat(TOTAL(_nowmonthlyreportList));
            #endregion

            #region 详细数据行
            for (int i = 1; i < result.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", result[i].ORGNAME);
                //sb.AppendFormat("<td class=\"left\" style=\"{1}\" >{0}</td>", result[i].ORGNAME, PublicCls.getOrgTDNameClass(ORGNO, result[i].ORGNO));组织机构的渐进格式
                List<FIRERECORD_FIREINFO_Model> templist = LYReportTemplist(result, _nowmonthlyreportList, i);
                sb.AppendFormat("<td class=\"center\">{0}</td>", templist.Count);
                sb.AppendFormat(TOTAL(templist));
            }
            #endregion

            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult MONTHLYREPORTExportExcel()
        {
            #region 数据查询条件
            string Time = Request.Params["Time"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            string nowFireTime;
            string nowfireEndTime;
            string fireTime;
            string fireEndTime;
            LYReportQueryTerm(Time, out nowFireTime, out nowfireEndTime, out fireTime, out fireEndTime);
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result;
            List<FIRERECORD_FIREINFO_Model> _monthlyreportList;
            List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList;
            LYReportQueryData(ORGNO, nowFireTime, nowfireEndTime, fireTime, fireEndTime, out result, out _monthlyreportList, out _nowmonthlyreportList);
            List<T_SYS_DICTModel> dic304 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "304" }).ToList();
            int colsCount = dic304.Count + 18;
            string orgName = T_SYS_ORGCls.getorgname(ORGNO);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "041001", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + Time + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("月报表");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            string headname = "地级\n或县级\n名称";
            CreatExcelHead(dic304, colsCount, title, book, sheet1, headname);
            #endregion

            #region 表身及数据

            #region 一至本月累计
            IRow row4 = sheet1.CreateRow(4);
            int j = 0;
            row4.CreateCell(j).SetCellValue("一至本月累计");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(_monthlyreportList.Count);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            for (int d1 = 0; d1 < dic304.Count; d1++)
            {
                var templist = _monthlyreportList.FindAll(a => a.FIRERECINFO001 == dic304[d1].DICTVALUE);
                row4.CreateCell(j).SetCellValue(templist.Count);
                row4.GetCell(j).CellStyle = getCellStyleCenter(book);
                j++;
            }
            j = ExcelTotal(_monthlyreportList, book, row4, j);
            #endregion

            #region 本月合计
            IRow row5 = sheet1.CreateRow(5);
            int k = 0;
            row5.CreateCell(k).SetCellValue("本月合计");
            row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            k++;
            row5.CreateCell(k).SetCellValue(_nowmonthlyreportList.Count);
            row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            k++;
            for (int d1 = 0; d1 < dic304.Count; d1++)
            {
                var templist = _nowmonthlyreportList.FindAll(a => a.FIRERECINFO001 == dic304[d1].DICTVALUE);
                row5.CreateCell(k).SetCellValue(templist.Count);
                row5.GetCell(k).CellStyle = getCellStyleCenter(book);
                k++;
            }
            k = ExcelTotal(_nowmonthlyreportList, book, row5, k);
            //List<float> acountlist1 = CalZJ(_nowmonthlyreportList);
            //row5.CreateCell(k).SetCellValue(acountlist1[0] > 0 ? string.Format("{0:0.00}", acountlist1[0]) : "0");
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[1] + acountlist1[2] > 0 ? string.Format("{0:0.00}", acountlist1[1] + acountlist1[2]) : "0");
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[1] > 0 ? string.Format("{0:0.00}", acountlist1[1]) : "0");
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[2] > 0 ? string.Format("{0:0.00}", acountlist1[2]) : "0");
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[3] > 0 ? string.Format("{0:0.00}", acountlist1[3]) : "0");
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[4] > 0 ? string.Format("{0:0.00}", acountlist1[4]) : "0");
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[5] + acountlist1[6] + acountlist1[7]);
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[5]);
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[6]);
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[7]);
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[8] > 0 ? string.Format("{0:0.00}", acountlist1[8]) : "0");
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[9] > 0 ? string.Format("{0:0.00}", acountlist1[9]) : "0");
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[19]);
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[10]);
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[11]);
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            //row5.CreateCell(k).SetCellValue(acountlist1[12] > 0 ? string.Format("{0:0.00}", acountlist1[12]) : "0");
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            #endregion

            #region 详细数据
            int rowIndex = 0;
            for (int i = 1; i < result.Count; i++)
            {
                int z = 0;
                IRow row6 = sheet1.CreateRow(rowIndex + 6);
                if (PublicCls.OrgIsShi(result[i].ORGNO)) { result[i].ORGNAME = "  " + result[i].ORGNAME; }
                else if (PublicCls.OrgIsXian(result[i].ORGNO)) { result[i].ORGNAME = "  " + result[i].ORGNAME; }
                else { result[i].ORGNAME = "  " + result[i].ORGNAME; }
                row6.CreateCell(z).SetCellValue(result[i].ORGNAME);
                row6.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                List<FIRERECORD_FIREINFO_Model> templist = LYReportTemplist(result, _nowmonthlyreportList, i);
                row6.CreateCell(z).SetCellValue(templist.Count);
                row6.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                for (int d1 = 0; d1 < dic304.Count; d1++)
                {
                    var v = templist.FindAll(a => a.FIRERECINFO001 == dic304[d1].DICTVALUE);
                    row6.CreateCell(z).SetCellValue(v.Count);
                    row6.GetCell(z).CellStyle = getCellStyleCenter(book);
                    z++;
                }
                z = ExcelTotal(templist, book, row6, z);
                rowIndex++;
            }
            #endregion

            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }

        #endregion

        #region 林火1表分地区报表
        /// <summary>
        /// 林火1表表分地区报表
        /// </summary>
        /// <returns></returns>
        public ActionResult AREAREPORT()
        {
            pubViewBag("041002", "041002", "林火1表分地区报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.YEAR = DateTime.Now.ToString("yyyy");
            return View();
        }

        /// <summary>
        /// 林火1表分地区表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult AREAREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            DateTime startYear = new DateTime(int.Parse(YEAR), 1, 1);  //本年年初
            DateTime endYear = new DateTime(int.Parse(YEAR), 12, 31).AddDays(1).AddSeconds(-1);  //本年年末
            string fireTime = startYear.ToString();
            string fireEndTime = endYear.ToString();
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = new List<T_SYS_ORGModel>();
            if (PublicCls.OrgIsShi(ORGNO))
                result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = ORGNO, OnlyGetShiXian = "1" }).ToList();
            if (PublicCls.OrgIsXian(ORGNO))
                result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = ORGNO, OnlyGetShiXianXZ = "1" }).ToList();
            if (PublicCls.OrgIsZhen(ORGNO))
                result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = ORGNO }).ToList();
            List<FIRERECORD_FIREINFO_Model> _list = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = fireTime, FIREENDTIME = fireEndTime }).ToList();
            List<T_SYS_DICTModel> dic304 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "304" }).ToList();
            #endregion

            #region 数据表
            sb.AppendFormat("<table  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            string headname = "地&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;级</br></br>或&nbsp;&nbsp;县&nbsp;&nbsp;级</br></br>名&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;称";
            sb.AppendFormat(HEADER(headname));
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");

            #region 第一行列号
            sb.AppendFormat("<tr>");
            sb.AppendFormat(COLUMN());
            sb.AppendFormat("</tr>");
            #endregion

            #region 第二行：全年累计
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td class=\"center\">全年累计</td>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", _list.Count);
            sb.AppendFormat(TOTAL(_list));
            #endregion

            #region 详细数据行
            for (int i = 1; i < result.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", result[i].ORGNAME);
                List<FIRERECORD_FIREINFO_Model> templist = LYReportTemplist(result, _list, i);
                //List<FIRERECORD_FIREINFO_Model> templist = new List<FIRERECORD_FIREINFO_Model>();
                //if (PublicCls.OrgIsShi(result[i].ORGNO))
                //    templist = _list.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 4) == result[i].ORGNO.Substring(0, 4)).ToList();
                //if (PublicCls.OrgIsXian(result[i].ORGNO))
                //    templist = _list.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                //if (PublicCls.OrgIsZhen(result[i].ORGNO))
                //    templist = _list.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                //if (PublicCls.OrgIsCun(result[i].ORGNO))
                //    templist = _list.FindAll(a => a.FIREADDRESSTOWNS == result[i].ORGNO).ToList();
                sb.AppendFormat("<td class=\"center\">{0}</td>", templist.Count);
                sb.AppendFormat(TOTAL(templist));
            }
            #endregion

            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult AREAREPORTExportExcel()
        {
            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            DateTime startYear = new DateTime(int.Parse(YEAR), 1, 1);  //本年年初
            DateTime endYear = new DateTime(int.Parse(YEAR), 12, 31).AddDays(1).AddSeconds(-1);  //本年年末
            string fireTime = startYear.ToString();
            string fireEndTime = endYear.ToString();
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = new List<T_SYS_ORGModel>();
            if (PublicCls.OrgIsShi(ORGNO))
                result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = ORGNO, OnlyGetShiXian = "1" }).ToList();
            if (PublicCls.OrgIsXian(ORGNO))
                result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = ORGNO, OnlyGetShiXianXZ = "1" }).ToList();
            if (PublicCls.OrgIsZhen(ORGNO))
                result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = ORGNO }).ToList();
            List<FIRERECORD_FIREINFO_Model> _list = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = fireTime, FIREENDTIME = fireEndTime }).ToList();
            List<T_SYS_DICTModel> dic304 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "304" }).ToList();
            int colsCount = dic304.Count + 18;
            string orgName = T_SYS_ORGCls.getorgname(ORGNO);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "041002", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + YEAR + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("分地区表");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            string headname = "地级\n或县级\n名称";
            CreatExcelHead(dic304, colsCount, title, book, sheet1, headname);
            #endregion

            #region 表身及数据

            #region 全年累计
            IRow row4 = sheet1.CreateRow(4);
            int j = 0;
            row4.CreateCell(j).SetCellValue("全年累计");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(_list.Count);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            for (int d1 = 0; d1 < dic304.Count; d1++)
            {
                var templist = _list.FindAll(a => a.FIRERECINFO001 == dic304[d1].DICTVALUE);
                row4.CreateCell(j).SetCellValue(templist.Count);
                row4.GetCell(j).CellStyle = getCellStyleCenter(book);
                j++;
            }
            j = ExcelTotal(_list, book, row4, j);
            #endregion

            #region 详细数据
            int rowIndex = 0;
            for (int i = 1; i < result.Count; i++)
            {
                int z = 0;
                IRow row5 = sheet1.CreateRow(rowIndex + 5);
                if (PublicCls.OrgIsShi(result[i].ORGNO)) { }
                else if (PublicCls.OrgIsXian(result[i].ORGNO)) { result[i].ORGNAME = "  " + result[i].ORGNAME; }
                else { result[i].ORGNAME = "  " + result[i].ORGNAME; }
                row5.CreateCell(z).SetCellValue(result[i].ORGNAME);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                List<FIRERECORD_FIREINFO_Model> templist = LYReportTemplist(result, _list, i);
                row5.CreateCell(z).SetCellValue(templist.Count);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                for (int d1 = 0; d1 < dic304.Count; d1++)
                {
                    var v = templist.FindAll(a => a.FIRERECINFO001 == dic304[d1].DICTVALUE);
                    row5.CreateCell(z).SetCellValue(v.Count);
                    row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                    z++;
                }
                z = ExcelTotal(templist, book, row5, z);
                rowIndex++;
            }
            #endregion

            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }

        #endregion

        #region 林火1表分月报表
        /// <summary>
        /// 林火1表分月报表
        /// </summary>
        /// <returns></returns>
        public ActionResult THEMONTHLYREPORT()
        {
            pubViewBag("041003", "041003", "林火1表分月报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.YEAR = DateTime.Now.ToString("yyyy");
            return View();
        }
        /// <summary>
        /// 林火1表分月报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult THEMONTHLYREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            DateTime startYear = new DateTime(int.Parse(YEAR), 1, 1);  //本年年初
            DateTime endYear = new DateTime(int.Parse(YEAR), 12, 31).AddDays(1).AddSeconds(-1);  //本年年末
            string fireTime = startYear.ToString();
            string fireEndTime = endYear.ToString();
            #endregion

            #region 数据准备
            List<String> monthList = monthlist();
            List<FIRERECORD_FIREINFO_Model> _themonthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = fireTime, FIREENDTIME = fireEndTime }).ToList();
            List<T_SYS_DICTModel> dic304 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "304" }).ToList();
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"THEMONTHLYREPORTTable\"  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            string headname = "月&nbsp;&nbsp;份";
            sb.AppendFormat(HEADER(headname));
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");

            #region 第一行  列号
            sb.AppendFormat("<tr>");
            sb.AppendFormat(COLUMN());
            sb.AppendFormat("</tr>");
            #endregion

            #region 全年累计
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td  class=\"center\">全年累计</td>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", _themonthlyreportList.Count);
            sb.AppendFormat(TOTAL(_themonthlyreportList));
            #endregion

            #region 详细数据行
            for (int i = 0; i < monthList.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", monthList[i]);
                List<FIRERECORD_FIREINFO_Model> templist = new List<FIRERECORD_FIREINFO_Model>();
                DateTime startTime = new DateTime(int.Parse(YEAR), 1, 1);
                DateTime endTime = new DateTime(int.Parse(YEAR), 1, 13);
                List<float> arealist = new List<float>();
                startTime = new DateTime(int.Parse(YEAR), i + 1, 1);
                if (i != monthList.Count - 1)
                    endTime = new DateTime(int.Parse(YEAR), i + 2, 1).AddSeconds(-1);
                else
                    endTime = new DateTime(int.Parse(YEAR) + 1, 1, 1).AddSeconds(-1);
                templist = _themonthlyreportList.FindAll(a => DateTime.Parse(a.FIRETIME) >= startTime && DateTime.Parse(a.FIRETIME) <= endTime).ToList();
                arealist = CalZJ(templist);
                sb.AppendFormat("<td class=\"center\">{0}</td>", templist.Count);
                sb.AppendFormat(TOTAL(templist));
            }
            #endregion

            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult THEMONTHLYREPORTExportExcel()
        {
            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            DateTime startYear = new DateTime(int.Parse(YEAR), 1, 1);  //本年年初
            DateTime endYear = new DateTime(int.Parse(YEAR), 12, 31).AddDays(1).AddSeconds(-1);  //本年年末
            string fireTime = startYear.ToString();
            string fireEndTime = endYear.ToString();
            #endregion

            #region 数据准备
            List<String> monthList = monthlist();
            List<FIRERECORD_FIREINFO_Model> _monthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = fireTime, FIREENDTIME = fireEndTime }).ToList();
            List<T_SYS_DICTModel> dic304 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "304" }).ToList();
            int colsCount = dic304.Count + 18;
            string orgName = T_SYS_ORGCls.getorgname(ORGNO);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "041003", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + YEAR + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("分月报表");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            string headname = "月 份";
            CreatExcelHead(dic304, colsCount, title, book, sheet1, headname);
            #endregion

            #region 表身及数据

            #region 全年累计
            IRow row4 = sheet1.CreateRow(4);
            int j = 0;
            row4.CreateCell(j).SetCellValue("全年累计");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(_monthlyreportList.Count);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            for (int d1 = 0; d1 < dic304.Count; d1++)
            {
                var templist = _monthlyreportList.FindAll(a => a.FIRERECINFO001 == dic304[d1].DICTVALUE);
                row4.CreateCell(j).SetCellValue(templist.Count);
                row4.GetCell(j).CellStyle = getCellStyleCenter(book);
                j++;
            }
            j = ExcelTotal(_monthlyreportList, book, row4, j);
            #endregion

            #region 详细数据
            int rowIndex = 0;
            for (int i = 0; i < monthList.Count; i++)
            {
                int z = 0;
                IRow row5 = sheet1.CreateRow(rowIndex + 5);
                row5.CreateCell(z).SetCellValue(monthList[i]);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                List<FIRERECORD_FIREINFO_Model> templist = new List<FIRERECORD_FIREINFO_Model>();
                DateTime startTime = new DateTime(int.Parse(YEAR), 1, 1);
                DateTime endTime = new DateTime(int.Parse(YEAR), 1, 13);
                List<float> arealist = new List<float>();
                startTime = new DateTime(int.Parse(YEAR), i + 1, 1);
                if (i != monthList.Count - 1)
                    endTime = new DateTime(int.Parse(YEAR), i + 2, 1).AddSeconds(-1);
                else
                    endTime = new DateTime(int.Parse(YEAR) + 1, 1, 1).AddSeconds(-1);
                templist = _monthlyreportList.FindAll(a => DateTime.Parse(a.FIRETIME) >= startTime && DateTime.Parse(a.FIRETIME) <= endTime).ToList();
                arealist = CalZJ(templist);
                row5.CreateCell(z).SetCellValue(templist.Count);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                for (int d1 = 0; d1 < dic304.Count; d1++)
                {
                    var v = templist.FindAll(a => a.FIRERECINFO001 == dic304[d1].DICTVALUE);
                    row5.CreateCell(z).SetCellValue(v.Count);
                    row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                    z++;
                }
                z = ExcelTotal(templist, book, row5, z);
                rowIndex++;
            }
            #endregion

            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }

        #endregion

        #region 林火1表火灾类型表
        /// <summary>
        ///  林火1表火灾类型表
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRETYPEREPORT()
        {
            pubViewBag("041004", "041004", " 林火1表火灾类型表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.YEAR = DateTime.Now.ToString("yyyy");
            return View();
        }
        /// <summary>
        /// 林火1表火灾类型表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRETYPEREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            DateTime startYear = new DateTime(int.Parse(YEAR), 1, 1);  //本年年初
            DateTime endYear = new DateTime(int.Parse(YEAR), 12, 31).AddDays(1).AddSeconds(-1);  //本年年末
            string fireTime = startYear.ToString();
            string fireEndTime = endYear.ToString();
            #endregion

            #region 数据准备
            List<String> monthList = monthlist();
            List<FIRERECORD_FIREINFO_Model> _firetypelist = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = fireTime, FIREENDTIME = fireEndTime }).ToList();
            List<T_SYS_DICTModel> dic304 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "304" }).ToList();
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"FIRETYPEREPORTTable\"  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"3\">月&nbsp;&nbsp;&nbsp;&nbsp;份</th>");
            sb.AppendFormat("<th rowspan=\"3\"> 火灾类型</th>");
            sb.AppendFormat("<th >森林火灾次数</th>");
            sb.AppendFormat("<th rowspan=\"3\"> 火&nbsp;&nbsp;场</br></br>总面积</br></br>(公顷)</th>");
            sb.AppendFormat("<th colspan=\"3\">受害森林面积(公顷)</th>");
            sb.AppendFormat("<th colspan=\"2\">损失林木</th>");
            sb.AppendFormat("<th colspan=\"4\" style=\"width:150px;\">人员伤亡</th>");
            sb.AppendFormat("<th rowspan=\"3\">其他损失</br></br>折&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;款</br></br>(万元)</th>");
            sb.AppendFormat("<th rowspan=\"3\">出动扑</br></br>火人工</br></br>(工日)</th>");
            sb.AppendFormat("<th colspan=\"2\">出动车辆(台)</th>");
            sb.AppendFormat("<th rowspan=\"3\">出动</br></br>飞机</br></br>(架次)</th>");
            sb.AppendFormat("<th rowspan=\"3\">扑火</br></br>经费</br></br>(万元)</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"2\">计</th>");
            sb.AppendFormat("<th rowspan=\"2\">计</th>");
            sb.AppendFormat("<th colspan=\"2\">其&nbsp;&nbsp;中</th>");
            sb.AppendFormat("<th rowspan=\"2\">成林蓄积<br /><br />(立方米)</th>");
            sb.AppendFormat("<th rowspan=\"2\">幼林株数<br /><br />(万株)</th>");
            sb.AppendFormat("<th rowspan=\"2\">计</th>");
            sb.AppendFormat("<th rowspan=\"2\">轻<br /><br />伤</th>");
            sb.AppendFormat("<th rowspan=\"2\">重<br /><br />伤</th>");
            sb.AppendFormat("<th rowspan=\"2\">死<br /><br />亡</th>");
            sb.AppendFormat("<th rowspan=\"2\">计</th>");
            sb.AppendFormat("<th rowspan=\"2\">其中<br><br>汽车</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>原始林</th>");
            sb.AppendFormat("<th>人工林</th>");
            sb.AppendFormat("</tr");
            sb.AppendFormat("</thead>");
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");

            #region 第一行 列号
            sb.AppendFormat("<tr class=\"center\" >");
            sb.AppendFormat("<td  class=\"center\"></td>");
            sb.AppendFormat("<td  class=\"center\">甲</td>");
            sb.AppendFormat("<td  class=\"center\">1</td>");
            sb.AppendFormat("<td  class=\"center\">2</td>");
            sb.AppendFormat("<td  class=\"center\">3</td>");
            sb.AppendFormat("<td  class=\"center\">4</td>");
            sb.AppendFormat("<td  class=\"center\">5</td>");
            sb.AppendFormat("<td  class=\"center\">6</td>");
            sb.AppendFormat("<td  class=\"center\">7</td>");
            sb.AppendFormat("<td  class=\"center\">8</td>");
            sb.AppendFormat("<td  class=\"center\">9</td>");
            sb.AppendFormat("<td  class=\"center\">10</td>");
            sb.AppendFormat("<td  class=\"center\">11</td>");
            sb.AppendFormat("<td  class=\"center\">12</td>");
            sb.AppendFormat("<td  class=\"center\">13</td>");
            sb.AppendFormat("<td  class=\"center\">14</td>");
            sb.AppendFormat("<td  class=\"center\">15</td>");
            sb.AppendFormat("<td  class=\"center\">16</td>");
            sb.AppendFormat("<td  class=\"center\">17</td>");
            sb.AppendFormat("</tr>");
            #endregion

            #region 全年总计
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td  class=\"center\" rowspan=\"5\">全年总计</td>");
            sb.AppendFormat("<td  class=\"center\">全年总计");
            sb.AppendFormat("<td  class=\"center\">{0}</td>", _firetypelist.Count);
            List<float> totalist = CalZJ(_firetypelist);
            sb.AppendFormat("<td  class=\"center\">{0}</td>", totalist[0]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[1] + totalist[2]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[1]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[2]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[3]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[4]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[5] + totalist[6] + totalist[7]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[5]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[6]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[7]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[8]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[9]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[19]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[10]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[11]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[12]);
            foreach (var d1 in dic304)
            {
                string title = d1.DICTNAME + "火灾";
                if (!string.IsNullOrEmpty(d1.STANDBY1))
                    title += "</br>(" + d1.STANDBY1 + ")";
                var templist = _firetypelist.FindAll(a => a.FIRERECINFO001 == d1.DICTVALUE);
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>{0}</td>", title);
                sb.AppendFormat("<td class=\"center\">{0}</td>", templist.Count);
                List<float> firelevellist = CalZJ(templist);
                sb.AppendFormat("<td  class=\"center\">{0}</td>", firelevellist[0]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[1] + firelevellist[2]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[1]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[2]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[3]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[4]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[5] + firelevellist[6] + firelevellist[7]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[5]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[6]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[7]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[8]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[9]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[19]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[10]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[11]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[12]);
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</td>");

            sb.AppendFormat("</tr>");
            #endregion

            #region 详细数据行
            for (int i = 0; i < monthList.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", "row1");
                sb.AppendFormat("<td class=\"center\" rowspan=\"5\" >{0}</td>", monthList[i]);
                sb.AppendFormat("<td class=\"{0}\">本月合计", "row1");
                List<FIRERECORD_FIREINFO_Model> monthtotallist = new List<FIRERECORD_FIREINFO_Model>();
                DateTime startTime = new DateTime(int.Parse(YEAR), 1, 1);
                DateTime endTime = new DateTime(int.Parse(YEAR), 1, 13);
                List<float> arealist = new List<float>();
                startTime = new DateTime(int.Parse(YEAR), i + 1, 1);
                if (i != monthList.Count - 1)
                    endTime = new DateTime(int.Parse(YEAR), i + 2, 1).AddSeconds(-1);
                else
                    endTime = new DateTime(int.Parse(YEAR) + 1, 1, 1).AddSeconds(-1);
                monthtotallist = _firetypelist.FindAll(a => DateTime.Parse(a.FIRETIME) >= startTime && DateTime.Parse(a.FIRETIME) <= endTime).ToList();
                sb.AppendFormat("<td  class=\"center\">{0}</td>", monthtotallist.Count);
                List<float> list = CalZJ(monthtotallist);
                sb.AppendFormat("<td  class=\"center\">{0}</td>", list[0]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[1] + list[2]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[1]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[2]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[3]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[4]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[5] + list[6] + list[7]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[5]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[6]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[7]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[8]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[9]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[19]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[10]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[11]);
                sb.AppendFormat("<td class=\"center\">{0}</td>", list[12]);
                foreach (var d1 in dic304)
                {
                    string title = d1.DICTNAME + "火灾";
                    if (!string.IsNullOrEmpty(d1.STANDBY1))
                        title += "</br>(" + d1.STANDBY1 + ")";
                    var templist = monthtotallist.FindAll(a => a.FIRERECINFO001 == d1.DICTVALUE);
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>{0}</td>", title);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", templist.Count);
                    List<float> firelevellist = CalZJ(templist);
                    sb.AppendFormat("<td  class=\"center\">{0}</td>", firelevellist[0]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[1] + firelevellist[2]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[1]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[2]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[3]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[4]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[5] + firelevellist[6] + firelevellist[7]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[5]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[6]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[7]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[8]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[9]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[19]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[10]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[11]);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", firelevellist[12]);
                    sb.AppendFormat("</tr>");
                }
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
            }
            #endregion

            sb.AppendFormat("</tbody>");
            #endregion
            sb.AppendFormat("</table>");
            #endregion
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult FIRETYPEREPORTExportExcel()
        {
            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            DateTime startYear = new DateTime(int.Parse(YEAR), 1, 1);  //本年年初
            DateTime endYear = new DateTime(int.Parse(YEAR), 12, 31).AddDays(1).AddSeconds(-1);  //本年年末
            string fireTime = startYear.ToString();
            string fireEndTime = endYear.ToString();
            #endregion

            #region 数据准备
            List<String> monthList = monthlist();
            List<FIRERECORD_FIREINFO_Model> _firetypelist = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = fireTime, FIREENDTIME = fireEndTime }).ToList();
            List<T_SYS_DICTModel> dic304 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "304" }).ToList();
            int colsCount = 18;
            string orgName = T_SYS_ORGCls.getorgname(ORGNO);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "041004", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + YEAR + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("分火灾类型表");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            for (int i = 0; i < colsCount; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 20 * 256);
                else
                    sheet1.SetColumnWidth(i, 15 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.Height = 2 * 350;
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.Height = 2 * 350;
            rowHead1.CreateCell(0).SetCellValue("月  份");
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(1).SetCellValue("火灾类型");
            rowHead1.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(2).SetCellValue("森林火灾次数");
            rowHead1.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(3).SetCellValue("火场\n总面积\n（公顷）");
            rowHead1.GetCell(3).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(4).SetCellValue("受害森林面积（公顷）");
            rowHead1.GetCell(4).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(7).SetCellValue("损失林木");
            rowHead1.GetCell(7).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(9).SetCellValue("人员伤亡");
            rowHead1.GetCell(9).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(13).SetCellValue("其他损失\n折款\n(万元)");
            rowHead1.GetCell(13).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(14).SetCellValue("出动扑\n火人工\n(工日)");
            rowHead1.GetCell(14).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(15).SetCellValue("出动车辆(台)");
            rowHead1.GetCell(15).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(17).SetCellValue("出动\n飞机\n(架次)");
            rowHead1.GetCell(17).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(18).SetCellValue("扑火\n经费\n(万元)");
            rowHead1.GetCell(18).CellStyle = getCellStyleHead(book);
            IRow rowHead2 = sheet1.CreateRow(2);
            rowHead2.CreateCell(2).SetCellValue("合计");
            rowHead2.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(4).SetCellValue("计");
            rowHead2.GetCell(4).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(5).SetCellValue("其中");
            rowHead2.GetCell(5).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(7).SetCellValue("成林蓄积\n(立方米)");
            rowHead2.GetCell(7).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(8).SetCellValue("幼林株数\n(万株)");
            rowHead2.GetCell(8).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(9).SetCellValue("计");
            rowHead2.GetCell(9).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(10).SetCellValue("轻伤");
            rowHead2.GetCell(10).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(11).SetCellValue("重伤");
            rowHead2.GetCell(11).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(12).SetCellValue("死亡");
            rowHead2.GetCell(12).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(15).SetCellValue("计");
            rowHead2.GetCell(15).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(16).SetCellValue("其中汽车");
            rowHead2.GetCell(16).CellStyle = getCellStyleHead(book);

            IRow rowHead3 = sheet1.CreateRow(3);
            rowHead3.CreateCell(5).SetCellValue("原始林");
            rowHead3.GetCell(5).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(6).SetCellValue("人工林");
            rowHead3.GetCell(6).CellStyle = getCellStyleHead(book);

            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 0, 0));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 1, 1));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 2, 2));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 3, 3));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 4, 6));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 7, 8));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 9, 12));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 13, 13));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 14, 14));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 15, 16));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 17, 17));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 18, 18));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 2, 2));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 4, 4));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 5, 6));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 7, 7));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 8, 8));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 9, 9));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 10, 10));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 11, 11));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 12, 12));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 15, 15));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 16, 16));
            #endregion

            #region 表身及数据

            #region 全年总计
            sheet1.AddMergedRegion(new CellRangeAddress(4, 8, 0, 0));
            IRow row4 = sheet1.CreateRow(4);
            int j = 0;
            row4.CreateCell(j).SetCellValue("全年总计");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue("全年总计");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(_firetypelist.Count);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            j = ExcelTotal(_firetypelist, book, row4, j);
            j++;
            int rowindex = 5;
            foreach (var d1 in dic304)
            {
                int k = 1;
                string firelevel = d1.DICTNAME + "火灾";
                var templist = _firetypelist.FindAll(a => a.FIRERECINFO001 == d1.DICTVALUE);
                IRow row5 = sheet1.CreateRow(rowindex);
                row5.CreateCell(k).SetCellValue(firelevel);
                row5.GetCell(k).CellStyle = getCellStyleCenter(book);
                k++;
                row5.CreateCell(k).SetCellValue(templist.Count);
                row5.GetCell(k).CellStyle = getCellStyleCenter(book);
                k++;
                k = ExcelTotal(templist, book, row5, k);
                rowindex++;
            }
            #endregion

            #region 详细数据
            int y = 9;
            for (int i = 0; i < monthList.Count; i++)
            {
                int z = 0;
                sheet1.AddMergedRegion(new CellRangeAddress(y, y + 4, 0, 0));
                IRow row5 = sheet1.CreateRow(y);
                y++;
                row5.CreateCell(z).SetCellValue(monthList[i]);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue("本月合计");
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                List<FIRERECORD_FIREINFO_Model> monthtotallist = new List<FIRERECORD_FIREINFO_Model>();
                DateTime startTime = new DateTime(int.Parse(YEAR), 1, 1);
                DateTime endTime = new DateTime(int.Parse(YEAR), 1, 13);
                // List<float> arealist = new List<float>();
                startTime = new DateTime(int.Parse(YEAR), i + 1, 1);
                if (i != monthList.Count - 1)
                    endTime = new DateTime(int.Parse(YEAR), i + 2, 1).AddSeconds(-1);
                else
                    endTime = new DateTime(int.Parse(YEAR) + 1, 1, 1).AddSeconds(-1);
                monthtotallist = _firetypelist.FindAll(a => DateTime.Parse(a.FIRETIME) >= startTime && DateTime.Parse(a.FIRETIME) <= endTime).ToList();
                row5.CreateCell(z).SetCellValue(monthtotallist.Count);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                z = ExcelTotal(monthtotallist, book, row5, z);
                int rowindex1 = y;
                foreach (var d1 in dic304)
                {
                    int k = 1;
                    string firelevel = d1.DICTNAME + "火灾";
                    var templist = monthtotallist.FindAll(a => a.FIRERECINFO001 == d1.DICTVALUE);
                    IRow row6 = sheet1.CreateRow(rowindex1);
                    row6.CreateCell(k).SetCellValue(firelevel);
                    row6.GetCell(k).CellStyle = getCellStyleCenter(book);
                    k++;
                    row6.CreateCell(k).SetCellValue(templist.Count);
                    row6.GetCell(k).CellStyle = getCellStyleCenter(book);
                    k++;
                    k = ExcelTotal(templist, book, row6, k);
                    rowindex1++;
                }
                y = y + 4;
            }

            #endregion

            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }

        #endregion

        #region 林火2表月报表
        /// <summary>
        /// 林火2表月报表
        /// </summary>
        /// <returns></returns>
        public ActionResult SECONDMONTHLYREPORT()
        {
            pubViewBag("041005", "041005", "林火2表月报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM");
            return View();
        }
        /// <summary>
        /// 林火2表月报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SECONDMONTHLYREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string Time = Request.Params["Time"];
            string[] sTime = Time.Split('-');
            string ORGNO = SystemCls.getCurUserOrgNo();
            string nowFireTime;
            string nowfireEndTime;
            string fireTime;
            string fireEndTime;
            LYReportQueryTerm(Time, out nowFireTime, out nowfireEndTime, out fireTime, out fireEndTime);
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result;
            List<FIRERECORD_FIREINFO_Model> _monthlyreportList;
            List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList;
            LYReportQueryData(ORGNO, nowFireTime, nowfireEndTime, fireTime, fireEndTime, out result, out _monthlyreportList, out _nowmonthlyreportList);
            //List<T_SYS_ORGModel> result = new List<T_SYS_ORGModel>();
            //if (PublicCls.OrgIsShi(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXian = "1" }).ToList();
            //if (PublicCls.OrgIsXian(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXianXZ = "1" }).ToList();
            //if (PublicCls.OrgIsZhen(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO }).ToList();
            List<T_SYS_DICTModel> dic302 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "302" }).ToList();
            List<T_SYS_DICTModel> dic203 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "203" }).ToList();
            //List<FIRERECORD_FIREINFO_Model> _monthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = fireTime, FIREENDTIME = nowfireEndTime }).ToList();
            //List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = nowFireTime, FIREENDTIME = nowfireEndTime }).ToList();
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"SECONDMONTHLYREPORTTable\"  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat(HEADER2());
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");

            #region 第一行列号
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat(COLUMN());
            sb.AppendFormat("<td  class=\"center\">22</td>");
            sb.AppendFormat("<td  class=\"center\">23</td>");
            sb.AppendFormat("<td  class=\"center\">24</td>");
            sb.AppendFormat("<td  class=\"center\">25</td>");
            sb.AppendFormat("<td  class=\"center\">26</td>");
            sb.AppendFormat("<td  class=\"center\">27</td>");
            sb.AppendFormat("<td  class=\"center\">28</td>");
            sb.AppendFormat("<td  class=\"center\">29</td>");
            sb.AppendFormat("<td  class=\"center\">30</td>");
            sb.AppendFormat("</tr>");
            #endregion

            #region 一至本月累计
            sb.AppendFormat("<tr  class=\"center\" >");
            sb.AppendFormat("<td  class=\"center\">一至本月累计</td>");
            sb.AppendFormat("<td  class=\"center\">{0}</td>", _monthlyreportList.Count);
            sb.AppendFormat(METHODS(_monthlyreportList));
            #endregion

            #region 本月合计
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td  class=\"center\">本月合计</td>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList.Count);
            sb.AppendFormat(METHODS(_nowmonthlyreportList));
            #endregion

            #region 详细数据行
            for (int i = 1; i < result.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", result[i].ORGNAME);
                List<FIRERECORD_FIREINFO_Model> templist = LYReportTemplist(result, _nowmonthlyreportList, i);
                //List<FIRERECORD_FIREINFO_Model> templist = new List<FIRERECORD_FIREINFO_Model>();
                //if (PublicCls.OrgIsShi(result[i].ORGNO))
                //    templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 4) == result[i].ORGNO.Substring(0, 4)).ToList();
                //if (PublicCls.OrgIsXian(result[i].ORGNO))
                //    templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                //if (PublicCls.OrgIsZhen(result[i].ORGNO))
                //    templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                //if (PublicCls.OrgIsCun(result[i].ORGNO))
                //    templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS == result[i].ORGNO).ToList();
                sb.AppendFormat("<td class=\"center\">{0}</td>", templist.Count);
                sb.AppendFormat(METHODS(templist));
            }
            #endregion

            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult SECONDMONTHLYREPORTExportExcel()
        {
            #region 数据查询条件
            string Time = Request.Params["Time"];
            string[] sTime = Time.Split('-');
            string ORGNO = SystemCls.getCurUserOrgNo();
            string nowFireTime;
            string nowfireEndTime;
            string fireTime;
            string fireEndTime;
            LYReportQueryTerm(Time, out nowFireTime, out nowfireEndTime, out fireTime, out fireEndTime);
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result;
            List<FIRERECORD_FIREINFO_Model> _monthlyreportList;
            List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList;
            LYReportQueryData(ORGNO, nowFireTime, nowfireEndTime, fireTime, fireEndTime, out result, out _monthlyreportList, out _nowmonthlyreportList);
            //List<T_SYS_ORGModel> result = new List<T_SYS_ORGModel>();
            //if (PublicCls.OrgIsShi(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXian = "1" }).ToList();
            //if (PublicCls.OrgIsXian(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXianXZ = "1" }).ToList();
            //if (PublicCls.OrgIsZhen(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO }).ToList();
            List<T_SYS_DICTModel> dic302 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "302" }).ToList();
            List<T_SYS_DICTModel> dic203 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "203" }).ToList();
            //List<FIRERECORD_FIREINFO_Model> _monthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = fireTime, FIREENDTIME = nowfireEndTime }).ToList();
            //List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = nowFireTime, FIREENDTIME = nowfireEndTime }).ToList();
            int colsCount = (dic302.Count) + 8;
            string orgName = T_SYS_ORGCls.getorgname(ORGNO);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "041005", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + Time + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("月报表");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            CreatExcel2Head(dic302, colsCount, title, book, sheet1);
            #endregion

            #region 表身及数据

            #region 一至本月累计
            IRow row4 = sheet1.CreateRow(4);
            int j = 0;
            row4.CreateCell(j).SetCellValue("一至本月累计");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            int count1;
            int count2;
            int count3;
            j = CreateExcel2Total(dic302, _monthlyreportList, book, row4, j, out count1, out count2, out count3);
            #endregion

            #region 本月合计
            IRow row5 = sheet1.CreateRow(5);
            int k = 0;
            row5.CreateCell(k).SetCellValue("本月合计");
            row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            k++;
            //row5.CreateCell(k).SetCellValue(_nowmonthlyreportList.Count);
            //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
            //k++;
            int count4;
            int count5;
            int count6;
            k = CreateExcel2Total(dic302, _nowmonthlyreportList, book, row5, k, out count4, out count5, out count6);
            #endregion

            #region 详细数据
            int rowIndex = 0;
            for (int i = 1; i < result.Count; i++)
            {
                int z = 0;
                IRow row6 = sheet1.CreateRow(rowIndex + 6);
                if (PublicCls.OrgIsShi(result[i].ORGNO)) { }
                else if (PublicCls.OrgIsXian(result[i].ORGNO)) { result[i].ORGNAME = "  " + result[i].ORGNAME; }
                else { result[i].ORGNAME = "　　" + result[i].ORGNAME; }
                row6.CreateCell(z).SetCellValue(result[i].ORGNAME);
                row6.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                List<FIRERECORD_FIREINFO_Model> templist = LYReportTemplist(result, _nowmonthlyreportList, i);
                //List<FIRERECORD_FIREINFO_Model> templist = new List<FIRERECORD_FIREINFO_Model>();
                //if (PublicCls.OrgIsShi(result[i].ORGNO))
                //    templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 4) == result[i].ORGNO.Substring(0, 4)).ToList();
                //if (PublicCls.OrgIsXian(result[i].ORGNO))
                //    templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                //if (PublicCls.OrgIsZhen(result[i].ORGNO))
                //    templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                //if (PublicCls.OrgIsCun(result[i].ORGNO))
                //    templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS == result[i].ORGNO).ToList();
                //row6.CreateCell(z).SetCellValue(templist.Count);
                //row6.GetCell(z).CellStyle = getCellStyleCenter(book);
                //z++;
                int count7;
                int count8;
                int count9;
                z = CreateExcel2Total(dic302, templist, book, row6, z, out count7, out count8, out count9);
                rowIndex++;
            }
            #endregion

            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }

        #endregion

        #region 林火2表分地区报表
        /// <summary>
        ///林火2表分地区报表
        /// </summary>
        /// <returns></returns>
        public ActionResult SECONDAREAREPORT()
        {
            pubViewBag("041006", "041006", "林火2表分地区报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM");
            return View();
        }
        /// <summary>
        /// 林火2表分地区报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SECONDAREAREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string Time = Request.Params["Time"];
            string[] sTime = Time.Split('-');
            string ORGNO = SystemCls.getCurUserOrgNo();
            string nowFireTime;
            string nowfireEndTime;
            string fireTime;
            string fireEndTime;
            LYReportQueryTerm(Time, out nowFireTime, out nowfireEndTime, out fireTime, out fireEndTime);

            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result;
            List<FIRERECORD_FIREINFO_Model> _monthlyreportList;
            List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList;
            LYReportQueryData(ORGNO, nowFireTime, nowfireEndTime, fireTime, fireEndTime, out result, out _monthlyreportList, out _nowmonthlyreportList);
            //List<T_SYS_ORGModel> result = new List<T_SYS_ORGModel>();
            //if (PublicCls.OrgIsShi(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXian = "1" }).ToList();
            //if (PublicCls.OrgIsXian(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXianXZ = "1" }).ToList();
            //if (PublicCls.OrgIsZhen(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO }).ToList();
            List<T_SYS_DICTModel> dic302 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "302" }).ToList();
            List<T_SYS_DICTModel> dic203 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "203" }).ToList();
            //List<FIRERECORD_FIREINFO_Model> _monthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = fireTime, FIREENDTIME = nowfireEndTime }).ToList();
            //List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = nowFireTime, FIREENDTIME = nowfireEndTime }).ToList();

            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"SECONDMONTHLYREPORTTable\"  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat(HEADER2());
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");

            #region 第一行列号
            sb.AppendFormat("<tr>");
            sb.AppendFormat(COLUMN());
            sb.AppendFormat("<td  class=\"center\">22</td>");
            sb.AppendFormat("<td  class=\"center\">23</td>");
            sb.AppendFormat("<td  class=\"center\">24</td>");
            sb.AppendFormat("<td  class=\"center\">25</td>");
            sb.AppendFormat("<td  class=\"center\">26</td>");
            sb.AppendFormat("<td  class=\"center\">27</td>");
            sb.AppendFormat("<td  class=\"center\">28</td>");
            sb.AppendFormat("<td  class=\"center\">29</td>");
            sb.AppendFormat("<td  class=\"center\">30</td>");
            sb.AppendFormat("</tr>");
            #endregion

            #region 一至本月累计
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td  class=\"center\">一至本月累计</td>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", _monthlyreportList.Count);
            sb.AppendFormat(METHODS(_monthlyreportList));
            #endregion

            #region 详细数据行
            for (int i = 1; i < result.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", result[i].ORGNAME);
                List<FIRERECORD_FIREINFO_Model> templist = LYReportTemplist(result, _nowmonthlyreportList, i);
                //List<FIRERECORD_FIREINFO_Model> templist = new List<FIRERECORD_FIREINFO_Model>();
                //if (PublicCls.OrgIsShi(result[i].ORGNO))
                //    templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 4) == result[i].ORGNO.Substring(0, 4)).ToList();
                //if (PublicCls.OrgIsXian(result[i].ORGNO))
                //    templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                //if (PublicCls.OrgIsZhen(result[i].ORGNO))
                //    templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                //if (PublicCls.OrgIsCun(result[i].ORGNO))
                //    templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS == result[i].ORGNO).ToList();
                sb.AppendFormat("<td class=\"center\">{0}</td>", templist.Count);
                sb.AppendFormat(METHODS(templist));
            }
            #endregion

            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult SECONDAREAREPORTExportExcel()
        {
            #region 数据查询条件
            string Time = Request.Params["Time"];
            string[] sTime = Time.Split('-');
            string ORGNO = SystemCls.getCurUserOrgNo();
            string nowFireTime;
            string nowfireEndTime;
            string fireTime;
            string fireEndTime;
            LYReportQueryTerm(Time, out nowFireTime, out nowfireEndTime, out fireTime, out fireEndTime);
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result;
            List<FIRERECORD_FIREINFO_Model> _monthlyreportList;
            List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList;
            LYReportQueryData(ORGNO, nowFireTime, nowfireEndTime, fireTime, fireEndTime, out result, out _monthlyreportList, out _nowmonthlyreportList);
            //List<T_SYS_ORGModel> result = new List<T_SYS_ORGModel>();
            //if (PublicCls.OrgIsShi(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXian = "1" }).ToList();
            //if (PublicCls.OrgIsXian(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXianXZ = "1" }).ToList();
            //if (PublicCls.OrgIsZhen(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO }).ToList();
            List<T_SYS_DICTModel> dic302 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "302" }).ToList();
            List<T_SYS_DICTModel> dic203 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "203" }).ToList();
            //List<FIRERECORD_FIREINFO_Model> _monthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = fireTime, FIREENDTIME = nowfireEndTime }).ToList();
            //List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = nowFireTime, FIREENDTIME = nowfireEndTime }).ToList();
            int colsCount = (dic302.Count) + 8;
            string orgName = T_SYS_ORGCls.getorgname(ORGNO);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "041006", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + Time + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("月报表");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            CreatExcel2Head(dic302, colsCount, title, book, sheet1);
            #endregion

            #region 表身及数据

            #region 一至本月累计
            IRow row4 = sheet1.CreateRow(4);
            int j = 0;
            row4.CreateCell(j).SetCellValue("一至本月累计");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            int count1;
            int count2;
            int count3;
            j = CreateExcel2Total(dic302, _monthlyreportList, book, row4, j, out count1, out count2, out count3);
            //row4.CreateCell(j).SetCellValue(_monthlyreportList.Count);
            //row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            //j++;

            #endregion

            #region 详细数据
            int rowIndex = 0;
            for (int i = 1; i < result.Count; i++)
            {
                int z = 0;
                IRow row6 = sheet1.CreateRow(rowIndex + 5);
                if (PublicCls.OrgIsShi(result[i].ORGNO)) { }
                else if (PublicCls.OrgIsXian(result[i].ORGNO)) { result[i].ORGNAME = "  " + result[i].ORGNAME; }
                else { result[i].ORGNAME = "   " + result[i].ORGNAME; }
                row6.CreateCell(z).SetCellValue(result[i].ORGNAME);
                row6.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                List<FIRERECORD_FIREINFO_Model> templist = LYReportTemplist(result, _nowmonthlyreportList, i);
                //row6.CreateCell(z).SetCellValue(templist.Count);
                //row6.GetCell(z).CellStyle = getCellStyleCenter(book);
                //z++;
                int count7;
                int count8;
                int count9;
                z = CreateExcel2Total(dic302, templist, book, row6, z, out count7, out count8, out count9);
                rowIndex++;
            }
            #endregion

            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }

        #endregion

        #region 林火2表分月报表
        /// <summary>
        ///林火2表分月报表
        /// </summary>
        /// <returns></returns>
        public ActionResult SECONDTHEMONTHLYREPORT()
        {
            pubViewBag("041007", "041007", "林火2表分月报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM");
            return View();
        }
        /// <summary>
        ///  林火2表分月报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SECONDTHEMONTHLYREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string Time = Request.Params["Time"];
            string[] sTime = Time.Split('-');
            string ORGNO = SystemCls.getCurUserOrgNo();
            string nowFireTime;
            string nowfireEndTime;
            string fireTime;
            string fireEndTime;
            LYReportQueryTerm(Time, out nowFireTime, out nowfireEndTime, out fireTime, out fireEndTime);
            #endregion

            #region 数据准备
            List<String> monthList = monthlist();
            List<T_SYS_DICTModel> dic302 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "302" }).ToList();
            List<T_SYS_DICTModel> dic203 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "203" }).ToList();
            List<FIRERECORD_FIREINFO_Model> _monthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = fireTime, FIREENDTIME = nowfireEndTime }).ToList();
            List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = nowFireTime, FIREENDTIME = nowfireEndTime }).ToList();

            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"SECONDTHEMONTHLYREPORTTable\"  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat(HEADER2());
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");

            #region 第一行列号
            sb.AppendFormat("<tr>");
            sb.AppendFormat(COLUMN());
            sb.AppendFormat("<td  class=\"center\">22</td>");
            sb.AppendFormat("<td  class=\"center\">23</td>");
            sb.AppendFormat("<td  class=\"center\">24</td>");
            sb.AppendFormat("<td  class=\"center\">25</td>");
            sb.AppendFormat("<td  class=\"center\">26</td>");
            sb.AppendFormat("<td  class=\"center\">27</td>");
            sb.AppendFormat("<td  class=\"center\">28</td>");
            sb.AppendFormat("<td  class=\"center\">29</td>");
            sb.AppendFormat("<td  class=\"center\">30</td>");
            sb.AppendFormat("</tr>");
            #endregion

            #region 一至本月累计
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td  class=\"center\">一至本月累计</td>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", _monthlyreportList.Count);
            sb.AppendFormat(METHODS(_monthlyreportList));
            #endregion

            #region 详细数据行
            int m = int.Parse(sTime[1]);
            for (int i = 0; i < m; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\"  >{0}</td>", monthList[i]);
                List<FIRERECORD_FIREINFO_Model> templist = new List<FIRERECORD_FIREINFO_Model>();
                DateTime endTime = new DateTime(int.Parse(sTime[0]), 1, 13);
                List<float> arealist = new List<float>();
                DateTime startTime = new DateTime(int.Parse(sTime[0]), i + 1, 1);
                if (i != monthList.Count - 1)
                    endTime = new DateTime(int.Parse(sTime[0]), i + 2, 1).AddSeconds(-1);
                else
                    endTime = new DateTime(int.Parse(sTime[0]) + 1, 1, 1).AddSeconds(-1);
                templist = _monthlyreportList.FindAll(a => DateTime.Parse(a.FIRETIME) >= startTime && DateTime.Parse(a.FIRETIME) <= endTime).ToList();
                sb.AppendFormat("<td class=\"center\">{0}</td>", templist.Count);
                sb.AppendFormat(METHODS(templist));
            }
            #endregion

            sb.AppendFormat("</tbody>");
            #endregion
            sb.AppendFormat("</table>");
            #endregion
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult SECONDTHEMONTHLYREPORTExportExcel()
        {
            #region 数据查询条件
            string Time = Request.Params["Time"];
            string[] sTime = Time.Split('-');
            string ORGNO = SystemCls.getCurUserOrgNo();
            string nowFireTime;
            string nowfireEndTime;
            string fireTime;
            string fireEndTime;
            LYReportQueryTerm(Time, out nowFireTime, out nowfireEndTime, out fireTime, out fireEndTime);
            #endregion

            #region 数据准备
            List<String> monthList = monthlist();
            List<T_SYS_DICTModel> dic302 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "302" }).ToList();
            List<T_SYS_DICTModel> dic203 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "203" }).ToList();
            List<FIRERECORD_FIREINFO_Model> _monthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = fireTime, FIREENDTIME = nowfireEndTime }).ToList();
            List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = nowFireTime, FIREENDTIME = nowfireEndTime }).ToList();
            int colsCount = (dic302.Count) + 8;
            string orgName = T_SYS_ORGCls.getorgname(ORGNO);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "041007", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + Time + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("月报表");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            CreatExcel2Head(dic302, colsCount, title, book, sheet1);
            #endregion

            #region 表身及数据

            #region 一至本月累计
            IRow row4 = sheet1.CreateRow(4);
            int j = 0;
            row4.CreateCell(j).SetCellValue("一至本月累计");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            int count1;
            int count2;
            int count3;
            j = CreateExcel2Total(dic302, _monthlyreportList, book, row4, j, out count1, out count2, out count3);

            #endregion

            #region 详细数据
            int rowIndex = 0;
            int m = int.Parse(sTime[1]);
            for (int i = 0; i < m; i++)
            {
                int z = 0;
                IRow row5 = sheet1.CreateRow(rowIndex + 5);
                row5.CreateCell(z).SetCellValue(monthList[i]);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                List<FIRERECORD_FIREINFO_Model> templist = new List<FIRERECORD_FIREINFO_Model>();
                DateTime endTime = new DateTime(int.Parse(sTime[0]), 1, 13);
                List<float> arealist = new List<float>();
                DateTime startTime = new DateTime(int.Parse(sTime[0]), i + 1, 1);
                if (i != monthList.Count - 1)
                    endTime = new DateTime(int.Parse(sTime[0]), i + 2, 1).AddSeconds(-1);
                else
                    endTime = new DateTime(int.Parse(sTime[0]) + 1, 1, 1).AddSeconds(-1);
                templist = _monthlyreportList.FindAll(a => DateTime.Parse(a.FIRETIME) >= startTime && DateTime.Parse(a.FIRETIME) <= endTime).ToList();
                //arealist = CalZJ(templist);
                int count7;
                int count8;
                int count9;
                z = CreateExcel2Total(dic302, templist, book, row5, z, out count7, out count8, out count9);

                rowIndex++;
            }
            #endregion

            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }


        #endregion

        #region 林火4表森林火灾统计表
        /// <summary>
        /// 林火4表森林火灾统计表
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRETOTALREPORT()
        {
            pubViewBag("041008", "041008", "林火4表森林火灾统计表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM");
            return View();
        }

        /// <summary>
        /// 林火4表森林火灾统计表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRETOTALREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string Time = Request.Params["Time"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            string nowFireTime;
            string nowfireEndTime;
            string fireTime;
            string fireEndTime;
            LYReportQueryTerm(Time, out nowFireTime, out nowfireEndTime, out fireTime, out fireEndTime);
            #endregion

            #region 数据准备
            List<FIRERECORD_FIREINFO_Model> _monthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { FIRETIME = fireTime, FIREENDTIME = fireEndTime }).ToList();
            List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = nowFireTime, FIREENDTIME = nowfireEndTime }).ToList();
            List<T_SYS_DICTModel> dic301 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "301" }).ToList();
            List<T_SYS_DICTModel> dic304 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "304" }).ToList();
            #endregion

            #region 数据表
            sb.AppendFormat("<table  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"3\">甲</th>");
            sb.AppendFormat("<th colspan=\"3\" style=\"width:200px;\">起火地点</th>");
            sb.AppendFormat("<th colspan=\"3\" style=\"width:100px;\">起火时间</th>");
            sb.AppendFormat("<th colspan=\"3\" style=\"width:100px;\">扑灭时间</th>");
            sb.AppendFormat("<th rowspan=\"2\" style=\"width:41px;\">起火</br></br>原因</th>");
            sb.AppendFormat("<th rowspan=\"2\" style=\"width:32px;\">火灾</br></br>种类</th>");
            sb.AppendFormat("<th rowspan=\"2\" style=\"width:32px;\">火灾</br></br>等级</th>");
            sb.AppendFormat("<th rowspan=\"2\">火 场</br></br>面积</br></br>(公顷)</th>");
            sb.AppendFormat("<th rowspan=\"2\">有林 地</br></br>面积</br></br>(公顷)</th>");
            sb.AppendFormat("<th colspan=\"4\">成灾面积&nbsp;(公顷)</th>");
            sb.AppendFormat("<th colspan=\"2\">林木损失情况</th>");
            sb.AppendFormat("<th rowspan=\"2\">林 分</br></br>组 成</th>");
            sb.AppendFormat("<th rowspan=\"2\">林 龄</th>");
            sb.AppendFormat("<th colspan=\"2\">火场指挥员</th>");
            sb.AppendFormat("<th colspan=\"2\">人员伤亡</th>");
            sb.AppendFormat("<th colspan=\"2\">火案查处情况</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>县</th>");
            sb.AppendFormat("<th>乡</th>");
            sb.AppendFormat("<th>村</th>");
            sb.AppendFormat("<th>月</th>");
            sb.AppendFormat("<th>日</th>");
            sb.AppendFormat("<th>时</th>");
            sb.AppendFormat("<th>月</th>");
            sb.AppendFormat("<th>日</th>");
            sb.AppendFormat("<th>时</th>");
            sb.AppendFormat("<th>计</th>");
            sb.AppendFormat("<th>原始林</th>");
            sb.AppendFormat("<th>次生林</th>");
            sb.AppendFormat("<th>人工林</th>");
            sb.AppendFormat("<th>成 林</br></br>蓄 积</br></br>(立方米)</th>");
            sb.AppendFormat("<th>幼 林</br></br>株 数</br></br>(万株)</th>");
            sb.AppendFormat("<th>姓名</th>");
            sb.AppendFormat("<th>职务</th>");
            sb.AppendFormat("<th>伤</br></br>（人）</th>");
            sb.AppendFormat("<th>亡</br></br>（人）</th>");
            sb.AppendFormat("<th>林政</br></br>处罚</br></br>（人）</th>");
            sb.AppendFormat("<th>刑事</br></br>处罚</br></br>（人）</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");

            #region 第一行：列号
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\">1</td>");
            sb.AppendFormat("<td class=\"center\">2</td>");
            sb.AppendFormat("<td class=\"center\">3</td>");
            sb.AppendFormat("<td class=\"center\">4</td>");
            sb.AppendFormat("<td class=\"center\">5</td>");
            sb.AppendFormat("<td class=\"center\">6</td>");
            sb.AppendFormat("<td class=\"center\">7</td>");
            sb.AppendFormat("<td class=\"center\">8</td>");
            sb.AppendFormat("<td class=\"center\">9</td>");
            sb.AppendFormat("<td class=\"center\">10</td>");
            sb.AppendFormat("<td class=\"center\">11</td>");
            sb.AppendFormat("<td class=\"center\">12</td>");
            sb.AppendFormat("<td class=\"center\">13</td>");
            sb.AppendFormat("<td class=\"center\">14</td>");
            sb.AppendFormat("<td class=\"center\">15</td>");
            sb.AppendFormat("<td class=\"center\">16</td>");
            sb.AppendFormat("<td class=\"center\">17</td>");
            sb.AppendFormat("<td class=\"center\">18</td>");
            sb.AppendFormat("<td class=\"center\">19</td>");
            sb.AppendFormat("<td class=\"center\">20</td>");
            sb.AppendFormat("<td class=\"center\">21</td>");
            sb.AppendFormat("<td class=\"center\">22</td>");
            sb.AppendFormat("<td class=\"center\">23</td>");
            sb.AppendFormat("<td class=\"center\">24</td>");
            sb.AppendFormat("<td class=\"center\">25</td>");
            sb.AppendFormat("<td class=\"center\">26</td>");
            sb.AppendFormat("<td class=\"center\">27</td>");
            sb.AppendFormat("<td class=\"center\">28</td>");
            sb.AppendFormat("</tr>");
            #endregion

            #region 合计
            List<float> totalist = CalZJ(_nowmonthlyreportList);
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"center\">合计</td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[0]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[17]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[1] + totalist[2] + totalist[18]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[1]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[18]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[2]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[3]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[4]);
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[5] + totalist[6]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[7]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[15]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[16]);
            sb.AppendFormat("</tr>");
            #endregion

            #region 详细数据行
            for (int i = 0; i < _nowmonthlyreportList.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td>{0}</td>", i + 1);
                //根据组织机构编码获取组织机构名称
                string FIREADDRESSCOUNTY = T_SYS_ORGCls.getorgname(_nowmonthlyreportList[i].FIREADDRESSCOUNTY);
                string FIREADDRESSTOWNS = T_SYS_ORGCls.getorgname(_nowmonthlyreportList[i].FIREADDRESSTOWNS);
                sb.AppendFormat("<td class=\"center\">{0}</td>", FIREADDRESSCOUNTY);
                sb.AppendFormat("<td class=\"center\">{0}</td>", FIREADDRESSTOWNS);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIREADDRESSVILLAGES);
                //起火时间
                DateTime date = DateTime.Parse(_nowmonthlyreportList[i].FIRETIME.ToString().Trim());
                string month = date.ToString("MM");
                string day = date.ToString("dd");
                string time = date.ToString("HH:mm");
                sb.AppendFormat("<td class=\"center\">{0}</td>", month);
                sb.AppendFormat("<td class=\"center\">{0}</td>", day);
                sb.AppendFormat("<td class=\"center\">{0}</td>", time);
                //扑灭时间
                DateTime enddate = DateTime.Parse(_nowmonthlyreportList[i].FIREENDTIME.ToString().Trim());
                string endmonth = enddate.ToString("MM");
                string endday = enddate.ToString("dd");
                string endtime = enddate.ToString("HH:mm");
                sb.AppendFormat("<td class=\"center\">{0}</td>", endmonth);
                sb.AppendFormat("<td class=\"center\">{0}</td>", endday);
                sb.AppendFormat("<td class=\"center\">{0}</td>", endtime);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO150);
                //火灾种类
                string name = T_SYS_DICTCls.getDicName(new T_SYS_DICTSW { DICTTYPEID = "301", DICTVALUE = (_nowmonthlyreportList[i].FIRERECINFO000) }); sb.AppendFormat("<td class=\"center\">{0}</td>", name);
                //火灾等级
                string levelname = T_SYS_DICTCls.getDicName(new T_SYS_DICTSW { DICTTYPEID = "304", DICTVALUE = (_nowmonthlyreportList[i].FIRERECINFO001) });
                sb.AppendFormat("<td class=\"center\">{0}</td>", levelname);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO020);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO021);
                //成灾面积
                float total = float.Parse(_nowmonthlyreportList[i].FIRERECINFO030) + float.Parse(_nowmonthlyreportList[i].FIRERECINFO031) + float.Parse(_nowmonthlyreportList[i].FIRERECINFO032);
                // List<float> total = CalZJ(_nowmonthlyreportList);
                sb.AppendFormat("<td class=\"center\">{0}</td>", total);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO030);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO031);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO032);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO040);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO041);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO050);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO051);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO060);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO061);
                //伤
                int total1 = int.Parse(_nowmonthlyreportList[i].FIRERECINFO070) + int.Parse(_nowmonthlyreportList[i].FIRERECINFO071);
                // List<float> total1 = CalZJ(_nowmonthlyreportList);
                sb.AppendFormat("<td class=\"center\">{0}</td>", total1);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO072);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO081);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _nowmonthlyreportList[i].FIRERECINFO082);
                sb.AppendFormat("</tr>");
            }
            #endregion

            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult FIRETOTALREPORTExportExcel()
        {
            #region 数据查询条件
            string Time = Request.Params["Time"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            string nowFireTime;
            string nowfireEndTime;
            string fireTime;
            string fireEndTime;
            LYReportQueryTerm(Time, out nowFireTime, out nowfireEndTime, out fireTime, out fireEndTime);
            #endregion

            #region 数据准备
            List<T_SYS_DICTModel> dic304 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "304" }).ToList();
            List<FIRERECORD_FIREINFO_Model> _monthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = fireTime, FIREENDTIME = fireEndTime }).ToList();
            List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = nowFireTime, FIREENDTIME = nowfireEndTime }).ToList();
            int colsCount = 29;
            string orgName = T_SYS_ORGCls.getorgname(ORGNO);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "041008", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + Time + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("火灾统计表");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.Height = 2 * 350;
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.Height = 2 * 350;
            rowHead1.CreateCell(0).SetCellValue("甲");
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(1).SetCellValue("起火地点");
            rowHead1.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(4).SetCellValue("起火时间");
            rowHead1.GetCell(4).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(7).SetCellValue("扑灭时间");
            rowHead1.GetCell(7).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(10).SetCellValue("起火\n原因");
            rowHead1.GetCell(10).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(11).SetCellValue("火灾\n种类");
            rowHead1.GetCell(11).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(12).SetCellValue("火灾\n等级");
            rowHead1.GetCell(12).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(13).SetCellValue("火 场\n面 积\n(公顷）");
            rowHead1.GetCell(13).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(14).SetCellValue("有林地\n面积\n（公顷)");
            rowHead1.GetCell(14).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(15).SetCellValue("成 灾\n面 积(公顷)");
            rowHead1.GetCell(15).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(19).SetCellValue("林木损失情况");
            rowHead1.GetCell(19).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(21).SetCellValue("林分\n组成");
            rowHead1.GetCell(21).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(22).SetCellValue("林龄");
            rowHead1.GetCell(22).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(23).SetCellValue("火场指挥员");
            rowHead1.GetCell(23).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(25).SetCellValue("人员伤亡");
            rowHead1.GetCell(25).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(27).SetCellValue("火案查处情况");
            rowHead1.GetCell(27).CellStyle = getCellStyleHead(book);

            IRow rowHead2 = sheet1.CreateRow(2);
            rowHead2.CreateCell(1).SetCellValue("县");
            rowHead2.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(2).SetCellValue("乡");
            rowHead2.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(3).SetCellValue("村");
            rowHead2.GetCell(3).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(4).SetCellValue("月");
            rowHead2.GetCell(4).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(5).SetCellValue("日");
            rowHead2.GetCell(5).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(6).SetCellValue("时");
            rowHead2.GetCell(6).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(7).SetCellValue("月");
            rowHead2.GetCell(7).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(8).SetCellValue("日");
            rowHead2.GetCell(8).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(9).SetCellValue("时");
            rowHead2.GetCell(9).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(15).SetCellValue("计");
            rowHead2.GetCell(15).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(16).SetCellValue("原始林");
            rowHead2.GetCell(16).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(17).SetCellValue("次生林");
            rowHead2.GetCell(17).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(18).SetCellValue("人工林");
            rowHead2.GetCell(18).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(19).SetCellValue("成 林\n蓄 积\n(立方米)");
            rowHead2.GetCell(19).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(20).SetCellValue("幼 林\n株 数\n(万株)");
            rowHead2.GetCell(20).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(23).SetCellValue("姓名");
            rowHead2.GetCell(23).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(24).SetCellValue("职务");
            rowHead2.GetCell(24).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(25).SetCellValue("伤\n(人)");
            rowHead2.GetCell(25).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(26).SetCellValue("亡\n(人)");
            rowHead2.GetCell(26).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(27).SetCellValue("林政\n处罚\n(人)");
            rowHead2.GetCell(27).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(28).SetCellValue("刑事\n处罚\n(人)");
            rowHead2.GetCell(28).CellStyle = getCellStyleHead(book);
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount - 1));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 1, 3));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 4, 6));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 7, 9));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 10, 10));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 11, 11));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 12, 12));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 13, 13));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 14, 14));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 15, 18));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 19, 20));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 21, 21));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 22, 22));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 23, 24));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 25, 26));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 27, 28));
            #endregion

            #region 表身及数据

            #region 合计
            IRow row4 = sheet1.CreateRow(3);
            int j = 0;
            List<float> totalist = CalZJ(_nowmonthlyreportList);
            row4.CreateCell(j).SetCellValue("合计");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j = 13;
            row4.CreateCell(j).SetCellValue(totalist[0] > 0 ? string.Format("{0:0.00}", totalist[0]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(totalist[17] > 0 ? string.Format("{0:0.00}", totalist[17]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(totalist[1] + totalist[2] + totalist[18] > 0 ? string.Format("{0:0.00}", totalist[1] + totalist[2] + totalist[18]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(totalist[1] > 0 ? string.Format("{0:0.00}", totalist[1]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(totalist[18] > 0 ? string.Format("{0:0.00}", totalist[18]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(totalist[2] > 0 ? string.Format("{0:0.00}", totalist[2]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(totalist[3] > 0 ? string.Format("{0:0.00}", totalist[3]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(totalist[4] > 0 ? string.Format("{0:0.00}", totalist[4]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j = j + 5;
            row4.CreateCell(j).SetCellValue(totalist[5] + totalist[6] > 0 ? string.Format("{0:0.00}", totalist[5] + totalist[6]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(totalist[7] > 0 ? string.Format("{0:0.00}", totalist[7]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(totalist[15] > 0 ? string.Format("{0:0.00}", totalist[15]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(totalist[16] > 0 ? string.Format("{0:0.00}", totalist[16]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            #endregion

            #region 详细数据行
            int rowIndex = 0;
            for (int i = 0; i < _nowmonthlyreportList.Count; i++)
            {
                int z = 0;
                IRow row5 = sheet1.CreateRow(rowIndex + 4);
                row5.CreateCell(z).SetCellValue(i + 1);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                //根据组织机构编码获取组织机构名称
                string FIREADDRESSCOUNTY = T_SYS_ORGCls.getorgname(_nowmonthlyreportList[i].FIREADDRESSCOUNTY);
                string FIREADDRESSTOWNS = T_SYS_ORGCls.getorgname(_nowmonthlyreportList[i].FIREADDRESSTOWNS);
                row5.CreateCell(z).SetCellValue(FIREADDRESSCOUNTY);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(FIREADDRESSTOWNS);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIREADDRESSVILLAGES);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                //起火时间
                DateTime date = DateTime.Parse(_nowmonthlyreportList[i].FIRETIME.ToString().Trim());
                string month = date.ToString("MM");
                string day = date.ToString("dd");
                string time = date.ToString("HH:mm");
                row5.CreateCell(z).SetCellValue(month);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(day);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(time);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                //扑灭时间
                DateTime enddate = DateTime.Parse(_nowmonthlyreportList[i].FIREENDTIME.ToString().Trim());
                string endmonth = enddate.ToString("MM");
                string endday = enddate.ToString("dd");
                string endtime = enddate.ToString("HH:mm");
                row5.CreateCell(z).SetCellValue(endmonth);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(endday);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(endtime);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIRERECINFO150);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                //火灾种类
                string name = T_SYS_DICTCls.getDicName(new T_SYS_DICTSW { DICTTYPEID = "301", DICTVALUE = (_nowmonthlyreportList[i].FIRERECINFO000) });
                row5.CreateCell(z).SetCellValue(name);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                //火灾等级
                string levelname = T_SYS_DICTCls.getDicName(new T_SYS_DICTSW { DICTTYPEID = "304", DICTVALUE = (_nowmonthlyreportList[i].FIRERECINFO001) });
                row5.CreateCell(z).SetCellValue(levelname);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(string.Format("{0:0.00}", _nowmonthlyreportList[i].FIRERECINFO020));
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIRERECINFO021);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                //成灾面积
                float total = float.Parse(_nowmonthlyreportList[i].FIRERECINFO030) + float.Parse(_nowmonthlyreportList[i].FIRERECINFO031) + float.Parse(_nowmonthlyreportList[i].FIRERECINFO032);
                //List<float> total = CalZJ(_nowmonthlyreportList);
                row5.CreateCell(z).SetCellValue(total);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIRERECINFO030);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIRERECINFO031);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIRERECINFO032);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIRERECINFO040);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIRERECINFO041);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIRERECINFO050);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIRERECINFO051);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIRERECINFO060);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIRERECINFO061);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                //伤
                int total1 = int.Parse(_nowmonthlyreportList[i].FIRERECINFO070) + int.Parse(_nowmonthlyreportList[i].FIRERECINFO071);
                //List<float> total1 = CalZJ(_nowmonthlyreportList);
                row5.CreateCell(z).SetCellValue(total1);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIRERECINFO072);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIRERECINFO081);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                z++;
                row5.CreateCell(z).SetCellValue(_nowmonthlyreportList[i].FIRERECINFO082);
                row5.GetCell(z).CellStyle = getCellStyleCenter(book);
                rowIndex++;
            }
            #endregion

            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }
        #endregion

        #region 火情档案_森林防火组织机构统计
        /// <summary>
        /// 火情档案_森林防火组织机构统计--数据增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT8Manager()
        {
            FIRERECORD_REPORT8_Model m = new FIRERECORD_REPORT8_Model();
            // m.FIRERECORD_REPORT8ID = Request.Params["FIRERECORD_REPORT8ID"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.REPORTYEAR = Request.Params["REPORTYEAR"];
            m.REPORTCODE = Request.Params["REPORTCODE"];
            m.SSXTYPELEVELCODE = Request.Params["SSXTYPELEVELCODE"];
            m.REPORTVALUE = Request.Params["REPORTVALUE"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(FIRERECORD_REPORT8Cls.Manager(m)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 森林防火组织机构统计
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT8Man()
        {
            string Method = Request.Params["Method"];
            string REPORTYEAR = Request.Params["REPORTYEAR"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            FIRERECORD_REPORT8_Model model = new FIRERECORD_REPORT8_Model();
            if (!string.IsNullOrEmpty(REPORTYEAR))
            {
                if (Method == "Add")
                {

                    model = FIRERECORD_REPORT8Cls.getModel(new FIRERECORD_REPORT8_SW { BYORGNO = ORGNO, REPORTYEAR = REPORTYEAR });
                }
            }
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOptionByORGNO(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), OnlyGetShiXian = ORGNO, TopORGNO = ORGNO });//获取州级和市县
            // vdorg(ORGNO);
            ViewBag.REPORTYEAR = DateTime.Now.ToString("yyyy");
            ViewBag.Method = Method;
            List<T_SYS_DICTModel> dic305list = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "305" }).ToList();
            List<T_SYS_DICTModel> dic310List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "310" }).ToList();
            ViewBag.dic305Value = T_SYS_DICTCls.getDicValueStr(dic305list);
            ViewBag.dic310Value = T_SYS_DICTCls.getDicValueStr(dic310List);
            ViewBag.dic305Count = dic305list.Count;
            ViewBag.dic310Count = dic310List.Count;
            return View(model);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetReport8json()
        {
            string REPORTYEAR = Request.Params["REPORTYEAR"];
            string BYORGNO = Request.Params["BYORGNO"];
            // string BYORGNO = SystemCls.getCurUserOrgNo();
            return Content(JsonConvert.SerializeObject(FIRERECORD_REPORT8Cls.getListModel(new FIRERECORD_REPORT8_SW { BYORGNO = BYORGNO, REPORTYEAR = REPORTYEAR, })), "text/html;charset=UTF-8");
        }
        #endregion

        #region 火情档案_森林防火组织机构统计年报表
        /// <summary>
        /// 火情档案_森林防火组织机构统计年报表
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT8()
        {
            pubViewBag("041009", "041009", "森林防火组织机构统计年报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.YEAR = DateTime.Now.ToString("yyyy");
            return View();
        }

        /// <summary>
        ///火情档案_森林防火组织机构统计年报表
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT8Query()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string BYORGNO = Request.Params["BYORGNO"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = ResultList(ORGNO);//获取组织机构编码
            List<FIRERECORD_REPORT8_Model> yearreportlist = new List<FIRERECORD_REPORT8_Model>();
            if (ORGNO.Substring(4, 11) == "00000000000")
            {
                yearreportlist = FIRERECORD_REPORT8Cls.getListModel(new FIRERECORD_REPORT8_SW { REPORTYEAR = YEAR, }).ToList();
            }
            else
            {
                yearreportlist = FIRERECORD_REPORT8Cls.getListModel(new FIRERECORD_REPORT8_SW { BYORGNO = ORGNO, REPORTYEAR = YEAR, }).ToList();
            }
            List<T_SYS_DICTModel> dic305 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "305" }).ToList();//省地县
            List<T_SYS_DICTModel> dic310 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "310" }).ToList();//组织机构统计年报
            #endregion

            #region 数据表
            sb.AppendFormat("<table  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"3\"colspan=\"2\"style=\" width:50px;\">单位</th>");
            sb.AppendFormat("<th colspan=\"2\" >森林防火指挥部</th>");
            sb.AppendFormat("<th colspan=\"3\">森林防火办事机构</th>");
            sb.AppendFormat("<th colspan=\"2\">防火检查站</th>");
            sb.AppendFormat("<th colspan=\"2\"style=\" width:100px;\">专业(半专业)森林消防队</th>");
            sb.AppendFormat("<th colspan=\"2\">义务森林消防队</th>");
            sb.AppendFormat("<th colspan=\"2\">护林员(人)</th>");
            sb.AppendFormat("<th rowspan=\"3\">备注</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            for (int i = 0; i < dic310.Count - 1; i++)
            {
                string title = dic310[i].DICTNAME;
                if (!string.IsNullOrEmpty(dic310[i].STANDBY2))
                    title += "</br>(" + dic310[i].STANDBY2 + ")";
                sb.AppendFormat("<th rowspan=\"2\"  >{0}</th>", title);
            }
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");

            #region 第一行：列号
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td class=\"center\"colspan=\"2\">甲</td>");
            sb.AppendFormat("<td class=\"center\">1</td>");
            sb.AppendFormat("<td class=\"center\">2</td>");
            sb.AppendFormat("<td class=\"center\">3</td>");
            sb.AppendFormat("<td class=\"center\">4</td>");
            sb.AppendFormat("<td class=\"center\">5</td>");
            sb.AppendFormat("<td class=\"center\">6</td>");
            sb.AppendFormat("<td class=\"center\">7</td>");
            sb.AppendFormat("<td class=\"center\">8</td>");
            sb.AppendFormat("<td class=\"center\">9</td>");
            sb.AppendFormat("<td class=\"center\">10</td>");
            sb.AppendFormat("<td class=\"center\">11</td>");
            sb.AppendFormat("<td class=\"center\">12</td>");
            sb.AppendFormat("<td class=\"center\">13</td>");
            sb.AppendFormat("<td class=\"center\">14</td>");
            sb.AppendFormat("</tr>");
            #endregion

            #region 合计
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"center\"colspan=\"2\">合   计</td>");
            List<int> _templist = new List<int>();
            for (int i = 0; i < dic310.Count - 1; i++)
            {
                var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic310[i].DICTVALUE);
                int REPORTVALUE = 0;
                foreach (var v in templist)
                {
                    if (!string.IsNullOrEmpty(v.REPORTVALUE))
                    {
                        REPORTVALUE += int.Parse(v.REPORTVALUE);
                    }
                }
                sb.AppendFormat("<td>{0}</td>", templist.Count > 0 ? REPORTVALUE : 0);
            }
            //var bzlist = yearreportlist.FindAll(a => a.REPORTCODE == dic310[dic310.Count - 1].DICTVALUE);
            // sb.AppendFormat("<td>{0}</td>", bzlist.Count > 0 ? bzlist[0].REPORTVALUE : "");
            sb.AppendFormat("<td></td>");
            sb.AppendFormat("</tr>");
            #endregion

            #region 省地县
            foreach (var d1 in dic305)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (int.Parse(d1.DICTVALUE) % 2 == 0) ? "" : "row1");
                if (int.Parse(d1.DICTVALUE) == 1)
                {
                    sb.AppendFormat("<td rowspan=\"3\">其中</td>");
                    string title = d1.DICTNAME;
                    sb.AppendFormat("<td>{0}</td>", title);
                    for (int i = 0; i < dic310.Count - 1; i++)
                    {
                        var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic310[i].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);
                        int REPORTVALUE = 0;
                        foreach (var v in templist)
                        {
                            if (!string.IsNullOrEmpty(v.REPORTVALUE))
                            {
                                REPORTVALUE += int.Parse(v.REPORTVALUE);
                            }
                        }
                        sb.AppendFormat("<td>{0}</td>", templist.Count > 0 ? REPORTVALUE : 0);
                    }
                    //var bzlist1 = yearreportlist.FindAll(a => a.REPORTCODE == dic310[dic310.Count - 1].DICTVALUE);
                    //sb.AppendFormat("<td>{0}</td>", bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
                    sb.AppendFormat("<td></td>");
                    sb.AppendFormat("</tr>");
                }
                else
                {
                    string title = d1.DICTNAME;
                    sb.AppendFormat("<td>{0}</td>", title);
                    for (int i = 0; i < dic310.Count - 1; i++)
                    {
                        var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic310[i].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);
                        int REPORTVALUE = 0;
                        foreach (var v in templist)
                        {
                            if (!string.IsNullOrEmpty(v.REPORTVALUE))
                            {
                                REPORTVALUE += int.Parse(v.REPORTVALUE);
                            }
                        }
                        sb.AppendFormat("<td>{0}</td>", templist.Count > 0 ? REPORTVALUE : 0);
                    }
                    //var bzlist1 = yearreportlist.FindAll(a => a.REPORTCODE == dic310[dic310.Count - 1].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);
                    //sb.AppendFormat("<td>{0}</td>", bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
                    sb.AppendFormat("<td></td>");
                    sb.AppendFormat("</tr>");
                }
            }
            #endregion

            #region 详细数据行
            for (int i = 0; i < result.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"Center\"  colspan=\"2\">{0}</td>", result[i].ORGNAME);
                List<FIRERECORD_REPORT8_Model> templist = new List<FIRERECORD_REPORT8_Model>();
                if (PublicCls.OrgIsShi(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO.Substring(4, 11) == result[i].ORGNO.Substring(4, 11)).ToList();//判断是否州级
                //if (PublicCls.OrgIsShi(result[i].ORGNO))
                //    templist = yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 4) == result[i].ORGNO.Substring(0, 4)).ToList();
                if (PublicCls.OrgIsXian(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                if (PublicCls.OrgIsZhen(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                if (PublicCls.OrgIsCun(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO == result[i].ORGNO).ToList();
                for (int j = 0; j < dic310.Count - 1; j++)
                {
                    var templist2 = templist.FindAll(a => a.REPORTCODE == dic310[j].DICTVALUE);
                    int REPORTVALUE = 0;
                    foreach (var v in templist2)
                    {
                        if (!string.IsNullOrEmpty(v.REPORTVALUE))
                        {
                            REPORTVALUE += int.Parse(v.REPORTVALUE);
                        }
                    }
                    sb.AppendFormat("<td>{0}</td>", templist2.Count > 0 ? REPORTVALUE : 0);
                }
                var bzlist1 = templist.FindAll(a => a.REPORTCODE == dic310[dic310.Count - 1].DICTVALUE);
                sb.AppendFormat("<td>{0}</td>", bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
            }
            #endregion

            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult FIRERECORD_REPORT8ExportExcel()
        {
            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = ResultList(ORGNO);//获取组织机构编码;
            // List<FIRERECORD_REPORT8_Model> yearreportlist = FIRERECORD_REPORT8Cls.getListModel(new FIRERECORD_REPORT8_SW { REPORTYEAR = YEAR }).ToList();
            List<FIRERECORD_REPORT8_Model> yearreportlist = new List<FIRERECORD_REPORT8_Model>();
            if (ORGNO.Substring(4, 11) == "00000000000")
            {
                yearreportlist = FIRERECORD_REPORT8Cls.getListModel(new FIRERECORD_REPORT8_SW { REPORTYEAR = YEAR, }).ToList();
            }
            else
            {
                yearreportlist = FIRERECORD_REPORT8Cls.getListModel(new FIRERECORD_REPORT8_SW { BYORGNO = ORGNO, REPORTYEAR = YEAR, }).ToList();
            }
            List<T_SYS_DICTModel> dic305 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "305" }).ToList();//省地县
            List<T_SYS_DICTModel> dic310 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "310" }).ToList();//组织机构统计年报
            int colsCount = (dic310.Count + 2);
            //string orgName = T_SYS_ORGCls.getorgname(ORGNO);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "041009", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = YEAR + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("森林防火组织机构统计年报表");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.Height = 2 * 350;
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.Height = 2 * 350;
            rowHead1.CreateCell(0).SetCellValue("单位");
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(2).SetCellValue("森林防火指挥部");
            rowHead1.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(4).SetCellValue("森林防火办事机构");
            rowHead1.GetCell(4).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(7).SetCellValue("防火检查站");
            rowHead1.GetCell(7).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(9).SetCellValue("专业(半专业)森林消防队");
            rowHead1.GetCell(9).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(11).SetCellValue("义务森林消防队");
            rowHead1.GetCell(11).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(13).SetCellValue("护林员（人）");
            rowHead1.GetCell(13).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(15).SetCellValue("备 注");
            rowHead1.GetCell(15).CellStyle = getCellStyleHead(book);

            IRow rowHead2 = sheet1.CreateRow(2);
            rowHead2.CreateCell(2).SetCellValue("机构数");
            rowHead2.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(3).SetCellValue("成员数");
            rowHead2.GetCell(3).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(4).SetCellValue("机构数");
            rowHead2.GetCell(4).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(5).SetCellValue("编制数");
            rowHead2.GetCell(5).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(6).SetCellValue("实有数");
            rowHead2.GetCell(6).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(7).SetCellValue("机构数");
            rowHead2.GetCell(7).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(8).SetCellValue("人数");
            rowHead2.GetCell(8).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(9).SetCellValue("队数");
            rowHead2.GetCell(9).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(10).SetCellValue("人数");
            rowHead2.GetCell(10).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(11).SetCellValue("队数");
            rowHead2.GetCell(11).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(12).SetCellValue("人数");
            rowHead2.GetCell(12).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(13).SetCellValue("专职");
            rowHead2.GetCell(13).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(14).SetCellValue("兼职");
            rowHead2.GetCell(14).CellStyle = getCellStyleHead(book);

            IRow rowHead3 = sheet1.CreateRow(3);
            rowHead3.CreateCell(2).SetCellValue("(个)");
            rowHead3.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(3).SetCellValue("(人)");
            rowHead3.GetCell(3).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(4).SetCellValue("(个)");
            rowHead3.GetCell(4).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(5).SetCellValue("(人)");
            rowHead3.GetCell(5).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(6).SetCellValue("(人)");
            rowHead3.GetCell(6).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(7).SetCellValue("(个)");
            rowHead3.GetCell(7).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(8).SetCellValue("(人)");
            rowHead3.GetCell(8).CellStyle = getCellStyleHead(book);

            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount - 1));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 0, 1));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 2, 3));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 4, 6));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 7, 8));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 9, 10));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 11, 12));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 13, 14));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 15, 15));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 9, 9));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 10, 10));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 11, 11));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 12, 12));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 13, 13));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 14, 14));

            sheet1.AddMergedRegion(new CellRangeAddress(4, 4, 0, 1));
            sheet1.AddMergedRegion(new CellRangeAddress(5, 7, 0, 0));
            for (int i = 0; i < result.Count; i++)
            {
                sheet1.AddMergedRegion(new CellRangeAddress(i + 8, i + 8, 0, 1));
            }
            #endregion

            #region 表身及数据

            #region 合计
            IRow row4 = sheet1.CreateRow(4);
            int j = 0;
            row4.CreateCell(j).SetCellValue("合计");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j = 2;
            List<int> _templist = new List<int>();
            for (int i = 0; i < dic310.Count - 1; i++)
            {
                var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic310[i].DICTVALUE);
                int REPORTVALUE = 0;
                foreach (var v in templist)
                {
                    if (!string.IsNullOrEmpty(v.REPORTVALUE))
                    {
                        REPORTVALUE += int.Parse(v.REPORTVALUE);
                    }
                }
                row4.CreateCell(j).SetCellValue(templist.Count > 0 ? REPORTVALUE : 0);
                row4.GetCell(j).CellStyle = getCellStyleCenter(book);
                j++;
            }
            //var bzlist = yearreportlist.FindAll(a => a.REPORTCODE == dic310[dic310.Count - 1].DICTVALUE);
            //row4.CreateCell(j).SetCellValue(bzlist.Count > 0 ? bzlist[0].REPORTVALUE : "");
            //row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            row4.CreateCell(j).SetCellValue("");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            #endregion

            #region 省县地级
            int z = 5;
            foreach (var d1 in dic305)
            {
                IRow row5 = sheet1.CreateRow(z);
                int k = 0;
                if (int.Parse(d1.DICTVALUE) == 1)
                {
                    row5.CreateCell(k).SetCellValue("其中");
                    row5.GetCell(k).CellStyle = getCellStyleCenter(book);
                    k++;
                    string SDXtitle = d1.DICTNAME;
                    row5.CreateCell(k).SetCellValue(SDXtitle);
                    row5.GetCell(k).CellStyle = getCellStyleCenter(book);
                    k++;
                    for (int i = 0; i < dic310.Count - 1; i++)
                    {
                        var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic310[i].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);
                        int REPORTVALUE = 0;
                        foreach (var v in templist)
                        {
                            if (!string.IsNullOrEmpty(v.REPORTVALUE))
                            {
                                REPORTVALUE += int.Parse(v.REPORTVALUE);
                            }
                        }
                        row5.CreateCell(k).SetCellValue(templist.Count > 0 ? REPORTVALUE : 0);
                        row5.GetCell(k).CellStyle = getCellStyleCenter(book);
                        k++;
                    }
                    //var bzlist1 = yearreportlist.FindAll(a => a.REPORTCODE == dic310[dic310.Count - 1].DICTVALUE);
                    //row5.CreateCell(k).SetCellValue(bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
                    //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
                    row5.CreateCell(k).SetCellValue("");
                    row5.GetCell(k).CellStyle = getCellStyleCenter(book);
                    k++;
                }
                else
                {
                    k = 1;
                    string SDXtitle = d1.DICTNAME;
                    row5.CreateCell(k).SetCellValue(SDXtitle);
                    row5.GetCell(k).CellStyle = getCellStyleCenter(book);
                    k++;
                    for (int i = 0; i < dic310.Count - 1; i++)
                    {
                        var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic310[i].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);
                        int REPORTVALUE = 0;
                        foreach (var v in templist)
                        {
                            if (!string.IsNullOrEmpty(v.REPORTVALUE))
                            {
                                REPORTVALUE += int.Parse(v.REPORTVALUE);
                            }
                        }
                        row5.CreateCell(k).SetCellValue(templist.Count > 0 ? REPORTVALUE : 0);
                        row5.GetCell(k).CellStyle = getCellStyleCenter(book);
                        k++;
                    }
                    //var bzlist1 = yearreportlist.FindAll(a => a.REPORTCODE == dic310[dic310.Count - 1].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);
                    //row5.CreateCell(k).SetCellValue(bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
                    //row5.GetCell(k).CellStyle = getCellStyleCenter(book);
                    row5.CreateCell(k).SetCellValue("");
                    row5.GetCell(k).CellStyle = getCellStyleCenter(book);
                    k++;
                }
                z++;
            }
            #endregion

            #region 详细数据行
            int rowIndex = 0;
            for (int i = 0; i < result.Count; i++)
            {
                int x = 0;
                IRow row6 = sheet1.CreateRow(rowIndex + 8);
                row6.CreateCell(x).SetCellValue(result[i].ORGNAME);
                row6.GetCell(x).CellStyle = getCellStyleCenter(book);
                x = 2;
                List<FIRERECORD_REPORT8_Model> templist = new List<FIRERECORD_REPORT8_Model>();
                if (PublicCls.OrgIsShi(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO.Substring(4, 11) == result[i].ORGNO.Substring(4, 11)).ToList();//判断是否州级
                if (PublicCls.OrgIsXian(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                if (PublicCls.OrgIsZhen(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                if (PublicCls.OrgIsCun(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO == result[i].ORGNO).ToList();
                for (int k = 0; k < dic310.Count - 1; k++)
                {
                    var templist2 = templist.FindAll(a => a.REPORTCODE == dic310[k].DICTVALUE);
                    int REPORTVALUE = 0;
                    foreach (var v in templist2)
                    {
                        if (!string.IsNullOrEmpty(v.REPORTVALUE))
                        {
                            REPORTVALUE += int.Parse(v.REPORTVALUE);
                        }
                    }
                    row6.CreateCell(x).SetCellValue(templist2.Count > 0 ? REPORTVALUE : 0);
                    row6.GetCell(x).CellStyle = getCellStyleCenter(book);
                    x++;
                }
                var bzlist1 = templist.FindAll(a => a.REPORTCODE == dic310[dic310.Count - 1].DICTVALUE);
                row6.CreateCell(x).SetCellValue(bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
                row6.GetCell(x).CellStyle = getCellStyleCenter(book);
                x++;
                rowIndex++;
            }
            #endregion

            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }
        #endregion

        #region 火情档案_森林防火办事机构人员统计
        /// <summary>
        /// 火情档案_森林防火办事机构人员统计--数据增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT9Manager()
        {
            FIRERECORD_REPORT9_Model m = new FIRERECORD_REPORT9_Model();
            m.BYORGNO = Request.Params["BYORGNO"];
            m.REPORTYEAR = Request.Params["REPORTYEAR"];
            m.REPORTCODE = Request.Params["REPORTCODE"];
            m.SSXTYPELEVELCODE = Request.Params["SSXTYPELEVELCODE"];
            m.REPORTVALUE = Request.Params["REPORTVALUE"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(FIRERECORD_REPORT9Cls.Manager(m)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 森林防火办事机构人员统计
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT9Man()
        {
            string Method = Request.Params["Method"];
            string FIRERECORD_REPORT9ID = Request.Params["FIRERECORD_REPORT9ID"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            FIRERECORD_REPORT9_Model model = new FIRERECORD_REPORT9_Model();
            if (!string.IsNullOrEmpty(FIRERECORD_REPORT9ID))
            {
                if (Method == "Add")
                {
                    model = FIRERECORD_REPORT9Cls.getModel(new FIRERECORD_REPORT9_SW { FIRERECORD_REPORT9ID = FIRERECORD_REPORT9ID });
                }
            }
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOptionByORGNO(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), OnlyGetShiXian = ORGNO, TopORGNO = ORGNO });//获取州级和市县
            //vdorg(ORGNO);
            ViewBag.REPORTYEAR = DateTime.Now.ToString("yyyy");
            ViewBag.Method = Method;
            List<T_SYS_DICTModel> dic305list = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "305" }).ToList();
            List<T_SYS_DICTModel> dic311List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "311" }).ToList();
            ViewBag.dic305Value = T_SYS_DICTCls.getDicValueStr(dic305list);
            ViewBag.dic311Value = T_SYS_DICTCls.getDicValueStr(dic311List);
            ViewBag.dic305Count = dic305list.Count;
            ViewBag.dic311Count = dic311List.Count;
            return View(model);
        }


        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetReport9json()
        {
            string REPORTYEAR = Request.Params["REPORTYEAR"];
            string BYORGNO = Request.Params["BYORGNO"];
            return Content(JsonConvert.SerializeObject(FIRERECORD_REPORT9Cls.getListModel(new FIRERECORD_REPORT9_SW { BYORGNO = BYORGNO, REPORTYEAR = REPORTYEAR, })), "text/html;charset=UTF-8");
        }
        #endregion

        #region 火情档案_森林防火办事机构人员统计年报表
        /// <summary>
        /// 火情档案_森林防火办事机构人员统计年报表
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT9()
        {
            pubViewBag("041010", "041010", "森林防火办事机构人员统计年报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.YEAR = DateTime.Now.ToString("yyyy");
            return View();
        }

        /// <summary>
        ///火情档案_森林防火办事机构人员统计年报表
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT9Query()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string BYORGNO = Request.Params["BYORGNO"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = ResultList(ORGNO);//获取组织机构编码
            // List<FIRERECORD_REPORT9_Model> yearreportlist = FIRERECORD_REPORT9Cls.getListModel(new FIRERECORD_REPORT9_SW { REPORTYEAR = YEAR, }).ToList();
            List<FIRERECORD_REPORT9_Model> yearreportlist = new List<FIRERECORD_REPORT9_Model>();
            if (ORGNO.Substring(4, 11) == "00000000000")
            {
                yearreportlist = FIRERECORD_REPORT9Cls.getListModel(new FIRERECORD_REPORT9_SW { REPORTYEAR = YEAR, }).ToList();
            }
            else
            {
                yearreportlist = FIRERECORD_REPORT9Cls.getListModel(new FIRERECORD_REPORT9_SW { BYORGNO = ORGNO, REPORTYEAR = YEAR, }).ToList();
            }
            List<T_SYS_DICTModel> dic305 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "305" }).ToList();//省地县
            List<T_SYS_DICTModel> dic311 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "311" }).ToList();//组织机构统计年报
            #endregion

            #region 数据表
            sb.AppendFormat("<table  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"2\"colspan=\"2\"style=\" width:50px;\">单 位/项 目</th>");
            sb.AppendFormat("<th colspan=\"2\">实有人数</th>");
            sb.AppendFormat("<th colspan=\"3\">年龄</th>");
            sb.AppendFormat("<th colspan=\"4\">职务</th>");
            sb.AppendFormat("<th colspan=\"3\"style=\" width:100px;\">文化程度</th>");
            sb.AppendFormat("<th colspan=\"3\">技术职称</th>");
            sb.AppendFormat("<th colspan=\"3\">防火工作年限</th>");
            sb.AppendFormat("<th rowspan=\"2\">备注</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>合计</th>");
            sb.AppendFormat("<th>其中女</th>");
            for (int i = 2; i < dic311.Count - 1; i++)
            {
                string title = dic311[i].DICTNAME;
                if (!string.IsNullOrEmpty(dic311[i].STANDBY1))
                    sb.AppendFormat("<th>{0}</th>", title);
            }
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");

            #region 第一行：列号
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td class=\"center\"colspan=\"2\">甲</td>");
            sb.AppendFormat("<td class=\"center\">1</td>");
            sb.AppendFormat("<td class=\"center\">2</td>");
            sb.AppendFormat("<td class=\"center\">3</td>");
            sb.AppendFormat("<td class=\"center\">4</td>");
            sb.AppendFormat("<td class=\"center\">5</td>");
            sb.AppendFormat("<td class=\"center\">6</td>");
            sb.AppendFormat("<td class=\"center\">7</td>");
            sb.AppendFormat("<td class=\"center\">8</td>");
            sb.AppendFormat("<td class=\"center\">9</td>");
            sb.AppendFormat("<td class=\"center\">10</td>");
            sb.AppendFormat("<td class=\"center\">11</td>");
            sb.AppendFormat("<td class=\"center\">12</td>");
            sb.AppendFormat("<td class=\"center\">13</td>");
            sb.AppendFormat("<td class=\"center\">14</td>");
            sb.AppendFormat("<td class=\"center\">15</td>");
            sb.AppendFormat("<td class=\"center\">16</td>");
            sb.AppendFormat("<td class=\"center\">17</td>");
            sb.AppendFormat("<td class=\"center\">18</td>");
            sb.AppendFormat("<td class=\"center\">19</td>");
            sb.AppendFormat("</tr>");
            #endregion

            #region 合计
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"center\"colspan=\"2\">合   计</td>");
            List<int> _templist = new List<int>();
            int total = 0;
            for (int i = 0; i < 2; i++)
            {
                var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic311[i].DICTVALUE);

                foreach (var v in templist)
                {
                    if (!string.IsNullOrEmpty(v.REPORTVALUE))
                    {
                        total += int.Parse(v.REPORTVALUE);
                    }
                }
            }
            sb.AppendFormat("<td>{0}</td>", total);
            for (int i = 1; i < dic311.Count - 1; i++)
            {
                var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic311[i].DICTVALUE);
                int REPORTVALUE = 0;
                foreach (var v in templist)
                {
                    if (!string.IsNullOrEmpty(v.REPORTVALUE))
                    {
                        REPORTVALUE += int.Parse(v.REPORTVALUE);
                    }
                }
                sb.AppendFormat("<td>{0}</td>", templist.Count > 0 ? REPORTVALUE : 0);
            }
            //var bzlist = yearreportlist.FindAll(a => a.REPORTCODE == dic311[dic311.Count - 1].DICTVALUE);
            //sb.AppendFormat("<td>{0}</td>", bzlist.Count > 0 ? bzlist[0].REPORTVALUE : "");
            sb.AppendFormat("<td></td>");
            sb.AppendFormat("</tr>");
            #endregion

            #region 省地县
            foreach (var d1 in dic305)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (int.Parse(d1.DICTVALUE) % 2 == 0) ? "" : "row1");
                if (int.Parse(d1.DICTVALUE) == 1)
                {
                    sb.AppendFormat("<td rowspan=\"3\">其中</td>");
                    string title = d1.DICTNAME;
                    sb.AppendFormat("<td>{0}</td>", title);
                    int total1 = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic311[i].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);

                        foreach (var v in templist)
                        {
                            if (!string.IsNullOrEmpty(v.REPORTVALUE))
                            {
                                total1 += int.Parse(v.REPORTVALUE);
                            }
                        }
                    }
                    sb.AppendFormat("<td>{0}</td>", total1);
                    for (int i = 1; i < dic311.Count - 1; i++)
                    {
                        var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic311[i].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);
                        int REPORTVALUE = 0;
                        foreach (var v in templist)
                        {
                            if (!string.IsNullOrEmpty(v.REPORTVALUE))
                            {
                                REPORTVALUE += int.Parse(v.REPORTVALUE);
                            }
                        }
                        sb.AppendFormat("<td>{0}</td>", templist.Count > 0 ? REPORTVALUE : 0);
                    }
                    //var bzlist1 = yearreportlist.FindAll(a => a.REPORTCODE == dic311[dic311.Count - 1].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);
                    //sb.AppendFormat("<td>{0}</td>", bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
                    sb.AppendFormat("<td></td>");
                    sb.AppendFormat("</tr>");
                }
                else
                {
                    string title = d1.DICTNAME;
                    sb.AppendFormat("<td>{0}</td>", title);
                    int total1 = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic311[i].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);

                        foreach (var v in templist)
                        {
                            if (!string.IsNullOrEmpty(v.REPORTVALUE))
                            {
                                total1 += int.Parse(v.REPORTVALUE);
                            }
                        }
                    }
                    sb.AppendFormat("<td>{0}</td>", total1);
                    for (int i = 1; i < dic311.Count - 1; i++)
                    {
                        var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic311[i].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);
                        int REPORTVALUE = 0;
                        foreach (var v in templist)
                        {
                            if (!string.IsNullOrEmpty(v.REPORTVALUE))
                            {
                                REPORTVALUE += int.Parse(v.REPORTVALUE);
                            }
                        }
                        sb.AppendFormat("<td>{0}</td>", templist.Count > 0 ? REPORTVALUE : 0);
                    }
                    //var bzlist1 = yearreportlist.FindAll(a => a.REPORTCODE == dic311[dic311.Count - 1].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);
                    //sb.AppendFormat("<td>{0}</td>", bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
                    sb.AppendFormat("<td></td>");
                    sb.AppendFormat("</tr>");
                }
            }
            #endregion

            #region 详细数据行
            for (int i = 0; i < result.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"Center\"  colspan=\"2\">{0}</td>", result[i].ORGNAME);
                List<FIRERECORD_REPORT9_Model> templist = new List<FIRERECORD_REPORT9_Model>();
                if (PublicCls.OrgIsShi(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO.Substring(4, 11) == result[i].ORGNO.Substring(4, 11)).ToList();
                if (PublicCls.OrgIsXian(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                if (PublicCls.OrgIsZhen(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                if (PublicCls.OrgIsCun(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO == result[i].ORGNO).ToList();
                int total2 = 0;
                for (int k = 0; k < 2; k++)
                {
                    var templist2 = templist.FindAll(a => a.REPORTCODE == dic311[k].DICTVALUE);
                    foreach (var v in templist2)
                    {
                        if (!string.IsNullOrEmpty(v.REPORTVALUE))
                        {
                            total2 += int.Parse(v.REPORTVALUE);
                        }
                    }
                }
                sb.AppendFormat("<td>{0}</td>", total2);
                for (int j = 1; j < dic311.Count - 1; j++)
                {
                    var templist2 = templist.FindAll(a => a.REPORTCODE == dic311[j].DICTVALUE);
                    int REPORTVALUE = 0;
                    foreach (var v in templist2)
                    {
                        if (!string.IsNullOrEmpty(v.REPORTVALUE))
                        {
                            REPORTVALUE += int.Parse(v.REPORTVALUE);
                        }
                    }
                    sb.AppendFormat("<td>{0}</td>", templist2.Count > 0 ? REPORTVALUE : 0);
                }
                var bzlist1 = templist.FindAll(a => a.REPORTCODE == dic311[dic311.Count - 1].DICTVALUE);
                sb.AppendFormat("<td>{0}</td>", bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
            }
            #endregion

            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult FIRERECORD_REPORT9ExportExcel()
        {
            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = ResultList(ORGNO);//获取组织机构编码
            // List<FIRERECORD_REPORT9_Model> yearreportlist = FIRERECORD_REPORT9Cls.getListModel(new FIRERECORD_REPORT9_SW { REPORTYEAR = YEAR }).ToList();
            List<FIRERECORD_REPORT9_Model> yearreportlist = new List<FIRERECORD_REPORT9_Model>();
            if (ORGNO.Substring(4, 11) == "00000000000")
            {
                yearreportlist = FIRERECORD_REPORT9Cls.getListModel(new FIRERECORD_REPORT9_SW { REPORTYEAR = YEAR, }).ToList();
            }
            else
            {
                yearreportlist = FIRERECORD_REPORT9Cls.getListModel(new FIRERECORD_REPORT9_SW { BYORGNO = ORGNO, REPORTYEAR = YEAR, }).ToList();
            }
            List<T_SYS_DICTModel> dic305 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "305" }).ToList();//省地县
            List<T_SYS_DICTModel> dic311 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "311" }).ToList();//组织机构统计年报
            int colsCount = (dic311.Count + 2);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "041010", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = YEAR + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("森林防火办事机构人员统计报表");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.Height = 2 * 350;
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.Height = 2 * 350;
            rowHead1.CreateCell(0).SetCellValue("单位/项目");
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(2).SetCellValue("实有人数");
            rowHead1.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(4).SetCellValue("年龄");
            rowHead1.GetCell(4).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(7).SetCellValue("职务");
            rowHead1.GetCell(7).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(11).SetCellValue("文化程度");
            rowHead1.GetCell(11).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(14).SetCellValue("技术职称");
            rowHead1.GetCell(14).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(17).SetCellValue("防火工作年限");
            rowHead1.GetCell(17).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(20).SetCellValue("备 注");
            rowHead1.GetCell(20).CellStyle = getCellStyleHead(book);

            IRow rowHead2 = sheet1.CreateRow(2);
            rowHead2.CreateCell(2).SetCellValue("合计");
            rowHead2.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(3).SetCellValue("其中女");
            rowHead2.GetCell(3).CellStyle = getCellStyleHead(book);
            for (int i = 2; i < dic311.Count; i++)
            {
                // int x = 2;
                string name = dic311[i].DICTNAME;
                if (!string.IsNullOrEmpty(dic311[i].STANDBY1))
                    rowHead2.CreateCell(i + 2).SetCellValue(name);
                rowHead2.GetCell(i + 2).CellStyle = getCellStyleHead(book);
            }

            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount - 1));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 0, 1));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 2, 3));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 4, 6));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 7, 10));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 11, 13));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 14, 16));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 17, 19));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 20, 20));

            sheet1.AddMergedRegion(new CellRangeAddress(3, 3, 0, 1));
            sheet1.AddMergedRegion(new CellRangeAddress(4, 6, 0, 0));
            for (int i = 0; i < result.Count; i++)
            {
                sheet1.AddMergedRegion(new CellRangeAddress(i + 7, i + 7, 0, 1));
            }
            #endregion

            #region 表身及数据

            #region 合计
            IRow row3 = sheet1.CreateRow(3);
            int j = 0;
            row3.CreateCell(j).SetCellValue("合计");
            row3.GetCell(j).CellStyle = getCellStyleCenter(book);
            j = 2;
            List<int> _templist = new List<int>();
            int total = 0;
            for (int i = 0; i < 2; i++)
            {
                var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic311[i].DICTVALUE);

                foreach (var v in templist)
                {
                    if (!string.IsNullOrEmpty(v.REPORTVALUE))
                    {
                        total += int.Parse(v.REPORTVALUE);
                    }
                }
            }
            row3.CreateCell(j).SetCellValue(total);
            row3.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            for (int i = 1; i < dic311.Count - 1; i++)
            {
                var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic311[i].DICTVALUE);
                int REPORTVALUE = 0;
                foreach (var v in templist)
                {
                    if (!string.IsNullOrEmpty(v.REPORTVALUE))
                    {
                        REPORTVALUE += int.Parse(v.REPORTVALUE);
                    }
                }
                row3.CreateCell(j).SetCellValue(templist.Count > 0 ? REPORTVALUE : 0);
                row3.GetCell(j).CellStyle = getCellStyleCenter(book);
                j++;
            }
            //var bzlist = yearreportlist.FindAll(a => a.REPORTCODE == dic311[dic311.Count - 1].DICTVALUE);
            //row3.CreateCell(j).SetCellValue(bzlist.Count > 0 ? bzlist[0].REPORTVALUE : "");
            //row3.GetCell(j).CellStyle = getCellStyleCenter(book);
            row3.CreateCell(j).SetCellValue("");
            row3.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            #endregion

            #region 省县地级
            int z = 4;
            foreach (var d1 in dic305)
            {
                IRow row4 = sheet1.CreateRow(z);
                int k = 0;
                if (int.Parse(d1.DICTVALUE) == 1)
                {
                    row4.CreateCell(k).SetCellValue("其中");
                    row4.GetCell(k).CellStyle = getCellStyleCenter(book);
                    k++;
                    string SDXtitle = d1.DICTNAME;
                    row4.CreateCell(k).SetCellValue(SDXtitle);
                    row4.GetCell(k).CellStyle = getCellStyleCenter(book);
                    k++;
                    int total1 = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic311[i].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);

                        foreach (var v in templist)
                        {
                            if (!string.IsNullOrEmpty(v.REPORTVALUE))
                            {
                                total1 += int.Parse(v.REPORTVALUE);
                            }
                        }
                    }
                    row4.CreateCell(k).SetCellValue(total1);
                    row4.GetCell(k).CellStyle = getCellStyleCenter(book);
                    k++;
                    for (int i = 1; i < dic311.Count - 1; i++)
                    {
                        var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic311[i].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);
                        int REPORTVALUE = 0;
                        foreach (var v in templist)
                        {
                            if (!string.IsNullOrEmpty(v.REPORTVALUE))
                            {
                                REPORTVALUE += int.Parse(v.REPORTVALUE);
                            }
                        }
                        row4.CreateCell(k).SetCellValue(templist.Count > 0 ? REPORTVALUE : 0);
                        row4.GetCell(k).CellStyle = getCellStyleCenter(book);
                        k++;
                    }
                    //var bzlist1 = yearreportlist.FindAll(a => a.REPORTCODE == dic311[dic311.Count - 1].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);
                    //row4.CreateCell(k).SetCellValue(bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
                    //row4.GetCell(k).CellStyle = getCellStyleCenter(book);
                    row4.CreateCell(j).SetCellValue("");
                    row4.GetCell(j).CellStyle = getCellStyleCenter(book);
                    k++;
                }
                else
                {
                    k = 1;
                    string SDXtitle = d1.DICTNAME;
                    row4.CreateCell(k).SetCellValue(SDXtitle);
                    row4.GetCell(k).CellStyle = getCellStyleCenter(book);
                    k++;
                    int total1 = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic311[i].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);

                        foreach (var v in templist)
                        {
                            if (!string.IsNullOrEmpty(v.REPORTVALUE))
                            {
                                total1 += int.Parse(v.REPORTVALUE);
                            }
                        }
                    }
                    row4.CreateCell(k).SetCellValue(total1);
                    row4.GetCell(k).CellStyle = getCellStyleCenter(book);
                    k++;
                    for (int i = 1; i < dic311.Count - 1; i++)
                    {
                        var templist = yearreportlist.FindAll(a => a.REPORTCODE == dic311[i].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);
                        int REPORTVALUE = 0;
                        foreach (var v in templist)
                        {
                            if (!string.IsNullOrEmpty(v.REPORTVALUE))
                            {
                                REPORTVALUE += int.Parse(v.REPORTVALUE);
                            }
                        }
                        row4.CreateCell(k).SetCellValue(templist.Count > 0 ? REPORTVALUE : 0);
                        row4.GetCell(k).CellStyle = getCellStyleCenter(book);
                        k++;
                    }
                    //var bzlist1 = yearreportlist.FindAll(a => a.REPORTCODE == dic311[dic311.Count - 1].DICTVALUE && a.SSXTYPELEVELCODE == d1.DICTVALUE);
                    //row4.CreateCell(k).SetCellValue(bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
                    //row4.GetCell(k).CellStyle = getCellStyleCenter(book);
                    row4.CreateCell(k).SetCellValue("");
                    row4.GetCell(k).CellStyle = getCellStyleCenter(book);
                    k++;
                }
                z++;
            }

            #endregion

            #region 详细数据行
            int rowIndex = 0;
            for (int i = 0; i < result.Count; i++)
            {
                int x = 0;
                IRow row5 = sheet1.CreateRow(rowIndex + 7);
                row5.CreateCell(x).SetCellValue(result[i].ORGNAME);
                row5.GetCell(x).CellStyle = getCellStyleCenter(book);
                x = 2;
                List<FIRERECORD_REPORT9_Model> templist = new List<FIRERECORD_REPORT9_Model>();
                if (PublicCls.OrgIsShi(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO.Substring(4, 11) == result[i].ORGNO.Substring(4, 11)).ToList();
                if (PublicCls.OrgIsXian(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                if (PublicCls.OrgIsZhen(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                if (PublicCls.OrgIsCun(result[i].ORGNO))
                    templist = yearreportlist.FindAll(a => a.BYORGNO == result[i].ORGNO).ToList();
                int total2 = 0;
                for (int k = 0; k < 2; k++)
                {
                    var templist2 = templist.FindAll(a => a.REPORTCODE == dic311[k].DICTVALUE);
                    foreach (var v in templist2)
                    {
                        if (!string.IsNullOrEmpty(v.REPORTVALUE))
                        {
                            total2 += int.Parse(v.REPORTVALUE);
                        }
                    }
                }
                row5.CreateCell(x).SetCellValue(total2);
                row5.GetCell(x).CellStyle = getCellStyleCenter(book);
                x++;
                for (int k = 1; k < dic311.Count - 1; k++)
                {
                    var templist2 = templist.FindAll(a => a.REPORTCODE == dic311[k].DICTVALUE);
                    int REPORTVALUE = 0;
                    foreach (var v in templist2)
                    {
                        if (!string.IsNullOrEmpty(v.REPORTVALUE))
                        {
                            REPORTVALUE += int.Parse(v.REPORTVALUE);
                        }
                    }
                    row5.CreateCell(x).SetCellValue(templist2.Count > 0 ? REPORTVALUE : 0);
                    row5.GetCell(x).CellStyle = getCellStyleCenter(book);
                    x++;
                }
                var bzlist1 = templist.FindAll(a => a.REPORTCODE == dic311[dic311.Count - 1].DICTVALUE);
                row5.CreateCell(x).SetCellValue(bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
                row5.GetCell(x).CellStyle = getCellStyleCenter(book);
                x++;
                rowIndex++;
            }
            #endregion

            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }
        #endregion

        #region 火情档案_森林防火基础设施统计
        /// <summary>
        /// 火情档案_森林防火基础设施统计一--数据增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT10Manager()
        {
            FIRERECORD_REPORT10_Model m = new FIRERECORD_REPORT10_Model();
            m.BYORGNO = Request.Params["BYORGNO"];
            m.REPORTYEAR = Request.Params["REPORTYEAR"];
            m.REPORTCODE = Request.Params["REPORTCODE"];
            m.REPORTVALUE = Request.Params["REPORTVALUE"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(FIRERECORD_REPORT10Cls.Manager(m)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 森林防火基础设施统计
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT10Man()
        {
            string Method = Request.Params["Method"];
            string FIRERECORD_REPORT10ID = Request.Params["FIRERECORD_REPORT10ID"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            FIRERECORD_REPORT10_Model model = new FIRERECORD_REPORT10_Model();
            if (!string.IsNullOrEmpty(FIRERECORD_REPORT10ID))
            {
                if (Method == "Add")
                {
                    model = FIRERECORD_REPORT10Cls.getModel(new FIRERECORD_REPORT10_SW { FIRERECORD_REPORT10ID = FIRERECORD_REPORT10ID });
                }
            }
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOptionByORGNO(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), OnlyGetShiXian = ORGNO, TopORGNO = ORGNO });//获取州级和市县
            // vdorg(ORGNO);
            ViewBag.REPORTYEAR = DateTime.Now.ToString("yyyy");
            ViewBag.Method = Method;
            List<T_SYS_DICTModel> dic312List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "312" }).ToList();
            ViewBag.dic312Value = T_SYS_DICTCls.getDicValueStr(dic312List);
            ViewBag.dic312Count = dic312List.Count;
            return View(model);
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetReport10json()
        {
            string REPORTYEAR = Request.Params["REPORTYEAR"];
            string BYORGNO = Request.Params["BYORGNO"];
            List<FIRERECORD_REPORT10_Model> _accountlist = FIRERECORD_REPORT10Cls.getListModel(new FIRERECORD_REPORT10_SW { BYORGNO = BYORGNO, REPORTYEAR = REPORTYEAR, }).ToList();
            List<FIRERECORD_REPORT10_Model> _list = _accountlist.FindAll(a => a.REPORTYEAR == REPORTYEAR);
            return Content(JsonConvert.SerializeObject(_list));
            //return Content(JsonConvert.SerializeObject(FIRERECORD_REPORT10Cls.getListModel(new FIRERECORD_REPORT10_SW { BYORGNO = BYORGNO, REPORTYEAR = REPORTYEAR, })), "text/html;charset=UTF-8");
        }
        #endregion

        #region 火情档案_森林防火基础设施统计年报表一
        /// <summary>
        /// 火情档案_森林防火基础设施统计年报表一
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT10()
        {
            pubViewBag("041011", "041011", "森林防火基础设施统计年报表一");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.YEAR = DateTime.Now.ToString("yyyy");
            return View();
        }

        /// <summary>
        ///火情档案_森林防火基础设施统计年报表一
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT10Query()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string BYORGNO = Request.Params["BYORGNO"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = ResultList(ORGNO);//获取组织机构编码
            // List<FIRERECORD_REPORT10_Model> _totalyearreportlist = FIRERECORD_REPORT10Cls.getListModel(new FIRERECORD_REPORT10_SW { REPORTYEAR = YEAR, }).ToList();
            List<FIRERECORD_REPORT10_Model> _totalyearreportlist = new List<FIRERECORD_REPORT10_Model>();
            if (ORGNO.Substring(4, 11) == "00000000000")
            {
                _totalyearreportlist = FIRERECORD_REPORT10Cls.getListModel(new FIRERECORD_REPORT10_SW { REPORTYEAR = YEAR, }).ToList();
            }
            else
            {
                _totalyearreportlist = FIRERECORD_REPORT10Cls.getListModel(new FIRERECORD_REPORT10_SW { BYORGNO = ORGNO, REPORTYEAR = YEAR, }).ToList();
            }
            var _yearreportlist = _totalyearreportlist.FindAll(a => a.REPORTYEAR == YEAR);
            List<T_SYS_DICTModel> dic312 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "312" }).ToList();//基础设施统计年报表一
            #endregion

            #region 数据表
            sb.AppendFormat("<table  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"3\"style=\" width:100px;\">单  位/项  目</th>");
            sb.AppendFormat("<th colspan=\"4\">瞭 望 台   (座)</th>");
            sb.AppendFormat("<th colspan=\"2\">望 远 镜  (台)</th>");
            sb.AppendFormat("<th rowspan=\"3\">专用</br>电话线</br> (公里)</th>");
            sb.AppendFormat("<th rowspan=\"3\">有线</br>电话机</br>（部）</th>");
            sb.AppendFormat("<th rowspan=\"3\">图文</br>传真机</br>（部）</th>");
            sb.AppendFormat("<th rowspan=\"3\">微型</br>计算机</br>（部）</th>");
            sb.AppendFormat("<th rowspan=\"3\">数传</br>终端机</br>（部）</th>");
            sb.AppendFormat("<th colspan=\"8\">无&nbsp;线&nbsp;电&nbsp;台&nbsp;（部）</th>");
            sb.AppendFormat("<th rowspan=\"3\">备注</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"2\">计</th>");
            sb.AppendFormat("<th rowspan=\"2\">铁质</th>");
            sb.AppendFormat("<th rowspan=\"2\">砖瓦</th>");
            sb.AppendFormat("<th rowspan=\"2\">木质</th>");
            sb.AppendFormat("<th rowspan=\"2\">计</th>");
            sb.AppendFormat("<th rowspan=\"2\">其中</br>40倍</th>");
            sb.AppendFormat("<th rowspan=\"2\">计</th>");
            sb.AppendFormat("<th  colspan=\"3\">短  波</th>");
            sb.AppendFormat("<th  colspan=\"4\">甚、特高频  （部）</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>小计</th>");
            sb.AppendFormat("<th><15W</th>");
            sb.AppendFormat("<th>>=15W</th>");
            sb.AppendFormat("<th>小计</th>");
            sb.AppendFormat("<th><=5W</th>");
            sb.AppendFormat("<th>>5W</th>");
            sb.AppendFormat("<th>中继台</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");

            #region 第一行：列号
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td class=\"center\">甲</td>");
            sb.AppendFormat("<td class=\"center\">1</td>");
            sb.AppendFormat("<td class=\"center\">2</td>");
            sb.AppendFormat("<td class=\"center\">3</td>");
            sb.AppendFormat("<td class=\"center\">4</td>");
            sb.AppendFormat("<td class=\"center\">5</td>");
            sb.AppendFormat("<td class=\"center\">6</td>");
            sb.AppendFormat("<td class=\"center\">7</td>");
            sb.AppendFormat("<td class=\"center\">8</td>");
            sb.AppendFormat("<td class=\"center\">9</td>");
            sb.AppendFormat("<td class=\"center\">10</td>");
            sb.AppendFormat("<td class=\"center\">11</td>");
            sb.AppendFormat("<td class=\"center\">12</td>");
            sb.AppendFormat("<td class=\"center\">13</td>");
            sb.AppendFormat("<td class=\"center\">14</td>");
            sb.AppendFormat("<td class=\"center\">15</td>");
            sb.AppendFormat("<td class=\"center\">16</td>");
            sb.AppendFormat("<td class=\"center\">17</td>");
            sb.AppendFormat("<td class=\"center\">18</td>");
            sb.AppendFormat("<td class=\"center\">19</td>");
            sb.AppendFormat("<td class=\"center\">20</td>");
            sb.AppendFormat("</tr>");
            #endregion

            #region 累计实有
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"center\">累 计 实 有</td>");
            List<float> total = CalHJ1(_totalyearreportlist);
            sb.AppendFormat("<td>{0}</td>", total[0] + total[1] + total[2]);
            for (int i = 0; i < dic312.Count - 6; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total[i]);
            }
            float HJ = (total[10]) + (total[11]) + (total[12]) + (total[13]) + (total[14]);
            float DBHJ = (total[10]) + (total[11]);
            float TGPHJ = (total[12]) + (total[13]) + (total[14]);
            sb.AppendFormat("<td>{0}</td>", HJ);
            sb.AppendFormat("<td>{0}</td>", DBHJ);
            sb.AppendFormat("<td>{0}</td>", total[10]);
            sb.AppendFormat("<td>{0}</td>", total[11]);
            sb.AppendFormat("<td>{0}</td>", TGPHJ);
            sb.AppendFormat("<td>{0}</td>", total[12]);
            sb.AppendFormat("<td>{0}</td>", total[13]);
            sb.AppendFormat("<td>{0}</td>", total[14]);
            //var bzlist = _yearreportlist.FindAll(a => a.REPORTCODE == dic312[dic312.Count - 1].DICTVALUE);
            //sb.AppendFormat("<td>{0}</td>", bzlist.Count > 0 ? bzlist[0].REPORTVALUE : "");
            sb.AppendFormat("<td>{0}</td>", "");
            sb.AppendFormat("</tr>");
            #endregion

            #region 本年合计
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td class=\"center\">本 年 合 计</td>");
            List<float> total1 = CalHJ1(_yearreportlist);
            sb.AppendFormat("<td>{0}</td>", total1[0] + total1[1] + total1[2]);
            for (int i = 0; i < dic312.Count - 6; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total1[i]);
            }
            float HJ1 = total1[10] + total1[11] + total1[12] + total1[13] + total1[14];
            float DBHJ1 = total1[10] + total1[11];
            float TGPHJ1 = total1[12] + total1[13] + total1[14];
            sb.AppendFormat("<td>{0}</td>", HJ1);
            sb.AppendFormat("<td>{0}</td>", DBHJ1);
            sb.AppendFormat("<td>{0}</td>", total1[10]);
            sb.AppendFormat("<td>{0}</td>", total1[11]);
            sb.AppendFormat("<td>{0}</td>", TGPHJ1);
            sb.AppendFormat("<td>{0}</td>", total1[12]);
            sb.AppendFormat("<td>{0}</td>", total1[13]);
            sb.AppendFormat("<td>{0}</td>", total1[14]);
            //var bzlist1 = _yearreportlist.FindAll(a => a.REPORTCODE == dic312[dic312.Count - 1].DICTVALUE);
            //sb.AppendFormat("<td>{0}</td>", bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
            sb.AppendFormat("<td>{0}</td>", "");
            sb.AppendFormat("</tr>");
            #endregion

            #region 详细数据行
            for (int i = 0; i < result.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"Center\" >{0}</td>", result[i].ORGNAME);
                List<FIRERECORD_REPORT10_Model> templist = new List<FIRERECORD_REPORT10_Model>();
                if (PublicCls.OrgIsShi(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(4, 11) == result[i].ORGNO.Substring(4, 11)).ToList();
                if (PublicCls.OrgIsXian(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                if (PublicCls.OrgIsZhen(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                if (PublicCls.OrgIsCun(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO == result[i].ORGNO).ToList();

                List<float> total2 = CalHJ1(templist);
                sb.AppendFormat("<td>{0}</td>", total2[0] + total2[1] + total2[2]);
                for (int j = 0; j < dic312.Count - 6; j++)
                {
                    sb.AppendFormat("<td>{0}</td>", total2[j]);
                }
                float HJ2 = total2[10] + total2[11] + total2[12] + total2[13] + total2[14];
                float DBHJ2 = total2[10] + total2[11];
                float TGPHJ2 = total2[12] + total2[13] + total2[14];
                sb.AppendFormat("<td>{0}</td>", HJ2);
                sb.AppendFormat("<td>{0}</td>", DBHJ2);
                sb.AppendFormat("<td>{0}</td>", total2[10]);
                sb.AppendFormat("<td>{0}</td>", total2[11]);
                sb.AppendFormat("<td>{0}</td>", TGPHJ2);
                sb.AppendFormat("<td>{0}</td>", total2[12]);
                sb.AppendFormat("<td>{0}</td>", total2[13]);
                sb.AppendFormat("<td>{0}</td>", total2[14]);
                var bzlist2 = templist.FindAll(a => a.REPORTCODE == dic312[dic312.Count - 1].DICTVALUE);
                sb.AppendFormat("<td>{0}</td>", bzlist2.Count > 0 ? bzlist2[0].REPORTVALUE : "");

            }
            #endregion

            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult FIRERECORD_REPORT10ExportExcel()
        {
            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = ResultList(ORGNO);//获取组织机构编码
            // List<FIRERECORD_REPORT10_Model> _totalyearreportlist = FIRERECORD_REPORT10Cls.getListModel(new FIRERECORD_REPORT10_SW { REPORTYEAR = YEAR, }).ToList();
            List<FIRERECORD_REPORT10_Model> _totalyearreportlist = new List<FIRERECORD_REPORT10_Model>();
            if (ORGNO.Substring(4, 11) == "00000000000")
            {
                _totalyearreportlist = FIRERECORD_REPORT10Cls.getListModel(new FIRERECORD_REPORT10_SW { REPORTYEAR = YEAR, }).ToList();
            }
            else
            {
                _totalyearreportlist = FIRERECORD_REPORT10Cls.getListModel(new FIRERECORD_REPORT10_SW { BYORGNO = ORGNO, REPORTYEAR = YEAR, }).ToList();
            }
            var _yearreportlist = _totalyearreportlist.FindAll(a => a.REPORTYEAR == YEAR);
            List<T_SYS_DICTModel> dic312 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "312" }).ToList();//基础设施统计年报表一
            int colsCount = (dic312.Count + 4);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "041011", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = YEAR + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("森林防火办事机构人员统计报表");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            for (int i = 0; i < colsCount; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 20 * 256);
                else
                    sheet1.SetColumnWidth(i, 10 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.Height = 2 * 350;
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.Height = 2 * 350;
            rowHead1.CreateCell(0).SetCellValue("单位/项目");
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(1).SetCellValue("瞭 望 台 （座）");
            rowHead1.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(5).SetCellValue("望远镜(台)");
            rowHead1.GetCell(5).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(7).SetCellValue("专用电话线 （公里）");
            rowHead1.GetCell(7).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(8).SetCellValue("有线电话机（部）");
            rowHead1.GetCell(8).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(9).SetCellValue("图文传真机（部）");
            rowHead1.GetCell(9).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(10).SetCellValue("微型计算机（部）");
            rowHead1.GetCell(10).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(11).SetCellValue("数传终端机（部）");
            rowHead1.GetCell(11).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(12).SetCellValue("无  线  电  台 （部）");
            rowHead1.GetCell(12).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(20).SetCellValue("备 注");
            rowHead1.GetCell(20).CellStyle = getCellStyleHead(book);

            IRow rowHead2 = sheet1.CreateRow(2);
            rowHead2.CreateCell(1).SetCellValue("合计");
            rowHead2.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(2).SetCellValue("铁质");
            rowHead2.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(3).SetCellValue("砖瓦");
            rowHead2.GetCell(3).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(4).SetCellValue("木质");
            rowHead2.GetCell(4).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(5).SetCellValue("计");
            rowHead2.GetCell(5).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(6).SetCellValue("其中40倍");
            rowHead2.GetCell(6).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(12).SetCellValue("计");
            rowHead2.GetCell(12).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(13).SetCellValue("短波");
            rowHead2.GetCell(13).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(16).SetCellValue(" 甚、特高频（部）");
            rowHead2.GetCell(16).CellStyle = getCellStyleHead(book);

            IRow rowHead3 = sheet1.CreateRow(3);
            rowHead3.CreateCell(13).SetCellValue("小计");
            rowHead3.GetCell(13).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(14).SetCellValue("<15W");
            rowHead3.GetCell(14).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(15).SetCellValue(">=15W");
            rowHead3.GetCell(15).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(16).SetCellValue("小计");
            rowHead3.GetCell(16).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(17).SetCellValue("<=5W");
            rowHead3.GetCell(17).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(18).SetCellValue(">5W");
            rowHead3.GetCell(18).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(19).SetCellValue("中继台");
            rowHead3.GetCell(19).CellStyle = getCellStyleHead(book);
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 0, 0));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 1, 4));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 5, 6));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 7, 7));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 8, 8));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 9, 9));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 10, 10));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 11, 11));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 12, 19));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 20, 20));

            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 1, 1));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 2, 2));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 3, 3));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 4, 4));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 5, 5));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 6, 6));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 12, 12));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 13, 15));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 16, 19));
            #endregion

            #region 表身及数据

            #region 累计实有
            IRow row4 = sheet1.CreateRow(4);
            int x = 0;
            row4.CreateCell(x).SetCellValue("累计实有");
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            x = FIRERECORD_REPORT10Excel(_totalyearreportlist, dic312, book, row4, x);
            //var bzlist = _yearreportlist.FindAll(a => a.REPORTCODE == dic312[dic312.Count - 1].DICTVALUE);
            //row4.CreateCell(x).SetCellValue(bzlist.Count > 0 ? bzlist[0].REPORTVALUE : "");
            //row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            row4.CreateCell(x).SetCellValue("");
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            #endregion

            #region 本年合计
            IRow row5 = sheet1.CreateRow(5);
            int j = 0;
            row5.CreateCell(j).SetCellValue("本年合计");
            row5.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            j = FIRERECORD_REPORT10Excel(_yearreportlist, dic312, book, row5, j);
            //var bzlist1 = _yearreportlist.FindAll(a => a.REPORTCODE == dic312[dic312.Count - 1].DICTVALUE);
            //row5.CreateCell(j).SetCellValue(bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
            //row5.GetCell(j).CellStyle = getCellStyleCenter(book);
            row5.CreateCell(j).SetCellValue("");
            row5.GetCell(j).CellStyle = getCellStyleCenter(book);
            #endregion

            #region 详细数据行
            int rowIndex = 0;
            for (int i = 0; i < result.Count; i++)
            {
                int k = 0;
                IRow row6 = sheet1.CreateRow(rowIndex + 6);
                row6.CreateCell(k).SetCellValue(result[i].ORGNAME);
                row6.GetCell(k).CellStyle = getCellStyleCenter(book);
                k++;
                List<FIRERECORD_REPORT10_Model> templist = new List<FIRERECORD_REPORT10_Model>();
                if (PublicCls.OrgIsShi(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(4, 11) == result[i].ORGNO.Substring(4, 11)).ToList();//判断是否为州级
                if (PublicCls.OrgIsXian(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                if (PublicCls.OrgIsZhen(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                if (PublicCls.OrgIsCun(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO == result[i].ORGNO).ToList();
                k = FIRERECORD_REPORT10Excel(templist, dic312, book, row6, k);
                var bzlist2 = templist.FindAll(a => a.REPORTCODE == dic312[dic312.Count - 1].DICTVALUE);
                row6.CreateCell(k).SetCellValue(bzlist2.Count > 0 ? bzlist2[0].REPORTVALUE : "");
                row6.GetCell(k).CellStyle = getCellStyleCenter(book);
                rowIndex++;
            }
            #endregion

            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }
        #endregion

        #region 火情档案_森林防火基础设施统计
        /// <summary>
        /// 火情档案_森林防火基础设施统计--数据增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT11Manager()
        {
            FIRERECORD_REPORT11_Model m = new FIRERECORD_REPORT11_Model();
            m.BYORGNO = Request.Params["BYORGNO"];
            m.REPORTYEAR = Request.Params["REPORTYEAR"];
            m.REPORTCODE = Request.Params["REPORTCODE"];
            m.REPORTVALUE = Request.Params["REPORTVALUE"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(FIRERECORD_REPORT11Cls.Manager(m)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 森林防火基础设施统计
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT11Man()
        {
            string Method = Request.Params["Method"];
            string FIRERECORD_REPORT11ID = Request.Params["FIRERECORD_REPORT11ID"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            FIRERECORD_REPORT11_Model model = new FIRERECORD_REPORT11_Model();
            if (!string.IsNullOrEmpty(FIRERECORD_REPORT11ID))
            {
                if (Method == "Add")
                {
                    model = FIRERECORD_REPORT11Cls.getModel(new FIRERECORD_REPORT11_SW { FIRERECORD_REPORT11ID = FIRERECORD_REPORT11ID });
                }
            }
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOptionByORGNO(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), OnlyGetShiXian = ORGNO, TopORGNO = ORGNO });//获取州级和市县
            //vdorg(ORGNO);
            ViewBag.REPORTYEAR = DateTime.Now.ToString("yyyy");
            ViewBag.Method = Method;
            List<T_SYS_DICTModel> dic313List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "313" }).ToList();
            ViewBag.dic313Value = T_SYS_DICTCls.getDicValueStr(dic313List);
            ViewBag.dic313Count = dic313List.Count;
            return View(model);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetReport11json()
        {
            string REPORTYEAR = Request.Params["REPORTYEAR"];
            string BYORGNO = Request.Params["BYORGNO"];
            List<FIRERECORD_REPORT11_Model> _accountlist = FIRERECORD_REPORT11Cls.getListModel(new FIRERECORD_REPORT11_SW { BYORGNO = BYORGNO, REPORTYEAR = REPORTYEAR, }).ToList();
            List<FIRERECORD_REPORT11_Model> _list = _accountlist.FindAll(a => a.REPORTYEAR == REPORTYEAR);
            return Content(JsonConvert.SerializeObject(_list));
            // return Content(JsonConvert.SerializeObject(FIRERECORD_REPORT11Cls.getListModel(new FIRERECORD_REPORT11_SW { BYORGNO = BYORGNO, REPORTYEAR = REPORTYEAR, })), "text/html;charset=UTF-8");
        }
        #endregion

        #region 火情档案_森林防火基础设施统计年报表二
        /// <summary>
        /// 火情档案_森林防火基础设施统计年报表二
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT11()
        {
            pubViewBag("041012", "041012", "森林防火基础设施统计年报表二");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.YEAR = DateTime.Now.ToString("yyyy");
            return View();
        }

        /// <summary>
        ///火情档案_森林防火基础设施统计
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT11Query()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string BYORGNO = Request.Params["BYORGNO"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = ResultList(ORGNO);//获取组织机构编码
            // List<FIRERECORD_REPORT11_Model> _totalyearreportlist = FIRERECORD_REPORT11Cls.getListModel(new FIRERECORD_REPORT11_SW { REPORTYEAR = YEAR, }).ToList();
            List<FIRERECORD_REPORT11_Model> _totalyearreportlist = new List<FIRERECORD_REPORT11_Model>();
            if (ORGNO.Substring(4, 11) == "00000000000")
            {
                _totalyearreportlist = FIRERECORD_REPORT11Cls.getListModel(new FIRERECORD_REPORT11_SW { REPORTYEAR = YEAR, }).ToList();
            }
            else
            {
                _totalyearreportlist = FIRERECORD_REPORT11Cls.getListModel(new FIRERECORD_REPORT11_SW { BYORGNO = ORGNO, REPORTYEAR = YEAR, }).ToList();
            }
            var _yearreportlist = _totalyearreportlist.FindAll(a => a.REPORTYEAR == YEAR);
            List<T_SYS_DICTModel> dic313 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "313" }).ToList();//基础设施统计年报表二
            #endregion

            #region 数据表
            sb.AppendFormat("<table  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"3\"style=\" width:100px;\">单  位/项  目</th>");
            sb.AppendFormat("<th colspan=\"3\">防火隔离带（公里）</th>");
            sb.AppendFormat("<th rowspan=\"3\">防火</br>公路</br>（公里）</th>");
            sb.AppendFormat("<th colspan=\"6\">森林消防车辆（台）</th>");
            sb.AppendFormat("<th rowspan=\"3\">专用</br>马匹</th>");
            sb.AppendFormat("<th colspan=\"2\">防火物资储备库</th>");
            sb.AppendFormat("<th colspan=\"2\"style=\"width:90px;\">扑火工具(台、把、套)</th>");
            sb.AppendFormat("<th rowspan=\"3\">备注</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"2\">计</th>");
            sb.AppendFormat("<th colspan=\"2\">其中</th>");
            sb.AppendFormat("<th rowspan=\"2\">计</th>");
            sb.AppendFormat("<th rowspan=\"2\">指挥车</th>");
            sb.AppendFormat("<th rowspan=\"2\">宣传车</th>");
            sb.AppendFormat("<th rowspan=\"2\">运输车</th>");
            sb.AppendFormat("<th rowspan=\"2\">摩托车</th>");
            sb.AppendFormat("<th rowspan=\"2\">其他车辆</th>");
            sb.AppendFormat("<th rowspan=\"2\">数量</br>(座)</th>");
            sb.AppendFormat("<th rowspan=\"2\">面积</br>(平方米)</th>");
            sb.AppendFormat("<th rowspan=\"2\">计</th>");
            sb.AppendFormat("<th>其中</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>生物林带</th>");
            sb.AppendFormat("<th>生土带</th>");
            sb.AppendFormat("<th>风力</br>灭火机</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");

            #region 第一行：列号
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td class=\"center\">甲</td>");
            sb.AppendFormat("<td class=\"center\">1</td>");
            sb.AppendFormat("<td class=\"center\">2</td>");
            sb.AppendFormat("<td class=\"center\">3</td>");
            sb.AppendFormat("<td class=\"center\">4</td>");
            sb.AppendFormat("<td class=\"center\">5</td>");
            sb.AppendFormat("<td class=\"center\">6</td>");
            sb.AppendFormat("<td class=\"center\">7</td>");
            sb.AppendFormat("<td class=\"center\">8</td>");
            sb.AppendFormat("<td class=\"center\">9</td>");
            sb.AppendFormat("<td class=\"center\">10</td>");
            sb.AppendFormat("<td class=\"center\">11</td>");
            sb.AppendFormat("<td class=\"center\">12</td>");
            sb.AppendFormat("<td class=\"center\">13</td>");
            sb.AppendFormat("<td class=\"center\">14</td>");
            sb.AppendFormat("<td class=\"center\">15</td>");
            sb.AppendFormat("<td class=\"center\">16</td>");
            sb.AppendFormat("</tr>");
            #endregion

            #region 累计实有
            int count1 = 0;
            int count2 = 0;
            for (int i = 0; i < dic313.Count; i++)
            {
                if (int.Parse(dic313[i].DICTVALUE) >= 100 && int.Parse(dic313[i].DICTVALUE) <= 299)
                {
                    count1++;
                }
                if (int.Parse(dic313[i].DICTVALUE) >= 300 && int.Parse(dic313[i].DICTVALUE) <= 399)
                {
                    count2++;
                }
            }
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"center\">累 计 实 有</td>");
            List<float> total = CalHJ2(_totalyearreportlist);
            for (int i = 0; i < count1; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total[i]);
            }
            float XFCLHJ = total[4] + total[5] + total[6] + total[7] + total[8];
            sb.AppendFormat("<td>{0}</td>", XFCLHJ);

            for (int i = count1; i < count1 + count2; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total[i]);
            }
            for (int i = count1 + count2; i < dic313.Count - 1; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total[i]);
            }
            //var bzlist = _yearreportlist.FindAll(a => a.REPORTCODE == dic313[dic313.Count - 1].DICTVALUE);
            //sb.AppendFormat("<td>{0}</td>", bzlist.Count > 0 ? bzlist[0].REPORTVALUE : "");
            sb.AppendFormat("<td>{0}</td>", "");
            sb.AppendFormat("</tr>");
            #endregion

            #region 本年合计
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td class=\"center\">本 年 合 计</td>");
            List<float> total1 = CalHJ2(_yearreportlist);
            for (int i = 0; i < count1; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total1[i]);
            }
            float XFCLHJ1 = total1[4] + total1[5] + total1[6] + total1[7] + total1[8];
            sb.AppendFormat("<td>{0}</td>", XFCLHJ1);

            for (int i = count1; i < count1 + count2; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total1[i]);
            }
            for (int i = count1 + count2; i < dic313.Count - 1; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total1[i]);
            }
            //var bzlist1 = _yearreportlist.FindAll(a => a.REPORTCODE == dic313[dic313.Count - 1].DICTVALUE);
            //sb.AppendFormat("<td>{0}</td>", bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
            sb.AppendFormat("<td>{0}</td>", "");
            sb.AppendFormat("</tr>");
            #endregion

            #region 详细数据行
            for (int i = 0; i < result.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"Center\"  >{0}</td>", result[i].ORGNAME);
                List<FIRERECORD_REPORT11_Model> templist = new List<FIRERECORD_REPORT11_Model>();
                if (PublicCls.OrgIsShi(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(4, 11) == result[i].ORGNO.Substring(4, 11)).ToList();
                if (PublicCls.OrgIsXian(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                if (PublicCls.OrgIsZhen(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                if (PublicCls.OrgIsCun(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO == result[i].ORGNO).ToList();
                List<float> total2 = CalHJ2(templist);
                for (int j = 0; j < count1; j++)
                {
                    sb.AppendFormat("<td>{0}</td>", total2[j]);
                }
                float XFCLHJ2 = total2[4] + total2[5] + total2[6] + total2[7] + total2[8];
                sb.AppendFormat("<td>{0}</td>", XFCLHJ2);

                for (int k = count1; k < count1 + count2; k++)
                {
                    sb.AppendFormat("<td>{0}</td>", total2[k]);
                }
                for (int l = count1 + count2; l < dic313.Count - 1; l++)
                {
                    sb.AppendFormat("<td>{0}</td>", total2[l]);
                }
                var bzlist2 = templist.FindAll(a => a.REPORTCODE == dic313[dic313.Count - 1].DICTVALUE);
                sb.AppendFormat("<td>{0}</td>", bzlist2.Count > 0 ? bzlist2[0].REPORTVALUE : "");

            }
            #endregion

            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult FIRERECORD_REPORT11ExportExcel()
        {
            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = ResultList(ORGNO);//获取组织机构编码
            // List<FIRERECORD_REPORT11_Model> _totalyearreportlist = FIRERECORD_REPORT11Cls.getListModel(new FIRERECORD_REPORT11_SW { REPORTYEAR = YEAR, }).ToList();
            List<FIRERECORD_REPORT11_Model> _totalyearreportlist = new List<FIRERECORD_REPORT11_Model>();
            if (ORGNO.Substring(4, 11) == "00000000000")
            {
                _totalyearreportlist = FIRERECORD_REPORT11Cls.getListModel(new FIRERECORD_REPORT11_SW { REPORTYEAR = YEAR, }).ToList();
            }
            else
            {
                _totalyearreportlist = FIRERECORD_REPORT11Cls.getListModel(new FIRERECORD_REPORT11_SW { BYORGNO = ORGNO, REPORTYEAR = YEAR, }).ToList();
            }
            var _yearreportlist = _totalyearreportlist.FindAll(a => a.REPORTYEAR == YEAR);
            List<T_SYS_DICTModel> dic313 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "313" }).ToList();//基础设施统计年报表二
            int colsCount = (dic313.Count + 1);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "041012", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = YEAR + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("森林防火办事机构人员统计报表");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            for (int i = 0; i < colsCount; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 20 * 256);
                else
                    sheet1.SetColumnWidth(i, 10 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.Height = 2 * 350;
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.Height = 2 * 350;
            rowHead1.CreateCell(0).SetCellValue("单位/项目");
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(1).SetCellValue("防火隔离带（公里）");
            rowHead1.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(4).SetCellValue("防火公路(公里)");
            rowHead1.GetCell(4).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(5).SetCellValue("森林消防车辆 （台）");
            rowHead1.GetCell(5).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(11).SetCellValue("专用马匹");
            rowHead1.GetCell(11).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(12).SetCellValue("防火物资储备库");
            rowHead1.GetCell(12).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(14).SetCellValue("扑火工具   （台、把、套）");
            rowHead1.GetCell(14).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(16).SetCellValue("备 注");
            rowHead1.GetCell(16).CellStyle = getCellStyleHead(book);

            IRow rowHead2 = sheet1.CreateRow(2);
            rowHead2.CreateCell(1).SetCellValue("计");
            rowHead2.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(2).SetCellValue("其中");
            rowHead2.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(5).SetCellValue("计");
            rowHead2.GetCell(5).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(6).SetCellValue("指挥车");
            rowHead2.GetCell(6).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(7).SetCellValue("宣传车");
            rowHead2.GetCell(7).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(8).SetCellValue("运输车");
            rowHead2.GetCell(8).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(9).SetCellValue("摩托车");
            rowHead2.GetCell(9).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(10).SetCellValue("其他车辆");
            rowHead2.GetCell(10).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(12).SetCellValue(" 数量（座）");
            rowHead2.GetCell(12).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(13).SetCellValue(" 面 积  (平方米)");
            rowHead2.GetCell(13).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(14).SetCellValue(" 计");
            rowHead2.GetCell(14).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(15).SetCellValue(" 其中");
            rowHead2.GetCell(15).CellStyle = getCellStyleHead(book);

            IRow rowHead3 = sheet1.CreateRow(3);
            rowHead3.CreateCell(2).SetCellValue("生物林带");
            rowHead3.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(3).SetCellValue("生土带");
            rowHead3.GetCell(3).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(15).SetCellValue("风力灭火机");
            rowHead3.GetCell(15).CellStyle = getCellStyleHead(book);
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 0, 0));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 1, 3));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 4, 4));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 5, 10));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 11, 11));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 12, 13));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 14, 15));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 16, 16));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 1, 1));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 2, 3));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 5, 5));

            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 6, 6));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 7, 7));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 8, 8));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 9, 9));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 10, 10));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 12, 12));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 13, 13));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 14, 14));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 15, 15));
            #endregion

            #region 表身及数据

            #region 累计实有
            IRow row4 = sheet1.CreateRow(4);
            int x = 0;
            row4.CreateCell(x).SetCellValue("累计实有");
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            x = FIRERECORD_REPORT11Excel(_totalyearreportlist, dic313, book, row4, x);
            //var bzlist = _yearreportlist.FindAll(a => a.REPORTCODE == dic313[dic313.Count - 1].DICTVALUE);
            //row4.CreateCell(x).SetCellValue(bzlist.Count > 0 ? bzlist[0].REPORTVALUE : "");
            //row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            row4.CreateCell(x).SetCellValue("");
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            #endregion

            #region 本年合计
            IRow row5 = sheet1.CreateRow(5);
            int j = 0;
            row5.CreateCell(j).SetCellValue("本年合计");
            row5.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            j = FIRERECORD_REPORT11Excel(_yearreportlist, dic313, book, row5, j);
            //var bzlist1 = _yearreportlist.FindAll(a => a.REPORTCODE == dic313[dic313.Count - 1].DICTVALUE);
            //row5.CreateCell(j).SetCellValue(bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
            //row5.GetCell(j).CellStyle = getCellStyleCenter(book);
            row5.CreateCell(j).SetCellValue("");
            row5.GetCell(j).CellStyle = getCellStyleCenter(book);
            #endregion

            #region 详细数据行
            int rowIndex = 0;
            for (int i = 0; i < result.Count; i++)
            {
                int k = 0;
                IRow row6 = sheet1.CreateRow(rowIndex + 6);
                row6.CreateCell(k).SetCellValue(result[i].ORGNAME);
                row6.GetCell(k).CellStyle = getCellStyleCenter(book);
                k++;
                List<FIRERECORD_REPORT11_Model> templist = new List<FIRERECORD_REPORT11_Model>();
                if (PublicCls.OrgIsShi(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(4, 11) == result[i].ORGNO.Substring(4, 11)).ToList();
                if (PublicCls.OrgIsXian(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                if (PublicCls.OrgIsZhen(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                if (PublicCls.OrgIsCun(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO == result[i].ORGNO).ToList();
                k = FIRERECORD_REPORT11Excel(templist, dic313, book, row6, k);
                var bzlist2 = templist.FindAll(a => a.REPORTCODE == dic313[dic313.Count - 1].DICTVALUE);
                row6.CreateCell(k).SetCellValue(bzlist2.Count > 0 ? bzlist2[0].REPORTVALUE : "");
                row6.GetCell(k).CellStyle = getCellStyleCenter(book);
                rowIndex++;
            }
            #endregion

            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }

        #endregion

        #region 火情档案_森林防火建设资金统计
        /// <summary>
        ///火情档案_森林防火建设资金统计--数据增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT12Manager()
        {
            FIRERECORD_REPORT12_Model m = new FIRERECORD_REPORT12_Model();
            m.BYORGNO = Request.Params["BYORGNO"];
            m.REPORTYEAR = Request.Params["REPORTYEAR"];
            m.REPORTCODE = Request.Params["REPORTCODE"];
            m.REPORTVALUE = Request.Params["REPORTVALUE"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(FIRERECORD_REPORT12Cls.Manager(m)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 火情档案_森林防火建设资金统计
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT12Man()
        {
            string Method = Request.Params["Method"];
            string FIRERECORD_REPORT12ID = Request.Params["FIRERECORD_REPORT12ID"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            FIRERECORD_REPORT12_Model model = new FIRERECORD_REPORT12_Model();
            if (!string.IsNullOrEmpty(FIRERECORD_REPORT12ID))
            {
                if (Method == "Add")
                {
                    model = FIRERECORD_REPORT12Cls.getModel(new FIRERECORD_REPORT12_SW { FIRERECORD_REPORT12ID = FIRERECORD_REPORT12ID });
                }
            }
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOptionByORGNO(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), OnlyGetShiXian = ORGNO, TopORGNO = ORGNO });//获取州级和市县
            // vdorg(ORGNO);
            ViewBag.REPORTYEAR = DateTime.Now.ToString("yyyy");
            ViewBag.Method = Method;
            List<T_SYS_DICTModel> dic314List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "314" }).ToList();
            ViewBag.dic314Value = T_SYS_DICTCls.getDicValueStr(dic314List);
            ViewBag.dic314Count = dic314List.Count;
            return View(model);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetReport12json()
        {
            string REPORTYEAR = Request.Params["REPORTYEAR"];
            string BYORGNO = Request.Params["BYORGNO"];
            List<FIRERECORD_REPORT12_Model> _accountlist = FIRERECORD_REPORT12Cls.getListModel(new FIRERECORD_REPORT12_SW { BYORGNO = BYORGNO, REPORTYEAR = REPORTYEAR, }).ToList();
            List<FIRERECORD_REPORT12_Model> _list = _accountlist.FindAll(a => a.REPORTYEAR == REPORTYEAR);
            return Content(JsonConvert.SerializeObject(_list));
            // return Content(JsonConvert.SerializeObject(FIRERECORD_REPORT12Cls.getListModel(new FIRERECORD_REPORT12_SW { BYORGNO = BYORGNO, REPORTYEAR = REPORTYEAR, })), "text/html;charset=UTF-8");
        }
        #endregion

        #region 火情档案_森林防火建设资金统计年报表
        /// <summary>
        /// 火情档案_森林防火建设资金统计年报表
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT12()
        {
            pubViewBag("041013", "041013", "森林防火建设资金统计年报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.YEAR = DateTime.Now.ToString("yyyy");
            return View();
        }

        /// <summary>
        ///火情档案_森林防火建设资金统计年报表
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_REPORT12Query()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string BYORGNO = Request.Params["BYORGNO"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = ResultList(ORGNO);//获取组织机构编码
            //List<FIRERECORD_REPORT12_Model> _totalyearreportlist = FIRERECORD_REPORT12Cls.getListModel(new FIRERECORD_REPORT12_SW { REPORTYEAR = YEAR, }).ToList();
            List<FIRERECORD_REPORT12_Model> _totalyearreportlist = new List<FIRERECORD_REPORT12_Model>();
            if (ORGNO.Substring(4, 11) == "00000000000")
            {
                _totalyearreportlist = FIRERECORD_REPORT12Cls.getListModel(new FIRERECORD_REPORT12_SW { REPORTYEAR = YEAR, }).ToList();
            }
            else
            {
                _totalyearreportlist = FIRERECORD_REPORT12Cls.getListModel(new FIRERECORD_REPORT12_SW { BYORGNO = ORGNO, REPORTYEAR = YEAR, }).ToList();
            }
            var _yearreportlist = _totalyearreportlist.FindAll(a => a.REPORTYEAR == YEAR);
            List<T_SYS_DICTModel> dic314 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "314" }).ToList();//森林防火建设资金统计年报表
            #endregion

            #region 数据表
            sb.AppendFormat("<table  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"3\"style=\" width:100px;\">单  位/项  目</th>");
            sb.AppendFormat("<th colspan=\"7\">森林防火建资金（万元）</th>");
            sb.AppendFormat("<th colspan=\"7\">其中国家专项补助（万元）</th>");
            sb.AppendFormat("<th colspan=\"5\">地方配套（万元）</th>");
            sb.AppendFormat("<th rowspan=\"3\">备 注</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>计</th>");
            sb.AppendFormat("<th>瞭望</br>系统</th>");
            sb.AppendFormat("<th>通信</br>系统</th>");
            sb.AppendFormat("<th>阻隔</br>系统</th>");
            sb.AppendFormat("<th>交通</br>工具</th>");
            sb.AppendFormat("<th>扑火</br>机具</th>");
            sb.AppendFormat("<th>其他</br>项目</th>");
            sb.AppendFormat("<th>计</th>");
            sb.AppendFormat("<th>瞭望</br>系统</th>");
            sb.AppendFormat("<th>通信</br>系统</th>");
            sb.AppendFormat("<th>阻隔</br>系统</th>");
            sb.AppendFormat("<th>交通</br>工具</th>");
            sb.AppendFormat("<th>扑火</br>机具</th>");
            sb.AppendFormat("<th>其他</br>项目</th>");
            sb.AppendFormat("<th>计</th>");
            sb.AppendFormat("<th>省 级</th>");
            sb.AppendFormat("<th>州 级</th>");
            sb.AppendFormat("<th>县 级</th>");
            sb.AppendFormat("<th>林 业</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");

            #region 第一行：列号
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td class=\"center\">甲</td>");
            sb.AppendFormat("<td class=\"center\">1</td>");
            sb.AppendFormat("<td class=\"center\">2</td>");
            sb.AppendFormat("<td class=\"center\">3</td>");
            sb.AppendFormat("<td class=\"center\">4</td>");
            sb.AppendFormat("<td class=\"center\">5</td>");
            sb.AppendFormat("<td class=\"center\">6</td>");
            sb.AppendFormat("<td class=\"center\">7</td>");
            sb.AppendFormat("<td class=\"center\">8</td>");
            sb.AppendFormat("<td class=\"center\">9</td>");
            sb.AppendFormat("<td class=\"center\">10</td>");
            sb.AppendFormat("<td class=\"center\">11</td>");
            sb.AppendFormat("<td class=\"center\">12</td>");
            sb.AppendFormat("<td class=\"center\">13</td>");
            sb.AppendFormat("<td class=\"center\">14</td>");
            sb.AppendFormat("<td class=\"center\">15</td>");
            sb.AppendFormat("<td class=\"center\">16</td>");
            sb.AppendFormat("<td class=\"center\">17</td>");
            sb.AppendFormat("<td class=\"center\">18</td>");
            sb.AppendFormat("<td class=\"center\">19</td>");
            sb.AppendFormat("<td class=\"center\">20</td>");
            sb.AppendFormat("</tr>");
            #endregion

            #region 到本年累计
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            for (int i = 0; i < dic314.Count; i++)
            {
                if (int.Parse(dic314[i].DICTVALUE) >= 100 && int.Parse(dic314[i].DICTVALUE) <= 199)
                {
                    count1++;
                }
                if (int.Parse(dic314[i].DICTVALUE) >= 200 && int.Parse(dic314[i].DICTVALUE) <= 299)
                {
                    count2++;
                }
                if (int.Parse(dic314[i].DICTVALUE) >= 300 && int.Parse(dic314[i].DICTVALUE) <= 399)
                {
                    count3++;
                }

            }
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"center\">到本年累计</td>");
            List<float> total = CalHJ3(_totalyearreportlist);
            float JSZJ = total[0] + total[1] + total[2] + total[3] + total[4] + total[5];
            sb.AppendFormat("<td>{0}</td>", JSZJ);
            for (int i = 0; i < count1; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total[i]);
            }
            float GJZXBZ = total[6] + total[7] + total[8] + total[9] + total[10] + total[11];
            sb.AppendFormat("<td>{0}</td>", GJZXBZ);

            for (int i = count1; i < count1 + count2; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total[i]);
            }
            float DFPT = total[12] + total[13] + total[14] + total[15];
            sb.AppendFormat("<td>{0}</td>", DFPT);
            for (int i = count1 + count2; i < dic314.Count - 1; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total[i]);
            }
            //var bzlist = _yearreportlist.FindAll(a => a.REPORTCODE == dic314[dic314.Count - 1].DICTVALUE);
            //sb.AppendFormat("<td>{0}</td>", bzlist.Count > 0 ? bzlist[0].REPORTVALUE : "");
            sb.AppendFormat("<td>{0}</td>", "");
            sb.AppendFormat("</tr>");
            #endregion

            #region 本年合计
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td class=\"center\">本 年 合 计</td>");
            List<float> total1 = CalHJ3(_yearreportlist);
            float JSZJ1 = total1[0] + total1[1] + total1[2] + total1[3] + total1[4] + total1[5];
            sb.AppendFormat("<td>{0}</td>", JSZJ1);
            for (int i = 0; i < count1; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total1[i]);
            }
            float GJZXBZ1 = total1[6] + total1[7] + total1[8] + total1[9] + total1[10] + total1[11];
            sb.AppendFormat("<td>{0}</td>", GJZXBZ1);

            for (int i = count1; i < count1 + count2; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total1[i]);
            }
            float DFPT1 = total1[12] + total1[13] + total1[14] + total1[15];
            sb.AppendFormat("<td>{0}</td>", DFPT1);
            for (int i = count1 + count2; i < dic314.Count - 1; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total1[i]);
            }
            //var bzlist1 = _yearreportlist.FindAll(a => a.REPORTCODE == dic314[dic314.Count - 1].DICTVALUE);
            //sb.AppendFormat("<td>{0}</td>", bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
            sb.AppendFormat("<td>{0}</td>", "");
            sb.AppendFormat("</tr>");
            #endregion

            #region 详细数据行
            for (int i = 0; i < result.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"Center\" >{0}</td>", result[i].ORGNAME);
                List<FIRERECORD_REPORT12_Model> templist = new List<FIRERECORD_REPORT12_Model>();
                if (PublicCls.OrgIsShi(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(4, 11) == result[i].ORGNO.Substring(4, 11)).ToList();
                if (PublicCls.OrgIsXian(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                if (PublicCls.OrgIsZhen(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                if (PublicCls.OrgIsCun(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO == result[i].ORGNO).ToList();
                List<float> total2 = CalHJ3(templist);
                float JSZJ2 = total2[0] + total2[1] + total2[2] + total2[3] + total2[4] + total2[5];
                sb.AppendFormat("<td>{0}</td>", JSZJ2);
                for (int j = 0; j < count1; j++)
                {
                    sb.AppendFormat("<td>{0}</td>", total2[j]);
                }
                float GJZXBZ2 = total2[6] + total2[7] + total2[8] + total2[9] + total2[10] + total2[11];
                sb.AppendFormat("<td>{0}</td>", GJZXBZ2);

                for (int k = count1; k < count1 + count2; k++)
                {
                    sb.AppendFormat("<td>{0}</td>", total2[k]);
                }
                float DFPT2 = total2[12] + total2[13] + total2[14] + total2[15];
                sb.AppendFormat("<td>{0}</td>", DFPT2);
                for (int l = count1 + count2; l < dic314.Count - 1; l++)
                {
                    sb.AppendFormat("<td>{0}</td>", total2[l]);
                }
                var bzlist2 = templist.FindAll(a => a.REPORTCODE == dic314[dic314.Count - 1].DICTVALUE);
                sb.AppendFormat("<td>{0}</td>", bzlist2.Count > 0 ? bzlist2[0].REPORTVALUE : "");
                sb.AppendFormat("</tr>");
            }
            #endregion

            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult FIRERECORD_REPORT12ExportExcel()
        {
            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = ResultList(ORGNO);//获取组织机构编码
            // List<FIRERECORD_REPORT12_Model> _totalyearreportlist = FIRERECORD_REPORT12Cls.getListModel(new FIRERECORD_REPORT12_SW { REPORTYEAR = YEAR, }).ToList();
            List<FIRERECORD_REPORT12_Model> _totalyearreportlist = new List<FIRERECORD_REPORT12_Model>();
            if (ORGNO.Substring(4, 11) == "00000000000")
            {
                _totalyearreportlist = FIRERECORD_REPORT12Cls.getListModel(new FIRERECORD_REPORT12_SW { REPORTYEAR = YEAR, }).ToList();
            }
            else
            {
                _totalyearreportlist = FIRERECORD_REPORT12Cls.getListModel(new FIRERECORD_REPORT12_SW { BYORGNO = ORGNO, REPORTYEAR = YEAR, }).ToList();
            }
            var _yearreportlist = _totalyearreportlist.FindAll(a => a.REPORTYEAR == YEAR);
            List<T_SYS_DICTModel> dic314 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "314" }).ToList();//森林防火建设资金统计年报表
            int colsCount = (dic314.Count + 3);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "041013", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = YEAR + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("森林防火建设资金统计年报表");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            for (int i = 0; i < colsCount; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 20 * 256);
                else
                    sheet1.SetColumnWidth(i, 10 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.Height = 2 * 350;
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.Height = 2 * 350;
            rowHead1.CreateCell(0).SetCellValue("单位/项目");
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(1).SetCellValue("合计（万元）");
            rowHead1.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(8).SetCellValue("其中国家专项补助（万元）");
            rowHead1.GetCell(8).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(15).SetCellValue("地方配套（万元）");
            rowHead1.GetCell(15).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(20).SetCellValue("备 注");
            rowHead1.GetCell(20).CellStyle = getCellStyleHead(book);

            IRow rowHead2 = sheet1.CreateRow(2);
            rowHead2.CreateCell(1).SetCellValue("计");
            rowHead2.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(2).SetCellValue("瞭望系统");
            rowHead2.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(3).SetCellValue("通信系统");
            rowHead2.GetCell(3).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(4).SetCellValue("阻隔系统");
            rowHead2.GetCell(4).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(5).SetCellValue("交通工具");
            rowHead2.GetCell(5).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(6).SetCellValue("扑火机具");
            rowHead2.GetCell(6).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(7).SetCellValue("其他项目");
            rowHead2.GetCell(7).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(8).SetCellValue("计");
            rowHead2.GetCell(8).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(9).SetCellValue(" 瞭望系统");
            rowHead2.GetCell(9).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(10).SetCellValue("通信系统");
            rowHead2.GetCell(10).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(11).SetCellValue(" 阻隔系统");
            rowHead2.GetCell(11).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(12).SetCellValue(" 交通工具");
            rowHead2.GetCell(12).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(13).SetCellValue("扑火机具");
            rowHead2.GetCell(13).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(14).SetCellValue("其他项目");
            rowHead2.GetCell(14).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(15).SetCellValue("计");
            rowHead2.GetCell(15).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(16).SetCellValue("省级");
            rowHead2.GetCell(16).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(17).SetCellValue("州级");
            rowHead2.GetCell(17).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(18).SetCellValue("县级");
            rowHead2.GetCell(18).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(19).SetCellValue("林业");
            rowHead2.GetCell(19).CellStyle = getCellStyleHead(book);

            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 1, 7));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 8, 14));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 15, 19));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 20, 20));
            #endregion

            #region 表身及数据

            #region 到本年累计
            IRow row3 = sheet1.CreateRow(3);
            int x = 0;
            row3.CreateCell(x).SetCellValue("到本年累计");
            row3.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            x = FIRERECORD_REPORT12Excel(_totalyearreportlist, dic314, book, row3, x);
            //var bzlist = _yearreportlist.FindAll(a => a.REPORTCODE == dic314[dic314.Count - 1].DICTVALUE);
            //row3.CreateCell(x).SetCellValue(bzlist.Count > 0 ? bzlist[0].REPORTVALUE : "");
            //row3.GetCell(x).CellStyle = getCellStyleCenter(book);
            row3.CreateCell(x).SetCellValue("");
            row3.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            #endregion

            #region 本年合计
            IRow row4 = sheet1.CreateRow(4);
            int j = 0;
            row4.CreateCell(j).SetCellValue("本年合计");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            j = FIRERECORD_REPORT12Excel(_yearreportlist, dic314, book, row4, j);
            //var bzlist1 = _yearreportlist.FindAll(a => a.REPORTCODE == dic314[dic314.Count - 1].DICTVALUE);
            //row4.CreateCell(j).SetCellValue(bzlist1.Count > 0 ? bzlist1[0].REPORTVALUE : "");
            //row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            row4.CreateCell(j).SetCellValue("");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            #endregion

            #region 详细数据行
            int rowIndex = 0;
            for (int i = 0; i < result.Count; i++)
            {
                int k = 0;
                IRow row5 = sheet1.CreateRow(rowIndex + 5);
                row5.CreateCell(k).SetCellValue(result[i].ORGNAME);
                row5.GetCell(k).CellStyle = getCellStyleCenter(book);
                k++;
                List<FIRERECORD_REPORT12_Model> templist = new List<FIRERECORD_REPORT12_Model>();
                if (PublicCls.OrgIsShi(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(4, 11) == result[i].ORGNO.Substring(4, 11)).ToList();
                if (PublicCls.OrgIsXian(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                if (PublicCls.OrgIsZhen(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                if (PublicCls.OrgIsCun(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO == result[i].ORGNO).ToList();
                k = FIRERECORD_REPORT12Excel(templist, dic314, book, row5, k);
                var bzlist2 = templist.FindAll(a => a.REPORTCODE == dic314[dic314.Count - 1].DICTVALUE);
                row5.CreateCell(k).SetCellValue(bzlist2.Count > 0 ? bzlist2[0].REPORTVALUE : "");
                row5.GetCell(k).CellStyle = getCellStyleCenter(book);
                rowIndex++;
            }
            #endregion

            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }

        #endregion

        #region 火情档案_队伍表统计
        /// <summary>
        ///火情档案_队伍表--数据增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_ARMYManager()
        {
            FIRERECORD_ARMY_Model m = new FIRERECORD_ARMY_Model();
            m.BYORGNO = Request.Params["BYORGNO"];
            m.REPORTYEAR = Request.Params["REPORTYEAR"];
            m.REPORTCODE = Request.Params["REPORTCODE"];
            m.REPORTVALUE = Request.Params["REPORTVALUE"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(FIRERECORD_ARMYCls.Manager(m)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 火情档案_队伍表
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_ARMYMan()
        {
            string Method = Request.Params["Method"];
            string FIRERECORD_ARMYID = Request.Params["FIRERECORD_ARMYID"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            FIRERECORD_ARMY_Model model = new FIRERECORD_ARMY_Model();
            if (!string.IsNullOrEmpty(FIRERECORD_ARMYID))
            {
                if (Method == "Add")
                {
                    model = FIRERECORD_ARMYCls.getModel(new FIRERECORD_ARMY_SW { FIRERECORD_ARMYID = FIRERECORD_ARMYID });
                }
            }
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOptionByORGNO(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), OnlyGetShiXian = ORGNO, TopORGNO = ORGNO });//获取州级和市县
            //vdorg(ORGNO);
            ViewBag.REPORTYEAR = DateTime.Now.ToString("yyyy");
            ViewBag.Method = Method;
            List<T_SYS_DICTModel> dic315List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "315" }).ToList();
            ViewBag.dic315Value = T_SYS_DICTCls.getDicValueStr(dic315List);
            ViewBag.dic315Count = dic315List.Count;
            return View(model);
        }


        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetArmyjson()
        {
            string REPORTYEAR = Request.Params["REPORTYEAR"];
            string BYORGNO = Request.Params["BYORGNO"];
            List<FIRERECORD_ARMY_Model> _accountlist = FIRERECORD_ARMYCls.getListModel(new FIRERECORD_ARMY_SW { BYORGNO = BYORGNO, REPORTYEAR = REPORTYEAR, }).ToList();
            List<FIRERECORD_ARMY_Model> _list = _accountlist.FindAll(a => a.REPORTYEAR == REPORTYEAR);
            return Content(JsonConvert.SerializeObject(_list));
            // return Content(JsonConvert.SerializeObject(FIRERECORD_ARMYCls.getListModel(new FIRERECORD_ARMY_SW { BYORGNO = BYORGNO, REPORTYEAR = REPORTYEAR, })), "text/html;charset=UTF-8");
        }
        #endregion

        #region 火情档案_队伍表
        /// <summary>
        /// 火情档案_队伍表
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_ARMY()
        {
            pubViewBag("041014", "041014", "队伍表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.YEAR = DateTime.Now.ToString("yyyy");
            return View();
        }

        /// <summary>
        ///火情档案_队伍表
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRERECORD_ARMYQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string BYORGNO = Request.Params["BYORGNO"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = ResultList(ORGNO);//获取组织机构编码
            //List<FIRERECORD_ARMY_Model> _totalyearreportlist = FIRERECORD_ARMYCls.getListModel(new FIRERECORD_ARMY_SW { REPORTYEAR = YEAR, }).ToList();
            List<FIRERECORD_ARMY_Model> _totalyearreportlist = new List<FIRERECORD_ARMY_Model>();
            if (ORGNO.Substring(4, 11) == "00000000000")
            {
                _totalyearreportlist = FIRERECORD_ARMYCls.getListModel(new FIRERECORD_ARMY_SW { REPORTYEAR = YEAR, }).ToList();
            }
            else
            {
                _totalyearreportlist = FIRERECORD_ARMYCls.getListModel(new FIRERECORD_ARMY_SW { BYORGNO = ORGNO, REPORTYEAR = YEAR, }).ToList();
            }
            var _yearreportlist = _totalyearreportlist.FindAll(a => a.REPORTYEAR == YEAR);
            List<T_SYS_DICTModel> dic315 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "315" }).ToList();//队伍表
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            for (int i = 0; i < dic315.Count; i++)
            {
                if (int.Parse(dic315[i].DICTVALUE) >= 100 && int.Parse(dic315[i].DICTVALUE) <= 399)
                {
                    count1++;
                }
                if (int.Parse(dic315[i].DICTVALUE) >= 400 && int.Parse(dic315[i].DICTVALUE) <= 499)
                {
                    count2++;
                }
                if (int.Parse(dic315[i].DICTVALUE) >= 500 && int.Parse(dic315[i].DICTVALUE) <= 599)
                {
                    count3++;
                }

            }
            #endregion

            #region 数据表
            sb.AppendFormat("<table  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"2\"style=\" width:100px;\">单  位/项  目</th>");
            sb.AppendFormat("<th colspan=\"2\"style=\" width:80px;\">本年新建队伍（支）</th>");
            sb.AppendFormat("<th colspan=\"2\"style=\" width:80px;\">本年新建基地（个）</th>");
            sb.AppendFormat("<th colspan=\"2\"style=\" width:80px;\">目前全省共有队伍数(支)</th>");
            sb.AppendFormat("<th colspan=\"6\">全省各类基地数量（个）</th>");
            sb.AppendFormat("<th colspan=\"6\">全省各类基地产值（元）</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>已建成</br>（支）</th>");
            sb.AppendFormat("<th>正在建</br>（支）</th>");
            sb.AppendFormat("<th>已建成</br>(个)</th>");
            sb.AppendFormat("<th>正在建</br>(个)</th>");
            sb.AppendFormat("<th>合计</br>（支）</th>");
            sb.AppendFormat("<th>其中</br>已有效益</br>（支）</th>");
            sb.AppendFormat("<th>合计</br>（支）</th>");
            sb.AppendFormat("<th>种植</br>基地</th>");
            sb.AppendFormat("<th>养殖</br>基地</th>");
            sb.AppendFormat("<th>加工</br>基地</th>");
            sb.AppendFormat("<th>第三</br>产业</th>");
            sb.AppendFormat("<th>其他</th>");
            sb.AppendFormat("<th>合计</th>");
            sb.AppendFormat("<th>种植</br>基地</th>");
            sb.AppendFormat("<th>养殖</br>基地</th>");
            sb.AppendFormat("<th>加工</br>基地</th>");
            sb.AppendFormat("<th>第三</br>产业</th>");
            sb.AppendFormat("<th>其他</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");

            #region 第一行：列号
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td class=\"center\" >甲</td>");
            sb.AppendFormat("<td class=\"center\">1</td>");
            sb.AppendFormat("<td class=\"center\">2</td>");
            sb.AppendFormat("<td class=\"center\">3</td>");
            sb.AppendFormat("<td class=\"center\">4</td>");
            sb.AppendFormat("<td class=\"center\">5</td>");
            sb.AppendFormat("<td class=\"center\">6</td>");
            sb.AppendFormat("<td class=\"center\">7</td>");
            sb.AppendFormat("<td class=\"center\">8</td>");
            sb.AppendFormat("<td class=\"center\">9</td>");
            sb.AppendFormat("<td class=\"center\">10</td>");
            sb.AppendFormat("<td class=\"center\">11</td>");
            sb.AppendFormat("<td class=\"center\">12</td>");
            sb.AppendFormat("<td class=\"center\">13</td>");
            sb.AppendFormat("<td class=\"center\">14</td>");
            sb.AppendFormat("<td class=\"center\">15</td>");
            sb.AppendFormat("<td class=\"center\">16</td>");
            sb.AppendFormat("<td class=\"center\">17</td>");
            sb.AppendFormat("<td class=\"center\">18</td>");
            sb.AppendFormat("</tr>");
            #endregion

            #region 到本年累计
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"center\">到本年累计</td>");
            List<float> total = CalARMYHJ(_totalyearreportlist);
            for (int i = 0; i < count1; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total[i]);
            }
            float JDSL = total[6] + total[7] + total[8] + total[9] + total[10];
            sb.AppendFormat("<td>{0}</td>", JDSL);

            for (int i = count1; i < count1 + count2; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total[i]);
            }
            float GDCZ = total[11] + total[12] + total[13] + total[14] + total[15];
            sb.AppendFormat("<td>{0}</td>", GDCZ);
            for (int i = count1 + count2; i < dic315.Count; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total[i]);
            }
            sb.AppendFormat("</tr>");
            #endregion

            #region 本年合计
            sb.AppendFormat("<tr class=\"{0}\">", "row1");
            sb.AppendFormat("<td class=\"center\">本 年 合 计</td>");
            List<float> total1 = CalARMYHJ(_yearreportlist);
            for (int i = 0; i < count1; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total1[i]);
            }
            float JDSL1 = total1[6] + total1[7] + total1[8] + total1[9] + total1[10];
            sb.AppendFormat("<td>{0}</td>", JDSL1);

            for (int i = count1; i < count1 + count2; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total1[i]);
            }
            float GDCZ1 = total1[11] + total1[12] + total1[13] + total1[14] + total1[15];
            sb.AppendFormat("<td>{0}</td>", GDCZ1);
            for (int i = count1 + count2; i < dic315.Count; i++)
            {
                sb.AppendFormat("<td>{0}</td>", total1[i]);
            }
            sb.AppendFormat("</tr>");
            #endregion

            #region 详细数据行
            for (int i = 0; i < result.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"Center\"  >{0}</td>", result[i].ORGNAME);
                List<FIRERECORD_ARMY_Model> templist = new List<FIRERECORD_ARMY_Model>();
                if (PublicCls.OrgIsShi(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(4, 11) == result[i].ORGNO.Substring(4, 11)).ToList();
                if (PublicCls.OrgIsXian(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                if (PublicCls.OrgIsZhen(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                if (PublicCls.OrgIsCun(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO == result[i].ORGNO).ToList();
                List<float> total2 = CalARMYHJ(templist);
                for (int j = 0; j < count1; j++)
                {
                    sb.AppendFormat("<td>{0}</td>", total2[j]);
                }
                float JDSL2 = total2[6] + total2[7] + total2[8] + total2[9] + total2[10];
                sb.AppendFormat("<td>{0}</td>", JDSL2);

                for (int k = count1; k < count1 + count2; k++)
                {
                    sb.AppendFormat("<td>{0}</td>", total2[k]);
                }
                float GDCZ2 = total2[11] + total2[12] + total2[13] + total2[14] + total2[15];
                sb.AppendFormat("<td>{0}</td>", GDCZ2);
                for (int l = count1 + count2; l < dic315.Count; l++)
                {
                    sb.AppendFormat("<td>{0}</td>", total2[l]);
                }
                sb.AppendFormat("</tr>");
            }
            #endregion

            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult FIRERECORD_ARMYExportExcel()
        {
            #region 数据查询条件
            string YEAR = Request.Params["YEAR"];
            string ORGNO = SystemCls.getCurUserOrgNo();
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = ResultList(ORGNO);//获取组织机构编码
            // List<FIRERECORD_ARMY_Model> _totalyearreportlist = FIRERECORD_ARMYCls.getListModel(new FIRERECORD_ARMY_SW { REPORTYEAR = YEAR, }).ToList();
            List<FIRERECORD_ARMY_Model> _totalyearreportlist = new List<FIRERECORD_ARMY_Model>();
            if (ORGNO.Substring(4, 11) == "00000000000")
            {
                _totalyearreportlist = FIRERECORD_ARMYCls.getListModel(new FIRERECORD_ARMY_SW { REPORTYEAR = YEAR, }).ToList();
            }
            else
            {
                _totalyearreportlist = FIRERECORD_ARMYCls.getListModel(new FIRERECORD_ARMY_SW { BYORGNO = ORGNO, REPORTYEAR = YEAR, }).ToList();
            }
            var _yearreportlist = _totalyearreportlist.FindAll(a => a.REPORTYEAR == YEAR);
            List<T_SYS_DICTModel> dic315 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "315" }).ToList();//队伍表
            int colsCount = (dic315.Count + 2);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "041014", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = YEAR + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("队伍表");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            for (int i = 0; i < colsCount; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 20 * 256);
                else
                    sheet1.SetColumnWidth(i, 10 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.Height = 2 * 350;
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.Height = 2 * 350;
            rowHead1.CreateCell(0).SetCellValue("单位/项目");
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(1).SetCellValue("本年新建队伍（支）");
            rowHead1.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(3).SetCellValue("本年新建基地（个）");
            rowHead1.GetCell(3).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(5).SetCellValue("目前全省共有队数（支）");
            rowHead1.GetCell(5).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(7).SetCellValue("全省各类基地数量（个）");
            rowHead1.GetCell(7).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(13).SetCellValue("全省各类基地产值（元）");
            rowHead1.GetCell(13).CellStyle = getCellStyleHead(book);

            IRow rowHead2 = sheet1.CreateRow(2);
            rowHead2.CreateCell(1).SetCellValue("已建成");
            rowHead2.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(2).SetCellValue("正在建");
            rowHead2.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(3).SetCellValue("已建成");
            rowHead2.GetCell(3).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(4).SetCellValue("正在建");
            rowHead2.GetCell(4).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(5).SetCellValue("合计");
            rowHead2.GetCell(5).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(6).SetCellValue("其中已有效益");
            rowHead2.GetCell(6).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(7).SetCellValue("合计");
            rowHead2.GetCell(7).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(8).SetCellValue("种植基地");
            rowHead2.GetCell(8).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(9).SetCellValue("养殖基地");
            rowHead2.GetCell(9).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(10).SetCellValue("加工基地");
            rowHead2.GetCell(10).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(11).SetCellValue(" 第三产业");
            rowHead2.GetCell(11).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(12).SetCellValue("其他");
            rowHead2.GetCell(12).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(13).SetCellValue("合计");
            rowHead2.GetCell(13).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(14).SetCellValue("种植基地");
            rowHead2.GetCell(14).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(15).SetCellValue("养殖基地");
            rowHead2.GetCell(15).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(16).SetCellValue("加工基地");
            rowHead2.GetCell(16).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(17).SetCellValue("第三产业");
            rowHead2.GetCell(17).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(18).SetCellValue("其他");
            rowHead2.GetCell(18).CellStyle = getCellStyleHead(book);

            IRow rowHead3 = sheet1.CreateRow(3);
            rowHead3.CreateCell(1).SetCellValue("（支）");
            rowHead3.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(2).SetCellValue("（支）");
            rowHead3.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(3).SetCellValue("（个）");
            rowHead3.GetCell(3).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(4).SetCellValue("（个）");
            rowHead3.GetCell(4).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(5).SetCellValue("（支）");
            rowHead3.GetCell(5).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(6).SetCellValue("（支）");
            rowHead3.GetCell(6).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(7).SetCellValue("（支）");
            rowHead3.GetCell(7).CellStyle = getCellStyleHead(book);
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 0, 0));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 1, 2));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 3, 4));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 5, 6));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 7, 12));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 13, 18));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 8, 8));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 9, 9));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 10, 10));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 11, 11));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 12, 12));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 13, 13));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 14, 14));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 15, 15));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 16, 16));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 17, 17));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 18, 18));
            #endregion

            #region 表身及数据

            #region 到本年累计
            IRow row4 = sheet1.CreateRow(4);
            int x = 0;
            row4.CreateCell(x).SetCellValue("到本年累计");
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            x = ARMYExcel(_totalyearreportlist, dic315, book, row4, x);
            #endregion

            #region 本年合计
            IRow row5 = sheet1.CreateRow(5);
            int j = 0;
            row5.CreateCell(j).SetCellValue("本年合计");
            row5.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            j = ARMYExcel(_yearreportlist, dic315, book, row5, j);
            #endregion

            #region 详细数据行
            int rowIndex = 0;
            for (int i = 0; i < result.Count; i++)
            {
                int k = 0;
                IRow row6 = sheet1.CreateRow(rowIndex + 6);
                row6.CreateCell(k).SetCellValue(result[i].ORGNAME);
                row6.GetCell(k).CellStyle = getCellStyleCenter(book);
                k++;
                List<FIRERECORD_ARMY_Model> templist = new List<FIRERECORD_ARMY_Model>();
                if (PublicCls.OrgIsShi(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(4, 11) == result[i].ORGNO.Substring(4, 11)).ToList();
                if (PublicCls.OrgIsXian(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
                if (PublicCls.OrgIsZhen(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
                if (PublicCls.OrgIsCun(result[i].ORGNO))
                    templist = _yearreportlist.FindAll(a => a.BYORGNO == result[i].ORGNO).ToList();
                k = ARMYExcel(templist, dic315, book, row6, k);
                rowIndex++;
            }
            #endregion

            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }
        #endregion

        #region Private

        #region 计算合计
        /// <summary>
        /// 计算合计
        /// </summary>
        /// <param name="_list">数据列表</param>
        /// <returns></returns>
        private static List<float> CalZJ(List<FIRERECORD_FIREINFO_Model> _list)
        {
            List<float> _templist = new List<float>();
            float Area = 0;
            float YSLArea = 0;
            float RGLArea = 0;
            float CSLArea = 0;
            float CLXJ = 0;
            float YLZS = 0;
            float RYQS = 0;
            float RYZS = 0;
            float RYSW = 0;
            float QTZK = 0;
            float CDPG = 0;
            float CDCL = 0;
            float CDFJ = 0;
            float PHJF = 0;
            float HACHSFCL = 0;
            float HACHSFCM = 0;
            float LZCF = 0;
            float XSCF = 0;
            float YLDMJArea = 0;
            float QCHJ = 0;
            foreach (var v in _list)
            {
                if (!string.IsNullOrEmpty(v.FIRERECINFO020))
                    Area += float.Parse(v.FIRERECINFO020);
                if (!string.IsNullOrEmpty(v.FIRERECINFO021))
                    YLDMJArea += float.Parse(v.FIRERECINFO021);
                if (!string.IsNullOrEmpty(v.FIRERECINFO030))
                    YSLArea += float.Parse(v.FIRERECINFO030);
                if (!string.IsNullOrEmpty(v.FIRERECINFO031))
                    CSLArea += float.Parse(v.FIRERECINFO031);
                if (!string.IsNullOrEmpty(v.FIRERECINFO032))
                    RGLArea += float.Parse(v.FIRERECINFO032);
                if (!string.IsNullOrEmpty(v.FIRERECINFO040))
                    CLXJ += float.Parse(v.FIRERECINFO040);
                if (!string.IsNullOrEmpty(v.FIRERECINFO041))
                    YLZS += float.Parse(v.FIRERECINFO041);
                if (!string.IsNullOrEmpty(v.FIRERECINFO070))
                    RYQS += float.Parse(v.FIRERECINFO070);
                if (!string.IsNullOrEmpty(v.FIRERECINFO071))
                    RYZS += float.Parse(v.FIRERECINFO071);
                if (!string.IsNullOrEmpty(v.FIRERECINFO072))
                    RYSW += float.Parse(v.FIRERECINFO072);
                if (!string.IsNullOrEmpty(v.FIRERECINFO080))
                    HACHSFCL += float.Parse(v.FIRERECINFO080);
                if (!string.IsNullOrEmpty(v.FIRERECINFO081))
                    LZCF += float.Parse(v.FIRERECINFO081);
                if (!string.IsNullOrEmpty(v.FIRERECINFO082))
                    XSCF += float.Parse(v.FIRERECINFO082);
                if (!string.IsNullOrEmpty(v.FIRERECINFO090))
                    QTZK += float.Parse(v.FIRERECINFO090);
                if (!string.IsNullOrEmpty(v.FIRERECINFO100))
                    CDPG += float.Parse(v.FIRERECINFO100);
                if (!string.IsNullOrEmpty(v.FIRERECINFO110))
                    CDCL += float.Parse(v.FIRERECINFO110);
                if (!string.IsNullOrEmpty(v.FIRERECINFO111))
                    QCHJ += float.Parse(v.FIRERECINFO111);
                if (!string.IsNullOrEmpty(v.FIRERECINFO120))
                    CDFJ += float.Parse(v.FIRERECINFO120);
                if (!string.IsNullOrEmpty(v.FIRERECINFO130))
                    PHJF += float.Parse(v.FIRERECINFO130);
                if (!string.IsNullOrEmpty(v.FIRERECINFO140))
                    HACHSFCM += float.Parse(v.FIRERECINFO140);
            }
            _templist.Add(Area);
            _templist.Add(YSLArea);
            _templist.Add(RGLArea);
            _templist.Add(CLXJ);
            _templist.Add(YLZS);
            _templist.Add(RYQS);
            _templist.Add(RYZS);
            _templist.Add(RYSW);
            _templist.Add(QTZK);
            _templist.Add(CDPG);
            _templist.Add(CDCL);
            _templist.Add(CDFJ);
            _templist.Add(PHJF);
            _templist.Add(HACHSFCL);
            _templist.Add(HACHSFCM);
            _templist.Add(LZCF);
            _templist.Add(XSCF);
            _templist.Add(YLDMJArea);
            _templist.Add(CSLArea);
            _templist.Add(QCHJ);
            return _templist;
        }
        #endregion

        #region 12个月份
        private static List<string> monthlist()
        {
            List<String> monthlist = new List<String>();
            monthlist.Add("一月份");
            monthlist.Add("二月份");
            monthlist.Add("三月份");
            monthlist.Add("四月份");
            monthlist.Add("五月份");
            monthlist.Add("六月份");
            monthlist.Add("七月份");
            monthlist.Add("八月份");
            monthlist.Add("九月份");
            monthlist.Add("十月份");
            monthlist.Add("十一月份");
            monthlist.Add("十二月份");
            return monthlist;
        }
        #endregion

        #region  第一行：列号
        private static string COLUMN()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<td class=\"center\">甲</td>");
            sb.AppendFormat("<td class=\"center\">1</td>");
            sb.AppendFormat("<td class=\"center\">2</td>");
            sb.AppendFormat("<td class=\"center\">3</td>");
            sb.AppendFormat("<td class=\"center\">4</td>");
            sb.AppendFormat("<td class=\"center\">5</td>");
            sb.AppendFormat("<td class=\"center\">6</td>");
            sb.AppendFormat("<td class=\"center\">7</td>");
            sb.AppendFormat("<td class=\"center\">8</td>");
            sb.AppendFormat("<td class=\"center\">9</td>");
            sb.AppendFormat("<td class=\"center\">10</td>");
            sb.AppendFormat("<td class=\"center\">11</td>");
            sb.AppendFormat("<td class=\"center\">12</td>");
            sb.AppendFormat("<td class=\"center\">13</td>");
            sb.AppendFormat("<td class=\"center\">14</td>");
            sb.AppendFormat("<td class=\"center\">15</td>");
            sb.AppendFormat("<td class=\"center\">16</td>");
            sb.AppendFormat("<td class=\"center\">17</td>");
            sb.AppendFormat("<td class=\"center\">18</td>");
            sb.AppendFormat("<td class=\"center\">19</td>");
            sb.AppendFormat("<td class=\"center\">20</td>");
            sb.AppendFormat("<td class=\"center\">21</td>");
            return sb.ToString();
        }
        #endregion

        #region 报表的查询条件和查询数据
        /// <summary>
        /// 林火1表月报表查询条件
        /// </summary>
        /// <param name="Time">要查询的时间</param>
        /// <param name="nowFireTime">查询的月初</param>
        /// <param name="nowfireEndTime">查询的月末</param>
        /// <param name="fireTime">年初</param>
        /// <param name="fireEndTime">当前日期</param>
        private static void LYReportQueryTerm(string Time, out string nowFireTime, out string nowfireEndTime, out string fireTime, out string fireEndTime)
        {
            string[] sTime = Time.Split('-');
            DateTime startTime = new DateTime(int.Parse(sTime[0]), int.Parse(sTime[1]), 1); //查询时的月初
            nowFireTime = startTime.ToString();
            DateTime nowstartTime = new DateTime(int.Parse(sTime[0]), int.Parse(sTime[1]) + 1, 1).AddSeconds(-1);  //查询时的月末
            nowfireEndTime = nowstartTime.ToString();
            fireTime = DateTime.Now.ToString("yyyy-01-01");//年初
            fireEndTime = DateTime.Now.ToString("yyyy-MM-dd");//当前时间
        }

        /// <summary>
        /// 林火1表月报表查询数据
        /// </summary>
        /// <param name="ORGNO">当前组织机构</param>
        /// <param name="nowFireTime">查询的月初</param>
        /// <param name="nowfireEndTime">查询的月末</param>
        /// <param name="fireTime">年初</param>
        /// <param name="fireEndTime">当前日期</param>
        /// <param name="result"></param>
        /// <param name="_monthlyreportList">一至本月累计</param>
        /// <param name="_nowmonthlyreportList">本月合计</param>
        private static void LYReportQueryData(string ORGNO, string nowFireTime, string nowfireEndTime, string fireTime, string fireEndTime, out List<T_SYS_ORGModel> result, out List<FIRERECORD_FIREINFO_Model> _monthlyreportList, out List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList)
        {
            result = new List<T_SYS_ORGModel>();
            if (PublicCls.OrgIsShi(ORGNO))
                result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXian = "1" }).ToList();
            if (PublicCls.OrgIsXian(ORGNO))
                result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXianXZ = "1" }).ToList();
            if (PublicCls.OrgIsZhen(ORGNO))
                result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO }).ToList();
            //if (PublicCls.OrgIsCun(ORGNO))
            // result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO }).ToList();
            List<T_SYS_DICTModel> dic304 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "304" }).ToList();
            _monthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = fireTime, FIREENDTIME = nowfireEndTime }).ToList();
            _nowmonthlyreportList = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW { BYORGNO = ORGNO, FIRETIME = nowFireTime, FIREENDTIME = nowfireEndTime }).ToList();
        }

        /// <summary>
        /// 组织机构对应的数据列表
        /// </summary>
        /// <param name="result">组织机构</param>
        /// <param name="_nowmonthlyreportList">获取的数据列表</param>
        /// <param name="i">i</param>
        /// <returns></returns>
        private static List<FIRERECORD_FIREINFO_Model> LYReportTemplist(List<T_SYS_ORGModel> result, List<FIRERECORD_FIREINFO_Model> _nowmonthlyreportList, int i)
        {
            List<FIRERECORD_FIREINFO_Model> templist = new List<FIRERECORD_FIREINFO_Model>();
            if (PublicCls.OrgIsShi(result[i].ORGNO))
                templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 4) == result[i].ORGNO.Substring(0, 4)).ToList();
            if (PublicCls.OrgIsXian(result[i].ORGNO))
                templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 6) == result[i].ORGNO.Substring(0, 6)).ToList();
            if (PublicCls.OrgIsZhen(result[i].ORGNO))
                templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS.Substring(0, 9) == result[i].ORGNO.Substring(0, 9)).ToList();
            if (PublicCls.OrgIsCun(result[i].ORGNO))
                templist = _nowmonthlyreportList.FindAll(a => a.FIREADDRESSTOWNS == result[i].ORGNO).ToList();
            return templist;
        }
        #endregion

        #region  组织机构编码集合
        /// <summary>
        /// 获取组织机构编码的集合
        /// </summary>
        /// <param name="ORGNO">当前登陆的组织机构编码</param>
        /// <returns></returns>
        private static List<T_SYS_ORGModel> ResultList(string ORGNO)
        {
            List<T_SYS_ORGModel> result = new List<T_SYS_ORGModel>();
            if (PublicCls.OrgIsShi(ORGNO))
                result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXian = "1" }).ToList();
            if (PublicCls.OrgIsXian(ORGNO))
                result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetXianXZ = "1" }).ToList();
            if (PublicCls.OrgIsZhen(ORGNO))
                result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO }).ToList();
            //if (PublicCls.OrgIsShi(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXian = "1" }).ToList();
            //if (PublicCls.OrgIsXian(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXianXZ = "1" }).ToList();
            //if (PublicCls.OrgIsZhen(ORGNO))
            //    result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO }).ToList();
            return result;
        }
        /// <summary>
        /// 获取管理中的单位编码
        /// </summary>
        /// <param name="ORGNO">系统登录的单位编码</param>
        private void vdorg(string ORGNO)
        {
            if (PublicCls.OrgIsShi(SystemCls.getCurUserOrgNo()))//如果是州级获取市县,否则取乡镇
            {
                ViewBag.vdOrg = T_SYS_ORGCls.getSelectOptionByORGNO(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXian = "1" });
            }
            else
            {
                ViewBag.vdOrg = T_SYS_ORGCls.getSelectOptionByORGNO(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = ORGNO, TopORGNO = ORGNO, OnlyGetShiXianXZ = "1", GetXZOrgNOByConty = ORGNO, });
            }
        }
        #endregion

        #region 林火表一

        #region  林火1表的表头
        private static string HEADER(string headname)
        {
            StringBuilder sb = new StringBuilder();
            List<T_SYS_DICTModel> dic304 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "304" }).ToList();
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"3\">{0}</th>", headname);
            sb.AppendFormat("<th colspan=\"" + (dic304.Count + 1) + "\" >森林火灾次数</th>");
            sb.AppendFormat("<th rowspan=\"3\"> 火&nbsp;&nbsp;场</br></br>总面积</br></br>(公顷)</th>");
            sb.AppendFormat("<th colspan=\"3\">受害森林面积(公顷)</th>");
            sb.AppendFormat("<th colspan=\"2\">损失林木</th>");
            sb.AppendFormat("<th colspan=\"4\" style=\"width:150px;\">人员伤亡</th>");
            sb.AppendFormat("<th rowspan=\"3\">其他损失</br></br>折&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;款</br></br>(万元)</th>");
            sb.AppendFormat("<th rowspan=\"3\">出动扑</br></br>火人工</br></br>(工日)</th>");
            sb.AppendFormat("<th colspan=\"2\">出动车辆(台)</th>");
            sb.AppendFormat("<th rowspan=\"3\">出动</br></br>飞机</br></br>(架次)</th>");
            sb.AppendFormat("<th rowspan=\"3\">扑火</br></br>经费</br></br>(万元)</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"2\">计</th>");
            foreach (var d1 in dic304)
            {
                string title = d1.DICTNAME + "</br></br>" + "火灾";
                if (!string.IsNullOrEmpty(d1.STANDBY1))
                    title += "</br>(" + d1.STANDBY1 + ")";
                sb.AppendFormat("<th rowspan=\"2\">{0}</th>", title);
            }
            sb.AppendFormat("<th rowspan=\"2\">计</th>");
            sb.AppendFormat("<th colspan=\"2\">其&nbsp;&nbsp;中</th>");
            sb.AppendFormat("<th rowspan=\"2\">成林蓄积<br /><br />(立方米)</th>");
            sb.AppendFormat("<th rowspan=\"2\">幼林株数<br /><br />(万株)</th>");
            sb.AppendFormat("<th rowspan=\"2\">计</th>");
            sb.AppendFormat("<th rowspan=\"2\">轻<br /><br />伤</th>");
            sb.AppendFormat("<th rowspan=\"2\">重<br /><br />伤</th>");
            sb.AppendFormat("<th rowspan=\"2\">死<br /><br />亡</th>");
            sb.AppendFormat("<th rowspan=\"2\">计</th>");
            sb.AppendFormat("<th rowspan=\"2\">其中<br><br>汽车</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>原始林</th>");
            sb.AppendFormat("<th>人工林</th>");
            sb.AppendFormat("</tr");
            sb.AppendFormat("</thead>");
            return sb.ToString();
        }
        #endregion

        #region  林火1表的数据累计
        private static string TOTAL(List<FIRERECORD_FIREINFO_Model> _monthlyreportList)
        {
            StringBuilder sb = new StringBuilder();
            List<T_SYS_DICTModel> dic304 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "304" }).ToList();
            for (int d1 = 0; d1 < dic304.Count; d1++)
            {
                var templist = _monthlyreportList.FindAll(a => a.FIRERECINFO001 == dic304[d1].DICTVALUE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", templist.Count);
            }
            List<float> totalist = CalZJ(_monthlyreportList);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[0]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[1] + totalist[2]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[1]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[2]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[3]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[4]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[5] + totalist[6] + totalist[7]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[5]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[6]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[7]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[8]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[9]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[19]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[10]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[11]);
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[12]);
            sb.AppendFormat("</tr>");
            return sb.ToString();
        }
        #endregion

        #region 林火1表中的Excel表头
        private void CreatExcelHead(List<T_SYS_DICTModel> dic304, int colsCount, string title, HSSFWorkbook book, ISheet sheet1, string headname)
        {
            for (int i = 0; i < colsCount; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 20 * 256);
                else
                    sheet1.SetColumnWidth(i, 15 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.Height = 2 * 350;
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.Height = 2 * 350;
            rowHead1.CreateCell(0).SetCellValue(headname);
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(1).SetCellValue("森林火灾次数");
            rowHead1.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(6).SetCellValue("火场\n总面积\n（公顷）");
            rowHead1.GetCell(6).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(7).SetCellValue("受害森林面积（公顷）");
            rowHead1.GetCell(7).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(10).SetCellValue("损失林木");
            rowHead1.GetCell(10).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(12).SetCellValue("人员伤亡");
            rowHead1.GetCell(12).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(16).SetCellValue("其他损失\n折款\n(万元)");
            rowHead1.GetCell(16).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(17).SetCellValue("出动扑\n火人工\n(工日)");
            rowHead1.GetCell(17).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(18).SetCellValue("出动车辆(台)");
            rowHead1.GetCell(18).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(20).SetCellValue("出动\n飞机\n(架次)");
            rowHead1.GetCell(20).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(21).SetCellValue("扑火\n经费\n(万元)");
            rowHead1.GetCell(21).CellStyle = getCellStyleHead(book);
            IRow rowHead2 = sheet1.CreateRow(2);
            rowHead2.CreateCell(1).SetCellValue("合计");
            rowHead2.GetCell(1).CellStyle = getCellStyleHead(book);
            int x = 2;
            for (int i = 0; i < dic304.Count; i++)
            {
                string name = dic304[i].DICTNAME;
                if (!string.IsNullOrEmpty(dic304[i].STANDBY1))
                    name += "\n(" + dic304[i].STANDBY1 + ")";
                rowHead2.CreateCell(x).SetCellValue(name);
                rowHead2.GetCell(x).CellStyle = getCellStyleHead(book);
                x++;
            }
            x = x + 1;
            rowHead2.CreateCell(x).SetCellValue("计");
            rowHead2.GetCell(x).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(x + 1).SetCellValue("其中");
            rowHead2.GetCell(x + 1).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(x + 3).SetCellValue("成林蓄积\n(立方米)");
            rowHead2.GetCell(x + 3).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(x + 4).SetCellValue("幼林株数\n(万株)");
            rowHead2.GetCell(x + 4).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(x + 5).SetCellValue("计");
            rowHead2.GetCell(x + 5).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(x + 6).SetCellValue("轻伤");
            rowHead2.GetCell(x + 6).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(x + 7).SetCellValue("重伤");
            rowHead2.GetCell(x + 7).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(x + 8).SetCellValue("死亡");
            rowHead2.GetCell(x + 8).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(x + 11).SetCellValue("计");
            rowHead2.GetCell(x + 11).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(x + 12).SetCellValue("其中汽车");
            rowHead2.GetCell(x + 12).CellStyle = getCellStyleHead(book);

            IRow rowHead3 = sheet1.CreateRow(3);
            rowHead3.CreateCell(8).SetCellValue("原始林");
            rowHead3.GetCell(8).CellStyle = getCellStyleHead(book);
            rowHead3.CreateCell(9).SetCellValue("人工林");
            rowHead3.GetCell(9).CellStyle = getCellStyleHead(book);

            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount - 1));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 0, 0));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 1, dic304.Count + 1));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, dic304.Count + 2, dic304.Count + 2));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, dic304.Count + 3, dic304.Count + 5));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, dic304.Count + 6, dic304.Count + 7));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, dic304.Count + 8, dic304.Count + 11));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, dic304.Count + 12, dic304.Count + 12));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, dic304.Count + 13, dic304.Count + 13));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, dic304.Count + 14, dic304.Count + 15));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, dic304.Count + 16, dic304.Count + 16));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, dic304.Count + 17, dic304.Count + 17));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 1, 1));
            int index = 2;
            for (int i = 0; i < dic304.Count; i++)
            {
                sheet1.AddMergedRegion(new CellRangeAddress(2, 3, index, index));
                index++;
            }
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, index + 1, index + 1));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 2, index + 2, index + 3));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, index + 4, index + 4));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, index + 5, index + 5));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, index + 6, index + 6));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, index + 7, index + 7));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, index + 8, index + 8));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, index + 9, index + 9));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, index + 12, index + 12));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, index + 13, index + 13));
        }
        #endregion

        #region 林火1表中的Excel数据累计
        private int ExcelTotal(List<FIRERECORD_FIREINFO_Model> _monthlyreportList, HSSFWorkbook book, IRow row4, int j)
        {
            List<float> acountlist = CalZJ(_monthlyreportList);
            row4.CreateCell(j).SetCellValue(acountlist[0] > 0 ? string.Format("{0:0.00}", acountlist[0]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[1] + acountlist[2] > 0 ? string.Format("{0:0.00}", acountlist[1] + acountlist[2]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[1] > 0 ? string.Format("{0:0.00}", acountlist[1]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[2] > 0 ? string.Format("{0:0.00}", acountlist[2]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[3] > 0 ? string.Format("{0:0.00}", acountlist[3]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[4] > 0 ? string.Format("{0:0.00}", acountlist[4]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[5] + acountlist[6] + acountlist[7]);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[5]);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[6]);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[7]);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[8] > 0 ? string.Format("{0:0.00}", acountlist[8]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[9] > 0 ? string.Format("{0:0.00}", acountlist[9]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[19]);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[10]);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[11]);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            row4.CreateCell(j).SetCellValue(acountlist[12] > 0 ? string.Format("{0:0.00}", acountlist[12]) : "0");
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            return j;
        }
        #endregion

        #endregion

        #region 林火表2

        #region  林火2表的表头
        private static string HEADER2()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"3\">地&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;级</br></br>或&nbsp;&nbsp;县&nbsp;&nbsp;级</br></br>名&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;称</th>");
            sb.AppendFormat("<th colspan=\"26\"  style=\"width:400px;\"> 已&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;查&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;明&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;火&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;源&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;次&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;数</th>");
            sb.AppendFormat("<th rowspan=\"3\">未查明</br></br>火源</br></br>次数</th>");
            sb.AppendFormat("<th colspan=\"3\">火&nbsp;&nbsp;案&nbsp;&nbsp;处&nbsp;&nbsp;理&nbsp;&nbsp;情&nbsp;&nbsp;况</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"2\">合计</th>");
            sb.AppendFormat("<th colspan=\"10\">生&nbsp;&nbsp;产&nbsp;&nbsp;性&nbsp;&nbsp;火&nbsp;&nbsp;源</th>");
            sb.AppendFormat("<th colspan=\"10\">非&nbsp;&nbsp;生&nbsp;&nbsp;产&nbsp;&nbsp;性&nbsp;&nbsp;用&nbsp;&nbsp;火</th>");
            ////数据字典表头
            //List<int> totallist = HEADMETHODS();
            //for (int d1 = totallist[1]; d1 < totallist[2]; d1++)
            //{
            //    string title = dic302[d1].DICTNAME;
            //    string title1 = title.Substring(0,2)+"</br></br>"+title.Substring (2);
            //    sb.AppendFormat("<th rowspan=\"2\">{0}</th>", title1);
            //}
            sb.AppendFormat("<th rowspan=\"2\" style=\"width:27px;\">故</br></br>意</br></br>放</br></br>火</th>");
            sb.AppendFormat("<th rowspan=\"2\">外省</br></br>（区）</br></br>烧入</th>");
            sb.AppendFormat("<th rowspan=\"2\" style=\"width:27px;\">外</br></br>国</br></br>烧</br></br>入</th>");
            sb.AppendFormat("<th rowspan=\"2\" style=\"width:27px;\">雷</br></br>击</br></br>火</th>");
            sb.AppendFormat("<th rowspan=\"2\">其</br></br>他</br></br>自</br>然</br>火</th>");
            sb.AppendFormat("<th rowspan=\"2\">已处理</br></br>起数</th>");
            sb.AppendFormat("<th rowspan=\"2\">已处理</br></br>人数</th>");
            sb.AppendFormat("<th rowspan=\"2\">其中</br></br>刑事</br></br>处罚</br></br>人数</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>计</th>");
            //for (int d1 = 0; d1 < totallist[0]; d1++)
            //{
            //    string title = dic302[d1].DICTNAME;
            //    sb.AppendFormat("<th rowspan=\"2\">{0}</th>", title);
            //}
            sb.AppendFormat("<th>烧荒</br>烧炭</th>");
            sb.AppendFormat("<th>练山</br>造林</th>");
            sb.AppendFormat("<th>烧牧</br>场</th>");
            sb.AppendFormat("<th>烧窑</th>");
            sb.AppendFormat("<th>烧隔</br>离带</th>");
            sb.AppendFormat("<th>火车</br>喷漏</th>");
            sb.AppendFormat("<th>火车</br>甩瓦</th>");
            sb.AppendFormat("<th>机车</br>喷火</th>");
            sb.AppendFormat("<th>其他</th>");
            sb.AppendFormat("<th>计</th>");
            //for (int d1 = totallist[0]; d1 < totallist[1]; d1++)
            //{
            //    string title = dic302[d1].DICTNAME;
            //    sb.AppendFormat("<th>{0}</th>", title);
            //}
            sb.AppendFormat("<th>野外</br>吸烟 </th>");
            sb.AppendFormat("<th>取暖</br>做饭</th>");
            sb.AppendFormat("<th>上坟</br>烧纸</th>");
            sb.AppendFormat("<th>烧山</br>驱兽</th>");
            sb.AppendFormat("<th>小孩</br>玩火</th>");
            sb.AppendFormat("<th>痴呆</br>弄火</th>");
            sb.AppendFormat("<th>家火</br>上山</th>");
            sb.AppendFormat("<th>电线</br>引起</th>");
            sb.AppendFormat("<th>其他</th>");
            sb.AppendFormat("</tr");
            sb.AppendFormat("</thead>");
            return sb.ToString();
        }
        #endregion

        #region 林火2表中的共用方法
        private static string METHODS(List<FIRERECORD_FIREINFO_Model> _monthlyreportList)
        {
            StringBuilder sb = new StringBuilder();
            List<T_SYS_DICTModel> dic302 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "302" }).ToList();
            List<FIRERECORD_FIREINFO_Model> list1 = new List<FIRERECORD_FIREINFO_Model>();
            List<FIRERECORD_FIREINFO_Model> list2 = new List<FIRERECORD_FIREINFO_Model>();
            List<FIRERECORD_FIREINFO_Model> list3 = new List<FIRERECORD_FIREINFO_Model>();
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            for (int i = 0; i < dic302.Count; i++)
            {
                if (int.Parse(dic302[i].DICTVALUE) >= 100 && int.Parse(dic302[i].DICTVALUE) <= 199)
                {
                    count1++;
                    list1.AddRange(_monthlyreportList.FindAll(a => a.FIRERECINFO160 == dic302[i].DICTVALUE));
                }
                if (int.Parse(dic302[i].DICTVALUE) >= 200 && int.Parse(dic302[i].DICTVALUE) <= 299)
                {
                    count2++;
                    list2.AddRange(_monthlyreportList.FindAll(a => a.FIRERECINFO160 == dic302[i].DICTVALUE));
                }
                if (int.Parse(dic302[i].DICTVALUE) >= 300 && int.Parse(dic302[i].DICTVALUE) <= 399)
                {
                    count3++;
                    list3.AddRange(_monthlyreportList.FindAll(a => a.FIRERECINFO160 == dic302[i].DICTVALUE));
                }
            }
            sb.AppendFormat("<td class=\"center\">{0}</td>", list1.Count);
            for (int d1 = 0; d1 < count1; d1++)
            {
                var templist1 = list1.FindAll(a => a.FIRERECINFO160 == dic302[d1].DICTVALUE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", templist1.Count);
            }
            sb.AppendFormat("<td class=\"center\">{0}</td>", list2.Count);
            for (int d2 = count2; d2 < count1 + count2; d2++)
            {
                var templist2 = list2.FindAll(a => a.FIRERECINFO160 == dic302[d2].DICTVALUE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", templist2.Count);
            }
            for (int d3 = count1 + count2; d3 < count1 + count2 + count3; d3++)
            {
                var templist3 = list3.FindAll(a => a.FIRERECINFO160 == dic302[d3].DICTVALUE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", templist3.Count);
            }
            List<float> totalist = CalZJ(_monthlyreportList);
            //未查明火源次数
            sb.AppendFormat("<td class=\"center\">{0}</td>", _monthlyreportList.Count - totalist[13]);
            //已处理起数
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[14]);
            //已经处理人数
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[15] + totalist[16]);
            //其中刑事处罚人数
            sb.AppendFormat("<td class=\"center\">{0}</td>", totalist[16]);
            sb.AppendFormat("</tr>");
            return sb.ToString();
        }
        #endregion

        #region 林火2表中的Excel的表头
        private void CreatExcel2Head(List<T_SYS_DICTModel> dic302, int colsCount, string title, HSSFWorkbook book, ISheet sheet1)
        {
            for (int i = 0; i < colsCount; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 20 * 256);
                else
                    sheet1.SetColumnWidth(i, 10 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.Height = 2 * 350;
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.Height = 2 * 350;
            rowHead1.CreateCell(0).SetCellValue("地级\n\n或县级\n\n名称");
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(1).SetCellValue("已  查  明  火  源  次  数");
            rowHead1.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(27).SetCellValue("未查明\n火源\n次数");
            rowHead1.GetCell(27).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(28).SetCellValue("火案处理情况");
            rowHead1.GetCell(28).CellStyle = getCellStyleHead(book);

            IRow rowHead2 = sheet1.CreateRow(2);
            rowHead2.Height = 2 * 350;
            rowHead2.CreateCell(1).SetCellValue("合计");
            rowHead2.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(2).SetCellValue("生 产 性 火 源");
            rowHead2.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(12).SetCellValue("非 生 产 性 用 火");
            rowHead2.GetCell(12).CellStyle = getCellStyleHead(book);
            //数据字典表头
            List<int> totallist = HEADMETHODS();
            int x = 22;
            for (int d1 = totallist[1]; d1 < totallist[2]; d1++)
            {
                string DICT = dic302[d1].DICTNAME;
                rowHead2.CreateCell(x).SetCellValue(DICT);
                rowHead2.GetCell(x).CellStyle = getCellStyleHead(book);
                x++;
            }
            //rowHead2.CreateCell(22).SetCellValue("故意放火");
            //rowHead2.GetCell(22).CellStyle = getCellStyleHead(book);
            //rowHead2.CreateCell(23).SetCellValue("外省(区)烧入");
            //rowHead2.GetCell(23).CellStyle = getCellStyleHead(book);
            //rowHead2.CreateCell(24).SetCellValue("外国烧入");
            //rowHead2.GetCell(24).CellStyle = getCellStyleHead(book);
            //rowHead2.CreateCell(25).SetCellValue("雷击火");
            //rowHead2.GetCell(25).CellStyle = getCellStyleHead(book);
            //rowHead2.CreateCell(26).SetCellValue("其他自然火");
            //rowHead2.GetCell(26).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(28).SetCellValue("已处理\n起数");
            rowHead2.GetCell(28).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(29).SetCellValue("已处理\n人数");
            rowHead2.GetCell(29).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(30).SetCellValue("其中\n刑事\n处罚\n人数");
            rowHead2.GetCell(30).CellStyle = getCellStyleHead(book);

            IRow rowHead3 = sheet1.CreateRow(3);
            rowHead3.Height = 2 * 350;
            rowHead3.CreateCell(2).SetCellValue("计");
            rowHead3.GetCell(2).CellStyle = getCellStyleHead(book);
            int y = 3;
            for (int d1 = 0; d1 < totallist[0]; d1++)
            {
                string DICT = dic302[d1].DICTNAME;
                rowHead3.CreateCell(y).SetCellValue(DICT);
                rowHead3.GetCell(y).CellStyle = getCellStyleHead(book);
                y++;
            }
            rowHead3.CreateCell(y).SetCellValue("计");
            rowHead3.GetCell(y).CellStyle = getCellStyleHead(book);
            y++;
            for (int d1 = totallist[0]; d1 < totallist[1]; d1++)
            {
                string DICT = dic302[d1].DICTNAME;
                rowHead3.CreateCell(y).SetCellValue(DICT);
                rowHead3.GetCell(y).CellStyle = getCellStyleHead(book);
                y++;
            }
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount - 1));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 0, 0));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 1, dic302.Count + 3));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 27, 27));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 28, 30));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 1, 1));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 2, 11));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 12, 21));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 12, 21));
            int index = 22;
            for (int d1 = totallist[1]; d1 < totallist[2]; d1++)
            {
                sheet1.AddMergedRegion(new CellRangeAddress(2, 3, index, index));
                index++;
            }
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 28, 28));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 29, 29));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 30, 30));
        }
        #endregion

        #region  林火2表中的Excel数据累计
        private int CreateExcel2Total(List<T_SYS_DICTModel> dic302, List<FIRERECORD_FIREINFO_Model> _monthlyreportList, HSSFWorkbook book, IRow row4, int j, out int count1, out int count2, out int count3)
        {
            row4.CreateCell(j).SetCellValue(_monthlyreportList.Count);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            List<FIRERECORD_FIREINFO_Model> list1 = new List<FIRERECORD_FIREINFO_Model>();
            List<FIRERECORD_FIREINFO_Model> list2 = new List<FIRERECORD_FIREINFO_Model>();
            List<FIRERECORD_FIREINFO_Model> list3 = new List<FIRERECORD_FIREINFO_Model>();
            count1 = 0;
            count2 = 0;
            count3 = 0;
            for (int i = 0; i < dic302.Count; i++)
            {
                if (int.Parse(dic302[i].DICTVALUE) >= 100 && int.Parse(dic302[i].DICTVALUE) <= 199)
                {
                    count1++;
                    list1.AddRange(_monthlyreportList.FindAll(a => a.FIRERECINFO160 == dic302[i].DICTVALUE));
                }
                if (int.Parse(dic302[i].DICTVALUE) >= 200 && int.Parse(dic302[i].DICTVALUE) <= 299)
                {
                    count2++;
                    list2.AddRange(_monthlyreportList.FindAll(a => a.FIRERECINFO160 == dic302[i].DICTVALUE));
                }
                if (int.Parse(dic302[i].DICTVALUE) >= 300 && int.Parse(dic302[i].DICTVALUE) <= 399)
                {
                    count3++;
                    list3.AddRange(_monthlyreportList.FindAll(a => a.FIRERECINFO160 == dic302[i].DICTVALUE));
                }
            }
            row4.CreateCell(j).SetCellValue(list1.Count);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            for (int d1 = 0; d1 < count1; d1++)
            {
                var templist1 = list1.FindAll(a => a.FIRERECINFO160 == dic302[d1].DICTVALUE);
                row4.CreateCell(j).SetCellValue(templist1.Count);
                row4.GetCell(j).CellStyle = getCellStyleCenter(book);
                j++;
            }
            row4.CreateCell(j).SetCellValue(list2.Count);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            for (int d2 = count2; d2 < count1 + count2; d2++)
            {
                var templist2 = list2.FindAll(a => a.FIRERECINFO160 == dic302[d2].DICTVALUE);
                row4.CreateCell(j).SetCellValue(templist2.Count);
                row4.GetCell(j).CellStyle = getCellStyleCenter(book);
                j++;
            }
            for (int d3 = count1 + count2; d3 < count1 + count2 + count3; d3++)
            {
                var templist3 = list3.FindAll(a => a.FIRERECINFO160 == dic302[d3].DICTVALUE);
                row4.CreateCell(j).SetCellValue(templist3.Count);
                row4.GetCell(j).CellStyle = getCellStyleCenter(book);
                j++;
            }
            List<float> totalist = CalZJ(_monthlyreportList);
            //未查明火源次数
            row4.CreateCell(j).SetCellValue(_monthlyreportList.Count - totalist[13]);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            //已处理起数
            row4.CreateCell(j).SetCellValue(totalist[14]);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            //已经处理人数
            row4.CreateCell(j).SetCellValue(totalist[15] + totalist[16]);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            j++;
            //其中刑事处罚人数
            row4.CreateCell(j).SetCellValue(totalist[16]);
            row4.GetCell(j).CellStyle = getCellStyleCenter(book);
            return j;
        }
        #endregion

        #region 林火2表中的数据字典表头方法
        private static List<int> HEADMETHODS()
        {
            List<int> _templist = new List<int>();
            List<T_SYS_DICTModel> dic302 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "302" }).ToList();
            List<FIRERECORD_FIREINFO_Model> list1 = new List<FIRERECORD_FIREINFO_Model>();
            List<FIRERECORD_FIREINFO_Model> list2 = new List<FIRERECORD_FIREINFO_Model>();
            List<FIRERECORD_FIREINFO_Model> list3 = new List<FIRERECORD_FIREINFO_Model>();
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            for (int i = 0; i < dic302.Count; i++)
            {
                if (int.Parse(dic302[i].DICTVALUE) >= 100 && int.Parse(dic302[i].DICTVALUE) <= 199)
                    count1++;
                if (int.Parse(dic302[i].DICTVALUE) >= 200 && int.Parse(dic302[i].DICTVALUE) <= 299)
                    count2++;
                if (int.Parse(dic302[i].DICTVALUE) >= 300 && int.Parse(dic302[i].DICTVALUE) <= 399)
                    count3++;
            }
            _templist.Add(count1);
            _templist.Add(count1 + count2);
            _templist.Add(count1 + count2 + count3);
            return _templist;
        }
        #endregion

        #endregion

        #region 森林防火基础设施统计表一

        #region 计算森林防火基础设施统计表一合计
        /// <summary>
        /// 计算森林防火基础设施统计表一合计
        /// </summary>
        /// <param name="_yearreportlist">数据列表</param>
        /// <returns></returns>
        private static List<float> CalHJ1(List<FIRERECORD_REPORT10_Model> _yearreportlist)
        {
            List<T_SYS_DICTModel> dic312 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "312" }).ToList();//基础设施统计年报表一
            List<float> _totallist = new List<float>();
            for (int i = 0; i < dic312.Count - 1; i++)
            {
                float total = 0;
                var templist = _yearreportlist.FindAll(a => a.REPORTCODE == dic312[i].DICTVALUE);
                if (templist.Count == 0)
                {
                    _totallist.Add(total);
                }
                else
                {
                    foreach (var v in templist)
                    {
                        if (!string.IsNullOrEmpty(v.REPORTVALUE))
                        {
                            total += float.Parse(v.REPORTVALUE);
                        }

                    } _totallist.Add(total);
                }
            }
            return _totallist;
        }
        #endregion

        #region 森林防火基础设施统计表一Excel
        /// <summary>
        /// 森林防火基础设施统计表一
        /// </summary>
        /// <param name="_yearreportlist">获取的数据列表</param>
        /// <param name="dic312">森林防火基础设施统计表一数据字典</param>
        /// <param name="book">HSSFWorkbook book</param>
        /// <param name="row4">行号</param>
        /// <param name="x">增加列</param>
        /// <returns></returns>
        private int FIRERECORD_REPORT10Excel(List<FIRERECORD_REPORT10_Model> _yearreportlist, List<T_SYS_DICTModel> dic312, HSSFWorkbook book, IRow row4, int x)
        {
            List<float> total = CalHJ1(_yearreportlist);
            row4.CreateCell(x).SetCellValue(total[0] + total[1] + total[2]);
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            for (int i = 0; i < dic312.Count - 6; i++)
            {
                row4.CreateCell(x).SetCellValue(total[i]);
                row4.GetCell(x).CellStyle = getCellStyleCenter(book);
                x++;
            }
            float HJ = total[10] + total[11] + total[12] + total[13] + total[14];
            float DBHJ = total[10] + total[11];
            float TGPHJ = total[12] + total[13] + total[14];
            row4.CreateCell(x).SetCellValue(HJ);
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            row4.CreateCell(x).SetCellValue(DBHJ);
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            row4.CreateCell(x).SetCellValue(total[10]);
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            row4.CreateCell(x).SetCellValue(total[11]);
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            row4.CreateCell(x).SetCellValue(TGPHJ);
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            row4.CreateCell(x).SetCellValue(total[12]);
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            row4.CreateCell(x).SetCellValue(total[13]);
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            row4.CreateCell(x).SetCellValue(total[14]);
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            return x;
        }
        #endregion
        #endregion

        #region 森林防火基础设施统计表二

        #region 计算森林防火基础设施统计表二合计
        /// <summary>
        /// 计算森林防火基础设施统计表二合计
        /// </summary>
        /// <param name="_yearreportlist">数据列表</param>
        /// <returns></returns>
        private static List<float> CalHJ2(List<FIRERECORD_REPORT11_Model> _yearreportlist)
        {
            List<T_SYS_DICTModel> dic313 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "313" }).ToList();//基础设施统计年报表一
            List<float> _totallist = new List<float>();
            for (int i = 0; i < dic313.Count - 1; i++)
            {
                float total = 0;
                var templist = _yearreportlist.FindAll(a => a.REPORTCODE == dic313[i].DICTVALUE);
                if (templist.Count == 0)
                {
                    _totallist.Add(total);
                }
                else
                {
                    foreach (var v in templist)
                    {
                        if (!string.IsNullOrEmpty(v.REPORTVALUE))
                        {
                            total += float.Parse(v.REPORTVALUE);
                        }

                    } _totallist.Add(total);
                }
            }
            return _totallist;
        }
        #endregion

        #region 森林防火基础设施统计表二Excel
        /// <summary>
        /// 森林防火基础设施统计表二
        /// </summary>
        /// <param name="_yearreportlist">获取的数据列表</param>
        /// <param name="dic313">森林防火基础设施统计表二数据字典</param>
        /// <param name="book">HSSFWorkbook book</param>
        /// <param name="row4">行号</param>
        /// <param name="x">增加列</param>
        /// <returns></returns>
        private int FIRERECORD_REPORT11Excel(List<FIRERECORD_REPORT11_Model> _yearreportlist, List<T_SYS_DICTModel> dic313, HSSFWorkbook book, IRow row4, int x)
        {
            int count1 = 0;
            int count2 = 0;
            for (int i = 0; i < dic313.Count; i++)
            {
                if (int.Parse(dic313[i].DICTVALUE) >= 100 && int.Parse(dic313[i].DICTVALUE) <= 299)
                {
                    count1++;
                }
                if (int.Parse(dic313[i].DICTVALUE) >= 300 && int.Parse(dic313[i].DICTVALUE) <= 399)
                {
                    count2++;
                }
            }
            List<float> total = CalHJ2(_yearreportlist);
            for (int i = 0; i < count1; i++)
            {
                row4.CreateCell(x).SetCellValue(total[i]);
                row4.GetCell(x).CellStyle = getCellStyleCenter(book);
                x++;
            }
            float XFCLHJ = total[4] + total[5] + total[6] + total[7] + total[8];
            row4.CreateCell(x).SetCellValue(XFCLHJ);
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            for (int i = count1; i < count1 + count2; i++)
            {
                row4.CreateCell(x).SetCellValue(total[i]);
                row4.GetCell(x).CellStyle = getCellStyleCenter(book);
                x++;
            }
            for (int i = count1 + count2; i < dic313.Count - 1; i++)
            {
                row4.CreateCell(x).SetCellValue(total[i]);
                row4.GetCell(x).CellStyle = getCellStyleCenter(book);
                x++;
            }
            return x;
        }
        #endregion

        #endregion

        #region 火情档案_森林防火建设资金统计年报表

        #region 火情档案_森林防火建设资金统计年报表合计
        /// <summary>
        /// 火情档案_森林防火建设资金统计年报表合计
        /// </summary>
        /// <param name="_yearreportlist">数据列表</param>
        /// <returns></returns>
        private static List<float> CalHJ3(List<FIRERECORD_REPORT12_Model> _yearreportlist)
        {
            List<T_SYS_DICTModel> dic314 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "314" }).ToList();//森林防火建设资金统计
            List<float> _totallist = new List<float>();
            for (int i = 0; i < dic314.Count - 1; i++)
            {
                float total = 0;
                var templist = _yearreportlist.FindAll(a => a.REPORTCODE == dic314[i].DICTVALUE);
                if (templist.Count == 0)
                {
                    _totallist.Add(total);
                }
                else
                {
                    foreach (var v in templist)
                    {
                        if (!string.IsNullOrEmpty(v.REPORTVALUE))
                        {
                            total += float.Parse(v.REPORTVALUE);
                        }

                    } _totallist.Add(total);
                }
            }
            return _totallist;
        }
        #endregion

        #region 森林防火建设资金统计年报表Excel
        /// <summary>
        /// 森林防火建设资金统计年报表Excel
        /// </summary>
        /// <param name="_yearreportlist">数据列表</param>
        /// <param name="dic314">数据字典森林防火建设资金统计</param>
        /// <param name="book">HSSFWorkbook book</param>
        /// <param name="row3">增加行</param>
        /// <param name="x">增加列</param>
        /// <returns></returns>
        private int FIRERECORD_REPORT12Excel(List<FIRERECORD_REPORT12_Model> _yearreportlist, List<T_SYS_DICTModel> dic314, HSSFWorkbook book, IRow row3, int x)
        {
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            for (int i = 0; i < dic314.Count; i++)
            {
                if (int.Parse(dic314[i].DICTVALUE) >= 100 && int.Parse(dic314[i].DICTVALUE) <= 199)
                {
                    count1++;
                }
                if (int.Parse(dic314[i].DICTVALUE) >= 200 && int.Parse(dic314[i].DICTVALUE) <= 299)
                {
                    count2++;
                }
                if (int.Parse(dic314[i].DICTVALUE) >= 300 && int.Parse(dic314[i].DICTVALUE) <= 399)
                {
                    count3++;
                }

            }
            List<float> total = CalHJ3(_yearreportlist);
            float HJ = total[0] + total[1] + total[2] + total[3] + total[4] + total[5];
            row3.CreateCell(x).SetCellValue(HJ);
            row3.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            for (int i = 0; i < count1; i++)
            {
                row3.CreateCell(x).SetCellValue(total[i]);
                row3.GetCell(x).CellStyle = getCellStyleCenter(book);
                x++;
            }
            float GJZXBZ = total[6] + total[7] + total[8] + total[9] + total[10] + total[11];
            row3.CreateCell(x).SetCellValue(GJZXBZ);
            row3.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;

            for (int i = count1; i < count1 + count2; i++)
            {
                row3.CreateCell(x).SetCellValue(total[i]);
                row3.GetCell(x).CellStyle = getCellStyleCenter(book);
                x++;
            }
            float DFPT = total[12] + total[13] + total[14] + total[15];
            row3.CreateCell(x).SetCellValue(DFPT);
            row3.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            for (int i = count1 + count2; i < dic314.Count - 1; i++)
            {
                row3.CreateCell(x).SetCellValue(total[i]);
                row3.GetCell(x).CellStyle = getCellStyleCenter(book);
                x++;
            }
            return x;
        }

        #endregion

        #endregion

        #region 火情档案_队伍表

        #region 火情档案_队伍表合计
        /// <summary>
        /// 火情档案_队伍表合计
        /// </summary>
        /// <param name="_yearreportlist">数据列表</param>
        /// <returns></returns>
        private static List<float> CalARMYHJ(List<FIRERECORD_ARMY_Model> _yearreportlist)
        {
            List<T_SYS_DICTModel> dic315 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "315" }).ToList();//队伍表
            List<float> _totallist = new List<float>();
            for (int i = 0; i < dic315.Count; i++)
            {
                float total = 0;
                var templist = _yearreportlist.FindAll(a => a.REPORTCODE == dic315[i].DICTVALUE);
                if (templist.Count == 0)
                {
                    _totallist.Add(total);
                }
                else
                {
                    foreach (var v in templist)
                    {
                        if (!string.IsNullOrEmpty(v.REPORTVALUE))
                        {
                            total += float.Parse(v.REPORTVALUE);
                        }
                    }
                    _totallist.Add(total);
                }
            }
            return _totallist;
        }
        #endregion

        #region 队伍表Excel
        /// <summary>
        /// 队伍表Excel
        /// </summary>
        /// <param name="_yearreportlist">数据列表</param>
        /// <param name="dic315">队伍表数据字典</param>
        /// <param name="book">sheet表</param>
        /// <param name="row4">增加行</param>
        /// <param name="x">增加列</param>
        /// <returns></returns>
        private int ARMYExcel(List<FIRERECORD_ARMY_Model> _yearreportlist, List<T_SYS_DICTModel> dic315, HSSFWorkbook book, IRow row4, int x)
        {
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            for (int i = 0; i < dic315.Count; i++)
            {
                if (int.Parse(dic315[i].DICTVALUE) >= 100 && int.Parse(dic315[i].DICTVALUE) <= 399)
                {
                    count1++;
                }
                if (int.Parse(dic315[i].DICTVALUE) >= 400 && int.Parse(dic315[i].DICTVALUE) <= 499)
                {
                    count2++;
                }
                if (int.Parse(dic315[i].DICTVALUE) >= 500 && int.Parse(dic315[i].DICTVALUE) <= 599)
                {
                    count3++;
                }

            }
            List<float> total = CalARMYHJ(_yearreportlist);
            for (int i = 0; i < count1; i++)
            {
                row4.CreateCell(x).SetCellValue(total[i]);
                row4.GetCell(x).CellStyle = getCellStyleCenter(book);
                x++;
            }
            float JDSL = total[6] + total[7] + total[8] + total[9] + total[10];
            row4.CreateCell(x).SetCellValue(JDSL);
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            for (int i = count1; i < count1 + count2; i++)
            {
                row4.CreateCell(x).SetCellValue(total[i]);
                row4.GetCell(x).CellStyle = getCellStyleCenter(book);
                x++;
            }
            float JDCZ = total[11] + total[12] + total[13] + total[14] + total[15];
            row4.CreateCell(x).SetCellValue(JDCZ);
            row4.GetCell(x).CellStyle = getCellStyleCenter(book);
            x++;
            for (int i = count1 + count2; i < dic315.Count; i++)
            {
                row4.CreateCell(x).SetCellValue(total[i]);
                row4.GetCell(x).CellStyle = getCellStyleCenter(book);
                x++;
            }
            return x;
        }
        #endregion
        #endregion

        #endregion
    }
}
