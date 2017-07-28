using ManagerSystemClassLibrary;
using ManagerSystemModel;
using ManagerSystemModel.SDEModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    /// <summary>
    /// 数据采集
    /// </summary>
    public class DataCollectController : BaseController
    {
        
        /// <summary>
        /// 获取采集数据Ajax
        /// </summary>
        /// <param name="orgname"></param>
        /// <returns>参见模型</returns>
        [HttpPost]
        public JsonResult GetCollectAjax(string Hid)
        {
            //获取参数
            var sw = new CollectDataSW();
            sw.HID = Convert.ToInt32(Hid);
            var model = T_IPSCOL_COLLECTDATACls.get_CollectDataModelList(sw);
            return Json(model);
        }


        #region Ajax
        /// <summary>
        /// 获取采集信息List
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult GetCollectDataListAjax()
        {
            string state = Request.Params["state"];//获取处理状态
            string type = Request.Params["type"];//采集数据类型
            string strarttime = Request.Params["strarttime"];//开始时间
            string endtime = Request.Params["endtime"];//结束时间
            var sw = new T_IPSCOL_COLLECT_SW();
            if (state != "2")
            {
                sw.MANSTATE = state;
            }
            sw.DateBegin = strarttime;
            sw.DateEnd = endtime;
            sw.SYSTYPEVALUE = type;//采集类型
            sw.orgNo = SystemCls.getCurUserOrgNo();//当前单位
            var list = T_IPSCOL_COLLECTCls.getModelList(sw);
            if (list.Any())
                return Json(new MessageListObject(true, list));
            else
                return Json(new MessageListObject(false, null));
        }

        /// <summary>
        /// 获取采集信息model
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult GetCollectModelDataAjax()
        {
            string cid = Request.Params["cid"];//获取处理状态
            string maptype = Request.Params["maptype"];
            var sw = new T_IPSCOL_COLLECT_SW();
            if (!string.IsNullOrEmpty(maptype))
            {
                sw.MapType = maptype;//地图类型
            }
            sw.COLLECTID = cid;
            var list = T_IPSCOL_COLLECTCls.getModelList(sw);
            if (list.Any())
                return Json(new MessageListObject(true, list));
            else
                return Json(new MessageListObject(false, null));
        }

        /// <summary>
        /// 获取采集明细List
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult GetCollectInfoAjax()
        {
            string cid = Request.Params["cid"];
            string maptype = Request.Params["maptype"];
            if (string.IsNullOrEmpty(cid))
            {
                return Json(new Message(false, "cid参数传递失败", ""));
            }
            var sw = new T_IPSCOL_COLLECT_SW();
            sw.COLLECTID = cid;
            sw.MapType = maptype;
            var list = T_IPSCOL_COLLECTCls.getDetailModelList(sw);
            return Json(new MessageListObject(true, list));
        }


        /// <summary>
        /// 转换为护林员巡检线和责任区
        /// </summary>
        /// <returns></returns>
        public JsonResult ConvertHlyLineArea()
        {
            Message msg = new Message(false, "转化失败！", "");
            string cid = Request.Params["cid"];//采集id
            string hid = Request.Params["hid"];//护林员id
            string htype = Request.Params["htype"];//护林员转换类型 0 表是线 1 表示面
            var sw = new T_IPSCOL_COLLECT_SW();
            sw.COLLECTID = cid;
            var list = T_IPSCOL_COLLECTCls.getDetailModelList(sw);
            if (list.Any())
            {
                string pointstr = string.Empty;
                int j = 0;
                string str = string.Empty;
                foreach (var item in list)
                {
                    if (htype == "1")//面 特殊处理
                    {
                        if (j == 0)
                        {
                            str = item.LONGITUDE + "," + item.LATITUDE + "|";
                        }
                    }
                    //pointstr += item.ORILONGITUDE + "," + item.ORILATITUDE + ",,|";
                    pointstr += item.LONGITUDE + "," + item.LATITUDE + "|";
                    j++;
                }
                pointstr += str;
                var m = new T_IPSFR_ROUTERAIL_Model();//巡检线和责任区
                m.HID = hid;
                m.longitLatitList = pointstr;
                m.longitLatitList = m.longitLatitList.Replace("|", ", , |");
                m.longitLatitList = m.longitLatitList + ";";
                m.opMethod = "AddBatch";
                m.ROADTYPE = htype;
                var ms = T_IPSFR_ROUTERAILCls.Manager(m);
                if (ms.Success)
                {
                    string jd = "";
                    string wd = "";
                    string line = "";
                    string polygon = "";
                    //入三维空间库
                    #region 责任线
                    if (htype == "0")
                    {
                        m.longitLatitList = m.longitLatitList.Substring(0, m.longitLatitList.LastIndexOf(";"));
                        m.longitLatitList = m.longitLatitList.Replace(",,", "");
                        if (!string.IsNullOrEmpty(m.longitLatitList))
                        {
                            var result = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { HID = hid }).FirstOrDefault();//获取护林员信息
                            var m1 = new TD_DUTYROUTE_Model();//三维责任路线模型
                            m1.opMethod = m.opMethod;
                            m1.NAME = result.HNAME;
                            m1.OBJECTID = result.HID;
                            m1.ORGNAME = result.ORGNAME;
                            m1.TELEPHONE = result.PHONE;
                            string[] arr = m.longitLatitList.Split('|');
                            for (int i = 0; i < arr.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(arr[i]))
                                {
                                    string[] brr = arr[i].Split(',');
                                    double[] drr = ClsPositionTrans.GpsTransform(double.Parse(brr[1]), double.Parse(brr[0]), ConfigCls.getSDELonLatTransform());//坐标系转换
                                    wd = drr[0].ToString();
                                    jd = drr[1].ToString();
                                }
                                if (i == arr.Length - 1)//最后一条记录
                                {
                                    line += jd + " " + wd;
                                }
                                else
                                {
                                    line += jd + " " + wd + ",";
                                }
                            }
                            #region 中心点获取
                            if (arr.Length % 2 == 0)
                            {
                                string[] crr = arr[arr.Length / 2].Split(',');
                                double[] drr = ClsPositionTrans.GpsTransform(double.Parse(crr[1]), double.Parse(crr[0]), ConfigCls.getSDELonLatTransform());
                                m1.DISPLAY_X = drr[1].ToString();
                                m1.DISPLAY_Y = drr[0].ToString();
                            }
                            else
                            {
                                string[] crr = arr[(arr.Length + 1) / 2].Split(',');
                                double[] drr = ClsPositionTrans.GpsTransform(double.Parse(crr[1]), double.Parse(crr[0]), ConfigCls.getSDELonLatTransform());//中心点偏移
                                m1.DISPLAY_X = drr[1].ToString();
                                m1.DISPLAY_Y = drr[0].ToString();
                            }
                            #endregion
                            m1.Shape = "geometry::STGeomFromText('LINESTRING(" + line + ")',4326).MakeValid()";
                            DC_DUTYROUTECls.Manager(m1);
                        }
                    }
                    #endregion

                    #region 责任面
                    else
                    {
                        m.longitLatitList = m.longitLatitList.Substring(0, m.longitLatitList.LastIndexOf(";"));
                        m.longitLatitList = m.longitLatitList.Replace(",,", "");
                        if (!string.IsNullOrEmpty(m.longitLatitList))
                        {
                            var result = T_IPSFR_USERCls.getListModel(new T_IPSFR_USER_SW { HID = hid }).FirstOrDefault();//获取护林员信息
                            var m2 = new TD_DUTYAREA_Model();//三维责任区模型
                            m2.opMethod = m.opMethod;
                            m2.NAME = result.HNAME;
                            m2.OBJECTID = result.HID;
                            m2.ORGNAME = result.ORGNAME;
                            m2.TELEPHONE = result.PHONE;
                            string[] arr = m.longitLatitList.Split('|');
                            for (int i = 0; i < arr.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(arr[i]))
                                {
                                    string[] brr = arr[i].Split(',');
                                    double[] drr = ClsPositionTrans.GpsTransform(double.Parse(brr[1]), double.Parse(brr[0]), ConfigCls.getSDELonLatTransform());//坐标系转换
                                    wd = drr[0].ToString();
                                    jd = drr[1].ToString();
                                }
                                if (i == arr.Length - 1)//最后一条记录
                                {
                                    polygon += jd + " " + wd;
                                }
                                else
                                {
                                    polygon += jd + " " + wd + ",";
                                }
                            }
                            #region 中心点获取
                            if (arr.Length % 2 == 0)
                            {
                                string[] crr = arr[arr.Length / 2].Split(',');
                                double[] drr = ClsPositionTrans.GpsTransform(double.Parse(crr[1]), double.Parse(crr[0]), ConfigCls.getSDELonLatTransform());
                                m2.DISPLAY_X = drr[1].ToString();
                                m2.DISPLAY_Y = drr[0].ToString();
                            }
                            else
                            {
                                string[] crr = arr[(arr.Length + 1) / 2].Split(',');
                                double[] drr = ClsPositionTrans.GpsTransform(double.Parse(crr[1]), double.Parse(crr[0]), ConfigCls.getSDELonLatTransform());//中心点偏移
                                m2.DISPLAY_X = drr[1].ToString();
                                m2.DISPLAY_Y = drr[0].ToString();
                            }
                            #endregion
                            m2.Shape = "geometry::STGeomFromText('Polygon((" + polygon + "))',4326).MakeValid()";
                            DC_DUTYAREACls.Manager(m2);
                        }
                    }
                    msg = new Message(true, "转换成功！", "");
                    #endregion
                }
            }
            return Json(msg);
        }

        /// <summary>
        /// 获取采集明细
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult GetCollectModelInfoAjax()
        {
            string cid = Request.Params["cid"];
            if (string.IsNullOrEmpty(cid))
            {
                return Json(new Message(false, "cid参数传递失败", ""));
            }
            var sw = new T_IPSCOL_COLLECT_SW();
            sw.COLLECTID = cid;
            var model = T_IPSCOL_COLLECTCls.getModel(sw);
            return Json(new MessageObject(true, model));
        }

        /// <summary>
        /// 采集点详细Html
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult GetCollectDetailAjax()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table id=\"sample-table-1\" class=\"table table-striped table-bordered table-hover\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("  <tr> ");
            sb.AppendFormat("  <th>序号</th>");
            sb.AppendFormat("  <th>经度</th>");
            sb.AppendFormat("  <th>纬度</th>");
            sb.AppendFormat("  <th>采集时间</th>");
            sb.AppendFormat("   </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            string cid = Request.Params["cid"];
            if (string.IsNullOrEmpty(cid))
                return Json(new Message(false, "cid参数传递失败", ""));
            var sw = new T_IPSCOL_COLLECT_SW();
            sw.COLLECTID = cid;
            var list = T_IPSCOL_COLLECTCls.getDetailModelList(sw);
            if (list.Any())
            {
                int i = 0;
                foreach (var item in list)
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>{0}</td>", ++i);
                    sb.AppendFormat("<td>{0}</td>", item.LONGITUDE);
                    sb.AppendFormat("<td>{0}</td>", item.LATITUDE);
                    sb.AppendFormat("<td>{0}</td>", item.COLLECTTIME);
                    sb.AppendFormat("</tr>");
                }
            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan=\"4\">暂无经纬度信息</td>");
                sb.AppendFormat("</tr>");

            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return Json(new Message(true, sb.ToString(), ""));
        }

        /// <summary>
        /// 获取采集图片List
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult GetPhotoInfoAjax()
        {
            string cid = Request.Params["cid"];
            if (string.IsNullOrEmpty(cid))
                return Json(new Message(false, "cid参数传递失败", ""));
            var sw = new T_IPSCOL_COLLECT_SW();
            sw.COLLECTID = cid;
            var list = T_IPSCOL_COLLECTCls.getUploadlModelList(sw);
            return Json(new MessageListObject(true, list));
        }

        /// <summary>
        /// 采集管理
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult SaveCollectDataAjax()
        {
            string cid = Request.Params["cid"];
            string describe = Request.Params["describe"];
            string result = Request.Params["result"];
            string typeid = Request.Params["typeid"];
            string state = Request.Params["state"];
            if (string.IsNullOrEmpty(cid))
                return Json(new Message(false, "cid参数传递失败", ""));
            var m = new T_IPSCOL_COLLECT_Model();
            m.opMethod = "Man";
            m.COLLECTID = cid;
            m.MANUSERID = SystemCls.getUserID();
            m.MANRESULT = result;
            m.COLLECTNAME = describe;
            var ms = T_IPSCOL_COLLECTCls.Manager(m);
            var sde = System.Configuration.ConfigurationManager.AppSettings["IsInsertSDE"].ToString();
            if (sde == "1")
            {
                #region 空间数据库
                //空间数据库处理
                if (state != "1")//1表示已处理
                {
                    if (typeid != "3")//非线类型
                    {
                        T_IPSCOL_COLLECT_SW sw = new T_IPSCOL_COLLECT_SW();
                        sw.COLLECTID = cid;
                        var reocrdlist = T_IPSCOL_COLLECTCls.getDetailModelList(sw);
                        if (reocrdlist.Any())
                        {
                            var point = reocrdlist.OrderByDescending(p => p.COLLECTTIME).FirstOrDefault();
                            T_COLLECTPOINTS_Model pointmodel = new T_COLLECTPOINTS_Model();//点
                            pointmodel.TypeId = Convert.ToInt32(typeid);
                            pointmodel.NAME = m.COLLECTNAME;
                            //POINT (117.14508056640625 31.764892578125)
                            //geometry::STGeomFromText('POINT(103.397553 23.365441)',4326)
                            pointmodel.Shape = " POINT (" + point.LONGITUDE + " " + point.LATITUDE + ")";
                            pointmodel.opMethod = "ADD";
                            T_COLLECTPOINTSCls.Manager(pointmodel);
                        }
                    }
                    else//线
                    {
                        T_IPSCOL_COLLECT_SW sw = new T_IPSCOL_COLLECT_SW();
                        sw.COLLECTID = cid;
                        var reocrdlist = T_IPSCOL_COLLECTCls.getDetailModelList(sw);
                        if (reocrdlist.Any())
                        {
                            string line = "";
                            int i = 0;
                            foreach (var item in reocrdlist)
                            {
                                if (i == reocrdlist.Count() - 1)//最后一条记录
                                {
                                    line += item.LONGITUDE + " " + item.LATITUDE;
                                }
                                else
                                {
                                    line += item.LONGITUDE + " " + item.LATITUDE + ",";
                                }
                                i++;
                            }

                            T_COLLECTLINES_Model linemodel = new T_COLLECTLINES_Model();//线
                            linemodel.TypeId = Convert.ToInt32(typeid);
                            linemodel.NAME = m.COLLECTNAME;
                            //LINESTRING (116.9428721060001 31.786694108000063, 117.22840742700009 31.74386381000005)
                            //geometry::STGeomFromText()
                            linemodel.Shape = "geometry::STGeomFromText('LINESTRING (" + line + ")',4326)";
                            linemodel.opMethod = "ADD";
                            T_COLLECTLINESCls.Manager(linemodel);
                        }
                    }
                }
                #endregion
            }
            return Json(ms);
        }

        /// <summary>
        /// 获取检索上报数据ListHtml
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult GetCollectDataHtmlAjax()
        {
            Message ms = null;
            string person = Request.Params["person"];
            string starttime = Request.Params["strarttime"];
            string endtime = Request.Params["endtime"];
            string state = Request.Params["state"];
            string type = Request.Params["cid"];
            string Skytype = Request.Params["type"];
            StringBuilder sb = new StringBuilder();
            if (Skytype == "Skyline")
            {
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<thead>");
                sb.AppendFormat("  <tr> ");
                sb.AppendFormat("  <th>序号</th>"); ;
                sb.AppendFormat("  <th>采集人</th>");
                sb.AppendFormat("  <th>采集时间</th>");
                sb.AppendFormat("  <th>操作</th>");
            }
            else
            {
                sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
                sb.AppendFormat("<thead>");
                sb.AppendFormat("  <tr> ");
                sb.AppendFormat("  <th>序号</th>");
                sb.AppendFormat("  <th>数据类型</th>");
                sb.AppendFormat("  <th>采集人</th>");
                sb.AppendFormat("  <th>电话号码</th>");
                sb.AppendFormat("  <th>采集名称</th>");
                sb.AppendFormat("  <th>采集时间</th>");
                sb.AppendFormat("  <th>状态</th>");
                sb.AppendFormat("  <th>操作</th>");
            }
            sb.AppendFormat("   </tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");

            var sw = new T_IPSCOL_COLLECT_SW();
            sw.DateBegin = starttime;
            sw.DateEnd = endtime;
            sw.SYSTYPEVALUE = type;
            sw.UnionHUser = false;
            if (!string.IsNullOrEmpty(person))
            {
                sw.HUserName = person;
                sw.UnionHUser = true;
            }
            if (state != "2")
                sw.MANSTATE = state;
            var cuurorg = SystemCls.getCurUserOrgNo();
            if (!string.IsNullOrEmpty(cuurorg))
                sw.orgNo = cuurorg;
            var list = T_IPSCOL_COLLECTCls.getModelList(sw);
            if (list.Any())
            {
                var url = System.Configuration.ConfigurationManager.AppSettings["SkylineUrl"].ToString();
                var personPopurl = url + @"/SkylineManger/PersonDetailIndex";
                var collectViewPopurl = url + @"/SkylineManger/DataCollectDetailViewIndex";
                int i = 0;
                foreach (var item in list)
                {
                    sb.AppendFormat("<tr>");
                    sb.AppendFormat("<td>{0}</td>", ++i);
                    if (Skytype == "Skyline")
                    {
                        sb.AppendFormat("<td><a href=\"javascript:void(0);\" title=\"人员信息\" onclick=\"PopUrlCollect('" + personPopurl + "'," + item.HID + ")\">{0}</a></td>", item.HName);
                        //  sb.AppendFormat("<td>{0}</td>", item.PHONE);
                        sb.AppendFormat("<td title=\"{0}\">{1}</td>", item.COLLECTTIME, Convert.ToDateTime(item.COLLECTTIME).ToString("MM-dd HH:mm"));
                    }
                    else
                    {
                        sb.AppendFormat("<td>{0}</td>", item.SYSTYPEName);
                        sb.AppendFormat("<td>{0}</td>", item.HName);
                        sb.AppendFormat("<td>{0}</td>", item.Phone);
                        sb.AppendFormat("<td>{0}</td>", item.COLLECTNAME);
                        sb.AppendFormat("<td>{0}</td>", item.COLLECTTIME);
                        if (item.MANSTATE == "0")
                        {
                            sb.AppendFormat("<td><a class=\"label label-danger\">未处理</a></td>");
                        }
                        else
                        {
                            sb.AppendFormat("<td><a class=\"label label-success\">已处理</a></td>");
                        }
                    }
                    //class=\"icon-flag\" 
                    if (Skytype == "Skyline")
                    {
                        sb.AppendFormat("<td  class=\"center\"> ");
                        sb.AppendFormat("<a href=\"javascript:void(0);\" onClick=\"getLocaCollect(" + item.COLLECTID + ")\" class=\"dw option\" title=\"展示\"></a><a href=\"javascript:void(0);\" onClick=\"getCollectView('" + collectViewPopurl + "'," + item.COLLECTID + ")\" class=\"ck option\" title=\"查看\"></a>");

                        //if (sw.SYSTYPEVALUE == "3")
                        //{
                        //    StringBuilder sb1 = new StringBuilder();
                        //    sb1.AppendFormat("<a href=\"javascript:void(0);\" onClick=\"ConvertType(" + item.COLLECTID + "," + item.HID + ",0)\" class=\"xjx option\" title=\"转为巡检线\"></a><a href=\"javascript:void(0);\"  onClick=\"ConvertType(" + item.COLLECTID + "," + item.HID + ",1)\" class=\"zrq option\" title=\"转为责任区\"></a>");
                        //    sb.Append(sb1.ToString());
                        //}
                        sb.AppendFormat("<a href=\"javascript:void(0);\" class=\"dj option\" title=\"对讲\"></a>");
                        sb.AppendFormat("</td>");
                    }
                    else
                    {
                        sb.AppendFormat("<td><a href=\"javascript:void(0);\" onClick=\"getLocaCollect(" + item.COLLECTID + ")\">定位</a></td>");
                    }
                    sb.AppendFormat("</tr>");
                }
            }
            else
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td colspan=\"8\">暂无信息</td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");

            ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }

        /// <summary>
        /// 删除采集数据
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult DeleteCollectDataAjax()
        {
            string cid = Request.Params["cid"];
            if (string.IsNullOrEmpty(cid))
            {
                return Json(new Message(false, "cid参数传递失败", ""));
            }
            var m = new T_IPSCOL_COLLECT_Model();
            m.opMethod = "Del";
            m.COLLECTID = cid;
            m.MANUSERID = SystemCls.getUserID();
            var ms = T_IPSCOL_COLLECTCls.Manager(m);
            return Json(ms);
        }

        /// <summary>
        /// 修改采集点
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult ModifyCollectDataDetailAjax()
        {
            string editid = Request.Params["editid"];//编辑采集点id
            if (string.IsNullOrEmpty(editid))
            {
                return Json(new Message(false, "editid参数传递失败", ""));
            }
            string txt = Request.Params["edittxt"];//修改的点
            string[] strList = txt.Split('|');
            if (strList.Any())
            {
                var m = new T_IPSCOL_COLLECT_Model();
                m.opMethod = "ModifyDetail";
                m.COLLECTID = editid;//采集点
                var detailList = new List<T_IPSCOL_COLLECTDETAIL_SW>();
                foreach (var item in strList)
                {
                    if (!string.IsNullOrEmpty(item))//不为空
                    {
                        var detail = new T_IPSCOL_COLLECTDETAIL_SW();
                        var lonlat = item.Split(',');
                        detail.COLLECTID = editid;//采集点
                        detail.LON = lonlat[0];//经度
                        detail.LAT = lonlat[1];//纬度
                        detail.COLLECTTIME = DateTime.Now.ToString();//采集时间
                        detailList.Add(detail);
                    }
                }
                m.DataList = detailList;
                var ms = T_IPSCOL_COLLECTCls.Manager(m);
                return Json(ms);
            }
            else
            {
                return Json(new Message(false, "编辑采集点传递失败", ""));
            }

        }

        /// <summary>
        /// 获取字典类型数据
        /// </summary>
        /// <returns>参见模型</returns>
        public JsonResult GetDicDataAjax()
        {
            var sw = new T_SYS_DICTSW();
            sw.DICTTYPEID = Request.Params["TYPEID"]; ;
            sw.DICTVALUE = Request.Params["TID"];
            var model = T_SYS_DICTCls.getModel(sw);
            return Json(model);

        }
        #endregion
    }
}