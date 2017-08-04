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
using ManagerSystem.MVC.HelpCom;

namespace ManagerSystem.MVC.Controllers
{
    public class ArtDocumentController : BaseController
    {
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

        #region 文档管理 DocManager()
        /// <summary>
        /// 文档管理
        /// </summary>
        /// <returns></returns>

        [ValidateInput(false)]
        public ActionResult DocManager()
        {
            //string tid = Request.Params["typeID"];
            //string id = Request.Params["id"];
            //string ARTTITLE = Request.Params["ARTTITLE"];
            //string ARTCONTENT = Request.Params["ARTCONTENT"];
            //string PLANFILENAME = Request.Params["PLANFILENAME"];
            //string returnUrl = Request.Params["returnUrl"];
            //string ARTTYPEID = Request.Params["ARTTYPEID"];
            //if (string.IsNullOrEmpty(returnUrl))
            //    returnUrl = "/ArtDocument/DocList";
            string Method = Request.Params["Method"];
            //默认为添加
            if (string.IsNullOrEmpty(Method))
            {
                Method = "Add";
            }
            if (Method != "Del")
            {
                if (string.IsNullOrEmpty(Request.Params["ARTTITLE"]))
                    return Content(JsonConvert.SerializeObject(new Message(false, "请输入标题！", "")), "text/html;charset=UTF-8");
            }
            ART_DOCUMENT_Model m = new ART_DOCUMENT_Model();
            m.ARTID = Request.Params["id"];
            m.ARTTYPEID = Request.Params["ARTTYPEID"];
            m.ARTTITLE = Request.Params["ARTTITLE"];
            m.ARTCONTENT = Request.Params["ARTCONTENT"];
            m.PLANFILENAME = Request.Params["PLANFILENAME"];
            m.opMethod = Method;
            m.returnUrl = Request.Params["returnUrl"];
            m.BYORGNO = SystemCls.getCurUserOrgNo();
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

        #region 文档管理页面DocMan()

        /// <summary>
        /// 文档管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DocMan()
        {
            string typeID = (string.IsNullOrEmpty(Request.Params["typeID"])) ? "" : Request.Params["typeID"].ToString();
            if (string.IsNullOrEmpty(typeID))
                return View();
            pubViewBag(typeID, typeID, "");
            if (ViewBag.isPageRight == false)
                return View();

            //ViewBag.isDel = (SystemCls.isRight(ViewBag.typeid + "002")) ? "1" : "0";
            ViewBag.T_Method = Request.Params["Method"];
            //如果未传参数，默认为添加
            if (string.IsNullOrEmpty(ViewBag.T_Method))
                ViewBag.T_Method = "Add";
            ViewBag.id = Request.Params["id"];
            ViewBag.typeID = typeID;
            string bigTypeID = "";// (string.IsNullOrEmpty(Request.Params["ArtTID"])) ? "" : Request.Params["ArtTID"].ToString();
            if (typeID == "006101") bigTypeID = "013";//通告公告
            if (typeID == "006102") bigTypeID = "014";//防火百科
            if (typeID == "006103") bigTypeID = "015";//帮助
            if (typeID == "006024") bigTypeID = "031";//帮助
            string typeid = "";
            if (bigTypeID == "013") typeid = "47";
            if (bigTypeID == "014") typeid = "48";
            if (bigTypeID == "015") typeid = "49";
            if (bigTypeID == "031") typeid = "54";
            ViewBag.vdARTTYPEID = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = typeid });
            return View();
        }
        #endregion

        #region 文档查看 DocShow()
        /// <summary>
        /// 文档查看
        /// </summary>
        /// <returns></returns>
        public ActionResult DocShow()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            ART_DOCUMENT_Model m = ART_DOCUMENTCls.getModel(new ART_DOCUMENT_SW { ARTID = ID });
            pubViewBag(m.ARTTYPEID, m.ARTTYPEID, "");
            ViewBag.isPageRight = true;
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
            if (string.IsNullOrEmpty(v.PLANFILENAME) == false)
                sb.AppendFormat("<div class=' showArtFile'>文件下载：<br><a href='/UploadFile/Doc/{0}'>{1}</a></div>", v.PLANFILENAME, v.ARTTITLE);

            ViewBag.Content = sb.ToString();
            return View();
        }

        #endregion

        #region 搜索 DocQuery()
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
            pubViewBag(tid, tid, "");
            string str = ClsStr.EncryptA01((PageSize + "|" + EMAILTITLE + "|" + tid), "kkkkkkkk");
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/ArtDocument/DocIndex?trans=" + str + "&typeID=" + tid + "&page=" + Page)), "text/html;charset=UTF-8");
        }

        #endregion

        #region 文档管理
        /// <summary>
        /// 文档管理
        /// </summary>
        /// <returns></returns>
        public ActionResult DocIndex()
        {
            string typeID = (string.IsNullOrEmpty(Request.Params["typeID"])) ? "" : Request.Params["typeID"].ToString();
            if (string.IsNullOrEmpty(typeID))
                return View();
            pubViewBag(typeID, typeID, "");
            if (ViewBag.isPageRight == false)
                return View();

            //string PageSize = Request.Params["PageSize"];
            //string page = Request.Params["page"];
            //page = (string.IsNullOrEmpty(page)) ? "1" : page;
            //string trans = Request.Params["trans"];
            //string[] arr = new string[3];
            //if (string.IsNullOrEmpty(trans) == false)
            //{
            //    arr = ClsStr.DecryptA01(trans, "kkkkkkkk").Split('|');
            //    //typeID = arr[2];//类别序号
            //}
            //else
            //{
            //    arr[0] = PagerCls.getDefaultPageSize().ToString();
            //}
            //ViewBag.SearchTITLE = arr[1];

            //StringBuilder sb = new StringBuilder();
            ////获取顶级
            //var list = ART_TYPECls.getListModel(new ART_TYPE_SW { ARTTYPERID = "0" });
            //foreach (var v in list)
            //{
            //    sb.AppendFormat("<ul>");

            //    sb.AppendFormat("<li class='liType'>{0}</li>", v.ARTTYPENAME);
            //    //获取子类别
            //    var listChild = ART_TYPECls.getListModel(new ART_TYPE_SW { ARTTYPERID = v.ARTTYPEID });
            //    foreach (var vC in listChild)
            //    {
            //        if (string.IsNullOrEmpty(typeID))
            //            typeID = vC.ARTTYPEID;
            //        if (typeID == vC.ARTTYPEID)
            //            sb.AppendFormat("<li class='liCur title'><a href='DocIndex?tid={0}'>{1}</a>", vC.ARTTYPEID, vC.ARTTYPENAME);
            //        else
            //            sb.AppendFormat("<li class='title'><a href='DocIndex?tid={0}'>{1}</a>", vC.ARTTYPEID, vC.ARTTYPENAME);
            //    }
            //    sb.Append("</li>");
            //    sb.AppendFormat("</ul>");
            //}
            //ART_TYPE_Model artM = ART_TYPECls.getModel(new ART_TYPE_SW { ARTTYPEID = typeID });
            //ViewBag.TypeName = artM.ARTTYPENAME;
            ViewBag.typeid = typeID;
            // ViewBag.typeList = sb.ToString();
            ViewBag.isDel = (SystemCls.isRight(ViewBag.typeid + "002")) ? "1" : "0";
            //int total = 0;
            //ViewBag.List = getDocListStr(new ART_DOCUMENT_SW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), ARTTITLE = arr[1], ARTTYPEID = typeID }, out total);//列表
            //ViewBag.PagerInfo = PagerCls.getPagerInfo_New(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), rowCount = total, url = "/ArtDocument/DocIndex?typeID=" + typeID + "&trans=" + trans });
            return View();
        }

        #endregion

        #region 文档管理Ajax列表
        /// <summary>
        /// 文档管理Ajax列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getDocIndexAjax()
        {

            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            
            string EMAILTITLE = Request.Params["TITLE"];
            string tid = Request.Params["tid"];

            string BYORGNO = Request.Params["BYORGNO"];
            string FIRELEVEL = Request.Params["FIRELEVEL"];

            int total = 0;
            //JC_FIRE_PLAN_SW sw = new JC_FIRE_PLAN_SW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), BYORGNO = BYORGNO, FIRELEVEL = FIRELEVEL };
            // T_SYSSEC_IPSUSER_SW sw1 = new T_SYSSEC_IPSUSER_SW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), LOGINUSERNAME = LoginUserName, USERNAME = UserName, DEPARTMENT = DEPARTMENT, ORGNO = ORGNO };
            ART_DOCUMENT_SW sw = new ART_DOCUMENT_SW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), ARTTITLE = EMAILTITLE, ARTTYPEID = tid };
            //var result = JC_FIRE_PLANCls.getModelList(sw, out total);
            var result = ART_DOCUMENTCls.getModelList(sw, out total);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style='width:10%;'>序号</th>");
            sb.AppendFormat("<th style='width:10%;'>单位</th>");
            sb.AppendFormat("<th style='width:30%;'>标题</th>");
            sb.AppendFormat("<th style='width:15%;'>添加人</th>");
            sb.AppendFormat("<th style='width:15%;'>时间</th>");
            //if (blnDel || blnMdy)
            //    sb.AppendFormat("<th style='width:20%;'>管理</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center  \">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center  \">{0}</td>", v.orgName);// v.ARTTYPENAME);
                sb.AppendFormat("<td class=\"left  \"><a href=\"/ArtDocument/DocShow?ID={1}\" target='_blank'>{0}</a></td>", v.ARTTITLE, v.ARTID);
                sb.AppendFormat("<td class=\"left  \">{0}</td>", v.ARTADDUSERName);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", v.ARTTIME);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");

            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }


        #endregion
      
        #region 搜索 DocDefaultQuery
        /// <summary>
        /// 搜索
        /// </summary>
        /// <returns></returns>
        //public ActionResult DocDefaultQuery()
        //{
        //    string PageSize = Request.Params["PageSize"];
        //    string Page = Request.Params["Page"];
        //    string EMAILTITLE = Request.Params["TITLE"];
        //    string typeID = Request.Params["typeID"];
        //    pubViewBag(typeID, typeID, "");
        //    string str = ClsStr.EncryptA01((PageSize + "|" + EMAILTITLE + "|" + typeID), "kkkkkkkk");
        //    return Content(JsonConvert.SerializeObject(new Message(true, "", "/ArtDocument/DocDefault?trans=" + str + "&typeID=" + typeID + "&page=" + Page)), "text/html;charset=UTF-8");
        //}

        #endregion

        #region 文档管理 DocDefault()
        /// <summary>
        /// 文档管理
        /// </summary>
        /// <returns></returns>
        public ActionResult DocDefault()
        {
            //013	通告公告 014	防火百科 015	帮助
            string typeID = (string.IsNullOrEmpty(Request.Params["typeID"])) ? "" : Request.Params["typeID"].ToString();
            //string bigTypeID = "";// (string.IsNullOrEmpty(Request.Params["ArtTID"])) ? "" : Request.Params["ArtTID"].ToString();
            //if (typeID == "006101") bigTypeID = "013";//通告公告
            //if (typeID == "006102") bigTypeID = "014";//防火百科
            //if (typeID == "006103") bigTypeID = "015";//帮助
            if (string.IsNullOrEmpty(typeID))
                return View();
            pubViewBag(typeID, typeID, "");
            if (ViewBag.isPageRight == false)
                return View();

            //string PageSize = Request.Params["PageSize"];
            //string page = Request.Params["page"];
            //page = (string.IsNullOrEmpty(page)) ? "1" : page;
            //string trans = Request.Params["trans"];
            //string[] arr = new string[3];
            //if (string.IsNullOrEmpty(trans) == false)
            //{
            //    arr = ClsStr.DecryptA01(trans, "kkkkkkkk").Split('|');
            //    //typeID = arr[2];//类别序号
            //}
            //else
            //{
            //    arr[0] = PagerCls.getDefaultPageSize().ToString();
            //}
            //ViewBag.SearchTITLE = arr[1];
            ViewBag.typeID = typeID;
            //ViewBag.ArtTID = bigTypeID;
            // ViewBag.typeList = sb.ToString();
            //ViewBag.isDel = (SystemCls.isRight(ViewBag.typeid + "002")) ? "1" : "0";
            //int total = 0;
            //ViewBag.List = getDocBigListStr(new ART_DOCUMENT_SW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), ARTTITLE = arr[1], ARTBigTYPEID = bigTypeID, BYORGNO = SystemCls.getCurUserOrgNo() }, typeID, out total);//列表
            //ViewBag.PagerInfo = PagerCls.getPagerInfo_New(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), rowCount = total, url = "/ArtDocument/DocDefault?typeID=" + typeID + "&trans=" + trans });
            return View();
        }

        #endregion

        #region 文档管理Ajax列表
        /// <summary>
        /// 文档管理Ajax列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getDocDefaultAjax()
        {

            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            string EMAILTITLE = Request.Params["TITLE"];
            string typeID = (string.IsNullOrEmpty(Request.Params["typeID"])) ? "" : Request.Params["typeID"].ToString();
            string bigTypeID = "";// (string.IsNullOrEmpty(Request.Params["ArtTID"])) ? "" : Request.Params["ArtTID"].ToString();
            if (typeID == "006101") bigTypeID = "013";//通告公告
            if (typeID == "006102") bigTypeID = "014";//防火百科
            if (typeID == "006103") bigTypeID = "015";//帮助
            if (typeID == "006024") bigTypeID = "031";//有害生物

            int total = 0;
            ART_DOCUMENT_SW sw = new ART_DOCUMENT_SW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), ARTTITLE = EMAILTITLE, ARTBigTYPEID = bigTypeID, BYORGNO = SystemCls.getCurUserOrgNo() };

           // JC_FIRE_PLAN_SW sw = new JC_FIRE_PLAN_SW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), BYORGNO = BYORGNO, FIRELEVEL = FIRELEVEL };
            // T_SYSSEC_IPSUSER_SW sw1 = new T_SYSSEC_IPSUSER_SW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), LOGINUSERNAME = LoginUserName, USERNAME = UserName, DEPARTMENT = DEPARTMENT, ORGNO = ORGNO };

           // var result = JC_FIRE_PLANCls.getModelList(sw, out total);
            var result = ART_DOCUMENTCls.getModelList(sw, out total);


            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style='width:10%;'>序号</th>");
            sb.AppendFormat("<th style='width:10%;'>类别</th>");
            sb.AppendFormat("<th style='width:30%;'>标题</th>");
            sb.AppendFormat("<th style='width:15%;'>添加人</th>");
            sb.AppendFormat("<th style='width:15%;'>时间</th>");
            sb.AppendFormat("<th style='width:20%;'>管理</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center  \">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center  \">{0}</td>", v.ARTTYPENAME);// v.ARTTYPENAME);
                sb.AppendFormat("<td class=\"left  \"><a href=\"/ArtDocument/DocShow?ID={1}\" target='_blank'>{0}</a></td>", v.ARTTITLE, v.ARTID);
                sb.AppendFormat("<td class=\"left  \">{0}</td>", v.ARTADDUSERName);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", v.ARTTIME);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("<td class=\"center  \">");
                sb.AppendFormat("<a href='#' onclick=\"Mdy('{0}','{1}')\" class=\"searchBox_01 LinkMdy\">修改</a>", v.ARTID, typeID);
                sb.AppendFormat("&nbsp;<a href='#' onclick='Manager({0})' class=\"searchBox_01 LinkDel\">删除</a>", v.ARTID);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");

            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }


        #endregion

        #region 组合表格 getDocBigListStr
        /// <summary>
        /// 组合表格
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        private string getDocBigListStr(ART_DOCUMENT_SW sw, string typeid, out int total)
        {
            //bool blnMdy = SystemCls.isRight(ViewBag.typeid + "003");
            //bool blnDel = SystemCls.isRight(ViewBag.typeid + "004");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style='width:10%;'>序号</th>");
            sb.AppendFormat("<th style='width:10%;'>类别</th>");
            sb.AppendFormat("<th style='width:30%;'>标题</th>");
            sb.AppendFormat("<th style='width:15%;'>添加人</th>");
            sb.AppendFormat("<th style='width:15%;'>时间</th>");
            sb.AppendFormat("<th style='width:20%;'>管理</th>");
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
                sb.AppendFormat("<td class=\"center  \">{0}</td>", v.ARTTYPENAME);// v.ARTTYPENAME);
                sb.AppendFormat("<td class=\"left  \"><a href=\"/ArtDocument/DocShow?ID={1}\" target='_blank'>{0}</a></td>", v.ARTTITLE, v.ARTID);
                sb.AppendFormat("<td class=\"left  \">{0}</td>", v.ARTADDUSERName);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", v.ARTTIME);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("<td class=\"center  \">");
                //sb.AppendFormat("<a  href='/ArtDocument/DocMan?Method=Mdy&id={0}&typeID={1}' class=\"searchBox_01 LinkMdy\">修改</a>", v.ARTID, typeid);
                sb.AppendFormat("<a href='#' onclick=\"Mdy('{0}','{1}')\" class=\"searchBox_01 LinkMdy\">修改</a>", v.ARTID, typeid);
                sb.AppendFormat("&nbsp;<a href='#' onclick='Manager({0})' class=\"searchBox_01 LinkDel\">删除</a>", v.ARTID);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        #endregion
    }
}
