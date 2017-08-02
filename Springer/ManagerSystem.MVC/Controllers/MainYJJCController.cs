using ManagerSystem.MVC.HelpCom;
using ManagerSystem.MVC.Models;
using ManagerSystemClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using Newtonsoft.Json;
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
    public class MainYJJCController : BaseController
    {
       

        /// <summary>
        /// 预警监测首页
        /// </summary>
        /// <returns></returns>
        public ActionResult YJJCIndex()
        {
            pubViewBag("008009", "008009", "");
            var result = GetModelList();
            var list = YJ_SATELLITECLOUDCls.getListModelTop(new YJ_SATELLITECLOUD_SW() { TopCount = "1" });//卫星云图
            ViewBag.ytsy = list;
            return View(result);
        }

        /// <summary>
        /// 预警监测首页1
        /// </summary>
        /// <returns></returns>
        public ActionResult YJJCNIndex()
        {
            string typeid = Request.Params["typeid"];
            if (string.IsNullOrEmpty(typeid))
                typeid = "2";
            string pageCode = "04000" + typeid;
            //pubViewBag("008009", "008009", "");
            pubViewBag(pageCode, pageCode, "");
            ViewBag.typeID = typeid;

            #region
            //StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("<ul>");
            //sb.AppendFormat("    <li class=\" title {0}\" id=\"li-2\"><a data-toggle=\"tab\" href=\"#home\" onclick=\"FireAjax('2')\">卫星监控</a></li>", (typeid == "2") ? "liCur" : "");
            //sb.AppendFormat("    <li class=\" title fkqk {0}\" id=\"li-5\"><a data-toggle=\"tab\" href=\"#profile\" onclick=\"FireAjax('5')\">瞭望护林员</a></li>", (typeid == "5") ? "liCur" : "");
            //sb.AppendFormat("    <li class=\" title spjc {0}\" id=\"li-4\"><a data-toggle=\"tab\" href=\"#elecfile\" onclick=\"FireAjax('4')\">电子监控</a></li>", (typeid == "4") ? "liCur" : "");
            //sb.AppendFormat("    <li class=\" title fjjj {0}\" id=\"li-6\"><a data-toggle=\"tab\" href=\"#planefile\" onclick=\"FireAjax('6')\">无人机巡护</a></li>", (typeid == "6") ? "liCur" : "");
            //sb.AppendFormat("    <li class=\" title dhjj {0}\" id=\"li-3\"><a data-toggle=\"tab\" href=\"#phonefile\" onclick=\"FireAjax('3')\">电话报警</a></li>", (typeid == "3") ? "liCur" : "");
            //sb.AppendFormat("</ul>");
            //ViewBag.Menu = sb.ToString();
            #endregion

            //var list = new List<JC_FIRE_Model>();
            //string orgno = "";
            //bool bo = PublicCls.OrgIsShi(SystemCls.getCurUserOrgNo());//市机构
            //if (!bo)
            //{
            //    bool bc = PublicCls.OrgIsZhen(SystemCls.getCurUserOrgNo());//乡镇机构
            //    if (bc)
            //    {
            //        orgno = SystemCls.getCurUserOrgNo();//
            //    }
            //    else
            //    {
            //        orgno = SystemCls.getCurUserOrgNo().Substring(0, 6);//
            //    }
            //}
            //if (string.IsNullOrEmpty(orgno))
            //{
            //    list = JC_FIRECls.GetListModel(new JC_FIRE_SW { }).Where(p => p.ISOUTFIRE.Trim() != "1" && p.MANSTATE != "19" && p.MANSTATE != "18").ToList();//不筛选出火已灭的记录并且已经上报（非火情）19为市（州）已经上报  // && p.MANSTATE != "19"
            //}
            //else
            //{
            //    list = JC_FIRECls.GetListModel(new JC_FIRE_SW { }).Where(p => p.BYORGNO.StartsWith(orgno.ToString()) && p.ISOUTFIRE.Trim() != "1" && p.MANSTATE != "19" && p.MANSTATE != "18").ToList();//不筛选出火已灭的记录
            //}
            // //<li class="title" id="li-2" onclick="FireAjax('2')"><label class="wxjc"></label>卫星监控(@ViewBag.wxJcCount)</li>
            // //           <li class="title" id="li-5" onclick="FireAjax('5')"><label class="fkqk"></label>瞭望护林员(@ViewBag.hlyJcCount)</li>
            // //           <li class="title " id="li-4" onclick="FireAjax('4')"><label class="spjc"></label>电子监控(@ViewBag.dzJcCount)</li>
            // //           <li class="title " id="li-6" onclick="FireAjax('6')"><label class="fjjc"></label>无人机巡护(@ViewBag.planeJcCount)</li>
            // //           <li class="title " id="li-3" onclick="FireAjax('3')"><label class="dhjc"></label>电话报警(@ViewBag.phoneJcCount)</li>

            //ViewBag.wxJcCount = list.Where(p => p.FIREFROM == "2").Count();
            //ViewBag.hlyJcCount = list.Where(p => p.FIREFROM == "5").Count();
            //ViewBag.dzJcCount = list.Where(p => p.FIREFROM == "4").Count();
            //ViewBag.planeJcCount = list.Where(p => p.FIREFROM == "6").Count();
            //ViewBag.phoneJcCount = list.Where(p => p.FIREFROM == "3").Count();
            //ViewBag.pfJcCount = list.Where(p => !string.IsNullOrEmpty(p.OWERJCFID) && p.OWERJCFID != "0").Count();
            return View();
        }

        /// <summary>
        /// 预警监测首页
        /// </summary>
        /// <returns></returns>
        public ActionResult YJJYCNIndexYL()
        {
            pubViewBag("008009", "008009", "");
            return View();
        }


        /// <summary>
        /// 预警响应
        /// </summary>
        /// <returns></returns>
        public ActionResult YJXYIndex()
        {
            pubViewBag("008014", "008014", "");
            var result = GetModelList();
            return View(result);
        }

        /// <summary>
        /// 火险等级Map
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLevelMapIndex()
        {
            pubViewBag("008010", "008010", "");
            return View();
        }

        /// <summary>
        /// 火险等级
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLevelIndex()
        {
            pubViewBag("008017", "008017", "火险等级(无map)");
            var result = GetModelList();
            return View(result);
        }

        /// <summary>
        /// 卫星云图
        /// </summary>
        /// <returns></returns>
        public ActionResult WxCloudImageIndex()
        {
            pubViewBag("008012", "008012", "");
            var list = YJ_SATELLITECLOUDCls.getListModelTop(new YJ_SATELLITECLOUD_SW() { TopCount = "48" });//卫星云图
            ViewBag.ytsy = list.ToList();
            return View();
        }

        /// <summary>
        /// 气象信息Map
        /// </summary>
        /// <returns></returns>
        public ActionResult MapWeatherIndex()
        {
            pubViewBag("008013", "008013", "");
            //ViewBag.loadFunc = "loadWeather()";
            ViewBag.url = ConfigCls.getConfigValue("2DMAPUrl");
            return View();
        }

        /// <summary>
        /// 气象信息
        /// </summary>
        /// <returns></returns>
        public ActionResult WeatherIndex()
        {
            pubViewBag("008018", "008018", "");
            var result = GetWeatherList();
            return View(result);
        }


        /// <summary>
        /// 气象信息首页展示
        /// </summary>
        /// <returns></returns>
        public ActionResult MapWeatherSYIndex()
        {
            ViewBag.loadFunc = "loadWeather()";
            return View();
        }

        /// <summary>
        /// 批量导出火情
        /// </summary>
        /// <returns></returns>
        public FileResult ExportFireExcel()
        {
            string OrgNo = Request.Params["OrgNo"];
            string Source = Request.Params["Source"];
            string Starttime = Request.Params["Starttime"];
            string Endtime = Request.Params["Endtime"];
            string HotType = Request.Params["HotType"];

            var sw = new JC_FIRE_SW();
            sw.BYORGNO = OrgNo.Trim();
            sw.FIREFROM = Source.Trim();
            sw.HOTTYPE = HotType.Trim();
            if (!string.IsNullOrEmpty(Starttime) && !string.IsNullOrEmpty(Endtime))
            {
                sw.BeginTime = Starttime.Trim();
                sw.EndTime = Endtime.Trim();
            }
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("热点区域");
            row1.CreateCell(2).SetCellValue("来源");
            row1.CreateCell(3).SetCellValue("快速反馈");
            row1.CreateCell(4).SetCellValue("经度");
            row1.CreateCell(5).SetCellValue("纬度");
            row1.CreateCell(6).SetCellValue("接收时间");
            row1.CreateCell(7).SetCellValue("像素");
            row1.CreateCell(8).SetCellValue("烟云");
            row1.CreateCell(9).SetCellValue("连续");
            row1.CreateCell(10).SetCellValue("地类");
            //获取list数据
            int total = 0;
            var resultlist = JC_FIRECls.GetListModelAndCount(sw, out total, "1");//获取记录
            //将数据逐步写入sheet1各个行
            if (resultlist.Any())
            {
                int i = 0;
                int j = 0;
                foreach (var item in resultlist)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(++j);//序号
                    rowtemp.CreateCell(1).SetCellValue(item.ORGNAME.ToString());//热点区域
                    if (!string.IsNullOrEmpty(item.FIREFROM))
                    {
                        rowtemp.CreateCell(2).SetCellValue(Enum.GetName(typeof(EnumType), Convert.ToInt32(item.FIREFROM)));
                    }
                    else
                    {
                        rowtemp.CreateCell(2).SetCellValue("");
                    }
                    var record = JC_FIRETICKLINGCls.GetFKFireInfoData(item.JCFID);//获取火情反馈信息
                    rowtemp.CreateCell(3).SetCellValue(StateSwitch.DicStateNameByDicTypeID("10", record.JC_FireFKData.HOTTYPE));
                    rowtemp.CreateCell(4).SetCellValue(item.JD.ToString());
                    rowtemp.CreateCell(5).SetCellValue(item.WD.ToString());
                    rowtemp.CreateCell(6).SetCellValue(item.RECEIVETIME.ToString());
                    rowtemp.CreateCell(7).SetCellValue(item.RSMJ);
                    rowtemp.CreateCell(8).SetCellValue(StateSwitch.DicStateNameByDicTypeID("11", record.JC_FireFKData.YY.ToString()));
                    rowtemp.CreateCell(9).SetCellValue(StateSwitch.DicStateNameByDicTypeID("12", record.JC_FireFKData.JXHQSJ.ToString()));
                    rowtemp.CreateCell(10).SetCellValue(StateSwitch.DicStateNameByDicTypeID("7", record.JC_FireFKData.DL.ToString()));
                    ++i;
                }
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", Url.Encode("火情导出.xls"));
        }

        /// <summary>
        /// 预警响应（火情处理）
        /// </summary>
        /// <returns></returns>
        public ActionResult YJResponseIndex()
        {
            pubViewBag("008011", "008011", "");
            if (ViewBag.isPageRight == false)
                return View();

            string page = Request.Params["page"];//当前页数
            string trans = Request.Params["trans"];//传递网页参数
            if (string.IsNullOrEmpty(page))
                page = "1";
            //查询条件
            string[] arr = new string[7];//存放查询条件的数组 根据实际存放的数据
            if (string.IsNullOrEmpty(trans) == false)
                arr = ClsStr.DecryptA01(trans, "kkkkkkkk").Split('|');
            if (string.IsNullOrEmpty(arr[0]) == true)
                arr[0] = PagerCls.getDefaultPageSize().ToString();//默认记录数

            var sw = new JC_FIRE_SW();
            var curorgno = SystemCls.getCurUserOrgNo();//获取当前机构
            var bo = PublicCls.OrgIsShi(curorgno);//是否为市级别
            if (!bo)//市以下级别
            {
                sw.BYORGNO = curorgno;
            }

            //todo
            // sw.FIREFROM = "2";//卫星热点
            sw.curPage = int.Parse(page);//当前页
            sw.pageSize = int.Parse(arr[0]);//每页行数
            if (!string.IsNullOrEmpty(arr[1]))//机构
            {
                sw.BYORGNO = arr[1];//热点区域
            }
            if (string.IsNullOrEmpty(arr[2]))
            {
                arr[2] = "";
            }
            sw.MANSTATE = arr[2];//处理状态
            sw.FIREFROM = arr[3];//火情来源
            sw.BeginTime = arr[4];//开始时间
            sw.EndTime = arr[5];//结束时间
            sw.HOTTYPE = arr[6];//热点类别
            ViewBag.starttime = arr[4];//开始时间
            ViewBag.endtime = arr[5];//结束时间

            int total = 0;
            //火情响应信息
            ViewBag.ResponseList = getFireStr(sw, out total);
            //分页信息 需使用上面的total
            ViewBag.PagerInfo = PagerCls.getPagerInfo_New(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), rowCount = total, url = "/MainYJJC/YJResponseIndex?trans=" + trans });
            //热点区域
            ViewBag.hotOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO = SystemCls.getCurUserOrgNo(), CurORGNO = arr[1] });//获取市县机构
            //热点状态
            //ViewBag.hotState = getSelectHotState(SystemCls.getCurUserOrgNo(), arr[2]);
            //热点来源
            ViewBag.hotSource = getSelectHotSource(arr[3]);
            //热点类别
            ViewBag.hotType = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "10", isShowAll = "1", DICTVALUE = arr[6] });//热点类别
            return View();
        }

        /// <summary>
        /// 预警响应翻页ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult YJResponseQuery()
        {
            string PageSize = Request.Params["PageSize"];
            string Page = Request.Params["Page"];
            string OrgNo = Request.Params["OrgNo"];
            string State = Request.Params["State"];
            string Source = Request.Params["Source"];
            string Starttime = Request.Params["Starttime"];
            string Endtime = Request.Params["Endtime"];
            string HotType = Request.Params["HotType"];
            string str = ClsStr.EncryptA01(PageSize + "|" + OrgNo + "|" + State + "|" + Source + "|" + Starttime + "|" + Endtime + "|" + HotType, "kkkkkkkk");
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/MainYJJC/YJResponseIndex?trans=" + str + "&page=" + Page)), "text/html;charset=UTF-8");
        }

        #region Ajax

        /// <summary>
        /// 火险等级文件导入
        /// </summary>
        /// <returns></returns>
        public JsonResult ExportFireLevlFile()
        {
            Message ms = null;
            //接收上传后的文件
            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["Filedata"];
            //判断上传的文件是否为空
            if (file != null)
            {
                string ipath = System.Configuration.ConfigurationManager.AppSettings["FireLevelTxtPath"].ToString();//相对路径
                string PhysicalPath = Server.MapPath(ipath + "\\");
                if (!Directory.Exists(PhysicalPath))//判断文件夹是否已经存在
                {
                    Directory.CreateDirectory(PhysicalPath);//创建文件夹
                }
                //保存路径
                string type = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);
                string newName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + "." + type;
                //  string path = PhysicalPath + file.FileName;
                try
                {
                    string path = PhysicalPath + newName;
                    //保存文件
                    file.SaveAs(path);
                    bool bo = ReadTxt(path);//读取txt 文件 入库
                    if (bo)
                    {
                        ms = new Message(true, "上传成功", "");
                    }
                    else
                    {
                        ms = new Message(false, "上传失败,检查导入文件格式", "");
                    }
                }
                catch (Exception)
                {
                    ms = new Message(false, "上传出错", "");
                }

            }
            return Json(ms);
        }


        /// <summary>
        /// 获取火情监测信息按类别获取
        /// </summary>
        /// <param name="type">火情类型（卫星 护林员 电话 视频 飞机）</param>
        /// <returns></returns>
        //红外相机 = 1,
        //   卫星热点 = 2,
        //   人工报警 = 3,
        //   电子报警 = 4,
        //   护林员火情上报 = 5
        //   无人机 = 6
        public JsonResult GetModelListBy(string type)
        {
            Message ms = null;
            var sw = new JC_FIRE_SW();
            if (type != "-1")
            {
                sw.FIREFROM = type;
            }
            var orgno = SystemCls.getCurUserOrgNo();//获取当前组织结构 
            bool bo = PublicCls.OrgIsShi(orgno);//市机构
            bool bb = PublicCls.OrgIsXian(orgno);//县机构
            var strorg = "";
            if (bo)
            {
                strorg = "";
            }
            else
            {
                if (bb)
                {
                    strorg = orgno.Substring(0, 6);
                }
                else
                {
                    strorg = orgno;
                }
            }
            var list = new List<JC_FIRE_Model>();
            if (string.IsNullOrEmpty(strorg))
            {
                list = JC_FIRECls.GetListModel(sw).Where(p => p.ISOUTFIRE.Trim() != "1" && p.MANSTATE != "19" && p.MANSTATE != "18").ToList();//不筛选出火已灭的记录//&& p.MANSTATE != "19"
            }
            else
            {
                list = JC_FIRECls.GetListModel(sw).Where(p => p.BYORGNO.StartsWith(strorg.ToString()) && p.ISOUTFIRE.Trim() != "1" && p.MANSTATE != "19" && p.MANSTATE != "18").ToList();//不筛选出火已灭的记录 // && p.MANSTATE != "19"
            }
            if (type == "-1")//派发核查列表
            {
                list = list.Where(p => !string.IsNullOrEmpty(p.OWERJCFID) && p.OWERJCFID != "0").ToList();
            }
            else
            {
                list = list.Where(p => string.IsNullOrEmpty(p.OWERJCFID) || p.OWERJCFID == "0").ToList();

            }
            var str = this.GetHtmlStr(type, list);
            ms = new Message(true, str, "");
            return Json(ms);
            // return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }


        /// <summary>
        /// 获取html标签
        /// </summary>
        /// <param name="type">火情类型（卫星 护林员 电话 视频 飞机）</param>
        /// <param name="list"></param>
        /// <returns></returns>
        private string GetHtmlStr(string type, List<JC_FIRE_Model> list)
        {
            var recordlist = new List<string>();
            var curorgno = SystemCls.getCurUserOrgNo();//当前机构码
            bool citybo = PublicCls.OrgIsShi(curorgno);
            bool contybo = PublicCls.OrgIsXian(curorgno);
            bool xzbo = PublicCls.OrgIsZhen(curorgno);
            var strstatus = "";
            StringBuilder sb = new StringBuilder();
            if (type == "2" || type == "-1")//卫星热点
            {
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<thead>");
                sb.AppendFormat("  <tr> ");
                sb.AppendFormat("  <th><input type=\"checkbox\" id=\"CHK_ALL\" onClick=\"checkfun()\"/>全选</th>");
                sb.AppendFormat("  <th>卫星编号</th>");
                sb.AppendFormat("  <th>区域</th>");
                sb.AppendFormat("  <th>像素</th>");
                sb.AppendFormat("  <th>经度</th>");
                sb.AppendFormat("  <th>纬度</th>");
                sb.AppendFormat("  <th>接收时间</th>");
                //sb.AppendFormat("  <th>上报时间</th>");
                sb.AppendFormat("  <th>来源</th>");
                sb.AppendFormat("  <th>状态</th>");
                sb.AppendFormat("  <th>操作</th>");
                //if (type != "-1")
                //{
                //    sb.AppendFormat("  <th>人工派发</th>");
                //}
                sb.AppendFormat("   </tr>");
                sb.AppendFormat("</thead>");
                sb.AppendFormat("<tbody>");
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        if (string.IsNullOrEmpty(item.MANSTATE))
                        {
                            item.MANSTATE = "0";
                        }
                        var fklist = GetFKInfoList(item.JCFID);
                        recordlist = fklist.Select(p => p.MANSTATE).ToList();//MANSTATE状态集合
                        if (Convert.ToInt32(item.MANSTATE) > 10)//大于10 说明已经入反馈阶段有顺序 
                        {
                            strstatus = StateSwitch.QSStateNew(SystemCls.getCurUserOrgNo(), item.MANSTATE);
                        }
                        else//签收无顺序性 状态判断是否反馈表包含 签到状态 1 市 2 县 3 乡镇
                        {
                            strstatus = StateSwitch.QSStateNewList(SystemCls.getCurUserOrgNo(), recordlist.ToList());
                        }
                        sb.AppendFormat("  <tr> ");
                        sb.AppendFormat("<td style=\"text-align: center\"><input type=\"checkbox\" value=\"{0}\" name=\"chk_list\" id=\"CHK_{0}\"/></td>", item.JCFID);
                        sb.AppendFormat("<td>{0}</td>", item.WXBH);
                        sb.AppendFormat("<td>{0}</td>", item.ZQWZ);
                        sb.AppendFormat("<td>{0}</td>", item.RSMJ);
                        sb.AppendFormat("<td>{0}</td>", item.JD);
                        sb.AppendFormat("<td>{0}</td>", item.WD);
                        sb.AppendFormat("<td>{0}</td>", ClsSwitch.SwitTM(item.FIRETIME));
                        if (item.FIREFROMWEATHER == "1")
                        {
                            sb.AppendFormat("<td>【气象卫星】</td>");
                        }
                        else if (item.FIREFROMWEATHER == "2")
                        {
                            sb.AppendFormat("<td>【人工补录】</td>");
                        }
                        else
                        {
                            sb.AppendFormat("<td>【林业卫星】</td>");
                        }
                        //   sb.AppendFormat("<td>{0}</td>", ClsSwitch.SwitTM(item.RECEIVETIME));
                        #region 操作
                        //if (strstatus.Trim() == "本级已上报")
                        //{
                        //    sb.AppendFormat("<td><a  href=\"javascript:void(0);\" onClick=\"StateLogsLayer(" + item.JCFID + "," + item.BYORGNO + ")\" >{0}</a></td>", strstatus);//状态
                        //}
                        //else
                        //{
                        //    if (strstatus.Trim() == "本级未签收")
                        //    {
                        //        sb.AppendFormat("<td><span  style=\"color:red;\">{0}</span></td>", strstatus);
                        //    }
                        //    else
                        //    {
                        //        sb.AppendFormat("<td><a  href=\"javascript:void(0);\" onClick=\"StateLogsLayer(" + item.JCFID + "," + item.BYORGNO + ")\" >{0}</a></td>", strstatus);//状态
                        //    }
                        //}
                        //if (citybo)//市局
                        //{
                        //    if (!recordlist.Contains("1"))//本级未签收
                        //    {
                        //        sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a>  <a href=\"javascript:void(0);\" onClick=\"CityQS(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                        //    }
                        //    else if (item.MANSTATE == "11" || item.MANSTATE == "15" || item.MANSTATE == "18" || item.MANSTATE == "19")
                        //    {
                        //        string manstate = "19";//默认通过审核
                        //        if (item.MANSTATE == "15" || item.MANSTATE == "18")//县级处理
                        //        {
                        //            manstate = "18";
                        //        }
                        //        sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a> <a href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + type + "," + manstate + ",'')\">审核</a></td>");
                        //    }
                        //    else
                        //    {
                        //        sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a> <span style=\"color: red;\">正在核查中...</span></td>");
                        //        //sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a></td>");
                        //    }
                        //}
                        //else if (contybo)//县局
                        //{
                        //    if (item.MANSTATE == "0" && (!recordlist.Contains("2") && !recordlist.Contains("32")))//32 为县级本级单位签收
                        //    {
                        //        sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a>  <a href=\"javascript:void(0);\" onClick=\"QSSXJOrgSelect(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                        //    }
                        //    else if (item.MANSTATE == "31" || item.MANSTATE == "19" || item.MANSTATE == "11" || item.MANSTATE == "34" || item.PFFLAG == "2" || item.MANSTATE == "18" || item.MANSTATE == "15" || item.MANSTATE == "33")
                        //    {
                        //        string manstate = "11";
                        //        if (item.PFFLAG == "2")//2 为县局本级单位处理
                        //        {
                        //            manstate = "15";
                        //        }
                        //        sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a>  <a href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + type + "," + manstate + ",'')\">反馈</a></td>");
                        //    }
                        //    else
                        //    {
                        //        sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a> <span  style=\"color: red;\">正在核查中...</span></td>");
                        //        //sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a> <a href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + type + ",3,'')\">反馈</a></td>");
                        //    }
                        //}
                        //else if (xzbo)//乡镇
                        //{
                        //    if (item.MANSTATE == "0" && !recordlist.Contains("3"))
                        //    {
                        //        sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a>  <a href=\"javascript:void(0);\" onClick=\"QSXZJOrgSelect(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                        //    }
                        //    else
                        //    {
                        //        sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a>  <a href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + type + ",31,'')\">反馈</a></td>");
                        //    }
                        //}

                        #endregion
                        var strsb = getSb(recordlist, item, strstatus, citybo, contybo, xzbo, type);
                        sb.Append(strsb);
                        //if (type != "-1")
                        //{
                        //    sb.AppendFormat("<td><input type=\"button\" value=\"派发核查\" onClick=\"pFCheck(" + item.JCFID + ")\"></td>");
                        //}

                        sb.AppendFormat("   </tr>");
                    }
                }
                else
                {
                    sb.AppendFormat("<tr><td colspan='10'><em>暂无热点信息</em></td></tr>");
                }

                sb.AppendFormat("</tbody>");
                sb.AppendFormat("</table>");
            }
            else
            {
                if (type == "3" || type == "4" || type == "6")//3 人工（电话）报警 4 电子监控 5 护林员上报 6 无人机巡护
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                    sb.AppendFormat("<thead>");
                    sb.AppendFormat("  <tr> ");
                    sb.AppendFormat("  <th><input type=\"checkbox\" id=\"CHK_ALL\" onClick=\"checkfun()\"/>全选</th>");
                    sb.AppendFormat("  <th>序号</th>");
                    sb.AppendFormat("  <th>火情名称</th>");
                    sb.AppendFormat("  <th>发生区域</th>");
                    sb.AppendFormat("  <th>火灾发生地</th>");
                    sb.AppendFormat("  <th>经度</th>");
                    sb.AppendFormat("  <th>纬度</th>");
                    sb.AppendFormat("  <th>接收时间</th>");
                    // sb.AppendFormat("  <th>上报时间</th>");
                    sb.AppendFormat("  <th>状态</th>");
                    sb.AppendFormat("  <th>操作</th>");
                    //sb.AppendFormat("  <th>人工派发</th>");
                    sb.AppendFormat("   </tr>");
                    sb.AppendFormat("</thead>");
                    sb.AppendFormat("<tbody>");
                    if (list.Any())
                    {
                        int i = 0;
                        foreach (var item in list)
                        {
                            if (string.IsNullOrEmpty(item.MANSTATE))
                            {
                                item.MANSTATE = "0";
                            }
                            var fklist = GetFKInfoList(item.JCFID);
                            recordlist = fklist.Select(p => p.MANSTATE).ToList();//MANSTATE状态集合
                            if (Convert.ToInt32(item.MANSTATE) > 10)//大于10 说明已经入反馈阶段有顺序 
                            {
                                strstatus = StateSwitch.QSStateNew(SystemCls.getCurUserOrgNo(), item.MANSTATE);
                            }
                            else//签收无顺序性 状态判断是否反馈表包含 签到状态 1 市 2 县 3 乡镇
                            {
                                strstatus = StateSwitch.QSStateNewList(SystemCls.getCurUserOrgNo(), recordlist.ToList());
                            }
                            sb.AppendFormat("  <tr> ");
                            sb.AppendFormat("<td style=\"text-align: center\"><input type=\"checkbox\" name=\"chk_list\" value=\"{0}\" id=\"CHK_{0}\"/></td>", item.JCFID);
                            sb.AppendFormat("<td>{0}</td>", (++i).ToString());
                            sb.AppendFormat("<td>{0}</td>", item.FIRENAME);
                            sb.AppendFormat("<td>{0}</td>", item.ORGNAME);
                            sb.AppendFormat("<td>{0}</td>", item.ZQWZ);
                            sb.AppendFormat("<td>{0}</td>", item.JD);
                            sb.AppendFormat("<td>{0}</td>", item.WD);
                            sb.AppendFormat("<td>{0}</td>", ClsSwitch.SwitTM(item.FIRETIME));
                            // sb.AppendFormat("<td>{0}</td>", ClsSwitch.SwitTM(item.RECEIVETIME));
                            #region 操作
                            //if (strstatus.Trim() == "本级已上报")
                            //{
                            //    sb.AppendFormat("<td><a  href=\"javascript:void(0);\" onClick=\"StateLogsLayer(" + item.JCFID + "," + item.BYORGNO + ")\" >{0}</a></td>", strstatus);//状态
                            //}
                            //else
                            //{
                            //    if (strstatus.Trim() == "本级未签收")
                            //    {
                            //        sb.AppendFormat("<td><span  style=\"color:red;\">{0}</span></td>", strstatus);
                            //    }
                            //    else
                            //    {
                            //        sb.AppendFormat("<td><a  href=\"javascript:void(0);\" onClick=\"StateLogsLayer(" + item.JCFID + "," + item.BYORGNO + ")\" >{0}</a></td>", strstatus);//状态
                            //    }
                            //}
                            //if (citybo)//市局
                            //{
                            //    if (!recordlist.Contains("1"))//本级未签收
                            //    {
                            //        if (PublicCls.OrgIsShi(item.BYORGNO) && item.PFFLAG != "1")
                            //        {
                            //            sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a>  <a href=\"javascript:void(0);\" onClick=\"CityQSOrgSelect(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                            //        }
                            //        else
                            //        {
                            //            sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a>  <a href=\"javascript:void(0);\" onClick=\"CityQS(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                            //        }
                            //    }
                            //    else if (item.MANSTATE == "11" || item.MANSTATE == "15" || item.MANSTATE == "18" || item.MANSTATE == "19")
                            //    {
                            //        string manstate = "19";//默认通过审核
                            //        if (item.MANSTATE == "15" || item.MANSTATE == "18")//县级处理
                            //        {
                            //            manstate = "18";
                            //        }
                            //        sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a> <a href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + type + "," + manstate + ",'')\">审核</a></td>");
                            //    }
                            //    else
                            //    {
                            //        sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a> <span style=\"color: red;\">正在核查中...</span></td>");
                            //        //sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a></td>");
                            //    }
                            //}
                            //else if (contybo)//县局
                            //{
                            //    if (item.MANSTATE == "0" && (!recordlist.Contains("2") && !recordlist.Contains("32")))//32 为县级本级单位签收
                            //    {
                            //        if (PublicCls.OrgIsZhen(item.BYORGNO))//乡镇
                            //        {
                            //            sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a>  <a href=\"javascript:void(0);\" onClick=\"QSSXJOrg(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                            //        }
                            //        else if (PublicCls.OrgIsXian(item.BYORGNO))
                            //        {
                            //            sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a>  <a href=\"javascript:void(0);\" onClick=\"QSSXJOrgSelect(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                            //        }

                            //    }
                            //    else if (item.MANSTATE == "31" || item.MANSTATE == "19" || item.MANSTATE == "11" || item.MANSTATE == "34" || item.PFFLAG == "2" || item.MANSTATE == "18" || item.MANSTATE == "15" || item.MANSTATE == "33")
                            //    {
                            //        string manstate = "11";
                            //        if (item.PFFLAG == "2")//2 为县局本级单位处理
                            //        {
                            //            manstate = "15";
                            //        }
                            //        sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a>  <a href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + type + "," + manstate + ",'')\">反馈</a></td>");
                            //    }
                            //    else
                            //    {
                            //        sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a> <span  style=\"color: red;\">正在核查中...</span></td>");
                            //        //sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a> <a href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + type + ",3,'')\">反馈</a></td>");
                            //    }
                            //}
                            //else if (xzbo)//乡镇
                            //{
                            //    if (item.MANSTATE == "0" && !recordlist.Contains("3"))
                            //    {
                            //        sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a>  <a href=\"javascript:void(0);\" onClick=\"QSXZJOrgSelect(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                            //    }
                            //    else
                            //    {
                            //        sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a>  <a href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + type + ",31,'')\">反馈</a></td>");
                            //    }
                            //}

                            #endregion
                            var strsb = getSb(recordlist, item, strstatus, citybo, contybo, xzbo, type);
                            sb.Append(strsb);
                            //sb.AppendFormat("<td><input type=\"button\" value=\"派发核查\" onClick=\"pFCheck(" + item.JCFID + ")\"></td>");
                            sb.AppendFormat("  </tr> ");
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<tr><td colspan='12'><em>暂无热点信息</em></td></tr>");
                    }
                    sb.AppendFormat("</tbody>");
                    sb.AppendFormat("</table>");
                }
                else if (type == "5")//3 人工（电话）报警 4 电子监控 5 护林员上报 6 无人机巡护
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                    sb.AppendFormat("<thead>");
                    sb.AppendFormat("  <tr> ");
                    sb.AppendFormat("  <th><input type=\"checkbox\" id=\"CHK_ALL\" onClick=\"checkfun()\"/>全选</th>");
                    sb.AppendFormat("  <th>序号</th>");
                    //  sb.AppendFormat("  <th>火情名称</th>");
                    sb.AppendFormat("  <th>发生区域</th>");
                    sb.AppendFormat("  <th>火灾发生地</th>");
                    sb.AppendFormat("  <th>经度</th>");
                    sb.AppendFormat("  <th>纬度</th>");
                    sb.AppendFormat("  <th>接收时间</th>");
                    // sb.AppendFormat("  <th>上报时间</th>");
                    sb.AppendFormat("  <th>状态</th>");
                    sb.AppendFormat("  <th>操作</th>");
                    //sb.AppendFormat("  <th>人工派发</th>");
                    sb.AppendFormat("   </tr>");
                    sb.AppendFormat("</thead>");
                    sb.AppendFormat("<tbody>");
                    if (list.Any())
                    {
                        int i = 0;
                        foreach (var item in list)
                        {
                            if (string.IsNullOrEmpty(item.MANSTATE))
                            {
                                item.MANSTATE = "0";
                            }
                            var fklist = GetFKInfoList(item.JCFID);
                            recordlist = fklist.Select(p => p.MANSTATE).ToList();//MANSTATE状态集合
                            if (Convert.ToInt32(item.MANSTATE) > 10)//大于10 说明已经入反馈阶段有顺序 
                            {
                                strstatus = StateSwitch.QSStateNew(SystemCls.getCurUserOrgNo(), item.MANSTATE);
                            }
                            else//签收无顺序性 状态判断是否反馈表包含 签到状态 1 市 2 县 3 乡镇
                            {
                                strstatus = StateSwitch.QSStateNewList(SystemCls.getCurUserOrgNo(), recordlist.ToList());
                            }
                            sb.AppendFormat("  <tr> ");
                            sb.AppendFormat("<td style=\"text-align: center\"><input type=\"checkbox\" name=\"chk_list\" value=\"{0}\" id=\"CHK_{0}\"/></td>", item.JCFID);
                            sb.AppendFormat("<td>{0}</td>", (++i).ToString());
                            //sb.AppendFormat("<td>{0}</td>", item.FIRENAME);
                            sb.AppendFormat("<td>{0}</td>", item.ORGNAME);
                            sb.AppendFormat("<td>{0}</td>", item.ZQWZ);
                            sb.AppendFormat("<td>{0}</td>", item.JD);
                            sb.AppendFormat("<td>{0}</td>", item.WD);
                            sb.AppendFormat("<td>{0}</td>", ClsSwitch.SwitTM(item.FIRETIME));
                            //sb.AppendFormat("<td>{0}</td>", ClsSwitch.SwitTM(item.RECEIVETIME));
                            var strsb = getSb(recordlist, item, strstatus, citybo, contybo, xzbo, type);
                            sb.Append(strsb);
                            //sb.AppendFormat("<td><input type=\"button\" value=\"派发核查\" onClick=\"pFCheck(" + item.JCFID + ")\"></td>");
                            sb.AppendFormat("  </tr> ");
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<tr><td colspan='10'><em>暂无热点信息</em></td></tr>");
                    }
                    sb.AppendFormat("</tbody>");
                    sb.AppendFormat("</table>");
                }
            }
            return sb.ToString();
        }



        /// <summary>
        /// 市县乡镇反馈信息
        /// </summary>
        /// <param name="jcid"></param>
        /// <returns></returns>
        public JsonResult FKMethod(string jcid)
        {
            if (string.IsNullOrEmpty(jcid))
            {
                return Json(new Message(false, "jcid 参数缺失", ""));
            }
            string statetype = Request.Params["statetype"];
            string type = "1";
            var curorgno = SystemCls.getCurUserOrgNo();//当前机构
            bool bo = PublicCls.OrgIsShi(curorgno);//市局
            bool bb = PublicCls.OrgIsXian(curorgno);//县局
            if (string.IsNullOrEmpty(statetype))
            {
                return Json(new Message(false, "statetype为反馈类型不可为空", ""));
            }
            else
            {
                type = statetype;
            }
            var model = new JC_FIRE_Model();//监测火情
            model.MANSTATE = type;
            model.JCFID = jcid;
            //model.ISSUEDTIME = DateTime.Now.ToString();//下发（签收） 时间
            model.LASTPROCESSTIME = DateTime.Now.ToString();//最后处理时间
            var sw = new JC_FIRETICKLING_SW();//火情反馈
            sw.JCFID = jcid;
            sw.MANSTATE = type;//状态
            sw.BYORGNO = curorgno;//处理机构码
            sw.MANTIME = DateTime.Now.ToString();//处理时间
            sw.MANUSERID = SystemCls.getUserID();//处理人

            if (!string.IsNullOrEmpty(statetype))//审核反馈
            {
                var dl = Request.Params["dl"];
                var forestname = Request.Params["forestname"];
                var forestfiretype = Request.Params["forestfiretype"];
                var fueltype = Request.Params["fueltype"];
                var hottype = Request.Params["hottype"];
                var checktime = Request.Params["checktime"];
                var yy = Request.Params["yy"];
                var jxhqsj = Request.Params["jxhqsj"];
                var firebegintime = Request.Params["firebegintime"];
                var fireendtime = Request.Params["fireendtime"];
                var chk = Request.Params["chk"];
                var burnedarea = Request.Params["burnedarea"];
                var overdoarea = Request.Params["overdoarea"];
                var lostforestarea = Request.Params["lostforestarea"];
                var fireintro = Request.Params["fireintro"];
                var elselossintro = Request.Params["elselossintro"];
                var shyj = Request.Params["shyj"];//0 审核未通过 1 审核通过
                var txtreson = Request.Params["txtreson"]; //审核不通过意见
                var hqaddress = Request.Params["hqaddress"];//实际发生地
                var hqjd = Request.Params["hqjd"];//实际经度
                var hqwd = Request.Params["hqwd"];//实际纬度

                sw.DL = dl;
                sw.FORESTNAME = forestname;
                sw.FORESTFIRETYPE = forestfiretype;
                sw.FUELTYPE = fueltype;
                sw.HOTTYPE = hottype;
                sw.CHECKTIME = checktime;
                sw.YY = yy;
                sw.JXHQSJ = jxhqsj;
                sw.FIREBEGINTIME = firebegintime;
                sw.FIREENDTIME = fireendtime;
                sw.ISOUTFIRE = chk;
                sw.BURNEDAREA = burnedarea;
                sw.OVERDOAREA = overdoarea;
                sw.LOSTFORESTAREA = lostforestarea;
                sw.FIREINTRO = fireintro.Trim();
                sw.ELSELOSSINTRO = elselossintro.Trim();
                sw.ADDRESS = hqaddress.Trim();
                sw.JD = hqjd.Trim();
                sw.WD = hqwd.Trim();
                if (!string.IsNullOrEmpty(shyj))
                {
                    if (shyj == "0")//0 审核未通过
                    {
                        sw.AUDITREASON = txtreson.Trim();
                        if (bb)//县局审核乡镇反馈信息时 审核通过为11 不通过为51
                        {
                            model.MANSTATE = "51";
                            sw.MANSTATE = "51";
                        }
                        else if (bo)//市局审核乡镇反馈信息时 审核通过为19 不通过为34
                        {
                            if (statetype == "15" || statetype == "18")//县级处理（本单位处理） 通过 火情表＝18	未通过＝33
                            {
                                model.MANSTATE = "33";
                                sw.MANSTATE = "33";
                            }
                            else
                            {
                                model.MANSTATE = "34";
                                sw.MANSTATE = "34";
                            }
                        }
                    }
                }
            }
            var ms = JC_FIRECls.QSFireTrans(model, sw);
            return Json(ms);
        }



        /// <summary>
        /// 签收(市局县局) 弃用
        /// </summary>
        /// <param name="jcid">监测火情id</param>
        /// <param name="orgno">市局签收时选择的下级县局单位机构码</param>
        ///<param name="statetype">市局审核4 县局反馈 3</param>
        /// <returns></returns>
        public JsonResult QSMethod(string jcid, string orgno)
        {
            if (string.IsNullOrEmpty(jcid))
            {
                return Json(new Message(false, "jcid 参数缺失", ""));
            }
            string statetype = Request.Params["statetype"];
            string type = "1";
            var curorgno = SystemCls.getCurUserOrgNo();//当前机构
            bool bo = PublicCls.OrgIsShi(curorgno);//市局
            bool bb = PublicCls.OrgIsXian(curorgno);//县局
            if (string.IsNullOrEmpty(statetype))
            {
                if (bo)
                {
                    if (string.IsNullOrEmpty(orgno))
                    {
                        return Json(new Message(false, "市局选择下级单位县局的orgno 参数缺失", ""));
                    }
                    type = "1";
                }
                else
                {
                    if (bb)
                    {
                        type = "2";
                    }
                    else
                    {
                        return Json(new Message(false, "orgno为乡镇级别，没有权限", ""));
                    }
                }
            }
            else
            {
                type = statetype;//审核 4 反馈 3
            }

            var model = new JC_FIRE_Model();//监测火情
            model.MANSTATE = type;
            model.JCFID = jcid;
            if (bo && string.IsNullOrEmpty(statetype))//市局 并且 排除反馈和审核阶段 不更新机构码
            {
                model.BYORGNO = orgno;//市局签收时选择的县局单位机构码
                model.ISSUEDTIME = DateTime.Now.ToString();//下发（签收） 时间
            }
            model.LASTPROCESSTIME = DateTime.Now.ToString();//最后处理时间
            var sw = new JC_FIRETICKLING_SW();//火情反馈
            sw.JCFID = jcid;
            sw.MANSTATE = type;//状态
            sw.BYORGNO = curorgno;//处理机构码
            sw.MANTIME = DateTime.Now.ToString();//处理时间
            sw.MANUSERID = SystemCls.getUserID();//处理人
            if (bb)//县局签收时默认林火
            {
                sw.HOTTYPE = "1";//默认林火
            }
            if (!string.IsNullOrEmpty(statetype))//审核反馈
            {
                var dl = Request.Params["dl"];
                var forestname = Request.Params["forestname"];
                var forestfiretype = Request.Params["forestfiretype"];
                var fueltype = Request.Params["fueltype"];
                var hottype = Request.Params["hottype"];
                var checktime = Request.Params["checktime"];
                var yy = Request.Params["yy"];
                var jxhqsj = Request.Params["jxhqsj"];
                var firebegintime = Request.Params["firebegintime"];
                var fireendtime = Request.Params["fireendtime"];
                var chk = Request.Params["chk"];
                var burnedarea = Request.Params["burnedarea"];
                var overdoarea = Request.Params["overdoarea"];
                var lostforestarea = Request.Params["lostforestarea"];
                var fireintro = Request.Params["fireintro"];
                var elselossintro = Request.Params["elselossintro"];

                sw.DL = dl;
                sw.FORESTNAME = forestname;
                sw.FORESTFIRETYPE = forestfiretype;
                sw.FUELTYPE = fueltype;
                sw.HOTTYPE = hottype;
                sw.CHECKTIME = checktime;
                sw.YY = yy;
                sw.JXHQSJ = jxhqsj;
                sw.FIREBEGINTIME = firebegintime;
                sw.FIREENDTIME = fireendtime;
                sw.ISOUTFIRE = chk;
                sw.BURNEDAREA = burnedarea;
                sw.OVERDOAREA = overdoarea;
                sw.LOSTFORESTAREA = lostforestarea;
                sw.FIREINTRO = fireintro.Trim();
                sw.ELSELOSSINTRO = elselossintro.Trim();
            }
            var ms = JC_FIRECls.QSFireTrans(model, sw);
            return Json(ms);
        }

        /// <summary>
        /// 获取签收部门（市局）
        /// </summary>
        /// <returns></returns>
        public JsonResult GetQSOrg()
        {
            string curorgno = SystemCls.getCurUserOrgNo();
            var sw = new T_SYS_ORGSW();
            sw.GetContyORGNOByCity = curorgno;
            var str = T_SYS_ORGCls.getSelectOptionByCity(sw);
            return Json(new Message(true, str, ""));
        }

        /// <summary>
        /// 获取卫星云图
        /// </summary>
        /// <returns></returns>
        public JsonResult GetYtImages()
        {
            string count = Request.Params["count"];
            MessageListObject ms = null;
            var list = YJ_SATELLITECLOUDCls.getListModelTop(new YJ_SATELLITECLOUD_SW() { TopCount = count }).OrderBy(p => p.CLOUDID);//卫星云图
            ms = new MessageListObject(true, list);
            return Json(ms);
        }

        /// <summary>
        /// 获取流程日志
        /// </summary>
        /// <returns></returns>
        public JsonResult GetStateLogs(string jcfid, string orgno)
        {
            Message ms = null;
            if (string.IsNullOrEmpty(jcfid))
            {
                ms = new Message(false, "jcfid 参数丢失", "");
            }
            else
            {
                var sw = new JC_FIRETICKLING_SW();
                sw.JCFID = jcfid;
                var msg = getLogsStr(sw, orgno);
                ms = new Message(true, msg, "");
            }
            return Json(ms,JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Private

        /// <summary>
        /// 获取操作stringbuilder
        /// </summary>
        /// <param name="recordlist"></param>
        /// <param name="item"></param>
        /// <param name="strstatus"></param>
        /// <param name="citybo"></param>
        /// <param name="contybo"></param>
        /// <param name="xzbo"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private StringBuilder getSb(List<string> recordlist, JC_FIRE_Model item, string strstatus, bool citybo, bool contybo, bool xzbo, string type)
        {
            StringBuilder sb = new StringBuilder();
            if (strstatus.Trim() == "已上报")
            {
                sb.AppendFormat("<td><a  href=\"javascript:void(0);\" onClick=\"StateLogsLayer(" + item.JCFID + "," + item.BYORGNO + ")\" >{0}</a></td>", strstatus);//状态
            }
            else
            {
                if (strstatus.Trim() == "未签收")
                {
                    sb.AppendFormat("<td><span  style=\"color:red;\">{0}</span></td>", strstatus);
                }
                else
                {
                    sb.AppendFormat("<td><a  href=\"javascript:void(0);\" onClick=\"StateLogsLayer(" + item.JCFID + "," + item.BYORGNO + ")\" >{0}</a></td>", strstatus);//状态
                }
            }
            if (citybo)//市局
            {
                if (!recordlist.Contains("1"))//本级未签收
                {
                    if (type == "3" || type == "4" || type == "5" || type == "6")
                    {
                        if (PublicCls.OrgIsShi(item.BYORGNO) && item.PFFLAG != "1")
                        {
                            sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">二维定位</a>  <a href=\"javascript:void(0);\" onClick=\"Show3DMapLoc(" + item.JCFID + ")\">三维定位</a>   <a href=\"javascript:void(0);\" onClick=\"CityQSOrgSelect(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                        }
                        else
                        {
                            sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">二维定位</a> <a href=\"javascript:void(0);\" onClick=\"Show3DMapLoc(" + item.JCFID + ")\">三维定位</a>  <a href=\"javascript:void(0);\" onClick=\"CityQS(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">二维定位</a>  <a href=\"javascript:void(0);\" onClick=\"Show3DMapLoc(" + item.JCFID + ")\">三维定位</a>  <a href=\"javascript:void(0);\" onClick=\"CityQS(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                    }
                }
                else if (item.MANSTATE == "11" || item.MANSTATE == "15" || item.MANSTATE == "18" || item.MANSTATE == "19")
                {
                    string manstate = "19";//默认通过审核
                    if (item.MANSTATE == "15" || item.MANSTATE == "18")//县级处理
                    {
                        manstate = "18";
                    }
                    sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">二维定位</a> <a href=\"javascript:void(0);\" onClick=\"Show3DMapLoc(" + item.JCFID + ")\">三维定位</a>  <a href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + type + "," + manstate + ",'')\">审核</a></td>");
                }
                else
                {
                    sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">二维定位</a> <a href=\"javascript:void(0);\" onClick=\"Show3DMapLoc(" + item.JCFID + ")\">三维定位</a>  <span style=\"color: red;\">正在核查中...</span></td>");
                    //sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a></td>");
                }
            }
            else if (contybo)//县局
            {
                if (item.MANSTATE == "0" && (!recordlist.Contains("2") && !recordlist.Contains("32")))//32 为县级本级单位签收
                {
                    if (type == "3" || type == "4" || type == "5" || type == "6")
                    {
                        if (PublicCls.OrgIsZhen(item.BYORGNO))//乡镇
                        {
                            sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">二维定位</a>  <a href=\"javascript:void(0);\" onClick=\"Show3DMapLoc(" + item.JCFID + ")\">三维定位</a>  <a href=\"javascript:void(0);\" onClick=\"QSSXJOrg(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                        }
                        else if (PublicCls.OrgIsXian(item.BYORGNO))
                        {
                            sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">二维定位</a> <a href=\"javascript:void(0);\" onClick=\"Show3DMapLoc(" + item.JCFID + ")\">三维定位</a>   <a href=\"javascript:void(0);\" onClick=\"QSSXJOrgSelect(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">二维定位</a> <a href=\"javascript:void(0);\" onClick=\"Show3DMapLoc(" + item.JCFID + ")\">三维定位</a>   <a href=\"javascript:void(0);\" onClick=\"QSSXJOrgSelect(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                    }
                }
                else if (item.MANSTATE == "31" || item.MANSTATE == "19" || item.MANSTATE == "11" || item.MANSTATE == "34" || item.PFFLAG == "2" || item.MANSTATE == "18" || item.MANSTATE == "15" || item.MANSTATE == "33")
                {
                    string manstate = "11";
                    if (item.PFFLAG == "2")//2 为县局本级单位处理
                    {
                        manstate = "15";
                    }
                    sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">二维定位</a>  <a href=\"javascript:void(0);\" onClick=\"Show3DMapLoc(" + item.JCFID + ")\">三维定位</a>  <a href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + type + "," + manstate + ",'')\">反馈</a></td>");
                }
                else
                {
                    sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">二维定位</a>  <a href=\"javascript:void(0);\" onClick=\"Show3DMapLoc(" + item.JCFID + ")\">三维定位</a> <span  style=\"color: red;\">正在核查中...</span></td>");
                    //sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">定位</a> <a href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + type + ",3,'')\">反馈</a></td>");
                }
            }
            else if (xzbo)//乡镇
            {
                if (item.MANSTATE == "0" && !recordlist.Contains("3"))
                {
                    sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">二维定位</a> <a href=\"javascript:void(0);\" onClick=\"Show3DMapLoc(" + item.JCFID + ")\">三维定位</a>   <a href=\"javascript:void(0);\" onClick=\"QSXZJOrgSelect(" + item.JCFID + "," + type + ",'')\">签收</a></td>");
                }
                else
                {
                    sb.AppendFormat("<td> <a href=\"javascript:void(0);\" onClick=\"ShowMapLoc(" + item.JCFID + ")\">二维定位</a> <a href=\"javascript:void(0);\" onClick=\"Show3DMapLoc(" + item.JCFID + ")\">三维定位</a>   <a href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + type + ",31,'')\">反馈</a></td>");
                }
            }
            return sb;
        }

        /// <summary>
        /// 读取火险等级txt文件并入库
        /// </summary>
        /// <param name="path"></param>
        public bool ReadTxt(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            string dt = "";
            bool bo = false;
            int row = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(line.ToString()))
                {
                    var linelist = line.ToString().Split(' ').Where(p => !string.IsNullOrEmpty(p)).ToArray();//空格分隔
                    if (linelist.Length == 1 && row == 0)//第一行
                    {
                        string time = linelist[0];
                        if (!string.IsNullOrEmpty(time))//140328
                        {
                            if (time.Length != 6)
                            {
                                bo = false;
                                break;
                            }
                            else
                            {
                                dt = DateTime.Now.Year.ToString().Substring(0, 2) + time.Substring(0, 2) + "-" + time.Substring(2, 2) + "-" + time.Substring(4, 2) + " 00:00:00.000";
                                var ss = YJ_DANGERCLASSCls.RemoveDataLevelClass(new YJ_DANGERCLASS_Model { DCDATE = dt });//根据火险等级时间删除
                            }
                        }
                        else
                        {
                            bo = false;
                            break;
                        }
                    }
                    else if (linelist.Length == 0)
                    {
                        bo = false;
                        break;
                    }
                    else
                    {
                        var model = new YJ_DANGERCLASS_Model();
                        model.DCDATE = dt;
                        //var orglist = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { });
                        var list = T_ALL_AREACls.getListModel(new T_ALL_AREA_SW());
                        for (int i = 0; i < linelist.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(linelist[i]))
                            {
                                var str = linelist[i].ToString();
                                if (i == 0)
                                {
                                    model.TOWNNAME = str;//乡镇名称
                                    var record = list.Where(p => p.AREAJC.Trim() == str.Trim()).FirstOrDefault();
                                    if (record != null)
                                    {
                                        model.BYORGNO = record.AREACODE;//机构编码
                                    }
                                }
                                else if (i == 1)
                                {
                                    model.JD = str;//经度
                                }
                                else if (i == 2)
                                {
                                    model.WD = str;//纬度
                                }
                                else if (i == 3)
                                {
                                    model.DANGERCLASS = str;//火险等级
                                }
                                else if (i == 4)
                                {
                                    model.TOPTOWNNAME = str;//市县名称
                                }
                            }
                        }
                        var ms = YJ_DANGERCLASSCls.ExportData(model);
                        var flag = System.Configuration.ConfigurationManager.AppSettings["IsInsertSDE"].ToString();//是否更新空间数据库火险等级
                        if (flag == "1")
                        {
                            var m = new YJ_DANGERCLASS_Model();
                            m.Name = model.TOWNNAME.Trim();
                            m.DValue = model.DANGERCLASS.Trim();
                            var mm = YJ_DANGERCLASSCls.UpdateAceHuoXianDengJiData(m);
                        }

                        bo = ms.Success;
                    }
                }
                ++row;
            }
            return bo;
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

        /// <summary>
        /// 获取市县乡镇天气列表
        /// </summary>
        /// <returns></returns>
        private IEnumerable<WeatherInfoModel> GetWeatherList()
        {
            var result = new List<WeatherInfoModel>();
            var list = WeatherCls.getWeatherData(new YJ_WEATHER_SW() { });
            if (list.Any())
            {
                foreach (var item in list.OrderByDescending(p => p.BYORGNO))
                {
                    var model = new WeatherInfoModel();
                    model.AreaName = item.TOWNNAME; //地区名
                    model.WeatherDate = Convert.ToDateTime(item.WEATHERDATE).ToString("yyyy-MM-dd hh:mm:ss");//日期
                    model.Hum = item.P;//雨量
                    model.TCur = item.TCUR;//当前温度
                    if (string.IsNullOrEmpty(item.THIGH) && string.IsNullOrEmpty(item.TLOWER))
                    {
                        model.HighAndLow = "";
                    }
                    else
                    {
                        model.HighAndLow = item.TLOWER + "--" + item.THIGH;//最高温度&最低温度
                    }
                    result.Add(model);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取火情(预警响应)html
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        private string getFireStr(JC_FIRE_SW sw, out int total)
        {
            var strstatus = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr role=\"row\">");
            sb.AppendFormat("        <th style=\"width: 57px;\">序号</th>");
            sb.AppendFormat("        <th>编号</th>");
            sb.AppendFormat("        <th>状态</th>");
            sb.AppendFormat("        <th>热点区域</th>");
            sb.AppendFormat("        <th>来源</th>");
            sb.AppendFormat("        <th>热点类别</th>");
            sb.AppendFormat("        <th>经度</th>");
            sb.AppendFormat("        <th>纬度</th>");
            sb.AppendFormat("        <th>接收时间</th>");
            sb.AppendFormat("        <th>像素</th>");
            sb.AppendFormat("        <th>烟云</th>");
            sb.AppendFormat("        <th>连续</th>");
            sb.AppendFormat("        <th>地类</th>");
            // sb.AppendFormat("        <th></th>");
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody role=\"alert\" aria-live=\"polite\" aria-relevant=\"all\">");
            int i = 0;
            var resultlist = JC_FIRECls.GetListModelAndCount(sw, out total);//获取记录
            if (resultlist.Any())
            {
                var curorgno = SystemCls.getCurUserOrgNo();//当前机构
                bool bo = PublicCls.OrgIsShi(curorgno);//市局
                bool bb = PublicCls.OrgIsXian(curorgno);//县局
                bool bx = PublicCls.OrgIsZhen(curorgno);//乡镇局
                foreach (var item in resultlist)
                {
                    if (string.IsNullOrEmpty(item.MANSTATE))
                    {
                        item.MANSTATE = "0";
                    }
                    var fklist = GetFKInfoList(item.JCFID);
                    var recordlist = fklist.Select(p => p.MANSTATE).ToList();//MANSTATE状态集合
                    if (Convert.ToInt32(item.MANSTATE) > 10)//大于10 说明已经入反馈阶段有顺序 
                    {
                        strstatus = StateSwitch.QSStateNew(SystemCls.getCurUserOrgNo(), item.MANSTATE);
                    }
                    else//签收无顺序性 状态判断是否反馈表包含 签到状态 1 市 2 县 3 乡镇
                    {
                        strstatus = StateSwitch.QSStateNewList(SystemCls.getCurUserOrgNo(), recordlist.ToList());
                    }
                    if (i % 2 == 0)
                        sb.AppendFormat("<tr>");
                    else
                        sb.AppendFormat("<tr class='row1'>");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", ((sw.curPage - 1) * sw.pageSize + i + 1).ToString());
                    sb.AppendFormat("<td class=\"center\">{0}</td>", item.WXBH);//卫星编号
                    //状态
                    if (bo && !recordlist.Contains("1"))//市局
                    {
                        if (PublicCls.OrgIsShi(item.BYORGNO) && item.FIREFROM == "3")
                        {
                            sb.AppendFormat("<td class=\"center\">  <a href=\"javascript:void(0);\" onClick=\"CityQSOrgSelect(" + item.JCFID + "," + item.FIREFROM + ",'reload')\">本级未签收</a></td>");
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"center\"><a href=\"javascript:void(0);\" onClick=\"CityQS(" + item.JCFID + "," + item.FIREFROM + ",'reload')\">本级未签收</a></td>");
                        }
                    }
                    else
                    {
                        if (bb && !recordlist.Contains("2"))//县局
                        {
                            if (PublicCls.OrgIsZhen(item.BYORGNO))//乡镇
                            {
                                sb.AppendFormat("<td>  <a href=\"javascript:void(0);\" onClick=\"QSSXJOrg(" + item.JCFID + "," + item.FIREFROM + ",'reload')\">本级未签收</a></td>");
                            }
                            else
                            {
                                sb.AppendFormat("<td class=\"center\"><a href=\"javascript:void(0);\" onClick=\"QSSXJOrgSelect(" + item.JCFID + "," + item.FIREFROM + ",'reload')\">本级未签收</a></td>");
                            }
                        }
                        else if (bx && !recordlist.Contains("3"))//乡镇局
                        {
                            sb.AppendFormat("<td class=\"center\"><a href=\"javascript:void(0);\" onClick=\"QSXZJOrgSelect(" + item.JCFID + "," + item.FIREFROM + ",'')\">本级未签收</a></td>");
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"center\"><a  href=\"javascript:void(0);\" onClick=\"StateLogsLayer(" + item.JCFID + "," + item.BYORGNO + ")\" >{0}</a></td>", strstatus);//状态
                        }
                    }
                    sb.AppendFormat("<td class=\"center\">{0}</td>", item.ORGNAME);//热点区域
                    sb.AppendFormat("<td class=\"center\">{0}</td>", Enum.GetName(typeof(EnumType), Convert.ToInt32(item.FIREFROM)));//来源
                    var record = JC_FIRETICKLINGCls.GetFKFireInfoData(item.JCFID);//获取火情反馈信息
                    //fttype 火情类型
                    //红外相机 = 1,
                    //   卫星热点 = 2,
                    //   人工报警 = 3,
                    //   电子报警 = 4,
                    //   护林员火情上报 = 5
                    if (bo)
                    {
                        string manstate = "19";//默认通过审核
                        if (item.MANSTATE == "15" || item.MANSTATE == "18")//县级处理
                        {
                            manstate = "18";
                        }
                        sb.AppendFormat("<td class=\"center\"><a  href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + item.FIREFROM + "," + manstate + ",'reload')\">{0}</a></td>", StateSwitch.DicStateName("热点类别", record.JC_FireFKData.HOTTYPE));//快速反馈(热点类别)
                    }
                    else if (bb)
                    {
                        string manstate = "11";
                        if (item.PFFLAG == "2")//2 为县局本级单位处理
                        {
                            manstate = "15";
                        }
                        sb.AppendFormat("<td class=\"center\"><a  href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + item.FIREFROM + "," + manstate + ",'reload')\">{0}</a></td>", StateSwitch.DicStateName("热点类别", record.JC_FireFKData.HOTTYPE));//快速反馈(热点类别)
                    }
                    else
                    {
                        sb.AppendFormat("<td class=\"center\"><a  href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + "," + item.FIREFROM + ",31,'reload')\">{0}</a></td>", StateSwitch.DicStateName("热点类别", record.JC_FireFKData.HOTTYPE));//快速反馈(热点类别)
                    }
                    sb.AppendFormat("<td class=\"center\">{0}</td>", item.JD);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", item.WD);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", item.RECEIVETIME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", item.RSMJ);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", StateSwitch.DicStateName("烟云类别", record.JC_FireFKData.YY));//烟云（烟云类别）
                    sb.AppendFormat("<td class=\"center\">{0}</td>", StateSwitch.DicStateName("是否连续", record.JC_FireFKData.JXHQSJ));//是否连续
                    sb.AppendFormat("<td class=\"center\">{0}</td>", StateSwitch.DicStateName("地类", record.JC_FireFKData.DL));//地类
                    sb.AppendFormat("</tr>");
                    i++;
                }
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        ///// <summary>
        ///// 获取火情(预警响应)html 弃用
        ///// </summary>
        ///// <param name="sw"></param>
        ///// <param name="total"></param>
        ///// <returns></returns>
        //private string getFireStr(JC_FIRE_SW sw, out int total)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
        //    sb.AppendFormat("<thead>");
        //    sb.AppendFormat("    <tr role=\"row\">");
        //    sb.AppendFormat("        <th style=\"width: 57px;\">序号</th>");
        //    sb.AppendFormat("        <th>编号</th>");
        //    sb.AppendFormat("        <th>状态</th>");
        //    sb.AppendFormat("        <th>热点区域</th>");
        //    sb.AppendFormat("        <th>来源</th>");
        //    sb.AppendFormat("        <th>快速反馈</th>");
        //    sb.AppendFormat("        <th>经度</th>");
        //    sb.AppendFormat("        <th>纬度</th>");
        //    sb.AppendFormat("        <th>发生时间</th>");
        //    sb.AppendFormat("        <th>像素</th>");
        //    sb.AppendFormat("        <th>烟云</th>");
        //    sb.AppendFormat("        <th>连续</th>");
        //    sb.AppendFormat("        <th>地类</th>");
        //    // sb.AppendFormat("        <th></th>");
        //    sb.AppendFormat("    </tr>");
        //    sb.AppendFormat("</thead>");
        //    sb.AppendFormat("<tbody role=\"alert\" aria-live=\"polite\" aria-relevant=\"all\">");
        //    int i = 0;
        //    var resultlist = JC_FIRECls.GetListModelAndCount(sw, out total);//获取记录
        //    if (resultlist.Any())
        //    {
        //        var curorgno = SystemCls.getCurUserOrgNo();//当前机构
        //        bool bo = PublicCls.OrgIsShi(curorgno);//市局
        //        bool bb = PublicCls.OrgIsXian(curorgno);//县局
        //        foreach (var item in resultlist)
        //        {
        //            if (i % 2 == 0)
        //                sb.AppendFormat("<tr>");
        //            else
        //                sb.AppendFormat("<tr class='row1'>");
        //            sb.AppendFormat("<td class=\"center\">{0}</td>", ((sw.curPage - 1) * sw.pageSize + i + 1).ToString());
        //            sb.AppendFormat("<td class=\"center\">{0}</td>", item.WXBH);//卫星编号
        //            //状态

        //            if (bo && item.MANSTATE == "0")
        //            {
        //                sb.AppendFormat("<td class=\"center\"><a href=\"javascript:void(0);\" onClick=\"QSSJOrgSelect(" + item.JCFID + ")\">本级未签收</a></td>");
        //            }
        //            else
        //            {
        //                if (bb && item.MANSTATE == "1")
        //                {
        //                    sb.AppendFormat("<td class=\"center\"><a href=\"javascript:void(0);\" onClick=\"QSXJ(" + item.JCFID + ")\">本级未签收</a></td>");
        //                }
        //                else
        //                {
        //                    sb.AppendFormat("<td class=\"center\"><a  href=\"javascript:void(0);\" onClick=\"StateLogsLayer(" + item.JCFID + "," + item.BYORGNO + ")\" >{0}</a></td>", StateSwitch.QSState(SystemCls.getCurUserOrgNo(), item.MANSTATE));//状态
        //                }

        //            }

        //            sb.AppendFormat("<td class=\"center\">{0}</td>", item.ORGNAME);//热点区域
        //            sb.AppendFormat("<td class=\"center\">{0}</td>", Enum.GetName(typeof(EnumType), Convert.ToInt32(item.FIREFROM)));//来源
        //            var record = JC_FIRETICKLINGCls.GetFKFireInfoData(item.JCFID);//获取火情反馈信息
        //            //fttype 火情类型
        //            //红外相机 = 1,
        //            //   卫星热点 = 2,
        //            //   人工报警 = 3,
        //            //   电子报警 = 4,
        //            //   护林员火情上报 = 5
        //            if (bo)
        //            {
        //                sb.AppendFormat("<td class=\"center\"><a  href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + ",2,4,'reload')\">{0}</a></td>", StateSwitch.DicStateName("热点类别", record.JC_FireFKData.HOTTYPE));//快速反馈(热点类别)
        //            }
        //            else
        //            {
        //                sb.AppendFormat("<td class=\"center\"><a  href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + ",2,3,'reload')\">{0}</a></td>", StateSwitch.DicStateName("热点类别", record.JC_FireFKData.HOTTYPE));//快速反馈(热点类别)
        //            }
        //            sb.AppendFormat("<td class=\"center\">{0}</td>", item.JD);
        //            sb.AppendFormat("<td class=\"center\">{0}</td>", item.WD);
        //            sb.AppendFormat("<td class=\"center\">{0}</td>", item.FIRETIME);
        //            sb.AppendFormat("<td class=\"center\">{0}</td>", item.RSMJ);
        //            sb.AppendFormat("<td class=\"center\">{0}</td>", StateSwitch.DicStateName("烟云类别", record.JC_FireFKData.YY));//烟云（烟云类别）
        //            sb.AppendFormat("<td class=\"center\">{0}</td>", StateSwitch.DicStateName("是否连续", record.JC_FireFKData.JXHQSJ));//是否连续
        //            sb.AppendFormat("<td class=\"center\">{0}</td>", StateSwitch.DicStateName("地类", record.JC_FireFKData.DL));//地类
        //            sb.AppendFormat("</tr>");
        //            i++;
        //        }
        //    }
        //    sb.AppendFormat("</tbody>");
        //    sb.AppendFormat("</table>");

        //    return sb.ToString();
        //}

        private string getFireStr1(JC_FIRE_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"sample-table-2\" class=\"table table-striped table-bordered table-hover dataTable\" aria-describedby=\"sample-table-2_info\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("    <tr role=\"row\">");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" rowspan=\"1\" colspan=\"1\" aria-label=\"\" style=\"width: 57px;\">序号</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\">编号</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\">状态</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\">热点区域</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\">来源</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\">快速反馈</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\">经度</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\">纬度</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\">发生时间</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\">像素</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\">烟云</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\">连续</th>");
            sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" tabindex=\"0\" aria-controls=\"sample-table-2\" rowspan=\"1\" colspan=\"1\">地类</th>");
            // sb.AppendFormat("        <th class=\"center sorting_disabled\" role=\"columnheader\" rowspan=\"1\" colspan=\"1\" aria-label=\"\" style=\"width: 148px;\"></th>");
            sb.AppendFormat("    </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody role=\"alert\" aria-live=\"polite\" aria-relevant=\"all\">");
            int i = 0;
            var resultlist = JC_FIRECls.GetListModelAndCount(sw, out total);//获取记录
            if (resultlist.Any())
            {
                var curorgno = SystemCls.getCurUserOrgNo();//当前机构
                bool bo = PublicCls.OrgIsShi(curorgno);//市局
                bool bb = PublicCls.OrgIsXian(curorgno);//县局
                foreach (var item in resultlist)
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", ((sw.curPage - 1) * sw.pageSize + i + 1).ToString());
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", item.WXBH);//卫星编号
                    //状态
                    if (bo && item.MANSTATE == "0")
                    {
                        sb.AppendFormat("<td class=\"center  sorting_1\"><a href=\"javascript:void(0);\" onClick=\"QSSJOrgSelect(" + item.JCFID + ")\">本级未签收</a></td>");
                    }
                    else
                    {
                        if (bb && item.MANSTATE == "1")
                        {
                            sb.AppendFormat("<td class=\"center  sorting_1\"><a href=\"javascript:void(0);\" onClick=\"QSXJ(" + item.JCFID + ")\">本级未签收</a></td>");
                        }
                        else
                        {
                            sb.AppendFormat("<td class=\"center  sorting_1\"><a  href=\"javascript:void(0);\" onClick=\"StateLogsLayer(" + item.JCFID + "," + item.BYORGNO + ")\" >{0}</a></td>", StateSwitch.QSState(SystemCls.getCurUserOrgNo(), item.MANSTATE));//状态
                        }
                    }
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", item.ORGNAME);//热点区域
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", Enum.GetName(typeof(EnumType), Convert.ToInt32(item.FIREFROM)));//来源
                    var record = JC_FIRETICKLINGCls.GetFKFireInfoData(item.JCFID);//获取火情反馈信息
                    //fttype 火情类型
                    //红外相机 = 1,
                    //   卫星热点 = 2,
                    //   人工报警 = 3,
                    //   电子报警 = 4,
                    //   护林员火情上报 = 5
                    if (bo)
                    {
                        sb.AppendFormat("<td class=\"center  sorting_1\"><a  href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + ",2,4,'reload')\">{0}</a></td>", StateSwitch.DicStateName("热点类别", record.JC_FireFKData.HOTTYPE));//快速反馈(热点类别)
                    }
                    else
                    {
                        sb.AppendFormat("<td class=\"center  sorting_1\"><a  href=\"javascript:void(0);\" onClick=\"FkFireInfo('../JCFireInfo/FireHtmlFKIndex'," + item.JCFID + ",2,3,'reload')\">{0}</a></td>", StateSwitch.DicStateName("热点类别", record.JC_FireFKData.HOTTYPE));//快速反馈(热点类别)
                    }
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", item.JD);
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", item.WD);
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", item.FIRETIME);
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", item.RSMJ);
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", StateSwitch.DicStateName("烟云类别", record.JC_FireFKData.YY));//烟云（烟云类别）
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", StateSwitch.DicStateName("是否连续", record.JC_FireFKData.JXHQSJ));//是否连续
                    sb.AppendFormat("<td class=\"center  sorting_1\">{0}</td>", StateSwitch.DicStateName("地类", record.JC_FireFKData.DL));//地类
                    sb.AppendFormat("</tr>");
                    i++;
                }
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        /// <summary>
        ///  获取反馈日志流程
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="orgno"></param>
        /// <returns></returns>
        private string getLogsStr(JC_FIRETICKLING_SW sw, string orgno)
        {
            var strorgname = "";//县局
            var strxzname = "";//乡镇局
            var bc = PublicCls.OrgIsXian(orgno);//县局
            var bx = PublicCls.OrgIsZhen(orgno);//乡镇局
            if (bc)
            {
                strorgname = StateSwitch.GetOrgNameByOrgNO(orgno);
            }
            else if (bx)
            {   
                strorgname = StateSwitch.GetOrgNameByOrgNO(orgno.Remove(orgno.Length - 9, 9) + "000000000");
                strxzname = StateSwitch.GetOrgNameByOrgNO(orgno);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"divTable\">");
            sb.Append("<table  style=\"margin:8px;width:98%\"  cellpadding=\"0\" cellspacing=\"0\">");
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th>序号</th>");
            sb.Append("<th>操作时间</th>");
            sb.Append("<th>红河州森林防火办</th>");
            if (!string.IsNullOrEmpty(strorgname))
            {
                sb.AppendFormat("<th>{0}</th>", strorgname);
            }
            if (!string.IsNullOrEmpty(strxzname))
            {
                sb.AppendFormat("<th>{0}</th>", strxzname);
            }
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            var list = JC_FIRETICKLINGCls.GetModelList(sw);
            if (list.Any())
            {
                int i = 0;
                foreach (var item in list.OrderBy(p => Convert.ToDateTime(p.MANTIME)))
                {
                    i++;
                    sb.Append("<tr>");
                    sb.AppendFormat("<td  style=\"width: 25px;\">{0}</td>", i);
                    sb.AppendFormat("<td  style=\"width: 180px;\">{0}</td>", ClsSwitch.SwitTM(item.MANTIME));
                    var bo = PublicCls.OrgIsShi(item.BYORGNO);//市局
                    if (bo)
                    {
                        sb.AppendFormat("<td style=\"width: 200px;\">{0}</td>", logstr(item.MANSTATE, item.MANUSERID));
                        if (!string.IsNullOrEmpty(strorgname))
                        {
                            sb.Append("<td></td>");
                        }
                        if (!string.IsNullOrEmpty(strxzname))
                        {
                            sb.Append("<td></td>");
                        }
                    }
                    var bb = PublicCls.OrgIsXian(item.BYORGNO);//县局
                    if (bb)
                    {
                        sb.Append("<td></td>");
                        sb.AppendFormat("<td style=\"width: 200px;\">{0}</td>", logstr(item.MANSTATE, item.MANUSERID));
                        if (!string.IsNullOrEmpty(strxzname))
                        {
                            sb.Append("<td></td>");
                        }
                    }
                    var bxz = PublicCls.OrgIsZhen(item.BYORGNO);//乡镇局
                    if (bxz)
                    {
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.AppendFormat("<td style=\"width: 200px;\">{0}</td>", logstr(item.MANSTATE, item.MANUSERID));
                    }
                    sb.Append("</tr>");
                }
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("</div>");
            return sb.ToString();
        }

        /// <summary>
        /// 获取相应状态信息(反馈表取值)
        /// </summary>
        /// <param name="state"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        private string logstr(string state, string userid)
        {
            var str = "";
            var username = StateSwitch.GetUsrNameByUserid(userid);
            if (state == "1" || state == "2" || state == "3")
            {
                str = "签收";
            }
            else if (state == "4")
            {
                str = "【市级处理】已签收";
            }
            else if (state == "11" || state == "18" || state == "19")
            {
                str = "已上报";
            }
            else if (state == "33" || state == "34" || state == "51")
            {
                str = "审核未通过";
            }
            else if (state == "15")
            {
                str = "【县级处理】已反馈";
            }
            //else if (state == "18")
            //{
            //    str = "【县级处理】已上报";
            //}
            else if (state == "32")
            {
                str = "【县级处理】已签收";
            }
            else if (Convert.ToInt32(state) > 0 && Convert.ToInt32(state) < 50)
            {
                str = "已反馈";
            }
            else
            {
                str = "状态有误";
            }
            return str + "(" + username + ")";
        }

        /// <summary>
        /// 获取监测反馈信息
        /// </summary>
        /// <param name="jcfid"></param>
        /// <returns></returns>
        private IEnumerable<JC_FIRETICKLING_Model> GetFKInfoList(string jcfid)
        {
            return JC_FIRETICKLINGCls.GetModelList(new JC_FIRETICKLING_SW { JCFID = jcfid });
        }

        /// <summary>
        /// 获取热点状态
        /// </summary>
        /// <param name="orgno"></param>
        /// <returns></returns>
        private string getSelectHotState(string orgno, string curstate)
        {
            StringBuilder sb = new StringBuilder();

            var bo = PublicCls.OrgIsShi(orgno);//市级用户
            if (bo)
            {
                sb.Append("<option value=\"0,1,2,3,4,5,6,7,8\">全部状态</option>");

                sb.AppendFormat("<option value=\"0\" {0}>本级未签收</option>", curstate == "0" ? "selected" : "");
                sb.AppendFormat("<option value=\"1\" {0}>下级未签收</option>", curstate == "1" ? "selected" : "");
                sb.AppendFormat("<option value=\"2\" {0}>下级未反馈</option>", curstate == "2" ? "selected" : "");
                sb.AppendFormat("<option value=\"3\" {0}>本级未审核</option>", curstate == "3" ? "selected" : "");
                sb.AppendFormat("<option value=\"4\" {0}>本级已上报</option>", curstate == "4" ? "selected" : "");
            }

            var bb = PublicCls.OrgIsXian(orgno);//县级用户
            if (bb)
            {
                sb.Append("<option value=\"0,1,2,3,4,5,6,7,8\">全部状态</option>");
                sb.AppendFormat("<option value=\"1\" {0}>本级未签收</option>", curstate == "1" ? "selected" : "");
                sb.AppendFormat("<option value=\"2\" {0}>本级未反馈</option>", curstate == "2" ? "selected" : "");
                sb.AppendFormat("<option value=\"3,4\" {0}>本级已反馈</option>", curstate == "3,4" ? "selected" : "");
            }
            var bx = PublicCls.OrgIsZhen(orgno);//乡镇级用户
            if (bx)
            {
                sb.Append("<option value=\"0,3,4,5\">全部状态</option>");
                sb.AppendFormat("<option value=\"0\" {0}>本级未签收</option>", curstate == "1" ? "selected" : "");
                sb.AppendFormat("<option value=\"3\" {0}>本级未反馈</option>", curstate == "3" ? "selected" : "");
                sb.AppendFormat("<option value=\"4\" {0}>本级已反馈</option>", curstate == "4" ? "selected" : "");
                sb.AppendFormat("<option value=\"5\" {0}>上级审核不通过</option>", curstate == "5" ? "selected" : "");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取枚举热点来源
        /// </summary>
        /// <param name="cursource"></param>
        /// <returns></returns>
        private string getSelectHotSource(string cursource)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<option value=\"\">全部</option>");
            Array ary = Enum.GetValues(typeof(EnumType));  //array是数组的基类, 无法实例化
            foreach (int i in ary)  //列出枚举项对应的数字
            {
                sb.AppendFormat("<option value=" + i.ToString() + " {0}>" + ary.GetValue(i - 1).ToString() + "</option>", cursource == i.ToString() ? "selected" : "");
            }
            return sb.ToString();
        }

        #endregion

    }
}
