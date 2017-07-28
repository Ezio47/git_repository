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
    ///  监测点表
    /// </summary>
    public class PEST_MONITORINGSTATIONCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_MONITORINGSTATION_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.PEST_MONITORINGSTATION.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.PEST_MONITORINGSTATION.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);

            }
            if (m.opMethod == "MdyJWD")
            {
                Message msg = BaseDT.PEST_MONITORINGSTATION.MdyJWD(m);
                return new Message(msg.Success, msg.Msg, msg.Url);

            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.PEST_MONITORINGSTATION.Del(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            return new Message(false, "无效操作", "");
        }

        #endregion

        #region 获取单条数据
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_MONITORINGSTATION_Model getModel(PEST_MONITORINGSTATION_SW sw)
        {
            DataTable dt = BaseDT.PEST_MONITORINGSTATION.getDT(sw);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt119 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "119" });//无线电传输方式
            DataTable dt120 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "120" });//使用现状
            DataTable dt121 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "121" });//维护类型
            PEST_MONITORINGSTATION_Model m = new PEST_MONITORINGSTATION_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.PEST_MONITORINGSTATIONID = dt.Rows[i]["PEST_MONITORINGSTATIONID"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.MODEL = dt.Rows[i]["MODEL"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                if (PublicCls.OrgIsShi(m.BYORGNO) || PublicCls.OrgIsXian(m.BYORGNO))
                {
                    m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                    m.ORGXSName = "--";
                }
                else
                {
                    m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO.Substring(0, 6) + "000");
                    m.ORGXSName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                }
                m.TRANSFERMODETYPE = dt.Rows[i]["TRANSFERMODETYPE"].ToString();
                m.TRANSFERMODETYPEName = BaseDT.T_SYS_DICT.getName(dt119, m.TRANSFERMODETYPE);
                m.MONICONTENT = dt.Rows[i]["MONICONTENT"].ToString();
                m.BUILDDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATE"].ToString());
                m.USESTATE = dt.Rows[i]["USESTATE"].ToString();
                m.USESTATEName = BaseDT.T_SYS_DICT.getName(dt120, m.USESTATE);
                m.MANAGERSTATE = dt.Rows[i]["MANAGERSTATE"].ToString();
                m.MANAGERSTATEName = BaseDT.T_SYS_DICT.getName(dt121, m.MANAGERSTATE);
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt119.Clear();
            dt119.Dispose();
            dt120.Clear();
            dt120.Dispose();
            dt121.Clear();
            dt121.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param> 
        /// <returns></returns>
        public static IEnumerable<PEST_MONITORINGSTATION_Model> getListModel(PEST_MONITORINGSTATION_SW sw, out int total)
        {
            var result = new List<PEST_MONITORINGSTATION_Model>();
            DataTable dt = BaseDT.PEST_MONITORINGSTATION.getDT(sw, out total);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt119 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "119" });//无线电传输方式
            DataTable dt120 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "120" });//使用现状
            DataTable dt121 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "121" });//维护类型
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_MONITORINGSTATION_Model m = new PEST_MONITORINGSTATION_Model();
                m.PEST_MONITORINGSTATIONID = dt.Rows[i]["PEST_MONITORINGSTATIONID"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.MODEL = dt.Rows[i]["MODEL"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                if (PublicCls.OrgIsShi(m.BYORGNO) || PublicCls.OrgIsXian(m.BYORGNO))
                {
                    m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                    m.ORGXSName = "--";
                }
                else
                {
                    m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO.Substring(0, 6) + "000");
                    m.ORGXSName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                }
                m.TRANSFERMODETYPE = dt.Rows[i]["TRANSFERMODETYPE"].ToString();
                m.TRANSFERMODETYPEName = BaseDT.T_SYS_DICT.getName(dt119, m.TRANSFERMODETYPE);
                m.MONICONTENT = dt.Rows[i]["MONICONTENT"].ToString();
                m.BUILDDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATE"].ToString());
                m.USESTATE = dt.Rows[i]["USESTATE"].ToString();
                m.USESTATEName = BaseDT.T_SYS_DICT.getName(dt120, m.USESTATE);
                m.MANAGERSTATE = dt.Rows[i]["MANAGERSTATE"].ToString();
                m.MANAGERSTATEName = BaseDT.T_SYS_DICT.getName(dt121, m.MANAGERSTATE);
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt119.Clear();
            dt119.Dispose();
            dt120.Clear();
            dt120.Dispose();
            dt121.Clear();
            dt121.Dispose();
            return result;
        }
        #endregion
    }
}
