using ManagerSystemClassLibrary;
using ManagerSystemClassLibrary.BaseDT.SDE;
using ManagerSystemClassLibrary.SDECLS;
using ManagerSystemModel;
using ManagerSystemModel.SDEModel;
using ManagerSystemSearchWhereModel;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
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
    public class PESTController : BaseController
    {
        #region 面积单位
        private static string dic113Name = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "113" }).ToList()[0].DICTNAME;
        #endregion

        #region 有害生物管理
        /// <summary>
        /// 有害生物列表
        /// </summary>
        /// <returns></returns>
        public ActionResult PESTList()
        {
            pubViewBag("006021", "006021", "有害生物管理");
            if (ViewBag.isPageRight == false)
                return View();
            string PESTCODE = Request.Params["PESTCODE"];//当前页面传递编号
            //导航条
            string navStr = "";
            if (string.IsNullOrEmpty(PESTCODE))
                ViewBag.PESTList = GetPESTStr(new T_SYS_PEST_SW { IsGetTopCode = true });
            else
            {
                if (PESTCODE.Length > 1)
                {
                    ViewBag.PESTList = GetPESTStr(new T_SYS_PEST_SW { PESTCODE = PESTCODE, ChildCODELength = PESTCODE.Length + 2 });
                    for (int i = 0; i < PESTCODE.Length / 2; i++)
                    {
                        if (i != PESTCODE.Length / 2 - 1)
                            navStr += "<li class=\"active\"><a href=\"/PEST/PESTList?PESTCODE=" + PESTCODE.Substring(0, (i + 1) * 2) + "\" >" + T_SYS_PESTCls.getName(PESTCODE.Substring(0, (i + 1) * 2)) + "</a></li>";

                        else
                            navStr += "<li class=\"active\">" + T_SYS_PESTCls.getName(PESTCODE) + "</li>";
                    }
                }
            }
            ViewBag.navList = navStr;
            ViewBag.PESTCODE = PESTCODE;
            ViewBag.CODELength = !string.IsNullOrEmpty(PESTCODE) ? PESTCODE.Length + 2 : 2;
            ViewBag.Add = (SystemCls.isRight("006021001")) ? 1 : 0;
            ViewBag.Del = (SystemCls.isRight("006021003")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 有害生物列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult PESTListQuery()
        {
            string PESTCODE = Request.Params["PESTCODE"];//当前页面传递编号
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(PESTCODE))
                sb.AppendFormat(GetPESTStr(new T_SYS_PEST_SW { IsGetTopCode = true }));
            else
                sb.AppendFormat(GetPESTStr(new T_SYS_PEST_SW { PESTCODE = PESTCODE, ChildCODELength = PESTCODE.Length + 2 }));
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取有害生物列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        private string GetPESTStr(T_SYS_PEST_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            bool IsMdy = SystemCls.isRight("006021002") ? true : false;
            List<T_SYS_PEST_Model> result = T_SYS_PESTCls.getListModel(sw).ToList();

            #region 数据表
            sb.AppendFormat("<table id=\"PestTable\" cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            string dis = result.Count <= 0 ? "disabled=\"disabled\"" : "";
            sb.AppendFormat("<th style=\"width:5%\"><input id=\"tbxPESTCODEALL\" name=\"tbxPESTCODEALL\" type=\"checkbox\" class=\"ace\" value=\"ALL\" onclick=\"SelectAll(this.value,this.checked)\" {0} /></th>", dis);
            sb.AppendFormat("<th>序号</th><th style=\"width:20%;\">编码</th><th style=\"width:20%;\">名称</th><th style=\"width:20%;\">拉丁名称</th><th>排序号</th>");
            if (IsMdy)
                sb.AppendFormat("<th>操作</th>");
            sb.AppendFormat("<th style=\"width:10%;\"></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"SetColor(this)\" >", i % 2 == 0 ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxPESTCODE" + i + "\" name=\"tbxPESTCODE\"  type=\"checkbox\" class=\"ace\" value=\"" + v.PESTCODE + "\" onclick=\"SelectAll(this.value,this.checked)\" />");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PESTCODE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PESTNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LATINNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORDERBY);
                if (IsMdy)
                    sb.AppendFormat("<td class=\"center\"><input type=\"button\" value=\"修改\" onclick=\"Manager('Mdy','{0}')\" class=\"btnMdyCss\" /></td>", v.PESTCODE);
                sb.AppendFormat("<td class=\"center\">");
                if (v.PESTCODE.Length < 20)
                    sb.AppendFormat("<a href=\"/PEST/PESTList?PESTCODE={0}\" >下属有害生物</a>", v.PESTCODE);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion

            return sb.ToString();
        }

        /// <summary>
        /// 获取有害生物单条数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPESTDataJson()
        {
            string PESTCODE = Request.Params["PESTCODE"];
            T_SYS_PEST_Model m = T_SYS_PESTCls.getModel(new T_SYS_PEST_SW { PESTCODE = PESTCODE });
            return Content(JsonConvert.SerializeObject(m), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 病虫害管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult PESTManager()
        {
            T_SYS_PEST_Model m = new T_SYS_PEST_Model();
            m.PESTCODE = Request.Params["PESTCODE"];
            m.PESTNAME = Request.Params["PESTNAME"];
            m.LATINNAME = Request.Params["LATINNAME"];
            m.ORDERBY = Request.Params["ORDERBY"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(T_SYS_PESTCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 有害生物数据采集
        /// <summary>
        /// 有害生物采集
        /// </summary>
        /// <returns></returns>
        public ActionResult PESTCollectList()
        {
            pubViewBag("023001", "023001", "有害生物采集");
            if (ViewBag.isPageRight == false)
                return View();
            string Page = string.IsNullOrEmpty(Request.Params["Page"]) ? "1" : Request.Params["Page"];//当前页数
            string trans = Request.Params["trans"];//传递网页参数 
            string[] arr = new string[6];//存放查询条件的数组 根据实际存放的数据
            if (!string.IsNullOrEmpty(trans))
                arr = ClsStr.DecryptA01(trans, "kkkkkkkk").Split('|');
            if (string.IsNullOrEmpty(arr[0]))
                arr[0] = PagerCls.getDefaultPageSize().ToString();
            if (string.IsNullOrEmpty(arr[1]))
                arr[1] = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(arr[2]))
                arr[2] = DateTime.Now.ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(arr[5]))
                arr[5] = SystemCls.getCurUserOrgNo();
            ViewBag.StartTime = arr[1];
            ViewBag.EndTime = arr[2];
            ViewBag.VILLAGENAME = arr[3];
            ViewBag.SMALLADDRESS = arr[4];
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = arr[5] });
            ViewBag.TREES = T_SYS_TREESPECIESCls.getSelectOption(new T_SYS_TREESPECIES_SW());
            ViewBag.PESTTYPE = T_SYS_PESTCls.getSelectOption(new T_SYS_PEST_SW { IsGetTopCode = true });
            ViewBag.HARMPOSITION = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "102" });
            ViewBag.HARMLEVEL = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "103" });
            ViewBag.MANSTATE = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "104" });
            int total = 0;
            ViewBag.TableInfo = GetPESTCollectStr(new PEST_COLLECTDATA_SW { CurPage = int.Parse(Page), PageSize = int.Parse(arr[0]), StartTime = arr[1], EndTime = arr[2], VILLAGENAME = arr[3], SMALLADDRESS = arr[4], BYORGNO = arr[5] }, out total);
            ViewBag.PageInfo = PagerCls.getPagerInfo_New(new PagerSW { curPage = int.Parse(Page), pageSize = int.Parse(arr[0]), rowCount = total, url = "/PEST/PESTCollectList?trans=" + trans });
            ViewBag.Add = (SystemCls.isRight("023001001")) ? 1 : 0;
            ViewBag.Del = (SystemCls.isRight("023001004")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 加载病虫名称
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadCOLLECTPEST()
        {
            string PESTCODE = Request.Params["PESTCODE"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(T_SYS_PESTCls.getSelectOption(new T_SYS_PEST_SW { GetAllChileCode = true, PESTCODE = PESTCODE }));
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 有害生物采集列表——查询时
        /// </summary>
        /// <returns></returns>
        public ActionResult PESTCollectListQuery()
        {
            string Page = Request.Params["Page"];
            string PageSize = string.IsNullOrEmpty(Request.Params["PageSize"]) ? PagerCls.getDefaultPageSize().ToString() : Request.Params["PageSize"];
            string StartTime = Request.Params["StartTime"];
            string EndTime = Request.Params["EndTime"];
            string VILLAGENAME = Request.Params["VILLAGENAME"];
            string SMALLADDRESS = Request.Params["SMALLADDRESS"];
            string BYORGNO = Request.Params["BYORGNO"];
            string str = ClsStr.EncryptA01(PageSize + "|" + StartTime + "|" + EndTime + "|" + VILLAGENAME + "|" + SMALLADDRESS + "|" + BYORGNO, "kkkkkkkk");
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/PEST/PESTCollectList?trans=" + str + "&Page=" + Page)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取有害生物采集列表
        /// </summary>
        /// <returns></returns>
        private string GetPESTCollectStr(PEST_COLLECTDATA_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            List<PEST_COLLECTDATA_Page_Model> result = PEST_COLLECTDATACls.getModeList(sw, out total).ToList();

            #region 数据表
            sb.AppendFormat("<table id=\"CollectTable\" cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            string dis = result.Count <= 0 ? "disabled=\"disabled\"" : "";
            sb.AppendFormat("<th style=\"width:5%\"><input id=\"tbxCollectALL\" name=\"tbxCollectALL\" type=\"checkbox\" class=\"ace\" value=\"ALL\" onclick=\"SelectAll(this.value,this.checked)\" {0} /></th>", dis);
            sb.AppendFormat("<th>序号</th><th>单位</th><th>村名</th><th>小地名</th><th>小班号</th><th>小班面积</th><th>寄主树种</th><th>病虫名称</th><th>上传时间</th><th>处理状态</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"SetColor(this)\" >", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxCollect" + i + "\" name=\"tbxCollect\"  type=\"checkbox\" class=\"ace\" value=\"" + v.PESTCOLLDATAID + "\" onclick=\"SelectAll(this.value,this.checked)\" />");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\" style=\"{1}\">{0}</td>", v.BYORGNONAME, PublicCls.getOrgTDNameClass(sw.BYORGNO, v.BYORGNO));
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.VILLAGENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.SMALLADDRESS);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.SMALLCLASSCODE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.SMALLCLASSAREA);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.HOSTTREESPECIESNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.COLLECTPESTNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.UPLOADTIME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MANSTATE);
                sb.AppendFormat("<td>");
                if (SystemCls.isRight("023001005"))
                {
                    if (string.IsNullOrEmpty(v.JWDLIST))
                        sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                    else
                        sb.AppendFormat("<a href=\"#\" onclick=\"PositionLine('PEST_COLLECTDATA','{0}','{1}',2)\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", v.PESTCOLLDATAID, v.COLLECTPESTNAME);
                }
                if (SystemCls.isRight("023001003"))
                    sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.PESTCOLLDATAID);
                if (SystemCls.isRight("023001006"))
                    sb.AppendFormat("<a href=\"#\" onclick=\"Photo('{0}')\" title='图片' class=\"searchBox_01 LinkPhoto\">图片</a>", v.PESTCOLLDATAID);
                if (SystemCls.isRight("023001002"))
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}')\" title='修改' class=\"searchBox_01 LinkMdy\">修改</a>", v.PESTCOLLDATAID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion

            return sb.ToString();
        }

        /// <summary>
        /// 获取单条有害生物数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPESTCollectDataJson()
        {
            string PESTCOLLDATAID = Request.Params["PESTCOLLDATAID"];
            return Content(JsonConvert.SerializeObject(PEST_COLLECTDATACls.getModel(new PEST_COLLECTDATA_SW { PESTCOLLDATAID = PESTCOLLDATAID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 有害生物数据查看
        /// </summary>
        /// <returns></returns>
        public ActionResult PESTCollectDataSee()
        {
            string PESTCOLLDATAID = Request.Params["DataId"];
            PEST_COLLECTDATA_Model m = PEST_COLLECTDATACls.getModel(new PEST_COLLECTDATA_SW { PESTCOLLDATAID = PESTCOLLDATAID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td style=\"width:15%\">单位:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.BYORGNONAME);
            sb.AppendFormat("<td style=\"width:15%\">寄主树种</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.HOSTTREESPECIESNAME);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td >调查类型:</td>");
            sb.AppendFormat("<td >{0}</td>", m.SEARCHTYPENAME);
            sb.AppendFormat("<td >病虫名称:</td>");
            sb.AppendFormat("<td >{0}</td>", m.COLLECTPESTNAME);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>村名:</td>");
            sb.AppendFormat("<td>{0}</td>", m.VILLAGENAME);
            sb.AppendFormat("<td>小地名:</td>");
            sb.AppendFormat("<td>{0}</td>", m.SMALLADDRESS);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>小班号:</td>");
            sb.AppendFormat("<td>{0}</td>", m.SMALLCLASSCODE);
            sb.AppendFormat("<td>小班面积:</td>");
            sb.AppendFormat("<td>{0}</td>", !string.IsNullOrEmpty(m.SMALLCLASSAREA) ? m.SMALLCLASSAREA + "公顷" : m.SMALLCLASSAREA);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>经纬度集合:</td>");
            sb.AppendFormat("<td colspan=\"3\"></td>", m.JWDLIST); ;
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>疑似病死:</td>");
            sb.AppendFormat("<td>{0}</td>", !string.IsNullOrEmpty(m.DEADCOUNT) ? m.DEADCOUNT + "株" : m.DEADCOUNT);
            sb.AppendFormat("<td>不明枯死:</td>");
            sb.AppendFormat("<td>{0}</td>", !string.IsNullOrEmpty(m.UNKNOWNDIEOFFCOUNT) ? m.UNKNOWNDIEOFFCOUNT + "株" : m.UNKNOWNDIEOFFCOUNT);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>其他枯死:</td>");
            sb.AppendFormat("<td>{0}</td>", !string.IsNullOrEmpty(m.ELSEDIEOFFCOUNT) ? m.ELSEDIEOFFCOUNT + "株" : m.ELSEDIEOFFCOUNT);
            sb.AppendFormat("<td>取&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;样:</td>");
            sb.AppendFormat("<td>{0}</td>", !string.IsNullOrEmpty(m.SAMPLECOUNT) ? m.SAMPLECOUNT + "株" : m.SAMPLECOUNT);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>处理状态:</td>");
            sb.AppendFormat("<td >{0}</td>", m.MANSTATENAME);
            sb.AppendFormat("<td>反馈结果:</td>");
            sb.AppendFormat("<td >{0}</td>", m.MANRESULT);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>备注:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK); ;
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tbody>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 有害生物数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult PESTCollectDataManager()
        {
            #region  有害生物_采集数据模型
            PEST_COLLECTDATA_Model m = new PEST_COLLECTDATA_Model();
            CookieModel cookie = SystemCls.getCookieInfo();
            m.COLLECTRESOURCE = "2";
            m.PESTCOLLDATAID = Request.Params["PESTCOLLDATAID"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.HOSTTREESPECIESCODE = Request.Params["HOSTTREESPECIESCODE"];
            m.SEARCHTYPE = Request.Params["SEARCHTYPE"];
            m.COLLECTPESTCODE = Request.Params["COLLECTPESTCODE"];
            m.VILLAGENAME = Request.Params["VILLAGENAME"];
            m.SMALLADDRESS = Request.Params["SMALLADDRESS"];
            m.SMALLCLASSCODE = Request.Params["SMALLCLASSCODE"];
            m.SMALLCLASSAREA = Request.Params["SMALLCLASSAREA"];
            m.JWDLIST = Request.Params["JWDLIST"];
            m.KID = !string.IsNullOrEmpty(Request.Params["KID"]) ? Request.Params["KID"] : (BINGCHONGHAICls.GetMaxOBJECTID() + 1).ToString();
            m.HARMPOSITION = Request.Params["HARMPOSITION"];
            m.HARMLEVEL = Request.Params["HARMLEVEL"];
            m.DEADCOUNT = Request.Params["DEADCOUNT"];
            m.UNKNOWNDIEOFFCOUNT = Request.Params["UNKNOWNDIEOFFCOUNT"];
            m.ELSEDIEOFFCOUNT = Request.Params["ELSEDIEOFFCOUNT"];
            m.SAMPLECOUNT = Request.Params["SAMPLECOUNT"];
            m.MANSTATE = Request.Params["MANSTATE"];
            m.MANRESULT = Request.Params["MANRESULT"];
            m.MARK = Request.Params["MARK"];
            m.UPLOADTIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            m.MANTIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            m.MANUSERID = cookie.UID;
            m.opMethod = Request.Params["Method"];
            var ms = PEST_COLLECTDATACls.Manager(m);
            #endregion

            #region 三维-病虫害
            BINGCHONGHAI_Model bm = new BINGCHONGHAI_Model();
            bm.NAME = m.COLLECTPESTCODE;
            bm.category = m.SEARCHTYPE;
            bm.opMethod = m.opMethod;
            string polygon = "";
            string polygon1 = "";
            string jd = "";
            string wd = "";
            string dx = "";
            string dy = "";
            if (!string.IsNullOrEmpty(m.JWDLIST))
            {
                string[] arr1 = m.JWDLIST.Split(';');
                for (int j = 0; j < arr1.Length - 1; j++)
                {
                    string[] arr = arr1[j].Split('|');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (string.IsNullOrEmpty(arr[i]) == false)
                        {
                            string[] brr = arr[i].Split(',');
                            double[] drr = ClsPositionTrans.GpsTransform(double.Parse(brr[1]), double.Parse(brr[0]), ConfigCls.getSDELonLatTransform());
                            wd = drr[0].ToString();
                            jd = drr[1].ToString();
                        }
                        dx += jd + ",";
                        dy += wd + ",";
                        if (i == arr.Length - 1)//最后一条记录
                        {
                            polygon += jd + " " + wd + "|";
                        }
                        else
                        {
                            polygon += jd + " " + wd + ",";
                        }
                    }
                    string[] arr2 = polygon.Split('|');
                    if (j == arr1.Length - 1)//最后一条记录
                    {
                        polygon1 += "";
                    }
                    else
                    {
                        polygon1 += "(" + arr2[j].ToString() + "),";
                    }
                }
                string[] crr = dx.Split(',');
                string[] jrr = dy.Split(',');
                var jdmax = crr.Where(p => p != "").Select(p => float.Parse(p)).Max();
                var jdmin = crr.Where(p => p != "").Select(p => float.Parse(p)).Min();
                var wdmax = jrr.Where(p => p != "").Select(p => float.Parse(p)).Max();
                var wdmin = jrr.Where(p => p != "").Select(p => float.Parse(p)).Min();
                bm.DISPLAY_X = ((jdmax + jdmin) / 2).ToString();
                bm.DISPLAY_Y = ((wdmax + wdmin) / 2).ToString();
            }
            if (polygon1 != "")
            {
                polygon1 = polygon1.Substring(0, polygon1.LastIndexOf(","));
            }
            bm.Shape = "geometry::STGeomFromText('MULTIPolygon((" + polygon1 + "))',4326).MakeValid()";
            if (m.opMethod != "Del")
            {
                bm.OBJECTID = ms.Url;
            }
            else
            {
                bm.OBJECTID = m.PESTCOLLDATAID;
            }
            if (string.IsNullOrEmpty(m.JWDLIST) && bm.opMethod == "Add")
            {

            }
            else
            {
                BINGCHONGHAICls.Manager(bm);
            }
            #endregion

            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }

        #region 图片管理
        /// <summary>
        /// 图片View
        /// </summary>
        /// <returns></returns>
        public ActionResult PhotoIndex()
        {
            string DataId = Request.Params["DataId"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr><th>序号</th><th>照片标题</th><th>照片说明</th><th>缩略图</th><th>操作</th></tr></thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = PEST_COLLECT_DATAUPLOADCls.getModelList(new PEST_COLLECT_DATAUPLOAD_SW { PESTCOLLDATAID = DataId });
            foreach (var s in result)
            {
                sb.AppendFormat("<tr class=\"{6}\" onclick=\"PHOnclik('{0}','{1}','{2}','{3}','{4}','{5}')\">", s.PESTCOLLDATAUPLOADID, s.PESTCOLLDATAID, s.UPLOADNAME, s.UPLOADDESCRIBE, s.UPLOADURL, s.UPLOADTYPE, (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.UPLOADNAME, s.UPLOADURL);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.UPLOADDESCRIBE);
                sb.AppendFormat("<td class=\"center\"><a href=\"{0}\" target=\"_blank\"><img src=\"{0}\" alt=\"alttext\" title=\"{1}\" height =\"35px\" wideth=\"35px\"/></a></td>", s.UPLOADURL, s.UPLOADNAME);
                sb.AppendFormat("<td class=\"center\"></td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
            sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"UPLOADNAME\" type=\"text\"   style=\"width:98%;\" />");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"UPLOADDESCRIBE\" type=\"text\"  style=\"width:98%;\" />");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td>");
            sb.AppendFormat("{0}", "<input id=\"PESTCOLLDATAUPLOADID\" type=\"hidden\" />");
            sb.AppendFormat("{0}", "<input id=\"PESTCOLLDATAID\" type=\"hidden\"  value=\"" + DataId + "\" />");
            sb.AppendFormat("{0}", "<input id=\"UPLOADURL\" type=\"hidden\" />");
            sb.AppendFormat("{0}", "<input id=\"UPLOADTYPE\" type=\"hidden\" />");
            sb.AppendFormat("{0}", " <form id=\"uploadForm\" enctype=\"multipart/form-data\">");
            sb.AppendFormat("{0}", " <input type=\"file\" name=\"uploadify\" id=\"attachment\" style=\"width:180px;\" />");
            sb.AppendFormat("{0}", "<input type=\"button\" id=\"btnPhotoupload\" value=\"新增\" onclick=\"UploadPhoto('Add')\" class=\"btnAddCss\" />");
            sb.AppendFormat("{0}", "<input type=\"button\" id=\"btnPhotoMdy\" value=\"修改\" style=\"display:none;\"  onclick=\"UploadPhoto('Mdy')\" class=\"btnMdyCss\" />");
            sb.AppendFormat("{0}", "<label id=\"lblInfo\" style=\"color:red;\"></label>");
            sb.AppendFormat("{0}", "<input type=\"button\" id=\"btnPhotoDel\" value=\"删除\" style=\"display:none;\"  onclick=\"ManagerPhoto('Del')\" class=\"btnDelCss\" />");
            sb.AppendFormat("{0}", " </form>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            ViewBag.Photo = sb.ToString();
            return View();
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadPhoto()
        {
            string PESTCOLLDATAUPLOADID = Request.Params["PESTCOLLDATAUPLOADID"];
            string PESTCOLLDATAID = Request.Params["PESTCOLLDATAID"];
            string UPLOADNAME = Request.Params["UPLOADNAME"];
            string UPLOADDESCRIBE = Request.Params["UPLOADDESCRIBE"];
            string method = Request.Params["Method"];
            Message ms = null;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string Path = "";
            string[] arr = hfc[0].FileName.Split('.');
            string type = arr[arr.Length - 1].ToLower();
            if (method == "Mdy" && string.IsNullOrEmpty(hfc[0].FileName))
            {
                PEST_COLLECT_DATAUPLOAD_Model m = new PEST_COLLECT_DATAUPLOAD_Model();
                m.opMethod = "MdyTP";
                m.PESTCOLLDATAUPLOADID = PESTCOLLDATAUPLOADID;
                m.PESTCOLLDATAID = PESTCOLLDATAID;
                m.UPLOADNAME = UPLOADNAME;
                m.UPLOADDESCRIBE = UPLOADDESCRIBE;
                m.UPLOADTYPE = "1";
                ms = PEST_COLLECT_DATAUPLOADCls.Manager(m);
            }
            else
            {
                if (string.IsNullOrEmpty(UPLOADNAME))
                    return Json(new Message(false, "请输入上传文件名!", ""));
                if (string.IsNullOrEmpty(hfc[0].FileName))
                    return Json(new Message(false, "请选择上传图片!", ""));
                if (type != "jpg" && type != "jpeg" && type != "bmp" && type != "gif" && type != "png")
                    return Json(new Message(false, "禁止上传非图片文件!", ""));
                if (hfc.Count > 0)
                {
                    string ipath = "~/UploadFile/PESTPhoto/";//相对路径
                    string PhyPath = Server.MapPath(ipath);
                    if (!Directory.Exists(PhyPath))//判断文件夹是否已经存在
                        Directory.CreateDirectory(PhyPath);//创建文件夹
                    PEST_COLLECT_DATAUPLOAD_Model m = new PEST_COLLECT_DATAUPLOAD_Model();
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        m.PESTCOLLDATAUPLOADID = PESTCOLLDATAUPLOADID;
                        m.PESTCOLLDATAID = PESTCOLLDATAID;
                        m.UPLOADNAME = UPLOADNAME;
                        m.UPLOADDESCRIBE = UPLOADDESCRIBE;
                        m.UPLOADTYPE = "1";
                        Path = "/UploadFile/PESTPhoto/" + hfc[i].FileName;
                        m.UPLOADURL = Path;
                        string PhysicalPath = Server.MapPath(Path);
                        hfc[i].SaveAs(PhysicalPath);
                        m.opMethod = method;
                    }
                    ms = PEST_COLLECT_DATAUPLOADCls.Manager(m);
                }
            }
            return Json(ms);
        }

        /// <summary>
        /// 图片管理
        /// </summary>
        /// <returns></returns>
        public ActionResult PhotoManager()
        {
            PEST_COLLECT_DATAUPLOAD_Model m = new PEST_COLLECT_DATAUPLOAD_Model();
            m.PESTCOLLDATAUPLOADID = Request.Params["PESTCOLLDATAUPLOADID"];
            m.PESTCOLLDATAID = Request.Params["PESTCOLLDATAID"];
            m.UPLOADNAME = Request.Params["UPLOADNAME"];
            m.UPLOADDESCRIBE = Request.Params["UPLOADDESCRIBE"];
            m.UPLOADURL = Request.Params["UPLOADURL"];
            m.opMethod = Request.Params["Method"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod == "Del")
            {
                string file = Server.MapPath(m.UPLOADURL);
                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);
            }
            else
            {
                if (string.IsNullOrEmpty(m.UPLOADNAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入照片名称!", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(PEST_COLLECT_DATAUPLOADCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #endregion

        #region 有害生物树种关联
        /// <summary>
        /// 树种有害生物关联
        /// </summary>
        /// <returns></returns>
        public ActionResult SZYHSWGL()
        {
            pubViewBag("006022", "006022", "有害生物树种关联");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.PEST = T_SYS_PESTCls.getSelectOption(new T_SYS_PEST_SW());
            ViewBag.Save = (SystemCls.isRight("006022001")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 有害生物树种关联--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SZYHSWGLQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string PESTCODE = Request.Params["PESTCODE"];
            #endregion

            #region 数据准备
            string PESTNAME = T_SYS_PESTCls.getName(PESTCODE);
            List<T_SYS_TREESPECIES_Model> _list = T_SYS_TREESPECIESCls.getListModel(new T_SYS_TREESPECIES_SW { }).ToList();
            List<T_SYS_TREESPECIES_PEST_Model> _templist = T_SYS_TREESPECIES_PESTCls.getListModel(new T_SYS_TREESPECIES_PEST_SW { PESTCODE = PESTCODE }).ToList();
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"SZYHSWGLTable\"  cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr>");
            if (_list.Count <= 0)
                sb.AppendFormat("<th style=\"width:5%\"><input id=\"tbxPESTALL\" name=\"tbxPESTALL\" type=\"checkbox\" class=\"ace\" value=\"ALL\" onclick=\"SelectAll(this.value,this.checked)\" disabled=\"disabled\" /></th>");
            else
                sb.AppendFormat("<th style=\"width:5%\"><input id=\"tbxPESTALL\" name=\"tbxPESTALL\" type=\"checkbox\" class=\"ace\" value=\"ALL\" onclick=\"SelectAll(this.value,this.checked)\" /></th>");
            sb.AppendFormat("<th style=\"width:15%;\">序号</th><th style=\"width:40%;\">有害生物</th><th style=\"width:40%;\">树种名称</th></tr></thead>");
            sb.AppendFormat("<tbody>");
            for (int i = 0; i < _list.Count; i++)
            {
                string chk = _templist.Find(a => a.TSPCODE == _list[i].TSPCODE) != null ? "checked" : "";
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td><input id=\"tbxPEST" + i + "\" name=\"tbxPEST\"  type=\"checkbox\" class=\"ace\" value=\"" + _list[i].TSPCODE + "\" onclick=\"SelectAll(this.value,this.checked)\" {0} /></td>", chk);
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", PESTNAME);
                sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", _list[i].TSPNAME, PublicCls.getTSPNameClass(_list[i].TSPCODE));
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 有害生物树种关联--数据增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult SZYHSWGLManager()
        {
            T_SYS_TREESPECIES_PEST_Model m = new T_SYS_TREESPECIES_PEST_Model();
            m.PESTCODE = Request.Params["PESTCODE"];
            m.TSPCODE = Request.Params["TSPCODE"];
            return Content(JsonConvert.SerializeObject(T_SYS_TREESPECIES_PESTCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 应施面积
        /// <summary>
        /// 应施面积
        /// </summary>
        /// <returns></returns>
        public ActionResult PESTOBSERVEAREAList()
        {
            pubViewBag("006023", "006023", "应施面积");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.Time = DateTime.Now.ToString("yyyy");
            ViewBag.Save = (SystemCls.isRight("006023001")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 应施面积列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult PESTOBSERVEAREAListQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string ORGNO = Request.Params["ORGNO"];
            string YEAR = Request.Params["YEAR"];
            #endregion

            #region 数据准备
            List<T_SYS_ORGModel> result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { TopORGNO = ORGNO }).ToList();
            List<T_SYS_PEST_OBSERVEAREA_Model> templist = T_SYS_PEST_OBSERVEAREACls.getListModel(new T_SYS_PEST_OBSERVEAREA_SW()).ToList();
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"AreaTable\"  cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr><th style=\"width:10%;\">序号</th><th style=\"width:25%;\">年份</th><th style=\"width:40%;\">单位名称</th><th style=\"width:25%;\">应施面积</br>(" + dic113Name + ")</th></tr></thead>");
            int i = 0;
            foreach (var v in result)
            {
                T_SYS_PEST_OBSERVEAREA_Model m = templist.Find(a => a.BYORGNO == v.ORGNO && a.OBSERVEYEAR == YEAR);
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", YEAR);
                sb.AppendFormat("<td class=\"left\" style=\"{2}\" >{0}{1}</td>", v.ORGNAME, "<input id=\"txtORGNO" + i + "\" type=\"hidden\" value=\"" + v.ORGNO + "\" />", PublicCls.getOrgTDNameClass(ORGNO, v.ORGNO));
                sb.AppendFormat("<td><input id=\"txtAREA" + i + "\" type=\"text\" value=\"{0}\" class=\"center\" style=\"width:99%;\" /></td>", (m != null && !string.IsNullOrEmpty(m.OBSERVEAREA)) ? string.Format("{0:0.00}", float.Parse(m.OBSERVEAREA)) : "");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 应施面积数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult PESTOBSERVEAREAManager()
        {
            T_SYS_PEST_OBSERVEAREA_Model m = new T_SYS_PEST_OBSERVEAREA_Model();
            m.TopORGNO = Request.Params["TopORGNO"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.OBSERVEAREA = Request.Params["OBSERVEAREA"];
            m.OBSERVEYEAR = Request.Params["OBSERVEYEAR"];
            return Content(JsonConvert.SerializeObject(T_SYS_PEST_OBSERVEAREACls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 发生报表
        /// <summary>
        /// 发生报表
        /// </summary>
        /// <returns></returns>
        public ActionResult HAPPENREPORT()
        {
            pubViewBag("024005", "024005", "发生报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.PEST = T_SYS_PESTCls.getSelectOption(new T_SYS_PEST_SW());
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM");
            List<T_SYS_DICTModel> dic105list = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "105" }).ToList();
            List<T_SYS_DICTModel> dic106List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "106" }).ToList();
            ViewBag.dic105Value = T_SYS_DICTCls.getDicValueStr(dic105list);
            ViewBag.dic106Value = T_SYS_DICTCls.getDicValueStr(dic106List);
            ViewBag.dic105Count = dic105list.Count;
            ViewBag.dic106Count = dic106List.Count;
            ViewBag.Save = (SystemCls.isRight("024005001")) ? 1 : 0;
            ViewBag.Export = (SystemCls.isRight("024005002")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 发生报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult HAPPENREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string PESTBYCODE = Request.Params["PESTCODE"];
            string Time = Request.Params["Time"];
            string[] sTime;
            List<T_SYS_ORGModel> result;
            List<T_SYS_DICTModel> dic105;
            List<T_SYS_DICTModel> dic106;
            List<T_SYS_PEST_OBSERVEAREA_Model> templist;
            List<PEST_REPORT_HAPPEN_Model> list;
            HAPPENREPORTData(BYORGNO, PESTBYCODE, Time, out sTime, out result, out dic105, out dic106, out templist, out list);
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"HappenTable\"  cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"2\" style=\"width:4%;\" >序号</th><th rowspan=\"2\" style=\"width:14%;\" >单位名称</th><th rowspan=\"2\" style=\"width:10%;\">应施面积</br>(" + dic113Name + ")</th>");
            foreach (var d1 in dic105)
            {
                string title = d1.DICTNAME;
                if (!string.IsNullOrEmpty(d1.STANDBY1))
                    title += "</br>(" + d1.STANDBY1 + ")";
                sb.AppendFormat("<th colspan=\"" + (dic106.Count + 1) + "\"  >{0}</th>", title);
            }
            sb.AppendFormat("<th rowspan=\"2\" style=\"width:8%;\">无</br>(" + dic113Name + ")</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            foreach (var d2 in dic105)
            {
                sb.AppendFormat("<th style=\"width:40px;\">合计</th>");
                foreach (var d3 in dic106)
                {
                    string title = d3.DICTNAME;
                    if (!string.IsNullOrEmpty(d3.STANDBY1))
                        title += "</br>(" + d3.STANDBY1 + ")";
                    sb.AppendFormat("<th style=\"width:30px;\">{0}</th>", title);
                }
            }
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                T_SYS_PEST_OBSERVEAREA_Model m = templist.Find(a => a.BYORGNO == v.ORGNO && a.OBSERVEYEAR == sTime[0]);
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"left\" style=\"{2}\" >{0}{1}</td>", v.ORGNAME, "<input id=\"txtORGNO" + i + "\" type=\"hidden\" value=\"" + v.ORGNO + "\" />", PublicCls.getOrgTDNameClass(BYORGNO, v.ORGNO));
                sb.AppendFormat("<td class=\"center\" >{0}</td>", (m != null && !string.IsNullOrEmpty(m.OBSERVEAREA)) ? string.Format("{0:0.00}", float.Parse(m.OBSERVEAREA)) : "");
                for (int d2 = 0; d2 < dic105.Count; d2++)
                {
                    List<string> area = PEST_REPORT_HAPPENCls.GetDetailArea(v.ORGNO, PESTBYCODE, dic105[d2].DICTVALUE, dic106, list);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", (!string.IsNullOrEmpty(area[area.Count - 1]) ? string.Format("{0:0.00}", float.Parse(area[area.Count - 1])) : ""));
                    for (int d3 = 0; d3 < dic106.Count; d3++)
                    {
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"txt" + (i.ToString() + d2.ToString() + d3.ToString()) + "\" type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + (!string.IsNullOrEmpty(area[d3]) ? string.Format("{0:0.00}", float.Parse(area[d3])) : "") + "\" >");
                    }
                }
                if (m != null && !string.IsNullOrEmpty(m.OBSERVEAREA))
                {
                    float NoArea = PEST_REPORT_HAPPENCls.GetNoArea(m.OBSERVEAREA, v.ORGNO, "", dic105, dic106, list);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", string.Format("{0:0.00}", NoArea));
                }
                else
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 发生报表数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult HAPPENREPORTManager()
        {
            PEST_REPORT_HAPPEN_Model m = new PEST_REPORT_HAPPEN_Model();
            m.TopORGNO = Request.Params["TopORGNO"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.HAPPENYEAR = Request.Params["HAPPENYEAR"];
            m.HAPPENMONTH = Request.Params["HAPPENMONTH"];
            m.PESTBYCODE = Request.Params["PESTBYCODE"];
            m.HARMTYPEID = Request.Params["HARMTYPEID"];
            m.HARMLEVELCODE = Request.Params["HARMLEVELCODE"];
            m.HAPPENAREA = Request.Params["HAPPENAREA"];
            return Content(JsonConvert.SerializeObject(PEST_REPORT_HAPPENCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult HAPPENREPORTExportExcel()
        {
            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string PESTBYCODE = Request.Params["PESTCODE"];
            string Time = Request.Params["Time"];
            string[] sTime;
            List<T_SYS_ORGModel> result;
            List<T_SYS_DICTModel> dic105;
            List<T_SYS_DICTModel> dic106;
            List<T_SYS_PEST_OBSERVEAREA_Model> templist;
            List<PEST_REPORT_HAPPEN_Model> list;
            HAPPENREPORTData(BYORGNO, PESTBYCODE, Time, out sTime, out result, out dic105, out dic106, out templist, out list);
            int colsCount = (dic106.Count + 1) * dic105.Count + 3;
            string orgName = T_SYS_ORGCls.getorgname(BYORGNO);
            string pestName = T_SYS_PESTCls.getName(PESTBYCODE);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "024005", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + pestName + "-" + Time + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            for (int i = 0; i < colsCount; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 30 * 256);
                else
                    sheet1.SetColumnWidth(i, 15 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.Height = 2 * 350;
            rowHead1.CreateCell(0).SetCellValue("单位名称");
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(1).SetCellValue("应施面积\n(" + dic113Name + ")");
            rowHead1.GetCell(1).CellStyle = getCellStyleHead(book);
            int x = 2;
            for (int i = 0; i < dic105.Count; i++)
            {
                string name = dic105[i].DICTNAME;
                if (!string.IsNullOrEmpty(dic105[i].STANDBY1))
                    name += "\n(" + dic105[i].STANDBY1 + ")";
                rowHead1.CreateCell(x).SetCellValue(name);
                rowHead1.GetCell(x).CellStyle = getCellStyleHead(book);
                x = x + dic106.Count + 1;
            }
            rowHead1.CreateCell(x).SetCellValue("无\n(" + dic113Name + ")");
            rowHead1.GetCell(x).CellStyle = getCellStyleHead(book);
            IRow rowHead2 = sheet1.CreateRow(2);
            int y = 2;
            foreach (var d1 in dic105)
            {
                rowHead2.CreateCell(y).SetCellValue("合计");
                rowHead2.GetCell(y).CellStyle = getCellStyleHead(book);
                y++;
                foreach (var d2 in dic106)
                {
                    string name = d2.DICTNAME;
                    if (!string.IsNullOrEmpty(d2.STANDBY1))
                        name += "\n(" + d2.STANDBY1 + ")";
                    rowHead2.CreateCell(y).SetCellValue(name);
                    rowHead2.GetCell(y).CellStyle = getCellStyleHead(book);
                    y++;
                }
            }
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount - 1));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 1, 1));
            for (int i = 0; i < dic105.Count; i++)
            {
                int index = 0;
                if (i == 0)
                    index = i * (dic106.Count) + 2;
                else
                    index = i * (dic106.Count) + 3;
                sheet1.AddMergedRegion(new CellRangeAddress(1, 1, index, index + dic106.Count));
            }
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, colsCount - 1, colsCount - 1));
            #endregion

            #region 表身及数据
            int rowIndex = 0;
            foreach (var v in result)
            {
                T_SYS_PEST_OBSERVEAREA_Model m = templist.Find(a => a.BYORGNO == v.ORGNO && a.OBSERVEYEAR == sTime[0]);
                string OBSERVEAREA = (m != null && !string.IsNullOrEmpty(m.OBSERVEAREA)) ? string.Format("{0:0.00}", float.Parse(m.OBSERVEAREA)) : "";
                if (PublicCls.OrgIsShi(v.ORGNO)) { }
                else if (PublicCls.OrgIsXian(v.ORGNO)) { v.ORGNAME = "　　" + v.ORGNAME; }
                else { v.ORGNAME = "　　　　" + v.ORGNAME; }
                int j = 0;
                IRow row = sheet1.CreateRow(rowIndex + 3);
                row.CreateCell(j).SetCellValue(v.ORGNAME);
                row.GetCell(j).CellStyle = getCellStyleLeft(book);
                j++;
                row.CreateCell(j).SetCellValue(OBSERVEAREA);
                row.GetCell(j).CellStyle = getCellStyleCenter(book);
                j++;
                for (int d1 = 0; d1 < dic105.Count; d1++)
                {
                    List<string> areaList = PEST_REPORT_HAPPENCls.GetDetailArea(v.ORGNO, PESTBYCODE, dic105[d1].DICTVALUE, dic106, list);
                    string hjArea = (!string.IsNullOrEmpty(areaList[areaList.Count - 1]) ? string.Format("{0:0.00}", float.Parse(areaList[areaList.Count - 1])) : "");
                    for (int d2 = 0; d2 < dic106.Count; d2++)
                    {
                        string area = (!string.IsNullOrEmpty(areaList[d2]) ? string.Format("{0:0.00}", float.Parse(areaList[d2])) : "");
                        row.CreateCell(j).SetCellValue(area);
                        row.GetCell(j).CellStyle = getCellStyleCenter(book);
                        j++;
                    }
                    j++;
                }
                if (OBSERVEAREA != "")
                {
                    float NoArea = PEST_REPORT_HAPPENCls.GetNoArea(m.OBSERVEAREA, v.ORGNO, "", dic105, dic106, list);
                    row.CreateCell(j).SetCellValue(string.Format("{0:0.00}", NoArea));
                    row.GetCell(j).CellStyle = getCellStyleCenter(book);
                }
                else
                {
                    row.CreateCell(j).SetCellValue("");
                    row.GetCell(j).CellStyle = getCellStyleCenter(book);
                }
                rowIndex++;
            }
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

        #region 防治报表
        /// <summary>
        /// 防治报表
        /// </summary>
        /// <returns></returns>
        public ActionResult CONTROLREPORT()
        {
            pubViewBag("024006", "024006", "防治报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.PEST = T_SYS_PESTCls.getSelectOption(new T_SYS_PEST_SW());
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM");
            List<T_SYS_DICTModel> dic107List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "107" }).ToList();
            ViewBag.dic107Value = T_SYS_DICTCls.getDicValueStr(dic107List);
            ViewBag.dic107Count = dic107List.Count;
            ViewBag.Save = (SystemCls.isRight("024006001")) ? 1 : 0;
            ViewBag.Export = (SystemCls.isRight("024006002")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 防治报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult CONTROLREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string PESTBYCODE = Request.Params["PESTCODE"];
            string Time = Request.Params["Time"];
            List<T_SYS_ORGModel> result;
            List<PEST_REPORT_HAPPEN_Model> _happenList;
            List<T_SYS_DICTModel> _dic107List;
            List<T_SYS_DICTModel> _dic107WGHList;
            List<T_SYS_DICTModel> _dic107YGHList;
            List<PEST_REPORT_CONTROL_Model> _controlList;
            string pestName;
            CONTROLREPORTData(BYORGNO, PESTBYCODE, Time, out result, out _happenList, out _dic107List, out _dic107WGHList, out _dic107YGHList, out _controlList, out pestName);
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"CONTROLTable\"  cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"3\" style=\"width:5%;\" >序号</th>");
            sb.AppendFormat("<th rowspan=\"3\" style=\"width:15%;\" >单位名称</th>");
            sb.AppendFormat("<th style=\"width:10%;\">发生面积</br>(" + dic113Name + ")</th>");
            sb.AppendFormat("<th colspan=\"" + (_dic107List.Count + 2) + "\" style=\"width:54%;\" >作业面积</br>(" + dic113Name + ")</th>");
            sb.AppendFormat("<th colspan=\"2\" rowspan=\"2\" style=\"width:16%;\" >防治率</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"2\">{0}</th>", pestName);
            sb.AppendFormat("<th rowspan=\"2\">合计</th>");
            sb.AppendFormat("<th colspan=\"" + (_dic107WGHList.Count + 1) + "\">无公害</th>");
            foreach (var YGH in _dic107YGHList)
            {
                string title = YGH.DICTNAME;
                if (!string.IsNullOrEmpty(YGH.STANDBY1))
                    title += "</br>(" + YGH.STANDBY1 + ")";
                sb.AppendFormat("<th rowspan=\"2\">{0}</th>", title);
            }
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>合计</th>");
            foreach (var WGH in _dic107WGHList)
            {
                string title = WGH.DICTNAME;
                if (!string.IsNullOrEmpty(WGH.STANDBY1))
                    title += "</br>(" + WGH.STANDBY1 + ")";
                sb.AppendFormat("<th>{0}</th>", title);
            }
            sb.AppendFormat("<th>无公害防治率</th>");
            sb.AppendFormat("<th>防治率</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                PEST_REPORT_HAPPEN_Model m = _happenList.Find(a => a.BYORGNO == v.ORGNO);
                List<float> _WGHList = PEST_REPORT_CONTROLCls.GetCONTROLDetailArea(v.ORGNO, _dic107WGHList, _controlList);
                List<float> _YGHList = PEST_REPORT_CONTROLCls.GetCONTROLDetailArea(v.ORGNO, _dic107YGHList, _controlList);
                float HJ = _WGHList[_WGHList.Count - 1] + _YGHList[_YGHList.Count - 1];
                float WHGFZL = (m != null && !string.IsNullOrEmpty(m.HAPPENAREA)) ? _WGHList[_WGHList.Count - 1] / float.Parse(m.HAPPENAREA) : 0;
                float FZL = (m != null && !string.IsNullOrEmpty(m.HAPPENAREA)) ? HJ / float.Parse(m.HAPPENAREA) : 0;
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"left\" style=\"{2}\" >{0}{1}</td>", v.ORGNAME, "<input id=\"txtORGNO" + i + "\" type=\"hidden\" value=\"" + v.ORGNO + "\" />", PublicCls.getOrgTDNameClass(BYORGNO, v.ORGNO));
                sb.AppendFormat("<td class=\"center\">{0}</td>", (m != null && !string.IsNullOrEmpty(m.HAPPENAREA)) ? string.Format("{0:0.00}", float.Parse(m.HAPPENAREA)) : "");
                sb.AppendFormat("<td class=\"center\">{0}</td>", HJ > 0 ? string.Format("{0:0.00}", HJ) : "");
                sb.AppendFormat("<td class=\"center\">{0}</td>", _WGHList[_WGHList.Count - 1] > 0 ? string.Format("{0:0.00}", _WGHList[_WGHList.Count - 1]) : "");
                int j = 0;
                for (int x = 0; x < _WGHList.Count - 1; x++)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"txt" + (i.ToString() + j.ToString()) + "\" type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + (_WGHList[x] > 0 ? string.Format("{0:0.00}", _WGHList[x]) : "") + "\" >");
                    j++;
                }

                for (int y = 0; y < _YGHList.Count - 1; y++)
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"txt" + (i.ToString() + j.ToString()) + "\" type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + (_YGHList[y] > 0 ? string.Format("{0:0.00}", _YGHList[y]) : "") + "\" >");
                    j++;
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", WHGFZL > 0 ? string.Format("{0:P}", WHGFZL) : "");
                sb.AppendFormat("<td class=\"center\">{0}</td>", FZL > 0 ? string.Format("{0:P}", FZL) : "");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 防治报表数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult CONTROLREPORTManager()
        {
            PEST_REPORT_CONTROL_Model m = new PEST_REPORT_CONTROL_Model();
            m.TopORGNO = Request.Params["TopORGNO"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.HAPPENYEAR = Request.Params["HAPPENYEAR"];
            m.HAPPENMONTH = Request.Params["HAPPENMONTH"];
            m.PESTBYCODE = Request.Params["PESTBYCODE"];
            m.CONTROLMETHODCODE = Request.Params["CONTROLMETHODCODE"];
            m.CONTROLAREA = Request.Params["CONTROLAREA"];
            return Content(JsonConvert.SerializeObject(PEST_REPORT_CONTROLCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult CONTROLREPORTExportExcel()
        {
            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string PESTBYCODE = Request.Params["PESTCODE"];
            string Time = Request.Params["Time"];
            List<T_SYS_ORGModel> result;
            List<PEST_REPORT_HAPPEN_Model> _happenList;
            List<T_SYS_DICTModel> _dic107List;
            List<T_SYS_DICTModel> _dic107WGHList;
            List<T_SYS_DICTModel> _dic107YGHList;
            List<PEST_REPORT_CONTROL_Model> _controlList;
            string pestName;
            CONTROLREPORTData(BYORGNO, PESTBYCODE, Time, out result, out _happenList, out _dic107List, out _dic107WGHList, out _dic107YGHList, out _controlList, out pestName);
            int colsCount = _dic107List.Count + 6;
            string orgName = T_SYS_ORGCls.getorgname(BYORGNO);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "024006", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + pestName + "-" + Time + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线  

            #region 表头及格式
            for (int i = 0; i < colsCount; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 30 * 256);
                else
                    sheet1.SetColumnWidth(i, 15 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.Height = 2 * 350;
            rowHead1.CreateCell(0).SetCellValue("单位名称");
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(1).SetCellValue("发生面积\n(" + dic113Name + ")");
            rowHead1.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(2).SetCellValue("作业面积\n(" + dic113Name + ")");
            rowHead1.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(colsCount - 2).SetCellValue("防治率");
            rowHead1.GetCell(colsCount - 2).CellStyle = getCellStyleHead(book);
            IRow rowHead2 = sheet1.CreateRow(2);
            rowHead2.CreateCell(1).SetCellValue(pestName);
            rowHead2.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(2).SetCellValue("合计");
            rowHead2.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead2.CreateCell(3).SetCellValue("无公害");
            rowHead2.GetCell(3).CellStyle = getCellStyleHead(book);
            int x = 4 + _dic107WGHList.Count;
            for (int i = 0; i < _dic107YGHList.Count; i++)
            {
                string name = _dic107YGHList[i].DICTNAME;
                if (!string.IsNullOrEmpty(_dic107YGHList[i].STANDBY1))
                    name += "\n(" + _dic107WGHList[i].STANDBY1 + ")";
                rowHead2.CreateCell(x).SetCellValue(name);
                rowHead2.GetCell(x).CellStyle = getCellStyleHead(book);
                x++;
            }
            IRow rowHead3 = sheet1.CreateRow(3);
            rowHead3.CreateCell(3).SetCellValue("合计");
            rowHead3.GetCell(3).CellStyle = getCellStyleHead(book);
            int y = 4;
            for (int i = 0; i < _dic107WGHList.Count; i++)
            {
                string name = _dic107WGHList[i].DICTNAME;
                if (!string.IsNullOrEmpty(_dic107WGHList[i].STANDBY1))
                    name += "\n(" + _dic107WGHList[i].STANDBY1 + ")";
                rowHead3.CreateCell(y).SetCellValue(name);
                rowHead3.GetCell(y).CellStyle = getCellStyleHead(book);
                y++;
            }
            y += 2;
            rowHead3.CreateCell(y).SetCellValue("无公害防治率");
            rowHead3.GetCell(y).CellStyle = getCellStyleHead(book);
            y++;
            rowHead3.CreateCell(y).SetCellValue("防治率");
            rowHead3.GetCell(y).CellStyle = getCellStyleHead(book);
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount - 1));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 2, 3 + _dic107List.Count));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 3, 0, 0));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 1, 1));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 2, 2));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 3, 3 + _dic107WGHList.Count));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 3 + _dic107WGHList.Count + 1, 3 + _dic107WGHList.Count + 1));
            sheet1.AddMergedRegion(new CellRangeAddress(2, 3, 3 + _dic107WGHList.Count + 2, 3 + _dic107WGHList.Count + 2));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, colsCount - 2, colsCount - 1));
            #endregion

            #region 表身及数据
            int rowIndex = 0;
            foreach (var v in result)
            {
                PEST_REPORT_HAPPEN_Model model = _happenList.Find(a => a.BYORGNO == v.ORGNO);
                List<float> _WGHList = PEST_REPORT_CONTROLCls.GetCONTROLDetailArea(v.ORGNO, _dic107WGHList, _controlList);
                List<float> _YGHList = PEST_REPORT_CONTROLCls.GetCONTROLDetailArea(v.ORGNO, _dic107YGHList, _controlList);
                string happenArea = model != null && !string.IsNullOrEmpty(model.HAPPENAREA) ? string.Format("{0:0.00}", float.Parse(model.HAPPENAREA)) : "";
                float HJ = _WGHList[_WGHList.Count - 1] + _YGHList[_YGHList.Count - 1];
                float WHGFZL = happenArea != "" ? _WGHList[_WGHList.Count - 1] / float.Parse(happenArea) : 0;
                float FZL = happenArea != "" ? HJ / float.Parse(happenArea) : 0;
                if (PublicCls.OrgIsShi(v.ORGNO)) { }
                else if (PublicCls.OrgIsXian(v.ORGNO)) { v.ORGNAME = "　　" + v.ORGNAME; }
                else { v.ORGNAME = "　　　　" + v.ORGNAME; }
                int j = 0;
                IRow row = sheet1.CreateRow(rowIndex + 4);
                row.CreateCell(j).SetCellValue(v.ORGNAME);
                row.GetCell(j).CellStyle = getCellStyleLeft(book);
                j++;
                row.CreateCell(j).SetCellValue(happenArea);
                row.GetCell(j).CellStyle = getCellStyleCenter(book);
                j++;
                row.CreateCell(j).SetCellValue(HJ > 0 ? string.Format("{0:0.00}", HJ) : "");
                row.GetCell(j).CellStyle = getCellStyleCenter(book);
                j++;
                row.CreateCell(j).SetCellValue(_WGHList[_WGHList.Count - 1] > 0 ? string.Format("{0:0.00}", _WGHList[_WGHList.Count - 1]) : "");
                row.GetCell(j).CellStyle = getCellStyleCenter(book);
                j++;
                for (int m = 0; m < _WGHList.Count - 1; m++)
                {
                    row.CreateCell(j).SetCellValue(_WGHList[m] > 0 ? string.Format("{0:0.00}", _WGHList[m]) : "");
                    row.GetCell(j).CellStyle = getCellStyleCenter(book);
                    j++;
                }
                for (int n = 0; n < _YGHList.Count - 1; n++)
                {
                    row.CreateCell(j).SetCellValue(_YGHList[n] > 0 ? string.Format("{0:0.00}", _YGHList[n]) : "");
                    row.GetCell(j).CellStyle = getCellStyleCenter(book);
                    j++;
                }
                row.CreateCell(j).SetCellValue(WHGFZL > 0 ? string.Format("{0:P}", WHGFZL) : "");
                row.GetCell(j).CellStyle = getCellStyleCenter(book);
                j++;
                row.CreateCell(j).SetCellValue(FZL > 0 ? string.Format("{0:P}", FZL) : "");
                row.GetCell(j).CellStyle = getCellStyleCenter(book);
                rowIndex++;
            }
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

        #region 成灾报表
        /// <summary>
        /// 成灾报表
        /// </summary>
        /// <returns></returns>
        public ActionResult HARMREPORT()
        {
            pubViewBag("024007", "024007", "成灾报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.PEST = T_SYS_PESTCls.getSelectOption(new T_SYS_PEST_SW());
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM");
            ViewBag.Save = (SystemCls.isRight("024007001")) ? 1 : 0;
            ViewBag.Export = (SystemCls.isRight("024007002")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 成灾报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult HARMREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string PESTBYCODE = Request.Params["PESTCODE"];
            string Time = Request.Params["Time"];
            List<T_SYS_ORGModel> result;
            List<PEST_REPORT_HARM_Model> _harmList;
            HARMREPORTData(BYORGNO, PESTBYCODE, Time, out result, out _harmList);
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"HARMTable\"  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>单位名称</th><th>成灾面积</br>(" + dic113Name + ")</th><th>预计成灾面积</br>(" + dic113Name + ")</th><th>死亡株数</th></tr>");
            sb.AppendFormat("</thead>");
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                PEST_REPORT_HARM_Model m = _harmList.Find(a => a.BYORGNO == v.ORGNO);
                string DISASTERAREA = "", FORECASTDISASTERAREA = "", DIEPLATECOUNT = "";
                if (m != null)
                {
                    if (!string.IsNullOrEmpty(m.DISASTERAREA))
                        DISASTERAREA = string.Format("{0:0.00}", float.Parse(m.DISASTERAREA));
                    if (!string.IsNullOrEmpty(m.FORECASTDISASTERAREA))
                        FORECASTDISASTERAREA = string.Format("{0:0.00}", float.Parse(m.FORECASTDISASTERAREA));
                    if (!string.IsNullOrEmpty(m.DIEPLATECOUNT))
                        DIEPLATECOUNT = m.DIEPLATECOUNT;
                }
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"left\" style=\"{2}\" >{0}{1}</td>", v.ORGNAME, "<input id=\"txtORGNO" + i + "\" type=\"hidden\" value=\"" + v.ORGNO + "\" />", PublicCls.getOrgTDNameClass(BYORGNO, v.ORGNO));
                sb.AppendFormat("<td  class=\"center\">{0}</td>", "<input id=\"txt" + (i.ToString() + "0") + "\" type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + DISASTERAREA + "\" >");
                sb.AppendFormat("<td  class=\"center\">{0}</td>", "<input id=\"txt" + (i.ToString() + "1") + "\" type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + FORECASTDISASTERAREA + "\" >");
                sb.AppendFormat("<td  class=\"center\">{0}</td>", "<input id=\"txt" + (i.ToString() + "2") + "\" type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + DIEPLATECOUNT + "\" >");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 成灾报表数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult HARMREPORTManager()
        {
            PEST_REPORT_HARM_Model m = new PEST_REPORT_HARM_Model();
            m.TopORGNO = Request.Params["TopORGNO"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.HAPPENYEAR = Request.Params["HAPPENYEAR"];
            m.HAPPENMONTH = Request.Params["HAPPENMONTH"];
            m.PESTBYCODE = Request.Params["PESTBYCODE"];
            m.DISASTERAREA = Request.Params["DISASTERAREA"];
            m.FORECASTDISASTERAREA = Request.Params["FORECASTDISASTERAREA"];
            m.DIEPLATECOUNT = Request.Params["DIEPLATECOUNT"];
            return Content(JsonConvert.SerializeObject(PEST_REPORT_HARMCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult HARMREPORTExportExcel()
        {
            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string PESTBYCODE = Request.Params["PESTCODE"];
            string Time = Request.Params["Time"];
            List<T_SYS_ORGModel> result;
            List<PEST_REPORT_HARM_Model> _harmList;
            HARMREPORTData(BYORGNO, PESTBYCODE, Time, out result, out _harmList);
            string orgName = T_SYS_ORGCls.getorgname(BYORGNO);
            string pestName = T_SYS_PESTCls.getName(PESTBYCODE);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "024007", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + pestName + "-" + Time + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 30 * 256);
                else
                    sheet1.SetColumnWidth(i, 15 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead = sheet1.CreateRow(1);
            rowHead.CreateCell(0).SetCellValue("单位名称");
            rowHead.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead.CreateCell(1).SetCellValue("成灾面积\n(" + dic113Name + ")");
            rowHead.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead.CreateCell(2).SetCellValue("预计成灾面积\n(" + dic113Name + ")");
            rowHead.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead.CreateCell(3).SetCellValue("死亡株数");
            rowHead.GetCell(3).CellStyle = getCellStyleHead(book);
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 3));
            #endregion

            #region 表身及数据
            int rowIndex = 0;
            foreach (var v in result)
            {
                PEST_REPORT_HARM_Model m = _harmList.Find(a => a.BYORGNO == v.ORGNO);
                string DISASTERAREA = "", FORECASTDISASTERAREA = "", DIEPLATECOUNT = "";
                if (m != null)
                {
                    if (!string.IsNullOrEmpty(m.DISASTERAREA))
                        DISASTERAREA = string.Format("{0:0.00}", float.Parse(m.DISASTERAREA));
                    if (!string.IsNullOrEmpty(m.FORECASTDISASTERAREA))
                        FORECASTDISASTERAREA = string.Format("{0:0.00}", float.Parse(m.FORECASTDISASTERAREA));
                    if (!string.IsNullOrEmpty(m.DIEPLATECOUNT))
                        DIEPLATECOUNT = m.DIEPLATECOUNT;
                }
                if (PublicCls.OrgIsShi(v.ORGNO)) { }
                else if (PublicCls.OrgIsXian(v.ORGNO)) { v.ORGNAME = "　　" + v.ORGNAME; }
                else { v.ORGNAME = "　　　　" + v.ORGNAME; }
                IRow row = sheet1.CreateRow(rowIndex + 2);
                row.CreateCell(0).SetCellValue(v.ORGNAME);
                row.GetCell(0).CellStyle = getCellStyleLeft(book);
                row.CreateCell(1).SetCellValue(DISASTERAREA);
                row.GetCell(1).CellStyle = getCellStyleCenter(book);
                row.CreateCell(2).SetCellValue(FORECASTDISASTERAREA);
                row.GetCell(2).CellStyle = getCellStyleCenter(book);
                row.CreateCell(3).SetCellValue(DIEPLATECOUNT);
                row.GetCell(3).CellStyle = getCellStyleCenter(book);
                rowIndex++;
            }
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

        #region 检疫报表
        /// <summary>
        /// 检疫报表
        /// </summary>
        /// <returns></returns>
        public ActionResult QUARANTINEREPORT()
        {
            pubViewBag("024008", "024008", "检疫报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.Time = DateTime.Now.ToString("yyyy");
            List<T_SYS_DICTModel> dic108List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "108" }).ToList();
            ViewBag.dic108Value = T_SYS_DICTCls.getDicValueStr(dic108List);
            ViewBag.dic108Count = dic108List.Count;
            ViewBag.Save = (SystemCls.isRight("024008001")) ? 1 : 0;
            ViewBag.Export = (SystemCls.isRight("024008002")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 检疫报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult QUARANTINEREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string Time = Request.Params["Time"];
            List<T_SYS_ORGModel> result;
            List<T_SYS_DICTModel> dic108List;
            List<PEST_REPORT_QUARANTINE_Model> _list;
            QUARANTINEREPORTData(BYORGNO, Time, out result, out dic108List, out _list);
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"QUARANTINETable\"  cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style=\"width:4%;\">序号</th><th style=\"width:14%;\">单位名称</th>");
            foreach (var d in dic108List)
            {
                string title = d.DICTNAME;
                if (!string.IsNullOrEmpty(d.STANDBY1))
                    title += "</br>(" + d.STANDBY1 + ")";
                sb.AppendFormat("<th>{0}</th>", title);
            }
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"left\" style=\"{2}\" >{0}{1}</td>", v.ORGNAME, "<input id=\"txtORGNO" + i + "\" type=\"hidden\" value=\"" + v.ORGNO + "\" />", PublicCls.getOrgTDNameClass(BYORGNO, v.ORGNO));
                for (int j = 0; j < dic108List.Count; j++)
                {
                    string QUARANTINEVALUE = "";
                    PEST_REPORT_QUARANTINE_Model m = _list.Find(a => a.BYORGNO == v.ORGNO && a.QUARANTINETYPECODE == dic108List[j].DICTVALUE);
                    if (dic108List[j].DICTNAME.Contains("率"))
                        QUARANTINEVALUE = (m != null && !string.IsNullOrEmpty(m.QUARANTINEVALUE)) ? string.Format("{0:P}", float.Parse(m.QUARANTINEVALUE) / 100) : "";
                    else
                        QUARANTINEVALUE = (m != null && !string.IsNullOrEmpty(m.QUARANTINEVALUE)) ? string.Format("{0:0.00}", float.Parse(m.QUARANTINEVALUE)) : "";
                    sb.AppendFormat("<th>{0}</th>", "<input id=\"txt" + (i.ToString() + j.ToString()) + "\" type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + QUARANTINEVALUE + "\" >");
                }
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 成灾报表数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult QUARANTINEREPORTManager()
        {
            PEST_REPORT_QUARANTINE_Model m = new PEST_REPORT_QUARANTINE_Model();
            m.TopORGNO = Request.Params["TopORGNO"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.HAPPENYEAR = Request.Params["HAPPENYEAR"];
            m.QUARANTINETYPECODE = Request.Params["QUARANTINETYPECODE"];
            m.QUARANTINEVALUE = Request.Params["QUARANTINEVALUE"];
            return Content(JsonConvert.SerializeObject(PEST_REPORT_QUARANTINECls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult QUARANTINEREPORTExportExcel()
        {
            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string Time = Request.Params["Time"];
            List<T_SYS_ORGModel> result;
            List<T_SYS_DICTModel> dic108List;
            List<PEST_REPORT_QUARANTINE_Model> _list;
            QUARANTINEREPORTData(BYORGNO, Time, out result, out dic108List, out _list);
            int colsCount = dic108List.Count + 1;
            string orgName = T_SYS_ORGCls.getorgname(BYORGNO);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "024008", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + Time + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            for (int i = 0; i < colsCount; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 30 * 256);
                else
                    sheet1.SetColumnWidth(i, 15 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead = sheet1.CreateRow(1);
            rowHead.CreateCell(0).SetCellValue("单位名称");
            rowHead.GetCell(0).CellStyle = getCellStyleHead(book);
            for (int i = 0; i < dic108List.Count; i++)
            {
                string name = dic108List[i].DICTNAME;
                if (!string.IsNullOrEmpty(dic108List[i].STANDBY1))
                    name += "\n(" + dic108List[i].STANDBY1 + ")";
                rowHead.CreateCell(i + 1).SetCellValue(name);
                rowHead.GetCell(i + 1).CellStyle = getCellStyleHead(book);
            }
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount - 1));
            #endregion

            #region 表身及数据
            int rowIndex = 0;
            foreach (var v in result)
            {
                if (PublicCls.OrgIsShi(v.ORGNO)) { }
                else if (PublicCls.OrgIsXian(v.ORGNO)) { v.ORGNAME = "　　" + v.ORGNAME; }
                else { v.ORGNAME = "　　　　" + v.ORGNAME; }
                IRow row = sheet1.CreateRow(rowIndex + 2);
                row.CreateCell(0).SetCellValue(v.ORGNAME);
                row.GetCell(0).CellStyle = getCellStyleLeft(book);
                for (int j = 0; j < dic108List.Count; j++)
                {
                    string QUARANTINEVALUE = "";
                    PEST_REPORT_QUARANTINE_Model m = _list.Find(a => a.BYORGNO == v.ORGNO && a.QUARANTINETYPECODE == dic108List[j].DICTVALUE);
                    if (dic108List[j].DICTNAME.Contains("率"))
                        QUARANTINEVALUE = (m != null && !string.IsNullOrEmpty(m.QUARANTINEVALUE)) ? string.Format("{0:P}", float.Parse(m.QUARANTINEVALUE) / 100) : "";
                    else
                        QUARANTINEVALUE = (m != null && !string.IsNullOrEmpty(m.QUARANTINEVALUE)) ? string.Format("{0:0.00}", float.Parse(m.QUARANTINEVALUE)) : "";
                    row.CreateCell(j + 1).SetCellValue(QUARANTINEVALUE);
                    row.GetCell(j + 1).CellStyle = getCellStyleCenter(book);
                }
                rowIndex++;
            }
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

        #region 人财物报表
        /// <summary>
        /// 人财物报表
        /// </summary>
        /// <returns></returns>
        public ActionResult RCWREPORT()
        {
            pubViewBag("024009", "024009", "人财物报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.Time = DateTime.Now.ToString("yyyy");
            List<PEST_REPORT_RCWTYPE_Model> _rcwTypeTitleList = PEST_REPORT_RCWTYPECls.getListModel(new PEST_REPORT_RCWTYPE_SW { ISGetOnlyTypeTitle = true }).ToList();
            ViewBag.REPORTCount = _rcwTypeTitleList.Count;
            ViewBag.REPORTCols = PEST_REPORT_RCWTYPECls.GetREPORTCols(_rcwTypeTitleList);
            ViewBag.Save = (SystemCls.isRight("024009001")) ? 1 : 0;
            ViewBag.Export = (SystemCls.isRight("024009002")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 人财物报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult RCWREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string Time = Request.Params["Time"];
            List<PEST_REPORT_RCWTYPE_Model> _rcwTypeList;
            List<PEST_REPORT_RCWTYPE_Model> _rcwTypeTitleList;
            List<PEST_REPORT_RCW_Model> _list;
            RCWREPORTData(BYORGNO, Time, out _rcwTypeList, out _rcwTypeTitleList, out _list);
            #endregion

            #region 数据表
            for (int i = 0; i < _rcwTypeTitleList.Count; i++)
            {
                int _titleCodeLength = _rcwTypeTitleList[i].RCWCODE.Length;
                List<PEST_REPORT_RCWTYPE_Model> _rcwTypeColsList = _rcwTypeList.FindAll(a => a.RCWCODE.Length > _titleCodeLength && a.RCWCODE.Substring(0, _titleCodeLength) == _rcwTypeTitleList[i].RCWCODE);
                sb.AppendFormat("<table id=\"type" + i.ToString() + "\" cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<thead>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<th colspan=\"" + _rcwTypeColsList.Count + "\">{0}</th>", _rcwTypeTitleList[i].RCWNAME);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                for (int j = 0; j < _rcwTypeColsList.Count; j++)
                {
                    sb.AppendFormat("<th>{0}</th>", _rcwTypeColsList[j].RCWNAME);
                }
                sb.AppendFormat("</tr>");
                sb.AppendFormat("</thead>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("<tr>");
                for (int x = 0; x < _rcwTypeColsList.Count; x++)
                {
                    PEST_REPORT_RCW_Model m = _list.Find(a => a.RCWCODE == _rcwTypeColsList[x].RCWCODE);
                    string value = (m != null && m.RCWVALUE != null) ? m.RCWVALUE : "";
                    sb.AppendFormat("<td>{0}{1}</td>",
                        "<input id=\"txtCode" + (i.ToString() + x.ToString()) + "\" type=\"hidden\"  value=\"" + _rcwTypeColsList[x].RCWCODE + "\" />",
                        "<input id=\"txtValue" + (i.ToString() + x.ToString()) + "\" type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + value + "\" />");
                }
                sb.AppendFormat("</tr>");
                sb.AppendFormat("</tbody>");
                sb.AppendFormat("</table>");
                if (i != _rcwTypeTitleList.Count - 1)
                    sb.AppendFormat("<br /><br />");
                else
                    sb.AppendFormat("<br />");
            }
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 人财物报表数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult RCWREPORTManager()
        {
            PEST_REPORT_RCW_Model m = new PEST_REPORT_RCW_Model();
            m.BYORGNO = Request.Params["BYORGNO"];
            m.RCWYEAR = Request.Params["RCWYEAR"];
            m.RCWCODE = Request.Params["RCWCODE"];
            m.RCWVALUE = Request.Params["RCWVALUE"];
            return Content(JsonConvert.SerializeObject(PEST_REPORT_RCWCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult RCWREPORTExportExcel()
        {
            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string Time = Request.Params["Time"];
            List<PEST_REPORT_RCWTYPE_Model> _rcwTypeList;
            List<PEST_REPORT_RCWTYPE_Model> _rcwTypeTitleList;
            List<PEST_REPORT_RCW_Model> _list;
            RCWREPORTData(BYORGNO, Time, out _rcwTypeList, out _rcwTypeTitleList, out _list);
            string[] ArrCols = PEST_REPORT_RCWTYPECls.GetREPORTCols(_rcwTypeTitleList).Split(',');
            int MaxCol = 0;
            for (int i = 0; i < ArrCols.Length; i++)
            {
                MaxCol = MaxCol > int.Parse(ArrCols[i]) ? MaxCol : int.Parse(ArrCols[i]);
            }
            string orgName = T_SYS_ORGCls.getorgname(BYORGNO);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "024009", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + Time + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线
            for (int i = 0; i < MaxCol; i++)
            {
                sheet1.SetColumnWidth(i, 16 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, MaxCol - 1));
            int rowIndex = 1;
            for (int i = 0; i < _rcwTypeTitleList.Count; i++)
            {
                int _titleCodeLength = _rcwTypeTitleList[i].RCWCODE.Length;
                List<PEST_REPORT_RCWTYPE_Model> _rcwTypeColsList = _rcwTypeList.FindAll(a => a.RCWCODE.Length > _titleCodeLength && a.RCWCODE.Substring(0, _titleCodeLength) == _rcwTypeTitleList[i].RCWCODE);
                IRow rowHead = sheet1.CreateRow(rowIndex);
                rowHead.CreateCell(0).SetCellValue(_rcwTypeTitleList[i].RCWNAME);
                rowHead.GetCell(0).CellStyle = getCellStyleHead(book);
                sheet1.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 0, _rcwTypeColsList.Count - 1));
                rowIndex++;
                IRow row = sheet1.CreateRow(rowIndex);
                for (int j = 0; j < _rcwTypeColsList.Count; j++)
                {
                    row.CreateCell(j).SetCellValue(_rcwTypeColsList[j].RCWNAME);
                    row.GetCell(j).CellStyle = getCellStyleHead(book);
                }
                rowIndex++;
                row = sheet1.CreateRow(rowIndex);
                for (int x = 0; x < _rcwTypeColsList.Count; x++)
                {
                    PEST_REPORT_RCW_Model m = _list.Find(a => a.RCWCODE == _rcwTypeColsList[x].RCWCODE);
                    string value = (m != null && m.RCWVALUE != null) ? m.RCWVALUE : "";
                    row.CreateCell(x).SetCellValue(value);
                    row.GetCell(x).CellStyle = getCellStyleCenter(book);
                }
                if (i != _rcwTypeTitleList.Count - 1)
                    rowIndex += 2;
            }
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

        #region 目标考核报表
        /// <summary>
        /// 目标考核报表
        /// </summary>
        /// <returns></returns>
        public ActionResult ASSESSINGTARGETREPORT()
        {
            pubViewBag("024010", "024010", "目标报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.YEAR = DateTime.Now.ToString("yyyy");
            List<T_SYS_DICTModel> dic109List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "109" }).ToList();
            ViewBag.dic109Value = T_SYS_DICTCls.getDicValueStr(dic109List);
            ViewBag.dic109Count = dic109List.Count;
            ViewBag.Save = (SystemCls.isRight("024010001")) ? 1 : 0;
            ViewBag.Export = (SystemCls.isRight("024010002")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 目标考核报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult ASSESSINGTARGETREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string YEAR = Request.Params["YEAR"];
            List<T_SYS_ORGModel> result;
            List<T_SYS_DICTModel> dic109List;
            List<PEST_REPORT_ASSESSINGTARGET_Model> _list;
            ASSESSINGTARGETREPORTData(BYORGNO, YEAR, out result, out dic109List, out _list);
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"ASSESSINGTARGETTable\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style=\"width:4%;\">序号</th><th style=\"width:14%;\">单位名称</th>");
            foreach (var d in dic109List)
            {
                string title = d.DICTNAME;
                if (!string.IsNullOrEmpty(d.STANDBY1))
                    title += "</br>(" + d.STANDBY1 + ")";
                sb.AppendFormat("<th>{0}</th>", title);
            }
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            for (int i = 0; i < result.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"left\" style=\"{2}\" >{0}{1}</td>", result[i].ORGNAME, "<input id=\"txtORGNO" + i + "\" type=\"hidden\" value=\"" + result[i].ORGNO + "\" />", PublicCls.getOrgTDNameClass(BYORGNO, result[i].ORGNO));
                for (int j = 0; j < dic109List.Count; j++)
                {
                    PEST_REPORT_ASSESSINGTARGET_Model m = _list.Find(a => a.BYORGNO == result[i].ORGNO && a.ASSESSINGTARGETTYPECODE == dic109List[j].DICTVALUE);
                    string value = (m != null && !string.IsNullOrEmpty(m.ASSESSINGTARGETVALUE)) ? string.Format("{0:P}", float.Parse(m.ASSESSINGTARGETVALUE) / 100) : "";
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"txt" + (i.ToString() + j.ToString()) + "\" type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + value + "\" >");
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 目标考核报表数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult ASSESSINGTARGETREPORTManager()
        {
            PEST_REPORT_ASSESSINGTARGET_Model m = new PEST_REPORT_ASSESSINGTARGET_Model();
            m.TopORGNO = Request.Params["TopORGNO"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.RCWYEAR = Request.Params["RCWYEAR"];
            m.ASSESSINGTARGETTYPECODE = Request.Params["ASSESSINGTARGETTYPECODE"];
            m.ASSESSINGTARGETVALUE = Request.Params["ASSESSINGTARGETVALUE"];
            return Content(JsonConvert.SerializeObject(PEST_REPORT_ASSESSINGTARGETCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult ASSESSINGTARGETREPORTExportExcel()
        {
            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string YEAR = Request.Params["YEAR"];
            List<T_SYS_ORGModel> result;
            List<T_SYS_DICTModel> dic109List;
            List<PEST_REPORT_ASSESSINGTARGET_Model> _list;
            ASSESSINGTARGETREPORTData(BYORGNO, YEAR, out result, out dic109List, out _list);
            int colsCount = dic109List.Count + 1;
            string orgName = T_SYS_ORGCls.getorgname(BYORGNO);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "024010", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + YEAR + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            for (int i = 0; i < colsCount; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 30 * 256);
                else
                    sheet1.SetColumnWidth(i, 18 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead = sheet1.CreateRow(1);
            rowHead.CreateCell(0).SetCellValue("单位名称");
            rowHead.GetCell(0).CellStyle = getCellStyleHead(book);
            for (int i = 0; i < dic109List.Count; i++)
            {
                string name = dic109List[i].DICTNAME;
                if (!string.IsNullOrEmpty(dic109List[i].STANDBY1))
                    name += "\n(" + dic109List[i].STANDBY1 + ")";
                rowHead.CreateCell(i + 1).SetCellValue(name);
                rowHead.GetCell(i + 1).CellStyle = getCellStyleHead(book);
            }
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount - 1));
            #endregion

            #region 表身及数据
            int rowIndex = 0;
            foreach (var v in result)
            {
                if (PublicCls.OrgIsShi(v.ORGNO)) { }
                else if (PublicCls.OrgIsXian(v.ORGNO)) { v.ORGNAME = "　　" + v.ORGNAME; }
                else { v.ORGNAME = "　　　　" + v.ORGNAME; }
                IRow row = sheet1.CreateRow(rowIndex + 2);
                row.CreateCell(0).SetCellValue(v.ORGNAME);
                row.GetCell(0).CellStyle = getCellStyleLeft(book);
                for (int j = 0; j < dic109List.Count; j++)
                {
                    PEST_REPORT_ASSESSINGTARGET_Model m = _list.Find(a => a.BYORGNO == v.ORGNO && a.ASSESSINGTARGETTYPECODE == dic109List[j].DICTVALUE);
                    string value = (m != null && !string.IsNullOrEmpty(m.ASSESSINGTARGETVALUE)) ? string.Format("{0:P}", float.Parse(m.ASSESSINGTARGETVALUE) / 100) : "";
                    row.CreateCell(j + 1).SetCellValue(value);
                    row.GetCell(j + 1).CellStyle = getCellStyleCenter(book);
                }
                rowIndex++;
            }
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

        #region 预测报表
        /// <summary>
        /// 预测报表
        /// </summary>
        /// <returns></returns>
        public ActionResult FORECASTREPORT()
        {
            pubViewBag("024011", "024011", "预测报表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.PEST = T_SYS_PESTCls.getSelectOption(new T_SYS_PEST_SW());
            ViewBag.YEAR = DateTime.Now.ToString("yyyy");
            List<T_SYS_DICTModel> dic110List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "110" }).ToList();
            ViewBag.dic110Value = T_SYS_DICTCls.getDicValueStr(dic110List);
            ViewBag.dic110Count = dic110List.Count;
            ViewBag.Save = (SystemCls.isRight("024011001")) ? 1 : 0;
            ViewBag.Export = (SystemCls.isRight("024011002")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 预测报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult FORECASTREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string PESTCODE = Request.Params["PESTCODE"];
            string YEAR = Request.Params["YEAR"];
            List<T_SYS_ORGModel> result;
            List<T_SYS_DICTModel> _dic110List;
            List<PEST_LOCALTREESPECIES_Model> _AREAList;
            List<PEST_REPORT_FORECAST_Model> _list1;
            List<PEST_REPORT_FORECAST_Model> _list2;
            FORECASTREPORTData(BYORGNO, PESTCODE, YEAR, out result, out _dic110List, out _AREAList, out _list1, out _list2);
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"FORECASTTable\" cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th rowspan=\"2\" style=\"width:4%;\">序号</th><th rowspan=\"2\" style=\"width:14%;\">单位名称</th><th rowspan=\"2\"  style=\"width:12%;\">寄主面积</br>(" + dic113Name + "))</th>");
            sb.AppendFormat("<th colspan=\"" + (_dic110List.Count + 1) + "\"  style=\"width:35%;\">去年发生</br>(" + dic113Name + ")</th><th colspan=\"" + (_dic110List.Count + 1) + "\"  style=\"width:35%;\">今年预测</br>(" + dic113Name + ")</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>合计</th>");
            foreach (var d in _dic110List)
            {
                string title = d.DICTNAME;
                if (!string.IsNullOrEmpty(d.STANDBY1))
                    title += "</br>(" + d.STANDBY1 + ")";
                sb.AppendFormat("<th>{0}</th>", title);
            }
            sb.AppendFormat("<th>合计</th>");
            foreach (var d in _dic110List)
            {
                string title = d.DICTNAME;
                if (!string.IsNullOrEmpty(d.STANDBY1))
                    title += "</br>(" + d.STANDBY1 + ")";
                sb.AppendFormat("<th>{0}</th>", title);
            }
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");
            for (int i = 0; i < result.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"left\" style=\"{2}\" >{0}{1}</td>", result[i].ORGNAME, "<input id=\"txtORGNO" + i + "\" type=\"hidden\" value=\"" + result[i].ORGNO + "\" />", PublicCls.getOrgTDNameClass(BYORGNO, result[i].ORGNO));
                float JZArea = PEST_REPORT_FORECASTCls.GetJZArea(result[i].ORGNO, _AREAList);
                sb.AppendFormat("<td>{0}</td>", JZArea > 0 ? string.Format("{0:0.00}", JZArea) : "");
                float HJArea1 = PEST_REPORT_FORECASTCls.GetHJArea(result[i].ORGNO, _list1);
                sb.AppendFormat("<td>{0}</td>", HJArea1 > 0 ? string.Format("{0:0.00}", HJArea1) : "");
                foreach (var d in _dic110List)
                {
                    PEST_REPORT_FORECAST_Model m = _list1.Find(a => a.BYORGNO == result[i].ORGNO && a.FORECASTSTAGECODE == d.DICTVALUE);
                    string area = (m != null && !string.IsNullOrEmpty(m.FORECASTAREA)) ? string.Format("{0:0.00}", float.Parse(m.FORECASTAREA)) : "";
                    sb.AppendFormat("<td>{0}</td>", area);
                }
                float HJArea2 = PEST_REPORT_FORECASTCls.GetHJArea(result[i].ORGNO, _list2);
                sb.AppendFormat("<td>{0}</td>", HJArea2 > 0 ? string.Format("{0:0.00}", HJArea2) : "");
                for (int j = 0; j < _dic110List.Count; j++)
                {
                    PEST_REPORT_FORECAST_Model m = _list2.Find(a => a.BYORGNO == result[i].ORGNO && a.FORECASTSTAGECODE == _dic110List[j].DICTVALUE);
                    string area = (m != null && !string.IsNullOrEmpty(m.FORECASTAREA)) ? string.Format("{0:0.00}", float.Parse(m.FORECASTAREA)) : "";
                    sb.AppendFormat("<td>{0}</td>", "<input id=\"txt" + (i.ToString() + j.ToString()) + "\" type=\"text\" class=\"center\" style=\"style:98%;\" value=\"" + area + "\" />");
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 预测报表数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult FORECASTREPORTManager()
        {
            PEST_REPORT_FORECAST_Model m = new PEST_REPORT_FORECAST_Model();
            m.TopORGNO = Request.Params["TopORGNO"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.FORECASTYEAR = Request.Params["FORECASTYEAR"];
            m.PESTBYCODE = Request.Params["PESTBYCODE"];
            m.FORECASTSTAGECODE = Request.Params["FORECASTSTAGECODE"];
            m.FORECASTAREA = Request.Params["FORECASTAREA"];
            return Content(JsonConvert.SerializeObject(PEST_REPORT_FORECASTCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult FORECASTREPORTExportExcel()
        {
            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string PESTCODE = Request.Params["PESTCODE"];
            string YEAR = Request.Params["YEAR"];
            List<T_SYS_ORGModel> result;
            List<T_SYS_DICTModel> _dic110List;
            List<PEST_LOCALTREESPECIES_Model> _AREAList;
            List<PEST_REPORT_FORECAST_Model> _list1;
            List<PEST_REPORT_FORECAST_Model> _list2;
            FORECASTREPORTData(BYORGNO, PESTCODE, YEAR, out result, out _dic110List, out _AREAList, out _list1, out _list2); int colsCount = (_dic110List.Count + 1) * 2 + 2;
            string orgName = T_SYS_ORGCls.getorgname(BYORGNO);
            string pestName = T_SYS_PESTCls.getName(PESTCODE);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "024011", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + pestName + "-" + YEAR + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            for (int i = 0; i < colsCount; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 30 * 256);
                else
                    sheet1.SetColumnWidth(i, 15 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.Height = 2 * 350;
            rowHead1.CreateCell(0).SetCellValue("单位名称");
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(1).SetCellValue("寄主面积\n(" + dic113Name + ")");
            rowHead1.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(2).SetCellValue("去年发生\n(" + dic113Name + ")");
            rowHead1.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(3 + _dic110List.Count).SetCellValue("今年预测\n(" + dic113Name + ")");
            rowHead1.GetCell(3 + _dic110List.Count).CellStyle = getCellStyleHead(book);
            IRow rowHead2 = sheet1.CreateRow(2);
            int y = 2;
            for (int i = 0; i < 2; i++)
            {
                rowHead2.CreateCell(y).SetCellValue("合计");
                rowHead2.GetCell(y).CellStyle = getCellStyleHead(book);
                y++;
                foreach (var d2 in _dic110List)
                {
                    string name = d2.DICTNAME;
                    if (!string.IsNullOrEmpty(d2.STANDBY1))
                        name += "\n(" + d2.STANDBY1 + ")";
                    rowHead2.CreateCell(y).SetCellValue(name);
                    rowHead2.GetCell(y).CellStyle = getCellStyleHead(book);
                    y++;
                }
            }
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount - 1));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 1, 1));
            for (int i = 0; i < 2; i++)
            {
                int index = 0;
                if (i == 0)
                    index = i * (_dic110List.Count) + 2;
                else
                    index = i * (_dic110List.Count) + 3;
                sheet1.AddMergedRegion(new CellRangeAddress(1, 1, index, index + _dic110List.Count));
            }
            #endregion

            #region 表身及数据
            int rowIndex = 0;
            foreach (var v in result)
            {
                if (PublicCls.OrgIsShi(v.ORGNO)) { }
                else if (PublicCls.OrgIsXian(v.ORGNO)) { v.ORGNAME = "　　" + v.ORGNAME; }
                else { v.ORGNAME = "　　　　" + v.ORGNAME; }
                IRow row = sheet1.CreateRow(rowIndex + 3);
                row.CreateCell(0).SetCellValue(v.ORGNAME);
                row.GetCell(0).CellStyle = getCellStyleLeft(book);
                float JZArea = PEST_REPORT_FORECASTCls.GetJZArea(v.ORGNO, _AREAList);
                row.CreateCell(1).SetCellValue(JZArea > 0 ? string.Format("{0:0.00}", JZArea) : "");
                row.GetCell(1).CellStyle = getCellStyleCenter(book);
                int index = 2;
                float HJArea1 = PEST_REPORT_FORECASTCls.GetHJArea(v.ORGNO, _list1);
                row.CreateCell(index).SetCellValue(HJArea1 > 0 ? string.Format("{0:0.00}", HJArea1) : "");
                row.GetCell(index).CellStyle = getCellStyleCenter(book);
                index++;
                foreach (var d in _dic110List)
                {
                    PEST_REPORT_FORECAST_Model m = _list1.Find(a => a.BYORGNO == v.ORGNO && a.FORECASTSTAGECODE == d.DICTVALUE);
                    string area = (m != null && !string.IsNullOrEmpty(m.FORECASTAREA)) ? string.Format("{0:0.00}", float.Parse(m.FORECASTAREA)) : "";
                    row.CreateCell(index).SetCellValue(area);
                    row.GetCell(index).CellStyle = getCellStyleCenter(book);
                    index++;
                }
                float HJArea2 = PEST_REPORT_FORECASTCls.GetHJArea(v.ORGNO, _list2);
                row.CreateCell(index).SetCellValue(HJArea2 > 0 ? string.Format("{0:0.00}", HJArea2) : "");
                row.GetCell(index).CellStyle = getCellStyleCenter(book);
                index++;
                for (int j = 0; j < _dic110List.Count; j++)
                {
                    PEST_REPORT_FORECAST_Model m = _list2.Find(a => a.BYORGNO == v.ORGNO && a.FORECASTSTAGECODE == _dic110List[j].DICTVALUE);
                    string area = (m != null && !string.IsNullOrEmpty(m.FORECASTAREA)) ? string.Format("{0:0.00}", float.Parse(m.FORECASTAREA)) : "";
                    row.CreateCell(index).SetCellValue(area);
                    row.GetCell(index).CellStyle = getCellStyleCenter(book);
                    index++;
                }
                rowIndex++;
            }
            #endregion

            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = T_SYS_ORGCls.getorgname(BYORGNO) + "-" + T_SYS_PESTCls.getName(PESTCODE) + "-" + YEAR + "-" + T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "024011", SYSFLAG = ConfigCls.getSystemFlag() }) + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }
        #endregion

        #region 枯死松树调查表
        /// <summary>
        /// 枯死松树调查报表
        /// </summary>
        /// <returns></returns>
        public ActionResult DIEPINESURVEYREPORT()
        {
            pubViewBag("024012", "024012", "枯死松树调查表");
            if (ViewBag.isPageRight == false)
                return View();
            string Page = string.IsNullOrEmpty(Request.Params["Page"]) ? "1" : Request.Params["Page"];//当前页数
            string trans = Request.Params["trans"];//传递网页参数 
            string[] arr = new string[5];//存放查询条件的数组 根据实际存放的数据
            if (!string.IsNullOrEmpty(trans))
                arr = ClsStr.DecryptA01(trans, "kkkkkkkk").Split('|');
            if (string.IsNullOrEmpty(arr[0]))
                arr[0] = PagerCls.getDefaultPageSize().ToString();
            if (string.IsNullOrEmpty(arr[1]))
                arr[1] = SystemCls.getCurUserOrgNo();
            if (string.IsNullOrEmpty(arr[3]))
                arr[3] = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(arr[4]))
                arr[4] = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = arr[1] });
            ViewBag.FINDER = arr[2];
            ViewBag.STARTDATE = arr[3];
            ViewBag.ENDDATE = arr[4];
            ViewBag.FINDDATE = DateTime.Now.ToString("yyyy-MM-dd");
            int total = 0;
            ViewBag.TableInfo = GetDIEPINESURVEYREPORTStr(new PEST_REPORT_DIEPINESURVEY_SW { CurPage = int.Parse(Page), PageSize = int.Parse(arr[0]), BYORGNO = arr[1], FINDER = arr[2], STARTDATE = arr[3],ENDDATE=arr[4] }, out total);
            ViewBag.PageInfo = PagerCls.getPagerInfo_New(new PagerSW { curPage = int.Parse(Page), pageSize = int.Parse(arr[0]), rowCount = total, url = "/PEST/DIEPINESURVEYREPORT?trans=" + trans });
            ViewBag.Add = (SystemCls.isRight("024012001")) ? 1 : 0;
            ViewBag.Del = (SystemCls.isRight("024012004")) ? 1 : 0;
            ViewBag.Export = (SystemCls.isRight("024012005")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 枯死松树调查报表异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult DIEPINESURVEYREPORTQuery()
        {
            string Page = Request.Params["Page"];
            string PageSize = string.IsNullOrEmpty(Request.Params["PageSize"]) ? PagerCls.getDefaultPageSize().ToString() : Request.Params["PageSize"];
            string BYORGNO = Request.Params["BYORGNO"];
            string FINDER = Request.Params["FINDER"];
            string STARTDATE = Request.Params["STARTDATE"];
            string ENDDATE = Request.Params["ENDDATE"];
            string str = ClsStr.EncryptA01(PageSize + "|" + BYORGNO + "|" + FINDER + "|" + STARTDATE + "|" + ENDDATE, "kkkkkkkk");
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/PEST/DIEPINESURVEYREPORT?trans=" + str + "&Page=" + Page)), "text/html;charset=UTF-8");
        }

        /// <summary>
        ///枯死松树调查报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        private string GetDIEPINESURVEYREPORTStr(PEST_REPORT_DIEPINESURVEY_SW sw, out int total)
        {
            bool IsView = (SystemCls.isRight("024012002")) ? true : false;
            bool IsMDy = (SystemCls.isRight("024012003")) ? true : false;
            StringBuilder sb = new StringBuilder();

            #region 数据准备
            List<PEST_REPORT_DIEPINESURVEY_Model> _list = PEST_REPORT_DIEPINESURVEYCls.getListModel(sw, out total);
            string dis = _list.Count <= 0 ? "disabled=\"disabled\"" : "";
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"DIEPINESURVEYTable\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style=\"width:5%\"><input id=\"tbxDIEPINEALL\" name=\"tbxDIEPINEALL\" type=\"checkbox\" class=\"ace\" value=\"ALL\" onclick=\"SelectAll(this.value,this.checked)\" {0} /></th>", dis);
            sb.AppendFormat("<th style=\"width:5%;\">序号</th><th style=\"width:15%;\">单位名称</th>");
            sb.AppendFormat("<th>发现日期</th><th>发现人</th><th>联系电话</th><th>取样株数</th><th>死亡株数</th><th>报告日期</th><th>操作</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            for (int i = 0; i < _list.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\"><input id=\"tbxDIEPINE" + i + "\" name=\"tbxDIEPINE\"  type=\"checkbox\" class=\"ace\"  value=\"" + _list[i].PEST_REPORT_DIEPINESURVEYID + "\" onclick=\"SelectAll(this.value,this.checked)\"  /></td>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", _list[i].BYORGNONAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _list[i].FINDDATE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _list[i].FINDER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _list[i].LINKTELL);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _list[i].SAMPLINGCOUNT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _list[i].DIEPINECOUNT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _list[i].REPORTDATE);
                sb.AppendFormat("<td class=\"center\">");
                if (IsView)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", _list[i].PEST_REPORT_DIEPINESURVEYID);
                if (IsMDy)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}')\" title='修改' class=\"searchBox_01 LinkMdy\">修改</a>", _list[i].PEST_REPORT_DIEPINESURVEYID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            #endregion

            return sb.ToString();
        }

        /// <summary>
        /// 获取单条枯死松树数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDIEPINESURVEYREPORTDataJson()
        {
            string DIEPINESURVEYID = Request.Params["DIEPINESURVEYID"];
            PEST_REPORT_DIEPINESURVEY_Model m = PEST_REPORT_DIEPINESURVEYCls.getModel(new PEST_REPORT_DIEPINESURVEY_SW { PEST_REPORT_DIEPINESURVEYID = DIEPINESURVEYID });
            return Content(JsonConvert.SerializeObject(m), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 枯死松树数据查看
        /// </summary>
        /// <returns></returns>
        public ActionResult DIEPINESURVEYREPORTDataSee()
        {
            string DIEPINESURVEYID = Request.Params["DIEPINESURVEYID"];
            PEST_REPORT_DIEPINESURVEY_Model m = PEST_REPORT_DIEPINESURVEYCls.getModel(new PEST_REPORT_DIEPINESURVEY_SW { PEST_REPORT_DIEPINESURVEYID = DIEPINESURVEYID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td style=\"width:15%\">单位:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.BYORGNONAME);
            sb.AppendFormat("<td style=\"width:15%\">发现日期</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.FINDDATE);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td >发现人:</td>");
            sb.AppendFormat("<td >{0}</td>", m.FINDER);
            sb.AppendFormat("<td >联系电话:</td>");
            sb.AppendFormat("<td >{0}</td>", m.LINKTELL);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>取样株数:</td>");
            sb.AppendFormat("<td>{0}</td>", m.SAMPLINGCOUNT);
            sb.AppendFormat("<td>死亡株数:</td>");
            sb.AppendFormat("<td>{0}</td>", m.DIEPINECOUNT);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>报告日期:</td>");
            sb.AppendFormat("<td>{0}</td>", m.REPORTDATE);
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>鉴定结果:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.AUTHENTICATERESULT);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tbody>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 枯死松树调查报表数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult DIEPINESURVEYREPORTManager()
        {
            PEST_REPORT_DIEPINESURVEY_Model m = new PEST_REPORT_DIEPINESURVEY_Model();
            m.PEST_REPORT_DIEPINESURVEYID = Request.Params["DIEPINESURVEYID"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.FINDER = Request.Params["FINDER"];
            m.FINDDATE = Request.Params["FINDDATE"];
            m.LINKTELL = Request.Params["LINKTELL"];
            m.DIEPINECOUNT = Request.Params["DIEPINECOUNT"];
            m.REPORTDATE = DateTime.Now.ToString();
            m.SAMPLINGCOUNT = Request.Params["SAMPLINGCOUNT"];
            m.AUTHENTICATERESULT = Request.Params["AUTHENTICATERESULT"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(PEST_REPORT_DIEPINESURVEYCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 松材线虫病普查报表
        /// <summary>
        /// 松材线虫病普查报表
        /// </summary>
        /// <returns></returns>
        public ActionResult SCXCBPCREPORT()
        {
            pubViewBag("024013", "024013", "松材线虫病普查表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.YEAR = DateTime.Now.ToString("yyyy");
            ViewBag.SEASON = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "111" });
            List<T_SYS_DICTModel> dic112List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "112" }).ToList();
            ViewBag.dic112Value = T_SYS_DICTCls.getDicValueStr(dic112List);
            ViewBag.dic112Count = dic112List.Count;
            ViewBag.Save = (SystemCls.isRight("024013001")) ? 1 : 0;
            ViewBag.Export = (SystemCls.isRight("024013002")) ? 1 : 0;
            return View();
        }

        /// <summary>
        ///松材线虫病普查报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SCXCBPCREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string YEAR = Request.Params["YEAR"];
            string SEASON = Request.Params["SEASON"];
            List<T_SYS_ORGModel> result;
            List<T_SYS_DICTModel> dic112List;
            List<PEST_REPORT_SCXCBPC_Model> _list;
            SCXCBPCREPORTData(BYORGNO, YEAR, SEASON, out result, out dic112List, out _list);
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"SCXCBPCREPORTTable\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style=\"width:4%;\">序号</th><th style=\"width:14%;\">单位名称</th>");
            foreach (var d in dic112List)
            {
                string title = d.DICTNAME;
                if (!string.IsNullOrEmpty(d.STANDBY1))
                    title += "</br>(" + d.STANDBY1 + ")";
                sb.AppendFormat("<th>{0}</th>", title);
            }
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            for (int i = 0; i < result.Count; i++)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"left\" style=\"{2}\" >{0}{1}</td>", result[i].ORGNAME, "<input id=\"txtORGNO" + i + "\" type=\"hidden\" value=\"" + result[i].ORGNO + "\" />", PublicCls.getOrgTDNameClass(BYORGNO, result[i].ORGNO));
                for (int j = 0; j < dic112List.Count; j++)
                {
                    PEST_REPORT_SCXCBPC_Model m = _list.Find(a => a.BYORGNO == result[i].ORGNO && a.SCXCBPCTYPECODE == dic112List[j].DICTVALUE);
                    string value = (m != null && !string.IsNullOrEmpty(m.SCXCBPCVALUE)) ? string.Format("{0:0.00}", float.Parse(m.SCXCBPCVALUE)) : "";
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"txt" + (i.ToString() + j.ToString()) + "\" type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + value + "\" >");
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 松材线虫病普查报表数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult SCXCBPCREPORTManager()
        {
            PEST_REPORT_SCXCBPC_Model m = new PEST_REPORT_SCXCBPC_Model();
            m.TopORGNO = Request.Params["TopORGNO"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.SCXCBPCYEAR = Request.Params["SCXCBPCYEAR"];
            m.SCXCBPCSEASONCODE = Request.Params["SCXCBPCSEASONCODE"];
            m.SCXCBPCTYPECODE = Request.Params["SCXCBPCTYPECODE"];
            m.SCXCBPCVALUE = Request.Params["SCXCBPCVALUE"];
            return Content(JsonConvert.SerializeObject(PEST_REPORT_SCXCBPCCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult SCXCBPCREPORTExportExcel()
        {
            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string YEAR = Request.Params["YEAR"];
            string SEASON = Request.Params["SEASON"];
            List<T_SYS_ORGModel> result;
            List<T_SYS_DICTModel> dic112List;
            List<PEST_REPORT_SCXCBPC_Model> _list;
            SCXCBPCREPORTData(BYORGNO, YEAR, SEASON, out result, out dic112List, out _list);
            int colsCount = dic112List.Count + 1;
            string orgName = T_SYS_ORGCls.getorgname(BYORGNO);
            string seasonName = T_SYS_DICTCls.getDicName(new T_SYS_DICTSW { DICTTYPEID = "111", DICTVALUE = SEASON });
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "024013", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + YEAR + "-" + seasonName + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            for (int i = 0; i < colsCount; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 30 * 256);
                else
                    sheet1.SetColumnWidth(i, 20 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.CreateCell(0).SetCellValue("单位名称");
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            for (int i = 0; i < dic112List.Count; i++)
            {
                int index = i + 1;
                string name = dic112List[i].DICTNAME;
                if (!string.IsNullOrEmpty(dic112List[i].STANDBY1))
                    name += "\n(" + dic112List[i].STANDBY1 + ")";
                rowHead1.CreateCell(index).SetCellValue(name);
                rowHead1.GetCell(index).CellStyle = getCellStyleHead(book);
            }
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount - 1));
            #endregion

            #region 表身及数据
            int rowIndex = 2;
            foreach (var v in result)
            {
                if (PublicCls.OrgIsShi(v.ORGNO)) { }
                else if (PublicCls.OrgIsXian(v.ORGNO)) { v.ORGNAME = "　　" + v.ORGNAME; }
                else { v.ORGNAME = "　　　　" + v.ORGNAME; }
                IRow row = sheet1.CreateRow(rowIndex);
                int x = 0;
                row.CreateCell(x).SetCellValue(v.ORGNAME);
                row.GetCell(x).CellStyle = getCellStyleLeft(book);
                x++;
                for (int j = 0; j < dic112List.Count; j++)
                {
                    PEST_REPORT_SCXCBPC_Model m = _list.Find(a => a.BYORGNO == v.ORGNO && a.SCXCBPCTYPECODE == dic112List[j].DICTVALUE);
                    string value = (m != null && !string.IsNullOrEmpty(m.SCXCBPCVALUE)) ? string.Format("{0:0.00}", float.Parse(m.SCXCBPCVALUE)) : "";
                    row.CreateCell(x).SetCellValue(value);
                    row.GetCell(x).CellStyle = getCellStyleLeft(book);
                    x++;
                }
                rowIndex++;
            }
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

        #region 松材线虫病防治报表
        /// <summary>
        /// 松材线虫病防治报表
        /// </summary>
        /// <returns></returns>
        public ActionResult SCXCBFZREPORT()
        {
            pubViewBag("024014", "024014", "松材线虫病防治表");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.YEAR = DateTime.Now.ToString("yyyy");
            T_SYS_DICTTYPE_Model dictType114 = T_SYS_DICTCls.getTypeModel(new T_SYS_DICTTYPE_SW { DICTTYPEID = "114" });
            ViewBag.DICTTYPEID = "";
            string DICTTValue = "";
            if (dictType114 != null && dictType114.DICTTYPEListModel.Count() > 0)
            {
                ViewBag.DICTTYPEID = T_SYS_DICTCls.getDicTypeValueStr(dictType114.DICTTYPEListModel.ToList());
                for (int x = 0; x < dictType114.DICTTYPEListModel.Count(); x++)
                {
                    if (dictType114.DICTTYPEListModel[x].DICTListModel.Count() > 0)
                        DICTTValue += T_SYS_DICTCls.getDicValueStr(dictType114.DICTTYPEListModel[x].DICTListModel) + ";";
                    else
                        DICTTValue += "0" + ";";
                }
            }
            if (DICTTValue.Length > 0)
                DICTTValue = DICTTValue.Substring(0, DICTTValue.Length - 1);
            ViewBag.DICTTValue = DICTTValue;
            ViewBag.Save = (SystemCls.isRight("024014001")) ? 1 : 0;
            ViewBag.Export = (SystemCls.isRight("024014002")) ? 1 : 0;
            return View();
        }

        /// <summary>
        ///松材线虫病防治报表数据列表--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SCXCBFZREPORTQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string YEAR = Request.Params["YEAR"];
            List<T_SYS_ORGModel> result;
            T_SYS_DICTTYPE_Model dictType114;
            List<PEST_REPORT_SCXCBFZ_Model> _list1;
            List<PEST_REPORT_SCXCBFZMX_Model> _list2;
            SCXCBFZREPORTData(BYORGNO, YEAR, out result, out dictType114, out _list1, out _list2);
            #endregion

            #region 数据表

            sb.AppendFormat("<table id=\"SCXCBFZTable\" cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style=\"width:4%;\" rowspan=\"2\">序号</th>");
            sb.AppendFormat("<th style=\"width:14%;\" rowspan=\"2\">单位名称</th>");
            sb.AppendFormat("<th rowspan=\"2\">发生面积</br>(" + dic113Name + ")</th>");
            sb.AppendFormat("<th rowspan=\"2\">计划防治面积</br>(" + dic113Name + ")</th>");
            sb.AppendFormat("<th rowspan=\"2\">已经防治面积</br>(" + dic113Name + ")</th>");
            if (dictType114 != null && dictType114.DICTTYPEListModel.Count() > 0)
            {
                foreach (var type in dictType114.DICTTYPEListModel)
                {
                    int cols = 0;
                    if (type.DICTListModel.Count() > 0)
                    {
                        cols = type.DICTListModel.Count();
                        sb.AppendFormat("<th colspan=\"{1}\">{0}</th>", type.DICTTYPENAME, cols);
                    }
                    else
                        sb.AppendFormat("<th rowspan=\"2\">{0}</th>", type.DICTTYPENAME);
                }
            }
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            if (dictType114 != null && dictType114.DICTTYPEListModel.Count() > 0)
            {
                foreach (var type in dictType114.DICTTYPEListModel)
                {
                    if (type.DICTListModel.Count() > 0)
                    {
                        foreach (var dc in type.DICTListModel)
                        {
                            string title = dc.DICTNAME;
                            if (!string.IsNullOrEmpty(dc.STANDBY1))
                                title += "<br />(" + dc.STANDBY1 + ")";
                            sb.AppendFormat("<th>{0}</th>", title);
                        }
                    }
                }
            }
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            #endregion

            #region 表身
            sb.AppendFormat("<tbody>");
            for (int i = 0; i < result.Count; i++)
            {
                PEST_REPORT_SCXCBFZ_Model model = _list1.Find(a => a.BYORGNO == result[i].ORGNO);
                string area = (model != null && !string.IsNullOrEmpty(model.SCXCBFZAREA)) ? string.Format("{0:0.00}", float.Parse(model.SCXCBFZAREA)) : "";
                string planarea = (model != null && !string.IsNullOrEmpty(model.SCXCBFZPLANAREA)) ? string.Format("{0:0.00}", float.Parse(model.SCXCBFZPLANAREA)) : "";
                string finisharea = (model != null && !string.IsNullOrEmpty(model.SCXCBFZFINISHAREA)) ? string.Format("{0:0.00}", float.Parse(model.SCXCBFZFINISHAREA)) : "";
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"left\" style=\"{2}\" >{0}{1}</td>", result[i].ORGNAME, "<input id=\"txtORGNO" + i + "\" type=\"hidden\" value=\"" + result[i].ORGNO + "\" />", PublicCls.getOrgTDNameClass(BYORGNO, result[i].ORGNO));
                sb.AppendFormat("<td>{0}</td>", "<input id=\"tbxAREA" + i + "\" type=\"text\" value=\"" + area + "\" class=\"center\"  style=\"width:98%\">");
                sb.AppendFormat("<td>{0}</td>", "<input id=\"tbxPLANAREA" + i + "\" type=\"text\" value=\"" + planarea + "\" class=\"center\" style=\"width:98%\">");
                sb.AppendFormat("<td>{0}</td>", "<input id=\"tbxFINISHAREA" + i + "\" type=\"text\" value=\"" + finisharea + "\" class=\"center\" style=\"width:98%\">");
                if (dictType114 != null && dictType114.DICTTYPEListModel.Count > 0)
                {
                    for (int x = 0; x < dictType114.DICTTYPEListModel.Count; x++)
                    {
                        if (dictType114.DICTTYPEListModel[x].DICTListModel.Count > 0)
                        {
                            for (int y = 0; y < dictType114.DICTTYPEListModel[x].DICTListModel.Count; y++)
                            {
                                string id = i.ToString() + dictType114.DICTTYPEListModel[x].DICTTYPEID + dictType114.DICTTYPEListModel[x].DICTListModel[y].DICTVALUE;
                                string value = "";
                                PEST_REPORT_SCXCBFZMX_Model mx = new PEST_REPORT_SCXCBFZMX_Model();
                                if (model != null)
                                {
                                    mx = _list2.Find(a => a.PEST_REPORT_SCXCBFZID == model.PEST_REPORT_SCXCBFZID
                                        && a.SCXCBFZMXTYPEID == dictType114.DICTTYPEListModel[x].DICTTYPEID
                                    && a.SCXCBFZMXTYPEVALUE == dictType114.DICTTYPEListModel[x].DICTListModel[y].DICTVALUE);
                                    value = (mx != null && !string.IsNullOrEmpty(mx.SCXCBFZMXVARCHAR)) ? mx.SCXCBFZMXVARCHAR : "";
                                }
                                if (dictType114.DICTTYPEListModel[x].DICTListModel[y].DICTVALUE == "1")
                                {
                                    string time = value != "" ? Convert.ToDateTime(value).ToString("yyyy-MM-dd") : "";
                                    sb.AppendFormat("<td>{0}</td>", "<input id=\"tbx" + id + "\" type=\"text\" value=\"" + time + "\" class=\"Wdate\" style=\"width:98%;\" onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd'})\"  />");
                                }
                                else
                                {
                                    value = value != "" ? string.Format("{0:0.00}", float.Parse(value)) : "";
                                    sb.AppendFormat("<td>{0}</td>", "<input id=\"tbx" + id + "\" type=\"text\" value=\"" + value + "\" class=\"center\" style=\"width:98%\">");
                                }
                            }
                        }
                    }
                }
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            #endregion

            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 松材线虫病防治报表数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult SCXCBFZREPORTManager()
        {
            PEST_REPORT_SCXCBFZ_Model m = new PEST_REPORT_SCXCBFZ_Model();
            m.TopORGNO = Request.Params["TopORGNO"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.SCXCBFZYEAR = Request.Params["SCXCBFZYEAR"];
            m.SCXCBFZAREA = Request.Params["SCXCBFZAREA"];
            m.SCXCBFZPLANAREA = Request.Params["SCXCBFZPLANAREA"];
            m.SCXCBFZFINISHAREA = Request.Params["SCXCBFZFINISHAREA"];
            return Content(JsonConvert.SerializeObject(PEST_REPORT_SCXCBFZCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        ///  松材线虫病防治明细数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult SCXCBFZMXREPORTManager()
        {
            PEST_REPORT_SCXCBFZMX_Model m = new PEST_REPORT_SCXCBFZMX_Model();
            m.BYORGNO = Request.Params["ORGNO"];
            m.SCXCBFZYEAR = Request.Params["YEAR"];
            m.SCXCBFZMXTYPEID = Request.Params["SCXCBFZMXTYPEID"];
            m.SCXCBFZMXTYPEVALUE = Request.Params["SCXCBFZMXTYPEVALUE"];
            m.SCXCBFZMXVARCHAR = Request.Params["SCXCBFZMXVARCHAR"];
            return Content(JsonConvert.SerializeObject(PEST_REPORT_SCXCBFZMXCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult SCXCBFZREPORTExportExcel()
        {
            #region 数据准备
            string BYORGNO = Request.Params["ORGNO"];
            string YEAR = Request.Params["YEAR"];
            List<T_SYS_ORGModel> result;
            T_SYS_DICTTYPE_Model dictType114;
            List<PEST_REPORT_SCXCBFZ_Model> _list1;
            List<PEST_REPORT_SCXCBFZMX_Model> _list2;
            SCXCBFZREPORTData(BYORGNO, YEAR, out result, out dictType114, out _list1, out _list2);
            int colsCount = 4;
            if (dictType114 != null && dictType114.DICTTYPEListModel.Count() > 0)
            {
                foreach (var type in dictType114.DICTTYPEListModel)
                {
                    colsCount += type.DICTListModel.Count;
                }
            }
            string orgName = T_SYS_ORGCls.getorgname(BYORGNO);
            string menuName = T_SYS_MENUCls.getMenuNameByCode(new T_SYS_MENU_SW { MENUCODE = "024014", SYSFLAG = ConfigCls.getSystemFlag() });
            string title = orgName + "-" + YEAR + "-" + menuName;
            #endregion

            #region 写入Excel工作簿
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 

            #region 表头及格式
            for (int i = 0; i < colsCount; i++)
            {
                if (i == 0)
                    sheet1.SetColumnWidth(i, 30 * 256);
                else
                    sheet1.SetColumnWidth(i, 15 * 256);
            }
            IRow rowTitle = sheet1.CreateRow(0);
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            IRow rowHead1 = sheet1.CreateRow(1);
            rowHead1.CreateCell(0).SetCellValue("单位名称");
            rowHead1.GetCell(0).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(1).SetCellValue("发生面积\n(" + dic113Name + ")");
            rowHead1.GetCell(1).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(2).SetCellValue("计划防治面积\n(" + dic113Name + ")");
            rowHead1.GetCell(2).CellStyle = getCellStyleHead(book);
            rowHead1.CreateCell(3).SetCellValue("已经防治面积\n(" + dic113Name + ")");
            rowHead1.GetCell(3).CellStyle = getCellStyleHead(book);
            int x = 4;
            if (dictType114 != null && dictType114.DICTTYPEListModel.Count() > 0)
            {
                foreach (var type in dictType114.DICTTYPEListModel)
                {
                    rowHead1.CreateCell(x).SetCellValue(type.DICTTYPENAME);
                    rowHead1.GetCell(x).CellStyle = getCellStyleHead(book);
                    x += type.DICTListModel.Count();
                }
            }
            IRow rowHead2 = sheet1.CreateRow(2);
            int y = 4;
            if (dictType114 != null && dictType114.DICTTYPEListModel.Count() > 0)
            {
                foreach (var type in dictType114.DICTTYPEListModel)
                {
                    if (type.DICTListModel.Count() > 0)
                    {
                        foreach (var dc in type.DICTListModel)
                        {
                            string name = dc.DICTNAME;
                            if (!string.IsNullOrEmpty(dc.STANDBY1))
                                name += "\n(" + dc.STANDBY1 + ")";
                            rowHead2.CreateCell(y).SetCellValue(name);
                            rowHead2.GetCell(y).CellStyle = getCellStyleHead(book);
                            y++;
                        }
                    }
                }
            }
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount - 1));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 0, 0));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 1, 1));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 2, 2));
            sheet1.AddMergedRegion(new CellRangeAddress(1, 2, 3, 3));
            if (dictType114 != null && dictType114.DICTTYPEListModel.Count() > 0)
            {
                int index = 4;
                for (int i = 0; i < dictType114.DICTTYPEListModel.Count(); i++)
                {
                    sheet1.AddMergedRegion(new CellRangeAddress(1, 1, index, index + dictType114.DICTTYPEListModel[i].DICTListModel.Count - 1));
                    index += dictType114.DICTTYPEListModel[i].DICTListModel.Count;
                }
            }
            #endregion

            #region 表身及数据
            int rowIndex = 3;
            for (int v = 0; v < result.Count; v++)
            {
                if (PublicCls.OrgIsShi(result[v].ORGNO)) { }
                else if (PublicCls.OrgIsXian(result[v].ORGNO)) { result[v].ORGNAME = "　　" + result[v].ORGNAME; }
                else { result[v].ORGNAME = "　　　　" + result[v].ORGNAME; }
                PEST_REPORT_SCXCBFZ_Model model = _list1.Find(a => a.BYORGNO == result[v].ORGNO);
                string area = (model != null && !string.IsNullOrEmpty(model.SCXCBFZAREA)) ? string.Format("{0:0.00}", float.Parse(model.SCXCBFZAREA)) : "";
                string planarea = (model != null && !string.IsNullOrEmpty(model.SCXCBFZPLANAREA)) ? string.Format("{0:0.00}", float.Parse(model.SCXCBFZPLANAREA)) : "";
                string finisharea = (model != null && !string.IsNullOrEmpty(model.SCXCBFZFINISHAREA)) ? string.Format("{0:0.00}", float.Parse(model.SCXCBFZFINISHAREA)) : "";
                IRow row = sheet1.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(result[v].ORGNAME);
                row.GetCell(0).CellStyle = getCellStyleLeft(book);
                row.CreateCell(1).SetCellValue(area);
                row.GetCell(1).CellStyle = getCellStyleCenter(book);
                row.CreateCell(2).SetCellValue(planarea);
                row.GetCell(2).CellStyle = getCellStyleCenter(book);
                row.CreateCell(3).SetCellValue(finisharea);
                row.GetCell(3).CellStyle = getCellStyleCenter(book);
                int m = 4;
                if (dictType114 != null && dictType114.DICTTYPEListModel.Count > 0)
                {
                    for (int i = 0; i < dictType114.DICTTYPEListModel.Count; i++)
                    {
                        if (dictType114.DICTTYPEListModel[i].DICTListModel.Count > 0)
                        {
                            for (int j = 0; j < dictType114.DICTTYPEListModel[i].DICTListModel.Count; j++)
                            {
                                PEST_REPORT_SCXCBFZMX_Model mx = new PEST_REPORT_SCXCBFZMX_Model();
                                string value = "";
                                if (model != null)
                                {
                                    mx = _list2.Find(a => a.PEST_REPORT_SCXCBFZID == model.PEST_REPORT_SCXCBFZID
                                        && a.SCXCBFZMXTYPEID == dictType114.DICTTYPEListModel[i].DICTTYPEID
                                    && a.SCXCBFZMXTYPEVALUE == dictType114.DICTTYPEListModel[i].DICTListModel[j].DICTVALUE);
                                    value = (mx != null && !string.IsNullOrEmpty(mx.SCXCBFZMXVARCHAR)) ? mx.SCXCBFZMXVARCHAR : "";
                                }
                                if (dictType114.DICTTYPEListModel[i].DICTListModel[j].DICTVALUE == "1")
                                {
                                    string time = value != "" ? Convert.ToDateTime(value).ToString("yyyy-MM-dd") : "";
                                    row.CreateCell(m).SetCellValue(time);
                                    m++;
                                }
                                else
                                {
                                    value = value != "" ? string.Format("{0:0.00}", float.Parse(value)) : "";
                                    row.CreateCell(m).SetCellValue(value);
                                    m++;
                                }
                            }
                        }
                    }
                }
                rowIndex++;
            }

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

        #region 监测点
        /// <summary>
        /// 监测点
        /// </summary>
        /// <returns></returns>
        public ActionResult MonitoringStation()
        {
            pubViewBag("023002", "023002", "监测点");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.TransfermodeType = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "119", isShowAll = "1" });
            ViewBag.TransfermodeTypeAdd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "119" });
            ViewBag.UsesSate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "120", isShowAll = "1" });
            ViewBag.UsesSateAdd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "120" });
            ViewBag.ManagerStateAdd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "121" });
            ViewBag.IsAdd = (SystemCls.isRight("023002001")) ? "1" : "0";
            return View();
        }

        /// <summary>
        /// 获取监测点列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMonitoringStationList()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            string BYORGNO = Request.Params["BYORGNO"];
            string TRANSFERMODETYPE = Request.Params["TRANSFERMODETYPE"];
            string USESTATE = Request.Params["USESTATE"];
            string NAME = Request.Params["NAME"];
            bool IsLocate = SystemCls.isRight("023002005") ? true : false;
            bool IsView = SystemCls.isRight("023002003") ? true : false;
            bool IsMdy = SystemCls.isRight("023002002") ? true : false;
            bool IsDel = SystemCls.isRight("023002004") ? true : false;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th><th>所属市县</th><th>所属乡镇</th><th>编号</th><th>名称</th><th>型号</th><th>监测内容</th>");
            sb.AppendFormat("<th>传输方式</th><th>使用现状</th><th>维护类型</th><th>建成日期</th><th>总价(万元)</th><th>操作</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int total = 0;
            var result = PEST_MONITORINGSTATIONCls.getListModel(new PEST_MONITORINGSTATION_SW
            {
                CurPage = int.Parse(Page),
                PageSize = int.Parse(PageSize),
                BYORGNO = BYORGNO,
                TRANSFERMODETYPE = TRANSFERMODETYPE,
                USESTATE = USESTATE,
                NAME = NAME
            }, out total);
            int i = 0;
            foreach (var s in result)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGXSName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.MODEL);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.MONICONTENT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.TRANSFERMODETYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.USESTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.MANAGERSTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.BUILDDATE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.WORTH);
                sb.AppendFormat("<td class=\" \">");
                if (IsLocate)
                {
                    if (string.IsNullOrEmpty(s.JD))
                        sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                    else
                        sb.AppendFormat("<a href=\"#\" onclick=\" Position('PEST_MONITORINGSTATION','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.PEST_MONITORINGSTATIONID, s.NAME);
                }
                if (IsView)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}','{1}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", s.PEST_MONITORINGSTATIONID, "");
                if (IsMdy)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", s.PEST_MONITORINGSTATIONID, Page);
                if (IsDel)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.PEST_MONITORINGSTATIONID, Page);
                sb.AppendFormat(" </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取单个监测点
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMonitoringStationJson()
        {
            string PEST_MONITORINGSTATIONID = Request.Params["PEST_MONITORINGSTATIONID"];
            return Content(JsonConvert.SerializeObject(PEST_MONITORINGSTATIONCls.getModel(new PEST_MONITORINGSTATION_SW { PEST_MONITORINGSTATIONID = PEST_MONITORINGSTATIONID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查看监测点
        /// </summary>
        /// <returns></returns>
        public ActionResult MonitoringStationDataSee()
        {
            string PEST_MONITORINGSTATIONID = Request.Params["PEST_MONITORINGSTATIONID"];
            PEST_MONITORINGSTATION_Model m = PEST_MONITORINGSTATIONCls.getModel(new PEST_MONITORINGSTATION_SW { PEST_MONITORINGSTATIONID = PEST_MONITORINGSTATIONID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td style=\"width:15%\">单位:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ORGXSName != "--" ? m.ORGName + "--" + m.ORGXSName : m.ORGName);
            sb.AppendFormat("<td style=\"width:15%\">无线电传输方式:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.TRANSFERMODETYPEName);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>编号:</td>");
            sb.AppendFormat("<td>{0}</td>", m.NUMBER);
            sb.AppendFormat("<td>名称:</td>");
            sb.AppendFormat("<td>{0}</td>", m.NAME);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>型号:</td>");
            sb.AppendFormat("<td>{0}</td>", m.MODEL);
            sb.AppendFormat("<td>监测内容:</td>");
            sb.AppendFormat("<td>{0}</td>", m.MONICONTENT);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>使用现状:</td>");
            sb.AppendFormat("<td>{0}</td>", m.USESTATEName);
            sb.AppendFormat("<td>维护管理:</td>");
            sb.AppendFormat("<td>{0}</td>", m.MANAGERSTATEName);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>经度:</td>");
            sb.AppendFormat("<td >{0}</td>", m.JD);
            sb.AppendFormat("<td>经度:</td>");
            sb.AppendFormat("<td >{0}</td>", m.JD);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>建设日期:</td>");
            sb.AppendFormat("<td>{0}</td>", m.BUILDDATE);
            sb.AppendFormat("<td>总价:</td>");
            sb.AppendFormat("<td>{0}</td>", !string.IsNullOrEmpty(m.WORTH) ? m.WORTH + "万元" : m.WORTH);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>地址:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.ADDRESS);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>备注:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 监测点数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult MonitoringStationManager()
        {
            #region 监测点
            PEST_MONITORINGSTATION_Model m = new PEST_MONITORINGSTATION_Model();
            m.PEST_MONITORINGSTATIONID = Request.Params["PEST_MONITORINGSTATIONID"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.TRANSFERMODETYPE = Request.Params["TRANSFERMODETYPE"];
            m.NUMBER = Request.Params["NUMBER"];
            m.NAME = Request.Params["NAME"];
            m.MODEL = Request.Params["MODEL"];
            m.MONICONTENT = Request.Params["MONICONTENT"];
            m.USESTATE = Request.Params["USESTATE"];
            m.MANAGERSTATE = Request.Params["MANAGERSTATE"];
            m.BUILDDATE = Request.Params["BUILDDATE"];
            m.WORTH = Request.Params["WORTH"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.ADDRESS = Request.Params["ADDRESS"];
            m.MARK = Request.Params["MARK"];
            m.opMethod = Request.Params["Method"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间,请重新定位!", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间,请重新定位!", "")), "text/html;charset=UTF-8");
                }
            }
            var ms = PEST_MONITORINGSTATIONCls.Manager(m);
            if (ms.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, ms.Msg, "")), "text/html;charset=UTF-8");
            #endregion

            #region 三维--监测点
            ManagerSystemModel.SDEModel.YHSWJCD_Model m1 = new ManagerSystemModel.SDEModel.YHSWJCD_Model();
            if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
            {
                double[] arr = ClsPositionTrans.GpsTransform(double.Parse(m.WD), double.Parse(m.JD), ConfigCls.getSDELonLatTransform());
                m1.WD = arr[0].ToString();
                m1.JD = arr[1].ToString();
                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
            }
            m1.NAME = m.NAME;
            m1.ADDRESS = m.ADDRESS;
            m1.opMethod = m.opMethod;
            if (m.opMethod != "Del")
                m1.OBJECTID = ms.Url;
            else
                m1.OBJECTID = m.PEST_MONITORINGSTATIONID;
            if ((string.IsNullOrEmpty(m.JD) || string.IsNullOrEmpty(m.WD)) && m1.opMethod == "Add") { }
            else { YHSWJCDCls.Manager(m1); }

            #endregion

            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        #endregion

        #region 专家会诊
        /// <summary>
        /// 专家会诊
        /// </summary>
        /// <returns></returns>
        public ActionResult Consul()
        {
            pubViewBag("023003", "023003", "专家会诊");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.StartTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            ViewBag.EndTime = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.IsAdd = (SystemCls.isRight("023003001")) ? "1" : "0";
            return View();
        }

        /// <summary>
        /// 获取专家会诊主题列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetConsulTationList()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            string TITLE = Request.Params["TITLE"];
            string StartTime = Request.Params["StartTime"];
            string EndTime = Request.Params["EndTime"];
            bool IsReply = SystemCls.isRight("023003005") ? true : false;
            bool IsView = SystemCls.isRight("023003002") ? true : false;
            bool IsMdy = SystemCls.isRight("023003003") ? true : false;
            bool IsDel = SystemCls.isRight("023003004") ? true : false;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>主题</th><th>提问人手机号码</th><th>提问时间</th><th>内容</th><th>已回复</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int total = 0;
            var result = PEST_CONSULTATIONCls.getListModel(new PEST_CONSULTATION_SW
            {
                CurPage = int.Parse(Page),
                PageSize = int.Parse(PageSize),
                CONSULTITLE = TITLE,
                CONSULSTARTTIME = StartTime,
                CONSULENDTIME = EndTime
            }, out total);
            int i = 0;
            foreach (var s in result)
            {
                var ReplyList = PEST_CONSULREPLYCls.getListModel(new PEST_CONSULREPLY_SW { PEST_CONSULTATIONID = s.PEST_CONSULTATIONID });
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.CONSULTITLE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.CONSULPHONE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.CONSULTIME);
                sb.AppendFormat("<td class=\"left\">{0}</td>", !string.IsNullOrEmpty(s.CONSULCONTENT) && s.CONSULCONTENT.Length > 10 ? s.CONSULCONTENT.Substring(0, 10) + "......" : s.CONSULCONTENT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", ReplyList.Count());
                sb.AppendFormat("<td class=\" \">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Photo('{0}','{1}')\" title='图片' class=\"searchBox_01 LinkPhoto\">图片</a>", "PEST_CONSULTATION", s.PEST_CONSULTATIONID);
                if (IsReply)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Relay','{0}','{1}')\" title='回复' class=\"searchBox_01 LinkMdy\">回复</a>", s.PEST_CONSULTATIONID, Page);
                if (IsView)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}','{1}')\"  title='查看' class=\"searchBox_01 LinkSee\">查看</a>", s.PEST_CONSULTATIONID, "");
                if (IsMdy)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", s.PEST_CONSULTATIONID, Page);
                if (IsDel)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.PEST_CONSULTATIONID, Page);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查看会诊信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsulDataSee()
        {
            string PEST_CONSULTATIONID = Request.Params["PEST_CONSULTATIONID"];
            PEST_CONSULTATION_Model model = PEST_CONSULTATIONCls.getModel(new PEST_CONSULTATION_SW { PEST_CONSULTATIONID = PEST_CONSULTATIONID });
            var _list = PEST_CONSULREPLYCls.getListModel(new PEST_CONSULREPLY_SW { PEST_CONSULTATIONID = PEST_CONSULTATIONID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table id=\"TationTable\" cellpadding=\"0\" cellspacing=\"0\" style=\"height:280px\">");
            sb.AppendFormat("<thead><tr><th colspan=\"4\">会诊信息</th></tr></thead>");
            sb.AppendFormat("<tbody>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td style=\"width:20%;\">主题: </td>");
            sb.AppendFormat("<td class=\"left\" style=\"width:40%;\">{0}</td>", model.CONSULTITLE);
            sb.AppendFormat("<td colspan=\"2\" rowspan=\"4\">");
            sb.AppendFormat("<div class=\"easyui-layout\" data-options=\"fit:true\" style=\"border:0;\">");
            sb.AppendFormat("<div id=\"divImg\" data-options=\"region:'center'\" title=\"\" class=\"LayoutCenterBG\">");
            sb.AppendFormat("</div>");
            sb.AppendFormat("<div data-options=\"region:'south'\" title=\"\" style=\"height:35px; border: none; overflow:hidden; text-align:center;\">");
            sb.AppendFormat("<div class=\"divOP\">");
            sb.AppendFormat("<input type=\"button\" value=\"上一张\" onclick=\"ManagerPhoto('Up')\" id=\"btnUp\" style=\"display:none;\" class=\"btnLastsealCss\" />");
            sb.AppendFormat("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.AppendFormat("<input type=\"button\" value=\"下一张\" onclick=\"ManagerPhoto('Down')\" id=\"btnDown\" style=\"display:none;\" class=\"btnNextsealCss\" />");
            sb.AppendFormat("</div>");
            sb.AppendFormat("</div>");
            sb.AppendFormat("</div>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>内容:</td>");
            sb.AppendFormat("<td class=\"left\">{0}</td>", model.CONSULCONTENT);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>提问人手机号码:</td>");
            sb.AppendFormat("<td>{0}</td>", model.CONSULPHONE);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>提问时间:</td>");
            sb.AppendFormat("<td>{0}</td>", model.CONSULTIME);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("<br />");
            sb.AppendFormat("<table id=\"RelayTable\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th colspan=\"4\">专家回复列表</th></tr>");
            sb.AppendFormat("<tr><th style=\"width:5%;\">序号</th><th style=\"width:10%;\">回复人</th><th style=\"width:20%;\">回复时间</th><th style=\"width:65%;\">回复内容</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 1;
            foreach (var v in _list)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.REPLYUSERANME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.REPLYTIME);
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.REPLYCONTENT);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            ViewBag.HidTable = "PEST_CONSULTATION";
            ViewBag.PEST_CONSULTATIONID = PEST_CONSULTATIONID;
            return View();
        }

        /// <summary>
        /// 获取单条会诊信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetConsulDataJson()
        {
            string PEST_CONSULTATIONID = Request.Params["PEST_CONSULTATIONID"];
            return Content(JsonConvert.SerializeObject(PEST_CONSULTATIONCls.getModel(new PEST_CONSULTATION_SW { PEST_CONSULTATIONID = PEST_CONSULTATIONID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 专家会诊主题数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsulTationManager()
        {
            PEST_CONSULTATION_Model m = new PEST_CONSULTATION_Model();
            m.PEST_CONSULTATIONID = Request.Params["PEST_CONSULTATIONID"];
            m.CONSULTITLE = Request.Params["CONSULTITLE"];
            m.CONSULPHONE = Request.Params["CONSULPHONE"];
            m.CONSULCONTENT = Request.Params["CONSULCONTENT"];
            m.opMethod = Request.Params["Method"];
            if (m.opMethod == "Add")
            {
                m.CONSULTIME = DateTime.Now.ToString();
            }
            return Content(JsonConvert.SerializeObject(PEST_CONSULTATIONCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 会诊回复
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsulRealy()
        {
            string PEST_CONSULTATIONID = Request.Params["PEST_CONSULTATIONID"];
            PEST_CONSULTATION_Model model = new PEST_CONSULTATION_Model();
            model = PEST_CONSULTATIONCls.getModel(new PEST_CONSULTATION_SW { PEST_CONSULTATIONID = PEST_CONSULTATIONID });
            ViewBag.HidTable = "PEST_CONSULTATION";
            return View(model);
        }

        /// <summary>
        /// 获取专家会诊图片
        /// </summary>
        /// <returns></returns>
        public ActionResult GetConsulPhoto()
        {
            string PHOTOTYPE = Request.Params["PHOTOTYPE"];
            string PRID = Request.Params["PRID"];
            List<PEST_PHOTO_Model> modelList = PEST_PHOTOCls.getModelList(new PEST_PHOTO_SW { PHOTOTYPE = PHOTOTYPE, PRID = PRID }).ToList();
            StringBuilder sb = new StringBuilder();
            if (modelList.Count > 0)
            {
                PEST_PHOTO_Model m = modelList[0];
                sb.AppendFormat("<a href=\"{0}\" target=\"_blank\">", m.PHOTOFILENAME);
                sb.AppendFormat("<img src=\"{0}\" alt=\"alttext\" title=\"{1}\" style=\"width:100%;height:95%\" />", m.PHOTOFILENAME, m.PHOTOTITLE);
                sb.AppendFormat("<input id=\"HidPHOTOID\" name=\"HidPHOTOID\" type=\"hidden\" value=\"{0}\" />", m.PEST_PHOTOID);
                sb.AppendFormat("</a>");
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), modelList.Count.ToString())), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 上一张、下一站图片
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsulPhotoManager()
        {
            string PHOTOTYPE = Request.Params["PHOTOTYPE"];
            string PRID = Request.Params["PRID"];
            string PEST_PHOTOID = Request.Params["PEST_PHOTOID"];
            string Method = Request.Params["Method"];
            List<PEST_PHOTO_Model> modelList = PEST_PHOTOCls.getModelList(new PEST_PHOTO_SW { PHOTOTYPE = PHOTOTYPE, PRID = PRID }).ToList();
            StringBuilder sb = new StringBuilder();
            if (modelList.Count > 0)
            {
                int index = modelList.FindIndex(a => a.PEST_PHOTOID == PEST_PHOTOID);
                if (Method == "Up")
                {
                    index++;
                    if (index >= modelList.Count)
                        index = 0;
                }
                if (Method == "Down")
                {
                    index--;
                    if (index < 0)
                        index = modelList.Count - 1;
                }
                PEST_PHOTO_Model m = modelList[index];
                sb.AppendFormat("<a href=\"{0}\" target=\"_blank\">", m.PHOTOFILENAME);
                sb.AppendFormat("<img src=\"{0}\" alt=\"alttext\" title=\"{1}\" style=\"width:100%;height:95%\" />", m.PHOTOFILENAME, m.PHOTOTITLE);
                sb.AppendFormat("<input id=\"HidPHOTOID\" name=\"HidPHOTOID\" type=\"hidden\" value=\"{0}\" />", m.PEST_PHOTOID);
                sb.AppendFormat("</a>");
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), modelList.Count.ToString())), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取专家回复列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetConsulRealyList()
        {
            StringBuilder sb = new StringBuilder();
            string PEST_CONSULTATIONID = Request.Params["PEST_CONSULTATIONID"];
            var _list = PEST_CONSULREPLYCls.getListModel(new PEST_CONSULREPLY_SW { PEST_CONSULTATIONID = PEST_CONSULTATIONID });
            int i = 1;
            foreach (var v in _list)
            {
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.REPLYUSERANME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.REPLYTIME);
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.REPLYCONTENT);
                sb.AppendFormat("</tr>");
                i++;
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 专家会诊回复数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsulRepalyManager()
        {
            PEST_CONSULREPLY_Model m = new PEST_CONSULREPLY_Model();
            CookieModel cookieInfo = SystemCls.getCookieInfo();
            m.PEST_CONSULREPLYID = Request.Params["PEST_CONSULREPLYID"];
            m.PEST_CONSULTATIONID = Request.Params["PEST_CONSULTATIONID"];
            m.REPLYUID = cookieInfo.UID;
            m.REPLYTIME = DateTime.Now.ToString();
            m.REPLYCONTENT = Request.Params["REPLYCONTENT"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(PEST_CONSULREPLYCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 远程诊断
        /// <summary>
        /// 远程诊断
        /// </summary>
        /// <returns></returns>
        public ActionResult RemoteDiagn()
        {
            pubViewBag("023004", "023004", "远程诊断");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.DIAGNSTATUS = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "122", isShowAll = "1" });
            ViewBag.DIAGNSTATUSAdd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "122" });
            ViewBag.StartTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            ViewBag.EndTime = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.IsAdd = (SystemCls.isRight("023004001")) ? "1" : "0";
            return View();
        }

        /// <summary>
        /// 获取远程诊断列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRemoteDiagnList()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            string TITLE = Request.Params["TITLE"];
            string STATUS = Request.Params["STATUS"];
            string StartTime = Request.Params["StartTime"];
            string EndTime = Request.Params["EndTime"];
            bool IsDign = SystemCls.isRight("023004005") ? true : false;
            bool IsView = SystemCls.isRight("023004002") ? true : false;
            bool IsMdy = SystemCls.isRight("023004003") ? true : false;
            bool IsDel = SystemCls.isRight("023004004") ? true : false;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>主题</th><th>求诊时间</th><th>内容</th><th>诊断状态</th><th>发起人</th><th>诊断时间</th><th>诊断结论</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int total = 0;
            var result = PEST_REMOTEDIAGNCls.getListModel(new PEST_REMOTEDIAGN_SW
            {
                CurPage = int.Parse(Page),
                PageSize = int.Parse(PageSize),
                DIAGNTITLE = TITLE,
                DIAGNSTATUS = STATUS,
                DIAGNSTARTTIME = StartTime,
                DIAGNENDTIME = EndTime
            }, out total);
            int i = 0;
            foreach (var s in result)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.DIAGNTITLE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.DIAGNTIME);
                sb.AppendFormat("<td class=\"left\">{0}</td>", (!string.IsNullOrEmpty(s.DIAGNCONTENT) && s.DIAGNCONTENT.Length > 10) ? s.DIAGNCONTENT.Substring(0, 10) + "......" : s.DIAGNCONTENT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.DIAGNSTATUSName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.DIAGNSPONSERNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.DIAGNSPONSERTIME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", (!string.IsNullOrEmpty(s.DIAGNRESULT) && s.DIAGNRESULT.Length > 10) ? s.DIAGNRESULT.Substring(0, 10) + "......" : s.DIAGNRESULT);
                sb.AppendFormat("<td class=\" \">");
                if (IsDign)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Dign','{0}','{1}')\" title='诊断' class=\"searchBox_01 LinkMdy\">诊断</a>", s.PEST_REMOTEDIAGNID, Page);
                if (IsView)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}','{1}')\"  title='查看' class=\"searchBox_01 LinkSee\">查看</a>", s.PEST_REMOTEDIAGNID, "");
                if (IsMdy)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", s.PEST_REMOTEDIAGNID, Page);
                if (IsDel)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.PEST_REMOTEDIAGNID, Page);
                sb.AppendFormat(" </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 远程诊断信息
        /// </summary>
        /// <returns></returns>
        public ActionResult RemoteDiagnDataSee()
        {
            string PEST_REMOTEDIAGNID = Request.Params["PEST_REMOTEDIAGNID"];
            PEST_REMOTEDIAGN_Model model = PEST_REMOTEDIAGNCls.getModel(new PEST_REMOTEDIAGN_SW { PEST_REMOTEDIAGNID = PEST_REMOTEDIAGNID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table id=\"TationTable\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td style=\"width:15%;\">主题: </td>");
            sb.AppendFormat("<td style=\"width:35%;\" class=\"left\">{0}</td>", model.DIAGNTITLE);
            sb.AppendFormat("<td style=\"width:15%;\">诊断状态: </td>");
            sb.AppendFormat("<td style=\"width:35%;\" class=\"center\">{0}</td>", model.DIAGNSTATUSName);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>参与专家:</td>");
            sb.AppendFormat("<td colspan=\"3\" class=\"left\">{0}</td>", model.DIAGNEXPERTS);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>内容:</td>");
            sb.AppendFormat("<td colspan=\"3\" class=\"left\">{0}</td>", model.DIAGNCONTENT);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>诊断发起人: </td>");
            sb.AppendFormat("<td>{0}</td>", model.DIAGNSPONSERNAME);
            sb.AppendFormat("<td>诊断时间: </td>");
            sb.AppendFormat("<td>{0}</td>", model.DIAGNSPONSERTIME);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>诊断结论:</td>");
            sb.AppendFormat("<td colspan=\"3\" class=\"left\">{0}</td>", model.DIAGNRESULT);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 获取单条远程诊断信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRemoteDiagnDataJson()
        {
            string PEST_REMOTEDIAGNID = Request.Params["PEST_REMOTEDIAGNID"];
            return Content(JsonConvert.SerializeObject(PEST_REMOTEDIAGNCls.getModel(new PEST_REMOTEDIAGN_SW { PEST_REMOTEDIAGNID = PEST_REMOTEDIAGNID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 远程诊断数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult RemoteDiagnManager()
        {
            PEST_REMOTEDIAGN_Model m = new PEST_REMOTEDIAGN_Model();
            m.PEST_REMOTEDIAGNID = Request.Params["PEST_REMOTEDIAGNID"];
            m.DIAGNTITLE = Request.Params["DIAGNTITLE"];
            m.DIAGNCONTENT = Request.Params["DIAGNCONTENT"];
            m.DIAGNEXPERTS = Request.Params["DIAGNEXPERTS"];
            m.DIAGNSTATUS = Request.Params["DIAGNSTATUS"];
            m.DIAGNRESULT = Request.Params["DIAGNRESULT"];
            m.opMethod = Request.Params["Method"];
            if (m.opMethod == "Add")
                m.DIAGNTIME = DateTime.Now.ToString();
            if (m.opMethod == "MdyZT")
            {
                CookieModel cookieInfo = SystemCls.getCookieInfo();
                m.DIAGNSPONSERUID = cookieInfo.UID;
                m.DIAGNSPONSERTIME = DateTime.Now.ToString();
            }
            return Content(JsonConvert.SerializeObject(PEST_REMOTEDIAGNCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 有害生物照片管理
        /// <summary>
        /// 有害生物图片
        /// </summary>
        /// <returns></returns>
        public ActionResult PESTPhoto()
        {
            string TableName = Request.Params["TableName"];
            string DataId = Request.Params["DataId"];
            ViewBag.TableName = TableName;
            ViewBag.DataId = DataId;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead><tr><th>序号</th><th>照片标题</th><th>照片说明</th><th>缩略图</th><th>操作</th></tr></thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = PEST_PHOTOCls.getModelList(new PEST_PHOTO_SW { PHOTOTYPE = TableName, PRID = DataId });
            foreach (var s in result)
            {
                sb.AppendFormat("<tr class=\"{4}\" onclick=\"ShowValues('{0}','{1}','{2}','{3}')\">", s.PEST_PHOTOID, s.PHOTOTITLE, s.PHOTOEXPLAIN, s.PHOTOFILENAME, (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PHOTOTITLE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PHOTOEXPLAIN);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{0}\" alt=\"alttext\" title=\"{1}\" height =\"35px\" wideth=\"35px\"/></a>", s.PHOTOFILENAME, s.PHOTOTITLE);
                sb.AppendFormat("</td>");
                sb.AppendFormat("<td class=\"center\"></td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("<tfoot>");
            sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
            sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"PHOTOTITLE\" type=\"text\"   style=\"width:99%;\" />");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"PHOTOEXPLAIN\" type=\"text\"  style=\"width:99%;\" />");
            sb.AppendFormat("<td class=\"center\"></td>");
            sb.AppendFormat("<td>");
            sb.AppendFormat("{0}", "<input id=\"PEST_PHOTOID\" type=\"hidden\" />");
            sb.AppendFormat("{0}", "<input id=\"PHOTOFILENAME\" type=\"hidden\" />");
            sb.AppendFormat("{0}", "<input id=\"PHOTOTYPE\" type=\"hidden\" value=\"" + TableName + "\" />");
            sb.AppendFormat("{0}", "<input id=\"PRID\" type=\"hidden\"  value=\"" + DataId + "\" />");
            sb.AppendFormat("{0}", "<form id=\"uploadForm\" enctype=\"multipart/form-data\">");
            sb.AppendFormat("{0}", "<input type=\"file\" id=\"attachment\" name=\"attachment\"  style=\"width:180px;\" />");
            sb.AppendFormat("{0}", "<input type=\"button\" id=\"btnPhotoAdd\" value=\"新增\" onclick=\"PESTPhotoUpload('Add')\" class=\"btnAddCss\" />");
            sb.AppendFormat("{0}", "<input type=\"button\" id=\"btnPhotoMdy\" value=\"修改\" style=\"display:none;\"  onclick=\"PESTPhotoUpload('Mdy')\" class=\"btnMdyCss\" />");
            sb.AppendFormat("{0}", "<input type=\"button\" id=\"btnPhotoDel\" value=\"删除\" style=\"display:none;\"  onclick=\"PESTPhotoManager('Del')\" class=\"btnDelCss\" />");
            sb.AppendFormat("{0}", "</form>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tfoot>");
            sb.AppendFormat("</table>");
            ViewBag.PESTPhoto = sb.ToString();
            return View();
        }

        /// <summary>
        /// 有害生物图片上传
        /// </summary>
        /// <returns></returns>
        public JsonResult PESTPhotoUpload()
        {
            string PEST_PHOTOID = Request.Params["PEST_PHOTOID"];
            string PHOTOTITLE = Request.Params["PHOTOTITLE"];
            string PHOTOEXPLAIN = Request.Params["PHOTOEXPLAIN"];
            string PHOTOTYPE = Request.Params["PHOTOTYPE"];
            string PRID = Request.Params["PRID"];
            string Method = Request.Params["Method"];
            Message ms = null;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string[] arr = hfc[0].FileName.Split('.');
            string type = arr[arr.Length - 1].ToLower();
            if (Method == "Mdy" && string.IsNullOrEmpty(hfc[0].FileName))
            {
                PEST_PHOTO_Model m = new PEST_PHOTO_Model();
                m.PEST_PHOTOID = PEST_PHOTOID;
                m.PHOTOTITLE = PHOTOTITLE;
                m.PHOTOEXPLAIN = PHOTOEXPLAIN;
                m.PHOTOTIME = DateTime.Now.ToString();
                m.PHOTOTYPE = PHOTOTYPE;
                m.PRID = PRID;
                m.opMethod = "MdyTP";
                ms = PEST_PHOTOCls.Manager(m);
            }
            else
            {
                if (string.IsNullOrEmpty(PHOTOTITLE))
                    return Json(new Message(false, "请输入照片名称!", ""));
                if (string.IsNullOrEmpty(hfc[0].FileName))
                    return Json(new Message(false, "请选择上传图片!", ""));
                if (type != "jpg" && type != "jpeg" && type != "bmp" && type != "gif" && type != "png")
                    return Json(new Message(false, "禁止上传非图片文件!", ""));
                if (hfc.Count > 0)
                {
                    string ipath = "~/UploadFile/DCPhoto/";//相对路径
                    string PhyPath = Server.MapPath(ipath);
                    if (!Directory.Exists(PhyPath))//判断文件夹是否已经存在
                        Directory.CreateDirectory(PhyPath);//创建文件夹
                    PEST_PHOTO_Model m = new PEST_PHOTO_Model();
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        m.PEST_PHOTOID = PEST_PHOTOID;
                        m.PHOTOTITLE = PHOTOTITLE;
                        m.PHOTOEXPLAIN = PHOTOEXPLAIN;
                        m.PHOTOTIME = DateTime.Now.ToString();
                        m.PHOTOTYPE = PHOTOTYPE;
                        m.PRID = PRID;
                        m.PHOTOFILENAME = "/UploadFile/DCPhoto/" + hfc[i].FileName;
                        string PhysicalPath = Server.MapPath(m.PHOTOFILENAME);
                        hfc[i].SaveAs(PhysicalPath);
                        m.opMethod = Method;
                    }
                    ms = PEST_PHOTOCls.Manager(m);
                }
            }
            return Json(ms);
        }

        /// <summary>
        /// 有害生物图片管理
        /// </summary>
        /// <returns></returns>
        public ActionResult PESTPhotoManager()
        {
            PEST_PHOTO_Model m = new PEST_PHOTO_Model();
            m.PEST_PHOTOID = Request.Params["PEST_PHOTOID"];
            m.PHOTOTITLE = Request.Params["PHOTOTITLE"];
            m.PHOTOFILENAME = Request.Params["PHOTOFILENAME"];
            m.PHOTOEXPLAIN = Request.Params["PHOTOEXPLAIN"];
            m.PHOTOTIME = DateTime.Now.ToString();
            m.PHOTOTYPE = Request.Params["PHOTOTYPE"];
            m.PRID = Request.Params["PRID"];
            m.opMethod = Request.Params["Method"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod == "Del")
            {
                string file = Server.MapPath(m.PHOTOFILENAME);
                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);
            }
            else
            {
                if (string.IsNullOrEmpty(m.PHOTOTITLE))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入照片标题!", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(PEST_PHOTOCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region private
        /// <summary>
        /// 发生报表数据准备
        /// </summary>
        /// <param name="BYORGNO"></param>
        /// <param name="PESTBYCODE"></param>
        /// <param name="Time"></param>
        /// <param name="sTime"></param>
        /// <param name="result"></param>
        /// <param name="dic105"></param>
        /// <param name="dic106"></param>
        /// <param name="templist"></param>
        /// <param name="list"></param>
        private static void HAPPENREPORTData(string BYORGNO, string PESTBYCODE, string Time, out string[] sTime, out List<T_SYS_ORGModel> result, out List<T_SYS_DICTModel> dic105, out List<T_SYS_DICTModel> dic106, out List<T_SYS_PEST_OBSERVEAREA_Model> templist, out List<PEST_REPORT_HAPPEN_Model> list)
        {
            sTime = Time.Split('-');
            result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { TopORGNO = BYORGNO }).ToList();
            dic105 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "105" }).ToList();
            dic106 = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "106" }).ToList();
            templist = T_SYS_PEST_OBSERVEAREACls.getListModel(new T_SYS_PEST_OBSERVEAREA_SW { BYORGNO = BYORGNO }).ToList();
            list = PEST_REPORT_HAPPENCls.getListModel(new PEST_REPORT_HAPPEN_SW { BYORGNO = BYORGNO, PESTBYCODE = PESTBYCODE, HAPPENYEAR = sTime[0], HAPPENMONTH = sTime[1] }).ToList();
        }

        /// <summary>
        /// 防治报表数据准备
        /// </summary>
        /// <param name="BYORGNO"></param>
        /// <param name="PESTBYCODE"></param>
        /// <param name="Time"></param>
        /// <param name="result"></param>
        /// <param name="_happenList"></param>
        /// <param name="_dic107List"></param>
        /// <param name="_dic107WGHList"></param>
        /// <param name="_dic107YGHList"></param>
        /// <param name="_controlList"></param>
        /// <param name="pestName"></param>
        private static void CONTROLREPORTData(string BYORGNO, string PESTBYCODE, string Time, out List<T_SYS_ORGModel> result, out List<PEST_REPORT_HAPPEN_Model> _happenList, out List<T_SYS_DICTModel> _dic107List, out List<T_SYS_DICTModel> _dic107WGHList, out List<T_SYS_DICTModel> _dic107YGHList, out List<PEST_REPORT_CONTROL_Model> _controlList, out string pestName)
        {
            string[] sTime = Time.Split('-');
            result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { TopORGNO = BYORGNO }).ToList();
            _happenList = PEST_REPORT_HAPPENCls.getListModel(new PEST_REPORT_HAPPEN_SW { BYORGNO = BYORGNO, PESTBYCODE = PESTBYCODE, HAPPENYEAR = sTime[0], HAPPENMONTH = sTime[1] }).ToList();
            _dic107List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "107" }).ToList();
            _dic107WGHList = _dic107List.FindAll(a => a.DICTVALUE.Length < 3);
            _dic107YGHList = _dic107List.FindAll(a => a.DICTVALUE.Length >= 3);
            _controlList = PEST_REPORT_CONTROLCls.getListModel(new PEST_REPORT_CONTROL_SW { BYORGNO = BYORGNO, PESTBYCODE = PESTBYCODE, HAPPENYEAR = sTime[0], HAPPENMONTH = sTime[1] }).ToList();
            pestName = T_SYS_PESTCls.getName(PESTBYCODE);
        }

        /// <summary>
        /// 防治报表数据
        /// </summary>
        /// <param name="BYORGNO"></param>
        /// <param name="PESTBYCODE"></param>
        /// <param name="Time"></param>
        /// <param name="result"></param>
        /// <param name="_harmList"></param>
        private static void HARMREPORTData(string BYORGNO, string PESTBYCODE, string Time, out List<T_SYS_ORGModel> result, out List<PEST_REPORT_HARM_Model> _harmList)
        {
            string[] sTime = Time.Split('-');
            result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { TopORGNO = BYORGNO }).ToList();
            _harmList = PEST_REPORT_HARMCls.getListModel(new PEST_REPORT_HARM_SW { BYORGNO = BYORGNO, PESTBYCODE = PESTBYCODE, HAPPENYEAR = sTime[0], HAPPENMONTH = sTime[1] }).ToList();
        }

        /// <summary>
        /// 检疫报表
        /// </summary>
        /// <param name="BYORGNO"></param>
        /// <param name="Time"></param>
        /// <param name="result"></param>
        /// <param name="dic108List"></param>
        /// <param name="_list"></param>
        private static void QUARANTINEREPORTData(string BYORGNO, string Time, out List<T_SYS_ORGModel> result, out List<T_SYS_DICTModel> dic108List, out List<PEST_REPORT_QUARANTINE_Model> _list)
        {
            result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { TopORGNO = BYORGNO }).ToList();
            dic108List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "108" }).ToList();
            _list = PEST_REPORT_QUARANTINECls.getListModel(new PEST_REPORT_QUARANTINE_SW { BYORGNO = BYORGNO, HAPPENYEAR = Time }).ToList();
        }

        /// <summary>
        /// 人财物报表数据
        /// </summary>
        /// <param name="BYORGNO"></param>
        /// <param name="Time"></param>
        /// <param name="_rcwTypeList"></param>
        /// <param name="_rcwTypeTitleList"></param>
        /// <param name="_list"></param>
        private static void RCWREPORTData(string BYORGNO, string Time, out List<PEST_REPORT_RCWTYPE_Model> _rcwTypeList, out List<PEST_REPORT_RCWTYPE_Model> _rcwTypeTitleList, out List<PEST_REPORT_RCW_Model> _list)
        {
            _rcwTypeList = PEST_REPORT_RCWTYPECls.getListModel(new PEST_REPORT_RCWTYPE_SW()).ToList();
            _rcwTypeTitleList = _rcwTypeList.FindAll(a => a.RCWCODE.Length == 2);
            _list = PEST_REPORT_RCWCls.getListModel(new PEST_REPORT_RCW_SW { BYORGNO = BYORGNO, RCWYEAR = Time }).ToList();
        }

        /// <summary>
        /// 目标考核报表数据
        /// </summary>
        /// <param name="BYORGNO"></param>
        /// <param name="YEAR"></param>
        /// <param name="result"></param>
        /// <param name="dic109List"></param>
        /// <param name="_list"></param>
        private static void ASSESSINGTARGETREPORTData(string BYORGNO, string YEAR, out List<T_SYS_ORGModel> result, out List<T_SYS_DICTModel> dic109List, out List<PEST_REPORT_ASSESSINGTARGET_Model> _list)
        {
            result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { TopORGNO = BYORGNO }).ToList();
            dic109List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "109" }).ToList();
            _list = PEST_REPORT_ASSESSINGTARGETCls.getListModel(new PEST_REPORT_ASSESSINGTARGET_SW { BYORGNO = BYORGNO, RCWYEAR = YEAR }).ToList();
        }

        /// <summary>
        /// 预测报表数据
        /// </summary>
        /// <param name="BYORGNO"></param>
        /// <param name="PESTCODE"></param>
        /// <param name="YEAR"></param>
        /// <param name="result"></param>
        /// <param name="_dic110List"></param>
        /// <param name="_AREAList"></param>
        /// <param name="_list1"></param>
        /// <param name="_list2"></param>
        private static void FORECASTREPORTData(string BYORGNO, string PESTCODE, string YEAR, out List<T_SYS_ORGModel> result, out List<T_SYS_DICTModel> _dic110List, out List<PEST_LOCALTREESPECIES_Model> _AREAList, out List<PEST_REPORT_FORECAST_Model> _list1, out List<PEST_REPORT_FORECAST_Model> _list2)
        {
            int year2 = int.Parse(YEAR);
            int year1 = year2 - 1;
            result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { TopORGNO = BYORGNO }).ToList();
            _dic110List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "110" }).ToList();
            _AREAList = PEST_LOCALTREESPECIESCls.getListAREA(PESTCODE).ToList();
            _list1 = PEST_REPORT_FORECASTCls.getListModel(new PEST_REPORT_FORECAST_SW { BYORGNO = BYORGNO, PESTBYCODE = PESTCODE, FORECASTYEAR = year1.ToString() }).ToList();
            _list2 = PEST_REPORT_FORECASTCls.getListModel(new PEST_REPORT_FORECAST_SW { BYORGNO = BYORGNO, PESTBYCODE = PESTCODE, FORECASTYEAR = year2.ToString() }).ToList();
        }

        /// <summary>
        /// 松材线虫病普查报表数据
        /// </summary>
        /// <param name="BYORGNO"></param>
        /// <param name="YEAR"></param>
        /// <param name="SEASON"></param>
        /// <param name="result"></param>
        /// <param name="dic112List"></param>
        /// <param name="_list"></param>
        private static void SCXCBPCREPORTData(string BYORGNO, string YEAR, string SEASON, out List<T_SYS_ORGModel> result, out List<T_SYS_DICTModel> dic112List, out List<PEST_REPORT_SCXCBPC_Model> _list)
        {
            result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { TopORGNO = BYORGNO }).ToList();
            dic112List = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "112" }).ToList();
            _list = PEST_REPORT_SCXCBPCCls.getListModel(new PEST_REPORT_SCXCBPC_SW { BYORGNO = BYORGNO, SCXCBPCYEAR = YEAR, SCXCBPCSEASONCODE = SEASON }).ToList();
        }

        /// <summary>
        /// 松材线虫病防治报表数据
        /// </summary>
        /// <param name="BYORGNO"></param>
        /// <param name="YEAR"></param>
        /// <param name="result"></param>
        /// <param name="dictType114"></param>
        /// <param name="_list1"></param>
        /// <param name="_list2"></param>
        private static void SCXCBFZREPORTData(string BYORGNO, string YEAR, out List<T_SYS_ORGModel> result, out T_SYS_DICTTYPE_Model dictType114, out List<PEST_REPORT_SCXCBFZ_Model> _list1, out List<PEST_REPORT_SCXCBFZMX_Model> _list2)
        {
            result = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { TopORGNO = BYORGNO }).ToList();
            dictType114 = T_SYS_DICTCls.getTypeModel(new T_SYS_DICTTYPE_SW { DICTTYPEID = "114" });
            _list1 = PEST_REPORT_SCXCBFZCls.getListModel(new PEST_REPORT_SCXCBFZ_SW { BYORGNO = BYORGNO, SCXCBFZYEAR = YEAR }).ToList();
            _list2 = PEST_REPORT_SCXCBFZMXCls.getListModel(new PEST_REPORT_SCXCBFZMX_SW { }).ToList();
        }
        #endregion
    }
}
