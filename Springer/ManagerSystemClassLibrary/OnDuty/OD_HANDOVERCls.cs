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
    /// 交接班
    /// </summary>
    public class OD_HANDOVERCls
    {
        #region 对交接班进行管理 Message Manager(OD_ODTYPE_Model od)
        /// <summary>
        /// 根据标识op_Method执行修改操作
        /// </summary>
        /// <param name="m">对象</param>
        /// <returns></returns>
        public static Message Manager(OD_HANDOVER_Model m)
        {
            if (m.opMethod == "Mdy")
            {
                return BaseDT.OD_HANDOVER.Mdy(m);
            }
            if (m.opMethod == "Add")
            { 
                return BaseDT.OD_HANDOVER.Add(m);
            }
            if (m.opMethod == "Del")
            {
                return BaseDT.OD_HANDOVER.Del(m);
            }
            return new Message(false, "无效操作", "");
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static bool isExists(OD_HANDOVER_SW sw)
        {
            return BaseDT.OD_HANDOVER.isExists(sw);
        }
        #endregion

        #region 获取一条记录 OD_HANDOVER_Model getModel(OD_HANDOVER_SW sw)
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static OD_HANDOVER_Model getModel(OD_HANDOVER_SW sw)
        {
           
            DataTable dt = BaseDT.OD_HANDOVER.getDT(sw);
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { curOrgNo = sw.BYORGNO });
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { });
            OD_HANDOVER_Model m = new OD_HANDOVER_Model();
            if(dt.Rows.Count>0)
            {
                int i = 0;
                m.ODHID = dt.Rows[i]["ODHID"].ToString();
                m.ONDUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["ONDUTYDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ONDUTYUSERTYPE = dt.Rows[i]["ONDUTYUSERTYPE"].ToString();
                m.ONDUTYTYPE = dt.Rows[i]["ONDUTYTYPE"].ToString();
                m.ONDUTYUSERID = dt.Rows[i]["ONDUTYUSERID"].ToString();
                m.OPTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["OPTIME"].ToString());
                m.OPCONTENT = dt.Rows[i]["OPCONTENT"].ToString();
                m.ONDUTYUSERName = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.ONDUTYUSERID);
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

        #region 获取列表 IEnumerable<OD_HANDOVER_Model> getListModel(OD_HANDOVER_SW sw)
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<OD_HANDOVER_Model> getListModel(OD_HANDOVER_SW sw)
        {
            var result = new List<OD_HANDOVER_Model>();
            DataTable dt = BaseDT.OD_HANDOVER.getDT(sw);
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { curOrgNo = sw.BYORGNO });
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OD_HANDOVER_Model m = new OD_HANDOVER_Model();
                m.ODHID = dt.Rows[i]["ODHID"].ToString();
                m.ONDUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["ONDUTYDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ONDUTYUSERTYPE = dt.Rows[i]["ONDUTYUSERTYPE"].ToString();
                m.ONDUTYTYPE = dt.Rows[i]["ONDUTYTYPE"].ToString();
                m.ONDUTYUSERID = dt.Rows[i]["ONDUTYUSERID"].ToString();
                m.OPTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["OPTIME"].ToString());
                m.OPCONTENT = dt.Rows[i]["OPCONTENT"].ToString();
                m.ONDUTYUSERName = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.ONDUTYUSERID);
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
