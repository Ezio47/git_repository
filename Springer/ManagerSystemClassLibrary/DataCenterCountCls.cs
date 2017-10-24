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
    /// 数据中心统计
    /// </summary>
    public class DataCenterCountCls
    {
        #region 数据中心队伍统计
        #region 队伍统计
        /// <summary>
        /// 队伍类型统计
        /// </summary>
        /// <param name="dt">队伍DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <param name="type">队伍类型值</param>
        /// <returns>队伍统计</returns>
        private static string getCountByARMYTYPE(DataTable dt, string orgNo, string type)
        {
            string str = "";
            if (orgNo.Substring(4, 5) == "00000")//统计市
            {
                if (string.IsNullOrEmpty(type))
                    str = dt.Compute("count(DC_ARMY_ID)", "substring(BYORGNO,1,4)='" + orgNo.Substring(0, 4) + "'").ToString();
                else
                    str = dt.Compute("count(DC_ARMY_ID)", "substring(BYORGNO,1,4)='" + orgNo.Substring(0, 4) + "' and ARMYTYPE=" + type + "").ToString();
            }
            else if (orgNo.Substring(6, 3) == "000")//县
            {
                if (string.IsNullOrEmpty(type))
                    str = dt.Compute("count(DC_ARMY_ID)", "substring(BYORGNO,1,6)='" + orgNo.Substring(0, 6) + "'").ToString();
                else
                    str = dt.Compute("count(DC_ARMY_ID)", "substring(BYORGNO,1,6)='" + orgNo.Substring(0, 6) + "' and ARMYTYPE=" + type + "").ToString();
            }
            else
            {
                if (string.IsNullOrEmpty(type))
                    str = dt.Compute("count(DC_ARMY_ID)", "BYORGNO='" + orgNo + "'").ToString();
                else
                    str = dt.Compute("count(DC_ARMY_ID)", "BYORGNO='" + orgNo + "' and ARMYTYPE=" + type + "").ToString();
            }
            return str;
        }
        /// <summary>
        /// 数据中心-队伍统计
        /// </summary>
        /// <returns>参见DataCenterHU_Model</returns>
        public static IEnumerable<DC_ARMYCount_Model> getModelCount(DC_ARMY_SW sw, out IEnumerable<DC_ARMY_TypeCountModel> TypeModel)
        {
            var result = new List<DC_ARMYCount_Model>();
            //获取单位
            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.TopEchartORGNO = sw.TopORGNO;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            DataTable dt26 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "26" });//数据中心队伍类型
            DataTable dtarmy = BaseDT.DC_ARMY.getDT(sw);
            //out 类别
            var ArmyType = new List<DC_ARMY_TypeCountModel>();
            for (int i = 0; i < dt26.Rows.Count; i++)
            {
                DC_ARMY_TypeCountModel m = new DC_ARMY_TypeCountModel();
                m.DICTNAME = dt26.Rows[i]["DICTNAME"].ToString();
                m.DICTVALUE = dt26.Rows[i]["DICTVALUE"].ToString();
                ArmyType.Add(m);
            }
            TypeModel = ArmyType;
            if (PublicCls.OrgIsZhen(sw.TopORGNO) == false)
            {
                DC_ARMYCount_Model mm = new DC_ARMYCount_Model();
                mm.ORGName = BaseDT.T_SYS_ORG.getName(sw.TopORGNO) + "合计";
                mm.ORGNo = sw.TopORGNO;
                mm.ARMYCount = BaseDT.DC_ARMY.getCountarmyByOrgNo(dtarmy, sw.TopORGNO, "");//统计队伍总数
                mm.MEMBERCount = BaseDT.DC_ARMY.getMCount(dtarmy, sw.TopORGNO, "");
                if (string.IsNullOrEmpty(mm.ARMYCount))
                {
                    mm.ARMYCount = "0";
                }
                if (string.IsNullOrEmpty(mm.MEMBERCount))
                {
                    mm.MEMBERCount = "0";
                }
                var typeResult1 = new List<DC_ARMY_TypeCountModel>();
                for (int j = 0; j < dt26.Rows.Count; j++)
                {
                    DC_ARMY_TypeCountModel m1 = new DC_ARMY_TypeCountModel();
                    m1.DICTVALUE = dt26.Rows[j]["DICTVALUE"].ToString();
                    m1.DICTNAME = dt26.Rows[j]["DICTNAME"].ToString();
                    m1.ARMYTYPECount = BaseDT.DC_ARMY.getCountarmyByOrgNo(dtarmy, sw.TopORGNO, m1.DICTVALUE);
                    m1.MEMBERTYPECount = BaseDT.DC_ARMY.getMCount(dtarmy, sw.TopORGNO, m1.DICTVALUE);
                    if (string.IsNullOrEmpty(m1.ARMYTYPECount))
                    {
                        m1.ARMYTYPECount = "0";
                    }
                    if (string.IsNullOrEmpty(m1.MEMBERTYPECount))
                    {
                        m1.MEMBERTYPECount = "0";
                    }
                    typeResult1.Add(m1);
                }
                mm.TypeCountModel = typeResult1;
                result.Add(mm);
            }

            //循环单位
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DC_ARMYCount_Model m = new DC_ARMYCount_Model();
                m.ORGNo = dtOrg.Rows[i]["ORGNO"].ToString();
                m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                if (PublicCls.OrgIsZhen(m.ORGNo) == false && m.ORGNo == sw.TopORGNO)
                {
                    m.ARMYCount = BaseDT.DC_ARMY.getCountXSByOrgNo(dtarmy, m.ORGNo, "");//统计队伍总数
                    if (string.IsNullOrEmpty(m.ARMYCount))
                    {
                        m.ARMYCount = "0";
                    }
                    m.MEMBERCount = BaseDT.DC_ARMY.getMXSCount(dtarmy, m.ORGNo, "");
                    if (string.IsNullOrEmpty(m.MEMBERCount))
                    {
                        m.MEMBERCount = "0";
                    }
                    var typeResult = new List<DC_ARMY_TypeCountModel>();
                    //循环类别
                    for (int j = 0; j < dt26.Rows.Count; j++)
                    {
                        DC_ARMY_TypeCountModel m1 = new DC_ARMY_TypeCountModel();
                        m1.DICTVALUE = dt26.Rows[j]["DICTVALUE"].ToString();
                        m1.DICTNAME = dt26.Rows[j]["DICTNAME"].ToString();
                        m1.ARMYTYPECount = BaseDT.DC_ARMY.getCountXSByOrgNo(dtarmy, m.ORGNo, m1.DICTVALUE);
                        m1.MEMBERTYPECount = BaseDT.DC_ARMY.getMXSCount(dtarmy, m.ORGNo, m1.DICTVALUE);
                        if (string.IsNullOrEmpty(m1.ARMYTYPECount))
                        {
                            m1.ARMYTYPECount = "0";
                        }
                        if (string.IsNullOrEmpty(m1.MEMBERTYPECount))
                        {
                            m1.MEMBERTYPECount = "0";
                        }
                        typeResult.Add(m1);
                    }
                    m.TypeCountModel = typeResult;
                    result.Add(m);
                }
                else
                {
                    m.ARMYCount = BaseDT.DC_ARMY.getCountarmyByOrgNo(dtarmy, m.ORGNo, "");//统计队伍总数
                    if (string.IsNullOrEmpty(m.ARMYCount))
                    {
                        m.ARMYCount = "0";
                    }
                    m.MEMBERCount = BaseDT.DC_ARMY.getMCount(dtarmy, m.ORGNo, "");
                    if (string.IsNullOrEmpty(m.MEMBERCount))
                    {
                        m.MEMBERCount = "0";
                    }
                    var typeResult = new List<DC_ARMY_TypeCountModel>();
                    //循环类别
                    for (int j = 0; j < dt26.Rows.Count; j++)
                    {
                        DC_ARMY_TypeCountModel m1 = new DC_ARMY_TypeCountModel();
                        m1.DICTVALUE = dt26.Rows[j]["DICTVALUE"].ToString();
                        m1.DICTNAME = dt26.Rows[j]["DICTNAME"].ToString();
                        m1.ARMYTYPECount = BaseDT.DC_ARMY.getCountarmyByOrgNo(dtarmy, m.ORGNo, m1.DICTVALUE);
                        m1.MEMBERTYPECount = BaseDT.DC_ARMY.getMCount(dtarmy, m.ORGNo, m1.DICTVALUE);
                        if (string.IsNullOrEmpty(m1.ARMYTYPECount))
                        {
                            m1.ARMYTYPECount = "0";
                        }
                        if (string.IsNullOrEmpty(m1.MEMBERTYPECount))
                        {
                            m1.MEMBERTYPECount = "0";
                        }
                        typeResult.Add(m1);
                    }
                    m.TypeCountModel = typeResult;
                    result.Add(m);
                }

            }
            dtarmy.Clear();
            dtarmy.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }

        #endregion
        #endregion

        #region 数据中心资源统计
        #region 资源统计
        /// <summary>
        /// 资源类型统计
        /// </summary>
        /// <param name="dt">资源DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <param name="value">资源的各个类型值</param>
        /// <param name="type">确定各个资源类型</param>
        /// <returns></returns>
        private static string getCountByDCRESOURCETYPE(DataTable dt, string orgNo, string value, string type)
        {
            string str = "";
            if (type == "28")//资源类型统计
            {
                if (orgNo.Substring(4, 5) == "00000")//统计市
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + orgNo.Substring(0, 4) + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + orgNo.Substring(0, 4) + "' and RESOURCETYPE=" + value + "").ToString();
                }
                else if (orgNo.Substring(6, 3) == "000")//县
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + orgNo.Substring(0, 6) + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + orgNo.Substring(0, 6) + "' and RESOURCETYPE=" + value + "").ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "' and RESOURCETYPE=" + value + "").ToString();
                }
            }
            if (type == "27")//资源林龄类别
            {
                if (orgNo.Substring(4, 5) == "00000")//统计市
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + orgNo.Substring(0, 4) + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + orgNo.Substring(0, 4) + "' and AGETYPE=" + value + "").ToString();
                }
                else if (orgNo.Substring(6, 3) == "000")//县
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + orgNo.Substring(0, 6) + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + orgNo.Substring(0, 6) + "' and AGETYPE=" + value + "").ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "' and AGETYPE=" + value + "").ToString();
                }
            }
            if (type == "29")//起源类型类别
            {
                if (orgNo.Substring(4, 5) == "00000")//统计市
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + orgNo.Substring(0, 4) + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + orgNo.Substring(0, 4) + "' and ORIGINTYPE=" + value + "").ToString();
                }
                else if (orgNo.Substring(6, 3) == "000")//县
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + orgNo.Substring(0, 6) + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + orgNo.Substring(0, 6) + "' and ORIGINTYPE=" + value + "").ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "' and ORIGINTYPE=" + value + "").ToString();
                }
            }
            if (type == "30")//可燃类型类别
            {
                if (orgNo.Substring(4, 5) == "00000")//统计市
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + orgNo.Substring(0, 4) + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + orgNo.Substring(0, 4) + "' and BURNTYPE=" + value + "").ToString();
                }
                else if (orgNo.Substring(6, 3) == "000")//县
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + orgNo.Substring(0, 6) + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + orgNo.Substring(0, 6) + "' and BURNTYPE=" + value + "").ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "' and BURNTYPE=" + value + "").ToString();
                }
            }
            if (type == "31")//可燃类型类别
            {
                if (orgNo.Substring(4, 5) == "00000")//统计市
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + orgNo.Substring(0, 4) + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + orgNo.Substring(0, 4) + "' and TREETYPE=" + value + "").ToString();
                }
                else if (orgNo.Substring(6, 3) == "000")//县
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + orgNo.Substring(0, 6) + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + orgNo.Substring(0, 6) + "' and TREETYPE=" + value + "").ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(type))
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        str = dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "' and TREETYPE=" + value + "").ToString();
                }
            }
            return str;
        }
        #endregion

        #region 资源各个类型统计
        /// <summary>
        /// 资源各个类型统计
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="RESOURCETYPECountModel">数据中心资源模型</param>
        /// <param name="AGETYPECountModel">林龄类型</param>
        /// <param name="ORIGINTYPECountModel">起源类型</param>
        /// <param name="BURNTYPECountModel">可燃类型</param>
        /// <param name="TREETYPECountModel">林木类型</param>
        /// <returns></returns>
        public static IEnumerable<DC_RESOURCECount_Model> getRESOURCEModelCount(DC_RESOURCE_NEW_SW sw,
            out IEnumerable<RESOURCETYPECountModel> RESOURCETYPECountModel,
            out IEnumerable<AGETYPECountModel> AGETYPECountModel,
            out IEnumerable<ORIGINTYPECountModel> ORIGINTYPECountModel,
            out IEnumerable<BURNTYPECountModel> BURNTYPECountModel,
            out IEnumerable<TREETYPECountModel> TREETYPECountModel)
        {

            var result = new List<DC_RESOURCECount_Model>();

            //获取单位
            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.TopEchartORGNO = sw.TopORGNO;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            DataTable dt27 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "27" });//林龄类型
            DataTable dt28 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "28" });//数据中心资源类型
            DataTable dt29 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "29" });//数据中心资源起源类型
            DataTable dt30 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "30" });//数据中心资源可燃类型
            DataTable dt31 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "31" });//数据中心资源林木类型

            DataTable dtRESOURCE = BaseDT.DC_RESOURCE_NEW.getDT(sw);
            //out 类别
            var RESOURCETYPE = new List<RESOURCETYPECountModel>();
            for (int j = 0; j < dt28.Rows.Count; j++)
            {
                RESOURCETYPECountModel m = new RESOURCETYPECountModel();
                m.RESOURCETYPEVALUE = dt28.Rows[j]["DICTVALUE"].ToString();
                m.RESOURCETYPENAME = dt28.Rows[j]["DICTNAME"].ToString();
                RESOURCETYPE.Add(m);
            }
            RESOURCETYPECountModel = RESOURCETYPE;
            var AGETYPE = new List<AGETYPECountModel>();
            for (int j = 0; j < dt27.Rows.Count; j++)
            {
                AGETYPECountModel m = new AGETYPECountModel();
                m.AGETYPEVALUE = dt27.Rows[j]["DICTVALUE"].ToString();
                m.AGETYPENAME = dt27.Rows[j]["DICTNAME"].ToString();
                AGETYPE.Add(m);
            }
            AGETYPECountModel = AGETYPE;
            var ORIGINTYPE = new List<ORIGINTYPECountModel>();
            for (int j = 0; j < dt29.Rows.Count; j++)
            {
                ORIGINTYPECountModel m = new ORIGINTYPECountModel();
                m.ORIGINTYPEVALUE = dt29.Rows[j]["DICTVALUE"].ToString();
                m.ORIGINTYPENAME = dt29.Rows[j]["DICTNAME"].ToString();
                ORIGINTYPE.Add(m);
            }
            ORIGINTYPECountModel = ORIGINTYPE;
            var BURNTYPE = new List<BURNTYPECountModel>();
            for (int j = 0; j < dt30.Rows.Count; j++)
            {
                BURNTYPECountModel m = new BURNTYPECountModel();
                m.BURNTYPEVALUE = dt30.Rows[j]["DICTVALUE"].ToString();
                m.BURNTYPENAME = dt30.Rows[j]["DICTNAME"].ToString();
                BURNTYPE.Add(m);
            }
            BURNTYPECountModel = BURNTYPE;
            var TREETYPE = new List<TREETYPECountModel>();
            for (int j = 0; j < dt31.Rows.Count; j++)
            {
                TREETYPECountModel m = new TREETYPECountModel();
                m.TREETYPEVALUE = dt31.Rows[j]["DICTVALUE"].ToString();
                m.TREETYPENAME = dt31.Rows[j]["DICTNAME"].ToString();
                TREETYPE.Add(m);
            }
            TREETYPECountModel = TREETYPE;
            if (PublicCls.OrgIsZhen(sw.TopORGNO) == false)
            {
                DC_RESOURCECount_Model mm = new DC_RESOURCECount_Model();
                mm.ORGName = BaseDT.T_SYS_ORG.getName(sw.TopORGNO) + "合计";
                mm.ORGNo = sw.TopORGNO;
                mm.RESOURCETYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, sw.TopORGNO, "", "1");//统计资源类型总数
                mm.AGETYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, sw.TopORGNO, "", "2");//统计林龄类别
                mm.ORIGINTYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, sw.TopORGNO, "", "3");//统计起源类型
                mm.BURNTYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, sw.TopORGNO, "", "4");//统计可燃类型
                mm.TREETYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, sw.TopORGNO, "", "5");//统计林木类型
                mm.AREACount = BaseDT.DC_RESOURCE_NEW.getAREACount(dtRESOURCE, sw.TopORGNO, "", "1");//面积统计
                if (string.IsNullOrEmpty(mm.AREACount))
                {
                    mm.AREACount = "0";
                }
                var RESOURCETYPEResult1 = new List<RESOURCETYPECountModel>();
                //循环资源类型
                for (int j = 0; j < dt28.Rows.Count; j++)
                {
                    RESOURCETYPECountModel m1 = new RESOURCETYPECountModel();
                    m1.RESOURCETYPEVALUE = dt28.Rows[j]["DICTVALUE"].ToString();
                    m1.RESOURCETYPENAME = dt28.Rows[j]["DICTNAME"].ToString();
                    m1.DICTRESOURCETYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, sw.TopORGNO, m1.RESOURCETYPEVALUE, "1");
                    m1.AREATYPE1Count = BaseDT.DC_RESOURCE_NEW.getAREACount(dtRESOURCE, sw.TopORGNO, m1.RESOURCETYPEVALUE, "1");
                    if (string.IsNullOrEmpty(m1.AREATYPE1Count))
                    {
                        m1.AREATYPE1Count = "0";
                    }
                    RESOURCETYPEResult1.Add(m1);
                }
                mm.RESOURCETYPECountModel = RESOURCETYPEResult1;
                var AGETYPEResult1 = new List<AGETYPECountModel>();
                //循环林龄类别类型
                for (int j = 0; j < dt27.Rows.Count; j++)
                {
                    AGETYPECountModel m1 = new AGETYPECountModel();
                    m1.AGETYPEVALUE = dt27.Rows[j]["DICTVALUE"].ToString();
                    m1.AGETYPENAME = dt27.Rows[j]["DICTNAME"].ToString();
                    m1.DICTAGETYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, sw.TopORGNO, m1.AGETYPEVALUE, "2");
                    m1.AREATYPE2Count = BaseDT.DC_RESOURCE_NEW.getAREACount(dtRESOURCE, sw.TopORGNO, m1.AGETYPEVALUE, "2");
                    if (string.IsNullOrEmpty(m1.AREATYPE2Count))
                    {
                        m1.AREATYPE2Count = "0";
                    }
                    AGETYPEResult1.Add(m1);
                }
                mm.AGETYPECountModel = AGETYPEResult1;
                var ORIGINTYPEResult1 = new List<ORIGINTYPECountModel>();
                //循环起源类型
                for (int j = 0; j < dt29.Rows.Count; j++)
                {
                    ORIGINTYPECountModel m1 = new ORIGINTYPECountModel();
                    m1.ORIGINTYPEVALUE = dt29.Rows[j]["DICTVALUE"].ToString();
                    m1.ORIGINTYPENAME = dt29.Rows[j]["DICTNAME"].ToString();
                    m1.DICORIGINTYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, sw.TopORGNO, m1.ORIGINTYPEVALUE, "3");
                    m1.AREATYPE3Count = BaseDT.DC_RESOURCE_NEW.getAREACount(dtRESOURCE, sw.TopORGNO, m1.ORIGINTYPEVALUE, "3");
                    if (string.IsNullOrEmpty(m1.AREATYPE3Count))
                    {
                        m1.AREATYPE3Count = "0";
                    }
                    ORIGINTYPEResult1.Add(m1);
                }
                mm.ORIGINTYPECountModel = ORIGINTYPEResult1;
                var BURNTYPEResult1 = new List<BURNTYPECountModel>();
                //循环可燃类型
                for (int j = 0; j < dt30.Rows.Count; j++)
                {
                    BURNTYPECountModel m1 = new BURNTYPECountModel();
                    m1.BURNTYPEVALUE = dt30.Rows[j]["DICTVALUE"].ToString();
                    m1.BURNTYPENAME = dt30.Rows[j]["DICTNAME"].ToString();
                    m1.DICTBURNTYPETYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, sw.TopORGNO, m1.BURNTYPEVALUE, "4");
                    m1.AREATYPE4Count = BaseDT.DC_RESOURCE_NEW.getAREACount(dtRESOURCE, sw.TopORGNO, m1.BURNTYPEVALUE, "4");
                    if (string.IsNullOrEmpty(m1.AREATYPE4Count))
                    {
                        m1.AREATYPE4Count = "0";
                    }
                    BURNTYPEResult1.Add(m1);
                }
                mm.BURNTYPECountModel = BURNTYPEResult1;
                var TREETYPEResult1 = new List<TREETYPECountModel>();
                //循环林木类型
                for (int j = 0; j < dt31.Rows.Count; j++)
                {
                    TREETYPECountModel m1 = new TREETYPECountModel();
                    m1.TREETYPEVALUE = dt31.Rows[j]["DICTVALUE"].ToString();
                    m1.TREETYPENAME = dt31.Rows[j]["DICTNAME"].ToString();
                    m1.DICTTREETYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, sw.TopORGNO, m1.TREETYPEVALUE, "5");
                    m1.AREATYPE5Count = BaseDT.DC_RESOURCE_NEW.getAREACount(dtRESOURCE, sw.TopORGNO, m1.TREETYPEVALUE, "5");
                    if (string.IsNullOrEmpty(m1.AREATYPE5Count))
                    {
                        m1.AREATYPE5Count = "0";
                    }
                    TREETYPEResult1.Add(m1);
                }
                mm.TREETYPECountModel = TREETYPEResult1;
                result.Add(mm);
            }
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DC_RESOURCECount_Model m = new DC_RESOURCECount_Model();
                m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                m.ORGNo = dtOrg.Rows[i]["ORGNO"].ToString();
                if (PublicCls.OrgIsZhen(m.ORGNo) == false && m.ORGNo == sw.TopORGNO)
                {
                    m.RESOURCETYPECount = BaseDT.DC_RESOURCE_NEW.getCountXSByOrgNo(dtRESOURCE, m.ORGNo, "", "1");//统计资源类型总数
                    m.AGETYPECount = BaseDT.DC_RESOURCE_NEW.getCountXSByOrgNo(dtRESOURCE, m.ORGNo, "", "2");//统计林龄类别
                    m.ORIGINTYPECount = BaseDT.DC_RESOURCE_NEW.getCountXSByOrgNo(dtRESOURCE, m.ORGNo, "", "3");//统计起源类型
                    m.BURNTYPECount = BaseDT.DC_RESOURCE_NEW.getCountXSByOrgNo(dtRESOURCE, m.ORGNo, "", "4");//统计可燃类型
                    m.TREETYPECount = BaseDT.DC_RESOURCE_NEW.getCountXSByOrgNo(dtRESOURCE, m.ORGNo, "", "5");//统计林木类型
                    m.AREACount = BaseDT.DC_RESOURCE_NEW.getAREACountXSByOrgNo(dtRESOURCE, m.ORGNo, "", "1");//面积统计
                    if (string.IsNullOrEmpty(m.AREACount))
                    {
                        m.AREACount = "0";
                    }

                    var RESOURCETYPEResult = new List<RESOURCETYPECountModel>();
                    //循环资源类型
                    for (int j = 0; j < dt28.Rows.Count; j++)
                    {
                        RESOURCETYPECountModel m1 = new RESOURCETYPECountModel();
                        m1.RESOURCETYPEVALUE = dt28.Rows[j]["DICTVALUE"].ToString();
                        m1.RESOURCETYPENAME = dt28.Rows[j]["DICTNAME"].ToString();
                        m1.DICTRESOURCETYPECount = BaseDT.DC_RESOURCE_NEW.getCountXSByOrgNo(dtRESOURCE, m.ORGNo, m1.RESOURCETYPEVALUE, "1");
                        m1.AREATYPE1Count = BaseDT.DC_RESOURCE_NEW.getAREACountXSByOrgNo(dtRESOURCE, m.ORGNo, m1.RESOURCETYPEVALUE, "1");//资源类型面积统计
                        if (string.IsNullOrEmpty(m1.AREATYPE1Count))
                        {
                            m1.AREATYPE1Count = "0";
                        }
                        RESOURCETYPEResult.Add(m1);
                    }
                    m.RESOURCETYPECountModel = RESOURCETYPEResult;
                    var AGETYPEResult = new List<AGETYPECountModel>();
                    //循环林龄类别类型
                    for (int j = 0; j < dt27.Rows.Count; j++)
                    {
                        AGETYPECountModel m1 = new AGETYPECountModel();
                        m1.AGETYPEVALUE = dt27.Rows[j]["DICTVALUE"].ToString();
                        m1.AGETYPENAME = dt27.Rows[j]["DICTNAME"].ToString();
                        m1.DICTAGETYPECount = BaseDT.DC_RESOURCE_NEW.getCountXSByOrgNo(dtRESOURCE, m.ORGNo, m1.AGETYPEVALUE, "2");
                        m1.AREATYPE2Count = BaseDT.DC_RESOURCE_NEW.getAREACountXSByOrgNo(dtRESOURCE, m.ORGNo, m1.AGETYPEVALUE, "2");//林龄类别面积统计
                        if (string.IsNullOrEmpty(m1.AREATYPE2Count))
                        {
                            m1.AREATYPE2Count = "0";
                        }
                        AGETYPEResult.Add(m1);
                    }
                    m.AGETYPECountModel = AGETYPEResult;
                    var ORIGINTYPEResult = new List<ORIGINTYPECountModel>();
                    //循环起源类型
                    for (int j = 0; j < dt29.Rows.Count; j++)
                    {
                        ORIGINTYPECountModel m1 = new ORIGINTYPECountModel();
                        m1.ORIGINTYPEVALUE = dt29.Rows[j]["DICTVALUE"].ToString();
                        m1.ORIGINTYPENAME = dt29.Rows[j]["DICTNAME"].ToString();
                        m1.DICORIGINTYPECount = BaseDT.DC_RESOURCE_NEW.getCountXSByOrgNo(dtRESOURCE, m.ORGNo, m1.ORIGINTYPEVALUE, "3");
                        m1.AREATYPE3Count = BaseDT.DC_RESOURCE_NEW.getAREACountXSByOrgNo(dtRESOURCE, m.ORGNo, m1.ORIGINTYPEVALUE, "3");//起源类型面积统计
                        if (string.IsNullOrEmpty(m1.AREATYPE3Count))
                        {
                            m1.AREATYPE3Count = "0";
                        }
                        ORIGINTYPEResult.Add(m1);
                    }
                    m.ORIGINTYPECountModel = ORIGINTYPEResult;
                    var BURNTYPEResult = new List<BURNTYPECountModel>();
                    //循环可燃类型
                    for (int j = 0; j < dt30.Rows.Count; j++)
                    {
                        BURNTYPECountModel m1 = new BURNTYPECountModel();
                        m1.BURNTYPEVALUE = dt30.Rows[j]["DICTVALUE"].ToString();
                        m1.BURNTYPENAME = dt30.Rows[j]["DICTNAME"].ToString();
                        m1.DICTBURNTYPETYPECount = BaseDT.DC_RESOURCE_NEW.getCountXSByOrgNo(dtRESOURCE, m.ORGNo, m1.BURNTYPEVALUE, "4");
                        m1.AREATYPE4Count = BaseDT.DC_RESOURCE_NEW.getAREACountXSByOrgNo(dtRESOURCE, m.ORGNo, m1.BURNTYPEVALUE, "4");//可燃类型面积统计
                        if (string.IsNullOrEmpty(m1.AREATYPE4Count))
                        {
                            m1.AREATYPE4Count = "0";
                        }
                        BURNTYPEResult.Add(m1);
                    }
                    m.BURNTYPECountModel = BURNTYPEResult;
                    var TREETYPEResult = new List<TREETYPECountModel>();
                    //循环林木类型
                    for (int j = 0; j < dt31.Rows.Count; j++)
                    {
                        TREETYPECountModel m1 = new TREETYPECountModel();
                        m1.TREETYPEVALUE = dt31.Rows[j]["DICTVALUE"].ToString();
                        m1.TREETYPENAME = dt31.Rows[j]["DICTNAME"].ToString();
                        m1.DICTTREETYPECount = BaseDT.DC_RESOURCE_NEW.getCountXSByOrgNo(dtRESOURCE, m.ORGNo, m1.TREETYPEVALUE, "5");
                        m1.AREATYPE5Count = BaseDT.DC_RESOURCE_NEW.getAREACountXSByOrgNo(dtRESOURCE, m.ORGNo, m1.TREETYPEVALUE, "5");//可燃类型面积统计
                        if (string.IsNullOrEmpty(m1.AREATYPE5Count))
                        {
                            m1.AREATYPE5Count = "0";
                        }
                        TREETYPEResult.Add(m1);
                    }
                    m.TREETYPECountModel = TREETYPEResult;
                    result.Add(m);
                }
                else
                {
                    m.RESOURCETYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, m.ORGNo, "", "1");//统计资源类型总数
                    m.AGETYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, m.ORGNo, "", "2");//统计林龄类别
                    m.ORIGINTYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, m.ORGNo, "", "3");//统计起源类型
                    m.BURNTYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, m.ORGNo, "", "4");//统计可燃类型
                    m.TREETYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, m.ORGNo, "", "5");//统计林木类型
                    m.AREACount = BaseDT.DC_RESOURCE_NEW.getAREACount(dtRESOURCE, m.ORGNo, "", "1");//面积统计
                    if (string.IsNullOrEmpty(m.AREACount))
                    {
                        m.AREACount = "0";
                    }
                    var RESOURCETYPEResult = new List<RESOURCETYPECountModel>();
                    //循环资源类型
                    for (int j = 0; j < dt28.Rows.Count; j++)
                    {
                        RESOURCETYPECountModel m1 = new RESOURCETYPECountModel();
                        m1.RESOURCETYPEVALUE = dt28.Rows[j]["DICTVALUE"].ToString();
                        m1.RESOURCETYPENAME = dt28.Rows[j]["DICTNAME"].ToString();
                        m1.DICTRESOURCETYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, m.ORGNo, m1.RESOURCETYPEVALUE, "1");
                        m1.AREATYPE1Count = BaseDT.DC_RESOURCE_NEW.getAREACount(dtRESOURCE, m.ORGNo, m1.RESOURCETYPEVALUE, "1");//资源类型面积统计
                        if (string.IsNullOrEmpty(m1.AREATYPE1Count))
                        {
                            m1.AREATYPE1Count = "0";
                        }
                        RESOURCETYPEResult.Add(m1);
                    }
                    m.RESOURCETYPECountModel = RESOURCETYPEResult;
                    var AGETYPEResult = new List<AGETYPECountModel>();
                    //循环林龄类别类型
                    for (int j = 0; j < dt27.Rows.Count; j++)
                    {
                        AGETYPECountModel m1 = new AGETYPECountModel();
                        m1.AGETYPEVALUE = dt27.Rows[j]["DICTVALUE"].ToString();
                        m1.AGETYPENAME = dt27.Rows[j]["DICTNAME"].ToString();
                        m1.DICTAGETYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, m.ORGNo, m1.AGETYPEVALUE, "2");
                        m1.AREATYPE2Count = BaseDT.DC_RESOURCE_NEW.getAREACount(dtRESOURCE, m.ORGNo, m1.AGETYPEVALUE, "2");//林龄类别面积统计
                        if (string.IsNullOrEmpty(m1.AREATYPE2Count))
                        {
                            m1.AREATYPE2Count = "0";
                        }
                        AGETYPEResult.Add(m1);
                    }
                    m.AGETYPECountModel = AGETYPEResult;
                    var ORIGINTYPEResult = new List<ORIGINTYPECountModel>();
                    //循环起源类型
                    for (int j = 0; j < dt29.Rows.Count; j++)
                    {
                        ORIGINTYPECountModel m1 = new ORIGINTYPECountModel();
                        m1.ORIGINTYPEVALUE = dt29.Rows[j]["DICTVALUE"].ToString();
                        m1.ORIGINTYPENAME = dt29.Rows[j]["DICTNAME"].ToString();
                        m1.DICORIGINTYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, m.ORGNo, m1.ORIGINTYPEVALUE, "3");
                        m1.AREATYPE3Count = BaseDT.DC_RESOURCE_NEW.getAREACount(dtRESOURCE, m.ORGNo, m1.ORIGINTYPEVALUE, "3");//起源类型面积统计
                        if (string.IsNullOrEmpty(m1.AREATYPE3Count))
                        {
                            m1.AREATYPE3Count = "0";
                        }
                        ORIGINTYPEResult.Add(m1);
                    }
                    m.ORIGINTYPECountModel = ORIGINTYPEResult;
                    var BURNTYPEResult = new List<BURNTYPECountModel>();
                    //循环可燃类型
                    for (int j = 0; j < dt30.Rows.Count; j++)
                    {
                        BURNTYPECountModel m1 = new BURNTYPECountModel();
                        m1.BURNTYPEVALUE = dt30.Rows[j]["DICTVALUE"].ToString();
                        m1.BURNTYPENAME = dt30.Rows[j]["DICTNAME"].ToString();
                        m1.DICTBURNTYPETYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, m.ORGNo, m1.BURNTYPEVALUE, "4");
                        m1.AREATYPE4Count = BaseDT.DC_RESOURCE_NEW.getAREACount(dtRESOURCE, m.ORGNo, m1.BURNTYPEVALUE, "4");//可燃类型面积统计
                        if (string.IsNullOrEmpty(m1.AREATYPE4Count))
                        {
                            m1.AREATYPE4Count = "0";
                        }
                        BURNTYPEResult.Add(m1);
                    }
                    m.BURNTYPECountModel = BURNTYPEResult;
                    var TREETYPEResult = new List<TREETYPECountModel>();
                    //循环林木类型
                    for (int j = 0; j < dt31.Rows.Count; j++)
                    {
                        TREETYPECountModel m1 = new TREETYPECountModel();
                        m1.TREETYPEVALUE = dt31.Rows[j]["DICTVALUE"].ToString();
                        m1.TREETYPENAME = dt31.Rows[j]["DICTNAME"].ToString();
                        m1.DICTTREETYPECount = BaseDT.DC_RESOURCE_NEW.getCountarmyByOrgNo(dtRESOURCE, m.ORGNo, m1.TREETYPEVALUE, "5");
                        m1.AREATYPE5Count = BaseDT.DC_RESOURCE_NEW.getAREACount(dtRESOURCE, m.ORGNo, m1.TREETYPEVALUE, "5");//可燃类型面积统计
                        if (string.IsNullOrEmpty(m1.AREATYPE5Count))
                        {
                            m1.AREATYPE5Count = "0";
                        }
                        TREETYPEResult.Add(m1);
                    }
                    m.TREETYPECountModel = TREETYPEResult;
                    result.Add(m);
                }
            }
            return result;
        }
        #endregion

        #region 取得每个资源类型的种类的数量
        /// <summary>
        ///取得每个资源类型的种类的数量
        /// </summary>
        /// <param name="DICTTYPEID">字典值</param>
        /// <returns></returns>
        public static string getCount(string DICTTYPEID)
        {
            var Count = "";
            if (string.IsNullOrEmpty(DICTTYPEID))
                return "";
            else
                Count = BaseDT.DC_RESOURCE_NEW.getCount(new T_SYS_DICTSW { DICTTYPEID = DICTTYPEID });
            return Count;
        }
        #endregion
        #endregion

        #region 数据中心装备统计
        #region 装备各个类型统计
        /// <summary>
        /// 装备各个类型统计
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="EQUIPTYPECountModel">装备类型</param>
        /// <param name="USESTATECountModel">使用现状</param> 
        /// <returns></returns>
        public static IEnumerable<DC_EQUIPCount_Model> getEQUIPModelCount(DC_EQUIP_NEW_SW sw,
            out IEnumerable<EQUIPTYPECountModel> EQUIPTYPECountModel,
            out IEnumerable<USESTATECountModel> USESTATECountModel)
        {

            var result = new List<DC_EQUIPCount_Model>();

            //获取单位
            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.TopEchartORGNO = sw.TopORGNO;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            DataTable dt32 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "32" });//装备类型
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用类别
            DataTable dtEQUIP = BaseDT.DC_EQUIP_NEW.getDT(sw);
            //out 类别
            var EQUIPTYPE = new List<EQUIPTYPECountModel>();
            for (int j = 0; j < dt32.Rows.Count; j++)
            {
                EQUIPTYPECountModel m = new EQUIPTYPECountModel();
                m.EQUIPTYPEVALUE = dt32.Rows[j]["DICTVALUE"].ToString();
                m.EQUIPTYPENAME = dt32.Rows[j]["DICTNAME"].ToString();
                EQUIPTYPE.Add(m);
            }
            EQUIPTYPECountModel = EQUIPTYPE;
            var USESTATE = new List<USESTATECountModel>();
            for (int j = 0; j < dt36.Rows.Count; j++)
            {
                USESTATECountModel m = new USESTATECountModel();
                m.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                m.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                USESTATE.Add(m);
            }
            USESTATECountModel = USESTATE;
            if (PublicCls.OrgIsZhen(sw.TopORGNO) == false)
            {
                DC_EQUIPCount_Model mm = new DC_EQUIPCount_Model();
                mm.ORGName = BaseDT.T_SYS_ORG.getName(sw.TopORGNO) + "合计";
                mm.BYORGNO = sw.TopORGNO;

                mm.EQUIPTYPECount = BaseDT.DC_EQUIP_NEW.getCountEquipByOrgNo(dtEQUIP, sw.TopORGNO, "", "1");//统计装备类型总数
                mm.USESTATECount = BaseDT.DC_EQUIP_NEW.getCountEquipByOrgNo(dtEQUIP, sw.TopORGNO, "", "2");//统计使用类别
                var EQUIPTYPEResult1 = new List<EQUIPTYPECountModel>();
                //循环装备类型
                for (int j = 0; j < dt32.Rows.Count; j++)
                {
                    EQUIPTYPECountModel m1 = new EQUIPTYPECountModel();
                    m1.EQUIPTYPEVALUE = dt32.Rows[j]["DICTVALUE"].ToString();
                    m1.EQUIPTYPENAME = dt32.Rows[j]["DICTNAME"].ToString();
                    m1.DICTEQUIPTYPECount = BaseDT.DC_EQUIP_NEW.getCountEquipByOrgNo(dtEQUIP, sw.TopORGNO, m1.EQUIPTYPEVALUE, "1");
                    EQUIPTYPEResult1.Add(m1);
                }
                mm.EQUIPTYPECountModel = EQUIPTYPEResult1;
                var USESTATEResult1 = new List<USESTATECountModel>();
                //循环使用类型
                for (int j = 0; j < dt36.Rows.Count; j++)
                {
                    USESTATECountModel m1 = new USESTATECountModel();
                    m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                    m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                    m1.USESTATECount = BaseDT.DC_EQUIP_NEW.getCountEquipByOrgNo(dtEQUIP, sw.TopORGNO, m1.USESTATEVALUE, "2");
                    USESTATEResult1.Add(m1);
                }
                mm.USESTATECountModel = USESTATEResult1;
                result.Add(mm);
            }
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DC_EQUIPCount_Model m = new DC_EQUIPCount_Model();
                m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                m.BYORGNO = dtOrg.Rows[i]["ORGNO"].ToString();
                if (PublicCls.OrgIsZhen(m.BYORGNO) == false && m.BYORGNO == sw.TopORGNO)
                {
                    m.EQUIPTYPECount = BaseDT.DC_EQUIP_NEW.getCountXSByOrgNo(dtEQUIP, m.BYORGNO, "", "1");//统计装备类型总数
                    m.USESTATECount = BaseDT.DC_EQUIP_NEW.getCountXSByOrgNo(dtEQUIP, m.BYORGNO, "", "2");//统计使用类别
                    var EQUIPTYPEResult = new List<EQUIPTYPECountModel>();
                    //循环装备类型
                    for (int j = 0; j < dt32.Rows.Count; j++)
                    {
                        EQUIPTYPECountModel m1 = new EQUIPTYPECountModel();
                        m1.EQUIPTYPEVALUE = dt32.Rows[j]["DICTVALUE"].ToString();
                        m1.EQUIPTYPENAME = dt32.Rows[j]["DICTNAME"].ToString();
                        m1.DICTEQUIPTYPECount = BaseDT.DC_EQUIP_NEW.getCountXSByOrgNo(dtEQUIP, m.BYORGNO, m1.EQUIPTYPEVALUE, "1");
                        EQUIPTYPEResult.Add(m1);
                    }
                    m.EQUIPTYPECountModel = EQUIPTYPEResult;
                    var USESTATEResult = new List<USESTATECountModel>();
                    //循环使用类型
                    for (int j = 0; j < dt36.Rows.Count; j++)
                    {
                        USESTATECountModel m1 = new USESTATECountModel();
                        m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                        m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                        m1.USESTATECount = BaseDT.DC_EQUIP_NEW.getCountXSByOrgNo(dtEQUIP, m.BYORGNO, m1.USESTATEVALUE, "2");
                        USESTATEResult.Add(m1);
                    }
                    m.USESTATECountModel = USESTATEResult;
                    result.Add(m);
                }
                else
                {
                    m.EQUIPTYPECount = BaseDT.DC_EQUIP_NEW.getCountEquipByOrgNo(dtEQUIP, m.BYORGNO, "", "1");//统计装备类型总数
                    m.USESTATECount = BaseDT.DC_EQUIP_NEW.getCountEquipByOrgNo(dtEQUIP, m.BYORGNO, "", "2");//统计使用类别
                    var EQUIPTYPEResult = new List<EQUIPTYPECountModel>();
                    //循环装备类型
                    for (int j = 0; j < dt32.Rows.Count; j++)
                    {
                        EQUIPTYPECountModel m1 = new EQUIPTYPECountModel();
                        m1.EQUIPTYPEVALUE = dt32.Rows[j]["DICTVALUE"].ToString();
                        m1.EQUIPTYPENAME = dt32.Rows[j]["DICTNAME"].ToString();
                        m1.DICTEQUIPTYPECount = BaseDT.DC_EQUIP_NEW.getCountEquipByOrgNo(dtEQUIP, m.BYORGNO, m1.EQUIPTYPEVALUE, "1");
                        EQUIPTYPEResult.Add(m1);
                    }
                    m.EQUIPTYPECountModel = EQUIPTYPEResult;
                    var USESTATEResult = new List<USESTATECountModel>();
                    //循环使用类型
                    for (int j = 0; j < dt36.Rows.Count; j++)
                    {
                        USESTATECountModel m1 = new USESTATECountModel();
                        m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                        m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                        m1.USESTATECount = BaseDT.DC_EQUIP_NEW.getCountEquipByOrgNo(dtEQUIP, m.BYORGNO, m1.USESTATEVALUE, "2");
                        USESTATEResult.Add(m1);
                    }
                    m.USESTATECountModel = USESTATEResult;
                    result.Add(m);
                }
            }
            return result;
        }
        #endregion
        #endregion

        #region 数据中心车辆统计
        /// <summary>
        /// 数据中心车辆统计
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="CARTYPECountModel"></param>
        /// <returns></returns>
        public static IEnumerable<DC_CARCount_Model> getCARModelCount(DC_CAR_SW sw, out IEnumerable<CARTYPECountModel> CARTYPECountModel)
        {
            var result = new List<DC_CARCount_Model>();

            //获取单位
            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.TopEchartORGNO = sw.TopORGNO;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            DataTable dt33 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "33" });//车辆类型
            DataTable dtCAR = BaseDT.DC_CAR.getDT(sw);
            //out 类别
            var CARType = new List<CARTYPECountModel>();
            for (int i = 0; i < dt33.Rows.Count; i++)
            {
                CARTYPECountModel m = new CARTYPECountModel();
                m.CARTYPENAME = dt33.Rows[i]["DICTNAME"].ToString();
                m.CARTYPEVALUE = dt33.Rows[i]["DICTVALUE"].ToString();
                CARType.Add(m);
            }
            CARTYPECountModel = CARType;
            if (PublicCls.OrgIsZhen(sw.TopORGNO) == false)
            {
                DC_CARCount_Model mm = new DC_CARCount_Model();
                mm.ORGName = BaseDT.T_SYS_ORG.getName(sw.TopORGNO) + "合计";
                mm.BYORGNO = sw.TopORGNO;

                mm.CARTYPECount = BaseDT.DC_CAR.getCountCARByOrgNo(dtCAR, sw.TopORGNO, "");//统计车辆总数

                var typeResult1 = new List<CARTYPECountModel>();
                //循环车辆类型
                for (int j = 0; j < dt33.Rows.Count; j++)
                {
                    CARTYPECountModel m1 = new CARTYPECountModel();
                    m1.CARTYPEVALUE = dt33.Rows[j]["DICTVALUE"].ToString();
                    m1.CARTYPENAME = dt33.Rows[j]["DICTNAME"].ToString();
                    m1.CARTYPECount = BaseDT.DC_CAR.getCountCARByOrgNo(dtCAR, sw.TopORGNO, m1.CARTYPEVALUE);
                    typeResult1.Add(m1);
                }
                mm.CARTYPECountModel = typeResult1;
                result.Add(mm);
            }
            //循环单位
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DC_CARCount_Model m = new DC_CARCount_Model();
                m.BYORGNO = dtOrg.Rows[i]["ORGNO"].ToString();
                m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                if (PublicCls.OrgIsZhen(m.BYORGNO) == false && m.BYORGNO == sw.TopORGNO)
                {
                    m.CARTYPECount = BaseDT.DC_CAR.getCountXSByOrgNo(dtCAR, m.BYORGNO, "");//统计车辆总数
                    var typeResult = new List<CARTYPECountModel>();
                    //循环车辆类型
                    for (int j = 0; j < dt33.Rows.Count; j++)
                    {
                        CARTYPECountModel m1 = new CARTYPECountModel();
                        m1.CARTYPEVALUE = dt33.Rows[j]["DICTVALUE"].ToString();
                        m1.CARTYPENAME = dt33.Rows[j]["DICTNAME"].ToString();
                        m1.CARTYPECount = BaseDT.DC_CAR.getCountXSByOrgNo(dtCAR, m.BYORGNO, m1.CARTYPEVALUE);
                        typeResult.Add(m1);
                    }
                    m.CARTYPECountModel = typeResult;
                    result.Add(m);
                }
                else
                {
                    m.CARTYPECount = BaseDT.DC_CAR.getCountCARByOrgNo(dtCAR, m.BYORGNO, "");//统计车辆总数
                    var typeResult = new List<CARTYPECountModel>();
                    //循环车辆类型
                    for (int j = 0; j < dt33.Rows.Count; j++)
                    {
                        CARTYPECountModel m1 = new CARTYPECountModel();
                        m1.CARTYPEVALUE = dt33.Rows[j]["DICTVALUE"].ToString();
                        m1.CARTYPENAME = dt33.Rows[j]["DICTNAME"].ToString();
                        m1.CARTYPECount = BaseDT.DC_CAR.getCountCARByOrgNo(dtCAR, m.BYORGNO, m1.CARTYPEVALUE);
                        typeResult.Add(m1);
                    }
                    m.CARTYPECountModel = typeResult;
                    result.Add(m);
                }
            }
            dtCAR.Clear();
            dtCAR.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }
        #endregion

        #region 数据中心档案统计
        /// <summary>
        /// 数据中心档案统计
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="HOTTYPECountModel">热点类别</param>
        /// <param name="FIRELEVELCountModel">火险等级</param>
        /// <returns></returns>
        public static IEnumerable<DCArchivalCount_Model> getArchivalModelCount(JC_FIRE_SW sw, out IEnumerable<HOTTYPECountModel> HOTTYPECountModel, out IEnumerable<FIRELEVELCountModel> FIRELEVELCountModel)
        {
            var result = new List<DCArchivalCount_Model>();

            //获取单位
            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.TopEchartORGNO = sw.TopORGNO;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            DataTable dt10 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "10" });//热点类别 
            DataTable dt22 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "22" });//火险等级
            DataTable dtArchival = BaseDT.JC_FIRE.GetArchivalDT(sw);
            //out 类别;
            var HOTTYPE = new List<HOTTYPECountModel>();
            for (int i = 0; i < dt10.Rows.Count; i++)
            {
                HOTTYPECountModel m = new HOTTYPECountModel();
                m.HOTTYPEvalue = dt10.Rows[i]["DICTVALUE"].ToString();
                m.HOTTYPEname = dt10.Rows[i]["DICTNAME"].ToString();
                HOTTYPE.Add(m);
            }
            HOTTYPECountModel = HOTTYPE;
            var FIRELEVEL = new List<FIRELEVELCountModel>();
            for (int i = 0; i < dt22.Rows.Count; i++)
            {
                FIRELEVELCountModel m = new FIRELEVELCountModel();
                m.FIRELEVELname = dt22.Rows[i]["DICTNAME"].ToString();
                m.FIRELEVELvalue = dt22.Rows[i]["DICTVALUE"].ToString();
                FIRELEVEL.Add(m);
            }
            FIRELEVELCountModel = FIRELEVEL;

            //循环单位
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DCArchivalCount_Model m = new DCArchivalCount_Model();
                m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                m.BYORGNO = dtOrg.Rows[i]["ORGNO"].ToString();
                m.FIRELEVELCount = BaseDT.JC_FIRE.getCountFIREByOrgNo(dtArchival, m.BYORGNO, "", "3");//统计火险等级
                m.HOTTYPECount = BaseDT.JC_FIRE.getCountFIREByOrgNo(dtArchival, m.BYORGNO, "", "2");//统计热点类别
                m.FIREFROMCount = BaseDT.JC_FIRE.getCountFIREByOrgNo(dtArchival, m.BYORGNO, "", "1");//统计火情来源
                m.FIREFROMCount1 = BaseDT.JC_FIRE.getCountFIREByOrgNo(dtArchival, m.BYORGNO, "1", "1");//红外相机统计
                m.FIREFROMCount2 = BaseDT.JC_FIRE.getCountFIREByOrgNo(dtArchival, m.BYORGNO, "2", "1");//电话报警统计
                m.FIREFROMCount3 = BaseDT.JC_FIRE.getCountFIREByOrgNo(dtArchival, m.BYORGNO, "3", "1");//卫星热点统计
                m.FIREFROMCount4 = BaseDT.JC_FIRE.getCountFIREByOrgNo(dtArchival, m.BYORGNO, "4", "1");//电子报警统计
                m.FIREFROMCount5 = BaseDT.JC_FIRE.getCountFIREByOrgNo(dtArchival, m.BYORGNO, "5", "1");//护林员火情统计
                m.FIREFROMCount6 = BaseDT.JC_FIRE.getCountFIREByOrgNo(dtArchival, m.BYORGNO, "6", "1");//飞机巡护统计
                m.FIREFROMCount7 = BaseDT.JC_FIRE.getCountFIREByOrgNo(dtArchival, m.BYORGNO, "50", "1");//历史补录

                var typeResult = new List<HOTTYPECountModel>();
                for (int j = 0; j < dt10.Rows.Count; j++)
                {
                    HOTTYPECountModel m1 = new HOTTYPECountModel();
                    m1.HOTTYPEvalue = dt10.Rows[j]["DICTVALUE"].ToString();
                    m1.DictHOTTYPECount = BaseDT.JC_FIRE.getCountFIREByOrgNo(dtArchival, m.BYORGNO, m1.HOTTYPEvalue, "2");
                    typeResult.Add(m1);
                }
                m.HOTTYPECountModel = typeResult;
                var typeResult1 = new List<FIRELEVELCountModel>();
                for (int k = 0; k < dt22.Rows.Count; k++)
                {
                    FIRELEVELCountModel m1 = new FIRELEVELCountModel();
                    m1.FIRELEVELvalue = dt22.Rows[k]["DICTVALUE"].ToString();
                    m1.DictFIRELEVELCount = BaseDT.JC_FIRE.getCountFIREByOrgNo(dtArchival, m.BYORGNO, m1.FIRELEVELvalue, "3");
                    typeResult1.Add(m1);
                }
                m.FIRELEVELCountModel = typeResult1;
                result.Add(m);
            }
            dtArchival.Clear();
            dtArchival.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }
        #endregion

        #region 设施统计

        #region 营房统计
        /// <summary>
        /// 营房统计
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="STRUCTURETYPECountModel"></param>
        /// <returns></returns>
        public static IEnumerable<DCCAMPCount_Model> getCAMPModelCount(DC_UTILITY_CAMP_SW sw, out IEnumerable<STRUCTURETYPECountModel> STRUCTURETYPECountModel)
        {
            var result = new List<DCCAMPCount_Model>();

            //获取单位
            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.TopEchartORGNO = sw.TopORGNO;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            DataTable dt34 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "34" });//结构类型
            DataTable dtCAMP = BaseDT.DC_UTILITY_CAMP.getDT(sw);
            //out 类别
            var STRUCTURETYPE = new List<STRUCTURETYPECountModel>();
            for (int i = 0; i < dt34.Rows.Count; i++)
            {
                STRUCTURETYPECountModel m = new STRUCTURETYPECountModel();
                m.STRUCTURETYPENAME = dt34.Rows[i]["DICTNAME"].ToString();
                m.STRUCTURETYPEVALUE = dt34.Rows[i]["DICTVALUE"].ToString();
                STRUCTURETYPE.Add(m);
            }
            STRUCTURETYPECountModel = STRUCTURETYPE;
            if (PublicCls.OrgIsZhen(sw.TopORGNO) == false)
            {
                DCCAMPCount_Model mm = new DCCAMPCount_Model();
                mm.ORGName = BaseDT.T_SYS_ORG.getName(sw.TopORGNO) + "合计";
                mm.BYORGNO = sw.TopORGNO;
                mm.STRUCTURETYPECount = BaseDT.DC_UTILITY_CAMP.getCountCAMPByOrgNo(dtCAMP, sw.TopORGNO, "");//统计车辆总数
                var typeResult1 = new List<STRUCTURETYPECountModel>();
                //循环结构类型
                for (int j = 0; j < dt34.Rows.Count; j++)
                {
                    STRUCTURETYPECountModel m1 = new STRUCTURETYPECountModel();
                    m1.STRUCTURETYPEVALUE = dt34.Rows[j]["DICTVALUE"].ToString();
                    m1.STRUCTURETYPENAME = dt34.Rows[j]["DICTNAME"].ToString();
                    m1.STRUCTURETYPECount = BaseDT.DC_UTILITY_CAMP.getCountCAMPByOrgNo(dtCAMP, sw.TopORGNO, m1.STRUCTURETYPEVALUE);
                    typeResult1.Add(m1);
                }
                mm.STRUCTURETYPECountModel = typeResult1;
                result.Add(mm);
            }
            //循环单位
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DCCAMPCount_Model m = new DCCAMPCount_Model();
                m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                m.BYORGNO = dtOrg.Rows[i]["ORGNO"].ToString();
                if (PublicCls.OrgIsZhen(m.BYORGNO) == false && m.BYORGNO == sw.TopORGNO)
                {
                    m.STRUCTURETYPECount = BaseDT.DC_UTILITY_CAMP.getCountXSByOrgNo(dtCAMP, m.BYORGNO, "");//统计车辆总数
                    var typeResult = new List<STRUCTURETYPECountModel>();
                    //循环结构类型
                    for (int j = 0; j < dt34.Rows.Count; j++)
                    {
                        STRUCTURETYPECountModel m1 = new STRUCTURETYPECountModel();
                        m1.STRUCTURETYPEVALUE = dt34.Rows[j]["DICTVALUE"].ToString();
                        m1.STRUCTURETYPENAME = dt34.Rows[j]["DICTNAME"].ToString();
                        m1.STRUCTURETYPECount = BaseDT.DC_UTILITY_CAMP.getCountXSByOrgNo(dtCAMP, m.BYORGNO, m1.STRUCTURETYPEVALUE);
                        typeResult.Add(m1);
                    }
                    m.STRUCTURETYPECountModel = typeResult;
                    result.Add(m);
                }
                else
                {
                    m.STRUCTURETYPECount = BaseDT.DC_UTILITY_CAMP.getCountCAMPByOrgNo(dtCAMP, m.BYORGNO, "");//统计车辆总数
                    var typeResult = new List<STRUCTURETYPECountModel>();
                    //循环结构类型
                    for (int j = 0; j < dt34.Rows.Count; j++)
                    {
                        STRUCTURETYPECountModel m1 = new STRUCTURETYPECountModel();
                        m1.STRUCTURETYPEVALUE = dt34.Rows[j]["DICTVALUE"].ToString();
                        m1.STRUCTURETYPENAME = dt34.Rows[j]["DICTNAME"].ToString();
                        m1.STRUCTURETYPECount = BaseDT.DC_UTILITY_CAMP.getCountCAMPByOrgNo(dtCAMP, m.BYORGNO, m1.STRUCTURETYPEVALUE);
                        typeResult.Add(m1);
                    }
                    m.STRUCTURETYPECountModel = typeResult;
                    result.Add(m);
                }
            }
            dtCAMP.Clear();
            dtCAMP.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }
        #endregion

        #region 瞭望台统计
        /// <summary>
        /// 瞭望台统计
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="TYPECountModel"></param>
        /// <returns></returns>
        public static IEnumerable<DCOVERWATCHCount_Model> getOVERWATCHModelCount(DC_UTILITY_OVERWATCH_SW sw, out IEnumerable<TYPECountModel> TYPECountModel)
        {
            var result = new List<DCOVERWATCHCount_Model>();

            //获取单位
            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.TopEchartORGNO = sw.TopORGNO;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            DataTable dt34 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "34" });//结构类型
            DataTable dtOVERWATCH = BaseDT.DC_UTILITY_OVERWATCH.getDT(sw);
            //out 类别
            var STRUCTURETYPE = new List<TYPECountModel>();
            for (int i = 0; i < dt34.Rows.Count; i++)
            {
                TYPECountModel m = new TYPECountModel();
                m.STRUCTURETYPENAME = dt34.Rows[i]["DICTNAME"].ToString();
                m.STRUCTURETYPEVALUE = dt34.Rows[i]["DICTVALUE"].ToString();
                STRUCTURETYPE.Add(m);
            }
            TYPECountModel = STRUCTURETYPE;
            if (PublicCls.OrgIsZhen(sw.TopORGNO) == false)
            {
                DCOVERWATCHCount_Model mm = new DCOVERWATCHCount_Model();
                mm.ORGName = BaseDT.T_SYS_ORG.getName(sw.TopORGNO) + "合计";
                mm.BYORGNO = sw.TopORGNO;

                mm.STRUCTURETYPECount = BaseDT.DC_UTILITY_OVERWATCH.getCountOVERWATCHByOrgNo(dtOVERWATCH, sw.TopORGNO, "");//统计瞭望台总数
                var typeResult1 = new List<TYPECountModel>();
                //循环结构类型
                for (int j = 0; j < dt34.Rows.Count; j++)
                {
                    TYPECountModel m1 = new TYPECountModel();
                    m1.STRUCTURETYPEVALUE = dt34.Rows[j]["DICTVALUE"].ToString();
                    m1.STRUCTURETYPENAME = dt34.Rows[j]["DICTNAME"].ToString();
                    m1.STRUCTURETYPECount = BaseDT.DC_UTILITY_OVERWATCH.getCountOVERWATCHByOrgNo(dtOVERWATCH, sw.TopORGNO, m1.STRUCTURETYPEVALUE);
                    typeResult1.Add(m1);
                }
                mm.TYPECountModel = typeResult1;
                result.Add(mm);
            }
            //循环单位
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DCOVERWATCHCount_Model m = new DCOVERWATCHCount_Model();
                m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                m.BYORGNO = dtOrg.Rows[i]["ORGNO"].ToString();
                if (PublicCls.OrgIsZhen(m.BYORGNO) == false && m.BYORGNO == sw.TopORGNO)
                {
                    m.STRUCTURETYPECount = BaseDT.DC_UTILITY_OVERWATCH.getCountXSByOrgNo(dtOVERWATCH, m.BYORGNO, "");//统计瞭望台总数
                    var typeResult = new List<TYPECountModel>();
                    //循环结构类型
                    for (int j = 0; j < dt34.Rows.Count; j++)
                    {
                        TYPECountModel m1 = new TYPECountModel();
                        m1.STRUCTURETYPEVALUE = dt34.Rows[j]["DICTVALUE"].ToString();
                        m1.STRUCTURETYPENAME = dt34.Rows[j]["DICTNAME"].ToString();
                        m1.STRUCTURETYPECount = BaseDT.DC_UTILITY_OVERWATCH.getCountXSByOrgNo(dtOVERWATCH, m.BYORGNO, m1.STRUCTURETYPEVALUE);
                        typeResult.Add(m1);
                    }
                    m.TYPECountModel = typeResult;
                    result.Add(m);
                }
                else
                {
                    m.STRUCTURETYPECount = BaseDT.DC_UTILITY_OVERWATCH.getCountOVERWATCHByOrgNo(dtOVERWATCH, m.BYORGNO, "");//统计瞭望台总数
                    var typeResult = new List<TYPECountModel>();
                    //循环结构类型
                    for (int j = 0; j < dt34.Rows.Count; j++)
                    {
                        TYPECountModel m1 = new TYPECountModel();
                        m1.STRUCTURETYPEVALUE = dt34.Rows[j]["DICTVALUE"].ToString();
                        m1.STRUCTURETYPENAME = dt34.Rows[j]["DICTNAME"].ToString();
                        m1.STRUCTURETYPECount = BaseDT.DC_UTILITY_OVERWATCH.getCountOVERWATCHByOrgNo(dtOVERWATCH, m.BYORGNO, m1.STRUCTURETYPEVALUE);
                        typeResult.Add(m1);
                    }
                    m.TYPECountModel = typeResult;
                    result.Add(m);
                }

            }
            dtOVERWATCH.Clear();
            dtOVERWATCH.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }
        #endregion

        #region 防火通道统计
        /// <summary>
        /// 防火通道统计
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="USESTATECountModel">使用现状</param>
        /// <param name="MANAGERSTATECountModel">维护类型</param>
        /// <param name="FIRECHANNELLEVELTYPECountModel">防火通道等级类型</param>
        /// <param name="FIRECHANNELUSERTYPECountModel">防火通道使用性质</param>
        /// <returns></returns>
        public static IEnumerable<DCFIRECHANNELCount_Model> getFIRECHANNELModelCount(DC_UTILITY_FIRECHANNEL_SW sw,
            out IEnumerable<USESTATECountModel> USESTATECountModel,
            out IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel,
            out IEnumerable<FIRECHANNELLEVELTYPECountModel> FIRECHANNELLEVELTYPECountModel,
            out IEnumerable<FIRECHANNELUSERTYPECountModel> FIRECHANNELUSERTYPECountModel
            )
        {

            var result = new List<DCFIRECHANNELCount_Model>();

            //获取单位

            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.TopEchartORGNO = sw.TopORGNO;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt38 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "38" });//防火通道等级类型
            DataTable dt39 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "39" });//防火通道使用性质
            DataTable dtFIRECHANNEL = BaseDT.DC_UTILITY_FIRECHANNEL.getDT(sw);
            //out 类别
            var USESTATE = new List<USESTATECountModel>();
            for (int j = 0; j < dt36.Rows.Count; j++)
            {
                USESTATECountModel m = new USESTATECountModel();
                m.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                m.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                USESTATE.Add(m);
            }
            USESTATECountModel = USESTATE;
            var MANAGERSTATE = new List<MANAGERSTATECountModel>();
            for (int j = 0; j < dt37.Rows.Count; j++)
            {
                MANAGERSTATECountModel m = new MANAGERSTATECountModel();
                m.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                m.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                MANAGERSTATE.Add(m);
            }
            MANAGERSTATECountModel = MANAGERSTATE;
            var FIRECHANNELLEVELTYPE = new List<FIRECHANNELLEVELTYPECountModel>();
            for (int j = 0; j < dt38.Rows.Count; j++)
            {
                FIRECHANNELLEVELTYPECountModel m = new FIRECHANNELLEVELTYPECountModel();
                m.FIRECHANNELLEVELTYPEVALUE = dt38.Rows[j]["DICTVALUE"].ToString();
                m.FIRECHANNELLEVELTYPENAME = dt38.Rows[j]["DICTNAME"].ToString();
                FIRECHANNELLEVELTYPE.Add(m);
            }
            FIRECHANNELLEVELTYPECountModel = FIRECHANNELLEVELTYPE;
            var FIRECHANNELUSERTYPE = new List<FIRECHANNELUSERTYPECountModel>();
            for (int j = 0; j < dt39.Rows.Count; j++)
            {
                FIRECHANNELUSERTYPECountModel m = new FIRECHANNELUSERTYPECountModel();
                m.FIRECHANNELUSERTYPEVALUE = dt39.Rows[j]["DICTVALUE"].ToString();
                m.FIRECHANNELUSERTYPENAME = dt39.Rows[j]["DICTNAME"].ToString();
                FIRECHANNELUSERTYPE.Add(m);
            }
            FIRECHANNELUSERTYPECountModel = FIRECHANNELUSERTYPE;
            if (PublicCls.OrgIsZhen(sw.TopORGNO) == false)
            {
                DCFIRECHANNELCount_Model mm = new DCFIRECHANNELCount_Model();
                mm.ORGName = BaseDT.T_SYS_ORG.getName(sw.TopORGNO) + "合计";
                mm.BYORGNO = sw.TopORGNO;
                mm.USESTATECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, sw.TopORGNO, "", "1");//统计使用现状总数
                mm.MANAGERSTATECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, sw.TopORGNO, "", "2");//统计维护类型
                mm.FIRECHANNELLEVELTYPECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, sw.TopORGNO, "", "3");//统计防火通道等级类型
                mm.FIRECHANNELUSERTYPECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, sw.TopORGNO, "", "4");//统计防火通道使用性质

                var MANAGERSTATEResult1 = new List<MANAGERSTATECountModel>();
                //循环维护类型
                for (int j = 0; j < dt37.Rows.Count; j++)
                {
                    MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                    m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                    m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                    m1.MANAGERSTATCount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, sw.TopORGNO, m1.MANAGERSTATVALUE, "2");
                    MANAGERSTATEResult1.Add(m1);
                }
                mm.MANAGERSTATECountModel = MANAGERSTATEResult1;
                var USESTATEResult1 = new List<USESTATECountModel>();
                //循环使用类型
                for (int j = 0; j < dt36.Rows.Count; j++)
                {
                    USESTATECountModel m1 = new USESTATECountModel();
                    m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                    m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                    m1.USESTATECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, sw.TopORGNO, m1.USESTATEVALUE, "1");
                    USESTATEResult1.Add(m1);
                }
                mm.USESTATECountModel = USESTATEResult1;
                var FIRECHANNELLEVELTYPEResult1 = new List<FIRECHANNELLEVELTYPECountModel>();
                //循环防火通道等级
                for (int j = 0; j < dt38.Rows.Count; j++)
                {
                    FIRECHANNELLEVELTYPECountModel m1 = new FIRECHANNELLEVELTYPECountModel();
                    m1.FIRECHANNELLEVELTYPEVALUE = dt38.Rows[j]["DICTVALUE"].ToString();
                    m1.FIRECHANNELLEVELTYPENAME = dt38.Rows[j]["DICTNAME"].ToString();
                    m1.FIRECHANNELLEVELTYPECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, sw.TopORGNO, m1.FIRECHANNELLEVELTYPEVALUE, "3");
                    FIRECHANNELLEVELTYPEResult1.Add(m1);
                }
                mm.FIRECHANNELLEVELTYPECountModel = FIRECHANNELLEVELTYPEResult1;
                var FIRECHANNELUSERTYPEResult1 = new List<FIRECHANNELUSERTYPECountModel>();
                //循环防火通道使用性质
                for (int j = 0; j < dt39.Rows.Count; j++)
                {
                    FIRECHANNELUSERTYPECountModel m1 = new FIRECHANNELUSERTYPECountModel();
                    m1.FIRECHANNELUSERTYPEVALUE = dt39.Rows[j]["DICTVALUE"].ToString();
                    m1.FIRECHANNELUSERTYPENAME = dt39.Rows[j]["DICTNAME"].ToString();
                    m1.FIRECHANNELUSERTYPECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, sw.TopORGNO, m1.FIRECHANNELUSERTYPEVALUE, "4");
                    FIRECHANNELUSERTYPEResult1.Add(m1);
                }
                mm.FIRECHANNELUSERTYPECountModel = FIRECHANNELUSERTYPEResult1;
                result.Add(mm);
            }
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DCFIRECHANNELCount_Model m = new DCFIRECHANNELCount_Model();
                m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                m.BYORGNO = dtOrg.Rows[i]["ORGNO"].ToString();
                if (PublicCls.OrgIsZhen(m.BYORGNO) == false && m.BYORGNO == sw.TopORGNO)
                {
                    m.USESTATECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountXSByOrgNo(dtFIRECHANNEL, m.BYORGNO, "", "1");//统计使用现状总数
                    m.MANAGERSTATECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountXSByOrgNo(dtFIRECHANNEL, m.BYORGNO, "", "2");//统计维护类型
                    m.FIRECHANNELLEVELTYPECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountXSByOrgNo(dtFIRECHANNEL, m.BYORGNO, "", "3");//统计防火通道等级类型
                    m.FIRECHANNELUSERTYPECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountXSByOrgNo(dtFIRECHANNEL, m.BYORGNO, "", "4");//统计防火通道使用性质
                    var MANAGERSTATEResult = new List<MANAGERSTATECountModel>();
                    //循环维护类型
                    for (int j = 0; j < dt37.Rows.Count; j++)
                    {
                        MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                        m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                        m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                        m1.MANAGERSTATCount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountXSByOrgNo(dtFIRECHANNEL, m.BYORGNO, m1.MANAGERSTATVALUE, "2");
                        MANAGERSTATEResult.Add(m1);
                    }
                    m.MANAGERSTATECountModel = MANAGERSTATEResult;
                    var USESTATEResult = new List<USESTATECountModel>();
                    //循环使用类型
                    for (int j = 0; j < dt36.Rows.Count; j++)
                    {
                        USESTATECountModel m1 = new USESTATECountModel();
                        m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                        m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                        m1.USESTATECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountXSByOrgNo(dtFIRECHANNEL, m.BYORGNO, m1.USESTATEVALUE, "1");
                        USESTATEResult.Add(m1);
                    }
                    m.USESTATECountModel = USESTATEResult;
                    var FIRECHANNELLEVELTYPEResult = new List<FIRECHANNELLEVELTYPECountModel>();
                    //循环防火通道等级
                    for (int j = 0; j < dt38.Rows.Count; j++)
                    {
                        FIRECHANNELLEVELTYPECountModel m1 = new FIRECHANNELLEVELTYPECountModel();
                        m1.FIRECHANNELLEVELTYPEVALUE = dt38.Rows[j]["DICTVALUE"].ToString();
                        m1.FIRECHANNELLEVELTYPENAME = dt38.Rows[j]["DICTNAME"].ToString();
                        m1.FIRECHANNELLEVELTYPECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountXSByOrgNo(dtFIRECHANNEL, m.BYORGNO, m1.FIRECHANNELLEVELTYPEVALUE, "3");
                        FIRECHANNELLEVELTYPEResult.Add(m1);
                    }
                    m.FIRECHANNELLEVELTYPECountModel = FIRECHANNELLEVELTYPEResult;
                    var FIRECHANNELUSERTYPEResult = new List<FIRECHANNELUSERTYPECountModel>();
                    //循环防火通道使用性质
                    for (int j = 0; j < dt39.Rows.Count; j++)
                    {
                        FIRECHANNELUSERTYPECountModel m1 = new FIRECHANNELUSERTYPECountModel();
                        m1.FIRECHANNELUSERTYPEVALUE = dt39.Rows[j]["DICTVALUE"].ToString();
                        m1.FIRECHANNELUSERTYPENAME = dt39.Rows[j]["DICTNAME"].ToString();
                        m1.FIRECHANNELUSERTYPECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountXSByOrgNo(dtFIRECHANNEL, m.BYORGNO, m1.FIRECHANNELUSERTYPEVALUE, "4");
                        FIRECHANNELUSERTYPEResult.Add(m1);
                    }
                    m.FIRECHANNELUSERTYPECountModel = FIRECHANNELUSERTYPEResult;
                    result.Add(m);
                }
                else
                {
                    m.USESTATECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, m.BYORGNO, "", "1");//统计使用现状总数
                    m.MANAGERSTATECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, m.BYORGNO, "", "2");//统计维护类型
                    m.FIRECHANNELLEVELTYPECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, m.BYORGNO, "", "3");//统计防火通道等级类型
                    m.FIRECHANNELUSERTYPECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, m.BYORGNO, "", "4");//统计防火通道使用性质
                    var MANAGERSTATEResult = new List<MANAGERSTATECountModel>();
                    //循环维护类型
                    for (int j = 0; j < dt37.Rows.Count; j++)
                    {
                        MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                        m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                        m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                        m1.MANAGERSTATCount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, m.BYORGNO, m1.MANAGERSTATVALUE, "2");
                        MANAGERSTATEResult.Add(m1);
                    }
                    m.MANAGERSTATECountModel = MANAGERSTATEResult;
                    var USESTATEResult = new List<USESTATECountModel>();
                    //循环使用类型
                    for (int j = 0; j < dt36.Rows.Count; j++)
                    {
                        USESTATECountModel m1 = new USESTATECountModel();
                        m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                        m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                        m1.USESTATECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, m.BYORGNO, m1.USESTATEVALUE, "1");
                        USESTATEResult.Add(m1);
                    }
                    m.USESTATECountModel = USESTATEResult;
                    var FIRECHANNELLEVELTYPEResult = new List<FIRECHANNELLEVELTYPECountModel>();
                    //循环防火通道等级
                    for (int j = 0; j < dt38.Rows.Count; j++)
                    {
                        FIRECHANNELLEVELTYPECountModel m1 = new FIRECHANNELLEVELTYPECountModel();
                        m1.FIRECHANNELLEVELTYPEVALUE = dt38.Rows[j]["DICTVALUE"].ToString();
                        m1.FIRECHANNELLEVELTYPENAME = dt38.Rows[j]["DICTNAME"].ToString();
                        m1.FIRECHANNELLEVELTYPECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, m.BYORGNO, m1.FIRECHANNELLEVELTYPEVALUE, "3");
                        FIRECHANNELLEVELTYPEResult.Add(m1);
                    }
                    m.FIRECHANNELLEVELTYPECountModel = FIRECHANNELLEVELTYPEResult;
                    var FIRECHANNELUSERTYPEResult = new List<FIRECHANNELUSERTYPECountModel>();
                    //循环防火通道使用性质
                    for (int j = 0; j < dt39.Rows.Count; j++)
                    {
                        FIRECHANNELUSERTYPECountModel m1 = new FIRECHANNELUSERTYPECountModel();
                        m1.FIRECHANNELUSERTYPEVALUE = dt39.Rows[j]["DICTVALUE"].ToString();
                        m1.FIRECHANNELUSERTYPENAME = dt39.Rows[j]["DICTNAME"].ToString();
                        m1.FIRECHANNELUSERTYPECount = BaseDT.DC_UTILITY_FIRECHANNEL.getCountFIRECHANNELByOrgNo(dtFIRECHANNEL, m.BYORGNO, m1.FIRECHANNELUSERTYPEVALUE, "4");
                        FIRECHANNELUSERTYPEResult.Add(m1);
                    }
                    m.FIRECHANNELUSERTYPECountModel = FIRECHANNELUSERTYPEResult;
                    result.Add(m);
                }
            }
            dtFIRECHANNEL.Clear();
            dtFIRECHANNEL.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }
        #endregion

        #region 隔离带统计
        /// <summary>
        /// 隔离带统计
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="USESTATECountModel">使用现状</param>
        /// <param name="MANAGERSTATECountModel">维护类型</param>
        /// <param name="ISOLATIONTYPECountModel">隔离带类型</param>
        /// <returns></returns>
        public static IEnumerable<DCISOLATIONSTRIPCount_Model> getISOLATIONSTRIPModelCount(DC_UTILITY_ISOLATIONSTRIP_SW sw,
            out IEnumerable<USESTATECountModel> USESTATECountModel,
            out IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel,
            out IEnumerable<ISOLATIONTYPECountModel> ISOLATIONTYPECountModel
            )
        {
            var result = new List<DCISOLATIONSTRIPCount_Model>();
            //获取单位
            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.TopEchartORGNO = sw.TopORGNO;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt35 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "35" });//隔离带类型
            DataTable dtISOLATIONSTRIP = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getDT(sw);
            //out 类别
            var USESTATE = new List<USESTATECountModel>();
            for (int j = 0; j < dt36.Rows.Count; j++)
            {
                USESTATECountModel m = new USESTATECountModel();
                m.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                m.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                USESTATE.Add(m);
            }
            USESTATECountModel = USESTATE;
            var MANAGERSTATE = new List<MANAGERSTATECountModel>();
            for (int j = 0; j < dt37.Rows.Count; j++)
            {
                MANAGERSTATECountModel m = new MANAGERSTATECountModel();
                m.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                m.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                MANAGERSTATE.Add(m);
            }
            MANAGERSTATECountModel = MANAGERSTATE;
            var ISOLATIONTYPE = new List<ISOLATIONTYPECountModel>();
            for (int j = 0; j < dt35.Rows.Count; j++)
            {
                ISOLATIONTYPECountModel m = new ISOLATIONTYPECountModel();
                m.ISOLATIONTYPEVALUE = dt35.Rows[j]["DICTVALUE"].ToString();
                m.ISOLATIONTYPENAME = dt35.Rows[j]["DICTNAME"].ToString();
                ISOLATIONTYPE.Add(m);
            }
            ISOLATIONTYPECountModel = ISOLATIONTYPE;
            if (PublicCls.OrgIsZhen(sw.TopORGNO) == false)
            {
                DCISOLATIONSTRIPCount_Model mm = new DCISOLATIONSTRIPCount_Model();
                mm.ORGName = BaseDT.T_SYS_ORG.getName(sw.TopORGNO) + "合计";
                mm.BYORGNO = sw.TopORGNO;
                mm.USESTATECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountISOLATIONSTRIPByOrgNo(dtISOLATIONSTRIP, sw.TopORGNO, "", "1");//统计使用现状总数
                mm.MANAGERSTATECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountISOLATIONSTRIPByOrgNo(dtISOLATIONSTRIP, sw.TopORGNO, "", "2");//统计维护类型
                mm.ISOLATIONTYPECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountISOLATIONSTRIPByOrgNo(dtISOLATIONSTRIP, sw.TopORGNO, "", "3");//隔离带类型
                mm.LENGTHCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getLENGTHCount(dtISOLATIONSTRIP, sw.TopORGNO, "", "1");//隔离带类型
                var MANAGERSTATEResult1 = new List<MANAGERSTATECountModel>();
                //循环维护类型
                for (int j = 0; j < dt37.Rows.Count; j++)
                {
                    MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                    m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                    m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                    m1.MANAGERSTATCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountISOLATIONSTRIPByOrgNo(dtISOLATIONSTRIP, sw.TopORGNO, m1.MANAGERSTATVALUE, "2");
                    m1.MANAGERSTATLENGTHCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getLENGTHCount(dtISOLATIONSTRIP, sw.TopORGNO, m1.MANAGERSTATVALUE, "2");
                    if (string.IsNullOrEmpty(m1.MANAGERSTATLENGTHCount))
                    {
                        m1.MANAGERSTATLENGTHCount = "0";
                    }
                    MANAGERSTATEResult1.Add(m1);
                }
                mm.MANAGERSTATECountModel = MANAGERSTATEResult1;
                var USESTATEResult1 = new List<USESTATECountModel>();
                //循环使用类型
                for (int j = 0; j < dt36.Rows.Count; j++)
                {
                    USESTATECountModel m1 = new USESTATECountModel();
                    m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                    m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                    m1.USESTATECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountISOLATIONSTRIPByOrgNo(dtISOLATIONSTRIP, sw.TopORGNO, m1.USESTATEVALUE, "1");
                    m1.USESTATELENGTHCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getLENGTHCount(dtISOLATIONSTRIP, sw.TopORGNO, m1.USESTATEVALUE, "1");
                    if (string.IsNullOrEmpty(m1.USESTATELENGTHCount))
                    {
                        m1.USESTATELENGTHCount = "0";
                    }
                    USESTATEResult1.Add(m1);
                }
                mm.USESTATECountModel = USESTATEResult1;
                var ISOLATIONSTRIPResult1 = new List<ISOLATIONTYPECountModel>();
                //循环防火通道等级
                for (int j = 0; j < dt35.Rows.Count; j++)
                {
                    ISOLATIONTYPECountModel m1 = new ISOLATIONTYPECountModel();
                    m1.ISOLATIONTYPEVALUE = dt35.Rows[j]["DICTVALUE"].ToString();
                    m1.ISOLATIONTYPENAME = dt35.Rows[j]["DICTNAME"].ToString();
                    m1.DICTISOLATIONTYPECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountISOLATIONSTRIPByOrgNo(dtISOLATIONSTRIP, sw.TopORGNO, m1.ISOLATIONTYPEVALUE, "3");
                    m1.DICTISOLATIONTYPELENGTHCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getLENGTHCount(dtISOLATIONSTRIP, sw.TopORGNO, m1.ISOLATIONTYPEVALUE, "3");
                    if (string.IsNullOrEmpty(m1.DICTISOLATIONTYPELENGTHCount))
                    {
                        m1.DICTISOLATIONTYPELENGTHCount = "0";
                    }
                    ISOLATIONSTRIPResult1.Add(m1);
                }
                mm.ISOLATIONTYPECountModel = ISOLATIONSTRIPResult1;
                result.Add(mm);
            }
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DCISOLATIONSTRIPCount_Model m = new DCISOLATIONSTRIPCount_Model();
                m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                m.BYORGNO = dtOrg.Rows[i]["ORGNO"].ToString();
                if (PublicCls.OrgIsZhen(m.BYORGNO) == false && m.BYORGNO == sw.TopORGNO)
                {
                    m.USESTATECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountXSByOrgNo(dtISOLATIONSTRIP, m.BYORGNO, "", "1");//统计使用现状总数
                    m.MANAGERSTATECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountXSByOrgNo(dtISOLATIONSTRIP, m.BYORGNO, "", "2");//统计维护类型
                    m.ISOLATIONTYPECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountXSByOrgNo(dtISOLATIONSTRIP, m.BYORGNO, "", "3");//隔离带类型
                    m.LENGTHCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getLENGTHXSCount(dtISOLATIONSTRIP, m.BYORGNO, "", "1");
                    if (string.IsNullOrEmpty(m.LENGTHCount))
                    {
                        m.LENGTHCount = "0";
                    }
                    var MANAGERSTATEResult = new List<MANAGERSTATECountModel>();
                    //循环维护类型
                    for (int j = 0; j < dt37.Rows.Count; j++)
                    {
                        MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                        m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                        m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                        m1.MANAGERSTATCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountXSByOrgNo(dtISOLATIONSTRIP, m.BYORGNO, m1.MANAGERSTATVALUE, "2");
                        m1.MANAGERSTATLENGTHCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getLENGTHXSCount(dtISOLATIONSTRIP, m.BYORGNO, m1.MANAGERSTATVALUE, "2");
                        if (string.IsNullOrEmpty(m1.MANAGERSTATLENGTHCount))
                        {
                            m1.MANAGERSTATLENGTHCount = "0";
                        }
                        MANAGERSTATEResult.Add(m1);
                    }
                    m.MANAGERSTATECountModel = MANAGERSTATEResult;
                    var USESTATEResult = new List<USESTATECountModel>();
                    //循环使用类型
                    for (int j = 0; j < dt36.Rows.Count; j++)
                    {
                        USESTATECountModel m1 = new USESTATECountModel();
                        m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                        m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                        m1.USESTATECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountXSByOrgNo(dtISOLATIONSTRIP, m.BYORGNO, m1.USESTATEVALUE, "1");
                        m1.USESTATELENGTHCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getLENGTHXSCount(dtISOLATIONSTRIP, m.BYORGNO, m1.USESTATEVALUE, "1");
                        if (string.IsNullOrEmpty(m1.USESTATELENGTHCount))
                        {
                            m1.USESTATELENGTHCount = "0";
                        }
                        USESTATEResult.Add(m1);
                    }
                    m.USESTATECountModel = USESTATEResult;
                    var ISOLATIONSTRIPResult = new List<ISOLATIONTYPECountModel>();
                    //循环隔离带类型
                    for (int j = 0; j < dt35.Rows.Count; j++)
                    {
                        ISOLATIONTYPECountModel m1 = new ISOLATIONTYPECountModel();
                        m1.ISOLATIONTYPEVALUE = dt35.Rows[j]["DICTVALUE"].ToString();
                        m1.ISOLATIONTYPENAME = dt35.Rows[j]["DICTNAME"].ToString();
                        m1.DICTISOLATIONTYPECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountXSByOrgNo(dtISOLATIONSTRIP, m.BYORGNO, m1.ISOLATIONTYPEVALUE, "3");
                        m1.DICTISOLATIONTYPELENGTHCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getLENGTHXSCount(dtISOLATIONSTRIP, m.BYORGNO, m1.ISOLATIONTYPEVALUE, "3");
                        if (string.IsNullOrEmpty(m1.DICTISOLATIONTYPELENGTHCount))
                        {
                            m1.DICTISOLATIONTYPELENGTHCount = "0";
                        }
                        ISOLATIONSTRIPResult.Add(m1);
                    }
                    m.ISOLATIONTYPECountModel = ISOLATIONSTRIPResult;
                    result.Add(m);
                }
                else
                {
                    m.USESTATECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountISOLATIONSTRIPByOrgNo(dtISOLATIONSTRIP, m.BYORGNO, "", "1");//统计使用现状总数
                    m.MANAGERSTATECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountISOLATIONSTRIPByOrgNo(dtISOLATIONSTRIP, m.BYORGNO, "", "2");//统计维护类型
                    m.ISOLATIONTYPECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountISOLATIONSTRIPByOrgNo(dtISOLATIONSTRIP, m.BYORGNO, "", "3");//隔离带类型
                    m.LENGTHCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getLENGTHCount(dtISOLATIONSTRIP, m.BYORGNO, "", "1");
                    if (string.IsNullOrEmpty(m.LENGTHCount))
                    {
                        m.LENGTHCount = "0";
                    }
                    var MANAGERSTATEResult = new List<MANAGERSTATECountModel>();
                    //循环维护类型
                    for (int j = 0; j < dt37.Rows.Count; j++)
                    {
                        MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                        m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                        m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                        m1.MANAGERSTATCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountISOLATIONSTRIPByOrgNo(dtISOLATIONSTRIP, m.BYORGNO, m1.MANAGERSTATVALUE, "2");
                        m1.MANAGERSTATLENGTHCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getLENGTHCount(dtISOLATIONSTRIP, m.BYORGNO, m1.MANAGERSTATVALUE, "2");
                        if (string.IsNullOrEmpty(m1.MANAGERSTATLENGTHCount))
                        {
                            m1.MANAGERSTATLENGTHCount = "0";
                        }
                        MANAGERSTATEResult.Add(m1);
                    }
                    m.MANAGERSTATECountModel = MANAGERSTATEResult;
                    var USESTATEResult = new List<USESTATECountModel>();
                    //循环使用类型
                    for (int j = 0; j < dt36.Rows.Count; j++)
                    {
                        USESTATECountModel m1 = new USESTATECountModel();
                        m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                        m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                        m1.USESTATECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountISOLATIONSTRIPByOrgNo(dtISOLATIONSTRIP, m.BYORGNO, m1.USESTATEVALUE, "1");
                        m1.USESTATELENGTHCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getLENGTHCount(dtISOLATIONSTRIP, m.BYORGNO, m1.USESTATEVALUE, "1");
                        if (string.IsNullOrEmpty(m1.USESTATELENGTHCount))
                        {
                            m1.USESTATELENGTHCount = "0";
                        }
                        USESTATEResult.Add(m1);
                    }
                    m.USESTATECountModel = USESTATEResult;
                    var ISOLATIONSTRIPResult = new List<ISOLATIONTYPECountModel>();
                    //循环隔离带类型
                    for (int j = 0; j < dt35.Rows.Count; j++)
                    {
                        ISOLATIONTYPECountModel m1 = new ISOLATIONTYPECountModel();
                        m1.ISOLATIONTYPEVALUE = dt35.Rows[j]["DICTVALUE"].ToString();
                        m1.ISOLATIONTYPENAME = dt35.Rows[j]["DICTNAME"].ToString();
                        m1.DICTISOLATIONTYPECount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getCountISOLATIONSTRIPByOrgNo(dtISOLATIONSTRIP, m.BYORGNO, m1.ISOLATIONTYPEVALUE, "3");
                        m1.DICTISOLATIONTYPELENGTHCount = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getLENGTHCount(dtISOLATIONSTRIP, m.BYORGNO, m1.ISOLATIONTYPEVALUE, "3");
                        if (string.IsNullOrEmpty(m1.DICTISOLATIONTYPELENGTHCount))
                        {
                            m1.DICTISOLATIONTYPELENGTHCount = "0";
                        }
                        ISOLATIONSTRIPResult.Add(m1);
                    }
                    m.ISOLATIONTYPECountModel = ISOLATIONSTRIPResult;
                    result.Add(m);
                }
            }
            dtISOLATIONSTRIP.Clear();
            dtISOLATIONSTRIP.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }
        #endregion

        #region 宣传碑牌统计
        /// <summary>
        /// 宣传碑牌统计
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="USESTATECountModel">使用现状</param>
        /// <param name="MANAGERSTATECountModel">维护类型</param>
        /// <param name="PROPAGANDASTELETYPECountModel">宣传碑类型</param>
        /// /// <param name="STRUCTURETYPECountModel">结构类型</param>
        /// <returns></returns>
        public static IEnumerable<DCPROPAGANDASTELECount_Model> getPROPAGANDASTELEModelCount(DC_UTILITY_PROPAGANDASTELE_SW sw,
            out IEnumerable<USESTATECountModel> USESTATECountModel,
            out IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel,
            out IEnumerable<PROPAGANDASTELETYPECountModel> PROPAGANDASTELETYPECountModel,
            out IEnumerable<STRUCTURETYPECountModel> STRUCTURETYPECountModel
            )
        {

            var result = new List<DCPROPAGANDASTELECount_Model>();

            //获取单位
            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.TopEchartORGNO = sw.TopORGNO;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            DataTable dt34 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "34" });//建筑物结构类型
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt40 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "40" });//宣传碑类型
            DataTable dtPROPAGANDASTELE = BaseDT.DC_UTILITY_PROPAGANDASTELE.getDT(sw);
            //out 类别
            var USESTATE = new List<USESTATECountModel>();
            for (int j = 0; j < dt36.Rows.Count; j++)
            {
                USESTATECountModel m = new USESTATECountModel();
                m.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                m.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                USESTATE.Add(m);
            }
            USESTATECountModel = USESTATE;
            var MANAGERSTATE = new List<MANAGERSTATECountModel>();
            for (int j = 0; j < dt37.Rows.Count; j++)
            {
                MANAGERSTATECountModel m = new MANAGERSTATECountModel();
                m.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                m.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                MANAGERSTATE.Add(m);
            }
            MANAGERSTATECountModel = MANAGERSTATE;
            var PROPAGANDASTELETYPE = new List<PROPAGANDASTELETYPECountModel>();
            for (int j = 0; j < dt40.Rows.Count; j++)
            {
                PROPAGANDASTELETYPECountModel m = new PROPAGANDASTELETYPECountModel();
                m.PROPAGANDASTELETYPEVALUE = dt40.Rows[j]["DICTVALUE"].ToString();
                m.PROPAGANDASTELETYPENAME = dt40.Rows[j]["DICTNAME"].ToString();
                PROPAGANDASTELETYPE.Add(m);
            }
            PROPAGANDASTELETYPECountModel = PROPAGANDASTELETYPE;
            var STRUCTURETYPE = new List<STRUCTURETYPECountModel>();
            for (int j = 0; j < dt34.Rows.Count; j++)
            {
                STRUCTURETYPECountModel m = new STRUCTURETYPECountModel();
                m.STRUCTURETYPEVALUE = dt34.Rows[j]["DICTVALUE"].ToString();
                m.STRUCTURETYPENAME = dt34.Rows[j]["DICTNAME"].ToString();
                STRUCTURETYPE.Add(m);
            }
            STRUCTURETYPECountModel = STRUCTURETYPE;
            if (PublicCls.OrgIsZhen(sw.TopORGNO) == false)
            {
                DCPROPAGANDASTELECount_Model mm = new DCPROPAGANDASTELECount_Model();
                mm.ORGName = BaseDT.T_SYS_ORG.getName(sw.TopORGNO) + "合计";
                mm.BYORGNO = sw.TopORGNO;
                mm.USESTATECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, sw.TopORGNO, "", "1");//统计使用现状总数
                mm.MANAGERSTATECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, sw.TopORGNO, "", "2");//统计维护类型
                mm.PROPAGANDASTELETYPECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, sw.TopORGNO, "", "3");//宣传碑类型
                mm.STRUCTURETYPECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, sw.TopORGNO, "", "4");//结构类型
                var MANAGERSTATEResult1 = new List<MANAGERSTATECountModel>();
                //循环维护类型
                for (int j = 0; j < dt37.Rows.Count; j++)
                {
                    MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                    m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                    m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                    m1.MANAGERSTATCount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, sw.TopORGNO, m1.MANAGERSTATVALUE, "2");
                    MANAGERSTATEResult1.Add(m1);
                }
                mm.MANAGERSTATECountModel = MANAGERSTATEResult1;
                var USESTATEResult1 = new List<USESTATECountModel>();
                //循环使用类型
                for (int j = 0; j < dt36.Rows.Count; j++)
                {
                    USESTATECountModel m1 = new USESTATECountModel();
                    m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                    m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                    m1.USESTATECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, sw.TopORGNO, m1.USESTATEVALUE, "1");
                    USESTATEResult1.Add(m1);
                }
                mm.USESTATECountModel = USESTATEResult1;
                var PROPAGANDASTELETYPEResult1 = new List<PROPAGANDASTELETYPECountModel>();
                //循环宣传碑类型
                for (int j = 0; j < dt40.Rows.Count; j++)
                {
                    PROPAGANDASTELETYPECountModel m1 = new PROPAGANDASTELETYPECountModel();
                    m1.PROPAGANDASTELETYPEVALUE = dt40.Rows[j]["DICTVALUE"].ToString();
                    m1.PROPAGANDASTELETYPENAME = dt40.Rows[j]["DICTNAME"].ToString();
                    m1.DICTPROPAGANDASTELETYPECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, sw.TopORGNO, m1.PROPAGANDASTELETYPEVALUE, "3");
                    PROPAGANDASTELETYPEResult1.Add(m1);
                }
                mm.PROPAGANDASTELETYPECountModel = PROPAGANDASTELETYPEResult1;
                var STRUCTURETYPEResult1 = new List<STRUCTURETYPECountModel>();
                //循环结构类型
                for (int j = 0; j < dt34.Rows.Count; j++)
                {
                    STRUCTURETYPECountModel m1 = new STRUCTURETYPECountModel();
                    m1.STRUCTURETYPEVALUE = dt34.Rows[j]["DICTVALUE"].ToString();
                    m1.STRUCTURETYPENAME = dt34.Rows[j]["DICTNAME"].ToString();
                    m1.STRUCTURETYPECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, sw.TopORGNO, m1.STRUCTURETYPEVALUE, "4");
                    STRUCTURETYPEResult1.Add(m1);
                }
                mm.STRUCTURETYPECountModel = STRUCTURETYPEResult1;
                result.Add(mm);
            }
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DCPROPAGANDASTELECount_Model m = new DCPROPAGANDASTELECount_Model();
                m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                m.BYORGNO = dtOrg.Rows[i]["ORGNO"].ToString();
                if (PublicCls.OrgIsZhen(m.BYORGNO) == false && m.BYORGNO == sw.TopORGNO)
                {
                    m.USESTATECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountXSByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, "", "1");//统计使用现状总数
                    m.MANAGERSTATECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountXSByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, "", "2");//统计维护类型
                    m.PROPAGANDASTELETYPECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountXSByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, "", "3");//宣传碑类型
                    m.STRUCTURETYPECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountXSByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, "", "4");//结构类型
                    var MANAGERSTATEResult = new List<MANAGERSTATECountModel>();
                    //循环维护类型
                    for (int j = 0; j < dt37.Rows.Count; j++)
                    {
                        MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                        m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                        m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                        m1.MANAGERSTATCount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountXSByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, m1.MANAGERSTATVALUE, "2");
                        MANAGERSTATEResult.Add(m1);
                    }
                    m.MANAGERSTATECountModel = MANAGERSTATEResult;
                    var USESTATEResult = new List<USESTATECountModel>();
                    //循环使用类型
                    for (int j = 0; j < dt36.Rows.Count; j++)
                    {
                        USESTATECountModel m1 = new USESTATECountModel();
                        m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                        m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                        m1.USESTATECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountXSByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, m1.USESTATEVALUE, "1");
                        USESTATEResult.Add(m1);
                    }
                    m.USESTATECountModel = USESTATEResult;
                    var PROPAGANDASTELETYPEResult = new List<PROPAGANDASTELETYPECountModel>();
                    //循环宣传碑类型
                    for (int j = 0; j < dt40.Rows.Count; j++)
                    {
                        PROPAGANDASTELETYPECountModel m1 = new PROPAGANDASTELETYPECountModel();
                        m1.PROPAGANDASTELETYPEVALUE = dt40.Rows[j]["DICTVALUE"].ToString();
                        m1.PROPAGANDASTELETYPENAME = dt40.Rows[j]["DICTNAME"].ToString();
                        m1.DICTPROPAGANDASTELETYPECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountXSByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, m1.PROPAGANDASTELETYPEVALUE, "3");
                        PROPAGANDASTELETYPEResult.Add(m1);
                    }
                    m.PROPAGANDASTELETYPECountModel = PROPAGANDASTELETYPEResult;
                    var STRUCTURETYPEResult = new List<STRUCTURETYPECountModel>();
                    //循环结构类型
                    for (int j = 0; j < dt34.Rows.Count; j++)
                    {
                        STRUCTURETYPECountModel m1 = new STRUCTURETYPECountModel();
                        m1.STRUCTURETYPEVALUE = dt34.Rows[j]["DICTVALUE"].ToString();
                        m1.STRUCTURETYPENAME = dt34.Rows[j]["DICTNAME"].ToString();
                        m1.STRUCTURETYPECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountXSByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, m1.STRUCTURETYPEVALUE, "4");
                        STRUCTURETYPEResult.Add(m1);
                    }
                    m.STRUCTURETYPECountModel = STRUCTURETYPEResult;
                    result.Add(m);
                }
                else
                {
                    m.USESTATECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, "", "1");//统计使用现状总数
                    m.MANAGERSTATECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, "", "2");//统计维护类型
                    m.PROPAGANDASTELETYPECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, "", "3");//宣传碑类型
                    m.STRUCTURETYPECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, "", "4");//结构类型
                    var MANAGERSTATEResult = new List<MANAGERSTATECountModel>();
                    //循环维护类型
                    for (int j = 0; j < dt37.Rows.Count; j++)
                    {
                        MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                        m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                        m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                        m1.MANAGERSTATCount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, m1.MANAGERSTATVALUE, "2");
                        MANAGERSTATEResult.Add(m1);
                    }
                    m.MANAGERSTATECountModel = MANAGERSTATEResult;
                    var USESTATEResult = new List<USESTATECountModel>();
                    //循环使用类型
                    for (int j = 0; j < dt36.Rows.Count; j++)
                    {
                        USESTATECountModel m1 = new USESTATECountModel();
                        m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                        m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                        m1.USESTATECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, m1.USESTATEVALUE, "1");
                        USESTATEResult.Add(m1);
                    }
                    m.USESTATECountModel = USESTATEResult;
                    var PROPAGANDASTELETYPEResult = new List<PROPAGANDASTELETYPECountModel>();
                    //循环宣传碑类型
                    for (int j = 0; j < dt40.Rows.Count; j++)
                    {
                        PROPAGANDASTELETYPECountModel m1 = new PROPAGANDASTELETYPECountModel();
                        m1.PROPAGANDASTELETYPEVALUE = dt40.Rows[j]["DICTVALUE"].ToString();
                        m1.PROPAGANDASTELETYPENAME = dt40.Rows[j]["DICTNAME"].ToString();
                        m1.DICTPROPAGANDASTELETYPECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, m1.PROPAGANDASTELETYPEVALUE, "3");
                        PROPAGANDASTELETYPEResult.Add(m1);
                    }
                    m.PROPAGANDASTELETYPECountModel = PROPAGANDASTELETYPEResult;
                    var STRUCTURETYPEResult = new List<STRUCTURETYPECountModel>();
                    //循环结构类型
                    for (int j = 0; j < dt34.Rows.Count; j++)
                    {
                        STRUCTURETYPECountModel m1 = new STRUCTURETYPECountModel();
                        m1.STRUCTURETYPEVALUE = dt34.Rows[j]["DICTVALUE"].ToString();
                        m1.STRUCTURETYPENAME = dt34.Rows[j]["DICTNAME"].ToString();
                        m1.STRUCTURETYPECount = BaseDT.DC_UTILITY_PROPAGANDASTELE.getCountPROPAGANDASTELEByOrgNo(dtPROPAGANDASTELE, m.BYORGNO, m1.STRUCTURETYPEVALUE, "4");
                        STRUCTURETYPEResult.Add(m1);
                    }
                    m.STRUCTURETYPECountModel = STRUCTURETYPEResult;
                    result.Add(m);
                }

            }
            dtPROPAGANDASTELE.Clear();
            dtPROPAGANDASTELE.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }
        #endregion

        #region 中继站统计
        /// <summary>
        /// 中继站统计
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="USESTATECountModel">使用现状</param>
        /// <param name="MANAGERSTATECountModel">维护类型</param>
        /// <param name="COMMUNICATIONWAYCountModel">通讯方式</param>
        /// <returns></returns>
        public static IEnumerable<DCRELAYCount_Model> getRELAYModelCount(DC_UTILITY_RELAY_SW sw,
            out IEnumerable<USESTATECountModel> USESTATECountModel,
            out IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel,
            out IEnumerable<COMMUNICATIONWAYCountModel> COMMUNICATIONWAYCountModel
            )
        {

            var result = new List<DCRELAYCount_Model>();

            //获取单位
            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.TopEchartORGNO = sw.TopORGNO;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt41 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "41" });//通讯方式
            DataTable dtRELAY = BaseDT.DC_UTILITY_RELAY.getDT(sw);
            //out 类别
            var USESTATE = new List<USESTATECountModel>();
            for (int j = 0; j < dt36.Rows.Count; j++)
            {
                USESTATECountModel m = new USESTATECountModel();
                m.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                m.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                USESTATE.Add(m);
            }
            USESTATECountModel = USESTATE;
            var MANAGERSTATE = new List<MANAGERSTATECountModel>();
            for (int j = 0; j < dt37.Rows.Count; j++)
            {
                MANAGERSTATECountModel m = new MANAGERSTATECountModel();
                m.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                m.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                MANAGERSTATE.Add(m);
            }
            MANAGERSTATECountModel = MANAGERSTATE;
            var COMMUNICATIONWAY = new List<COMMUNICATIONWAYCountModel>();
            for (int j = 0; j < dt41.Rows.Count; j++)
            {
                COMMUNICATIONWAYCountModel m = new COMMUNICATIONWAYCountModel();
                m.COMMUNICATIONWAYVALUE = dt41.Rows[j]["DICTVALUE"].ToString();
                m.COMMUNICATIONWAYNAME = dt41.Rows[j]["DICTNAME"].ToString();
                COMMUNICATIONWAY.Add(m);
            }
            COMMUNICATIONWAYCountModel = COMMUNICATIONWAY;
            if (PublicCls.OrgIsZhen(sw.TopORGNO) == false)
            {
                DCRELAYCount_Model mm = new DCRELAYCount_Model();
                mm.ORGName = BaseDT.T_SYS_ORG.getName(sw.TopORGNO) + "合计";
                mm.BYORGNO = sw.TopORGNO;
                mm.USESTATECount = BaseDT.DC_UTILITY_RELAY.getCountRELAYByOrgNo(dtRELAY, sw.TopORGNO, "", "1");//统计使用现状总数
                mm.MANAGERSTATECount = BaseDT.DC_UTILITY_RELAY.getCountRELAYByOrgNo(dtRELAY, sw.TopORGNO, "", "2");//统计维护类型
                mm.COMMUNICATIONWAYCount = BaseDT.DC_UTILITY_RELAY.getCountRELAYByOrgNo(dtRELAY, sw.TopORGNO, "", "3");//通讯方式类型
                var MANAGERSTATEResult1 = new List<MANAGERSTATECountModel>();
                //循环维护类型
                for (int j = 0; j < dt37.Rows.Count; j++)
                {
                    MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                    m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                    m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                    m1.MANAGERSTATCount = BaseDT.DC_UTILITY_RELAY.getCountRELAYByOrgNo(dtRELAY, sw.TopORGNO, m1.MANAGERSTATVALUE, "2");
                    MANAGERSTATEResult1.Add(m1);
                }
                mm.MANAGERSTATECountModel = MANAGERSTATEResult1;
                var USESTATEResult1 = new List<USESTATECountModel>();
                //循环使用类型
                for (int j = 0; j < dt36.Rows.Count; j++)
                {
                    USESTATECountModel m1 = new USESTATECountModel();
                    m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                    m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                    m1.USESTATECount = BaseDT.DC_UTILITY_RELAY.getCountRELAYByOrgNo(dtRELAY, sw.TopORGNO, m1.USESTATEVALUE, "1");
                    USESTATEResult1.Add(m1);
                }
                mm.USESTATECountModel = USESTATEResult1;
                var COMMUNICATIONWAYResult1 = new List<COMMUNICATIONWAYCountModel>();
                //循环通讯方式类型
                for (int j = 0; j < dt41.Rows.Count; j++)
                {
                    COMMUNICATIONWAYCountModel m1 = new COMMUNICATIONWAYCountModel();
                    m1.COMMUNICATIONWAYVALUE = dt41.Rows[j]["DICTVALUE"].ToString();
                    m1.COMMUNICATIONWAYNAME = dt41.Rows[j]["DICTNAME"].ToString();
                    m1.DICTCOMMUNICATIONWAYCount = BaseDT.DC_UTILITY_RELAY.getCountRELAYByOrgNo(dtRELAY, sw.TopORGNO, m1.COMMUNICATIONWAYVALUE, "3");
                    COMMUNICATIONWAYResult1.Add(m1);
                }
                mm.COMMUNICATIONWAYCountModel = COMMUNICATIONWAYResult1;
                result.Add(mm);
            }
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DCRELAYCount_Model m = new DCRELAYCount_Model();
                m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                m.BYORGNO = dtOrg.Rows[i]["ORGNO"].ToString();
                if (PublicCls.OrgIsZhen(m.BYORGNO) == false && m.BYORGNO == sw.TopORGNO)
                {
                    m.USESTATECount = BaseDT.DC_UTILITY_RELAY.getCountXSByOrgNo(dtRELAY, m.BYORGNO, "", "1");//统计使用现状总数
                    m.MANAGERSTATECount = BaseDT.DC_UTILITY_RELAY.getCountXSByOrgNo(dtRELAY, m.BYORGNO, "", "2");//统计维护类型
                    m.COMMUNICATIONWAYCount = BaseDT.DC_UTILITY_RELAY.getCountXSByOrgNo(dtRELAY, m.BYORGNO, "", "3");//通讯方式类型
                    var MANAGERSTATEResult = new List<MANAGERSTATECountModel>();
                    //循环维护类型
                    for (int j = 0; j < dt37.Rows.Count; j++)
                    {
                        MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                        m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                        m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                        m1.MANAGERSTATCount = BaseDT.DC_UTILITY_RELAY.getCountXSByOrgNo(dtRELAY, m.BYORGNO, m1.MANAGERSTATVALUE, "2");
                        MANAGERSTATEResult.Add(m1);
                    }
                    m.MANAGERSTATECountModel = MANAGERSTATEResult;
                    var USESTATEResult = new List<USESTATECountModel>();
                    //循环使用类型
                    for (int j = 0; j < dt36.Rows.Count; j++)
                    {
                        USESTATECountModel m1 = new USESTATECountModel();
                        m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                        m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                        m1.USESTATECount = BaseDT.DC_UTILITY_RELAY.getCountXSByOrgNo(dtRELAY, m.BYORGNO, m1.USESTATEVALUE, "1");
                        USESTATEResult.Add(m1);
                    }
                    m.USESTATECountModel = USESTATEResult;
                    var COMMUNICATIONWAYResult = new List<COMMUNICATIONWAYCountModel>();
                    //循环通讯方式类型
                    for (int j = 0; j < dt41.Rows.Count; j++)
                    {
                        COMMUNICATIONWAYCountModel m1 = new COMMUNICATIONWAYCountModel();
                        m1.COMMUNICATIONWAYVALUE = dt41.Rows[j]["DICTVALUE"].ToString();
                        m1.COMMUNICATIONWAYNAME = dt41.Rows[j]["DICTNAME"].ToString();
                        m1.DICTCOMMUNICATIONWAYCount = BaseDT.DC_UTILITY_RELAY.getCountXSByOrgNo(dtRELAY, m.BYORGNO, m1.COMMUNICATIONWAYVALUE, "3");
                        COMMUNICATIONWAYResult.Add(m1);
                    }
                    m.COMMUNICATIONWAYCountModel = COMMUNICATIONWAYResult;
                    result.Add(m);

                }
                else
                {
                    m.USESTATECount = BaseDT.DC_UTILITY_RELAY.getCountRELAYByOrgNo(dtRELAY, m.BYORGNO, "", "1");//统计使用现状总数
                    m.MANAGERSTATECount = BaseDT.DC_UTILITY_RELAY.getCountRELAYByOrgNo(dtRELAY, m.BYORGNO, "", "2");//统计维护类型
                    m.COMMUNICATIONWAYCount = BaseDT.DC_UTILITY_RELAY.getCountRELAYByOrgNo(dtRELAY, m.BYORGNO, "", "3");//通讯方式类型
                    var MANAGERSTATEResult = new List<MANAGERSTATECountModel>();
                    //循环维护类型
                    for (int j = 0; j < dt37.Rows.Count; j++)
                    {
                        MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                        m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                        m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                        m1.MANAGERSTATCount = BaseDT.DC_UTILITY_RELAY.getCountRELAYByOrgNo(dtRELAY, m.BYORGNO, m1.MANAGERSTATVALUE, "2");
                        MANAGERSTATEResult.Add(m1);
                    }
                    m.MANAGERSTATECountModel = MANAGERSTATEResult;
                    var USESTATEResult = new List<USESTATECountModel>();
                    //循环使用类型
                    for (int j = 0; j < dt36.Rows.Count; j++)
                    {
                        USESTATECountModel m1 = new USESTATECountModel();
                        m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                        m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                        m1.USESTATECount = BaseDT.DC_UTILITY_RELAY.getCountRELAYByOrgNo(dtRELAY, m.BYORGNO, m1.USESTATEVALUE, "1");
                        USESTATEResult.Add(m1);
                    }
                    m.USESTATECountModel = USESTATEResult;
                    var COMMUNICATIONWAYResult = new List<COMMUNICATIONWAYCountModel>();
                    //循环通讯方式类型
                    for (int j = 0; j < dt41.Rows.Count; j++)
                    {
                        COMMUNICATIONWAYCountModel m1 = new COMMUNICATIONWAYCountModel();
                        m1.COMMUNICATIONWAYVALUE = dt41.Rows[j]["DICTVALUE"].ToString();
                        m1.COMMUNICATIONWAYNAME = dt41.Rows[j]["DICTNAME"].ToString();
                        m1.DICTCOMMUNICATIONWAYCount = BaseDT.DC_UTILITY_RELAY.getCountRELAYByOrgNo(dtRELAY, m.BYORGNO, m1.COMMUNICATIONWAYVALUE, "3");
                        COMMUNICATIONWAYResult.Add(m1);
                    }
                    m.COMMUNICATIONWAYCountModel = COMMUNICATIONWAYResult;
                    result.Add(m);

                }

            }

            dtRELAY.Clear();
            dtRELAY.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }
        #endregion

        #region 监测站统计
        /// <summary>
        /// 监测站统计
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="USESTATECountModel">使用现状</param>
        /// <param name="MANAGERSTATECountModel">维护类型</param>
        /// <param name="TRANSFERMODETYPECountModel">无线电传输方式</param>
        /// <returns></returns>
        public static IEnumerable<DCMONITORINGSTATIONCount_Model> getMONITORINGSTATIONModelCount(DC_UTILITY_MONITORINGSTATION_SW sw,
           out IEnumerable<USESTATECountModel> USESTATECountModel,
           out IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel,
           out IEnumerable<TRANSFERMODETYPECountModel> TRANSFERMODETYPECountModel
           )
        {

            var result = new List<DCMONITORINGSTATIONCount_Model>();

            //获取单位
            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.TopEchartORGNO = sw.TopORGNO;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt42 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "42" });//无线电传输方式
            DataTable dtMONITORINGSTATION = BaseDT.DC_UTILITY_MONITORINGSTATION.getDT(sw);
            //out 类别
            var USESTATE = new List<USESTATECountModel>();
            for (int j = 0; j < dt36.Rows.Count; j++)
            {
                USESTATECountModel m = new USESTATECountModel();
                m.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                m.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                USESTATE.Add(m);
            }
            USESTATECountModel = USESTATE;
            var MANAGERSTATE = new List<MANAGERSTATECountModel>();
            for (int j = 0; j < dt37.Rows.Count; j++)
            {
                MANAGERSTATECountModel m = new MANAGERSTATECountModel();
                m.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                m.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                MANAGERSTATE.Add(m);
            }
            MANAGERSTATECountModel = MANAGERSTATE;
            var TRANSFERMODETYPE = new List<TRANSFERMODETYPECountModel>();
            for (int j = 0; j < dt42.Rows.Count; j++)
            {
                TRANSFERMODETYPECountModel m = new TRANSFERMODETYPECountModel();
                m.TRANSFERMODETYPEVALUE = dt42.Rows[j]["DICTVALUE"].ToString();
                m.TRANSFERMODETYPENAME = dt42.Rows[j]["DICTNAME"].ToString();
                TRANSFERMODETYPE.Add(m);
            }
            TRANSFERMODETYPECountModel = TRANSFERMODETYPE;
            if (PublicCls.OrgIsZhen(sw.TopORGNO) == false)
            {
                DCMONITORINGSTATIONCount_Model mm = new DCMONITORINGSTATIONCount_Model();
                mm.ORGName = BaseDT.T_SYS_ORG.getName(sw.TopORGNO) + "合计";
                mm.BYORGNO = sw.TopORGNO;
                mm.USESTATECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountMONITORINGSTATIONByOrgNo(dtMONITORINGSTATION, sw.TopORGNO, "", "1");//统计使用现状总数
                mm.MANAGERSTATECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountMONITORINGSTATIONByOrgNo(dtMONITORINGSTATION, sw.TopORGNO, "", "2");//统计维护类型
                mm.TRANSFERMODETYPECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountMONITORINGSTATIONByOrgNo(dtMONITORINGSTATION, sw.TopORGNO, "", "3");//无线电传输方式
                var MANAGERSTATEResult1 = new List<MANAGERSTATECountModel>();
                //循环维护类型
                for (int j = 0; j < dt37.Rows.Count; j++)
                {
                    MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                    m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                    m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                    m1.MANAGERSTATCount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountMONITORINGSTATIONByOrgNo(dtMONITORINGSTATION, sw.TopORGNO, m1.MANAGERSTATVALUE, "2");
                    MANAGERSTATEResult1.Add(m1);
                }
                mm.MANAGERSTATECountModel = MANAGERSTATEResult1;
                var USESTATEResult1 = new List<USESTATECountModel>();
                //循环使用类型
                for (int j = 0; j < dt36.Rows.Count; j++)
                {
                    USESTATECountModel m1 = new USESTATECountModel();
                    m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                    m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                    m1.USESTATECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountMONITORINGSTATIONByOrgNo(dtMONITORINGSTATION, sw.TopORGNO, m1.USESTATEVALUE, "1");
                    USESTATEResult1.Add(m1);
                }
                mm.USESTATECountModel = USESTATEResult1;
                var TRANSFERMODETYPEResult1 = new List<TRANSFERMODETYPECountModel>();
                //循环通讯方式类型
                for (int j = 0; j < dt42.Rows.Count; j++)
                {
                    TRANSFERMODETYPECountModel m1 = new TRANSFERMODETYPECountModel();
                    m1.TRANSFERMODETYPEVALUE = dt42.Rows[j]["DICTVALUE"].ToString();
                    m1.TRANSFERMODETYPENAME = dt42.Rows[j]["DICTNAME"].ToString();
                    m1.DICTTRANSFERMODETYPECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountMONITORINGSTATIONByOrgNo(dtMONITORINGSTATION, sw.TopORGNO, m1.TRANSFERMODETYPEVALUE, "3");
                    TRANSFERMODETYPEResult1.Add(m1);
                }
                mm.TRANSFERMODETYPECountModel = TRANSFERMODETYPEResult1;
                result.Add(mm);
            }
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DCMONITORINGSTATIONCount_Model m = new DCMONITORINGSTATIONCount_Model();
                m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                m.BYORGNO = dtOrg.Rows[i]["ORGNO"].ToString();
                if (PublicCls.OrgIsZhen(m.BYORGNO) == false && m.BYORGNO == sw.TopORGNO)
                {
                    m.USESTATECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountXSByOrgNo(dtMONITORINGSTATION, m.BYORGNO, "", "1");//统计使用现状总数
                    m.MANAGERSTATECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountXSByOrgNo(dtMONITORINGSTATION, m.BYORGNO, "", "2");//统计维护类型
                    m.TRANSFERMODETYPECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountXSByOrgNo(dtMONITORINGSTATION, m.BYORGNO, "", "3");//无线电传输方式
                    var MANAGERSTATEResult = new List<MANAGERSTATECountModel>();
                    //循环维护类型
                    for (int j = 0; j < dt37.Rows.Count; j++)
                    {
                        MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                        m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                        m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                        m1.MANAGERSTATCount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountXSByOrgNo(dtMONITORINGSTATION, m.BYORGNO, m1.MANAGERSTATVALUE, "2");
                        MANAGERSTATEResult.Add(m1);
                    }
                    m.MANAGERSTATECountModel = MANAGERSTATEResult;
                    var USESTATEResult = new List<USESTATECountModel>();
                    //循环使用类型
                    for (int j = 0; j < dt36.Rows.Count; j++)
                    {
                        USESTATECountModel m1 = new USESTATECountModel();
                        m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                        m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                        m1.USESTATECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountXSByOrgNo(dtMONITORINGSTATION, m.BYORGNO, m1.USESTATEVALUE, "1");
                        USESTATEResult.Add(m1);
                    }
                    m.USESTATECountModel = USESTATEResult;
                    var TRANSFERMODETYPEResult = new List<TRANSFERMODETYPECountModel>();
                    //循环通讯方式类型
                    for (int j = 0; j < dt42.Rows.Count; j++)
                    {
                        TRANSFERMODETYPECountModel m1 = new TRANSFERMODETYPECountModel();
                        m1.TRANSFERMODETYPEVALUE = dt42.Rows[j]["DICTVALUE"].ToString();
                        m1.TRANSFERMODETYPENAME = dt42.Rows[j]["DICTNAME"].ToString();
                        m1.DICTTRANSFERMODETYPECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountXSByOrgNo(dtMONITORINGSTATION, m.BYORGNO, m1.TRANSFERMODETYPEVALUE, "3");
                        TRANSFERMODETYPEResult.Add(m1);
                    }
                    m.TRANSFERMODETYPECountModel = TRANSFERMODETYPEResult;
                    result.Add(m);
                }
                else
                {
                    m.USESTATECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountMONITORINGSTATIONByOrgNo(dtMONITORINGSTATION, m.BYORGNO, "", "1");//统计使用现状总数
                    m.MANAGERSTATECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountMONITORINGSTATIONByOrgNo(dtMONITORINGSTATION, m.BYORGNO, "", "2");//统计维护类型
                    m.TRANSFERMODETYPECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountMONITORINGSTATIONByOrgNo(dtMONITORINGSTATION, m.BYORGNO, "", "3");//无线电传输方式
                    var MANAGERSTATEResult = new List<MANAGERSTATECountModel>();
                    //循环维护类型
                    for (int j = 0; j < dt37.Rows.Count; j++)
                    {
                        MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                        m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                        m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                        m1.MANAGERSTATCount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountMONITORINGSTATIONByOrgNo(dtMONITORINGSTATION, m.BYORGNO, m1.MANAGERSTATVALUE, "2");
                        MANAGERSTATEResult.Add(m1);
                    }
                    m.MANAGERSTATECountModel = MANAGERSTATEResult;
                    var USESTATEResult = new List<USESTATECountModel>();
                    //循环使用类型
                    for (int j = 0; j < dt36.Rows.Count; j++)
                    {
                        USESTATECountModel m1 = new USESTATECountModel();
                        m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                        m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                        m1.USESTATECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountMONITORINGSTATIONByOrgNo(dtMONITORINGSTATION, m.BYORGNO, m1.USESTATEVALUE, "1");
                        USESTATEResult.Add(m1);
                    }
                    m.USESTATECountModel = USESTATEResult;
                    var TRANSFERMODETYPEResult = new List<TRANSFERMODETYPECountModel>();
                    //循环通讯方式类型
                    for (int j = 0; j < dt42.Rows.Count; j++)
                    {
                        TRANSFERMODETYPECountModel m1 = new TRANSFERMODETYPECountModel();
                        m1.TRANSFERMODETYPEVALUE = dt42.Rows[j]["DICTVALUE"].ToString();
                        m1.TRANSFERMODETYPENAME = dt42.Rows[j]["DICTNAME"].ToString();
                        m1.DICTTRANSFERMODETYPECount = BaseDT.DC_UTILITY_MONITORINGSTATION.getCountMONITORINGSTATIONByOrgNo(dtMONITORINGSTATION, m.BYORGNO, m1.TRANSFERMODETYPEVALUE, "3");
                        TRANSFERMODETYPEResult.Add(m1);
                    }
                    m.TRANSFERMODETYPECountModel = TRANSFERMODETYPEResult;
                    result.Add(m);
                }
            }

            dtMONITORINGSTATION.Clear();
            dtMONITORINGSTATION.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }
        #endregion

        #region 因子采集站统计
        /// <summary>
        /// 因子采集站统计
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="USESTATECountModel">使用现状</param>
        /// <param name="MANAGERSTATECountModel">维护类型</param>
        /// <param name="TRANSFERMODETYPECountModel">无线电传输方式</param>
        /// <returns></returns>
        public static IEnumerable<DCFACTORCOLLECTSTATIONCount_Model> getFACTORCOLLECTSTATIONModelCount(DC_UTILITY_FACTORCOLLECTSTATION_SW sw,
          out IEnumerable<USESTATECountModel> USESTATECountModel, out IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel, out IEnumerable<TRANSFERMODETYPECountModel> TRANSFERMODETYPECountModel)
        {
            var result = new List<DCFACTORCOLLECTSTATIONCount_Model>();
            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.TopEchartORGNO = sw.TopORGNO;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt42 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "42" });//无线电传输方式
            DataTable dtFACTORCOLLECTSTATION = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getDT(sw);
            //out 类别
            var USESTATE = new List<USESTATECountModel>();
            for (int j = 0; j < dt36.Rows.Count; j++)
            {
                USESTATECountModel m = new USESTATECountModel();
                m.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                m.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                USESTATE.Add(m);
            }
            USESTATECountModel = USESTATE;
            var MANAGERSTATE = new List<MANAGERSTATECountModel>();
            for (int j = 0; j < dt37.Rows.Count; j++)
            {
                MANAGERSTATECountModel m = new MANAGERSTATECountModel();
                m.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                m.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                MANAGERSTATE.Add(m);
            }
            MANAGERSTATECountModel = MANAGERSTATE;
            var TRANSFERMODETYPE = new List<TRANSFERMODETYPECountModel>();
            for (int j = 0; j < dt42.Rows.Count; j++)
            {
                TRANSFERMODETYPECountModel m = new TRANSFERMODETYPECountModel();
                m.TRANSFERMODETYPEVALUE = dt42.Rows[j]["DICTVALUE"].ToString();
                m.TRANSFERMODETYPENAME = dt42.Rows[j]["DICTNAME"].ToString();
                TRANSFERMODETYPE.Add(m);
            }
            TRANSFERMODETYPECountModel = TRANSFERMODETYPE;
            if (PublicCls.OrgIsZhen(sw.TopORGNO) == false)
            {
                DCFACTORCOLLECTSTATIONCount_Model mm = new DCFACTORCOLLECTSTATIONCount_Model();
                mm.ORGName = BaseDT.T_SYS_ORG.getName(sw.TopORGNO) + "合计";
                mm.BYORGNO = sw.TopORGNO;
                mm.USESTATECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountFACTORCOLLECTSTATIONByOrgNo(dtFACTORCOLLECTSTATION, sw.TopORGNO, "", "1");//统计使用现状总数
                mm.MANAGERSTATECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountFACTORCOLLECTSTATIONByOrgNo(dtFACTORCOLLECTSTATION, sw.TopORGNO, "", "2");//统计维护类型
                mm.TRANSFERMODETYPECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountFACTORCOLLECTSTATIONByOrgNo(dtFACTORCOLLECTSTATION, sw.TopORGNO, "", "3");//无线电传输方式
                var MANAGERSTATEResult1 = new List<MANAGERSTATECountModel>();
                //循环维护类型
                for (int j = 0; j < dt37.Rows.Count; j++)
                {
                    MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                    m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                    m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                    m1.MANAGERSTATCount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountFACTORCOLLECTSTATIONByOrgNo(dtFACTORCOLLECTSTATION, sw.TopORGNO, m1.MANAGERSTATVALUE, "2");
                    MANAGERSTATEResult1.Add(m1);
                }
                mm.MANAGERSTATECountModel = MANAGERSTATEResult1;
                var USESTATEResult1 = new List<USESTATECountModel>();
                //循环使用类型
                for (int j = 0; j < dt36.Rows.Count; j++)
                {
                    USESTATECountModel m1 = new USESTATECountModel();
                    m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                    m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                    m1.USESTATECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountFACTORCOLLECTSTATIONByOrgNo(dtFACTORCOLLECTSTATION, sw.TopORGNO, m1.USESTATEVALUE, "1");
                    USESTATEResult1.Add(m1);
                }
                mm.USESTATECountModel = USESTATEResult1;
                var TRANSFERMODETYPEResult1 = new List<TRANSFERMODETYPECountModel>();
                //循环通讯方式类型
                for (int j = 0; j < dt42.Rows.Count; j++)
                {
                    TRANSFERMODETYPECountModel m1 = new TRANSFERMODETYPECountModel();
                    m1.TRANSFERMODETYPEVALUE = dt42.Rows[j]["DICTVALUE"].ToString();
                    m1.TRANSFERMODETYPENAME = dt42.Rows[j]["DICTNAME"].ToString();
                    m1.DICTTRANSFERMODETYPECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountFACTORCOLLECTSTATIONByOrgNo(dtFACTORCOLLECTSTATION, sw.TopORGNO, m1.TRANSFERMODETYPEVALUE, "3");
                    TRANSFERMODETYPEResult1.Add(m1);
                }
                mm.TRANSFERMODETYPECountModel = TRANSFERMODETYPEResult1;
                result.Add(mm);
            }
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DCFACTORCOLLECTSTATIONCount_Model m = new DCFACTORCOLLECTSTATIONCount_Model();
                m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                m.BYORGNO = dtOrg.Rows[i]["ORGNO"].ToString();
                if (PublicCls.OrgIsZhen(m.BYORGNO) == false && m.BYORGNO == sw.TopORGNO)
                {
                    m.USESTATECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountXSByOrgNo(dtFACTORCOLLECTSTATION, m.BYORGNO, "", "1");//统计使用现状总数
                    m.MANAGERSTATECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountXSByOrgNo(dtFACTORCOLLECTSTATION, m.BYORGNO, "", "2");//统计维护类型
                    m.TRANSFERMODETYPECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountXSByOrgNo(dtFACTORCOLLECTSTATION, m.BYORGNO, "", "3");//无线电传输方式
                    var MANAGERSTATEResult = new List<MANAGERSTATECountModel>();
                    //循环维护类型
                    for (int j = 0; j < dt37.Rows.Count; j++)
                    {
                        MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                        m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                        m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                        m1.MANAGERSTATCount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountXSByOrgNo(dtFACTORCOLLECTSTATION, m.BYORGNO, m1.MANAGERSTATVALUE, "2");
                        MANAGERSTATEResult.Add(m1);
                    }
                    m.MANAGERSTATECountModel = MANAGERSTATEResult;
                    var USESTATEResult = new List<USESTATECountModel>();
                    //循环使用类型
                    for (int j = 0; j < dt36.Rows.Count; j++)
                    {
                        USESTATECountModel m1 = new USESTATECountModel();
                        m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                        m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                        m1.USESTATECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountXSByOrgNo(dtFACTORCOLLECTSTATION, m.BYORGNO, m1.USESTATEVALUE, "1");
                        USESTATEResult.Add(m1);
                    }
                    m.USESTATECountModel = USESTATEResult;
                    var TRANSFERMODETYPEResult = new List<TRANSFERMODETYPECountModel>();
                    //循环通讯方式类型
                    for (int j = 0; j < dt42.Rows.Count; j++)
                    {
                        TRANSFERMODETYPECountModel m1 = new TRANSFERMODETYPECountModel();
                        m1.TRANSFERMODETYPEVALUE = dt42.Rows[j]["DICTVALUE"].ToString();
                        m1.TRANSFERMODETYPENAME = dt42.Rows[j]["DICTNAME"].ToString();
                        m1.DICTTRANSFERMODETYPECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountXSByOrgNo(dtFACTORCOLLECTSTATION, m.BYORGNO, m1.TRANSFERMODETYPEVALUE, "3");
                        TRANSFERMODETYPEResult.Add(m1);
                    }
                    m.TRANSFERMODETYPECountModel = TRANSFERMODETYPEResult;
                    result.Add(m);
                }
                else
                {
                    m.USESTATECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountFACTORCOLLECTSTATIONByOrgNo(dtFACTORCOLLECTSTATION, m.BYORGNO, "", "1");//统计使用现状总数
                    m.MANAGERSTATECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountFACTORCOLLECTSTATIONByOrgNo(dtFACTORCOLLECTSTATION, m.BYORGNO, "", "2");//统计维护类型
                    m.TRANSFERMODETYPECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountFACTORCOLLECTSTATIONByOrgNo(dtFACTORCOLLECTSTATION, m.BYORGNO, "", "3");//无线电传输方式
                    var MANAGERSTATEResult = new List<MANAGERSTATECountModel>();
                    //循环维护类型
                    for (int j = 0; j < dt37.Rows.Count; j++)
                    {
                        MANAGERSTATECountModel m1 = new MANAGERSTATECountModel();
                        m1.MANAGERSTATVALUE = dt37.Rows[j]["DICTVALUE"].ToString();
                        m1.MANAGERSTATNAME = dt37.Rows[j]["DICTNAME"].ToString();
                        m1.MANAGERSTATCount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountFACTORCOLLECTSTATIONByOrgNo(dtFACTORCOLLECTSTATION, m.BYORGNO, m1.MANAGERSTATVALUE, "2");
                        MANAGERSTATEResult.Add(m1);
                    }
                    m.MANAGERSTATECountModel = MANAGERSTATEResult;
                    var USESTATEResult = new List<USESTATECountModel>();
                    //循环使用类型
                    for (int j = 0; j < dt36.Rows.Count; j++)
                    {
                        USESTATECountModel m1 = new USESTATECountModel();
                        m1.USESTATEVALUE = dt36.Rows[j]["DICTVALUE"].ToString();
                        m1.USESTATENAME = dt36.Rows[j]["DICTNAME"].ToString();
                        m1.USESTATECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountFACTORCOLLECTSTATIONByOrgNo(dtFACTORCOLLECTSTATION, m.BYORGNO, m1.USESTATEVALUE, "1");
                        USESTATEResult.Add(m1);
                    }
                    m.USESTATECountModel = USESTATEResult;
                    var TRANSFERMODETYPEResult = new List<TRANSFERMODETYPECountModel>();
                    //循环通讯方式类型
                    for (int j = 0; j < dt42.Rows.Count; j++)
                    {
                        TRANSFERMODETYPECountModel m1 = new TRANSFERMODETYPECountModel();
                        m1.TRANSFERMODETYPEVALUE = dt42.Rows[j]["DICTVALUE"].ToString();
                        m1.TRANSFERMODETYPENAME = dt42.Rows[j]["DICTNAME"].ToString();
                        m1.DICTTRANSFERMODETYPECount = BaseDT.DC_UTILITY_FACTORCOLLECTSTATION.getCountFACTORCOLLECTSTATIONByOrgNo(dtFACTORCOLLECTSTATION, m.BYORGNO, m1.TRANSFERMODETYPEVALUE, "3");
                        TRANSFERMODETYPEResult.Add(m1);
                    }
                    m.TRANSFERMODETYPECountModel = TRANSFERMODETYPEResult;
                    result.Add(m);
                }
            }
            dtFACTORCOLLECTSTATION.Clear();
            dtFACTORCOLLECTSTATION.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }
        #endregion

        #endregion

    }
}
