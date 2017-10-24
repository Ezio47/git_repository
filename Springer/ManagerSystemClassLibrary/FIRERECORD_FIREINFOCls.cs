using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 火情档案管理
    /// </summary>
    public class FIRERECORD_FIREINFOCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型FIRERECORD_FIREINFO_Model</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(FIRERECORD_FIREINFO_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgMENU = BaseDT.FIRERECORD_FIREINFO.Add(m);
                if (msgMENU.Success == false)
                    return new Message(msgMENU.Success, msgMENU.Msg, "");
                return new Message(msgMENU.Success, msgMENU.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgMENU = BaseDT.FIRERECORD_FIREINFO.Mdy(m);
                if (msgMENU.Success == false)
                    return new Message(msgMENU.Success, msgMENU.Msg, "");
                return new Message(msgMENU.Success, msgMENU.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {
                Message msgMENU = BaseDT.FIRERECORD_FIREINFO.Del(m);
                if (msgMENU.Success == false)
                    return new Message(msgMENU.Success, msgMENU.Msg, "");
                return new Message(msgMENU.Success, msgMENU.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作!", m.returnUrl);
        }

        #endregion

        #region 获取单条数据
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static FIRERECORD_FIREINFO_Model getModel(FIRERECORD_FIREINFO_SW sw)
        {
            DataTable dt = BaseDT.FIRERECORD_FIREINFO.getDT2(sw);
            FIRERECORD_FIREINFO_Model m = new FIRERECORD_FIREINFO_Model();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            if (dt != null && dt.Rows.Count > 0)
            {
                int i = 0;
                m.FRFIID = dt.Rows[i]["FRFIID"].ToString();
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FIRECODE = dt.Rows[i]["FIRECODE"].ToString();
                m.FIREADDRESSCOUNTY = dt.Rows[i]["FIREADDRESSCOUNTY"].ToString();
                m.FIREADDRESSTOWNS = dt.Rows[i]["FIREADDRESSTOWNS"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.FIREADDRESSTOWNS);
                m.FIREADDRESSVILLAGES = dt.Rows[i]["FIREADDRESSVILLAGES"].ToString();
                m.FIRETIME = ClsSwitch.SwitTM(dt.Rows[i]["FIRETIME"].ToString());
                m.FIREENDTIME = ClsSwitch.SwitTM(dt.Rows[i]["FIREENDTIME"].ToString());
                m.FIREADDRESS = dt.Rows[i]["FIREADDRESS"].ToString();
                m.FIRERECINFO000 = dt.Rows[i]["FIRERECINFO000"].ToString();
                m.FIRERECINFO001 = dt.Rows[i]["FIRERECINFO001"].ToString();
                m.FIRERECINFO020 = dt.Rows[i]["FIRERECINFO020"].ToString();
                m.FIRERECINFO021 = dt.Rows[i]["FIRERECINFO021"].ToString();
                m.FIRERECINFO030 = dt.Rows[i]["FIRERECINFO030"].ToString();
                m.FIRERECINFO031 = dt.Rows[i]["FIRERECINFO031"].ToString();
                m.FIRERECINFO032 = dt.Rows[i]["FIRERECINFO032"].ToString();
                m.FIRERECINFO040 = dt.Rows[i]["FIRERECINFO040"].ToString();
                m.FIRERECINFO041 = dt.Rows[i]["FIRERECINFO041"].ToString();
                m.FIRERECINFO050 = dt.Rows[i]["FIRERECINFO050"].ToString();
                m.FIRERECINFO051 = dt.Rows[i]["FIRERECINFO051"].ToString();
                m.FIRERECINFO060 = dt.Rows[i]["FIRERECINFO060"].ToString();
                m.FIRERECINFO061 = dt.Rows[i]["FIRERECINFO061"].ToString();
                m.FIRERECINFO070 = dt.Rows[i]["FIRERECINFO070"].ToString();
                m.FIRERECINFO071 = dt.Rows[i]["FIRERECINFO071"].ToString();
                m.FIRERECINFO072 = dt.Rows[i]["FIRERECINFO072"].ToString();
                m.FIRERECINFO080 = dt.Rows[i]["FIRERECINFO080"].ToString();
                m.FIRERECINFO081 = dt.Rows[i]["FIRERECINFO081"].ToString();
                m.FIRERECINFO082 = dt.Rows[i]["FIRERECINFO082"].ToString();
                m.FIRERECINFO090 = dt.Rows[i]["FIRERECINFO090"].ToString();
                m.FIRERECINFO100 = dt.Rows[i]["FIRERECINFO100"].ToString();
                m.FIRERECINFO110 = dt.Rows[i]["FIRERECINFO110"].ToString();
                m.FIRERECINFO111 = dt.Rows[i]["FIRERECINFO111"].ToString();
                m.FIRERECINFO120 = dt.Rows[i]["FIRERECINFO120"].ToString();
                m.FIRERECINFO130 = dt.Rows[i]["FIRERECINFO130"].ToString();
                m.FIRERECINFO140 = dt.Rows[i]["FIRERECINFO140"].ToString();
                m.FIRERECINFO150 = dt.Rows[i]["FIRERECINFO150"].ToString();
                m.FIRERECINFO160 = dt.Rows[i]["FIRERECINFO160"].ToString();
                m.FIRELOSEAREA = dt.Rows[i]["FIRELOSEAREA"].ToString();
                //m.JD = dt.Rows[i]["JD"].ToString();
                //m.WD = dt.Rows[i]["WD"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型FIRERECORD_FIREINFO_Model</param>
        /// <returns>参见模型FIRERECORD_FIREINFO_Model</returns>
        public static IEnumerable<FIRERECORD_FIREINFO_Model> getListModel(FIRERECORD_FIREINFO_SW sw)
        {
            var result = new List<FIRERECORD_FIREINFO_Model>();
            DataTable dt = BaseDT.FIRERECORD_FIREINFO.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRERECORD_FIREINFO_Model m = new FIRERECORD_FIREINFO_Model();
                m.FRFIID = dt.Rows[i]["FRFIID"].ToString();
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FIRECODE = dt.Rows[i]["FIRECODE"].ToString();
                m.FIREADDRESSCOUNTY = dt.Rows[i]["FIREADDRESSCOUNTY"].ToString();
                m.FIREADDRESSTOWNS = dt.Rows[i]["FIREADDRESSTOWNS"].ToString();
                m.FIREADDRESSVILLAGES = dt.Rows[i]["FIREADDRESSVILLAGES"].ToString();
                m.FIRETIME = ClsSwitch.SwitTM(dt.Rows[i]["FIRETIME"].ToString());
                m.FIREENDTIME = ClsSwitch.SwitTM(dt.Rows[i]["FIREENDTIME"].ToString());
                m.FIREADDRESS = dt.Rows[i]["FIREADDRESS"].ToString();
                m.FIRERECINFO000 = dt.Rows[i]["FIRERECINFO000"].ToString();
                m.FIRERECINFO001 = dt.Rows[i]["FIRERECINFO001"].ToString();
                m.FIRERECINFO020 = dt.Rows[i]["FIRERECINFO020"].ToString();
                m.FIRERECINFO021 = dt.Rows[i]["FIRERECINFO021"].ToString();
                m.FIRERECINFO030 = dt.Rows[i]["FIRERECINFO030"].ToString();
                m.FIRERECINFO031 = dt.Rows[i]["FIRERECINFO031"].ToString();
                m.FIRERECINFO032 = dt.Rows[i]["FIRERECINFO032"].ToString();
                m.FIRERECINFO040 = dt.Rows[i]["FIRERECINFO040"].ToString();
                m.FIRERECINFO041 = dt.Rows[i]["FIRERECINFO041"].ToString();
                m.FIRERECINFO050 = dt.Rows[i]["FIRERECINFO050"].ToString();
                m.FIRERECINFO051 = dt.Rows[i]["FIRERECINFO051"].ToString();
                m.FIRERECINFO060 = dt.Rows[i]["FIRERECINFO060"].ToString();
                m.FIRERECINFO061 = dt.Rows[i]["FIRERECINFO061"].ToString();
                m.FIRERECINFO070 = dt.Rows[i]["FIRERECINFO070"].ToString();
                m.FIRERECINFO071 = dt.Rows[i]["FIRERECINFO071"].ToString();
                m.FIRERECINFO072 = dt.Rows[i]["FIRERECINFO072"].ToString();
                m.FIRERECINFO080 = dt.Rows[i]["FIRERECINFO080"].ToString();
                m.FIRERECINFO081 = dt.Rows[i]["FIRERECINFO081"].ToString();
                m.FIRERECINFO082 = dt.Rows[i]["FIRERECINFO082"].ToString();
                m.FIRERECINFO090 = dt.Rows[i]["FIRERECINFO090"].ToString();
                m.FIRERECINFO100 = dt.Rows[i]["FIRERECINFO100"].ToString();
                m.FIRERECINFO110 = dt.Rows[i]["FIRERECINFO110"].ToString();
                m.FIRERECINFO111 = dt.Rows[i]["FIRERECINFO111"].ToString();
                m.FIRERECINFO120 = dt.Rows[i]["FIRERECINFO120"].ToString();
                m.FIRERECINFO130 = dt.Rows[i]["FIRERECINFO130"].ToString();
                m.FIRERECINFO140 = dt.Rows[i]["FIRERECINFO140"].ToString();
                m.FIRERECINFO150 = dt.Rows[i]["FIRERECINFO150"].ToString();
                m.FIRERECINFO160 = dt.Rows[i]["FIRERECINFO160"].ToString();
                m.FIRELOSEAREA = dt.Rows[i]["FIRELOSEAREA"].ToString();
                //m.JD = dt.Rows[i]["JD"].ToString();
                //m.WD = dt.Rows[i]["WD"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<FIRERECORD_FIREINFO_Model> getListModel(FIRERECORD_FIREINFO_SW sw, out int total)
        {
            var result = new List<FIRERECORD_FIREINFO_Model>();
            DataTable dt = BaseDT.FIRERECORD_FIREINFO.getDT(sw, out total);
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRERECORD_FIREINFO_Model m = new FIRERECORD_FIREINFO_Model();
                m.FRFIID = dt.Rows[i]["FRFIID"].ToString();
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FIREADDRESSCOUNTY = dt.Rows[i]["FIREADDRESSCOUNTY"].ToString();
                m.FIREADDRESSTOWNS = dt.Rows[i]["FIREADDRESSTOWNS"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.FIREADDRESSTOWNS);
                m.FIREADDRESSVILLAGES = dt.Rows[i]["FIREADDRESSVILLAGES"].ToString();
                m.FIRETIME = ClsSwitch.SwitTM(dt.Rows[i]["FIRETIME"].ToString());
                m.FIREENDTIME = ClsSwitch.SwitTM(dt.Rows[i]["FIREENDTIME"].ToString());
                m.FIRERECINFO000 = dt.Rows[i]["FIRERECINFO000"].ToString();
                m.FIRERECINFO001 = dt.Rows[i]["FIRERECINFO001"].ToString();
                m.FIRERECINFO020 = dt.Rows[i]["FIRERECINFO020"].ToString();
                m.FIRERECINFO021 = dt.Rows[i]["FIRERECINFO021"].ToString();
                m.FIRERECINFO030 = dt.Rows[i]["FIRERECINFO030"].ToString();
                m.FIRERECINFO031 = dt.Rows[i]["FIRERECINFO031"].ToString();
                m.FIRERECINFO032 = dt.Rows[i]["FIRERECINFO032"].ToString();
                m.FIRERECINFO040 = dt.Rows[i]["FIRERECINFO040"].ToString();
                m.FIRERECINFO041 = dt.Rows[i]["FIRERECINFO041"].ToString();
                m.FIRERECINFO050 = dt.Rows[i]["FIRERECINFO050"].ToString();
                m.FIRERECINFO051 = dt.Rows[i]["FIRERECINFO051"].ToString();
                m.FIRERECINFO060 = dt.Rows[i]["FIRERECINFO060"].ToString();
                m.FIRERECINFO061 = dt.Rows[i]["FIRERECINFO061"].ToString();
                m.FIRERECINFO070 = dt.Rows[i]["FIRERECINFO070"].ToString();
                m.FIRERECINFO071 = dt.Rows[i]["FIRERECINFO071"].ToString();
                m.FIRERECINFO072 = dt.Rows[i]["FIRERECINFO072"].ToString();
                m.FIRERECINFO080 = dt.Rows[i]["FIRERECINFO080"].ToString();
                m.FIRERECINFO081 = dt.Rows[i]["FIRERECINFO081"].ToString();
                m.FIRERECINFO082 = dt.Rows[i]["FIRERECINFO082"].ToString();
                m.FIRERECINFO090 = dt.Rows[i]["FIRERECINFO090"].ToString();
                m.FIRERECINFO100 = dt.Rows[i]["FIRERECINFO100"].ToString();
                m.FIRERECINFO110 = dt.Rows[i]["FIRERECINFO110"].ToString();
                m.FIRERECINFO120 = dt.Rows[i]["FIRERECINFO120"].ToString();
                m.FIRERECINFO130 = dt.Rows[i]["FIRERECINFO130"].ToString();
                m.FIRERECINFO140 = dt.Rows[i]["FIRERECINFO140"].ToString();
                m.FIRERECINFO150 = dt.Rows[i]["FIRERECINFO150"].ToString();
                m.FIRERECINFO160 = dt.Rows[i]["FIRERECINFO160"].ToString();
                if (BaseDT.FIRELOST_FIREINFO.isAssess(new FIRELOST_FIREINFO_SW { JCFID = m.JCFID }))
                {
                    FIRELOST_FIREINFO_Model m2 = FIRELOST_FIREINFOCls.getModel(new FIRELOST_FIREINFO_SW { JCFID = m.JCFID });
                    if (m2 != null)
                    {
                        m.LOSSCOUNT = !string.IsNullOrEmpty(m2.LOSSCOUNT) ? float.Parse(m2.LOSSCOUNT) : 0;
                        m.FORESTRESOURCELOSSRATIO = !string.IsNullOrEmpty(m2.FORESTRESOURCELOSSRATIO) ? float.Parse(m2.FORESTRESOURCELOSSRATIO) : 0;
                        m.AVGLOSSPERCATITAVALUE = !string.IsNullOrEmpty(m2.AVGLOSSPERCATITAVALUE) ? float.Parse(m2.AVGLOSSPERCATITAVALUE) : 0;
                        m.WOODLANDLOSSAVGVALUE = !string.IsNullOrEmpty(m2.WOODLANDLOSSAVGVALUE) ? float.Parse(m2.WOODLANDLOSSAVGVALUE) : 0;
                        m.FIRESUPPEFFECTTHAN = !string.IsNullOrEmpty(m2.FIRESUPPEFFECTTHAN) ? float.Parse(m2.FIRESUPPEFFECTTHAN) : 0;
                    }
                }
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return result;
        }
        #endregion

        #region  根据JCFID获取FRFIID的值
        /// <summary>
        /// 根据JCFID获取FRFIID的值
        /// </summary>
        /// <param name="sw">FIRERECORD_FIREINFO_SW</param>
        /// <returns></returns>
        public static string getFrfiid(FIRERECORD_FIREINFO_SW sw)
        {
            DataTable dt = BaseDT.FIRERECORD_FIREINFO.getFRFIID(sw);
            FIRERECORD_FIREINFO_Model m = new FIRERECORD_FIREINFO_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.FRFIID = dt.Rows[i]["FRFIID"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m.FRFIID;
        }
        #endregion
    }
}
