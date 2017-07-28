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
    /// 有害生物_专家会诊主题表
    /// </summary>
    public class PEST_CONSULTATIONCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_CONSULTATION_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.PEST_CONSULTATION.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.PEST_CONSULTATION.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.PEST_CONSULTATION.Del(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            return new Message(false, "无效操作", "");
        }

        #endregion

        #region 获取单条数据
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_CONSULTATION_Model getModel(PEST_CONSULTATION_SW sw)
        {
            DataTable dt = BaseDT.PEST_CONSULTATION.getDT(sw);//列表
            PEST_CONSULTATION_Model m = new PEST_CONSULTATION_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.PEST_CONSULTATIONID = dt.Rows[i]["PEST_CONSULTATIONID"].ToString();
                m.CONSULTITLE = dt.Rows[i]["CONSULTITLE"].ToString();
                m.CONSULPHONE = dt.Rows[i]["CONSULPHONE"].ToString();
                m.CONSULTIME = ClsSwitch.SwitMN(dt.Rows[i]["CONSULTIME"].ToString());
                m.CONSULCONTENT = dt.Rows[i]["CONSULCONTENT"].ToString();
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
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<PEST_CONSULTATION_Model> getListModel(PEST_CONSULTATION_SW sw, out int total)
        {
            var result = new List<PEST_CONSULTATION_Model>();
            DataTable dt = BaseDT.PEST_CONSULTATION.getDT(sw, out total);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_CONSULTATION_Model m = new PEST_CONSULTATION_Model();
                m.PEST_CONSULTATIONID = dt.Rows[i]["PEST_CONSULTATIONID"].ToString();
                m.CONSULTITLE = dt.Rows[i]["CONSULTITLE"].ToString();
                m.CONSULPHONE = dt.Rows[i]["CONSULPHONE"].ToString();
                m.CONSULTIME = ClsSwitch.SwitMN(dt.Rows[i]["CONSULTIME"].ToString());
                m.CONSULCONTENT = dt.Rows[i]["CONSULCONTENT"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
