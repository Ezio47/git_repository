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
    /// 树种关系表
    /// </summary>
    public class T_SYS_TREESPECIESCls
    {
        #region 增删改
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="m">参见模型T_SYS_TREESPECIES_Model</param>
        /// <returns></returns>
        public static Message Manager(T_SYS_TREESPECIES_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgTREESPECIES = BaseDT.T_SYS_TREESPECIES.Add(m);
                if (msgTREESPECIES.Success == false)
                    return new Message(msgTREESPECIES.Success, msgTREESPECIES.Msg, "");
                return new Message(msgTREESPECIES.Success, msgTREESPECIES.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgTREESPECIES = BaseDT.T_SYS_TREESPECIES.Mdy(m);
                if (msgTREESPECIES.Success == false)
                    return new Message(msgTREESPECIES.Success, msgTREESPECIES.Msg, "");
                return new Message(msgTREESPECIES.Success, msgTREESPECIES.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {
                Message msgTREESPECIES = BaseDT.T_SYS_TREESPECIES.Del(m);
                if (msgTREESPECIES.Success == false)
                    return new Message(msgTREESPECIES.Success, msgTREESPECIES.Msg, "");
                return new Message(msgTREESPECIES.Success, msgTREESPECIES.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", m.returnUrl);
        }

        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见条件模型T_SYS_TREESPECIES_SW</param>
        /// <returns>参见模型T_SYS_TREESPECIES_Model</returns>
        public static T_SYS_TREESPECIES_Model getModel(T_SYS_TREESPECIES_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_TREESPECIES.getModel(sw);
            T_SYS_TREESPECIES_Model m = new T_SYS_TREESPECIES_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.TSPCODE = dt.Rows[i]["TSPCODE"].ToString();
                m.TSPNAME = dt.Rows[i]["TSPNAME"].ToString();
                m.LATINNAME = dt.Rows[i]["LATINNAME"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                //扩充字段
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
        /// <param name="sw">参见条件模型T_SYS_TREESPECIES_SW</param>
        /// <returns>参见模型T_SYS_TREESPECIES_Model</returns>
        public static IEnumerable<T_SYS_TREESPECIES_Model> getListModel(T_SYS_TREESPECIES_SW sw)
        {
            var result = new List<T_SYS_TREESPECIES_Model>();
            DataTable dt = BaseDT.T_SYS_TREESPECIES.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_TREESPECIES_Model m = new T_SYS_TREESPECIES_Model();
                m.TSPCODE = dt.Rows[i]["TSPCODE"].ToString();
                m.TSPNAME = dt.Rows[i]["TSPNAME"].ToString();
                m.LATINNAME = dt.Rows[i]["LATINNAME"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 根据编码获取名称

        /// <summary>
        /// 根据编码获取名称
        /// </summary>
        /// <param name="TSPCODE">编码</param>
        /// <returns>名称</returns>
        public static string getName(string TSPCODE)
        {
            return BaseDT.T_SYS_TREESPECIES.getName(TSPCODE);
        }

        #endregion

        #region 判断是否有下级
        /// <summary>
        /// 判断是否有下级
        /// </summary>
        /// <param name="sw">参见模型T_SYS_TREESPECIES_Model</param>
        /// <returns>true:存在 false：不存在</returns>
        public static bool isExistsChild(T_SYS_TREESPECIES_SW sw)
        {
            return BaseDT.T_SYS_TREESPECIES.isExistsChild(sw);
        }
        #endregion

        #region 获取树种下拉框
        /// <summary>
        /// 获取树种下拉框
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string getSelectOption(T_SYS_TREESPECIES_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.T_SYS_TREESPECIES.getDT(sw);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string code = dt.Rows[i]["TSPCODE"].ToString();
                    string name = dt.Rows[i]["TSPNAME"].ToString();
                    if (code.Length == 2)
                        name = "" + name;
                    if (code.Length == 4)
                        name = "--" + name;
                    if (code.Length == 6)
                        name = "----" + name;
                    if (i == 0)
                        sb.AppendFormat("<option value=\"{0}\" selected>{1}</option>", code, name);
                    else
                        sb.AppendFormat("<option value=\"{0}\" >{1}</option>", code, name);
                }
            }
            else
                sb.AppendFormat("<option>==暂无树种==</option>");
            dt.Clear();
            dt.Dispose();
            return sb.ToString();
        }
        #endregion
    }
}
