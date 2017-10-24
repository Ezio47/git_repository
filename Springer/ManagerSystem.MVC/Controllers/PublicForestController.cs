using ManagerSystemClassLibrary;
using ManagerSystemClassLibrary.SDECLS;
using ManagerSystemModel;
using ManagerSystemModel.SDEModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    public class PublicForestController : BaseController
    {
        //
        // GET: /PublicForest/

        /// <summary>
        /// 公益林
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            pubViewBag("017001", "017001", "");
            return View();
        }

        /// <summary>
        /// 公益林详细
        /// </summary>
        /// <returns></returns>
        public ActionResult PopDetailIndex()
        {
            string objId = Request.Params["objId"];
            var model = GONGYILINCls.getListModel(new SDE_GONGYILIN_Model { OBJECTID = objId }).FirstOrDefault();
            return View(model);
        }

        #region Ajax
        /// <summary>
        /// ajax 获取公益林数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetGYLAjax()
        {
            int total = 0;//记录总数
            var COUNTY = Request.Params["COUNTY"];//县市
            var COUNTRY = Request.Params["COUNTRY"];//乡镇
            var VILLAGE = Request.Params["VILLAGE"];//村
            var LINBAN = Request.Params["LINBAN"];//林班
            var XIAOBAN = Request.Params["XIAOBAN"];//小班
            string PageSize = Request.Params["PageSize"];//记录个数
            string page = Request.Params["page"];//页数
            var result = GONGYILINCls.getModelPager(new SDE_GONGYILIN_Model { curPage = int.Parse(page), pageSize = int.Parse(PageSize), COUNTY = COUNTY, COUNTRY = COUNTRY, VILLAGE = VILLAGE, LINBAN = LINBAN, XIAOBAN = XIAOBAN }, out total);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>序号</th>");
            sb.AppendFormat("<th>县</th>");
            sb.AppendFormat("<th>乡</th>");
            sb.AppendFormat("<th>村</th>");
            sb.AppendFormat("<th>林班</th>");
            sb.AppendFormat("<th>小班</th>");
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
                        sb.AppendFormat("<tr onClick='onClickGYL(" + s.OBJECTID + "," + s.STX + "," + s.STY + ")'>");
                    else
                        sb.AppendFormat("<tr class='row1'  onClick='onClickGYL(" + s.OBJECTID + "," + s.STX + "," + s.STY + ")'>");
                    sb.AppendFormat("<td>{0}</td>", ++rowB);
                    sb.AppendFormat("<td>{0}</td>", s.COUNTY);
                    sb.AppendFormat("<td>{0}</td>", s.COUNTRY);
                    sb.AppendFormat("<td>{0}</td>", s.VILLAGE);
                    sb.AppendFormat("<td>{0}</td>", s.LINBAN);
                    sb.AppendFormat("<td>{0}</td>", s.XIAOBAN);
                    sb.AppendFormat("</tr>");
                    ++i;
                }
            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan='6'>未查询出结果</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            string pageInfo = PagerCls.getPagerInfoAjax(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(PageSize), rowCount = total, hidePageList = true ,hidePageSize=true});
            return Json(new MessagePagerAjax(true, sb.ToString(), pageInfo));
        }
        #endregion
    }
}
