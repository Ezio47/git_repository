using ManagerSystem.MVC.Models;
using ManagerSystemClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.IO;
using Omu.ValueInjecter;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using PublicClassLibrary;
using System.Net;
using ManagerSystem.MVC.HelpCom;
using ManagerSystemClassLibrary.SDECLS;
using ManagerSystemSearchWhereModel.LogicModel;
using ManagerSystemModel.SDEModel;
using ManagerSystemModel.LogicModel;

namespace ManagerSystem.MVC.Controllers
{
    /// <summary>
    /// 应急处置
    /// </summary>
    public class EmergencyHandController : BaseController
    {
        //private static ILog logs = LogHelper.GetInstance();
        //
        // GET: /EmergencyHand/
        /// <summary>
        /// 当前火情
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            pubViewBag("012001", "012001", "");
            //string ipath = System.Configuration.ConfigurationManager.AppSettings["FireFlaPath"].ToString();//相对路径
            //string PhysicalPath = Server.MapPath(ipath + "\\");//绝路径
            //FileOptionCls.GetFileList(PhysicalPath,".fly");

            // var datamodel = new YJZHModel();
            var modelfirelist = GetCUrFireList();//当前火情
            // var modelflalist = GetFlaModelList();
            //datamodel.Data.EHCurFireModeList.InjectFrom(modelfirelist);
            return View(modelfirelist);
        }
        /// <summary>
        /// 三维绘图
        /// </summary>
        /// <returns></returns>
        public ActionResult Draw()
        {
            return View();
        }
        public ActionResult Nr()
        {
            var model = new List<EHCurFireMode>();
            return View(model);
        }

        /// <summary>
        /// 生成地图图片
        /// </summary>
        /// <returns></returns>
        public ActionResult GenerateImages() 
        {
            CookieModel cookieInfo = SystemCls.getCookieInfo();
            var model = T_SYSSEC_IPSUSERCls.getModel(new T_SYSSEC_IPSUSER_SW { USERID = cookieInfo.UID });
            var modelOrg = T_SYS_ORGCls.getModel(new T_SYS_ORGSW { ORGNO = model.ORGNO });
            ViewBag.DEPT = modelOrg.ORGNAME + modelOrg.COMMANDNAME;
            ViewBag.NAME = cookieInfo.trueName;
            ViewBag.TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return View();
        }

        /// <summary>
        /// 经纬度定位界面
        /// </summary>
        /// <returns></returns>
        public ActionResult LocationLngLat()
        {
            return View();
        }

        public ActionResult Part3DIndex()
        {
            pubViewBag("012001", "012001", "");
            return View();
        }
        /// <summary>
        /// 三维定位--预警监测
        /// </summary>
        /// <returns></returns>
        public ActionResult From2Dto3D()
        {
            CookieModel cookieInfo1 = SystemCls.getCookieInfo();
            ViewBag.LAYERNAME = T_SYS_LAYERCls.getLayerNameStr(new T_SYS_LAYER_SW { USERID = cookieInfo1.UID });
            ViewBag.DEFAULTCH = T_SYS_LAYERCls.getLayerDEFAULTCHStr(new T_SYS_LAYER_SW { USERID = cookieInfo1.UID });
            ViewBag.AllNAME = T_SYS_LAYERCls.getLayerAllNAME();
            var result = new List<MapShowModel>();
            var sw = new JC_FIRE_SW();
            string jcfid = Request.Params["jcfid"];
            if (!string.IsNullOrEmpty(jcfid))
            {
                ViewBag.strjcfid = jcfid;
                var ss = jcfid.Split(',');
                if (ss.Length == 1)
                {
                    sw.JCFID = jcfid;
                }
                else
                {
                    sw.JCFIDSTR = jcfid;
                }
            }
            var list = JC_FIRECls.GetListModel(sw);
            if (list.Any())
            {
                foreach (var item in list)
                {
                    //var info = JC_FIRETICKLINGCls.GetFKFireInfoData(item.JCFID);
                    var fklist = GetFKInfoList(item.JCFID);
                    var recordlist = fklist.Select(p => p.MANSTATE).ToList();//MANSTATE状态集合

                    var model = new MapShowModel();
                    model.JCFID = item.JCFID;
                    model.AREA = item.ZQWZ;
                    model.BH = item.WXBH;
                    if (Convert.ToInt32(item.MANSTATE) > 10)//大于10 说明已经入反馈阶段有顺序 
                    {
                        model.FKSTATE = StateSwitch.QSStateNew(SystemCls.getCurUserOrgNo(), item.MANSTATE);
                    }
                    else//签收无顺序性 状态判断是否反馈表包含 签到状态 1 市 2 县 3 乡镇
                    {
                        model.FKSTATE = StateSwitch.QSStateNewList(SystemCls.getCurUserOrgNo(), recordlist);
                    }
                    //model.FKSTATE = StateSwitch.QSStateNew(SystemCls.getCurUserOrgNo(), info.JC_FireFKData.MANSTATE);
                    result.Add(model);
                }
            }
            return View(result);
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
        /// 三维查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Qurey()
        {
            var model = new List<EHCurFireMode>();
            return View(model);
        }
        /// <summary>
        /// 三维电量查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Power()
        {
            string uid = Request.Params["uid"];//护林员id
            ViewBag.uid = uid;
            return View();
        }
        /// <summary>
        /// 图层控制
        /// </summary>
        /// <returns></returns>
        public ActionResult Tckz()
        {
            CookieModel cookieInfo = SystemCls.getCookieInfo();
            ViewBag.LAYERNAME = T_SYS_LAYERCls.getLayerNameStr(new T_SYS_LAYER_SW { USERID = cookieInfo.UID });
            ViewBag.DEFAULTCH = T_SYS_LAYERCls.getLayerDEFAULTCHStr(new T_SYS_LAYER_SW { USERID = cookieInfo.UID });
            ViewBag.LAYERCODE = T_SYS_LAYERCls.getLayerLAYERCODEStr(new T_SYS_LAYER_SW { USERID = cookieInfo.UID });
            ViewBag.YEAR = T_SYS_LAYERCls.getLayerYEAR();//从空间库获取火情档案的年份
            var model = new List<EHCurFireMode>();
            //string result = T_SYS_LAYERCls.getTree(new T_SYS_LAYER_SW { USERID = cookieInfo.UID });//普通方法取图层
            string result = T_SYS_LAYERCls.getTckzTree(new T_SYS_LAYER_SW { USERID = cookieInfo.UID });//递归方法取图层
            ViewBag.TreeData = result;
            string resultChecked = T_SYS_LAYERCls.getTckzTreeChecked(new T_SYS_LAYER_SW { USERID = cookieInfo.UID });
            ViewBag.TreeDataChe = resultChecked;
            return View(model);
        }

        /// <summary>
        /// 图层控制树图层Json
        /// </summary>
        /// <returns></returns>
        public ActionResult TckzJson()
        {
            CookieModel cookieInfo = SystemCls.getCookieInfo();
            string result = T_SYS_LAYERCls.getTree(new T_SYS_LAYER_SW {USERID=cookieInfo.UID});
            return Content(result, "application/json");
        }

        /// <summary>
        /// 设置火情结束时间
        /// </summary>
        /// <returns></returns>
        public ActionResult SetOverFireDateIndex()
        {
            return View();
        }


        /// <summary>
        /// 火情属性
        /// </summary>
        /// <returns></returns>
        public ActionResult FireSXIndex()
        {
            string jcfid = Request.Params["jcfid"];
            var model = GetFireLevelModel(jcfid);
            if (string.IsNullOrEmpty(model.MGSD))
            {
                model.MGSD = "-1";
            }
            if (string.IsNullOrEmpty(model.ZDQY))
            {
                model.ZDQY = "-1";
            }
            if (string.IsNullOrEmpty(model.FIRELEVEL))
            {
                model.FIRELEVEL = "-1";
            }
            if (string.IsNullOrEmpty(model.ZZH))
            {
                model.ZZH = "-1";
            }
            if (string.IsNullOrEmpty(model.QHS))
            {
                model.QHS = "-1";
            }
            if (string.IsNullOrEmpty(model.SSJB))
            {
                model.SSJB = "-1";
            }
            ViewBag.mg = model.MGSD;
            ViewBag.zd = model.ZDQY;
            ViewBag.level = model.FIRELEVEL;
            ViewBag.zzh = model.ZZH;
            ViewBag.qhs = model.QHS;
            ViewBag.ssjb = model.SSJB;
            return View(model);
        }

        /// <summary>
        /// 预案
        /// </summary>
        /// <returns></returns>
        public ActionResult YAIndex()
        {
            string level = Request.Params["level"];
            if (level.ToLower() == "null")
                level = "";
            string org = Request.Params["org"];
            var list = JC_FIRE_PLANCls.getModelList(new JC_FIRE_PLAN_SW { FIRELEVEL = level }, org.Trim());
            return View(list);
        }

        /// <summary>
        /// 地图工具
        /// </summary>
        /// <returns></returns>
        public ActionResult MapToolsIndex()
        {
            return View();
        }

        /// <summary>
        /// 护林员信息
        /// </summary>
        /// <returns></returns>
        public ActionResult HuserInfoIndex()
        {
            string hid = Request.Params["hid"];
            ViewBag.hid=hid;
            if (string.IsNullOrEmpty(hid))
            {
                return Content("护林员id传输错误");
            }
            //获取最新护林员当前坐标点
            var sw = new T_IPS_REALDATATEMPORARYSW();
            sw.USERID = hid;
            var model = T_IPS_REALDATATEMPORARYCls.getTopOneModelList(sw).FirstOrDefault();
            CookieModel cookieInfo1 = SystemCls.getCookieInfo();
            ViewBag.LAYERID = T_SYS_LAYERCls.getLayerHuLinYuanLAYERID(new T_SYS_LAYER_SW { USERID = cookieInfo1.UID });
            return View(model);
        }

        /// <summary>
        /// fly 文件上传
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadFlyIndex()
        {
            string url = Request.Params["localurl"];
            string jcfid = Request.Params["jcfid"];
            //C:/Users/yayn/AppData/Roaming/Skyline/TerraExplorer/201632691820.fly
            //C:/Users/yayn/AppData/Roaming/==>%AppData%
            var arr = url.Split('/');
            var index = arr.ToList().IndexOf("Roaming");
            string strurl = "%AppData%";
            for (int i = index + 1; i < arr.Length; i++)
            {
                strurl += "\\" + arr[i];
            }
            ViewBag.url = strurl.TrimEnd('\\');
            ViewBag.jcfid = jcfid;
            return View();
        }

        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase upFile, string jcfid, string subjectName)
        {
            if (upFile == null)
            {
                return Json(new { purl = "", error = "请上传文件" });
            }
            string fileName = System.IO.Path.GetFileName(upFile.FileName);
            string ipath = System.Configuration.ConfigurationManager.AppSettings["FireFlaPath"].ToString();//相对路径
            string PhysicalPath = Server.MapPath(ipath + "\\");
            if (!Directory.Exists(PhysicalPath))//判断文件夹是否已经存在
            {
                Directory.CreateDirectory(PhysicalPath);//创建文件夹
            }
            string filePhysicalPath = PhysicalPath + fileName;
            string purl = "", error = "";
         
            try
            {
                //int i = 0;
                upFile.SaveAs(filePhysicalPath);
                //写库
                var m = new JC_FIRE_PLOTTING_Model();//监测_火情标绘表
                m.opMethod = "Add";
                m.JCFID = jcfid;
                m.PLOTTINGFILENAME = fileName;//文件名称
                m.PLOTTINGTIME = DateTime.Now.ToString();//标绘时间
                var infolist = JC_FIRE_PLOTTINGCls.getModelList(new JC_FIRE_PLOTTING_SW { JCFID = jcfid });
                //if (infolist.Any())
                //{
                //    i = infolist.Max(p => Convert.ToInt32(p.PLOTTINGTITLE));//获取最大序号标题
                //}
                m.PLOTTINGTITLE = subjectName;
                var ms = JC_FIRE_PLOTTINGCls.Manager(m);
                purl = PhysicalPath + fileName;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return Json(new{ purl = purl, error = error });
        }

        #region Private
        /// <summary>
        /// 根据jcfid获取fla文件
        /// </summary>
        /// <returns></returns>
        private IEnumerable<JC_FIRE_PLOTTING_Model> GetFlaModelList(string jcfid)
        {
            var list = JC_FIRE_PLOTTINGCls.getModelList(new JC_FIRE_PLOTTING_SW { JCFID = jcfid });
            return list;

        }
        /// <summary>
        /// 获取当前火情
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EHCurFireMode> GetCUrFireList()
        {
            var result = new List<EHCurFireMode>();
            var jcfirelist = JC_FIRECls.GetListModel(new JC_FIRE_SW { BYORGNO = SystemCls.getCurUserOrgNo() }).Where(p => (p.ISOUTFIRE.Trim() != "1"));//监测火情信息
            if (jcfirelist.Any())
            {
                foreach (var item in jcfirelist)
                {
                    var firefkmodel = JC_FIRETICKLINGCls.GetModelList(new JC_FIRETICKLING_SW() { JCFID = item.JCFID, ISOUTFIRE = "0" }).Where(p => p.HOTTYPE == "1" || p.HOTTYPE == "6" || p.HOTTYPE == "10").FirstOrDefault();
                    if (firefkmodel != null)
                    {
                        var model = new EHCurFireMode();
                        model.ADDRESSS = item.ZQWZ;
                        model.FIRENAME = item.FIRENAME;
                        model.JCFID = item.JCFID;
                        model.JD = item.JD;
                        model.WD = item.WD;
                        model.ORGNO = item.BYORGNO;
                        var record = JC_FIRE_PROPCls.getModel(new JC_FIRE_PROP_SW { JCFID = item.JCFID });
                        if (record != null)
                        {
                            model.FIRELEVEL = record.FIRELEVEL;
                        }
                        if (model.FIRELEVEL == null) model.FIRELEVEL = "";
                        result.Add(model);
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// 获取火情属性model
        /// </summary>
        /// <param name="jcfid"></param>
        /// <returns></returns>
        private YAFireLevelInfoModel GetFireLevelModel(string jcfid)
        {
            var model = new YAFireLevelInfoModel();
            var record = JC_FIRECls.GetListModel(new JC_FIRE_SW { JCFID = jcfid }).FirstOrDefault();
            if (record != null)
            {
                model.JCFID = record.JCFID;
                model.FIRETIME = record.FIRETIME;
                model.FIREENDTIME = record.FIREENDTIME;
                model.ISOUTFIRE = record.ISOUTFIRE;
                model.JD = record.JD;
                model.WD = record.WD;
                var recordsx = JC_FIRE_PROPCls.getModel(new JC_FIRE_PROP_SW { JCFID = jcfid });//火情属性
                if (recordsx != null)
                {
                    model.JC_FIRE_PROPID = recordsx.JC_FIRE_PROPID;
                    model.GHMJ = recordsx.GHMJ;
                    model.GHLDMJ = recordsx.GHLDMJ;
                    model.SHSLMJ = recordsx.SHSLMJ;
                    model.RYS = recordsx.RYS;
                    model.RYW = recordsx.RYW;
                    model.MGSD = recordsx.MGSD;
                    model.ZDQY = recordsx.ZDQY;
                    model.GJJL = recordsx.GJJL;
                    model.ZZH = recordsx.ZZH;
                    model.QHS = recordsx.QHS;
                    model.SSJB = recordsx.SSJB;
                    model.FIRELEVEL = recordsx.FIRELEVEL;
                    model.FIRECODE = recordsx.FIRECODE;
                }
            }
            return model;
        }

        #endregion

        #region Ajax

        /// <summary>
        /// 获取蔓延分析结果
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAnalysysResult()
        {
            MessageObject ms = null;
            string jd = Request.Params["jd"];//经度 
            string wd = Request.Params["wd"];// 纬度 
            string dWindDirection = Request.Params["dWindDirection"];//风向角度值
            string dWindSpeed = Request.Params["dWindSpeed"];//风力
            string dHumidity = Request.Params["dHumidity"];//湿度值
            string dTemperature = Request.Params["dTemperature"];//温度值
            string dTime = Request.Params["dTime"];//分段值
            SpringerServiceReference.FireSpreadServiceClient clinet = new SpringerServiceReference.FireSpreadServiceClient();
            var list = clinet.FireSpread(jd, wd, Convert.ToDouble(dWindDirection), Convert.ToDouble(dWindSpeed), Convert.ToDouble(dHumidity),
                Convert.ToDouble(dTemperature), -1, Convert.ToDouble(dTime), false);
            if (list.Any())
            {
                ms = new MessageObject(true, list);
            }
            else
            {
                ms = new MessageObject(false, null);
            }
            return Json(ms);
        }

        /// <summary>
        /// 删除标绘
        /// </summary>
        /// <returns></returns>
        public JsonResult RemoveFlyFire()
        {
            string id = Request.Params["id"];
            var m = new JC_FIRE_PLOTTING_Model();
            m.opMethod = "Del";
            m.JC_FIRE_PLOTTINGID = id;
            var ms = JC_FIRE_PLOTTINGCls.Manager(m);
            return Json(ms);
        }

        /// <summary>
        /// Ajax获取fly文件
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFlyFireList()
        {
            var path = System.Configuration.ConfigurationManager.AppSettings["FireFlaPath"].ToString();
            MessageListObject ms = null;
            var result = new List<FlaModel>();
            string jcfid = Request.Params["jcfid"];
            var list = GetFlaModelList(jcfid);
            foreach (var item in list)
            {
                var info = new FlaModel();
                info.InjectFrom(item);
                // info.PLOTTINGFILENAME = path.Replace('~', ' ') + "/" + item.PLOTTINGFILENAME;
                result.Add(info);
            }
            ms = new MessageListObject(true, result);
            return Json(ms);

        }

        /// <summary>
        /// 保存火险属性
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveFireLevelSX()
        {
            string propid = Request.Params["propid"];
            string jcfid = Request.Params["jcfid"];
            string ghmj = Request.Params["ghmj"];
            string ghldmj = Request.Params["ghldmj"];
            string shslmj = Request.Params["shslmj"];
            string rys = Request.Params["rys"];
            string ryw = Request.Params["ryw"];
            string mg = Request.Params["mg"];
            string zd = Request.Params["zd"];
            string gjjl = Request.Params["gjjl"];
            string zzh = Request.Params["zzh"];
            string qhs = Request.Params["qhs"];
            string ssjb = Request.Params["ssjb"];
            string firelevel = Request.Params["firelevel"];
            string firecode = Request.Params["firecode"];

            var m = new JC_FIRE_PROP_Model();
            m.opMethod = "Save";
            m.JC_FIRE_PROPID = propid;
            m.JCFID = jcfid;
            m.GHMJ = ghmj;
            m.GHLDMJ = ghldmj;
            m.SHSLMJ = shslmj;
            m.RYS = rys;
            m.RYW = ryw;
            m.MGSD = mg;
            m.ZDQY = zd;
            m.GJJL = gjjl;
            m.ZZH = zzh;
            m.QHS = qhs;
            m.SSJB = ssjb;
            m.FIRELEVEL = firelevel;
            m.FIRECODE = firecode;
            var ms = JC_FIRE_PROPCls.Manager(m);
            return Json(ms);
        }


        /// <summary>
        /// 获取属性
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSXModel()
        {
            string jcfid = Request.Params["jcfid"];
            var model = GetFireLevelModel(jcfid);
            return Json(new MessageObject(true, model));
        }
        #endregion

        /// <summary>
        /// 当前火情
        /// </summary>
        /// <returns></returns>
        public ActionResult test()
        {
            pubViewBag("012002", "012002", "");
            var model = GetCUrFireList();
            return View(model);
        }

        #region Ajax
        /// <summary>
        /// 火情结束
        /// </summary>
        /// <returns></returns>
        public JsonResult FireOverMethod()
        {
            Message ms = null;
            string jcfid = Request.Params["jcfid"];
            var m = new JC_FIRE_Model();
            m.JCFID = jcfid;
            m.ISOUTFIRE = "1";
            m.FIREENDTIME = DateTime.Now.ToString();
            ms = JC_FIRECls.MdyJCFireOver(m);
            return Json(ms);
        }
        /// <summary>
        /// ajax 获取当前火情
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCurrentFireInfo()
        {
            MessageListObject ms = null;
            var list = GetCUrFireList();
            ms = new MessageListObject(true, list);
            return Json(ms);
        }

        /// <summary>
        /// 获取图层查询结果
        /// </summary>
        /// <returns></returns>
        public JsonResult GetQueryLayerInfos()
        {
            string name = Request.Params["name"];//名称
            string flagstr = Request.Params["flagstr"];//图层类型
            var sw = new QueryLayerDataSW();
            sw.FlagStr = flagstr;
            sw.Name = name;
            var list = TD_CUNZHUDICls.GetQueryLayerUnionResult(sw);
            var ms = new MessageListObject(true, list);
            return Json(ms);
        }


        /// <summary>
        /// 获取图层
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAroundLayersInfo()
        {
            var result = new List<SDE_QueyLayerResultModel>();
            string flagstr = Request.Params["flagstr"];//图层类型
            string disval = Request.Params["disval"];//周边距离
            string jd = Request.Params["jd"];//经度
            string wd = Request.Params["wd"];//纬度
            var sw = new QueryLayerDataSW();
            sw.FlagStr = flagstr;
            sw.AroundValue = disval;
            sw.JD = jd;
            sw.WD = wd;
            //获取符合条件的结果
            var list = TD_CUNZHUDICls.GetQueryLayerUnionResult(sw);
            var layers = flagstr.Split(',');
            if (layers.Length > 0)
            {
                foreach (var item in layers)
                {
                    var model = new SDE_QueyLayerResultModel();
                    model.LayerId = item.Trim();
                    model.LayerName = Enum.GetName(typeof(ManagerSystemClassLibrary.EnumCls.LayerType), int.Parse(model.LayerId));
                    if (list.Any())
                    {
                        var recordlist = list.Where(p => p.Flag == item);//每个图层里的信息
                        if (recordlist.Any())
                        {
                            foreach (var record in recordlist)
                            {
                                var data = new SDE_QueyLayerModel();
                                data.ID = record.ID;
                                data.Flag = record.Flag;
                                data.Display_X = record.Display_X;
                                data.Display_Y = record.Display_Y;
                                data.LayerName = model.LayerName;
                                data.Name = record.Name;
                                data.LNGLATSTRS = record.LNGLATSTRS;
                                data.DBTYPE = record.DBTYPE;
                                data.TYPE = record.TYPE;
                                data.CATEGORY = record.CATEGORY;
                                data.ImageUrl = record.ImageUrl;
                                model.DataList.Add(data);
                            }
                        }
                    }
                    result.Add(model);
                }
            }
            return Json(result);
        }

        /// <summary>
        /// 上传测试 弃用
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadFile()
        {
            string localpath = Request.Params["localpath"];//本地路径（包含文件名）
            string jcfid = Request.Params["jcfid"];//监测火情id
            Message ms = null;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string Path = "";
            string[] arr = hfc[0].FileName.Split('.');
            if (hfc.Count > 0)
            {
                string ipath = System.Configuration.ConfigurationManager.AppSettings["FireFlaPath"].ToString();//相对路径
                string PhysicalPath = Server.MapPath(ipath + "\\");
                if (!Directory.Exists(PhysicalPath))//判断文件夹是否已经存在
                {
                    Directory.CreateDirectory(PhysicalPath);//创建文件夹
                }
                for (int i = 0; i < hfc.Count; i++)
                {

                    Path = PhysicalPath + "/" + hfc[i].FileName;
                    hfc[i].SaveAs(Path);

                }
            }
            ms = new Message(true, "上传成功", "");
            return Json(ms);
        }


        /// <summary>
        /// 本地文件上传服务器
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadFileFromLocal()
        {
            ILog logs = LogHelper.GetInstance();
            Message ms = null;
            string localpath = Request.Params["localpath"];//本地路径（包含文件名）
            string jcfid = Request.Params["jcfid"];//监测火情id

            FileStream myStream = new FileStream(localpath, FileMode.Open, FileAccess.Read);
            byte[] dataByte = new byte[myStream.Length];
            myStream.Read(dataByte, 0, dataByte.Length);        //写到2进制数组中
            myStream.Close();

            FileStream fos = null;
            try
            {
                var i = 0;
                string ipath = System.Configuration.ConfigurationManager.AppSettings["FireFlaPath"].ToString();//相对路径
                string PhysicalPath = Server.MapPath(ipath + "\\");
                if (!Directory.Exists(PhysicalPath))//判断文件夹是否已经存在
                {
                    Directory.CreateDirectory(PhysicalPath);//创建文件夹
                }
                string filename = System.IO.Path.GetFileName(localpath);//文件名  
                fos = new FileStream(PhysicalPath + filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fos.Write(dataByte, 0, dataByte.Length);
                fos.Close();

                //写库
                var m = new JC_FIRE_PLOTTING_Model();//监测_火情标绘表
                m.opMethod = "Add";
                m.JCFID = jcfid;
                m.PLOTTINGFILENAME = filename;//文件名称
                m.PLOTTINGTIME = DateTime.Now.ToString();//标绘时间
                var infolist = JC_FIRE_PLOTTINGCls.getModelList(new JC_FIRE_PLOTTING_SW { JCFID = jcfid });
                if (infolist.Any())
                {
                    i = infolist.Max(p => Convert.ToInt32(p.PLOTTINGTITLE));//获取最大序号标题
                }
                m.PLOTTINGTITLE = (++i).ToString();
                ms = JC_FIRE_PLOTTINGCls.Manager(m);
                if (ms.Success == true)
                {
                    ms = new Message(true, "上传服务器成功", "");
                }
                else
                {
                    ms = new Message(false, "上传服务器出错", "");
                }
            }
            catch (Exception ex)
            {
                logs.Error(ex.Message);
                ms = new Message(false, "上传服务器出错", "");
            }
            finally
            {
                if (fos != null)
                {
                    try
                    {
                        fos.Close();
                    }
                    catch
                    {
                        ms = new Message(false, "上传服务器出错", "");
                    }
                }
            }
            return Json(ms);
        }


        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://localhost/test111/113.txt");  //地址是你要上传文件并且在服务器上创建的文件名
        //request.Method = WebRequestMethods.File.UploadFile;
        //request.AllowWriteStreamBuffering = true;
        //Stream s = request.GetRequestStream();
        //FileStream fs = new FileStream(@"C:\text.txt", FileMode.Open, FileAccess.Read);
        //byte[] b = new byte[fs.Length];
        //fs.Read(b, 0, b.Length);
        //s.Write(b, 0, b.Length);
        //s.Flush();
        //fs.Close();
        //s.Close();
        //request.GetResponse();  //这句话一定要写否则虚拟目录下出不来文件
        #endregion
    }
}
