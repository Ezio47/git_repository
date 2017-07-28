using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 系统参数管理
    /// </summary>
    public class T_SYS_PARAMETERCls
    {
        #region 修改
        
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(T_SYS_PARAMETER_Model m)
        {
            if (m.opMETHOD == "Mdy")
            {
                return BaseDT.T_SYS_PARAMETER.Mdy(m);
            }
            return new Message(false, "无效操作", "");
        }
        #endregion

        #region 根据系统标识和参数标识符获取参数值
        /// <summary>
        /// 根据系统标识和参数标识符获取参数值
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参数值</returns>
        public static string getValueByFlag(T_SYS_PARAMETER_SW sw)
        {
            var v = getModel(sw);
            return v.PARAMVALUE;
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="SW">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_SYS_PARAMETER_Model getModel(T_SYS_PARAMETER_SW SW)
        {
            DataTable dt = BaseDT.T_SYS_PARAMETER.getDT(SW);
            T_SYS_PARAMETER_Model m = new T_SYS_PARAMETER_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.PARAMID = dt.Rows[i]["PARAMID"].ToString();
                m.PARAMFLAG = dt.Rows[i]["PARAMFLAG"].ToString();
                m.PARAMNAME = dt.Rows[i]["PARAMNAME"].ToString();
               m.PARAMVALUE = dt.Rows[i]["PARAMVALUE"].ToString();
               m.PARAMMARK = dt.Rows[i]["PARAMMARK"].ToString();
                //扩充字段
            }
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYS_PARAMETER_Model> getListModel(T_SYS_PARAMETER_SW sw)
        {
            var result = new List<T_SYS_PARAMETER_Model>();

            DataTable dt = BaseDT.T_SYS_PARAMETER.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_PARAMETER_Model m = new T_SYS_PARAMETER_Model();
                m.PARAMID = dt.Rows[i]["PARAMID"].ToString();
                m.PARAMFLAG = dt.Rows[i]["PARAMFLAG"].ToString();
                m.PARAMNAME = dt.Rows[i]["PARAMNAME"].ToString();
                m.PARAMVALUE = dt.Rows[i]["PARAMVALUE"].ToString();
                m.PARAMMARK = dt.Rows[i]["PARAMMARK"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();

            return result;
        }
        #endregion

    }
}