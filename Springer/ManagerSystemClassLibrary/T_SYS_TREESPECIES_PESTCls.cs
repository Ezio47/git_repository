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
    /// 有害生物与树种对应表
    /// </summary>
    public class T_SYS_TREESPECIES_PESTCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(T_SYS_TREESPECIES_PEST_Model m)
        {
            return BaseDT.T_SYS_TREESPECIES_PEST.Save(m);
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_SYS_TREESPECIES_PEST_Model getModel(T_SYS_TREESPECIES_PEST_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_TREESPECIES_PEST.getDT(sw);
            T_SYS_TREESPECIES_PEST_Model m = new T_SYS_TREESPECIES_PEST_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.PESTCODE = dt.Rows[i]["PESTCODE"].ToString();
                m.TSPCODE = dt.Rows[i]["TSPCODE"].ToString();
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
        /// <param name="sw">参见模型T_SYS_PEST_OBSERVEAREA_SW</param>
        /// <returns>参见模型T_SYS_TREESPECIES_PEST_Model</returns>
        public static IEnumerable<T_SYS_TREESPECIES_PEST_Model> getListModel(T_SYS_TREESPECIES_PEST_SW sw)
        {
            var result = new List<T_SYS_TREESPECIES_PEST_Model>();
            DataTable dt = BaseDT.T_SYS_TREESPECIES_PEST.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_TREESPECIES_PEST_Model m = new T_SYS_TREESPECIES_PEST_Model();
                m.PESTCODE = dt.Rows[i]["PESTCODE"].ToString();
                m.TSPCODE = dt.Rows[i]["TSPCODE"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
