using ManagerSystem.MVC.HelpCom;
using ManagerSystem.MVC.Models;
using ManagerSystemClassLibrary;
using ManagerSystemClassLibrary.SmsHelp;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    public class BigDataShowController : BaseController
    {
        public ActionResult Index()
        {
            Task.Factory.StartNew(() =>
            {
                VoiceCom.ReadVoice();
            });
            return View();
        }

        /// <summary>
        /// 测试地图
        /// </summary>
        /// <returns></returns>
        public ActionResult TestIndex()
        {
            return View();
        }

        public ActionResult TestIndex2()
        {
            return View();
        }

        #region AJAX

        /// <summary>
        /// 获取火情来源数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFireSourceData()
        {
            var result = new List<OutData>();
            MessageListObject ms = new MessageListObject(false, null);
            var sw = new JC_FIRE_SW();
            var curogr = SystemCls.getCurUserOrgNo();
            if (!string.IsNullOrEmpty(curogr))
            {
                var bo = PublicCls.OrgIsShi(curogr);
                var bb = PublicCls.OrgIsXian(curogr);
                var bx = PublicCls.OrgIsZhen(curogr);
                if (!bo)
                {
                    sw.BYORGNO = curogr;
                }
                sw.isCountIndex = "1";
                //遍历火情来源
                Array ary = Enum.GetValues(typeof(EnumType));  //array是数组的基类, 无法实例化
                var list = JC_FIRECls.GetListModel(sw);
                foreach (int item in ary)
                {
                    string hotsum = "0";
                    var fireData = new OutData();
                    if (item.ToString() != "1")//排除红外相机
                    {
                        if (list.Any())
                        {
                            var firelist = list.Where(p => p.FIREFROM == item.ToString() && !string.IsNullOrEmpty(p.FIREFROM) && p.ISOUTFIRE != "1" && p.MANSTATE != "19" && p.MANSTATE != "18");//筛选热点类型 排除已灭的  && ((p.ISOUTFIRE.Trim() == "1" && p.MANSTATE.Trim() != "4") || p.ISOUTFIRE.Trim() != "1") 已在程序中处理
                            hotsum = firelist.Count().ToString();//热点个数
                        }
                        fireData.name = Enum.GetName(typeof(EnumType), item);
                        fireData.value = hotsum;
                        result.Add(fireData);
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }

        /// <summary>
        /// 获取火险等级数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFireLevelData()
        {
            var result = new List<OutData>();
            MessageListObject ms = new MessageListObject(false, null);
            var dataList = YJ_DANGERCLASSCls.getListModel(new YJ_DANGERCLASS_SW { DCDATE = "2017-04-26 00:00:00.000" });
            if (dataList.Any())
            {
                var list = dataList.Where(p => p.BYORGNO.Substring(p.BYORGNO.Length - 3, 3) == "000");
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        var levelData = new OutData();
                        levelData.name = item.TOWNNAME;
                        levelData.value = item.DANGERCLASS;
                        result.Add(levelData);
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }

        /// <summary>
        /// 获取火点信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFireDisTributionData()
        {
            MessageListObject ms = new MessageListObject(false, null);
            var curogr = SystemCls.getCurUserOrgNo();
            if (string.IsNullOrEmpty(curogr))
            {
                ms = new MessageListObject(false, null);
            }
            else
            {
                var dataList = JC_FIRECls.GetListModel(new JC_FIRE_SW() { BYORGNO = curogr, BeginTime = DateTime.Now.AddMonths(-6).ToShortDateString(), EndTime = DateTime.Now.ToShortDateString() });
                ms = new MessageListObject(true, dataList);
            }
            return Json(ms);
        }

        /// <summary>
        /// 获取系统相关信息
        /// </summary>
        /// <returns></returns>
        public JsonResult ShowSysInfoData()
        {
            var linemodel = T_SYSSEC_IPSUSERCls.getUserLineModel(new T_SYSSEC_IPSUSER_SW() { });
            if (linemodel != null)
            {
                string msgcount = "0";
                var ms = SmsCom.GetMsgCount();
                if (ms.Success)
                {
                    msgcount = ms.Msg;
                }
                return Json(new { totalCount = linemodel.LineCount, inCount = linemodel.LineInCount, outCount = linemodel.LineOutCount, msgcount = msgcount });
            }
            else
            {
                return Json(null);
            }
        }
        #endregion
    }
}
