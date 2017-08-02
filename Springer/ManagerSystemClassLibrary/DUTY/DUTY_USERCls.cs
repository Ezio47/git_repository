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
    /// 值班管理
    /// </summary>
    public class DUTY_USERCls
    {
        #region 获取单条记录 OD_USER_Model getModel(OD_USER_SW sw)
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <returns></returns>
        public static DUTY_USER_Model getModel(DUTY_USER_SW sw)
        {
            DUTY_USER_Model m = new DUTY_USER_Model();
            DataTable dt = BaseDT.Duty.DUTY_USER.getDT(sw);
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { curOrgNo = SystemCls.getCurUserOrgNo() });
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DUID = dt.Rows[i]["DUID"].ToString();
                m.DUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["DUTYDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.DUTYUSERTYPE = dt.Rows[i]["DUTYUSERTYPE"].ToString();
                m.DUTYUSERID = dt.Rows[i]["DUTYUSERID"].ToString();
                m.ISATTENDED = dt.Rows[i]["ISATTENDED"].ToString();
                m.ATTENDEDTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["ATTENDEDTIME"].ToString());
                m.USERNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.DUTYUSERID);
            }
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            return m;
        }

        #endregion

        #region 获取列表 IEnumerable<DUTY_USER_Model> getListModel(DUTY_USER_SW sw)
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DUTY_USER_Model> getListModel(DUTY_USER_SW sw)
        {
            var result = new List<DUTY_USER_Model>();
            DataTable dt = BaseDT.Duty.DUTY_USER.getDT(sw);
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { curOrgNo = sw.BYORGNO });
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DUTY_USER_Model m = new DUTY_USER_Model();
                m.DUID = dt.Rows[i]["DUID"].ToString();
                m.DUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["DUTYDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.DUTYUSERTYPE = dt.Rows[i]["DUTYUSERTYPE"].ToString();
                m.DUTYUSERID = dt.Rows[i]["DUTYUSERID"].ToString();
                m.ISATTENDED = dt.Rows[i]["ISATTENDED"].ToString();
                m.ATTENDEDTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["ATTENDEDTIME"].ToString());
                m.USERNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.DUTYUSERID);
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

        #region 获取查询结果JsonString
        /// <summary>
        /// 获取查询结果json字符串
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string getJsonStr(DUTY_USER_Model m)
        {
            DateTime dtBegin = Convert.ToDateTime(m.dateBegin);
            DateTime dtEnd = Convert.ToDateTime(m.dateEnd);
            char[] specialChars = new char[] { ',' };
            string strname = "";
            string strid = "";
            string strTitle = ""; //标题 
            string strZID = ""; //DUTYCLASSID
            string strBanCi = "";//用作前台判断班次
            string JSONstring = "[";
            var list = DUTY_CLASSCls.GetListModel(new DUTY_CLASS_SW { BYORGNO = SystemCls.getCurUserOrgNo() }).Select(p => p.DUTYCLASSID).ToList();//查询当前组织机构有几个班次 
            var result = DUTY_USERCls.getListModel(new DUTY_USER_SW { BYORGNO = SystemCls.getCurUserOrgNo(), DTBegin=m.dateBegin,DTEnd=m.dateEnd });          
            if (result.Any())
            {
                var datelist = result.Select(p => p.DUTYDATE).ToList();
                var date = datelist.Distinct();//当前日期集合
                for (DateTime dt = dtBegin; dt < dtEnd; dt = dt.AddDays(1))
                {
                    foreach (var item in date)
                    {
                        if (list.Count > 0 && item == dt.ToString("yyyy-MM-dd"))
                        {
                            var length = list.Count + 2;
                            for (int k = 0; k < length; k++)
                            {
                                if (k == length - 2)
                                {
                                    m.DUTYUSERTYPE = "-1";
                                    strname = "带班领导：";
                                }
                                else if (k == length - 1)
                                {
                                    m.DUTYUSERTYPE = "-2";
                                    strname = "总带班领导：";
                                }
                                else if (list[k] == "1")
                                {
                                    m.DUTYUSERTYPE = "1";
                                    strname = "早班：";
                                    strBanCi += "1,";
                                }
                                else if (list[k] == "2")
                                {
                                    m.DUTYUSERTYPE = "2";
                                    strname = "中班：";
                                    strBanCi += "2,";
                                }
                                else if (list[k] == "3")
                                {
                                    m.DUTYUSERTYPE = "3";
                                    strname = "晚班：";
                                    strBanCi += "3,";
                                }

                                var str1 = result.Where(p => p.DUTYUSERTYPE == m.DUTYUSERTYPE && p.DUTYDATE == dt.ToString("yyyy-MM-dd")).Select(p => p.USERNAME).ToList();//姓名
                                var str2 = result.Where(p => p.DUTYUSERTYPE == m.DUTYUSERTYPE && p.DUTYDATE == dt.ToString("yyyy-MM-dd")).Select(p => p.DUTYUSERID).ToList();//DUTYUSERID
                                if (str1.Count > 0)
                                {
                                    if (str1.Count > 1)
                                    {
                                        for (int j = 0; j < str1.Count; j++)
                                        {
                                            if (j == str1.Count - 1)
                                            {
                                                strname += str1[j];
                                                strid += str2[j];
                                            }
                                            else
                                            {
                                                strname += str1[j] + ",";
                                                strid += str2[j] + ",";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        strname += str1[0];
                                        strid = str2[0];
                                    }

                                }
                                strTitle += strname + "\\r\\n";
                                strZID += strid + "|";
                                strname = "";
                                strid = "";
                            }
                            JSONstring += "{";
                            JSONstring += "\"title\":\"" + strTitle + "\",";
                            JSONstring += "\"id\":\"" + strZID + "\",";
                            JSONstring += "\"banci\":\"" + strBanCi + "\",";
                            JSONstring += "\"start\":\"" + dt.ToString("yyyy-MM-dd") + "\"";
                            JSONstring += "},";
                            strTitle = "";
                            strZID = "";
                            strBanCi = "";
                        }
                    }
                    
                }
            }
            
            JSONstring = JSONstring.TrimEnd(specialChars);
            JSONstring += "]";

            //var str = JSONstring.ToString().Replace("\"", "\\\"").Replace("\r\n", "<br/>").Replace("\n", "<br/>").Replace("\r", "<br/>");
            return JSONstring.ToString();
        }
        #endregion

        #region 获取下一班次值班员情况 IEnumerable<DUTY_USER_Model> getListModel(DUTY_USER_SW sw)
        /// <summary>
        /// 获取下一班次值班员情况
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DUTY_USER_Model> getNextClassListModel(DUTY_USER_SW sw)
        {
            int classID = int.Parse(sw.DUTYUSERTYPE);//当前班次
            switch (classID)
            {
                case 1:
                    sw.DUTYUSERTYPE = "2";
                    break;
                case 2:
                    sw.DUTYUSERTYPE = "3";
                    break;
                default:
                    sw.DUTYUSERTYPE = "1";
                    sw.DUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(Convert.ToDateTime(sw.DUTYDATE).AddDays(1));
                    break;
            }

            var result = new List<DUTY_USER_Model>();
            DataTable dt = BaseDT.Duty.DUTY_USER.getDT(sw);
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { curOrgNo = sw.BYORGNO });
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DUTY_USER_Model m = new DUTY_USER_Model();
                m.DUID = dt.Rows[i]["DUID"].ToString();
                m.DUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["DUTYDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.DUTYUSERTYPE = dt.Rows[i]["DUTYUSERTYPE"].ToString();
                m.DUTYUSERID = dt.Rows[i]["DUTYUSERID"].ToString();
                m.ISATTENDED = dt.Rows[i]["ISATTENDED"].ToString();
                m.ATTENDEDTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["ATTENDEDTIME"].ToString());
                m.USERNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.DUTYUSERID);
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

        #region 管理 Message Manager(DUTY_USER_Model m)
        /// <summary>
        /// 管理
        /// </summary>
        /// <returns></returns>
        public static Message Manager(DUTY_USER_Model m)
        {
            if (m.opMethod == "Add")
                return BaseDT.Duty.DUTY_USER.Add(m);
            if (m.opMethod == "PLAdd")
                return BaseDT.Duty.DUTY_USER.PLAdd(m);
            if (m.opMethod == "Del")
                return BaseDT.Duty.DUTY_USER.Del(m);
            if (m.opMethod == "Sign")
                return BaseDT.Duty.DUTY_USER.Sign(m);
            return new Message(false, "无效操作", "");
        }
        #endregion

        #region 周末值班统计 
        /// <summary>
        /// 周末值班统计
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DUTY_USER_Model> getWeekCountListModel(DUTY_USER_SW sw)
        {
            var result = new List<DUTY_USER_Model>();
            DataTable dt = BaseDT.Duty.DUTY_USER.getWeekDT(sw);

            DataTable dtU = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });//获取所有系统用户
            DataTable dtORG =null;
            if (sw.curOrgNo.Substring(4, 11) == "00000000000" || sw.curOrgNo.Substring(6, 9) == "000000000")//市县
            {
                 dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = sw.curOrgNo, OnlyGetShiXian = "1" });//获取单位
            }
            else//乡镇
            {
                dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = sw.curOrgNo });//获取单位
            }
            
            for (int i = 0; i < dtORG.Rows.Count; i++)
            {
                string orgNo = dtORG.Rows[i]["ORGNO"].ToString();//编码
                string orgName = dtORG.Rows[i]["ORGNAME"].ToString();//名称
                DataRow[] dr = dtU.Select("ORGNO='" + orgNo + "'");
                for (int k = 0; k < dr.Length; k++)
                {
                    string uid = dr[k]["USERID"].ToString();
                    string uName = BaseDT.T_SYSSEC_USER.getNameByUserList(dtU, uid);
                    string count = BaseDT.Duty.DUTY_USER.getCountByDT(dt, uid);
                    DUTY_USER_Model m = new DUTY_USER_Model();
                    m.ORGNAME = orgName;
                    m.USERNAME = uName;
                    m.weekCount = count;
                    if (count != "0")
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
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DUTY_USER_DUCKCOUNT_Model> GetDutyCount(DUTY_USER_SW sw)
        {
            var result = new List<DUTY_USER_DUCKCOUNT_Model>();
            var dt = BaseDT.Duty.DUTY_USER.GetDutyCoutDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DUTY_USER_DUCKCOUNT_Model oud = new DUTY_USER_DUCKCOUNT_Model();
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

        #region 获取值班人的userID
        /// <summary>
        /// 获取值班人的userID
        /// </summary>
        /// <param name="ondutyTime">日期</param>
        /// <param name="byorgon">组织机构编码（多个以，号分开）</param>
        /// <returns></returns>
        public static IEnumerable<string> GetOndutyUserid(string ondutyTime, string byorgon)
        {
            var result = new List<string>();
            var dt = BaseDT.Duty.DUTY_USER.GetOndutyUserid(ondutyTime, byorgon);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // OD_USER_Model m = new OD_USER_Model();
                //// O_ODUSER_Model m = new O_ODUSER_Model();
                // m.ONDUTYUSERID = dt.Rows[i]["ONDUTYUSERID"].ToString();
                result.Add(dt.Rows[i]["DUTYUSERID"].ToString());
            }
            dt.Clear();
            dt.Dispose();
            return result;
        } 
        #endregion
    }
}
