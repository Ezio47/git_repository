using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 森林防火组织机构统计
    /// </summary>
    public class FIRERECORD_REPORT8Cls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型FIRERECORD_REPORT8_Model</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(FIRERECORD_REPORT8_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgMENU = BaseDT.FIRERECORD_REPORT8.Add(m);
                if (msgMENU.Success == false)
                    return new Message(msgMENU.Success, msgMENU.Msg, "");
                return new Message(msgMENU.Success, msgMENU.Msg, m.returnUrl);
            }
           
            return new Message(false, "无效操作!", m.returnUrl);
        }

        #endregion


        #region 获取森林防火统计单条数据
        /// <summary>
        /// 获取森林防火统计单条数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static FIRERECORD_REPORT8_Model getModel(FIRERECORD_REPORT8_SW sw)
        {
            DataTable dt = BaseDT.FIRERECORD_REPORT8.getDT(sw);
            FIRERECORD_REPORT8_Model m = new FIRERECORD_REPORT8_Model();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            if (dt != null && dt.Rows.Count > 0)
            {
                int i = 0;
                m.FIRERECORD_REPORT8ID = dt.Rows[i]["FIRERECORD_REPORT8ID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.REPORTYEAR = dt.Rows[i]["REPORTYEAR"].ToString();
                m.REPORTCODE = dt.Rows[i]["REPORTCODE"].ToString();
                m.SSXTYPELEVELCODE = dt.Rows[i]["SSXTYPELEVELCODE"].ToString();
                m.REPORTVALUE = dt.Rows[i]["REPORTVALUE"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return m;
        }
        #endregion

        #region 获取森林防火统计数据列表
        /// <summary>
        /// 获取森林防火统计数据列表
        /// </summary>
        /// <param name="sw">参见模型FIRERECORD_REPORT8_Model</param>
        /// <returns>参见模型FIRERECORD_REPORT8_Model</returns>
        public static IEnumerable<FIRERECORD_REPORT8_Model> getListModel(FIRERECORD_REPORT8_SW sw)
        {
            var result = new List<FIRERECORD_REPORT8_Model>();
            DataTable dt = BaseDT.FIRERECORD_REPORT8.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRERECORD_REPORT8_Model m = new FIRERECORD_REPORT8_Model();
                m.FIRERECORD_REPORT8ID = dt.Rows[i]["FIRERECORD_REPORT8ID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.REPORTYEAR = dt.Rows[i]["REPORTYEAR"].ToString();
                m.REPORTCODE = dt.Rows[i]["REPORTCODE"].ToString();
                m.SSXTYPELEVELCODE = dt.Rows[i]["SSXTYPELEVELCODE"].ToString();
                m.REPORTVALUE = dt.Rows[i]["REPORTVALUE"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
