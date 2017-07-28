using ManagerSystem.MVC.HelpCom;
using ManagerSystem.MVC.Models;
using ManagerSystemClassLibrary;
using ManagerSystemModel;
using ManagerSystemModel.ExtenAttribute;
using ManagerSystemModel.LogicModel;
using ManagerSystemSearchWhereModel;
using ManagerSystemSearchWhereModel.LogicModel;
using PublicClassLibrary;
using PublicClassLibrary.PublicCom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace ManagerSystem.MVC.Controllers
{
    /// <summary>
    /// 地图共用界面
    /// </summary>
    public class MapCommonController : Controller
    {
        /// <summary>
        /// client
        /// </summary>
        SpringerServiceReference.IFireSpreadService client;

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
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
            else
            {
                sw.BYORGNO = SystemCls.getCurUserOrgNo();
            }
            var list = JC_FIRECls.GetListModel(sw);
            if (list.Any())
            {
                foreach (var item in list)
                {
                    var info = JC_FIRETICKLINGCls.GetFKFireInfoData(item.JCFID);
                    var model = new MapShowModel();
                    model.JCFID = item.JCFID;
                    model.AREA = item.ZQWZ;
                    model.BH = item.WXBH;
                    model.JD = item.JD;
                    model.WD = item.WD;
                    model.FKSTATE = StateSwitch.QSStateNew(SystemCls.getCurUserOrgNo(), info.JC_FireFKData.MANSTATE);
                    result.Add(model);
                }
            }
            return View(result);
        }

        /// <summary>
        /// 获取坐标点
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMapPontIndex()
        {
            string jd = Request.Params["jd"];
            string wd = Request.Params["wd"];
            string line = Request.Params["LINE"];
            #region 线
            if (line == "1")
            {
                ViewBag.type = "1";
                string jwdlist = Request.Params["jwdlist"];
                if (!string.IsNullOrEmpty(jwdlist))
                {
                    ViewBag.jwdlist = jwdlist;
                    ViewBag.method = "getLocaCollectLine()";
                }
                else
                {
                    ViewBag.jwdlist = "";
                    ViewBag.method = "";
                }
            }
            #endregion

            #region 面
            else if (line == "2")
            {
                ViewBag.type = "2";
                string jwdlist = Request.Params["jwdlist"];
                if (!string.IsNullOrEmpty(jwdlist))
                {
                    ViewBag.jwdlist = jwdlist;
                    ViewBag.method = "getLocaCollectPolygon()";
                }
                else
                {
                    ViewBag.jwdlist = "";
                    ViewBag.method = "";
                }
            }
            #endregion

            #region 点
            else
            {
                if (string.IsNullOrEmpty(jd) || string.IsNullOrEmpty(wd))
                {
                    ViewBag.type = "0";
                    ViewBag.method = "setLocationByAddress()";
                }
                else
                {
                    ViewBag.type = "0";
                    ViewBag.method = "getLocaCollectPont(" + jd + "," + wd + ")";
                }
            }
            #endregion
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
            return View();
        }

        /// <summary>
        /// 保存点新的经度纬度
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveMapPoint()
        {
            MessageObject ms = null;
            string tablename = Request.Params["tablename"];//表名
            string id = Request.Params["id"];
            string jd = Request.Params["jd"];
            string wd = Request.Params["wd"];
            string key = GetKey(tablename);//主键
            if (tablename != "DC_CAR" && tablename != "JC_FIRE" && tablename != "")
            {
                if (jd == "" || wd == "")
                {
                    ms = new MessageObject(false, null);
                }
                else
                {
                    var model = ManagerSystemClassLibrary.BaseDT.Map_Common.updatePoint(jd, wd, tablename, id, key);
                    if (model.Success == false)
                    {
                        ms = new MessageObject(false, null);
                    }
                    else
                    {
                        ms = new MessageObject(true, model);
                    }
                }
            }
            return Json(ms);
        }

        #region 火点周边护林员
        /// <summary>
        /// 火点周边护林员
        /// </summary>
        /// <returns></returns>
        public JsonResult GetHlyByArea()
        {
            Message ms;
            string uidstr = string.Empty;//护林员id字符串集合
            string area = Request.Params["area"];//周边距离
            string jd = Request.Params["jd"];//经度
            string wd = Request.Params["wd"];//纬度
            if (!string.IsNullOrEmpty(area) && !string.IsNullOrEmpty(jd) && !string.IsNullOrEmpty(wd))
            {
                var model = new HlyAreaDataSW();
                model.DATETIME = DateTime.Now.ToString("yyyy-MM-dd");
                model.JD = jd;
                model.WD = wd;
                model.AREA = area;
                var RealDataTemopryList = T_IPS_REALDATATEMPORARYCls.getModelList(model);//符合周边距离分析数据
                if (RealDataTemopryList.Any())
                {
                    foreach (var RealData in RealDataTemopryList)
                    {
                        if (!string.IsNullOrEmpty(RealData.LATITUDE) && !string.IsNullOrEmpty(RealData.LONGITUDE))
                        {
                            uidstr += RealData.USERID + ",";
                        }
                    }
                    if (string.IsNullOrEmpty(uidstr))
                    {
                        ms = new Message(false, "未检索到附近的护林员", "");
                    }
                    else
                    {
                        ms = new Message(true, uidstr.TrimEnd(','), "");
                    }

                }
                else
                {
                    ms = new Message(false, "未检索到附近的护林员", "");
                }
            }
            else
            {
                ms = new Message(false, "周围距离无值", "");
            }
            return Json(ms);
        }
        #endregion

        /// <summary>
        /// 保存线新的经度纬度
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveMapLine()
        {
            MessageObject ms = null;
            string tablename = Request.Params["tablename"];//表名
            string id = Request.Params["id"];
            //string jd = Request.Params["jd"];
            //string wd = Request.Params["wd"];
            //string jd1 = Request.Params["jd1"];
            //string wd1 = Request.Params["wd1"];
            string key = GetKey(tablename);//主键
            string JWDLIST = Request.Params["JWDLIST"];
            if (tablename != "DC_CAR" && tablename != "JC_FIRE" && tablename != "")
            {
                if (JWDLIST == "")
                {
                    ms = new MessageObject(false, null);
                }
                else
                {
                    var model = ManagerSystemClassLibrary.BaseDT.Map_Common.updateLine(JWDLIST, tablename, id, key);
                    if (model.Success == false)
                    {
                        ms = new MessageObject(false, null);
                    }
                    else
                    {
                        ms = new MessageObject(true, model);
                    }
                }
            }
            return Json(ms);
        }

        /// <summary>
        /// 获取表的主键
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public string GetKey(string tablename)
        {
            string result = string.Empty;
            switch (tablename)
            {
                case "DC_ARMY"://队伍
                    result = "DC_ARMY_ID";
                    break;
                case "DC_RESOURCE_NEW"://资源
                    result = "DC_RESOURCE_NEW_ID";
                    break;
                case "DC_UTILITY_CAMP"://营房
                    result = "DC_UTILITY_CAMP_ID";
                    break;
                case "DC_EQUIP_NEW"://设备
                    result = "DC_EQUIP_NEW_ID";
                    break;
                case "DC_UTILITY_OVERWATCH": //瞭望台
                    result = "DC_UTILITY_OVERWATCH_ID";
                    break;
                case "DC_UTILITY_PROPAGANDASTELE"://宣传碑牌
                    result = "DC_UTILITY_PROPAGANDASTELE_ID";
                    break;
                case "DC_UTILITY_RELAY"://中继站
                    result = "DC_UTILITY_RELAY_ID";
                    break;
                case "DC_UTILITY_MONITORINGSTATION"://监测站
                    result = "DC_UTILITY_MONITORINGSTATION_ID";
                    break;
                case "DC_UTILITY_FACTORCOLLECTSTATION"://因子采集站
                    result = "DC_UTILITY_FACTORCOLLECTSTATION_ID";
                    break;
                case "DC_UTILITY_ISOLATIONSTRIP"://隔离带
                    result = "DC_UTILITY_ISOLATIONSTRIP_ID";
                    break;
                case "DC_UTILITY_FIRECHANNEL"://防火通道
                    result = "DC_UTILITY_FIRECHANNEL_ID";
                    break;
                case "DC_REPOSITORY"://仓库
                    result = "DCREPOSITORYID";
                    break;
                case "JC_INFRAREDCAMERA_BASICINFO"://红外相机
                    result = "INFRAREDCAMERAID";
                    break;
                case "TD_POINTMARK"://自然村
                    result = "OBJECTID";
                    break;
                case "TD_MOUNTAIN"://自定义数据
                    result = "OBJECTID";
                    break;
                case "CUNZHUDI"://自定义数据
                    result = "OBJECTID";
                    break;
            }
            return result;
        }

        /// <summary>
        /// 地图定位展示(点 线 面)
        /// </summary>
        /// <returns></returns>
        public ActionResult MapPostionCommonIndex()
        {
            string tablename = Request.Params["tablename"];
            string id = Request.Params["id"];
            string type = Request.Params["type"];//0表示点 1 表示线  2 表示面
            if (string.IsNullOrEmpty(tablename) || string.IsNullOrEmpty(id))
                ViewBag.method = "ShowMsg()";
            else
            {
                #region 线
                if (type == "1")
                {
                    if (tablename.Trim().ToUpper() == "DC_UTILITY_FIRECHANNEL")
                    {
                        var model = DC_UTILITY_FIRECHANNELCls.getModel(new DC_UTILITY_FIRECHANNEL_SW { DC_UTILITY_FIRECHANNEL_ID = id });
                        if (!string.IsNullOrEmpty(model.JWDLIST))
                        {
                            ViewBag.jwdlist = model.JWDLIST;
                            ViewBag.method = "getLocaCollectLine()";//地图线定位
                        }
                        else
                            ViewBag.method = "ShowMsg()";
                    }
                    else if (tablename.Trim().ToUpper() == "DC_UTILITY_ISOLATIONSTRIP")
                    {
                        var model = DC_UTILITY_ISOLATIONSTRIPCls.getModel(new DC_UTILITY_ISOLATIONSTRIP_SW { DC_UTILITY_ISOLATIONSTRIP_ID = id });
                        if (!string.IsNullOrEmpty(model.JWDLIST))
                        {
                            ViewBag.jwdlist = model.JWDLIST;
                            ViewBag.method = "getLocaCollectLine()";//地图定位
                        }
                        else
                            ViewBag.method = "ShowMsg()";
                    }
                }
                #endregion

                #region 面
                if (type == "2")
                {
                    if (tablename.Trim().ToUpper() == "DC_RESOURCE_NEW")
                    {
                        var model = DC_RESOURCE_NEWCls.getModel(new DC_RESOURCE_NEW_SW { DC_RESOURCE_NEW_ID = id });
                        if (!string.IsNullOrEmpty(model.JWDLIST))
                        {
                            ViewBag.jwdlist = model.JWDLIST;
                            ViewBag.method = "getLocaCollectPolygon()";//地图定位
                        }
                        else
                            ViewBag.method = "ShowMsg()";
                    }
                    else if (tablename.Trim().ToUpper() == "DC_UTILITY_ISOLATIONSTRIP")
                    {
                        var model = DC_UTILITY_ISOLATIONSTRIPCls.getModel(new DC_UTILITY_ISOLATIONSTRIP_SW { DC_UTILITY_ISOLATIONSTRIP_ID = id });
                        if (!string.IsNullOrEmpty(model.JWDLIST))
                        {
                            ViewBag.jwdlist = model.JWDLIST;
                            ViewBag.method = "getLocaCollectPolygon()";//地图定位
                        }
                        else
                            ViewBag.method = "ShowMsg()";
                    }
                    else if (tablename.Trim().ToUpper() == "PEST_COLLECTDATA")
                    {
                        var model = PEST_COLLECTDATACls.getModel(new PEST_COLLECTDATA_SW { PESTCOLLDATAID = id });
                        if (!string.IsNullOrEmpty(model.JWDLIST))
                        {
                            ViewBag.jwdlist = model.JWDLIST;
                            ViewBag.method = "getLocaCollectPolygon()";//地图定位
                        }
                        else
                            ViewBag.method = "ShowMsg()";
                    }
                }
                #endregion

                #region DC_REPOSITORY
                else if (tablename.Trim().ToUpper() == "DC_REPOSITORY")
                {
                    var model = DC_REPOSITORYCls.getModel(new DC_REPOSITORY_SW { DCREPOSITORYID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                        ViewBag.method = "getLocaCollectPont(" + model.JD + "," + model.WD + ")";//地图定位
                    else
                        ViewBag.method = "ShowMsg()";
                }
                #endregion

                #region TD_POINTMARK
                if (tablename.Trim().ToUpper() == "TD_POINTMARK")
                {
                    var model = TD_POINTMARKCls.getModel(new TD_POINTMARK_SW { OBJECTID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                    {
                        double[] arr = ClsPositionTrans.GpsTransform(double.Parse(model.WD), double.Parse(model.JD), "1");
                        string JD = arr[1].ToString();
                        string WD = arr[0].ToString();
                        ViewBag.method = "getLocaCollectPont(" + JD + "," + WD + ")";//地图定位
                    }
                    else
                        ViewBag.method = "ShowMsg()";
                } 
                #endregion

                #region TD_MOUNTAIN
                if (tablename.Trim().ToUpper() == "TD_MOUNTAIN")
                {
                    var model = TD_MOUNTAINCls.getModel(new TD_MOUNTAIN_SW { OBJECTID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                    {
                        double[] arr = ClsPositionTrans.GpsTransform(double.Parse(model.WD), double.Parse(model.JD), "1");
                        string JD = arr[1].ToString();
                        string WD = arr[0].ToString();
                        ViewBag.method = "getLocaCollectPont(" + JD + "," + WD + ")";//地图定位
                    }
                    else
                        ViewBag.method = "ShowMsg()";
                } 
                #endregion

                #region DC_ARMY
                if (tablename.Trim().ToUpper() == "DC_ARMY")
                {
                    var model = DC_ARMYCls.getModel(new DC_ARMY_SW { DC_ARMY_ID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                        ViewBag.method = "getLocaCollectPont(" + model.JD + "," + model.WD + ")";//地图定位
                    else
                        ViewBag.method = "ShowMsg()";
                } 
                #endregion

                #region DC_CAR
                if (tablename.Trim().ToUpper() == "DC_CAR")
                {
                    var model = DC_CARCls.getModel(new DC_CAR_SW { DC_CAR_ID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                        ViewBag.method = "getLocaCollectPont(" + model.JD + "," + model.WD + ")";//地图定位
                    else
                        ViewBag.method = "ShowMsg()";
                } 
                #endregion

                #region DC_EQUIP_NEW
                if (tablename.Trim().ToUpper() == "DC_EQUIP_NEW")
                {
                    var model = DC_EQUIP_NEWCls.getModel(new DC_EQUIP_NEW_SW { DC_EQUIP_NEW_ID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                        ViewBag.method = "getLocaCollectPont(" + model.JD + "," + model.WD + ")";//地图定位
                    else
                        ViewBag.method = "ShowMsg()";
                } 
                #endregion

                #region DC_UTILITY_CAMP
                if (tablename.Trim().ToUpper() == "DC_UTILITY_CAMP")
                {
                    var model = DC_UTILITY_CAMPCls.getModel(new DC_UTILITY_CAMP_SW { DC_UTILITY_CAMP_ID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                        ViewBag.method = "getLocaCollectPont(" + model.JD + "," + model.WD + ")";//地图定位
                    else
                        ViewBag.method = "ShowMsg()";
                } 
                #endregion

                #region DC_UTILITY_OVERWATCH
                if (tablename.Trim().ToUpper() == "DC_UTILITY_OVERWATCH")
                {
                    var model = DC_UTILITY_OVERWATCHCls.getModel(new DC_UTILITY_OVERWATCH_SW { DC_UTILITY_OVERWATCH_ID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                        ViewBag.method = "getLocaCollectPont(" + model.JD + "," + model.WD + ")";//地图定位
                    else
                        ViewBag.method = "ShowMsg()";
                } 
                #endregion

                #region DC_UTILITY_PROPAGANDASTELE
                if (tablename.Trim().ToUpper() == "DC_UTILITY_PROPAGANDASTELE")
                {
                    var model = DC_UTILITY_PROPAGANDASTELECls.getModel(new DC_UTILITY_PROPAGANDASTELE_SW { DC_UTILITY_PROPAGANDASTELE_ID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                        ViewBag.method = "getLocaCollectPont(" + model.JD + "," + model.WD + ")";//地图定位
                    else
                        ViewBag.method = "ShowMsg()";
                } 
                #endregion

                #region DC_UTILITY_RELAY
                if (tablename.Trim().ToUpper() == "DC_UTILITY_RELAY")
                {
                    var model = DC_UTILITY_RELAYCls.getModel(new DC_UTILITY_RELAY_SW { DC_UTILITY_RELAY_ID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                        ViewBag.method = "getLocaCollectPont(" + model.JD + "," + model.WD + ")";//地图定位
                    else
                        ViewBag.method = "ShowMsg()";
                } 
                #endregion

                #region DC_UTILITY_MONITORINGSTATION
                if (tablename.Trim().ToUpper() == "DC_UTILITY_MONITORINGSTATION")
                {
                    var model = DC_UTILITY_MONITORINGSTATIONCls.getModel(new DC_UTILITY_MONITORINGSTATION_SW { DC_UTILITY_MONITORINGSTATION_ID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                        ViewBag.method = "getLocaCollectPont(" + model.JD + "," + model.WD + ")";//地图定位
                    else
                        ViewBag.method = "ShowMsg()";
                } 
                #endregion

                #region DC_UTILITY_FACTORCOLLECTSTATION
                if (tablename.Trim().ToUpper() == "DC_UTILITY_FACTORCOLLECTSTATION")
                {
                    var model = DC_UTILITY_FACTORCOLLECTSTATIONCls.getModel(new DC_UTILITY_FACTORCOLLECTSTATION_SW { DC_UTILITY_FACTORCOLLECTSTATION_ID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                        ViewBag.method = "getLocaCollectPont(" + model.JD + "," + model.WD + ")";//地图定位
                    else
                        ViewBag.method = "ShowMsg()";
                } 
                #endregion

                #region JC_FIRE
                if (tablename.Trim().ToUpper() == "JC_FIRE")
                {
                    var model = JC_FIRECls.getModel(new JC_FIRE_SW { JCFID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                        ViewBag.method = "getLocaCollectPont(" + model.JD + "," + model.WD + ")";//地图定位
                    else
                        ViewBag.method = "ShowMsg()";
                } 
                #endregion

                #region JC_MONITOR_BASICINFO
                if (tablename.Trim().ToUpper() == "JC_MONITOR_BASICINFO")
                {
                    var model = JC_MONITORCls.getModel(new JC_MONITOR_BASICINFO_SW { EMID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                        ViewBag.method = "getLocaCollectPont(" + model.JD + "," + model.WD + ")";//地图定位
                    else
                        ViewBag.method = "ShowMsg()";
                } 
                #endregion

                #region JC_INFRAREDCAMERA_BASICINFO
                if (tablename.Trim().ToUpper() == "JC_INFRAREDCAMERA_BASICINFO")
                {
                    var model = JC_INFRAREDCAMERACls.getModel(new JC_INFRAREDCAMERA_BASICINFO_SW { INFRAREDCAMERAID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                        ViewBag.method = "getLocaCollectPont(" + model.JD + "," + model.WD + ")";//地图定位
                    else
                        ViewBag.method = "ShowMsg()";
                }
                #endregion

                #region PEST_MONITORINGSTATION
                if (tablename.Trim().ToUpper() == "PEST_MONITORINGSTATION")
                {
                    var model = PEST_MONITORINGSTATIONCls.getModel(new PEST_MONITORINGSTATION_SW { PEST_MONITORINGSTATIONID = id });
                    if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
                        ViewBag.method = "getLocaCollectPont(" + model.JD + "," + model.WD + ")";//地图定位
                    else
                        ViewBag.method = "ShowMsg()";
                } 
                #endregion
            }
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
            return View();
        }

        /// <summary>
        /// 地图弹出框展示详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult PopViewIndex()
        {
            string dbType = Request.Params["dbType"];//数据表名类型
            string dbid = Request.Params["dbid"];//数据类型id
            int type = int.Parse(dbType);
            //将int类型值转换为对应的枚举 
            DbCenterSourceType dbname = (DbCenterSourceType)type;
            string key = GetKey(dbname.ToString());//获取表主键key名称
            ViewBag.CommonID = dbid;//获取id主键
            ViewBag.id = "id";
            if (type == 1)
            {
                ViewBag.data = GetDicDataModel<DC_ARMY_Model>(dbname.ToString(), key, dbid);
                ViewBag.type1 = "ARMYEQUIP";
                ViewBag.type2 = "ARMYMEMBER";
                ViewBag.type3 = "ARMYPHOTO";
                ViewBag.PhotoType = "DC_ARMY";//照片表中的照片类型
                ViewBag.Select = "1";
            }
            else if (type == 2)
            {
                ViewBag.Select = "2";
                ViewBag.type3 = "RESOURCEPHOTO";
                ViewBag.PhotoType = "DC_RESOURCE_NEW";//照片表中的照片类型
                ViewBag.data = GetDicDataModel<DC_RESOURCE_NEW_Model>(dbname.ToString(), key, dbid);
            }
            else if (type == 3)
            {
                ViewBag.Select = "2";
                ViewBag.type3 = "CAMPPHOTO";
                ViewBag.PhotoType = "DC_UTILITY_CAMP";//照片表中的照片类型
                ViewBag.data = GetDicDataModel<DC_UTILITY_CAMP_Model>(dbname.ToString(), key, dbid);
            }
            else if (type == 4)
            {
                ViewBag.data = GetDicDataModel<DC_EQUIP_NEW_Model>(dbname.ToString(), key, dbid);
            }
            else if (type == 5)
            {
                ViewBag.Select = "2";
                ViewBag.type3 = "OVERWATCHPHOTO";
                ViewBag.PhotoType = "DC_UTILITY_OVERWATCH";//照片表中的照片类型
                ViewBag.data = GetDicDataModel<DC_UTILITY_OVERWATCH_Model>(dbname.ToString(), key, dbid);
            }
            else if (type == 6)
            {
                ViewBag.data = GetDicDataModel<DC_UTILITY_PROPAGANDASTELE_Model>(dbname.ToString(), key, dbid);
            }
            else if (type == 7)
            {
                ViewBag.data = GetDicDataModel<DC_UTILITY_RELAY_Model>(dbname.ToString(), key, dbid);
            }
            else if (type == 8)
            {
                ViewBag.data = GetDicDataModel<DC_UTILITY_MONITORINGSTATION_Model>(dbname.ToString(), key, dbid);
            }
            else if (type == 9)
            {
                ViewBag.data = GetDicDataModel<DC_UTILITY_FACTORCOLLECTSTATION_Model>(dbname.ToString(), key, dbid);
            }
            else if (type == 10)
            {
                ViewBag.data = GetDicDataModel<DC_UTILITY_ISOLATIONSTRIP_Model>(dbname.ToString(), key, dbid);
            }
            else if (type == 11)
            {
                ViewBag.data = GetDicDataModel<DC_UTILITY_FIRECHANNEL_Model>(dbname.ToString(), key, dbid);
            }
            else if (type == 12)
            {
                ViewBag.Select = "3";
                ViewBag.type1 = "SUPPLIES";
                ViewBag.data = GetDicDataModel<DC_REPOSITORY_Model>(dbname.ToString(), key, dbid);//仓库
            }
            else if (type == 13)
            {
                ViewBag.Select = "4";
                ViewBag.type3 = "INFRAREDCAMERA_BASICINFO";
                //ViewBag.PhotoType = "DC_UTILITY_OVERWATCH";//照片表中的照片类型
                ViewBag.data = GetDicDataModel<JC_INFRAREDCAMERA_BASICINFO_Model>(dbname.ToString(), key, dbid);
            }
            return View();
        }

        /// <summary>
        /// 巡检路线和责任区页面
        /// </summary>
        public ActionResult RoutIndex()
        {
            string dbType = Request.Params["dbType"];//数据表名类型
            string dbid = Request.Params["dbid"];//数据类型id
            ViewBag.id = dbid;
            int type = int.Parse(dbType);
            DbCenterSourceType dbname = (DbCenterSourceType)type;
            string key = GetKey(dbname.ToString());//获取表主键key名称
            ViewBag.type = type;
            string polygon = "";
            string line = "";
            ViewBag.data = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { HID = dbid });
            if (type == 2)//责任区
            {
                var ROUTERAIL = T_IPSFR_ROUTERAILCls.getModelList(new T_IPSFR_ROUTERAIL_SW { HID = dbid, ROADTYPE = "1" });//责任区
                foreach (var item in ROUTERAIL)
                {
                    polygon += item.LONGITUDE + "," + item.LATITUDE + "|";
                }
                polygon = polygon.TrimEnd('|');
            }
            if (type == 1)//巡检线
            {
                var ROUTERAIL = T_IPSFR_ROUTERAILCls.getModelList(new T_IPSFR_ROUTERAIL_SW { HID = dbid, ROADTYPE = "0" });//巡检线
                foreach (var item in ROUTERAIL)
                {
                    line += item.LONGITUDE + "," + item.LATITUDE + "|";
                }
                line = line.TrimEnd('|');
            }
            ViewBag.polygon = polygon;
            ViewBag.line = line;
            return View();
        }

        /// <summary>
        /// 获取护林员线路管理坐标点_用于面积和长度计算
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult GetFRUserRots1()
        {
            string id = Request.Params["id"];//护林员id
            string type = Request.Params["type"];//点类型
            if (string.IsNullOrEmpty(id)) { return Json(new Message(false, "护林员id参数传递错误", "")); }
            var sw = new T_IPSFR_ROUTERAIL_SW();
            sw.HID = id;
            sw.ROADTYPE = type;
            var list = T_IPSFR_ROUTERAILCls.getModelList(sw);
            var ValuesList = list.Select(p => p.LINEARAEID).Distinct();
            var DataModel = new MutipileLineAndPolyModel();
            var data = new List<IEnumerable<T_IPSFR_ROUTERAIL_Model>>();
            if (ValuesList.Any())
            {
                foreach (var value in ValuesList)
                {
                    var RecordList = list.Where(p => p.LINEARAEID == value);
                    if (RecordList.Any())
                    {
                        data.Add(RecordList);
                    }
                }
            }
            DataModel.DataList = data;
            return Json(new MessageObject(true, DataModel));
        }

        /// <summary>
        /// 仓库物资统计
        /// </summary>
        /// <returns></returns>
        public ActionResult SUPPLIES()
        {
            string id = Request.Params["id"];//数据类型id
            ViewBag.REPID = id;
            ViewBag.SUPPLIES = suppliesstr(new DC_SUPPLIES_SW { REPID = id });
            return View();
        }

        /// <summary>
        /// 物资的详细信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        private string suppliesstr(DC_SUPPLIES_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>物资名称</th>");
            sb.AppendFormat("<th>装备类型</th>");
            sb.AppendFormat("<th>数量</th>");
            sb.AppendFormat("<th>物资单价(元)</th>");
            sb.AppendFormat("<th>总价值(元)</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = DC_SUPPLIESCls.getModelList(sw);
            foreach (var item in result)
            {
                sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.SUPNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.EQUIPTYPEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.DCSUPCOUNT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", item.PRICE);
                if (item.PRICE == "0" || item.DCSUPCOUNT == "0" || string.IsNullOrEmpty(item.PRICE) || string.IsNullOrEmpty(item.DCSUPCOUNT))
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", "0");
                }
                else
                {
                    sb.AppendFormat("<td class=\"center\">{0}</td>", float.Parse(item.PRICE) * int.Parse(item.DCSUPCOUNT));
                }
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// 队伍装备弹出界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ARMYEQUIP()
        {
            string id = Request.Params["id"];//数据类型id
            ViewBag.DC_ARMY_ID = id;
            ViewBag.EQUIP = ArmyEquipStr(new DC_ARMY_EQUIP_SW { DC_ARMY_ID = id });
            return View();
        }

        /// <summary>
        /// 获取队伍装备信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public string ArmyEquipStr(DC_ARMY_EQUIP_SW sw)
        {
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
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = DC_ARMY_EQUIPCls.getModelList(sw);
            foreach (var s in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.EQUIPNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.EQUIPNUM);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.EQUIPMODEL);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.EQUIPUSESTATEName);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.EQUIPSUM);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        #region 队伍的人员，装备，照片在三维中展示详细信息
        /// <summary>
        /// 队伍人员弹出界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ARMYMEMBER()
        {
            string id = Request.Params["id"];//数据类型id
            ViewBag.DC_ARMY_ID = id;
            ViewBag.MEMBER = ArmyMEMBERStr(new DC_ARMY_MEMBER_SW { DC_ARMY_ID = id });
            return View();
        }

        /// <summary>
        /// 获取人员页面
        /// </summary>
        /// <returns></returns>
        public string ArmyMEMBERStr(DC_ARMY_MEMBER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>姓名</th>");
            sb.AppendFormat("<th>性别</th>");
            sb.AppendFormat("<th>联系方式</th>");
            sb.AppendFormat("<th>出生日期</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = DC_ARMY_MEMBERCls.getModelList(sw);
            foreach (var s in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.MEMBERNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.SEXNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.CONTACTS);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.BIRTH);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// 队伍照片弹出界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ARMYPHOTO()
        {
            string id = Request.Params["id"];//数据类型id
            string PhotoType = Request.Params["PHOTOTYPE"];//数据类型id
            ViewBag.DC_ARMY_ID = id;
            ViewBag.PHOTO = ArmyPHOTOStr(new DC_PHOTO_SW { PRID = id, PHOTOTYPE = PhotoType });
            return View();
        }

        /// <summary>
        /// 获取队伍照片页面
        /// </summary>
        /// <returns></returns>
        public string ArmyPHOTOStr(DC_PHOTO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>照片标题</th>");
            sb.AppendFormat("<th>照片说明</th>");
            sb.AppendFormat("<th>照片时间</th>");
            sb.AppendFormat("<th>缩略图</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = DC_PHOTOCls.getModelList(sw);
            foreach (var s in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\"><a href=\"/UploadFile/DacPhoto/{1}\" target=\"_blank\">{0}</a></td>", s.PHOTOTITLE, s.PHOTOFILENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PHOTOEXPLAIN);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PHOTOTIME);
                sb.AppendFormat("<td class=\"left\"><img src=\"/UploadFile/DacPhoto/{0}\" alt=\"alttext\" title=\"{1}\" height =\"35px\" wideth=\"35px\"/></a></td>", s.PHOTOFILENAME, s.PHOTOTITLE);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 资源弹出界面
        /// </summary>
        /// <returns></returns>
        public ActionResult RESOURCEPHOTO()
        {
            string id = Request.Params["id"];//数据类型id
            ViewBag.DC_RESOURCE_NEWID = id;
            ViewBag.PHOTO = ArmyPHOTOStr(new DC_PHOTO_SW { PRID = id });
            return View();
        }

        /// <summary>
        /// 获取资源照片页面
        /// </summary>
        /// <returns></returns>
        public string ResourcePHOTOStr(DC_PHOTO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>照片标题</th>");
            sb.AppendFormat("<th>照片说明</th>");
            sb.AppendFormat("<th>照片时间</th>");
            sb.AppendFormat("<th>缩略图</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = DC_PHOTOCls.getModelList(sw);
            foreach (var s in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\"><a href=\"/UploadFile/DacPhoto/{1}\" target=\"_blank\">{0}</a></td>", s.PHOTOTITLE, s.PHOTOFILENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PHOTOEXPLAIN);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PHOTOTIME);
                sb.AppendFormat("<td class=\"left\"><img src=\"/UploadFile/DacPhoto/{0}\" alt=\"alttext\" title=\"{1}\" height =\"35px\" wideth=\"35px\"/></a></td>", s.PHOTOFILENAME, s.PHOTOTITLE);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// 营房弹出界面
        /// </summary>
        /// <returns></returns>
        public ActionResult CAMPPHOTO()
        {
            string id = Request.Params["id"];//数据类型id
            ViewBag.DC_CAMP_ID = id;
            ViewBag.PHOTO = CampPHOTOStr(new DC_PHOTO_SW { PRID = id });
            return View();
        }

        /// <summary>
        /// 获取营房照片页面
        /// </summary>
        /// <returns></returns>
        public string CampPHOTOStr(DC_PHOTO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>照片标题</th>");
            sb.AppendFormat("<th>照片说明</th>");
            sb.AppendFormat("<th>照片时间</th>");
            sb.AppendFormat("<th>缩略图</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = DC_PHOTOCls.getModelList(sw);
            foreach (var s in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\"><a href=\"/UploadFile/DacPhoto/{1}\" target=\"_blank\">{0}</a></td>", s.PHOTOTITLE, s.PHOTOFILENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PHOTOEXPLAIN);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PHOTOTIME);
                sb.AppendFormat("<td class=\"left\"><img src=\"/UploadFile/DacPhoto/{0}\" alt=\"alttext\" title=\"{1}\" height =\"35px\" wideth=\"35px\"/></a></td>", s.PHOTOFILENAME, s.PHOTOTITLE);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// 瞭望台弹出界面
        /// </summary>
        /// <returns></returns>
        public ActionResult OVERWATCHPHOTO()
        {
            string id = Request.Params["id"];//数据类型id
            ViewBag.DC_OVERWATCH_ID = id;
            ViewBag.PHOTO = OverWatchPHOTOStr(new DC_PHOTO_SW { PRID = id });
            return View();
        }

        /// <summary>
        /// 获取瞭望台照片页面
        /// </summary>
        /// <returns></returns>
        public string OverWatchPHOTOStr(DC_PHOTO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>照片标题</th>");
            sb.AppendFormat("<th>照片说明</th>");
            sb.AppendFormat("<th>照片时间</th>");
            sb.AppendFormat("<th>缩略图</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = DC_PHOTOCls.getModelList(sw);
            foreach (var s in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\"><a href=\"/UploadFile/DacPhoto/{1}\" target=\"_blank\">{0}</a></td>", s.PHOTOTITLE, s.PHOTOFILENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PHOTOEXPLAIN);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PHOTOTIME);
                sb.AppendFormat("<td class=\"left\"><img src=\"/UploadFile/DacPhoto/{0}\" alt=\"alttext\" title=\"{1}\" height =\"35px\" wideth=\"35px\"/></a></td>", s.PHOTOFILENAME, s.PHOTOTITLE);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }


        /// <summary>
        /// 红外相机弹出界面
        /// </summary>
        /// <returns></returns>
        public ActionResult INFRAREDCAMERA_BASICINFO()
        {
            string id = Request.Params["id"];//数据类型id
            ViewBag.INFRAREDCAMERAID = id;
            ViewBag.PHOTO = INFRAREDCAMERA_BASICINFOPHOTOStr(new JC_INFRAREDCAMERA_PHOTO_SW { INFRAREDCAMERAID = id });
            return View();
        }

        /// <summary>
        /// 获取红外相机照片页面
        /// </summary>
        /// <returns></returns>
        public string INFRAREDCAMERA_BASICINFOPHOTOStr(JC_INFRAREDCAMERA_PHOTO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>照片标题</th>");
            sb.AppendFormat("<th>照片时间</th>");
            sb.AppendFormat("<th>缩略图</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            int i = 0;
            var result = JC_INFRAREDCAMERACls.getListNewModelPhoto(sw);
            foreach (var s in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PHOTOTITLE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.PHOTOTIME);
                sb.AppendFormat("<td class=\"left\"><img src=\"/UploadFile/INFRAREDCAMERA/{0}\" alt=\"alttext\" title=\"{1}\" height =\"35px\" wideth=\"35px\"/></a></td>", s.PHOTOTITLE, s.PHOTOTITLE);
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// 火情基本信息
        /// </summary>
        /// <returns></returns>
        public ActionResult FireBasicInfo()
        {
            CookieModel cookieInfo1 = SystemCls.getCookieInfo();
            string jcfid = Request.Params["jcfid"];
            var model = JC_FIRECls.getModel(new JC_FIRE_SW { JCFID = jcfid });
            ViewBag.Fire = T_SYS_LAYERCls.getTreeFireQuery(new T_SYS_LAYER_SW { USERID = cookieInfo1.UID });
            ViewBag.LAYERID = T_SYS_LAYERCls.getLayerFireLAYERID(new T_SYS_LAYER_SW { USERID = cookieInfo1.UID });
            return View(model);
        }

        #region Ajax
        /// <summary>
        /// 根据地址获取经纬度
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLonLatByAddress()
        {
            string address = Request.Params["address"];
            var str = GetLonLat(address);
            return Json(str);
        }

        /// <summary>
        /// 林火蔓延经度纬度点的集合获取
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSpreadPointsAjax()
        {
            MessageListObject mm = null;
            string jd = Request.Params["jd"];//经度
            string wd = Request.Params["wd"];//纬度
            double dWindDirection = Convert.ToDouble(Request.Params["dWindDirection"]);//风向角度值（北风：180，东风：270，南风：0，西风：45以此类推，即：方向与北方向的顺时针夹角，切记有效值为0至360）
            double dWindSpeed = Convert.ToDouble(Request.Params["dWindSpeed"]);//风力（单位：米/每秒）
            double dHumidity = Convert.ToDouble(Request.Params["dHumidity"]);//湿度值
            double dTemperature = Convert.ToDouble(Request.Params["dTemperature"]);//温度值
            double dW = Convert.ToDouble(Request.Params["dW"]);//网格宽度（小于0时，系统自动分配）
            double dTime = Convert.ToDouble(Request.Params["dTime"]);//分段值（即：相对速度的时间值）（单位：分钟）
            bool bConvexHull = Convert.ToBoolean(Request.Params["bConvexHull"]);//凸多边形
            var model = client.FireSpread(jd, wd, dWindDirection, dWindSpeed, dHumidity, dTemperature, dW, dTime, bConvexHull);//
            if (model.Count() > 0)
            {
                mm = new MessageListObject(true, model);
            }
            else
            {
                mm = new MessageListObject(false, null);
            }
            return Json(mm);
        }

        /// <summary>
        /// 数据中心地图展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMapDataForDataCenter()
        {
            MessageObject ms = null;
            string tablename = Request.Params["tablename"];
            string id = Request.Params["id"];
            if (tablename.Trim().ToUpper() == "DC_ARMY")
            {
                var model = DC_ARMYCls.getModel(new DC_ARMY_SW { DC_ARMY_ID = id });
                ms = new MessageObject(true, model);
            }
            return Json(ms);
        }

        /// <summary>
        /// 获取地图展示模式详细信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMapDataInfoAjax()
        {
            MessageObject ms = null; ;
            string jcfid = Request.Params["jcfid"];
            if (string.IsNullOrEmpty(jcfid))
            {
                ms = new MessageObject(false, null);
            }
            var model = JC_FIRETICKLINGCls.GetFKFireInfoData(jcfid);

            model.FIRESOURCENAME = Enum.GetName(typeof(EnumType), Convert.ToInt32(model.JC_FireData.FIREFROM));
            model.HOTETYPENAME = StateSwitch.DicStateName("热点类别", model.JC_FireFKData.HOTTYPE);
            model.LXNAME = StateSwitch.DicStateName("是否连续", model.JC_FireFKData.JXHQSJ);
            model.FKNAME = StateSwitch.QSStateNew(SystemCls.getCurUserOrgNo(), model.JC_FireFKData.MANSTATE);
            ms = new MessageObject(true, model);
            return Json(ms);
        }

        /// <summary>
        /// 获取地图展示模式详细信息List
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMapDataListInfoAjax()
        {
            MessageListObject ms = null; ;
            string strjcfid = Request.Params["jcfid"];
            if (string.IsNullOrEmpty(strjcfid))
            {
                ms = new MessageListObject(false, null);
            }
            else
            {
                var result = new List<JCFireFKInfoModel>();
                var ss = strjcfid.Split(',');
                foreach (var jcfid in ss)
                {
                    var model = JC_FIRETICKLINGCls.GetFKFireInfoData(jcfid);
                    model.FIRESOURCENAME = Enum.GetName(typeof(EnumType), Convert.ToInt32(model.JC_FireData.FIREFROM));
                    model.HOTETYPENAME = StateSwitch.DicStateName("热点类别", model.JC_FireFKData.HOTTYPE);
                    model.LXNAME = StateSwitch.DicStateName("是否连续", model.JC_FireFKData.JXHQSJ);
                    model.FKNAME = StateSwitch.QSStateNew(SystemCls.getCurUserOrgNo(), model.JC_FireFKData.MANSTATE);
                    result.Add(model);
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }

        /// <summary>
        /// 由经纬度取地址(逆地理编码)
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAddressAjax()
        {
            string jd = Request.Params["jd"];
            string wd = Request.Params["wd"];
            var address = GetAddress(decimal.Parse(jd), decimal.Parse(wd));
            Message ms = null;
            ms = new Message(true, address, "");
            return Json(ms);
        }

        /// <summary>
        /// 有地理位置取经纬度
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLngLatAjax()
        {
            string address = Request.Params["address"];
            Message ms = null;
            var lnglatstr = GetLonLat(address);
            if (lnglatstr == "0")
            {
                ms = new Message(false, "地址解析未查询到", "");
            }
            else
            {
                ms = new Message(true, lnglatstr, "");
            }
            return Json(ms);
        }

        /// <summary>
        /// 获取两个经度纬度之间距离
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLngLatBetweenTime()
        {
            Message ms = null;
            string lng1 = Request.Params["lng1"];
            string lat1 = Request.Params["lat1"];
            string lng2 = Request.Params["lng2"];
            string lat2 = Request.Params["lat2"];
            string t1 = Request.Params["t1"];//第一个经纬度时间
            string t2 = Request.Params["t2"];//第二个经纬度时间
            if (string.IsNullOrEmpty(lng1) || string.IsNullOrEmpty(lat1) || string.IsNullOrEmpty(lng2) || string.IsNullOrEmpty(lat2))
            {
                ms = new Message(false, "参数错误", "");
            }
            var paramodel = T_SYS_PARAMETERCls.getModel(new T_SYS_PARAMETER_SW { PARAMFLAG = "HisTraceDistiance", SYSFLAG = ConfigCls.getSystemFlag() });
            if (paramodel != null)
            {
                var dis = MapComHelpr.DistanceOfTwoPoints(Convert.ToDouble(lng1), Convert.ToDouble(lat1), Convert.ToDouble(lng2), Convert.ToDouble(lat2), GaussSphere.WGS84);
                var paradis = Convert.ToDouble(paramodel.PARAMVALUE);//系统参数读取的数值
                if (dis < paradis)
                {
                    DateTime ts1 = DateTime.Parse(t1);
                    DateTime te2 = DateTime.Parse(t2);
                    string span = te2.Subtract(ts1).TotalHours.ToString("F2");
                    ms = new Message(true, span, "");
                }
                else
                {
                    ms = new Message(false, "在系统参数设置距离之外", "");
                }
            }
            return Json(ms);
        }
        #endregion

        #region Private
        /// <summary>
        /// 通过反射获取表属性和值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tablename">表名</param>
        /// <param name="key">主键</param>
        /// <param name="id">字段id值</param>
        /// <returns></returns>
        private Dictionary<string, DataModel> GetDicDataModel<T>(string tablename, string key, string id)
        {
            var result = new Dictionary<string, DataModel>();
            var data = ManagerSystemClassLibrary.BaseDT.Map_Common.GetTModel<T>(tablename, key, id);//获取属性数据
            if (data != null)
            {
                Type t = typeof(T);
                PropertyInfo[] properties = t.GetProperties();
                foreach (PropertyInfo info in properties)
                {
                    var pName = t.GetProperty(info.Name);
                    //4.0或以上版本
                    var displayName = pName.GetCustomAttribute<DisplayNameAttribute>();//获取属性
                    var dicType = pName.GetCustomAttribute<DicTypeAttribute>();//获取属性
                    var unitType = pName.GetCustomAttribute<UnitDisplayAttribute>();//获取单位属性
                    var model = new DataModel();
                    if (displayName != null)
                    {
                        model.Name = displayName.DisplayName;//字段名字
                        var datavalue = ReflectCom.GetObjectPropertyValue<T>(data, info.Name);//获取value值
                        if (string.IsNullOrEmpty(datavalue))
                        {
                            model.Value = "--";
                        }
                        else
                        {
                            model.Value = datavalue;
                        }
                        if (dicType != null)
                        {
                            if (dicType.DisplayName.ToLower().Trim() == "orgno")//获取机构
                            {
                                var orgmodel = T_SYS_ORGCls.getModel(new T_SYS_ORGSW { ORGNO = datavalue });
                                if (orgmodel != null)
                                {
                                    model.Value = orgmodel.ORGNAME;
                                }
                            }
                            else if (dicType.DisplayName.ToLower().Trim() == "date")//格式转换
                            {
                                if (!string.IsNullOrEmpty(datavalue))
                                {
                                    model.Value = Convert.ToDateTime(datavalue).ToString("yyyy-MM-dd");
                                }
                            }
                            else if (dicType.DisplayName.ToLower().Trim() == "datetime")//格式转换
                            {
                                if (!string.IsNullOrEmpty(datavalue))
                                {
                                    model.Value = Convert.ToDateTime(datavalue).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                            }
                            else
                            {
                                var type = dicType.DisplayName;//字典类型数值
                                var dicmodel = T_SYS_DICTCls.getModel(new T_SYS_DICTSW { DICTTYPEID = type, DICTVALUE = datavalue });
                                model.Value = dicmodel.DICTNAME;
                            }
                        }
                        else if (unitType != null)//单位
                        {
                            if (!string.IsNullOrEmpty(unitType.Unit))
                            {
                                model.Value += " (" + unitType.Unit + ")";
                            }
                        }
                        //else
                        //{
                        //    //model.Value = ReflectCom.GetObjectPropertyValue<T>(data, info.Name);
                        //    model.Value = datavalue;
                        //}
                        result.Add(info.Name, model);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 逆地理编码
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public string GetAddress(decimal lng, decimal lat)
        {
            var com = new HttpCommon();
            BaiDuApiAddressModel model = new BaiDuApiAddressModel();
            //string para="name="+name + "&ak=" + ak + "&is_published=" + is_published + "&geotype=" + geotype;
            //string url = "http://api.map.baidu.com/geocoder/v2/?ak=wYCjPb9rxUueQP8xcNwqGLFw&callback=renderReverselocation=39.983424,116.322987&output=json&pois=1";
            //http://api.map.baidu.com/geocoder/v2/?ak=E4805d16520de693a3fe707cdc962045&callback=renderReverse&location=39.983424,116.322987&output=json&pois=0
            string url = "http://api.map.baidu.com/geocoder/v2/";
            string postdata = "ak=wYCjPb9rxUueQP8xcNwqGLFw&location=" + lat + "," + lng + "&output=json&pois=0";
            try
            {
                string str = com.HttpGet(url, postdata);
                model = JsonHelper.JsonDeserialize<BaiDuApiAddressModel>(str);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return model.result.formatted_address;
        }

        /// <summary>
        /// 地理编码
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public string GetLonLat(string address)
        {
            var com = new HttpCommon();
            BaiDuApiAddressToLngLat model = new BaiDuApiAddressToLngLat();
            string url = "http://api.map.baidu.com/geocoder/v2/";
            string postdata = "ak=wYCjPb9rxUueQP8xcNwqGLFw&address=" + address + "&output=json";
            try
            {
                string str = com.HttpGet(url, postdata);
                model = JsonHelper.JsonDeserialize<BaiDuApiAddressToLngLat>(str);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            if (model.status == 1)
            {
                return "0";
            }
            else
            {
                return model.result.location.lng.ToString() + "," + model.result.location.lat.ToString();
            }
        }
        //{"status":0,"result":{"location":{"lng":116.30815063007,"lat":40.056890127931},"precise":1,"confidence":80,"level":"\u9053\u8def"}}
        //http://api.map.baidu.com/geocoder/v2/?address=北京市海淀区上地十街10号&output=json&ak=wYCjPb9rxUueQP8xcNwqGLFw&callback=showLocation
        #endregion
    }
}
