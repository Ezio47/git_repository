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
using System.Text;

namespace ManagerSystem.MVC.Controllers
{
    public class YJJCController : BaseController
    {
        #region 红外相机基本信息管理
        public ActionResult INFRAREDCAMERA_BASICINFOManger()
        {
            JC_INFRAREDCAMERA_BASICINFO_Model m = new JC_INFRAREDCAMERA_BASICINFO_Model();
            string INFRAREDCAMERAID = Request.Params["INFRAREDCAMERAID"];
            string BYORGNO = Request.Params["BYORGNO"];
            string INFRAREDCAMERANAME = Request.Params["INFRAREDCAMERANAME"];
            string PHONE = Request.Params["PHONE"];
            string JD = Request.Params["JD"];
            string WD = Request.Params["WD"];
            string GC = Request.Params["GC"];
            string ADDRESS = Request.Params["ADDRESS"];
            string Method = Request.Params["Method"];
            string returnUrl = Request.Params["returnUrl"];
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/YYJC/INFRAREDCAMERA_BASICINFOList";

            m.INFRAREDCAMERAID = INFRAREDCAMERAID;
            m.BYORGNO = BYORGNO;
            m.INFRAREDCAMERANAME = INFRAREDCAMERANAME;
            m.JD = JD;
            m.WD = WD;
            m.Shape = "geometry::STGeomFromText('POINT(" + m.JD + " " + m.WD + ")',4326)";
            m.GC = GC;
            m.ADDRESS = ADDRESS;
            m.PHONE = PHONE;
            m.opMethod = Method;
            m.returnUrl = returnUrl;
            if (m.opMethod != "Del")
            {
                if (float.Parse(JD) >= 180 || float.Parse(JD) <= -180)
                    return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间,请重新输入!", "")), "text/html;charset=UTF-8");
                if (float.Parse(WD) >= 90 || float.Parse(WD) <= -90)
                    return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间,请重新输入!", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(JC_INFRAREDCAMERACls.Manager(m)), "text/html;charset=UTF-8");
        }

        public ActionResult INFRAREDCAMERAMan()
        {
            pubViewBag("006007", "006007", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.T_Method = Request.Params["Method"];
            //如果未传参数,默认为添加
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });// ipsuM.ORGNAME });
            string ID = Request.Params["ID"];
            ViewBag.T_ID = ID;
            if (string.IsNullOrEmpty(ID) == false)
            {
                if (string.IsNullOrEmpty(ViewBag.T_ID))
                    return Redirect("/System/Error?ID=2");//参数错误 
            }
            if (string.IsNullOrEmpty(ViewBag.T_Method))
                ViewBag.T_Method = "Add";
            return View();
        }

        public ActionResult GetINFRAREDCAMERA_BASICINFOJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            return Content(JsonConvert.SerializeObject(JC_INFRAREDCAMERACls.getModel(new JC_INFRAREDCAMERA_BASICINFO_SW { INFRAREDCAMERAID = ID })), "text/html;charset=UTF-8");
        }

        public ActionResult getINFRAREDCAMERA_BASICINFOList()
        {
            string PHONE = Request.Params["PHONE"];
            //string INFRAREDCAMERANAME = Request.Params["INFRAREDCAMERANAME"];
            string BYORGNO = Request.Params["BYORGNO"];
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            //string str = ClsStr.EncryptA01(INFRAREDCAMERANAME + "|" + PHONE + "|" + BYORGNO, "kkkkkkkk");
            int total = 0;
            JC_INFRAREDCAMERA_BASICINFO_SW sw = new JC_INFRAREDCAMERA_BASICINFO_SW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), BYORGNO = BYORGNO, PHONE = PHONE };
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"sample-table-2\" class=\"table table-striped table-bordered table-hover dataTable\" aria-describedby=\"sample-table-2_info\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr role=\"row\">");
            sb.AppendFormat("<th class=\"center sorting_disabled\">序号</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\">单位名称</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\">相机名称</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\">手机号码</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\">经度</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\">纬度</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\">高程</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\">详细地址</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\"></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody role=\"alert\" aria-live=\"polite\" aria-relevant=\"all\">");
            var result = JC_INFRAREDCAMERACls.getListModel(sw, out total);
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.ORGNAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.INFRAREDCAMERANAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.PHONE);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.JD);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.WD);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.GC);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.ADDRESS);
                sb.AppendFormat("<td class=\"center\">");
                if (string.IsNullOrEmpty(v.JD) || string.IsNullOrEmpty(v.WD))
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                else
                    sb.AppendFormat("<a href=\"#\" onclick=\" Position('JC_INFRAREDCAMERA_BASICINFO','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", v.INFRAREDCAMERAID, v.INFRAREDCAMERANAME);
                if (SystemCls.isRight("006007002") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\" See('{0}')\" title='查看' class=\"searchBox_01 LinkPhoto\">查看</a>", v.INFRAREDCAMERAID);
                if (SystemCls.isRight("006007003") == true)
                    sb.AppendFormat("<a  href='#' onclick=\"Mdy('{0}','Mdy')\" title='修改' class=\"searchBox_01 LinkMdy\">修改</a>", v.INFRAREDCAMERAID);
                if (SystemCls.isRight("006007005") == true)
                    sb.AppendFormat("<a href=\"#\" onclick=\" Photo('{0}')\" title='照片' class=\"searchBox_01 LinkPhoto\">照片</a>", v.INFRAREDCAMERAID);
                if (SystemCls.isRight("006007004") == true)
                    sb.AppendFormat("&nbsp;<a href='#' onclick=\"Manager('{0}','Del')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.INFRAREDCAMERAID);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8"); ;
        }

        public ActionResult INFRAREDCAMERA_BASICINFOList()
        {
            pubViewBag("006007", "006007", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.isAdd = (SystemCls.isRight("006007001")) ? "1" : "0";
            return View();
        }

        public ActionResult ViewINFRAREDCAMERA()
        {
            var id = Request.Params["ID"];
            StringBuilder sb = new StringBuilder();
            var model = JC_INFRAREDCAMERACls.getModel(new JC_INFRAREDCAMERA_BASICINFO_SW { INFRAREDCAMERAID = id });
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td  class=\"left tdField\" style=\"width:15%\">单位名称:</td>");
            sb.AppendFormat("<td  class=\"left\" style=\"width:35%\">{0}</td>", model.ORGNAME);
            sb.AppendFormat("<td  class=\"left tdField\" style=\"width:15%\">相机名:</td>");
            sb.AppendFormat("<td  class=\"left\" style=\"width:35%\">{0}</td>", model.INFRAREDCAMERANAME);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td  class=\"left tdField\">手机号码:</td>");
            sb.AppendFormat("<td  class=\"left\">{0}</td>", model.PHONE);
            sb.AppendFormat("<td  class=\"left tdField\">高程:</td>");
            sb.AppendFormat("<td  class=\"left\">{0}</td>", model.GC);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td  class=\"left tdField\">经度:</td>");
            sb.AppendFormat("<td  class=\"left\">{0}</td>", model.JD);
            sb.AppendFormat("<td  class=\"left tdField\">纬度:</td>");
            sb.AppendFormat("<td  class=\"left\">{0}</td>", model.WD);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<td  class=\"left tdField\">地址:</td>");
            sb.AppendFormat("<td  class=\"left\" colspan=\"3\">{0}</td>", model.ADDRESS);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.detail = sb.ToString();
            return View();
        }

        private string getINFRAREDCAMERA_BASICINFOStr(JC_INFRAREDCAMERA_BASICINFO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"sample-table-2\" class=\"table table-striped table-bordered table-hover dataTable\" aria-describedby=\"sample-table-2_info\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr role=\"row\">");
            sb.AppendFormat("<th class=\"center sorting_disabled\">序号</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\">单位名称</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\">相机名称</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\">手机号码</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\">经度</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\">纬度</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\">高程</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\">详细地址</th>");
            sb.AppendFormat("<th class=\"center sorting_disabled\"></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody role=\"alert\" aria-live=\"polite\" aria-relevant=\"all\">");
            var result = JC_INFRAREDCAMERACls.getListModel(sw);
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.ORGNAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.INFRAREDCAMERANAME);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.PHONE);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.JD);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.WD);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.GC);
                sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", v.ADDRESS);
                sb.AppendFormat("<td class=\"center\">");
                if (string.IsNullOrEmpty(v.JD) || string.IsNullOrEmpty(v.WD))
                    sb.AppendFormat("<a  href=\"javascript:void(0);\"title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                else
                    sb.AppendFormat("<a href=\"#\" onclick=\" Position('JC_INFRAREDCAMERA_BASICINFO','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", v.INFRAREDCAMERAID, v.INFRAREDCAMERANAME);
                sb.AppendFormat("<a href=\"#\" onclick=\" Photo('{0}')\" title='照片管理' class=\"searchBox_01 LinkPhoto\">照片</a>", v.INFRAREDCAMERAID);
                sb.AppendFormat("<a href='#' onclick=\"Mdy('{0}','Mdy')\" title='修改' class=\"searchBox_01 LinkMdy\">修改</a>", v.INFRAREDCAMERAID);
                sb.AppendFormat("&nbsp;<a href='#' onclick=\"Manager('{0}','Del')\"  title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.INFRAREDCAMERAID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        #endregion

        #region 红外相机图片管理
        public ActionResult INFRAREDCAMERPhoto()
        {
            var id = Request.Params["ID"];
            var result = JC_INFRAREDCAMERACls.getListNewModelPhoto(new JC_INFRAREDCAMERA_PHOTO_SW { INFRAREDCAMERAID = id });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div>");
            //sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            //sb.AppendFormat("<tr>");
            //sb.AppendFormat("<td  class=\"center\">照片</td>");
            //sb.AppendFormat("</tr>");
            //sb.AppendFormat("<tr>");
            //sb.AppendFormat("<td  class=\"center\">");
            //foreach (var item in result)
            //{
            //    sb.AppendFormat("<img src=\"/UploadFile/INFRAREDCAMERA/{0}\" alt=\"alttext\"  style=\"padding:2px\" height =\"100px\" width=\"100px\"/></a>", item.PHOTOTITLE);
            //}
            //sb.AppendFormat("</td>");
            //sb.AppendFormat("</tr>");
            //sb.AppendFormat("</table>");
            if (result.Any())
            {
                foreach (var s in result)
                {
                    sb.AppendFormat("<div style=\"float:left;margin:5px\">");
                    sb.AppendFormat("<a href=\"/UploadFile/INFRAREDCAMERA/{0}\" target=\"_blank\"><img src=\"/UploadFile/INFRAREDCAMERA/{0}\" alt=\"alttext\" height =\"160px\" width=\"165px\"/></a>", s.PHOTOTITLE);
                    sb.AppendFormat("<p align=\"center\">{0}</p>", s.PHOTOTITLE);
                    sb.AppendFormat("</div>");
                }
            }
            else
            {
                sb.AppendFormat("<p align=\"center\">暂无图片</p>");
            }
            sb.AppendFormat("</div>");
            ViewBag.photo = sb.ToString();
            return View();
        }

        public ActionResult CAMERA_PHOTOManger()
        {
            JC_INFRAREDCAMERA_PHOTO_Model m = new JC_INFRAREDCAMERA_PHOTO_Model();
            string smid = Request.Params["smid"];
            string tpa = Request.Params["tpa"];
            string recvdatetime = Request.Params["recvdatetime"];
            string mmsfilesid = Request.Params["mmsfilesid"];
            string filetype = Request.Params["filetype"];
            string filename = Request.Params["filename"];
            string MANSTATE = Request.Params["MANSTATE"];
            string MANRESULT = Request.Params["MANRESULT"];
            string MANTIME = Request.Params["MANTIME"];
            string MANUSERID = Request.Params["MANUSERID"];
            string ManUserName = Request.Params["ManUserName"];
            string Method = Request.Params["opMethod"];
            string returnUrl = Request.Params["returnUrl"];
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/YYJC/CAMERA_PHOTOList";
            m.smid = smid;
            m.tpa = tpa;
            m.recvdatetime = recvdatetime;
            m.mmsfilesid = mmsfilesid;
            m.filetype = filetype;
            m.filename = filename;
            m.MANSTATE = MANSTATE;
            m.MANRESULT = MANRESULT;
            m.MANTIME = MANTIME;
            m.MANUSERID = MANUSERID;
            m.ManUserName = ManUserName;
            m.opMethod = Method;
            m.returnUrl = returnUrl;
            return Content(JsonConvert.SerializeObject(JC_INFRAREDCAMERACls.ManagerPhoto(m)), "text/html;charset=UTF-8");
        }

        public ActionResult CAMERA_PHOTOListQuery()
        {
            string PageSize = Request.Params["PageSize"];
            string Page = Request.Params["Page"];
            string tpa = Request.Params["tpa"];
            string DateBegin = Request.Params["DateBegin"];
            string DateEnd = Request.Params["DateEnd"];
            string str = ClsStr.EncryptA01(PageSize + "|" + tpa + "|" + DateBegin + "|" + DateEnd, "kkkkkkkk");
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/YJJC/CAMERA_PHOTOList?trans=" + str + "&page=" + Page)), "text/html;charset=UTF-8");
        }

        public ActionResult CAMERA_PHOTOList()
        {
            pubViewBag("008006", "008006", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.T_Method = Request.Params["Method"];
            string page = Request.Params["page"];//当前页数
            string trans = Request.Params["trans"];//传递参数
            if (string.IsNullOrEmpty(page))
                page = "1";
            string[] arr = new string[4];//存放查询条件的数组 根据实际存放的数据
            if (string.IsNullOrEmpty(trans) == false)
                arr = ClsStr.DecryptA01(trans, "kkkkkkkk").Split('|');
            if (string.IsNullOrEmpty(arr[0]) == true)
                arr[0] = PagerCls.getDefaultPageSize().ToString();//默认记录数
            ViewBag.tpa = arr[1];//显示查询号码
            ViewBag.DateBegin = arr[2];
            ViewBag.DateEnd = arr[3];
            if (string.IsNullOrEmpty(arr[2]) == false) arr[2] = arr[2];
            if (string.IsNullOrEmpty(arr[3]) == false) arr[3] = arr[3];
            //列表
            int total = 0;
            ViewBag.CAMERA_PHOTOList = getCAMERA_PHOTOStr(new JC_INFRAREDCAMERA_PHOTO_SW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), tpa = arr[1], DateBegin = arr[2], DateEnd = arr[3] }, out total);
            //分页信息 需使用上面的total
            ViewBag.PagerInfo = PagerCls.getPagerInfo(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), rowCount = total, url = "/YJJC/CAMERA_PHOTOList?trans=" + trans, pageSizeArr = new string[] { "20", "40", "80" } });
            //ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });// ipsuM.ORGNAME });
            return View();
        }

        private string getCAMERA_PHOTOStr(JC_INFRAREDCAMERA_PHOTO_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            var result = JC_INFRAREDCAMERACls.getListModelPhotoPager(sw, out  total);
            if (result.Count() > 0)
            {
                sb.AppendFormat("<table id=\"sample-table-2\" class=\"table table-striped table-bordered table-hover dataTable\" aria-describedby=\"sample-table-2_info\">");
                //sb.AppendFormat("<thead>");
                int fieldCount = 4;
                int fieldI = 1;
                var imgurl = System.Configuration.ConfigurationManager.AppSettings["ImageUrl"].ToString();
                foreach (var v in result)
                {
                    v.filename = v.filename.Replace(imgurl, "");
                    if (fieldI == 1)
                        sb.AppendFormat("<tr role=\"row\">");
                    sb.AppendFormat("<td class=\"class=\"left  sorting_1\">");
                    sb.AppendFormat("<table width=312 height=170 border=1>");
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td rowspan='6'><a href='" + v.filename + "'target='_blank'><img src='" + v.filename + "' width=150 height=170>");//内部table一行2列
                    sb.AppendFormat("</td>");
                    sb.AppendFormat("<td>相机名称：{0}", v.BasicInfoModel.INFRAREDCAMERANAME);
                    sb.AppendFormat("</td>");
                    sb.AppendFormat("</tr>"); //内部table一行2列
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>单位名称:{0}", v.BasicInfoModel.ORGNAME);
                    sb.AppendFormat("</td>");
                    sb.AppendFormat("</tr>");
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>接收号码:{0}", v.tpa);
                    sb.AppendFormat("</td>");
                    sb.AppendFormat("</tr>");
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>接收日期:{0}", v.recvdatetime);
                    sb.AppendFormat("</td>");
                    sb.AppendFormat("</tr>");
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>地址:{0}", v.BasicInfoModel.ADDRESS);
                    sb.AppendFormat("</td>");
                    sb.AppendFormat("</tr>");
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td><input type='checkbox' name='chk1' value='" + v.smid + "'/>是否删除");
                    sb.AppendFormat("</td>");
                    sb.AppendFormat("</tr>");
                    sb.AppendFormat("</table>");
                    sb.AppendFormat("</td>");
                    if (fieldI == fieldCount)
                        sb.AppendFormat("    </tr>");
                    if (fieldI == fieldCount)
                        fieldI = 1;
                    else
                        fieldI++;
                }
                if (sb.ToString().Substring(sb.ToString().Length - 5, 5) != "</tr>")
                    sb.AppendFormat("</tr>");
                sb.AppendFormat("</table>");
            }
            return sb.ToString();
        }

        #endregion

        #region 电子监控基本信息管理
        public ActionResult MONITOR_INFOManger()
        {
            JC_MONITOR_BASICINFO_Model m = new JC_MONITOR_BASICINFO_Model();
            m.EMID = Request.Params["EMID"];
            m.TTBH = Request.Params["TTBH"];
            m.EMNAME = Request.Params["EMNAME"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.GC = Request.Params["GC"];
            m.ADDRESS = Request.Params["ADDRESS"];
            m.IP = Request.Params["IP"];
            m.LOGINUSERNAME = Request.Params["LOGINUSERNAME"];
            m.USERPWD = Request.Params["USERPWD"];
            m.XH = Request.Params["XH"];
            m.PP = Request.Params["PP"];
            m.GD = Request.Params["GD"];
            m.Shape = "geometry::STGeomFromText('POINT(" + m.JD + " " + m.WD + ")',4326)";
            m.JCJL = Request.Params["JCJL"];
            m.OBJID = Request.Params["OBJID"];
            m.TEMPLATEDID = Request.Params["TEMPLATEDID"];
            m.PORT = Request.Params["PORT"];
            m.TYPE = Request.Params["TYPE"];
            m.opMethod = Request.Params["Method"];
            m.returnUrl = Request.Params["returnUrl"];
            if (string.IsNullOrEmpty(m.returnUrl))
                m.returnUrl = "/YJJC/MONITOR_INFOList";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间,请重新输入!", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间,请重新输入!", "")), "text/html;charset=UTF-8");
                }
            }
            return Content(JsonConvert.SerializeObject(JC_MONITORCls.Manager(m)));
        }

        public ActionResult MONITOR_INFOMan()
        {
            pubViewBag("006008", "006008", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.T_Method = Request.Params["Method"];
            //如果未传参数,默认为添加
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });// ipsuM.ORGNAME });
            string ID = Request.Params["ID"];
            ViewBag.T_ID = ID;
            if (string.IsNullOrEmpty(ID) == false)
            {
                if (string.IsNullOrEmpty(ViewBag.T_ID))
                    return Redirect("/System/Error?ID=2");//参数错误  
            }
            if (string.IsNullOrEmpty(ViewBag.T_Method))
                ViewBag.T_Method = "Add";
            return View();
        }
      
        public ActionResult GetMONITOR_INFOJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            return Content(JsonConvert.SerializeObject(JC_MONITORCls.getModel(new JC_MONITOR_BASICINFO_SW { EMID = ID })), "text/html;charset=UTF-8");
        }

        public ActionResult MONITOR_INFOList()
        {
            pubViewBag("006008", "006008", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.isAdd = (SystemCls.isRight("006008001")) ? "1" : "0";
            return View();
        }

        public ActionResult getMONITOR_INFOListAjax()
        {
            string TTBH = Request.Params["TTBH"];
            string BYORGNO = Request.Params["BYORGNO"];
            JC_MONITOR_BASICINFO_SW sw = new JC_MONITOR_BASICINFO_SW { BYORGNO = BYORGNO, TTBH = TTBH };
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr role=\"row1\">");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>单位名称</th>");
            sb.AppendFormat("<th>设备编号</th>");
            sb.AppendFormat("<th>监控名称</th>");
            sb.AppendFormat("<th>型号</th>");
            sb.AppendFormat("<th>品牌</th>");
            sb.AppendFormat("<th>经度</th>");
            sb.AppendFormat("<th>纬度</th>");
            sb.AppendFormat("<th>高程(m)</th>");
            sb.AppendFormat("<th>高度(m)</th>");
            sb.AppendFormat("<th>监控距离(km)</th>");
            sb.AppendFormat("<th>详细地址</th>");
            sb.AppendFormat("<th></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = JC_MONITORCls.getListModel(sw);
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORGNAME);
                sb.AppendFormat("<td class=\"center\"><a class=\"green\"  href='#' onclick=\"See('{1}','See')\"title='编辑'>{0}</td>", v.TTBH, v.EMID);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.EMNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.XH);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PP);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.JD);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.WD);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.GC);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.GD);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.JCJL);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ADDRESS);
                sb.AppendFormat("<td class=\"center\">");
                if (string.IsNullOrEmpty(v.JD) || string.IsNullOrEmpty(v.WD))
                    sb.AppendFormat("<a  href=\"javascript:void(0);\" title='定位' style=\"background-color:gray;\" class=\"searchBox_01 LinkLocation\">定位</a>");
                else
                    sb.AppendFormat("<a href=\"#\" onclick=\" Position('JC_MONITOR_BASICINFO','{0}','{1}')\" title='定位' class=\"searchBox_01 LinkLocation\">定位</a>", v.EMID, v.EMNAME);
                if (SystemCls.isRight("006008003") == true)
                    sb.AppendFormat(" <a  href='#' onclick=\"Mdy('{0}','Mdy')\" title='修改' class=\"searchBox_01 LinkMdy\">修改</a>", v.EMID);
                if (SystemCls.isRight("006008004") == true)
                    sb.AppendFormat("&nbsp;<a href='#' onclick='Manager(\"{0}\")' title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.EMID);
                if (SystemCls.isRight("006008002") == true)
                    sb.AppendFormat("&nbsp;<a href='#' onclick='VidewView(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\")' class=\"searchBox_01 LinkDel\">视频查看</a>", v.BYORGNO, v.EMID, v.EMNAME, v.ORGNAME, v.TYPE);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }
        #endregion

        #region 群众报警管理
        /// <summary>
        /// 群众报警管理 增、删、该
        /// </summary>
        /// <returns></returns>      
        public ActionResult PERALARMManger()
        {
            JC_PERALARM_Model m = new JC_PERALARM_Model();
            string PERALARMID = Request.Params["PERALARMID"];
            string PERALARMPHONE = Request.Params["PERALARMPHONE"];
            string FIRENAME = Request.Params["FIRENAME"];
            string PERALARMNAME = Request.Params["PERALARMNAME"];
            string PERALARMTIME = Request.Params["PERALARMTIME"];
            string PERALARMADDRESS = Request.Params["PERALARMADDRESS"];
            string PERALARMCONTENT = Request.Params["PERALARMCONTENT"];
            string MANSTATE = Request.Params["MANSTATE"];
            string MANRESULT = Request.Params["MANRESULT"];
            string MANTIME = Request.Params["MANTIME"];
            string MANUSERID = Request.Params["MANUSERID"];
            string ManUserName = Request.Params["ManUserName"];
            string BYORGNO = Request.Params["BYORGNO"];
            string Method = Request.Params["Method"];
            string returnUrl = Request.Params["returnUrl"];
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/YJJC/PERALARMList";
            m.PERALARMID = PERALARMID;
            m.PERALARMPHONE = PERALARMPHONE;
            m.FIRENAME = FIRENAME;
            m.PERALARMNAME = PERALARMNAME;
            m.PERALARMTIME = PERALARMTIME;
            m.PERALARMADDRESS = PERALARMADDRESS;
            m.PERALARMCONTENT = PERALARMCONTENT;
            m.MANSTATE = MANSTATE;
            m.MANRESULT = MANRESULT;
            m.MANTIME = MANTIME;
            m.BYORGNOLIST = Request.Params["BYORGNOLIST"];
            m.MANUSERID = SystemCls.getUserID();
            m.ManUserName = ManUserName;
            m.BYORGNO = BYORGNO;
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.PEARLARMPRE = Request.Params["PEARLARMPRE"];
            m.PEARLARMISSUED = Request.Params["PEARLARMISSUED"];
            m.opMethod = Method;
            m.returnUrl = returnUrl;
            //if (MANSTATE == "0")
            //    m.MANRESULT = "待处理";
            //if (MANSTATE == "1")
            //    m.MANRESULT = "已处理：非火情";
            //if (MANSTATE == "2")
            m.MANSTATE = "2";
            m.MANRESULT = "已处理：火情";
            if (m.opMethod != "Del")
            {
                if (string.IsNullOrEmpty(m.BYORGNOLIST))
                    return Content(JsonConvert.SerializeObject(new Message(false, "区域不可为空,请选择区域", "")), "text/html;charset=UTF-8");
                if (string.IsNullOrEmpty(m.JD) == false)
                {
                    if (float.Parse(m.JD) >= 180 || float.Parse(m.JD) <= -180)
                        return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间,请重新输入!", "")), "text/html;charset=UTF-8");
                }
                if (string.IsNullOrEmpty(m.WD) == false)
                {
                    if (float.Parse(m.WD) >= 90 || float.Parse(m.WD) <= -90)
                        return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间,请重新输入!", "")), "text/html;charset=UTF-8");
                }
            }
            string dttime = "";
            string ph = "";
            var msg = JC_PERALARMCls.Manager(m);
            string[] arr = m.BYORGNOLIST.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                JC_FIRE_Model m1 = new JC_FIRE_Model();
                m1.opMethod = "Add";
                m1.PFTIME = DateTime.Now.ToString();
                m1.FIRETIME = m.PERALARMTIME;
                m1.ZQWZ = m.PERALARMADDRESS;
                m1.FIRENAME = m.FIRENAME;
                m1.MARK = m.PERALARMCONTENT;
                m1.BYORGNO = arr[i];
                m1.FIREFROM = "3";
                m1.JD = m.JD;
                m1.WD = m.WD;
                dttime = m.PERALARMTIME;
                ph = m.PERALARMPHONE;
                m1.RECEIVETIME = DateTime.Now.ToString();
                var m2 = JC_PERALARMCls.getModel(new JC_PERALARM_SW { PERALARMTIME = dttime, BYORGNO = arr[i], PERALARMPHONE = ph });
                m1.FIREFROMID = m2.PERALARMID;
                JC_FIRECls.Manager(m1);
            }
            return Content(JsonConvert.SerializeObject(msg));
        }

        public ActionResult getPERALARMJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            var m = JC_PERALARMCls.getModel(new JC_PERALARM_SW { PERALARMID = ID });
            if (string.IsNullOrEmpty(m.MANUSERID))
                m.MANUSERID = SystemCls.getUserID();
            if (string.IsNullOrEmpty(m.PERALARMTIME))
                m.PERALARMTIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now.ToString());
            if (string.IsNullOrEmpty(m.MANTIME))
                m.MANTIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now.ToString());
            return Content(JsonConvert.SerializeObject(m));
        }

        public ActionResult PERALARMListQuery()
        {
            string PageSize = Request.Params["PageSize"];
            string Page = Request.Params["Page"];
            string DateBegin = Request.Params["DateBegin"];
            string DateEnd = Request.Params["DateEnd"];
            string PERALARMPHONE = Request.Params["PERALARMPHONE"];
            string PERALARMNAME = Request.Params["PERALARMNAME"];
            string BYORGNO = Request.Params["BYORGNO"];
            string MANSTATE = Request.Params["MANSTATE"];
            string str = ClsStr.EncryptA01((PageSize + "|" + DateBegin + "|" + DateEnd + "|" + PERALARMPHONE + "|" + PERALARMNAME + "|" + BYORGNO + "|" + MANSTATE), "kkkkkkkk"); ;
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/YJJC/PERALARMList?trans=" + str + "&page=" + Page)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 电话报警增加
        /// </summary>
        /// <returns></returns>
        public ActionResult PERALARMMan()
        {
            pubViewBag("008002", "008002", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.Method = Request.Params["Method"];
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            string ID = Request.Params["ID"];
            ViewBag.T_ID = ID;
            if (string.IsNullOrEmpty(ID) == false)
            {
                if (string.IsNullOrEmpty(ViewBag.T_ID))
                    return Redirect("/System/Error?ID=2");//参数错误  
                string otid = Request.Params["tNo"];
                if (string.IsNullOrEmpty(otid))
                    return Redirect("/System/Error?ID=3");//参数错误 
                if (ClsStr.EncryptA01(ViewBag.T_ID, "kdiekdfd") != otid)
                    return Redirect("/System/Error?ID=4");//参数错误 
            }
            if (string.IsNullOrEmpty(ViewBag.Method))
                ViewBag.Method = "Add";
            return View();
        }

        /// <summary>
        /// 电话报警
        /// </summary>
        /// <returns></returns>
        public ActionResult PERALARMList()
        {
            pubViewBag("008002", "008002", "");
            if (ViewBag.isPageRight == false)
                return View();
            string page = Request.Params["page"];
            if (string.IsNullOrEmpty(page))
                page = "1";
            string trans = Request.Params["trans"];
            string[] arr = new string[7];
            if (string.IsNullOrEmpty(trans) == false)
                arr = ClsStr.DecryptA01(trans, "kkkkkkkk").Split('|');
            else
                arr[5] = SystemCls.getCurUserOrgNo();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = arr[5], TopORGNO = SystemCls.getCurUserOrgNo() });
            if (string.IsNullOrEmpty(arr[0]) == true)
                arr[0] = PagerCls.getDefaultPageSize().ToString();
            ViewBag.PERALARMPHONE = arr[3];
            ViewBag.PERALARMNAME = arr[4];
            ViewBag.DateBegin = arr[1];
            ViewBag.DateEnd = arr[2];
            ViewBag.MANSTATE = arr[6];
            if (string.IsNullOrEmpty(arr[2]) == false) arr[2] = arr[2];
            if (string.IsNullOrEmpty(arr[3]) == false) arr[3] = arr[3];
            int total = 0;
            ViewBag.PERALARMList = getPERALARMListStr(new JC_PERALARM_SW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), PERALARMPHONE = arr[3], PERALARMNAME = arr[4], DateBegin = arr[1], DateEnd = arr[2], BYORGNO = arr[5], MANSTATE = arr[6] }, out total);
            //分页信息 需使用上面的total
            ViewBag.PagerInfo = PagerCls.getPagerInfo_New(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), rowCount = total, url = "/YJJC/PERALARMList?trans=" + trans });
            return View();
        }

        private string getPERALARMListStr(JC_PERALARM_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr role=\"row\">");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>发生地</th>");
            sb.AppendFormat("<th>报警人</th>");
            sb.AppendFormat("<th>报警电话号码</th>");
            sb.AppendFormat("<th>报警时间</th>");
            sb.AppendFormat("<th>详细地址</th>");
            sb.AppendFormat("<th>处理人</th>");
            //sb.AppendFormat("<th>处理状态</th>");
            //sb.AppendFormat("<th>操作</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = JC_PERALARMCls.getListModelPager(sw, out total);
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                string tNo = ClsStr.EncryptA01(v.PERALARMID, "kdiekdfd");
                sb.AppendFormat("<td class=\"center\">{0}</td>", ((sw.curPage - 1) * sw.pageSize + i + 1).ToString());
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.ORGNAME);
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.PERALARMNAME);
                sb.AppendFormat("<td class=\"center\"><a href=\"/YJJC/PERALARMMan?Method=See&ID={0}&tNo={1}\" title='查看'>{2}</a></td>", v.PERALARMID, tNo, v.PERALARMPHONE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PERALARMTIME);
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.PERALARMADDRESS);
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.ManUserName);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// 卫星报警
        /// </summary>
        /// <returns></returns>
        public ActionResult SatelliteAlarmIndex()
        {
            pubViewBag("008016", "008016", "");
            ViewBag.AlarmTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOptionByWX(new T_SYS_ORGSW { });
            return View();
        }

        public ActionResult SatelliteAlarmSave()
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string WXBH = Request.Params["WXBH"];
            string RDBH = Request.Params["RDBH"];
            string BYORGNAME = Request.Params["BYORGNAME"];
            string BYORGNO = Request.Params["BYORGNO"];
            string XS = Request.Params["XS"];
            string JD = Request.Params["JD"];
            string WD = Request.Params["WD"];
            string JD_DFM = Request.Params["JD_DFM"];
            string WD_DFM = Request.Params["WD_DFM"];
            string NEWJD = "";
            string NEWWD = "";
            string ALARMTIME = Request.Params["ALARMTIME"];
            string MARK = Request.Params["MARK"];
            string FireName = WXBH + " " + BYORGNAME + " " + date + " " + "卫星热点火情";
            if (JD == "" && WD == "")
            {
                NEWJD = float.Parse(ClsMapCommon.ConvertDegreesToDigital(JD_DFM).ToString()).ToString("F6");
                NEWWD = float.Parse(ClsMapCommon.ConvertDegreesToDigital(WD_DFM).ToString()).ToString("F6");
            }
            else
            {
                NEWJD = JD;
                NEWWD = WD;
            }
            if (float.Parse(NEWJD) >= 180 || float.Parse(NEWJD) <= -180)
                return Content(JsonConvert.SerializeObject(new Message(false, "经度范围在-180~180之间,请重新输入!", "")), "text/html;charset=UTF-8");
            if (float.Parse(NEWWD) >= 90 || float.Parse(NEWWD) <= -90)
                return Content(JsonConvert.SerializeObject(new Message(false, "纬度范围在-90~90之间,请重新输入!", "")), "text/html;charset=UTF-8");
            JC_FIRE_Model m = new JC_FIRE_Model();
            m.FIRENAME = FireName;
            m.BYORGNO = BYORGNO;
            m.FIREFROM = "2";
            m.FIRETIME = ALARMTIME;
            m.ISOUTFIRE = "0";
            m.MARK = MARK;
            m.JD = NEWJD;
            m.WD = NEWWD;
            m.ZQWZ = BYORGNAME;
            m.WXBH = WXBH;
            m.DQRDBH = RDBH;
            m.RSMJ = XS;
            m.RECEIVETIME = DateTime.Now.ToString();
            m.FIREFROMWEATHER = "2";//1 表示气象卫星接收 2 表示人工补录  0 表示其他
            m.returnUrl = "/YJJC/SatelliteAlarmIndex";
            m.opMethod = "AddWX";
            return Content(JsonConvert.SerializeObject(JC_FIRECls.Manager(m)));
        }

        #region 获取多组织机构的combox
        /// <summary>
        /// 获取多组织机构的combox
        /// </summary>
        /// <returns></returns>
        public string PERALARMCheckOrgJson()
        {
            var str = T_SYS_ORGCls.getOrgJsonStr(new T_SYS_ORGSW { });
            return str;
        }
        #endregion
        #endregion

        #region 火线等级上传
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return Content("没有文件!", "text/plain");
            }
            var filepath = Path.Combine(Request.MapPath("~/Upload"), Path.GetFileName(file.FileName));
            try
            {
                if (filepath != null)
                {
                    file.SaveAs(filepath);
                    StreamReader sr = new StreamReader(filepath, Encoding.UTF8);///读取文件对象
                    //StringBuilder sb = new StringBuilder();//可变长度的字符串---用于读取 filepath 的所有内容  
                    int rowNum = 0;//用于记录filepath文本内容的行数----以便实例化数组
                    //读取文件内容 并记录行数
                    string str = "";
                    string dttime = "";
                    while ((str = sr.ReadLine()) != null)
                    {
                        if (str == null)
                        {
                            break;
                        }
                        if (rowNum == 0)
                        {
                            dttime = "20" + str.Trim();
                        }
                        else
                        {
                            string[] strs = str.ToString().Split(' ');
                            YJ_DANGERCLASS_Model m1 = new YJ_DANGERCLASS_Model();
                            m1.BYORGNO = strs[0];
                            m1.DANGERCLASS = strs[3];
                            m1.JD = strs[1];
                            m1.WD = strs[2];
                            m1.TOPTOWNNAME = strs[4];
                            m1.DCDATE = dttime;
                        }
                        rowNum++;

                    }
                }
            }
            catch
            {
                return Content("上传异常!", "text/plain");
            }
            return View();
        }
        #endregion
    }
}
