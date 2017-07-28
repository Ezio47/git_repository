using ManagerSystem.MVC.HelpCom;
using ManagerSystem.MVC.Models;
using ManagerSystemClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using NPOI.SS.UserModel;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using Aspose.Words;
using System.Data;
using PublicClassLibrary;
using ManagerSystemClassLibrary.SDECLS;
using ManagerSystemModel.SDEModel;


namespace ManagerSystem.MVC.Controllers
{
    public class MainDefaultController : BaseController
    {
        

        /// <summary>
        /// 系统用户在线界面
        /// </summary>
        /// <returns></returns>
        public ActionResult OnLineIndex()
        {
            return View();
        }

        /// <summary>
        /// 护林员在线界面
        /// </summary>
        /// <returns></returns>
        public ActionResult HlyOnLineIndex()
        {
            return View();
        }


        #region 获取热点个数

        /// <summary>
        /// 获取热点个数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetHotInfoJson()
        {
            var result = new List<HotInfoModel>();
            var sw = new JC_FIRE_SW();
            var curogr = SystemCls.getCurUserOrgNo();
            var bo = PublicCls.OrgIsShi(curogr);
            var bb = PublicCls.OrgIsXian(curogr);
            var bx = PublicCls.OrgIsZhen(curogr);
            if (!bo)
            {
                sw.BYORGNO = curogr;
            }
            sw.isCountIndex = "1";
            var list = JC_FIRECls.GetListModel(sw);
            StringBuilder sb = new StringBuilder();
            //遍历火情来源
            Array ary = Enum.GetValues(typeof(EnumType));  //array是数组的基类, 无法实例化
            bool blnAlarm = false;//是否报警
            foreach (int item in ary)
            {
                if (item.ToString() != "1")  //&& item.ToString() != "6")
                {
                    string[] arr = new string[5];
                    int wqs = 0;
                    arr[0] = "0";
                    if (list.Any())
                    {
                        var firelist = list.Where(p => p.FIREFROM == item.ToString() && !string.IsNullOrEmpty(p.FIREFROM) && p.ISOUTFIRE != "1" && p.MANSTATE != "19" && p.MANSTATE != "18");//筛选热点类型 排除已灭的  && ((p.ISOUTFIRE.Trim() == "1" && p.MANSTATE.Trim() != "4") || p.ISOUTFIRE.Trim() != "1") 已在程序中处理
                        arr[0] = firelist.Count().ToString();//热点个数
                        //签收
                        if (bo)//市
                        {
                            arr[1] = JC_FIRECls.GetCount("1", item.ToString(), "", "0").ToString();
                            wqs = JC_FIRECls.GetCount("1", item.ToString(), "", "1");
                            arr[2] = wqs.ToString();
                        }
                        else if (bb)//县
                        {
                            //32 为本级单位签收
                            arr[1] = JC_FIRECls.GetCount("2,32", item.ToString(), curogr.Substring(0, 6), "0").ToString();
                            wqs = JC_FIRECls.GetCount("2,32", item.ToString(), curogr.Substring(0, 6), "1");
                            arr[2] = wqs.ToString();
                        }
                        else//乡镇
                        {
                            arr[1] = JC_FIRECls.GetCount("3", item.ToString(), curogr, "0").ToString();
                            wqs = JC_FIRECls.GetCount("3", item.ToString(), curogr, "1");
                            arr[2] = wqs.ToString();
                        }
                        //上报 反馈
                        if (bo)//市
                        {
                            arr[3] = firelist.Where(p => p.MANSTATE == "19" || p.MANSTATE == "18").ToList().Count.ToString();
                            arr[4] = firelist.Where(p => p.MANSTATE == "11" || p.MANSTATE == "15").ToList().Count.ToString();
                        }
                        else if (bb)//县
                        {
                            arr[3] = firelist.Where(p => p.MANSTATE == "11" || p.MANSTATE == "19" || p.MANSTATE == "15" || p.MANSTATE == "18").ToList().Count.ToString();
                            var wfkcount = firelist.Where(p => p.MANSTATE == "31" || p.MANSTATE == "33" || p.MANSTATE == "34").ToList().Count;
                            string count = "0";
                            if (wqs <= wfkcount)//未反馈中去除未签收
                            {
                                count = (wfkcount - wqs).ToString();
                            }
                            arr[4] = count;
                        }
                        else//乡镇
                        {
                            arr[3] = firelist.Where(p => Convert.ToInt32(p.MANSTATE) > 0 && Convert.ToInt32(p.MANSTATE) < 50).ToList().Count.ToString();
                            var wfkcount = firelist.Where(p => p.MANSTATE == "0" || p.MANSTATE == "51").ToList().Count;
                            string count = "0";
                            if (wqs <= wfkcount)//未反馈中去除未签收
                            {
                                count = (wfkcount - wqs).ToString();
                            }
                            //arr[4] = "未反馈:" + count;
                            arr[4] = count;
                        }
                    }
                    else
                    {
                        arr[1] = "0";
                        arr[2] = "0";
                        arr[3] = "0";
                        arr[4] = "0";
                    }
                    string strclass = "wxjk";//卫星报警
                    if (item == 3)
                    {
                        strclass = "dhbj";//电话报警
                    }
                    else if (item == 4)
                    {
                        strclass = "dzjk";//电子监控
                    }
                    else if (item == 5)
                    {
                        strclass = "lwhly";//瞭望护林员
                    }
                    else if (item == 6)
                    {
                        strclass = "wrjxh";//无人机巡护
                    }
                    if (arr[2] != "0" || arr[4] != "0")//未签收 未上报报警
                        blnAlarm = true;
                    sb.AppendFormat("<dl class=\"hqjc\">");
                    sb.AppendFormat("<dt class=\"floatLeft center\">");
                    sb.AppendFormat("<p><span class=\"same {1} floatLeft\"></span>|<span class=\"cor_ff0 floatRight\">{0}</span></p>", arr[0], strclass);
                    //sb.AppendFormat("<p><a href='/MainYJJC/YJJCNIndex?typeid={1}' target='_blank'>{0}</a></p>", Enum.GetName(typeof(EnumType), item), item.ToString());
                    sb.AppendFormat("<p><a onClick=\"ShowFires({1})\" title=\"查看\">{0}</a></p>", Enum.GetName(typeof(EnumType), item), item.ToString());
                    sb.AppendFormat("</dt>");
                    sb.AppendFormat("<dd class=\"floatLeft\">");
                    sb.AppendFormat("<p>已签收:<span class=\"cor_4d9\">{0}</span></p>", arr[1]);
                    sb.AppendFormat("<p>未签收:<span class=\"cor_ff7\">{0}</span></p>", arr[2]);
                    sb.AppendFormat("<p>已上报:<span class=\"cor_4d9\">{0}</span></p>", arr[3]);
                    sb.AppendFormat("<p>未上报:<span class=\"cor_ff7\">{0}</span></p>", arr[4]);
                    sb.AppendFormat("</dd>");
                    sb.AppendFormat("<div class=\"floatRight\" style=\" margin-top: -20px;\"><p><a href='/MainYJJC/YJJCNIndex?typeid={0}' target='_blank' title=\"查看更多信息\">更多</a> </p></div>", item.ToString());
                    sb.AppendFormat("<div class=\"clear\"></div>");
                    sb.AppendFormat("</dl>");
                }
            }
            if (blnAlarm)
            {
                sb.AppendFormat("<audio autoplay='autoplay'>        <source src='/Content/albram.mp3' type='audio/mpeg'>    </audio>");
            }
            string type = Request.Params["type"];//用于所有页面公用提醒
            if (type == "Alarm")
            {
                string a = SystemCls.getCurUserOrgNo();
                if (string.IsNullOrEmpty(SystemCls.getCurUserOrgNo()))
                    return Content(JsonConvert.SerializeObject(new Message(false, "", "")), "text/html;charset=UTF-8");
                if (blnAlarm)
                    return Content(JsonConvert.SerializeObject(new Message(true, "<audio autoplay='autoplay'>        <source src='/Content/albram.mp3' type='audio/mpeg'>    </audio><img src='/images/fireAlarm.gif' width='25' height='30'>", "")), "text/html;charset=UTF-8");
                else
                    return Content(JsonConvert.SerializeObject(new Message(true, "", "")), "text/html;charset=UTF-8");
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取热点个数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLineInfoJson()
        {
            var linemodel = T_SYSSEC_IPSUSERCls.getUserLineModel(new T_SYSSEC_IPSUSER_SW() { });
            T_IPSFR_USER_OnLine_Model hum = T_IPSFR_USERCls.getUserLineModel(new T_IPSFR_USER_SW { });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("用户数:{0}  在线:<a onclick=\"showPerson('1')\">", linemodel.LineCount);
            sb.AppendFormat("    <font face=\"arial\" color=\"green\"> {0}</font>", linemodel.LineInCount);
            sb.AppendFormat("</a> 不在线: <a onclick=\"showPerson('0')\"><font face=\"arial\" color=\"red\">{0}</font> </a><br />", linemodel.LineOutCount);
            sb.AppendFormat("护林员:{0}  在/离线:<a onclick=\"showHlyPerson('0')\"><font color=\"green\">{1}</font></a>/<a onclick=\"showHlyPerson('1')\"><font color=\"red\">{2}</font></a>出围:<a onclick=\"showHlyPerson('2')\"><font face=\"arial\" color=\"#DAA520\">{3} </font> </a><br />"
                , hum.LineCount , hum.LineInCount, hum.LineOutCount, hum.LineOutRouteCount );
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取护林员在线出围信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetHlyInfoCountJson()
        {
            MessageObject ms = new MessageObject(false, null);
            T_IPSFR_USER_OnLine_Model hum = T_IPSFR_USERCls.getUserLineModel(new T_IPSFR_USER_SW { });
            if (hum != null)
            {
                ms = new MessageObject(true, hum);
            }
            return Json(ms);
        }
        #endregion

        #region 火险等级
        /// <summary>
        /// 火险等级
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLeverIndex(string level = "")
        {
            if (string.IsNullOrEmpty(level))
            {
                ViewBag.loadFunc = "loadFireLevel()";
            }
            else
            {
                ViewBag.loadFunc = "loadFireLevelStr(" + level + ")";
            }
            var list = YJ_DANGERCLASSCls.getListModelTop(new YJ_DANGERCLASS_SW { });
            var model = new FireLevelCountModel();
            model.CountA = list.Count().ToString();
            model.CountB = list.Where(p => Convert.ToInt32(p.DANGERCLASS) >= 2).ToList().Count.ToString();
            model.CountC = list.Where(p => Convert.ToInt32(p.DANGERCLASS) >= 3).ToList().Count.ToString();
            model.CountD = list.Where(p => Convert.ToInt32(p.DANGERCLASS) >= 4).ToList().Count.ToString();
            model.CountE = list.Where(p => Convert.ToInt32(p.DANGERCLASS) >= 5).ToList().Count.ToString();
            if (list.Count()>0)
               model.Time = Convert.ToDateTime(list.FirstOrDefault().DCDATE).ToString("yyyy年MM月dd日");
            else
               model.Time = "";
            ViewBag.url = ConfigCls.getConfigValue("2DMAPUrl");
            return View(model);
        }

        #endregion

        #region 获取在线离线人员

        /// <summary>
        /// 获取在线离线人员
        /// </summary>
        /// <returns></returns>
        public JsonResult showLineInOrOut()
        {
            Message ms = null;
            string str = Request.Params["obj"];
            if (string.IsNullOrEmpty(str))
            {
                ms = new Message(false, "离在线参数传参错误", "");
                return Json(ms);
            }
            var personlist = new List<T_SYSSEC_IPSUSER_Pager_Model>();
            var linemodel = T_SYSSEC_IPSUSERCls.getUserLineModel(new T_SYSSEC_IPSUSER_SW() { });
            if (str == "0")//系统用户离线
            {
                personlist = linemodel.LineOutUserListModel.ToList();
            }
            else if (str == "1")//系统用户在线
            {
                personlist = linemodel.LineInUserListModel.ToList();
            }
            IEnumerable<T_SYSSEC_IPSUSER_Pager_Model> query = from items in personlist orderby items.LASTOPTIME descending select items;
            StringBuilder sb = new StringBuilder();
            int i = 0;
            sb.AppendFormat("<div class=\"divTable\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style='width:10%;'>序号</th>");
            sb.AppendFormat("<th style='width:15%;'>单位</th>");
            sb.AppendFormat("<th style='width:20%;'>姓名</th>");
            sb.AppendFormat("<th style='width:20%;'>电话</th>");
            sb.AppendFormat("<th style='width:35%;'>最后操作时间</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody role=\"alert\" aria-live=\"polite\" aria-relevant=\"all\">");
            foreach (var person in query)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", ++i);
                sb.AppendFormat("<td class=\"center\">{0}</td>", person.ORGNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", person.USERNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", person.PHONE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", person.LASTOPTIME);
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }
        #endregion

        #region 获取护林员离线 在线 出围
        /// <summary>
        /// 获取护林员离线 在线 出围
        /// </summary>
        /// <returns></returns>
        public JsonResult showHLYLineInOrOut()
        {
            Message ms = null;
            string str = Request.Params["obj"];
            string curUserOrg = SystemCls.getCurUserOrgNo();//获取当前登录用户的机构编码
            var dataResult = new List<T_IPSFR_USER_Model>();//护林员结果
            if (string.IsNullOrEmpty(str))
            {
                ms = new Message(false, "护林员离在出线参数传参错误", "");
                return Json(ms);
            }
            string hidstr = string.Empty;
            var sw = new T_IPSFR_USER_SW();
            if (str == "0")//护林员在线
            {
                hidstr = T_IPSFR_USERCls.GetHids(curUserOrg, "0");
            }
            else if (str == "1")//护林员离线
            {
                hidstr = T_IPSFR_USERCls.GetHids(curUserOrg, "1");
            }
            else if (str == "2")//护林员出围
            {
                hidstr = T_IPSFR_USERCls.GetHids(curUserOrg, "2");
            }
            sw.HID = hidstr;
            if (!string.IsNullOrEmpty(hidstr))
            {
                dataResult = T_IPSFR_USERCls.getListModel(sw).ToList();
            }
            StringBuilder sb = new StringBuilder();
            int i = 0;
            sb.AppendFormat("<div class=\"divTable\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>单位</th>");
            sb.AppendFormat("<th>姓名</th>");
            sb.AppendFormat("<th>电话</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody role=\"alert\" aria-live=\"polite\" aria-relevant=\"all\">");
            if (dataResult.Any())
            {
                foreach (var person in dataResult)
                {
                    if (i % 2 == 0)
                        sb.AppendFormat("<tr>");
                    else
                        sb.AppendFormat("<tr class='row1'>");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", ++i);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", person.ORGNAME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", person.HNAME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", person.PHONE);
                    sb.AppendFormat("</tr>");
                }
            }
            else
            {
                sb.AppendFormat("<td colspan=\"4\"><em>暂无信息</em></td>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }
        #endregion

        #region 获取待审核(反馈)个数
        /// <summary>
        /// 获取待审核(反馈)个数
        /// </summary>
        /// <returns></returns>
        public JsonResult getSHFKCount()
        {
            Message ms = null;
            int count = 0;
            var curog = SystemCls.getCurUserOrgNo();
            var bo = PublicCls.OrgIsShi(curog);//市
            var bb = PublicCls.OrgIsXian(curog);//县
            var bx = PublicCls.OrgIsZhen(curog);//镇
            if (bo)
            {
                var list = JC_FIRECls.GetListModel(new JC_FIRE_SW { MANSTATESTR = "11,15" });
                count = list.Count();
            }
            else if (bb)
            {
                var list = JC_FIRECls.GetListModel(new JC_FIRE_SW { MANSTATESTR = "31,32,33,34" });
                count = list.Count();
            }
            else if (bx)
            {
                var list = JC_FIRECls.GetListModel(new JC_FIRE_SW { MANSTATESTR = "0,51" });
                count = list.Count();
            }
            ms = new Message(true, count.ToString(), "");
            return Json(ms);
        }
        #endregion

        #region 获取热点个数

       
        #endregion

        #region 导出高火险等级报告

        //public FileResult DownloadFile()'
        //{
        //    DataSet ds = Person.GetPersonDataSet(new DataSet());

        //    MemoryStream stream = ExportTool.ExportDatasetToExcel(ds);
        //    stream.Seek(0, SeekOrigin.Begin);

        //    return File(stream, "application/vnd.ms-excel", "spreadsheet1.xls");
        //return File(byteArray, 'application/ms-word', 'wordtest' + '.doc');
        //}

        public ActionResult ExportFireLevelWord()
        {
            ////创建新的word文档
            //XWPFDocument doc = new XWPFDocument();
            ////新建段落 头部
            //XWPFParagraph p0 = doc.CreateParagraph();
            //p0.Alignment = ParagraphAlignment.CENTER;
            //XWPFRun r0 = p0.CreateRun();
            //r0.FontFamily = "宋体";
            //r0.FontSize = 35;
            //r0.SetColor("red");
            //r0.SetBold(true);
            //r0.SetText("红河州森林高火险警报");

            //XWPFParagraph p1 = doc.CreateParagraph();
            //p1.Alignment = ParagraphAlignment.CENTER;
            //XWPFRun r1 = p1.CreateRun();
            //r1.FontFamily = "仿宋_GB2312";
            //r1.FontSize = 16;
            //var str1 = DateTime.Now.Year.ToString() + "第XX期";
            //r1.SetText(str1);

            //XWPFParagraph p2 = doc.CreateParagraph();
            //p2.Alignment = ParagraphAlignment.CENTER;
            //XWPFRun r2 = p2.CreateRun();
            //r2.FontFamily = "仿宋_GB2312";
            //r2.FontSize = 16;
            ////红河州森林防火指挥部办公室       2016年3月31日
            //var str2 = "红河州森林防火指挥部办公室" + "          " + DateTime.Now.ToString("yyyy年MM月dd日");
            //r2.SetText(str2);


            ////保存文件到磁盘
            //FileStream out1 = new FileStream("D:\\doc\\simpleTable1.docx", FileMode.Create, FileAccess.Write);
            //doc.Write(out1);
            //out1.Close();

            //FileInfo file = new FileInfo("D:\\doc\\simpleTable1.docx");//文件保存路径及名称  
            ////注意: 文件保存的父文件夹需添加Everyone用户，并给予其完全控制权限
            //Response.Clear();
            //Response.ClearHeaders();
            //Response.Buffer = false;
            //Response.ContentType = "application/octet-stream";
            //Response.AppendHeader("Content-Disposition", "attachment;filename="
            //    + HttpUtility.UrlEncode("simpleTable1.docx", System.Text.Encoding.UTF8));
            //Response.AppendHeader("Content-Length", file.Length.ToString());
            //Response.WriteFile(file.FullName);
            //Response.Flush();                           //以上将生成的word文件发送至用户浏览器

            //System.IO.File.Delete("D:\\doc\\simpleTable1.docx");                 //清除服务端生成的word文件
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFireLevelFData() {
            Message ms = null;
            //提供数据源
            var value = System.Configuration.ConfigurationManager.AppSettings["FireLevelValue"].ToString();//火险等级以上
            if (string.IsNullOrEmpty(value.Trim()))
            {
                value = "4";
            }
            var resultlist = YJ_DANGERCLASSCls.getListModelTop(new YJ_DANGERCLASS_SW { }).Where(p => Convert.ToInt32(p.DANGERCLASS) >= 4);//获取最新4级别以上的火险信息
            if (!resultlist.Any())
            {
                ms = new Message(false, "无报告数据", "");
            }
            else {
                ms = new Message(true, "有报告数据", "");
            }
            return Json(ms);
        }

        /// <summary>
        /// 下载森林高火险警报
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadFireLevelFile()
        {
            string tmppath = Server.MapPath("~/UploadFile/MBDoc/firelevelmb.doc");
            string outputPath = Server.MapPath("~/UploadFile/Output/");
            Document doc = new Document(tmppath); //载入模板
            //提供数据源
            var value = System.Configuration.ConfigurationManager.AppSettings["FireLevelValue"].ToString();//火险等级以上
            if (string.IsNullOrEmpty(value.Trim()))
            {
                value = "4";
            }
            var resultlist = YJ_DANGERCLASSCls.getListModelTop(new YJ_DANGERCLASS_SW { }).Where(p => Convert.ToInt32(p.DANGERCLASS) >= 4);//获取最新4级别以上的火险信息
            var orgstr = string.Join("、", resultlist.Select(p => p.TOWNNAME).ToArray());

            var date=DateTime.Now;
            if (resultlist.Any())
            {
                date = Convert.ToDateTime(resultlist.FirstOrDefault().DCDATE);
            }
            else {
                 //return Content(@"<script>alert('无火险等级报告!');history.go(-1)</script>");
                return null;
            }
            
            var datafrom = date.ToString("MM月dd日 16时");
            var datato = date.AddDays(1).ToString("MM月dd日 16时");
            String[] fieldNames = new String[] { "year", "date", "orgstr", "datafrom", "datato" };
            Object[] fieldValues = new Object[] { DateTime.Now.Year.ToString(), DateTime.Now.ToString("yyyy年MM月dd日"), orgstr, datafrom, datato };
            //合并模版，相当于页面的渲染
            doc.MailMerge.Execute(fieldNames, fieldValues);

            #region DataTable数据集合拼接
            DataTable dt;
            dt = ConvertToDataTableLevelList(resultlist.ToList());
            dt.TableName = "PlaceList";
            doc.MailMerge.ExecuteWithRegions(dt);
            #endregion
            var TemplateName = DateTime.Now.ToString("yyyy年MM月dd日") + "森林高火险警报.doc";
            //保存合并后的文档
            doc.Save(outputPath + Guid.NewGuid().ToString().Replace("-", "") + ".doc");

            var docStream = new MemoryStream();
            doc.Save(docStream, SaveFormat.Doc);
            return File(docStream.ToArray(), "application/msword", TemplateName);
        }

        #endregion

        #region MyPrivate

        /// <summary>
        /// 火险等级 List转为Datable 
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        private DataTable ConvertToDataTableLevelList(List<YJ_DANGERCLASS_Model> tasks)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ContyName"));
            dt.Columns.Add(new DataColumn("Level"));
            dt.Columns.Add(new DataColumn("LevelDate"));
            foreach (var task in tasks)
            {
                var dr = dt.NewRow();
                dr["ContyName"] = task.TOWNNAME;
                dr["Level"] = task.DANGERCLASS;
                dr["LevelDate"] = ClsSwitch.SwitDate(task.DCDATE);
                dt.Rows.Add(dr);
            }
            tasks.Clear();
            return dt;
        }

        /// <summary>
        /// 获取热点个数
        /// </summary>
        /// <returns></returns>
        private IEnumerable<HotInfoModel> GetHotInfoList()
        {
            var result = new List<HotInfoModel>();
            var sw = new JC_FIRE_SW();
            var curogr = SystemCls.getCurUserOrgNo();
            var bb = PublicCls.OrgIsXian(curogr);
            if (bb)
            {
                sw.BYORGNO = curogr;
            }
            var list = JC_FIRECls.GetListModel(sw);

            //遍历火情来源
            Array ary = Enum.GetValues(typeof(EnumType));  //array是数组的基类, 无法实例化
            foreach (int item in ary)
            {
                var info = new HotInfoModel();
                info.HotType = item.ToString();
                info.HotName = Enum.GetName(typeof(EnumType), item);//名称
                if (list.Any())
                {
                    var firelist = list.Where(p => p.FIREFROM == info.HotType && ((p.ISOUTFIRE.Trim() == "1" && p.MANSTATE.Trim() != "4") || p.ISOUTFIRE.Trim() != "1"));//筛选热点类型 排除已灭的

                    info.HotSum = firelist.Count().ToString();//热点个数
                    if (bb)//县
                    {
                        info.QSSum = firelist.Where(p => p.MANSTATE != "1").ToList().Count.ToString();
                        info.WQSSum = firelist.Where(p => p.MANSTATE == "1").ToList().Count.ToString();
                    }
                    else
                    {
                        info.QSSum = firelist.Where(p => p.MANSTATE == "1").ToList().Count.ToString();
                        info.WQSSum = firelist.Where(p => p.MANSTATE == "0").ToList().Count.ToString();
                    }

                    if (bb)//县
                    {
                        info.FKSum = firelist.Where(p => p.MANSTATE == "3" || p.MANSTATE == "4").ToList().Count.ToString();
                        info.WFKSum = firelist.Where(p => p.MANSTATE == "2").ToList().Count.ToString();
                    }
                    else
                    {
                        info.FKSum = firelist.Where(p => p.MANSTATE == "4").ToList().Count.ToString();
                        info.WFKSum = firelist.Where(p => p.MANSTATE == "3").ToList().Count.ToString();
                    }
                }
                else
                {
                    info.HotSum = "0";
                    info.QSSum = "0";
                    info.WQSSum = "0";
                    info.FKSum = "0";
                    info.WFKSum = "0";
                }
                result.Add(info);
            }
            return result;
        }

        private List<ART_DOCUMENT_Model> GetList1(ART_DOCUMENT_SW sw)
        {
            var list = ART_DOCUMENTCls.getModelList(sw).ToList();
            return list;
        }
        private string GetList(ART_DOCUMENT_SW sw)
        {
            var list = ART_DOCUMENTCls.getModelList(sw).ToList();
            string str = "";
            int newSCount = 0;
            foreach (var v in list)
            {
                newSCount++;
                str += "<li>\r\n";
                str += "<div class=\"text\"><a href='/ArtDocument/DocShow?ID=" + v.ARTID + "' target=\"_blank\">" + v.ARTTITLE + "</a></div>";
                str += "<div class=\"right\">" + v.ARTTIME + "</div>";
                str += "</li>";
            }
            for (int i = 0; i < Convert.ToInt16(ConfigCls.getTopNewsTopCount()) - newSCount; i++)
            {
                str += "<li>\r\n";
                //str += "<p style=\"color:red;\">暂无消息</p>";

                str += "<div class=\"text\"></div>";
                str += "<div class=\"right\"></div>";
                str += "</li>";
            }
            return str;
        }
        #endregion

        #region Ajax
        /// <summary>
        /// ajax 获取各种火情数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFireInfosAjax()
        {
            int total = 0;//记录总数
            string topOrg = "";
            var firetype = Request.Params["firetype"];//火情类型
            string PageSize = Request.Params["PageSize"];//记录个数
            string page = Request.Params["page"];//页数
            if (string.IsNullOrEmpty(firetype))
            {
                return Json(new Message(false, "没有火情类型", ""));
            }
            var currOrgNo = SystemCls.getCurUserOrgNo();
            if (PublicCls.OrgIsXian(currOrgNo))//县
            {
                topOrg = currOrgNo.Substring(0, 6);
            }
            else if (PublicCls.OrgIsZhen(currOrgNo))//乡镇
            {
                topOrg = currOrgNo;
            }
            var result = JC_FIRECls.getModelPager(new JC_FIRE_SW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), FIREFROM = firetype, TopORGNO = topOrg }, out total);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>区域</th>");
            //sb.AppendFormat("<th>经度</th>");
            //sb.AppendFormat("<th>纬度</th>");
            sb.AppendFormat("<th>接收时间</th>");
            //sb.AppendFormat("<th>周围（公里）</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            if (result.Any())
            {
                int i = 0;
                int rowB = (int.Parse(page) - 1) * int.Parse(PageSize);
                foreach (var s in result)
                {
                    if (i % 2 == 0)
                        // onclick="moveto(@item.JD,@item.WD,'@item.FIRENAME',@item.JCFID);"
                        sb.AppendFormat("<tr style=\"cursor: pointer;\" title='双击行定位' ondblclick=\"moveto(" + s.JD + "," + s.WD + ",'" + s.FIRENAME + "'," + s.JCFID + ")\">");
                    else
                        sb.AppendFormat("<tr style=\"cursor: pointer;\" title='双击行定位' class='row1'  ondblclick=\"moveto(" + s.JD + "," + s.WD + ",'" + s.FIRENAME + "'," + s.JCFID + ")\">");
                    sb.AppendFormat("<td>{0}</td>", ++rowB);
                    if (s.FIREFROM == "5")//护林员报警
                    {
                        sb.AppendFormat("<td><font color=\"#FF0000;\">{0}</font></td>", StateSwitch.GetOrgNameByOrgNO(s.BYORGNO));
                    }
                    else
                    {
                        sb.AppendFormat("<td><font color=\"#FF0000;\">{0}</font></td>", s.ZQWZ);
                    }
                    //sb.AppendFormat("<td>{0}</td>", Convert.ToDouble(s.JD).ToString("f3"));
                    //sb.AppendFormat("<td>{0}</td>", Convert.ToDouble(s.WD).ToString("f3"));
                    sb.AppendFormat("<td>{0}</td>", Convert.ToDateTime(s.RECEIVETIME).ToString("MM-dd HH:mm"));
                    //sb.AppendFormat("<td><select onchange=\"GetHlyInfos(" + s.JD + "," + s.WD + ",'" + s.FIRENAME + "'," + s.JCFID + ")\" id=\"areaselect_{0}\" ><option value=\"0\">请选择</option><option value=\"1\">1</option><option value=\"2\">2</option><option value=\"3\">3</option><option value=\"4\">4</option><option value=\"5\">5</option>/select></td>", s.JCFID);
                    sb.AppendFormat("</tr>");
                    ++i;
                }
            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan='5'>未查询出结果</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total, hidePageList = true, hidePageSize = true });
            return Json(new MessagePagerAjax(true, sb.ToString(), pageInfo));
        }
        #endregion
    }
}
