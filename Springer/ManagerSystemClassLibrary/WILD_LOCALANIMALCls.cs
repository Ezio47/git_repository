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
    /// 本地化动物表
    /// </summary>
    public class WILD_LOCALANIMALCls
    {
        #region 管理
        /// <summary>
        /// 管理
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(WILD_LOCALANIMAL_Model m)
        {

            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.WILD_LOCALANIMAL.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            else if (m.opMethod == "Del")
            {
                Message msg = BaseDT.WILD_LOCALANIMAL.Del(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            return new Message(false, "无效操作", "");
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static WILD_LOCALANIMAL_Model getModel(WILD_LOCALANIMAL_SW sw)
        {
            DataTable dt = BaseDT.WILD_LOCALANIMAL.getDT(sw);
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
            WILD_LOCALANIMAL_Model m = new WILD_LOCALANIMAL_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.WILD_LOCALANIMALID = dt.Rows[i]["WILD_LOCALANIMALID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNONAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
                m.BIOLOGICALTYPECODENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.BIOLOGICALTYPECODE);
                m.PESTKECODE = m.BIOLOGICALTYPECODE.Substring(0, 10) + "0000";
                m.PESTKENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTKECODE);
                m.PESTSHUCODE = m.BIOLOGICALTYPECODE.Substring(0, 12) + "00";
                m.PESTSHUNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTSHUCODE);
            }
            dtORG.Clear();
            dtORG.Dispose();
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
        public static IEnumerable<WILD_LOCALANIMAL_Model> getListModel(WILD_LOCALANIMAL_SW sw)
        {
            var result = new List<WILD_LOCALANIMAL_Model>();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
            DataTable dt = BaseDT.WILD_LOCALANIMAL.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WILD_LOCALANIMAL_Model m = new WILD_LOCALANIMAL_Model();
                m.WILD_LOCALANIMALID = dt.Rows[i]["WILD_LOCALANIMALID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNONAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
                m.BIOLOGICALTYPECODENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.BIOLOGICALTYPECODE);
                m.PESTKECODE = m.BIOLOGICALTYPECODE.Substring(0, 10) + "0000";
                m.PESTKENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTKECODE);
                m.PESTSHUCODE = m.BIOLOGICALTYPECODE.Substring(0, 12) + "00";
                m.PESTSHUNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTSHUCODE);
                result.Add(m);
            }
            dtORG.Clear();
            dtORG.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 获取数据列表(分页)
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">总记录数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<WILD_LOCALANIMAL_Model> getListModel(WILD_LOCALANIMAL_SW sw, out int total)
        {
            var result = new List<WILD_LOCALANIMAL_Model>();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
            DataTable dt = BaseDT.WILD_LOCALANIMAL.getDT(sw, out total);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WILD_LOCALANIMAL_Model m = new WILD_LOCALANIMAL_Model();
                m.WILD_LOCALANIMALID = dt.Rows[i]["WILD_LOCALANIMALID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNONAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
                m.BIOLOGICALTYPECODENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.BIOLOGICALTYPECODE);
                m.PESTKECODE = m.BIOLOGICALTYPECODE.Substring(0, 10) + "0000";
                m.PESTKENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTKECODE);
                m.PESTSHUCODE = m.BIOLOGICALTYPECODE.Substring(0, 12) + "00";
                m.PESTSHUNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.PESTSHUCODE);
                result.Add(m);
            }
            dtORG.Clear();
            dtORG.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 获取所有科级及以下生物及对应地区已关联的生物列表
        /// <summary>
        /// 获取所有科级及以下生物及对应地区已关联的生物列表
        /// </summary>
        /// <param name="sw">sw</param>
        /// <returns></returns>
        public static IEnumerable<T_SYS_BIOLOGICALTYPE_WILD_LOCALANIMAL_Model> GetWILD_LOCALANIMALModel(WILD_LOCALANIMAL_SW sw)
        {
            List<WILD_LOCALANIMAL_Model> joinList = getListModel(new WILD_LOCALANIMAL_SW { BYORGNO = sw.BYORGNO }).ToList();
            DataTable dtbiolog = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW { BIOLOCODE = "01000000000000", IsOnlyGetKe = true });
            return GetWILD_LOCALANIMALModel(joinList, dtbiolog, "");
        }

        /// <summary>
        /// 获取所有科级及以下生物及对应地区、调查类型已关联的生物列表
        /// </summary>
        /// <param name="joinList"></param>
        /// <param name="dtbiolog"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private static IEnumerable<T_SYS_BIOLOGICALTYPE_WILD_LOCALANIMAL_Model> GetWILD_LOCALANIMALModel(List<WILD_LOCALANIMAL_Model> joinList, DataTable dtbiolog, string code)
        {
            var result = new List<T_SYS_BIOLOGICALTYPE_WILD_LOCALANIMAL_Model>();
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
                T_SYS_BIOLOGICALTYPE_WILD_LOCALANIMAL_Model m = new T_SYS_BIOLOGICALTYPE_WILD_LOCALANIMAL_Model();
                string chk = joinList.Exists(a => a.BIOLOGICALTYPECODE == dr[i]["BIOLOCODE"].ToString()) ? "1" : "0";
                m.isCheck = chk;
                m.BIOLOCODE = dr[i]["BIOLOCODE"].ToString();
                m.BIOLONAME = dr[i]["BIOLONAME"].ToString();
                if (!PublicCls.BioCodeIsZHong(dr[i]["BIOLOCODE"].ToString()))
                    m.subModel = GetWILD_LOCALANIMALModel(joinList, dtbiolog, dr[i]["BIOLOCODE"].ToString());
                result.Add(m);
            }
            return result;
        }
        #endregion
    }
}
