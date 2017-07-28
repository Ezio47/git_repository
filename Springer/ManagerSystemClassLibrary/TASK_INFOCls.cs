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
    /// 任务信息表
    /// </summary>
    public class TASK_INFOCls
    {
        #region 增、删、改
        /// <summary>
        /// 任务增删改
        /// </summary>
        /// <param name="m"></param>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static Message Manager(TASK_INFO_Model m, TASK_FEEDBACK_Model m1, TASK_TURNOVER_Model m2)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.TASK_INFO.Add(m);//任务信息表
                var TASK_INFOID = msgUser.Url;//获得添加记录的TASK_INFOID
                Message msgUser1 = BaseDT.TASK_FEEDBACK.Add(m1, TASK_INFOID);//任务反馈表
                Message msgUser2 = BaseDT.TASK_TURNOVER.Add(m2, TASK_INFOID);//流转表
                return new Message(msgUser2.Success, msgUser2.Msg, msgUser2.Url);
            }
            else if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.TASK_INFO.Del(m);
                Message msgUser1 = BaseDT.TASK_FEEDBACK.Del(m1);
                Message msgUser2 = BaseDT.TASK_TURNOVER.Add(m2, m.TASK_INFOID);//流转表 
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            else if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.TASK_INFO.Update(m);
                Message msgUser1 = BaseDT.TASK_FEEDBACK.Update(m1);
                Message msgUser2 = BaseDT.TASK_TURNOVER.Add(m2, m.TASK_INFOID);//流转表 
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            else if (m.opMethod == "End")
            {
                Message msgUser = BaseDT.TASK_INFO.Update(m);
                //Message msgUser1 = BaseDT.TASK_FEEDBACK.Update(m1);
                Message msgUser2 = BaseDT.TASK_TURNOVER.Add(m2, m.TASK_INFOID);//流转表 
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            return new Message(false, "无效操作", "");
        } 
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<TASK_INFO_Model> getModelList(TASK_INFO_SW sw, out int total)
        {
            var result = new List<TASK_INFO_Model>();

            DataTable dt = BaseDT.TASK_INFO.getDT(sw, out total);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TASK_INFO_Model m = new TASK_INFO_Model();
                m.TASK_INFOID = dt.Rows[i]["TASK_INFOID"].ToString();
                m.TASKTITLE = dt.Rows[i]["TASKTITLE"].ToString();
                m.TASKTYPE = dt.Rows[i]["TASKTYPE"].ToString();
                m.TASKLEVEL = dt.Rows[i]["TASKLEVEL"].ToString();
                m.TASKSTAUTS = dt.Rows[i]["TASKSTAUTS"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.TASKSTARTTIME = dt.Rows[i]["TASKSTARTTIME"].ToString();
                m.TASKBEGINTIME = dt.Rows[i]["TASKBEGINTIME"].ToString();
                m.TASKENDTIME = dt.Rows[i]["TASKENDTIME"].ToString();
                m.TASKCONTENT = dt.Rows[i]["TASKCONTENT"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return result;
        }
        #endregion

        #region 获取单条
        /// <summary>
        /// 根据查询条件获取某一条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static TASK_INFO_Model getModel(TASK_INFO_SW sw)
        {
            TASK_INFO_Model m = new TASK_INFO_Model();
            DataTable dt = BaseDT.TASK_INFO.getDT(sw);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位

            var result = TASK_FEEDBACKCls.getModelList(new TASK_FEEDBACK_SW { TASK_INFOID = sw.TASK_INFOID}).Where(p=>p.FEEDBACKSTATUS!="-1");
            string hname = "";
            string hid = "";
            foreach (var item in result)
            {
                hname += item.HNAME + ",";
                hid += item.HID + ",";
            }
            hname = hname.Substring(0, hname.Length - 1);
            hid = hid.Substring(0, hid.Length - 1);
            if (dt.Rows.Count > 0)
            {
                int i = 0;   
                m.TASK_INFOID = dt.Rows[i]["TASK_INFOID"].ToString();
                m.TASKTITLE = dt.Rows[i]["TASKTITLE"].ToString();
                m.TASKTYPE = dt.Rows[i]["TASKTYPE"].ToString();
                m.TASKLEVEL = dt.Rows[i]["TASKLEVEL"].ToString();
                m.TASKSTAUTS = dt.Rows[i]["TASKSTAUTS"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.TASKSTARTTIME = dt.Rows[i]["TASKSTARTTIME"].ToString();
                m.TASKBEGINTIME = dt.Rows[i]["TASKBEGINTIME"].ToString();
                m.TASKENDTIME = dt.Rows[i]["TASKENDTIME"].ToString();
                m.TASKCONTENT = dt.Rows[i]["TASKCONTENT"].ToString();
                m.HLYNAMELIST = hname;
                m.HIDLIST = hid;
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return m;
        }

        #endregion
    }
}
