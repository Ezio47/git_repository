using ManagerSystemClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using ManagerSystemSearchWhereModel.LogicModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 坐标上传中间表公共类
    /// </summary>
    public class T_IPS_REALDATATEMPORARYCls
    {
        #region 护林员定位 获取护林员最新一条位置信息
        /// <summary>
        /// 护林员定位 获取护林员最新一条位置信息
        /// </summary>
        /// <param name="sw">传递护林员列表 参见条件模型T_IPS_REALDATATEMPORARYSW</param>
        /// <returns>参见模型T_IPS_REALDATATEMPORARYModel</returns>
        public static IEnumerable<T_IPS_REALDATATEMPORARYModel> getTopOneModelList(T_IPS_REALDATATEMPORARYSW sw)
        {
            var result = new List<T_IPS_REALDATATEMPORARYModel>();
            DataTable dt = BaseDT.T_IPS_REALDATATEMPORARY.getTopOneDT(sw);
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取当前登录用户有权限查看的组织机构
            DataTable dtFRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", HID = sw.USERID });//获取所有有权限查看的护林员
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                T_IPS_REALDATATEMPORARYModel m = new T_IPS_REALDATATEMPORARYModel();
                //m.REALDATAID = dt.Rows[i]["REALDATAID"].ToString();
                m.USERID = dt.Rows[i]["USERID"].ToString();
                m.LONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();
                m.LATITUDE = dt.Rows[i]["LATITUDE"].ToString();
                m.ORILONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();//原经度
                m.ORILATITUDE = dt.Rows[i]["LATITUDE"].ToString();//原纬度
                if (sw.MapType != "Skyline")
                {
                    //******************计算坐标偏移量
                    string[] arr = PublicCls.switJWD(m.LATITUDE, m.LONGITUDE);
                    m.LATITUDE = arr[0];
                    m.LONGITUDE = arr[1];
                    //******************计算坐标偏移量 End
                }
                m.HEIGHT = dt.Rows[i]["HEIGHT"].ToString();
                m.ELECTRIC = dt.Rows[i]["ELECTRIC"].ToString();
                m.SPEED = dt.Rows[i]["SPEED"].ToString();
                m.DIRECTION = dt.Rows[i]["DIRECTION"].ToString();
                m.SBTIME = dt.Rows[i]["SBTIME"].ToString();
                if (string.IsNullOrEmpty(m.SBTIME) == false)
                    m.SBTIME = PublicClassLibrary.ClsSwitch.SwitTM(m.SBTIME);
                m.NOTE = dt.Rows[i]["NOTE"].ToString();
                m.ORGNO = dt.Rows[i]["ORGNO"].ToString();
                m.SBDATE = dt.Rows[i]["SBDATE"].ToString();
                m.ISOUTRAIL = dt.Rows[i]["ISOUTRAIL"].ToString();//是否出围
                if (string.IsNullOrEmpty(m.SBDATE) == false)
                    m.SBDATE = PublicClassLibrary.ClsSwitch.SwitDate(m.SBDATE);
                m.SBTIMEBEGIN = dt.Rows[i]["SBTIMEBEGIN"].ToString();
                m.PATROLLENGTH = dt.Rows[i]["PATROLLENGTH"].ToString();
                DataRow[] drFRUser = dtFRUser.Select("HID=" + m.USERID);
                if (drFRUser.Length > 0)
                {
                    m.HNAME = drFRUser[0]["HNAME"].ToString();
                    m.PHONE = drFRUser[0]["PHONE"].ToString();
                    m.ORGNO = drFRUser[0]["BYORGNO"].ToString();
                    m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtOrg, m.ORGNO);
                }
                double intervalTime = ConfigCls.inLineTimeInterval();
                var absTime = Math.Abs(ClsStr.getMinutesDiff(DateTime.Now, m.SBTIME));
                m.HSTATE = absTime > intervalTime ? "0" : "1";//护林员在线状态 0 表示离线 1 表示在线

                //
                //m.ISOUTRAIL
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            dtFRUser.Clear();
            dtFRUser.Dispose();

            return result;
        }
        #endregion

        #region 根据护林员ID获取在线状态
        /// <summary>
        /// 根据护林员ID获取在线状态 ，用于点名｜
        /// </summary>
        /// <param name="sw">参见条件模型T_IPS_REALDATATEMPORARYSW</param>
        /// <example>
        /// sw.USERID       用户序号（多用户以逗号分隔）
        /// sw.SearchTime   判断时间 可为空，为空默认为当前时间
        /// </example>
        /// <param name="FRUserCount">总人数</param>
        /// <param name="FRUserOnLineCount">在线人数</param>
        /// <returns>
        /// m.HID       护林员序号
        /// m.HNAME     姓名
        /// m.PHONE     电话
        /// m.BYORGNO   机构编码
        /// m.ORGNAME   机构名称
        /// m.isOnLine  在线状态 1在线 0离线
        /// </returns>
        public static IEnumerable<T_IPSFR_USER_Model> getFROnLineByUID(T_IPS_REALDATATEMPORARYSW sw, out int FRUserCount, out int FRUserOnLineCount)
        {
            var result = new List<T_IPSFR_USER_Model>();

            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取当前登录用户有权限查看的组织机构
            DataTable dtOnState = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "固兼职状态" });

            DataTable dtFRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", HID = sw.USERID });//获取所有有权限查看的护林员
            //当前在线的护林员
            DataTable dtRealTmp = BaseDT.T_IPS_REALDATATEMPORARY.getOnLineDtByOrgno(new T_IPS_REALDATATEMPORARYSW { USERID = sw.USERID });
            FRUserCount = dtFRUser.Rows.Count;
            FRUserOnLineCount = dtRealTmp.Rows.Count;
            //获取在线人员列表
            for (int i = 0; i < dtRealTmp.Rows.Count; i++)
            {//HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO,ISENABLE,SBTIME
                T_IPSFR_USER_Model m = new T_IPSFR_USER_Model();
                m.HID = dtRealTmp.Rows[i]["HID"].ToString();
                m.HNAME = dtRealTmp.Rows[i]["HNAME"].ToString();
                m.PHONE = dtRealTmp.Rows[i]["PHONE"].ToString();
                m.BYORGNO = dtRealTmp.Rows[i]["BYORGNO"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtOrg, m.BYORGNO);
                m.ONSTATE = dtRealTmp.Rows[i]["ONSTATE"].ToString();
                m.ONSTATENAME = BaseDT.T_SYS_DICT.getName(dtOnState, m.ONSTATE);
                m.SBTIME = dtRealTmp.Rows[i]["SBTIME"].ToString();
                m.isOnLine = "1";
                result.Add(m);
            }

            //获取非在线人员列表
            for (int i = 0; i < dtFRUser.Rows.Count; i++)
            {
                //判断该用户是否是在线状态
                if (dtRealTmp.Select("HID=" + dtFRUser.Rows[i]["HID"].ToString()).Length == 0)
                {
                    T_IPSFR_USER_Model m = new T_IPSFR_USER_Model();
                    m.HID = dtFRUser.Rows[i]["HID"].ToString();
                    m.HNAME = dtFRUser.Rows[i]["HNAME"].ToString();
                    m.PHONE = dtFRUser.Rows[i]["PHONE"].ToString();
                    m.BYORGNO = dtFRUser.Rows[i]["BYORGNO"].ToString();
                    m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtOrg, m.BYORGNO);
                    m.ONSTATE = dtFRUser.Rows[i]["ONSTATE"].ToString();
                    m.ONSTATENAME = BaseDT.T_SYS_DICT.getName(dtOnState, m.ONSTATE);
                    m.SBTIME = "";
                    m.isOnLine = "0";
                    result.Add(m);
                }
            }
            dtOnState.Clear();
            dtOnState.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            dtFRUser.Clear();
            dtFRUser.Dispose();
            dtRealTmp.Clear();
            dtRealTmp.Dispose();
            return result;
        }
        #endregion

        #region 获取历史轨迹、最新坐标

        /// <summary>
        /// 获取临时表列表
        /// <example>
        /// 获取历史轨迹传递参数：
        /// 用户ID列表（sw.USERID逗号分隔）
        /// 开始日期（sw.DateBegin年月日）
        /// 结束日期（sw.DateEnd年月日）
        /// 
        /// 
        /// 获取某日最新坐标：
        /// 用户ID列表（为空代表获取所有）
        /// 查询日期（sw.SearchTime 年月日）
        /// 组织机构编码(sw.ORGNO 获取该组织机构下所有的护林员）
        /// </example>
        /// </summary>
        /// <param name="sw">参见条件模型T_IPS_REALDATATEMPORARYSW</param>
        /// <returns>参见模型T_IPS_REALDATATEMPORARYModel</returns>
        public static IEnumerable<T_IPS_REALDATATEMPORARYModel> getModelList(T_IPS_REALDATATEMPORARYSW sw)
        {
            var result = new List<T_IPS_REALDATATEMPORARYModel>();
            DataTable dt = BaseDT.T_IPS_REALDATATEMPORARY.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                T_IPS_REALDATATEMPORARYModel m = new T_IPS_REALDATATEMPORARYModel();
                //m.REALDATAID = dt.Rows[i]["REALDATAID"].ToString();
                m.USERID = dt.Rows[i]["USERID"].ToString();
                m.LONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();
                m.LATITUDE = dt.Rows[i]["LATITUDE"].ToString();
                m.ORILONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();//原始经度
                m.ORILATITUDE = dt.Rows[i]["LATITUDE"].ToString();//原始纬度
                if (sw.MapType != "Skyline")
                {
                    //******************计算坐标偏移量
                    string[] arr = PublicCls.switJWD(m.LATITUDE, m.LONGITUDE);
                    m.LATITUDE = arr[0];
                    m.LONGITUDE = arr[1];
                    //******************计算坐标偏移量 End
                }
                m.HEIGHT = dt.Rows[i]["HEIGHT"].ToString();
                m.ELECTRIC = dt.Rows[i]["ELECTRIC"].ToString();
                m.SPEED = dt.Rows[i]["SPEED"].ToString();
                m.DIRECTION = dt.Rows[i]["DIRECTION"].ToString();
                m.SBTIME = dt.Rows[i]["SBTIME"].ToString();
                if (string.IsNullOrEmpty(m.SBTIME) == false)
                    m.SBTIME = PublicClassLibrary.ClsSwitch.SwitTM(m.SBTIME);
                m.NOTE = dt.Rows[i]["NOTE"].ToString();
                m.ORGNO = dt.Rows[i]["ORGNO"].ToString();
                m.SBDATE = dt.Rows[i]["SBDATE"].ToString();
                if (string.IsNullOrEmpty(m.SBDATE) == false)
                    m.SBDATE = PublicClassLibrary.ClsSwitch.SwitDate(m.SBDATE);
                m.SBTIMEBEGIN = dt.Rows[i]["SBTIMEBEGIN"].ToString();
                m.PATROLLENGTH = dt.Rows[i]["PATROLLENGTH"].ToString();

                result.Add(m);
            }
            return result;
        }
        #endregion

        #region 巡查监控实时InfoWindows

        /// <summary>
        /// 巡查监控实时InfoWindows
        /// </summary>
        /// <example>
        /// sw.SearchTime   查询日期，某日的
        /// sw.ORGNO        机构编码
        /// sw.USERID       护林员序号（多序号以逗号分隔）
        /// </example>
        /// <param name="sw">参见条件模型T_IPS_REALDATATEMPORARYSW</param>
        /// <returns>参见模型T_IPS_REALDATATEMPORARYModel</returns>
        public static IEnumerable<T_IPS_REALDATATEMPORARYModel> getHRUserModelList(T_IPS_REALDATATEMPORARYSW sw)
        {
            var result = new List<T_IPS_REALDATATEMPORARYModel>();
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取当前登录用户有权限查看的组织机构

            DataTable dtFRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", BYORGNO = sw.ORGNO, HID = sw.USERID });//获取所有有权限查看的护林员
            DataTable dt = BaseDT.T_IPS_REALDATATEMPORARY.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_IPS_REALDATATEMPORARYModel m = new T_IPS_REALDATATEMPORARYModel();

                m.USERID = dt.Rows[i]["USERID"].ToString();
                m.LONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();
                m.LATITUDE = dt.Rows[i]["LATITUDE"].ToString();
                m.HEIGHT = dt.Rows[i]["HEIGHT"].ToString();
                m.ELECTRIC = dt.Rows[i]["ELECTRIC"].ToString();
                m.SPEED = dt.Rows[i]["SPEED"].ToString();
                m.DIRECTION = dt.Rows[i]["DIRECTION"].ToString();
                m.SBTIME = dt.Rows[i]["SBTIME"].ToString();
                if (!string.IsNullOrEmpty(m.SBTIME))
                {
                    m.SBTIME = PublicClassLibrary.ClsSwitch.SwitTM(m.SBTIME);
                }
                m.NOTE = dt.Rows[i]["NOTE"].ToString();

                m.SBDATE = dt.Rows[i]["SBDATE"].ToString();
                m.SBTIMEBEGIN = dt.Rows[i]["SBTIMEBEGIN"].ToString();
                m.PATROLLENGTH = dt.Rows[i]["PATROLLENGTH"].ToString();
                DataRow[] drFRUser = dtFRUser.Select("HID=" + m.USERID);
                if (drFRUser.Length > 0)
                {
                    m.HNAME = drFRUser[0]["HNAME"].ToString();
                    m.PHONE = drFRUser[0]["PHONE"].ToString();
                    m.ORGNO = drFRUser[0]["BYORGNO"].ToString();
                    m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtOrg, m.ORGNO);
                }
                result.Add(m);
            }
            return result;
        }
        #endregion

        #region 护林员周边火险分析
        /// <summary>
        /// 护林员周边
        /// </summary>
        /// <param name="sw">周边模型</param>
        /// <returns></returns>
        public static IEnumerable<T_IPS_REALDATATEMPORARYModel> getModelList(HlyAreaDataSW sw)
        {
            var result = new List<T_IPS_REALDATATEMPORARYModel>();
            DataTable dt = BaseDT.T_IPS_REALDATATEMPORARY.getDTByArea(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                T_IPS_REALDATATEMPORARYModel m = new T_IPS_REALDATATEMPORARYModel();
                //m.REALDATAID = dt.Rows[i]["REALDATAID"].ToString();
                m.USERID = dt.Rows[i]["USERID"].ToString();
                m.LONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();
                m.LATITUDE = dt.Rows[i]["LATITUDE"].ToString();
                m.ORILONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();//原始经度
                m.ORILATITUDE = dt.Rows[i]["LATITUDE"].ToString();//原始纬度
                if (sw.MapType != "Skyline")
                {
                    //******************计算坐标偏移量
                    string[] arr = PublicCls.switJWD(m.LATITUDE, m.LONGITUDE);
                    m.LATITUDE = arr[0];
                    m.LONGITUDE = arr[1];
                    //******************计算坐标偏移量 End
                }
                m.HEIGHT = dt.Rows[i]["HEIGHT"].ToString();
                m.ELECTRIC = dt.Rows[i]["ELECTRIC"].ToString();
                m.SPEED = dt.Rows[i]["SPEED"].ToString();
                m.DIRECTION = dt.Rows[i]["DIRECTION"].ToString();
                m.SBTIME = dt.Rows[i]["SBTIME"].ToString();
                if (string.IsNullOrEmpty(m.SBTIME) == false)
                    m.SBTIME = PublicClassLibrary.ClsSwitch.SwitTM(m.SBTIME);
                m.NOTE = dt.Rows[i]["NOTE"].ToString();
                m.ORGNO = dt.Rows[i]["ORGNO"].ToString();
                m.SBDATE = dt.Rows[i]["SBDATE"].ToString();
                if (string.IsNullOrEmpty(m.SBDATE) == false)
                    m.SBDATE = PublicClassLibrary.ClsSwitch.SwitDate(m.SBDATE);
                m.SBTIMEBEGIN = dt.Rows[i]["SBTIMEBEGIN"].ToString();
                m.PATROLLENGTH = dt.Rows[i]["PATROLLENGTH"].ToString();

                result.Add(m);
            }
            return result;
        }
        #endregion
    }
}
