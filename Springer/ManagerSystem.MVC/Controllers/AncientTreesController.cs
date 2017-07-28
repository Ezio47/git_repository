using ManagerSystemClassLibrary;
using ManagerSystemClassLibrary.BaseDT;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    public class AncientTreesController : BaseController
    {
        #region 面积单位
        private string dic113Name = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "113" }).ToList()[0].DICTNAME;
        #endregion

        #region 古树名木管理
        /// <summary>
        /// 古树名木管理－增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult AncientTreesManager()
        {
            T_SYS_TREESPECIES_Model m = new T_SYS_TREESPECIES_Model();
            m.TSPCODE = Request.Params["TSPCODE"];
            m.TSPNAME = Request.Params["TSPNAME"];
            m.LATINNAME = Request.Params["LATINNAME"];
            m.ORDERBY = Request.Params["ORDERBY"];
            m.opMethod = Request.Params["Method"];
            if (m.opMethod != "Del")
                m.returnUrl = "/AncientTrees/TreeSpeciesList?TSPCODE=" + m.TSPCODE;
            //默认为添加
            if (string.IsNullOrEmpty(m.opMethod))
                m.opMethod = "Add";
            return Content(JsonConvert.SerializeObject(T_SYS_TREESPECIESCls.Manager(m)), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 获取古树名木Json字符串
        /// </summary>
        /// <returns></returns>
        public ActionResult getAncientTreesJson()
        {
            string TSPCODE = Request.Params["TSPCODE"];
            if (string.IsNullOrEmpty(TSPCODE))
                TSPCODE = "0";
            return Content(JsonConvert.SerializeObject(T_SYS_TREESPECIESCls.getModel(new T_SYS_TREESPECIES_SW { TSPCODE = TSPCODE })), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 古树名木列表--查询
        /// </summary>
        /// <returns></returns>
        public ActionResult AncientTreesListQuery()
        {
            string TSPCODE = Request.Params["TSPCODE"];//当前页面传递编号
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(TSPCODE))
                sb.AppendFormat(getAncientTreesStr(new T_SYS_TREESPECIES_SW { IsGetTopCode = true }));
            else
                sb.AppendFormat(getAncientTreesStr(new T_SYS_TREESPECIES_SW { TSPCODE = TSPCODE, ChildCODELength = TSPCODE.Length + 2 }));
            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 古树名木列表
        /// </summary>
        /// <returns></returns>
        public ActionResult TreeSpeciesList()
        {
            pubViewBag("006019", "006019", "");
            if (ViewBag.isPageRight == false)
                return View();
            string TSPCODE = Request.Params["TSPCODE"];//当前页面传递编号
            //导航条
            string navStr = "";
            if (string.IsNullOrEmpty(TSPCODE))
                ViewBag.TreeSpeciesList = getAncientTreesStr(new T_SYS_TREESPECIES_SW { IsGetTopCode = true });
            else
            {
                if (TSPCODE.Length > 1)
                {
                    ViewBag.TreeSpeciesList = getAncientTreesStr(new T_SYS_TREESPECIES_SW { TSPCODE = TSPCODE, ChildCODELength = TSPCODE.Length + 2 });
                    for (int i = 0; i < TSPCODE.Length / 2; i++)
                    {
                        if (i != TSPCODE.Length / 2 - 1)
                            navStr += "<li class=\"active\"><a href=\"/AncientTrees/TreeSpeciesList?TSPCODE=" + TSPCODE.Substring(0, (i + 1) * 2) + "\" >" + T_SYS_TREESPECIESCls.getName(TSPCODE.Substring(0, (i + 1) * 2)) + "</a></li>";

                        else
                            navStr += "<li class=\"active\">" + T_SYS_TREESPECIESCls.getName(TSPCODE) + "</li>";
                    }
                }
            }
            ViewBag.navList = navStr;
            ViewBag.TSPCODE = TSPCODE;
            ViewBag.Add = (SystemCls.isRight("006019001")) ? 1 : 0;
            ViewBag.Mdy = (SystemCls.isRight("006019002")) ? 1 : 0;
            ViewBag.Del = (SystemCls.isRight("006019003")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 获取古树名木列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        private string getAncientTreesStr(T_SYS_TREESPECIES_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style=\"width:10%;\">序号</th>");
            sb.AppendFormat("<th style=\"width:20%;\">编码</th>");
            sb.AppendFormat("<th style=\"width:25%;\">名称</th>");
            sb.AppendFormat("<th style=\"width:25%;\">拉丁名称</th>");
            sb.AppendFormat("<th style=\"width:10%;\">排序</th>");
            sb.AppendFormat("<th style=\"width:10%;\"></th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = T_SYS_TREESPECIESCls.getListModel(sw);
            int i = 0;
            foreach (var v in result)
            {
                sb.AppendFormat("<tr class=\"{1}\" onclick=\"showValue('{0}')\">", v.TSPCODE, (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.TSPCODE);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.TSPNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.LATINNAME);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.ORDERBY);
                sb.AppendFormat("    <td class=\" \">");
                if (v.TSPCODE.Length < 6)
                {
                    sb.AppendFormat("<a href=\"/AncientTrees/TreeSpeciesList?TSPCODE={0}\" >下属树种</a>", v.TSPCODE);
                }
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        #endregion

        #region 本地树种管理
        /// <summary>
        /// 本地树种管理
        /// </summary>
        /// <returns></returns>
        public ActionResult LOCALTREESPECIES()
        {
            pubViewBag("006020", "006020", "本地树种管理");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.vdOrg = T_SYS_ORGCls.getSelectOption(new T_SYS_ORGSW { TopORGNO = SystemCls.getCurUserOrgNo(), SYSFLAG = ConfigCls.getSystemFlag(), CurORGNO = SystemCls.getCurUserOrgNo() });
            ViewBag.Save = (SystemCls.isRight("006020001")) ? 1 : 0;
            return View();
        }

        /// <summary>
        /// 本地树种管理--异步查询
        /// </summary>
        /// <returns></returns>
        public ActionResult LOCALTREESPECIESQuery()
        {
            StringBuilder sb = new StringBuilder();

            #region 数据查询条件
            string ORGNO = Request.Params["ORGNO"];
            #endregion

            #region 数据准备
            string ORGNONAME = T_SYS_ORGCls.getorgname(ORGNO);
            List<T_SYS_TREESPECIES_Model> _list = T_SYS_TREESPECIESCls.getListModel(new T_SYS_TREESPECIES_SW { }).ToList();
            List<PEST_LOCALTREESPECIES_Model> _templist = PEST_LOCALTREESPECIESCls.getListModel(new PEST_LOCALTREESPECIES_SW { BYORGNO = ORGNO }).ToList();
            string dis = _list.Count <= 0 ? "disabled=\"disabled\"" : "";
            #endregion

            #region 数据表
            sb.AppendFormat("<table id=\"LOCALTREESPECIESTable\"  cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style=\"width:5%\"><input id=\"tbxTSPALL\" name=\"tbxTSPALL\" type=\"checkbox\" class=\"ace\" value=\"ALL\" onclick=\"SelectAll(this.value,this.checked)\" {0} /></th>", dis);
            sb.AppendFormat("<th style=\"width:10%;\">序号</th><th style=\"width:25%;\">单位名称</th><th style=\"width:25%;\">树种名称</th><th style=\"width:35%;\">本地面积</br>(" + dic113Name + ")</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            for (int i = 0; i < _list.Count; i++)
            {
                PEST_LOCALTREESPECIES_Model m = _templist.Find(a => a.BYORGNO == ORGNO && a.TSPCODE == _list[i].TSPCODE);
                string area = (m != null && !string.IsNullOrEmpty(m.TSPAREA)) ? string.Format("{0:0.00}", float.Parse(m.TSPAREA)) : "";
                string chk = _templist.Find(a => a.BYORGNO == ORGNO && a.TSPCODE == _list[i].TSPCODE) != null ? " checked" : "";
                sb.AppendFormat("<tr class=\"{0}\">", (i % 2 == 0) ? "" : "row1");
                sb.AppendFormat("<td><input id=\"tbxTSP" + i + "\" name=\"tbxTSP\"  type=\"checkbox\" class=\"ace\" value=\"" + _list[i].TSPCODE + "\" onclick=\"SelectAll(this.value,this.checked)\" {0} /></td>", chk);
                sb.AppendFormat("<td class=\"center\">{0}</td>", (i + 1).ToString());
                sb.AppendFormat("<td class=\"center\">{0}</td>", ORGNONAME);
                sb.AppendFormat("<td class=\"left\" style=\"{1}\">{0}</td>", _list[i].TSPNAME, PublicCls.getTSPNameClass(_list[i].TSPCODE));
                sb.AppendFormat("<td class=\"center\">{0}</td>", "<input id=\"tbxAREA" + _list[i].TSPCODE + "\"  type=\"text\"  value=\"" + area + "\" style=\"width:98%;\" class=\"center\"  />");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            #endregion

            return Content(JsonConvert.SerializeObject(new Message(true, sb.ToString(), "")), "text/html;charset=UTF-8");
        }

        /// <summary>
        /// 本地树种管理-增、删、改
        /// </summary>
        /// <returns></returns>
        public ActionResult LOCALTREESPECIESManager()
        {
            PEST_LOCALTREESPECIES_Model m = new PEST_LOCALTREESPECIES_Model();
            m.BYORGNO = Request.Params["ORGNO"];
            m.TSPCODE = Request.Params["TSPCODE"];
            m.TSPAREA = Request.Params["TSPAREA"];
            return Content(JsonConvert.SerializeObject(PEST_LOCALTREESPECIESCls.Manager(m)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 古树名木采集
        /// <summary>
        /// 古树名木采集
        /// </summary>
        /// <returns></returns>
        public ActionResult TreesInfoList()
        {
            pubViewBag("023003", "023003", "");
            if (ViewBag.isPageRight == false)
                return View();
            return View();
        }
        #endregion

        #region 古树群采集
        /// <summary>
        /// 古树群采集
        /// </summary>
        /// <returns></returns>
        public ActionResult TreeGroupInfoList()
        {
            pubViewBag("023004", "023004", "");
            if (ViewBag.isPageRight == false)
                return View();
            return View();
        }
        #endregion
    }
}
