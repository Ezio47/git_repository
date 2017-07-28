using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.DUTY
{
    /// <summary>
    /// 交接班
    /// </summary>
    public class DUTY_HANDOVERCls
    {
        #region 对交接班进行管理 Message Manager(OD_ODTYPE_Model od)
        /// <summary>
        /// 根据标识op_Method执行修改操作
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Manager(DUTY_HANDOVER_Model m)
        {
            if (m.opMethod == "Mdy")
            {
                return BaseDT.Duty.DUTY_HANDOVER.Mdy(m);
            }
            if (m.opMethod == "Add")
                return BaseDT.Duty.DUTY_HANDOVER.Add(m);
            if (m.opMethod == "Del")
                return BaseDT.Duty.DUTY_HANDOVER.Del(m);
            return new Message(false, "无效操作", "");
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static bool isExists(DUTY_HANDOVER_SW sw)
        {
            return BaseDT.Duty.DUTY_HANDOVER.isExists(sw);
        }
        #endregion

        #region 获取一条记录 DUTY_HANDOVER_Model getModel(DUTY_HANDOVER_SW sw)
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DUTY_HANDOVER_Model getModel(DUTY_HANDOVER_SW sw)
        {

            DataTable dt = BaseDT.Duty.DUTY_HANDOVER.getDT(sw);
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { curOrgNo = sw.BYORGNO });
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { });
            DUTY_HANDOVER_Model m = new DUTY_HANDOVER_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DHID = dt.Rows[i]["DHID"].ToString();
                m.DUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["DUTYDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.DUTYUSERTYPE = dt.Rows[i]["DUTYUSERTYPE"].ToString();
                m.DUTYTYPE = dt.Rows[i]["DUTYTYPE"].ToString();
                m.DUTYUSERID = dt.Rows[i]["DUTYUSERID"].ToString();
                m.OPTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["OPTIME"].ToString());
                m.OPCONTENT = dt.Rows[i]["OPCONTENT"].ToString();
                m.DUTYUSERName = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.DUTYUSERID);
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["BYORGNO"].ToString());

            }
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return m;
        }

        #endregion

        #region 获取列表 IEnumerable<DUTY_HANDOVER_Model> getListModel(DUTY_HANDOVER_SW sw)
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DUTY_HANDOVER_Model> getListModel(DUTY_HANDOVER_SW sw)
        {
            var result = new List<DUTY_HANDOVER_Model>();
            DataTable dt = BaseDT.Duty.DUTY_HANDOVER.getDT(sw);
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { curOrgNo = sw.BYORGNO });
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DUTY_HANDOVER_Model m = new DUTY_HANDOVER_Model();
                m.DHID = dt.Rows[i]["DHID"].ToString();
                m.DUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["DUTYDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.DUTYUSERTYPE = dt.Rows[i]["DUTYUSERTYPE"].ToString();
                m.DUTYTYPE = dt.Rows[i]["DUTYTYPE"].ToString();
                m.DUTYUSERID = dt.Rows[i]["DUTYUSERID"].ToString();
                m.OPTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["OPTIME"].ToString());
                m.OPCONTENT = dt.Rows[i]["OPCONTENT"].ToString();
                m.DUTYUSERName = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.DUTYUSERID);
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["BYORGNO"].ToString());
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return result;
        }

        #endregion
    }
}
