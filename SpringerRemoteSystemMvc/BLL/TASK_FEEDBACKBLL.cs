using Springer.Common;
using Springer.DAL;
using Springer.EntityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Springer.BLL
{
    public partial class TASK_FEEDBACKBLL
    {
        private readonly TASK_FEEDBACKDAL dal = new TASK_FEEDBACKDAL();


        #region 获取列表
        /// <summary>
        /// 根据查询条件获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public List<TASK_FEEDBACK_Model> getModelList(string hid)
        {
            DataSet ds = dal.getDT(hid);
            var result = new List<TASK_FEEDBACK_Model>();
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TASK_FEEDBACK_Model m = new TASK_FEEDBACK_Model();
                m.TASK_FEEDBACKID = dt.Rows[i]["TASK_FEEDBACKID"].ToString();
                m.HID = dt.Rows[i]["HID"].ToString();
                m.TASK_INFOID = dt.Rows[i]["TASK_INFOID"].ToString();
                m.RECEIVETIME = dt.Rows[i]["RECEIVETIME"].ToString();
                m.ACCEPTTIME = dt.Rows[i]["ACCEPTTIME"].ToString();
                m.FEEDBACKTIME = dt.Rows[i]["FEEDBACKTIME"].ToString();
                //m.HNAME = BaseDT.T_IPSFR_USER.getName(dt.Rows[i]["HID"].ToString());
                m.FEEDBACKCONTENT = dt.Rows[i]["FEEDBACKCONTENT"].ToString();
                m.FEEDBACKSTATUS = dt.Rows[i]["FEEDBACKSTATUS"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        //public int Update(string hid, string taskinfoid,string accepttime)
        //{
        //    return dal.UpdateStatus(hid, taskinfoid, accepttime);
        //}

        //public int UpdateContent(string hid, string taskinfoid, string feedbacktime,string feedbackcontent)
        //{
        //    return dal.UpdateContent(hid, taskinfoid, feedbacktime, feedbackcontent);
        //}

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool UpdateFeedback(TASK_FEEDBACK_Model m) 
        {
            return dal.UpdateFeedback(m);
        }

        #endregion
    }
}
