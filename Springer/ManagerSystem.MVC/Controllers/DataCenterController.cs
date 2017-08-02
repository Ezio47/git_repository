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
namespace ManagerSystem.MVC.Controllers
{
    /// <summary>
    /// 数据中心
    /// </summary>
    public class DataCenterController : BaseController
    {

        #region 类别管理


        /// <summary>
        /// 类别管理－增、删、改
        /// </summary>
        /// <returns>参见模型</returns>
        /// 
        public ActionResult DCTYPEManager()
        {
            DC_TYPE_Model m = new DC_TYPE_Model();
            m.DCTYPEID = Request.Params["DCTYPEID"];
            m.DCTYPETOPID = Request.Params["DCTYPETOPID"];
            m.DCTYPENAME = Request.Params["DCTYPENAME"];
            m.ORDERBY = Request.Params["ORDERBY"];
            m.DCTYPEFLAG = Request.Params["DCTYPEFLAG"];

            m.opMethod = Request.Params["Method"];
            m.returnUrl = Request.Params["returnUrl"];

            if (string.IsNullOrEmpty(m.returnUrl))
                m.returnUrl = "/DataCenter/TypeList";
            //默认为添加
            if (string.IsNullOrEmpty(m.opMethod))
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.ORDERBY))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入排序号！", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.DCTYPENAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入名称！", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(DC_TYPECls.Manager(m)), "text/html;charset=UTF-8");

            //return Content(JsonConvert.SerializeObject(new Message(false, "保存失败，请检查各输入框是否正确！", "")), "text/html;charset=UTF-8");
        }


        public ActionResult getDCTYPEJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            return Content(JsonConvert.SerializeObject(DC_TYPECls.getModel(new DC_TYPE_SW { DCTYPEID = ID })), "text/html;charset=UTF-8");
        }
        private string getNav(string id)
        {
            DC_TYPE_Model m = DC_TYPECls.getModel(new DC_TYPE_SW { DCTYPEID = id });
            if (string.IsNullOrEmpty(m.DCTYPEID))
                return "";
            if (m.DCTYPETOPID == "0")
                return "<li class=\"active\"><a href=\"/DataCenter/TypeList?ID=" + m.DCTYPEID + "\">" + m.DCTYPENAME + "</a></li>";
            else
                return "<li class=\"active\"><a href=\"/DataCenter/TypeList?ID=" + m.DCTYPEID + "\">" + m.DCTYPENAME + "</a></li>|" + getNav(m.DCTYPETOPID);
        }
        ///// <summary>
        ///// 类别列表
        ///// </summary>
        ///// <returns>参见模型</returns>
        public ActionResult TypeList()
        {
            pubViewBag("007005", "007005", "");


            string ID = Request.Params["ID"];//当前页面传递编号
            if (string.IsNullOrEmpty(ID) == true)
                ID = "0";
            ViewBag.T_ID = ID;
            ViewBag.T_UrlReferrer = "/DataCenter/TypeList?ID=" + ID;
            //导航条
            string[] arr = getNav(ID).Split('|');
            string str = "";
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (string.IsNullOrEmpty(arr[i]) == false)
                    str += arr[i];
            }
            ViewBag.navList = str;


            ViewBag.DCTYPEList = getDCTYPEStr(new DC_TYPE_SW { DCTYPETOPID = ID });
            return View();

        }
        private string getDCTYPEStr(DC_TYPE_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>名称</th>");
            sb.AppendFormat("<th>排序号</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = DC_TYPECls.getModelList(sw);//.getListMode(sw);
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr onclick=\"showValue('{0}')\">", v.DCTYPEID);
                else
                    sb.AppendFormat("<tr class='row1' onclick=\"showValue('{0}')\">", v.DCTYPEID);
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.DCTYPENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORDERBY);
                sb.AppendFormat("    <td class=\" \">");
                sb.AppendFormat("            <a href=\"/DataCenter/TypeList?ID={0}\">", v.DCTYPEID);
                sb.AppendFormat("                <i class=\"icon-zoom-in bigger-130\"></i>");
                sb.AppendFormat("            下级类别管理</a>");

                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        #endregion

        #region 机构
        /// <summary>
        /// 机构管理
        /// </summary>
        /// <returns></returns>
        public ActionResult OrgIndex()
        {
            pubViewBag("011001", "011001", "");
            if (ViewBag.isPageRight == false)
                return View();
            return View();
        }
        /// <summary>
        /// 获取组织机构
        /// </summary>
        /// <returns></returns>
        public ActionResult OrgTreeget()
        {
            string orgno = Request.Params["orgno"];
            if (string.IsNullOrEmpty(orgno))
            {
                orgno = SystemCls.getCurUserOrgNo();
            }
            var str = T_SYS_ORGCls.getORGTree(new T_SYS_ORGSW { ORGNO = orgno });
            return Content(str.ToString(), "application/json");
        }
        /// <summary>
        /// 仅仅获取单独的市县镇
        /// </summary>
        /// <returns></returns>
        public ActionResult OnlyOrgTreeget()
        {
            string orgno = Request.Params["orgno"];
            if (string.IsNullOrEmpty(orgno))
            {
                orgno = SystemCls.getCurUserOrgNo();
            }
            var str = T_SYS_ORGCls.getOnlyORGTree(new T_SYS_ORGSW { ORGNO = orgno });
            return Content(str.ToString(), "application/json");
        }
        /// <summary>
        /// 获取仓库
        /// </summary>
        /// <returns></returns>
        public ActionResult DEPOTTreeget()
        {
            string result = DC_TYPECls.getdepotTree(new DC_TYPE_SW { });
            return Content(result, "application/json");
        }
        #endregion

        #region 测试表格
        /// <summary>
        /// 队伍管理
        /// </summary>
        /// <returns></returns>
        public ActionResult testTable()
        {
            pubViewBag("011002", "011002", "");
            if (ViewBag.isPageRight == false)
                return View();
            //分页信息 需使用上面的total
            ViewBag.PagerInfo = PagerCls.getPagerInfo_New(new PagerSW { curPage = int.Parse("1"), pageSize = int.Parse("10"), rowCount = 153, url = "/System/LogList?trans=" });
            return View();
        }
        #endregion

        #region 队伍
        /// <summary>
        /// 队伍管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ArmyIndex()
        {
            pubViewBag("011002", "011002", "");
            if (ViewBag.isPageRight == false)
                return View();
            //分页信息 需使用上面的total
            ViewBag.PagerInfo = PagerCls.getPagerInfo_New(new PagerSW { curPage = int.Parse("1"), pageSize = int.Parse("10"), rowCount = 153, url = "/System/LogList?trans=" });
            return View();
        }
        /// <summary>
        /// 取出队伍树形菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult ArmyTreeget()
        {
            string result = DC_TYPECls.getArmytree();
            return Content(result, "application/json");
        }
        #endregion

        #region 资源
        /// <summary>
        /// 资源管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ResourceIndex()
        {
            pubViewBag("011003", "011003", "");
            if (ViewBag.isPageRight == false)
                return View();
            return View();
        }
        /// <summary>
        /// 取出资源树形菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult RESOURCETreeget()
        {
            string result = DC_TYPECls.getRESOURCETree(new DC_TYPE_SW { });
            return Content(result, "application/json");
        }
        #endregion

        #region 设施
        /// <summary>
        /// 设施管理
        /// </summary>
        /// <returns></returns>
        public ActionResult FacilityIndex()
        {
            pubViewBag("011004", "011004", "");
            if (ViewBag.isPageRight == false)
                return View();
            return View();
        }
        /// <summary>
        /// 取出设施树形菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult FACILITYTreeget()
        {
            string result = DC_TYPECls.getFACILITYTree(new DC_TYPE_SW { });
            return Content(result, "application/json");
        }
        #endregion

        #region 装备
        /// <summary>
        /// 装备管理
        /// </summary>
        /// <returns></returns>
        public ActionResult EquipIndex()
        {
            pubViewBag("011005", "011005", "");
            if (ViewBag.isPageRight == false)
                return View();
            return View();
        }
        /// <summary>
        /// 取出装备树形菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult EQUIPTreeget()
        {
            string result = DC_TYPECls.getEQUIPTree(new DC_TYPE_SW { });
            return Content(result, "application/json");
        }
        #endregion

        #region 档案
        /// <summary>
        /// 档案管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ArchivalIndex()
        {
            pubViewBag("011006", "011006", "");
            if (ViewBag.isPageRight == false)
                return View();
            return View();
        }
        /// <summary>
        /// 取出档案树形菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult ARCHIVESTreeget()
        {
            string result = DC_TYPECls.getARCHIVESTree(new T_SYS_DICTSW { });
            return Content(result, "application/json");
        }
        #endregion

        #region 图层控制
        /// <summary>
        /// 图层控制管理
        /// </summary>
        /// <returns></returns>
        public ActionResult LayerIndex()
        {
            pubViewBag("011007", "011007", "");
            if (ViewBag.isPageRight == false)
                return View();
            return View();
        }
        #endregion

        #region 文档管理


        #region 文件上传
        public JsonResult DocUpload()
        {
            Message ms = null;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string Path = "";
            string[] arr = hfc[0].FileName.Split('.');
            if (string.IsNullOrEmpty(hfc[0].FileName))
                return Json(new Message(false, "请选择附件！", ""));
            if (arr[arr.Length - 1].ToLower() == "exe")
                return Json(new Message(false, "禁止上传exe文件！", ""));
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss.") + arr[arr.Length - 1];
            Path = "/UploadFile/Doc/" + filename;// hfc[i].FileName;
            string PhysicalPath = Server.MapPath(Path);
            hfc[0].SaveAs(PhysicalPath);
            ms = new Message(true, filename, "");

            return Json(ms);
        }
        #endregion

        #region 文档管理
        /// <summary>
        /// 文档管理
        /// </summary>
        /// <returns></returns>

        [ValidateInput(false)]
        public ActionResult DocManager()
        {
            string tid = Request.Params["tid"];
            string id = Request.Params["id"];
            string ARTTITLE = Request.Params["ARTTITLE"];
            string ARTCONTENT = Request.Params["ARTCONTENT"];
            string PLANFILENAME = Request.Params["PLANFILENAME"];
            string Method = Request.Params["Method"];
            string returnUrl = Request.Params["returnUrl"];
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/DataCenter/DocList";
            //默认为添加
            if (string.IsNullOrEmpty(Method))
            {
                Method = "Add";

            }
            if (Method != "Del")
            {
                if (string.IsNullOrEmpty(ARTTITLE))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入标题！", "")), "text/html;charset=UTF-8");
            }
            ART_DOCUMENT_Model m = new ART_DOCUMENT_Model();
            m.ARTID = id;
            m.ARTTYPEID = tid;
            m.ARTTITLE = ARTTITLE;
            m.ARTCONTENT = ARTCONTENT;
            m.PLANFILENAME = PLANFILENAME;
            m.opMethod = Method;
            m.returnUrl = returnUrl;

            m.ARTADDUSERID = SystemCls.getUserID();
            return Content(JsonConvert.SerializeObject(ART_DOCUMENTCls.Manager(m)), "text/html;charset=UTF-8");

        }

        #endregion

        #region 根据序号获取内容
        /// <summary>
        /// 根据序号获取内容
        /// </summary>
        /// <returns>参见模型</returns>
        public ActionResult getDocJson()
        {
            string id = Request.Params["id"];
            if (string.IsNullOrEmpty(id))
                id = "0";
            return Content(JsonConvert.SerializeObject(ART_DOCUMENTCls.getModel(new ART_DOCUMENT_SW { ARTID = id })), "text/html;charset=UTF-8");
        }

        #endregion

        #region 文档管理页面


        /// <summary>
        /// 文档管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DocMan()
        {
            pubViewBag("011007", "011007", "");

            if (ViewBag.isPageRight == false)
                return View();

            StringBuilder sb = new StringBuilder();
            //获取顶级
            string typeID = Request.Params["tid"];
            var list = ART_TYPECls.getListModel(new ART_TYPE_SW { ARTTYPERID = "0" });
            foreach (var v in list)
            {
                sb.AppendFormat("<ul>");

                sb.AppendFormat("<li class='liType'>{0}</li>", v.ARTTYPENAME);
                //获取子类别
                var listChild = ART_TYPECls.getListModel(new ART_TYPE_SW { ARTTYPERID = v.ARTTYPEID });
                foreach (var vC in listChild)
                {
                    if (string.IsNullOrEmpty(typeID))
                        typeID = vC.ARTTYPEID;
                    if (typeID == vC.ARTTYPEID)
                        sb.AppendFormat("<li class='liCur title'><a href='DocIndex?tid={0}'>{1}</a>", vC.ARTTYPEID, vC.ARTTYPENAME);
                    else
                        sb.AppendFormat("<li class='title'><a href='DocIndex?tid={0}'>{1}</a>", vC.ARTTYPEID, vC.ARTTYPENAME);
                }
                sb.Append("</li>");
                sb.AppendFormat("</ul>");
            }
            ViewBag.tid = typeID;

            ViewBag.T_Method = Request.Params["Method"];
            //如果未传参数，默认为添加
            if (string.IsNullOrEmpty(ViewBag.T_Method))
                ViewBag.T_Method = "Add";
            ViewBag.id = Request.Params["id"];

            ViewBag.typeList = sb.ToString();
            return View();
        }
        #endregion

        #region 文档查看
        /// <summary>
        /// 文档查看
        /// </summary>
        /// <returns></returns>
        public ActionResult DocShow()
        {
            pubViewBag("011007", "011007", "");

            if (ViewBag.isPageRight == false)
                return View();
            string ID = Request.Params["ID"];
            StringBuilder sb = new StringBuilder();
            var v = ART_DOCUMENTCls.getModel(new ART_DOCUMENT_SW { ARTID = ID });
            sb.AppendFormat("<div class=' showArtTitle'>{0}</div>", v.ARTTITLE);
            sb.AppendFormat("<div class=' showArtSmallTitle'>");
            //sb.AppendFormat("类别:[{0}]&nbsp;", v.ARTTYPENAME);
            //sb.AppendFormat("录入：[{0}]&nbsp;", v.ARTADDUSERName);
            //sb.AppendFormat("添加时间：{0}", v.ARTTIME);
            sb.AppendFormat("{0}", v.ARTTIME);
            sb.AppendFormat("</div>");
            sb.AppendFormat("<div class=' showArtContent'>{0}</div>", v.ARTCONTENT);
            sb.AppendFormat("<div class=' showArtFile'>文件下载：<br><a href='/UploadFile/Doc/{0}'>{1}</a></div>", v.PLANFILENAME, v.ARTTITLE);
            ViewBag.Content = sb.ToString();
            return View();
        }

        #endregion

        #region 搜索
        /// <summary>
        /// 搜索
        /// </summary>
        /// <returns></returns>
        public ActionResult DocQuery()
        {
            string PageSize = Request.Params["PageSize"];
            string Page = Request.Params["Page"];
            string EMAILTITLE = Request.Params["TITLE"];
            string tid = Request.Params["tid"];
            string str = ClsStr.EncryptA01((PageSize + "|" + EMAILTITLE + "|" + tid), "kkkkkkkk");
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/DataCenter/DocIndex?trans=" + str + "&tid=" + tid + "&page=" + Page)), "text/html;charset=UTF-8");
        }

        #endregion

        #region 文档管理
        /// <summary>
        /// 文档管理
        /// </summary>
        /// <returns></returns>
        public ActionResult DocIndex()
        {
            pubViewBag("011007", "011007", "");
            if (ViewBag.isPageRight == false)
                return View();

            string typeID = "";

            string PageSize = Request.Params["PageSize"];
            string page = Request.Params["page"];
            typeID = (string.IsNullOrEmpty(Request.Params["tid"])) ? "" : Request.Params["tid"].ToString();
            page = (string.IsNullOrEmpty(page)) ? "1" : page;
            string trans = Request.Params["trans"];
            string[] arr = new string[3];
            if (string.IsNullOrEmpty(trans) == false)
            {
                arr = ClsStr.DecryptA01(trans, "kkkkkkkk").Split('|');
                //typeID = arr[2];//类别序号
            }
            else
            {
                arr[0] = PagerCls.getDefaultPageSize().ToString();
            }
            ViewBag.SearchTITLE = arr[1];

            StringBuilder sb = new StringBuilder();
            //获取顶级
            var list = ART_TYPECls.getListModel(new ART_TYPE_SW { ARTTYPERID = "0" });
            foreach (var v in list)
            {
                sb.AppendFormat("<ul>");

                sb.AppendFormat("<li class='liType'>{0}</li>", v.ARTTYPENAME);
                //获取子类别
                var listChild = ART_TYPECls.getListModel(new ART_TYPE_SW { ARTTYPERID = v.ARTTYPEID });
                foreach (var vC in listChild)
                {
                    if (string.IsNullOrEmpty(typeID))
                        typeID = vC.ARTTYPEID;
                    if (typeID == vC.ARTTYPEID)
                        sb.AppendFormat("<li class='liCur title'><a href='DocIndex?tid={0}'>{1}</a>", vC.ARTTYPEID, vC.ARTTYPENAME);
                    else
                        sb.AppendFormat("<li class='title'><a href='DocIndex?tid={0}'>{1}</a>", vC.ARTTYPEID, vC.ARTTYPENAME);
                }
                sb.Append("</li>");
                sb.AppendFormat("</ul>");
            }
            ViewBag.typeid = typeID;
            ViewBag.typeList = sb.ToString();

            int total = 0;
            ViewBag.List = getDocListStr(new ART_DOCUMENT_SW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), ARTTITLE = arr[1], ARTTYPEID = typeID }, out total);//列表
            ViewBag.PagerInfo = PagerCls.getPagerInfo_New(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), rowCount = total, url = "/DataCenter/DocIndex?tid=" + typeID + "&trans=" + trans });
            return View();
        }

        #endregion

        #region 组合表格
        /// <summary>
        /// 组合表格
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        private string getDocListStr(ART_DOCUMENT_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style='width:10%;'>序号</th>");
            sb.AppendFormat("<th style='width:10%;'>类别</th>");
            sb.AppendFormat("<th style='width:40%;'>标题</th>");
            sb.AppendFormat("<th style='width:15%;'>添加人</th>");
            sb.AppendFormat("<th style='width:15%;'>时间</th>");
            sb.AppendFormat("<th style='width:10%;'>管理</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = ART_DOCUMENTCls.getModelList(sw, out total);
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center  \">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center  \">{0}</td>", v.ARTTYPENAME);
                sb.AppendFormat("<td class=\"left  \"><a href=\"/DataCenter/DocShow?ID={1}\" target='_blank'>{0}</a></td>", v.ARTTITLE, v.ARTID);
                sb.AppendFormat("<td class=\"left  \">{0}</td>", v.ARTADDUSERName);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", v.ARTTIME);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("<td class=\"center  \">");
                sb.AppendFormat("<a href='/DataCenter/DocMan?Method=Mdy&id={0}&tid={1}'>修改</a>", v.ARTID, v.ARTTYPEID);
                sb.AppendFormat("&nbsp;<a href='#' onclick='Manager({0})'>删除</a>", v.ARTID);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        #endregion

        #endregion

        #region Ajax 数据中心定位
        /// <summary>
        /// 获取设施
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFACILITYModel()
        {
            var modellist = new List<DC_FACILITY_Model>();
            string id = Request.Params["id"];
            string flag = Request.Params["flag"];//1 为设施级别
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(flag))
            {
                return Json(new MessageListObject(false, null));
            }
            if (flag == "1")
            {
                modellist = DC_FACILITYCls.getListModel(new DC_FACILITY_SW { DC_FACILITYID = id }).ToList();
            }
            else
            {
                modellist = DC_FACILITYCls.getListModel(new DC_FACILITY_SW { TYPEID = id }).ToList();
            }

            return Json(new MessageListObject(true, modellist));
        }

        /// <summary>
        /// 获取装备
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEQUIPModel()
        {
            var modellist = new List<DC_EQUIP_Model>();
            string id = Request.Params["id"];
            string flag = Request.Params["flag"];//1 为设施级别
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(flag))
            {
                return Json(new MessageListObject(false, null));
            }
            if (flag == "1")
            {
                modellist = DC_EQUIPCls.getListModel(new DC_EQUIP_SW { DC_EQUIPID = id }).ToList();
            }
            else
            {
                modellist = DC_EQUIPCls.getListModel(new DC_EQUIP_SW { TYPEID = id }).ToList();
            }

            return Json(new MessageListObject(true, modellist));
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRESOURCEModel()
        {
            var modellist = new List<DC_RESOURCE_Model>();
            string id = Request.Params["id"];
            string flag = Request.Params["flag"];//1 为设施级别
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(flag))
            {
                return Json(new MessageListObject(false, null));
            }
            if (flag == "1")
            {
                modellist = DC_RESOURCECls.getListModel(new DC_RESOURCE_SW { DC_RESOURCEID = id }).ToList();
            }
            else
            {
                modellist = DC_RESOURCECls.getListModel(new DC_RESOURCE_SW { TYPEID = id }).ToList();
            }
            return Json(new MessageListObject(true, modellist));
        }

        /// <summary>
        /// 获取队伍
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPROTEAMModel()
        {
            string id = Request.Params["id"];
            string flag = Request.Params["flag"];//1 为子目录 0为目录
            string type = Request.Params["type"];// 1 专职人员 2 护林员 3 专业队 4 瞭望塔

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(flag) || string.IsNullOrEmpty(type))
            {
                return Json(new MessageListObject(false, null));
            }
            if (type == "1")//1 专职人员
            {
                var modellist = new List<PersonModel>();
                if (flag == "1")
                {
                    var model = new PersonModel();
                    var info = DC_FULLTIMEUSERCls.getListModel(new DC_FULLTIMEUSER_SW { DC_FULLTIMEUSERID = id }).FirstOrDefault();
                    var orginfo = T_SYS_ORGCls.getModel(new T_SYS_ORGSW { ORGNO = info.BYORGNO });
                    //model.InjectFrom(info);
                    model.JD = orginfo.JD;
                    model.WD = orginfo.WD;
                    modellist.Add(model);
                }
                else
                {
                    //父节点时 id为机构码
                    // var infolist = DC_FULLTIMEUSERCls.getListModel(new DC_FULLTIMEUSER_SW { BYORGNO = id });
                    var orginfo = T_SYS_ORGCls.getModel(new T_SYS_ORGSW { ORGNO = id });
                    var model = new PersonModel();
                    model.JD = orginfo.JD;
                    model.WD = orginfo.WD;
                    modellist.Add(model);

                    //foreach (var info in infolist)
                    //{
                    //    var model = new PersonModel();
                    //    model.InjectFrom(info);
                    //    model.JD = orginfo.JD;
                    //    model.WD = orginfo.WD;
                    //    modellist.Add(model);
                    //}
                }
                return Json(new MessageListObject(true, modellist));
            }

            if (type == "2")//护林员
            {
                var modellist = new List<PersonModel>();
                if (flag == "1")
                {
                    var model = new PersonModel();
                    var info = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { HID = id }).FirstOrDefault();
                    var orginfo = T_SYS_ORGCls.getModel(new T_SYS_ORGSW { ORGNO = info.BYORGNO });

                    //model.DC_FULLTIMEUSERID = info.HID;
                    //model.FTNAME = info.HNAME;
                    //model.BYORGNO = info.BYORGNO;
                    //model.BIRTH = info.BIRTH;
                    //model.SEX = info.SEX;
                    //model.LINKWAY = info.PHONE;

                    model.JD = orginfo.JD;
                    model.WD = orginfo.WD;
                    modellist.Add(model);
                }
                else
                {
                    //父节点时 id为机构码
                    var infolist = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { BYORGNO = id });
                    var orginfo = T_SYS_ORGCls.getModel(new T_SYS_ORGSW { ORGNO = id });
                    var model = new PersonModel();
                    model.JD = orginfo.JD;
                    model.WD = orginfo.WD;
                    modellist.Add(model);

                    //foreach (var info in infolist)
                    //{
                    //    var model = new PersonModel();
                    //    model.DC_FULLTIMEUSERID = info.HID;
                    //    model.FTNAME = info.HNAME;
                    //    model.BYORGNO = info.BYORGNO;
                    //    model.BIRTH = info.BIRTH;
                    //    model.SEX = info.SEX;
                    //    model.LINKWAY = info.PHONE;

                    //    model.JD = orginfo.JD;
                    //    model.WD = orginfo.WD;
                    //    modellist.Add(model);
                    //}
                }
                return Json(new MessageListObject(true, modellist));
            }
            if (type == "3")//3 专业队
            {
                var modellist = new List<DC_PROTEAM_Model>();
                if (flag == "1")
                {
                    modellist = DC_PROTEAMCls.getListModel(new DC_PROTEAM_SW { DC_PROTEAMID = id }).ToList();
                }
                else
                {
                    modellist = DC_PROTEAMCls.getListModel(new DC_PROTEAM_SW { TYPEID = id }).ToList();
                }
                return Json(new MessageListObject(true, modellist));
            }
            if (type == "4")//4  瞭望台
            {
                var modellist = new List<DC_WATCHTOWER_Model>();
                if (flag == "1")
                {
                    modellist = DC_WATCHTOWERCls.getListModel(new DC_WATCHTOWER_SW { DC_WATCHTOWERID = id }).ToList();
                }
                else
                {
                    modellist = DC_WATCHTOWERCls.getListModel(new DC_WATCHTOWER_SW { BYORGNO = id }).ToList();
                }
                return Json(new MessageListObject(true, modellist));
            }

            return Json(null);
        }

        /// <summary>
        /// 人员信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPersons()
        {
            Message ms = null;
            string id = Request.Params["id"];
            string flag = Request.Params["flag"];
            string type = Request.Params["type"];

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divTable\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            //sb.AppendFormat("<th style='width:10%;'>序号</th>");
            sb.AppendFormat("<th>姓名</th>");
            sb.AppendFormat("<th>性别</th>");
            sb.AppendFormat("<th>出生日期</th>");
            sb.AppendFormat("<th>单位</th>");
            sb.AppendFormat("<th>职务</th>");
            sb.AppendFormat("<th>联系方式</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody role=\"alert\" aria-live=\"polite\" aria-relevant=\"all\">");
            var modellist = new List<PersonModel>();//人员模型
            if (type == "1")//专职人员
            {
                if (flag == "1")//子目录
                {
                    var model = new PersonModel();
                    var info = DC_FULLTIMEUSERCls.getListModel(new DC_FULLTIMEUSER_SW { DC_FULLTIMEUSERID = id }).FirstOrDefault();
                    model.InjectFrom(info);
                    model.BYORGNONAME = StateSwitch.GetOrgNameByOrgNO(info.BYORGNO);
                    modellist.Add(model);
                }
                else
                {
                    var infolist = DC_FULLTIMEUSERCls.getListModel(new DC_FULLTIMEUSER_SW { BYORGNO = id });
                    if (infolist.Any())
                    {
                        foreach (var info in infolist)
                        {
                            var model = new PersonModel();
                            model.InjectFrom(info);
                            model.BYORGNONAME = StateSwitch.GetOrgNameByOrgNO(info.BYORGNO);
                            modellist.Add(model);
                        }
                    }
                }
            }
            if (type == "2")//护林员
            {
                if (flag == "1")//子目录
                {
                    var model = new PersonModel();
                    var info = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { HID = id }).FirstOrDefault();
                    model.DC_FULLTIMEUSERID = info.HID;
                    model.FTNAME = info.HNAME;
                    model.BYORGNO = info.BYORGNO;
                    model.BIRTH = info.BIRTH;
                    model.SEX = info.SEX;
                    model.BYORGNONAME = StateSwitch.GetOrgNameByOrgNO(info.BYORGNO);
                    model.LINKWAY = info.PHONE;
                    modellist.Add(model);
                }
                else
                {
                    //父节点时 id为机构码
                    var infolist = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { BYORGNO = id });
                    if (infolist.Any())
                    {
                        foreach (var info in infolist)
                        {
                            var model = new PersonModel();
                            model.DC_FULLTIMEUSERID = info.HID;
                            model.FTNAME = info.HNAME;
                            model.BYORGNO = info.BYORGNO;
                            model.BIRTH = info.BIRTH;
                            model.SEX = info.SEX;
                            model.BYORGNONAME = StateSwitch.GetOrgNameByOrgNO(info.BYORGNO);
                            model.LINKWAY = info.PHONE;
                            modellist.Add(model);
                        }
                    }
                }
            }
            if (type == "4")//瞭望台
            {
                if (flag == "1")//子目录
                {
                    var model = new PersonModel();
                    var info = DC_WATCHTOWERUSERCls.getListModel(new DC_WATCHTOWERUSER_SW { WATCHTOWERID = id }).FirstOrDefault();
                    model.DC_FULLTIMEUSERID = info.DC_WATCHTOWERUSERID;
                    model.FTNAME = info.FTNAME;
                    model.BYORGNO = info.WATCHTOWERID;
                    model.BIRTH = info.BIRTH;
                    model.SEX = info.SEX;
                    var tower = DC_WATCHTOWERCls.getListModel(new DC_WATCHTOWER_SW { DC_WATCHTOWERID = info.WATCHTOWERID }).FirstOrDefault();
                    model.BYORGNONAME = tower.WATCHNAME;
                    model.LINKWAY = info.LINKWAY;
                    modellist.Add(model);
                }
                else
                {
                    var model = new PersonModel();
                    var towerlist = DC_WATCHTOWERCls.getListModel(new DC_WATCHTOWER_SW { BYORGNO = id });
                    if (towerlist.Any())
                    {
                        foreach (var tower in towerlist)
                        {
                            var infolist = DC_WATCHTOWERUSERCls.getListModel(new DC_WATCHTOWERUSER_SW { WATCHTOWERID = tower.DC_WATCHTOWERID });
                            foreach (var info in infolist)
                            {
                                model.DC_FULLTIMEUSERID = info.DC_WATCHTOWERUSERID;
                                model.FTNAME = info.FTNAME;
                                model.BYORGNO = info.WATCHTOWERID;
                                model.BIRTH = info.BIRTH;
                                model.SEX = info.SEX;
                                model.BYORGNONAME = tower.WATCHNAME;
                                model.LINKWAY = info.LINKWAY;
                                modellist.Add(model);
                            }
                        }
                    }
                }
            }
            if (modellist.Any())//遍历人员信息
            {
                foreach (var model in modellist)
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", model.FTNAME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", model.SEX == "0" ? "男" : "女");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", ClsSwitch.SwitDate(model.BIRTH));
                    sb.AppendFormat("<td class=\"center\">{0}</td>", model.BYORGNONAME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", model.USERJOB);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", model.LINKWAY);
                    sb.AppendFormat("</tr>");
                }
            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\" colspan=\"6\"><em>未查询到信息</em></td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }

        /// <summary>
        /// 火险预案等级
        /// </summary>
        /// <returns></returns>
        public JsonResult GetYALevel()
        {
            MessageListObject ms = null;
            string id = Request.Params["id"];//机构编码
            string type = Request.Params["type"];//预案等级
            var lista = JC_FIRECls.GetListModel(new JC_FIRE_SW { ISOUTFIRE = "1" });//火情表 以灭火
            var listb = JC_FIRE_PROPCls.getListModel(new JC_FIRE_PROP_SW { FIRELEVEL = type.Trim() });//火灾等级list
            int strlen = 9;
            var bo = PublicCls.OrgIsShi(id);//市 
            if (bo)
            {
                strlen = 4;
            }
            else
            {
                var bb = PublicCls.OrgIsXian(id);//县 
                if (bb)
                {
                    strlen = 6;
                }
            }
            //选出所属火情List
            var resultist = from a in lista
                            join b in listb on a.JCFID equals b.JCFID
                            where a.BYORGNO.StartsWith(id.Substring(0, strlen))
                            select new YAFireLevelInfoModel
                            {
                                JCFID = a.JCFID,
                                FIRENAME = a.FIRENAME,
                                FIRETIME = a.FIRETIME,
                                FIREENDTIME = a.FIREENDTIME,
                                ISOUTFIRE = a.ISOUTFIRE,
                                ISOUTFIRENAME = a.ISOUTFIRE == "1" ? "是" : "否",
                                JD = a.JD,
                                WD = a.WD,
                                MGSD = b.MGSD,
                                MGSDNAME = b.MGSD == "1" ? "是" : "否",
                                GHMJ = b.GHMJ,
                                GHLDMJ = b.GHLDMJ,
                                SHSLMJ = b.SHSLMJ,
                                RYS = b.RYS,
                                RYW = b.RYW,
                                ZDQY = b.ZDQY,
                                ZDQYNAME = b.ZDQY == "1" ? "是" : "否",
                                GJJL = b.GJJL,
                                ZZH = b.ZZH,
                                QHS = b.QHS,
                                SSJB = b.SSJB,
                                FIRELEVEL = b.FIRELEVEL
                            };
            ms = new MessageListObject(true, resultist.ToList());
            return Json(ms);
        }
        #endregion

        #region 仓库管理
        /// <summary>
        /// 物资明细管理
        /// </summary>
        /// <returns></returns>
        public ActionResult DETAILSManger()
        {
            DC_DETAILS_Model m = new DC_DETAILS_Model();
            string SUPID = Request.Params["SUPID"];
            string REPID = Request.Params["REPID"];
            string DCREPSUPCOUNT = Request.Params["DCREPSUPCOUNT"];
            string DCSUPPROPUNIT = Request.Params["DCSUPPROPUNIT"];
            string Method = Request.Params["Method"];
            string returnUrl = Request.Params["returnUrl"];
            if (string.IsNullOrEmpty(returnUrl))
                m.returnUrl = "/DataCenter/DEPTlist";
            m.SUPID = SUPID;
            m.REPID = REPID;
            m.CountID = Request.Params["CountID"];
            m.DCREPSUPCOUNT = DCREPSUPCOUNT;
            if (string.IsNullOrEmpty(DCREPSUPCOUNT) == true)
                m.DCREPSUPCOUNT = "0";
            m.opMethod = Method;
            m.DCUSERORG = Request.Params["DCUSERORG"];
            m.DCENTYMANID = SystemCls.getUserID();
            m.DCZHIBIAOREN = Request.Params["DCZHIBIAOREN"];
            m.DCFAFANGREN = Request.Params["DCFAFANGREN"];
            m.returnUrl = returnUrl;
            m.DCUSERID = Request.Params["DCUSERID"];
            m.DCCUSTODIANID = Request.Params["DCCUSTODIANID"];
            m.PRICE = Request.Params["PRICE"];
            m.DCREPTIME = Request.Params["DCREPTIME"];
            m.MARK = Request.Params["MARK"];
            m.NUMBER = DC_DETAILSCls.getnumber(DC_DETAILSCls.swDate(DateTime.Now.ToString()));
            if (string.IsNullOrEmpty(m.DCZHIBIAOREN))
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入制单人", "")), "text/html;charset=UTF-8");
            if (m.opMethod == "EXPORT")
            {
                m.DCREPFLAG = "1";
                if (string.IsNullOrEmpty(m.DCFAFANGREN))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入发放人", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.DCUSERORG))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入调入单位", "")), "text/html;charset=UTF-8");
            }
            if (m.opMethod == "INPORT")
            {
                m.DCREPFLAG = "0";
            }
            if (string.IsNullOrEmpty(DCSUPPROPUNIT) == false)
            {
                DC_SUPPLIESPROP_Model m1 = new DC_SUPPLIESPROP_Model();
                m1.DC_SUPPLIESPROP_ID = m.SUPID;
                m1.DCSUPPROPUNIT = DCSUPPROPUNIT;
                m.opMethod = "Mdyunit";
                DC_SUPPLIESPROPCls.Manager(m1);
            }
            return Content(JsonConvert.SerializeObject(DC_DETAILSCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 仓库管理首页-展示盘点的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DEPTlist()
        {
            pubViewBag("011008", "011008", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.suppliename = DC_SUPPLIESPROPCls.getnameSelectOption(new DC_SUPPLIESPROP_SW { isShowAll = "1" });
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.supunit = DC_SUPPLIESPROPCls.getSelectOption(new DC_SUPPLIESPROP_SW { });
            ViewBag.user = T_IPSFR_USERCls.getuser(new T_SYSSEC_IPSUSER_SW { });
            //ViewBag.isexport = (SystemCls.isRight("011008002")) ? "1" : "0";
            //ViewBag.isinport = (SystemCls.isRight("011008003")) ? "1" : "0";
            ViewBag.repository = DC_REPOSITORYCls.getSelectOption(new DC_REPOSITORY_SW { });
            ViewBag.isAdd = (SystemCls.isRight("011008004")) ? "1" : "0";
            return View();
        }
        /// <summary>
        /// 仓库的列表展示
        /// </summary>
        /// <returns></returns>
        public ActionResult getREPOSITORYlist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string name = Request.Params["NAME"];
            string orgno = Request.Params["BYORGNO"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>所属县市</th>");
            sb.AppendFormat("<th>所属乡镇</th>");
            sb.AppendFormat("<th>名称</th>");
            sb.AppendFormat("<th>负责人</th>");
            sb.AppendFormat("<th>联系方式</th>");
            sb.AppendFormat("<th>地址</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = DC_REPOSITORYCls.getModelList(new DC_REPOSITORY_SW { NAME = name, BYORGNO = orgno, curPage = int.Parse(page), pageSize = int.Parse(PageSize) }, out total);
            foreach (var s in result)
            {
                //if (i % 2 == 0)
                //    sb.AppendFormat("<tr>");
                //else
                sb.AppendFormat("<tr class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGXSName);
                if (string.IsNullOrEmpty(s.ORGName))
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "--");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGName);
                }
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.RESPONSIBLEMAN);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.LINKWAY);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.ADDRESS);
                sb.AppendFormat("    <td class=\" \">");
                if (string.IsNullOrEmpty(s.JD))
                {
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                }
                else
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\" Position('DC_REPOSITORY','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.DCREPOSITORYID, s.NAME);
                }
                sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", s.DCREPOSITORYID, "DCREPOSITORY");
                if (SystemCls.isRight("011008005") == true)
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", s.DCREPOSITORYID, page);
                }
                if (SystemCls.isRight("011008002") == true)
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\"getSupplies('{0}','{1}')\" title='入库' class=\"searchBox_01 LinkMdy\">入库</a>", s.DCREPOSITORYID, s.NAME);
                }
                if (SystemCls.isRight("011008003") == true)
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\"getSupplie('{0}','{1}')\" title='出库' class=\"searchBox_01 LinkMdy\">出库</a>", s.DCREPOSITORYID, s.NAME);
                }
                sb.AppendFormat("<a href=\"#\" onclick=\"showSupplie('{0}','{1}')\" title='物资统计' class=\"searchBox_01 LinkMdy\">物资统计</a>", s.DCREPOSITORYID, s.NAME);
                sb.AppendFormat("<a href=\"#\" onclick=\"Detail('{0}','{1}')\" title='明细查询' class=\"searchBox_01 LinkMdy\">明细查询</a>", s.DCREPOSITORYID, s.NAME);
                if (SystemCls.isRight("011008006") == true)
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.DCREPOSITORYID, page);
                }
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;

            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }
        /// <summary>
        /// 根据仓库id获取单条信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetREPOSITORYjson()
        {
            string DCREPOSITORYID = Request.Params["DCREPOSITORYID"];
            return Content(JsonConvert.SerializeObject(DC_REPOSITORYCls.getModel(new DC_REPOSITORY_SW { DCREPOSITORYID = DCREPOSITORYID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 仓库明细页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailIndex()
        {
            string repid = Request.Params["REPID"];
            ViewBag.repid = repid;
            string NAME = Request.Params["NAME"];
            ViewBag.NAME = NAME;
            ViewBag.suppliename = DC_SUPPLIESCls.getnameSelectOption(new DC_SUPPLIES_SW { isShowAll = "1", REPID = repid });
            return View();
        }
        /// <summary>
        /// 出入库查询
        /// </summary>
        /// <returns></returns>
        public ActionResult DEPTdetaillist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string supid = Request.Params["SUPID"];
            string repid = Request.Params["REPID"];
            string depotman = DC_REPOSITORYCls.getdepotname(repid);
            string dcrepflag = Request.Params["DCREPFLAG"];
            string databegin = Request.Params["DateBegin"];
            string dataend = Request.Params["DateEnd"];
            int total = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th class=\"center\" colspan='12'>" + depotman + "仓库出入明细查询表</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style='width:5%;'>序号</th>");
            sb.AppendFormat("<th style='width:10%;'>仓库</th>");
            sb.AppendFormat("<th style='width:10%;'>物资</th>");
            sb.AppendFormat("<th style='width:10%;'>型号</th>");
            sb.AppendFormat("<th style='width:7%;'>出入库</th>");
            sb.AppendFormat("<th style='width:10%;'>时间</th>");
            sb.AppendFormat("<th style='width:5%;'>数量</th>");
            sb.AppendFormat("<th style='width:5%;'>单价(元)</th>");
            sb.AppendFormat("<th style='width:7%;'>经办人</th>");
            sb.AppendFormat("<th style='width:12%;'>调入单位</th>");
            sb.AppendFormat("<th style='width:7%;'>金额(元)</th>");
            sb.AppendFormat("<th style='width:12%;'></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = DC_DETAILSCls.getModelPager(new DC_DETAILS_SW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), SUPID = supid, REPID = repid, DCREPFLAG = dcrepflag, DateBegin = databegin, DateEnd = dataend }, out total);
            int i = 0;
            foreach (var s in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.DPNAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.SUPNAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.DCSUPPROPMODEL);
                if (s.DCREPFLAG == "0")
                {
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", "入库");
                }
                if (s.DCREPFLAG == "1")
                {
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", "出库");
                }
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.DCREPTIME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.DCREPSUPCOUNT);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.PRICE);

                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.DCUSERID);

                if (s.DCREPFLAG == "1")
                {
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.DCUSERORG);
                }
                if (s.DCREPFLAG == "0")
                {
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", "--");
                }
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.SUM);
                sb.AppendFormat("    </td>");
                if (s.DCREPFLAG == "1")
                {
                    sb.AppendFormat("<td class=\"\">");
                    sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.DCDETAILSID, "DC_DETAILS");
                    sb.AppendFormat("<a href=\"#\" onclick=\"EXPrint('{0}','{1}')\" title='打印' class=\"searchBox_01 LinkPrint\">打印</a>", s.NUMBER, s.DPNAME);
                    sb.AppendFormat("</td>");
                }
                if (s.DCREPFLAG == "0")
                {
                    sb.AppendFormat("<td class=\"\">");
                    sb.AppendFormat("&nbsp;<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.DCDETAILSID, "DC_DETAILS");
                    sb.AppendFormat("&nbsp;<a href=\"#\" onclick=\"INPrint('{0}','{1}')\" title='打印' class=\"searchBox_01 LinkPrint\">打印</a>", s.NUMBER, s.DPNAME);
                    sb.AppendFormat("</td>");
                }
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }
        /// <summary>
        /// 点击仓库-取出所有的物资数量情况
        /// </summary>
        /// <returns></returns>
        public ActionResult getSuppliesCount()
        {
            string ID = Request.Params["ID"];
            string depotman = DC_REPOSITORYCls.getdepotname(ID);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th class=\"center\" colspan='3'>" + depotman + "仓库物资统计</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style='width:10%;'>序号</th>");
            sb.AppendFormat("<th style='width:35%;'>物资</th>");
            sb.AppendFormat("<th style='width:35%;'>数量</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = DC_SUPPLIESCls.getModelList(new DC_SUPPLIES_SW { REPID = ID });
            int i = 0;
            foreach (var s in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center  \">{0}</td>", s.SUPNAME);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", s.DCSUPCOUNT);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 仓库管理-增删改
        /// </summary>
        /// <returns></returns>
        public ActionResult DEPTManager()
        {
            DC_REPOSITORY_Model m = new DC_REPOSITORY_Model();
            m.DCREPOSITORYID = Request.Params["DCREPOSITORYID"];
            m.REPTYPEID = Request.Params["REPTYPEID"];
            m.NAME = Request.Params["NAME"];
            m.ADDRESS = Request.Params["ADDRESS"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.RESPONSIBLEMAN = Request.Params["RESPONSIBLEMAN"];
            m.LINKWAY = Request.Params["LINKWAY"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.opMethod = Request.Params["Method"];
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.NAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入仓库名称！", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.RESPONSIBLEMAN))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入仓库负责人！", "")), "text/html;charset=UTF-8");
            }
            var ms = DC_REPOSITORYCls.Manager(m);
            if (ms.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, ms.Msg, "")), "text/html;charset=UTF-8");
            if (ConfigCls.getSDEDBTeam() == "1")//配置文件
            {
                TD_REPOSITORY_Model m1 = new TD_REPOSITORY_Model();
                if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
                {
                    //double[] arr = ClsPositionUtil.gcj_To_Gps84(double.Parse(m.WD), double.Parse(m.JD));
                    double[] arr = ClsPositionTrans.GpsTransform(double.Parse(m.WD), double.Parse(m.JD), ConfigCls.getSDELonLatTransform());
                    m1.JD = arr[1].ToString();
                    m1.WD = arr[0].ToString();
                }
                m1.opMethod = m.opMethod;
                m1.NAME = m.NAME;
                m1.TYPE = "1";
                //geometry::STGeomFromText('POINT(103.397553 23.365441)',4326)
                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                //m1.Shape = "POINT(" + m.JD + " " + m.WD + ")";
                if (m.opMethod != "Del")
                {
                    m1.OBJECTID = ms.Url;
                }
                else
                {
                    m1.OBJECTID = m.DCREPOSITORYID;
                }
                if ((string.IsNullOrEmpty(m.JD) || string.IsNullOrEmpty(m.WD)) && m1.opMethod == "Add")
                {

                }
                else
                {
                    TD_REPOSITORYCls.Manager(m1);
                }

            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        #endregion

        #region 车辆管理(新)
        /// <summary>
        /// 车辆管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CarIndex()
        {
            pubViewBag("011009", "011009", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.cartype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "33", isShowAll = "1" });
            ViewBag.cartypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "33" });
            ViewBag.isAdd = (SystemCls.isRight("011009002")) ? "1" : "0";
            return View();
        }
        /// <summary>
        /// 获取查看和修改前的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCarjson()
        {
            string DC_CAR_ID = Request.Params["DC_CAR_ID"];
            return Content(JsonConvert.SerializeObject(DC_CARCls.getModel(new DC_CAR_SW { DC_CAR_ID = DC_CAR_ID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 车辆增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult CarManager()
        {
            DC_CAR_Model m = new DC_CAR_Model();
            m.DC_CAR_ID = Request.Params["DC_CAR_ID"];
            m.CARTYPE = Request.Params["CARTYPE"];
            m.NUMBER = Request.Params["NUMBER"];
            m.NAME = Request.Params["NAME"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.BUYYEAR = Request.Params["BUYYEAR"];
            m.BUYPRICE = Request.Params["BUYPRICE"];
            m.PLATENUM = Request.Params["PLATENUM"];
            m.DRIVER = Request.Params["DRIVER"];
            m.CONTACTS = Request.Params["CONTACTS"];
            m.GPSEQUIP = Request.Params["GPSEQUIP"];
            m.GPSTELL = Request.Params["GPSTELL"];
            m.STOREADDR = Request.Params["STOREADDR"];
            m.MARK = Request.Params["MARK"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.opMethod = Request.Params["Method"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.NAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入名称", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
            }

            var ms = DC_CARCls.Manager(m);
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 查询-弹出列表展示
        /// </summary>
        /// <returns></returns>
        public ActionResult getcarlist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string cartype = Request.Params["CARTYPE"];
            string name = Request.Params["NAME"];
            string platenum = Request.Params["PLATENUM"];
            string orgno = Request.Params["BYORGNO"];
            string number = Request.Params["NUMBER"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table  cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>所属县市</th>");
            sb.AppendFormat("<th>所属乡镇</th>");
            sb.AppendFormat("<th>车辆类型</th>");
            sb.AppendFormat("<th>名称</th>");
            sb.AppendFormat("<th>编号</th>");
            sb.AppendFormat("<th>号牌</th>");
            sb.AppendFormat("<th>驾驶员</th>");
            sb.AppendFormat("<th>联系方式</th>");
            sb.AppendFormat("<th>储存地点</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = DC_CARCls.getModelList(new DC_CAR_SW { CARTYPE = cartype, NUMBER = number, NAME = name, PLATENUM = platenum, BYORGNO = orgno, curPage = int.Parse(page), pageSize = int.Parse(PageSize) }, out total);
            foreach (var s in result)
            {
                //if (i % 2 == 0)
                //    sb.AppendFormat("<tr style=\"cursor:pointer;\" onclick=\"setColor(this)\">");
                //else
                sb.AppendFormat("<tr  style=\"cursor:pointer;\"  class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGXSName);
                if (string.IsNullOrEmpty(s.ORGName))
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "--");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGName);
                }
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.CARTYPEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NUMBER);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.PLATENUM);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.DRIVER);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.CONTACTS);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.STOREADDR);
                sb.AppendFormat("    <td class=\" \">");
                if (string.IsNullOrEmpty(s.JD))
                {
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                }
                else
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\" Position('DC_CAR','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\" >定位</a>", s.DC_CAR_ID, s.NAME);
                }
                sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.DC_CAR_ID, "DC_CAR");
                if (SystemCls.isRight("011009003") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", s.DC_CAR_ID, page);
                if (SystemCls.isRight("011009004") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.DC_CAR_ID, page);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;

            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }
        #region 车辆上传

        [HttpPost]
        public ActionResult CarList(FormCollection form)
        {
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                //扩展名
                string extension = System.IO.Path.GetExtension(File.FileName);//
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                    string name = DateTime.Now.ToString("车辆导入-yyyyMMddHHmmss") + extension;
                    if (!FileType.Contains(extension))
                    {

                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content(@"<script>alert('上传文件超过4M，不能上传');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/DataCenterExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        //DC_CARCls.CarUpload(savePath);
                        HSSFWorkbook hssfworkbook;
                        FileStream file = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                         hssfworkbook = new HSSFWorkbook(file);
                        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
                        int rowCount = sheet.LastRowNum;
                        for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                        {

                            IRow row = sheet.GetRow(i);
                            string[] arr = new string[12];
                            for (int k = 0; k < arr.Length; k++)
                            {
                                if (k != 6)
                                    arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                                else
                                    arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                            }
                            DC_CAR_Model m = new DC_CAR_Model();
                            //单位	车辆类型	名称	编号	号牌	存储地点	购买年份 购买价格 驾驶员 联系方式 经度 纬度
                            if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                            {
                                continue;
                            }
                            m.BYORGNO = T_SYS_ORGCls.getCodeByName(arr[0]);
                            if (string.IsNullOrEmpty(m.BYORGNO))
                            {
                                return Content(@"<script>alert('表格中组织机构名称错误，请确认后再上传！');history.go(-1);</script>");
                            }
                            m.opMethod = "Add";
                            m.NAME = arr[2];
                            m.NUMBER = arr[3];
                            m.PLATENUM = arr[4];
                            m.STOREADDR = arr[5];
                            m.BUYYEAR = arr[6];
                            if (m.BUYYEAR == "9999-12-31")
                                m.BUYYEAR = "1900-01-01";
                            m.BUYPRICE = arr[7];
                            m.DRIVER = arr[8];
                            m.CONTACTS = arr[9];
                            string jd = arr[10];
                            string wd = arr[11];
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                double[] brr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");
                                m.JD = brr[1].ToString();
                                m.WD = brr[0].ToString();
                            }
                            if (arr[1].Trim() == "指挥车")//装备类型
                            {
                                m.CARTYPE = "1";
                            }
                            else if (arr[1].Trim() == "运兵车")
                            {
                                m.CARTYPE = "2";
                            }
                            else if (arr[1].Trim() == "供水车")
                            {
                                m.CARTYPE = "3";
                            }
                            else if (arr[1].Trim() == "通讯车")
                            {
                                m.CARTYPE = "4";
                            }
                            else if (arr[1].Trim() == "宣传车")
                            {
                                m.CARTYPE = "5";
                            }
                            else
                            {
                                m.CARTYPE = "1";
                            }
                            DC_CARCls.Manager(m);

                            string a = row.GetCell(0).ToString();
                            string a1 = row.GetCell(1).ToString();
                            string a2 = row.GetCell(2).ToString();
                        }
                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传！');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的车辆表格');history.go(-1);</script>");
                }
            }
            return Content("<script>alert('导入成功');window.location.href='CarIndex';</script>");
        }
        #endregion
        #endregion

        #region 装备管理（新）
        /// <summary>
        /// 装备管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EQUIP_NEWIndex()
        {
            pubViewBag("011005", "011005", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.equiptype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "32", isShowAll = "1" });
            ViewBag.equiptypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "32" });
            ViewBag.useatate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36", isShowAll = "1" });
            ViewBag.useatateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" });
            ViewBag.repository = DC_REPOSITORYCls.getSelectOption(new DC_REPOSITORY_SW { BYORGNO = SystemCls.getCurUserOrgNo(), Other = "1" });
            ViewBag.Unit = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "50" });
            ViewBag.isAdd = (SystemCls.isRight("011005002")) ? "1" : "0";
            return View();
        }
        /// <summary>
        /// 获取查看和修改前的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetEquipNewjson()
        {
            string DC_EQUIP_NEW_ID = Request.Params["DC_EQUIP_NEW_ID"];
            return Content(JsonConvert.SerializeObject(DC_EQUIP_NEWCls.getModel(new DC_EQUIP_NEW_SW { DC_EQUIP_NEW_ID = DC_EQUIP_NEW_ID })), "text/html;charset=UTF-8");

        }
        /// <summary>
        /// 装备增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult EquipNewManager()
        {
            DC_EQUIP_NEW_Model m = new DC_EQUIP_NEW_Model();
            m.DC_EQUIP_NEW_ID = Request.Params["DC_EQUIP_NEW_ID"];
            m.EQUIPTYPE = Request.Params["EQUIPTYPE"];
            m.NUMBER = Request.Params["NUMBER"];
            m.NAME = Request.Params["NAME"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.MODEL = Request.Params["MODEL"];
            m.BUYYEAR = Request.Params["BUYYEAR"];
            m.USESTATE = Request.Params["USESTATE"];
            m.STOREADDR = Request.Params["STOREADDR"];
            m.MARK = Request.Params["MARK"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.WORTH = Request.Params["WORTH"];
            m.EQUIPAMOUNT = Request.Params["EQUIPAMOUNT"];
            m.DCSUPPROPUNIT = Request.Params["DCSUPPROPUNIT"];
            m.REPID = Request.Params["REPID"];
            m.PRICE = Request.Params["PRICE"];
            m.opMethod = Request.Params["Method"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.NAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "名称不可为空，请重新输入！", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
            }
            var ms = DC_EQUIP_NEWCls.Manager(m);
            if (ms.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, ms.Msg, "")), "text/html;charset=UTF-8");
            if (ConfigCls.getSDEDBTeam() == "1")//配置文件
            {
                TD_EQUIP_Model m1 = new TD_EQUIP_Model();
                if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
                {
                    double[] arr = ClsPositionTrans.GpsTransform(double.Parse(m.WD), double.Parse(m.JD), ConfigCls.getSDELonLatTransform());
                    m1.JD = arr[1].ToString();
                    m1.WD = arr[0].ToString();
                }
                m1.opMethod = m.opMethod;
                m1.NAME = m.NAME;
                m1.TYPE = m.EQUIPTYPE;
                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                if (m.opMethod != "Del")
                {
                    m1.OBJECTID = ms.Url;
                }
                else
                {
                    m1.OBJECTID = m.DC_EQUIP_NEW_ID;
                }
                if ((string.IsNullOrEmpty(m.JD) || string.IsNullOrEmpty(m.WD)) && m1.opMethod == "Add")
                {

                }
                else
                {
                    TD_EQUIPCls.Manager(m1);
                }
            }
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.REPID) == false && m.REPID != "办公室")
                {
                    DC_SUPPLIESPROP_Model m1 = new DC_SUPPLIESPROP_Model();
                    m1.DCSUPPROPNAME = m.NAME;
                    m1.DCSUPPROPMODEL = m.MODEL;
                    m1.DCSUPPROPUNIT = m.DCSUPPROPUNIT;
                    m1.opMethod = m.opMethod;
                    m1.DC_SUPPLIESPROP_ID = ms.Url;
                    DC_SUPPLIESPROPCls.Manager(m1);
                    DC_SUPPLIES_Model m2 = new DC_SUPPLIES_Model();
                    m2.opMethod = m.opMethod;
                    m2.REPID = m.REPID;
                    m2.SUPID = m1.DC_SUPPLIESPROP_ID;
                    m2.DCSUPCOUNT = m.EQUIPAMOUNT;
                    m2.DCSUPPLIESID = ms.Url;
                    if (string.IsNullOrEmpty(m.PRICE) == false)
                    {
                        m2.PRICE = m.PRICE;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(m.WORTH) && !string.IsNullOrEmpty(m.EQUIPAMOUNT) && m.EQUIPAMOUNT != "0")
                        {
                            m2.PRICE = (double.Parse(m.WORTH) / double.Parse(m.EQUIPAMOUNT)).ToString("f2");
                        }
                        else
                        {
                            m2.PRICE = "0";
                        }
                    }
                    DC_SUPPLIESCls.Manager(m2);
                }

            }
            else
            {
                DC_SUPPLIESPROP_Model m1 = new DC_SUPPLIESPROP_Model();
                m1.DC_SUPPLIESPROP_ID = m.DC_EQUIP_NEW_ID;
                m1.opMethod = m.opMethod;
                DC_SUPPLIESPROPCls.Manager(m1);
                DC_SUPPLIES_Model m2 = new DC_SUPPLIES_Model();
                m2.opMethod = m.opMethod;
                m2.DCSUPPLIESID = m.DC_EQUIP_NEW_ID;
                DC_SUPPLIESCls.Manager(m2);
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 查询-弹出列表展示
        /// </summary>
        /// <returns></returns>
        public ActionResult getEquipNewlist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string equiptype = Request.Params["EQUIPTYPE"];
            string name = Request.Params["NAME"];
            string model = Request.Params["MODEL"];
            string orgno = Request.Params["BYORGNO"];
            string number = Request.Params["NUMBER"];
            string useatate = Request.Params["USESTATE"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>所属县市</th>");
            sb.AppendFormat("<th>所属乡镇</th>");
            sb.AppendFormat("<th>装备类型</th>");
            sb.AppendFormat("<th>名称</th>");
            sb.AppendFormat("<th>编号</th>");
            sb.AppendFormat("<th>型号</th>");
            sb.AppendFormat("<th>使用现状</th>");
            sb.AppendFormat("<th>储存地点</th>");
            sb.AppendFormat("<th>仓库</th>");
            sb.AppendFormat("<th>数量</th>");
            sb.AppendFormat("<th>总价(元)</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = DC_EQUIP_NEWCls.getModelList(new DC_EQUIP_NEW_SW { EQUIPTYPE = equiptype, NUMBER = number, NAME = name, MODEL = model, USESTATE = useatate, BYORGNO = orgno, curPage = int.Parse(page), pageSize = int.Parse(PageSize) }, out total);
            foreach (var s in result)
            {
                //if (i % 2 == 0)
                //    sb.AppendFormat("<tr>");
                //else
                sb.AppendFormat("<tr class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center  \">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center  \">{0}</td>", s.ORGXSName);
                if (string.IsNullOrEmpty(s.ORGName))
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "--");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGName);
                }
                sb.AppendFormat("<td class=\"center  \">{0}</td>", s.EQUIPTYPEName);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", s.NAME);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", s.NUMBER);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", s.MODEL);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", s.USESTATEName);
                sb.AppendFormat("<td class=\"center \">{0}</td>", s.STOREADDR);
                sb.AppendFormat("<td class=\"center \">{0}</td>", s.REPNAME);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", s.EQUIPAMOUNT);
                sb.AppendFormat("<td class=\"center \">{0}</td>", s.WORTH);
                sb.AppendFormat("    <td class=\" \">");
                if (string.IsNullOrEmpty(s.JD))
                {
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                }
                else
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\" Position('DC_EQUIP_NEW','{0}','{1}')\" title='定位'  class=\"searchBox_01 LinkLocation\">定位</a>", s.DC_EQUIP_NEW_ID, s.NAME);
                }
                sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.DC_EQUIP_NEW_ID, "DC_EQUIP_NEW");
                if (SystemCls.isRight("011009003") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑'  class=\"searchBox_01 LinkMdy\">修改</a>", s.DC_EQUIP_NEW_ID, page);
                if (SystemCls.isRight("011009004") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.DC_EQUIP_NEW_ID, page);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }


        #region 装备上传

        [HttpPost]
        public ActionResult EquipList(FormCollection form)
        {
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                //扩展名
                string extension = System.IO.Path.GetExtension(File.FileName);//
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                    string name = DateTime.Now.ToString("装备导入-yyyyMMddHHmmss") + extension;
                    if (!FileType.Contains(extension))
                    {

                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content(@"<script>alert('上传文件超过4M，不能上传');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/DataCenterExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        //DC_EQUIP_NEWCls.EQUIPUpload(savePath);
                        HSSFWorkbook hssfworkbook;
                        FileStream file = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                        hssfworkbook = new HSSFWorkbook(file);
                        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
                        int rowCount = sheet.LastRowNum;
                        for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                        {
                            IRow row = sheet.GetRow(i);
                            string[] arr = new string[12];
                            for (int k = 0; k < arr.Length; k++)
                            {
                                if (k != 6)
                                    arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                                else
                                    arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                            }
                            DC_EQUIP_NEW_Model m = new DC_EQUIP_NEW_Model();
                            //单位	装备类型	名称	编号	型号	使用现状	购买年份 存储地点 数量 价值 经度 纬度
                            if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                            {
                                continue;
                            }
                            m.BYORGNO = T_SYS_ORGCls.getCodeByName(arr[0]);
                            if (string.IsNullOrEmpty(m.BYORGNO))
                            {
                                return Content(@"<script>alert('表格中组织机构名称错误，请确认后再上传！');history.go(-1);</script>");
                            }
                            m.opMethod = "Add";
                            m.NAME = arr[2];
                            m.NUMBER = arr[3];
                            m.MODEL = arr[4];
                            m.BUYYEAR = arr[6];
                            if (m.BUYYEAR == "9999-12-31")
                                m.BUYYEAR = "1900-01-01";
                            m.STOREADDR = arr[7];
                            m.EQUIPAMOUNT = arr[8];
                            m.WORTH = arr[9];
                            string jd = arr[10];
                            string wd = arr[11];
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                double[] brr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");
                                m.JD = brr[1].ToString();
                                m.WD = brr[0].ToString();
                            }
                            if (arr[1].Trim() == "扑救类")//装备类型
                            {
                                m.EQUIPTYPE = "1";
                            }
                            else if (arr[1].Trim() == "阻隔类")
                            {
                                m.EQUIPTYPE = "2";
                            }
                            else if (arr[1].Trim() == "防护类")
                            {
                                m.EQUIPTYPE = "3";
                            }
                            else if (arr[1].Trim() == "通讯类")
                            {
                                m.EQUIPTYPE = "4";
                            }
                            else if (arr[1].Trim() == "户外类")
                            {
                                m.EQUIPTYPE = "5";
                            }
                            else if (arr[1].Trim() == "运输类")
                            {
                                m.EQUIPTYPE = "6";
                            }
                            else
                            {
                                m.EQUIPTYPE = "1";
                            }
                            if (arr[5].Trim() == "在用")//使用类型
                            {
                                m.USESTATE = "1";
                            }
                            else if (arr[5].Trim() == "储存")
                            {
                                m.USESTATE = "2";
                            }
                            else if (arr[5].Trim() == "报废")
                            {
                                m.USESTATE = "3";
                            }
                            else
                            {
                                m.USESTATE = "1";
                            }
                            var ms = DC_EQUIP_NEWCls.Manager(m);
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                TD_EQUIP_Model m1 = new TD_EQUIP_Model();
                                m1.OBJECTID = ms.Url;
                                m1.NAME = m.NAME;
                                m1.opMethod = "Add";
                                m1.TYPE = m.EQUIPTYPE;
                                m1.JD = jd;
                                m1.WD = wd;
                                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                                TD_EQUIPCls.Manager(m1);
                            }
                            string a = row.GetCell(0).ToString();
                            string a1 = row.GetCell(1).ToString();
                            string a2 = row.GetCell(2).ToString();
                        }
                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传！');history.go(-1);</script>");
                    }
                
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的装备表格');history.go(-1);</script>");
                }
            }
            return Content("<script>alert('导入成功');window.location.href='EQUIP_NEWIndex';</script>");
        
        }
        #endregion
        #endregion

        #region 设施管理（新）

        #region 设施-营房
        /// <summary>
        /// 营房页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CAMPIndex()
        {
            pubViewBag("016001", "016001", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.structure = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "34", isShowAll = "1" });
            ViewBag.structureadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "34" });
            ViewBag.isAdd = (SystemCls.isRight("016001002")) ? "1" : "0";
            return View();
        }
        /// <summary>
        /// 根据序号获取内容
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCAMPjson()
        {
            string DC_UTILITY_CAMP_ID = Request.Params["DC_UTILITY_CAMP_ID"];
            //if (string.IsNullOrEmpty(ID))
            //    ID = "0";
            return Content(JsonConvert.SerializeObject(DC_UTILITY_CAMPCls.getModel(new DC_UTILITY_CAMP_SW { DC_UTILITY_CAMP_ID = DC_UTILITY_CAMP_ID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 营房管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CAMPManager()
        {
            DC_UTILITY_CAMP_Model m = new DC_UTILITY_CAMP_Model();
            m.DC_UTILITY_CAMP_ID = Request.Params["DC_UTILITY_CAMP_ID"];
            m.NUMBER = Request.Params["NUMBER"];
            m.NAME = Request.Params["NAME"];
            m.ORGNOS = Request.Params["ORGNOS"];
            m.AREA = Request.Params["AREA"];
            m.BUILDDATE = Request.Params["BUILDDATE"];
            m.FLOOR = Request.Params["FLOOR"];
            m.STRUCTURETYPE = Request.Params["STRUCTURETYPE"];
            m.SUBFACILITIES = Request.Params["SUBFACILITIES"];
            m.MARK = Request.Params["MARK"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.WORTH = Request.Params["WORTH"];
            m.opMethod = Request.Params["Method"];
            m.BUILDDATEBEGIN = Request.Params["BUILDDATEBEGIN"];
            m.BUILDDATEEND = Request.Params["BUILDDATEEND"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.NAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "名称不可为空，请重新输入！", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
            }
            var ms = DC_UTILITY_CAMPCls.Manager(m);
            if (ms.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, ms.Msg, "")), "text/html;charset=UTF-8");
            if (ConfigCls.getSDEDBTeam() == "1")//配置文件
            {
                TD_CAMP_Model m1 = new TD_CAMP_Model();
                if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
                {
                    //double[] arr = ClsPositionUtil.gcj_To_Gps84(double.Parse(m.WD), double.Parse(m.JD));
                    double[] arr = ClsPositionTrans.GpsTransform(double.Parse(m.WD), double.Parse(m.JD), ConfigCls.getSDELonLatTransform());
                    m1.JD = arr[1].ToString();
                    m1.WD = arr[0].ToString();
                }
                m1.opMethod = m.opMethod;
                m1.NAME = m.NAME;
                m1.TYPE = m.STRUCTURETYPE;
                //geometry::STGeomFromText('POINT(103.397553 23.365441)',4326)
                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                //m1.Shape = "POINT(" + m.JD + " " + m.WD + ")";
                if (m.opMethod != "Del")
                {
                    m1.OBJECTID = ms.Url;
                }
                else
                {
                    m1.OBJECTID = m.DC_UTILITY_CAMP_ID;
                }
                if ((string.IsNullOrEmpty(m.JD) || string.IsNullOrEmpty(m.WD)) && m1.opMethod == "Add")
                {

                }
                else
                {
                    TD_CAMPCls.Manager(m1);
                }
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getCAMPlist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string structuretype = Request.Params["STRUCTURETYPE"];
            string name = Request.Params["NAME"];
            string number = Request.Params["NUMBER"];
            string orgno = Request.Params["ORGNOS"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th >序号</th>");
            sb.AppendFormat("<th >所属县市</th>");
            sb.AppendFormat("<th >所属乡镇</th>");
            sb.AppendFormat("<th >结构类型</th>");
            sb.AppendFormat("<th >名称</th>");
            sb.AppendFormat("<th >编号</th>");
            sb.AppendFormat("<th >建筑面积(平方米)</th>");
            sb.AppendFormat("<th >楼层</th>");
            sb.AppendFormat("<th >建成日期</th>");
            sb.AppendFormat("<th >附属设施</th>");
            sb.AppendFormat("<th >总价(元)</th>");
            sb.AppendFormat("<th ></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = DC_UTILITY_CAMPCls.getModelList(new DC_UTILITY_CAMP_SW { STRUCTURETYPE = structuretype, NUMBER = number, ORGNOS = orgno, NAME = name, curPage = int.Parse(page), pageSize = int.Parse(PageSize) }, out total);
            foreach (var s in result)
            {
                //if (i % 2 == 0)
                //    sb.AppendFormat("<tr>");
                //else
                sb.AppendFormat("<tr class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGXSName);
                if (string.IsNullOrEmpty(s.ORGName))
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "--");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGName);
                }
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.STRUCTURETYPEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NUMBER);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.AREA);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.FLOOR);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.BUILDDATE);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.SUBFACILITIES);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.WORTH);
                sb.AppendFormat("    <td class=\" \">");
                if (string.IsNullOrEmpty(s.JD))
                {
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                }
                else
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\"Position('DC_UTILITY_CAMP','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.DC_UTILITY_CAMP_ID, s.NAME);
                }
                sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.DC_UTILITY_CAMP_ID, "DC_UTILITY_CAMP");
                sb.AppendFormat("<a href=\"#\" onclick=\"Photo('{0}','{1}')\" title='照片' class=\"searchBox_01 LinkPhoto\">照片</a>", s.DC_UTILITY_CAMP_ID, "DC_UTILITY_CAMP");
                if (SystemCls.isRight("016001003") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", s.DC_UTILITY_CAMP_ID, page);
                if (SystemCls.isRight("016001004") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.DC_UTILITY_CAMP_ID, page);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }
        #region 营房上传

        [HttpPost]
        public ActionResult CAMPList(FormCollection form)
        {
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                //扩展名
                string extension = System.IO.Path.GetExtension(File.FileName);//
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                    string name = DateTime.Now.ToString("营房导入-yyyyMMddHHmmss") + extension;
                    if (!FileType.Contains(extension))
                    {

                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content(@"<script>alert('上传文件超过4M，不能上传');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/DataCenterExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        //DC_UTILITY_CAMPCls.CAMPUpload(savePath);
                        HSSFWorkbook hssfworkbook;
                        FileStream file = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                            hssfworkbook = new HSSFWorkbook(file);
                        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
                        int rowCount = sheet.LastRowNum;
                        for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                        {

                            IRow row = sheet.GetRow(i);
                            string[] arr = new string[12];
                            for (int k = 0; k < arr.Length; k++)
                            {
                                if (k != 6)
                                    arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                                else
                                    arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                            }
                            DC_UTILITY_CAMP_Model m = new DC_UTILITY_CAMP_Model();
                            //单位	结构类型 名称 编号 建筑面积 楼层 建设日期 附属设施 价值 经度 纬度
                            if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                            {
                                continue;
                            }
                            m.ORGNOS = T_SYS_ORGCls.getCodeByName(arr[0]);
                            if (string.IsNullOrEmpty(m.ORGNOS))
                            {
                                return Content(@"<script>alert('表格中组织机构名称错误，请确认后再上传！');history.go(-1);</script>");
                                
                            }
                            m.opMethod = "Add";
                            m.NAME = arr[2];
                            m.NUMBER = arr[3];
                            m.AREA = arr[4];
                            m.FLOOR = arr[5];
                            m.BUILDDATE = arr[6];
                            if (m.BUILDDATE == "9999-12-31")
                                m.BUILDDATE = "1900-01-01";
                            m.SUBFACILITIES = arr[7];
                            m.WORTH = arr[8];
                            string jd = arr[9];
                            string wd = arr[10];
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                double[] brr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");
                                m.JD = brr[1].ToString();
                                m.WD = brr[0].ToString();
                            }
                            if (arr[1].Trim() == "钢构")//装备类型
                            {
                                m.STRUCTURETYPE = "1";
                            }
                            else if (arr[1].Trim() == "砖混")
                            {
                                m.STRUCTURETYPE = "2";
                            }
                            else if (arr[1].Trim() == "钢混")
                            {
                                m.STRUCTURETYPE = "3";
                            }
                            else
                            {
                                m.STRUCTURETYPE = "1";
                            }
                            var ms = DC_UTILITY_CAMPCls.Manager(m);
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                TD_CAMP_Model m1 = new TD_CAMP_Model();
                                m1.OBJECTID = ms.Url;
                                m1.opMethod = "Add";
                                m1.NAME = m.NAME;
                                m1.TYPE = m.STRUCTURETYPE;
                                m1.JD = jd;
                                m1.WD = wd;
                                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                                TD_CAMPCls.Manager(m1);
                            }
                            string a = row.GetCell(0).ToString();
                            string a1 = row.GetCell(1).ToString();
                            string a2 = row.GetCell(2).ToString();
                        }
                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传！');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的营房表格');history.go(-1);</script>");
                }
            }
            return Content("<script>alert('导入成功');window.location.href='CAMPIndex';</script>");
        }
        #endregion
        #endregion

        #region 设施-瞭望台
        /// <summary>
        /// 瞭望台页面
        /// </summary>
        /// <returns></returns>
        public ActionResult OVERWATCHIndex()
        {
            pubViewBag("016002", "016002", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.structure = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "34", isShowAll = "1" });
            ViewBag.structureadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "34" });
            ViewBag.isAdd = (SystemCls.isRight("016002002")) ? "1" : "0";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOVERWATCHjson()
        {
            string DC_UTILITY_OVERWATCH_ID = Request.Params["DC_UTILITY_OVERWATCH_ID"];
            //if (string.IsNullOrEmpty(ID))
            //    ID = "0";
            return Content(JsonConvert.SerializeObject(DC_UTILITY_OVERWATCHCls.getModel(new DC_UTILITY_OVERWATCH_SW { DC_UTILITY_OVERWATCH_ID = DC_UTILITY_OVERWATCH_ID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 瞭望台管理
        /// </summary>
        /// <returns></returns>
        public ActionResult OVERWATCHManager()
        {
            DC_UTILITY_OVERWATCH_Model m = new DC_UTILITY_OVERWATCH_Model();
            m.DC_UTILITY_OVERWATCH_ID = Request.Params["DC_UTILITY_OVERWATCH_ID"];
            m.NUMBER = Request.Params["NUMBER"];
            m.NAME = Request.Params["NAME"];
            m.ORGNOS = Request.Params["ORGNOS"];
            m.AREA = Request.Params["AREA"];
            m.BUILDDATE = Request.Params["BUILDDATE"];
            m.FLOOR = Request.Params["FLOOR"];
            m.STRUCTURETYPE = Request.Params["STRUCTURETYPE"];
            m.SUBFACILITIES = Request.Params["SUBFACILITIES"];
            m.MARK = Request.Params["MARK"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.WORTH = Request.Params["WORTH"];
            m.opMethod = Request.Params["Method"];
            m.BUILDDATEBEGIN = Request.Params["BUILDDATEBEGIN"];
            m.BUILDDATEEND = Request.Params["BUILDDATEEND"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.NAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "名称不可为空，请重新输入！", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
            }
            var ms = DC_UTILITY_OVERWATCHCls.Manager(m);
            if (ms.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, ms.Msg, "")), "text/html;charset=UTF-8");
            if (ConfigCls.getSDEDBTeam() == "1")//配置文件
            {
                TD_OVERWATCH_Model m1 = new TD_OVERWATCH_Model();
                if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
                {
                    double[] arr = ClsPositionTrans.GpsTransform(double.Parse(m.WD), double.Parse(m.JD), ConfigCls.getSDELonLatTransform());
                    m1.JD = arr[1].ToString();
                    m1.WD = arr[0].ToString();
                }
                m1.opMethod = m.opMethod;
                m1.NAME = m.NAME;
                m1.TYPE = m.STRUCTURETYPE;
                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                if (m.opMethod != "Del")
                {
                    m1.OBJECTID = ms.Url;
                }
                else
                {
                    m1.OBJECTID = m.DC_UTILITY_OVERWATCH_ID;
                }
                if ((string.IsNullOrEmpty(m.JD) || string.IsNullOrEmpty(m.WD)) && m1.opMethod == "Add")
                {

                }
                else
                {
                    TD_OVERWATCHCls.Manager(m1);
                }
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getOVERWATCHlist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string structuretype = Request.Params["STRUCTURETYPE"];
            string name = Request.Params["NAME"];
            string number = Request.Params["NUMBER"];
            string orgno = Request.Params["ORGNOS"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>所属县市</th>");
            sb.AppendFormat("<th>所属乡镇</th>");
            sb.AppendFormat("<th>结构类型</th>");
            sb.AppendFormat("<th>名称</th>");
            sb.AppendFormat("<th>编号</th>");
            sb.AppendFormat("<th>建筑面积(平方米)</th>");
            sb.AppendFormat("<th>楼层</th>");
            sb.AppendFormat("<th>建成日期</th>");
            sb.AppendFormat("<th>附属设施</th>");
            sb.AppendFormat("<th>总价(元)</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = DC_UTILITY_OVERWATCHCls.getModelList(new DC_UTILITY_OVERWATCH_SW { STRUCTURETYPE = structuretype, NUMBER = number, ORGNOS = orgno, NAME = name, curPage = int.Parse(page), pageSize = int.Parse(PageSize) }, out total);
            foreach (var s in result)
            {
                //if (i % 2 == 0)
                //    sb.AppendFormat("<tr>");
                //else
                sb.AppendFormat("<tr class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGXSName);
                if (string.IsNullOrEmpty(s.ORGName))
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "--");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGName);
                }
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.STRUCTURETYPEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NUMBER);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.AREA);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.FLOOR);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.BUILDDATE);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.SUBFACILITIES);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.WORTH);
                sb.AppendFormat("    <td class=\" \">");
                if (string.IsNullOrEmpty(s.JD))
                {
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                }
                else
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\" Position('DC_UTILITY_OVERWATCH','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.DC_UTILITY_OVERWATCH_ID, s.NAME);
                }
                sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.DC_UTILITY_OVERWATCH_ID, "DC_UTILITY_OVERWATCH");
                sb.AppendFormat("<a href=\"#\" onclick=\" Photo('{0}','{1}')\" title='照片管理' class=\"searchBox_01 LinkPhoto\">照片</a>", s.DC_UTILITY_OVERWATCH_ID, "DC_UTILITY_OVERWATCH");
                if (SystemCls.isRight("016002003") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", s.DC_UTILITY_OVERWATCH_ID, page);
                if (SystemCls.isRight("016002004") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.DC_UTILITY_OVERWATCH_ID, page);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }
        #region 瞭望台上传

        [HttpPost]
        public ActionResult OVERWATCHList(FormCollection form)
        {
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                //扩展名
                string extension = System.IO.Path.GetExtension(File.FileName);//
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                    string name = DateTime.Now.ToString("瞭望台导入-yyyyMMddHHmmss") + extension;
                    if (!FileType.Contains(extension))
                    {

                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content(@"<script>alert('上传文件超过4M，不能上传');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/DataCenterExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        //DC_UTILITY_OVERWATCHCls.OVERWATCHUpload(savePath);
                        HSSFWorkbook hssfworkbook;
                        
                            FileStream file = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                            hssfworkbook = new HSSFWorkbook(file);
                        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
                        int rowCount = sheet.LastRowNum;
                        for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                        {

                            IRow row = sheet.GetRow(i);
                            string[] arr = new string[11];
                            for (int k = 0; k < arr.Length; k++)
                            {
                                if (k != 6)
                                    arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                                else
                                    arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                            }
                            DC_UTILITY_OVERWATCH_Model m = new DC_UTILITY_OVERWATCH_Model();
                            //单位	结构类型	名称	编号	建筑面积	楼层	建设日期 附属设施  价值 经度 纬度
                            if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                            {
                                continue;
                            }
                            m.ORGNOS = T_SYS_ORGCls.getCodeByName(arr[0]);
                            if (string.IsNullOrEmpty(m.ORGNOS))
                            {
                                return Content(@"<script>alert('表格中组织机构名称错误，请确认后再上传！');history.go(-1);</script>");
                            }
                            m.opMethod = "Add";
                            m.NAME = arr[2];
                            m.NUMBER = arr[3];
                            m.AREA = arr[4];
                            m.FLOOR = arr[5];
                            m.BUILDDATE = arr[6];
                            if (m.BUILDDATE == "9999-12-31")
                                m.BUILDDATE = "1900-01-01";
                            m.SUBFACILITIES = arr[7];
                            m.WORTH = arr[8];
                            string jd = arr[9];
                            string wd = arr[10];
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                double[] brr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");
                                m.JD = brr[1].ToString();
                                m.WD = brr[0].ToString();
                            }
                            if (arr[1].Trim() == "钢构")//结构类型
                            {
                                m.STRUCTURETYPE = "1";
                            }
                            else if (arr[1].Trim() == "砖混")
                            {
                                m.STRUCTURETYPE = "2";
                            }
                            else if (arr[1].Trim() == "钢混")
                            {
                                m.STRUCTURETYPE = "3";
                            }
                            else
                            {
                                m.STRUCTURETYPE = "1";
                            }
                            var ms = DC_UTILITY_OVERWATCHCls.Manager(m);
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                TD_OVERWATCH_Model m1 = new TD_OVERWATCH_Model();
                                m1.OBJECTID = ms.Url;
                                m1.NAME = m.NAME;
                                m1.opMethod = "Add";
                                m1.TYPE = m.STRUCTURETYPE;
                                m1.JD = jd;
                                m1.WD = wd;
                                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                                TD_OVERWATCHCls.Manager(m1);
                            }
                            string a = row.GetCell(0).ToString();
                            string a1 = row.GetCell(1).ToString();
                            string a2 = row.GetCell(2).ToString();

                        }

                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传！');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的瞭望台表格');history.go(-1);</script>");
                }
            }
            return Content("<script>alert('导入成功');window.location.href='OVERWATCHIndex';</script>");
        }
        #endregion

        #endregion

        #region 设施-隔离带
        /// <summary>
        /// 隔离带页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ISOLATIONSTRIPIndex()
        {
            pubViewBag("016003", "016003", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.isolationtype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "35", isShowAll = "1" });
            ViewBag.isolationtypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "35" });
            ViewBag.usestate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36", isShowAll = "1" });
            ViewBag.usestateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" });
            ViewBag.managerstate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37", isShowAll = "1" });
            ViewBag.managerstateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37" });
            ViewBag.treetypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "52" });
            ViewBag.positionadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "53" });
            ViewBag.isAdd = (SystemCls.isRight("016003002")) ? "1" : "0";//除了规划生物隔离带的添加权限
            ViewBag.isAddplan = (SystemCls.isRight("016003005")) ? "1" : "0";//规划生物隔离带的添加权限
            return View();
        }
        /// <summary>
        /// 根据序号获取内容
        /// </summary>
        /// <returns></returns>
        public ActionResult GetISOLATIONSTRIPjson()
        {
            string DC_UTILITY_ISOLATIONSTRIP_ID = Request.Params["DC_UTILITY_ISOLATIONSTRIP_ID"];
            return Content(JsonConvert.SerializeObject(DC_UTILITY_ISOLATIONSTRIPCls.getModel(new DC_UTILITY_ISOLATIONSTRIP_SW { DC_UTILITY_ISOLATIONSTRIP_ID = DC_UTILITY_ISOLATIONSTRIP_ID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 隔离带管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ISOLATIONSTRIPManager()
        {
            DC_UTILITY_ISOLATIONSTRIP_Model m = new DC_UTILITY_ISOLATIONSTRIP_Model();
            m.DC_UTILITY_ISOLATIONSTRIP_ID = Request.Params["DC_UTILITY_ISOLATIONSTRIP_ID"];
            m.ISOLATIONTYPE = Request.Params["ISOLATIONTYPE"];
            m.NUMBER = Request.Params["NUMBER"];
            m.NAME = Request.Params["NAME"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.BUILDDATE = Request.Params["BUILDDATE"];
            m.USESTATE = Request.Params["USESTATE"];
            m.MANAGERSTATE = Request.Params["MANAGERSTATE"];
            m.WIDTH = Request.Params["WIDTH"];
            m.LENGTH = Request.Params["LENGTH"];
            m.JWDLIST = Request.Params["JWDLIST"];
            m.PLANAREA = Request.Params["PLANAREA"];
            m.REALAREA = Request.Params["REALAREA"];
            m.WORTH = Request.Params["WORTH"];
            m.KINDTYPE = Request.Params["KINDTYPE"];
            m.TREETYPE = Request.Params["TREETYPE"];
            m.opMethod = Request.Params["Method"];
            m.AlleywayWideth = Request.Params["AlleywayWideth"];
            m.Position = Request.Params["Position"];
            m.Price = Request.Params["Price"];
            m.ENTRYTIME = DateTime.Now.ToString();
            m.BUILDDATEBEGIN = Request.Params["BUILDDATEBEGIN"];
            m.BUILDDATEEND = Request.Params["BUILDDATEEND"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.NAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "名称不可为空，请重新输入！", "")), "text/html;charset=UTF-8");
            }
            var ms = DC_UTILITY_ISOLATIONSTRIPCls.Manager(m);
            if (ms.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, ms.Msg, "")), "text/html;charset=UTF-8");
            if (ConfigCls.getSDEDBTeam() == "1")
            {
                string line = "";
                string line1 = "";
                string polygon = "";
                string polygon1 = "";
                string jd = "";
                string wd = "";
                string dx = "";
                string dy = "";
                if (m.opMethod != "Del")
                {
                    if (m.ISOLATIONTYPE != "4")
                    {
                        TD_ISOLATIONSTRIP_Model m1 = new TD_ISOLATIONSTRIP_Model();
                        m1.opMethod = m.opMethod;
                        m1.NAME = m.NAME;
                        m1.TYPE = m.ISOLATIONTYPE;
                        if (string.IsNullOrEmpty(m.JWDLIST) == false)
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
                                        line += jd + " " + wd + "|";
                                    }
                                    else
                                    {
                                        line += jd + " " + wd + ",";
                                    }
                                }
                                string[] arr2 = line.Split('|');
                                if (j == arr1.Length - 1)//最后一条记录
                                {
                                    line1 += "";
                                }
                                else
                                {
                                    line1 += "(" + arr2[j].ToString() + "),";
                                }
                                string[] krr = dx.Split(',');//经度集合
                                string[] err = dy.Split(',');//维度集合
                                var jdbegin = krr[0];
                                var wdbegin = err[0];
                                var jdend = krr[krr.Length - 2];
                                var wdend = err[err.Length - 2];
                                if (jdbegin.Trim() == jdend.Trim() && wdbegin.Trim() == wdend.Trim())
                                    line = line.Substring(0, line.LastIndexOf(","));
                                if (arr.Length % 2 == 0)
                                {
                                    string[] crr = arr[arr.Length / 2].Split(',');
                                    double[] drr = ClsPositionTrans.GpsTransform(double.Parse(crr[1]), double.Parse(crr[0]), ConfigCls.getSDELonLatTransform());//中心点偏移
                                    m1.CENTRE_X = drr[1].ToString();
                                    m1.CENTRE_Y = drr[0].ToString();
                                }
                                else
                                {
                                    string[] crr = arr[(arr.Length + 1) / 2].Split(',');//取出中心点
                                    double[] drr = ClsPositionTrans.GpsTransform(double.Parse(crr[1]), double.Parse(crr[0]), ConfigCls.getSDELonLatTransform());//中心点偏移
                                    m1.CENTRE_X = drr[1].ToString();
                                    m1.CENTRE_Y = drr[0].ToString();
                                }

                            }
                        }
                        if (line1 != "")
                        {
                            line1 = line1.Substring(0, line1.LastIndexOf(","));
                        }
                        m1.Shape = "geometry::STGeomFromText('MULTILINESTRING (" + line1 + ")',4326).MakeValid()";
                        m1.OBJECTID = ms.Url;
                        TD_HUOSHAOMIAN_Model m3 = new TD_HUOSHAOMIAN_Model();
                        m3.OBJECTID = ms.Url;
                        m3.opMethod = "Del";
                        TD_ISOLATIONSTRIPCls.Manager(m1);
                        TD_HUOSHAOMIANCLS.Manager(m3);
                    }
                    else
                    {
                        TD_HUOSHAOMIAN_Model m2 = new TD_HUOSHAOMIAN_Model();
                        m2.opMethod = m.opMethod;
                        m2.NAME = m.NAME;
                        m2.TYPE = m.ISOLATIONTYPE;
                        if (string.IsNullOrEmpty(m.JWDLIST) == false)
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
                                        polygon += jd + " " + wd + "|";
                                    else
                                        polygon += jd + " " + wd + ",";
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
                                string[] crr = dx.Split(',');
                                string[] jrr = dy.Split(',');
                                var jdbegin = crr[0];
                                var wdbegin = jrr[0];
                                var jdend = crr[crr.Length - 2];
                                var wdend = jrr[jrr.Length - 2];
                                //if (jdbegin.Trim() != jdend.Trim() || wdbegin.Trim() != wdend.Trim())
                                //    polygon = polygon + "," + jdbegin + " " + wdbegin;
                                var jdmax = crr.Where(p => p != "").Select(p => float.Parse(p)).Max();
                                var jdmin = crr.Where(p => p != "").Select(p => float.Parse(p)).Min();
                                var wdmax = jrr.Where(p => p != "").Select(p => float.Parse(p)).Max();
                                var wdmin = jrr.Where(p => p != "").Select(p => float.Parse(p)).Min();
                                m2.JD = ((jdmax + jdmin) / 2).ToString();
                                m2.WD = ((wdmax + wdmin) / 2).ToString();
                            }
                        }
                        if (polygon1 != "")
                        {
                            polygon1 = polygon1.Substring(0, polygon1.LastIndexOf(","));
                        }
                        m2.Shape = "geometry::STGeomFromText('MULTIPolygon((" + polygon1 + "))',4326).MakeValid()";
                        m2.OBJECTID = ms.Url;
                        TD_ISOLATIONSTRIP_Model m4 = new TD_ISOLATIONSTRIP_Model();
                        m4.OBJECTID = ms.Url;
                        m4.opMethod = "Del";
                        TD_HUOSHAOMIANCLS.Manager(m2);
                        TD_ISOLATIONSTRIPCls.Manager(m4);
                    }
                }
                else
                {
                    TD_ISOLATIONSTRIP_Model m3 = new TD_ISOLATIONSTRIP_Model();
                    m3.OBJECTID = m.DC_UTILITY_ISOLATIONSTRIP_ID;
                    m3.opMethod = "Del";
                    TD_ISOLATIONSTRIPCls.Manager(m3);
                    TD_HUOSHAOMIAN_Model m4 = new TD_HUOSHAOMIAN_Model();
                    m4.OBJECTID = m.DC_UTILITY_ISOLATIONSTRIP_ID;
                    m4.opMethod = "Del";
                    TD_HUOSHAOMIANCLS.Manager(m4);
                }
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getISOLATIONSTRIPlist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string orgno = Request.Params["BYORGNO"];
            string isolationtype = Request.Params["ISOLATIONTYPE"];
            string usestate = Request.Params["USESTATE"];
            string managerstate = Request.Params["MANAGERSTATE"];
            string name = Request.Params["NAME"];
            string number = Request.Params["NUMBER"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>所属县市</th>");
            sb.AppendFormat("<th>所属乡镇</th>");
            sb.AppendFormat("<th>隔离带类型</th>");
            sb.AppendFormat("<th>名称</th>");
            //sb.AppendFormat("<th>编号</th>");
            sb.AppendFormat("<th>使用现状</th>");
            sb.AppendFormat("<th>维护类型</th>");
            sb.AppendFormat("<th>长度(米)</th>");
            sb.AppendFormat("<th>宽度(米)</th>");
            sb.AppendFormat("<th>实际面积(公顷)</th>");
            sb.AppendFormat("<th>树种</th>");
            sb.AppendFormat("<th>录入时间</th>");
            //sb.AppendFormat("<th>总价</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = DC_UTILITY_ISOLATIONSTRIPCls.getModelList(new DC_UTILITY_ISOLATIONSTRIP_SW { ISOLATIONTYPE = isolationtype, BYORGNO = orgno, NUMBER = number, NAME = name, USESTATE = usestate, MANAGERSTATE = managerstate, curPage = int.Parse(page), pageSize = int.Parse(PageSize) }, out total);
            foreach (var s in result)
            {
                sb.AppendFormat("<tr class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGXSName);
                if (string.IsNullOrEmpty(s.ORGName))
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "--");
                else
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.ISOLATIONTYPEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NAME);
                //sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NUMBER);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.USESTATEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.MANAGERSTATEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.LENGTH);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.WIDTH);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.REALAREA);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.KINDTYPE);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.ENTRYTIME);
                //sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.WORTH);
                sb.AppendFormat("    <td class=\" \">");
                if (string.IsNullOrEmpty(s.JWDLIST))
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                else
                {
                    if (s.ISOLATIONTYPE != "4")
                        sb.AppendFormat("<a href=\"#\" onclick=\"PositionLine('DC_UTILITY_ISOLATIONSTRIP','{0}','{1}',1)\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.DC_UTILITY_ISOLATIONSTRIP_ID, s.NAME);
                    else
                        sb.AppendFormat("<a href=\"#\" onclick=\"PositionLine('DC_UTILITY_ISOLATIONSTRIP','{0}','{1}',2)\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.DC_UTILITY_ISOLATIONSTRIP_ID, s.NAME);
                }
                sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.DC_UTILITY_ISOLATIONSTRIP_ID, "DC_UTILITY_ISOLATIONSTRIP");
                if (s.ISOLATIONTYPE != "5")
                {
                    if (SystemCls.isRight("016003003") == true)
                        sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", s.DC_UTILITY_ISOLATIONSTRIP_ID, page);

                    if (SystemCls.isRight("016003004") == true)
                        sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.DC_UTILITY_ISOLATIONSTRIP_ID, page);
                }
                else
                {
                    if (SystemCls.isRight("016003006") == true)
                        sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", s.DC_UTILITY_ISOLATIONSTRIP_ID, page);
                    if (SystemCls.isRight("016003007") == true)
                        sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.DC_UTILITY_ISOLATIONSTRIP_ID, page);
                }
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }

        #region 隔离带上传

        [HttpPost]
        public ActionResult ISOLATIONSTRIPList(FormCollection form)
        {
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                //扩展名
                string extension = System.IO.Path.GetExtension(File.FileName);//
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                    string name = DateTime.Now.ToString("隔离带导入-yyyyMMddHHmmss") + extension;
                    if (!FileType.Contains(extension))
                    {

                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content(@"<script>alert('上传文件超过4M，不能上传');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/DataCenterExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        //DC_UTILITY_ISOLATIONSTRIPCls.ISOLATIONSTRIPUpload(savePath);
                        HSSFWorkbook hssfworkbook;

                        FileStream file = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                            
                                hssfworkbook = new HSSFWorkbook(file);
                            
                       

                        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
                        int rowCount = sheet.LastRowNum;
                        for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                        {

                            IRow row = sheet.GetRow(i);
                            string[] arr = new string[12];
                            for (int k = 0; k < arr.Length; k++)
                            {
                                arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                            }
                            DC_UTILITY_ISOLATIONSTRIP_Model m = new DC_UTILITY_ISOLATIONSTRIP_Model();
                            //单位	隔离带类型	名称	编号	使用现状 维护类型	宽度 长度 计划面积 实际面积 价值 树种
                            if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                            {
                                continue;
                            }
                            m.BYORGNO = T_SYS_ORGCls.getCodeByName(arr[0]);
                            if (string.IsNullOrEmpty(m.BYORGNO))
                            {
                                return Content(@"<script>alert('表格中组织机构名称错误，请确认后再上传！');history.go(-1);</script>");
                            }
                            m.opMethod = "Add";
                            m.NAME = arr[2];
                            m.NUMBER = arr[3];
                            m.WIDTH = arr[6];
                            m.LENGTH = arr[7];
                            m.PLANAREA = arr[8];
                            m.REALAREA = arr[9];
                            m.WORTH = arr[11];
                            m.KINDTYPE = arr[10];
                            if (arr[1].Trim() == "生物")//隔离带类型
                            {
                                m.ISOLATIONTYPE = "1";
                            }
                            else if (arr[1].Trim() == "生土")
                            {
                                m.ISOLATIONTYPE = "2";
                            }
                            else if (arr[1].Trim() == "火烧线")
                            {
                                m.ISOLATIONTYPE = "3";
                            }
                            else if (arr[1].Trim() == "计划烧除")
                            {
                                m.ISOLATIONTYPE = "4";
                            }
                             else if (arr[1].Trim() == "规划生物隔离带")
                            {
                                m.ISOLATIONTYPE = "5";
                            }
                            else
                            {
                                m.ISOLATIONTYPE = "1";
                            }
                            if (arr[4].Trim() == "在用")//使用类型
                            {
                                m.USESTATE = "1";
                            }
                            else if (arr[4].Trim() == "储存")
                            {
                                m.USESTATE = "2";
                            }
                            else if (arr[4].Trim() == "报废")
                            {
                                m.USESTATE = "3";
                            }
                            else
                            {
                                m.USESTATE = "1";
                            }
                            if (arr[5].Trim() == "维护")//使用类型
                            {
                                m.MANAGERSTATE = "2";
                            }
                            else if (arr[5].Trim() == "未维护")
                            {
                                m.MANAGERSTATE = "1";
                            }
                            else
                            {
                                m.MANAGERSTATE = "1";
                            }
                            DC_UTILITY_ISOLATIONSTRIPCls.Manager(m);
                            string a = row.GetCell(0).ToString();
                            string a1 = row.GetCell(1).ToString();
                            string a2 = row.GetCell(2).ToString();

                        }
                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传！');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的隔离带表格');history.go(-1);</script>");
                }
            }
            return Content("<script>alert('导入成功');window.location.href='ISOLATIONSTRIPIndex';</script>");
        }
        #endregion

        #endregion

        #region 设施-防火通道
        /// <summary>
        /// 防火通道页面
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRECHANNELIndex()
        {
            pubViewBag("016007", "016007", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.usestate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36", isShowAll = "1" });
            ViewBag.usestateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" });
            ViewBag.managerstate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37", isShowAll = "1" });
            ViewBag.managerstateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37" });
            ViewBag.fireleveltype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "38", isShowAll = "1" });
            ViewBag.fireleveltypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "38" });
            ViewBag.fireusetype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "39", isShowAll = "1" });
            ViewBag.fireusetypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "39" });
            ViewBag.isAdd = (SystemCls.isRight("016007002")) ? "1" : "0";
            return View();
        }
        ///<summary>
        /// 根据序号获取内容
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFIRECHANNELjson()
        {
            string DC_UTILITY_FIRECHANNEL_ID = Request.Params["DC_UTILITY_FIRECHANNEL_ID"];
            //if (string.IsNullOrEmpty(ID))
            //    ID = "0";
            return Content(JsonConvert.SerializeObject(DC_UTILITY_FIRECHANNELCls.getModel(new DC_UTILITY_FIRECHANNEL_SW { DC_UTILITY_FIRECHANNEL_ID = DC_UTILITY_FIRECHANNEL_ID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 防火通道管理
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRECHANNELManager()
        {
            DC_UTILITY_FIRECHANNEL_Model m = new DC_UTILITY_FIRECHANNEL_Model();
            m.DC_UTILITY_FIRECHANNEL_ID = Request.Params["DC_UTILITY_FIRECHANNEL_ID"];
            m.NUMBER = Request.Params["NUMBER"];
            m.NAME = Request.Params["NAME"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.BUILDDATE = Request.Params["BUILDDATE"];
            m.USESTATE = Request.Params["USESTATE"];
            m.MANAGERSTATE = Request.Params["MANAGERSTATE"];
            m.FIRECHANNELLEVELTYPE = Request.Params["FIRECHANNELLEVELTYPE"];
            m.FIRECHANNELUSERTYPE = Request.Params["FIRECHANNELUSERTYPE"];
            m.LENGTH = Request.Params["LENGTH"];
            m.JWDLIST = Request.Params["JWDLIST"];
            m.WORTH = Request.Params["WORTH"];
            m.opMethod = Request.Params["Method"];
            m.BUILDDATEBEGIN = Request.Params["BUILDDATEBEGIN"];
            m.BUILDDATEEND = Request.Params["BUILDDATEEND"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.NAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "名称不可为空，请重新输入！", "")), "text/html;charset=UTF-8");
            }
            var ms = DC_UTILITY_FIRECHANNELCls.Manager(m);
            if (ms.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, ms.Msg, "")), "text/html;charset=UTF-8");
            if (ConfigCls.getSDEDBTeam() == "1")//配置文件
            {
                TD_FIRECHANNEL_Model m1 = new TD_FIRECHANNEL_Model();
                m1.opMethod = m.opMethod;
                m1.NAME = m.NAME;
                m1.TYPE = m.FIRECHANNELLEVELTYPE;
                string line = "";
                string line1 = "";
                string jd = "";
                string wd = "";
                if (string.IsNullOrEmpty(m.JWDLIST) == false)
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

                                double[] drr = ClsPositionTrans.GpsTransform(double.Parse(brr[1]), double.Parse(brr[0]), ConfigCls.getSDELonLatTransform());//坐标系转换
                                wd = drr[0].ToString();
                                jd = drr[1].ToString();
                            }
                            if (i == arr.Length - 1)//最后一条记录
                            {
                                line += jd + " " + wd + "|"; ;
                            }
                            else
                            {
                                line += jd + " " + wd + ",";
                            }
                        }
                        string[] arr2 = line.Split('|');
                        if (j == arr1.Length - 1)//最后一条记录
                        {
                            line1 += "";
                        }
                        else
                        {
                            line1 += "(" + arr2[j].ToString() + "),";
                        }
                        if (arr.Length % 2 == 0)
                        {
                            string[] crr = arr[arr.Length / 2].Split(',');

                            double[] drr = ClsPositionTrans.GpsTransform(double.Parse(crr[1]), double.Parse(crr[0]), ConfigCls.getSDELonLatTransform());
                            m1.CENTRE_X = drr[1].ToString();
                            m1.CENTRE_Y = drr[0].ToString();
                        }
                        else
                        {
                            string[] crr = arr[(arr.Length + 1) / 2].Split(',');

                            double[] drr = ClsPositionTrans.GpsTransform(double.Parse(crr[1]), double.Parse(crr[0]), ConfigCls.getSDELonLatTransform());//中心点偏移
                            m1.CENTRE_X = drr[1].ToString();
                            m1.CENTRE_Y = drr[0].ToString();
                        }
                    }
                }
                if (line1 != "")
                {
                    line1 = line1.Substring(0, line1.LastIndexOf(","));
                }
                m1.Shape = "geometry::STGeomFromText('MULTILINESTRING (" + line1 + ")',4326).MakeValid()";
                if (m.opMethod != "Del")
                {
                    m1.OBJECTID = ms.Url;
                }
                else
                {
                    m1.OBJECTID = m.DC_UTILITY_FIRECHANNEL_ID;
                }

                if (string.IsNullOrEmpty(m.JWDLIST) && m1.opMethod == "Add")
                {

                }
                else
                {
                    TD_FIRECHANNELCls.Manager(m1);
                }

            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getFIRECHANNELlist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string orgno = Request.Params["BYORGNO"];
            string usestate = Request.Params["USESTATE"];
            string managerstate = Request.Params["MANAGERSTATE"];
            string name = Request.Params["NAME"];
            string number = Request.Params["NUMBER"];
            string firechannelleveltype = Request.Params["FIRECHANNELLEVELTYPE"];
            string firechannelusertype = Request.Params["FIRECHANNELUSERTYPE"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>所属县市</th>");
            sb.AppendFormat("<th>所属乡镇</th>");
            sb.AppendFormat("<th>名称</th>");
            sb.AppendFormat("<th>编号</th>");
            sb.AppendFormat("<th>使用现状</th>");
            sb.AppendFormat("<th>维护类型</th>");
            sb.AppendFormat("<th>通道等级类型</th>");
            sb.AppendFormat("<th>通道使用性质</th>");
            sb.AppendFormat("<th>总价(元)</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = DC_UTILITY_FIRECHANNELCls.getModelList(new DC_UTILITY_FIRECHANNEL_SW { FIRECHANNELLEVELTYPE = firechannelleveltype, BYORGNO = orgno, FIRECHANNELUSERTYPE = firechannelusertype, NUMBER = number, NAME = name, USESTATE = usestate, MANAGERSTATE = managerstate, curPage = int.Parse(page), pageSize = int.Parse(PageSize) }, out total);
            foreach (var s in result)
            {
                //if (i % 2 == 0)
                //    sb.AppendFormat("<tr>");
                //else
                sb.AppendFormat("<tr class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGXSName);
                if (string.IsNullOrEmpty(s.ORGName))
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "--");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGName);
                }
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NUMBER);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.USESTATEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.MANAGERSTATEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.FIRECHANNELLEVELTYPEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.FIRECHANNELUSERTYPEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.WORTH);
                sb.AppendFormat("    <td class=\" \">");
                if (string.IsNullOrEmpty(s.JWDLIST))
                {
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                }
                else
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\"PositionLine('DC_UTILITY_FIRECHANNEL','{0}','{1}',1)\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.DC_UTILITY_FIRECHANNEL_ID, s.NAME);
                }
                sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.DC_UTILITY_FIRECHANNEL_ID, "DC_UTILITY_FIRECHANNEL");
                if (SystemCls.isRight("016007003") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", s.DC_UTILITY_FIRECHANNEL_ID, page);
                if (SystemCls.isRight("016007004") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.DC_UTILITY_FIRECHANNEL_ID, page);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }

        #region 防火通道上传

        [HttpPost]
        public ActionResult FIRECHANNELList(FormCollection form)
        {
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                //扩展名
                string extension = System.IO.Path.GetExtension(File.FileName);//
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                    string name = DateTime.Now.ToString("防火通道导入-yyyyMMddHHmmss") + extension;
                    if (!FileType.Contains(extension))
                    {

                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content(@"<script>alert('上传文件超过4M，不能上传');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/DataCenterExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        //DC_UTILITY_FIRECHANNELCls.FIRECHANNELUpload(savePath);
                        HSSFWorkbook hssfworkbook;
                            FileStream file = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                                hssfworkbook = new HSSFWorkbook(file);
                        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
                        int rowCount = sheet.LastRowNum;
                        for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                        {

                            IRow row = sheet.GetRow(i);
                            string[] arr = new string[10];
                            for (int k = 0; k < arr.Length; k++)
                            {
                                if (k != 8)
                                    arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                                else
                                    arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                            }

                            DC_UTILITY_FIRECHANNEL_Model m = new DC_UTILITY_FIRECHANNEL_Model();
                            //单位	名称  长度	编号 使用现状	维护管理类型 防火通道等级 防火通道使用性质 建设日期 价值 
                            if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                            {
                                continue;
                            }
                            m.BYORGNO = T_SYS_ORGCls.getCodeByName(arr[0]);
                            if (string.IsNullOrEmpty(m.BYORGNO))
                            {
                                return Content(@"<script>alert('表格中组织机构名称错误，请确认后再上传！');history.go(-1);</script>");
                               
                            }
                            m.opMethod = "Add";
                            m.NAME = arr[1];
                            m.LENGTH = arr[2];
                            m.NUMBER = arr[3];
                            m.BUILDDATE = arr[8];
                            if (m.BUILDDATE == "9999-12-31")
                                m.BUILDDATE = "1900-01-01";
                            m.WORTH = arr[9];
                            if (arr[4].Trim() == "在用")//使用现状
                            {
                                m.USESTATE = "1";
                            }
                            else if (arr[4].Trim() == "储存")
                            {
                                m.USESTATE = "2";
                            }
                            else if (arr[4].Trim() == "报废")
                            {
                                m.USESTATE = "3";
                            }
                            else
                            {
                                m.USESTATE = "1";
                            }
                            if (arr[5].Trim() == "未维护")//维护管理类型
                            {
                                m.MANAGERSTATE = "1";
                            }
                            else if (arr[5].Trim() == "维护")
                            {
                                m.MANAGERSTATE = "2";
                            }
                            else
                            {
                                m.MANAGERSTATE = "1";
                            }
                            if (arr[6].Trim() == "便道")//防火通道等级类型
                            {
                                m.FIRECHANNELLEVELTYPE = "1";
                            }
                            else if (arr[6].Trim() == "林区道路")
                            {
                                m.FIRECHANNELLEVELTYPE = "2";
                            }
                            else
                            {
                                m.FIRECHANNELLEVELTYPE = "1";
                            }
                            if (arr[7].Trim() == "人行道")//防火通道性质类型
                            {
                                m.FIRECHANNELUSERTYPE = "1";
                            }
                            else if (arr[7].Trim() == "车行道")
                            {
                                m.FIRECHANNELUSERTYPE = "2";
                            }
                            else
                            {
                                m.FIRECHANNELUSERTYPE = "1";
                            }
                            DC_UTILITY_FIRECHANNELCls.Manager(m);
                            string a = row.GetCell(0).ToString();
                            string a1 = row.GetCell(1).ToString();
                            string a2 = row.GetCell(2).ToString();

                        }

                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传！');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的防火通道表格');history.go(-1);</script>");
                }
            }
            return Content("<script>alert('导入成功');window.location.href='FIRECHANNELIndex';</script>");
        }
        #endregion
        #endregion

        #region 设施-宣传碑牌
        /// <summary>
        /// 宣传碑牌页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PROPAGANDASTELEIndex()
        {
            pubViewBag("016004", "016004", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.structure = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "34", isShowAll = "1" });
            ViewBag.structureadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "34" });
            ViewBag.usestate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36", isShowAll = "1" });
            ViewBag.usestateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" });
            ViewBag.managerstate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37", isShowAll = "1" });
            ViewBag.managerstateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37" });
            ViewBag.propagandasteletype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "40", isShowAll = "1" });
            ViewBag.propagandasteletypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "40" });
            ViewBag.isAdd = (SystemCls.isRight("016004002")) ? "1" : "0";
            return View();
        }
        /// <summary>
        /// 根据序号获取内容
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPROPAGANDASTELEjson()
        {
            string DC_UTILITY_PROPAGANDASTELE_ID = Request.Params["DC_UTILITY_PROPAGANDASTELE_ID"];
            //if (string.IsNullOrEmpty(ID))
            //    ID = "0";
            return Content(JsonConvert.SerializeObject(DC_UTILITY_PROPAGANDASTELECls.getModel(new DC_UTILITY_PROPAGANDASTELE_SW { DC_UTILITY_PROPAGANDASTELE_ID = DC_UTILITY_PROPAGANDASTELE_ID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 宣传碑牌管理
        /// </summary>
        /// <returns></returns>
        public ActionResult PROPAGANDASTELEManager()
        {
            DC_UTILITY_PROPAGANDASTELE_Model m = new DC_UTILITY_PROPAGANDASTELE_Model();
            m.DC_UTILITY_PROPAGANDASTELE_ID = Request.Params["DC_UTILITY_PROPAGANDASTELE_ID"];
            m.PROPAGANDASTELETYPE = Request.Params["PROPAGANDASTELETYPE"];
            m.NUMBER = Request.Params["NUMBER"];
            m.NAME = Request.Params["NAME"];
            m.ADDRESS = Request.Params["ADDRESS"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.STRUCTURETYPE = Request.Params["STRUCTURETYPE"];
            m.BUILDDATE = Request.Params["BUILDDATE"];
            m.USESTATE = Request.Params["USESTATE"];
            m.MANAGERSTATE = Request.Params["MANAGERSTATE"];
            m.MARK = Request.Params["MARK"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.WORTH = Request.Params["WORTH"];
            m.opMethod = Request.Params["Method"];
            m.BUILDDATEBEGIN = Request.Params["BUILDDATEBEGIN"];
            m.BUILDDATEEND = Request.Params["BUILDDATEEND"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.NAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "名称不可为空，请重新输入！", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
            }
            var ms = DC_UTILITY_PROPAGANDASTELECls.Manager(m);
            if (ms.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, ms.Msg, "")), "text/html;charset=UTF-8");
            if (ConfigCls.getSDEDBTeam() == "1")//配置文件
            {
                TD_PROPAGANDASTELE_Model m1 = new TD_PROPAGANDASTELE_Model();
                if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
                {
                    double[] arr = ClsPositionTrans.GpsTransform(double.Parse(m.WD), double.Parse(m.JD), ConfigCls.getSDELonLatTransform());
                    m1.JD = arr[1].ToString();
                    m1.WD = arr[0].ToString();
                }
                m1.opMethod = m.opMethod;
                m1.NAME = m.NAME;
                m1.TYPE = m.PROPAGANDASTELETYPE;
                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                if (m.opMethod != "Del")
                {
                    m1.OBJECTID = ms.Url;
                }
                else
                {
                    m1.OBJECTID = m.DC_UTILITY_PROPAGANDASTELE_ID;
                }
                if ((string.IsNullOrEmpty(m.JD) || string.IsNullOrEmpty(m.WD)) && m1.opMethod == "Add")
                {

                }
                else
                {
                    TD_PROPAGANDASTELECls.Manager(m1);
                }
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getPROPAGANDASTELElist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string orgno = Request.Params["BYORGNO"];
            string usestate = Request.Params["USESTATE"];
            string managerstate = Request.Params["MANAGERSTATE"];
            string structuretype = Request.Params["STRUCTURETYPE"];
            string propagandasteletype = Request.Params["PROPAGANDASTELETYPE"];
            string name = Request.Params["NAME"];
            string number = Request.Params["NUMBER"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>所属县市</th>");
            sb.AppendFormat("<th>所属乡镇</th>");
            sb.AppendFormat("<th>名称</th>");
            sb.AppendFormat("<th>编号</th>");
            sb.AppendFormat("<th>结构类型</th>");
            sb.AppendFormat("<th>宣传碑类型</th>");
            sb.AppendFormat("<th>使用现状</th>");
            sb.AppendFormat("<th>维护管理类型</th>");
            sb.AppendFormat("<th>建成日期</th>");
            sb.AppendFormat("<th>总价(元)</th>");
            sb.AppendFormat("<th>地址</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = DC_UTILITY_PROPAGANDASTELECls.getModelList(new DC_UTILITY_PROPAGANDASTELE_SW
            {
                STRUCTURETYPE = structuretype,
                BYORGNO = orgno,
                USESTATE = usestate,
                MANAGERSTATE = managerstate,
                PROPAGANDASTELETYPE = propagandasteletype,
                NUMBER = number,
                NAME = name,
                curPage = int.Parse(page),
                pageSize = int.Parse(PageSize)
            }, out total);
            foreach (var s in result)
            {
                //if (i % 2 == 0)
                //    sb.AppendFormat("<tr>");
                //else
                sb.AppendFormat("<tr class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGXSName);
                if (string.IsNullOrEmpty(s.ORGName))
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "--");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGName);
                }
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NUMBER);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.STRUCTURETYPEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.PROPAGANDASTELETYPEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.USESTATEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.MANAGERSTATEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.BUILDDATE);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.WORTH);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.ADDRESS);
                sb.AppendFormat("    <td class=\" \">");
                if (string.IsNullOrEmpty(s.JD))
                {
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                }
                else
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\" Position('DC_UTILITY_PROPAGANDASTELE','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.DC_UTILITY_PROPAGANDASTELE_ID, s.NAME);
                }
                sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.DC_UTILITY_PROPAGANDASTELE_ID, "DC_UTILITY_PROPAGANDASTELE");
                if (SystemCls.isRight("016004003") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", s.DC_UTILITY_PROPAGANDASTELE_ID, page);
                if (SystemCls.isRight("016004004") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.DC_UTILITY_PROPAGANDASTELE_ID, page);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }
        #region 宣传碑牌上传

        [HttpPost]
        public ActionResult PROPAGANDASTELEList(FormCollection form)
        {
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                //扩展名
                string extension = System.IO.Path.GetExtension(File.FileName);//
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                    string name = DateTime.Now.ToString("宣传碑牌导入-yyyyMMddHHmmss") + extension;
                    if (!FileType.Contains(extension))
                    {

                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content(@"<script>alert('上传文件超过4M，不能上传');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/DataCenterExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        //DC_UTILITY_PROPAGANDASTELECls.PROPAGANDASTELEUpload(savePath);
                        HSSFWorkbook hssfworkbook;

                        FileStream file = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                            
                                hssfworkbook = new HSSFWorkbook(file);
                           

                        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
                        int rowCount = sheet.LastRowNum;
                        for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                        {

                            IRow row = sheet.GetRow(i);
                            string[] arr = new string[12];
                            for (int k = 0; k < arr.Length; k++)
                            {
                                if (k != 7)
                                    arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                                else
                                    arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                            }
                            DC_UTILITY_PROPAGANDASTELE_Model m = new DC_UTILITY_PROPAGANDASTELE_Model();
                            //单位	宣传碑类型	名称	编号 使用现状 维护管理类型 结构类型 建设日期 价值 地址 经度 纬度
                            if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                            {
                                continue;
                            }
                            m.BYORGNO = T_SYS_ORGCls.getCodeByName(arr[0]);
                            if (string.IsNullOrEmpty(m.BYORGNO))
                            {
                                return Content(@"<script>alert('表格中组织机构名称错误，请确认后再上传！');history.go(-1);</script>");
                            }
                            m.opMethod = "Add";
                            m.NAME = arr[2];
                            m.NUMBER = arr[3];
                            m.BUILDDATE = arr[7];
                            if (m.BUILDDATE == "9999-12-31")
                                m.BUILDDATE = "1900-01-01";
                            m.WORTH = arr[8];
                            m.ADDRESS = arr[9];
                            string jd = arr[10];
                            string wd = arr[11];
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                double[] brr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");
                                m.JD = brr[1].ToString();
                                m.WD = brr[0].ToString();
                            }
                            if (arr[1].Trim() == "永久性")//宣传碑类型
                            {
                                m.PROPAGANDASTELETYPE = "1";
                            }
                            else if (arr[1].Trim() == "临时性")
                            {
                                m.PROPAGANDASTELETYPE = "2";
                            }
                            else
                            {
                                m.PROPAGANDASTELETYPE = "1";
                            }
                            if (arr[4].Trim() == "在用")//使用现状
                            {
                                m.USESTATE = "1";
                            }
                            else if (arr[4].Trim() == "储存")
                            {
                                m.USESTATE = "2";
                            }
                            else if (arr[4].Trim() == "报废")
                            {
                                m.USESTATE = "3";
                            }
                            else
                            {
                                m.USESTATE = "1";
                            }
                            if (arr[5].Trim() == "未维护")//维护管理类型
                            {
                                m.MANAGERSTATE = "1";
                            }
                            else if (arr[5].Trim() == "维护")
                            {
                                m.MANAGERSTATE = "2";
                            }
                            else
                            {
                                m.MANAGERSTATE = "1";
                            }
                            if (arr[6].Trim() == "钢构")//结构类型
                            {
                                m.STRUCTURETYPE = "1";
                            }
                            else if (arr[6].Trim() == "砖混")
                            {
                                m.STRUCTURETYPE = "2";
                            }
                            else if (arr[6].Trim() == "钢混")
                            {
                                m.STRUCTURETYPE = "3";
                            }
                            else
                            {
                                m.STRUCTURETYPE = "1";
                            }
                            var ms = DC_UTILITY_PROPAGANDASTELECls.Manager(m);
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                TD_PROPAGANDASTELE_Model m1 = new TD_PROPAGANDASTELE_Model();
                                m1.OBJECTID = ms.Url;
                                m1.opMethod = "Add";
                                m1.NAME = m.NAME;
                                m1.TYPE = m.PROPAGANDASTELETYPE;
                                m1.JD = jd;
                                m1.WD = wd;
                                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                                TD_PROPAGANDASTELECls.Manager(m1);
                            }
                            string a = row.GetCell(0).ToString();
                            string a1 = row.GetCell(1).ToString();
                            string a2 = row.GetCell(2).ToString();

                        }
                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传！');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的宣传碑牌表格');history.go(-1);</script>");
                }
            }
            return Content("<script>alert('导入成功');window.location.href='PROPAGANDASTELEIndex';</script>");
        }
        #endregion
        #endregion

        #region 设施-中继站
        /// <summary>
        /// 中继站页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RELAYIndex()
        {
            pubViewBag("016005", "016005", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.usestate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36", isShowAll = "1" });
            ViewBag.usestateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" });
            ViewBag.managerstate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37", isShowAll = "1" });
            ViewBag.managerstateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37" });
            ViewBag.communicationway = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "41", isShowAll = "1" });
            ViewBag.communicationwayadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "41" });
            ViewBag.isAdd = (SystemCls.isRight("016005002")) ? "1" : "0";
            return View();
        }
        /// <summary>
        /// 根据序号获取内容
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRELAYjson()
        {
            string DC_UTILITY_RELAY_ID = Request.Params["DC_UTILITY_RELAY_ID"];
            //if (string.IsNullOrEmpty(ID))
            //    ID = "0";
            return Content(JsonConvert.SerializeObject(DC_UTILITY_RELAYCls.getModel(new DC_UTILITY_RELAY_SW { DC_UTILITY_RELAY_ID = DC_UTILITY_RELAY_ID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 中继站管理
        /// </summary>
        /// <returns></returns>
        public ActionResult RELAYManager()
        {
            DC_UTILITY_RELAY_Model m = new DC_UTILITY_RELAY_Model();
            m.DC_UTILITY_RELAY_ID = Request.Params["DC_UTILITY_RELAY_ID"];
            m.NUMBER = Request.Params["NUMBER"];
            m.NAME = Request.Params["NAME"];
            m.ADDRESS = Request.Params["ADDRESS"];
            m.MODEL = Request.Params["MODEL"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.COMMUNICATIONWAY = Request.Params["COMMUNICATIONWAY"];
            m.BUILDDATE = Request.Params["BUILDDATE"];
            m.USESTATE = Request.Params["USESTATE"];
            m.MANAGERSTATE = Request.Params["MANAGERSTATE"];
            m.MARK = Request.Params["MARK"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.WORTH = Request.Params["WORTH"];
            m.opMethod = Request.Params["Method"];
            m.BUILDDATEBEGIN = Request.Params["BUILDDATEBEGIN"];
            m.BUILDDATEEND = Request.Params["BUILDDATEEND"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.NAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "名称不可为空，请重新输入！", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
            }
            var ms = DC_UTILITY_RELAYCls.Manager(m);
            if (ms.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, ms.Msg, "")), "text/html;charset=UTF-8");
            if (ConfigCls.getSDEDBTeam() == "1")//配置文件
            {
                TD_RELAY_Model m1 = new TD_RELAY_Model();
                if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
                {

                    double[] arr = ClsPositionTrans.GpsTransform(double.Parse(m.WD), double.Parse(m.JD), ConfigCls.getSDELonLatTransform());
                    m1.JD = arr[1].ToString();
                    m1.WD = arr[0].ToString();
                }
                m1.opMethod = m.opMethod;
                m1.NAME = m.NAME;
                m1.TYPE = m.COMMUNICATIONWAY;

                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";

                if (m.opMethod != "Del")
                {
                    m1.OBJECTID = ms.Url;
                }
                else
                {
                    m1.OBJECTID = m.DC_UTILITY_RELAY_ID;
                }

                if ((string.IsNullOrEmpty(m.JD) || string.IsNullOrEmpty(m.WD)) && m1.opMethod == "Add")
                {

                }
                else
                {
                    TD_RELAYCls.Manager(m1);
                }
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getRELAYlist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string orgno = Request.Params["BYORGNO"];
            string usestate = Request.Params["USESTATE"];
            string managerstate = Request.Params["MANAGERSTATE"];
            string communicationway = Request.Params["COMMUNICATIONWAY"];
            string name = Request.Params["NAME"];
            string number = Request.Params["NUMBER"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>所属县市</th>");
            sb.AppendFormat("<th>所属乡镇</th>");
            sb.AppendFormat("<th>名称</th>");
            sb.AppendFormat("<th>编号</th>");
            sb.AppendFormat("<th>型号</th>");
            sb.AppendFormat("<th>通讯方式</th>");
            sb.AppendFormat("<th>使用现状</th>");
            sb.AppendFormat("<th>维护类型</th>");
            sb.AppendFormat("<th>建成日期</th>");
            sb.AppendFormat("<th>总价(元)</th>");
            sb.AppendFormat("<th>地址</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = DC_UTILITY_RELAYCls.getModelList(new DC_UTILITY_RELAY_SW
            {
                COMMUNICATIONWAY = communicationway,
                BYORGNO = orgno,
                USESTATE = usestate,
                MANAGERSTATE = managerstate,
                NUMBER = number,
                NAME = name,
                curPage = int.Parse(page),
                pageSize = int.Parse(PageSize)
            }, out total);
            foreach (var s in result)
            {
                //if (i % 2 == 0)
                //    sb.AppendFormat("<tr>");
                //else
                sb.AppendFormat("<tr class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGXSName);
                if (string.IsNullOrEmpty(s.ORGName))
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "--");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGName);
                }
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NUMBER);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.MODEL);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.COMMUNICATIONWAYName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.USESTATEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.MANAGERSTATEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.BUILDDATE);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.WORTH);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.ADDRESS);
                sb.AppendFormat("    <td class=\" \">");
                if (string.IsNullOrEmpty(s.JD))
                {
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                }
                else
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\" Position('DC_UTILITY_RELAY','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.DC_UTILITY_RELAY_ID, s.NAME);
                }
                sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.DC_UTILITY_RELAY_ID, "DC_UTILITY_RELAY");
                if (SystemCls.isRight("016005003") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", s.DC_UTILITY_RELAY_ID, page);
                if (SystemCls.isRight("016005004") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.DC_UTILITY_RELAY_ID, page);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }
        #region 中继站上传

        [HttpPost]
        public ActionResult RELAYList(FormCollection form)
        {
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                //扩展名
                string extension = System.IO.Path.GetExtension(File.FileName);//
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                    string name = DateTime.Now.ToString("中继站导入-yyyyMMddHHmmss") + extension;
                    if (!FileType.Contains(extension))
                    {

                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content(@"<script>alert('上传文件超过4M，不能上传');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/DataCenterExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        //DC_UTILITY_RELAYCls.RELAYUpload(savePath);
                        HSSFWorkbook hssfworkbook;

                        FileStream file = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                            
                                hssfworkbook = new HSSFWorkbook(file);
                           
                       

                        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
                        int rowCount = sheet.LastRowNum;
                        for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                        {

                            IRow row = sheet.GetRow(i);
                            string[] arr = new string[12];
                            for (int k = 0; k < arr.Length; k++)
                            {
                                if (k != 7)
                                    arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                                else
                                    arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                            }
                            DC_UTILITY_RELAY_Model m = new DC_UTILITY_RELAY_Model();
                            //单位	通讯方式	名称	编号	型号	使用现状	维护管理类型 建设日期  价值 地址 经度 纬度
                            if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                            {
                                continue;
                            }
                            m.BYORGNO = T_SYS_ORGCls.getCodeByName(arr[0]);
                            if (string.IsNullOrEmpty(m.BYORGNO))
                            {
                                return Content(@"<script>alert('表格中组织机构名称错误，请确认后再上传！');history.go(-1);</script>");
                                
                            }
                            m.opMethod = "Add";
                            m.NAME = arr[2];
                            m.NUMBER = arr[3];
                            m.MODEL = arr[4];
                            m.BUILDDATE = arr[7];
                            if (m.BUILDDATE == "9999-12-31")
                                m.BUILDDATE = "1900-01-01";
                            m.WORTH = arr[8];
                            m.ADDRESS = arr[9];
                            string jd = arr[10];
                            string wd = arr[11];
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                double[] brr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");
                                m.JD = brr[1].ToString();
                                m.WD = brr[0].ToString();
                            }
                            if (arr[5].Trim() == "在用")//使用类型
                            {
                                m.USESTATE = "1";
                            }
                            else if (arr[5].Trim() == "储存")
                            {
                                m.USESTATE = "2";
                            }
                            else if (arr[5].Trim() == "报废")
                            {
                                m.USESTATE = "3";
                            }
                            else
                            {
                                m.USESTATE = "1";
                            }
                            if (arr[6].Trim() == "未维护")//维护管理类型
                            {
                                m.MANAGERSTATE = "1";
                            }
                            else if (arr[6].Trim() == "维护")
                            {
                                m.MANAGERSTATE = "2";
                            }
                            else
                            {
                                m.MANAGERSTATE = "1";
                            }
                            if (arr[1].Trim() == "短波")//通讯方式
                            {
                                m.COMMUNICATIONWAY = "1";
                            }
                            else if (arr[1].Trim() == "超短波")
                            {
                                m.COMMUNICATIONWAY = "2";
                            }
                            else if (arr[1].Trim() == "微波")
                            {
                                m.COMMUNICATIONWAY = "3";
                            }
                            else
                            {
                                m.COMMUNICATIONWAY = "1";
                            }
                            var ms = DC_UTILITY_RELAYCls.Manager(m);
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                TD_RELAY_Model m1 = new TD_RELAY_Model();
                                m1.opMethod = "Add";
                                m1.OBJECTID = ms.Url;
                                m1.NAME = m.NAME;
                                m1.TYPE = m.COMMUNICATIONWAY;
                                m1.JD = jd;
                                m1.WD = wd;
                                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                                TD_RELAYCls.Manager(m1);
                            }
                            string a = row.GetCell(0).ToString();
                            string a1 = row.GetCell(1).ToString();
                            string a2 = row.GetCell(2).ToString();

                        }
                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传！');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的中继站表格');history.go(-1);</script>");
                }
            }
            return Content("<script>alert('导入成功');window.location.href='RELAYIndex';</script>");
        }
        #endregion
        #endregion

        #region 设施-监测站
        /// <summary>
        /// 监测站页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MONITORINGSTATIONIndex()
        {
            pubViewBag("016006", "016006", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.usestate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36", isShowAll = "1" });
            ViewBag.usestateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" });
            ViewBag.managerstate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37", isShowAll = "1" });
            ViewBag.managerstateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37" });
            ViewBag.transfermodetype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "42", isShowAll = "1" });
            ViewBag.transfermodetypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "42" });
            //ViewBag.monicontent = getMONICONTENT(new T_SYS_DICTSW { DICTTYPEID = "43" });

            ViewBag.isAdd = (SystemCls.isRight("016006002")) ? "1" : "0";
            return View();
        }
        /// <summary>
        /// 根据序号获取内容
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMONITORINGSTATIONjson()
        {
            string DC_UTILITY_MONITORINGSTATION_ID = Request.Params["DC_UTILITY_MONITORINGSTATION_ID"];
            //if (string.IsNullOrEmpty(ID))
            //    ID = "0";
            return Content(JsonConvert.SerializeObject(DC_UTILITY_MONITORINGSTATIONCls.getModel(new DC_UTILITY_MONITORINGSTATION_SW { DC_UTILITY_MONITORINGSTATION_ID = DC_UTILITY_MONITORINGSTATION_ID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 监测站管理
        /// </summary>
        /// <returns></returns>
        public ActionResult MONITORINGSTATIONManager()
        {
            DC_UTILITY_MONITORINGSTATION_Model m = new DC_UTILITY_MONITORINGSTATION_Model();
            m.DC_UTILITY_MONITORINGSTATION_ID = Request.Params["DC_UTILITY_MONITORINGSTATION_ID"];
            m.NUMBER = Request.Params["NUMBER"];
            m.NAME = Request.Params["NAME"];
            m.ADDRESS = Request.Params["ADDRESS"];
            m.MODEL = Request.Params["MODEL"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.TRANSFERMODETYPE = Request.Params["TRANSFERMODETYPE"];
            m.MONICONTENT = Request.Params["MONICONTENT"];
            m.BUILDDATE = Request.Params["BUILDDATE"];
            m.USESTATE = Request.Params["USESTATE"];
            m.MANAGERSTATE = Request.Params["MANAGERSTATE"];
            m.MARK = Request.Params["MARK"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.WORTH = Request.Params["WORTH"];
            m.opMethod = Request.Params["Method"];
            m.BUILDDATEBEGIN = Request.Params["BUILDDATEBEGIN"];
            m.BUILDDATEEND = Request.Params["BUILDDATEEND"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.NAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "名称不可为空，请重新输入！", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
            }
            var ms = DC_UTILITY_MONITORINGSTATIONCls.Manager(m);
            if (ms.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, ms.Msg, "")), "text/html;charset=UTF-8");
            if (ConfigCls.getSDEDBTeam() == "1")//配置文件
            {
                TD_MONITORINGSTATION_Model m1 = new TD_MONITORINGSTATION_Model();
                if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
                {
                    //double[] arr = ClsPositionUtil.gcj_To_Gps84(double.Parse(m.WD), double.Parse(m.JD));
                    double[] arr = ClsPositionTrans.GpsTransform(double.Parse(m.WD), double.Parse(m.JD), ConfigCls.getSDELonLatTransform());
                    m1.JD = arr[1].ToString();
                    m1.WD = arr[0].ToString();
                }
                m1.opMethod = m.opMethod;
                m1.NAME = m.NAME;
                m1.TYPE = m.TRANSFERMODETYPE;
                //geometry::STGeomFromText('POINT(103.397553 23.365441)',4326)
                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                //m1.Shape = "POINT(" + m.JD + " " + m.WD + ")";
                if (m.opMethod != "Del")
                {
                    m1.OBJECTID = ms.Url;
                }
                else
                {
                    m1.OBJECTID = m.DC_UTILITY_MONITORINGSTATION_ID;
                }
                if ((string.IsNullOrEmpty(m.JD) || string.IsNullOrEmpty(m.WD)) && m1.opMethod == "Add")
                {

                }
                else
                {
                    TD_MONITORINGSTATIONCls.Manager(m1);
                }
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getMONITORINGSTATIONlist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string orgno = Request.Params["BYORGNO"];
            string usestate = Request.Params["USESTATE"];
            string managerstate = Request.Params["MANAGERSTATE"];
            string transfermodetype = Request.Params["TRANSFERMODETYPE"];
            string name = Request.Params["NAME"];
            string number = Request.Params["NUMBER"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>所属县市</th>");
            sb.AppendFormat("<th>所属乡镇</th>");
            sb.AppendFormat("<th>名称</th>");
            sb.AppendFormat("<th>编号</th>");
            sb.AppendFormat("<th>型号</th>");
            sb.AppendFormat("<th>传输方式</th>");
            sb.AppendFormat("<th>监测内容</th>");
            sb.AppendFormat("<th>使用现状</th>");
            sb.AppendFormat("<th>维护类型</th>");
            sb.AppendFormat("<th>建成日期</th>");
            sb.AppendFormat("<th>总价(元)</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = DC_UTILITY_MONITORINGSTATIONCls.getModelList(new DC_UTILITY_MONITORINGSTATION_SW
            {
                TRANSFERMODETYPE = transfermodetype,
                BYORGNO = orgno,
                USESTATE = usestate,
                MANAGERSTATE = managerstate,
                NUMBER = number,
                NAME = name,
                curPage = int.Parse(page),
                pageSize = int.Parse(PageSize)
            }, out total);
            foreach (var s in result)
            {
                //if (i % 2 == 0)
                //    sb.AppendFormat("<tr>");
                //else
                sb.AppendFormat("<tr class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGXSName);
                if (string.IsNullOrEmpty(s.ORGName))
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "--");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGName);
                }
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NUMBER);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.MODEL);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.TRANSFERMODETYPEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.MONICONTENT);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.USESTATEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.MANAGERSTATEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.BUILDDATE);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.WORTH);
                sb.AppendFormat("    <td class=\" \">");
                if (string.IsNullOrEmpty(s.JD))
                {
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                }
                else
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\" Position('DC_UTILITY_MONITORINGSTATION','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.DC_UTILITY_MONITORINGSTATION_ID, s.NAME);
                }
                sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\"  class=\"searchBox_01 LinkSee\">查看</a>", s.DC_UTILITY_MONITORINGSTATION_ID, "DC_UTILITY_MONITORINGSTATION");
                if (SystemCls.isRight("016006003") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", s.DC_UTILITY_MONITORINGSTATION_ID, page);
                if (SystemCls.isRight("016006004") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.DC_UTILITY_MONITORINGSTATION_ID, page);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8");
        }
        #region 监测站上传

        [HttpPost]
        public ActionResult MONITORINGSTATIONList(FormCollection form)
        {
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                //扩展名
                string extension = System.IO.Path.GetExtension(File.FileName);//
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                    string name = DateTime.Now.ToString("监测站导入-yyyyMMddHHmmss") + extension;
                    if (!FileType.Contains(extension))
                    {

                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content(@"<script>alert('上传文件超过4M，不能上传');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/DataCenterExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        //DC_UTILITY_MONITORINGSTATIONCls.MONITORINGSTATIONUpload(savePath);
                        HSSFWorkbook hssfworkbook;

                        FileStream file = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                            
                                hssfworkbook = new HSSFWorkbook(file);
                       
                        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
                        int rowCount = sheet.LastRowNum;
                        for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                        {

                            IRow row = sheet.GetRow(i);
                            string[] arr = new string[13];
                            for (int k = 0; k < arr.Length; k++)
                            {
                                if (k != 7)
                                    arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                                else
                                    arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                            }
                            DC_UTILITY_MONITORINGSTATION_Model m = new DC_UTILITY_MONITORINGSTATION_Model();
                            //单位	无线电方式	名称	编号	型号	使用现状	维护管理类型 建设日期  监测内容 价值 地址 经度 纬度
                            if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                            {
                                continue;
                            }
                            m.BYORGNO = T_SYS_ORGCls.getCodeByName(arr[0]);
                            if (string.IsNullOrEmpty(m.BYORGNO))
                            {
                                return Content(@"<script>alert('表格中组织机构名称错误，请确认后再上传！');history.go(-1);</script>");
                             
                            }
                            m.opMethod = "Add";
                            m.NAME = arr[2];
                            m.NUMBER = arr[3];
                            m.MODEL = arr[4];
                            m.BUILDDATE = arr[7];
                            if (m.BUILDDATE == "9999-12-31")
                                m.BUILDDATE = "1900-01-01";
                            m.MONICONTENT = arr[8];
                            m.WORTH = arr[9];
                            m.ADDRESS = arr[10];
                            string jd = arr[11];
                            string wd = arr[12];
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                double[] brr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");
                                m.JD = brr[1].ToString();
                                m.WD = brr[0].ToString();
                            }
                            if (arr[5].Trim() == "在用")//使用类型
                            {
                                m.USESTATE = "1";
                            }
                            else if (arr[5].Trim() == "储存")
                            {
                                m.USESTATE = "2";
                            }
                            else if (arr[5].Trim() == "报废")
                            {
                                m.USESTATE = "3";
                            }
                            else
                            {
                                m.USESTATE = "1";
                            }
                            if (arr[6].Trim() == "未维护")//维护管理类型
                            {
                                m.MANAGERSTATE = "1";
                            }
                            else if (arr[6].Trim() == "维护")
                            {
                                m.MANAGERSTATE = "2";
                            }
                            else
                            {
                                m.MANAGERSTATE = "1";
                            }
                            if (arr[1].Trim() == "有线")//无线电方式
                            {
                                m.TRANSFERMODETYPE = "1";
                            }
                            else if (arr[1].Trim() == "无线")
                            {
                                m.TRANSFERMODETYPE = "2";
                            }
                            else
                            {
                                m.TRANSFERMODETYPE = "1";
                            }
                            var ms = DC_UTILITY_MONITORINGSTATIONCls.Manager(m);
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                TD_MONITORINGSTATION_Model m1 = new TD_MONITORINGSTATION_Model();
                                m1.opMethod = "Add";
                                m1.OBJECTID = ms.Url;
                                m1.NAME = m.NAME;
                                m1.TYPE = m.TRANSFERMODETYPE;
                                m1.JD = jd;
                                m1.WD = wd;
                                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                                TD_MONITORINGSTATIONCls.Manager(m1);
                            }
                            string a = row.GetCell(0).ToString();
                            string a1 = row.GetCell(1).ToString();
                            string a2 = row.GetCell(2).ToString();
                        }
                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传！');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的监测站表格');history.go(-1);</script>");
                }
            }
            return Content("<script>alert('导入成功');window.location.href='MONITORINGSTATIONIndex';</script>");
        }
        #endregion
        #endregion

        #region 设施-因子采集站
        /// <summary>
        /// 因子采集页面
        /// </summary>
        /// <returns></returns>
        public ActionResult FACTORCOLLECTSTATIONIndex()
        {
            pubViewBag("016008", "016008", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.usestate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36", isShowAll = "1" });
            ViewBag.usestateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" });
            ViewBag.managerstate = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37", isShowAll = "1" });
            ViewBag.managerstateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37" });
            ViewBag.transfermodetype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "42", isShowAll = "1" });
            ViewBag.transfermodetypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "42" });
            ViewBag.isAdd = (SystemCls.isRight("016008002")) ? "1" : "0";
            return View();
        }
        /// <summary>
        /// 根据序号获取内容
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFACTORCOLLECTSTATIONjson()
        {
            string DC_UTILITY_FACTORCOLLECTSTATION_ID = Request.Params["DC_UTILITY_FACTORCOLLECTSTATION_ID"];
            //if (string.IsNullOrEmpty(ID))
            //    ID = "0";
            return Content(JsonConvert.SerializeObject(DC_UTILITY_FACTORCOLLECTSTATIONCls.getModel(new DC_UTILITY_FACTORCOLLECTSTATION_SW { DC_UTILITY_FACTORCOLLECTSTATION_ID = DC_UTILITY_FACTORCOLLECTSTATION_ID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 因子采集站管理
        /// </summary>
        /// <returns></returns>
        public ActionResult FACTORCOLLECTSTATIONManager()
        {
            DC_UTILITY_FACTORCOLLECTSTATION_Model m = new DC_UTILITY_FACTORCOLLECTSTATION_Model();
            m.DC_UTILITY_FACTORCOLLECTSTATION_ID = Request.Params["DC_UTILITY_FACTORCOLLECTSTATION_ID"];
            m.NUMBER = Request.Params["NUMBER"];
            m.NAME = Request.Params["NAME"];
            m.ADDRESS = Request.Params["ADDRESS"];
            m.MODEL = Request.Params["MODEL"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.TRANSFERMODETYPE = Request.Params["TRANSFERMODETYPE"];
            m.BUILDDATE = Request.Params["BUILDDATE"];
            m.USESTATE = Request.Params["USESTATE"];
            m.MANAGERSTATE = Request.Params["MANAGERSTATE"];
            m.MARK = Request.Params["MARK"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.WORTH = Request.Params["WORTH"];
            m.BUILDDATEBEGIN = Request.Params["BUILDDATEBEGIN"];
            m.BUILDDATEEND = Request.Params["BUILDDATEEND"];
            m.opMethod = Request.Params["Method"];
            m.FACTCOLLCONTENT = Request.Params["FACTCOLLCONTENT"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.NAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "名称不可为空，请重新输入！", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
            }
            var ms = DC_UTILITY_FACTORCOLLECTSTATIONCls.Manager(m);
            if (ms.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, ms.Msg, "")), "text/html;charset=UTF-8");
            if (ConfigCls.getSDEDBTeam() == "1")//配置文件
            {
                TD_FACTORCOLLECTSTATION_Model m1 = new TD_FACTORCOLLECTSTATION_Model();
                if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
                {

                    double[] arr = ClsPositionTrans.GpsTransform(double.Parse(m.WD), double.Parse(m.JD), ConfigCls.getSDELonLatTransform());
                    m1.JD = arr[1].ToString();
                    m1.WD = arr[0].ToString();
                }
                m1.opMethod = m.opMethod;
                m1.NAME = m.NAME;
                m1.TYPE = m.TRANSFERMODETYPE;

                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";

                if (m.opMethod != "Del")
                {
                    m1.OBJECTID = ms.Url;
                }
                else
                {
                    m1.OBJECTID = m.DC_UTILITY_FACTORCOLLECTSTATION_ID;
                }
                if ((string.IsNullOrEmpty(m.JD) || string.IsNullOrEmpty(m.WD)) && m1.opMethod == "Add")
                {

                }
                else
                {
                    TD_FACTORCOLLECTSTATIONCls.Manager(m1);
                }
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getFACTORCOLLECTSTATIONlist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string orgno = Request.Params["BYORGNO"];
            string usestate = Request.Params["USESTATE"];
            string managerstate = Request.Params["MANAGERSTATE"];
            string transfermodetype = Request.Params["TRANSFERMODETYPE"];
            string name = Request.Params["NAME"];
            string number = Request.Params["NUMBER"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>所属县市</th>");
            sb.AppendFormat("<th>所属乡镇</th>");
            sb.AppendFormat("<th>名称</th>");
            sb.AppendFormat("<th>编号</th>");
            sb.AppendFormat("<th>型号</th>");
            sb.AppendFormat("<th>传输方式</th>");
            sb.AppendFormat("<th>采集内容</th>");
            sb.AppendFormat("<th>使用现状</th>");
            sb.AppendFormat("<th>维护类型</th>");
            sb.AppendFormat("<th>建成日期</th>");
            sb.AppendFormat("<th>总价(元)</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = DC_UTILITY_FACTORCOLLECTSTATIONCls.getModelList(new DC_UTILITY_FACTORCOLLECTSTATION_SW
            {
                TRANSFERMODETYPE = transfermodetype,
                BYORGNO = orgno,
                USESTATE = usestate,
                MANAGERSTATE = managerstate,
                NUMBER = number,
                NAME = name,
                curPage = int.Parse(page),
                pageSize = int.Parse(PageSize)
            }, out total);
            foreach (var s in result)
            {
                //if (i % 2 == 0)
                //    sb.AppendFormat("<tr>");
                //else
                sb.AppendFormat("<tr class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGXSName);
                if (string.IsNullOrEmpty(s.ORGName))
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "--");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGName);
                }
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NUMBER);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.MODEL);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.TRANSFERMODETYPEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.FACTCOLLCONTENT);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.USESTATEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.MANAGERSTATEName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.BUILDDATE);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.WORTH);
                sb.AppendFormat("    <td class=\" \">");
                if (string.IsNullOrEmpty(s.JD))
                {
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                }
                else
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\" Position('DC_UTILITY_FACTORCOLLECTSTATION','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.DC_UTILITY_FACTORCOLLECTSTATION_ID, s.NAME);
                }
                sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.DC_UTILITY_FACTORCOLLECTSTATION_ID, "DC_UTILITY_FACTORCOLLECTSTATION");
                if (SystemCls.isRight("016008003") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", s.DC_UTILITY_FACTORCOLLECTSTATION_ID, page);
                if (SystemCls.isRight("016008004") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.DC_UTILITY_FACTORCOLLECTSTATION_ID, page);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }

        #region 因子采集站上传

        [HttpPost]
        public ActionResult FACTORCOLLECTSTATIONList(FormCollection form)
        {
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                //扩展名
                string extension = System.IO.Path.GetExtension(File.FileName);//
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                    string name = DateTime.Now.ToString("因子采集站导入-yyyyMMddHHmmss") + extension;
                    if (!FileType.Contains(extension))
                    {

                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content(@"<script>alert('上传文件超过4M，不能上传');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/DataCenterExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        //DC_UTILITY_FACTORCOLLECTSTATIONCls.FACTORCOLLECTSTATIONUpload(savePath);
                        HSSFWorkbook hssfworkbook;
                        FileStream file = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                        hssfworkbook = new HSSFWorkbook(file);
                        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
                        int rowCount = sheet.LastRowNum;
                        for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                        {

                            IRow row = sheet.GetRow(i);
                            string[] arr = new string[13];
                            for (int k = 0; k < arr.Length; k++)
                            {
                                if (k != 7)
                                    arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                                else
                                    arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                            }
                            DC_UTILITY_FACTORCOLLECTSTATION_Model m = new DC_UTILITY_FACTORCOLLECTSTATION_Model();
                            //单位	无线电方式	名称	编号	型号	使用现状	维护管理类型 建设日期  采集内容 价值 地址 经度 纬度
                            if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                            {
                                continue;
                            }
                            m.BYORGNO = T_SYS_ORGCls.getCodeByName(arr[0]);
                            if (string.IsNullOrEmpty(m.BYORGNO))
                            {
                                return Content(@"<script>alert('表格中组织机构名称错误，请确认后再上传！');history.go(-1);</script>");
                                
                            }
                            m.opMethod = "Add";
                            m.NAME = arr[2];
                            m.NUMBER = arr[3];
                            m.MODEL = arr[4];
                            m.BUILDDATE = arr[7];
                            if (m.BUILDDATE == "9999-12-31")
                                m.BUILDDATE = "1900-01-01";
                            m.FACTCOLLCONTENT = arr[8];
                            m.WORTH = arr[9];
                            m.ADDRESS = arr[10];
                            string jd = arr[11];
                            string wd = arr[12];
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                double[] brr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");
                                m.JD = brr[1].ToString();
                                m.WD = brr[0].ToString();
                            }
                            if (arr[5].Trim() == "在用")//使用类型
                            {
                                m.USESTATE = "1";
                            }
                            else if (arr[5].Trim() == "储存")
                            {
                                m.USESTATE = "2";
                            }
                            else if (arr[5].Trim() == "报废")
                            {
                                m.USESTATE = "3";
                            }
                            else
                            {
                                m.USESTATE = "1";
                            }
                            if (arr[6].Trim() == "未维护")//维护管理类型
                            {
                                m.MANAGERSTATE = "1";
                            }
                            else if (arr[6].Trim() == "维护")
                            {
                                m.MANAGERSTATE = "2";
                            }
                            else
                            {
                                m.MANAGERSTATE = "1";
                            }
                            if (arr[1].Trim() == "有线")//无线电方式
                            {
                                m.TRANSFERMODETYPE = "1";
                            }
                            else if (arr[1].Trim() == "无线")
                            {
                                m.TRANSFERMODETYPE = "2";
                            }
                            else
                            {
                                m.TRANSFERMODETYPE = "1";
                            }
                            var ms = DC_UTILITY_FACTORCOLLECTSTATIONCls.Manager(m);
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                TD_FACTORCOLLECTSTATION_Model m1 = new TD_FACTORCOLLECTSTATION_Model();
                                m1.OBJECTID = ms.Url;
                                m1.opMethod = "Add";
                                m1.NAME = m.NAME;
                                m1.TYPE = m.TRANSFERMODETYPE;
                                m1.JD = jd;
                                m1.WD = wd;
                                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                                TD_FACTORCOLLECTSTATIONCls.Manager(m1);
                            }
                            string a = row.GetCell(0).ToString();
                            string a1 = row.GetCell(1).ToString();
                            string a2 = row.GetCell(2).ToString();

                        }
                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传！');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的因子采集站表格');history.go(-1);</script>");
                }
            }
            return Content("<script>alert('导入成功');window.location.href='FACTORCOLLECTSTATIONIndex';</script>");
        }
        #endregion
        #endregion

        #endregion

        #region 资源管理（新）
        /// <summary>
        /// 资源管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RESOURCE_NEWIndex()
        {
            pubViewBag("011003", "011003", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.agetype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "27", isShowAll = "1" });
            ViewBag.agetypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "27" });
            ViewBag.resourcetype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "28", isShowAll = "1" });
            ViewBag.resourcetypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "28" });
            ViewBag.originttype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "29", isShowAll = "1" });
            ViewBag.originttypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "29" });
            ViewBag.burntype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "30", isShowAll = "1" });
            ViewBag.burntypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "30" });
            ViewBag.treetype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "31", isShowAll = "1" });
            ViewBag.treetypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "31" });
            ViewBag.isAdd = (SystemCls.isRight("011003002")) ? "1" : "0";
            return View();
        }
        /// <summary>
        /// 根据序号获取内容
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRESOURCE_NEWjson()
        {
            string DC_RESOURCE_NEW_ID = Request.Params["DC_RESOURCE_NEW_ID"];
            //if (string.IsNullOrEmpty(ID))
            //    ID = "0";
            return Content(JsonConvert.SerializeObject(DC_RESOURCE_NEWCls.getModel(new DC_RESOURCE_NEW_SW { DC_RESOURCE_NEW_ID = DC_RESOURCE_NEW_ID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 资源管理
        /// </summary>
        /// <returns></returns>
        public ActionResult RESOURCE_NEWManager()
        {
            DC_RESOURCE_NEW_Model m = new DC_RESOURCE_NEW_Model();
            m.DC_RESOURCE_NEW_ID = Request.Params["DC_RESOURCE_NEW_ID"];
            m.RESOURCETYPE = Request.Params["RESOURCETYPE"];
            m.NUMBER = Request.Params["NUMBER"];
            m.NAME = Request.Params["NAME"];
            m.ORGNOS = Request.Params["ORGNOS"];
            m.KINDTYPE = Request.Params["KINDTYPE"];
            m.AGETYPE = Request.Params["AGETYPE"];
            m.ORIGINTYPE = Request.Params["ORIGINTYPE"];
            m.AREA = Request.Params["AREA"];
            m.BURNTYPE = Request.Params["BURNTYPE"];
            m.TREETYPE = Request.Params["TREETYPE"];
            m.ASPECT = Request.Params["ASPECT"];
            m.ANGLE = Request.Params["ANGLE"];
            m.MARK = Request.Params["MARK"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.POTHOOKLEADER = Request.Params["POTHOOKLEADER"];
            m.POTHOOKLEADERJOB = Request.Params["POTHOOKLEADERJOB"];
            m.POTHOOKLEADERTLEE = Request.Params["POTHOOKLEADERTLEE"];
            m.DUTYPERSON = Request.Params["DUTYPERSON"];
            m.DUTYPERSONTELL = Request.Params["DUTYPERSONTELL"];
            m.opMethod = Request.Params["Method"];
            m.JWDLIST = Request.Params["JWDLIST"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.NAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入名称！", "")), "text/html;charset=UTF-8");
            }
            var ms = DC_RESOURCE_NEWCls.Manager(m);
            if (ms.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, ms.Msg, "")), "text/html;charset=UTF-8");
            if (ConfigCls.getSDEDBTeam() == "1")//配置文件
            {
                TD_RESOURCE_Model m1 = new TD_RESOURCE_Model();
                m1.opMethod = m.opMethod;
                m1.NAME = m.NAME;
                m1.TYPE = m.RESOURCETYPE;
                string polygon = "";
                string polygon1 = "";
                string jd = "";
                string wd = "";
                string dx = "";
                string dy = "";
                if (string.IsNullOrEmpty(m.JWDLIST) == false)
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
                    m1.JD = ((jdmax + jdmin) / 2).ToString();
                    m1.WD = ((wdmax + wdmin) / 2).ToString();
                }
                if (polygon1 != "")
                {
                    polygon1 = polygon1.Substring(0, polygon1.LastIndexOf(","));
                }
                m1.Shape = "geometry::STGeomFromText('MULTIPolygon((" + polygon1 + "))',4326).MakeValid()";
                if (m.opMethod != "Del")
                {
                    m1.OBJECTID = ms.Url;
                }
                else
                {
                    m1.OBJECTID = m.DC_RESOURCE_NEW_ID;
                }
                if (string.IsNullOrEmpty(m.JWDLIST) && m1.opMethod == "Add")
                {

                }
                else
                {
                    TD_RESOURCECLS.Manager(m1);
                }

            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 资源查询页面
        /// </summary>
        /// <returns></returns>
        public ActionResult getRESOURCE_NEWlist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string orgno = Request.Params["ORGNOS"];
            string agetype = Request.Params["AGETYPE"];
            string resourcetype = Request.Params["RESOURCETYPE"];
            string originttype = Request.Params["ORIGINTYPE"];
            string burntype = Request.Params["BURNTYPE"];
            string treetype = Request.Params["TREETYPE"];
            string kindtype = Request.Params["KINDTYPE"];
            string name = Request.Params["NAME"];
            string number = Request.Params["NUMBER"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style=\"width:2%\">序号</th>");
            sb.AppendFormat("<th style=\"width:6%\">所属县市</th>");
            sb.AppendFormat("<th style=\"width:7%\">所属乡镇</th>");
            sb.AppendFormat("<th style=\"width:5%\">资源类型</th>");
            sb.AppendFormat("<th style=\"width:9%\">名称</th>");
            //sb.AppendFormat("<th style=\"width:5%\">编号</th>");
            sb.AppendFormat("<th style=\"width:8%\">树种</th>");
            sb.AppendFormat("<th style=\"width:5%\">林龄类别</th>");
            sb.AppendFormat("<th style=\"width:5%\">起源类型</th>");
            sb.AppendFormat("<th style=\"width:5%\">可燃类型</th>");
            sb.AppendFormat("<th style=\"width:5%\">林木类型</th>");
            sb.AppendFormat("<th style=\"width:8%\">面积(公顷)</th>");
            sb.AppendFormat("<th style=\"width:5%\">挂钩领导</th>");
            sb.AppendFormat("<th style=\"width:5%\">责任人</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = DC_RESOURCE_NEWCls.getModelList(new DC_RESOURCE_NEW_SW { AGETYPE = agetype, ORGNOS = orgno, NUMBER = number, KINDTYPE = kindtype, NAME = name, RESOURCETYPE = resourcetype, ORIGINTYPE = originttype, BURNTYPE = burntype, TREETYPE = treetype, curPage = int.Parse(page), pageSize = int.Parse(PageSize) }, out total);
            foreach (var s in result)
            {
                //if (i % 2 == 0)
                //    sb.AppendFormat("<tr>");
                //else
                sb.AppendFormat("<tr class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGXSName);
                if (string.IsNullOrEmpty(s.ORGNOSName))
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "--");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGNOSName);
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.RESOURCETYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.NAME);
                //sb.AppendFormat("<td class=\"center\">{0}</td>", s.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.KINDTYPE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.AGETYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORIGINTYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.BURNTYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.TREETYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.AREA);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.POTHOOKLEADER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.DUTYPERSON);
                sb.AppendFormat("    <td class=\" \" >");
                if (string.IsNullOrEmpty(s.JWDLIST))
                {
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                }
                else
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\" PositionLine('DC_RESOURCE_NEW','{0}','{1}',2)\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.DC_RESOURCE_NEW_ID, s.NAME);
                }
                sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.DC_RESOURCE_NEW_ID, "DC_RESOURCE_NEW");
                sb.AppendFormat("<a href=\"#\" onclick=\" Photo('{0}','{1}')\" title='照片管理' class=\"searchBox_01 LinkPhoto\">照片</a>", s.DC_RESOURCE_NEW_ID, "DC_RESOURCE_NEW");
                if (SystemCls.isRight("011003003") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", s.DC_RESOURCE_NEW_ID, page);
                if (SystemCls.isRight("011003004") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.DC_RESOURCE_NEW_ID, page);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8");
        }
        #region 资源上传

        [HttpPost]
        public ActionResult RESOURCEList(FormCollection form)
        {
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                //扩展名
                string extension = System.IO.Path.GetExtension(File.FileName);//
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                    string name = DateTime.Now.ToString("资源导入-yyyyMMddHHmmss") + extension;
                    if (!FileType.Contains(extension))
                    {
                        //ViewBag.error = "文件类型不对，只能导入xls和xlsx格式的文件";
                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        //ViewBag.error = "上传文件超过4M，不能上传";
                        //return View();
                        return Content(@"<script>alert('上传文件超过4M，不能上传');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/DataCenterExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        //DC_RESOURCE_NEWCls.RESOURCEUpload(savePath);
                        HSSFWorkbook hssfworkbook;
                        FileStream file = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                         hssfworkbook = new HSSFWorkbook(file);
                        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
                        int rowCount = sheet.LastRowNum;
                        for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                        {

                            IRow row = sheet.GetRow(i);
                            string[] arr = new string[17];
                            for (int k = 0; k < arr.Length; k++)
                            {
                                arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                            }
                            DC_RESOURCE_NEW_Model m = new DC_RESOURCE_NEW_Model();
                            //单位 资源类型 名称 编号 林龄类型 起源类型 可燃类型 林木类型 树种 面积 坡向 坡度 挂钩领导 职务 领导电话 责任人 责任人电话
                            if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                            {
                                continue;
                            }
                             m.ORGNOS = T_SYS_ORGCls.getCodeByName(arr[0]);
                            if (string.IsNullOrEmpty(m.ORGNOS))
                            {
                                   return Content(@"<script>alert('表格中组织机构名称错误，请确认后再上传！');history.go(-1);</script>");
                            }
                            m.opMethod= "Add";
                            m.NAME = arr[2];
                            m.NUMBER = arr[3];
                            m.KINDTYPE = arr[8];
                            m.AREA = arr[9];
                            m.ASPECT = arr[10];
                            m.ANGLE = arr[11];
                            m.POTHOOKLEADER = arr[12];
                            m.POTHOOKLEADERJOB = arr[13];
                            m.POTHOOKLEADERTLEE = arr[14];
                            m.DUTYPERSON = arr[15];
                            m.DUTYPERSONTELL = arr[16];
                            if (arr[1].Trim() == "重点林区")//资源类型
                            {
                                m.RESOURCETYPE = "1";
                            }
                            else if (arr[1].Trim() == "有林地")
                            {
                                m.RESOURCETYPE = "2";
                            }
                            else if (arr[1].Trim() == "荒山")
                            {
                                m.RESOURCETYPE = "3";
                            }
                            else if (arr[1].Trim() == "灌木丛")
                            {
                                m.RESOURCETYPE = "4";
                            }
                            else
                            {
                                m.RESOURCETYPE = "1";
                            }
                            if (arr[4].Trim() == "幼龄林")//林龄类型
                            {
                                m.AGETYPE = "1";
                            }
                            else if (arr[4].Trim() == "中龄林")
                            {
                                m.AGETYPE = "2";
                            }
                            else if (arr[4].Trim() == "近熟林")
                            {
                                m.AGETYPE = "3";
                            }
                            else if (arr[4].Trim() == "成熟林")
                            {
                                m.AGETYPE = "4";
                            }
                            else if (arr[4] == "过熟林")
                            {
                                m.AGETYPE = "5";
                            }
                            else
                            {
                                m.AGETYPE = "1";
                            }
                            if (arr[5].Trim() == "天然")//起源类型
                            {
                                m.ORIGINTYPE = "1";
                            }
                            else if (arr[5].Trim() == "人工")
                            {
                                m.ORIGINTYPE = "2";
                            }
                            else
                            {
                                m.ORIGINTYPE = "1";
                            }
                            if (arr[6].Trim() == "易燃")//可燃类型
                            {
                                m.BURNTYPE = "1";
                            }
                            else if (arr[6].Trim() == "不易燃")
                            {
                                m.BURNTYPE = "2";
                            }
                            else
                            {
                                m.BURNTYPE = "1";
                            }
                            if (arr[7].Trim() == "针叶林")//林木类型
                            {
                                m.TREETYPE = "1";
                            }
                            else if (arr[7].Trim() == "阔叶林")
                            {
                                m.TREETYPE = "2";
                            }
                            else if (arr[7].Trim() == "混交林")
                            {
                                m.TREETYPE = "3";
                            }
                            else
                            {
                                m.TREETYPE = "1";
                            }
                            DC_RESOURCE_NEWCls.Manager(m);
                            string a = row.GetCell(0).ToString();
                            string a1 = row.GetCell(1).ToString();
                            string a2 = row.GetCell(2).ToString();
                        }
                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传！');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的资源表格');history.go(-1);</script>");
                }
            }
            //return RedirectToAction("FRUserList", "System");
            return Content("<script>alert('导入成功');window.location.href='RESOURCE_NEWIndex';</script>");
            //return View();
        }
        #endregion
        #endregion

        #region 队伍管理（新）
        /// <summary>
        /// 队伍管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ARMY_NEWIndex()
        {
            pubViewBag("011002", "011002", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.armytype = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "26", isShowAll = "1" });
            ViewBag.armytypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "26" });
            ViewBag.vdSex = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTFLAG = "性别" });
            ViewBag.isAdd = (SystemCls.isRight("011002002")) ? "1" : "0";
            return View();
        }
        /// <summary>
        /// 根据序号获取内容
        /// </summary>
        /// <returns></returns>
        public ActionResult GetARMYjson()
        {
            string DC_ARMY_ID = Request.Params["DC_ARMY_ID"];
            //if (string.IsNullOrEmpty(ID))
            //    ID = "0";
            return Content(JsonConvert.SerializeObject(DC_ARMYCls.getModel(new DC_ARMY_SW { DC_ARMY_ID = DC_ARMY_ID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 队伍管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ARMYManager()
        {
            DC_ARMY_Model m = new DC_ARMY_Model();
            m.DC_ARMY_ID = Request.Params["DC_ARMY_ID"];
            m.ARMYTYPE = Request.Params["ARMYTYPE"];
            m.NUMBER = Request.Params["NUMBER"];
            m.NAME = Request.Params["NAME"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.ARMYMEMBERCOUNT = Request.Params["ARMYMEMBERCOUNT"];
            m.ARMYLEADER = Request.Params["ARMYLEADER"];
            m.CONTACTS = Request.Params["CONTACTS"];
            m.ARMYCHARACTER = Request.Params["ARMYCHARACTER"];
            m.ARMYEQUIP = Request.Params["ARMYEQUIP"];
            m.MARK = Request.Params["MARK"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.opMethod = Request.Params["Method"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.NAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入队伍名称", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
            }
            var ms = DC_ARMYCls.Manager(m);
            if (ms.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, ms.Msg, "")), "text/html;charset=UTF-8");
            if (ConfigCls.getSDEDBTeam() == "1")//配置文件
            {
                Firedepartment_Model m1 = new Firedepartment_Model();
                if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
                {
                    double[] arr = ClsPositionTrans.GpsTransform(double.Parse(m.WD), double.Parse(m.JD), ConfigCls.getSDELonLatTransform());
                    m1.JD = arr[1].ToString();
                    m1.WD = arr[0].ToString();
                }
                m1.opMethod = m.opMethod;
                m1.NAME = m.NAME;
                m1.TYPE = m.ARMYTYPE;
                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                if (m.opMethod != "Del")
                {
                    m1.OBJECTID = ms.Url;
                }
                else
                {
                    m1.OBJECTID = m.DC_ARMY_ID;
                }
                if ((string.IsNullOrEmpty(m.JD) || string.IsNullOrEmpty(m.WD)) && m1.opMethod == "Add")
                {

                }
                else
                {
                    FiredepartmentCls.Manager(m1);
                }


            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取列表查询页面
        /// </summary>
        /// <returns></returns>
        public ActionResult getARMYlist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string orgno = Request.Params["BYORGNO"];
            string armytype = Request.Params["ARMYTYPE"];
            string name = Request.Params["NAME"];
            string number = Request.Params["NUMBER"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>所属县市</th>");
            sb.AppendFormat("<th>所属乡镇</th>");
            sb.AppendFormat("<th>队伍类型</th>");
            sb.AppendFormat("<th>名称</th>");
            sb.AppendFormat("<th>编号</th>");
            sb.AppendFormat("<th>人数</th>");
            sb.AppendFormat("<th>队长</th>");
            sb.AppendFormat("<th>联系方式</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = DC_ARMYCls.getModelList(new DC_ARMY_SW { BYORGNO = orgno, NUMBER = number, ARMYTYPE = armytype, NAME = name, curPage = int.Parse(page), pageSize = int.Parse(PageSize) }, out total);
            foreach (var s in result)
            {
                //if (i % 2 == 0)
                //    sb.AppendFormat("<tr>");
                //else
                sb.AppendFormat("<tr class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGXSName);
                if (string.IsNullOrEmpty(s.ORGName))
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "--");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGName);
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ARMYTYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.NUMBER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ARMYMEMBERCOUNT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ARMYLEADER);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.CONTACTS);
                sb.AppendFormat("    <td class=\"center\">");
                if (string.IsNullOrEmpty(s.JD))
                {
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                }
                else
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\" Position('DC_ARMY','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.DC_ARMY_ID, s.NAME);
                }
                sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.DC_ARMY_ID, "DC_ARMY");
                sb.AppendFormat("<a href=\"#\" onclick=\" GetMember('{0}')\" title='人员管理' class=\"searchBox_01 LinkPeople\">人员</a>", s.DC_ARMY_ID);
                sb.AppendFormat("<a href=\"#\" onclick=\" GetArmyEQUIP('{0}')\" title='准备管理' class=\"searchBox_01 LinkEquip\">装备</a>", s.DC_ARMY_ID);
                sb.AppendFormat("<a href=\"#\" onclick=\" Photo('{0}','{1}')\" title='照片管理' class=\"searchBox_01 LinkPhoto\">照片</a>", s.DC_ARMY_ID, "DC_ARMY");
                if (SystemCls.isRight("011002003") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", s.DC_ARMY_ID, page);
                if (SystemCls.isRight("011002004") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.DC_ARMY_ID, page);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }
        /// <summary>
        /// 队伍人员管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ARMY_MEMBERManager()
        {
            DC_ARMY_MEMBER_Model m = new DC_ARMY_MEMBER_Model();
            m.DC_ARMY_MEMBER_ID = Request.Params["DC_ARMY_MEMBER_ID"];
            m.DC_ARMY_ID = Request.Params["DC_ARMY_ID"];
            m.MEMBERNAME = Request.Params["MEMBERNAME"];
            m.SEX = Request.Params["SEX"];
            m.CONTACTS = Request.Params["CONTACTS"];
            m.BIRTH = Request.Params["BIRTH"];
            m.opMethod = Request.Params["Method"];
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.MEMBERNAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入姓名", "")), "text/html;charset=UTF-8");
            }
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            return Content(JsonConvert.SerializeObject(DC_ARMY_MEMBERCls.Manager(m)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 队伍装备管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ARMY_EQUIPManager()
        {
            DC_ARMY_EQUIP_Model m = new DC_ARMY_EQUIP_Model();
            m.DC_ARMY_EQUIP_ID = Request.Params["DC_ARMY_EQUIP_ID"];
            m.DC_ARMY_ID = Request.Params["DC_ARMY_ID"];
            m.EQUIPNAME = Request.Params["EQUIPNAME"];
            m.EQUIPNUM = Request.Params["EQUIPNUM"];
            m.EQUIPMODEL = Request.Params["EQUIPMODEL"];
            m.EQUIPUSESTATE = Request.Params["EQUIPUSESTATE"];
            m.EQUIPSUM = Request.Params["EQUIPSUM"];
            m.opMethod = Request.Params["Method"];
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.EQUIPNAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入装备名称", "")), "text/html;charset=UTF-8");
            }
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            return Content(JsonConvert.SerializeObject(DC_ARMY_EQUIPCls.Manager(m)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 根据序号获取队伍人员
        /// </summary>
        /// <returns></returns>
        public ActionResult GetARMY_MEMBERjson()
        {
            string DC_ARMY_MEMBER_ID = Request.Params["DC_ARMY_MEMBER_ID"];
            //if (string.IsNullOrEmpty(ID))
            //    ID = "0";
            return Content(JsonConvert.SerializeObject(DC_ARMY_MEMBERCls.getModel(new DC_ARMY_MEMBER_SW { DC_ARMY_MEMBER_ID = DC_ARMY_MEMBER_ID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 根据序号获取队伍装备
        /// </summary>
        /// <returns></returns>
        public ActionResult GetARMY_EQUIPjson()
        {
            string DC_ARMY_EQUIP_ID = Request.Params["DC_ARMY_EQUIP_ID"];
            //if (string.IsNullOrEmpty(ID))
            //    ID = "0";
            return Content(JsonConvert.SerializeObject(DC_ARMY_EQUIPCls.getModel(new DC_ARMY_EQUIP_SW { DC_ARMY_EQUIP_ID = DC_ARMY_EQUIP_ID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取人员管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult getARMY_MEMBERlist()
        {
            string DC_ARMY_ID = Request.Params["DC_ARMY_ID"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>姓名</th>");
            sb.AppendFormat("<th>性别</th>");
            sb.AppendFormat("<th>联系方式</th>");
            sb.AppendFormat("<th>出生日期</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = DC_ARMY_MEMBERCls.getModelList(new DC_ARMY_MEMBER_SW { DC_ARMY_ID = DC_ARMY_ID });
            foreach (var s in result)
            {
                sb.AppendFormat("<tr class='{6}' onclick=\"MEMOnclik('{0}','{1}','{2}','{3}','{4}','{5}')\">", s.DC_ARMY_MEMBER_ID, s.MEMBERNAME, s.SEX, s.CONTACTS, s.BIRTH, s.DC_ARMY_ID, (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.MEMBERNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.SEXNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.CONTACTS);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.BIRTH);
                sb.AppendFormat("    <td class=\" \">");
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            if (i % 2 == 0)
                sb.AppendFormat("<tr>");
            else
                sb.AppendFormat("<tr class='row1'>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"MEMBERNAME\"/>");
            sb.AppendFormat("<td class=\"center\"><select  id=\"SEX\">{0}</select></td>", T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "3" }));
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"CONTACTS1\"/>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input  class=\"easyui-datebox\" type=\"text\" id=\"BIRTH1\" style=\"width:90px;height:28px;\"/>");
            sb.AppendFormat("    <td class=\" \">");
            sb.AppendFormat("{0}", "<input type=\"hidden\" id=\"DC_ARMY_MEMBER_ID1\"/>");
            sb.AppendFormat("{0}", "<input type=\"hidden\" id=\"DC_ARMY_ID1\"/>");
            if (SystemCls.isRight("011002005") == true)
                sb.AppendFormat("{0}", "<input type=\"button\" value=\"添加\" onclick=\"ManagerMem('Add')\" class=\"btnAddCss\"/>");
            if (SystemCls.isRight("011002006") == true)
                sb.AppendFormat("{0}", "<input type=\"button\" id=\"btnMemMdy\" style=\"display:none;\" value=\"修改\" onclick=\"ManagerMem('Mdy')\" class=\"btnMdyCss\"  />");
            if (SystemCls.isRight("011002007") == true)
                sb.AppendFormat("{0}", "<input type=\"button\" id=\"btnMemDel\" style=\"display:none;\"  value=\"删除\" onclick=\"ManagerMem('Del')\" class=\"btnDelCss\" />");
            sb.AppendFormat("    </td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), "")), "text/html;charset=UTF-8"); ;
        }

        public ActionResult getARMY_EQUIPlist()
        {
            string DC_ARMY_ID = Request.Params["DC_ARMY_ID"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>名称</th>");
            sb.AppendFormat("<th>编号</th>");
            sb.AppendFormat("<th>型号</th>");
            sb.AppendFormat("<th>使用现状类型</th>");
            sb.AppendFormat("<th>数量</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = DC_ARMY_EQUIPCls.getModelList(new DC_ARMY_EQUIP_SW { DC_ARMY_ID = DC_ARMY_ID });
            foreach (var s in result)
            {
                sb.AppendFormat("<tr class='{7}' onclick=\"EQUOnclik('{0}','{1}','{2}','{3}','{4}','{5}','{6}')\">", s.DC_ARMY_EQUIP_ID, s.EQUIPNAME, s.EQUIPNUM, s.EQUIPMODEL, s.EQUIPUSESTATE, s.EQUIPSUM, s.DC_ARMY_ID, (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.EQUIPNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.EQUIPNUM);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.EQUIPMODEL);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.EQUIPUSESTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.EQUIPSUM);
                sb.AppendFormat("    <td class=\" \">");
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            if (i % 2 == 0)
                sb.AppendFormat("<tr>");
            else
                sb.AppendFormat("<tr class='row1'>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"EQUIPNAME\"/>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"EQUIPNUM\"/>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"EQUIPMODEL\"/>");
            sb.AppendFormat("<td class=\"center\"><select  id=\"EQUIPUSESTATE\">{0}</select></td>", T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" }));
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"EQUIPSUM\"/>");
            sb.AppendFormat("    <td class=\" \">");
            sb.AppendFormat("{0}", "<input type=\"hidden\" id=\"DC_ARMY_EQUIP_ID\"/>");
            sb.AppendFormat("{0}", "<input type=\"hidden\" id=\"DC_ARMY_ID\"/>");
            if (SystemCls.isRight("011002011") == true)
                sb.AppendFormat("{0}", "<input type=\"button\" value=\"添加\" onclick=\"ManagerEqu('Add')\" class=\"btnAddCss\" />");
            if (SystemCls.isRight("011002012") == true)
                sb.AppendFormat("{0}", "<input type=\"button\" id=\"btnEquMdy\" style=\"display:none;\" value=\"修改\" onclick=\"ManagerEqu('Mdy')\" class=\"btnMdyCss\" />");
            if (SystemCls.isRight("011002013") == true)
                sb.AppendFormat("{0}", "<input type=\"button\" id=\"btnEquDel\" style=\"display:none;\"  value=\"删除\" onclick=\"ManagerEqu('Del')\" class=\"btnDelCss\" />");
            sb.AppendFormat("    </td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), "")), "text/html;charset=UTF-8"); ;
        }
        #region 队伍上传

        [HttpPost]
        public ActionResult ARMYList(FormCollection form)
        {
            string savePath = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase File = Request.Files[0];
                //扩展名
                string extension = System.IO.Path.GetExtension(File.FileName);//
                string Filename = File.FileName;
                string p_Name = Filename;
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(Filename);//获取无扩展名的文件名
                if (File.ContentLength != 0)
                {
                    int filesize = File.ContentLength;//获取上传文件的大小单位为字节byte
                    int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                    string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                    string name = DateTime.Now.ToString("队伍导入-yyyyMMddHHmmss") + extension;
                    if (!FileType.Contains(extension))
                    {
                        //ViewBag.error = "文件类型不对，只能导入xls和xlsx格式的文件";
                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        //ViewBag.error = "上传文件超过4M，不能上传";
                        //return View();
                        return Content(@"<script>alert('上传文件超过4M，不能上传');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/DataCenterExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        //DC_ARMYCls.ARMYUpload(savePath);
                        HSSFWorkbook hssfworkbook;

                        FileStream file = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                        hssfworkbook = new HSSFWorkbook(file);
                        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
                        int rowCount = sheet.LastRowNum;
                        for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                        {

                            IRow row = sheet.GetRow(i);
                            string[] arr = new string[9];
                            for (int k = 0; k < arr.Length; k++)
                            {
                                arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                            }
                            DC_ARMY_Model m = new DC_ARMY_Model();
                            //单位	队伍类型	名称	编号	人数	队长	联系方式
                            if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[1]) || string.IsNullOrEmpty(arr[2]))
                            {
                                continue;
                            }
                            m.BYORGNO = T_SYS_ORGCls.getCodeByName(arr[0]);
                            if (string.IsNullOrEmpty(m.BYORGNO))
                            {
                                return Content(@"<script>alert('表格中组织机构名称错误，请确认后再上传！');history.go(-1);</script>");
                            }
                            m.opMethod = "Add";
                            m.NAME = arr[2];
                            m.NUMBER = arr[3];
                            m.ARMYMEMBERCOUNT = arr[4];
                            m.ARMYLEADER = arr[5];
                            m.CONTACTS = arr[6];
                            string jd = arr[7];
                            string wd = arr[8];
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                double[] brr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");
                                m.JD = brr[1].ToString();
                                m.WD = brr[0].ToString();
                            }
                            if (arr[1].Trim() == "专业队伍")//性别
                            {
                                m.ARMYTYPE = "1";
                            }
                            else if (arr[1].Trim() == "半专业队伍")
                            {
                                m.ARMYTYPE = "2";
                            }
                            else if (arr[1].Trim() == "应急队伍")
                            {
                                m.ARMYTYPE = "3";
                            }
                            else if (arr[1].Trim() == "群众队伍")
                            {
                                m.ARMYTYPE = "4";
                            }
                            else
                            {
                                m.ARMYTYPE = "1";
                            }
                            var ms = DC_ARMYCls.Manager(m);
                            if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                            {
                                Firedepartment_Model m1 = new Firedepartment_Model();
                                m1.OBJECTID = ms.Url;
                                m1.NAME = m.NAME;
                                m1.TYPE = m.ARMYTYPE;
                                m1.JD = jd;
                                m1.WD = wd;
                                m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                                m1.opMethod = "Add";
                                FiredepartmentCls.Manager(m1);
                            }
                            string a = row.GetCell(0).ToString();
                            string a1 = row.GetCell(1).ToString();
                            string a2 = row.GetCell(2).ToString();
                        }
                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传！');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的队伍表格');history.go(-1);</script>");
                }
            }
            return Content("<script>alert('导入成功');window.location.href='ARMY_NEWIndex';</script>");
        }
        #endregion
        #endregion

        #region 图片管理
        /// <summary>
        /// 图片管理
        /// </summary>
        /// <returns></returns>
        public ActionResult PhotoManager()
        {
            DC_PHOTO_Model m = new DC_PHOTO_Model();
            m.PHOTO_ID = Request.Params["PHOTO_ID"];
            m.PHOTOTITLE = Request.Params["PHOTOTITLE"];
            m.PHOTOFILENAME = Request.Params["PHOTOFILENAME"];
            m.PHOTOEXPLAIN = Request.Params["PHOTOEXPLAIN"];
            m.PRID = Request.Params["PRID"];
            m.PHOTOTYPE = Request.Params["PHOTOTYPE"];
            m.opMethod = Request.Params["Method"];
            m.PHOTOTIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now.ToString());
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.PHOTOFILENAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入照片名称", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(DC_PHOTOCls.Manager(m)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取图片页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PhotoIndex()
        {
            string PRID = Request.Params["PRID"];
            string PHOTOTYPE = Request.Params["PHOTOTYPE"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>照片标题</th>");
            sb.AppendFormat("<th>照片说明</th>");
            sb.AppendFormat("<th>照片时间</th>");
            sb.AppendFormat("<th>缩略图</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = DC_PHOTOCls.getModelList(new DC_PHOTO_SW { PRID = PRID, PHOTOTYPE = PHOTOTYPE });
            foreach (var s in result)
            {
                sb.AppendFormat("<tr class='{4}' onclick=\"PHOnclik('{0}','{1}','{2}','{3}')\">", s.PHOTO_ID, s.PHOTOTITLE, s.PHOTOEXPLAIN, s.PHOTOFILENAME, (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\"><a href=\"/UploadFile/DacPhoto/{1}\" target=\"_blank\">{0}</a></td>", s.PHOTOTITLE, s.PHOTOFILENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PHOTOEXPLAIN);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PHOTOTIME);
                sb.AppendFormat("<td class=\"left\"><img src=\"/UploadFile/DacPhoto/{0}\" alt=\"alttext\" title=\"{1}\" height =\"35px\" wideth=\"35px\"/></a></td>", s.PHOTOFILENAME, s.PHOTOTITLE);
                sb.AppendFormat("    <td class=\"center\">");
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            if (i % 2 == 0)
                sb.AppendFormat("<tr>");
            else
                sb.AppendFormat("<tr class='row1'>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
            sb.AppendFormat("<td class=\"left\">{0}</td>", "<input type=\"text\" class=\"center\" id=\"PHOTOTITLE\"/>");
            sb.AppendFormat("<td class=\"left\">{0}</td>", "<input type=\"text\" class=\"center\" id=\"PHOTOEXPLAIN\"/>");
            sb.AppendFormat("<td class=\"left\"></td>");
            sb.AppendFormat("<td class=\"left\"></td>");
            sb.AppendFormat("    <td class=\" \">");
            sb.AppendFormat("{0}", "<input type=\"hidden\" id=\"PHOTO_ID\"/>");
            sb.AppendFormat("{0}", "<input type=\"hidden\" id=\"PHOTOFILENAME\"/>");
            sb.AppendFormat("{0}", "<input type=\"hidden\" id=\"PRID\" value=\"" + PRID + "\"/>");
            sb.AppendFormat("{0}", "<input type=\"hidden\" id=\"PHOTOTYPE\" value=\"" + PHOTOTYPE + "\"/>");
            sb.AppendFormat("{0}", " <form id=\"uploadForm\" enctype=\"multipart/form-data\">");
            sb.AppendFormat("{0}", " <input type=\"file\" name=\"uploadify\" id=\"attachment\" style=\"width:180px;\">");
            bool bln = false;
            if (PHOTOTYPE == "DC_ARMY" && SystemCls.isRight("011002008") == true)
            {
                bln = true;
            }
            if (PHOTOTYPE == "DC_RESOURCE_NEW" && SystemCls.isRight("011003005") == true)
            {
                bln = true;
            }
            if (PHOTOTYPE == "DC_UTILITY_CAMP" && SystemCls.isRight("016001005") == true)
            {
                bln = true;
            }
            if (PHOTOTYPE == "DC_UTILITY_OVERWATCH" && SystemCls.isRight("016002008") == true)
            {
                bln = true;
            }
            if (PHOTOTYPE == "T_IPSFR_USER" && SystemCls.isRight("006004004") == true)
            {
                bln = true;
            }
            if (bln == true)
                sb.AppendFormat("{0}", "<input type=\"button\" id=\"btnPhotoupload\" value=\"新增\" onclick=\"UploadPhoto('Add')\" class=\"btnAddCss\" />");
            bool bln1 = false;
            if (PHOTOTYPE == "DC_ARMY" && SystemCls.isRight("011002009") == true)
            {
                bln1 = true;
            }
            if (PHOTOTYPE == "DC_RESOURCE_NEW" && SystemCls.isRight("011003006") == true)
            {
                bln1 = true;
            }
            if (PHOTOTYPE == "DC_UTILITY_CAMP" && SystemCls.isRight("016001006") == true)
            {
                bln1 = true;
            }
            if (PHOTOTYPE == "DC_UTILITY_OVERWATCH" && SystemCls.isRight("016002009") == true)
            {
                bln1 = true;
            }
            if (PHOTOTYPE == "T_IPSFR_USER" && SystemCls.isRight("006004005") == true)
            {
                bln1 = true;
            }
            if (bln1 == true)
                sb.AppendFormat("{0}", "<input type=\"button\" id=\"btnPhotoMdy\" value=\"修改\" style=\"display:none;\"  onclick=\"UploadPhoto('Mdy')\" class=\"btnMdyCss\" />");
            sb.AppendFormat("{0}", "<label id=\"lblInfo\" style=\"color:red;\"></label>");
            bool bln2 = false;
            if (PHOTOTYPE == "DC_ARMY" && SystemCls.isRight("011002010") == true)
            {
                bln2 = true;
            }
            if (PHOTOTYPE == "DC_RESOURCE_NEW" && SystemCls.isRight("011003007") == true)
            {
                bln2 = true;
            }
            if (PHOTOTYPE == "DC_UTILITY_CAMP" && SystemCls.isRight("016001007") == true)
            {
                bln2 = true;
            }
            if (PHOTOTYPE == "DC_UTILITY_OVERWATCH" && SystemCls.isRight("016002010") == true)
            {
                bln2 = true;
            }
            if (PHOTOTYPE == "T_IPSFR_USER" && SystemCls.isRight("006004006") == true)
            {
                bln2 = true;
            }
            if (bln2 == true)
                sb.AppendFormat("{0}", "<input type=\"button\" id=\"btnPhotoDel\" value=\"删除\" style=\"display:none;\"  onclick=\"ManagerPhoto('Del')\" class=\"btnDelCss\" />");
            sb.AppendFormat("{0}", " </form>");
            sb.AppendFormat("    </td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            //return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), "")), "text/html;charset=UTF-8"); ;
            ViewBag.photo = sb.ToString();
            return View();
        }
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadPhoto()
        {
            string PHOTO_ID = Request.Params["PHOTO_ID"];
            string PHOTOTITLE = Request.Params["PHOTOTITLE"];
            string PRID = Request.Params["PRID"];
            string PHOTOEXPLAIN = Request.Params["PHOTOEXPLAIN"];
            string PHOTOTYPE = Request.Params["PHOTOTYPE"];
            string method = Request.Params["Method"];
            Message ms = null;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string Path = "";
            string[] arr = hfc[0].FileName.Split('.');
            if (method == "Mdy" && string.IsNullOrEmpty(hfc[0].FileName))
            {
                DC_PHOTO_Model m = new DC_PHOTO_Model();
                m.opMethod = "MdyTP";
                m.PHOTOTIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now.ToString());
                m.PHOTOTITLE = PHOTOTITLE;
                m.PHOTOEXPLAIN = PHOTOEXPLAIN;
                m.PRID = PRID;
                m.PHOTO_ID = PHOTO_ID;
                m.PHOTOTYPE = PHOTOTYPE;
                ms = DC_PHOTOCls.Manager(m);
            }
            else
            {
                if (string.IsNullOrEmpty(hfc[0].FileName))
                    return Json(new Message(false, "请选择上传图片！", ""));
                if (arr[arr.Length - 1].ToLower() != "jpg" && arr[arr.Length - 1].ToLower() != "jpeg" && arr[arr.Length - 1].ToLower() != "bmp" && arr[arr.Length - 1].ToLower() != "gif" && arr[arr.Length - 1].ToLower() != "png")
                    return Json(new Message(false, "禁止上传非图片文件！", ""));

                if (hfc.Count > 0)
                {
                    string ipath = "~/UploadFile/DacPhoto/";//相对路径
                    string PhyPath = Server.MapPath(ipath);
                    if (!Directory.Exists(PhyPath))//判断文件夹是否已经存在
                    {
                        Directory.CreateDirectory(PhyPath);//创建文件夹
                    }
                    DC_PHOTO_Model m = new DC_PHOTO_Model();
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        m.PHOTOTIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now.ToString());
                        m.PHOTOFILENAME = hfc[i].FileName;
                        m.PHOTOTITLE = PHOTOTITLE;
                        m.PHOTOEXPLAIN = PHOTOEXPLAIN;
                        m.PRID = PRID;
                        m.PHOTO_ID = PHOTO_ID;
                        m.PHOTOTYPE = PHOTOTYPE;
                        //m.opMethod = "Add";
                        m.opMethod = method;
                        Path = "/UploadFile/DacPhoto/" + hfc[i].FileName;
                        string PhysicalPath = Server.MapPath(Path);
                        hfc[i].SaveAs(PhysicalPath);

                    }
                    ms = DC_PHOTOCls.Manager(m);
                }
            }
            //ms = new Message(true, , "");
            return Json(ms);
        }
        #endregion

        #region 档案管理（新）
        /// <summary>
        ///档案管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Archival_NEWIIndex()
        {
            pubViewBag("011006", "011006", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.FIREFROM = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "99", isShowAll = "1" });
            ViewBag.dl = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "7" });//地类
            ViewBag.lh = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "8" });//林火类别
            ViewBag.fuel = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "9" });//可燃物类别
            ViewBag.hot = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "10" });//热点类别
            ViewBag.yy = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "11" });//是否烟云
            ViewBag.YEAR = DateTime.Now.ToString("yyyy");
            return View();
        }
        /// <summary>
        /// 获取查看单条记录
        /// </summary>
        /// <returns></returns>
        public ActionResult GetArchivaljson()
        {
            string JCFID = Request.Params["JCFID"];
            return Content(JsonConvert.SerializeObject(JC_FIRECls.getModel(new JC_FIRE_SW { JCFID = JCFID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取火情查询列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getArchivallist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string orgno = Request.Params["BYORGNO"];
            //string firename = Request.Params["FIRENAME"];
            string firefrom = Request.Params["FIREFROM"];
            string YEAR = Request.Params["YEAR"];
            string fireTime = !string.IsNullOrEmpty(YEAR) ? Convert.ToDateTime(YEAR + "-01-01 00:00:00").ToString() : "";
            string fireEndTime = !string.IsNullOrEmpty(YEAR) ? Convert.ToDateTime(YEAR + "-12-31 23:59:59").ToString() : "";
            //DateTime startYear = new DateTime(int.Parse(YEAR), 1, 1);  //本年年初
            //DateTime endYear = new DateTime(int.Parse(YEAR), 12, 31).AddDays(1).AddSeconds(-1);  //本年年末
            //string fireTime = startYear.ToString();
            //string fireEndTime = endYear.ToString();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>单位</th>");
            sb.AppendFormat("<th>火情来源</th>");
            sb.AppendFormat("<th>起火时间</th>");
            sb.AppendFormat("<th>灭火时间</th>");
            sb.AppendFormat("<th>火灾发生地</th>");
            sb.AppendFormat("<th>火灾等级</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = JC_FIRECls.getModelList(new JC_FIRE_SW { BYORGNO = orgno, FIREFROM = firefrom, FIRETIME = fireTime, FIREENDTIME = fireEndTime, curPage = int.Parse(page), pageSize = int.Parse(PageSize), ISOUTFIRE = "1" }, out total);

            foreach (var s in result)
            {
                //if (i % 2 == 0)
                //    sb.AppendFormat("<tr>");
                //else
                sb.AppendFormat("<tr class='row1' onclick=\"setColor(this,'{0}')\">", s.JCFID);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.ORGNAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.FIREFROMName);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.FIRETIME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.FIREENDTIME);
                sb.AppendFormat("<td class=\"left  sorting_1\">{0}</td>", s.ZQWZ);//(T_SYS_ORGCls.getorgname(s.ZQWZ)) 
                //获取火灾等级的名称
                if (!string.IsNullOrEmpty(s.FIRELEVEL))
                {
                    string levelname = T_SYS_DICTCls.getDicName(new T_SYS_DICTSW { DICTTYPEID = "304", DICTVALUE = (s.FIRELEVEL) });
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", levelname);
                }
                else
                {
                    sb.AppendFormat("<td class=\"center  sorting_1\"></td>");
                }
                sb.AppendFormat("    <td class=\" \">");

                if (string.IsNullOrEmpty(s.JD) || string.IsNullOrEmpty(s.WD))
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                else
                    sb.AppendFormat("<a href=\"#\" onclick=\" Position('JC_FIRE','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.JCFID, s.FIRENAME);
                sb.AppendFormat("<a href=\"#\" onclick=\"TotalSee('{0}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.JCFID);
                //sb.AppendFormat("<a href=\"#\" onclick=\" Position('JC_FIRE','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.JCFID, s.FIRENAME);
                //sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.JCFID, "JC_FIRE");
                //if (SystemCls.isRight("011006002") == true)
                //sb.AppendFormat("<a href=\"#\" onclick=\"StateLogsLayer({0},{1})\" title='操作流程' class=\"searchBox_01 LinkStateLogsLayer\">操作流程</a>", s.JCFID, s.BYORGNO);
                ////if (SystemCls.isRight("011009002") == true)
                //sb.AppendFormat("<a href=\"#\" onclick=\"Feedback({0})\" title='反馈信息' class=\"searchBox_01 LinkFeedback\">反馈信息</a>", s.JCFID);
                //sb.AppendFormat("<a href=\"#\" onclick=\"FireReport({0})\" title='火情报告' class=\"searchBox_01 LinkFireReport\">火情报告</a>", s.JCFID);
                //sb.AppendFormat("<a href=\"#\" onclick=\"Manager('{0}','See1',{1})\" title='查看火情档案' class=\"searchBox_01 LinkFireReport\">查看火情档案</a>", s.FRFIID, s.JCFID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('{0}','Mdy','{1}')\" title='修改' class=\"searchBox_01 LinkFireReport\">修改</a>", s.FRFIID, s.JCFID);
                sb.AppendFormat("<a href=\"#\"onclick=\"Manager('{0}','Del','{1}')\" title='删除' class=\"searchBox_01 LinkFireReport\">删除</a>", s.FRFIID, s.JCFID);
                sb.AppendFormat("  </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8");

        }
        /// <summary>
        /// 获取最新的火情反馈信息
        /// </summary>
        /// <returns></returns>
        public ActionResult getFIRETICKLINGjson()
        {
            string JCFID = Request.Params["JCFID"];
            StringBuilder sb = new StringBuilder();
            var m = JC_FIRETICKLINGCls.getLatestfeedback(new JC_FIRETICKLING_SW { JCFID = JCFID });
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td style=\"width:15%\">单位:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ORGNAME);
            sb.AppendFormat("<td style=\"width:15%\">地类</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.DLName);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>林区名称:</td>");
            sb.AppendFormat("<td>{0}</td>", m.FORESTNAME);
            sb.AppendFormat("<td>林火类别:</td>");
            sb.AppendFormat("<td>{0}</td>", m.FORESTFIRETYPENAME);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>可燃物类型:</td>");
            sb.AppendFormat("<td>{0}</td>", m.FUELTYPEName);
            sb.AppendFormat("<td>热点类别:</td>");
            sb.AppendFormat("<td>{0}</td>", m.HOTTYPEName);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>过火面积:</td>");
            sb.AppendFormat("<td>{0}</td>", m.BURNEDAREA);
            sb.AppendFormat("<td>过火林地面积:</td>");
            sb.AppendFormat("<td>{0}</td>", m.OVERDOAREA);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>受害森林面积:</td>");
            sb.AppendFormat("<td>{0}</td>", m.LOSTFORESTAREA);
            sb.AppendFormat("<td>其他损失情况:</td>");
            sb.AppendFormat("<td>{0}</td>", m.ELSELOSSINTRO);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>情况简介:</td>");
            sb.AppendFormat("<td>{0}</td>", m.FIREINTRO);
            sb.AppendFormat("<td>地址:</td>");
            sb.AppendFormat("<td>{0}</td>", m.ADDRESS);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>经度:</td>");
            sb.AppendFormat("<td>{0}</td>", m.JD);
            sb.AppendFormat("<td>纬度:</td>");
            sb.AppendFormat("<td>{0}</td>", m.WD);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tbody>");
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取火情报告
        /// </summary>
        /// <returns></returns>
        public ActionResult getFIREReport()
        {
            string JCFID = Request.Params["JCFID"];
            StringBuilder sb = new StringBuilder();
            //sb.Append("<div class=\"divMan\" id=\"tablereport\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>火情报告名称</th>");
            sb.AppendFormat("<th>上传时间</th>");
            sb.AppendFormat("<th>上传人</th>");
            sb.AppendFormat("<th>上传单位</th>");
            sb.AppendFormat("<th>操作</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = JC_FIRE_REPORTCls.getModelList(new JC_FIRE_REPORT_SW { OWERJCFID = JCFID }).OrderByDescending(p => p.UPLOADTIME);
            if (result.Any())
            {
                foreach (var s in result)
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                    sb.AppendFormat("<td class=\"left\">{0}</td>", s.FILENAME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.UPLOADTIME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.UPLOANAME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", s.UPLOADORGNAME);
                    sb.AppendFormat("    <td class=\" \">");
                    sb.AppendFormat("&nbsp;<a href=\"{0}\" title='下载'>下载</a>", s.FILEURL.Replace('~', ' '));
                    sb.AppendFormat("|<a href=\"/OfficeView/Index?name={0}&url={1}\" target=\"_blank\" title='预览'>预览</a>", Server.UrlEncode(s.FILENAME.Trim()), s.FILEURL.Replace('~', ' '));
                    sb.AppendFormat("    </td>");
                    sb.AppendFormat("</tr>");
                    i++;
                }
            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan=\"6\"><em>暂无上传报告</em></td>");
                sb.AppendFormat("</tr>");
            }

            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            // sb.Append("</div>");
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), "")), "text/html;charset=UTF-8"); ;
        }
        #endregion

        #region 查看通用页面
        /// <summary>
        /// 查看通用页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SeeIndex()
        {
            string tablename = Request.Params["tablename"];
            string ID = Request.Params["ID"];
            StringBuilder sb = new StringBuilder();
            if (tablename == "DC_UTILITY_CAMP")//设施-营房查看
            {
                var m = DC_UTILITY_CAMPCls.getModel(new DC_UTILITY_CAMP_SW { DC_UTILITY_CAMP_ID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:15%\">单位:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ORGName);
                sb.AppendFormat("<td style=\"width:15%\">结构类型</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.STRUCTURETYPEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td >名称:</td>");
                sb.AppendFormat("<td >{0}</td>", m.NAME);
                sb.AppendFormat("<td >编号:</td>");
                sb.AppendFormat("<td >{0}</td>", m.NUMBER);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>面积:</td>");
                sb.AppendFormat("<td>{0}</td>", m.AREA);
                sb.AppendFormat("<td>楼层:</td>");
                sb.AppendFormat("<td>{0}</td>", m.FLOOR);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>附属设施:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.SUBFACILITIES);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>规划建设日期:</td>");
                sb.AppendFormat("<td>{0}</td>", m.BUILDDATEBEGIN + "--" + m.BUILDDATEEND);
                sb.AppendFormat("<td>建成日期:</td>");
                sb.AppendFormat("<td>{0}</td>", m.BUILDDATE);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.JD) ? "" : Convert.ToDouble(m.JD).ToString("f6"));
                sb.AppendFormat("<td>纬度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.WD) ? "" : Convert.ToDouble(m.WD).ToString("f6"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>总价:</td>");
                sb.AppendFormat("<td >{0}</td>", m.WORTH);
                sb.AppendFormat("<td>备注:</td>");
                sb.AppendFormat("<td >{0}</td>", m.MARK);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "DC_UTILITY_OVERWATCH")//设施-瞭望台查看
            {
                var m = DC_UTILITY_OVERWATCHCls.getModel(new DC_UTILITY_OVERWATCH_SW { DC_UTILITY_OVERWATCH_ID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td  style=\"width:15%\">单位:</td>");
                sb.AppendFormat("<td  style=\"width:35%\">{0}</td>", m.ORGName);
                sb.AppendFormat("<td  style=\"width:15%\">结构类型</td>");
                sb.AppendFormat("<td  style=\"width:35%\">{0}</td>", m.STRUCTURETYPEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>名称:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NAME);
                sb.AppendFormat("<td>编号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NUMBER);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>面积:</td>");
                sb.AppendFormat("<td>{0}</td>", m.AREA);
                sb.AppendFormat("<td>楼层:</td>");
                sb.AppendFormat("<td>{0}</td>", m.FLOOR);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>附属设施:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.SUBFACILITIES);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>规划建设日期:</td>");
                sb.AppendFormat("<td>{0}</td>", m.BUILDDATEBEGIN + "--" + m.BUILDDATEEND);
                sb.AppendFormat("<td>建成日期:</td>");
                sb.AppendFormat("<td>{0}</td>", m.BUILDDATE);
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.JD) ? "" : Convert.ToDouble(m.JD).ToString("f6"));
                sb.AppendFormat("<td>纬度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.WD) ? "" : Convert.ToDouble(m.WD).ToString("f6"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>总价:</td>");
                sb.AppendFormat("<td >{0}</td>", m.WORTH);
                sb.AppendFormat("<td>备注:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MARK);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "DC_UTILITY_ISOLATIONSTRIP")//设施-隔离带查看
            {
                var m = DC_UTILITY_ISOLATIONSTRIPCls.getModel(new DC_UTILITY_ISOLATIONSTRIP_SW { DC_UTILITY_ISOLATIONSTRIP_ID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:20%\">单位:</td>");
                sb.AppendFormat("<td style=\"width:30%\">{0}</td>", m.ORGName);
                sb.AppendFormat("<td style=\"width:20%\">隔离带类型:</td>");
                sb.AppendFormat("<td style=\"width:30%\">{0}</td>", m.ISOLATIONTYPEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>名称:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NAME);
                sb.AppendFormat("<td>编号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NUMBER);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>使用现状类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.USESTATEName);
                sb.AppendFormat("<td>维护管理类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MANAGERSTATEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>宽度:</td>");
                sb.AppendFormat("<td>{0}</td>", m.WIDTH);
                sb.AppendFormat("<td>长度:</td>");
                sb.AppendFormat("<td>{0}</td>", m.LENGTH);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>计划面积:</td>");
                sb.AppendFormat("<td>{0}</td>", m.PLANAREA);
                sb.AppendFormat("<td>实际面积:</td>");
                sb.AppendFormat("<td>{0}</td>", m.REALAREA);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经纬度集合:</td>");
                sb.AppendFormat("<td colspan=\"3\"><input type=\"text\" value=\"{0}\" style=\"width:350px\"/ readonly></td>", m.JWDLIST);
                sb.AppendFormat("</tr>");
                if (m.ISOLATIONTYPE == "5")
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>树种:</td>");
                    sb.AppendFormat("<td >{0}</td>", m.KINDTYPE);
                    sb.AppendFormat("<td>树种组成:</td>");
                    sb.AppendFormat("<td>{0}</td>", m.TREETYPEName);
                    sb.AppendFormat("</tr>");
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>步行通道宽度:</td>");
                    sb.AppendFormat("<td >{0}</td>", m.TREETYPEName);
                    sb.AppendFormat("<td>位置:</td>");
                    sb.AppendFormat("<td >{0}</td>", m.TREETYPEName);
                    sb.AppendFormat("</tr>");
                }
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>单价:</td>");
                sb.AppendFormat("<td >{0}</td>", m.Price);
                sb.AppendFormat("<td>总价:</td>");
                sb.AppendFormat("<td >{0}</td>", m.WORTH);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>规划建设日期:</td>");
                sb.AppendFormat("<td >{0}</td>", m.BUILDDATEBEGIN + "--" + m.BUILDDATEEND);
                sb.AppendFormat("<td>建成日期:</td>");
                sb.AppendFormat("<td >{0}</td>", m.BUILDDATE);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "DC_UTILITY_FIRECHANNEL")//设施-防火通道查看
            {
                var m = DC_UTILITY_FIRECHANNELCls.getModel(new DC_UTILITY_FIRECHANNEL_SW { DC_UTILITY_FIRECHANNEL_ID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:25%\">单位:</td>");
                sb.AppendFormat("<td style=\"width:25%\">{0}</td>", m.ORGName);
                sb.AppendFormat("<td style=\"width:25%\">长度:</td>");
                sb.AppendFormat("<td style=\"width:25%\">{0}</td>", m.LENGTH);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>名称:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NAME);
                sb.AppendFormat("<td>编号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NUMBER);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>使用现状类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.USESTATEName);
                sb.AppendFormat("<td>维护管理类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MANAGERSTATEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>防火通道等级类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.FIRECHANNELLEVELTYPEName);
                sb.AppendFormat("<td>防火通道使用性质:</td>");
                sb.AppendFormat("<td>{0}</td>", m.FIRECHANNELUSERTYPEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经纬度集合:</td>");
                sb.AppendFormat("<td colspan=\"3\"><input type=\"text\" value=\"{0}\" style=\"width:350px\"/ readonly></td>", m.JWDLIST);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>建成日期:</td>");
                sb.AppendFormat("<td>{0}</td>", m.BUILDDATE);
                sb.AppendFormat("<td>规划建设日期:</td>");
                sb.AppendFormat("<td >{0}</td>", m.BUILDDATEBEGIN + "--" + m.BUILDDATEEND);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>总价:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.WORTH);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "DC_UTILITY_PROPAGANDASTELE")//设施-宣传碑牌查看
            {
                var m = DC_UTILITY_PROPAGANDASTELECls.getModel(new DC_UTILITY_PROPAGANDASTELE_SW { DC_UTILITY_PROPAGANDASTELE_ID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:20%\">单位:</td>");
                sb.AppendFormat("<td style=\"width:30%\">{0}</td>", m.ORGName);
                sb.AppendFormat("<td style=\"width:20%\">宣传碑类型</td>");
                sb.AppendFormat("<td style=\"width:30%\">{0}</td>", m.PROPAGANDASTELETYPEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>名称:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NAME);
                sb.AppendFormat("<td>编号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NUMBER);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>使用现状类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.USESTATEName);
                sb.AppendFormat("<td>维护管理类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MANAGERSTATEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>结构类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.STRUCTURETYPEName);
                sb.AppendFormat("<td>地址:</td>");
                sb.AppendFormat("<td>{0}</td>", m.ADDRESS);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.JD) ? "" : Convert.ToDouble(m.JD).ToString("f6"));
                sb.AppendFormat("<td>纬度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.WD) ? "" : Convert.ToDouble(m.WD).ToString("f6"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>规划建设日期:</td>");
                sb.AppendFormat("<td >{0}</td>", m.BUILDDATEBEGIN + "--" + m.BUILDDATEEND);
                sb.AppendFormat("<td>建成日期:</td>");
                sb.AppendFormat("<td>{0}</td>", m.BUILDDATE);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>总价:</td>");
                sb.AppendFormat("<td >{0}</td>", m.WORTH);
                sb.AppendFormat("<td>备注:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MARK);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "DC_UTILITY_RELAY")//设施-中继站查看
            {
                var m = DC_UTILITY_RELAYCls.getModel(new DC_UTILITY_RELAY_SW { DC_UTILITY_RELAY_ID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:20%\">单位:</td>");
                sb.AppendFormat("<td style=\"width:30%\">{0}</td>", m.ORGName);
                sb.AppendFormat("<td style=\"width:20%\">通讯方式</td>");
                sb.AppendFormat("<td style=\"width:30%\">{0}</td>", m.COMMUNICATIONWAYName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>名称:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NAME);
                sb.AppendFormat("<td>编号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NUMBER);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>使用现状类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.USESTATEName);
                sb.AppendFormat("<td>维护管理类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MANAGERSTATEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>型号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MODEL);
                sb.AppendFormat("<td>地址:</td>");
                sb.AppendFormat("<td>{0}</td>", m.ADDRESS);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.JD) ? "" : Convert.ToDouble(m.JD).ToString("f6"));
                sb.AppendFormat("<td>纬度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.WD) ? "" : Convert.ToDouble(m.WD).ToString("f6"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>规划建设日期:</td>");
                sb.AppendFormat("<td >{0}</td>", m.BUILDDATEBEGIN + "--" + m.BUILDDATEEND);
                sb.AppendFormat("<td>建成日期:</td>");
                sb.AppendFormat("<td>{0}</td>", m.BUILDDATE);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>总价:</td>");
                sb.AppendFormat("<td >{0}</td>", m.WORTH);
                sb.AppendFormat("<td>备注:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MARK);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "DC_UTILITY_MONITORINGSTATION")//设施-监测站查看
            {
                var m = DC_UTILITY_MONITORINGSTATIONCls.getModel(new DC_UTILITY_MONITORINGSTATION_SW { DC_UTILITY_MONITORINGSTATION_ID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:20%\">单位:</td>");
                sb.AppendFormat("<td style=\"width:30%\">{0}</td>", m.ORGName);
                sb.AppendFormat("<td style=\"width:25%\">无线电传输方式</td>");
                sb.AppendFormat("<td style=\"width:25%\">{0}</td>", m.TRANSFERMODETYPEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>名称:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NAME);
                sb.AppendFormat("<td>编号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NUMBER);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>使用现状类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.USESTATEName);
                sb.AppendFormat("<td>维护管理类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MANAGERSTATEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>型号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MODEL);
                sb.AppendFormat("<td>地址:</td>");
                sb.AppendFormat("<td>{0}</td>", m.ADDRESS);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>监测内容:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MONICONTENT);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>规划建设日期:</td>");
                sb.AppendFormat("<td >{0}</td>", m.BUILDDATEBEGIN + "--" + m.BUILDDATEEND);
                sb.AppendFormat("<td>建成日期:</td>");
                sb.AppendFormat("<td>{0}</td>", m.BUILDDATE);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.JD) ? "" : Convert.ToDouble(m.JD).ToString("f6"));
                sb.AppendFormat("<td>纬度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.WD) ? "" : Convert.ToDouble(m.WD).ToString("f6"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>总价:</td>");
                sb.AppendFormat("<td >{0}</td>", m.WORTH);
                sb.AppendFormat("<td>备注:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MARK);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "DC_UTILITY_FACTORCOLLECTSTATION")//设施-因子采集站查看
            {
                var m = DC_UTILITY_FACTORCOLLECTSTATIONCls.getModel(new DC_UTILITY_FACTORCOLLECTSTATION_SW { DC_UTILITY_FACTORCOLLECTSTATION_ID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:20%\">单位:</td>");
                sb.AppendFormat("<td style=\"width:30%\">{0}</td>", m.ORGName);
                sb.AppendFormat("<td style=\"width:25%\">无线电传输方式</td>");
                sb.AppendFormat("<td style=\"width:25%\">{0}</td>", m.TRANSFERMODETYPEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>名称:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NAME);
                sb.AppendFormat("<td>编号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NUMBER);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>使用现状类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.USESTATEName);
                sb.AppendFormat("<td>维护管理类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MANAGERSTATEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>型号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MODEL);
                sb.AppendFormat("<td>地址:</td>");
                sb.AppendFormat("<td>{0}</td>", m.ADDRESS);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>采集内容:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.FACTCOLLCONTENT);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>规划建设日期:</td>");
                sb.AppendFormat("<td >{0}</td>", m.BUILDDATEBEGIN + "--" + m.BUILDDATEEND);
                sb.AppendFormat("<td>建成日期:</td>");
                sb.AppendFormat("<td>{0}</td>", m.BUILDDATE);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<td>经度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.JD) ? "" : Convert.ToDouble(m.JD).ToString("f6"));
                sb.AppendFormat("<td>纬度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.WD) ? "" : Convert.ToDouble(m.WD).ToString("f6"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>总价:</td>");
                sb.AppendFormat("<td >{0}</td>", m.WORTH);
                sb.AppendFormat("<td>备注:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MARK);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "DC_ARMY")//队伍查看
            {
                var m = DC_ARMYCls.getModel(new DC_ARMY_SW { DC_ARMY_ID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:15%\">单位:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ORGName);
                sb.AppendFormat("<td style=\"width:15%\">队伍类型</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ARMYTYPEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>名称:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NAME);
                sb.AppendFormat("<td>编号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NUMBER);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>人数:</td>");
                sb.AppendFormat("<td>{0}</td>", m.ARMYMEMBERCOUNT);
                sb.AppendFormat("<td>队长:</td>");
                sb.AppendFormat("<td>{0}</td>", m.ARMYLEADER);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>联系方式:</td>");
                sb.AppendFormat("<td>{0}</td>", m.CONTACTS);
                sb.AppendFormat("<td>队伍特点:</td>");
                sb.AppendFormat("<td>{0}</td>", m.ARMYCHARACTER);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<td>经度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.JD) ? "" : Convert.ToDouble(m.JD).ToString("f6"));
                sb.AppendFormat("<td>纬度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.WD) ? "" : Convert.ToDouble(m.WD).ToString("f6"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td >备注:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "DC_RESOURCE_NEW")//资源查看
            {
                var m = DC_RESOURCE_NEWCls.getModel(new DC_RESOURCE_NEW_SW { DC_RESOURCE_NEW_ID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:18%\">单位:</td>");
                sb.AppendFormat("<td style=\"width:32%\">{0}</td>", m.ORGNOSName);
                sb.AppendFormat("<td style=\"width:15%\">资源类型</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.RESOURCETYPEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>名称:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NAME);
                sb.AppendFormat("<td>编号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NUMBER);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>林龄类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.AGETYPEName);
                sb.AppendFormat("<td>起源类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.ORIGINTYPEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>可燃类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.BURNTYPEName);
                sb.AppendFormat("<td>林木类型:</td>");
                sb.AppendFormat("<td>{0}</td>", m.TREETYPEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>树种:</td>");
                sb.AppendFormat("<td>{0}</td>", m.KINDTYPE);
                sb.AppendFormat("<td>面积:</td>");
                sb.AppendFormat("<td>{0}</td>", m.AREA);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>坡向:</td>");
                sb.AppendFormat("<td>{0}</td>", m.ASPECT);
                sb.AppendFormat("<td>坡度:</td>");
                sb.AppendFormat("<td>{0}</td>", m.ANGLE);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经纬度集合:</td>");
                sb.AppendFormat("<td colspan=\"3\"><input type=\"text\" value=\"{0}\" style=\"width:350px\"/ readonly></td>", m.JWDLIST);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>挂钩领导:</td>");
                sb.AppendFormat("<td>{0}</td>", m.POTHOOKLEADER);
                sb.AppendFormat("<td>职务:</td>");
                sb.AppendFormat("<td>{0}</td>", m.POTHOOKLEADERJOB);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>领导联系电话:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.POTHOOKLEADERTLEE);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>责任人:</td>");
                sb.AppendFormat("<td>{0}</td>", m.DUTYPERSON);
                sb.AppendFormat("<td>责任人联系电话:</td>");
                sb.AppendFormat("<td>{0}</td>", m.DUTYPERSONTELL);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>备注:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "DC_EQUIP_NEW")//装备查看
            {
                var m = DC_EQUIP_NEWCls.getModel(new DC_EQUIP_NEW_SW { DC_EQUIP_NEW_ID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:15%\">单位:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ORGName);
                sb.AppendFormat("<td style=\"width:15%\">装备类型</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.EQUIPTYPEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>名称:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NAME);
                sb.AppendFormat("<td>编号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NUMBER);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>型号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MODEL);
                sb.AppendFormat("<td>使用现状:</td>");
                sb.AppendFormat("<td>{0}</td>", m.USESTATEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>购买年份:</td>");
                sb.AppendFormat("<td>{0}</td>", m.BUYYEAR);
                sb.AppendFormat("<td>储存地点:</td>");
                sb.AppendFormat("<td>{0}</td>", m.STOREADDR);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.JD) ? "" : Convert.ToDouble(m.JD).ToString("f6"));
                sb.AppendFormat("<td>纬度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.WD) ? "" : Convert.ToDouble(m.WD).ToString("f6"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>数量:</td>");
                sb.AppendFormat("<td>{0}</td>", m.EQUIPAMOUNT);
                sb.AppendFormat("<td>单价:</td>");
                sb.AppendFormat("<td>{0}</td>", m.PRICE);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>备注:</td>");
                sb.AppendFormat("<td>{0}</td>", m.MARK);
                sb.AppendFormat("<td>总价:</td>");
                sb.AppendFormat("<td>{0}</td>", m.WORTH);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "DC_CAR")//车辆查看
            {
                var m = DC_CARCls.getModel(new DC_CAR_SW { DC_CAR_ID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:15%\">单位:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ORGName);
                sb.AppendFormat("<td style=\"width:15%\">车辆类型</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.CARTYPEName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>名称:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NAME);
                sb.AppendFormat("<td>编号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.NUMBER);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>号牌:</td>");
                sb.AppendFormat("<td>{0}</td>", m.PLATENUM);
                sb.AppendFormat("<td>储存地点:</td>");
                sb.AppendFormat("<td>{0}</td>", m.STOREADDR);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>购买年份:</td>");
                sb.AppendFormat("<td>{0}</td>", m.BUYYEAR);
                sb.AppendFormat("<td>购买价格:</td>");
                sb.AppendFormat("<td>{0}</td>", m.BUYPRICE);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>驾驶员:</td>");
                sb.AppendFormat("<td>{0}</td>", m.DRIVER);
                sb.AppendFormat("<td>联系方式:</td>");
                sb.AppendFormat("<td>{0}</td>", m.CONTACTS);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>GPS设备型号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.GPSEQUIP);
                sb.AppendFormat("<td>GPS号码:</td>");
                sb.AppendFormat("<td>{0}</td>", m.GPSTELL);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.JD) ? "" : Convert.ToDouble(m.JD).ToString("f6"));
                sb.AppendFormat("<td>纬度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.WD) ? "" : Convert.ToDouble(m.WD).ToString("f6"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>备注:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "JC_FIRE")//档案查看
            {
                var m = JC_FIRECls.getModel(new JC_FIRE_SW { JCFID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:15%\">单位:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ORGNAME);
                sb.AppendFormat("<td style=\"width:15%\">火灾发生地</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ZQWZ);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>火情名称:</td>");
                sb.AppendFormat("<td>{0}</td>", m.FIRENAME);
                sb.AppendFormat("<td>火情来源:</td>");
                sb.AppendFormat("<td>{0}</td>", m.FIREFROMName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.JD) ? "" : Convert.ToDouble(m.JD).ToString("f6"));
                sb.AppendFormat("<td>纬度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.WD) ? "" : Convert.ToDouble(m.WD).ToString("f6"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>卫星编号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.WXBH);
                sb.AppendFormat("<td>热点编号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.DQRDBH);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>面积:</td>");
                sb.AppendFormat("<td>{0}</td>", m.RSMJ);
                sb.AppendFormat("<td>地类:</td>");
                sb.AppendFormat("<td>{0}</td>", m.DL);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>派发人:</td>");
                sb.AppendFormat("<td>{0}</td>", m.PFUSERID);
                sb.AppendFormat("<td>派发单位:</td>");
                sb.AppendFormat("<td>{0}</td>", m.PFORGNOName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>起火时间:</td>");
                sb.AppendFormat("<td>{0}</td>", m.FIRETIME);
                sb.AppendFormat("<td>灭火时间:</td>");
                sb.AppendFormat("<td>{0}</td>", m.FIREENDTIME);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>接收时间:</td>");
                sb.AppendFormat("<td>{0}</td>", m.RECEIVETIME);
                sb.AppendFormat("<td>下发时间:</td>");
                sb.AppendFormat("<td>{0}</td>", m.ISSUEDTIME);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>备注:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "DCREPOSITORY")//仓库查看
            {
                var m = DC_REPOSITORYCls.getModel(new DC_REPOSITORY_SW { DCREPOSITORYID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:15%\">单位:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ORGName);
                sb.AppendFormat("<td style=\"width:15%\">名称</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.NAME);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>责任人:</td>");
                sb.AppendFormat("<td>{0}</td>", m.RESPONSIBLEMAN);
                sb.AppendFormat("<td>联系方式:</td>");
                sb.AppendFormat("<td>{0}</td>", m.LINKWAY);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.JD) ? "" : Convert.ToDouble(m.JD).ToString("f6"));
                sb.AppendFormat("<td>纬度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.WD) ? "" : Convert.ToDouble(m.WD).ToString("f6"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>地址:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.ADDRESS);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "TD_POINTMARK")//自然村查看
            {
                var m = TD_POINTMARKCls.getModel(new TD_POINTMARK_SW { OBJECTID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:15%\">所属县市:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ORGXSNAME);
                sb.AppendFormat("<td style=\"width:15%\">所属乡镇</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ORGNAME);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>名称:</td>");
                sb.AppendFormat("<td >{0}</td>", m.NAME);
                sb.AppendFormat("<td>所属地类:</td>");
                sb.AppendFormat("<td >{0}</td>", m.TYPENAME);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>所属自然村:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.VILLAGE);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.JD) ? "" : Convert.ToDouble(m.JD).ToString("f6"));
                sb.AppendFormat("<td>纬度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.WD) ? "" : Convert.ToDouble(m.WD).ToString("f6"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "TD_MOUNTAIN")//山查看
            {
                var m = TD_MOUNTAINCls.getModel(new TD_MOUNTAIN_SW { OBJECTID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:15%\">所属县市:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ORGXSNAME);
                sb.AppendFormat("<td style=\"width:15%\">所属乡镇</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.ORGNAME);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>名称:</td>");
                sb.AppendFormat("<td >{0}</td>", m.NAME);
                sb.AppendFormat("<td>所属地类:</td>");
                sb.AppendFormat("<td >{0}</td>", m.TYPENAME);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>所属自然村:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.VILLAGE);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.JD) ? "" : Convert.ToDouble(m.JD).ToString("f6"));
                sb.AppendFormat("<td>纬度:</td>");
                sb.AppendFormat("<td>{0}</td>", string.IsNullOrEmpty(m.WD) ? "" : Convert.ToDouble(m.WD).ToString("f6"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            if (tablename == "DC_DETAILS")//物资明细查看
            {
                var m = DC_DETAILSCls.getModel(new DC_DETAILS_SW { DCDETAILSID = ID });
                sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td style=\"width:15%\">仓库:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.DPNAME);
                sb.AppendFormat("<td style=\"width:15%\">物资</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.SUPNAME);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>型号:</td>");
                sb.AppendFormat("<td>{0}</td>", m.DCSUPPROPMODEL);
                sb.AppendFormat("<td>出入库:</td>");
                if (m.DCREPFLAG == "0")
                {
                    sb.AppendFormat("<td>{0}</td>", "入库");
                }
                if (m.DCREPFLAG == "1")
                {
                    sb.AppendFormat("<td>{0}</td>", "出库");
                }
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>时间:</td>");
                sb.AppendFormat("<td>{0}</td>", m.DCREPTIME);
                sb.AppendFormat("<td>数量:</td>");
                sb.AppendFormat("<td>{0}</td>", m.DCREPSUPCOUNT);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>单价:</td>");
                sb.AppendFormat("<td>{0}</td>", m.PRICE);
                sb.AppendFormat("<td>金额:</td>");
                sb.AppendFormat("<td>{0}</td>", m.SUM);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>制单人:</td>");
                sb.AppendFormat("<td>{0}</td>", m.DCZHIBIAOREN);
                sb.AppendFormat("<td>发放人:</td>");
                if (m.DCREPFLAG == "1")
                {
                    sb.AppendFormat("<td>{0}</td>", m.DCFAFANGREN);
                }
                if (m.DCREPFLAG == "0")
                {
                    sb.AppendFormat("<td>{0}</td>", "--");
                }
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td>经办人:</td>");
                sb.AppendFormat("<td>{0}</td>", m.DCUSERID);
                sb.AppendFormat("<td>调入单位:</td>");
                if (m.DCREPFLAG == "1")
                {
                    sb.AppendFormat("<td>{0}</td>", m.DCUSERORG);
                }
                if (m.DCREPFLAG == "0")
                {
                    sb.AppendFormat("<td>{0}</td>", "--");
                }
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tbody>");
                sb.AppendFormat("</div>");
            }
            ViewBag.See = sb.ToString();
            return View();
        }
        #endregion

        #region 仓库（新）
        /// <summary>
        /// 物资属性管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SUPPLIESPROPManager()
        {
            DC_SUPPLIESPROP_Model m = new DC_SUPPLIESPROP_Model();
            m.DC_SUPPLIESPROP_ID = Request.Params["DC_SUPPLIESPROP_ID"];
            m.DCSUPPROPNAME = Request.Params["DCSUPPROPNAME"];
            m.DCSUPPROPMODEL = Request.Params["DCSUPPROPMODEL"];
            m.DCSUPPROPUNIT = Request.Params["DCSUPPROPUNIT"];
            m.DCSUPPROPFACTORY = Request.Params["DCSUPPROPFACTORY"];
            if (string.IsNullOrEmpty(m.DCSUPPROPNAME))
                return Content(JsonConvert.SerializeObject(new Message(false, "物资名称不可为空，请重新输入！", "")), "text/html;charset=UTF-8");
            m.MARK = Request.Params["MARK"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(DC_SUPPLIESPROPCls.Manager(m)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 物资属性管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Suppliesproplist()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style='width:7%;'>序号</th>");
            sb.AppendFormat("<th style='width:13%;'>物资名称</th>");
            sb.AppendFormat("<th style='width:10%;'>物资型号</th>");
            sb.AppendFormat("<th style='width:10%;'>物资单位</th>");
            sb.AppendFormat("<th style='width:10%;'>物资厂家</th>");
            sb.AppendFormat("<th style='width:10%;'>备注</th>");
            sb.AppendFormat("<th style='width:40%;'></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = DC_SUPPLIESPROPCls.getModelList(new DC_SUPPLIESPROP_SW { });
            foreach (var s in result)
            {
                sb.AppendFormat("<tr class='{6}' onclick=\"SUPOnclik('{0}','{1}','{2}','{3}','{4}','{5}')\">", s.DC_SUPPLIESPROP_ID, s.DCSUPPROPNAME, s.DCSUPPROPMODEL, s.DCSUPPROPUNIT, s.DCSUPPROPFACTORY, s.MARK, (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.DCSUPPROPNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.DCSUPPROPMODEL);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.DCSUPPROPUNIT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.DCSUPPROPFACTORY);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.MARK);
                sb.AppendFormat("    <td class=\" \">");
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            if (i % 2 == 0)
                sb.AppendFormat("<tr>");
            else
                sb.AppendFormat("<tr class='row1'>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"DCSUPPROPNAME\"/>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"DCSUPPROPMODEL\"/>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"DCSUPPROPUNIT\"/>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"DCSUPPROPFACTORY\"/>");
            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" id=\"MARK\"/>");
            sb.AppendFormat("    <td class=\" \">");
            sb.AppendFormat("{0}", "<input type=\"hidden\" id=\"DC_SUPPLIESPROP_ID\"/>");
            if (SystemCls.isRight("011008007") == true)
                sb.AppendFormat("{0}", "<input type=\"button\" value=\"添加\" onclick=\"SUPPLIESPROPManager('Add')\" class=\"btnAddCss\"/>");
            if (SystemCls.isRight("011008008") == true)
                sb.AppendFormat("&nbsp;{0}", "<input type=\"button\" id=\"btnPROPMdy\" style=\"display:none;\" value=\"修改\" onclick=\"SUPPLIESPROPManager('Mdy')\" class=\"btnMdyCss\" />");
            if (SystemCls.isRight("011008009") == true)
                sb.AppendFormat("&nbsp;{0}", "<input type=\"button\" id=\"btnPROPDel\" style=\"display:none;\"  value=\"删除\" onclick=\"SUPPLIESPROPManager('Del')\" class=\"btnDelCss\"  />");
            sb.AppendFormat("    </td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 入库时获取物资+型号的combox
        /// </summary>
        /// <returns></returns>
        public string SupMOCheckJson()
        {
            var str = DC_SUPPLIESPROPCls.getSupMOJsonStr(new DC_SUPPLIESPROP_SW { });
            return str;
        }
        /// <summary>
        /// 出库时获取物资+型号的combox
        /// </summary>
        /// <returns></returns>
        public string SupCKCheckJson()
        {
            string repid = Request.Params["REPID"];
            string type = Request.Params["type"];
            var str = DC_SUPPLIESCls.getSupCKJsonStr(new DC_SUPPLIES_SW { REPID = repid }, type);
            return str;
        }
        /// <summary>
        /// 出库获取厂家和型号
        /// </summary>
        /// <returns></returns>
        public ActionResult getFac()
        {
            string id = Request.Params["id"];
            return Content(JsonConvert.SerializeObject(DC_DETAILSCls.getModel(new DC_DETAILS_SW { DCDETAILSID = id })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 出入库获取厂家和型号
        /// </summary>
        /// <returns></returns>
        public ActionResult getrkFac()
        {
            string id = Request.Params["id"];
            return Content(JsonConvert.SerializeObject(DC_SUPPLIESPROPCls.getModel(new DC_SUPPLIESPROP_SW { DC_SUPPLIESPROP_ID = id })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 出库界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportIndex()
        {
            string repid = Request.Params["REPID"];
            ViewBag.repid = repid;
            string name = Request.Params["Name"];
            ViewBag.name = name;
            ViewBag.depotman = DC_REPOSITORYCls.getdepotman(repid);
            ViewBag.number = DC_DETAILSCls.getnumber(DC_DETAILSCls.swDate(DateTime.Now.ToString()));//编号
            ViewBag.DCREPTIME = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now.ToString());//当前日期
            string ckid = Request.Params["ckid"];
            ViewBag.ID = ckid;
            return View();
        }
        public ActionResult InportIndex()
        {
            string repid = Request.Params["REPID"];
            ViewBag.repid = repid;
            string name = Request.Params["Name"];
            ViewBag.name = name;
            ViewBag.depotman = DC_REPOSITORYCls.getdepotman(repid);///仓库负责人
            ViewBag.suppliename = DC_SUPPLIESPROPCls.getnameSelectOption(new DC_SUPPLIESPROP_SW { isShowAll = "1" });
            ViewBag.user = T_IPSFR_USERCls.getuser(new T_SYSSEC_IPSUSER_SW { });
            ViewBag.number = DC_DETAILSCls.getnumber(DC_DETAILSCls.swDate(DateTime.Now.ToString()));//编号
            //ViewBag.PREPARED = T_SYSSEC_IPSUSERCls.getname(SystemCls.getUserID());//当前用户
            ViewBag.DCREPTIME = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now.ToString());//当前日期
            ViewBag.Unit = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "50" });
            return View();
        }
        /// <summary>
        /// 出库打印界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Print()
        {
            string number = Request.Params["number"];
            StringBuilder sb = new StringBuilder();
            var ss = DC_DETAILSCls.getModel(new DC_DETAILS_SW { NUMBER = number });
            var orgno = DC_REPOSITORYCls.getdepOrg(ss.REPID);
            var commandname = T_SYS_ORGCls.getComandname(orgno);
            sb.AppendFormat("<div id =\"center\" style=\"text-align: center;\">");
            sb.AppendFormat("<p style=\"text-align: center;\">{0}</p>", commandname + "物资调拨单");
            sb.AppendFormat("<table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"650\" height=\"50\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"center\" style=\"width:25%\">{0}</td>", "日期：");
            sb.AppendFormat("<td class=\"center\" style=\"width:25%\">{0}</td>", ss.DCREPTIME);
            sb.AppendFormat("<td class=\"center\" style=\"width:25%\">{0}</td>", "编号：");
            sb.AppendFormat("<td class=\"center\" style=\"width:25%\">{0}</td>", ss.NUMBER);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"center\" style=\"width:25%\">{0}</td>", "调出单位：");
            sb.AppendFormat("<td class=\"center\" style=\"width:25%\">{0}</td>", ss.DPNAME);
            sb.AppendFormat("<td class=\"center\" style=\"width:25%\">{0}</td>", "调入单位：");
            sb.AppendFormat("<td class=\"center\" style=\"width:25%\">{0}</td>", ss.DCUSERORG);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("<table  align=\"center\"  cellpadding=\"0\" cellspacing=\"0\" border=\"1\" width=\"650\" height=\"280\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style=\"width:10%\">序号</th>");
            sb.AppendFormat("<th style=\"width:20%\">物资名称</th>");
            sb.AppendFormat("<th style=\"width:20%\">型号</th>");
            sb.AppendFormat("<th style=\"width:10%\">单位</th>");
            sb.AppendFormat("<th style=\"width:10%\">数量</th>");
            sb.AppendFormat("<th style=\"width:10%\">单价</th>");
            sb.AppendFormat("<th style=\"width:10%\">金额</th>");
            sb.AppendFormat("<th style=\"width:10%\">备注</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = DC_DETAILSCls.getModelList(new DC_DETAILS_SW { NUMBER = number });
            float sumtotal = 0;
            float dccount = 0;
            foreach (var s in result)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\" >{0}</td>", s.SUPNAME);
                sb.AppendFormat("<td class=\"center\" >{0}</td>", s.DCSUPPROPMODEL);
                sb.AppendFormat("<td class=\"center\" >{0}</td>", s.DCSUPPROPUNIT);
                sb.AppendFormat("<td class=\"center\" >{0}</td>", s.DCREPSUPCOUNT);
                sb.AppendFormat("<td class=\"center\" >{0}</td>", s.PRICE);
                sb.AppendFormat("<td class=\"center\" >{0}</td>", s.SUM);
                if (i == 0)
                {
                    sb.AppendFormat("<td class=\"center\" rowspan=\"7\">{0}</td>", s.MARK);
                }
                ++i;
                sb.AppendFormat("</tr>");
                sumtotal += float.Parse(s.SUM);
                dccount += float.Parse(s.DCREPSUPCOUNT);
            }
            for (int k = 0; k < 6 - i; k++)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", (k + i + 1).ToString());
                sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"center\" colspan=\"3\">{0}</td>", "合计");
            sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
            sb.AppendFormat("<td class=\"center\" >{0}</td>", dccount);
            sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
            sb.AppendFormat("<td class=\"center\" >{0}</td>", sumtotal.ToString("F2"));
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("<table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"650\" height=\"50\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td  style=\"width:25%\">主管:&nbsp&nbsp{0}</td>", ss.RESPONSIBLEMAN);
            sb.AppendFormat("<td  style=\"width:25%\">制单:&nbsp&nbsp{0}</td>", ss.DCZHIBIAOREN);
            sb.AppendFormat("<td  style=\"width:20%\">发放:&nbsp&nbsp{0}</td>", ss.DCFAFANGREN);
            sb.AppendFormat("<td  style=\"width:30%\">调入单位经办人:{0}</td>", ss.DCUSERID);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.printexport = sb.ToString();
            return View();
        }
        /// <summary>
        /// 入库打印界面
        /// </summary>
        /// <returns></returns>
        public ActionResult INPrint()
        {
            string number = Request.Params["number"];
            StringBuilder sb = new StringBuilder();
            var ss = DC_DETAILSCls.getModel(new DC_DETAILS_SW { NUMBER = number });
            //var orgno = DC_REPOSITORYCls.getdepOrg(ss.REPID);
            //var commandname = T_SYS_ORGCls.getComandname(orgno);
            sb.AppendFormat("<div id =\"center\" style=\"text-align: center;\">");
            sb.AppendFormat("<p style=\"text-align: center;\">{0}</p>", ss.DPNAME + "仓库物资入库单");

            sb.AppendFormat("<table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"650\" height=\"50\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"center\" style=\"width:25%\">{0}</td>", "日期：");
            sb.AppendFormat("<td class=\"center\" style=\"width:25%\">{0}</td>", ss.DCREPTIME);
            sb.AppendFormat("<td class=\"center\" style=\"width:25%\">{0}</td>", "编号：");
            sb.AppendFormat("<td class=\"center\" style=\"width:25%\">{0}</td>", ss.NUMBER);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("<table  align=\"center\"  cellpadding=\"0\" cellspacing=\"0\" border=\"1\" width=\"650\" height=\"280\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style=\"width:10%\">序号</th>");
            sb.AppendFormat("<th style=\"width:20%\">物资名称</th>");
            sb.AppendFormat("<th style=\"width:20%\">型号</th>");
            sb.AppendFormat("<th style=\"width:10%\">单位</th>");
            sb.AppendFormat("<th style=\"width:10%\">数量</th>");
            sb.AppendFormat("<th style=\"width:10%\">单价</th>");
            sb.AppendFormat("<th style=\"width:10%\">金额</th>");
            sb.AppendFormat("<th style=\"width:10%\">备注</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = DC_DETAILSCls.getModelList(new DC_DETAILS_SW { NUMBER = number });
            float sumtotal = 0;
            float dccount = 0;
            foreach (var s in result)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\" >{0}</td>", s.SUPNAME);
                sb.AppendFormat("<td class=\"center\" >{0}</td>", s.DCSUPPROPMODEL);
                sb.AppendFormat("<td class=\"center\" >{0}</td>", s.DCSUPPROPUNIT);
                sb.AppendFormat("<td class=\"center\" >{0}</td>", s.DCREPSUPCOUNT);
                sb.AppendFormat("<td class=\"center\" >{0}</td>", s.PRICE);
                sb.AppendFormat("<td class=\"center\" >{0}</td>", s.SUM);
                if (i == 0)
                {
                    sb.AppendFormat("<td class=\"center\" rowspan=\"7\">{0}</td>", s.MARK);
                }
                ++i;
                sb.AppendFormat("</tr>");
                sumtotal += float.Parse(s.SUM);
                dccount += float.Parse(s.DCREPSUPCOUNT);
            }
            for (int k = 0; k < 6 - i; k++)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", (k + i + 1).ToString());
                sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
                sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"center\" colspan=\"3\">{0}</td>", "合计");
            sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
            sb.AppendFormat("<td class=\"center\" >{0}</td>", dccount);
            sb.AppendFormat("<td class=\"center\" >{0}</td>", "");
            sb.AppendFormat("<td class=\"center\" >{0}</td>", sumtotal.ToString("F2"));
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("<table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"650\" height=\"50\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td  style=\"width:25%\">主管:&nbsp&nbsp{0}</td>", ss.RESPONSIBLEMAN);
            sb.AppendFormat("<td  style=\"width:25%\">制单:&nbsp&nbsp{0}</td>", ss.DCZHIBIAOREN);
            sb.AppendFormat("<td  style=\"width:20%\">保管人:&nbsp&nbsp{0}</td>", ss.DCCUSTODIANID);
            sb.AppendFormat("<td  style=\"width:30%\">经办人:{0}</td>", ss.DCUSERID);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.printinport = sb.ToString();
            return View();
        }
        #endregion

        #region 地图数据标记
        /// <summary>
        /// 山数据管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MountainIndex()
        {
            pubViewBag("011011", "011011", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.isAdd = (SystemCls.isRight("011011002")) ? "1" : "0";
            ViewBag.type = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "51", isShowAll = "1" });
            ViewBag.typeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "51" });
            return View();
        }
        /// <summary>
        /// 获取查看和修改前的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMountainjson()
        {
            string OBJECTID = Request.Params["OBJECTID"];
            //if (string.IsNullOrEmpty(ID))
            //    ID = "0";
            return Content(JsonConvert.SerializeObject(TD_MOUNTAINCls.getModel(new TD_MOUNTAIN_SW { OBJECTID = OBJECTID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 山数据管理
        /// </summary>
        /// <returns></returns>
        public ActionResult MountainManager()
        {
            string TYPE = Request.Params["TYPE"];
            string TYPE1 = Request.Params["TYPE1"];
            string method = Request.Params["Method"];
            Message ms = null;
            TD_MOUNTAIN_Model m = new TD_MOUNTAIN_Model();
            TD_POINTMARK_Model m1 = new TD_POINTMARK_Model();
            if (method != "Mdy")
            {
                if (method == "Del")
                {
                    if (TYPE1 != "1")
                    {
                        m.OBJECTID = Request.Params["OBJECTID"];
                        m.opMethod = "Del";
                        ms = TD_MOUNTAINCls.Manager(m);
                    }
                    else
                    {
                        m1.OBJECTID = Request.Params["OBJECTID"];
                        m1.opMethod = "Del";
                        ms = TD_POINTMARKCls.Manager(m1);
                    }
                }
                else
                {
                    if (TYPE != "1")
                    {
                        m.TYPE = TYPE;
                        m.OBJECTID = Request.Params["OBJECTID"];
                        m.BYORGNO = Request.Params["BYORGNO"];
                        m.BYORGNOXS = Request.Params["BYORGNOXS"];
                        m.NAME = Request.Params["NAME"];
                        m.VILLAGE = Request.Params["VILLAGE"];
                        m.JD = Request.Params["JD"];
                        m.WD = Request.Params["WD"];
                        m.Shape = "geometry::STGeomFromText('POINT(" + m.JD + " " + m.WD + ")',4326)";
                        m.opMethod = Request.Params["Method"];
                        ms = TD_MOUNTAINCls.Manager(m);
                    }
                    else
                    {
                        m1.OBJECTID = Request.Params["OBJECTID"];
                        m1.TYPE = TYPE;
                        m1.NAME = Request.Params["NAME"];
                        m1.BYORGNO = Request.Params["BYORGNO"];
                        m1.BYORGNOXS = Request.Params["BYORGNOXS"];
                        m1.VILLAGE = Request.Params["VILLAGE"];
                        m1.JD = Request.Params["JD"];
                        m1.WD = Request.Params["WD"];
                        m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                        m1.opMethod = Request.Params["Method"];
                        ms = TD_POINTMARKCls.Manager(m1);
                    }
                }
            }
            else
            {
                if (TYPE == TYPE1)
                {
                    if (TYPE != "1")
                    {
                        m.TYPE = TYPE;
                        m.OBJECTID = Request.Params["OBJECTID"];
                        m.BYORGNO = Request.Params["BYORGNO"];
                        m.BYORGNOXS = Request.Params["BYORGNOXS"];
                        m.NAME = Request.Params["NAME"];
                        m.VILLAGE = Request.Params["VILLAGE"];
                        m.JD = Request.Params["JD"];
                        m.WD = Request.Params["WD"];
                        m.Shape = "geometry::STGeomFromText('POINT(" + m.JD + " " + m.WD + ")',4326)";
                        m.opMethod = Request.Params["Method"];
                        ms = TD_MOUNTAINCls.Manager(m);
                    }
                    else
                    {
                        m1.OBJECTID = Request.Params["OBJECTID"];
                        m1.TYPE = TYPE;
                        m1.NAME = Request.Params["NAME"];
                        m1.BYORGNO = Request.Params["BYORGNO"];
                        m1.BYORGNOXS = Request.Params["BYORGNOXS"];
                        m1.VILLAGE = Request.Params["VILLAGE"];
                        m1.JD = Request.Params["JD"];
                        m1.WD = Request.Params["WD"];
                        m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                        m1.opMethod = Request.Params["Method"];
                        ms = TD_POINTMARKCls.Manager(m1);
                    }
                }
                else
                {
                    if (TYPE != "1")
                    {
                        m1.OBJECTID = Request.Params["OBJECTID"];
                        m1.opMethod = "Del";
                        TD_POINTMARKCls.Manager(m1);
                        m.TYPE = TYPE;
                        m.BYORGNO = Request.Params["BYORGNO"];
                        m.BYORGNOXS = Request.Params["BYORGNOXS"];
                        m.NAME = Request.Params["NAME"];
                        m.VILLAGE = Request.Params["VILLAGE"];
                        m.JD = Request.Params["JD"];
                        m.WD = Request.Params["WD"];
                        m.Shape = "geometry::STGeomFromText('POINT(" + m.JD + " " + m.WD + ")',4326)";
                        m.opMethod = "Add";
                        ms = TD_MOUNTAINCls.Manager(m);
                    }
                    else
                    {
                        m.OBJECTID = Request.Params["OBJECTID"];
                        m.opMethod = "Del";
                        TD_MOUNTAINCls.Manager(m);
                        m1.TYPE = TYPE;
                        m1.BYORGNO = Request.Params["BYORGNO"];
                        m1.BYORGNOXS = Request.Params["BYORGNOXS"];
                        m1.NAME = Request.Params["NAME"];
                        m1.VILLAGE = Request.Params["VILLAGE"];
                        m1.JD = Request.Params["JD"];
                        m1.WD = Request.Params["WD"];
                        m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                        m1.opMethod = "Add";
                        ms = TD_POINTMARKCls.Manager(m1);
                    }
                }
            }
            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取自然村列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getMountainlist()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string page = Request.Params["page"];
            string name = Request.Params["NAME"];
            string orgno = Request.Params["BYORGNO"];
            string type = Request.Params["TYPE"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>所属县市</th>");
            sb.AppendFormat("<th>所属乡镇</th>");
            sb.AppendFormat("<th>所属自然村</th>");
            sb.AppendFormat("<th>所属地类</th>");
            sb.AppendFormat("<th>名称</th>");
            sb.AppendFormat("<th>经度</th>");
            sb.AppendFormat("<th>纬度</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            int total = 0;
            var result = TD_MOUNTAINCls.getModelList(new TD_MOUNTAIN_SW { NAME = name, type = type, BYORGNO = orgno, curPage = int.Parse(page), pageSize = int.Parse(PageSize) }, out total);
            foreach (var s in result)
            {
                sb.AppendFormat("<tr class='row1' onclick=\"setColor(this)\">");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.ORGXSNAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.ORGNAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.VILLAGE);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.TYPENAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.NAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.JD);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", s.WD);
                sb.AppendFormat("    <td class=\" \">");
                if (string.IsNullOrEmpty(s.JD))
                {
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                }
                else
                {
                    if (s.TYPE != "1")
                    {
                        sb.AppendFormat("<a href=\"#\" onclick=\" Position('TD_MOUNTAIN','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.OBJECTID, s.NAME);
                    }
                    else
                    {
                        sb.AppendFormat("<a href=\"#\" onclick=\" Position('TD_POINTMARK','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", s.OBJECTID, s.NAME);
                    }
                }
                if (s.TYPE != "1")
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.OBJECTID, "TD_MOUNTAIN");
                }
                else
                {
                    sb.AppendFormat("<a href=\"#\" onclick=\"See('{0}','{1}')\" class=\"searchBox_01 LinkSee\">查看</a>", s.OBJECTID, "TD_POINTMARK");
                }
                if (SystemCls.isRight("011011003") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}','{2}')\" title='编辑' class=\"searchBox_01 LinkMdy\">修改</a>", s.OBJECTID, page, s.TYPE);
                if (SystemCls.isRight("011011004") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}','{2}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", s.OBJECTID, page, s.TYPE);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 只获取县市
        /// </summary>
        /// <returns></returns>
        public string GetonlySXList()
        {
            var result = T_SYS_ORGCls.getOrgJsonStr(new T_SYS_ORGSW { TopSXORGNO = SystemCls.getCurUserOrgNo() });
            return result;
        }
        /// <summary>
        /// 获取所有的乡镇
        /// </summary>
        /// <returns></returns>
        public string GetonlyXZList()
        {
            string BYORGNOXZ = Request.Params["BYORGNOXZ"];
            var result = "";
            if (string.IsNullOrEmpty(BYORGNOXZ))
            {
                result = "";
            }
            else
            {
                result = T_SYS_ORGCls.getOrgJsonStr(new T_SYS_ORGSW { GetXZOrgNOByConty = BYORGNOXZ });
            }
            return result;
        }
        /// <summary>
        /// 获取乡镇下面的自然村
        /// </summary>
        /// <returns></returns>
        public string GetVillageList()
        {
            string BYORGNOCUN = Request.Params["BYORGNOCUN"];
            var result = "";
            if (string.IsNullOrEmpty(BYORGNOCUN))
            {
                result = "";
            }
            else
            {
                result = T_SYS_ORG_CWHCls.getVillageJsonStr(new T_SYS_ORG_CWH_SW { ORGNO = BYORGNOCUN, ORGLINKTYPE = "2" });
            }
            return result;
        }
        #endregion

        #region 自然村
        ///// <summary>
        ///// 自然村管理页面
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult PointmarkIndex()
        //{
        //    pubViewBag("011010", "011010", "");
        //    if (ViewBag.isPageRight == false)
        //        return View();
        //    ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
        //    ViewBag.isAdd = (SystemCls.isRight("011010002")) ? "1" : "0";
        //    return View();
        //}
        /// <summary>
        /// 获取查看和修改前的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPointmarkjson()
        {
            string OBJECTID = Request.Params["OBJECTID"];
            //if (string.IsNullOrEmpty(ID))
            //    ID = "0";
            return Content(JsonConvert.SerializeObject(TD_POINTMARKCls.getModel(new TD_POINTMARK_SW { OBJECTID = OBJECTID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 自然村管理
        /// </summary>
        /// <returns></returns>
        public ActionResult POINTMARKManager()
        {
            TD_POINTMARK_Model m = new TD_POINTMARK_Model();
            m.OBJECTID = Request.Params["OBJECTID"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.ADDRESS = Request.Params["ADDRESS"];
            m.KIND = Request.Params["ADDRESS"];
            m.NAME = Request.Params["NAME"];
            m.MAPNAME = Request.Params["MAPNAME"];
            m.TELEPHONE = Request.Params["TELEPHONE"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.Shape = "geometry::STGeomFromText('POINT(" + m.JD + " " + m.WD + ")',4326)";
            m.opMethod = Request.Params["Method"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.NAME))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入名称", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间，请重新输入！", "")), "text/html;charset=UTF-8");
                }
            }
            return Content(JsonConvert.SerializeObject(TD_POINTMARKCls.Manager(m)), "text/html;charset=UTF-8");
        }


        #endregion

        #region 综合查看
        /// <summary>
        /// 综合查看
        /// </summary>
        /// <returns></returns>
        public ActionResult TotalSee()
        {
            string id = Request.Params["ID"];
            ViewBag.orgno = JC_FIRECls.getByorgno(new JC_FIRE_SW { JCFID = id });
            //ViewBag.FRFIID = FIRERECORD_FIREINFOCls.getFrfiid(new FIRERECORD_FIREINFO_SW { JCFID = id }); 
            return View();
        }
        #endregion
    }
}
