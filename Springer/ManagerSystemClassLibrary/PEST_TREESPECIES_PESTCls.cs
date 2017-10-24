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
    public class PEST_TREESPECIES_PESTCls
    {
        #region 增、删、改
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_TREESPECIES_PEST_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.PEST_TREESPECIES_PEST.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            else if (m.opMethod == "Del")
            {
                Message msg = BaseDT.PEST_TREESPECIES_PEST.Del(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            return new Message(false, "无效操作!", "");
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_TREESPECIES_PEST_Model getModel(PEST_TREESPECIES_PEST_SW sw)
        {
            DataTable dt = BaseDT.PEST_TREESPECIES_PEST.getDT(sw);
            PEST_TREESPECIES_PEST_Model m = new PEST_TREESPECIES_PEST_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.PEST_TREESPECIES_PESTID = dt.Rows[i]["PEST_TREESPECIES_PESTID"].ToString();
                m.TREESPECIESCODE = dt.Rows[i]["TREESPECIESCODE"].ToString();
                m.PESTBYCODE = dt.Rows[i]["PESTBYCODE"].ToString();
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
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<PEST_TREESPECIES_PEST_Model> getListModel(PEST_TREESPECIES_PEST_SW sw)
        {
            var result = new List<PEST_TREESPECIES_PEST_Model>();
            DataTable dt = BaseDT.PEST_TREESPECIES_PEST.getDT(sw);
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_TREESPECIES_PEST_Model m = new PEST_TREESPECIES_PEST_Model();
                m.PEST_TREESPECIES_PESTID = dt.Rows[i]["PEST_TREESPECIES_PESTID"].ToString();
                m.TREESPECIESCODE = dt.Rows[i]["TREESPECIESCODE"].ToString();
                m.TREESPECIESNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.TREESPECIESCODE);
                m.PESTBYCODE = dt.Rows[i]["PESTBYCODE"].ToString();
                m.PESTBYNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTBYCODE);
                m.PESTKECODE = m.PESTBYCODE.Substring(0, 10) + "0000";
                m.PESTKENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTKECODE);
                m.PESTSHUCODE = m.PESTBYCODE.Substring(0, 12) + "00";
                m.PESTSHUNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTSHUCODE);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            return result;
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">总记录数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<PEST_TREESPECIES_PEST_Model> getListModel(PEST_TREESPECIES_PEST_SW sw, out int total)
        {
            var result = new List<PEST_TREESPECIES_PEST_Model>();
            DataTable dt = BaseDT.PEST_TREESPECIES_PEST.getDT(sw, out total);
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_TREESPECIES_PEST_Model m = new PEST_TREESPECIES_PEST_Model();
                m.PEST_TREESPECIES_PESTID = dt.Rows[i]["PEST_TREESPECIES_PESTID"].ToString();             
                m.TREESPECIESCODE = dt.Rows[i]["TREESPECIESCODE"].ToString();
                m.TREESPECIESNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.TREESPECIESCODE);
                m.PESTBYCODE = dt.Rows[i]["PESTBYCODE"].ToString();
                m.PESTBYNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTBYCODE);
                m.PESTKECODE = m.PESTBYCODE.Substring(0, 10) + "0000";
                m.PESTKENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTKECODE);
                m.PESTSHUCODE = m.PESTBYCODE.Substring(0, 12) + "00";
                m.PESTSHUNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTSHUCODE);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            return result;
        }
        #endregion

        #region  获取本地树种下拉框
        /// <summary>
        /// 获取本地树种下拉框
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string getSelectOption(PEST_TREESPECIES_PEST_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.PEST_TREESPECIES_PEST.getDT(sw);
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string code = dt.Rows[i]["PESTBYCODE"].ToString();
                    string name = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, code);
                    sb.AppendFormat("<option value=\"{0}\" >{1}</option>", code, name);
                }
            }
            else
                sb.AppendFormat("<option value=\"\">==暂无病虫==</option>");
            dt.Clear();
            dt.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            return sb.ToString();
        }
        #endregion
    }
}
