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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public class T_SYSSEC_RIGHTCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(T_SYSSEC_RIGHT_Model m)
        {

            if (m.opMethod == "Add")
            {
                SystemCls.LogSave("3", "权限:" + m.RIGHTNAME, ClsStr.getModelContent(m));
                return BaseDT.T_SYSSEC_RIGHT.Add(m);

            }
            if (m.opMethod == "Mdy")
            {
                SystemCls.LogSave("4", "权限:" + m.RIGHTNAME, ClsStr.getModelContent(m));
                return BaseDT.T_SYSSEC_RIGHT.Mdy(m);
            }
            if (m.opMethod == "Del")
            {
                SystemCls.LogSave("5", "权限:" + m.RIGHTNAME, ClsStr.getModelContent(m));
                return BaseDT.T_SYSSEC_RIGHT.Del(m);
            }

            return new Message(false, "无效操作", "");

        }

        #endregion

        #region 单条记录Model
        /// <summary>
        /// 根据查询条件获取某一条用户信息记录，用于修改、删除、用户登录验证
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_SYSSEC_RIGHT_Model getModel(T_SYSSEC_RIGHT_SW sw)
        {
            DataTable dt = BaseDT.T_SYSSEC_RIGHT.getDT(sw);
            T_SYSSEC_RIGHT_Model m = new T_SYSSEC_RIGHT_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.RIGHTID = dt.Rows[i]["RIGHTID"].ToString();
                m.RIGHTNAME = dt.Rows[i]["RIGHTNAME"].ToString();
                m.SYSFLAG = dt.Rows[i]["SYSFLAG"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();

                //扩充字段
            }
            return m;
        }
        #endregion

        #region 根据编号查询权限名称
        /// <summary>
        /// 根据编号查询权限名称
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string getNameByID(T_SYSSEC_RIGHT_SW sw)
        {
            return BaseDT.T_SYSSEC_RIGHT.getNameByID(sw);
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYSSEC_RIGHT_Model> getListModel(T_SYSSEC_RIGHT_SW sw)
        {
            var result = new List<T_SYSSEC_RIGHT_Model>();
            DataTable dt = BaseDT.T_SYSSEC_RIGHT.getDT(sw);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYSSEC_RIGHT_Model m = new T_SYSSEC_RIGHT_Model();
                m.RIGHTID = dt.Rows[i]["RIGHTID"].ToString();
                m.RIGHTNAME = dt.Rows[i]["RIGHTNAME"].ToString();
                m.SYSFLAG = dt.Rows[i]["SYSFLAG"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                result.Add(m);

            }
            dt.Clear();
            dt.Dispose();

            return result;
        }
        #endregion

        #region 获取所有权限、标识某一角色拥有的权限
        /// <summary>
        /// 获取所有权限、标识某一角色拥有的权限
        /// </summary>
        /// <param name="dt">权限DataTable</param>
        /// <param name="dtRole">角色DataTable</param>
        /// <param name="rightid">权限ID</param>
        /// <returns>参见模型</returns>
        private static IEnumerable<T_SYSSEC_RIGHT_ByRole_Model> getRightByRoleTmpModel(DataTable dt, DataTable dtRole, string rightid)
        {
            var result = new List<T_SYSSEC_RIGHT_ByRole_Model>();
            DataRow[] dr = dt.Select("len(RIGHTID)=" + (rightid.Length + 3).ToString() + "  AND SUBSTRING(RIGHTID,1," + (rightid.Length).ToString() + ")='" + rightid + "'", "ORDERBY");
            for (int i = 0; i < dr.Length; i++)
            {
                T_SYSSEC_RIGHT_ByRole_Model m = new T_SYSSEC_RIGHT_ByRole_Model();
                string chk = (BaseDT.T_SYSSEC_ROLE_RIGHT.isDTExists(dtRole, new T_SYSSEC_ROLE_RIGHT_SW { RIGHTID = dr[i]["RIGHTID"].ToString() }) == true) ? "1" : "0";
                m.isCheck = chk;
                m.RIGHTID = dr[i]["RIGHTID"].ToString();
                m.RIGHTNAME = dr[i]["RIGHTNAME"].ToString();
                m.subModel = getRightByRoleTmpModel(dt, dtRole, m.RIGHTID);
                result.Add(m);
            }
            return result;
        }
        /// <summary>
        /// 获取所有权限及对应角色是否具有该权限
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYSSEC_RIGHT_ByRole_Model> getRightByRoleModel(T_SYSSEC_ROLE_RIGHT_SW sw)
        {
            DataTable dtRight = BaseDT.T_SYSSEC_RIGHT.getDT(new T_SYSSEC_RIGHT_SW { SYSFLAG = ConfigCls.getSystemFlag() });
            DataTable dtRoleRight = BaseDT.T_SYSSEC_ROLE_RIGHT.getDT(new T_SYSSEC_ROLE_RIGHT_SW { ROLEID = sw.ROLEID });
            return getRightByRoleTmpModel(dtRight, dtRoleRight, "");
        }
        #endregion

        #region 返回某用户拥有的权限
        /// <summary>
        /// 返回某用户拥有的权限
        /// </summary>
        /// <param name="sw">sw.USERID 用户ID</param>
        /// <returns>返回有权限的列表，可用匹配，如",001,002,003,"</returns>
        public static string getRightStrByUID(T_SYSSEC_IPSUSER_SW sw)
        {

            DataTable dt = BaseDT.T_SYSSEC_ROLE_RIGHT.getDTByUID(new T_SYSSEC_IPSUSER_SW { USERID = sw.USERID });
            string str = ",";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str += dt.Rows[i]["RIGHTID"].ToString() + ",";
            }
            return str;
        }

        #endregion


        #region 树形菜单
        /// <summary>
        /// 类别树形菜单
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>string</returns>
        public static string getTypeTree(T_SYSSEC_RIGHT_SW sw)
        {
            JArray childobjArray = new JArray();
            DataTable dt = BaseDT.T_SYSSEC_RIGHT.getDT(sw);
            JArray JA = getTreeChild(sw, dt, "");//, dctypetopid);
            dt.Clear();
            dt.Dispose();
            return JsonConvert.SerializeObject(JA);
        }

        /// <summary>
        /// 获取类别子菜单
        /// </summary>
        /// <param name="sw">sw</param>
        /// <param name="dt">dt</param>
        /// <param name="SubRightID">SubRightID</param>
        /// <returns></returns>
        private static JArray getTreeChild(T_SYSSEC_RIGHT_SW sw, DataTable dt, string SubRightID)
        {
            JArray childobjArray = new JArray();
            DataRow[] dr = dt.Select("len(RIGHTID)=" + (SubRightID.Length + 3) + " and SUBSTRING(RIGHTID,1," + SubRightID.Length + ")='" + SubRightID + "'", "ORDERBY");
            string flag = ConfigCls.getSystemFlag();
            for (int i = 0; i < dr.Length; i++)
            {
                string text = dr[i]["RIGHTNAME"].ToString();
                if (dr[i]["SYSFLAG"].ToString() != flag)
                    text = "<font color=red>" + text + "</font>";
                JObject root = new JObject { { "id", dr[i]["RIGHTID"].ToString() }, { "text", text }, { "state", "open" }, };
                if (dr[i]["RIGHTID"].ToString().Length <= 3)
                    root.Add("children", getTreeChild(sw, dt, dr[i]["RIGHTID"].ToString()));
                childobjArray.Add(root);
            }
            return childobjArray;
        }

        #endregion
    }
}
