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
    /// 有害生物表
    /// </summary>
    public class T_SYS_PESTCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型T_SYS_PEST_Model</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(T_SYS_PEST_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgPEST = BaseDT.T_SYS_PEST.Add(m);
                if (msgPEST.Success == false)
                    return new Message(msgPEST.Success, msgPEST.Msg, "");
                return new Message(msgPEST.Success, msgPEST.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgPEST = BaseDT.T_SYS_PEST.Mdy(m);
                if (msgPEST.Success == false)
                    return new Message(msgPEST.Success, msgPEST.Msg, "");
                return new Message(msgPEST.Success, msgPEST.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {
                Message msgPEST = BaseDT.T_SYS_PEST.Del(m);
                if (msgPEST.Success == false)
                    return new Message(msgPEST.Success, msgPEST.Msg, "");
                return new Message(msgPEST.Success, msgPEST.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", m.returnUrl);
        }

        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见条件模型T_SYS_PEST_SW</param>
        /// <returns>参见模型T_SYS_PEST_Model</returns>
        public static T_SYS_PEST_Model getModel(T_SYS_PEST_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_PEST.getModel(sw);
            T_SYS_PEST_Model m = new T_SYS_PEST_Model();
            if (dt.Rows.Count > 0)
            {
                //数据库表字段
                m.PESTCODE = dt.Rows[0]["PESTCODE"].ToString();
                m.PESTNAME = dt.Rows[0]["PESTNAME"].ToString();
                m.LATINNAME = dt.Rows[0]["LATINNAME"].ToString();
                m.ISLOCAL = dt.Rows[0]["ISLOCAL"].ToString();
                m.ORDERBY = dt.Rows[0]["ORDERBY"].ToString();
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
        /// <param name="sw">参见模型T_SYS_PEST_SW</param>
        /// <returns>参见模型T_SYS_PEST_Model</returns>
        public static IEnumerable<T_SYS_PEST_Model> getListModel(T_SYS_PEST_SW sw)
        {
            var result = new List<T_SYS_PEST_Model>();
            DataTable dt = BaseDT.T_SYS_PEST.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_PEST_Model m = new T_SYS_PEST_Model();
                m.PESTCODE = dt.Rows[i]["PESTCODE"].ToString();
                m.PESTNAME = dt.Rows[i]["PESTNAME"].ToString();
                m.LATINNAME = dt.Rows[i]["LATINNAME"].ToString();
                m.ISLOCAL = dt.Rows[0]["ISLOCAL"].ToString();
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
        /// <param name="PESTCODE">编码</param>
        /// <returns>名称</returns>
        public static string getName(string PESTCODE)
        {
            return BaseDT.T_SYS_PEST.getName(PESTCODE);
        }

        #endregion

        #region 判断是否有下级
        /// <summary>
        /// 判断是否有下级
        /// </summary>
        /// <param name="sw">参见模型T_SYS_PEST_Model</param>
        /// <returns>true:存在 false：不存在</returns>
        public static bool isExistsChild(T_SYS_PEST_SW sw)
        {
            return BaseDT.T_SYS_PEST.isExistsChild(sw);
        }
        #endregion

        #region 获取最长编码长度
        /// <summary>
        /// 获取最长编码长度
        /// </summary>
        /// <returns></returns>
        public static int GetMaxCodeLength()
        {
            return Convert.ToInt32(BaseDT.T_SYS_PEST.GetMaxCodeLength());
        }
        #endregion

        #region 获取有害生物类型下拉框
        /// <summary>
        /// 获取有害生物下拉框
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string getSelectOption(T_SYS_PEST_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.T_SYS_PEST.getDT(sw);
            if (sw.IsShowALL)
                sb.AppendFormat("<option value=\"{0}\">==所有==</option>","All");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string code = dt.Rows[i]["PESTCODE"].ToString();
                    string name = dt.Rows[i]["PESTNAME"].ToString();
                    if (!string.IsNullOrEmpty(sw.PESTCODE))
                    {
                        int x = (code.Length - sw.PESTCODE.Length) / 2;
                        if (x == 0 || x == 1) name = "" + name;
                        if (x == 2) name = "--" + name;
                        if (x == 3) name = "----" + name;
                        if (x == 4) name = "--------" + name;
                        if (x == 5) name = "----------" + name;
                        if (x == 6) name = "------------" + name;
                        if (x == 7) name = "--------------" + name;
                        if (x == 8) name = "----------------" + name;
                        if (x == 9) name = "------------------" + name;
                    }
                    else
                    {
                        int x = code.Length / 2;
                        if (x == 1) name = "" + name;
                        if (x == 2) name = "--" + name;
                        if (x == 3) name = "----" + name;
                        if (x == 4) name = "------" + name;
                        if (x == 5) name = "--------" + name;
                        if (x == 6) name = "----------" + name;
                        if (x == 7) name = "------------" + name;
                        if (x == 8) name = "--------------" + name;
                        if (x == 9) name = "----------------" + name;
                        if (x == 10) name = "-----------------" + name;
                    }
                    sb.AppendFormat("<option value=\"{0}\" >{1}</option>", code, name);
                }
            }
            else
                sb.AppendFormat("<option value=\"{0}\">==暂无害虫==</option>","");
            dt.Clear();
            dt.Dispose();
            return sb.ToString();
        }
        #endregion
    }
}
