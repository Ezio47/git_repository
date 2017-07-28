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
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 统计分析
    /// </summary>
   public class HUReportCls
   {

       #region 巡检路线明细表
       /// <summary>
       /// 巡检路线明细表
       /// </summary>
       /// <param name="sw">参见PatrolRouteStat_SW</param>
       /// <returns>参见HUReport_PatrolRouteDetail_Model</returns>
       public static IEnumerable<HUReport_PatrolRouteDetail_Model> getPatrolRouteDetailModel(PatrolRouteStat_SW sw)
       {
           var result = new List<HUReport_PatrolRouteDetail_Model>();
           if (string.IsNullOrEmpty(sw.DateBegin))//开始时间为空
               return result;
           if (string.IsNullOrEmpty(sw.DateEnd))//结束时间为空
               return result;
           if (string.IsNullOrEmpty(sw.orgNo))//组织机构编码
               return result;

           T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
           swOrg.SYSFLAG = ConfigCls.getSystemFlag();
           swOrg.TopORGNO = sw.TopORGNO;

           if (PublicCls.OrgIsShi(sw.TopORGNO))
               swOrg.GetContyORGNOByCity = sw.TopORGNO;//获取所有县
           if (PublicCls.OrgIsXian(sw.TopORGNO))
               swOrg.GetXZOrgNOByConty = sw.TopORGNO;//获取所有镇
           DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
           //DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
           DataTable dtHU = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", PhoneHname = sw.PhoneHname, BYORGNO = sw.orgNo });
           //获取护林员每日巡检信息 
           DataTable dtHRRealData = BaseDT.T_IPS_REALDATATEMPORARY.getHRAndRealDataDT(new T_IPS_REALDATATEMPORARYSW { DateBegin = sw.DateBegin, DateEnd = sw.DateEnd, ORGNO = sw.orgNo, PhoneHname = sw.PhoneHname });
           //获取具体巡检情况
           DataTable dtPatrol = BaseDT.T_IPSFR_ROUTERAIL_PATROL.getDT(new T_IPSFR_ROUTERAIL_PATROL_SW { DateBegin = sw.DateBegin, DateEnd = sw.DateEnd });
          
           int days = ClsStr.getDateDiff(sw.DateBegin, sw.DateEnd) + 1;//日期包含天数

           for (int i = 0; i < dtHU.Rows.Count; i++)//获取每个护林员
           {
               string orgNo = dtHU.Rows[i]["BYORGNO"].ToString();
               string HID = dtHU.Rows[i]["HID"].ToString();
               string orgName = BaseDT.T_SYS_ORG.getName(dtOrg, orgNo);
               HUReport_PatrolRouteDetail_Model m = new HUReport_PatrolRouteDetail_Model();
               m.ORGName = orgName;
               m.ORGNo = orgNo;
               m.HNAME = dtHU.Rows[i]["HNAME"].ToString();
               m.PHONE = dtHU.Rows[i]["PHONE"].ToString();
               string CHr = "1";//每天一条路线 
               if (CHr != "0")
               {
                  
                   CHr = (int.Parse(CHr) * days).ToString();
                   string Chr0 = BaseDT.T_IPS_REALDATATEMPORARY.getCountByHID(dtHRRealData, HID).ToString();
                   string Chr1 = ClsStr.getDiff(CHr, Chr0).ToString("F0");
                   string Chr2 = ClsStr.getPercent(Chr0, CHr).ToString("F0");
                  m.LineCount= CHr;//总
                   m.LineCount0= Chr0;//完成
                    m.LineCount1= Chr1;//未完成
                    m.LineCount2 = Chr2 + "%";//完成率
               }
               //巡检与漏检
               string CCHr = BaseDT.T_IPSFR_ROUTERAIL_PATROL.getCountByHID(dtPatrol, HID, "").ToString();
               if (CCHr != "0")
               {
                   
                   // CCHr = (int.Parse(CCHr) * days).ToString();
                   string Chr0 = BaseDT.T_IPSFR_ROUTERAIL_PATROL.getCountByHID(dtPatrol, HID, "1").ToString();
                   string Chr1 = ClsStr.getDiff(CCHr, Chr0).ToString("F0");
                   string Chr2 = ClsStr.getPercent(Chr0, CCHr).ToString("F0");
                   m.PointCount = CCHr;//总
                   m.PointCount0 = Chr0;//完成
                   m.PointCount1 = Chr1;//未完成
                   m.PointCount2 = Chr2 + "%";//完成率
               }
               result.Add(m);
           }
           dtHU.Clear();
           dtHU.Dispose();
           dtHRRealData.Clear();
           dtHRRealData.Dispose();
           dtPatrol.Clear();
           dtPatrol.Dispose();
           dtOrg.Clear();
           dtOrg.Dispose();
           return result;
       }
       #endregion

       #region 巡检路线统计总表
       /// <summary>
       /// 巡检路线统计总表
       /// </summary>
       /// <param name="sw">参见PatrolRouteStat_SW</param>
       /// <returns>参见HUReport_PatrolRouteStat_Model</returns>
       public static IEnumerable<HUReport_PatrolRouteStat_Model> getPatrolRouteStatModel(PatrolRouteStat_SW sw)
       {
           var result = new List<HUReport_PatrolRouteStat_Model>();

           if (string.IsNullOrEmpty(sw.DateBegin))//开始时间为空
               return result;
           if (string.IsNullOrEmpty(sw.DateEnd))//结束时间为空
               return result;

           //DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), TopORGNO=sw.orgNo });
           //获取护林员每日巡检信息
           //DataTable dtHRRealData = BaseDT.T_IPS_REALDATATEMPORARY.getHRAndRealDataDT(new T_IPS_REALDATATEMPORARYSW { DateBegin = sw.DateBegin, DateEnd = sw.DateEnd });
           //获取具体巡检情况 
           //DataTable dtPatrol = BaseDT.T_IPSFR_ROUTERAIL_PATROL.getDT(new T_IPSFR_ROUTERAIL_PATROL_SW { DateBegin = sw.DateBegin, DateEnd = sw.DateEnd });
          // DataTable dtHRRealData = new DataTable();
           //DataTable dtPatrol = new DataTable();
           DataTable dtHU = new DataTable();
           DataTable dtHRRealData = BaseDT.T_IPS_REALDATATEMPORARY.getDTByOrgNo(sw);
           //DataTable dtPatrol = BaseDT.T_IPS_REALDATATEMPORARY.getPATROLCOUNTDTByOrgNo(sw);//获取巡检信息 含未检和已检
           if (PublicCls.OrgIsShi(sw.TopORGNO))
           {
               //dtHRRealData = BaseDT.T_IPS_REALDATATEMPORARY.getDTShi(sw);
               //dtPatrol = BaseDT.T_IPSFR_ROUTERAIL_PATROL.getDTShi(sw);//获取巡检信息 含未检和已检
               dtHU = BaseDT.T_IPSFR_USER.getDTShi(new T_IPSFR_USER_SW { });
           }
           else if (PublicCls.OrgIsXian(sw.TopORGNO))
           {
               //dtHRRealData = BaseDT.T_IPS_REALDATATEMPORARY.getDTXain(sw);
               //dtPatrol = BaseDT.T_IPSFR_ROUTERAIL_PATROL.getDTXain(sw);//获取巡检信息 含未检和已检
               dtHU = BaseDT.T_IPSFR_USER.getDTXain(new T_IPSFR_USER_SW { BYORGNO = sw.TopORGNO });
           }
           else
           {
               //dtHRRealData = BaseDT.T_IPS_REALDATATEMPORARY.getDTZhen(sw);
               //dtPatrol = BaseDT.T_IPSFR_ROUTERAIL_PATROL.getDTZhen(sw);//获取巡检信息 含未检和已检
                dtHU = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", BYORGNO = sw.TopORGNO });
           }

           int days = ClsStr.getDateDiff(sw.DateBegin, sw.DateEnd) + 1;//日期包含天数

           if (PublicCls.OrgIsZhen(sw.TopORGNO))
           {
               DataRow[] drOrg = dtHU.Select("", "");
               for (int i = 0; i < drOrg.Length; i++)
               {
                   HUReport_PatrolRouteStat_Model m = new HUReport_PatrolRouteStat_Model();
                   m.ORGName = drOrg[i]["HNAME"].ToString();
                   m.ORGNo = drOrg[i]["HID"].ToString();



                   string CHr = "";// BaseDT.T_IPSFR_USER.getCountByOrgNo(dtHU, m.ORGNo).ToString();
                   if (CHr != "0")
                   {
                       CHr =  days.ToString();
                       string Chr0 = BaseDT.T_IPS_REALDATATEMPORARY.getDTByOrgNoSum(dtHRRealData, m.ORGNo).ToString();
                       string Chr1 = ClsStr.getDiff(CHr, Chr0).ToString("F0");
                       string Chr2 = ClsStr.getPercent(Chr0, CHr).ToString("F0");
                       m.LineCount = CHr;//总
                       m.LineCount0 = Chr0;//完成
                       m.LineCount1 = Chr1;//未完成
                       m.LineCount2 = Chr2 + "%";//完成率
                   }

                   //巡检与漏检
                   string CCHr = dtHRRealData.Compute("sum(C1)", "BYORGNO='" + m.ORGNo + "'").ToString();
                   CCHr = (string.IsNullOrEmpty(CCHr) ? "0" : CCHr);
                   if (CCHr != "0")
                   {

                       string Chr0 = dtHRRealData.Compute("sum(C2)", "BYORGNO='" + m.ORGNo + "'").ToString();// BaseDT.T_IPSFR_ROUTERAIL_PATROL.getSumByOrgNo(dtPatrol, m.ORGNo, "1");
                       Chr0 = (string.IsNullOrEmpty(Chr0) ? "0" : Chr0);
                       string Chr1 = dtHRRealData.Compute("sum(C3)", "BYORGNO='" + m.ORGNo + "'").ToString();// BaseDT.T_IPSFR_ROUTERAIL_PATROL.getSumByOrgNo(dtPatrol, m.ORGNo, "0");
                       Chr1 = (string.IsNullOrEmpty(Chr1) ? "0" : Chr1);
                       string Chr2 = ClsStr.getPercent(Chr0, CCHr).ToString("F0");
                       m.PointCount = CCHr;
                       m.PointCount0 = Chr0;
                       m.PointCount1 = Chr1;
                       m.PointCount2 = Chr2 + "%";
                   }

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

                   HUReport_PatrolRouteStat_Model m = new HUReport_PatrolRouteStat_Model();
                   m.ORGName = drOrg[i]["ORGNAME"].ToString();
                   m.ORGNo = drOrg[i]["ORGNO"].ToString();



                   string CHr = BaseDT.T_IPSFR_USER.getSumByOrgNo(dtHU, m.ORGNo);//各单位护林员个数
                   if (CHr != "0")
                   {
                       CHr = (int.Parse(CHr) * days).ToString();
                       string Chr0 = BaseDT.T_IPS_REALDATATEMPORARY.getDTByOrgNoSum(dtHRRealData, m.ORGNo).ToString();
                       string Chr1 = ClsStr.getDiff(CHr, Chr0).ToString("F0");
                       string Chr2 = ClsStr.getPercent(Chr0, CHr).ToString("F0");
                       m.LineCount = CHr;//总
                       m.LineCount0 = Chr0;//完成
                       m.LineCount1 = Chr1;//未完成
                       m.LineCount2 = Chr2 + "%";//完成率
                   }
                   //巡检与漏检

                   //巡检与漏检
                   string CCHr = dtHRRealData.Compute("sum(C1)", "BYORGNO='" + m.ORGNo + "'").ToString();
                   CCHr = (string.IsNullOrEmpty(CCHr) ? "0" : CCHr);
                   if (CCHr != "0")
                   {

                       string Chr0 = dtHRRealData.Compute("sum(C2)", "BYORGNO='" + m.ORGNo + "'").ToString();// BaseDT.T_IPSFR_ROUTERAIL_PATROL.getSumByOrgNo(dtPatrol, m.ORGNo, "1");
                       Chr0 = (string.IsNullOrEmpty(Chr0) ? "0" : Chr0);
                       string Chr1 = dtHRRealData.Compute("sum(C3)", "BYORGNO='" + m.ORGNo + "'").ToString();// BaseDT.T_IPSFR_ROUTERAIL_PATROL.getSumByOrgNo(dtPatrol, m.ORGNo, "0");
                       Chr1 = (string.IsNullOrEmpty(Chr1) ? "0" : Chr1);
                       string Chr2 = ClsStr.getPercent(Chr0, CCHr).ToString("F0");
                       m.PointCount = CCHr;
                       m.PointCount0 = Chr0;
                       m.PointCount1 = Chr1;
                       m.PointCount2 = Chr2 + "%";
                   }

                   result.Add(m);
               }
               dtOrg.Clear();
               dtOrg.Dispose();
           }
           //dtPatrol.Clear();
           //dtPatrol.Dispose();
           dtHRRealData.Clear();
           dtHRRealData.Dispose();
           if (1 == 1)
           {
               HUReport_PatrolRouteStat_Model m = new HUReport_PatrolRouteStat_Model();
               m.ORGName = "合计";
               string LineCount = result.Sum(item => Convert.ToDecimal(item.LineCount)).ToString();
               string LineCount0 = result.Sum(item => Convert.ToDecimal(item.LineCount0)).ToString();
               string PointCount = result.Sum(item => Convert.ToDecimal(item.PointCount)).ToString();
               string PointCount0 = result.Sum(item => Convert.ToDecimal(item.PointCount0)).ToString();
               m.LineCount = LineCount;
               m.LineCount0 = LineCount0;
               m.LineCount1 = result.Sum(item => Convert.ToDecimal(item.LineCount1)).ToString();
               m.LineCount2 = ClsStr.getPercent(LineCount0, LineCount).ToString("F0") + "%";
               m.PointCount = PointCount;
               m.PointCount0 = PointCount0;
               m.PointCount1 = result.Sum(item => Convert.ToDecimal(item.PointCount1)).ToString();
               m.PointCount2 = ClsStr.getPercent(PointCount0, PointCount).ToString("F0") + "%";
               result.Insert(0, m);
           }
           return result;
       }

       #endregion

       #region 护林员统计
       /// <summary>
       /// 获取固兼职人数
       /// </summary>
       /// <param name="dt">护林员DataTable</param>
       /// <param name="orgNo">单位编码</param>
       /// <param name="value">固兼职值</param>
       /// <returns>返回人数统计</returns>

       private static string getHUCountByOnstate(DataTable dt, string orgNo, string value)
       {
           string str = "";
           if (orgNo.Substring(4, 5) == "00000")//统计市
           {
                   str = dt.Compute("count(HID)", "substring(BYORGNO,1,4)='" + orgNo.Substring(0, 4) + "' and ONSTATE=" + value + "").ToString();
           }
           else if (orgNo.Substring(6, 3) == "000")//县
           {
                   str = dt.Compute("count(HID)", "substring(BYORGNO,1,6)='" + orgNo.Substring(0, 6) + "' and ONSTATE=" + value + "").ToString();
           }
           else
           {
                   str = dt.Compute("count(HID)", "BYORGNO='" + orgNo + "' and ONSTATE=" + value + "").ToString();
           }
           return str;
       }
       /// <summary>
       /// 性别统计
       /// </summary>
       /// <param name="dt">护林员DataTable</param>
       /// <param name="orgNo">单位编码</param>
       /// <param name="value">性别值</param>
       /// <returns>人数统计</returns>
       private static string getHUCountBySex(DataTable dt, string orgNo, string value)
       {
           string str = "";
           if (orgNo.Substring(4, 5) == "00000")//统计市
           {
               if (string.IsNullOrEmpty(value))
                   str = dt.Compute("count(HID)", "substring(BYORGNO,1,4)='" + orgNo.Substring(0, 4) + "'").ToString();
               else
                   str = dt.Compute("count(HID)", "substring(BYORGNO,1,4)='" + orgNo.Substring(0, 4) + "' and SEX=" + value + "").ToString();
           }
           else if (orgNo.Substring(6, 3) == "000")//县
           {
               if (string.IsNullOrEmpty(value))
                   str = dt.Compute("count(HID)", "substring(BYORGNO,1,6)='" + orgNo.Substring(0, 6) + "'").ToString();
               else
                   str = dt.Compute("count(HID)", "substring(BYORGNO,1,6)='" + orgNo.Substring(0, 6) + "' and SEX=" + value + "").ToString();
           }
           else
           {
               if (string.IsNullOrEmpty(value))
                   str = dt.Compute("count(HID)", "BYORGNO='" + orgNo + "'").ToString();
               else
                   str = dt.Compute("count(HID)", "BYORGNO='" + orgNo + "' and SEX=" + value + "").ToString();
           }
           return str;
       }
       /// <summary>
       /// 护林员统计
       /// </summary>
       /// <returns>参见HUReport_HUCount_Model</returns>
       public static IEnumerable<HUReport_HUCount_Model> getHUCountModel(T_IPSFR_USER_SW sw)
       {
           var result = new List<HUReport_HUCount_Model>();

           T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
           swOrg.SYSFLAG = ConfigCls.getSystemFlag();
           swOrg.TopORGNO = sw.TopORGNO;

           if (PublicCls.OrgIsShi(sw.TopORGNO))
               swOrg.GetContyORGNOByCity = sw.TopORGNO;//获取所有县
           if (PublicCls.OrgIsXian(sw.TopORGNO))
               swOrg.GetXZOrgNOByConty = sw.TopORGNO;//获取所有镇
           DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);

           //DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
           DataTable dtHU = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1" });
           
           DataRow[] drOrg = dtOrg.Select("", "ORGNO");//取所有
           if (1 == 1)
           {
               HUReport_HUCount_Model m = new HUReport_HUCount_Model();

               m.ORGName = BaseDT.T_SYS_ORG.getName(sw.TopORGNO) + "合计";
               m.ORGNo = "";

               m.HUCount = getHUCountBySex(dtHU, sw.TopORGNO, "");
               m.Sex0Count = getHUCountBySex(dtHU, sw.TopORGNO, "1");
               m.Sex1Count = getHUCountBySex(dtHU, sw.TopORGNO, "0");
               m.Onstate0Count = getHUCountByOnstate(dtHU, sw.TopORGNO, "1");
               m.Onstate1Count = getHUCountByOnstate(dtHU, sw.TopORGNO, "2");
               result.Add(m);
           }
           for (int i = 0; i < drOrg.Length; i++)
           {
               HUReport_HUCount_Model m = new HUReport_HUCount_Model();
               string orgName = drOrg[i]["ORGNAME"].ToString();
               m.ORGName = orgName;
               m.ORGNo = drOrg[i]["ORGNO"].ToString();
               
               m.HUCount= getHUCountBySex(dtHU, drOrg[i]["ORGNO"].ToString(), "");
               m.Sex0Count= getHUCountBySex(dtHU, drOrg[i]["ORGNO"].ToString(), "1");
               m.Sex1Count= getHUCountBySex(dtHU, drOrg[i]["ORGNO"].ToString(), "0");
               m.Onstate0Count= getHUCountByOnstate(dtHU, drOrg[i]["ORGNO"].ToString(), "1");
               m.Onstate1Count=getHUCountByOnstate(dtHU, drOrg[i]["ORGNO"].ToString(), "2");
               result.Add(m);
           }
           dtHU.Clear();
           dtHU.Dispose();
           dtOrg.Clear();
           dtOrg.Dispose();
           return result;
       }
       
        #endregion


    }
}
