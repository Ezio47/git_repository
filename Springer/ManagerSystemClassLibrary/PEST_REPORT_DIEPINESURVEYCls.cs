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
    /// 有害生物_报表_枯死松树调查表
    /// </summary>
    public class PEST_REPORT_DIEPINESURVEYCls
    {
        #region 增删改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型T_ALL_AREA_Model</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(PEST_REPORT_DIEPINESURVEY_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.PEST_REPORT_DIEPINESURVEY.Add(m);
                if (msg.Success == false)
                    return new Message(msg.Success, msg.Msg, "");
                msg.Msg = msg.Msg + "," + BaseDT.PEST_REPORT_DIEPINESURVEY.GetMaxID();
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.PEST_REPORT_DIEPINESURVEY.Mdy(m);
                if (msg.Success == false)
                    return new Message(msg.Success, msg.Msg, "");
                msg.Msg = msg.Msg + "," + BaseDT.PEST_REPORT_DIEPINESURVEY.GetMaxID();
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                return BaseDT.PEST_REPORT_DIEPINESURVEY.Del(m);
            }
            return new Message(false, "无效操作", m.returnUrl);
        }
        #endregion

        #region 获取单条数据
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns></returns>
        public static PEST_REPORT_DIEPINESURVEY_Model getModel(PEST_REPORT_DIEPINESURVEY_SW sw)
        {
            DataTable dt = BaseDT.PEST_REPORT_DIEPINESURVEY.getDT(sw);
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            PEST_REPORT_DIEPINESURVEY_Model m = new PEST_REPORT_DIEPINESURVEY_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.PEST_REPORT_DIEPINESURVEYID = dt.Rows[i]["PEST_REPORT_DIEPINESURVEYID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.BYORGNONAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.FINDER = dt.Rows[i]["FINDER"].ToString();
                m.FINDDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["FINDDATE"].ToString());
                m.LINKTELL = dt.Rows[i]["LINKTELL"].ToString();
                m.DIEPINECOUNT = dt.Rows[i]["DIEPINECOUNT"].ToString();
                m.REPORTDATE =PublicClassLibrary.ClsSwitch.SwitDate( dt.Rows[i]["REPORTDATE"].ToString());
                m.SAMPLINGCOUNT = dt.Rows[i]["SAMPLINGCOUNT"].ToString();
                m.AUTHENTICATERESULT = dt.Rows[i]["AUTHENTICATERESULT"].ToString();
                //扩充字段
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表-分页
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        public static List<PEST_REPORT_DIEPINESURVEY_Model> getListModel(PEST_REPORT_DIEPINESURVEY_SW sw, out int total)
        {
            var result = new List<PEST_REPORT_DIEPINESURVEY_Model>();
            DataTable dt = BaseDT.PEST_REPORT_DIEPINESURVEY.getDT(sw, out total);
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_REPORT_DIEPINESURVEY_Model m = new PEST_REPORT_DIEPINESURVEY_Model();
                m.PEST_REPORT_DIEPINESURVEYID = dt.Rows[i]["PEST_REPORT_DIEPINESURVEYID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.BYORGNONAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.FINDER = dt.Rows[i]["FINDER"].ToString();
                m.FINDDATE =PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["FINDDATE"].ToString());
                m.LINKTELL = dt.Rows[i]["LINKTELL"].ToString();
                m.DIEPINECOUNT = dt.Rows[i]["DIEPINECOUNT"].ToString();
                m.REPORTDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["REPORTDATE"].ToString());
                m.SAMPLINGCOUNT = dt.Rows[i]["SAMPLINGCOUNT"].ToString();
                m.AUTHENTICATERESULT = dt.Rows[i]["AUTHENTICATERESULT"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return result;
        }       
        #endregion
    }
}
