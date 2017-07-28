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
    /// 有害生物_本地化树种信息表
    /// </summary>
    public class PEST_LOCALTREESPECIESCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_LOCALTREESPECIES_Model m)
        {
            return BaseDT.PEST_LOCALTREESPECIES.Save(m);
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_LOCALTREESPECIES_Model getModel(PEST_LOCALTREESPECIES_SW sw)
        {
            DataTable dt = BaseDT.PEST_LOCALTREESPECIES.getDT(sw);
            PEST_LOCALTREESPECIES_Model m = new PEST_LOCALTREESPECIES_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.PEST_LOCALTREESPECIESID = dt.Rows[i]["PEST_LOCALTREESPECIESID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.TSPCODE = dt.Rows[i]["TSPCODE"].ToString();
                m.TSPAREA = dt.Rows[i]["TSPAREA"].ToString();
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
        public static IEnumerable<PEST_LOCALTREESPECIES_Model> getListModel(PEST_LOCALTREESPECIES_SW sw)
        {
            var result = new List<PEST_LOCALTREESPECIES_Model>();
            DataTable dt = BaseDT.PEST_LOCALTREESPECIES.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_LOCALTREESPECIES_Model m = new PEST_LOCALTREESPECIES_Model();
                m.PEST_LOCALTREESPECIESID = dt.Rows[i]["PEST_LOCALTREESPECIESID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.TSPCODE = dt.Rows[i]["TSPCODE"].ToString();
                m.TSPAREA = dt.Rows[i]["TSPAREA"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 获取面积列表
        /// <summary>
        /// 获取面积列表
        /// </summary>
        /// <param name="PESTCODE">有害生物编码</param>
        /// <returns></returns>
        public static IEnumerable<PEST_LOCALTREESPECIES_Model> getListAREA(string PESTCODE)
        {
            var result = new List<PEST_LOCALTREESPECIES_Model>();
            DataTable dt = BaseDT.PEST_LOCALTREESPECIES.getAREADT(PESTCODE);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_LOCALTREESPECIES_Model m = new PEST_LOCALTREESPECIES_Model();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.TSPCODE = dt.Rows[i]["TSPCODE"].ToString();
                m.TSPAREA = dt.Rows[i]["TSPAREA"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        
        #endregion
    }
}
