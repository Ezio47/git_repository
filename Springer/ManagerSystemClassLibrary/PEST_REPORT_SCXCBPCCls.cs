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
    /// 有害生物_报表_松材线虫病普查表
    /// </summary>
    public class PEST_REPORT_SCXCBPCCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_REPORT_SCXCBPC_Model m)
        {
            return BaseDT.PEST_REPORT_SCXCBPC.Save(m);
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_REPORT_SCXCBPC_Model getModel(PEST_REPORT_SCXCBPC_SW sw)
        {
            DataTable dt = BaseDT.PEST_REPORT_SCXCBPC.getDT(sw);
            PEST_REPORT_SCXCBPC_Model m = new PEST_REPORT_SCXCBPC_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.PEST_REPORT_SCXCBPCID = dt.Rows[i]["PEST_REPORT_SCXCBPCID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.SCXCBPCYEAR = dt.Rows[i]["SCXCBPCYEAR"].ToString();
                m.SCXCBPCSEASONCODE = dt.Rows[i]["SCXCBPCSEASONCODE"].ToString();
                m.SCXCBPCTYPECODE = dt.Rows[i]["SCXCBPCTYPECODE"].ToString();
                m.SCXCBPCVALUE = dt.Rows[i]["SCXCBPCVALUE"].ToString();
                //扩充字段
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }

        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型PEST_REPORT_HAPPEN_SW</param>
        /// <returns>参见模型PEST_REPORT_HAPPEN_Model</returns>
        public static IEnumerable<PEST_REPORT_SCXCBPC_Model> getListModel(PEST_REPORT_SCXCBPC_SW sw)
        {
            var result = new List<PEST_REPORT_SCXCBPC_Model>();
            DataTable dt = BaseDT.PEST_REPORT_SCXCBPC.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_REPORT_SCXCBPC_Model m = new PEST_REPORT_SCXCBPC_Model();
                m.PEST_REPORT_SCXCBPCID = dt.Rows[i]["PEST_REPORT_SCXCBPCID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.SCXCBPCYEAR = dt.Rows[i]["SCXCBPCYEAR"].ToString();
                m.SCXCBPCSEASONCODE = dt.Rows[i]["SCXCBPCSEASONCODE"].ToString();
                m.SCXCBPCTYPECODE = dt.Rows[i]["SCXCBPCTYPECODE"].ToString();
                m.SCXCBPCVALUE = dt.Rows[i]["SCXCBPCVALUE"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
