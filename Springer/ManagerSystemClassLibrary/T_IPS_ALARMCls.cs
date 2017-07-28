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
    /// 报警管理
    /// </summary>
    public class T_IPS_ALARMCls
    {
        #region 删除、处理

        /// <summary>
        /// 删除、处理
        /// </summary>
        /// <param name="m">参见模型T_IPS_ALARM_Model</param>
        /// <returns>参见模型Manager</returns>
        public static Message Manager(T_IPS_ALARM_Model m)
        {
            if (m.opMethod == "Del")
            {
                T_IPS_ALARM_Model m1 = getModel(new T_IPS_ALARM_SW { ALARMID = m.ALARMID });
                SystemCls.LogSave("5", "报警处理:" + m.PHONE, ClsStr.getModelContent(m1));
                Message msgUser = BaseDT.T_IPS_ALARM.Del(m);

                return new Message(msgUser.Success, msgUser.Msg, "");

            }
            if (m.opMethod == "Man")
            {
                T_IPS_ALARM_Model m1 = getModel(new T_IPS_ALARM_SW { ALARMID = m.ALARMID });
                SystemCls.LogSave("4", "报警处理:" + m.PHONE, ClsStr.getModelContent(m1));
                Message msgUser = BaseDT.T_IPS_ALARM.Man(m);

                return new Message(msgUser.Success, msgUser.Msg, "");
            }

            return new Message(false, "无效操作", "");


        }
        #endregion

        #region 获取一条数据
        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <example>
        /// sw.ALARMID       报警ID
        /// </example>
        /// <param name="sw">参见条件模型T_IPS_ALARM_SW</param>
        /// <returns>参见模型T_IPS_ALARM_Model</returns>
        public static T_IPS_ALARM_Model getModel(T_IPS_ALARM_SW sw)
        {
            DataTable dt = BaseDT.T_IPS_ALARM.getDT(sw);
            T_IPS_ALARM_Model m = new T_IPS_ALARM_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.ALARMID = dt.Rows[i]["ALARMID"].ToString();
                m.LONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();
                m.LATITUDE = dt.Rows[i]["LATITUDE"].ToString();
                //******************计算坐标偏移量
                string[] arr = PublicCls.switJWD(m.LATITUDE, m.LONGITUDE);
                m.LATITUDE = arr[0];
                m.LONGITUDE = arr[1];
                //******************计算坐标偏移量 End
                m.HEIGHT = dt.Rows[i]["HEIGHT"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.ALARMTIME = dt.Rows[i]["ALARMTIME"].ToString();
                if (string.IsNullOrEmpty(m.ALARMTIME) == false)
                    m.ALARMTIME = PublicClassLibrary.ClsSwitch.SwitTM(m.ALARMTIME);
                m.ALARMCONTENT = dt.Rows[i]["ALARMCONTENT"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = dt.Rows[i]["MANTIME"].ToString();
                if (string.IsNullOrEmpty(m.MANTIME) == false)
                    m.MANTIME = PublicClassLibrary.ClsSwitch.SwitTM(m.MANTIME);
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();

                DataTable dtHRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { PHONE = m.PHONE });
                DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { USERID = m.MANUSERID });
                DataRow[] drHRUser = dtHRUser.Select("PHONE='" + m.PHONE + "'");
                if (drHRUser.Length > 0)
                {
                    m.OrgNoName = drHRUser[0]["ORGNAME"].ToString();
                    m.HName = drHRUser[0]["HNAME"].ToString();
                    m.HID = drHRUser[0]["HID"].ToString();
                    m.OrgNo = drHRUser[0]["BYORGNO"].ToString();
                }

                if (!string.IsNullOrEmpty(m.MANUSERID))
                {
                    DataRow[] drUser = dtUser.Select("USERID='" + m.MANUSERID + "'");
                    if (drUser.Length > 0)
                    {
                        m.ManUserName = drUser[0]["USERNAME"].ToString();
                    }
                }
                dtUser.Clear();
                dtUser.Dispose();
                dtHRUser.Clear();
                dtHRUser.Dispose();

                ////权限获取
                //var userid = SystemCls.getUserID();
                //var rightsw = new T_SYSSEC_IPSUSER_SW();
                //rightsw.USERID = userid;
                //m.Rights = T_SYSSEC_RIGHTCls.getRightStrByUID(rightsw);
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion

        #region 获取多条数据
        /// <summary>
        /// 获取多条数据
        /// </summary>
        /// <example>
        /// sw.orgNo            机构编码，用于获取该机构编码下所有的报警信息
        /// sw.MANSTATE       处理状态 0未处理1已处理
        /// sw.DateBegin    开始日期
        /// sw.DateEnd      结束日期
        /// </example>
        /// <param name="sw">参见条件模型T_IPS_ALARM_SW</param>
        /// <returns>参见模型T_IPS_ALARM_Model</returns>
        public static IEnumerable<T_IPS_ALARM_Model> getModelList(T_IPS_ALARM_SW sw)
        {
            //var userid = SystemCls.getUserID();
            //var rightsw = new T_SYSSEC_IPSUSER_SW();
            //rightsw.USERID = userid;

            DataTable dtHRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { BYORGNO = sw.orgNo });
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            var result = new List<T_IPS_ALARM_Model>();
            DataTable dt = BaseDT.T_IPS_ALARM.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_IPS_ALARM_Model m = new T_IPS_ALARM_Model();
                m.ALARMID = dt.Rows[i]["ALARMID"].ToString();
                m.LONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();
                m.LATITUDE = dt.Rows[i]["LATITUDE"].ToString();
                //******************计算坐标偏移量
                string[] arr = PublicCls.switJWD(m.LATITUDE, m.LONGITUDE);
                m.LATITUDE = arr[0];
                m.LONGITUDE = arr[1];
                //******************计算坐标偏移量 End
                m.HEIGHT = dt.Rows[i]["HEIGHT"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.ALARMTIME = dt.Rows[i]["ALARMTIME"].ToString();
                if (string.IsNullOrEmpty(m.ALARMTIME) == false)
                    m.ALARMTIME = PublicClassLibrary.ClsSwitch.SwitTM(m.ALARMTIME);
                m.ALARMCONTENT = dt.Rows[i]["ALARMCONTENT"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = dt.Rows[i]["MANTIME"].ToString();
                if (string.IsNullOrEmpty(m.MANTIME) == false)
                    m.MANTIME = PublicClassLibrary.ClsSwitch.SwitTM(m.MANTIME);
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();

                DataRow[] drHRUser = dtHRUser.Select("PHONE='" + m.PHONE + "'");
                if (drHRUser.Length > 0)
                {
                    m.OrgNoName = drHRUser[0]["ORGNAME"].ToString();
                    m.HName = drHRUser[0]["HNAME"].ToString();
                    m.HID = drHRUser[0]["HID"].ToString();
                    m.OrgNo = drHRUser[0]["BYORGNO"].ToString();
                }
                if (!string.IsNullOrEmpty(m.MANUSERID))
                {
                    DataRow[] drUser = dtUser.Select("USERID='" + m.MANUSERID + "'");
                    if (drUser.Length > 0)
                    {
                        m.ManUserName = drUser[0]["USERNAME"].ToString();
                    }
                }
                //权限获取
                //m.Rights = T_SYSSEC_RIGHTCls.getRightStrByUID(rightsw);
                result.Add(m);
            }
            dtUser.Clear();
            dtUser.Dispose();
            dtHRUser.Clear();
            dtHRUser.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
