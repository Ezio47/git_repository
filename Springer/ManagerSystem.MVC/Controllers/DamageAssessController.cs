using ManagerSystemClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Web.Mvc;
using NPOI.SS.Util;

namespace ManagerSystem.MVC.Controllers
{
    public class DamageAssessController : BaseController
    {
        #region 灾损评估
        /// <summary>
        /// 火灾档案列表
        /// </summary>
        /// <returns></returns>
        public ActionResult DamageAssess()
        {
            pubViewBag("026001", "026001", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.FIREFROM = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "99", isShowAll = "1" });
            ViewBag.Time = DateTime.Now.ToString("yyyy");
            return View();
        }

        /// <summary>
        /// 异步获取火情档案列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDamageList()
        {
            string PageSize = Request.Params["PageSize"];
            if (PageSize == "0")
                PageSize = ConfigCls.getTableDefaultPageSize();
            string Page = Request.Params["Page"];
            string FIREADDRESSTOWNS = Request.Params["BYORGNO"];
            string Time = Request.Params["Time"];
            string fireTime = !string.IsNullOrEmpty(Time) ? Convert.ToDateTime(Time + "-01-01 00:00:00").ToString() : "";
            string fireEndTime = !string.IsNullOrEmpty(Time) ? Convert.ToDateTime(Time + "-12-31 23:59:59").ToString() : "";
            int total = 0;
            var result = FIRERECORD_FIREINFOCls.getListModel(new FIRERECORD_FIREINFO_SW
            {
                CurPage = int.Parse(Page),
                PageSize = int.Parse(PageSize),
                FIREADDRESSTOWNS = FIREADDRESSTOWNS,
                FIRETIME = fireTime,
                FIREENDTIME = fireEndTime
            }, out total);
            int i = 0;
            bool IsSee = SystemCls.isRight("026001001") ? true : false;
            bool IsAssess = SystemCls.isRight("026001002") ? true : false;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>单位</th><th>发生地</th><th>起火时间</th><th>损失总计</br>元</th><th>森林资源损失率</br>%</th><th>人均损失价值</br>元/人</th><th>森林损失平均价值量</br>元/hm²</th><th>扑火成效比</br>%</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var s in result)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.ORGNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.FIREADDRESSVILLAGES);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.FIRETIME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.LOSSCOUNT > 0 ? string.Format("{0:0.00}", s.LOSSCOUNT) : "");
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.FORESTRESOURCELOSSRATIO > 0 ? string.Format("{0:P}", s.FORESTRESOURCELOSSRATIO) : "");
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.AVGLOSSPERCATITAVALUE > 0 ? string.Format("{0:0.00}", s.AVGLOSSPERCATITAVALUE) : "");
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.WOODLANDLOSSAVGVALUE > 0 ? string.Format("{0:0.00}", s.WOODLANDLOSSAVGVALUE) : "");
                sb.AppendFormat("<td class=\"center\">{0}</td>", s.FIRESUPPEFFECTTHAN > 0 ? string.Format("{0:P}", s.FIRESUPPEFFECTTHAN) : "");
                sb.AppendFormat("<td class=\" \">");
                if (IsSee)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}','{1}')\"  title='查看' class=\"searchBox_01 LinkSee\">查看</a>", s.JCFID, "");
                if (IsAssess)
                    sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Assess','{0}','{1}')\" title='评估' class=\"searchBox_01 LinkMdy\">评估</a>", s.JCFID, Page);
                sb.AppendFormat(" </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(Page), pageSize = int.Parse(PageSize), rowCount = total });
            return Content(JsonConvert.SerializeObject(new MessagePagerAjax(true, sb.ToString(), pageInfo)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 灾损查看
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLostDataSee()
        {
            string JCFID = Request.Params["JCFID"];
            FIRERECORD_FIREINFO_Model m;
            FIRELOST_FIREINFO_Model model;
            T_SYS_DICTTYPE_Model dicType501;
            GetModelandDicType(JCFID, out m, out model, out dicType501);
            ViewBag.SeeData = GetFireLostData(m, model, dicType501);
            return View();
        }

        /// <summary>
        /// 灾损评估
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLostAssess()
        {
            string JCFID = Request.Params["JCFID"];
            FIRERECORD_FIREINFO_Model m;
            FIRELOST_FIREINFO_Model model;
            T_SYS_DICTTYPE_Model dicType501;
            GetModelandDicType(JCFID, out m, out model, out dicType501);
            string dicCount = "";
            if (dicType501 != null && dicType501.DICTTYPEListModel.Count() > 0)
            {
                for (int x = 0; x < dicType501.DICTTYPEListModel.Count(); x++)
                {
                    if (dicType501.DICTTYPEListModel[x].DICTListModel.Count() > 0)
                        dicCount += T_SYS_DICTCls.getDicValueStr(dicType501.DICTTYPEListModel[x].DICTListModel) + ";";
                    else
                        dicCount += "0" + ";";
                }
            }
            if (dicCount.Length > 0)
                dicCount = dicCount.Substring(0, dicCount.Length - 1);
            ViewBag.dicType501 = dicType501;
            ViewBag.dicCount = dicCount;
            ViewBag.JCFID = m.JCFID;
            ViewBag.FIRECODE = m.FIRECODE;
            ViewBag.ORGNAME = m.ORGNAME;
            ViewBag.FIRETIME = m.FIRETIME;
            return View(model);
        }

        /// <summary>
        /// 异步加载灾损类别
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSSMX()
        {
            string FIREINFOID = Request.Params["FIREINFOID"];
            string Dic = Request.Params["Dic"];
            string Type = Request.Params["TYPE"];
            StringBuilder sb = new StringBuilder();
            FIRELOST_LOSTTYPECOUNT_Model m = new FIRELOST_LOSTTYPECOUNT_Model();
            string louseMoney = "", mark = "";
            float money = 0;

            #region 直接损失

            #region 林木资源价值损失
            if (Dic == "001")
            {

            }
            #endregion

            #region 木材损失
            if (Dic == "002")
            {
                var result = FIRELOST_LOSTTYPE_WOODCls.getListModel(new FIRELOST_LOSTTYPE_WOOD_SW { FIRELOST_FIREINFOID = FIREINFOID });
                if (result.Count() > 0)
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\">");
                    sb.AppendFormat("<tr><th>序号</th><th>名称</th><th>损失金额(元)</th><th>过火木材材积(m³)</th><th>市场价格(元/m³)</th><th>残值(元)</th></tr>");
                    int i = 0;
                    foreach (var r in result)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.WOODNAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEVOLUME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.MARKETPRICE);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.RESIDUALVALUE);
                        sb.AppendFormat("</tr>");
                    }
                    sb.AppendFormat("</table>");
                    money = result.Sum(a => float.Parse(a.LOSEMONEYCOUNT));
                }
            }
            #endregion

            #region 固定资产损失
            if (Dic == "003")
            {
                var result = FIRELOST_LOSTTYPE_FIXEDASSETSCls.getListModel(new FIRELOST_LOSTTYPE_FIXEDASSETS_SW { FIRELOST_FIREINFOID = FIREINFOID });
                if (result.Count() > 0)
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\">");
                    sb.AppendFormat("<tr><th>序号</th><th>名称</th><th>损失金额(元)</th><th>重置价值(元)</th><th>年平均折旧率</th><th>烧毁率</th></tr>");
                    int i = 0;
                    foreach (var r in result)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.FIXEDASSETSNAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.RESETVALUE);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", string.Format("{0:P}", float.Parse(r.YEARAVGDEPRECIATIONRATE) / 100));
                        sb.AppendFormat("<td class=\"center\">{0}</td>", string.Format("{0:P}", float.Parse(r.BURNRATE) / 100));
                        sb.AppendFormat("</tr>");
                    }
                    sb.AppendFormat("</table>");
                }
                money = result.Sum(a => float.Parse(a.LOSEMONEYCOUNT));
            }
            #endregion

            #region 流动资产损失
            if (Dic == "004")
            {
                var result = FIRELOST_LOSTTYPE_CURRENTASSETSCls.getListModel(new FIRELOST_LOSTTYPE_CURRENTASSETS_SW { FIRELOST_FIREINFOID = FIREINFOID });
                if (result.Count() > 0)
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\">");
                    sb.AppendFormat("<tr><th>序号</th><th>名称</th><th>损失金额(元)</th></th><th>数量</th><th>购入价格</th><th>残值(元)</th></tr>");
                    int i = 0;
                    foreach (var r in result)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.CURRENTASSETSNAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.CURRENTASSETSCOUNT + r.CURRENTASSETSUNIT);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.CURRENTASSETSPRICE + "元/" + r.CURRENTASSETSUNIT);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.RESIDUALVALUE);
                        sb.AppendFormat("</tr>");
                    }
                    sb.AppendFormat("</table>");
                }
                money = result.Sum(a => float.Parse(a.LOSEMONEYCOUNT));
            }
            #endregion

            #region 非木质林产品损失
            if (Dic == "005")
            {
                var result = FIRELOST_LOSTTYPE_NTFPCls.getListModel(new FIRELOST_LOSTTYPE_NTFP_SW { FIRELOST_FIREINFOID = FIREINFOID });
                if (result.Count() > 0)
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\">");
                    sb.AppendFormat("<tr><th>序号</th><th>名称</th><th>损失金额(元)</th><th>损失数量(kg)</th><th>市场平均价格(元/kg)</th></tr>");
                    int i = 0;
                    foreach (var r in result)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.NTFPNAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSECOUNT);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.MARKETAVGPRICE);
                        sb.AppendFormat("</tr>");
                    }
                    sb.AppendFormat("</table>");
                }
                money = result.Sum(a => float.Parse(a.LOSEMONEYCOUNT));
            }
            #endregion

            #region 农牧产品
            if (Dic == "006")
            {
                var result = FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTCls.getListModel(new FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_SW { FIRELOST_FIREINFOID = FIREINFOID });
                if (result.Count() > 0)
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\">");
                    sb.AppendFormat("<tr><th>序号</th><th>名称</th><th>损失类别</th><th>损失金额(元)</th><th>数量(面积)</th><th>市场价格(生产成本)</th></tr>");
                    int i = 0;
                    foreach (var r in result)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.FARMPASTUREPRODUCNAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.PASTUREPRODUCCODENAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        string loseCount = r.LOSECOUNT, basePrice = r.BASEPRICE;
                        if (r.FARMPASTUREPRODUCCODE == "1")
                        {
                            loseCount += "kg";
                            basePrice += "元/kg";
                        }
                        if (r.FARMPASTUREPRODUCCODE == "2")
                        {
                            loseCount += "hm²";
                            basePrice += "元/hm²";
                        }
                        if (r.FARMPASTUREPRODUCCODE == "3")
                        {
                            loseCount += "头/或只";
                            basePrice += "元/头或只";
                        }
                        sb.AppendFormat("<td class=\"center\">{0}</td>", loseCount);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", basePrice);
                        sb.AppendFormat("</tr>");
                    }
                    sb.AppendFormat("</table>");
                }
                money = result.Sum(a => float.Parse(a.LOSEMONEYCOUNT));
            }
            #endregion

            #region 火灾扑救费用
            if (Dic == "007")
            {
                var result1 = FIRELOST_LOSTTYPE_ATTACK_P1Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P1_SW { FIRELOST_FIREINFOID = FIREINFOID });
                var result2 = FIRELOST_LOSTTYPE_ATTACK_P2Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P2_SW { FIRELOST_FIREINFOID = FIREINFOID });
                var result3 = FIRELOST_LOSTTYPE_ATTACK_P3Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P3_SW { FIRELOST_FIREINFOID = FIREINFOID });
                var result4 = FIRELOST_LOSTTYPE_ATTACK_P4Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P4_SW { FIRELOST_FIREINFOID = FIREINFOID });
                var result5 = FIRELOST_LOSTTYPE_ATTACK_P5Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P5_SW { FIRELOST_FIREINFOID = FIREINFOID });
                var result6 = FIRELOST_LOSTTYPE_ATTACK_P6Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P6_SW { FIRELOST_FIREINFOID = FIREINFOID });
                string _dic1Name = T_SYS_DICTCls.getDicTypeName(new T_SYS_DICTTYPE_SW { DICTTYPEID = "513" });
                string _dic2Name = T_SYS_DICTCls.getDicTypeName(new T_SYS_DICTTYPE_SW { DICTTYPEID = "514" });
                string _dic3Name = T_SYS_DICTCls.getDicTypeName(new T_SYS_DICTTYPE_SW { DICTTYPEID = "515" });
                string _dic4Name = T_SYS_DICTCls.getDicTypeName(new T_SYS_DICTTYPE_SW { DICTTYPEID = "516" });
                string _dic5Name = T_SYS_DICTCls.getDicTypeName(new T_SYS_DICTTYPE_SW { DICTTYPEID = "517" });
                string _dic6Name = T_SYS_DICTCls.getDicTypeName(new T_SYS_DICTTYPE_SW { DICTTYPEID = "518" });
                if (result1.Count() > 0 || result2.Count() > 0 || result3.Count() > 0 || result4.Count() > 0 || result5.Count() > 0 || result6.Count() > 0)
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\">");
                    sb.AppendFormat("<tr><th>序号</td><th>名称</th><th>损失类别</th><th>损失金额(元)</th></tr>");
                    int i = 0;
                    foreach (var r in result1)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.P1NAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", _dic1Name);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        sb.AppendFormat("</tr>");
                    }
                    foreach (var r in result2)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.P2NAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", _dic2Name);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        sb.AppendFormat("</tr>");
                    }
                    foreach (var r in result3)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.P3NAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", _dic3Name);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        sb.AppendFormat("</tr>");
                    }
                    foreach (var r in result4)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.P4NAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", _dic4Name);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        sb.AppendFormat("</tr>");
                    }
                    foreach (var r in result5)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.P5NAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", _dic5Name);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        sb.AppendFormat("</tr>");
                    }
                    foreach (var r in result6)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.P6NAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", _dic6Name);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        sb.AppendFormat("</tr>");
                    }
                    sb.AppendFormat("</table>");
                }
                money = result1.Sum(a => float.Parse(a.LOSEMONEYCOUNT)) + result2.Sum(a => float.Parse(a.LOSEMONEYCOUNT)) + result3.Sum(a => float.Parse(a.LOSEMONEYCOUNT))
                    + result4.Sum(a => float.Parse(a.LOSEMONEYCOUNT)) + result5.Sum(a => float.Parse(a.LOSEMONEYCOUNT)) + result6.Sum(a => float.Parse(a.LOSEMONEYCOUNT));
            }
            #endregion

            #region 人员伤亡损失
            if (Dic == "008")
            {
                var result = FIRELOST_LOSTTYPE_CASUALTYCls.getListModel(new FIRELOST_LOSTTYPE_CASUALTY_SW { FIRELOST_FIREINFOID = FIREINFOID });
                if (result.Count() > 0)
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\">");
                    sb.AppendFormat("<tr><th>序号</th><th>伤亡名称</th><th>伤亡类别</th><th>损失金额(元)</th><th>伤亡人数</th></tr>");
                    int i = 0;
                    foreach (var r in result)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.CASUALTYNAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.CASUALTYCODENAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.CASUALTYNUMBERS);
                        sb.AppendFormat("</tr>");
                    }
                    sb.AppendFormat("</table>");
                }
                money = result.Sum(a => float.Parse(a.LOSEMONEYCOUNT));
            }
            #endregion

            #region 居民财产损失
            if (Dic == "009")
            {
                var result = FIRELOST_LOSTTYPE_RESIDENTPROPERTYCls.getListModel(new FIRELOST_LOSTTYPE_RESIDENTPROPERTY_SW { FIRELOST_FIREINFOID = FIREINFOID });
                if (result.Count() > 0)
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\">");
                    sb.AppendFormat("<tr><th>序号</th><th>财产名称</th><th>损失金额(元)</th><th>数量</th><th>购入价</th><th>年平均折旧率</th><th>已使用年限</th></tr>");
                    int i = 0;
                    foreach (var r in result)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.RESIDENTPROPERTYNAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.RESIDENTPROPERTYCOUNT + r.RESIDENTPROPERTYUNIT);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.RESIDENTPROPERTYPRICE + "元/" + r.RESIDENTPROPERTYUNIT);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", string.Format("{0:P}", float.Parse(r.YEARAVGDEPRECIATIONRATE) / 100));
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.HAVEUSEYEAR + "年");
                        sb.AppendFormat("</tr>");
                    }
                    sb.AppendFormat("</table>");
                }
                money = result.Sum(a => float.Parse(a.LOSEMONEYCOUNT));
            }
            #endregion

            #region 野生动物损失
            if (Dic == "010")
            {
                var result = FIRELOST_LOSTTYPE_WILDANIMALCls.getListModel(new FIRELOST_LOSTTYPE_WILDANIMAL_SW { FIRELOST_FIREINFOID = FIREINFOID });
                if (result.Count() > 0)
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\">");
                    sb.AppendFormat("<tr><th>序号</th><th>野生动物名称</th><th>损失金额(元)</th><th>烧死数量(头/只)</th><th>价格(元/头或只)</th><th>残值(元)</th></tr>");
                    int i = 0;
                    foreach (var r in result)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.WILDANIMALNAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.WILDANIMALCOUNT);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.WILDANIMALPRICE);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.RESIDUALVALUE);
                        sb.AppendFormat("</tr>");
                    }
                    sb.AppendFormat("</table>");
                }
                money = result.Sum(a => float.Parse(a.LOSEMONEYCOUNT));
            }
            #endregion

            #endregion

            #region 间接损失

            #region 停(减)产损失
            if (Dic == "101")
            {
                var result = FIRELOST_LOSTTYPE_STOPREDUCTIONCls.getListModel(new FIRELOST_LOSTTYPE_STOPREDUCTION_SW { FIRELOST_FIREINFOID = FIREINFOID });
                if (result.Count() > 0)
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\">");
                    sb.AppendFormat("<tr><th>序号</th><th>停(减)产名称</th><th>停(减)产类别</th><th>损失金额(元)</th><th>数量</th><th>时间</th><th>价格</th></tr>");
                    int i = 0;
                    foreach (var r in result)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.STOPREDUCTIONNAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.STOPREDUCTIONCODENAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        string stopCount = r.STOPREDUCTIONCOUNT, stopTime = r.STOPREDUCTIONTIME, stopPrice = r.STOPREDUCTIONPRICE;
                        if (r.STOPREDUCTIONCODE == "1")
                        {
                            stopCount += "人";
                            stopTime += "d";
                            stopPrice += "元/人/d";
                        }
                        if (r.STOPREDUCTIONCODE == "2")
                        {
                            stopCount += "件/d";
                            stopTime += "d";
                            stopPrice += "元/件";
                        }
                        if (r.STOPREDUCTIONCODE == "3")
                        {
                            stopCount = "—";
                            stopTime += "d";
                            stopPrice += "元/d";
                        }
                        sb.AppendFormat("<td class=\"center\">{0}</td>", stopCount);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", stopTime);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", stopPrice);
                        sb.AppendFormat("</tr>");
                    }
                    sb.AppendFormat("</table>");
                }
            }
            #endregion

            #region 灾后处理费用
            if (Dic == "102")
            {
                var result = FIRELOST_LOSTTYPE_LOSTPROCESSCls.getListModel(new FIRELOST_LOSTTYPE_LOSTPROCESS_SW { FIRELOST_FIREINFOID = FIREINFOID });
                if (result.Count() > 0)
                {
                    sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\">"); ;
                    sb.AppendFormat("<tr><th>序号</th><th>灾后处理名称</th><th>损失类别</th><th>损失金额(元)</th></tr>");
                    int i = 0;
                    foreach (var r in result)
                    {
                        i++;
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSTPROCESSNAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSTPROCESSCODENAME);
                        sb.AppendFormat("<td class=\"center\">{0}</td>", r.LOSEMONEYCOUNT);
                        sb.AppendFormat("</tr>");
                    }
                    sb.AppendFormat("</table>");
                }
            }
            #endregion

            #region 森林生态价值损失
            if (Dic == "103")
            {

            }
            #endregion

            #endregion

            m = FIRELOST_LOSTTYPECOUNTCls.getModel(new FIRELOST_LOSTTYPECOUNT_SW { FIRELOST_FIREINFOID = FIREINFOID, FIRELOSETYPECODE = Dic });
            if (m != null)
            {
                louseMoney = !string.IsNullOrEmpty(m.LOSEMONEY) && m.LOSEMONEY != "0" ? m.LOSEMONEY : "";
                mark = !string.IsNullOrEmpty(m.MARK) ? m.MARK : "";
            }

            if (Type == "1")
                return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), louseMoney + "," + mark)), "text/html;charset=UTF-8");
            else
                return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), money > 0 ? money.ToString() + "," + mark : "" + "," + mark)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 灾损分类统计数据管理
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRELOSTTYPEManager()
        {
            FIRELOST_LOSTTYPECOUNT_Model m = new FIRELOST_LOSTTYPECOUNT_Model();
            m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
            m.FIRELOSETYPECODE = Request.Params["TYPECODE"];
            m.LOSEMONEY = Request.Params["LOSEMONEY"];
            m.MARK = Request.Params["Mark"];
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPECOUNTCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 灾损评估报表
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLostReport()
        {
            string JCFID = Request.Params["JCFID"];
            FIRERECORD_FIREINFO_Model m;
            FIRELOST_FIREINFO_Model model;
            T_SYS_DICTTYPE_Model dicType501;
            GetModelandDicType(JCFID, out m, out model, out dicType501);
            ViewBag.JCFID = m.JCFID;
            ViewBag.SeeData = GetFireLostData(m, model, dicType501);
            return View();
        }

        /// <summary>
        /// 灾损评估报表导出
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLostExportExcel()
        {
            #region 数据准备
            string JCFID = Request.Params["JCFID"];
            FIRERECORD_FIREINFO_Model m;
            FIRELOST_FIREINFO_Model model;
            T_SYS_DICTTYPE_Model dicType501;
            GetModelandDicType(JCFID, out m, out model, out dicType501);
            List<FIRELOST_LOSTTYPECOUNT_Model> templist = FIRELOST_LOSTTYPECOUNTCls.getListModel(new FIRELOST_LOSTTYPECOUNT_SW { FIRELOST_FIREINFOID = model.FIRELOST_FIREINFOID }).ToList();
            int colsCount = 4;
            string title = "森林火灾损失汇总表";
            #endregion

            #region 写入Excel工作簿
            HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("Sheet1");//添加一个sheet
            sheet1.IsPrintGridlines = true; //打印时显示网格线
            sheet1.DisplayGridlines = true;//查看时显示网格线 
            for (int i = 0; i < colsCount; i++)
            {
                sheet1.SetColumnWidth(i, 30 * 256);
            }
            int index = 0;
            IRow rowTitle = sheet1.CreateRow(index);
            rowTitle.CreateCell(0).SetCellValue(title);
            rowTitle.GetCell(0).CellStyle = getCellStyleTitle(book);
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, colsCount - 1));
            index++;
            IRow row = sheet1.CreateRow(index);
            row.CreateCell(0).SetCellValue("森林火灾编号");
            row.GetCell(0).CellStyle = getCellStyleCenter(book);
            row.CreateCell(1).SetCellValue(m.FIRECODE);
            row.GetCell(1).CellStyle = getCellStyleLeft(book);
            sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 1, colsCount - 1));
            index++;
            row = sheet1.CreateRow(index);
            row.CreateCell(0).SetCellValue("起火单位");
            row.GetCell(0).CellStyle = getCellStyleCenter(book);
            row.CreateCell(1).SetCellValue(m.ORGNAME);
            row.GetCell(1).CellStyle = getCellStyleCenter(book);
            row.CreateCell(2).SetCellValue("起火时间");
            row.GetCell(2).CellStyle = getCellStyleCenter(book);
            row.CreateCell(3).SetCellValue(m.FIRETIME);
            row.GetCell(3).CellStyle = getCellStyleCenter(book);
            index++;
            row = sheet1.CreateRow(index);
            row.CreateCell(0).SetCellValue("火场面积/hm²");
            row.GetCell(0).CellStyle = getCellStyleCenter(book);
            row.CreateCell(1).SetCellValue(model.FIREAREA);
            row.GetCell(1).CellStyle = getCellStyleCenter(book);
            row.CreateCell(2).SetCellValue("森林火灾受害面积/hm²");
            row.GetCell(2).CellStyle = getCellStyleCenter(book);
            row.CreateCell(3).SetCellValue(model.FIRELOSEAREA);
            row.GetCell(3).CellStyle = getCellStyleCenter(book);
            index++;
            row = sheet1.CreateRow(index);
            row.CreateCell(0).SetCellValue("伤(亡)人数/人");
            row.GetCell(0).CellStyle = getCellStyleCenter(book);
            row.CreateCell(1).SetCellValue(model.CASUALTYCOUNT);
            row.GetCell(1).CellStyle = getCellStyleCenter(book);
            row.CreateCell(2).SetCellValue("损失林木蓄积/hm²");
            row.GetCell(2).CellStyle = getCellStyleCenter(book);
            row.CreateCell(3).SetCellValue(model.XJLLOSE);
            row.GetCell(3).CellStyle = getCellStyleCenter(book);
            index++;
            row = sheet1.CreateRow(index);
            row.CreateCell(0).SetCellValue("建筑物(或构建物)\n损失量/m²");
            row.GetCell(0).CellStyle = getCellStyleCenter(book);
            row.CreateCell(1).SetCellValue(model.BUILDINGLOSECOUNT);
            row.GetCell(1).CellStyle = getCellStyleCenter(book);
            row.CreateCell(2).SetCellValue("机械设备损失量/\n台、件");
            row.GetCell(2).CellStyle = getCellStyleCenter(book);
            row.CreateCell(3).SetCellValue(model.MACHINERYLOSECOUNT);
            row.GetCell(3).CellStyle = getCellStyleCenter(book);
            index++;
            row = sheet1.CreateRow(index);
            row.CreateCell(0).SetCellValue("损失分类");
            row.GetCell(0).CellStyle = getCellStyleCenter(book);
            sheet1.AddMergedRegion(new CellRangeAddress(index, index, 0, 1));
            row.CreateCell(2).SetCellValue("损失金额(元)");
            row.GetCell(2).CellStyle = getCellStyleCenter(book);
            row.CreateCell(3).SetCellValue("备注");
            row.GetCell(3).CellStyle = getCellStyleCenter(book);
            index++;

            #region 灾损分类
            if (dicType501 != null && dicType501.DICTTYPEListModel.Count > 0)
            {
                foreach (var type in dicType501.DICTTYPEListModel)
                {
                    int rowCount = type.DICTListModel.Count;
                    if (rowCount > 0)
                    {
                        FIRELOST_LOSTTYPECOUNT_Model m1 = templist.Where(a => a.FIRELOSETYPECODE == type.DICTListModel[0].DICTVALUE).FirstOrDefault();
                        string louseMoney1 = (m1 != null && !string.IsNullOrEmpty(m1.LOSEMONEY)) ? m1.LOSEMONEY : "";
                        string mark1 = (m1 != null && !string.IsNullOrEmpty(m1.MARK)) ? m1.MARK : "";
                        row = sheet1.CreateRow(index);
                        row.CreateCell(0).SetCellValue(type.DICTTYPENAME);
                        row.GetCell(0).CellStyle = getCellStyleCenter(book);
                        sheet1.AddMergedRegion(new CellRangeAddress(index, index + rowCount - 1, 0, 0));
                        row.CreateCell(1).SetCellValue(type.DICTListModel[0].DICTNAME);
                        row.GetCell(1).CellStyle = getCellStyleLeft(book);
                        row.CreateCell(2).SetCellValue(louseMoney1);
                        row.GetCell(2).CellStyle = getCellStyleCenter(book);
                        row.CreateCell(3).SetCellValue(mark1);
                        row.GetCell(3).CellStyle = getCellStyleCenter(book);
                        index++;
                        for (int i = 1; i < rowCount; i++)
                        {
                            FIRELOST_LOSTTYPECOUNT_Model m2 = templist.Where(a => a.FIRELOSETYPECODE == type.DICTListModel[i].DICTVALUE).FirstOrDefault();
                            string louseMoney2 = (m2 != null && !string.IsNullOrEmpty(m2.LOSEMONEY)) ? m2.LOSEMONEY : "";
                            string mark2 = (m2 != null && !string.IsNullOrEmpty(m2.MARK)) ? m2.MARK : "";
                            row = sheet1.CreateRow(index);
                            row.CreateCell(1).SetCellValue(type.DICTListModel[i].DICTNAME);
                            row.GetCell(1).CellStyle = getCellStyleLeft(book);
                            row.CreateCell(2).SetCellValue(louseMoney2);
                            row.GetCell(2).CellStyle = getCellStyleCenter(book);
                            row.CreateCell(3).SetCellValue(mark2);
                            row.GetCell(3).CellStyle = getCellStyleCenter(book);
                            index++;
                        }
                    }
                }
            }
            #endregion

            row = sheet1.CreateRow(index);
            row.CreateCell(0).SetCellValue("损失总计");
            row.GetCell(0).CellStyle = getCellStyleCenter(book);
            sheet1.AddMergedRegion(new CellRangeAddress(index, index, 0, 1));
            row.CreateCell(2).SetCellValue(model.LOSSCOUNT + "元");
            row.GetCell(2).CellStyle = getCellStyleCenter(book);
            sheet1.AddMergedRegion(new CellRangeAddress(index, index, 2, 3));
            index++;
            row = sheet1.CreateRow(index);
            row.CreateCell(0).SetCellValue("森林资源损失率/%");
            row.GetCell(0).CellStyle = getCellStyleCenter(book);
            sheet1.AddMergedRegion(new CellRangeAddress(index, index, 0, 1));
            row.CreateCell(2).SetCellValue(model.FORESTRESOURCELOSSRATIO);
            row.GetCell(2).CellStyle = getCellStyleCenter(book);
            sheet1.AddMergedRegion(new CellRangeAddress(index, index, 2, 3));
            index++;
            row = sheet1.CreateRow(index);
            row.CreateCell(0).SetCellValue("人均损失价值元/人");
            row.GetCell(0).CellStyle = getCellStyleCenter(book);
            sheet1.AddMergedRegion(new CellRangeAddress(index, index, 0, 1));
            row.CreateCell(2).SetCellValue(model.AVGLOSSPERCATITAVALUE);
            row.GetCell(2).CellStyle = getCellStyleCenter(book);
            sheet1.AddMergedRegion(new CellRangeAddress(index, index, 2, 3));
            index++;
            row = sheet1.CreateRow(index);
            row.CreateCell(0).SetCellValue("林地损失平均价值量元/hm²");
            row.GetCell(0).CellStyle = getCellStyleCenter(book);
            sheet1.AddMergedRegion(new CellRangeAddress(index, index, 0, 1));
            row.CreateCell(2).SetCellValue(model.WOODLANDLOSSAVGVALUE);
            row.GetCell(2).CellStyle = getCellStyleCenter(book);
            sheet1.AddMergedRegion(new CellRangeAddress(index, index, 2, 3));
            index++;
            row = sheet1.CreateRow(index);
            row.CreateCell(0).SetCellValue("扑火成效比%");
            row.GetCell(0).CellStyle = getCellStyleCenter(book);
            sheet1.AddMergedRegion(new CellRangeAddress(index, index, 0, 1));
            row.CreateCell(2).SetCellValue(model.FIRESUPPEFFECTTHAN);
            row.GetCell(2).CellStyle = getCellStyleCenter(book);
            sheet1.AddMergedRegion(new CellRangeAddress(index, index, 2, 3));
            index++;
            #endregion

            #region 保存到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string fileName = title + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
            #endregion
        }

        /// <summary>
        /// 灾损火情信息数据管理--增、改
        /// </summary>
        /// <returns></returns>
        public ActionResult FIRELOSTINFManager()
        {
            FIRELOST_FIREINFO_Model m = new FIRELOST_FIREINFO_Model();
            m.FIRELOST_FIREINFOID = Request.Params["FIRELOST_FIREINFOID"];
            m.JCFID = Request.Params["JCFID"];
            m.TOTALAREA = Request.Params["TOTALAREA"];
            m.TOTALPERSON = Request.Params["TOTALPERSON"];
            m.TOTALXJL = Request.Params["TOTALXJL"];
            m.FIREAREA = Request.Params["FIREAREA"];
            m.FIRELOSEAREA = Request.Params["FIRELOSEAREA"];
            m.XJLLOSE = Request.Params["XJLLOSE"];
            m.CASUALTYCOUNT = Request.Params["CASUALTYCOUNT"];
            m.BUILDINGLOSECOUNT = Request.Params["BUILDINGLOSECOUNT"];
            m.MACHINERYLOSECOUNT = Request.Params["MACHINERYLOSECOUNT"];
            m.TOTALAREAJWDLIST = Request.Params["TOTALAREAJWDLIST"];
            m.FIREAREAJWDLIST = Request.Params["FIREAREAJWDLIST"];
            m.FIRELOSEAREAJWDLIST = Request.Params["FIRELOSEAREAJWDLIST"];
            m.LOSSCOUNT = Request.Params["LOSSCOUNT"];
            m.FORESTRESOURCELOSSRATIO = Request.Params["FORESTRESOURCELOSSRATIO"];
            m.AVGLOSSPERCATITAVALUE = Request.Params["AVGLOSSPERCATITAVALUE"];
            m.WOODLANDLOSSAVGVALUE = Request.Params["WOODLANDLOSSAVGVALUE"];
            m.FIRESUPPEFFECTTHAN = Request.Params["FIRESUPPEFFECTTHAN"];
            m.opMethod = Request.Params["Method"];
            return Content(JsonConvert.SerializeObject(FIRELOST_FIREINFOCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 灾损分类管理

        #region 木材损失管理
        /// <summary>
        /// 木材损失
        /// </summary>
        /// <returns></returns>
        public ActionResult WOOD()
        {
            string FIREINFOID = Request.Params["FIREINFOID"];
            ViewBag.FIREINFOID = FIREINFOID;
            return View();
        }

        /// <summary>
        /// 异步获取木材损失数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWoodList()
        {
            StringBuilder sb = new StringBuilder();
            string FIREINFOID = Request.Params["FIREINFOID"];
            string WOODNAME = Request.Params["WOODNAME"];
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>木材名称</th><th>损失金额(元)</th><th>过火木材材积(m³)</th><th>市场价格(元/m³)</th><th>残值(元)</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            var list = FIRELOST_LOSTTYPE_WOODCls.getListModel(new FIRELOST_LOSTTYPE_WOOD_SW { FIRELOST_FIREINFOID = FIREINFOID, WOODNAME = WOODNAME });
            int i = 1;
            foreach (var v in list)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.WOODNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEVOLUME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MARKETPRICE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.RESIDUALVALUE);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.FIRELOST_LOSTTYPE_WOODID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.FIRELOST_LOSTTYPE_WOODID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.FIRELOST_LOSTTYPE_WOODID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取单条木材损失数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWOODJson()
        {
            string WOODID = Request.Params["WOODID"];
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_WOODCls.getModel(new FIRELOST_LOSTTYPE_WOOD_SW { FIRELOST_LOSTTYPE_WOODID = WOODID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查看木材损失
        /// </summary>
        /// <returns></returns>
        public ActionResult WOODDataSee()
        {
            string WOODID = Request.Params["WOODID"];
            FIRELOST_LOSTTYPE_WOOD_Model m = FIRELOST_LOSTTYPE_WOODCls.getModel(new FIRELOST_LOSTTYPE_WOOD_SW { FIRELOST_LOSTTYPE_WOODID = WOODID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"text-align:left;\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">木材损失名称:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.WOODNAME);
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">损失金额:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.LOSEMONEYCOUNT + "元");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">过火木材材积:</td>");
            sb.AppendFormat("<td>{0}</td>", m.LOSEVOLUME + "m³");
            sb.AppendFormat("<td class=\"tdField\">市场价格:</td>");
            sb.AppendFormat("<td>{0}</td>", m.MARKETPRICE + "元/m³");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">残值:</td>");
            sb.AppendFormat("<td>{0}</td>", m.RESIDUALVALUE + "元");
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">备注:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 木材损失数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult WOODManager()
        {
            FIRELOST_LOSTTYPE_WOOD_Model m = new FIRELOST_LOSTTYPE_WOOD_Model();
            m.FIRELOST_LOSTTYPE_WOODID = Request.Params["WOODID"];
            m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
            m.WOODNAME = Request.Params["WOODNAME"];
            m.LOSEVOLUME = Request.Params["LOSEVOLUME"];
            m.MARKETPRICE = Request.Params["MARKETPRICE"];
            m.RESIDUALVALUE = Request.Params["RESIDUALVALUE"];
            m.MARK = Request.Params["MARK"];
            m.opMethod = Request.Params["Method"];
            if (m.opMethod != "Del")
            {
                float temp = float.Parse(m.LOSEVOLUME) * float.Parse(m.MARKETPRICE) - float.Parse(m.RESIDUALVALUE);
                if (temp < 0)
                    return Content(JsonConvert.SerializeObject(new Message(false, "过火木材材积*市场价格-残值<0", "")), "text/html;charset=UTF-8");
                m.LOSEMONEYCOUNT = temp.ToString();
            }
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_WOODCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 固定资产损失管理
        /// <summary>
        /// 固定资产损失
        /// </summary>
        /// <returns></returns>
        public ActionResult FIXEDASSETS()
        {
            string FIREINFOID = Request.Params["FIREINFOID"];
            ViewBag.FIREINFOID = FIREINFOID;
            return View();
        }

        /// <summary>
        /// 异步获取固定资产损失数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFIXEDASSETSList()
        {
            StringBuilder sb = new StringBuilder();
            string FIREINFOID = Request.Params["FIREINFOID"];
            string DASSETSNAME = Request.Params["DASSETSNAME"];
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>固定资产名称</th><th>损失金额(元)</th><th>重置价值(元)</th><th>年平均折旧率</th><th>已使用年限</th><th>烧毁率</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            var list = FIRELOST_LOSTTYPE_FIXEDASSETSCls.getListModel(new FIRELOST_LOSTTYPE_FIXEDASSETS_SW { FIRELOST_FIREINFOID = FIREINFOID, FIXEDASSETSNAME = DASSETSNAME });
            int i = 1;
            foreach (var v in list)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.FIXEDASSETSNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.RESETVALUE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", string.Format("{0:P}", float.Parse(v.YEARAVGDEPRECIATIONRATE) / 100));
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.HAVEUSEYEAR);
                sb.AppendFormat("<td class=\"center\">{0}</td>", string.Format("{0:P}", float.Parse(v.BURNRATE) / 100));
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.FIRELOST_LOSTTYPE_FIXEDASSETSID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.FIRELOST_LOSTTYPE_FIXEDASSETSID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.FIRELOST_LOSTTYPE_FIXEDASSETSID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取单条固定资产损失数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFIXEDASSETSJson()
        {
            string FIXEDASSETSID = Request.Params["FIXEDASSETSID"];
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_FIXEDASSETSCls.getModel(new FIRELOST_LOSTTYPE_FIXEDASSETS_SW { FIRELOST_LOSTTYPE_FIXEDASSETSID = FIXEDASSETSID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查看固定资产损失
        /// </summary>
        /// <returns></returns>
        public ActionResult FIXEDASSETSDataSee()
        {
            string FIXEDASSETSID = Request.Params["FIXEDASSETSID"];
            FIRELOST_LOSTTYPE_FIXEDASSETS_Model m = FIRELOST_LOSTTYPE_FIXEDASSETSCls.getModel(new FIRELOST_LOSTTYPE_FIXEDASSETS_SW { FIRELOST_LOSTTYPE_FIXEDASSETSID = FIXEDASSETSID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"text-align:left;\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">固定资产名称:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.FIXEDASSETSNAME);
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">损失金额:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.LOSEMONEYCOUNT + "元");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">重置价值:</td>");
            sb.AppendFormat("<td>{0}</td>", m.RESETVALUE + "元");
            sb.AppendFormat("<td class=\"tdField\">年平均折旧率:</td>");
            sb.AppendFormat("<td>{0}</td>", string.Format("{0:P}", float.Parse(m.YEARAVGDEPRECIATIONRATE) / 100));
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">已使用年限:</td>");
            sb.AppendFormat("<td>{0}</td>", m.HAVEUSEYEAR + "年");
            sb.AppendFormat("<td class=\"tdField\">烧毁率:</td>");
            sb.AppendFormat("<td>{0}</td>", string.Format("{0:P}", float.Parse(m.BURNRATE) / 100));
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">备注:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 固定资产损失数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult FIXEDASSETSManager()
        {
            FIRELOST_LOSTTYPE_FIXEDASSETS_Model m = new FIRELOST_LOSTTYPE_FIXEDASSETS_Model();
            m.FIRELOST_LOSTTYPE_FIXEDASSETSID = Request.Params["FIXEDASSETSID"];
            m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
            m.FIXEDASSETSNAME = Request.Params["DASSETSNAME"];
            m.RESETVALUE = Request.Params["RESETVALUE"];
            m.YEARAVGDEPRECIATIONRATE = Request.Params["DEPRECIATIONRATE"];
            m.HAVEUSEYEAR = Request.Params["HAVEUSEYEAR"];
            m.BURNRATE = Request.Params["BURNRATE"];
            m.MARK = Request.Params["MARK"];
            m.opMethod = Request.Params["Method"];
            if (m.opMethod != "Del")
            {
                if (1 - (float.Parse(m.YEARAVGDEPRECIATIONRATE) / 100) * float.Parse(m.HAVEUSEYEAR) < 0)
                    return Content(JsonConvert.SerializeObject(new Message(false, "年平均折旧率*已使用年限>1", "")), "text/html;charset=UTF-8");
                m.LOSEMONEYCOUNT = (float.Parse(m.RESETVALUE) * (1 - (float.Parse(m.YEARAVGDEPRECIATIONRATE) / 100) * float.Parse(m.HAVEUSEYEAR)) * (float.Parse(m.BURNRATE) / 100)).ToString();
            }
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_FIXEDASSETSCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 流动资产损失管理
        /// <summary>
        /// 流动资产损失
        /// </summary>
        /// <returns></returns>
        public ActionResult CURRENTASSETS()
        {
            string FIREINFOID = Request.Params["FIREINFOID"];
            ViewBag.FIREINFOID = FIREINFOID;
            return View();
        }

        /// <summary>
        /// 异步获取流动资产损失数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCURRENTASSETSList()
        {
            StringBuilder sb = new StringBuilder();
            string FIREINFOID = Request.Params["FIREINFOID"];
            string ASSETSNAME = Request.Params["ASSETSNAME"];
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>流动资产名称</th><th>损失金额(元)</th><th>数量</th><th>购入价</th><th>残值(元)</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            var list = FIRELOST_LOSTTYPE_CURRENTASSETSCls.getListModel(new FIRELOST_LOSTTYPE_CURRENTASSETS_SW { FIRELOST_FIREINFOID = FIREINFOID, CURRENTASSETSNAME = ASSETSNAME });
            int i = 1;
            foreach (var v in list)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.CURRENTASSETSNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.CURRENTASSETSCOUNT + v.CURRENTASSETSUNIT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.CURRENTASSETSPRICE + "元/" + v.CURRENTASSETSUNIT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.RESIDUALVALUE);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.FIRELOST_LOSTTYPE_CURRENTASSETSID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.FIRELOST_LOSTTYPE_CURRENTASSETSID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.FIRELOST_LOSTTYPE_CURRENTASSETSID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取单条流动资产损失数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCURRENTASSETSJson()
        {
            string CURRENTASSETSID = Request.Params["CURRENTASSETSID"];
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_CURRENTASSETSCls.getModel(new FIRELOST_LOSTTYPE_CURRENTASSETS_SW { FIRELOST_LOSTTYPE_CURRENTASSETSID = CURRENTASSETSID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查看流动资产损失
        /// </summary>
        /// <returns></returns>
        public ActionResult CURRENTASSETSDataSee()
        {
            string CURRENTASSETSID = Request.Params["CURRENTASSETSID"];
            FIRELOST_LOSTTYPE_CURRENTASSETS_Model m = FIRELOST_LOSTTYPE_CURRENTASSETSCls.getModel(new FIRELOST_LOSTTYPE_CURRENTASSETS_SW { FIRELOST_LOSTTYPE_CURRENTASSETSID = CURRENTASSETSID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"text-align:left;\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">流动资产名称:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.CURRENTASSETSNAME);
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">损失金额:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.LOSEMONEYCOUNT + "元");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">资产数量:</td>");
            sb.AppendFormat("<td>{0}</td>", m.CURRENTASSETSCOUNT + m.CURRENTASSETSUNIT);
            sb.AppendFormat("<td class=\"tdField\">购入价格:</td>");
            sb.AppendFormat("<td>{0}</td>", m.CURRENTASSETSPRICE + "元/" + m.CURRENTASSETSUNIT);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">残值:</td>");
            sb.AppendFormat("<td>{0}</td>", m.RESIDUALVALUE + "元");
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">备注:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 流动资产损失数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult CURRENTASSETSManager()
        {
            FIRELOST_LOSTTYPE_CURRENTASSETS_Model m = new FIRELOST_LOSTTYPE_CURRENTASSETS_Model();
            m.FIRELOST_LOSTTYPE_CURRENTASSETSID = Request.Params["CURRENTASSETSID"];
            m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
            m.CURRENTASSETSNAME = Request.Params["ASSETSNAME"];
            m.CURRENTASSETSUNIT = Request.Params["ASSETSUNIT"];
            m.CURRENTASSETSCOUNT = Request.Params["ASSETSCOUNT"];
            m.CURRENTASSETSPRICE = Request.Params["ASSETSPRICE"];
            m.RESIDUALVALUE = Request.Params["RESIDUALVALUE"];
            m.MARK = Request.Params["MARK"];
            m.opMethod = Request.Params["Method"];
            if (m.opMethod != "Del")
            {
                float losecount = float.Parse(m.CURRENTASSETSCOUNT) * float.Parse(m.CURRENTASSETSPRICE) - float.Parse(m.RESIDUALVALUE);
                if (losecount < 0)
                    return Content(JsonConvert.SerializeObject(new Message(false, "资产数量*购入价格-残值<0", "")), "text/html;charset=UTF-8");
                m.LOSEMONEYCOUNT = (losecount).ToString();
            }
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_CURRENTASSETSCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 非木质林产品损失管理
        /// <summary>
        /// 非木质林产品损失
        /// </summary>
        /// <returns></returns>
        public ActionResult NTFP()
        {
            string FIREINFOID = Request.Params["FIREINFOID"];
            ViewBag.FIREINFOID = FIREINFOID;
            return View();
        }

        /// <summary>
        /// 异步获取非木质林产品损失数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNTFPList()
        {
            StringBuilder sb = new StringBuilder();
            string FIREINFOID = Request.Params["FIREINFOID"];
            string NTFPNAME = Request.Params["NTFPNAME"];
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>非木质林产品名称</th><th>损失金额(元)</th><th>数量(kg)</th><th>市场平均价格(元/kg)</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            var list = FIRELOST_LOSTTYPE_NTFPCls.getListModel(new FIRELOST_LOSTTYPE_NTFP_SW { FIRELOST_FIREINFOID = FIREINFOID, NTFPNAME = NTFPNAME });
            int i = 1;
            foreach (var v in list)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.NTFPNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSECOUNT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.MARKETAVGPRICE);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.FIRELOST_LOSTTYPE_NTFPID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.FIRELOST_LOSTTYPE_NTFPID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.FIRELOST_LOSTTYPE_NTFPID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取单条非木质林产品损失数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNTFPJson()
        {
            string NTFPID = Request.Params["NTFPID"];
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_NTFPCls.getModel(new FIRELOST_LOSTTYPE_NTFP_SW { FIRELOST_LOSTTYPE_NTFPID = NTFPID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查看非木质林产品损失
        /// </summary>
        /// <returns></returns>
        public ActionResult NTFPDataSee()
        {
            string NTFPID = Request.Params["NTFPID"];
            FIRELOST_LOSTTYPE_NTFP_Model m = FIRELOST_LOSTTYPE_NTFPCls.getModel(new FIRELOST_LOSTTYPE_NTFP_SW { FIRELOST_LOSTTYPE_NTFPID = NTFPID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"text-align:left;\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" style=\"width:20%\">非木质林产品名称:</td>");
            sb.AppendFormat("<td style=\"width:30%\">{0}</td>", m.NTFPNAME);
            sb.AppendFormat("<td class=\"tdField\" style=\"width:20%\">损失金额:</td>");
            sb.AppendFormat("<td style=\"width:30%\">{0}</td>", m.LOSEMONEYCOUNT + "元");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" >损失数量:</td>");
            sb.AppendFormat("<td>{0}</td>", m.LOSECOUNT + "kg");
            sb.AppendFormat("<td class=\"tdField\">市场平均价格:</td>");
            sb.AppendFormat("<td>{0}</td>", m.MARKETAVGPRICE + "元/kg");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">备注:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 非木质林产品损失数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult NTFPManager()
        {
            FIRELOST_LOSTTYPE_NTFP_Model m = new FIRELOST_LOSTTYPE_NTFP_Model();
            m.FIRELOST_LOSTTYPE_NTFPID = Request.Params["NTFPID"];
            m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
            m.NTFPNAME = Request.Params["NTFPNAME"];
            m.LOSECOUNT = Request.Params["LOSECOUNT"];
            m.MARKETAVGPRICE = Request.Params["AVGPRICE"];
            m.MARK = Request.Params["MARK"];
            m.opMethod = Request.Params["Method"];
            if (m.opMethod != "Del")
                m.LOSEMONEYCOUNT = (float.Parse(m.LOSECOUNT) * float.Parse(m.MARKETAVGPRICE)).ToString();
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_NTFPCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 农牧产品损失管理
        /// <summary>
        /// 农牧产品损失
        /// </summary>
        /// <returns></returns>
        public ActionResult FARMPASTUREPRODUCT()
        {
            string FIREINFOID = Request.Params["FIREINFOID"];
            ViewBag.FIREINFOID = FIREINFOID;
            ViewBag.PRODUCCODE = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "505", isShowAll = "1" });
            ViewBag.PRODUCCODEAdd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "505" });
            return View();
        }

        /// <summary>
        /// 异步获取农牧产品损失数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFARMPASTUREPRODUCTList()
        {
            StringBuilder sb = new StringBuilder();
            string FIREINFOID = Request.Params["FIREINFOID"];
            string PRODUCNAME = Request.Params["PRODUCNAME"];
            string RODUCCODE = Request.Params["PRODUCCODE"];
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>农牧产品名称</th><th>损失类别</th><th>损失金额(元)</th><th>数量(面积)</th><th>市场价格(生产成本)</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            var list = FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTCls.getListModel(new FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_SW { FIRELOST_FIREINFOID = FIREINFOID, FARMPASTUREPRODUCNAME = PRODUCNAME, FARMPASTUREPRODUCCODE = RODUCCODE });
            int i = 1;
            foreach (var v in list)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.FARMPASTUREPRODUCNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.PASTUREPRODUCCODENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                string loseCount = v.LOSECOUNT, basePrice = v.BASEPRICE;
                if (v.FARMPASTUREPRODUCCODE == "1")
                {
                    loseCount += "kg";
                    basePrice += "元/kg";
                }
                if (v.FARMPASTUREPRODUCCODE == "2")
                {
                    loseCount += "hm²";
                    basePrice += "元/hm²";
                }
                if (v.FARMPASTUREPRODUCCODE == "3")
                {
                    loseCount += "头/或只";
                    basePrice += "元/头或只";
                }
                sb.AppendFormat("<td class=\"center\">{0}</td>", loseCount);
                sb.AppendFormat("<td class=\"center\">{0}</td>", basePrice);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取单条农牧产品损失数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFARMPASTUREPRODUCTJson()
        {
            string PRODUCTID = Request.Params["PRODUCTID"];
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTCls.getModel(new FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_SW { FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTID = PRODUCTID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查看农牧产品损失
        /// </summary>
        /// <returns></returns>
        public ActionResult FARMPASTUREPRODUCTDataSee()
        {
            string PRODUCTID = Request.Params["PRODUCTID"];
            FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_Model m = FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTCls.getModel(new FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_SW { FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTID = PRODUCTID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"text-align:left;\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">农牧产品名称:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.FARMPASTUREPRODUCNAME);
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">损失类别:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.PASTUREPRODUCCODENAME);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">损失金额:</td>");
            sb.AppendFormat("<td>{0}</td>", m.LOSEMONEYCOUNT + "元");
            string loseType = "";
            string loseName = "";
            string loseCount = m.LOSECOUNT;
            string basePrice = m.BASEPRICE;
            if (m.FARMPASTUREPRODUCCODE == "1")
            {
                loseType = "损失数量";
                loseName = "市场平均价格";
                loseCount += "kg";
                basePrice += "元/kg";
            }
            if (m.FARMPASTUREPRODUCCODE == "2")
            {
                loseType = "损失面积";
                loseName = "生产成本";
                loseCount += "hm²";
                basePrice += "元/hm²";
            }
            if (m.FARMPASTUREPRODUCCODE == "3")
            {
                loseType = "数量";
                loseName = "市场价格";
                loseCount += "头/或只";
                basePrice += "元/头或只";
            }
            sb.AppendFormat("<td class=\"tdField\">{0}:</td>", loseType);
            sb.AppendFormat("<td>{0}</td>", loseCount);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">{0}:</td>", loseName);
            sb.AppendFormat("<td>{0}</td>", basePrice);
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">备注:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 农牧产品损失数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult FARMPASTUREPRODUCTManager()
        {
            FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_Model m = new FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_Model();
            m.FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTID = Request.Params["PRODUCTID"];
            m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
            m.FARMPASTUREPRODUCNAME = Request.Params["PRODUCNAME"];
            m.FARMPASTUREPRODUCCODE = Request.Params["PRODUCCODE"];
            m.LOSECOUNT = Request.Params["LOSECOUNT"];
            m.BASEPRICE = Request.Params["BASEPRICE"];
            m.MARK = Request.Params["MARK"];
            m.opMethod = Request.Params["Method"];
            if (m.opMethod != "Del")
                m.LOSEMONEYCOUNT = (float.Parse(m.LOSECOUNT) * float.Parse(m.BASEPRICE)).ToString();
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 火灾扑救费用
        /// <summary>
        /// 火灾扑救费用
        /// </summary>
        /// <returns></returns>
        public ActionResult ATTACK()
        {
            string FIREINFOID = Request.Params["FIREINFOID"];
            ViewBag.FIREINFOID = FIREINFOID;
            ViewBag.ATTACKTYPE = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTTYPE_SW { DICTTYPERID = "512", isShowAll = "1" });
            ViewBag.ATTACKTYPEAdd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTTYPE_SW { DICTTYPERID = "512" });
            ViewBag.P1CodeAdd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "513" });
            return View();
        }

        /// <summary>
        /// 异步获取火灾扑救费用数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetATTACKList()
        {
            StringBuilder sb = new StringBuilder();
            string PNAME = Request.Params["NAME"];
            string ATTACKTYPE = Request.Params["ATTACKTYPE"];
            List<FIRELOST_LOSTTYPE_ATTACK_P1_Model> p1List = new List<FIRELOST_LOSTTYPE_ATTACK_P1_Model>();
            List<FIRELOST_LOSTTYPE_ATTACK_P2_Model> p2List = new List<FIRELOST_LOSTTYPE_ATTACK_P2_Model>();
            List<FIRELOST_LOSTTYPE_ATTACK_P3_Model> p3List = new List<FIRELOST_LOSTTYPE_ATTACK_P3_Model>();
            List<FIRELOST_LOSTTYPE_ATTACK_P4_Model> p4List = new List<FIRELOST_LOSTTYPE_ATTACK_P4_Model>();
            List<FIRELOST_LOSTTYPE_ATTACK_P5_Model> p5List = new List<FIRELOST_LOSTTYPE_ATTACK_P5_Model>();
            List<FIRELOST_LOSTTYPE_ATTACK_P6_Model> p6List = new List<FIRELOST_LOSTTYPE_ATTACK_P6_Model>();
            string _dic1Name = T_SYS_DICTCls.getDicTypeName(new T_SYS_DICTTYPE_SW { DICTTYPEID = "513" });
            string _dic2Name = T_SYS_DICTCls.getDicTypeName(new T_SYS_DICTTYPE_SW { DICTTYPEID = "514" });
            string _dic3Name = T_SYS_DICTCls.getDicTypeName(new T_SYS_DICTTYPE_SW { DICTTYPEID = "515" });
            string _dic4Name = T_SYS_DICTCls.getDicTypeName(new T_SYS_DICTTYPE_SW { DICTTYPEID = "516" });
            string _dic5Name = T_SYS_DICTCls.getDicTypeName(new T_SYS_DICTTYPE_SW { DICTTYPEID = "517" });
            string _dic6Name = T_SYS_DICTCls.getDicTypeName(new T_SYS_DICTTYPE_SW { DICTTYPEID = "518" });
            if (!string.IsNullOrEmpty(ATTACKTYPE))
            {
                if (ATTACKTYPE == "513")
                    p1List = FIRELOST_LOSTTYPE_ATTACK_P1Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P1_SW { P1NAME = PNAME }).ToList();
                if (ATTACKTYPE == "514")
                    p2List = FIRELOST_LOSTTYPE_ATTACK_P2Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P2_SW { P2NAME = PNAME }).ToList();
                if (ATTACKTYPE == "515")
                    p3List = FIRELOST_LOSTTYPE_ATTACK_P3Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P3_SW { P3NAME = PNAME }).ToList();
                if (ATTACKTYPE == "516")
                    p4List = FIRELOST_LOSTTYPE_ATTACK_P4Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P4_SW { P4NAME = PNAME }).ToList();
                if (ATTACKTYPE == "517")
                    p5List = FIRELOST_LOSTTYPE_ATTACK_P5Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P5_SW { P5NAME = PNAME }).ToList();
                if (ATTACKTYPE == "518")
                    p6List = FIRELOST_LOSTTYPE_ATTACK_P6Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P6_SW { P6NAME = PNAME }).ToList();
            }
            else
            {
                p1List = FIRELOST_LOSTTYPE_ATTACK_P1Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P1_SW { P1NAME = PNAME }).ToList();
                p2List = FIRELOST_LOSTTYPE_ATTACK_P2Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P2_SW { P2NAME = PNAME }).ToList();
                p3List = FIRELOST_LOSTTYPE_ATTACK_P3Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P3_SW { P3NAME = PNAME }).ToList();
                p4List = FIRELOST_LOSTTYPE_ATTACK_P4Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P4_SW { P4NAME = PNAME }).ToList();
                p5List = FIRELOST_LOSTTYPE_ATTACK_P5Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P5_SW { P5NAME = PNAME }).ToList();
                p6List = FIRELOST_LOSTTYPE_ATTACK_P6Cls.getListModel(new FIRELOST_LOSTTYPE_ATTACK_P6_SW { P6NAME = PNAME }).ToList();
            }
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>名称</th><th>损失类别</th><th>损失金额(元)</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            int i = 1;

            #region 车马船交通费
            foreach (var v in p1List)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.P1NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _dic1Name);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}','{1}','{2}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.P1ID, v.P1NAME, "513");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}','{2}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.P1ID, v.P1NAME, "513");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}','{2}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.P1ID, v.P1NAME, "513");
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            #endregion

            #region 燃料材料费
            foreach (var v in p2List)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.P2NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _dic2Name);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}','{1}','{2}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.P2ID, v.P2NAME, "514");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}','{2}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.P2ID, v.P2NAME, "514");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}','{2}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.P2ID, v.P2NAME, "514");
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            #endregion

            #region 工资伙食费
            foreach (var v in p3List)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.P3NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _dic3Name);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}','{1}','{2}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.P3ID, v.P3NAME, "515");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}','{2}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.P3ID, v.P3NAME, "515");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}','{2}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.P3ID, v.P3NAME, "515");
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            #endregion

            #region 消防器材消耗费
            foreach (var v in p4List)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.P4NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _dic4Name);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}','{1}','{2}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.P4ID, v.P4NAME, "516");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}','{2}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.P4ID, v.P4NAME, "516");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}','{2}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.P4ID, v.P4NAME, "516");
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            #endregion

            #region 组织管理费
            foreach (var v in p5List)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.P5NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _dic5Name);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}','{1}','{2}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.P5ID, v.P5NAME, "517");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}','{2}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.P5ID, v.P5NAME, "517");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}','{2}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.P5ID, v.P5NAME, "517");
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            #endregion

            #region 其他扑救费
            foreach (var v in p6List)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.P6NAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", _dic6Name);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}','{1}','{2}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.P6ID, v.P6NAME, "518");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}','{1}','{2}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.P6ID, v.P6NAME, "518");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}','{1}','{2}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.P6ID, v.P6NAME, "518");
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            #endregion

            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 异步获取火灾扑救类别页面
        /// </summary>
        /// <returns></returns>
        public ActionResult GetATTACKTYPE()
        {
            StringBuilder sb = new StringBuilder();
            string Id = Request.Params["ID"];
            string AttackType = Request.Params["AttackType"];
            string Method = Request.Params["Method"];
            string _dicStr = "";

            #region 车马船交通费
            if (AttackType == "513")
            {
                FIRELOST_LOSTTYPE_ATTACK_P1_Model p1Model = new FIRELOST_LOSTTYPE_ATTACK_P1_Model();
                if (Method == "Add")
                    _dicStr = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = AttackType });
                if (Method == "Mdy")
                {
                    p1Model = FIRELOST_LOSTTYPE_ATTACK_P1Cls.getModel(new FIRELOST_LOSTTYPE_ATTACK_P1_SW { P1ID = Id });
                    _dicStr = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = AttackType, DICTVALUE = p1Model.P1CODE });
                }
                string p1Count = "", p1Unit = "", p1Price = "", mark = "";
                p1Count = (p1Model != null && !string.IsNullOrEmpty(p1Model.P1COUNT)) ? p1Model.P1COUNT : "";
                p1Unit = (p1Model != null && !string.IsNullOrEmpty(p1Model.P1UNIT)) ? p1Model.P1UNIT : "";
                p1Price = (p1Model != null && !string.IsNullOrEmpty(p1Model.P1PRICE)) ? p1Model.P1PRICE : "";
                mark = (p1Model != null && !string.IsNullOrEmpty(p1Model.MARK)) ? p1Model.MARK : "";
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">交通工具:</td>");
                sb.AppendFormat("<td><select id=\"tbxP1CODE\" style=\"width: 95%;\" onchange=\"p1codeChange()\">{0}</select></td>", _dicStr);
                sb.AppendFormat("<td class=\"tdField\">单位:</td>");
                sb.AppendFormat("<td><input id=\"P1UNIT\" type=\"text\" value=\"" + p1Unit + "\" style=\"width:95%;\" /></td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\" id=\"td1\">飞行时间:</td>");
                sb.AppendFormat("<td>");
                sb.AppendFormat("{0}", "<input id=\"tbxP1COUNT\" type=\"text\" value=\"" + p1Count + "\" style=\"width:80%;\" />");
                sb.AppendFormat("{0}", "<span class=\"spanMark\" id=\"span1\">h</span>");
                sb.AppendFormat("</td>");
                sb.AppendFormat("<td class=\"tdField\" id=\"td2\">飞行费:</td>");
                sb.AppendFormat("<td>");
                sb.AppendFormat("{0}", "<input id=\"tbxP1PRICE\" type=\"text\" value=\"" + p1Price + "\" style=\"width:80%;\" />");
                sb.AppendFormat("{0}", "<span class=\"spanMark\" id=\"span2\">元/h</span>");
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">备注:</td>");
                sb.AppendFormat("<td colspan=\"3\"><input id=\"tbxMARK\" type=\"text\" value=\"" + mark + "\" style=\"width:96%;\" /></td>");
                sb.AppendFormat("</tr>");
            }
            #endregion

            #region 燃料材料费
            if (AttackType == "514")
            {
                FIRELOST_LOSTTYPE_ATTACK_P2_Model p2Model = new FIRELOST_LOSTTYPE_ATTACK_P2_Model();
                if (Method == "Add")
                    _dicStr = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = AttackType });
                if (Method == "Mdy")
                {
                    p2Model = FIRELOST_LOSTTYPE_ATTACK_P2Cls.getModel(new FIRELOST_LOSTTYPE_ATTACK_P2_SW { P2ID = Id });
                    _dicStr = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = AttackType, DICTVALUE = p2Model.P2CODE });
                }
                string p2Count = "", p2Unit = "", nowPrice = "", mark = "";
                p2Count = (p2Model != null && !string.IsNullOrEmpty(p2Model.P2COUNT)) ? p2Model.P2COUNT : "";
                p2Unit = (p2Model != null && !string.IsNullOrEmpty(p2Model.P2UNIT)) ? p2Model.P2UNIT : "kg";
                nowPrice = (p2Model != null && !string.IsNullOrEmpty(p2Model.NOWPRICE)) ? p2Model.NOWPRICE : "";
                mark = (p2Model != null && !string.IsNullOrEmpty(p2Model.MARK)) ? p2Model.MARK : "";
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">材料类别:</td>");
                sb.AppendFormat("<td><select id=\"tbxP2CODE\" style=\"width: 95%;\" onchange=\"p2codeChange()\">{0}</select></td>", _dicStr);
                sb.AppendFormat("<td class=\"tdField\">单位:</td>");
                sb.AppendFormat("<td><input id=\"P2UNIT\" type=\"text\" value=\"" + p2Unit + "\" style=\"width:95%;\" /></td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">消耗量:</td>");
                sb.AppendFormat("<td><input id=\"tbxP2COUNT\" type=\"text\" value=\"" + p2Count + "\" style=\"width:95%;\" /></td>");
                sb.AppendFormat("<td class=\"tdField\">现行价格:</td>");
                sb.AppendFormat("<td><input id=\"tbxNOWPRICE\" type=\"text\" value=\"" + nowPrice + "\" style=\"width:95%;\" /></td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">备注:</td>");
                sb.AppendFormat("<td colspan=\"3\"><input id=\"tbxMARK\" type=\"text\" value=\"" + mark + "\" style=\"width:96%;\" /></td>");
                sb.AppendFormat("</tr>");
            }
            #endregion

            #region 工资伙食费
            if (AttackType == "515")
            {
                FIRELOST_LOSTTYPE_ATTACK_P3_Model p3Model = new FIRELOST_LOSTTYPE_ATTACK_P3_Model();
                if (Method == "Add")
                    _dicStr = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = AttackType });
                if (Method == "Mdy")
                {
                    p3Model = FIRELOST_LOSTTYPE_ATTACK_P3Cls.getModel(new FIRELOST_LOSTTYPE_ATTACK_P3_SW { P3ID = Id });
                    _dicStr = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = AttackType, DICTVALUE = p3Model.P3CODE });
                }
                string p3Money = "", attackNumbers = "", attackDays = "", mark = "";
                p3Money = (p3Model != null && !string.IsNullOrEmpty(p3Model.P3MONEY)) ? p3Model.P3MONEY : "";
                attackNumbers = (p3Model != null && !string.IsNullOrEmpty(p3Model.ATTACKNUMBERS)) ? p3Model.ATTACKNUMBERS : "";
                attackDays = (p3Model != null && !string.IsNullOrEmpty(p3Model.ATTACKDAYS)) ? p3Model.ATTACKDAYS : "";
                mark = (p3Model != null && !string.IsNullOrEmpty(p3Model.MARK)) ? p3Model.MARK : "";
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">食费类别:</td>");
                sb.AppendFormat("<td><select id=\"tbxP3CODE\" style=\"width: 95%;\" onchange=\"p3codeChange()\">{0}</select></td>", _dicStr);
                sb.AppendFormat("<td class=\"tdField\" id=\"td1\">工资标准:</td>");
                sb.AppendFormat("<td>");
                sb.AppendFormat("{0}", "<input id=\"tbxP3MONEY\" type=\"text\" value=\"" + p3Money + "\" style=\"width:80%;\" />");
                sb.AppendFormat("{0}", "<span class=\"spanMark\">元/d</span>");
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">扑救人数:</td>");
                sb.AppendFormat("<td>");
                sb.AppendFormat("{0}", "<input id=\"tbxATTACKNUMBERS\" type=\"text\" value=\"" + attackNumbers + "\" style=\"width:80%;\" />");
                sb.AppendFormat("{0}", "<span class=\"spanMark\">人</span>");
                sb.AppendFormat("</td>");
                sb.AppendFormat("<td class=\"tdField\">扑救天数:</td>");
                sb.AppendFormat("<td>");
                sb.AppendFormat("{0}", "<input id=\"tbxATTACKDAYS\" type=\"text\" value=\"" + attackDays + "\" style=\"width:80%;\" />");
                sb.AppendFormat("{0}", "<span class=\"spanMark\">d</span>");
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">备注:</td>");
                sb.AppendFormat("<td colspan=\"3\"><input id=\"tbxMARK\" type=\"text\" value=\"" + mark + "\" style=\"width:96%;\" /></td>");
                sb.AppendFormat("</tr>");
            }
            #endregion

            #region 消防器材消耗费
            if (AttackType == "516")
            {
                FIRELOST_LOSTTYPE_ATTACK_P4_Model p4Model = new FIRELOST_LOSTTYPE_ATTACK_P4_Model();
                if (Method == "Add")
                    _dicStr = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = AttackType });
                if (Method == "Mdy")
                {
                    p4Model = FIRELOST_LOSTTYPE_ATTACK_P4Cls.getModel(new FIRELOST_LOSTTYPE_ATTACK_P4_SW { P4ID = Id });
                    _dicStr = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = AttackType, DICTVALUE = p4Model.P4CODE });
                }
                string p4Unit = "", nowPrice = "", p4Count = "", depreciationRate = "", haveUserYear = "", mark = "";
                p4Unit = (p4Model != null && !string.IsNullOrEmpty(p4Model.P4UNIT)) ? p4Model.P4UNIT : "台或件";
                nowPrice = (p4Model != null && !string.IsNullOrEmpty(p4Model.NOWPRICE)) ? p4Model.NOWPRICE : "";
                p4Count = (p4Model != null && !string.IsNullOrEmpty(p4Model.P4COUNT)) ? p4Model.P4COUNT : "";
                depreciationRate = (p4Model != null && !string.IsNullOrEmpty(p4Model.YEARAVGDEPRECIATIONRATE)) ? string.Format("{0:P}", float.Parse(p4Model.YEARAVGDEPRECIATIONRATE) / 100) : "";
                haveUserYear = (p4Model != null && !string.IsNullOrEmpty(p4Model.HAVEUSEYEAR)) ? p4Model.HAVEUSEYEAR : "";
                mark = (p4Model != null && !string.IsNullOrEmpty(p4Model.MARK)) ? p4Model.MARK : "";
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">器材类别:</td>");
                sb.AppendFormat("<td><select id=\"tbxP4CODE\" style=\"width: 95%;\" onchange=\"p4codeChange()\">{0}</select></td>", _dicStr);
                sb.AppendFormat("<td class=\"tdField\">单位:</td>");
                sb.AppendFormat("<td><input id=\"P4UNIT\" type=\"text\" value=\"" + p4Unit + "\" style=\"width:95%;\" /></td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">现行价格:</td>");
                sb.AppendFormat("<td>");
                sb.AppendFormat("{0}", "<input id=\"tbxNOWPRICE\" type=\"text\" value=\"" + nowPrice + "\" style=\"width:70%;\" />");
                sb.AppendFormat("{0}", "<span class=\"spanMark\">元/台或件</span>");
                sb.AppendFormat("</td>");
                sb.AppendFormat("<td class=\"tdField\">数量:</td>");
                sb.AppendFormat("<td><input id=\"tbxP4COUNT\" type=\"text\" value=\"" + p4Count + "\" style=\"width:95%;\" /></td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">年平均折旧率:</td>");
                sb.AppendFormat("<td>");
                sb.AppendFormat("{0}", "<input id=\"tbxDEPRECIATIONRATE\" type=\"text\" value=\"" + depreciationRate + "\" style=\"width:80%;\" />");
                sb.AppendFormat("{0}", "<span class=\"spanMark\">&nbsp;%</span>");
                sb.AppendFormat("</td>");
                sb.AppendFormat("<td class=\"tdField\">已使用年限:</td>");
                sb.AppendFormat("<td><input id=\"tbxHAVEUSEYEAR\" type=\"text\" value=\"" + haveUserYear + "\" style=\"width:95%;\" /></td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">备注:</td>");
                sb.AppendFormat("<td colspan=\"3\"><input id=\"tbxMARK\" type=\"text\" value=\"" + mark + "\" style=\"width:96%;\" /></td>");
                sb.AppendFormat("</tr>");
            }
            #endregion

            #region 组织管理费
            if (AttackType == "517")
            {
                FIRELOST_LOSTTYPE_ATTACK_P5_Model p5Model = new FIRELOST_LOSTTYPE_ATTACK_P5_Model();
                if (Method == "Add")
                    _dicStr = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = AttackType });
                if (Method == "Mdy")
                {
                    p5Model = FIRELOST_LOSTTYPE_ATTACK_P5Cls.getModel(new FIRELOST_LOSTTYPE_ATTACK_P5_SW { P5ID = Id });
                    _dicStr = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = AttackType, DICTVALUE = p5Model.P5CODE });
                }
                string p5Money = "", attackDays = "", elseMoney = "", mark = "";
                p5Money = (p5Model != null && !string.IsNullOrEmpty(p5Model.P5MONEY)) ? p5Model.P5MONEY : "";
                attackDays = (p5Model != null && !string.IsNullOrEmpty(p5Model.ATTACKDAYS)) ? p5Model.ATTACKDAYS : "";
                elseMoney = (p5Model != null && !string.IsNullOrEmpty(p5Model.ELSEMONEY)) ? p5Model.ELSEMONEY : "";
                mark = (p5Model != null && !string.IsNullOrEmpty(p5Model.MARK)) ? p5Model.MARK : "";
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">管理费类别:</td>");
                sb.AppendFormat("<td><select id=\"tbxP5CODE\" style=\"width: 95%;\" onchange=\"p5codeChange()\">{0}</select></td>", _dicStr);
                sb.AppendFormat("<td class=\"tdField\">费用:</td>");
                sb.AppendFormat("<td>");
                sb.AppendFormat("{0}", "<input id=\"tbxP5MONEY\" type=\"text\" value=\"" + p5Money + "\" style=\"width:80%;\" />");
                sb.AppendFormat("{0}", "<span class=\"spanMark\" id=\"span1\">元/d</span>");
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">救火天数:</td>");
                sb.AppendFormat("<td>");
                sb.AppendFormat("{0}", "<input id=\"tbxATTACKDAYS\" type=\"text\" value=\"" + attackDays + "\" style=\"width:80%;\" />");
                sb.AppendFormat("{0}", "<span class=\"spanMark\">d</span>");
                sb.AppendFormat("</td>");
                sb.AppendFormat("<td class=\"tdField\">其他费用:</td>");
                sb.AppendFormat("<td>");
                sb.AppendFormat("{0}", "<input id=\"tbxELSEMONEY\" type=\"text\" value=\"" + elseMoney + "\" style=\"width:80%;\" />");
                sb.AppendFormat("{0}", "<span class=\"spanMark\">元</span>");
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">备注:</td>");
                sb.AppendFormat("<td colspan=\"3\"><input id=\"tbxMARK\" type=\"text\" value=\"" + mark + "\" style=\"width:95%;\" /></td>");
                sb.AppendFormat("</tr>");
            }
            #endregion

            #region 其他扑救费
            if (AttackType == "518")
            {
                FIRELOST_LOSTTYPE_ATTACK_P6_Model p6Model = new FIRELOST_LOSTTYPE_ATTACK_P6_Model();
                if (Method == "Add")
                { }
                if (Method == "Mdy")
                    p6Model = FIRELOST_LOSTTYPE_ATTACK_P6Cls.getModel(new FIRELOST_LOSTTYPE_ATTACK_P6_SW { P6ID = Id });
                string louseMoeny = (p6Model != null && !string.IsNullOrEmpty(p6Model.LOSEMONEYCOUNT)) ? p6Model.LOSEMONEYCOUNT : "";
                string mark = (p6Model != null && !string.IsNullOrEmpty(p6Model.MARK)) ? p6Model.MARK : "";
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">损失金额:</td>");
                sb.AppendFormat("<td colspan=\"3\"><input id=\"tbxLOSEMONEY\" type=\"text\" value=\"" + louseMoeny + "\" style=\"width:95%;\" />");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<td class=\"tdField\">备注:</td>");
                sb.AppendFormat("<td colspan=\"3\"><input id=\"tbxMARK\" type=\"text\" value=\"" + mark + "\" style=\"width:96%;\" /></td>");
                sb.AppendFormat("</tr>");
            }
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), AttackType)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查看火灾扑救费用
        /// </summary>
        /// <returns></returns>
        public ActionResult ATTACKDataSee()
        {
            string PID = Request.Params["PID"];
            string ATTACKTYPE = Request.Params["ATTACKTYPE"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"text-align:left;\">");
            string _dicName = T_SYS_DICTCls.getDicTypeName(new T_SYS_DICTTYPE_SW { DICTTYPEID = ATTACKTYPE });

            #region 车马船交通费
            if (ATTACKTYPE == "513")
            {
                FIRELOST_LOSTTYPE_ATTACK_P1_Model p1Model = FIRELOST_LOSTTYPE_ATTACK_P1Cls.getModel(new FIRELOST_LOSTTYPE_ATTACK_P1_SW { P1ID = PID });
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">名称:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", p1Model.P1NAME);
                sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">费用类别:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", _dicName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">交通工具:</td>");
                sb.AppendFormat("<td>{0}</td>", p1Model.P1CODENAME);
                sb.AppendFormat("<td class=\"tdField\">单位:</td>");
                sb.AppendFormat("<td>{0}</td>", p1Model.P1UNIT);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                string countName = "";
                string priceName = "";
                if (p1Model.P1CODE == "101")
                {
                    countName = "飞行时间";
                    priceName = "飞行费";
                }
                if (p1Model.P1CODE == "102")
                {
                    countName = "船舶租用时间";
                    priceName = "租赁费";
                }
                if (p1Model.P1CODE == "103")
                {
                    countName = "行车时间";
                    priceName = "租赁费";
                }
                if (p1Model.P1CODE == "104")
                {
                    countName = "马租用时间";
                    priceName = "租赁费";
                }
                sb.AppendFormat("<td class=\"tdField\">{0}:</td>", countName);
                sb.AppendFormat("<td>{0}</td>", p1Model.P1COUNT + "h");
                sb.AppendFormat("<td class=\"tdField\">{0}:</td>", priceName);
                sb.AppendFormat("<td>{0}</td>", p1Model.P1PRICE + "元/h");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">备注:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", p1Model.MARK);
                sb.AppendFormat("</tr>");
            }
            #endregion

            #region 燃料材料费
            if (ATTACKTYPE == "514")
            {
                FIRELOST_LOSTTYPE_ATTACK_P2_Model p2Model = FIRELOST_LOSTTYPE_ATTACK_P2Cls.getModel(new FIRELOST_LOSTTYPE_ATTACK_P2_SW { P2ID = PID });
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">名称:</td>");
                sb.AppendFormat("<td  style=\"width:35%\">{0}</td>", p2Model.P2NAME);
                sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">费用类别:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", _dicName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">材料类别:</td>");
                sb.AppendFormat("<td>{0}</td>", p2Model.P2CODENAME);
                sb.AppendFormat("<td class=\"tdField\">单位:</td>");
                sb.AppendFormat("<td>{0}</td>", p2Model.P2UNIT);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">消耗量:</td>");
                sb.AppendFormat("<td>{0}</td>", p2Model.P2COUNT + p2Model.P2UNIT);
                sb.AppendFormat("<td class=\"tdField\">现行价格:</td>");
                sb.AppendFormat("<td>{0}</td>", p2Model.NOWPRICE + "元/" + p2Model.P2UNIT);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">备注:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", p2Model.MARK);
                sb.AppendFormat("</tr>");
            }
            #endregion

            #region 工资伙食费
            if (ATTACKTYPE == "515")
            {
                FIRELOST_LOSTTYPE_ATTACK_P3_Model p3Model = FIRELOST_LOSTTYPE_ATTACK_P3Cls.getModel(new FIRELOST_LOSTTYPE_ATTACK_P3_SW { P3ID = PID });
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">名称:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", p3Model.P3NAME);
                sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">费用类别:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", _dicName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">食费类别:</td>");
                sb.AppendFormat("<td>{0}</td>", p3Model.P3CODENAME);
                string typeTile = "";
                if (p3Model.P3CODE == "301")
                    typeTile = "工资标准";
                if (p3Model.P3CODE == "302")
                    typeTile = "伙食标准";
                sb.AppendFormat("<td class=\"tdField\">{0}:</td>", typeTile);
                sb.AppendFormat("<td>{0}</td>", p3Model.P3MONEY + "元/d");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">扑救人数:</td>");
                sb.AppendFormat("<td>{0}</td>", p3Model.ATTACKNUMBERS + "人");
                sb.AppendFormat("<td class=\"tdField\">扑救天数:</td>");
                sb.AppendFormat("<td>{0}</td>", p3Model.ATTACKDAYS + "d");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">备注:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", p3Model.MARK);
                sb.AppendFormat("</tr>");
            }
            #endregion

            #region 消防器材消耗费
            if (ATTACKTYPE == "516")
            {
                FIRELOST_LOSTTYPE_ATTACK_P4_Model p4Model = FIRELOST_LOSTTYPE_ATTACK_P4Cls.getModel(new FIRELOST_LOSTTYPE_ATTACK_P4_SW { P4ID = PID });
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">名称:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", p4Model.P4NAME);
                sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">费用类别:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", _dicName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">器材类别:</td>");
                sb.AppendFormat("<td>{0}</td>", p4Model.P4CODENAME);
                sb.AppendFormat("<td class=\"tdField\">单位:</td>");
                sb.AppendFormat("<td>{0}</td>", p4Model.P4UNIT);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">现行价格:</td>");
                sb.AppendFormat("<td>{0}</td>", p4Model.NOWPRICE + "元/" + p4Model.P4UNIT);
                sb.AppendFormat("<td class=\"tdField\">数量:</td>");
                sb.AppendFormat("<td>{0}</td>", p4Model.P4COUNT + p4Model.P4UNIT);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">年平均折旧率:</td>");
                sb.AppendFormat("<td>{0}</td>", string.Format("{0:P}", float.Parse(p4Model.YEARAVGDEPRECIATIONRATE) / 100));
                sb.AppendFormat("<td class=\"tdField\">已使用年限:</td>");
                sb.AppendFormat("<td>{0}</td>", p4Model.HAVEUSEYEAR + "年");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">备注:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", p4Model.MARK);
                sb.AppendFormat("</tr>");
            }
            #endregion

            #region 组织管理费
            if (ATTACKTYPE == "517")
            {
                FIRELOST_LOSTTYPE_ATTACK_P5_Model p5Model = FIRELOST_LOSTTYPE_ATTACK_P5Cls.getModel(new FIRELOST_LOSTTYPE_ATTACK_P5_SW { P5ID = PID });
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">名称:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", p5Model.P5NAME);
                sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">费用类别:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", _dicName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">管理费类别:</td>");
                sb.AppendFormat("<td>{0}</td>", p5Model.P5CODENAME);
                sb.AppendFormat("<td class=\"tdField\">费用:</td>");
                sb.AppendFormat("<td>{0}</td>", p5Model.P5MONEY + "元/d");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">救火天数:</td>");
                sb.AppendFormat("<td>{0}</td>", p5Model.ATTACKDAYS + "d");
                sb.AppendFormat("<td class=\"tdField\">其他费用:</td>");
                sb.AppendFormat("<td>{0}</td>", p5Model.ELSEMONEY + "元/h");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">备注:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", p5Model.MARK);
                sb.AppendFormat("</tr>");
            }
            #endregion

            #region 其他扑救费
            if (ATTACKTYPE == "518")
            {
                FIRELOST_LOSTTYPE_ATTACK_P6_Model p6Model = FIRELOST_LOSTTYPE_ATTACK_P6Cls.getModel(new FIRELOST_LOSTTYPE_ATTACK_P6_SW { P6ID = PID });
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">名称:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", p6Model.P6NAME);
                sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">费用类别:</td>");
                sb.AppendFormat("<td style=\"width:35%\">{0}</td>", _dicName);
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">损失金额:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", p6Model.LOSEMONEYCOUNT + "元");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">备注:</td>");
                sb.AppendFormat("<td colspan=\"3\">{0}</td>", p6Model.MARK);
                sb.AppendFormat("</tr>");
            }
            #endregion

            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 火灾扑救费用数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult ATTACKManager()
        {
            string pId = Request.Params["PID"];
            string pName = Request.Params["PNAME"];
            string attackType = Request.Params["AttackType"];
            string method = Request.Params["Method"];
            Message ms = null;

            #region 车马船交通费
            if (attackType == "513")
            {
                FIRELOST_LOSTTYPE_ATTACK_P1_Model m = new FIRELOST_LOSTTYPE_ATTACK_P1_Model();
                m.P1ID = pId;
                m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
                m.P1NAME = pName;
                m.P1CODE = Request.Params["P1CODE"];
                m.P1COUNT = Request.Params["P1COUNT"];
                m.P1UNIT = Request.Params["P1UNIT"];
                m.P1PRICE = Request.Params["P1PRICE"];
                m.MARK = Request.Params["MARK"];
                m.opMethod = Request.Params["Method"];
                if (m.opMethod != "Del")
                    m.LOSEMONEYCOUNT = (float.Parse(m.P1COUNT) * float.Parse(m.P1PRICE)).ToString();
                if (string.IsNullOrEmpty(m.opMethod) == true)
                    m.opMethod = "Add";
                ms = FIRELOST_LOSTTYPE_ATTACK_P1Cls.Manager(m);
            }
            #endregion

            #region 燃料材料费
            if (attackType == "514")
            {
                FIRELOST_LOSTTYPE_ATTACK_P2_Model m = new FIRELOST_LOSTTYPE_ATTACK_P2_Model();
                m.P2ID = pId;
                m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
                m.P2NAME = pName;
                m.P2CODE = Request.Params["P2CODE"];
                m.P2COUNT = Request.Params["P2COUNT"];
                m.P2UNIT = Request.Params["P2UNIT"];
                m.NOWPRICE = Request.Params["NOWPRICE"];
                m.MARK = Request.Params["MARK"];
                m.opMethod = Request.Params["Method"];
                if (m.opMethod != "Del")
                    m.LOSEMONEYCOUNT = (float.Parse(m.P2COUNT) * float.Parse(m.NOWPRICE)).ToString();
                if (string.IsNullOrEmpty(m.opMethod) == true)
                    m.opMethod = "Add";
                ms = FIRELOST_LOSTTYPE_ATTACK_P2Cls.Manager(m);
            }
            #endregion

            #region 工资伙食费
            if (attackType == "515")
            {
                FIRELOST_LOSTTYPE_ATTACK_P3_Model m = new FIRELOST_LOSTTYPE_ATTACK_P3_Model();
                m.P3ID = pId;
                m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
                m.P3NAME = pName;
                m.P3CODE = Request.Params["P3CODE"];
                m.P3MONEY = Request.Params["P3MONEY"];
                m.ATTACKNUMBERS = Request.Params["ATTACKNUMBERS"];
                m.ATTACKDAYS = Request.Params["ATTACKDAYS"];
                m.MARK = Request.Params["MARK"];
                m.opMethod = Request.Params["Method"];
                if (m.opMethod != "Del")
                    m.LOSEMONEYCOUNT = (float.Parse(m.ATTACKNUMBERS) * float.Parse(m.P3MONEY) * float.Parse(m.ATTACKDAYS)).ToString();
                if (string.IsNullOrEmpty(m.opMethod) == true)
                    m.opMethod = "Add";
                ms = FIRELOST_LOSTTYPE_ATTACK_P3Cls.Manager(m);
            }
            #endregion

            #region 消防器材消耗费
            if (attackType == "516")
            {
                FIRELOST_LOSTTYPE_ATTACK_P4_Model m = new FIRELOST_LOSTTYPE_ATTACK_P4_Model();
                m.P4ID = pId;
                m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
                m.P4NAME = pName;
                m.P4CODE = Request.Params["P4CODE"];
                m.NOWPRICE = Request.Params["NOWPRICE"];
                m.P4COUNT = Request.Params["P4COUNT"];
                m.P4UNIT = Request.Params["P4UNIT"];
                m.YEARAVGDEPRECIATIONRATE = Request.Params["DEPRECIATIONRAT"];
                m.HAVEUSEYEAR = Request.Params["HAVEUSEYEAR"];
                m.MARK = Request.Params["MARK"];
                m.opMethod = Request.Params["Method"];
                if (m.opMethod != "Del")
                {
                    float temp = 1 - (float.Parse(m.YEARAVGDEPRECIATIONRATE) / 100) * float.Parse(m.HAVEUSEYEAR);
                    if (temp < 0)
                        return Content(JsonConvert.SerializeObject(new Message(false, "1-年平均折旧率*已使用年限<0", "")), "text/html;charset=UTF-8");
                    m.LOSEMONEYCOUNT = (float.Parse(m.P4COUNT) * float.Parse(m.NOWPRICE) * temp).ToString();
                }
                if (string.IsNullOrEmpty(m.opMethod) == true)
                    m.opMethod = "Add";
                ms = FIRELOST_LOSTTYPE_ATTACK_P4Cls.Manager(m);
            }
            #endregion

            #region 组织管理费
            if (attackType == "517")
            {
                FIRELOST_LOSTTYPE_ATTACK_P5_Model m = new FIRELOST_LOSTTYPE_ATTACK_P5_Model();
                m.P5ID = pId;
                m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
                m.P5NAME = pName;
                m.P5CODE = Request.Params["P5CODE"];
                m.P5MONEY = Request.Params["P5MONEY"];
                m.ATTACKDAYS = Request.Params["ATTACKDAYS"];
                m.ELSEMONEY = Request.Params["ELSEMONEY"];
                m.MARK = Request.Params["MARK"];
                m.opMethod = Request.Params["Method"];
                if (m.opMethod != "Del")
                    m.LOSEMONEYCOUNT = (float.Parse(m.P5MONEY) * float.Parse(m.ATTACKDAYS) + float.Parse(m.ELSEMONEY)).ToString();
                if (string.IsNullOrEmpty(m.opMethod) == true)
                    m.opMethod = "Add";
                ms = FIRELOST_LOSTTYPE_ATTACK_P5Cls.Manager(m);
            }
            #endregion

            #region 其他扑救费
            if (attackType == "518")
            {
                FIRELOST_LOSTTYPE_ATTACK_P6_Model m = new FIRELOST_LOSTTYPE_ATTACK_P6_Model();
                m.P6ID = pId;
                m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
                m.P6NAME = pName;
                m.LOSEMONEYCOUNT = Request.Params["LOSEMONEYCOUNT"];
                m.MARK = Request.Params["MARK"];
                m.opMethod = Request.Params["Method"];
                if (string.IsNullOrEmpty(m.opMethod) == true)
                    m.opMethod = "Add";
                ms = FIRELOST_LOSTTYPE_ATTACK_P6Cls.Manager(m);
            }
            #endregion

            return Content(JsonConvert.SerializeObject(ms), "text/html;charset=UTF-8");
        }
        #endregion

        #region 人员伤亡损失管理
        /// <summary>
        /// 人员伤亡损失
        /// </summary>
        /// <returns></returns>
        public ActionResult CASUALTY()
        {
            string FIREINFOID = Request.Params["FIREINFOID"];
            ViewBag.FIREINFOID = FIREINFOID;
            ViewBag.CODE = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTTYPE_SW { DICTTYPERID = "506", isShowAll = "1" });
            ViewBag.CODEAdd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTTYPE_SW { DICTTYPERID = "506" });
            return View();
        }

        /// <summary>
        /// 异步获取人员伤亡损失数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCASUALTYList()
        {
            StringBuilder sb = new StringBuilder();
            string FIREINFOID = Request.Params["FIREINFOID"];
            string NAME = Request.Params["NAME"];
            string CODE = Request.Params["CODE"];
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>伤亡名称</th><th>伤亡类别</th><th>伤亡人数</th><th>损失金额(元)</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            var list = FIRELOST_LOSTTYPE_CASUALTYCls.getListModel(new FIRELOST_LOSTTYPE_CASUALTY_SW { FIRELOST_FIREINFOID = FIREINFOID, CASUALTYNAME = NAME, CASUALTYCODE = CODE });
            int i = 1;
            foreach (var v in list)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.CASUALTYNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.CASUALTYCODENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.CASUALTYNUMBERS);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.FIRELOST_LOSTTYPE_CASUALTYID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.FIRELOST_LOSTTYPE_CASUALTYID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.FIRELOST_LOSTTYPE_CASUALTYID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 异步获取人员伤亡明细损失数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCASUALTYDETAILData()
        {
            StringBuilder sb = new StringBuilder();
            string casulId = Request.Params["CASUALTYID"];
            string casulCode = Request.Params["CASUALTYCODE"];
            string method = Request.Params["Method"];
            List<FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model> detailList = new List<FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model>();
            if (method == "Add")
            { }
            if (method == "Mdy")
            {
                detailList = FIRELOST_LOSTTYPE_CASUALTYDETAILCls.getListModel(new FIRELOST_LOSTTYPE_CASUALTYDETAIL_SW { FIRELOST_LOSTTYPE_CASUALTYID = casulId }).ToList();
            }
            List<T_SYS_DICTModel> dicList = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = casulCode }).ToList();
            for (int i = 0; i < dicList.Count / 2; i++)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">{0}:</td>", dicList[i * 2].DICTNAME);
                sb.AppendFormat("<td>");
                FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model m1 = detailList.Where(a => a.CASUALTYDETAILCODE == dicList[i * 2].DICTVALUE).FirstOrDefault();
                string money1 = (m1 != null && !string.IsNullOrEmpty(m1.CASUALTYDETAIMONEY)) ? m1.CASUALTYDETAIMONEY : "";
                sb.AppendFormat("{0}", "<input id=\"tbx" + dicList[i * 2].DICTVALUE + "\" type=\"text\" value=\"" + money1 + "\" style=\"width:85%;\" />");
                sb.AppendFormat("{0}", "<span class=\"spanMark\">&nbsp;元</span>");
                sb.AppendFormat("</td>");
                sb.AppendFormat("<td class=\"tdField\">{0}:</td>", dicList[i * 2 + 1].DICTNAME);
                sb.AppendFormat("<td>");
                FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model m2 = detailList.Where(a => a.CASUALTYDETAILCODE == dicList[i * 2 + 1].DICTVALUE).FirstOrDefault();
                string money2 = (m2 != null && !string.IsNullOrEmpty(m2.CASUALTYDETAIMONEY)) ? m2.CASUALTYDETAIMONEY : "";
                sb.AppendFormat("{0}", "<input id=\"tbx" + dicList[i * 2 + 1].DICTVALUE + "\" type=\"text\" value=\"" + money2 + "\" style=\"width:85%;\" />");
                sb.AppendFormat("{0}", "<span class=\"spanMark\">&nbsp;元</span>");
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
            }
            if (dicList.Count % 2 > 0)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">{0}:</td>", dicList[dicList.Count - 1].DICTNAME);
                sb.AppendFormat("<td>");
                FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model m3 = detailList.Where(a => a.CASUALTYDETAILCODE == dicList[dicList.Count - 1].DICTVALUE).FirstOrDefault();
                string money3 = (m3 != null && !string.IsNullOrEmpty(m3.CASUALTYDETAIMONEY)) ? m3.CASUALTYDETAIMONEY : "";
                sb.AppendFormat("{0}", "<input  id=\"tbx" + dicList[dicList.Count - 1].DICTVALUE + "\" type=\"text\" value=\"" + money3 + "\" style=\"width:85%;\" />");
                sb.AppendFormat("{0}", "<span class=\"spanMark\">&nbsp;元</span>");
                sb.AppendFormat("</td>");
                sb.AppendFormat("<td colspan=\"2\">");
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
            }
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), T_SYS_DICTCls.getDicValueStr(dicList))), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取单条人员伤亡损失数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCASUALTYJson()
        {
            string CASUALTYID = Request.Params["CASUALTYID"];
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_CASUALTYCls.getModel(new FIRELOST_LOSTTYPE_CASUALTY_SW { FIRELOST_LOSTTYPE_CASUALTYID = CASUALTYID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查看人员伤亡损失
        /// </summary>
        /// <returns></returns>
        public ActionResult CASUALTYDataSee()
        {
            string CASUALTYID = Request.Params["CASUALTYID"];
            FIRELOST_LOSTTYPE_CASUALTY_Model m = FIRELOST_LOSTTYPE_CASUALTYCls.getModel(new FIRELOST_LOSTTYPE_CASUALTY_SW { FIRELOST_LOSTTYPE_CASUALTYID = CASUALTYID });
            var detailList = FIRELOST_LOSTTYPE_CASUALTYDETAILCls.getListModel(new FIRELOST_LOSTTYPE_CASUALTYDETAIL_SW { FIRELOST_LOSTTYPE_CASUALTYID = CASUALTYID });
            List<T_SYS_DICTModel> dicList = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = m.CASUALTYCODE }).ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"text-align:left;\">");

            #region 人员伤亡基本信息
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" style=\"width:20%\">伤亡名称:</td>");
            sb.AppendFormat("<td style=\"width:30%\">{0}</td>", m.CASUALTYNAME);
            sb.AppendFormat("<td class=\"tdField\" style=\"width:20%\">伤亡类别:</td>");
            sb.AppendFormat("<td style=\"width:30%\">{0}</td>", m.CASUALTYCODENAME);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">伤亡人数:</td>");
            sb.AppendFormat("<td>{0}</td>", m.CASUALTYNUMBERS);
            sb.AppendFormat("<td class=\"tdField\">损失金额:</td>");
            sb.AppendFormat("<td>{0}</td>", m.LOSEMONEYCOUNT + "元");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">备注:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
            sb.AppendFormat("</tr>");
            #endregion

            #region 人员伤亡明细
            for (int i = 0; i < dicList.Count / 2; i++)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">{0}:</td>", dicList[i * 2].DICTNAME);
                FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model m1 = detailList.Where(a => a.CASUALTYDETAILCODE == dicList[i * 2].DICTVALUE).FirstOrDefault();
                sb.AppendFormat("<td>{0}</td>", (m1 != null && !string.IsNullOrEmpty(m1.CASUALTYDETAIMONEY)) ? m1.CASUALTYDETAIMONEY + "元" : "");
                sb.AppendFormat("<td class=\"tdField\">{0}:</td>", dicList[i * 2 + 1].DICTNAME);
                FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model m2 = detailList.Where(a => a.CASUALTYDETAILCODE == dicList[i * 2 + 1].DICTVALUE).FirstOrDefault();
                sb.AppendFormat("<td>{0}</td>", (m2 != null && !string.IsNullOrEmpty(m2.CASUALTYDETAIMONEY)) ? m2.CASUALTYDETAIMONEY + "元" : "");
                sb.AppendFormat("</tr>");
            }
            if (dicList.Count % 2 > 0)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdField\">{0}:</td>", dicList[dicList.Count - 1].DICTNAME);
                FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model m3 = detailList.Where(a => a.CASUALTYDETAILCODE == dicList[dicList.Count - 1].DICTVALUE).FirstOrDefault();
                sb.AppendFormat("<td>{0}</td>", (m3 != null && !string.IsNullOrEmpty(m3.CASUALTYDETAIMONEY)) ? m3.CASUALTYDETAIMONEY + "元" : "");
                sb.AppendFormat("<td colspan=\"2\">");
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
            }
            #endregion

            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 人员伤亡损失数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult CASUALTYManager()
        {
            FIRELOST_LOSTTYPE_CASUALTY_Model m = new FIRELOST_LOSTTYPE_CASUALTY_Model();
            m.FIRELOST_LOSTTYPE_CASUALTYID = Request.Params["CASUALTYID"];
            m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
            m.CASUALTYNAME = Request.Params["NAME"];
            m.CASUALTYCODE = Request.Params["CODE"];
            m.CASUALTYNUMBERS = Request.Params["NUMBERS"];
            m.MARK = Request.Params["MARK"];
            m.opMethod = Request.Params["Method"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            if (m.opMethod != "Del")
            {
                string[] detailMoney = Request.Params["DETAIMONEY"].Split(',');
                m.LOSEMONEYCOUNT = (detailMoney.Select(p => float.Parse(p)).Sum() * float.Parse(m.CASUALTYNUMBERS)).ToString();
            }
            var ms = FIRELOST_LOSTTYPE_CASUALTYCls.Manager(m);
            if (ms.Success == false)
                return Content(JsonConvert.SerializeObject(new Message(false, ms.Msg, "")), "text/html;charset=UTF-8");
            FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model m2 = new FIRELOST_LOSTTYPE_CASUALTYDETAIL_Model();
            if (m.opMethod != "Del")
                m2.FIRELOST_LOSTTYPE_CASUALTYID = ms.Url;
            else
                m2.FIRELOST_LOSTTYPE_CASUALTYID = m.FIRELOST_LOSTTYPE_CASUALTYID;
            m2.CASUALTYDETAILCODE = Request.Params["DETAILCODE"];
            m2.CASUALTYDETAIMONEY = Request.Params["DETAIMONEY"];
            m2.opMethod = m.opMethod;
            FIRELOST_LOSTTYPE_CASUALTYDETAILCls.Manager(m2);
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_CASUALTYCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 居民财产损失管理
        /// <summary>
        /// 居民财产损失
        /// </summary>
        /// <returns></returns>
        public ActionResult RESIDENTPROPERTY()
        {
            string FIREINFOID = Request.Params["FIREINFOID"];
            ViewBag.FIREINFOID = FIREINFOID;
            return View();
        }

        /// <summary>
        /// 异步获取居民财产损失数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRESIDENTPROPERTYList()
        {
            StringBuilder sb = new StringBuilder();
            string FIREINFOID = Request.Params["FIREINFOID"];
            string ROPERTYNAME = Request.Params["ROPERTYNAME"];
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>财产名称</th><th>损失金额(元)</th><th>数量</th><th>购入价</th><th>年平均折旧率</th><th>已使用年限</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            var list = FIRELOST_LOSTTYPE_RESIDENTPROPERTYCls.getListModel(new FIRELOST_LOSTTYPE_RESIDENTPROPERTY_SW { FIRELOST_FIREINFOID = FIREINFOID, RESIDENTPROPERTYNAME = ROPERTYNAME });
            int i = 1;
            foreach (var v in list)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.RESIDENTPROPERTYNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.RESIDENTPROPERTYCOUNT + v.RESIDENTPROPERTYUNIT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.RESIDENTPROPERTYPRICE + "元/" + v.RESIDENTPROPERTYUNIT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", string.Format("{0:P}", float.Parse(v.YEARAVGDEPRECIATIONRATE) / 100));
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.HAVEUSEYEAR);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.RESIDENTPROPERTYID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.RESIDENTPROPERTYID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.RESIDENTPROPERTYID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取单条居民财产损失数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRESIDENTPROPERTYJson()
        {
            string ROPERTYID = Request.Params["ROPERTYID"];
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_RESIDENTPROPERTYCls.getModel(new FIRELOST_LOSTTYPE_RESIDENTPROPERTY_SW { RESIDENTPROPERTYID = ROPERTYID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查看居民财产损失
        /// </summary>
        /// <returns></returns>
        public ActionResult RESIDENTPROPERTYDataSee()
        {
            string ROPERTYID = Request.Params["ROPERTYID"];
            FIRELOST_LOSTTYPE_RESIDENTPROPERTY_Model m = FIRELOST_LOSTTYPE_RESIDENTPROPERTYCls.getModel(new FIRELOST_LOSTTYPE_RESIDENTPROPERTY_SW { RESIDENTPROPERTYID = ROPERTYID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"text-align:left;\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">财产名称:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.RESIDENTPROPERTYNAME);
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">损失金额:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.LOSEMONEYCOUNT + "元");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">损失数量:</td>");
            sb.AppendFormat("<td>{0}</td>", m.LOSEMONEYCOUNT + m.RESIDENTPROPERTYUNIT);
            sb.AppendFormat("<td class=\"tdField\">购入价(造价):</td>");
            sb.AppendFormat("<td>{0}</td>", m.RESIDENTPROPERTYPRICE + "元/" + m.RESIDENTPROPERTYUNIT);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">年平均折旧率:</td>");
            sb.AppendFormat("<td>{0}</td>", string.Format("{0:P}", float.Parse(m.YEARAVGDEPRECIATIONRATE) / 100));
            sb.AppendFormat("<td class=\"tdField\">已使用年限:</td>");
            sb.AppendFormat("<td>{0}</td>", m.HAVEUSEYEAR + "年");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">备注:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 居民财产损失数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult RESIDENTPROPERTYManager()
        {
            FIRELOST_LOSTTYPE_RESIDENTPROPERTY_Model m = new FIRELOST_LOSTTYPE_RESIDENTPROPERTY_Model();
            m.RESIDENTPROPERTYID = Request.Params["PROPERTYID"];
            m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
            m.RESIDENTPROPERTYNAME = Request.Params["ROPERTYNAME"];
            m.RESIDENTPROPERTYCOUNT = Request.Params["PROPERTYCOUNT"];
            m.RESIDENTPROPERTYUNIT = Request.Params["PROPERTYUNIT"];
            m.RESIDENTPROPERTYPRICE = Request.Params["ROPERTYPRICE"];
            m.YEARAVGDEPRECIATIONRATE = Request.Params["PRECIATIONRATE"];
            m.HAVEUSEYEAR = Request.Params["HAVEUSEYEAR"];
            m.MARK = Request.Params["MARK"];
            m.opMethod = Request.Params["Method"];
            if (m.opMethod != "Del")
            {
                float temp = 1 - (float.Parse(m.YEARAVGDEPRECIATIONRATE) / 100) * float.Parse(m.HAVEUSEYEAR);
                if (temp < 0)
                    return Content(JsonConvert.SerializeObject(new Message(false, "1-年平均折旧率*已使用年限<0", "")), "text/html;charset=UTF-8");
                m.LOSEMONEYCOUNT = (float.Parse(m.RESIDENTPROPERTYCOUNT) * float.Parse(m.RESIDENTPROPERTYPRICE) * temp).ToString();
            }
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_RESIDENTPROPERTYCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 野生动物损失管理
        /// <summary>
        /// 野生动物损失
        /// </summary>
        /// <returns></returns>
        public ActionResult WILDANIMAL()
        {
            string FIREINFOID = Request.Params["FIREINFOID"];
            ViewBag.FIREINFOID = FIREINFOID;
            return View();
        }

        /// <summary>
        /// 异步获取野生动物损失数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWILDANIMALList()
        {
            StringBuilder sb = new StringBuilder();
            string FIREINFOID = Request.Params["FIREINFOID"];
            string DANIMALNAME = Request.Params["DANIMALNAME"];
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>野生动物名称</th><th>损失金额(元)</th><th>烧死数量(头或只)</th><th>价格(元/头或只)</th><th>残值(元)</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            var list = FIRELOST_LOSTTYPE_WILDANIMALCls.getListModel(new FIRELOST_LOSTTYPE_WILDANIMAL_SW { FIRELOST_FIREINFOID = FIREINFOID, WILDANIMALNAME = DANIMALNAME });
            int i = 1;
            foreach (var v in list)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.WILDANIMALNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.WILDANIMALCOUNT);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.WILDANIMALPRICE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.RESIDUALVALUE);
                sb.AppendFormat("<td class\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.WILDANIMALID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.WILDANIMALID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.WILDANIMALID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取单条野生动物损失数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWILDANIMALJson()
        {
            string DANIMALID = Request.Params["DANIMALID"];
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_WILDANIMALCls.getModel(new FIRELOST_LOSTTYPE_WILDANIMAL_SW { WILDANIMALID = DANIMALID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查看野生动物损失
        /// </summary>
        /// <returns></returns>
        public ActionResult WILDANIMALDataSee()
        {
            string DANIMALID = Request.Params["DANIMALID"];
            FIRELOST_LOSTTYPE_WILDANIMAL_Model m = FIRELOST_LOSTTYPE_WILDANIMALCls.getModel(new FIRELOST_LOSTTYPE_WILDANIMAL_SW { WILDANIMALID = DANIMALID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"text-align:left;\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" style=\"width:20%\">野生动物名称:</td>");
            sb.AppendFormat("<td style=\"width:30%\">{0}</td>", m.WILDANIMALNAME);
            sb.AppendFormat("<td class=\"tdField\" style=\"width:20%\">损失金额:</td>");
            sb.AppendFormat("<td style=\"width:30%\">{0}</td>", m.LOSEMONEYCOUNT + "元");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">烧死数量(头或只):</td>");
            sb.AppendFormat("<td>{0}</td>", m.WILDANIMALCOUNT);
            sb.AppendFormat("<td class=\"tdField\">价格(元/头或只):</td>");
            sb.AppendFormat("<td>{0}</td>", m.WILDANIMALPRICE);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">残值:</td>");
            sb.AppendFormat("<td>{0}</td>", m.RESIDUALVALUE + "元");
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">备注:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 野生动物损失数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult WILDANIMALManager()
        {
            FIRELOST_LOSTTYPE_WILDANIMAL_Model m = new FIRELOST_LOSTTYPE_WILDANIMAL_Model();
            m.WILDANIMALID = Request.Params["DANIMALID"];
            m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
            m.WILDANIMALNAME = Request.Params["DANIMALNAME"];
            m.WILDANIMALCOUNT = Request.Params["DANIMALCOUNT"];
            m.WILDANIMALPRICE = Request.Params["DANIMALPRICE"];
            m.RESIDUALVALUE = Request.Params["RESIDUALVALUE"];
            m.MARK = Request.Params["MARK"];
            m.opMethod = Request.Params["Method"];
            if (m.opMethod != "Del")
            {
                float temp = float.Parse(m.WILDANIMALCOUNT) * float.Parse(m.WILDANIMALPRICE) - float.Parse(m.RESIDUALVALUE);
                if (temp < 0)
                    return Content(JsonConvert.SerializeObject(new Message(false, "烧死数量*价格-残值<0", "")), "text/html;charset=UTF-8");
                m.LOSEMONEYCOUNT = temp.ToString();
            }
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_WILDANIMALCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 停(减)产损失管理
        /// <summary>
        /// 停(减)产损失
        /// </summary>
        /// <returns></returns>
        public ActionResult STOPREDUCTION()
        {
            string FIREINFOID = Request.Params["FIREINFOID"];
            ViewBag.FIREINFOID = FIREINFOID;
            ViewBag.STOPCODE = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "510", isShowAll = "1" });
            ViewBag.STOPCODEAdd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "510" });
            return View();
        }

        /// <summary>
        /// 异步获取停(减)产损失列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSTOPREDUCTIONList()
        {
            StringBuilder sb = new StringBuilder();
            string FIREINFOID = Request.Params["FIREINFOID"];
            string STOPNNAME = Request.Params["STOPNNAME"];
            string STOPNCODE = Request.Params["STOPNCODE"];
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>停(减)产名称</th><th>停(减)产类别</th><th>损失金额(元)</th><th>数量</th><th>时间</th><th>价格</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            var list = FIRELOST_LOSTTYPE_STOPREDUCTIONCls.getListModel(new FIRELOST_LOSTTYPE_STOPREDUCTION_SW { FIRELOST_FIREINFOID = FIREINFOID, STOPREDUCTIONNAME = STOPNNAME, STOPREDUCTIONCODE = STOPNCODE });
            int i = 1;
            foreach (var v in list)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.STOPREDUCTIONNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.STOPREDUCTIONCODENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                string stopCount = v.STOPREDUCTIONCOUNT, stopTime = v.STOPREDUCTIONTIME, stopPrice = v.STOPREDUCTIONPRICE;
                if (v.STOPREDUCTIONCODE == "1")
                {
                    stopCount += "人";
                    stopTime += "d";
                    stopPrice += "元/人/d";
                }
                if (v.STOPREDUCTIONCODE == "2")
                {
                    stopCount += "件/d";
                    stopTime += "d";
                    stopPrice += "元/件";
                }
                if (v.STOPREDUCTIONCODE == "3")
                {
                    stopCount = "—";
                    stopTime += "d";
                    stopPrice += "元/d";
                }
                sb.AppendFormat("<td class\"center\">{0}</td>", stopCount);
                sb.AppendFormat("<td class\"center\">{0}</td>", stopTime);
                sb.AppendFormat("<td class\"center\">{0}</td>", stopPrice);
                sb.AppendFormat("<td class\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.STOPREDUCTIONID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.STOPREDUCTIONID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.STOPREDUCTIONID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取单条停(减)产损失数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSTOPREDUCTIONJson()
        {
            string STOPID = Request.Params["STOPID"];
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_STOPREDUCTIONCls.getModel(new FIRELOST_LOSTTYPE_STOPREDUCTION_SW { STOPREDUCTIONID = STOPID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查看停(减)产损失
        /// </summary>
        /// <returns></returns>
        public ActionResult STOPREDUCTIONDataSee()
        {
            string STOPID = Request.Params["STOPID"];
            FIRELOST_LOSTTYPE_STOPREDUCTION_Model m = FIRELOST_LOSTTYPE_STOPREDUCTIONCls.getModel(new FIRELOST_LOSTTYPE_STOPREDUCTION_SW { STOPREDUCTIONID = STOPID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"text-align:left;\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">停(减)产名称:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.STOPREDUCTIONNAME);
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">停(减)产类别:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.STOPREDUCTIONCODENAME);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">损失金额:</td>");
            sb.AppendFormat("<td>{0}</td>", m.LOSEMONEYCOUNT + "元");
            string countTilte = "", timeTitle = "", priceTitle = "";
            string stopCount = m.STOPREDUCTIONCOUNT, stopTime = m.STOPREDUCTIONTIME, stopPrice = m.STOPREDUCTIONPRICE;
            if (m.STOPREDUCTIONCODE == "1")
            {
                countTilte = "停工人数";
                timeTitle = "停工天数";
                priceTitle = "日均工资总额";
                stopCount += "人";
                stopTime += "d";
                stopPrice += "元/人/d";
            }
            if (m.STOPREDUCTIONCODE == "2")
            {
                countTilte = "产品数量";
                timeTitle = "停(减)产时间";
                priceTitle = "产品出厂价";
                stopCount += "件/d";
                stopTime += "d";
                stopPrice += "元/件";
            }
            if (m.STOPREDUCTIONCODE == "3")
            {
                countTilte = "停业数量";
                timeTitle = "停业天数";
                priceTitle = "日营业额";
                stopCount = "—";
                stopTime += "d";
                stopPrice += "元/d";
            }
            sb.AppendFormat("<td class=\"tdField\">{0}:</td>", countTilte);
            sb.AppendFormat("<td>{0}</td>", stopCount);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">{0}:</td>", timeTitle);
            sb.AppendFormat("<td>{0}</td>", stopTime);
            sb.AppendFormat("<td class=\"tdField\">{0}:</td>", priceTitle);
            sb.AppendFormat("<td>{0}</td>", stopPrice);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">备注:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 停(减)产损失数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult STOPREDUCTIONManager()
        {
            FIRELOST_LOSTTYPE_STOPREDUCTION_Model m = new FIRELOST_LOSTTYPE_STOPREDUCTION_Model();
            m.STOPREDUCTIONID = Request.Params["STOPID"];
            m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
            m.STOPREDUCTIONNAME = Request.Params["STOPNAME"];
            m.STOPREDUCTIONCODE = Request.Params["STOPCODE"];
            m.LOSEMONEYCOUNT = Request.Params["MONEYCOUNT"];
            m.STOPREDUCTIONCOUNT = Request.Params["STOPCOUNT"];
            m.STOPREDUCTIONTIME = Request.Params["STOPTIME"];
            m.STOPREDUCTIONPRICE = Request.Params["STOPPRICE"];
            m.MARK = Request.Params["MARK"];
            m.opMethod = Request.Params["Method"];
            if (m.opMethod != "Del")
            {
                if (m.STOPREDUCTIONCODE == "1" || m.STOPREDUCTIONCODE == "2")
                    m.LOSEMONEYCOUNT = (float.Parse(m.STOPREDUCTIONCOUNT) * float.Parse(m.STOPREDUCTIONTIME) * float.Parse(m.STOPREDUCTIONPRICE)).ToString();
                if (m.STOPREDUCTIONCODE == "3")
                    m.LOSEMONEYCOUNT = (float.Parse(m.STOPREDUCTIONTIME) * float.Parse(m.STOPREDUCTIONPRICE)).ToString();
            }
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_STOPREDUCTIONCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 灾后处理费用管理
        /// <summary>
        /// 灾后处理费用
        /// </summary>
        /// <returns></returns>
        public ActionResult LOSTPROCESS()
        {
            string FIREINFOID = Request.Params["FIREINFOID"];
            ViewBag.FIREINFOID = FIREINFOID;
            ViewBag.PROCESSCODE = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "511", isShowAll = "1" });
            ViewBag.PROCESSCODEAdd = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW { DICTTYPEID = "511" });
            return View();
        }

        /// <summary>
        /// 异步获取灾后处理费用数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLOSTPROCESSList()
        {
            StringBuilder sb = new StringBuilder();
            string FIREINFOID = Request.Params["FIREINFOID"];
            string PROCESSNAME = Request.Params["PROCESSNAME"];
            string PROCESSCODE = Request.Params["PROCESSCODE"];
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr><th>序号</th><th>灾后处理名称</th><th>灾后处理类别</th><th>损失金额(元)</th><th>操作</th></tr>");
            sb.AppendFormat("</thead>");
            var list = FIRELOST_LOSTTYPE_LOSTPROCESSCls.getListModel(new FIRELOST_LOSTTYPE_LOSTPROCESS_SW { FIRELOST_FIREINFOID = FIREINFOID, LOSTPROCESSNAME = PROCESSNAME, LOSTPROCESSCODE = PROCESSCODE });
            int i = 1;
            foreach (var v in list)
            {
                sb.AppendFormat("<tr class=\"{0}\" onclick=\"setColor(this)\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", i.ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSTPROCESSNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSTPROCESSCODENAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LOSEMONEYCOUNT);
                sb.AppendFormat("<td class=\"center\">");
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('See','{0}')\" title='查看' class=\"searchBox_01 LinkSee\">查看</a>", v.LOSTPROCESSID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Mdy','{0}')\" title='编辑' class=\"searchBox_01 LinkMdy\">编辑</a>", v.LOSTPROCESSID);
                sb.AppendFormat("<a href=\"#\" onclick=\"Manager('Del','{0}')\" title='删除' class=\"searchBox_01 LinkDel\">删除</a>", v.LOSTPROCESSID);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</table>");
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取单条灾后处理费用数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLOSTPROCESSJson()
        {
            string PROCESSID = Request.Params["PROCESSID"];
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_LOSTPROCESSCls.getModel(new FIRELOST_LOSTTYPE_LOSTPROCESS_SW { LOSTPROCESSID = PROCESSID })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 查看灾后处理费用
        /// </summary>
        /// <returns></returns>
        public ActionResult LOSTPROCESSDataSee()
        {
            string PROCESSID = Request.Params["PROCESSID"];
            FIRELOST_LOSTTYPE_LOSTPROCESS_Model m = FIRELOST_LOSTTYPE_LOSTPROCESSCls.getModel(new FIRELOST_LOSTTYPE_LOSTPROCESS_SW { LOSTPROCESSID = PROCESSID });
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"text-align:left;\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">灾后处理名称:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.LOSTPROCESSNAME);
            sb.AppendFormat("<td class=\"tdField\" style=\"width:15%\">灾后处理类别:</td>");
            sb.AppendFormat("<td style=\"width:35%\">{0}</td>", m.LOSTPROCESSCODENAME);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">损失金额:</td>");
            sb.AppendFormat("<td>{0}</td>", m.LOSEMONEYCOUNT + "元");
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">备注:</td>");
            sb.AppendFormat("<td colspan=\"3\">{0}</td>", m.MARK);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ViewBag.SeeData = sb.ToString();
            return View();
        }

        /// <summary>
        /// 灾后处理费用数据管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult LOSTPROCESSManager()
        {
            FIRELOST_LOSTTYPE_LOSTPROCESS_Model m = new FIRELOST_LOSTTYPE_LOSTPROCESS_Model();
            m.LOSTPROCESSID = Request.Params["PROCESSID"];
            m.FIRELOST_FIREINFOID = Request.Params["FIREINFOID"];
            m.LOSTPROCESSNAME = Request.Params["PROCESSNAME"];
            m.LOSTPROCESSCODE = Request.Params["PROCESSCODE"];
            m.LOSEMONEYCOUNT = Request.Params["LOSEMONEYCOUNT"];
            m.MARK = Request.Params["MARK"];
            m.opMethod = Request.Params["Method"];
            if (string.IsNullOrEmpty(m.opMethod) == true)
                m.opMethod = "Add";
            return Content(JsonConvert.SerializeObject(FIRELOST_LOSTTYPE_LOSTPROCESSCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #endregion

        #region 灾损评估新
        /// <summary>
        /// 灾损评估新
        /// </summary>
        /// <returns></returns>
        public ActionResult FireLostAssessNew()
        {
            string JCFID = Request.Params["JCFID"];
            FIRERECORD_FIREINFO_Model m;
            FIRELOST_FIREINFO_Model model;
            T_SYS_DICTTYPE_Model dicType501;
            GetModelandDicType(JCFID, out m, out model, out dicType501);
            string dicCount = "";
            if (dicType501 != null && dicType501.DICTTYPEListModel.Count() > 0)
            {
                for (int x = 0; x < dicType501.DICTTYPEListModel.Count(); x++)
                {
                    if (dicType501.DICTTYPEListModel[x].DICTListModel.Count() > 0)
                        dicCount += T_SYS_DICTCls.getDicValueStr(dicType501.DICTTYPEListModel[x].DICTListModel) + ";";
                    else
                        dicCount += "0" + ";";
                }
            }
            if (dicCount.Length > 0)
                dicCount = dicCount.Substring(0, dicCount.Length - 1);
            ViewBag.dicType501 = dicType501;
            ViewBag.dicCount = dicCount;
            ViewBag.JCFID = m.JCFID;
            ViewBag.FIRECODE = m.FIRECODE;
            ViewBag.ORGNAME = m.ORGNAME;
            ViewBag.FIRETIME = m.FIRETIME;
            return View(model);
        }
        #endregion

        #region Private
        /// <summary>
        /// 获取模型及数字字典
        /// </summary>
        /// <param name="JCFID">火情ID</param>
        /// <param name="m">火灾档案</param>
        /// <param name="model">灾损火情基本信息</param>
        /// <param name="dicType501">灾损分类统计数据集合</param>
        private static void GetModelandDicType(string JCFID, out FIRERECORD_FIREINFO_Model m, out FIRELOST_FIREINFO_Model model, out T_SYS_DICTTYPE_Model dicType501)
        {
            m = FIRERECORD_FIREINFOCls.getModel(new FIRERECORD_FIREINFO_SW { JCFID = JCFID });
            model = FIRELOST_FIREINFOCls.getModel(new FIRELOST_FIREINFO_SW { JCFID = JCFID });
            if (string.IsNullOrEmpty(model.FIRELOST_FIREINFOID))
            {
                string fireInfoId = FIRELOST_FIREINFOCls.Manager(new FIRELOST_FIREINFO_Model { JCFID = JCFID, opMethod = "Init" }).Url;
                model = FIRELOST_FIREINFOCls.getModel(new FIRELOST_FIREINFO_SW { FIRELOST_FIREINFOID = fireInfoId });
            }
            if (model.TOTALAREA == "0") { model.TOTALAREA = ""; }
            if (model.FIREAREA == "0") { model.FIREAREA = ""; }
            if (model.FIRELOSEAREA == "0") { model.FIRELOSEAREA = ""; }
            if (model.TOTALXJL == "0") { model.TOTALXJL = ""; }
            if (model.XJLLOSE == "0") { model.XJLLOSE = ""; }
            if (model.TOTALPERSON == "0") { model.TOTALPERSON = ""; }
            if (model.CASUALTYCOUNT == "0") { model.CASUALTYCOUNT = ""; }
            if (model.BUILDINGLOSECOUNT == "0") { model.BUILDINGLOSECOUNT = ""; }
            if (model.MACHINERYLOSECOUNT == "0") { model.MACHINERYLOSECOUNT = ""; }
            if (model.LOSSCOUNT == "0") { model.LOSSCOUNT = ""; }
            if (model.FORESTRESOURCELOSSRATIO == "0")
                model.FORESTRESOURCELOSSRATIO = "";
            else
                model.FORESTRESOURCELOSSRATIO = string.Format("{0:P}", float.Parse(model.FORESTRESOURCELOSSRATIO));
            if (model.AVGLOSSPERCATITAVALUE == "0") { model.AVGLOSSPERCATITAVALUE = ""; }
            if (model.WOODLANDLOSSAVGVALUE == "0") { model.WOODLANDLOSSAVGVALUE = ""; }
            if (model.FIRESUPPEFFECTTHAN == "0")
                model.FIRESUPPEFFECTTHAN = "";
            else
                model.FIRESUPPEFFECTTHAN = string.Format("{0:P}", float.Parse(model.FIRESUPPEFFECTTHAN));
            dicType501 = T_SYS_DICTCls.getTypeModel(new T_SYS_DICTTYPE_SW { DICTTYPEID = "501" });
        }

        /// <summary>
        /// 获取灾损评估数据
        /// </summary>
        /// <param name="m">火灾档案</param>
        /// <param name="model">灾损火情基本信息</param>
        /// <param name="dicType501">灾损类别数字字典</param>
        /// <param name="templist">灾损分类统计数据集合</param>
        /// <returns></returns>
        private static string GetFireLostData(FIRERECORD_FIREINFO_Model m, FIRELOST_FIREINFO_Model model, T_SYS_DICTTYPE_Model dicType501)
        {
            List<FIRELOST_LOSTTYPECOUNT_Model> templist = FIRELOST_LOSTTYPECOUNTCls.getListModel(new FIRELOST_LOSTTYPECOUNT_SW { FIRELOST_FIREINFOID = model.FIRELOST_FIREINFOID }).ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"divMan\" style=\"margin-left:5px;margin-top:8px\">");
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" style=\"text-align:left;\">");
            sb.AppendFormat("<thead><tr><th colspan=\"4\">森林火灾损失汇总表</th></tr></thead>");
            sb.AppendFormat("<tbody>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" style=\"width: 25%\">森林火灾编号</td>");
            sb.AppendFormat("<td style=\"width: 25%\" colspan=\"3\">{0}</td>", m.FIRECODE);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td  class=\"tdField\" style=\"width:25%\">起火单位</td>");
            sb.AppendFormat("<td style=\"width:25%\">{0}</td>", m.ORGNAME);
            sb.AppendFormat("<td class=\"tdField\" style=\"width:25%\">起火时间</td>");
            sb.AppendFormat("<td style=\"width:25%\">{0}</td>", m.FIRETIME);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">火场面积/hm²</td>");
            sb.AppendFormat("<td>{0}</td>", model.FIREAREA);
            sb.AppendFormat("<td class=\"tdField\">森林火灾受害面积/hm²</td>");
            sb.AppendFormat("<td>{0}</td>", model.FIRELOSEAREA);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">伤(亡)人数/人</td>");
            sb.AppendFormat("<td>{0}</td>", model.CASUALTYCOUNT);
            sb.AppendFormat("<td class=\"tdField\">损失林木蓄积/hm²</td>");
            sb.AppendFormat("<td >{0}</td>", model.XJLLOSE);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\">建筑物(或构建物)损失量/m²</td>");
            sb.AppendFormat("<td>{0}</td>", model.BUILDINGLOSECOUNT);
            sb.AppendFormat("<td class=\"tdField\">机械设备损失量/台、件</td>");
            sb.AppendFormat("<td>{0}</td>", model.MACHINERYLOSECOUNT);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td colspan=\"2\" class=\"center tdField\">损失分类</td>");
            sb.AppendFormat("<td class=\"center tdField\">损失金额(元)</td>");
            sb.AppendFormat("<td class=\"center tdField\">备注</td>");
            sb.AppendFormat("</tr>");
            if (dicType501 != null && dicType501.DICTTYPEListModel.Count > 0)
            {
                foreach (var type in dicType501.DICTTYPEListModel)
                {
                    int rowCount = type.DICTListModel.Count;
                    if (rowCount > 0)
                    {
                        FIRELOST_LOSTTYPECOUNT_Model m1 = templist.Where(a => a.FIRELOSETYPECODE == type.DICTListModel[0].DICTVALUE).FirstOrDefault();
                        string louseMoney1 = (m1 != null && !string.IsNullOrEmpty(m1.LOSEMONEY)) ? m1.LOSEMONEY : "";
                        string mark1 = (m1 != null && !string.IsNullOrEmpty(m1.MARK)) ? m1.MARK : "";
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td rowspan=\"{1}\" class=\"tdField\">{0}</td>", type.DICTTYPENAME, rowCount);
                        sb.AppendFormat("<td class=\"tdField\">{0}</td>", type.DICTListModel[0].DICTNAME);
                        sb.AppendFormat("<td>{0}</td>", louseMoney1);
                        sb.AppendFormat("<td>{0}</td>", mark1);
                        sb.AppendFormat("</tr>");
                        for (int i = 1; i < rowCount; i++)
                        {
                            FIRELOST_LOSTTYPECOUNT_Model m2 = templist.Where(a => a.FIRELOSETYPECODE == type.DICTListModel[i].DICTVALUE).FirstOrDefault();
                            string louseMoney2 = (m2 != null && !string.IsNullOrEmpty(m2.LOSEMONEY)) ? m2.LOSEMONEY : "";
                            string mark2 = (m2 != null && !string.IsNullOrEmpty(m2.MARK)) ? m2.MARK : "";
                            sb.AppendFormat("<tr>");
                            sb.AppendFormat("<td class=\"tdField\">{0}</td>", type.DICTListModel[i].DICTNAME);
                            sb.AppendFormat("<td>{0}</td>", louseMoney2);
                            sb.AppendFormat("<td>{0}</td>", mark2);
                            sb.AppendFormat("</tr>");
                        }
                    }
                }
            }
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" colspan=\"2\">损失总计</td>");
            sb.AppendFormat("<td colspan=\"2\">{0}元</td>", model.LOSSCOUNT);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" colspan=\"2\">森林资源损失率</td>");
            sb.AppendFormat("<td colspan=\"2\">{0}</td>", model.FORESTRESOURCELOSSRATIO);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" colspan=\"2\">人均损失价值</td>");
            sb.AppendFormat("<td colspan=\"2\">{0}元/人</td>", model.AVGLOSSPERCATITAVALUE);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" colspan=\"2\">林地损失平均价值量</td>");
            sb.AppendFormat("<td colspan=\"2\">{0}元/hm²</td>", model.WOODLANDLOSSAVGVALUE);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"tdField\" colspan=\"2\">扑火成效比</td>");
            sb.AppendFormat("<td colspan=\"2\">{0}</td>", model.FIRESUPPEFFECTTHAN);
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            return sb.ToString();
        }
        #endregion
    }
}
