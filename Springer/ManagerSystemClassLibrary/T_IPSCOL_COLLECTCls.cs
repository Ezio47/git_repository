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
    /// 数据采集操作类
    /// </summary>
    public class T_IPSCOL_COLLECTCls
    {

        #region 删除、处理

        /// <summary>
        /// 删除、处理
        /// </summary>
        /// <param name="m">参见模型T_IPSCOL_COLLECT_Model</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(T_IPSCOL_COLLECT_Model m)
        {
            if (m.opMethod == "Del")
            {
                SystemCls.LogSave("5", "采集删除序号:" + m.COLLECTID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_IPSCOL_COLLECT.Del(new T_IPSCOL_COLLECT_SW { COLLECTID = m.COLLECTID });

                return new Message(msgUser.Success, msgUser.Msg, "");

            }
            if (m.opMethod == "DelDetail")
            {
                SystemCls.LogSave("5", "采集明细删除序号:" + m.COLLECTDETAILID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_IPSCOL_COLLECT.DelDetail(new T_IPSCOL_COLLECT_SW { COLLECTDETAILID = m.COLLECTDETAILID });

                return new Message(msgUser.Success, msgUser.Msg, "");

            }
            if (m.opMethod == "ModifyDetail")
            {
                SystemCls.LogSave("9", "数据采集坐标点修改" + m.COLLECTID, ClsStr.getModelContent(m));
                Message msg = BaseDT.T_IPSCOL_COLLECT.DelDetail(new T_IPSCOL_COLLECT_SW { COLLECTID = m.COLLECTID });
                if (msg.Success == true)
                {
                    Message msgUser = null;
                    foreach (var item in m.DataList)
                    {
                        msgUser = BaseDT.T_IPSCOL_COLLECT.AddDetail(item);
                    }
                    return new Message(msgUser.Success, "数据采集坐标点修改成功！", "");
                }
                return new Message(msg.Success, "数据采集坐标点修改失败！", "");
            }
            if (m.opMethod == "DelUpload")
            {
                SystemCls.LogSave("5", "采集文件删除序号:" + m.COLLECTUPLOADID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_IPSCOL_COLLECT.DelUpload(new T_IPSCOL_COLLECT_SW { COLLECTUPLOADID = m.COLLECTUPLOADID });

                return new Message(msgUser.Success, msgUser.Msg, "");

            }
            if (m.opMethod == "Man")
            {
                SystemCls.LogSave("4", "采集处理:" + m.COLLECTDETAILID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_IPSCOL_COLLECT.Man(m);

                return new Message(msgUser.Success, msgUser.Msg, "");
            }

            return new Message(false, "无效操作", "");


        }
        #endregion

        #region 根据ID获取采集数据
        /// <summary>
        /// 根据ID获取采集数据
        /// </summary>
        /// <example>
        /// sw.MANSTATE       处理状态 0未处理1已处理
        /// sw.DateBegin    开始日期
        /// sw.DateEnd      结束日期
        /// </example>
        /// <param name="sw">参见条件模型T_IPSCOL_COLLECT_SW</param>
        /// <returns>参见模型T_IPSCOL_COLLECT_Model</returns>
        public static T_IPSCOL_COLLECT_Model getModel(T_IPSCOL_COLLECT_SW sw)
        {
            DataTable dt = BaseDT.T_IPSCOL_COLLECT.getDT(sw);
            T_IPSCOL_COLLECT_Model m = new T_IPSCOL_COLLECT_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0; m.COLLECTID = dt.Rows[i]["COLLECTID"].ToString();
                m.HID = dt.Rows[i]["HID"].ToString();
                m.SYSTYPEVALUE = dt.Rows[i]["SYSTYPEVALUE"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.COLLECTTIME = ClsSwitch.SwitTM(dt.Rows[i]["COLLECTTIME"].ToString());
                m.COLLECTNAME = dt.Rows[i]["COLLECTNAME"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = ClsSwitch.SwitTM(dt.Rows[i]["MANTIME"].ToString());
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();

                DataTable dtHRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { HID = m.HID });
                if (dtHRUser.Rows.Count > 0)
                {
                    m.HName = dtHRUser.Rows[0]["HNAME"].ToString();
                    m.Phone = dtHRUser.Rows[0]["PHONE"].ToString();

                }
                dtHRUser.Clear();
                dtHRUser.Dispose();

                if (!string.IsNullOrEmpty(m.MANUSERID))
                {
                    DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { USERID = m.MANUSERID });
                    DataRow[] drUser = dtUser.Select("USERID='" + m.MANUSERID + "'");
                    if (drUser.Length > 0)
                    {
                        m.ManUserName = drUser[0]["USERNAME"].ToString();
                    }
                    dtUser.Clear();
                    dtUser.Dispose();

                }


            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion

        #region 根据ID获取采集文件
        /// <summary>
        /// 根据ID获取采集文件
        /// </summary>
        /// <param name="sw">sw.COLLECTID</param>
        /// <returns>参见模型T_IPSCOL_COLLECT_Model</returns>
        public static IEnumerable<T_IPSCOL_COLLECT_Model> getUploadlModelList(T_IPSCOL_COLLECT_SW sw)
        {
            var result = new List<T_IPSCOL_COLLECT_Model>();
            DataTable dt = BaseDT.T_IPSCOL_COLLECT.getUploadDT(new T_IPSCOL_COLLECT_SW { COLLECTID = sw.COLLECTID });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_IPSCOL_COLLECT_Model m = new T_IPSCOL_COLLECT_Model();
                m.COLLECTUPLOADID = dt.Rows[i]["COLLECTUPLOADID"].ToString();
                m.COLLECTID = dt.Rows[i]["COLLECTID"].ToString();
                m.UPLOADURL = dt.Rows[i]["UPLOADURL"].ToString();
                if (!string.IsNullOrEmpty(m.UPLOADURL))
                {
                    var wcfservice = System.Configuration.ConfigurationManager.AppSettings["SpringerWcfService"];//wcf服务地址
                    if (!string.IsNullOrEmpty(wcfservice))
                    {
                        m.UPLOADURL = wcfservice + m.UPLOADURL;
                    }
                }
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
        /// <param name="sw">sw.COLLECTID</param>
        /// <returns>参见模型T_IPSCOL_COLLECT_Model</returns>
        public static IEnumerable<T_IPSCOL_COLLECT_Model> getDetailModelList(T_IPSCOL_COLLECT_SW sw)
        {
            var result = new List<T_IPSCOL_COLLECT_Model>();
            DataTable dt = BaseDT.T_IPSCOL_COLLECT.getDetailDT(new T_IPSCOL_COLLECT_SW { COLLECTID = sw.COLLECTID });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_IPSCOL_COLLECT_Model m = new T_IPSCOL_COLLECT_Model();

                m.COLLECTDETAILID = dt.Rows[i]["COLLECTDETAILID"].ToString();
                m.COLLECTID = dt.Rows[i]["COLLECTID"].ToString();
                m.LONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();
                m.LATITUDE = dt.Rows[i]["LATITUDE"].ToString();
                m.ORILONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();//原始经度
                m.ORILATITUDE = dt.Rows[i]["LATITUDE"].ToString();//原始纬度
                //******************计算坐标偏移量
                if (sw.MapType != "Skyline")
                {
                    string[] arr = PublicCls.switJWD(m.LATITUDE, m.LONGITUDE);
                    m.LATITUDE = arr[0];
                    m.LONGITUDE = arr[1];
                }
                //******************计算坐标偏移量 End
                m.HEIGHT = dt.Rows[i]["HEIGHT"].ToString();
                m.DIRECTION = dt.Rows[i]["DIRECTION"].ToString();
                m.COLLECTTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["COLLECTTIME"].ToString());


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
        /// sw.orgNo            机构编码，用于获取该机构编码下所有的采集信息
        /// sw.MANSTATE       处理状态 0未处理1已处理
        /// sw.DateBegin    开始日期
        /// sw.DateEnd      结束日期
        /// </example>
        /// <param name="sw">参见条件模型T_IPSCOL_COLLECT_SW</param>
        /// <returns>参见模型T_IPSCOL_COLLECT_Model</returns>
        public static IEnumerable<T_IPSCOL_COLLECT_Model> getModelList(T_IPSCOL_COLLECT_SW sw)
        {
            DataTable dtHRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { BYORGNO = sw.orgNo });
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            DataTable dtCollectType = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "数据采集" });
            var result = new List<T_IPSCOL_COLLECT_Model>();
            DataTable dt = null;
            if (sw.UnionHUser)
            {
                dt = BaseDT.T_IPSCOL_COLLECT.getDtUnionHUser(sw); //关联护林员表
            }
            else
            {
                dt = BaseDT.T_IPSCOL_COLLECT.getDT(sw);
            }
            string IDList = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                    IDList += ",";
                IDList += dt.Rows[i]["COLLECTID"].ToString();
            }
            DataTable dtDetail = BaseDT.T_IPSCOL_COLLECT.getDetailDT(new T_IPSCOL_COLLECT_SW { COLLECTID = IDList });
            DataTable dtUpload = BaseDT.T_IPSCOL_COLLECT.getUploadDT(new T_IPSCOL_COLLECT_SW { COLLECTID = IDList });

            //var userid = SystemCls.getUserID();
            //var rightsw = new T_SYSSEC_IPSUSER_SW();
            //rightsw.USERID = userid;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_IPSCOL_COLLECT_Model m = new T_IPSCOL_COLLECT_Model();

                m.HID = dt.Rows[i]["HID"].ToString();
                m.COLLECTID = dt.Rows[i]["COLLECTID"].ToString();
                m.SYSTYPEVALUE = dt.Rows[i]["SYSTYPEVALUE"].ToString();
                m.SYSTYPEName = BaseDT.T_SYS_DICT.getName(dtCollectType, m.SYSTYPEVALUE);
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.COLLECTTIME = ClsSwitch.SwitTM(dt.Rows[i]["COLLECTTIME"].ToString());
                m.COLLECTNAME = dt.Rows[i]["COLLECTNAME"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = ClsSwitch.SwitTM(dt.Rows[i]["MANTIME"].ToString());
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();
                DataRow[] drDetail = dtDetail.Select("COLLECTID='" + m.COLLECTID + "'", "COLLECTTIME");
                if (drDetail.Length > 0)
                {

                    m.COLLECTDETAILID = drDetail[0]["COLLECTDETAILID"].ToString();
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
                    // m.COLLECTTIME = ClsSwitch.SwitTM(drDetail[0]["COLLECTTIME"].ToString());

                }

                DataRow[] drUpload = dtUpload.Select("COLLECTID='" + m.COLLECTID + "'", "COLLECTUPLOADID");
                if (drUpload.Length > 0)
                {
                    m.COLLECTUPLOADID = drUpload[0]["COLLECTUPLOADID"].ToString();
                    m.UPLOADURL = drUpload[0]["UPLOADURL"].ToString();
                    m.UPLOADNAME = drUpload[0]["UPLOADNAME"].ToString();
                    m.UPLOADDESCRIBE = drUpload[0]["UPLOADDESCRIBE"].ToString();

                }

                DataRow[] drHRUser = dtHRUser.Select("HID='" + m.HID + "'");
                if (drHRUser.Length > 0)
                {
                    m.OrgNoName = drHRUser[0]["ORGNAME"].ToString();
                    m.HName = drHRUser[0]["HNAME"].ToString();
                    m.Phone = drHRUser[0]["PHONE"].ToString();
                    m.OrgNo = drHRUser[0]["BYORGNO"].ToString();
                    m.Phone = drHRUser[0]["PHONE"].ToString();
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
            dtCollectType.Clear();
            dtCollectType.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion


        #region 采集数据统计
        /// <summary>
        /// 采集数据统计
        /// </summary>
        /// <param name="sw">参见模型  sw.TopORGNO 顶级单位编码 sw.DateBegin 开始日期 年月日 sw.DateEnd 结束日期 年月日</param>
        /// <param name="typeModel">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_IPSCOL_COLLECT_OrgCountModel> getModelCount(T_IPSCOL_COLLECT_SW sw, out IEnumerable<T_IPSCOL_COLLECT_TypeCountModel> typeModel)
        {
            var result = new List<T_IPSCOL_COLLECT_OrgCountModel>();
            //获取采集信息
            DataTable dt = BaseDT.T_IPSCOL_COLLECT.getDTByOrgHUse(sw);
            //获取单位

            T_SYS_ORGSW swOrg = new T_SYS_ORGSW();
            swOrg.SYSFLAG = ConfigCls.getSystemFlag();
            swOrg.TopORGNO = sw.TopORGNO;

            if (PublicCls.OrgIsShi(sw.TopORGNO))
                swOrg.GetContyORGNOByCity = sw.TopORGNO;//获取所有县
            if (PublicCls.OrgIsXian(sw.TopORGNO))
                swOrg.GetXZOrgNOByConty = sw.TopORGNO;//获取所有镇
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(swOrg);
            //采集类别
            DataTable dtReportType = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "4" });//采集类别
            //out 类别
            var resultReportType = new List<T_IPSCOL_COLLECT_TypeCountModel>();
            for (int i = 0; i < dtReportType.Rows.Count; i++)
            {
                T_IPSCOL_COLLECT_TypeCountModel m = new T_IPSCOL_COLLECT_TypeCountModel();
                m.typeName = dtReportType.Rows[i]["DICTNAME"].ToString();
                resultReportType.Add(m);
            }
            typeModel = resultReportType;
            if (1 == 1)
            {
                T_IPSCOL_COLLECT_OrgCountModel m = new T_IPSCOL_COLLECT_OrgCountModel();

                m.HName = BaseDT.T_SYS_ORG.getName(sw.TopORGNO) + "合计";
                m.ORGName = m.HName;
                m.CollectCount = BaseDT.T_IPSRPT_REPORT.getCountByOrgHUse(dt, sw.TopORGNO, "");//总数
                var typeResult = new List<T_IPSCOL_COLLECT_TypeCountModel>();
                //循环类别
                for (int k = 0; k < dtReportType.Rows.Count; k++)
                {
                    T_IPSCOL_COLLECT_TypeCountModel mm = new T_IPSCOL_COLLECT_TypeCountModel();
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
                    T_IPSCOL_COLLECT_OrgCountModel m = new T_IPSCOL_COLLECT_OrgCountModel();
                    m.ORGName = dtOrg.Rows[i]["ORGNAME"].ToString();
                    m.ORGNo = dtOrg.Rows[i]["ORGNO"].ToString();
                    //m.HID = dt.Rows[i]["HID"].ToString();
                    m.CollectCount = BaseDT.T_IPSCOL_COLLECT.getCountByOrgHUse(dt, m.ORGNo, "");//总数
                    var typeResult = new List<T_IPSCOL_COLLECT_TypeCountModel>();
                    //循环类别
                    for (int k = 0; k < dtReportType.Rows.Count; k++)
                    {
                        T_IPSCOL_COLLECT_TypeCountModel mm = new T_IPSCOL_COLLECT_TypeCountModel();
                        mm.typeID = dtReportType.Rows[k]["DICTVALUE"].ToString();
                        mm.typeName = dtReportType.Rows[k]["DICTNAME"].ToString();
                        mm.typeCount = BaseDT.T_IPSCOL_COLLECT.getCountByOrgHUse(dt, m.ORGNo, mm.typeID);//分类别统计
                        typeResult.Add(mm);
                    }
                    m.TypeCountModel = typeResult;
                    result.Add(m);
                }
            }
            else//if (PublicCls.OrgIsZhen(sw.TopORGNO))//如果查询的单位为镇，显示所有护林员的统计信息
            {
                DataTable dtHUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW
                {
                    BYORGNO = sw.TopORGNO
                });
                for (int i = 0; i < dtHUser.Rows.Count; i++)
                {
                    T_IPSCOL_COLLECT_OrgCountModel m = new T_IPSCOL_COLLECT_OrgCountModel();
                    m.HID = dtHUser.Rows[i]["HID"].ToString();
                    m.HName = dtHUser.Rows[i]["HNAME"].ToString();

                    m.CollectCount = BaseDT.T_IPSCOL_COLLECT.getCountByHID(dt, m.HID, "");//总数
                    var typeResult = new List<T_IPSCOL_COLLECT_TypeCountModel>();
                    //循环类别
                    for (int k = 0; k < dtReportType.Rows.Count; k++)
                    {
                        T_IPSCOL_COLLECT_TypeCountModel mm = new T_IPSCOL_COLLECT_TypeCountModel();
                        mm.typeID = dtReportType.Rows[k]["DICTVALUE"].ToString();
                        mm.typeName = dtReportType.Rows[k]["DICTNAME"].ToString();
                        mm.typeCount = BaseDT.T_IPSCOL_COLLECT.getCountByHID(dt, m.HID, mm.typeID);//分类别统计
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

