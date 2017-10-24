using log4net;
using ManagerSystem.MVC.HelpCom;
using ManagerSystem.MVC.Models;
using ManagerSystemClassLibrary;
using ManagerSystemClassLibrary.SmsHelp;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    /// <summary>
    /// 基本共用界面
    /// </summary>
    public class BaseCommonController : Controller
    {
        static string host = ConfigurationManager.AppSettings["redis:host:dbWrite"];/*访问host地址*/
        static string port = ConfigurationManager.AppSettings["redis:port"];/*访问host地址*/
        static readonly RedisClient redisclient = new RedisClient(host, int.Parse(port));
        private static readonly ILog logs = LogHelper.GetInstance();


        /// <summary>
        /// 预警响应
        /// </summary>
        /// <returns></returns>
        public ActionResult PopYjxyIndex()
        {
            return View();
        }

        /// <summary>
        /// 火险等级
        /// </summary>
        /// <returns></returns>
        public ActionResult PopFireLevelIndex()
        {
            var result = GetModelList();
            return View(result);
        }

        /// <summary>
        /// 火险等级BY
        /// </summary>
        /// <returns></returns>
        public ActionResult PopFireLevelIndexBY()
        {
            string level = Request.Params["level"];
            var result = GetModelListBy(level);
            return View(result);
        }

        /// <summary>
        /// 火险等级查询
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLevelIQuery()
        {
            var result = GetModelList();
            return View(result);
        }

        /// <summary>
        /// 天气情况
        /// </summary>
        /// <returns></returns>
        public ActionResult PopWeatherInfoIndex()
        {
            var result = GetWeatherList();
            return View(result);
        }

        /// <summary>
        /// 短信发送
        /// </summary>
        /// <returns></returns>
        public ActionResult MessageSendIndex()
        {
            var model = YJ_DCSMS_TMPCls.GetListModel(new YJ_DCSMS_TMP_SW() { DANGERCLASS = "5", ISENABLE = "1" });//默认红色预警模板
            return View(model);
        }

        /// <summary>
        /// 机构联系人树形菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult TreeORGUSERGet()
        {
            string ID = Request.Params["id"];
            string result = T_SYS_ORG_LINKCls.GetOrgTree(ID);
            return Content(result, "application/json");
        }

        /// <summary>
        /// 通讯录树形菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult TreeTXLUSERGet()
        {
            string result = T_SYS_ADDREDDBOOKCls.getTypeTree(new T_SYS_ADDREDDTYPE_SW { treeIDShowUserType = "{ADID}", treeNameShowUserType = "{ADNAME}[{USERJOB}]", treeIsShowTypeID = "0" });//  T_SYS_ADDREDDTYPECls.get DC_TYPECls.getEQUIPTree(new DC_TYPE_SW { });
            return Content(result, "application/json");
        }

        /// <summary>
        /// 视频界面
        /// </summary>
        /// <returns></returns>
        public ActionResult VideoIndex()
        {
            var id = Request.Params["id"];
            var eid = Request.Params["eid"];
            ViewBag.id = id;
            ViewBag.eid = eid;
            return View();
        }

        /// <summary>
        /// 视频界面--海威
        /// </summary>
        /// <returns></returns>
        public ActionResult VideoHWIndex()
        {
            var id = Request.Params["id"];//组织机构编码
            var eid = Request.Params["eid"];//设备标号
            var type = Request.Params["type"];//设备类型
            ViewBag.id = id;
            ViewBag.eid = eid;
            ViewBag.type = type;
            return View();
        }

        /// <summary>
        /// 获取视频下拉列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVideoTree()
        {
            //string str = Request.Params["phonehname"];
            string id = Request.Params["id"];//组织机构编码
            string eid = Request.Params["eid"];//设备标号
            string result = JC_INFRAREDCAMERACls.getSynTree(id, eid);
            return Content(result, "application/json");
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <returns></returns>
        public ActionResult SenMsgIndex()
        {
            return View();
        }

        /// <summary>
        /// 预警响应措施
        /// </summary>
        /// <returns></returns>
        public string YJXYCS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table style='border:1px solid #c9dfbb;width:100%;height:100% cellpadding=0 cellspacing=0;text-align:center;margin:0 auto;'>");
            var BYORGNO = SystemCls.getCurUserOrgNo();
            if (PublicCls.OrgIsShi(BYORGNO))
            {
                BYORGNO = ConfigCls.getConfigValue("ProvincialCapital");//州府所在地行政区划编码
            }
            var DANGERCLASS = YJ_DANGERCLASSCls.getModel(new YJ_DANGERCLASS_SW { BYORGNO = BYORGNO }).DANGERCLASS;
            if (string.IsNullOrEmpty(DANGERCLASS))
            {
                BYORGNO = BYORGNO.Substring(0, 6) + "000000000";//如果乡镇没有,查市的 
                DANGERCLASS = YJ_DANGERCLASSCls.getModel(new YJ_DANGERCLASS_SW { BYORGNO = BYORGNO }).DANGERCLASS;
                if (string.IsNullOrEmpty(DANGERCLASS))
                {
                    BYORGNO = ConfigCls.getConfigValue("ProvincialCapital");
                    //BYORGNO = ConfigurationManager.AppSettings["ProvincialCapital"].ToString();//如果市没有,查州的
                    DANGERCLASS = YJ_DANGERCLASSCls.getModel(new YJ_DANGERCLASS_SW { BYORGNO = BYORGNO }).DANGERCLASS;
                    if (string.IsNullOrEmpty(DANGERCLASS))
                    {
                        DANGERCLASS = "";
                    }
                }
            }
            ViewBag.DANGERCLASS = DANGERCLASS;
            var result = YJ_XY_WORKCls.getModelListMan(new YJ_XY_WORK_SW { });
            var resultClass = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW
            {
                DICTTYPEID = "24",
                DICTVALUE = DANGERCLASS
            });//火情预警等级
            var resultDept = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "25" });//预警响应部门
            foreach (var v in resultClass)
            {
                sb.AppendFormat("<tr style='background:{0};color:#000000'>", v.STANDBY1);
                //sb.AppendFormat("<td style='color:#000;font-weight:bold;font-size:14px;'>火险等级</td>");
                sb.AppendFormat("<td colspan=2 style='color:#000000;font-size:14px;text-align:center'>{0}</td>", v.DICTNAME);
                sb.AppendFormat("</tr>");
                foreach (var x in resultDept)
                {
                    sb.AppendFormat("<tr  style='background:{0};color:#000000'>", v.STANDBY1);
                    sb.AppendFormat("<td style='color:#000;font-weight:bold;font-size:9pt;'>{0}</td>", x.DICTNAME);
                    var jbs = result.Where(p => p.DANGERCLASS == DANGERCLASS && p.YJXYDEPT == x.DICTVALUE).FirstOrDefault();
                    if (jbs != null)
                        sb.AppendFormat("<td style='text-align:left;font-size:9pt;'>{0}</td>", jbs.YJXYCONTENT);
                    //sb.AppendFormat("{0}", jbs.YJXYCONTENT);
                    //else
                    //    sb.AppendFormat("{0}", "");
                    //sb.AppendFormat("</td>");
                    sb.AppendFormat("</tr>");
                }
            }
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        #region Ajax
        /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSysTime()
        {
            Message ms = null;
            string startime = Request.Params["startime"];
            if (string.IsNullOrEmpty(startime))
            {
                ms = new Message(false, "开始时间参数丢失!", "");
                return Json(ms);
            }
            var dtstartime = Convert.ToDateTime(startime);
            TimeSpan ts = DateTime.Now - dtstartime;
            ms = new Message(true, ts.TotalHours.ToString(), "");
            return Json(ms);
        }

        /// <summary>
        /// json获取火险等级
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFireLevel()
        {
            var sw = new YJ_DANGERCLASS_SW();
            string level = Request.Params["level"];
            string dt = Request.Params["dt"];
            sw.DANGERCLASS = level;
            sw.DCDATE = dt;
            var str = GetFireLevelHtml(sw);
            Message ms = new Message(true, str.ToString(), "");
            return Json(ms);
        }

        /// <summary>
        /// ajax 获取预警等级模板短信
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSmsTmpModel()
        {
            MessageListObject ms = null;
            string level = Request.Params["level"];
            var list = YJ_DCSMS_TMPCls.GetListModel(new YJ_DCSMS_TMP_SW() { DANGERCLASS = level, ISENABLE = "1" });
            ms = new MessageListObject(true, list);
            return Json(ms);
        }

        /// <summary>
        /// 手动发送短信
        /// </summary>
        /// <returns></returns>
        public JsonResult SendMsg()
        {
            Message ms = new Message(false, "错误信息初始化!", "");
            string SubjectPerson = Request.Params["SubjectPerson"];
            var paraArry1 = SubjectPerson.Split('|');//获取主题+；+人员+；+短信内容| 
            if (paraArry1.Length > 0)
            {
                foreach (var item in paraArry1)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        continue;
                    }
                    var subject = item.Split(';');//获取主题+；+人员+；+短信内容
                    var model = YJ_DCSMS_TMPCls.GetListModel(new YJ_DCSMS_TMP_SW { YJ_DCSMS_TMPID = subject[0], ISENABLE = "1" }).FirstOrDefault();
                    if (model != null && !string.IsNullOrEmpty(model.TMPCONTENT))
                    {
                        var info = new YJ_DCSMS_SEND_SW();
                        info.opMethod = "Add";//增加
                        info.YJ_DCSMS_TMPID = subject[0];
                        info.SMSSENDUSERLIST = subject[1];//接收人员
                        info.TMPCONTENT = subject[2];//获取界面的短信内容
                        info.DCDATE = DateTime.Now.ToString();//火险等级时间
                        info.BYORGNO = "";//所属机构 适用于值班员与护林员
                        int levelnum = 0;
                        var FireLevellist = YJ_DANGERCLASSCls.getListModelTop(new YJ_DANGERCLASS_SW() { }).Where(p => p.DANGERCLASS == model.DANGERCLASS);//获取最新的火险等级
                        if (FireLevellist.Any())
                        {
                            levelnum = FireLevellist.Count();//当前火险等级个数
                            #region 通讯录接收人员
                            if (!string.IsNullOrEmpty(info.SMSSENDUSERLIST))
                            {
                                info.DCDATE = FireLevellist.FirstOrDefault().DCDATE;
                                info.TMPCONTENT = subject[2].Replace("[num]", levelnum.ToString());//短信内容replace
                                if (!string.IsNullOrEmpty(info.TMPCONTENT))
                                {
                                    var arrpersonlist = info.SMSSENDUSERLIST.Split(',').Where(p => p != "" && p != null);
                                    string arrperson = string.Join(",", arrpersonlist);
                                    string txlMobile = arrperson;//通讯录电话列表
                                    string txlContent = "";// 通讯录模板 

                                    #region 注释
                                    // foreach (var p in arrperson)
                                    //  {
                                    //  var txlmodel = T_SYS_ADDREDDBOOKCls.getModel(new T_SYS_ADDREDDBOOK_SW { ADID = p });//通讯录
                                    //  if (txlmodel != null && !string.IsNullOrEmpty(txlmodel.PHONE))
                                    // {
                                    ////TODO  Send Message
                                    //add redis
                                    //队列
                                    //try
                                    //{
                                    //    var msg = txlmodel.PHONE.Trim() + "|" + info.TMPCONTENT;
                                    //    redisclient.EnqueueItemOnList("SendMsg", msg);//入队。
                                    //    logs.Info("==入队列号码信息==" + msg);
                                    //    Thread.Sleep(100);
                                    //    // client.SendMsg(info.TMPCONTENT, txlmodel.PHONE);
                                    //    // Thread.Sleep(200);
                                    //}
                                    //catch (Exception ex)
                                    //{
                                    //    logs.Error(ex.Message);
                                    //}
                                    //  txlMobile += txlmodel.PHONE.Trim() + ",";
                                    //   }   
                                    //}
                                    #endregion

                                    txlContent = info.TMPCONTENT;
                                    var mm = SmsCom.SendMsg(txlContent, txlMobile.TrimEnd(','));//发送短信
                                    if (mm.Success)
                                    {
                                        ms.Msg = mm.Msg;
                                        ms.Success = mm.Success;
                                        ms.Url = "";
                                    }
                                    //ms
                                    // var mm = Smsclient.SendMsg(txlContent, txlMobile);
                                    // ms = SmsMsgCom.SendMsg(txlContent, txlMobile);//发送短信
                                    // info.SMSSENDSTATUS = "1";//0 未发送 1 已发送 -1 发送失败
                                    // ms = YJ_DCSMS_SENDCls.Manager(info);
                                }
                            }
                            #endregion

                            #region 值班员 、护林员
                            if (string.IsNullOrEmpty(info.SMSSENDUSERLIST))//值班员 、护林员
                            {
                                var _cityorg = "";//市机构中间变量
                                var _contyorg = "";//县机构中间变量
                                int i = 0;
                                if (model.SMSGROUPTYPE == "2" || model.SMSGROUPTYPE == "1")//护林员接收人员 或者 值班员接收人员
                                {
                                    foreach (var level in FireLevellist)
                                    {
                                        info.DCDATE = level.DCDATE;//火险等级时间
                                        info.BYORGNO = level.BYORGNO;//火险等级所属机构
                                        #region 护林员接收人员
                                        if (model.SMSGROUPTYPE == "2")
                                        {
                                            info.TMPCONTENT = subject[2].Replace("[cityconty]", level.TOWNNAME);//短信内容replace
                                            var hlylist = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { BYORGNO = level.BYORGNO });//护林员
                                            if (hlylist.Any())
                                            {
                                                // var hlystr = "";//护林员人员 逗号分隔
                                                string hlyMobile = "";//护林员电话
                                                string hlyContent = "";//护林员模板
                                                foreach (var hly in hlylist.MyDistinct(s => s.PHONE))
                                                {
                                                    if (!string.IsNullOrEmpty(hly.PHONE))
                                                    {
                                                        hlyMobile += hly.PHONE.Trim() + ",";
                                                        //if (hlylist.Count() == 1)
                                                        //{
                                                        //    hlystr = hly.HID;
                                                        //}
                                                        //else
                                                        //{
                                                        //    hlystr += hly.HID + ",";
                                                        //}
                                                        //try
                                                        //{
                                                        //    //TODO Send Message
                                                        //    //add redis
                                                        //    //队列
                                                        //    var msg = hly.PHONE.Trim() + "|" + info.TMPCONTENT;
                                                        //    redisclient.EnqueueItemOnList("SendMsg", msg);//入队。
                                                        //    logs.Info("==入队列号码信息==" + msg);
                                                        //    //client.SendMsg(info.TMPCONTENT, hly.PHONE);
                                                        //    Thread.Sleep(200);
                                                        //}
                                                        //catch (Exception ex)
                                                        //{
                                                        //    logs.Error(ex.Message);
                                                        //}
                                                    }
                                                }
                                                hlyContent = info.TMPCONTENT;
                                                var mm = SmsCom.SendMsg(hlyContent, hlyMobile.TrimEnd(','));//发送短信
                                                if (mm.Success)
                                                {
                                                    ms.Msg = mm.Msg;
                                                    ms.Success = mm.Success;
                                                    ms.Url = "";
                                                }

                                                //info.SMSSENDUSERLIST = hlystr;//护林员人员
                                                //info.SMSSENDSTATUS = "1";//0 未发送 1 已发送 -1 发送失败
                                                // ms = YJ_DCSMS_SENDCls.Manager(info);
                                            }
                                        }
                                        #endregion

                                        #region 值班员接收人员
                                        if (model.SMSGROUPTYPE == "1")
                                        {
                                            var cityorg = level.BYORGNO.Substring(0, 4) + "00000";//市机构码
                                            var contyorg = level.BYORGNO.Substring(0, 6) + "000";//县机构码
                                            if (i == 0)//初次
                                            {
                                                _cityorg = cityorg;
                                                _contyorg = contyorg;
                                            }
                                            else
                                            {
                                                if (_cityorg == cityorg)//循环相同的市级单位 跳出循环
                                                {
                                                    continue;
                                                }
                                                if (_contyorg == contyorg)//循环相同的县级单位 跳出循环
                                                {
                                                    continue;
                                                }
                                            }
                                            info.TMPCONTENT = subject[2].Replace("[cityconty]", level.TOPTOWNNAME).Replace("[num]", levelnum.ToString());//短信内容replace
                                            string str = cityorg + "," + contyorg;
                                            var zbylist = OD_USERCls.GetOndutyUserid(level.DCDATE, str);//获取值班员useridlist
                                            if (zbylist.Any())
                                            {
                                                //var zbystr = "";
                                                string zbyMobile = "";//值班员电话
                                                string zbyContent = info.TMPCONTENT;//值班员模板
                                                foreach (var zby in zbylist)
                                                {
                                                    var m = T_SYSSEC_IPSUSERCls.getModel(new T_SYSSEC_IPSUSER_SW { USERID = zby.ToString() });
                                                    if (!string.IsNullOrEmpty(m.PHONE))
                                                    {
                                                        zbyMobile += m.PHONE.Trim() + ",";
                                                        //if (zbylist.Count() == 1)
                                                        //{
                                                        //    zbystr = zby;
                                                        //}
                                                        //else
                                                        //{
                                                        //    zbystr += zby + ",";
                                                        //}
                                                        //try
                                                        //{
                                                        //    ////TODO  Send Message
                                                        //    //add redis
                                                        //    //队列
                                                        //    var msg = m.PHONE.Trim() + "|" + info.TMPCONTENT;
                                                        //    redisclient.EnqueueItemOnList("SendMsg", msg);//入队。
                                                        //    logs.Info("==入队列号码信息==" + msg);
                                                        //    // client.SendMsg(info.TMPCONTENT, m.PHONE);
                                                        //    Thread.Sleep(200);

                                                        //}
                                                        //catch (Exception ex)
                                                        //{
                                                        //    logs.Error(ex.Message);
                                                        //}
                                                    }
                                                }
                                                var mm = SmsCom.SendMsg(zbyContent, zbyMobile.TrimEnd(','));//发送短信
                                                if (mm.Success)
                                                {
                                                    ms.Msg = mm.Msg;
                                                    ms.Success = mm.Success;
                                                    ms.Url = "";
                                                }
                                                // info.SMSSENDUSERLIST = zbystr;//值班员人员
                                                //info.SMSSENDSTATUS = "1";//0 未发送 1 已发送 -1 发送失败
                                                //ms = YJ_DCSMS_SENDCls.Manager(info);
                                            }
                                            ++i;
                                        }
                                        #endregion
                                    }
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            ms = new Message(false, "当前火险等级未达到预警，无需发短信!", "");
                        }
                    }
                    else
                    {
                        ms = new Message(false, "没有短信模板，不发短信!", "");
                    }
                }
            }
            else
            {
                ms = new Message(false, "传递短信模板主题与人员id参数错误!", "");
            }
            return Json(ms);
        }

        /// <summary>
        /// 自动发送短信
        /// </summary>
        /// <returns></returns>
        public JsonResult AutoSendMsg()
        {
            Message ms = null;
            var st = ConfigCls.getIsAutoSendFireLevelMsg();//读取配置
            if (st == "1")//自动
            {
                var smsmodel = YJ_DCSMS_TMPCls.GetListModel(new YJ_DCSMS_TMP_SW { ISENABLE = "1" });//获取短信模板
                if (smsmodel.Any())
                {
                    int levelnum = 0;
                    foreach (var sms in smsmodel)
                    {
                        var FireLevellist = YJ_DANGERCLASSCls.getListModelTop(new YJ_DANGERCLASS_SW() { }).Where(p => p.DANGERCLASS == sms.DANGERCLASS);//获取最新的火险等级
                        if (FireLevellist.Any())
                        {
                            var _cityorg = "";//市机构中间变量
                            var _contyorg = "";//县机构中间变量
                            int i = 0;
                            levelnum = FireLevellist.Count();//当前火险等级个数
                            foreach (var level in FireLevellist)
                            {
                                var info = new YJ_DCSMS_SEND_SW();
                                info.opMethod = "Add";
                                info.SMSSENDUSERLIST = sms.SMSSENDUSERLIST;
                                info.DCDATE = level.DCDATE;
                                info.BYORGNO = level.BYORGNO;
                                info.YJ_DCSMS_TMPID = sms.YJ_DCSMS_TMPID;
                                //info.TMPCONTENT = sms.TMPCONTENT;

                                #region 通讯录接收人员
                                if (!string.IsNullOrEmpty(info.SMSSENDUSERLIST))
                                {
                                    var arrperson = info.SMSSENDUSERLIST.Split(',');
                                    info.TMPCONTENT = sms.TMPCONTENT.Replace("[num]", levelnum.ToString());//短信内容replace
                                    if (!string.IsNullOrEmpty(info.TMPCONTENT))
                                    {
                                        foreach (var p in arrperson)
                                        {
                                            var txlmodel = T_SYS_ADDREDDBOOKCls.getModel(new T_SYS_ADDREDDBOOK_SW { ADID = p });//通讯录
                                            if (txlmodel != null && !string.IsNullOrEmpty(txlmodel.PHONE))
                                            {
                                                try
                                                {
                                                    ////TODO  Send Message
                                                    //add redis
                                                    //队列
                                                    var msg = txlmodel.PHONE.Trim() + "|" + info.TMPCONTENT;
                                                    redisclient.EnqueueItemOnList("SendMsg", msg);//入队。
                                                    logs.Info("==入队列号码信息==" + msg);
                                                    //  client.SendMsg(info.TMPCONTENT, txlmodel.PHONE);
                                                    Thread.Sleep(200);
                                                }
                                                catch (Exception ex)
                                                {
                                                    logs.Error(ex.Message);
                                                }
                                            }
                                        }
                                        info.SMSSENDSTATUS = "1";//0 未发送 1 已发送 -1 发送失败
                                    }
                                }
                                #endregion

                                #region  发送值班员与护林员
                                if (string.IsNullOrEmpty(sms.SMSSENDUSERLIST))//接收人为空 发送值班员与护林员
                                {
                                    #region 值班员 与护林员
                                    if (sms.SMSGROUPTYPE == "1")//值班员
                                    {
                                        var cityorg = level.BYORGNO.Substring(0, 4) + "00000";//市机构码
                                        var contyorg = level.BYORGNO.Substring(0, 6) + "000";//县机构码
                                        if (i == 0)//初次
                                        {
                                            _cityorg = cityorg;
                                            _contyorg = contyorg;
                                        }
                                        else
                                        {
                                            if (_cityorg == cityorg)//循环相同的市级单位 跳出循环
                                            {
                                                continue;
                                            }
                                            if (_contyorg == contyorg)//循环相同的县级单位 跳出循环
                                            {
                                                continue;
                                            }
                                        }
                                        info.TMPCONTENT = sms.TMPCONTENT.Replace("[cityconty]", level.TOPTOWNNAME).Replace("[num]", levelnum.ToString());//短信内容replace
                                        string str = cityorg + "," + contyorg;
                                        var zbylist = OD_USERCls.GetOndutyUserid(level.DCDATE, str);//获取值班员useridlist
                                        if (zbylist.Any())
                                        {
                                            var zbystr = "";
                                            foreach (var zby in zbylist)
                                            {
                                                var m = T_SYSSEC_IPSUSERCls.getModel(new T_SYSSEC_IPSUSER_SW { USERID = zby.ToString() });
                                                if (!string.IsNullOrEmpty(m.PHONE))
                                                {
                                                    if (zbylist.Count() == 1)
                                                    {
                                                        zbystr = zby;
                                                    }
                                                    else
                                                    {
                                                        zbystr += zby + ",";
                                                    }
                                                    try
                                                    {
                                                        ////TODO  Send Message
                                                        //add redis
                                                        //队列
                                                        var msg = m.PHONE.Trim() + "|" + info.TMPCONTENT;
                                                        redisclient.EnqueueItemOnList("SendMsg", msg);//入队。
                                                        logs.Info("==入队列号码信息==" + msg);
                                                        //client.SendMsg(info.TMPCONTENT, m.PHONE);
                                                        Thread.Sleep(200);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        logs.Error(ex.Message);
                                                    }

                                                }
                                            }
                                            info.SMSSENDUSERLIST = zbystr;//值班员人员
                                            info.SMSSENDSTATUS = "1";//0 未发送 1 已发送 -1 发送失败
                                            // ms = YJ_DCSMS_SENDCls.Manager(info);
                                        }
                                        ++i;
                                    }
                                    else if (sms.SMSGROUPTYPE == "2")//护林员
                                    {
                                        info.TMPCONTENT = sms.TMPCONTENT.Replace("[cityconty]", level.TOWNNAME);//短信内容replace
                                        var hlylist = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { BYORGNO = info.BYORGNO });//护林员
                                        if (hlylist.Any())
                                        {
                                            var hlystr = "";//护林员人员 逗号分隔
                                            foreach (var hly in hlylist)
                                            {
                                                if (!string.IsNullOrEmpty(hly.PHONE))
                                                {
                                                    if (hlylist.Count() == 1)
                                                    {
                                                        hlystr = hly.HID;
                                                    }
                                                    else
                                                    {
                                                        hlystr += hly.HID + ",";
                                                    }
                                                    try
                                                    {
                                                        //TODO  Send Message
                                                        //add redis
                                                        //队列
                                                        var msg = hly.PHONE.Trim() + "|" + info.TMPCONTENT;
                                                        redisclient.EnqueueItemOnList("SendMsg", msg);//入队。
                                                        logs.Info("==入队列号码信息==" + msg);
                                                        //client.SendMsg(info.TMPCONTENT, hly.PHONE);
                                                        Thread.Sleep(200);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        logs.Error(ex.Message);
                                                    }

                                                }
                                            }
                                            info.SMSSENDUSERLIST = hlystr;//护林员人员
                                            info.SMSSENDSTATUS = "1";//0 未发送 1 已发送 -1 发送失败
                                            //ms = YJ_DCSMS_SENDCls.Manager(info);
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }
                    }
                }
                else
                {
                    ms = new Message(true, "短信未设置模板!", "");
                }
            }
            else
            {
                ms = new Message(true, "短信发送已设置为手动发送,请手动发送短信!", "");
            }
            return Json(ms);
        }
        #endregion

        #region Private
        public string GetFireLevelHtml(YJ_DANGERCLASS_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" >");
            sb.Append(" <thead>");
            sb.Append("<tr><th>区域</th><th>等级</th><th>时间</th></tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            var list = new List<YJ_DANGERCLASS_Model>();
            if (string.IsNullOrEmpty(sw.DCDATE))
            {
                list = YJ_DANGERCLASSCls.getListModelTop(new YJ_DANGERCLASS_SW() { }).ToList();
                if (!string.IsNullOrEmpty(sw.DANGERCLASS) && sw.DANGERCLASS != "0")
                    list = list.Where(p => p.DANGERCLASS == sw.DANGERCLASS).ToList();
            }
            else
                list = YJ_DANGERCLASSCls.getListModel(sw).ToList();
            if (list.Any())
            {
                foreach (var item in list)
                {
                    sb.Append("<tr>");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", item.TOPTOWNNAME + "==>" + item.TOWNNAME);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", item.DANGERCLASS);
                    sb.AppendFormat("<td class=\"center\">{0}</td>", ClsSwitch.SwitDate(item.DCDATE));
                    sb.Append("</tr>");
                }
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<td colspan=\"3\"><em>暂未查询到记录!</em></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();
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
                    model.AreaName += item.TOWNNAME;//区域
                    model.FireLevel = item.DANGERCLASS;//等级
                    model.LevelDate = ClsSwitch.SwitDate(item.DCDATE);//等级时间; 
                    model.SourceForm = "人工导入";
                    result.Add(model);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据条件选出火险等级
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private IEnumerable<YJJCFireLevelModel> GetModelListBy(string level)
        {
            var result = new List<YJJCFireLevelModel>();
            var list = YJ_DANGERCLASSCls.getListModelTop(new YJ_DANGERCLASS_SW() { });
            if (list.Any())
            {
                var recordlist = list.Where(p => Convert.ToInt32(p.DANGERCLASS) >= Convert.ToInt32(level));
                foreach (var item in recordlist.OrderByDescending(p => p.DANGERCLASS))
                {
                    var model = new YJJCFireLevelModel();
                    if (!string.IsNullOrEmpty(item.TOPTOWNNAME))
                    {
                        model.AreaName = item.TOPTOWNNAME + "==>";
                    }
                    model.AreaName += item.TOWNNAME;//区域
                    model.FireLevel = item.DANGERCLASS;//等级
                    //model.SourceForm = "人工导入";
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

        #endregion
    }
}
