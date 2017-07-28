using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagerSystemClassLibrary;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using PublicClassLibrary;
using Newtonsoft.Json;
using System.IO;
using System.Text;


namespace ManagerSystem.MVC.Controllers
{
    public class EmailController : BaseController
    {
        #region 短信发送的tree
        /// <summary>
        /// 短信发送的tree
        /// </summary>
        /// <returns></returns>
        public ActionResult MessageTreeGet()
        {
            //string orgno = SystemCls.getCurUserOrgNo();//获取当前登录用户的机构编码
            string ID = Request.Params["id"];
            string type = Request.Params["type"];
            var result = T_IPSFR_USERCls.gettestUserTree(ID, type);
            return Content(result, "application/json");
        }
        /// <summary>
        /// 获取jason
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMessagejson()
        {
            string ID = Request.Params["ID"];
            string TYPE = Request.Params["TYPE"];
            return Content(JsonConvert.SerializeObject(T_SYS_ORG_LINKCls.getstr(ID, TYPE)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 获取tree的组织机构Jason作为父节点
        /// </summary>
        /// <returns></returns>
        public ActionResult GetORGNOjson()
        {
            string NAME = Request.Params["NAME"];
            string PHONE = Request.Params["PHONE"];
            return Content(JsonConvert.SerializeObject(T_SYS_ORG_LINKCls.getOrgno(PHONE, NAME)), "text/html;charset=UTF-8");
        }
        #endregion

        #region 短信发送
        /// <summary>
        /// 短信发送页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SendMessage() 
        {
            pubViewBag("009006", "009006", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.T_UrlReferrer = "/Email/SendMessage";
            return View();
        }
        /// <summary>
        /// 短信群组管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SendMessageMan()
        {
            pubViewBag("009006", "009006", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.T_UrlReferrer = "/Email/SendMessage";
            ViewBag.EGROUPID = Request.Params["EGID"];
            ViewBag.T_Method = Request.Params["Method"];
          
            //如果未传参数，默认为添加
            if (string.IsNullOrEmpty(ViewBag.T_Method))
                ViewBag.T_Method = "Add";
            return View();
        }
        /// <summary>
        /// 短信模板的修改页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SMStemplateMan()
        {
            pubViewBag("009006", "009006", "");
            if (ViewBag.isPageRight == false)
                return View();
            //ViewBag.T_UrlReferrer = "/Email/SendMessage";
            ViewBag.EM_MESSAGEID = Request.Params["EM_MESSAGEID"];
            ViewBag.T_Method = Request.Params["Method"];
            //如果未传参数，默认为添加
            if (string.IsNullOrEmpty(ViewBag.T_Method))
                ViewBag.T_Method = "Add";
            return View();
        }
        /// <summary>
        /// 短信模板修改时赋值
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSMStemplatejson()
        {
            string EM_MESSAGEID = Request.Params["EM_MESSAGEID"];
            return Content(JsonConvert.SerializeObject(EM_MessageCls.getModel(new EM_Message_SW { EM_MESSAGEID = EM_MESSAGEID })), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 短信发送
        /// </summary>
        /// <returns></returns>
        public ActionResult MessageSend()
        {
            SendMessage_Model m = new SendMessage_Model();
            m.MessageContent = Request.Params["MessageContent"];
            m.MessageName = Request.Params["MessageName"];
            m.MessageTitle = Request.Params["MessageTitle"];
            m.PHONE = Request.Params["PHONE"];
            m.NAME = Request.Params["NAME"];
            m.URL = "/Email/SendMessage";
            return Content(JsonConvert.SerializeObject(SendMessageCls.SEND(m)));
        }
        /// <summary>
        /// 获取短信模板
        /// </summary>
        /// <returns></returns>
        public ActionResult GetScSmsContentData()
        {
            int total = 0;
            int row = int.Parse(Request["rows"].ToString());
            int pageindex = int.Parse(Request["page"].ToString());
            var result = EM_MessageCls.getModelListpager(new EM_Message_SW { },out total);
            var list = result.Skip(row * (pageindex - 1)).Take(row);
            var jsonResult = new { total = total, rows = list };
            ////把json对象序列化成字符串 
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 短信模板的增删
        /// </summary>
        /// <returns></returns>
        public ActionResult MessageManger()
        {
            SendMessage_Model m = new SendMessage_Model();
            m.EM_MESSAGEID = Request.Params["EM_MESSAGEID"];
            m.MessageContent = Request.Params["MessageContent"];
            m.Opmethod = Request.Params["Method"];
            m.ORDERBY = Request.Params["ORDERBY"];
            if (m.Opmethod=="Add"&&string.IsNullOrEmpty(m.MessageContent))
            {
                return Content(JsonConvert.SerializeObject(new Message(false, "短信模板内容不可为空！", "")), "text/html;charset=UTF-8");
            }
            m.URL = "/Email/SendMessage";
            return Content(JsonConvert.SerializeObject(EM_MessageCls.Manager(m)));
        }
        #endregion

        #region 群组的管理
        /// <summary>
        /// 群组的增删改
        /// </summary>
        /// <returns></returns>
        public ActionResult EGROUPManger()
        {
            E_GROUP_Model m = new E_GROUP_Model();
            m.EGROUPID = Request.Params["EGROUPID"];
            m.EGROUPMEMBERLIST = Request.Params["EGROUPMEMBERLIST"];
            m.EGROUPPHONELIST = Request.Params["EGROUPPHONELIST"];
            m.opMethod = Request.Params["Method"];
            m.EGROUPNAME = Request.Params["EGROUPNAME"];
            m.EGROUPTYPE = Request.Params["EGROUPTYPE"];
            m.EGROUPUSERID = SystemCls.getUserID();
            m.URL = "/Email/SendMessage";
            return Content(JsonConvert.SerializeObject(E_GROUPCls.Manager(m)));
        }
        /// <summary>
        /// 短信群组的展示
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGroupContentData()
        {
            int total = 0;
            int row = int.Parse(Request["rows"].ToString());
            string EGROUPTYPE = Request.Params["EGROUPTYPE"];
            int pageindex = int.Parse(Request["page"].ToString());
            var result = E_GROUPCls.getModelListpager(new E_GROUP_SW { EGROUPTYPE = EGROUPTYPE,EGROUPUSERID=SystemCls.getUserID() }, out total);
            var list = result.Skip(row * (pageindex - 1)).Take(row);
            var jsonResult = new { total = total, rows = list };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取群组查看的记录
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGROUPjson()
        {
               string EGROUPID = Request.Params["EGROUPID"];
            return Content(JsonConvert.SerializeObject(E_GROUPCls.getModel(new E_GROUP_SW { EGROUPID = EGROUPID })), "text/html;charset=UTF-8");
        }
        #endregion

        #region 邮箱管理
        /// <summary>
        /// 主题表管理
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]        
        public ActionResult EmailManager()
        {
            E_SUBJECT_Model m = new E_SUBJECT_Model();
            string EMAILID = Request.Params["EMAILID"];
            string EMAILTITLE = Request.Params["EMAILTITLE"];
            string EMAILSTATUS = Request.Params["EMAILSTATUS"];
            string EMAILCONTENT = Request.Params["EMAILCONTENT"];
            string EMAILRECUSERLIST = Request.Params["EMAILRECUSERLIST"];
            string EMAILCOPYUSERLIST = Request.Params["EMAILCOPYUSERLIST"];
            string EMAILSECRETUSERLIST = Request.Params["EMAILSECRETUSERLIST"];
            string EMAILFileLIST = Request.Params["EMAILFileLIST"];
            string Method = Request.Params["Method"];
            string returnUrl = Request.Params["returnUrl"];
            m.EMAILCONTENT = EMAILCONTENT;
            m.EMAILTITLE = EMAILTITLE;
            if (string.IsNullOrEmpty(EMAILTITLE))
                return Content(JsonConvert.SerializeObject(new Message(false, "请输入主题", "")), "text/html;charset=UTF-8");
            m.EMAILID = EMAILID;
            m.EMAILSTATUS = EMAILSTATUS;
            m.EMAILRECUSERLIST = EMAILRECUSERLIST;
            if (string.IsNullOrEmpty(EMAILRECUSERLIST))
                return Content(JsonConvert.SerializeObject(new Message(false, "请选择收件人", "")), "text/html;charset=UTF-8");
            m.EMAILCOPYUSERLIST = EMAILCOPYUSERLIST;
            m.EMAILSECRETUSERLIST = EMAILSECRETUSERLIST;
            m.EMAILSENDUSERID = SystemCls.getUserID();
            m.EMAILTIME = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now.ToString());
            m.opMethod = Method;
            if (m.opMethod == "Send")
                m.EMAILSTATUS = "1";
            if (m.opMethod == "Add")
                m.EMAILSTATUS = "0";
            if (m.opMethod == "AddToSend")
                m.EMAILSTATUS = "1";
            if (m.opMethod == "Mdy")
                m.EMAILSTATUS = "-1";
            m.returnUrl = returnUrl;
            return Content(JsonConvert.SerializeObject(E_SUBJECTCls.Manager(m)));
        }

        # region 邮箱发送
        /// <summary>
        /// 邮箱发送页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Emailsend()
        {
            pubViewBag("009001", "009001", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.T_Method = Request.Params["Method"];
            ViewBag.T_UrlReferrer = "/Email/Emailsend";
            return View();
        }
        /// <summary>
        /// 邮箱群组管理页面
        /// </summary>
        /// <returns></returns>

        public ActionResult EmailtemplateMan()
        {
            pubViewBag("009001", "009001", "");
            if (ViewBag.isPageRight == false)
                return View();
            ViewBag.EGROUPID = Request.Params["EID"];
            ViewBag.T_Method = Request.Params["Method"];
            //如果未传参数，默认为添加
            if (string.IsNullOrEmpty(ViewBag.T_Method))
                ViewBag.T_Method = "Add";
            ViewBag.T_UrlReferrer = "/Email/Emailsend";
            return View();
        }
        #endregion

        #region 已发送箱
        /// <summary>
        /// 多条删除
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailDEL()
        {
            E_SUBJECT_Model m = new E_SUBJECT_Model();
            string EMAILID = Request.Params["EMAILID"];
            string Method = Request.Params["Method"];
            string returnUrl = Request.Params["returnUrl"];
            m.EMAILID = EMAILID;
            m.opMethod = Method;
            m.returnUrl = returnUrl;
            if (m.opMethod == "SendMdy")
                m.EMAILSTATUS = "-1";
            return Content(JsonConvert.SerializeObject(E_SUBJECTCls.Manager(m)));
        }
        /// <summary>
        /// 邮件搜索
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailsendListQuery()
        {
            string PageSize = Request.Params["PageSize"];
            string Page = Request.Params["Page"];
            string EMAILTITLE = Request.Params["EMAILTITLE"];
            string str = ClsStr.EncryptA01((PageSize + "|" + EMAILTITLE), "kkkkkkkk");
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/Email/EmailsendList?trans=" + str + "&page=" + Page)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 已发送页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailsendList()
        {
            pubViewBag("009002", "009002", "");
            ViewBag.T_Method = Request.Params["Method"];
            string PageSize = Request.Params["PageSize"];
            string page = Request.Params["page"];
            if (string.IsNullOrEmpty(page))
                page = "1";
            string trans = Request.Params["trans"];
            string[] arr = new string[2];
            if (string.IsNullOrEmpty(trans) == false)
                arr = ClsStr.DecryptA01(trans, "kkkkkkkk").Split('|');
            if (string.IsNullOrEmpty(arr[0]) == true)
                arr[0] = PagerCls.getDefaultPageSize().ToString();
            ViewBag.EMAILTITLE = arr[1];
            int total = 0;
            ViewBag.EmailsendList = getEmailsendListStr(new E_SUBJECT_SW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), EMAILTITLE = arr[1], EMAILSTATUS = "1", EMAILSENDUSERID = SystemCls.getUserID() }, out total);//已发送列表
            ViewBag.PagerInfo = PagerCls.getPagerInfo_New(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), rowCount = total, url = "/Email/EmailsendList?trans=" + trans });
            return View();
        }
        private string getEmailsendListStr(E_SUBJECT_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style='width:10%;'>序号</th>");
            sb.AppendFormat("<th style='width:35%;'>收件人</th>");
            sb.AppendFormat("<th style='width:35%;'>主题</th>");
            sb.AppendFormat("<th style='width:20%;'>时间</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = E_SUBJECTCls.getListModelPager(sw, out total);
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center  \"><input type='checkbox' name='chk1'  value='" + v.EMAILID + "'/></td>");
                sb.AppendFormat("<td class=\"left  \">{0}</td>", v.EMAILRECUSERNameLIST);
                sb.AppendFormat("<td class=\"left  \"><a href=\"/Email/EmailsendMan?Method=See&ID={1}\">{0}</a></td>", v.EMAILTITLE, v.EMAILID);
                sb.AppendFormat("<td class=\"center  \">{0}</td>", v.EMAILTIME);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        /// <summary>
        /// 已发送列表管理、查看单个邮件内容
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailsendMan()
        {
            pubViewBag("009002", "009002", "");

            if (ViewBag.isPageRight == false)
                return View();
            string ID = Request.Params["ID"];
            ViewBag.EmailsendMan = getEmailsendManStr(new E_SUBJECT_SW { EMAILID = ID });
            return View();
        }
        /// <summary>
        /// 查看取出该条记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public string getEmailsendManStr(E_SUBJECT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            var v = E_SUBJECTCls.getModel(sw);
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<tr class='row1'>");
            sb.AppendFormat("<td>");
            sb.AppendFormat("<font size='3px'><strong>{0}", v.EMAILTITLE);
            sb.AppendFormat("</strong></font>");
            sb.AppendFormat("<br><font color='grey'>发件人:</font>{0}", v.EMAILSENDUSERName);
            sb.AppendFormat("<br><font color='grey'>时间:{0}", v.EMAILTIME);
            sb.AppendFormat("</font>");
            sb.AppendFormat("<br><font color='grey'>收件人:</font>{0}", v.EMAILRECUSERNameLIST);
            if (string.IsNullOrEmpty(v.EMAILCOPYUSERLIST) == false)
            {
                sb.AppendFormat("<br><font color='grey'>抄送:</font>{0}", v.EMAILCOPYUSERNameLIST);
            }
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            var ss = v.FileModel;
            if (ss.Any())
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td >附件：<br>");
                foreach (var s in ss)
                {
                    sb.AppendFormat("<a href=\"{1}\">{0}</a><br/>", s.EMAILFILENAME, s.EMAILFILETITLE);
                }
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
            }
            if (ss.Any())
                sb.AppendFormat("<tr  class='row1' style='height:300px;'>");
            else
                sb.AppendFormat("<tr style='height:300px;'>");
            sb.AppendFormat("<td style='vertical-align:top;'>{0}", v.EMAILCONTENT);
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        #endregion

        #region 草稿箱
        /// <summary>
        /// 草稿箱多条删除
        /// </summary>
        public ActionResult EmailDraftDEL()
        {
            E_SUBJECT_Model m = new E_SUBJECT_Model();
            string EMAILID = Request.Params["EMAILID"];
            string Method = Request.Params["Method"];
            m.EMAILID = EMAILID;
            m.opMethod = Method;
            return Content(JsonConvert.SerializeObject(E_SUBJECTCls.Manager(m)));
        }
        /// <summary>
        /// 草稿箱取出草稿
        /// </summary>
        /// <returns></returns>
        public ActionResult GetEmailJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            var model = E_SUBJECTCls.getModel(new E_SUBJECT_SW { EMAILID = ID });
            return Content(JsonConvert.SerializeObject(model), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 草稿箱取出附件
        /// </summary>
        /// <returns></returns>
        public ActionResult GetEmailFILEJson()
        {
            string ID = Request.Params["ID"];
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            var model = E_FILECls.getListModel(new E_File_SW { BYEMAILID = ID });
            return Content(JsonConvert.SerializeObject(model), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 邮件搜索
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailDraftsListQuery()
        {
            string PageSize = Request.Params["PageSize"];
            string Page = Request.Params["Page"];
            string EMAILTITLE = Request.Params["EMAILTITLE"];
            string str = ClsStr.EncryptA01((PageSize + "|" + EMAILTITLE), "kkkkkkkk"); ;
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/Email/EmailDraftsList?trans=" + str + "&page=" + Page)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 草稿箱管理
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailDraftsMan()
        {
            pubViewBag("009004", "009004", "");

            if (ViewBag.isPageRight == false)
                return View();
            string ID = Request.Params["ID"];
            ViewBag.T_ID = ID;
            ViewBag.T_Method = Request.Params["Method"];
            return View();
        }
        public ActionResult EmailDraftsList()
        {
            pubViewBag("009004", "009004", "");
            ViewBag.T_Method = Request.Params["Method"];
            string PageSize = Request.Params["PageSize"];
            string page = Request.Params["page"];
            if (string.IsNullOrEmpty(page))
                page = "1";
            string trans = Request.Params["trans"];
            string[] arr = new string[2];
            if (string.IsNullOrEmpty(trans) == false)
                arr = ClsStr.DecryptA01(trans, "kkkkkkkk").Split('|');
            if (string.IsNullOrEmpty(arr[0]) == true)
                arr[0] = PagerCls.getDefaultPageSize().ToString();
            ViewBag.EMAILTITLE = arr[1];
            int total = 0;

            ViewBag.EmailDraftsList = getEmailDraftsListStr(new E_SUBJECT_SW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), EMAILTITLE = arr[1], EMAILSTATUS = "0" }, out total);//草稿箱列表
            //ViewBag.EmailInboxList = getEmailInboxListStr(new E_SUBJECT_SW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]) }, out total);//收件箱列表、收信
            ViewBag.PagerInfo = PagerCls.getPagerInfo_New(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), rowCount = total, url = "/Email/EmailDraftsList?trans=" + trans });
            return View();
        }
        private string getEmailDraftsListStr(E_SUBJECT_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style='width:10%;'>序号</th>");
            sb.AppendFormat("<th style='width:35%;'>收件人</th>");
            sb.AppendFormat("<th style='width:35%;'>主题</th>");
            sb.AppendFormat("<th style='width:20%;'>时间</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = E_SUBJECTCls.getListModelPager(sw, out total);
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center  \"><input type='checkbox' name='chk1'  value='" + v.EMAILID + "'/></td>");
                sb.AppendFormat("<td class=\"left  \">{0}</td>", v.EMAILRECUSERNameLIST);
                sb.AppendFormat("<td class=\"left  \"><a href=\"/Email/EmailDraftsMan?Method=AddToSend&ID={1}\">{0}</a></td>", v.EMAILTITLE, v.EMAILID);

                sb.AppendFormat("<td class=\"center  \">{0}</td>", v.EMAILTIME);
                sb.AppendFormat("    </td>");
                sb.AppendFormat("</tr>");
                i++;
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        #endregion

        #region 收件箱
        public ActionResult EmailRECManger()
        {
            E_RECEIVE_Model m = new E_RECEIVE_Model();
            string ERID = Request.Params["ERID"];
            string Method = Request.Params["Method"];
            string returnUrl = Request.Params["returnUrl"];
            m.ERID = ERID;
            m.opMethod = Method;
            m.returnUrl = returnUrl;
            if (m.opMethod == "SendMdy")
            {
                m.EMAILRECEIVESTATUS = "-1";
            }
            m.ERID = ERID;
            m.returnUrl = returnUrl;
            return Content(JsonConvert.SerializeObject(E_RECEIVE_Cls.Manager(m)));
        }
        /// <summary>
        /// 邮件搜索
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailRecieveListQuery()
        {
            string PageSize = Request.Params["PageSize"];
            string Page = Request.Params["Page"];
            string EMAILRECEIVESTATUS = Request.Params["EMAILRECEIVESTATUS"];
            string EMAILTITLE = Request.Params["EMAILTITLE"];
            string str = ClsStr.EncryptA01((PageSize + "|" + EMAILRECEIVESTATUS + "|" + EMAILTITLE), "kkkkkkkk"); ;
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/Email/EmailRecieveList?trans=" + str + "&page=" + Page)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 邮件管理-查看单条邮件
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailRecieveMan()
        {
            pubViewBag("009005", "009005", "");
            if (ViewBag.isPageRight == false)
                return View();
            string ID = Request.Params["ID"];
            ViewBag.EmailRecieveMan = getEmailRecieveManStr(new E_RECEIVE_SW { ERID = ID });
            return View();
        }
        private string getEmailRecieveManStr(E_RECEIVE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            var v = E_RECEIVE_Cls.getModel(sw);
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<tr class='row1'>");
            sb.AppendFormat("<td>");
            sb.AppendFormat("<font size='3px'><strong>{0}", v.SubjectModel.EMAILTITLE);
            sb.AppendFormat("</strong></font>");
            sb.AppendFormat("<br><font color='grey'>发件人:</font>{0}", v.SubjectModel.EMAILSENDUSERName);
            sb.AppendFormat("<br><font color='grey'>时间:{0}", v.SubjectModel.EMAILTIME);
            sb.AppendFormat("</font>");
            sb.AppendFormat("<br><font color='grey'>收件人:</font>{0}", v.SubjectModel.EMAILRECUSERNameLIST);
            if (string.IsNullOrEmpty(v.SubjectModel.EMAILCOPYUSERLIST) == false)
            {
                sb.AppendFormat("<br><font color='grey'>抄送:</font>{0}", v.SubjectModel.EMAILCOPYUSERNameLIST);

            }
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            var ss = v.FileModel;
            if (ss.Any())
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td >附件：");
                foreach (var s in ss)
                {
                    sb.AppendFormat("<a href=\"{1}\">{0}</a><br/>", s.EMAILFILENAME, s.EMAILFILETITLE);
                }
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
            }
            if (ss.Any())
                sb.AppendFormat("<tr  class='row1' style='height:300px;'>");
            else
                sb.AppendFormat("<tr style='height:300px;'>");
            sb.AppendFormat("<td >{0}", v.SubjectModel.EMAILCONTENT);
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            return sb.ToString();


        }
        /// <summary>
        /// 收件箱页面-列表展示
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailRecieveList()
        {
            pubViewBag("009005", "009005", "");
            ViewBag.T_Method = Request.Params["Method"];
            string page = Request.Params["page"];
            if (string.IsNullOrEmpty(page))
                page = "1";
            string trans = Request.Params["trans"];
            string[] arr = new string[3];
            if (string.IsNullOrEmpty(trans) == false)
                arr = ClsStr.DecryptA01(trans, "kkkkkkkk").Split('|');
            if (string.IsNullOrEmpty(arr[0]) == true)
                arr[0] = PagerCls.getDefaultPageSize().ToString();
            ViewBag.EMAILTITLE = arr[2];
            if (string.IsNullOrEmpty(arr[1]) == true)
                arr[1] = "0,1";
            ViewBag.EMAILRECEIVESTATUS = arr[1];
            int total = 0;
            ViewBag.EmailRecieveList = getEmailRecieveListStr(new E_RECEIVE_SW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), RECEIVEUSERID = SystemCls.getUserID(), EMAILRECEIVESTATUS = arr[1], EMAILTITLE = arr[2] }, out total);
            ViewBag.PagerInfo = PagerCls.getPagerInfo_New(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), rowCount = total, url = "/Email/EmailRecieveList?trans=" + trans });
            return View();
        }
        private string getEmailRecieveListStr(E_RECEIVE_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th style='width:10%;'>序号</th>");
            sb.AppendFormat("<th style='width:35%;'>发件人</th>");
            sb.AppendFormat("<th style='width:35%;'>主题</th>");
            sb.AppendFormat("<th style='width:20%;'>时间</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody role=\"alert\" aria-live=\"polite\" aria-relevant=\"all\">");
            var result = E_RECEIVE_Cls.getListModel(sw, out total);
            int i = 0;
            foreach (var v in result)
            {
                if (v.EMAILRECEIVESTATUS != "-1")
                {
                    if (i % 2 == 0)
                        sb.AppendFormat("<tr>");
                    else
                        sb.AppendFormat("<tr class='row1'>");
                    sb.AppendFormat("<td class=\"center\"><input type='checkbox' name='chk1'  value='" + v.ERID + "'/></td>");
                    sb.AppendFormat("<td class=\"left\">{0}</td>", v.SubjectModel.EMAILSENDUSERName);
                    if (v.EMAILRECEIVESTATUS == "0")
                    {
                        sb.AppendFormat("<td class=\"left  sorting_1\"><strong><a href=\"/Email/EmailRecieveMan?Method=Mdy&ID={1}\">{0}</a></strong></td>", v.SubjectModel.EMAILTITLE, v.ERID);
                    }
                    if (v.EMAILRECEIVESTATUS == "1")
                    {
                        sb.AppendFormat("<td class=\"left\"><a href=\"/Email/EmailRecieveMan?Method=Mdy&ID={1}\">{0}</a></td>", v.SubjectModel.EMAILTITLE, v.ERID);
                    }
                    sb.AppendFormat("<td class=\"center\">{0}</td>", v.SubjectModel.EMAILTIME);
                    sb.AppendFormat("    </td>");
                    sb.AppendFormat("</tr>");
                    i++;
                }
            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        #endregion

        #region 已删除箱
        /// <summary>
        /// 邮件搜索
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailDELListQuery()
        {
            string PageSize = Request.Params["PageSize"];
            string Page = Request.Params["Page"];
            string EMAILTITLE = Request.Params["EMAILTITLE"];
            string str = ClsStr.EncryptA01((PageSize + "|" + EMAILTITLE), "kkkkkkkk"); ;
            return Content(JsonConvert.SerializeObject(new Message(true, "", "/Email/EmailDELList?trans=" + str + "&page=" + Page)), "text/html;charset=UTF-8");
        }
        /// <summary>
        /// 已删除管理
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailDELMan()
        {
            pubViewBag("009003", "009003", "");
            if (ViewBag.isPageRight == false)
                return View();
            string ID = Request.Params["ID"];
            ViewBag.EmailDELMan = getEmailDELManStr(new E_SUBJECT_SW { EMAILID = ID });
            return View();
        }
        private string getEmailDELManStr(E_SUBJECT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            var v = E_SUBJECTCls.getModel(sw);
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<tr class='row1'>");
            sb.AppendFormat("<td>");
            sb.AppendFormat("<font size='3px'><strong>{0}", v.EMAILTITLE);
            sb.AppendFormat("</strong></font>");
            sb.AppendFormat("<br><font color='grey'>发件人:</font>{0}", v.EMAILSENDUSERName);
            sb.AppendFormat("<br><font color='grey'>时间:{0}", v.EMAILTIME);
            sb.AppendFormat("</font>");
            sb.AppendFormat("<br><font color='grey'>收件人:</font>{0}", v.EMAILRECUSERNameLIST);
            if (string.IsNullOrEmpty(v.EMAILCOPYUSERLIST) == false)
            {
                sb.AppendFormat("<br><font color='grey'>抄送:</font>{0}", v.EMAILCOPYUSERNameLIST);
            }
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            var ss = v.FileModel;
            if (ss.Any())
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td >附件：<br>");
                foreach (var s in ss)
                {
                    sb.AppendFormat("<a href=\"{1}\">{0}</a><br/>", s.EMAILFILENAME, s.EMAILFILETITLE);
                }
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
            }
            if (ss.Any())
                sb.AppendFormat("<tr  class='row1' style='height:300px;'>");
            else
                sb.AppendFormat("<tr style='height:300px;'>");
            sb.AppendFormat("<td style='vertical-align:top;'>{0}", v.EMAILCONTENT);
            sb.AppendFormat("</td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }
        /// <summary>
        /// 已删除页面 -列表展示
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailDELList()
        {
            pubViewBag("009003", "009003", "");
            ViewBag.T_Method = Request.Params["Method"];
            string PageSize = Request.Params["PageSize"];
            string page = Request.Params["page"];
            if (string.IsNullOrEmpty(page))
                page = "1";
            string trans = Request.Params["trans"];
            string[] arr = new string[2];
            if (string.IsNullOrEmpty(trans) == false)
                arr = ClsStr.DecryptA01(trans, "kkkkkkkk").Split('|');
            if (string.IsNullOrEmpty(arr[0]) == true)
                arr[0] = PagerCls.getDefaultPageSize().ToString();
            ViewBag.EMAILTITLE = arr[1];
            int total = 0;
            ViewBag.EmaildelList = getEmaildelListStr(new E_RECEIVE_SW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), RECEIVEUSERID = SystemCls.getUserID(), EMAILRECEIVESTATUS = "-1", EMAILTITLE = arr[1] }, out total);//已删除列表
            ViewBag.PagerInfo = PagerCls.getPagerInfo_New(new PagerSW { curPage = int.Parse(page), pageSize = int.Parse(arr[0]), rowCount = total, url = "/Email/EmailDELList?trans=" + trans });
            return View();
        }
        private string getEmaildelListStr(E_RECEIVE_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendFormat("<thead>");
            sb.AppendFormat("<th style='width:10%;'>序号</th>");
            sb.AppendFormat("<th style='width:35%;'>发件人</th>");
            sb.AppendFormat("<th style='width:35%;'>主题</th>");
            sb.AppendFormat("<th style='width:20%;'>时间</th>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            var result = E_RECEIVE_Cls.getListModel(sw, out total);
            int i = 0;
            foreach (var v in result)
            {
                if (i % 2 == 0)
                    sb.AppendFormat("<tr>");
                else
                    sb.AppendFormat("<tr class='row1'>");
                sb.AppendFormat("<td class=\"center\">{0}</td>", ((sw.curPage - 1) * sw.pageSize + i + 1).ToString());
                sb.AppendFormat("<td class=\"left\">{0}</td>", v.SubjectModel.EMAILSENDUSERName);
                sb.AppendFormat("<td class=\"left1\"><a href=\"/Email/EmailDELMan?Method=See&ID={1}\">{0}</a></td>", v.SubjectModel.EMAILTITLE, v.SubjectModel.EMAILID);
                sb.AppendFormat("<td class=\"center\">{0}</td>", v.SubjectModel.EMAILTIME);
                sb.AppendFormat("</td>");
                sb.AppendFormat("</tr>");
                i++;

            }
            sb.AppendFormat("</tbody>");
            sb.AppendFormat("</table>");
            return sb.ToString();
        }

        #endregion

        #region 附件
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <returns></returns>
        public JsonResult Uploadfire()
        {
            string emailid = Request.Params["emailid"];
            Message ms = null;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string Path = "";
            if (hfc.Count > 0)
            {
                E_FILE_Model m = new E_FILE_Model();
                Path = "/UploadFile/Email/";
                string PhysicalPath = Server.MapPath(Path);
                if (!Directory.Exists(PhysicalPath))//判断文件夹是否已经存在
                {
                    Directory.CreateDirectory(PhysicalPath);//创建文件夹
                }
                for (int i = 0; i < hfc.Count; i++)
                {
                    hfc[i].SaveAs(PhysicalPath + hfc[i].FileName);
                    string mm = emailid;
                    if (string.IsNullOrEmpty(emailid))
                    {
                        mm = E_SUBJECTCls.AddReturn(new E_SUBJECT_Model() { });
                        if (string.IsNullOrEmpty(mm))
                        {
                            ms = new Message(false, "主题表增加出错", "");
                        }
                    }
                    var dd = E_FILECls.AddReturn(new E_FILE_Model() { BYEMAILID = mm, EMAILFILENAME = hfc[i].FileName, EMAILFILESIZE = hfc[i].ContentLength.ToString(), EMAILFILETITLE = Path + hfc[i].FileName });
                    if (string.IsNullOrEmpty(dd))
                    {
                        ms = new Message(false, "附件表增加出错", "");
                    }
                    else
                    {
                        ms = new Message(true, mm, dd);
                    }

                }
            }
            return Json(ms);
        }
        /// <summary>
        /// 附件删除
        /// </summary>
        /// <returns></returns>
        public ActionResult DEL()
        {
            E_FILE_Model m = new E_FILE_Model();
            string EFID = Request.Params["EFID"];
            string Method = Request.Params["Method"];
            m.EFID = EFID;
            m.opMethod = Method;
            return Content(JsonConvert.SerializeObject(E_FILECls.DEL(m)));
        }
        #endregion

        #region 组织机构-用户tree
        /// <summary>
        /// 取出组织机构-用户tree
        /// </summary>
        /// <returns></returns>
        public ActionResult TreeUSERGet()
        {
            string result = T_IPSFR_USERCls.getUserTree(new T_IPSFR_USER_SW { });
            return Content(result, "application/json");
        }
        #endregion

        #endregion
    }
}
