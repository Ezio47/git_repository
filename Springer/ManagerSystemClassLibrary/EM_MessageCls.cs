using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 短信模板
    /// </summary>
    public class EM_MessageCls
    {
        #region 增、删
        /// <summary>
        /// 增、删
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(SendMessage_Model m)
        {
            if (m.Opmethod == "Add")
            {
                Message msgUser = BaseDT.EM_Message.Add(m);
                SmsHelp.SmsCom.SmsTemplateModify(m.MessageContent, "");
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.Opmethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.EM_Message.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.Opmethod == "Mdy")
            {

                Message msgUser = BaseDT.EM_Message.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
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
        public static SendMessage_Model getModel(EM_Message_SW sw)
        {
            var result = new List<SendMessage_Model>();

            DataTable dt = BaseDT.EM_Message.getDT(sw);//列表
            SendMessage_Model m = new SendMessage_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.EM_MESSAGEID = dt.Rows[i]["EM_MESSAGEID"].ToString();
                m.MessageContent = dt.Rows[i]["TMPCONTENT"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<SendMessage_Model> getModelList(EM_Message_SW sw)
        {
            var result = new List<SendMessage_Model>();
            DataTable dt = BaseDT.EM_Message.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SendMessage_Model m = new SendMessage_Model();
                m.EM_MESSAGEID = dt.Rows[i]["EM_MESSAGEID"].ToString();
                m.MessageContent = dt.Rows[i]["TMPCONTENT"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取收藏短信
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<SendMessage_Model> getModelListpager(EM_Message_SW sw, out int total)
        {
            total = 0;
            var result = new List<SendMessage_Model>();
            DataTable dt = BaseDT.EM_Message.getDT(sw, out total);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SendMessage_Model m = new SendMessage_Model();
                m.EM_MESSAGEID = dt.Rows[i]["EM_MESSAGEID"].ToString();
                m.MessageContent = dt.Rows[i]["TMPCONTENT"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
