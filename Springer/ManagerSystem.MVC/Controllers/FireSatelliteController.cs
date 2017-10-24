using ManagerSystem.MVC.Models;
using ManagerSystemClassLibrary;
using ManagerSystemClassLibrary.BaseDT;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
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
    public class FireSatelliteController : BaseController
    {
        #region 气象预报
        /// <summary>
        /// 气象预报
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLevelIndex()
        {
            pubViewBag("021001", "021001", "");
            if (ViewBag.isPageRight == false)
                return View();
            var DCDATE = Request.Params["DCDATE"];
            ViewBag.time = DCDATE;
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOptionBYWeather(new T_SYS_ORGSW { });
            StringBuilder sb = new StringBuilder();

            #region 天气预报
            sb.AppendFormat("<table id=\"tableweather\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>区域</th><th>天气</th><th>气温</th><th>风向风速</th><th>火险等级</th><th>日期</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");

            int j = 0;
            int l = 0;
            var list = T_ALL_AREACls.getListModel(new T_ALL_AREA_SW());
            if (Request.Params["savePath"] != "" && Request.Params["savePath"] != null)//文件模板导入
            {
                string filePath = Server.UrlDecode(Request.Params["savePath"]);
                HSSFWorkbook hssfworkbook;
                try
                {
                    using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        hssfworkbook = new HSSFWorkbook(file);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
                int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1
                for (int i = (sheet.FirstRowNum + 2); i <= rowCount; i++)
                {
                    IRow row = sheet.GetRow(i);
                    string[] arr = new string[5];
                    for (int k = 0; k < arr.Length; k++)
                    {
                        arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容                      
                    }
                    //地区 天气 气温 风向风速 火险等级  日期
                    if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[4]))
                    {
                        continue;
                    }
                    if (YJ_DANGERCLASSCls.isExists(new YJ_DANGERCLASS_SW { DCDATE = DCDATE, TOWNNAME = arr[0] }))
                    {
                        return Content(@"<script>alert('导入失败，该日期已经有数据!');window.location.href='FireLevelIndex';</script>");
                    }
                    else
                    {
                        j++;
                        var AREACODE = T_ALL_AREACls.getModel(new T_ALL_AREA_SW { AREAJC = arr[0] }).AREACODE;
                        sb.AppendFormat("<tr class='danger'>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + arr[0] + "\" code=\"" + AREACODE + "\" readonly=\"readonly\"  id=\"" + "tbxAddr_" + j + "\"  name=\"" + "tbxAddr_" + j + "\"/>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + arr[1] + "\" id=\"" + "tbxWeather_" + j + "\" name=\"" + "tbxWeather_" + j + "\"/>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + arr[2] + "\" id=\"" + "tbxTemp_" + j + "\" name=\"" + "tbxTemp_" + j + "\"/>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + arr[3] + "\" id=\"" + "tbxWind_" + j + "\" name=\"" + "tbxWind_" + j + "\"/>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + arr[4] + "\" id=\"" + "tbxDangerClass_" + j + "\" name=\"" + "tbxDangerClass_" + j + "\"/>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"" + "tbxTime_" + j + "\" type=\"text\" value=\"" + Convert.ToDateTime(DCDATE).ToString("yyyy-MM-dd") + "\" class=\"Wdate\" style=\"width:98%;\" onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd'})\"  />");
                    }
                }
                sb.AppendFormat("</tr>");
            }
            else //查询添加
            {
                foreach (var item in list)
                {
                    if (PublicCls.OrgIsXian(item.AREACODE) == true)//统计市，即所有县的
                    {
                        if (Request.Params["Add"] == "add")//添加
                        {
                            j++;
                            sb.AppendFormat("<tr class='danger'>");
                            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + item.AREAJC + "\"  id=\"" + "tbxAddr_" + j + "\"  code=\"" + item.AREACODE + "\" readonly=\"readonly\" name=\"" + "tbxAddr_" + j + "\"/>");
                            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\"  id=\"" + "tbxWeather_" + j + "\" name=\"" + "tbxWeather_" + j + "\"/>");
                            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\"  id=\"" + "tbxTemp_" + j + "\" name=\"" + "tbxTemp_" + j + "\"/>");
                            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\"  id=\"" + "tbxWind_" + j + "\" name=\"" + "tbxWind_" + j + "\"/>");
                            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\"  id=\"" + "tbxDangerClass_" + j + "\" name=\"" + "tbxDangerClass_" + j + "\"/>");
                            sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"" + "tbxTime_" + j + "\" type=\"text\" value=\"" + Convert.ToDateTime(DCDATE).ToString("yyyy-MM-dd") + "\"  class=\"Wdate\" style=\"width:98%;\" onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd'})\"  />");
                        }
                        else
                        {
                            var li = YJ_DANGERCLASSCls.getListModelArea(new YJ_DANGERCLASS_SW { TOWNNAME = item.AREAJC, DCDATE = DCDATE });
                            foreach (var i in li)
                            {
                                j++;
                                sb.AppendFormat("<tr class='danger'>");
                                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + i.TOWNNAME + "\" id=\"" + "tbxAddr_" + j + "\" code=\"" + item.AREACODE + "\" readonly=\"readonly\" name=\"" + "tbxAddr_" + j + "\"/>");
                                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + i.WEATHER + "\" id=\"" + "tbxWeather_" + j + "\" name=\"" + "tbxWeather_" + j + "\"/>");
                                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + i.TEMPREATURE + "\" id=\"" + "tbxTemp_" + j + "\" name=\"" + "tbxTemp_" + j + "\"/>");
                                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + i.WINDYSPEED + "\" id=\"" + "tbxWind_" + j + "\" name=\"" + "tbxWind_" + j + "\"/>");
                                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + i.DANGERCLASS + "\" id=\"" + "tbxDangerClass_" + j + "\" name=\"" + "tbxDangerClass_" + j + "\"/>");
                                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"" + "tbxTime_" + j + "\" type=\"text\" value=\"" + Convert.ToDateTime(i.DCDATE).ToString("yyyy-MM-dd") + "\" class=\"Wdate\" style=\"width:98%;\" onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd'})\"  />");
                            }
                        }
                    }
                    sb.AppendFormat("</tr>");
                }
            }
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td colspan=\"6\" class=\"center\">{0}</td>", "<input type=\"button\" value=\"保存\" id=\"addWeather\" onclick=\"SaveFireLevel()\" class=\"btnSaveCss\" />");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            #endregion

            sb.AppendFormat("<br /><br />");

            #region 卫星热点
            sb.AppendFormat("<table id=\"Satellite\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>区域</th><th>火点像元个数(像素)</th> <th>经度</th><th>纬度</th><th>日期</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");

            if (Request.Params["QUERY"] == "query")
            {
                //查询的
                var li = JC_FIRECls.GetListModel(new JC_FIRE_SW { FIRETIME = DCDATE, FIREFROMWEATHER = "1" });
                foreach (var item in li)
                {
                    l++;
                    sb.AppendFormat("<tr><td><select id=\"" + "tbxBYORGNO_" + l + "\" readonly=\"readonly\" style=\"width:98%;\">" + ViewBag.vdOrg + "</select></td>");
                    sb.AppendFormat("<td><input id=\"" + "tbxNum_" + l + "\" name=\"" + "tbxNum_" + l + "\" codee=\"" + item.BYORGNO + "\" type=\"text\" value=\"" + item.RSMJ + "\" style=\"width:98%;\" /></td>");
                    sb.AppendFormat("<td><input id=\"" + "tbxJD_" + l + "\"  type=\"text\" value=\"" + item.JD + "\"  style=\"width:98%;\" /></td>");
                    sb.AppendFormat("<td><input id=\"" + "tbxWD_" + l + "\" type=\"text\" value=\"" + item.WD + "\" style=\"width:98%;\" /></td>");
                    sb.AppendFormat("<td colspan=\"3\" class=\"center\">{0}</td>", "<input id=\"" + "tbxHotTime_" + l + "\" type=\"text\" value=\"" + Convert.ToDateTime(item.FIRETIME).ToString("yyyy-MM-dd HH:mm:ss") + "\" class=\"Wdate\" onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss'})\"  style=\"width:98%;\" />");
                    sb.AppendFormat("</tr>");
                }
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan=\"5\" class=\"center\">{0}</td>", "<input type=\"button\" value=\"添加\" id=\"addHot\" onclick=\"addTr()\" class=\"btnAddCss\" /> <input type=\"button\" value=\"删除\" id=\"delHot\" onclick=\"delTr()\" class=\"btnDelCss\" /> <input type=\"button\" value=\"保存\" id=\"saveHot\" onclick=\"SaveSatelliteHot()\" class=\"btnSaveCss\" />");
                sb.AppendFormat("</tr>");
            }
            else
            {
                sb.AppendFormat("<tr id=\"1\"><td><select id=\"tbxBYORGNO_1\" style=\"width:98%;\">" + ViewBag.vdOrg + "</select></td>");
                sb.AppendFormat("<td><input id=\"tbxNum_1\" type=\"text\" style=\"width:98%;\" /></td>");
                sb.AppendFormat("<td><input id=\"tbxJD_1\" type=\"text\" style=\"width:98%;\" /></td>");
                sb.AppendFormat("<td><input id=\"tbxWD_1\" type=\"text\" style=\"width:98%;\" /></td>");
                sb.AppendFormat("<td colspan=\"3\" class=\"center\">{0}</td>", "<input class=\"Wdate\" onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss'})\" id=\"tbxHotTime_1\" type=\"text\" value=\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\" style=\"width:98%;\" />");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan=\"5\" class=\"center\">{0}</td>", "<input type=\"button\" value=\"添加\" id=\"addHot\" onclick=\"addTr()\" class=\"btnAddCss\" /> <input type=\"button\" value=\"删除\" id=\"delHot\" onclick=\"delTr()\" class=\"btnDelCss\" /> <input type=\"button\" value=\"保存\" id=\"saveHot\" onclick=\"SaveSatelliteHot()\" class=\"btnSaveCss\" />");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            #endregion

            ViewBag.pageList = sb.ToString();
            return View();
        }

        #endregion

        #region 火险等级导入
        /// <summary>
        /// 火险等级导入
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLevelImport()
        {
            pubViewBag("021002", "021002", "");
            var result = GetModelList();
            return View(result);
        }

        /// <summary>
        /// 火险等级导入时查询
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLevelQuery()
        {
            string DCDATE = Request.Params["DCDATE"];
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/FireSatellite/FireLevelIndex?DCDATE=" + DCDATE + "&QUERY=query")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 火险等级上传
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FireLevelIndex(FormCollection form)
        {
            pubViewBag("021001", "021001", "");
            string date = Request.Form["tbxQueryTime"];
            if (date == "")
            {
                return Content("<script>alert('请选择要导入的日期!');window.location.href='FireLevelIndex';</script>");
            }
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
                    string name = DateTime.Now.ToString("火险等级导入-yyyyMMddHHmmss!") + extension;
                    if (!FileType.Contains(extension))
                    {
                        return Content(@"<script>alert('文件类型不对，只能导入xls和xlsx格式的文件!');history.go(-1);</script>");
                    }
                    if (filesize >= Maxsize)
                    {
                        return Content(@"<script>alert('上传文件超过4M，不能上传!');history.go(-1);</script>");
                    }
                    try
                    {
                        string virthpath = "/UploadFile/HXDJExcel";
                        string filePath = Server.MapPath("~" + virthpath);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        savePath = Path.Combine(filePath, name);
                        File.SaveAs(savePath);
                        var EncodeSavepath = Server.UrlEncode(savePath);//编码                       
                        return Content("<script>window.location.href='FireLevelIndex?savePath=" + EncodeSavepath + "&DCDATE=" + date + "'</script>");
                    }
                    catch (Exception)
                    {
                        return Content(@"<script>alert('上传文件模板错误，请确认后再上传!');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content(@"<script>alert('请选择需要导入的火险等级表格!');history.go(-1);</script>");
                }
            }
            return View();
        }

        /// <summary>
        /// 火险等级导入时保存
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLevelSave()
        {
            YJ_DANGERCLASS_Model m = new YJ_DANGERCLASS_Model();
            m.DANGERCLASS = Request.Params["DANGERCLASS"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.DCDATE = Request.Params["DCDATE"];
            m.TOWNNAME = Request.Params["BYORGNAME"];
            m.WEATHER = Request.Params["WEATHER"];
            m.TEMPREATURE = Request.Params["TEMPREATURE"];
            m.WINDYSPEED = Request.Params["WINDYSPEED"];
            m.opMethod = "PLAdd";
            return Content(JsonConvert.SerializeObject(YJ_DANGERCLASSCls.Manager(m)));
        }

        /// <summary>
        /// 获取火险等级模型列表
        /// </summary>
        /// <returns></returns>
        private IEnumerable<YJJCFireLevelModel> GetModelList()
        {
            var result = new List<YJJCFireLevelModel>();
            var list = YJ_DANGERCLASSCls.getListModelTop(new YJ_DANGERCLASS_SW() { });
            if (list.Any())
            {
                foreach (var item in list.OrderByDescending(p => p.DANGERCLASS))
                {
                    var model = new YJJCFireLevelModel();
                    if (!string.IsNullOrEmpty(item.TOPTOWNNAME))
                    {
                        model.AreaName = item.TOPTOWNNAME + "==>";
                    }
                    model.AreaName += item.TOWNNAME;//区域（乡镇）
                    model.FireLevel = item.DANGERCLASS;//等级
                    model.LevelDate = ClsSwitch.SwitDate(item.DCDATE);//等级时间
                    model.JD = item.JD;//经度
                    model.WD = item.WD;//纬度
                    model.OWnAreaName = item.TOPTOWNNAME;//上级单位（市县）
                    model.SourceForm = "人工导入";
                    result.Add(model);
                }
            }
            return result;
        }
        #endregion

        #region 卫星热点
        /// <summary>
        ///卫星热点
        /// </summary>
        /// <returns></returns>
        public ActionResult SatelliteHotIndex()
        {
            //pubViewBag("021002", "021002", "");
            //ViewBag.vdOrg = T_SYS_ORGCls.getSelectOptionBYWeather(new T_SYS_ORGSW { });
            //ViewBag.vdOrg = T_ALL_AREACls.getSelectOption(new T_ALL_AREA_SW());
            return View();
        }

        /// <summary>
        /// 卫星热点保存
        /// </summary>
        /// <returns></returns>
        public ActionResult SatelliteHotSave()
        {
            JC_FIRE_Model m = new JC_FIRE_Model();
            m.BYORGNO = Request.Params["BYORGNO"];
            m.FIREFROM = "2";
            m.FIRETIME = Request.Params["FIRETIME"];
            m.ISOUTFIRE = "0";
            m.JD = Request.Params["JD"];
            m.WD = Request.Params["WD"];
            m.ZQWZ = Request.Params["BYORGNAME"];
            m.WXBH = "QX-01";
            m.RSMJ =  Request.Params["RSMJ"];
            m.RECEIVETIME = DateTime.Now.ToString();
            m.FIREFROMWEATHER = "1"; //1 表示气象卫星接收  0 表示其他
            m.returnUrl = "/FireSatellite/FireLevelIndex";
            m.opMethod = "PLAdd";
            return Content(JsonConvert.SerializeObject(JC_FIRECls.Manager(m)));
        }

        #endregion

        #region 火险等级录入
        /// <summary>
        /// 火险等级录入
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLevelInput()
        {
            pubViewBag("021003", "021003", "");
            if (ViewBag.isPageRight == false)
                return View();
            var DCDATE = Request.Params["DCDATE"];
            var AREACODE = Request.Params["AREACODE"];
            string defaultAreaCode = "", defaultAreaName = "";
            string areaOption = T_ALL_AREACls.getSelectOptionBYWeather(new T_ALL_AREA_SW { OnlyGetShiXian = "true", CurAREACODE = AREACODE, TOPAREACODE = ConfigCls.getTopAreaCode() }, out defaultAreaCode, out defaultAreaName);
            DCDATE = string.IsNullOrEmpty(DCDATE) ? DateTime.Now.ToString("yyyy-MM-dd") : DCDATE;
            AREACODE = !string.IsNullOrEmpty(AREACODE) ? AREACODE : defaultAreaCode;
            List<T_ALL_AREA_Model> tempList = T_ALL_AREACls.getListModeBYAREACODE(new T_ALL_AREA_SW { CurAREACODE = AREACODE }).ToList();
            ViewBag.Time = DCDATE;
            int j = 0;
            StringBuilder sb = new StringBuilder();

            #region 数据表
            sb.AppendFormat("<table id=\"FireLevelInput\" cellpadding=\"0\" cellspacing=\"0\">");

            #region 表头
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>区域</th><th>天气</th><th>气温</th><th>风向风速</th><th>火险等级</th><th>时间</th></tr>");
            sb.AppendFormat("</thead>");
            #endregion

            if (areaOption.Length > 0)
            {
                YJ_DANGERCLASS_Model m = YJ_DANGERCLASSCls.getModel(new YJ_DANGERCLASS_SW { BYORGNO = AREACODE, DCDATE = DCDATE });

                #region 表身
                sb.AppendFormat("<tbody id=\"tcontent\" >");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"center\" >");
                sb.AppendFormat("<select id=\"tbxArea\" name=\"tbxArea\" class=\"center\"  style=\"width:98%;\" onchange=\"AreaChange()\" >" + areaOption + "</select>");
                sb.AppendFormat("<input id=\"tbxArea0\" name=\"tbxArea0\"  type=\"hidden\" value=\"" + defaultAreaName + "\"  />");
                sb.AppendFormat("<input id=\"hidArea0\" name=\"hidArea0\"  type=\"hidden\" value=\"" + defaultAreaCode + "\"  />");
                sb.AppendFormat("</td>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxWeather0\" name=\"tbxWeather0\" type=\"text\" style=\"width:98%;\" class=\"center\" value=\"" + m.WEATHER + "\"  />");
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxTemp0\" name=\"tbxTemp0\" type=\"text\" style=\"width:98%;\" class=\"center\" value=\"" + m.TEMPREATURE + "\"  />");
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxWind0\" name=\"tbxWind0\" type=\"text\" style=\"width:98%;\" class=\"center\" value=\"" + m.WINDYSPEED + "\" />");
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxDangerClass0\" name=\"tbxDangerClass0\" style=\"width:98%;\" type=\"text\" class=\"center\" value=\"" + m.DANGERCLASS + "\"  />");
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxTime0\" name=\"tbxTime0\" type=\"text\" style=\"width:98%;\" class=\"Wdate\" onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd'})\"   value=\"" + Convert.ToDateTime(DCDATE).ToString("yyyy-MM-dd") + "\" />");
                sb.AppendFormat("</tr>");
                for (int i = 0; i < tempList.Count; i++)
                {
                    j++;
                    YJ_DANGERCLASS_Model tm = YJ_DANGERCLASSCls.getModel(new YJ_DANGERCLASS_SW { BYORGNO = tempList[i].AREACODE, DCDATE = DCDATE });
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\">{0}{1}</td>",
                        "<input id=\"tbxArea" + j + "\" name=\"tbxArea" + j + "\" type=\"text\" style=\"width:98%;\" class=\"center\" value=\"" + tempList[i].AREANAME + "\" readonly=\"true\" />",
                        "<input id=\"hidArea" + j + "\" name=\"hidArea" + j + "\" type=\"hidden\"  value=\"" + tempList[i].AREACODE + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxWeather" + j + "\" name=\"tbxWeather" + j + "\" type=\"text\" style=\"width:98%;\" class=\"center\" value=\"" + tm.WEATHER + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxTemp" + j + "\" name=\"tbxTemp" + j + "\" type=\"text\" style=\"width:98%;\" class=\"center\" value=\"" + tm.TEMPREATURE + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxWind" + j + "\" name=\"tbxWind" + j + "\" type=\"text\"  style=\"width:98%;\" class=\"center\" value=\"" + tm.WINDYSPEED + "\" />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxDangerClass" + j + "\" name=\"tbxDangerClass" + j + "\" style=\"width:98%;\" type=\"text\" class=\"center\" value=\"" + tm.DANGERCLASS + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxTime" + j + "\"  name=\"tbxTime" + j + "\" type=\"text\" style=\"width:98%;\" class=\"Wdate\" onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd'})\"   value=\"" + Convert.ToDateTime(DCDATE).ToString("yyyy-MM-dd") + "\" />");
                    sb.AppendFormat("</tr>");
                }
                sb.AppendFormat("</tbody>");
                #endregion
            }
            sb.AppendFormat("</table>");
            #endregion

            ViewBag.pageList = sb.ToString();
            ViewBag.Save = (SystemCls.isRight("021003001")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 火险等级录入时查询
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLevelInputQuery()
        {
            string DCDATE = Request.Params["DCDATE"];
            string AREACODE = Request.Params["AreaCode"];
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/FireSatellite/FireLevelInput?DCDATE=" + DCDATE + "&AREACODE=" + AREACODE)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 区域发生改变
        /// </summary>
        public ActionResult AreaChange()
        {
            var DCDATE = Request.Params["DCDATE"];
            var AREACODE = Request.Params["AREACODE"];
            string defaultAreaCode = "", defaultAreaName = "";
            string areaOption = T_ALL_AREACls.getSelectOptionBYWeather(new T_ALL_AREA_SW { OnlyGetShiXian = "true", CurAREACODE = AREACODE, TOPAREACODE = ConfigCls.getTopAreaCode() }, out defaultAreaCode, out defaultAreaName);
            int j = 0;
            List<T_ALL_AREA_Model> areaList = T_ALL_AREACls.getListModeBYAREACODE(new T_ALL_AREA_SW { CurAREACODE = AREACODE }).ToList();
            StringBuilder sb = new StringBuilder();
            if (areaOption.Length > 0)
            {
                YJ_DANGERCLASS_Model m = YJ_DANGERCLASSCls.getModel(new YJ_DANGERCLASS_SW { BYORGNO = AREACODE, DCDATE = DCDATE });
                sb.AppendFormat("<tr>");
                sb.Append("<td class=\"center\" >");
                sb.AppendFormat("<select id=\"tbxArea\" name=\"tbxArea\" class=\"center\" style=\"width:98%;\" onchange=\"AreaChange()\" >" + areaOption + "</select>");
                sb.AppendFormat("<input id=\"tbxArea0\" name=\"tbxArea0\"  type=\"hidden\" value=\"" + defaultAreaName + "\"  />");
                sb.AppendFormat("<input id=\"hidArea0\" name=\"hidArea0\"  type=\"hidden\" value=\"" + defaultAreaCode + "\"  />");
                sb.AppendFormat("</td>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxWeather0\" name=\"tbxWeather0\" style=\"width:98%;\" type=\"text\" class=\"center\" value=\"" + m.WEATHER + "\"  />");
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxTemp0\" name=\"tbxTemp0\"  style=\"width:98%;\" type=\"text\" class=\"center\" value=\"" + m.TEMPREATURE + "\"  />");
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxWind0\" name=\"tbxWind0\" style=\"width:98%;\" type=\"text\" class=\"center\" value=\"" + m.WINDYSPEED + "\" />");
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxDangerClass0\" name=\"tbxDangerClass0\"  style=\"width:98%;\" type=\"text\" class=\"center\" value=\"" + m.DANGERCLASS + "\"  />");
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxTime0\" name=\"tbxTime0\" type=\"text\" style=\"width:98%;\" class=\"Wdate\" onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd'})\"   value=\"" + Convert.ToDateTime(DCDATE).ToString("yyyy-MM-dd") + "\" />");
                sb.AppendFormat("</tr>");
                for (int i = 0; i < areaList.Count; i++)
                {
                    j++;
                    YJ_DANGERCLASS_Model tm = YJ_DANGERCLASSCls.getModel(new YJ_DANGERCLASS_SW { BYORGNO = areaList[i].AREACODE, DCDATE = DCDATE });
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center\">{0}{1}</td>",
                            "<input id=\"tbxArea" + j + "\" name=\"tbxArea" + j + "\" type=\"text\" style=\"width:98%;\" class=\"center\" value=\"" + areaList[i].AREANAME + "\" readonly=\"true\" />",
                            "<input id=\"hidArea" + j + "\" name=\"hidArea" + j + "\" type=\"hidden\"  value=\"" + areaList[i].AREACODE + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxWeather" + j + "\" name=\"tbxWeather" + j + "\" type=\"text\" class=\"center\"  style=\"width:98%;\" value=\"" + tm.WEATHER + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxTemp" + j + "\" name=\"tbxTemp" + j + "\" type=\"text\"  class=\"center\" style=\"width:98%;\" value=\"" + tm.TEMPREATURE + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxWind" + j + "\" name=\"tbxWind" + j + "\" type=\"text\"  class=\"center\" style=\"width:98%;\"  value=\"" + tm.WINDYSPEED + "\" />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxDangerClass" + j + "\" name=\"tbxDangerClass" + i + "\" type=\"text\" class=\"center\" style=\"width:98%;\" value=\"" + tm.DANGERCLASS + "\"  />");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxTime" + j + "\"  name=\"tbxTime" + j + "\" type=\"text\" class=\"Wdate\" style=\"width:98%;\" onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd'})\"   value=\"" + Convert.ToDateTime(DCDATE).ToString("yyyy-MM-dd") + "\" />");
                    sb.AppendFormat("</tr>");
                }
            }
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// 火险等级录入时保存
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLevelInputSave()
        {
            YJ_DANGERCLASS_Model m = new YJ_DANGERCLASS_Model();
            m.DCDATE = Request.Params["DCDATE"];
            m.BYORGNO = Request.Params["BYORGNO"];
            m.DANGERCLASS = Request.Params["DANGERCLASS"];
            m.TOWNNAME = Request.Params["TOWNNAME"];
            m.WEATHER = Request.Params["WEATHER"];
            m.TEMPREATURE = Request.Params["TEMPREATURE"];
            m.WINDYSPEED = Request.Params["WINDYSPEED"];
            m.TOPTOWNNAME = Request.Params["TOPTOWNNAME"];
            m.opMethod = "PLAdd2";
            return Content(JsonConvert.SerializeObject(YJ_DANGERCLASSCls.Manager(m)));
        }
        #endregion
    }
}
