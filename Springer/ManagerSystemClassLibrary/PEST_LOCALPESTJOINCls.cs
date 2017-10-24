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
    /// 有害生物_本地化生物关联表
    /// </summary>
    public class PEST_LOCALPESTJOINCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_LOCALPESTJOIN_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.PEST_LOCALPESTJOIN.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            else if (m.opMethod == "Del")
            {
                Message msg = BaseDT.PEST_LOCALPESTJOIN.Del(m);
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
        public static PEST_LOCALPESTJOIN_Model getModel(PEST_LOCALPESTJOIN_SW sw)
        {
            DataTable dt = BaseDT.PEST_LOCALPESTJOIN.getDT(sw);
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt123 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "123" });//调查类型
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
            PEST_LOCALPESTJOIN_Model m = new PEST_LOCALPESTJOIN_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.PEST_LOCALPESTJOINID = dt.Rows[i]["PEST_LOCALPESTJOINID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNONAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.PESTBYCODE = dt.Rows[i]["PESTBYCODE"].ToString();
                m.PESTBYCODENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTBYCODE);
                m.PESTKECODE = m.PESTBYCODE.Substring(0, 10) + "0000";
                m.PESTKENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTKECODE);
                m.PESTSHUCODE = m.PESTBYCODE.Substring(0, 12) + "00";
                m.PESTSHUNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTSHUCODE);
                m.SEARCHTYPE = dt.Rows[i]["SEARCHTYPE"].ToString();
                m.SEARCHTYPENAME = BaseDT.T_SYS_DICT.getName(dt123, m.SEARCHTYPE);
            }
            dtORG.Clear();
            dtORG.Dispose();
            dt123.Clear();
            dt123.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
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
        public static IEnumerable<PEST_LOCALPESTJOIN_Model> getListModel(PEST_LOCALPESTJOIN_SW sw)
        {
            var result = new List<PEST_LOCALPESTJOIN_Model>();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt123 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "123" });//调查类型
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
            DataTable dt = BaseDT.PEST_LOCALPESTJOIN.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_LOCALPESTJOIN_Model m = new PEST_LOCALPESTJOIN_Model();
                m.PEST_LOCALPESTJOINID = dt.Rows[i]["PEST_LOCALPESTJOINID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNONAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.PESTBYCODE = dt.Rows[i]["PESTBYCODE"].ToString();
                m.PESTBYCODENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTBYCODE);
                m.PESTKECODE = m.PESTBYCODE.Substring(0, 10) + "0000";
                m.PESTKENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTKECODE);
                m.PESTSHUCODE = m.PESTBYCODE.Substring(0, 12) + "00";
                m.PESTSHUNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTSHUCODE);
                m.SEARCHTYPE = dt.Rows[i]["SEARCHTYPE"].ToString();
                m.SEARCHTYPENAME = BaseDT.T_SYS_DICT.getName(dt123, m.SEARCHTYPE);
                result.Add(m);
            }
            dtORG.Clear();
            dtORG.Dispose();
            dt123.Clear();
            dt123.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">总记录数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<PEST_LOCALPESTJOIN_Model> getListModel(PEST_LOCALPESTJOIN_SW sw, out int total)
        {
            var result = new List<PEST_LOCALPESTJOIN_Model>();
            DataTable dt = BaseDT.PEST_LOCALPESTJOIN.getDT(sw, out total);
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt123 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "123" });//调查类型
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_LOCALPESTJOIN_Model m = new PEST_LOCALPESTJOIN_Model();
                m.PEST_LOCALPESTJOINID = dt.Rows[i]["PEST_LOCALPESTJOINID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNONAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.PESTBYCODE = dt.Rows[i]["PESTBYCODE"].ToString();
                m.PESTBYCODENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTBYCODE);
                m.PESTKECODE = m.PESTBYCODE.Substring(0, 10) + "0000";
                m.PESTKENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTKECODE);
                m.PESTSHUCODE = m.PESTBYCODE.Substring(0, 12) + "00";
                m.PESTSHUNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTSHUCODE);
                m.SEARCHTYPE = dt.Rows[i]["SEARCHTYPE"].ToString();
                m.SEARCHTYPENAME = BaseDT.T_SYS_DICT.getName(dt123, m.SEARCHTYPE);
                result.Add(m);
            }
            dtORG.Clear();
            dtORG.Dispose();
            dt123.Clear();
            dt123.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region  获取下拉框
        /// <summary>
        /// 获取有害生物下拉框
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string GetPestSelectOption(PEST_LOCALPESTJOIN_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.PEST_LOCALPESTJOIN.getDT(sw);
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string code = dt.Rows[i]["PESTBYCODE"].ToString();
                    string name = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, code);
                    if (name != "")
                        sb.AppendFormat("<option value=\"{0}\" >{1}</option>", code, name);
                }
            }
            else
                sb.AppendFormat("<option value=\"\">==暂无有害生物==</option>");
            dt.Clear();
            dt.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            return sb.ToString();
        }

        /// <summary>
        /// 获取本地树种下拉框
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string GetTreeSelectOption(PEST_LOCALPESTJOIN_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.PEST_LOCALPESTJOIN.getDT2(sw);
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
                sb.AppendFormat("<option value=\"\">==暂无树种==</option>");
            dt.Clear();
            dt.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            return sb.ToString();
        }
        #endregion

        #region 获取所有科级及以下生物及对应地区、调查类型已关联的生物列表
        /// <summary>
        /// 获取所有科级及以下生物及对应地区、调查类型已关联的生物列表
        /// </summary>
        /// <param name="sw">sw</param>
        /// <returns></returns>
        public static IEnumerable<T_SYS_BIOLOGICALTYPE_BY_PESTJOIN_Model> GetBIOLOGByPESTJOINModel(PEST_LOCALPESTJOIN_SW sw)
        {
            List<PEST_LOCALPESTJOIN_Model> joinList = getListModel(new PEST_LOCALPESTJOIN_SW { BYORGNO = sw.BYORGNO, SEARCHTYPE = sw.SEARCHTYPE }).ToList();
            DataTable dtbiolog = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW { BIOLOCODE = "02000000000000", IsOnlyGetKe = true });
            return GetBIOLOGByPESTJOINModel(joinList, dtbiolog, "");
        }

        /// <summary>
        /// 获取所有科级及以下生物及对应地区、调查类型已关联的生物列表
        /// </summary>
        /// <param name="joinList"></param>
        /// <param name="dtbiolog"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private static IEnumerable<T_SYS_BIOLOGICALTYPE_BY_PESTJOIN_Model> GetBIOLOGByPESTJOINModel(List<PEST_LOCALPESTJOIN_Model> joinList, DataTable dtbiolog, string code)
        {
            var result = new List<T_SYS_BIOLOGICALTYPE_BY_PESTJOIN_Model>();
            DataRow[] dr = null;
            if (string.IsNullOrEmpty(code))
                dr = dtbiolog.Select("SUBSTRING(BIOLOCODE,9,2)<>'00' AND SUBSTRING(BIOLOCODE,11,4)='0000'");
            else
            {
                string str = "";
                if (PublicCls.BioCodeIsKe(code))
                {
                    str = PublicCls.GetKeBioCode(code);
                    dr = dtbiolog.Select("SUBSTRING(BIOLOCODE,1,10) = '" + str + "' AND SUBSTRING(BIOLOCODE,11,2)<>'00' AND SUBSTRING(BIOLOCODE,13,2)='00'");
                }
                else if (PublicCls.BioCodeIsShu(code))
                {
                    str = PublicCls.GetShuBioCode(code);
                    dr = dtbiolog.Select("SUBSTRING(BIOLOCODE,1,12) = '" + str + "' AND SUBSTRING(BIOLOCODE,13,2)<>'00'");
                }
            }
            for (int i = 0; i < dr.Length; i++)
            {
                T_SYS_BIOLOGICALTYPE_BY_PESTJOIN_Model m = new T_SYS_BIOLOGICALTYPE_BY_PESTJOIN_Model();
                string chk = joinList.Exists(a => a.PESTBYCODE == dr[i]["BIOLOCODE"].ToString()) ? "1" : "0";
                m.isCheck = chk;
                m.BIOLOCODE = dr[i]["BIOLOCODE"].ToString();
                m.BIOLONAME = dr[i]["BIOLONAME"].ToString();
                if (!PublicCls.BioCodeIsZHong(dr[i]["BIOLOCODE"].ToString()))
                    m.subModel = GetBIOLOGByPESTJOINModel(joinList, dtbiolog, dr[i]["BIOLOCODE"].ToString());
                result.Add(m);
            }
            return result;
        }
        #endregion
    }
}
