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
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.PEST_LOCALTREESPECIES.Add(m);
                return new Message(msg.Success, msg.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.PEST_LOCALTREESPECIES.Mdy(m);
                return new Message(msg.Success, msg.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.PEST_LOCALTREESPECIES.Del(m);
                return new Message(msg.Success, msg.Msg, m.returnUrl);
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
        public static PEST_LOCALTREESPECIES_Model getModel(PEST_LOCALTREESPECIES_SW sw)
        {
            DataTable dt = BaseDT.PEST_LOCALTREESPECIES.getDT(sw);
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
            PEST_LOCALTREESPECIES_Model m = new PEST_LOCALTREESPECIES_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.PEST_LOCALTREESPECIESID = dt.Rows[i]["PEST_LOCALTREESPECIESID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNONAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.TSPCODE = dt.Rows[i]["TSPCODE"].ToString();
                m.TSPNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.TSPCODE);
                m.TSPKECODE = m.TSPCODE.Substring(0, 10) + "0000";
                m.TSPKENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.TSPKECODE);
                m.TSPSHUCODE = m.TSPCODE.Substring(0, 12) + "00";
                m.TSPSHUNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.TSPSHUCODE);
                m.TSPAREA = dt.Rows[i]["TSPAREA"].ToString();
                //扩充字段
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<PEST_LOCALTREESPECIES_Model> getListModel(PEST_LOCALTREESPECIES_SW sw)
        {
            var result = new List<PEST_LOCALTREESPECIES_Model>();
            DataTable dt = BaseDT.PEST_LOCALTREESPECIES.getDT(sw);
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_LOCALTREESPECIES_Model m = new PEST_LOCALTREESPECIES_Model();
                m.PEST_LOCALTREESPECIESID = dt.Rows[i]["PEST_LOCALTREESPECIESID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNONAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.TSPCODE = dt.Rows[i]["TSPCODE"].ToString();
                m.TSPNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.TSPCODE);
                m.TSPKECODE = m.TSPCODE.Substring(0, 10) + "0000";
                m.TSPKENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.TSPKECODE);
                m.TSPSHUCODE = m.TSPCODE.Substring(0, 12) + "00";
                m.TSPSHUNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.TSPSHUCODE);
                m.TSPAREA = dt.Rows[i]["TSPAREA"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
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
        public static IEnumerable<PEST_LOCALTREESPECIES_Model> getListModel(PEST_LOCALTREESPECIES_SW sw,out int total)
        {
            var result = new List<PEST_LOCALTREESPECIES_Model>();
            DataTable dt = BaseDT.PEST_LOCALTREESPECIES.getDT(sw,out  total);
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_LOCALTREESPECIES_Model m = new PEST_LOCALTREESPECIES_Model();
                m.PEST_LOCALTREESPECIESID = dt.Rows[i]["PEST_LOCALTREESPECIESID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNONAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.TSPCODE = dt.Rows[i]["TSPCODE"].ToString();
                m.TSPNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.TSPCODE);
                m.TSPKECODE = m.TSPCODE.Substring(0, 10) + "0000";
                m.TSPKENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.TSPKECODE);
                m.TSPSHUCODE = m.TSPCODE.Substring(0, 12) + "00";
                m.TSPSHUNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.TSPSHUCODE);
                m.TSPAREA = dt.Rows[i]["TSPAREA"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            return result;
        }
        #endregion

        #region 获取本地树种下拉列表
        /// <summary>
        /// 字典名称下拉框
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string getSelectOption(PEST_LOCALTREESPECIES_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.PEST_LOCALTREESPECIES.getDT(sw);
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
            if (sw.IsShowAll == "1")
                sb.AppendFormat("<option value=\"\">{0}</option>", "--所有--");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string code = dt.Rows[i]["TSPCODE"].ToString();
                string name = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, code);
                if (sw.CurTSPCODE == code)
                    sb.AppendFormat("<option value=\"{0}\" selected>{1}</option>", code, name);
                else
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", code, name);
            }
            dt.Clear();
            dt.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            return sb.ToString();
        }

        #endregion

        #region 根据编码获取面积列表
        /// <summary>
        /// 根据编码获取面积列表
        /// </summary>
        /// <param name="PESTCODE">生物编码</param>
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
