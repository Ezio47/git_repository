using ManagerSystemClassLibrary;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    public class SkylineMangerController : Controller
    {
        //
        // GET: /SkylineManger/
        /// <summary>
        /// 历史轨迹查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string uid = Request.Params["uid"];
            ViewBag.uid = uid;
            return View();
        }


        /// <summary>
        /// 数据上报
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportIndex()
        {
            string type = Request.Params["type"];
            ViewBag.type = type;
            return View();
        }

        /// <summary>
        /// 查看上报人详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonDetailIndex()
        {
            string hid = Request.Params["hid"];//护林员hid
            var model = T_IPSFR_USERCls.getModel(new T_IPSFR_USER_SW { HID = hid });
            var info = DC_PHOTOCls.getModel(new DC_PHOTO_SW { PHOTOTYPE = "T_IPSFR_USER", PRID = hid }).PHOTOFILENAME;
            if (string.IsNullOrEmpty(info))
            {
                ViewBag.picurl = "";
            }
            else
            {
                ViewBag.picurl = @"/UploadFile/DacPhoto/" + info;
            }
            return View(model);
        }

        /// <summary>
        /// 上报信息详细查看
        /// </summary>
        /// <returns></returns>
        public ActionResult DataReportDetailViewIndex()
        {
            string repid = Request.Params["repid"];//数据上报id
            var sw = new T_IPSRPT_REPORT_SW();
            sw.REPORTID = repid;
            var list = T_IPSRPT_REPORTCls.getModelList(sw).FirstOrDefault();
            return View(list);
        }
        /// <summary>
        /// 采集数据详细查看
        /// </summary>
        /// <returns></returns>
        public ActionResult DataCollectDetailViewIndex()
        {
            string cid = Request.Params["cid"];//数据采集id
            var sw = new T_IPSCOL_COLLECT_SW();
            sw.COLLECTID = cid;
            var model = T_IPSCOL_COLLECTCls.getModel(sw);
            return View(model);
        }

        /// <summary>
        /// 获取详细采集点集合
        /// </summary>
        /// <returns></returns>
        public JsonResult DataCollectDetailList()
        {
            string cid = Request.Params["cid"];//数据采集id
            var sw = new T_IPSCOL_COLLECT_SW();
            sw.COLLECTID = cid;
            var list = T_IPSCOL_COLLECTCls.getDetailModelList(sw);
            return Json(list);

        }
        #region 点图层转换

        /// <summary>
        /// 点图层--队伍
        /// </summary>
        /// <returns></returns>
        public ActionResult DWIndex()
        {
            ViewBag.armytypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "26" });//队伍类型
            return View();
        }

        /// <summary>
        /// 点图层--仓库
        /// </summary>
        /// <returns></returns>
        public ActionResult CKIndex()
        {
            return View();
        }

        /// <summary>
        /// 点图层--营房
        /// </summary>
        /// <returns></returns>
        public ActionResult YFIndex()
        {
            ViewBag.structureadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "34" });//结构类型
            return View();
        }
        /// <summary>
        /// 点图层--瞭望台
        /// </summary>
        /// <returns></returns>
        public ActionResult LWTIndex()
        {
            ViewBag.structureadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "34" });//结构类型
            return View();
        }

        /// <summary>
        /// 点图层--宣传碑牌
        /// </summary>
        /// <returns></returns>
        public ActionResult XCBPIndex()
        {
            ViewBag.structureadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "34" });//结构类型
            ViewBag.managerstateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护管理类型
            ViewBag.usestateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用状态
            ViewBag.propagandasteletypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "40" });//宣传碑类型
            return View();
        }
        /// <summary>
        /// 点图层--中继站
        /// </summary>
        /// <returns></returns>
        public ActionResult ZJZIndex()
        {
            ViewBag.managerstateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护管理类型
            ViewBag.usestateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用状态
            ViewBag.communicationwayadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "41" });//通讯方式
            return View();
        }
        /// <summary>
        /// 点图层--监测站
        /// </summary>
        /// <returns></returns>
        public ActionResult JCZIndex()
        {
            ViewBag.managerstateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护管理类型
            ViewBag.usestateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用状态
            ViewBag.transfermodetypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "42" });//无线电传输方式
            return View();
        }
        /// <summary>
        /// 点图层--因子采集站
        /// </summary>
        /// <returns></returns>
        public ActionResult YZCJZIndex()
        {
            ViewBag.managerstateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护管理类型
            ViewBag.usestateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用状态
            ViewBag.transfermodetypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "42" });//无线电传输方式
            return View();
        }

        #endregion

        #region 线图层
        /// <summary>
        /// 线图层--隔离带
        /// </summary>
        /// <returns></returns>
        public ActionResult GLDIndex()
        {
            ViewBag.managerstateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护管理类型
            ViewBag.usestateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用状态
            return View();
        }
        /// <summary>
        /// 线图层--防火通道
        /// </summary>
        /// <returns></returns>
        public ActionResult FHTDIndex()
        {
            ViewBag.fireleveltypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "38" });//防火通道等级类型
            ViewBag.fireusetypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "39" });//防火通道使用性质
            ViewBag.managerstateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护管理类型
            ViewBag.usestateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用状态
            return View();
        }

        #endregion

        #region 面图层
        /// <summary>
        /// 面图层--资源
        /// </summary>
        /// <returns></returns>
        public ActionResult ZYIndex()
        {
            ViewBag.agetypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "27" });//林龄类型
            ViewBag.resourcetypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "28" });//资源类型
            ViewBag.originttypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "29" });//起源类型
            ViewBag.burntypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "30" });//可燃类型
            ViewBag.treetypeadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "31" });//林木类型
            return View();
        }

        /// <summary>
        /// 面图层--林下烧除
        /// </summary>
        /// <returns></returns>
        public ActionResult LXSCIndex()
        {
            ViewBag.managerstateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护管理类型
            ViewBag.usestateadd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用状态
            return View();
        }
        #endregion
    }
}
