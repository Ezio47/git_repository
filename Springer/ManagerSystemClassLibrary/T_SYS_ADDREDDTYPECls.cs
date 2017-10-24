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
    /// 通讯录类别管理
    /// </summary>
    public class T_SYS_ADDREDDTYPECls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(T_SYS_ADDREDDTYPE_Model m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_SYS_ADDREDDTYPE.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_SYS_ADDREDDTYPE.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_SYS_ADDREDDTYPE.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");
        }
        #endregion

        #region 获取单条数据
        /// <summary>
        /// 根据查询条件获取某一条信息记录，用于修改、删除、用户登录验证
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_SYS_ADDREDDTYPE_Model getModel(T_SYS_ADDREDDTYPE_SW sw)
        {
            var result = new List<T_SYS_ADDREDDTYPE_Model>();
            DataTable dt = BaseDT.T_SYS_ADDREDDTYPE.getDT(sw);//列表
            T_SYS_ADDREDDTYPE_Model m = new T_SYS_ADDREDDTYPE_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.ATID = dt.Rows[i]["ATID"].ToString();
                m.RATID = dt.Rows[i]["RATID"].ToString();
                m.RTNAME = dt.Rows[i]["RTNAME"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
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
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<T_SYS_ADDREDDTYPE_Model> getModelList(T_SYS_ADDREDDTYPE_SW sw)
        {
            var result = new List<T_SYS_ADDREDDTYPE_Model>();
            DataTable dt = BaseDT.T_SYS_ADDREDDTYPE.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_ADDREDDTYPE_Model m = new T_SYS_ADDREDDTYPE_Model();
                m.ATID = dt.Rows[i]["ATID"].ToString();
                m.RATID = dt.Rows[i]["RATID"].ToString();
                m.RTNAME = dt.Rows[i]["RTNAME"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 下拉框
        /// <summary>
        /// 下拉框
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getSelectOption(T_SYS_ADDREDDTYPE_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_ADDREDDTYPE.getDT(sw);
            string str = getSelectOptionChiled(dt, sw.CurATID, "0", 0);
            dt.Clear();
            dt.Dispose();
            return str;
        }

        private static string getSelectOptionChiled(DataTable dt, string curID, string id, int index)
        {
            string str = "";
            DataRow[] dr = dt.Select("RATID=" + id, "ORDERBY");
            for (int i = 0; i < dr.Length; i++)
            {
                string ATID = dr[i]["ATID"].ToString();
                string chk = (curID == ATID) ? " selected" : "";
                str += "<option value=\"" + ATID + "\"";
                str += chk;
                str += ">";
                for (int k = 0; k < index; k++)
                    str += "　";
                str += dr[i]["RTNAME"].ToString();
                str += "</option>";
                str += getSelectOptionChiled(dt, curID, ATID, index + 1);
            }
            return str;
        }
        #endregion
    }
}
