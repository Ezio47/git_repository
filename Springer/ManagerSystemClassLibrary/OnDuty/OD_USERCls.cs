using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using DataBaseClassLibrary;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 获取值班人员信息
    /// </summary>
   public class OD_USERCls
   {
       #region 获取单条记录 OD_USER_Model getModel(OD_USER_SW sw)
       /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <returns></returns>
        public static OD_USER_Model getModel(OD_USER_SW sw)
        {
            OD_USER_Model m = new OD_USER_Model();
            DataTable dt = BaseDT.OD_USER.getDT(sw);
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { curOrgNo=SystemCls.getCurUserOrgNo() });
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.ODUID = dt.Rows[i]["ODUID"].ToString();
                m.ONDUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["ONDUTYDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ONDUTYUSERTYPE = dt.Rows[i]["ONDUTYUSERTYPE"].ToString();
                m.ONDUTYUSERID = dt.Rows[i]["ONDUTYUSERID"].ToString();
                m.ISATTENDED = dt.Rows[i]["ISATTENDED"].ToString();
                m.ATTENDEDTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["ATTENDEDTIME"].ToString());
                m.USERNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.ONDUTYUSERID);

            }
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            return m;
        }

       #endregion

        #region 获取列表 IEnumerable<OD_USER_Model> getListModel(OD_USER_SW sw)
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<OD_USER_Model> getListModel(OD_USER_SW sw)
        {
            var result = new List<OD_USER_Model>();
            DataTable dt = BaseDT.OD_USER.getDT(sw);
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { curOrgNo =sw.BYORGNO });
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OD_USER_Model m = new OD_USER_Model();
                m.ODUID = dt.Rows[i]["ODUID"].ToString();
                m.ONDUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["ONDUTYDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ONDUTYUSERTYPE = dt.Rows[i]["ONDUTYUSERTYPE"].ToString();
                m.ONDUTYUSERID = dt.Rows[i]["ONDUTYUSERID"].ToString();
                m.ISATTENDED = dt.Rows[i]["ISATTENDED"].ToString();
                m.ATTENDEDTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["ATTENDEDTIME"].ToString());
                m.USERNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.ONDUTYUSERID);
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

        #region 获取下一班次值班员情况 IEnumerable<OD_USER_Model> getListModel(OD_USER_SW sw)
        /// <summary>
        /// 获取下一班次值班员情况
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<OD_USER_Model> getNextClassListModel(OD_USER_SW sw)
        {
            int classID = int.Parse(sw.ONDUTYUSERTYPE);//当前班次
            switch (classID)
            {
                case 1:
                    sw.ONDUTYUSERTYPE = "2";
                    break;
                case 2:
                    sw.ONDUTYUSERTYPE = "3";
                    break;
                default:
                    sw.ONDUTYUSERTYPE = "1";
                    sw.ONDUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(Convert.ToDateTime(sw.ONDUTYDATE).AddDays(1));
                    break;
            }

            var result = new List<OD_USER_Model>();
            DataTable dt = BaseDT.OD_USER.getDT(sw);
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { curOrgNo = sw.BYORGNO });
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OD_USER_Model m = new OD_USER_Model();
                m.ODUID = dt.Rows[i]["ODUID"].ToString();
                m.ONDUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["ONDUTYDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ONDUTYUSERTYPE = dt.Rows[i]["ONDUTYUSERTYPE"].ToString();
                m.ONDUTYUSERID = dt.Rows[i]["ONDUTYUSERID"].ToString();
                m.ISATTENDED = dt.Rows[i]["ISATTENDED"].ToString();
                m.ATTENDEDTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["ATTENDEDTIME"].ToString());
                m.USERNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.ONDUTYUSERID);
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

        #region 管理 Message Manager(OD_USER_Model m)
       /// <summary>
        ///  管理
       /// </summary>
        /// <param name="m">m</param>
       /// <returns></returns>
        public static Message Manager(OD_USER_Model m)
        {
            if (m.opMethod == "Copy")
                return BaseDT.OD_USER.Copy(m);
            if (m.opMethod == "Add")
                return BaseDT.OD_USER.Add(m);
            if (m.opMethod == "Sign")
                return BaseDT.OD_USER.Sign(m);
            return new Message(false, "无效操作", "");
        }
        #endregion

        #region 周末值班统计 IEnumerable<OD_USER_Model> getWeekCountListModel(OD_USER_SW sw)
       /// <summary>
        /// 周末值班统计
       /// </summary>
       /// <param name="sw"></param>
       /// <returns></returns>
        public static IEnumerable<OD_USER_Model> getWeekCountListModel(OD_USER_SW sw)
        {
            var result = new List<OD_USER_Model>();
            DataTable dt = BaseDT.OD_USER.getWeekDT(sw);

            DataTable dtU = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW {  });//获取所有系统用户
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = sw.curOrgNo ,OnlyGetShiXian="1"});//获取单位
            for (int i = 0; i < dtORG.Rows.Count; i++)
            {
                string orgNo = dtORG.Rows[i]["ORGNO"].ToString();//编码
                string orgName = dtORG.Rows[i]["ORGNAME"].ToString();//名称
                DataRow[] dr = dtU.Select("ORGNO='" + orgNo + "'");
                for (int k = 0; k < dr.Length; k++)
                {
                    string uid = dr[k]["USERID"].ToString();
                    string uName = BaseDT.T_SYSSEC_USER.getNameByUserList(dtU, uid);
                    string count = BaseDT.OD_USER.getCountByDT(dt, uid);
                    OD_USER_Model m = new OD_USER_Model();
                    m.ORGNAME = orgName;
                    m.USERNAME = uName;
                    m.weekCount = count;
                    if(count!="0")
                        result.Add(m);
                }
            }
                return result;
        }
        
        #endregion

        #region 统计值班人次数
        /// <summary>
        /// 统计值班人次数
        /// </summary>
        /// <param name="sw">sw</param>
        /// <returns></returns>
        public static IEnumerable<OD_USER_DUCKCOUNT_Model> GetDutyCount(OD_USER_SW sw)
        {
            var result = new List<OD_USER_DUCKCOUNT_Model>();
            var dt = BaseDT.OD_USER.GetDutyCoutDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OD_USER_DUCKCOUNT_Model oud = new OD_USER_DUCKCOUNT_Model();
                oud.USERNAME = dt.Rows[i]["USERNAME"].ToString();
                oud.zaobCount = dt.Rows[i]["zaob"].ToString();
                oud.zhongbCount = dt.Rows[i]["zhongb"].ToString();
                oud.wanbCount = dt.Rows[i]["wanb"].ToString();
                oud.daiBCount = dt.Rows[i]["daib"].ToString();
                result.Add(oud);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        
        #endregion

        /// <summary>
        /// 获取值班人的userID
        /// </summary>
        /// <param name="ondutyTime">日期</param>
        /// <param name="byorgon">组织机构编码（多个以，号分开）</param>
        /// <returns></returns>
        public static IEnumerable<string> GetOndutyUserid(string ondutyTime, string byorgon)
        {
            var result = new List<string>();
            var dt = BaseDT.OD_USER.GetOndutyUserid(ondutyTime, byorgon);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               // OD_USER_Model m = new OD_USER_Model();
               //// O_ODUSER_Model m = new O_ODUSER_Model();
                
               // m.ONDUTYUSERID = dt.Rows[i]["ONDUTYUSERID"].ToString();
                result.Add(dt.Rows[i]["ONDUTYUSERID"].ToString());
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
    }
}
