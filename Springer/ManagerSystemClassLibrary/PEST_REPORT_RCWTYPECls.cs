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
    /// 有害生物_报表_人财物类别表
    /// </summary>
    public class PEST_REPORT_RCWTYPECls
    {
        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_REPORT_RCWTYPE_Model getModel(PEST_REPORT_RCWTYPE_SW sw)
        {
            DataTable dt = BaseDT.PEST_REPORT_RCWTYPE.getDT(sw);
            PEST_REPORT_RCWTYPE_Model m = new PEST_REPORT_RCWTYPE_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.RCWCODE = dt.Rows[i]["RCWCODE"].ToString();
                m.RCWNAME = dt.Rows[i]["RCWNAME"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.ISUSING = dt.Rows[i]["ISUSING"].ToString();
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
        public static IEnumerable<PEST_REPORT_RCWTYPE_Model> getListModel(PEST_REPORT_RCWTYPE_SW sw)
        {
            var result = new List<PEST_REPORT_RCWTYPE_Model>();
            DataTable dt = BaseDT.PEST_REPORT_RCWTYPE.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_REPORT_RCWTYPE_Model m = new PEST_REPORT_RCWTYPE_Model();
                m.RCWCODE = dt.Rows[i]["RCWCODE"].ToString();
                m.RCWNAME = dt.Rows[i]["RCWNAME"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.ISUSING = dt.Rows[i]["ISUSING"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 计算报表的列数集合
        /// <summary>
        /// 计算报表的列数,英文逗号分割
        /// </summary>
        /// <param name="_list">报表类型数据列表</param>
        /// <returns></returns>
        public static string GetREPORTCols(List<PEST_REPORT_RCWTYPE_Model> _list)
        {
            string str = "";
            for (int i = 0; i < _list.Count; i++)
            {
                List<PEST_REPORT_RCWTYPE_Model> _templist = getListModel(new PEST_REPORT_RCWTYPE_SW { RCWCODE = _list[i].RCWCODE }).ToList();
                if (i != _list.Count - 1)
                    str = str + _templist.Count + ",";
                else
                    str = str + _templist.Count;
            }
            return str;
        }
        #endregion
    }
}
