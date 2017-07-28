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
    /// 有害生物_报表_松材线虫病防治表
    /// </summary>
    public class PEST_REPORT_SCXCBFZCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_REPORT_SCXCBFZ_Model m)
        {
            return BaseDT.PEST_REPORT_SCXCBFZ.Save(m);
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_REPORT_SCXCBFZ_Model getModel(PEST_REPORT_SCXCBFZ_SW sw)
        {
            DataTable dt = BaseDT.PEST_REPORT_SCXCBFZ.getDT(sw);
            PEST_REPORT_SCXCBFZ_Model m = new PEST_REPORT_SCXCBFZ_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.PEST_REPORT_SCXCBFZID = dt.Rows[i]["PEST_REPORT_SCXCBFZID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.SCXCBFZYEAR = dt.Rows[i]["SCXCBFZYEAR"].ToString();
                m.SCXCBFZAREA = dt.Rows[i]["SCXCBFZAREA"].ToString();
                m.SCXCBFZPLANAREA = dt.Rows[i]["SCXCBFZPLANAREA"].ToString();
                m.SCXCBFZFINISHAREA = dt.Rows[i]["SCXCBFZFINISHAREA"].ToString();
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
        public static IEnumerable<PEST_REPORT_SCXCBFZ_Model> getListModel(PEST_REPORT_SCXCBFZ_SW sw)
        {
            var result = new List<PEST_REPORT_SCXCBFZ_Model>();
            DataTable dt = BaseDT.PEST_REPORT_SCXCBFZ.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_REPORT_SCXCBFZ_Model m = new PEST_REPORT_SCXCBFZ_Model();
                m.PEST_REPORT_SCXCBFZID = dt.Rows[i]["PEST_REPORT_SCXCBFZID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.SCXCBFZYEAR = dt.Rows[i]["SCXCBFZYEAR"].ToString();
                m.SCXCBFZAREA = dt.Rows[i]["SCXCBFZAREA"].ToString();
                m.SCXCBFZPLANAREA = dt.Rows[i]["SCXCBFZPLANAREA"].ToString();
                m.SCXCBFZFINISHAREA = dt.Rows[i]["SCXCBFZFINISHAREA"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
