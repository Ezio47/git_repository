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
    /// 数据上报操作类
    /// </summary>
    public class T_IPSRPT_REPORTCls
    {
        #region 删除、处理

        /// <summary>
        /// 删除、处理
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(T_IPSRPT_REPORT_Model m)
        {
            if (m.opMethod == "Del")
            {
                SystemCls.LogSave("5", "上报删除序号:" + m.REPORTID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_IPSRPT_REPORT.Del(new T_IPSRPT_REPORT_SW { REPORTID = m.REPORTID });

                return new Message(msgUser.Success, msgUser.Msg, "");

            }
            if (m.opMethod == "DelDetail")
            {
                SystemCls.LogSave("5", "上报明细删除序号:" + m.REPORTDETAILID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_IPSRPT_REPORT.DelDetail(new T_IPSRPT_REPORT_SW { REPORTDETAILID = m.REPORTDETAILID });

                return new Message(msgUser.Success, msgUser.Msg, "");

            }
            if (m.opMethod == "DelUpload")
            {
                SystemCls.LogSave("5", "上报文件删除序号:" + m.REPORTUPLOADID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_IPSRPT_REPORT.DelUpload(new T_IPSRPT_REPORT_SW { REPORTUPLOADID = m.REPORTUPLOADID });

                return new Message(msgUser.Success, msgUser.Msg, "");

            }
            if (m.opMethod == "Man")
            {
                SystemCls.LogSave("4", "上报处理:" + m.REPORTDETAILID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_IPSRPT_REPORT.Man(m);

                return new Message(msgUser.Success, msgUser.Msg, "");
            }

            return new Message(false, "无效操作", "");


        }
        #endregion

        #region 根据ID获取上传数据
        /// <summary>
        /// 根据ID获取上传数据
        /// </summary>
        /// <example>
        /// sw.MANSTATE       处理状态 0未处理1已处理
        /// sw.DateBegin    开始日期
        /// sw.DateEnd      结束日期
        /// </example>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_IPSRPT_REPORT_Model getModel(T_IPSRPT_REPORT_SW sw)
        {
            DataTable dt = BaseDT.T_IPSRPT_REPORT.getDT(sw);
            T_IPSRPT_REPORT_Model m = new T_IPSRPT_REPORT_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.REPORTID = dt.Rows[i]["REPORTID"].ToString();
                m.HID = dt.Rows[i]["HID"].ToString();
                m.SYSTYPEVALUE = dt.Rows[i]["SYSTYPEVALUE"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.REPORTTIME = ClsSwitch.SwitTM(dt.Rows[i]["REPORTTIME"].ToString());
                m.COLLECTNAME = dt.Rows[i]["COLLECTNAME"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = dt.Rows[i]["MANTIME"].ToString();
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();

            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion

        #region 根据ID获取上传文件
        /// <summary>
        /// 根据ID获取上传文件
        /// </summary>
        /// <param name="sw">sw.REPORTID</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_IPSRPT_REPORT_Model> getUploadlModelList(T_IPSRPT_REPORT_SW sw)
        {
            var result = new List<T_IPSRPT_REPORT_Model>();
            DataTable dt = BaseDT.T_IPSRPT_REPORT.getUploadDT(new T_IPSRPT_REPORT_SW { REPORTID = sw.REPORTID });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_IPSRPT_REPORT_Model m = new T_IPSRPT_REPORT_Model();
                m.REPORTUPLOADID = dt.Rows[i]["REPORTUPLOADID"].ToString();
                m.REPORTID = dt.Rows[i]["REPORTID"].ToString();
                m.UPLOADURL = dt.Rows[i]["UPLOADURL"].ToString();
                m.UPLOADNAME = dt.Rows[i]["UPLOADNAME"].ToString();
                m.UPLOADDESCRIBE = dt.Rows[i]["UPLOADDESCRIBE"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 根据ID获取明细
        /// <summary>
        /// 根据ID获取明细
        /// </summary>
        /// <param name="sw">sw.REPORTID</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_IPSRPT_REPORT_Model> getDetailModelList(T_IPSRPT_REPORT_SW sw)
        {
            var result = new List<T_IPSRPT_REPORT_Model>();
            DataTable dt = BaseDT.T_IPSRPT_REPORT.getDetailDT(new T_IPSRPT_REPORT_SW { REPORTID = sw.REPORTID });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_IPSRPT_REPORT_Model m = new T_IPSRPT_REPORT_Model();

                m.REPORTDETAILID = dt.Rows[i]["REPORTDETAILID"].ToString();
                m.REPORTID = dt.Rows[i]["REPORTID"].ToString();
                m.LONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();
                m.LATITUDE = dt.Rows[i]["LATITUDE"].ToString();
                //******************计算坐标偏移量
                string[] arr = PublicCls.switJWD(m.LATITUDE, m.LONGITUDE);
                m.LATITUDE = arr[0];
                m.LONGITUDE = arr[1];
                //******************计算坐标偏移量 End
                m.HEIGHT = dt.Rows[i]["HEIGHT"].ToString();
                m.DIRECTION = dt.Rows[i]["DIRECTION"].ToString();
                m.SBTIME = ClsSwitch.SwitTM(dt.Rows[i]["SBTIME"].ToString());



                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 获取多条数据
        /// <summary>
        /// 获取多条数据
        /// </summary>
        /// <example>
        /// sw.orgNo            机构编码，用于获取该机构编码下所有的上报信息
        /// sw.MANSTATE       处理状态 0未处理1已处理
        /// sw.DateBegin    开始日期
        /// sw.DateEnd      结束日期
        /// </example>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_IPSRPT_REPORT_Model> getModelList(T_IPSRPT_REPORT_SW sw)
        {
            DataTable dtHRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { BYORGNO = sw.orgNo });
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            var result = new List<T_IPSRPT_REPORT_Model>();
            DataTable dt = null;
            if (sw.UnionHUser)
            {
                dt = BaseDT.T_IPSRPT_REPORT.getDtUnionHUser(sw); //关联护林员表
            }
            else
            {
                dt = BaseDT.T_IPSRPT_REPORT.getDT(sw);
            }
            string IDList = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                    IDList += ",";
                IDList += dt.Rows[i]["REPORTID"].ToString();
            }

            //var userid = SystemCls.getUserID();
            //var rightsw = new T_SYSSEC_IPSUSER_SW();
            //rightsw.USERID = userid;

            DataTable dtDetail = BaseDT.T_IPSRPT_REPORT.getDetailDT(new T_IPSRPT_REPORT_SW { REPORTID = IDList });
            DataTable dtUpload = BaseDT.T_IPSRPT_REPORT.getUploadDT(new T_IPSRPT_REPORT_SW { REPORTID = IDList });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_IPSRPT_REPORT_Model m = new T_IPSRPT_REPORT_Model();
                m.REPORTID = dt.Rows[i]["REPORTID"].ToString();
                m.HID = dt.Rows[i]["HID"].ToString();
                m.SYSTYPEVALUE = dt.Rows[i]["SYSTYPEVALUE"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.REPORTTIME = ClsSwitch.SwitTM(dt.Rows[i]["REPORTTIME"].ToString());
                m.COLLECTNAME = dt.Rows[i]["COLLECTNAME"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = dt.Rows[i]["MANTIME"].ToString();
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();
                DataRow[] drDetail = dtDetail.Select("REPORTID='" + m.REPORTID + "'", "SBTIME");
                if (drDetail.Length > 0)
                {
                    m.REPORTDETAILID = drDetail[0]["REPORTDETAILID"].ToString();
                    m.LONGITUDE = drDetail[0]["LONGITUDE"].ToString();
                    m.LATITUDE = drDetail[0]["LATITUDE"].ToString();
                    if (sw.MapType != "Skyline")
                    {
                        //******************计算坐标偏移量
                        string[] arr = PublicCls.switJWD(m.LATITUDE, m.LONGITUDE);
                        m.LATITUDE = arr[0];
                        m.LONGITUDE = arr[1];
                        //******************计算坐标偏移量 End
                    }

                    m.HEIGHT = drDetail[0]["HEIGHT"].ToString();
                    m.DIRECTION = drDetail[0]["DIRECTION"].ToString();
                    m.SBTIME = ClsSwitch.SwitTM(drDetail[0]["SBTIME"].ToString());
                }

                DataRow[] drUpload = dtUpload.Select("REPORTID='" + m.REPORTID + "'", "REPORTUPLOADID");
                if (drUpload.Length > 0)
                {
                    m.REPORTUPLOADID = drUpload[0]["REPORTUPLOADID"].ToString();
                    m.UPLOADURL = drUpload[0]["UPLOADURL"].ToString();
                    if (!string.IsNullOrEmpty(m.UPLOADURL))
                    {
                        var wcfservice = System.Configuration.ConfigurationManager.AppSettings["SpringerWcfService"];//wcf服务地址
                        if (!string.IsNullOrEmpty(wcfservice))
                        {
                            m.UPLOADURL = wcfservice + m.UPLOADURL;
                        };
                    }
                    m.UPLOADNAME = drUpload[0]["UPLOADNAME"].ToString();
                    m.UPLOADDESCRIBE = drUpload[0]["UPLOADDESCRIBE"].ToString();
                    m.UPLOADTYPE = drUpload[0]["UPLOADTYPE"].ToString();
                }

                DataRow[] drHRUser = dtHRUser.Select("HID='" + m.HID + "'");
                if (drHRUser.Length > 0)
                {
                    m.OrgNoName = drHRUser[0]["ORGNAME"].ToString();
                    m.HName = drHRUser[0]["HNAME"].ToString();
                    m.PHONE = drHRUser[0]["PHONE"].ToString();
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
            dtDetail.Clear();
            dtDetail.Dispose();
            dtUpload.Clear();
            dtUpload.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }




        #endregion

        #region 上报数据统计
        /// <summary>
        /// 上报数据统计
        /// </summary>
        /// <param name="sw">参见模型  sw.TopORGNO 顶级单位编码 sw.DateBegin 开始日期 年月日 sw.DateEnd 结束日期 年月日</param>
        /// <param name="typeModel">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_IPSRPT_REPORT_OrgCountModel> getModelCount(T_IPSRPT_REPORT_SW sw, out IEnumerable<T_IPSRPT_REPORT_TypeCountModel> typeModel)
        {
            var result = new List<T_IPSRPT_REPORT_OrgCountModel>();
            //获取上报信息
            DataTable dt = BaseDT.T_IPSRPT_REPORT.getDTByOrgHUse(sw);
            //获取单位

            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.SYSFLAG = ConfigCls.getSystemFlag();
            swOrg.TopORGNO = sw.TopORGNO;

            if (PublicCls.OrgIsShi(sw.TopORGNO))
                swOrg.GetContyORGNOByCity = sw.TopORGNO;//获取所有县
            if (PublicCls.OrgIsXian(sw.TopORGNO))
                swOrg.GetXZOrgNOByConty = sw.TopORGNO;//获取所有镇
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            //上报类别
            DataTable dtReportType = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "5" });//上报类别
            //out 类别
            var resultReportType = new List<T_IPSRPT_REPORT_TypeCountModel>();
            for (int i = 0; i < dtReportType.Rows.Count; i++)
            {
                T_IPSRPT_REPORT_TypeCountModel m = new T_IPSRPT_REPORT_TypeCountModel();
                m.typeName = dtReportType.Rows[i]["DICTNAME"].ToString();
                resultReportType.Add(m);
            }
            typeModel = resultReportType;
            if (1 == 1)
            {
                T_IPSRPT_REPORT_OrgCountModel m = new T_IPSRPT_REPORT_OrgCountModel();

                m.HName =BaseDT.T_SYS_ORG.getName(sw.TopORGNO)+ "合计";
                m.ORGName = m.HName;
                m.ReportCount = BaseDT.T_IPSRPT_REPORT.getCountByOrgHUse(dt, sw.TopORGNO, "");//总数
                var typeResult = new List<T_IPSRPT_REPORT_TypeCountModel>();
                //循环类别
                for (int k = 0; k < dtReportType.Rows.Count; k++)
                {
                    T_IPSRPT_REPORT_TypeCountModel mm = new T_IPSRPT_REPORT_TypeCountModel();
                    mm.typeID = dtReportType.Rows[k]["DICTVALUE"].ToString();
                    mm.typeName = dtReportType.Rows[k]["DICTNAME"].ToString();
                    mm.typeCount = BaseDT.T_IPSRPT_REPORT.getCountByOrgHUse(dt, sw.TopORGNO, mm.typeID);//分类别统计
                    typeResult.Add(mm);
                }
                m.TypeCountModel = typeResult;
                result.Add(m);

            }
            if (PublicCls.OrgIsZhen(sw.TopORGNO) == false)
            {
                //循环单位
                for (int i = 0; i < dtOrg.Rows.Count; i++)
                {
                    T_IPSRPT_REPORT_OrgCountModel m = new T_IPSRPT_REPORT_OrgCountModel();
                    m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                    m.ORGNo = dtOrg.Rows[i]["ORGNO"].ToString();
                    //m.HID = dt.Rows[i]["HID"].ToString();
                    m.ReportCount = BaseDT.T_IPSRPT_REPORT.getCountByOrgHUse(dt, m.ORGNo, "");//总数
                    var typeResult = new List<T_IPSRPT_REPORT_TypeCountModel>();
                    //循环类别
                    for (int k = 0; k < dtReportType.Rows.Count; k++)
                    {
                        T_IPSRPT_REPORT_TypeCountModel mm = new T_IPSRPT_REPORT_TypeCountModel();
                        mm.typeID = dtReportType.Rows[k]["DICTVALUE"].ToString();
                        mm.typeName = dtReportType.Rows[k]["DICTNAME"].ToString();
                        mm.typeCount = BaseDT.T_IPSRPT_REPORT.getCountByOrgHUse(dt, m.ORGNo, mm.typeID);//分类别统计
                        typeResult.Add(mm);
                    }
                    m.TypeCountModel = typeResult;
                    result.Add(m);
                }
                //if(1==1) //合计
                //{
                //    T_IPSRPT_REPORT_OrgCountModel m = new T_IPSRPT_REPORT_OrgCountModel();
                //    m.ORGName = "合计";
                //    m.ORGNo = sw.TopORGNO;
                //    //m.HID = dt.Rows[i]["HID"].ToString();
                //    m.ReportCount = BaseDT.T_IPSRPT_REPORT.getCountByOrgHUse(dt, sw.TopORGNO, "");//总数
                //    var typeResult = new List<T_IPSRPT_REPORT_TypeCountModel>();
                //    //循环类别
                //    for (int k = 0; k < dtReportType.Rows.Count; k++)
                //    {
                //        T_IPSRPT_REPORT_TypeCountModel mm = new T_IPSRPT_REPORT_TypeCountModel();
                //        mm.typeID = dtReportType.Rows[k]["DICTVALUE"].ToString();
                //        mm.typeName = dtReportType.Rows[k]["DICTNAME"].ToString();
                //        mm.typeCount = BaseDT.T_IPSRPT_REPORT.getCountByOrgHUse(dt, sw.TopORGNO, mm.typeID);//分类别统计
                //        typeResult.Add(mm);
                //    }
                //    m.TypeCountModel = typeResult;
                //    result.Add(m);
                //}
            }
            else//如果查询的单位为镇，显示所有护林员的统计信息
            {
                DataTable dtHUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW
                {
                    BYORGNO = sw.TopORGNO
                });
                for (int i = 0; i < dtHUser.Rows.Count; i++)
                {
                    T_IPSRPT_REPORT_OrgCountModel m = new T_IPSRPT_REPORT_OrgCountModel();
                    m.HID = dtHUser.Rows[i]["HID"].ToString();
                    m.HName = dtHUser.Rows[i]["HNAME"].ToString();

                    m.ReportCount = BaseDT.T_IPSRPT_REPORT.getCountByHID(dt, m.HID, "");//总数
                    var typeResult = new List<T_IPSRPT_REPORT_TypeCountModel>();
                    //循环类别
                    for (int k = 0; k < dtReportType.Rows.Count; k++)
                    {
                        T_IPSRPT_REPORT_TypeCountModel mm = new T_IPSRPT_REPORT_TypeCountModel();
                        mm.typeID = dtReportType.Rows[k]["DICTVALUE"].ToString();
                        mm.typeName = dtReportType.Rows[k]["DICTNAME"].ToString();
                        mm.typeCount = BaseDT.T_IPSRPT_REPORT.getCountByHID(dt, m.HID, mm.typeID);//分类别统计
                        typeResult.Add(mm);
                    }
                    m.TypeCountModel = typeResult;
                    result.Add(m);

                }
                
                dtHUser.Clear();
                dtHUser.Dispose();
            } 
            dt.Clear();
            dt.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            dtReportType.Clear();
            dtReportType.Dispose();
            return result;
        }
        #endregion
    }
}
