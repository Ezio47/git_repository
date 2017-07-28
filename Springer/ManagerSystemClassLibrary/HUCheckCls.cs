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
    /// 护林员考勤统计
    /// </summary>
    public class HUCheckCls
    {
        #region 出围统计 Model
        /// <summary>
        /// 考勤统计 Model
        /// </summary>
        /// <param name="sw">参见HUCheckINCount_SW</param>
        /// <returns>参见HUCheck_CheckInCount_Model</returns>getOutRaiLCountModel
        public static IEnumerable<OutRaiLCount_Model> getOutRaiLCountModel(OutRaiLCount_SW sw)
        {
            var result = new List<OutRaiLCount_Model>();
            if (string.IsNullOrEmpty(sw.DateBegin))//开始时间为空
                return result;
            if (string.IsNullOrEmpty(sw.DateEnd))//结束时间为空
                return result;
            if (string.IsNullOrEmpty(sw.TopORGNO))//组织机构编码
                return result;

            DateTime dt1 = Convert.ToDateTime(sw.DateBegin);
            DateTime dt2 = Convert.ToDateTime(sw.DateEnd);
            int days = ClsStr.getDateDiff(sw.DateBegin, sw.DateEnd) + 1;//日期包含天数

            DataTable dt = BaseDT.T_IPSFR_ROUTERAIL_RAIL.getDTByOrgNoToDate(new OutRaiLCount_SW { DateBegin = sw.DateBegin, DateEnd = sw.DateEnd, TopORGNO = sw.TopORGNO });

            DataTable dtHU = BaseDT.T_IPSFR_USER.getDTByOrgSum(new T_IPSFR_USER_SW { BYORGNO = sw.TopORGNO, ISENABLE = "1" });
            if (PublicCls.OrgIsZhen(sw.TopORGNO) == false)//市、县处理 
            {//只获取该市下面的县 县下面的乡

                T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
                swOrg.SYSFLAG = ConfigCls.getSystemFlag();
                swOrg.TopORGNO = sw.TopORGNO;

                if (PublicCls.OrgIsShi(sw.TopORGNO))
                    swOrg.GetContyORGNOByCity = sw.TopORGNO;//获取所有县
                if (PublicCls.OrgIsXian(sw.TopORGNO))
                    swOrg.GetXZOrgNOByConty = sw.TopORGNO;//获取所有镇
                DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");//取所有

                for (int i = 0; i < drOrg.Length; i++)
                {
                    OutRaiLCount_Model m = new OutRaiLCount_Model();


                    m.ORGName = drOrg[i]["ORGNAME"].ToString();
                    m.ORGNo = drOrg[i]["ORGNO"].ToString();
                    string CHr = dt.Compute("sum(C)", "BYORGNO=" + m.ORGNo).ToString();//计算该单位下总数
                    CHr = (string.IsNullOrEmpty(CHr) ? "0" : CHr);
                    m.Count = CHr;
                    string cList = "";
                    for (DateTime tm = dt1; tm <= dt2; tm = tm.AddDays(1))
                    {
                        string tm1 = PublicClassLibrary.ClsSwitch.SwitDate(tm).ToString();
                        if (string.IsNullOrEmpty(cList) == false)
                            cList += ",";
                        string tmp = dt.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "' and SBDATE='" + tm1 + "'").ToString();//计算该日期下数量
                        cList += (string.IsNullOrEmpty(tmp) ? "0" : tmp);//各日期以逗号分隔
                    }
                    m.DayCountList = cList;//日期考勤列表

                    result.Add(m);

                }
                dtOrg.Clear();
                dtOrg.Dispose();
            }
            else//显示乡的，乡的列出各个护林员
            {
                DataRow[] drOrg = dtHU.Select("", "");//取所有

                for (int i = 0; i < drOrg.Length; i++)
                {
                    OutRaiLCount_Model m = new OutRaiLCount_Model();


                    m.ORGName = drOrg[i]["hname"].ToString();
                    m.ORGNo = drOrg[i]["hid"].ToString();
                    string CHr = dt.Compute("sum(C)", "BYORGNO=" + m.ORGNo).ToString();//计算该单位下总数
                    CHr = (string.IsNullOrEmpty(CHr) ? "0" : CHr);
                    m.Count = CHr;

                    string cList = "";
                    for (DateTime tm = dt1; tm <= dt2; tm = tm.AddDays(1))
                    {
                        if (string.IsNullOrEmpty(cList) == false)
                            cList += ",";
                        string tmp = dt.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "' and SBDATE='" + PublicClassLibrary.ClsSwitch.SwitDate(tm).ToString() + "'").ToString();//计算该日期下人数
                        cList += (string.IsNullOrEmpty(tmp) ? "0" : tmp);//各日期以逗号分隔
                    }
                    m.DayCountList = cList;//日期列表
                    result.Add(m);
                }
            }
            dtHU.Clear();
            dtHU.Dispose();

            dt.Clear();
            dt.Dispose();

            if (1 == 1)
            {
                OutRaiLCount_Model m = new OutRaiLCount_Model();
                m.ORGName = "合计";
                string CHr = result.Sum(item => Convert.ToDecimal(item.Count)).ToString();//总数//计算该单位下总数
                CHr = (string.IsNullOrEmpty(CHr) ? "0" : CHr);
                m.Count = CHr;
                int[] arrI = new int[days];
                foreach (var v in result)
                {
                    string[] a = v.DayCountList.Split(',');//组合列表
                    for (int i = 0; i < days; i++)
                    {
                        if (string.IsNullOrEmpty(arrI[i].ToString())) arrI[i] = 0;
                        arrI[i] += int.Parse(a[i]);
                    }
                } string cList = "";
                for (int i = 0; i < days; i++)
                {
                    if (string.IsNullOrEmpty(cList) == false)
                        cList += ",";
                    cList += arrI[i];
                }
                m.DayCountList = cList;
                result.Insert(0, m);
            }

            return result;
        }
        #endregion

        #region 出围详单 Model

        /// <summary>
        /// 查询出围统计
        /// </summary>
        /// <param name="sw">参见OutRaiLCount_SW</param>
        /// <returns>参见OutRaiLDetail_Model</returns>
        public static IEnumerable<OutRaiLDetail_Model> getOutRaiLDetailModel(OutRaiLCount_SW sw)
        {
            var result = new List<OutRaiLDetail_Model>();
            if (string.IsNullOrEmpty(sw.DateBegin))//开始时间为空
                return result;
            if (string.IsNullOrEmpty(sw.DateEnd))//结束时间为空
                return result;
            if (string.IsNullOrEmpty(sw.orgNo))//组织机构编码
                return result;

            //根据机构编码获取下属组织机构 注意：这个函数含本级及所有下级的
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = sw.orgNo });

            //DataTable dtHU = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", BYORGNO = sw.orgNo, PhoneHname = sw.PhoneHname });

            //获取护林员每日巡检信息
            DataTable dt = BaseDT.T_IPSFR_ROUTERAIL_RAIL.getDT(new OutRaiLCount_SW { DateBegin = sw.DateBegin, DateEnd = sw.DateEnd, orgNo = sw.orgNo, PhoneHname = sw.PhoneHname });



            //仅查出围DataRow[] dr = dtHRRealData.Select("PatrolLenError<=" + LengthError.ToString(), "SBDATE DESC");
            DataRow[] dr = dt.Select("", "HID,SBTIME DESC");

            for (int i = 0; i < dr.Length; i++)
            {
                OutRaiLDetail_Model m = new OutRaiLDetail_Model();
                string hid = dr[i]["HID"].ToString();
                string orgNo = dr[i]["BYORGNO"].ToString();
                m.HID = hid;
                m.ORGNo = orgNo;
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtOrg, orgNo);//机构名称
                m.HNAME = dr[i]["HNAME"].ToString();//护林员姓名
                m.PHONE = dr[i]["PHONE"].ToString();//电话
                m.Date = PublicClassLibrary.ClsSwitch.SwitTM(dr[i]["SBTIME"].ToString());//日期
                m.X = dr[i]["LONGITUDE"].ToString();//经度
                m.Y = dr[i]["LATITUDE"].ToString();//纬度
                result.Add(m);
            }
            //dtHU.Clear();
            //dtHU.Dispose();
            dt.Clear();
            dt.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }
        #endregion

        #region 怠工明细 Model

        /// <summary>
        /// 查询怠工统计
        /// </summary>
        /// <param name="sw">参见SabotageCount_SW</param>
        /// <returns>参见SabotageDetail_Model</returns>
        public static IEnumerable<SabotageDetail_Model> getSabotageDetailModel(SabotageCount_SW sw)
        {
            var result = new List<SabotageDetail_Model>();
            if (string.IsNullOrEmpty(sw.DateBegin))//开始时间为空
                return result;
            if (string.IsNullOrEmpty(sw.DateEnd))//结束时间为空
                return result;
            if (string.IsNullOrEmpty(sw.ORGNO))//组织机构编码
                return result;


            //根据机构编码获取下属组织机构 注意：这个函数含本级及所有下级的
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = sw.ORGNO });

            DataTable dtHU = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", BYORGNO = sw.ORGNO, PhoneHname = sw.PhoneHname });

            //获取护林员每日巡检信息
            DataTable dtHRRealData = BaseDT.T_IPS_REALDATATEMPORARY.getHRAndRealDataDT(new T_IPS_REALDATATEMPORARYSW { DateBegin = sw.DateBegin, DateEnd = sw.DateEnd, ORGNO = sw.ORGNO, PhoneHname = sw.PhoneHname });

            float LengthError = ConfigCls.getPatrolLengthError() / 1000;//巡检距离误差


            //仅查怠工DataRow[] dr = dtHRRealData.Select("PatrolLenError<=" + LengthError.ToString(), "SBDATE DESC");
            DataRow[] dr = dtHRRealData.Select("", "HID,SBDATE DESC");

            for (int i = 0; i < dr.Length; i++)
            {
                SabotageDetail_Model m = new SabotageDetail_Model();
                string hid = dr[i]["HID"].ToString();
                m.HID = hid;
                string orgNo = dr[i]["BYORGNO"].ToString();
                m.ORGNo = orgNo;
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtOrg, orgNo);
                m.HNAME = dr[i]["HNAME"].ToString();//护林员姓名
                m.PHONE = dr[i]["PHONE"].ToString();//电话
                m.Date = PublicClassLibrary.ClsSwitch.SwitDate(dr[i]["SBDATE"].ToString());//日期
                m.Count0 = Convert.ToDouble(dr[i]["PATROLLENGTH"].ToString()).ToString("F2");//应巡长度
                m.Count1 = Convert.ToDouble(dr[i]["RealPATROLLENGTH"].ToString()).ToString("F2");//实巡长度

                m.CountPer = ClsStr.getPercent(dr[i]["RealPATROLLENGTH"].ToString(), dr[i]["PATROLLENGTH"].ToString());
                //string wcl1 = wcl.ToString("F0");
                //if (wcl <= 100)
                //    wcl1 = "<font color=red>" + wcl1 + "</font>";

                //m.CountPer = wcl1;
                result.Add(m);
            }

            dtHRRealData.Clear();
            dtHRRealData.Dispose();
            dtHU.Clear();
            dtHU.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }
        #endregion

        #region 怠工统计 Model

        /// <summary>
        /// 查询怠工统计
        /// </summary>
        /// <param name="sw">参见SabotageCount_SW</param>
        /// <returns>参见getSabotageCount_Model</returns>
        public static IEnumerable<getSabotageCount_Model> getSabotageCountModel(SabotageCount_SW sw)
        {

            var result = new List<getSabotageCount_Model>();

            if (string.IsNullOrEmpty(sw.DateBegin))//开始时间为空
                return result;
            if (string.IsNullOrEmpty(sw.DateEnd))//结束时间为空
                return result;

            DateTime dt1 = Convert.ToDateTime(sw.DateBegin);
            DateTime dt2 = Convert.ToDateTime(sw.DateEnd);
            int days = ClsStr.getDateDiff(sw.DateBegin, sw.DateEnd) + 1;//日期包含天数

            DataTable dtHU = new DataTable();
            DataTable dt = BaseDT.T_IPS_REALDATATEMPORARY.getDTSabotageByOrgNo(sw);
            if (PublicCls.OrgIsShi(sw.TopORGNO))
            {
                dtHU = BaseDT.T_IPSFR_USER.getDTShi(new T_IPSFR_USER_SW { });
            }
            else if (PublicCls.OrgIsXian(sw.TopORGNO))
            {
                dtHU = BaseDT.T_IPSFR_USER.getDTXain(new T_IPSFR_USER_SW { BYORGNO = sw.TopORGNO });
            }
            else
            {
                dtHU = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", BYORGNO = sw.TopORGNO });
            }

            if (PublicCls.OrgIsZhen(sw.TopORGNO))
            {
                DataRow[] drOrg = dtHU.Select("", "");
                for (int i = 0; i < drOrg.Length; i++)
                {
                    getSabotageCount_Model m = new getSabotageCount_Model();
                    m.ORGName = drOrg[i]["HNAME"].ToString();
                    m.ORGNo = drOrg[i]["HID"].ToString();
                    string CHr = dt.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "'").ToString();
                    CHr = (string.IsNullOrEmpty(CHr) ? "0" : CHr);
                    string cList = "";
                    for (DateTime tm = dt1; tm <= dt2; tm = tm.AddDays(1))
                    {
                        string tm1 = PublicClassLibrary.ClsSwitch.SwitDate(tm).ToString();
                        if (string.IsNullOrEmpty(cList) == false)
                            cList += ",";
                        string tmp = dt.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "' and SBDATE='" + tm1 + "'  and PatrolLenErro=1").ToString();//计算该日期下人数
                        cList += (string.IsNullOrEmpty(tmp) ? "0" : tmp);//各日期以逗号分隔
                    }
                    m.DayCountList = cList;//日期考勤列表

                    string Chr0 = dt.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "' and PatrolLenErro=0").ToString();
                    Chr0 = (string.IsNullOrEmpty(Chr0) ? "0" : Chr0);
                    string Chr1 = ClsStr.getDiff(CHr, Chr0).ToString("F0");
                    string Chr2 = ClsStr.getPercent(Chr0, CHr).ToString("F0");
                    m.Count = CHr;//总
                    m.Count0 = Chr0;//完成
                    m.Count1 = Chr1;//未完成
                    m.CountPer = Chr2 + "%";//完成率

                    result.Add(m);
                }
                dtHU.Clear();
                dtHU.Dispose();
            }
            else
            {

                T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
                swOrg.SYSFLAG = ConfigCls.getSystemFlag();
                swOrg.TopORGNO = sw.TopORGNO;

                if (PublicCls.OrgIsShi(sw.TopORGNO))
                    swOrg.GetContyORGNOByCity = sw.TopORGNO;//获取所有县
                if (PublicCls.OrgIsXian(sw.TopORGNO))
                    swOrg.GetXZOrgNOByConty = sw.TopORGNO;//获取所有镇
                DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);

                DataRow[] drOrg = dtOrg.Select("", "ORGNO");//取所有
                for (int i = 0; i < drOrg.Length; i++)
                {
                    // string orgName = drOrg[i]["ORGNAME"].ToString();
                    //string orgNo = drOrg[i]["ORGNO"].ToString();

                    getSabotageCount_Model m = new getSabotageCount_Model();
                    m.ORGName = drOrg[i]["ORGNAME"].ToString();
                    m.ORGNo = drOrg[i]["ORGNO"].ToString();



                    string CHr = dt.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "'").ToString();
                    CHr = (string.IsNullOrEmpty(CHr) ? "0" : CHr);
                    string cList = "";
                    for (DateTime tm = dt1; tm <= dt2; tm = tm.AddDays(1))
                    {
                        string tm1 = PublicClassLibrary.ClsSwitch.SwitDate(tm).ToString();
                        if (string.IsNullOrEmpty(cList) == false)
                            cList += ",";
                        string tmp = dt.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "' and SBDATE='" + tm1 + "'  and PatrolLenErro=0").ToString();//计算该日期下人数
                        cList += (string.IsNullOrEmpty(tmp) ? "0" : tmp);//各日期以逗号分隔
                    }
                    m.DayCountList = cList;//日期考勤列表

                    string Chr0 = dt.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "' and PatrolLenErro=1").ToString();
                    Chr0 = (string.IsNullOrEmpty(Chr0) ? "0" : Chr0);
                    string Chr1 = ClsStr.getDiff(CHr, Chr0).ToString("F0");
                    string Chr2 = ClsStr.getPercent(Chr0, CHr).ToString("F0");
                    m.Count = CHr;//总
                    m.Count0 = Chr0;//完成
                    m.Count1 = Chr1;//未完成
                    m.CountPer = Chr2 + "%";//完成率


                    result.Add(m);
                }
                dtOrg.Clear();
                dtOrg.Dispose();
            }
            dt.Clear();
            dt.Dispose();
            if (1 == 1)
            {
                getSabotageCount_Model m = new getSabotageCount_Model();
                m.ORGName = "合计";
                string LineCount = result.Sum(item => Convert.ToDecimal(item.Count)).ToString();
                string Count0 = result.Sum(item => Convert.ToDecimal(item.Count0)).ToString();
                string Count1 = result.Sum(item => Convert.ToDecimal(item.Count1)).ToString();
                m.Count = LineCount;
                m.Count0 = Count0;
                m.Count1 = Count1;
                m.CountPer = ClsStr.getPercent(m.Count0, m.Count).ToString("F0") + "%";

                int[] arrI = new int[days];
                foreach (var v in result)
                {
                    string[] a = v.DayCountList.Split(',');//组合列表
                    for (int i = 0; i < days; i++)
                    {
                        if (string.IsNullOrEmpty(arrI[i].ToString())) arrI[i] = 0;
                        arrI[i] += int.Parse(a[i]);
                    }
                } string cList = "";
                for (int i = 0; i < days; i++)
                {
                    if (string.IsNullOrEmpty(cList) == false)
                        cList += ",";
                    cList += arrI[i];
                }
                m.DayCountList = cList;


                result.Insert(0, m);
            }
            return result;
        }
        #endregion

        #region 漏检统计详单 Model

        /// <summary>
        /// 查询漏检统计
        /// </summary>
        /// <param name="sw">参见OmitCheckCount_SW</param>
        /// <returns>参见HUCheck_OmitDetail_Model</returns>
        public static IEnumerable<HUCheck_OmitDetail_Model> getOmitDetailModel(OmitCheckCount_SW sw)
        {
            var result = new List<HUCheck_OmitDetail_Model>();
            if (string.IsNullOrEmpty(sw.DateBegin))//开始时间为空
                return result;
            if (string.IsNullOrEmpty(sw.DateEnd))//结束时间为空
                return result;
            if (string.IsNullOrEmpty(sw.ORGNO))//组织机构编码
                return result;
            //根据机构编码获取下属组织机构 注意：这个函数含本级及所有下级的
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = sw.ORGNO });

            //DataTable dtHU = BaseDT.T_IPSFR_USER.getDT(
            //    new T_IPSFR_USER_SW
            //    {
            //        ISENABLE = "1",
            //        BYORGNO = sw.ORGNO,
            //        HNAME = sw.PhoneHname,
            //        HID = sw.HID
            //    });
            //获取具体巡检情况 
            DataTable dtPatrol = BaseDT.T_IPSFR_ROUTERAIL_PATROL.getDT(
                new T_IPSFR_ROUTERAIL_PATROL_SW
                {
                    DateBegin = sw.DateBegin,
                    DateEnd = sw.DateEnd,
                    orgNo = sw.ORGNO,
                    PhoneHname = sw.PhoneHname,
                    HID = sw.HID
                });
            DataRow[] dr = dtPatrol.Select("", "HID,ROUTEDATE desc,ROADID");//取所有

            for (int i = 0; i < dr.Length; i++)
            {
                HUCheck_OmitDetail_Model m = new HUCheck_OmitDetail_Model();
                string orgNo = dr[i]["BYORGNO"].ToString();
                m.ORGNo = orgNo;
                string orgName = BaseDT.T_SYS_ORG.getName(dtOrg, orgNo);
                m.ORGName = orgName;
                m.HNAME = dr[i]["HNAME"].ToString();
                m.PHONE = dr[i]["PHONE"].ToString();
                m.ROUTEDATE = PublicClassLibrary.ClsSwitch.SwitDate(dr[i]["ROUTEDATE"].ToString());
                m.ROUTESTATE = dr[i]["ROUTESTATE"].ToString();

                m.ROUTESTATE = (m.ROUTESTATE == "1") ? "√" : "×";
                m.LONGITUDE = dr[i]["LONGITUDE"].ToString();
                m.LATITUDE = dr[i]["LATITUDE"].ToString();
                result.Add(m);

            }
            //dtHU.Clear();
            //dtHU.Dispose();
            dtPatrol.Clear();
            dtPatrol.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }
        #endregion

        #region 漏检统计 Model
        /// <summary>
        /// 漏检统计 Model
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<HUCheck_OmitCount_Model> getOmitCheckModel(OmitCheckCount_SW sw)
        {
            var result = new List<HUCheck_OmitCount_Model>();
            if (string.IsNullOrEmpty(sw.DateBegin))//开始时间为空
                return result;
            if (string.IsNullOrEmpty(sw.DateEnd))//结束时间为空
                return result;
            if (string.IsNullOrEmpty(sw.ORGNO))//组织机构编码
                return result;

            DateTime dt1 = Convert.ToDateTime(sw.DateBegin);
            DateTime dt2 = Convert.ToDateTime(sw.DateEnd);
            int days = ClsStr.getDateDiff(sw.DateBegin, sw.DateEnd) + 1;//日期包含天数

            //根据机构编码获取下属组织机构 注意：这个函数含本级及所有下级的
            //DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = sw.ORGNO });
            DataTable dt = BaseDT.T_IPSFR_ROUTERAIL_PATROL.getDTByOrgNoToDate(new PatrolRouteStat_SW { DateBegin = sw.DateBegin, DateEnd = sw.DateEnd, TopORGNO = sw.ORGNO });

            DataTable dtHU = BaseDT.T_IPSFR_USER.getDTByOrgSum(new T_IPSFR_USER_SW { BYORGNO = sw.ORGNO, ISENABLE = "1" });
            if (PublicCls.OrgIsZhen(sw.ORGNO) == false)//市、县处理 
            {
                //只获取该市下面的县 县下面的乡
                T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
                swOrg.SYSFLAG = ConfigCls.getSystemFlag();
                swOrg.TopORGNO = sw.ORGNO;

                if (PublicCls.OrgIsShi(sw.ORGNO))
                    swOrg.GetContyORGNOByCity = sw.ORGNO;//获取所有县
                if (PublicCls.OrgIsXian(sw.ORGNO))
                    swOrg.GetXZOrgNOByConty = sw.ORGNO;//获取所有镇
                DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");//取所有

                for (int i = 0; i < drOrg.Length; i++)
                {
                    HUCheck_OmitCount_Model m = new HUCheck_OmitCount_Model();
                    m.ORGName = drOrg[i]["ORGNAME"].ToString();
                    m.ORGNo = drOrg[i]["ORGNO"].ToString();
                    string CHr = dtHU.Compute("sum(C)", "BYORGNO=" + m.ORGNo).ToString();//计算该单位下总人数
                    CHr = (string.IsNullOrEmpty(CHr) ? "0" : CHr);
                    m.HUCount = CHr;//考勤人数
                    string cList = "";
                    for (DateTime tm = dt1; tm <= dt2; tm = tm.AddDays(1))
                    {
                        string tm1 = PublicClassLibrary.ClsSwitch.SwitDate(tm).ToString();
                        if (string.IsNullOrEmpty(cList) == false)
                            cList += ",";
                        string tmp = dt.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "' and ROUTEDATE='" + tm1 + "'").ToString();//计算该日期下人数
                        cList += (string.IsNullOrEmpty(tmp) ? "0" : tmp);//各日期以逗号分隔
                    }
                    m.DayCountList = cList;//日期考勤列表
                    if (CHr != "0")
                    {
                        CHr = (int.Parse(CHr) * days).ToString();

                        string Chr0 = dt.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "'").ToString();
                        Chr0 = (string.IsNullOrEmpty(Chr0) ? "0" : Chr0); ;// BaseDT.T_IPS_REALDATATEMPORARY.getCountByOrgNo(dtHRRealData, orgNo).ToString();
                        string Chr1 = ClsStr.getDiff(CHr, Chr0).ToString("F0");
                        Chr1 = (string.IsNullOrEmpty(Chr1) ? "0" : Chr1);
                        string Chr2 = ClsStr.getPercent(Chr0, CHr).ToString("F0");
                        Chr2 = (string.IsNullOrEmpty(Chr2) ? "0" : Chr2);
                        m.OmitCount0 = Chr0;//总
                        m.OmitCount1 = Chr0;//完成
                        m.OmitCount2 = Chr1;//完成
                        m.OmitCount3 = Chr2 + "%";//完成率
                    }
                    result.Add(m);
                }
                dtOrg.Clear();
                dtOrg.Dispose();
            }
            else//显示乡的，乡的列出各个护林员
            {
                DataRow[] drOrg = dtHU.Select("", "");//取所有

                for (int i = 0; i < drOrg.Length; i++)
                {
                    HUCheck_OmitCount_Model m = new HUCheck_OmitCount_Model();
                    m.ORGName = drOrg[i]["hname"].ToString();
                    m.ORGNo = drOrg[i]["hid"].ToString();
                    string CHr = "1";// dtHU.Compute("BYORGNO=" + m.ORGNo, "").ToString();//计算该单位下总人数
                    m.HUCount = (string.IsNullOrEmpty(CHr) ? "0" : CHr);//考勤人数
                    string cList = "";
                    for (DateTime tm = dt1; tm <= dt2; tm = tm.AddDays(1))
                    {
                        if (string.IsNullOrEmpty(cList) == false)
                            cList += ",";
                        string tmp = dt.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "' and ROUTEDATE='" + PublicClassLibrary.ClsSwitch.SwitDate(tm).ToString() + "'").ToString();//计算该日期下人数
                        cList += (string.IsNullOrEmpty(tmp) ? "0" : tmp);//各日期以逗号分隔
                    }
                    m.DayCountList = cList;//日期考勤列表
                    if (CHr != "0")
                    {
                        CHr = (int.Parse(CHr) * days).ToString();

                        string Chr0 = dt.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "'").ToString();
                        Chr0 = (string.IsNullOrEmpty(Chr0) ? "0" : Chr0); ;// BaseDT.T_IPS_REALDATATEMPORARY.getCountByOrgNo(dtHRRealData, orgNo).ToString();
                        string Chr1 = ClsStr.getDiff(CHr, Chr0).ToString("F0");
                        Chr1 = (string.IsNullOrEmpty(Chr1) ? "0" : Chr1);
                        string Chr2 = ClsStr.getPercent(Chr0, CHr).ToString("F0");
                        Chr2 = (string.IsNullOrEmpty(Chr2) ? "0" : Chr2);
                        m.OmitCount0 = CHr;//总
                        m.OmitCount1 = Chr0;//完成
                        m.OmitCount2 = Chr1;//完成
                        m.OmitCount3 = Chr2 + "%";//完成率
                    }
                    result.Add(m);
                }
            }
            dtHU.Clear();
            dtHU.Dispose();

            //根据机构编码获取所有护林员信息
            // DataTable dtHU = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { BYORGNO = sw.ORGNO, ISENABLE = "1" });

            dt.Clear();
            dt.Dispose();

            if (1 == 1)
            {
                HUCheck_OmitCount_Model m = new HUCheck_OmitCount_Model();
                m.ORGName = "合计";
                string HUCount = result.Sum(item => Convert.ToDecimal(item.HUCount)).ToString();//总数
                string CHr = result.Sum(item => Convert.ToDecimal(item.OmitCount0)).ToString();
                string Chr0 = result.Sum(item => Convert.ToDecimal(item.OmitCount1)).ToString();
                string Chr1 = result.Sum(item => Convert.ToDecimal(item.OmitCount2)).ToString();
                //string OmitCount2 = result.Sum(item => Convert.ToDecimal(item.OmitCount2)).ToString();
                m.HUCount = (string.IsNullOrEmpty(HUCount) ? "0" : HUCount);

                m.OmitCount0 = CHr;//总
                m.OmitCount1 = Chr0;//完成
                m.OmitCount2 = Chr1;//未完成
                m.OmitCount3 = ClsStr.getPercent(m.OmitCount1, m.OmitCount0).ToString("F0") + "%";//完成率

                int[] arrI = new int[days];
                foreach (var v in result)
                {
                    string[] a = v.DayCountList.Split(',');//组合列表
                    for (int i = 0; i < days; i++)
                    {
                        if (string.IsNullOrEmpty(arrI[i].ToString())) arrI[i] = 0;
                        arrI[i] += int.Parse(a[i]);
                    }
                } string cList = "";
                for (int i = 0; i < days; i++)
                {
                    if (string.IsNullOrEmpty(cList) == false)
                        cList += ",";
                    cList += arrI[i];
                }
                m.DayCountList = cList;
                //m.LineCount2 = ClsStr.getPercent(LineCount0, LineCount).ToString("F0") + "%";
                //m.PointCount = PointCount;
                //m.PointCount0 = PointCount0;
                //m.PointCount1 = result.Sum(item => Convert.ToDecimal(item.PointCount1)).ToString();
                result.Insert(0, m);
            }
            return result;
        }
        /// <summary>
        /// 查询漏检统计
        /// </summary>
        /// <param name="sw">参见OmitCheckCount_SW</param>
        /// <returns>参见HUCheck_OmitCount_Model</returns>
        public static IEnumerable<HUCheck_OmitCount_Model> getOmitCheckModel1(OmitCheckCount_SW sw)
        {
            var result = new List<HUCheck_OmitCount_Model>();
            if (string.IsNullOrEmpty(sw.DateBegin))//开始时间为空
                return result;
            if (string.IsNullOrEmpty(sw.DateEnd))//结束时间为空
                return result;
            if (string.IsNullOrEmpty(sw.ORGNO))//组织机构编码
                return result;
            //根据机构编码获取下属组织机构 注意：这个函数含本级及所有下级的
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = sw.ORGNO });

            DataTable dtHU = BaseDT.T_IPSFR_USER.getDT(
                new T_IPSFR_USER_SW
                {
                    ISENABLE = "1",
                    BYORGNO = sw.ORGNO,
                    HNAME = sw.PhoneHname,
                    HID = sw.HID
                });
            //获取具体巡检情况 
            DataTable dtPatrol = BaseDT.T_IPSFR_ROUTERAIL_PATROL.getDT(
                new T_IPSFR_ROUTERAIL_PATROL_SW
                {
                    DateBegin = sw.DateBegin,
                    DateEnd = sw.DateEnd,
                    orgNo = sw.ORGNO,
                    PhoneHname = sw.PhoneHname,
                    HID = sw.HID
                });
            DateTime dt1 = Convert.ToDateTime(sw.DateBegin);
            DateTime dt2 = Convert.ToDateTime(sw.DateEnd);

            DataRow[] drOrg = dtOrg.Select("", "ORGNO");//取所有
            int days = ClsStr.getDateDiff(sw.DateBegin, sw.DateEnd) + 1;//日期包含天数
            for (int i = 0; i < drOrg.Length; i++)
            {
                HUCheck_OmitCount_Model m = new HUCheck_OmitCount_Model();
                string orgNo = drOrg[i]["ORGNO"].ToString();
                m.ORGNo = orgNo;
                string orgName = BaseDT.T_SYS_ORG.getName(dtOrg, orgNo);
                m.ORGName = orgName;
                //巡检与漏检
                string CCHr = BaseDT.T_IPSFR_ROUTERAIL_PATROL.getCountByOrgNo(dtPatrol, orgNo, "").ToString();
                if (CCHr != "0")
                {
                    string Chr0 = BaseDT.T_IPSFR_ROUTERAIL_PATROL.getCountByOrgNo(dtPatrol, orgNo, "1").ToString();
                    string Chr1 = ClsStr.getDiff(CCHr, Chr0).ToString("F0");
                    string Chr2 = ClsStr.getPercent(Chr0, CCHr).ToString("F0");
                    m.OmitCount0 = CCHr;//总
                    m.OmitCount1 = Chr0;//完成
                    m.OmitCount2 = Chr1;//未完成
                    m.OmitCount3 = Chr2 + "%";//完成率
                }
                result.Add(m);
            }
            if (PublicCls.OrgIsZhen(sw.ORGNO)) //显示所有护林员
            {


                DataRow[] drHU = dtHU.Select("", "");
                for (int k = 0; k < drHU.Length; k++)//循环护林员
                {
                    HUCheck_OmitCount_Model m = new HUCheck_OmitCount_Model();
                    string HID = dtHU.Rows[k]["HID"].ToString();
                    m.HID = HID;
                    m.ORGName = dtHU.Rows[k]["HNAME"].ToString();//护林员姓名与机构名称共用一个

                    string CCHr1 = BaseDT.T_IPSFR_ROUTERAIL_PATROL.getCountByHID(dtPatrol, HID, "").ToString();
                    if (CCHr1 != "0")
                    {
                        string Chr0 = BaseDT.T_IPSFR_ROUTERAIL_PATROL.getCountByHID(dtPatrol, HID, "1").ToString();
                        string Chr1 = ClsStr.getDiff(CCHr1, Chr0).ToString("F0");
                        string Chr2 = ClsStr.getPercent(Chr0, CCHr1).ToString("F0");
                        m.OmitCount0 = CCHr1;//总
                        m.OmitCount1 = Chr0;//完成
                        m.OmitCount2 = Chr1;//未完成
                        m.OmitCount3 = Chr2 + "%";//完成率
                    }
                    result.Add(m);
                }
            }
            dtHU.Clear();
            dtHU.Dispose();
            dtPatrol.Clear();
            dtPatrol.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }
        #endregion

        #region 考勤统计 Model
        /// <summary>
        /// 考勤统计 Model
        /// </summary>
        /// <param name="sw">参见HUCheckINCount_SW</param>
        /// <returns>参见HUCheck_CheckInCount_Model</returns>
        public static IEnumerable<HUCheck_CheckInCount_Model> getCheckInModel(HUCheckINCount_SW sw)
        {
            var result = new List<HUCheck_CheckInCount_Model>();
            if (string.IsNullOrEmpty(sw.DateBegin))//开始时间为空
                return result;
            if (string.IsNullOrEmpty(sw.DateEnd))//结束时间为空
                return result;
            if (string.IsNullOrEmpty(sw.ORGNO))//组织机构编码
                return result;

            DateTime dt1 = Convert.ToDateTime(sw.DateBegin);
            DateTime dt2 = Convert.ToDateTime(sw.DateEnd);
            int days = ClsStr.getDateDiff(sw.DateBegin, sw.DateEnd) + 1;//日期包含天数

            //根据机构编码获取下属组织机构 注意：这个函数含本级及所有下级的
            //DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = sw.ORGNO });
            DataTable dtHRRealData = BaseDT.T_IPS_REALDATATEMPORARY.getDTByOrgNoToDate(new PatrolRouteStat_SW { DateBegin = sw.DateBegin, DateEnd = sw.DateEnd, TopORGNO = sw.ORGNO });

            DataTable dtHU = BaseDT.T_IPSFR_USER.getDTByOrgSum(new T_IPSFR_USER_SW { BYORGNO = sw.ORGNO, ISENABLE = "1" });
            if (PublicCls.OrgIsZhen(sw.ORGNO) == false)//市、县处理 
            {//只获取该市下面的县 县下面的乡

                T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
                swOrg.SYSFLAG = ConfigCls.getSystemFlag();
                swOrg.TopORGNO = sw.ORGNO;

                if (PublicCls.OrgIsShi(sw.ORGNO))
                    swOrg.GetContyORGNOByCity = sw.ORGNO;//获取所有县
                if (PublicCls.OrgIsXian(sw.ORGNO))
                    swOrg.GetXZOrgNOByConty = sw.ORGNO;//获取所有镇
                DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");//取所有

                for (int i = 0; i < drOrg.Length; i++)
                {
                    HUCheck_CheckInCount_Model m = new HUCheck_CheckInCount_Model();


                    m.ORGName = drOrg[i]["ORGNAME"].ToString();
                    m.ORGNo = drOrg[i]["ORGNO"].ToString();
                    string CHr = dtHU.Compute("sum(C)", "BYORGNO=" + m.ORGNo).ToString();//计算该单位下总人数
                    CHr = (string.IsNullOrEmpty(CHr) ? "0" : CHr);
                    m.HUCount = CHr;//考勤人数
                    string cList = "";
                    for (DateTime tm = dt1; tm <= dt2; tm = tm.AddDays(1))
                    {
                        string tm1 = PublicClassLibrary.ClsSwitch.SwitDate(tm).ToString();
                        if (string.IsNullOrEmpty(cList) == false)
                            cList += ",";
                        string tmp = dtHRRealData.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "' and SBDATE='" + tm1 + "'").ToString();//计算该日期下人数
                        cList += (string.IsNullOrEmpty(tmp) ? "0" : tmp);//各日期以逗号分隔
                    }
                    m.DayCountList = cList;//日期考勤列表
                    if (CHr != "0")
                    {
                        CHr = (int.Parse(CHr) * days).ToString();

                        string Chr0 = dtHRRealData.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "'").ToString();
                        Chr0 = (string.IsNullOrEmpty(Chr0) ? "0" : Chr0); ;// BaseDT.T_IPS_REALDATATEMPORARY.getCountByOrgNo(dtHRRealData, orgNo).ToString();
                        string Chr1 = ClsStr.getDiff(CHr, Chr0).ToString("F0");
                        Chr1 = (string.IsNullOrEmpty(Chr1) ? "0" : Chr1);
                        string Chr2 = ClsStr.getPercent(Chr0, CHr).ToString("F0");
                        Chr2 = (string.IsNullOrEmpty(Chr2) ? "0" : Chr2);
                        m.daysC = CHr;//总
                        m.daysOK = Chr0;//完成
                        m.daysPer = Chr2 + "%";//完成率
                    }
                    result.Add(m);

                }
                dtOrg.Clear();
                dtOrg.Dispose();
            }
            else//显示乡的，乡的列出各个护林员
            {
                DataRow[] drOrg = dtHU.Select("", "");//取所有

                for (int i = 0; i < drOrg.Length; i++)
                {
                    HUCheck_CheckInCount_Model m = new HUCheck_CheckInCount_Model();


                    m.ORGName = drOrg[i]["hname"].ToString();
                    m.ORGNo = drOrg[i]["hid"].ToString();
                    string CHr = "1";// dtHU.Compute("BYORGNO=" + m.ORGNo, "").ToString();//计算该单位下总人数
                    m.HUCount = (string.IsNullOrEmpty(CHr) ? "0" : CHr);//考勤人数
                    string cList = "";
                    for (DateTime tm = dt1; tm <= dt2; tm = tm.AddDays(1))
                    {
                        if (string.IsNullOrEmpty(cList) == false)
                            cList += ",";
                        string tmp = dtHRRealData.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "' and SBDATE='" + PublicClassLibrary.ClsSwitch.SwitDate(tm).ToString() + "'").ToString();//计算该日期下人数
                        cList += (string.IsNullOrEmpty(tmp) ? "0" : tmp);//各日期以逗号分隔
                    }
                    m.DayCountList = cList;//日期考勤列表
                    if (CHr != "0")
                    {
                        CHr = (int.Parse(CHr) * days).ToString();

                        string Chr0 = dtHRRealData.Compute("sum(C)", "BYORGNO='" + m.ORGNo + "'").ToString();
                        Chr0 = (string.IsNullOrEmpty(Chr0) ? "0" : Chr0); ;// BaseDT.T_IPS_REALDATATEMPORARY.getCountByOrgNo(dtHRRealData, orgNo).ToString();
                        string Chr1 = ClsStr.getDiff(CHr, Chr0).ToString("F0");
                        Chr1 = (string.IsNullOrEmpty(Chr1) ? "0" : Chr1);
                        string Chr2 = ClsStr.getPercent(Chr0, CHr).ToString("F0");
                        Chr2 = (string.IsNullOrEmpty(Chr2) ? "0" : Chr2);
                        m.daysC = CHr;//总
                        m.daysOK = Chr0;//完成
                        m.daysPer = Chr2 + "%";//完成率
                    }
                    result.Add(m);
                }
            }
            dtHU.Clear();
            dtHU.Dispose();

            //根据机构编码获取所有护林员信息
            // DataTable dtHU = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { BYORGNO = sw.ORGNO, ISENABLE = "1" });

            dtHRRealData.Clear();
            dtHRRealData.Dispose();

            if (1 == 1)
            {
                HUCheck_CheckInCount_Model m = new HUCheck_CheckInCount_Model();
                m.ORGName = "合计";
                string HUCount = result.Sum(item => Convert.ToDecimal(item.HUCount)).ToString();//总数
                string daysC = result.Sum(item => Convert.ToDecimal(item.daysC)).ToString();
                string daysOK = result.Sum(item => Convert.ToDecimal(item.daysOK)).ToString();
                HUCount = (string.IsNullOrEmpty(HUCount) ? "0" : HUCount);
                daysC = (string.IsNullOrEmpty(daysC) ? "0" : daysC);
                daysOK = (string.IsNullOrEmpty(daysOK) ? "0" : daysOK);
                //string PointCount0 = result.Sum(item => Convert.ToDecimal(item.PointCount0)).ToString();
                m.HUCount = HUCount;
                m.daysC = daysC;//总天数
                m.daysOK = daysOK;//已出勤
                int[] arrI = new int[days];
                foreach (var v in result)
                {
                    string[] a = v.DayCountList.Split(',');//组合列表
                    for (int i = 0; i < days; i++)
                    {
                        if (string.IsNullOrEmpty(arrI[i].ToString())) arrI[i] = 0;
                        arrI[i] += int.Parse(a[i]);
                    }
                } string cList = "";
                for (int i = 0; i < days; i++)
                {
                    if (string.IsNullOrEmpty(cList) == false)
                        cList += ",";
                    cList += arrI[i];
                }
                m.DayCountList = cList;
                //m.LineCount2 = ClsStr.getPercent(LineCount0, LineCount).ToString("F0") + "%";
                //m.PointCount = PointCount;
                //m.PointCount0 = PointCount0;
                //m.PointCount1 = result.Sum(item => Convert.ToDecimal(item.PointCount1)).ToString();
                m.daysPer = ClsStr.getPercent(m.daysOK, m.daysC).ToString("F0") + "%";
                result.Insert(0, m);
            }

            return result;
        }
        #endregion
    }
}
