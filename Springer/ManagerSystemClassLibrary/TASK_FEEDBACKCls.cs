using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 任务反馈表
    /// </summary>
    public class TASK_FEEDBACKCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">m</param>
        /// <returns></returns>
        public static Message Manager(T_IPSFR_USER_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.T_IPSFR_USER.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");
        } 
        #endregion

        #region 获取列表
        /// <summary>
        /// 根据查询条件获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<TASK_FEEDBACK_Model> getModelList(TASK_FEEDBACK_SW sw)
        {
            var result = new List<TASK_FEEDBACK_Model>();
            DataTable dt = BaseDT.TASK_FEEDBACK.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TASK_FEEDBACK_Model m = new TASK_FEEDBACK_Model();
                m.TASK_FEEDBACKID = dt.Rows[i]["TASK_FEEDBACKID"].ToString();
                m.HID = dt.Rows[i]["HID"].ToString();
                m.TASK_INFOID = dt.Rows[i]["TASK_INFOID"].ToString();
                m.RECEIVETIME = dt.Rows[i]["RECEIVETIME"].ToString();
                m.ACCEPTTIME = dt.Rows[i]["ACCEPTTIME"].ToString();
                m.FEEDBACKTIME = dt.Rows[i]["FEEDBACKTIME"].ToString();
                m.HNAME = BaseDT.T_IPSFR_USER.getName(dt.Rows[i]["HID"].ToString());
                m.FEEDBACKCONTENT = dt.Rows[i]["FEEDBACKCONTENT"].ToString();
                m.FEEDBACKSTATUS = dt.Rows[i]["FEEDBACKSTATUS"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 获取单条
        /// <summary>
        /// 根据查询条件获取某一条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static TASK_FEEDBACK_Model getModel(TASK_FEEDBACK_SW sw)
        {
            TASK_FEEDBACK_Model m = new TASK_FEEDBACK_Model();
            DataTable dt = BaseDT.TASK_FEEDBACK.getDT(sw);//列表

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.TASK_FEEDBACKID = dt.Rows[i]["TASK_FEEDBACKID"].ToString();
                m.HID = dt.Rows[i]["HID"].ToString();
                m.TASK_INFOID = dt.Rows[i]["TASK_INFOID"].ToString();
                m.RECEIVETIME = dt.Rows[i]["RECEIVETIME"].ToString();
                m.ACCEPTTIME = dt.Rows[i]["ACCEPTTIME"].ToString();
                m.FEEDBACKTIME = dt.Rows[i]["FEEDBACKTIME"].ToString();
                m.HNAME = BaseDT.T_IPSFR_USER.getName(dt.Rows[i]["HID"].ToString());
                m.FEEDBACKCONTENT = dt.Rows[i]["FEEDBACKCONTENT"].ToString();
                m.FEEDBACKSTATUS = dt.Rows[i]["FEEDBACKSTATUS"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion
    }
}

