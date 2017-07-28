using log4net;
using ManagerSystemClassLibrary.SmsSendService;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 短信模板
    /// </summary>
    public class YJ_DCSMS_TMPCls
    {
        /// <summary>
        /// logs
        /// </summary>
        public static ILog logs = LogHelper.GetInstance();

        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(YJ_DCSMS_TMP_Model m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.YJ_DCSMS_TMP.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.YJ_DCSMS_TMP.Mdy(m);
                if (msgUser.Success == true)
                {
                    //获取模板tid
                    var smstemplate = getModel(new YJ_DCSMS_TMP_SW { YJ_DCSMS_TMPID = m.YJ_DCSMS_TMPID });
                    if (smstemplate != null)
                    {
                        string content = m.TMPCONTENT.Replace("[num]", "@").Replace("[cityconty]", "@");
                        string tid = smstemplate.TID;//选出你要修改记录的数据model.tid
                        if (string.IsNullOrEmpty(tid))
                        {
                            var sm = SmsHelp.SmsCom.SmsTemplateModify(content, "", "operate_templet");
                            if (sm.Success == true)
                            {
                                m.TID = sm.Msg.Trim();
                                msgUser = BaseDT.YJ_DCSMS_TMP.Mdy(m);
                            }
                        }
                        else
                        {
                            var sm = SmsHelp.SmsCom.SmsTemplateModify(content, tid, "operate_templet");
                        }
                    }
                }
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "MdyISENABLE")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.YJ_DCSMS_TMP.MdyISENABLE(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.YJ_DCSMS_TMP.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");


        }

        #endregion

        #region 获取单条
        /// <summary>
        /// 根据查询条件获取某一条用户信息记录，用于修改、删除、用户登录验证
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static YJ_DCSMS_TMP_Model getModel(YJ_DCSMS_TMP_SW sw)
        {
            var result = new List<YJ_DCSMS_TMP_Model>();

            DataTable dt = BaseDT.YJ_DCSMS_TMP.getDT(sw);//列表

            YJ_DCSMS_TMP_Model m = new YJ_DCSMS_TMP_Model();

            DataTable dtFIRELEVEL = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "22" });//预案
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.YJ_DCSMS_TMPID = dt.Rows[i]["YJ_DCSMS_TMPID"].ToString();
                m.SMSGROUPNAME = dt.Rows[i]["SMSGROUPNAME"].ToString();
                m.SMSGROUPTYPE = dt.Rows[i]["SMSGROUPTYPE"].ToString();
                m.SMSSENDUSERLIST = dt.Rows[i]["SMSSENDUSERLIST"].ToString();
                m.TMPCONTENT = dt.Rows[i]["TMPCONTENT"].ToString();
                m.DANGERCLASS = dt.Rows[i]["DANGERCLASS"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.ISENABLE = dt.Rows[i]["ISENABLE"].ToString();
                m.TID = dt.Rows[i]["TID"] == null ? "" : dt.Rows[i]["TID"].ToString();
                m.ISENABLEName = (m.ISENABLE == "1") ? "启用" : "未启用";
                if (m.SMSGROUPTYPE == "0")
                    m.SMSGROUPTYPEName = "通讯录";
                else if (m.SMSGROUPTYPE == "1")
                    m.SMSGROUPTYPEName = "值班员";
                else if (m.SMSGROUPTYPE == "2")
                    m.SMSGROUPTYPEName = "护林员";
                else
                    m.SMSGROUPTYPEName = "设置错误";
                m.FIRELEVELName = BaseDT.T_SYS_DICT.getName(dtFIRELEVEL, m.DANGERCLASS);
            }
            dt.Clear();
            dt.Dispose();
            dtFIRELEVEL.Clear();
            dtFIRELEVEL.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 模板列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<YJ_DCSMS_TMP_Model> GetListModel(YJ_DCSMS_TMP_SW sw)
        {
            var result = new List<YJ_DCSMS_TMP_Model>();
            DataTable dt = BaseDT.YJ_DCSMS_TMP.getDT(sw);//获取模板
            DataTable dtFIRELEVEL = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "24" });//预案
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                YJ_DCSMS_TMP_Model m = new YJ_DCSMS_TMP_Model();
                m.YJ_DCSMS_TMPID = dt.Rows[i]["YJ_DCSMS_TMPID"].ToString();
                m.SMSGROUPNAME = dt.Rows[i]["SMSGROUPNAME"].ToString();
                m.SMSGROUPTYPE = dt.Rows[i]["SMSGROUPTYPE"].ToString();
                m.SMSSENDUSERLIST = dt.Rows[i]["SMSSENDUSERLIST"].ToString();
                m.TMPCONTENT = dt.Rows[i]["TMPCONTENT"].ToString();
                m.DANGERCLASS = dt.Rows[i]["DANGERCLASS"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.ISENABLE = dt.Rows[i]["ISENABLE"].ToString();
                m.TID = dt.Rows[i]["TID"] == null ? "" : dt.Rows[i]["TID"].ToString();
                m.ISENABLEName = (m.ISENABLE == "1") ? "启用" : "未启用";

                if (m.SMSGROUPTYPE == "0")
                    m.SMSGROUPTYPEName = "通讯录";
                else if (m.SMSGROUPTYPE == "1")
                    m.SMSGROUPTYPEName = "值班员";
                else if (m.SMSGROUPTYPE == "2")
                    m.SMSGROUPTYPEName = "护林员";
                else
                    m.SMSGROUPTYPEName = "设置错误";
                m.FIRELEVELName = BaseDT.T_SYS_DICT.getName(dtFIRELEVEL, m.DANGERCLASS);
                m.dicModel = T_SYS_DICTCls.getModel(new T_SYS_DICTSW { DICTTYPEID = "24", DICTVALUE = m.DANGERCLASS });
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtFIRELEVEL.Clear();
            dtFIRELEVEL.Dispose();
            return result;
        }
        #endregion

    }
}
