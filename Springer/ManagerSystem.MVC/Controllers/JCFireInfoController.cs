using ManagerSystem.MVC.HelpCom;
using ManagerSystemClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using PublicClassLibrary;
using System.Web.Mvc;
using System.IO;
using ManagerSystemClassLibrary.SmsSendService;

namespace ManagerSystem.MVC.Controllers
{
    public class JCFireInfoController : BaseController
    {

        SmsSendServiceClient Smsclient = new SmsSendServiceClient();//短信服务
        //
        // GET: /JCFireInfo/

        /// <summary>
        /// 火情信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #region 转为火情监测表
        /// <summary>
        /// 获取火情信息
        /// </summary>
        /// <returns></returns>
        public JsonResult FireHtmlIndex()
        {
            Message ms = null;
            var model = new JC_FIRE_Model();//火情信息
            var firetype = Request.Params["firetype"];//火情来源类型
            var id = Request.Params["id"];//原始记录序号（主键）
            string typeName = Enum.Parse(typeof(EnumType), firetype.ToString()).ToString();//获取来源名称
            model.FIREFROM = firetype;//火情来源
            model.FIREFROMID = id;//原始序号
            //红外相机 = 1,
            //卫星热点 = 2,
            //人工报警 = 3,
            //电子报警 = 4,
            //护林员火情上报 = 5
            if (firetype == "1")//红外相机 
            {
                var photo = JC_INFRAREDCAMERACls.getModelPhoto(new JC_INFRAREDCAMERA_PHOTO_SW() { smid = id });//主键获取图片信息Message
                if (photo != null)
                {
                    var info = JC_INFRAREDCAMERACls.getModel(new JC_INFRAREDCAMERA_BASICINFO_SW() { PHONE = photo.tpa });
                    model.JD = info.JD;
                    model.WD = info.WD;
                    model.FIREFROMID = id;
                    model.FIRENAME = info.INFRAREDCAMERANAME;//相机名称
                    model.ZQWZ = info.ADDRESS;//地址
                    model.FIRETIME = photo.recvdatetime;//处理时间
                    model.RECEIVETIME = photo.recvdatetime;//接收时间
                }
            }
            if (firetype == "4")//电子报警
            {
                var info = JC_MONITORCls.getModelMonitor(new JC_MONITOR_SW() { IMBID = id });
                var monitor = JC_MONITORCls.getModel(new JC_MONITOR_BASICINFO_SW() { TTBH = info.TTBH });
                model.JD = info.JD;
                model.WD = info.WD;
                model.FIREFROMID = id;
                model.FIRENAME = monitor.EMNAME;//名称
                //model.ZQWZ = info.ADDRESS;//地址
                model.RECEIVETIME = ClsSwitch.SwitTM(DateTime.Now.ToString());//接收时间
                model.FIRETIME = info.IMBTIME;//时间

            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" <div id=\"divfiremanager\" style=\"margin:10px\">");
            sb.AppendFormat("<table  class=\"table table-striped table-bordered table-hover\">");
            sb.AppendFormat("<tbody>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>火情名称:</label></td>");
            sb.AppendFormat("<td><input type=\"text\" id=\"firename\"  value='" + model.FIRENAME + "' /></td>");
            sb.AppendFormat("<td>  <label>火情来源:</label></td>");
            sb.AppendFormat("<td><input readonly=\"readonly\" type=\"text\" id=\"firefromname\"  value='" + typeName + "'/><input type=\"hidden\" id=\"firefrom\"  value='" + model.FIREFROM + "'/></td>");
            sb.AppendFormat("<td>  <label>时间:</label></td>");
            sb.AppendFormat("<td><input type=\"text\" id=\"firetime\" readonly=\"readonly\" value='" + model.FIRETIME + "' /></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            //sb.AppendFormat("<td><input type=\"text\" id=\"firetime\" onclick=\"laydate()\"  /><span style='color:red'>*</span></td>");
            // sb.AppendFormat("<td><input class=\"date-picker\" id=\"firetime\"   value=\"\" type=\"text\" data-date-format=\"yyyy-mm-dd\" /> </td>");
            sb.AppendFormat("<td>  <label>经度:</label></td>");
            sb.AppendFormat("<td><input  type=\"text\" id=\"jd\" value='" + model.JD + "' /></td>");
            sb.AppendFormat("<td><label>纬度:</label></td>");
            sb.AppendFormat("<td> <input type=\"text\" id=\"wd\" value='" + model.WD + "' /></td>");
            sb.AppendFormat("<td>  <label>面积（像素）:</label></td>");
            sb.AppendFormat("<td> <input type=\"text\" id=\"area\"/></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>火灾发生地:</label></td>");
            sb.AppendFormat("<td colspan=\"5\"> <input type=\"text\" id=\"fireaddress\"  value='" + model.ZQWZ + "' style=\"width:90%\" /></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>备注:</label></td>");
            sb.AppendFormat("<td colspan=\"5\"> <textarea id=\"firenote\" class=\"autosize-transition form-control\" style=\"overflow: auto; word-wrap: break-word; resize: horizontal; height: 40px;\"></textarea></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>地类:</label></td>");
            sb.AppendFormat("<td><select style=\"width: 60%;\" id=\"firedl\">");
            sb.AppendFormat("<option value=\"林地\">林地</option>");
            sb.AppendFormat("<option value=\"其他\">其他</option>");
            sb.AppendFormat("</select></td>");
            sb.AppendFormat("<td><label>烟云:</label></td>");
            sb.AppendFormat("<td><select  style=\"width: 60%;\" id=\"fireyy\">");
            sb.AppendFormat("<option value=\"无\">无</option>");
            sb.AppendFormat("<option value=\"是\">是</option>");
            sb.AppendFormat("<option value=\"否\">否</option>");
            sb.AppendFormat("</select></td>");
            sb.AppendFormat("<td><label>连续火:</label></td>");
            sb.AppendFormat("<td><select  style=\"width: 60%;\" id=\"firejxhqus\">");
            sb.AppendFormat("<option value=\"无\">无</option>");
            sb.AppendFormat("<option value=\"是\">是</option>");
            sb.AppendFormat("<option value=\"否\">否</option>");
            sb.AppendFormat("</select></td>");
            sb.AppendFormat("</tr>");

            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>记录编号:</label></td>");
            sb.AppendFormat("<td><input type=\"text\"  readonly=\"readonly\" id=\"fireid\" value='" + model.FIREFROMID + "' /></td>");
            sb.AppendFormat("<td>  <label>卫星编号:</label></td>");
            sb.AppendFormat("<td><input readonly=\"readonly\" type=\"text\" id=\"firewxno\"  value='" + model.WXBH + "'/></td>");
            sb.AppendFormat("<td>  <label>热点编号:</label></td>");
            sb.AppendFormat("<td> <input type=\"text\" readonly=\"readonly\" id=\"firehotno\" value='" + model.DQRDBH + "'/></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>处理类型:</label></td>");
            sb.AppendFormat("<td colspan=\"5\"> <input id=\"radio0\" type=\"radio\" name=\"identity\" value=\"0\" checked=\"checked\" /><lable for=\"radio0\">火情</lable>&nbsp;&nbsp;&nbsp;&nbsp;<input id=\"radio1\" type=\"radio\" name=\"identity\" value=\"1\" /><lable for=\"radio1\">非火情</lable></td>");
            sb.AppendFormat("</tr>");

            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            ms = new Message(true, sb.ToString(), typeName);
            return Json(ms);
        }

        /// <summary>
        /// 转为火情
        /// </summary>
        /// <returns></returns>
        public JsonResult ConvertFireInfo()
        {
            Message ms = null;
            string fireid = Request.Params["fireid"];//记录编号
            string firewxno = Request.Params["firewxno"];//卫星编号
            string firehotno = Request.Params["firehotno"];//热点编号
            string firename = Request.Params["firename"];//火情名称
            string firefrom = Request.Params["firefrom"];//火情来源
            string area = Request.Params["area"];//面积
            string firetime = Request.Params["firetime"];//起火时间
            string jd = Request.Params["jd"];//经度
            string wd = Request.Params["wd"];//纬度
            string firedl = Request.Params["firedl"];//地类
            string fireyy = Request.Params["fireyy"];//烟云
            string firejxhqus = Request.Params["firejxhqus"];//连续火
            string fireaddress = Request.Params["fireaddress"];//火灾发生地
            string firenote = Request.Params["firenote"];//备注
            string checktype = Request.Params["checktype"];//选择处理类型 0 为火情 1为非火情
            //红外相机 = 1,
            //卫星热点 = 2,
            //人工报警 = 3,
            //电子报警 = 4,
            //护林员火情上报 = 5
            string firetype = Request.Params["firetype"];//火情类型
            if (checktype == "0")
            {
                var model = new JC_FIRE_Model();
                model.opMethod = "Add";
                model.FIREFROMID = fireid;
                model.WXBH = firewxno;
                model.DQRDBH = firehotno;
                model.FIRENAME = firename;
                model.FIREFROM = firefrom;
                model.RSMJ = area;
                model.FIRETIME = firetime;
                model.JD = jd;
                model.WD = wd;
                model.DL = firedl;
                model.YY = fireyy;
                model.JXHQSJ = firejxhqus;
                model.ZQWZ = fireaddress;
                model.MARK = firenote;
                model.RECEIVETIME = DateTime.Now.ToString();//接收（上报）时间
                ms = JC_FIRECls.Manager(model);
                if (ms.Success == true)
                {
                    if (firetype == "1")//红外相机
                    {
                        var m = new JC_INFRAREDCAMERA_PHOTO_Model();
                        m.opMethod = "Man";
                        m.smid = fireid;
                        m.MANTIME = DateTime.Now.ToString();
                        m.MANUSERID = SystemCls.getUserID();
                        m.MANSTATE = "2";
                        m.MANRESULT = "已转为火情";
                        var msg = JC_INFRAREDCAMERACls.ManagerPhoto(m);//更新红外相机状态
                    }
                    if (firetype == "4")//电子监控
                    {
                        var m = new JC_MONITOR_Model();
                        m.opMethod = "Man";
                        m.IMBID = fireid;
                        m.MANTIME = DateTime.Now.ToString();
                        m.MANUSERID = SystemCls.getUserID();
                        m.MANSTATE = "2";
                        m.MANRESULT = "已转为火情";
                        var msg = JC_MONITORCls.ManagerMonitor(m);//更新电子监控状态
                    }
                }
            }
            else
            {
                if (firetype == "1")//红外相机
                {
                    var m = new JC_INFRAREDCAMERA_PHOTO_Model();
                    m.opMethod = "Man";
                    m.smid = fireid;
                    m.MANTIME = DateTime.Now.ToString();
                    m.MANUSERID = SystemCls.getUserID();
                    m.MANSTATE = "1";
                    m.MANRESULT = "已处理";
                    ms = JC_INFRAREDCAMERACls.ManagerPhoto(m);//更新红外相机状态
                }
                if (firetype == "4")//电子监控
                {
                    var m = new JC_MONITOR_Model();
                    m.opMethod = "Man";
                    m.IMBID = fireid;
                    m.MANTIME = DateTime.Now.ToString();
                    m.MANUSERID = SystemCls.getUserID();
                    m.MANSTATE = "1";
                    m.MANRESULT = "已处理";
                    ms = JC_MONITORCls.ManagerMonitor(m);//更新电子监控状态
                }
            }
            return Json(ms);
        }

        #endregion


        #region 火情反馈表
        /// <summary>
        /// 火情反馈html
        /// </summary>
        /// <returns></returns>
        public JsonResult FireHtmlFKIndex()
        {
            var strs = "&#123istime: true, format: 'YYYY-MM-DD hh:mm:ss'&#125";
            var jcfid = Request.Params["jcfid"];//监测火情id
            var record = JC_FIRETICKLINGCls.GetFKFireInfoData(jcfid);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" <div class=\"divMan\" id=\"divfkfiremanager\" style=\"margin:10px\">");
            sb.AppendFormat("<table cellspacing=\"0\" cellpadding=\"0\">");
            sb.AppendFormat("<tbody>");
            if (record.JC_FireData.FIREFROM == "2")//卫星热点头信息
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td><label>热点编号:</label></td>");
                sb.AppendFormat("<td><input type=\"text\" readonly=\"readonly\" value='" + record.JC_FireData.DQRDBH + "' /></td>");
                sb.AppendFormat("<td>  <label>热点区域:</label></td>");
                sb.AppendFormat("<td><input readonly=\"readonly\" type=\"text\" value='" + record.JC_FireData.ZQWZ + "'/></td>");
                sb.AppendFormat("<td>  <label>卫星编号:</label></td>");
                sb.AppendFormat("<td><input type=\"text\"   readonly=\"readonly\" value='" + record.JC_FireData.WXBH + "' /></td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td><label>面积（像素）:</label></td>");
                sb.AppendFormat("<td><input type=\"text\" readonly=\"readonly\" value='" + record.JC_FireData.RSMJ + "' /></td>");
                sb.AppendFormat("<td>  <label>经度:</label></td>");
                sb.AppendFormat("<td><input readonly=\"readonly\" type=\"text\" value='" + record.JC_FireData.JD + "'/></td>");
                sb.AppendFormat("<td>  <label>纬度:</label></td>");
                sb.AppendFormat("<td><input type=\"text\"   readonly=\"readonly\" value='" + record.JC_FireData.WD + "' /></td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td><label>接收时间:</label></td>");
                sb.AppendFormat("<td><input type=\"text\" style=\"width:160px;\" readonly=\"readonly\" value='" + record.JC_FireData.RECEIVETIME + "' /></td>");
                sb.AppendFormat("<td>  <label>下发时间:</label></td>");
                sb.AppendFormat("<td><input readonly=\"readonly\"  style=\"width:160px;\" type=\"text\" value='" + record.JC_FireData.ISSUEDTIME + "'/></td>");
                sb.AppendFormat("<td colspan=\"2\"></td>");
                sb.AppendFormat("</tr>");
            }
            else if (record.JC_FireData.FIREFROM == "3")//电话报警
            {
                var info = JC_PERALARMCls.getModel(new JC_PERALARM_SW { PERALARMID = record.JC_FireData.FIREFROMID });
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td><label>电话火情名称:</label></td>");
                sb.AppendFormat("<td><input type=\"text\" readonly=\"readonly\" value='" + info.FIRENAME + "' /></td>");
                sb.AppendFormat("<td>  <label>发生区域:</label></td>");
                sb.AppendFormat("<td><input readonly=\"readonly\" type=\"text\" value='" + info.PERALARMADDRESS + "'/></td>");
                sb.AppendFormat("<td>  <label>发生地:</label></td>");
                sb.AppendFormat("<td><input type=\"text\"   readonly=\"readonly\" value='" + info.ORGNAME + "' /></td>");
                sb.AppendFormat("<td>  <label>报警时间:</label></td>");
                sb.AppendFormat("<td><input type=\"text\"   readonly=\"readonly\" value='" + info.PERALARMTIME + "' /></td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td><label>报警人:</label></td>");
                sb.AppendFormat("<td><input type=\"text\" readonly=\"readonly\" value='" + info.PERALARMNAME + "' /></td>");
                sb.AppendFormat("<td><label>电话:</label></td>");
                sb.AppendFormat("<td><input type=\"text\" readonly=\"readonly\" value='" + info.PERALARMPHONE + "' /></td>");
                sb.AppendFormat("<td>  <label>经度:</label></td>");
                sb.AppendFormat("<td><input readonly=\"readonly\" type=\"text\" value='" + info.JD + "'/></td>");
                sb.AppendFormat("<td>  <label>纬度:</label></td>");
                sb.AppendFormat("<td><input type=\"text\"   readonly=\"readonly\" value='" + info.WD + "' /></td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td><label>报警内容:</label></td>");
                sb.AppendFormat("<td  colspan=\"7\"><textarea  readonly=\"readonly\" style=\"width:690px;height:52px\">" + info.PERALARMCONTENT + "</textarea></td>");
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("<br/>");
            sb.AppendFormat("<table cellspacing=\"0\" cellpadding=\"0\">");
            sb.AppendFormat("<tbody>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>火情实际发生地址:</label></td>");
            sb.AppendFormat("<td><input type=\"text\" id=\"hqaddress\"  style=\"width:250px\" value='" + record.JC_FireFKData.ADDRESS + "'/></td>");
            sb.AppendFormat("<td><label>经度:</label></td>");
            sb.AppendFormat("<td><input  type=\"text\"  id=\"hqjd\" style=\"width:100px\"  value='" + record.JC_FireFKData.JD + "'/></td>");
            sb.AppendFormat("<td><label >纬度:</label></td>");
            sb.AppendFormat("<td><input type=\"text\" id=\"hqwd\"  style=\"width:100px\"  value='" + record.JC_FireFKData.WD + "'/><input type=\"button\" class=\"btnMapLoaclCss\"  style=\"width:50px\" value=\"定位\" onClick=\"setPoint('" + record.JC_FireFKData.JD + "','" + record.JC_FireFKData.WD + "')\"/></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>地类:</label></td>");
            sb.AppendFormat("<td><select   id=\"dl\">");
            sb.AppendFormat("{0}", GetSelectHtml("地类", record.JC_FireFKData.DL));
            sb.AppendFormat("</select></td>");
            sb.AppendFormat("<td>  <label>林区名称:</label></td>");
            sb.AppendFormat("<td><input  type=\"text\" id=\"forestname\"  value='" + record.JC_FireFKData.FORESTNAME + "'/></td>");
            sb.AppendFormat("<td><label>林火类别:</label></td>");
            sb.AppendFormat("<td><select   id=\"forestfiretype\">");
            sb.AppendFormat("{0}", GetSelectHtml("林火类别", record.JC_FireFKData.FORESTFIRETYPE));
            sb.AppendFormat("</select></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>可燃物类别:</label></td>");
            sb.AppendFormat("<td><select  id=\"fueltype\">");
            sb.AppendFormat("{0}", GetSelectHtml("可燃物类别", record.JC_FireFKData.FUELTYPE));
            sb.AppendFormat("</select></td>");
            sb.AppendFormat("<td><label>热点类别:</label></td>");
            sb.AppendFormat("<td><select  id=\"hottype\" onChange=\"setReport()\">");
            sb.AppendFormat("{0}", GetSelectHtml("热点类别", record.JC_FireFKData.HOTTYPE));
            sb.AppendFormat("</select></td>");
            sb.AppendFormat("<td>  <label>核查日期:</label></td>");
            if (string.IsNullOrEmpty(record.JC_FireFKData.CHECKTIME))
            {
                sb.AppendFormat("<td><input type=\"text\"  id=\"checktime\" onclick=\"laydate(" + strs + ")\" value='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'/></td>");
            }
            else
            {
                sb.AppendFormat("<td><input type=\"text\"  id=\"checktime\" onclick=\"laydate(" + strs + ")\" value='" + ClsSwitch.SwitTM(record.JC_FireFKData.CHECKTIME) + "'/></td>");
            }
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>烟云:</label></td>");
            sb.AppendFormat("<td><select  id=\"yy\">");
            sb.AppendFormat("{0}", GetSelectHtml("烟云类别", record.JC_FireFKData.YY));
            sb.AppendFormat("</select></td>");
            sb.AppendFormat("<td>  <label>是否连续:</label></td>");
            sb.AppendFormat("<td><select  id=\"jxhqsj\">");
            sb.AppendFormat("{0}", GetSelectHtml("是否连续", record.JC_FireFKData.JXHQSJ));
            sb.AppendFormat("</select></td>");
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>起火时间:</label></td>");
            sb.AppendFormat("<td><input type=\"text\" id=\"firebegintime\" onclick=\"laydate(" + strs + ")\" value='" + record.JC_FireFKData.FIREBEGINTIME + "' /></td>");
            sb.AppendFormat("<td>  <label>灭火时间:</label></td>");
            sb.AppendFormat("<td><input  type=\"text\"  id=\"fireendtime\"  onclick=\"laydate(" + strs + ")\" value='" + record.JC_FireFKData.FIREENDTIME + "'/></td>");
            var bo = PublicCls.OrgIsShi(SystemCls.getCurUserOrgNo());
            if (bo)
            {
                sb.AppendFormat("<td><label for=\"chk\">是否已灭:</label></td>");
                if (record.JC_FireFKData.ISOUTFIRE == "1")
                {
                    sb.AppendFormat("<td><input type=\"checkbox\" id=\"chk\" checked=\"checked\" ></td>");
                }
                else
                {
                    sb.AppendFormat("<td><input type=\"checkbox\" id=\"chk\" ></td>");
                }
            }
            else
            {
                sb.AppendFormat("<td style=\"display:none;\"><label for=\"chk\">是否已灭:</label></td>");
                if (record.JC_FireFKData.ISOUTFIRE == "1")
                {
                    sb.AppendFormat("<td style=\"display:none;\"><input type=\"checkbox\" id=\"chk\" checked=\"checked\" ></td>");
                }
                else
                {
                    sb.AppendFormat("<td style=\"display:none;\"><input type=\"checkbox\" id=\"chk\" ></td>");
                }
            }

            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>过火面积:</label></td>");
            sb.AppendFormat("<td><input type=\"text\" id=\"burnedarea\"  value='" + record.JC_FireFKData.BURNEDAREA + "' style=\"width:120px\"/>(公顷)</td>");
            sb.AppendFormat("<td>  <label>过火林地面积:</label></td>");
            sb.AppendFormat("<td><input  type=\"text\"  id=\"overdoarea\"   value='" + record.JC_FireFKData.OVERDOAREA + "' style=\"width:120px\"/>(公顷)</td>");
            sb.AppendFormat("<td><label >受害森林面积:</label></td>");
            sb.AppendFormat("<td><input type=\"text\" id=\"lostforestarea\"  value='" + record.JC_FireFKData.LOSTFORESTAREA + "' style=\"width:120px\">(公顷)</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>情况简介:</label></td>");
            sb.AppendFormat("<td colspan=\"5\"><textarea id=\"fireintro\" style=\"width:100%;height:70px\"> " + record.JC_FireFKData.FIREINTRO + "</textarea></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>其他损失情况:</label></td>");
            sb.AppendFormat("<td colspan=\"5\"><textarea id=\"elselossintro\" style=\"width:100%;height:40px\"> " + record.JC_FireFKData.ELSELOSSINTRO + "</textarea></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            if (record.JC_FireFKData.MANSTATE == "34" || record.JC_FireFKData.MANSTATE == "33"
                || record.JC_FireFKData.MANSTATE == "51")//51 县审核不通过 34市审核不通过   未通过＝33 县级本单位处理
            {
                sb.AppendFormat("<td><label>审核意见:</label></td>");
                sb.AppendFormat("<td>不通过</td>");
                sb.AppendFormat("<td colspan=\"4\">原因:<input type=\"text\" readonly=\"readonly\" id=\"shyjyy\" style=\"width:60%\" value='" + record.JC_FireFKData.AUDITREASON + "'/></td>");
                sb.AppendFormat("</tr>");
            }
            else
            {
                if (!PublicCls.OrgIsZhen(SystemCls.getCurUserOrgNo()))
                {
                    //  var bo = PublicCls.OrgIsShi(SystemCls.getCurUserOrgNo());//市
                    // var bb = PublicCls.OrgIsXian(SystemCls.getCurUserOrgNo());//县
                    if (record.JC_FireData.PFFLAG == "2" || record.JC_FireData.PFFLAG == "1")// "2 为当县级本单位处理时不需要审核项 1 为市级本单位"
                    {
                    }
                    else
                    {
                        sb.AppendFormat("<tr>");
                        sb.AppendFormat("<td><label>审核意见:</label></td>");
                        sb.AppendFormat("<td><input type=\"radio\" id=\"yjone\" name=\"shyj\" value=\"1\" checked=\"checked\" onchange=\"shyjChange()\" /><lable for=\"yjone\">通过</lable><input type=\"radio\" name=\"shyj\" id=\"yjtwo\" value=\"0\" onchange=\"shyjChange()\"/><lable for=\"yjtwo\">不通过</lable></td>");
                        sb.AppendFormat("<td colspan=\"4\"><div id=\"shyjdiv\" style=\"display:none;\">原因:<input type=\"text\" id=\"shyjyy\" style=\"width:60%\"/></div></td>");
                        sb.AppendFormat("</tr>");
                    }
                }
            }
            sb.AppendFormat("<tr id=\"trreport\" style=\"display:none;\">");
            sb.AppendFormat("<td><label>火情报告上传:</label></td>");
            sb.AppendFormat("<td colspan=\"2\"> <input type=\"hidden\" id=\"txtFileName\" /><form id=\"uploadForm\" enctype=\"multipart/form-data\"><input type=\"file\" name=\"uploadify\" id=\"uploadify\" onchange=\"CheckUploadType()\" /> <input type=\"button\" value=\"上传\" class=\"btnUploadCss\" onclick=\"upload(" + record.JC_FireData.JCFID + ")\"></form></td>");
            sb.AppendFormat("<td style=\"width:10px;\"><label id=\"lblInfo\" style=\"color:red;\"></label></td>");
            sb.AppendFormat("<td><a style =\"color:green;\" href=\"#\" onclick=\"FireReport({0})\">【查看已上传报告】</a></td>", record.JC_FireData.JCFID);
            //<a style =\"color:blue;\" href=\"#\" onClick=\"FireReportEdit()\">【报告在线编辑】</a>
            sb.AppendFormat("<td><a style =\"color:red;\" href=\"/UploadFile/MBDoc/firereportmb.doc\">【火情模板下载】</a></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td><label>填报单位:</label></td>");
            sb.AppendFormat("<td><input readonly=\"readonly\" type=\"text\" value='" + record.OrgName + "'/></td>");
            sb.AppendFormat("<td><label>填报人:</label></td>");
            sb.AppendFormat("<td><input readonly=\"readonly\" type=\"text\" value='" + record.UserName + "'/></td>");
            sb.AppendFormat("<td colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            //sb.AppendFormat("<div class=\"divMan\" id=\"tablereport\" style=\"display:none;margin-left:5px;margin-top:8px\"></div>");

            var ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }


        #region Private
        private string GetSelectHtml(string typename, string value)
        {
            var str = T_SYS_DICTCls.getSelectOption(new T_SYS_DICTSW() { DICTTYPENAME = typename, DICTVALUE = value });
            return str;
        }
        #endregion

        #endregion


        #region 市局签收 弃用

        //public JsonResult getSJQSSelect()
        //{
        //    var jcfid = Request.Params["jcfid"];//监测火情id
        //    var record = JC_FIRETICKLINGCls.GetFKFireInfoData(jcfid);
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendFormat("<div id=\"divselect\" >");
        //    sb.AppendFormat("    <div style=\"float:left;\"><label for=\"QSselect\">签收单位：</label></div>");
        //    sb.AppendFormat("    <div style=\"float:left;width:200px\">");
        //    sb.AppendFormat("        <select class=\"form-control\" id=\"QSselect\">");

        //    //string curorgno = SystemCls.getCurUserOrgNo();
        //    //var sw = new T_SYS_ORGSW();
        //    //sw.GetContyORGNOByCity = curorgno;
        //    //var str = T_SYS_ORGCls.getSelectOptionByCity(sw);

        //    sb.AppendFormat("{0}", T_SYS_ORGCls.getSelectOptionByCity(new T_SYS_ORGSW { GetContyORGNOByCity = SystemCls.getCurUserOrgNo() }));
        //    sb.AppendFormat("        </select>");
        //    sb.AppendFormat("    </div>");
        //    sb.AppendFormat("</div>");

        //    var ms = new Message(true, sb.ToString(), "");
        //    return Json(ms);
        // }
        #endregion

        /// <summary>
        /// 获取系统用户
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSYSUserIndex()
        {
            return View();
        }
        /// <summary>
        /// 获取护林员用户
        /// </summary>
        /// <returns></returns>
        public ActionResult GetHLYUserIndex()
        {
            return View();
        }
        /// <summary>
        /// 获取通讯录用户
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTXLUserIndex()
        {
            return View();
        }

        /// <summary>
        /// 派发核查单位Json
        /// </summary>
        /// <returns></returns>
        public string PFCheckOrgJson()
        {
            var sw = new T_SYS_ORGSW();
            var curorgno = SystemCls.getCurUserOrgNo();
            bool bb = PublicCls.OrgIsShi(curorgno);
            if (bb)
            {
                sw.GetContyORGNOByCity = curorgno;
            }
            else
            {
                sw.GetXZOrgNOByConty = curorgno;
            }
            var str = T_SYS_ORGCls.getOrgJsonStr(sw);
            return str;
        }


        #region Ajax

        #region 火情报告文件上传
        public JsonResult DocUpload()
        {
            Message ms = null;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string jcfid = Request.Params["jcfid"];
            string Path = "";
            string[] arr = hfc[0].FileName.Split('.');
            if (string.IsNullOrEmpty(hfc[0].FileName))
                return Json(new Message(false, "请选择附件！", ""));
            if (arr[arr.Length - 1].ToLower() != "doc" && arr[arr.Length - 1].ToLower() != "docx")
                return Json(new Message(false, "上传文件类型错误，请重新上传！", ""));
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss.") + arr[arr.Length - 1];
            var ipath = System.Configuration.ConfigurationManager.AppSettings["FireReportPath"].ToString();//上传火情报告
            if (!Directory.Exists(Server.MapPath(ipath)))
            {
                Directory.CreateDirectory(Server.MapPath(ipath));
            }
            Path = ipath + "/" + filename;// hfc[i].FileName;
            string PhysicalPath = Server.MapPath(Path);
            hfc[0].SaveAs(PhysicalPath);
            //保存库操作
            var sw = new JC_FIRE_REPORT_SW();
            sw.UPLOADORGNO = SystemCls.getCurUserOrgNo();
            sw.UPLOADTIME = ClsSwitch.SwitTM(DateTime.Now.ToString());
            sw.UPLOADUSERID = SystemCls.getUserID();
            sw.FILEURL = Path;
            sw.FILESIZE = (hfc[0].ContentLength / 1024).ToString();
            sw.FILENAME = System.IO.Path.GetFileName(hfc[0].FileName);
            sw.OWERJCFID = jcfid;
            sw.opMethod = "Add";
            var mm = JC_FIRE_REPORTCls.Manager(sw);
            if (mm.Success)
            {
                ms = new Message(true, filename, "");
            }
            else
            {
                ms = new Message(false, "保存库出错", "");
            }
            return Json(ms);
        }
        #endregion

        /// <summary>
        /// 派发单位核查记录
        /// </summary>
        /// <returns></returns>
        public JsonResult PFCheckMethod()
        {
            Message ms = null;
            string jcfid = Request.Params["jcfid"];
            string pforgstr = Request.Params["orgstr"];//派发单位（逗号分隔）
            if (string.IsNullOrEmpty(jcfid))
            {
                ms = new Message(false, "jcfid参数传递缺失", "");
                return Json(ms);
            }
            var record = JC_FIRECls.GetListModel(new JC_FIRE_SW { JCFID = jcfid }).FirstOrDefault();//获取主火情信息
            if (record != null)
            {
                var arrorg = pforgstr.Split(',');
                foreach (var item in arrorg)
                {
                    var m = new JC_FIRE_Model();
                    m.InjectFrom(record);
                    m.opMethod = "Add";
                    m.OWERJCFID = jcfid;
                    m.BYORGNO = item;
                    m.PFUSERID = SystemCls.getUserID();
                    m.PFORGNO = SystemCls.getCurUserOrgNo();
                    m.PFTIME = ClsSwitch.SwitTM(DateTime.Now.ToString());
                    m.MANSTATE = "0";
                    ms = JC_FIRECls.Manager(m);
                }
            }
            else
            {
                ms = new Message(false, "派发主火情信息没有记录", "");
            }
            return Json(ms);
        }

        /// <summary>
        /// 市（州）局签收
        /// </summary>
        /// <returns></returns>
        public JsonResult CityQSMethod()
        {
            Message ms = null;
            string jcfid = Request.Params["jcfid"];
            if (string.IsNullOrEmpty(jcfid))
            {
                ms = new Message(false, "监测火情jcfid缺失", "");
            }
            else
            {
                var sw = new JC_FIRETICKLING_SW();
                sw.opMethod = "Add";
                sw.JCFID = jcfid;
                sw.MANSTATE = "1";//1为市（州）局签收 2 为县局签收 3 为乡镇签收
                sw.MANTIME = ClsSwitch.SwitTM(DateTime.Now.ToString());
                sw.BYORGNO = SystemCls.getCurUserOrgNo();
                sw.MANUSERID = SystemCls.getUserID();
                ms = JC_FIRETICKLINGCls.Manager(sw);
            }
            return Json(ms);
        }

        /// <summary>
        /// 市（州）局签收派发下属单位
        /// </summary>
        /// <returns></returns>
        public JsonResult CityQSPFMethod()
        {
            Message ms = null;
            string jcfid = Request.Params["jcfid"];
            string selecttype = Request.Params["type"];
            if (string.IsNullOrEmpty(jcfid))
            {
                ms = new Message(false, "监测火情jcfid缺失", "");
            }
            else
            {
                string qstype = "1";//市签收
                var m = new JC_FIRE_Model();
                m.PFTIME = ClsSwitch.SwitTM(DateTime.Now.ToString());
                if (selecttype == "0")//下级单位核查 则需要变革火情监测表中的机构编码
                {
                    string orgno = Request.Params["orgno"];
                    m.opMethod = "Mdy";
                    m.BYORGNO = orgno;
                    m.JCFID = jcfid;
                    m.MANSTATE = "0";
                    m.LASTPROCESSTIME = ClsSwitch.SwitTM(DateTime.Now.ToString());
                }
                else if (selecttype == "1") //本级单位处置
                {
                    qstype = "1";//本级单位处理
                    m.opMethod = "Mdy";
                    m.BYORGNO = SystemCls.getCurUserOrgNo();
                    m.JCFID = jcfid;
                    m.PFFLAG = "1";//2 为县级本单位处理 1 位本市级单位处理
                    m.MANSTATE = "11";
                    m.LASTPROCESSTIME = ClsSwitch.SwitTM(DateTime.Now.ToString());
                }
                var msg = JC_FIRECls.Manager(m);
                var sw = new JC_FIRETICKLING_SW();
                sw.opMethod = "Add";
                sw.JCFID = jcfid;
                sw.MANSTATE = qstype;//1为市（州）局签收 2 为县局签收 3 为乡镇签收  32  为县本单位反馈（处理）//4市级本单位处理
                sw.MANTIME = ClsSwitch.SwitTM(DateTime.Now.ToString());
                sw.BYORGNO = SystemCls.getCurUserOrgNo();
                sw.MANUSERID = SystemCls.getUserID();
                ms = JC_FIRETICKLINGCls.Manager(sw);
            }
            return Json(ms);

        }

        /// <summary>
        /// 县级签收(派发核查人)
        /// </summary>
        /// <returns></returns>
        public JsonResult ContyQSMethod()
        {
            Message ms = null;
            string jcfid = Request.Params["jcfid"];
            string selecttype = Request.Params["type"];
            if (string.IsNullOrEmpty(jcfid))
            {
                ms = new Message(false, "监测火情jcfid缺失", "");
            }
            else
            {
                string qstype = "2";//县签收
                var m = new JC_FIRE_Model();
                if (selecttype == "0")//下级单位核查 则需要变革火情监测表中的机构编码
                {
                    string orgno = Request.Params["orgno"];
                    m.opMethod = "Mdy";
                    m.BYORGNO = orgno;
                    m.JCFID = jcfid;
                    m.MANSTATE = "0";
                    m.LASTPROCESSTIME = ClsSwitch.SwitTM(DateTime.Now.ToString());
                }
                else if (selecttype == "1") //本级单位处置
                {
                    qstype = "32";//本级单位处理
                    m.opMethod = "Mdy";
                    m.BYORGNO = SystemCls.getCurUserOrgNo();
                    m.JCFID = jcfid;
                    m.PFFLAG = "2";//2 为县级本单位处理
                    m.MANSTATE = "0";
                    m.LASTPROCESSTIME = ClsSwitch.SwitTM(DateTime.Now.ToString());
                }
                var msg = JC_FIRECls.Manager(m);
                var sw = new JC_FIRETICKLING_SW();
                sw.opMethod = "Add";
                sw.JCFID = jcfid;
                sw.MANSTATE = qstype;//1为市（州）局签收 2 为县局签收 3 为乡镇签收  32  为县本单位反馈（处理）
                sw.MANTIME = ClsSwitch.SwitTM(DateTime.Now.ToString());
                sw.BYORGNO = SystemCls.getCurUserOrgNo();
                sw.MANUSERID = SystemCls.getUserID();
                ms = JC_FIRETICKLINGCls.Manager(sw);
            }
            return Json(ms);

        }

        /// <summary>
        /// 县级签收(只签收)
        /// </summary>
        /// <returns></returns>
        public JsonResult ContyOnlyQSMethod()
        {
            Message ms = null;
            string jcfid = Request.Params["jcfid"];
            if (string.IsNullOrEmpty(jcfid))
            {
                ms = new Message(false, "监测火情jcfid缺失", "");
            }
            else
            {
                string qstype = "2";//县签收
                var sw = new JC_FIRETICKLING_SW();
                sw.opMethod = "Add";
                sw.JCFID = jcfid;
                sw.MANSTATE = qstype;//1为市（州）局签收 2 为县局签收 3 为乡镇签收  32  为县本单位反馈（处理）
                sw.MANTIME = ClsSwitch.SwitTM(DateTime.Now.ToString());
                sw.BYORGNO = SystemCls.getCurUserOrgNo();
                sw.MANUSERID = SystemCls.getUserID();
                ms = JC_FIRETICKLINGCls.Manager(sw);
            }
            return Json(ms);
        }

        /// <summary>
        /// 乡镇签收
        /// </summary>
        /// <returns></returns>
        public JsonResult XzQSMethod()
        {
            Message ms = null;
            string jcfid = Request.Params["jcfid"];
            string fsperson = Request.Params["fsperson"];
            string xgperson = Request.Params["xgperson"];
            if (string.IsNullOrEmpty(jcfid))
            {
                ms = new Message(false, "监测火情jcfid缺失", "");
            }
            else if (string.IsNullOrEmpty(fsperson))
            {
                ms = new Message(false, "发送人员fsperson缺失", "");
            }
            else
            {
                var sw = new JC_FIRETICKLING_SW();
                sw.opMethod = "Add";
                sw.JCFID = jcfid;
                sw.MANSTATE = "3";//1为市（州）局签收 2 为县局签收 3 为乡镇签收 4  为县反馈（审核）
                sw.MANTIME = ClsSwitch.SwitTM(DateTime.Now.ToString());
                sw.BYORGNO = SystemCls.getCurUserOrgNo();
                sw.MANUSERID = SystemCls.getUserID();
                ms = JC_FIRETICKLINGCls.Manager(sw);
                #region 发送短信通知
                var fireinfo = JC_FIRECls.getModel(new JC_FIRE_SW { JCFID = jcfid });//获取火情信息
                if (fireinfo != null)
                {
                    var fsarr = fsperson.Split(',');//护林员
                    string hlyMobile = "";
                    string hlyContent = "";
                    hlyContent = string.Format("现有热点：经度{0},纬度{1},起火时间{2},位于{3}区域,火点来源为{4},请速去核查！", fireinfo.JD, fireinfo.WD, Convert.ToDateTime(fireinfo.FIRETIME).ToString("yyyy-MM-dd HH:mm:ss"), fireinfo.ZQWZ, Enum.GetName(typeof(EnumType), fireinfo.FIREFROM));
                    foreach (var item in fsarr)
                    {
                        var phoneno = T_IPSFR_USERCls.getModel(new T_IPSFR_USER_SW { HID = item }).PHONE;
                        if (!string.IsNullOrEmpty(phoneno))
                        {
                            //todo sendmsg
                            //Smsclient.SendMsg();
                            hlyMobile += phoneno.Trim() + ",";
                        }
                    }
                    var mmhly = Smsclient.SendMsg(hlyContent, hlyMobile);//护林员发送短信
                    if (mmhly.Success == true)
                    {
                        ms = new Message(true, mmhly.Msg, "");
                    }
                    if (!string.IsNullOrEmpty(xgperson))
                    {
                        string txlMobile = "";
                        var xgarr = fsperson.Split(',');//通讯录
                        foreach (var item in xgarr)
                        {
                            var phoneno = T_SYS_ADDREDDBOOKCls.getModel(new T_SYS_ADDREDDBOOK_SW { ADID = item }).PHONE;
                            if (!string.IsNullOrEmpty(phoneno))
                            {
                                //todo sendmsg
                                txlMobile += phoneno.Trim() + ",";
                            }
                        }
                        var mmtxl = Smsclient.SendMsg(hlyContent, txlMobile);//护林员发送短信
                        if (mmtxl.Success == true)
                        {
                            ms = new Message(true, mmtxl.Msg, "");
                        }
                    }
                }
                #endregion

            }
            return Json(ms);
        }

        #region 市局签收派发Html
        public JsonResult getSJQSSelect()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div id=\"divselect\" >");
            sb.AppendFormat("<table>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>");
            sb.AppendFormat(" <input type=\"radio\" id=\"rad1\" name=\"radhc\" value=\"0\" checked=\"checked\" /><label for=\"rad1\">下属单位</label>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("<td colspan=\"2\">");
            sb.AppendFormat(" <select class=\"form-control\" id=\"QSselect\">");
            sb.AppendFormat("{0}", T_SYS_ORGCls.getSelectOptionByCity(new T_SYS_ORGSW { GetContyORGNOByCity = SystemCls.getCurUserOrgNo() }));
            sb.AppendFormat("</select>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>");
            sb.AppendFormat(" <input type=\"radio\" id=\"rad2\" name=\"radhc\" value=\"1\" /><label for=\"rad2\">本级单位</label>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("<td colspan=\"2\">人员：<input type=\"hidden\" id=\"hidtxt\"/><input type=\"text\"  readonly=\"readonly\" id=\"txtperson\" style=\"width:60%;\"/><a style=\"color: black;\" onClick=\"SelctOrgPeron(" + SystemCls.getCurUserOrgNo() + ")\"><em>人员选择</em></a></td>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td colspan=\"3\">相关人员：<input type=\"hidden\" id=\"hidxgtxt\"/><input type=\"text\"  readonly=\"readonly\" id=\"txtxgperson\" style=\"width:60%;\"/><a style=\"color: black;\" onClick=\"SelctTXLPeron(" + SystemCls.getCurUserOrgNo() + ")\"><em>人员选择</em></a></td>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            var ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }
        #endregion
        #region 县局签收派发Html
        public JsonResult getSXJQSSelect()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div id=\"divselect\" >");
            sb.AppendFormat("<table>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>");
            sb.AppendFormat(" <input type=\"radio\" id=\"rad1\" name=\"radhc\" value=\"0\" checked=\"checked\" /><label for=\"rad1\">下属单位</label>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("<td colspan=\"2\">");
            sb.AppendFormat(" <select class=\"form-control\" id=\"QSselect\">");
            sb.AppendFormat("{0}", T_SYS_ORGCls.getSelectOptionByCity(new T_SYS_ORGSW { GetXZOrgNOByConty = SystemCls.getCurUserOrgNo() }));
            sb.AppendFormat("</select>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>");
            sb.AppendFormat(" <input type=\"radio\" id=\"rad2\" name=\"radhc\" value=\"1\" /><label for=\"rad2\">本级单位</label>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("<td colspan=\"2\">人员：<input type=\"hidden\" id=\"hidtxt\"/><input type=\"text\"  readonly=\"readonly\" id=\"txtperson\" style=\"width:60%;\"/><a style=\"color: black;\" onClick=\"SelctOrgPeron(" + SystemCls.getCurUserOrgNo() + ")\"><em>人员选择</em></a></td>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td colspan=\"3\">相关人员：<input type=\"hidden\" id=\"hidxgtxt\"/><input type=\"text\"  readonly=\"readonly\" id=\"txtxgperson\" style=\"width:60%;\"/><a style=\"color: black;\" onClick=\"SelctTXLPeron(" + SystemCls.getCurUserOrgNo() + ")\"><em>人员选择</em></a></td>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            var ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }
        #endregion

        #region 乡镇签收派发Html
        public JsonResult getXZJQSSelect()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div id=\"divselect\" >");
            sb.AppendFormat("<table style=\"width:100%\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>护林员：<input type=\"hidden\" id=\"hidtxt\"/><input type=\"text\"  readonly=\"readonly\" id=\"txtperson\" style=\"width:60%;\"/><a style=\"color: black;\" onClick=\"SelctHLYPerson()\"><em>人员选择</em></a></td>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td>相关人员：<input type=\"hidden\" id=\"hidxgtxt\"/><input type=\"text\"  readonly=\"readonly\" id=\"txtxgperson\" style=\"width:60%;\"/><a style=\"color: black;\" onClick=\"SelctTXLPeron(" + SystemCls.getCurUserOrgNo() + ")\"><em>人员选择</em></a></td>");
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            sb.AppendFormat("</div>");
            var ms = new Message(true, sb.ToString(), "");
            return Json(ms);
        }
        #endregion

        /// <summary>
        /// 获取系统人员
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSYSPerson()
        {
            // Message ms = null;
            string orgno = Request.Params["orgno"];
            string id = Request.Params["id"];

            if (string.IsNullOrEmpty(id))
            {
                id = SystemCls.getCurUserOrgNo();
            }
            var str = T_SYSSEC_IPSUSERCls.getSystemUserTree(new T_SYSSEC_IPSUSER_SW { curOrgNo = id });
            // ms = new Message(true, str.ToString(), "");
            return Content(str.ToString(), "application/json");
            // return Json(ms);
        }

        /// <summary>
        /// 获取护林人员
        /// </summary>
        /// <returns></returns>
        public ActionResult GetHlyPerson()
        {
            //Message ms = null;
            string orgno = Request.Params["orgno"];
            if (string.IsNullOrEmpty(orgno))
            {
                orgno = SystemCls.getCurUserOrgNo();
            }
            var str = T_IPSFR_USERCls.getTree(new T_IPSFR_USER_SW { BYORGNO = orgno });
            //ms = new Message(true, str.ToString(), "");
            return Content(str.ToString(), "application/json");
            // return Json(ms);
        }


        /// <summary>
        /// 获取坐标点
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMapPontIndex()
        {
            string jd = Request.Params["jd"];
            string wd = Request.Params["wd"];
            ViewBag.type = "1";
            ViewBag.method = "getLocaCollectPont(" + jd + "," + wd + ")";//地图编辑
            return View();
        }
        #endregion



    }
}
