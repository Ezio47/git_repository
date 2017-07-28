using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{

    /// <summary>
    /// 数据中心_资源_新
    /// </summary>
    public class DC_RESOURCE_NEW
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(DC_RESOURCE_NEW_Model m)
        {
            
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("INSERT INTO  DC_RESOURCE_NEW( RESOURCETYPE, NUMBER, NAME, ORGNOS, KINDTYPE, AGETYPE, ORIGINTYPE, AREA, BURNTYPE, TREETYPE, ASPECT, ANGLE, MARK, JD, WD,POTHOOKLEADER,POTHOOKLEADERJOB,POTHOOKLEADERTLEE,DUTYPERSON,DUTYPERSONTELL,JWDLIST) output inserted.DC_RESOURCE_NEW_ID");
                sb.AppendFormat(" VALUES(");
                sb.AppendFormat("{0}", ClsSql.saveNullField(m.RESOURCETYPE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.NUMBER));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ORGNOS));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.KINDTYPE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.AGETYPE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ORIGINTYPE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.AREA));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BURNTYPE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.TREETYPE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ASPECT));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ANGLE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MARK));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.POTHOOKLEADER));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.POTHOOKLEADERJOB));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.POTHOOKLEADERTLEE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DUTYPERSON));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DUTYPERSONTELL));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.JWDLIST));
                sb.AppendFormat(")");
                try
                {
                    string strid = DataBaseClass.ReturnSqlField(sb.ToString());
                    return new Message(true, "添加成功！", strid);
                }
                catch (Exception)
                {

                    throw;
                }
            
           
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(DC_RESOURCE_NEW_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE DC_RESOURCE_NEW");
            sb.AppendFormat(" set ");
            sb.AppendFormat("RESOURCETYPE={0}", ClsSql.saveNullField(m.RESOURCETYPE));
            sb.AppendFormat(",NUMBER={0}", ClsSql.saveNullField(m.NUMBER));
            sb.AppendFormat(",NAME={0}", ClsSql.saveNullField(m.NAME));
            sb.AppendFormat(",ORGNOS={0}", ClsSql.saveNullField(m.ORGNOS));
            sb.AppendFormat(",KINDTYPE={0}", ClsSql.saveNullField(m.KINDTYPE));
            sb.AppendFormat(",AGETYPE={0}", ClsSql.saveNullField(m.AGETYPE));
            sb.AppendFormat(",ORIGINTYPE={0}", ClsSql.saveNullField(m.ORIGINTYPE));
            sb.AppendFormat(",AREA={0}", ClsSql.saveNullField(m.AREA));
            sb.AppendFormat(",BURNTYPE={0}", ClsSql.saveNullField(m.BURNTYPE));
            sb.AppendFormat(",TREETYPE={0}", ClsSql.saveNullField(m.TREETYPE));
            sb.AppendFormat(",ASPECT={0}", ClsSql.saveNullField(m.ASPECT));
            sb.AppendFormat(",ANGLE={0}", ClsSql.saveNullField(m.ANGLE));
            sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(",POTHOOKLEADER={0}", ClsSql.saveNullField(m.POTHOOKLEADER));
            sb.AppendFormat(",POTHOOKLEADERJOB={0}", ClsSql.saveNullField(m.POTHOOKLEADERJOB));
            sb.AppendFormat(",POTHOOKLEADERTLEE={0}", ClsSql.saveNullField(m.POTHOOKLEADERTLEE));
            sb.AppendFormat(",DUTYPERSON={0}", ClsSql.saveNullField(m.DUTYPERSON));
            sb.AppendFormat(",DUTYPERSONTELL={0}", ClsSql.saveNullField(m.DUTYPERSONTELL));
            sb.AppendFormat(",JWDLIST={0}", ClsSql.saveNullField(m.JWDLIST));
            sb.AppendFormat(" where DC_RESOURCE_NEW_ID= '{0}'", ClsSql.EncodeSql(m.DC_RESOURCE_NEW_ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", m.DC_RESOURCE_NEW_ID);
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 修改经纬度
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message MdyJWD(DC_RESOURCE_NEW_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE DC_RESOURCE_NEW");
            sb.AppendFormat(" set ");
            sb.AppendFormat("JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(" where DC_RESOURCE_NEW_ID= '{0}'", ClsSql.EncodeSql(m.DC_RESOURCE_NEW_ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(DC_RESOURCE_NEW_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete DC_RESOURCE_NEW");
            sb.AppendFormat(" where DC_RESOURCE_NEW_ID= '{0}'", ClsSql.EncodeSql(m.DC_RESOURCE_NEW_ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(DC_RESOURCE_NEW_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from DC_RESOURCE_NEW where 1=1");
            if (string.IsNullOrEmpty(sw.DC_RESOURCE_NEW_ID) == false)
                sb.AppendFormat(" where DC_RESOURCE_NEW_ID= '{0}'", ClsSql.EncodeSql(sw.DC_RESOURCE_NEW_ID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_RESOURCE_NEW_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      DC_RESOURCE_NEW");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DC_RESOURCE_NEW_ID) == false)
                sb.AppendFormat(" AND DC_RESOURCE_NEW_ID = '{0}'", ClsSql.EncodeSql(sw.DC_RESOURCE_NEW_ID));
            if (string.IsNullOrEmpty(sw.RESOURCETYPE) == false)
                sb.AppendFormat(" AND RESOURCETYPE = '{0}'", ClsSql.EncodeSql(sw.RESOURCETYPE));
            if (string.IsNullOrEmpty(sw.NUMBER) == false)
                sb.AppendFormat(" AND NUMBER like '%{0}%'", ClsSql.EncodeSql(sw.NUMBER));
            if (string.IsNullOrEmpty(sw.NAME) == false)
                sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
            if (string.IsNullOrEmpty(sw.KINDTYPE) == false)
                sb.AppendFormat(" AND KINDTYPE = '{0}'", ClsSql.EncodeSql(sw.KINDTYPE));
            if (string.IsNullOrEmpty(sw.AGETYPE) == false)
                sb.AppendFormat(" AND AGETYPE = '{0}'", ClsSql.EncodeSql(sw.AGETYPE));
            if (string.IsNullOrEmpty(sw.ORIGINTYPE) == false)
                sb.AppendFormat(" AND ORIGINTYPE = '{0}'", ClsSql.EncodeSql(sw.ORIGINTYPE));
            if (string.IsNullOrEmpty(sw.BURNTYPE) == false)
                sb.AppendFormat(" AND BURNTYPE = '{0}'", ClsSql.EncodeSql(sw.BURNTYPE));
            if (string.IsNullOrEmpty(sw.TREETYPE) == false)
                sb.AppendFormat(" AND TREETYPE = '{0}'", ClsSql.EncodeSql(sw.TREETYPE));
            if (!string.IsNullOrEmpty(sw.ORGNOS))
            {
                if (sw.ORGNOSXZ != "1")
                {
                    if (sw.ORGNOS.Substring(4, 5) == "00000")//获取所有市的
                        sb.AppendFormat(" AND (SUBSTRING(ORGNOS,1,4) = '{0}' or ORGNOS is null or ORGNOS='')", ClsSql.EncodeSql(sw.ORGNOS.Substring(0, 4)));
                    else if (sw.ORGNOS.Substring(6, 3) == "000")//获取所有县的
                        sb.AppendFormat(" AND (SUBSTRING(ORGNOS,1,6) = '{0}' or ORGNOS is null or ORGNOS='')", ClsSql.EncodeSql(sw.ORGNOS.Substring(0, 6)));
                    else
                        sb.AppendFormat(" AND ORGNOS = '{0}'", ClsSql.EncodeSql(sw.ORGNOS));
                }
                else 
                {
                    sb.AppendFormat(" AND ORGNOS = '{0}'", ClsSql.EncodeSql(sw.ORGNOS));
                }
            }
            string sql = "SELECT DC_RESOURCE_NEW_ID, RESOURCETYPE, NUMBER, NAME, ORGNOS, KINDTYPE, AGETYPE, ORIGINTYPE, AREA, BURNTYPE, TREETYPE, ASPECT, ANGLE, MARK, JD, WD,POTHOOKLEADER,POTHOOKLEADERJOB,POTHOOKLEADERTLEE,DUTYPERSON,DUTYPERSONTELL,JWDLIST"
            + sb.ToString()
            + " order by ORGNOS,DC_RESOURCE_NEW_ID desc";

            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion
                
      
        #region 获取分页列表
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_RESOURCE_NEW_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      DC_RESOURCE_NEW");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DC_RESOURCE_NEW_ID) == false)
                sb.AppendFormat(" AND DC_RESOURCE_NEW_ID = '{0}'", ClsSql.EncodeSql(sw.DC_RESOURCE_NEW_ID));
            if (string.IsNullOrEmpty(sw.RESOURCETYPE) == false)
                sb.AppendFormat(" AND RESOURCETYPE = '{0}'", ClsSql.EncodeSql(sw.RESOURCETYPE));
            if (string.IsNullOrEmpty(sw.NUMBER) == false)
                sb.AppendFormat(" AND NUMBER like '%{0}%'", ClsSql.EncodeSql(sw.NUMBER));
            if (string.IsNullOrEmpty(sw.NAME) == false)
                sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
            if (string.IsNullOrEmpty(sw.KINDTYPE) == false)
                sb.AppendFormat(" AND KINDTYPE = '{0}'", ClsSql.EncodeSql(sw.KINDTYPE));
            if (string.IsNullOrEmpty(sw.AGETYPE) == false)
                sb.AppendFormat(" AND AGETYPE = '{0}'", ClsSql.EncodeSql(sw.AGETYPE));
            if (string.IsNullOrEmpty(sw.ORIGINTYPE) == false)
                sb.AppendFormat(" AND ORIGINTYPE = '{0}'", ClsSql.EncodeSql(sw.ORIGINTYPE));
            if (string.IsNullOrEmpty(sw.BURNTYPE) == false)
                sb.AppendFormat(" AND BURNTYPE = '{0}'", ClsSql.EncodeSql(sw.BURNTYPE));
            if (string.IsNullOrEmpty(sw.TREETYPE) == false)
                sb.AppendFormat(" AND TREETYPE = '{0}'", ClsSql.EncodeSql(sw.TREETYPE));
            if (!string.IsNullOrEmpty(sw.ORGNOS))
            {
                if (sw.ORGNOS.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(ORGNOS,1,4) = '{0}')", ClsSql.EncodeSql(sw.ORGNOS.Substring(0, 4)));
                 else if (sw.ORGNOS.Substring(4, 5) == "xxxxx")//单独市
                    sb.AppendFormat(" AND (SUBSTRING(ORGNOS,1,9) = '{0}')", ClsSql.EncodeSql(sw.ORGNOS.Substring(0, 4) + "00000"));
                else if (sw.ORGNOS.Substring(6, 3) == "xxx")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(ORGNOS,1,6) = '{0}' )", ClsSql.EncodeSql(sw.ORGNOS.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND ORGNOS = '{0}'", ClsSql.EncodeSql(sw.ORGNOS));
            }
            string sql = "SELECT DC_RESOURCE_NEW_ID, RESOURCETYPE, NUMBER, NAME, ORGNOS, KINDTYPE, AGETYPE, ORIGINTYPE, AREA, BURNTYPE, TREETYPE, ASPECT, ANGLE, MARK, JD, WD,POTHOOKLEADER,POTHOOKLEADERJOB,POTHOOKLEADERTLEE,DUTYPERSON,DUTYPERSONTELL,JWDLIST"
            + sb.ToString()
            + " order by ORGNOS ";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion

        #region 根据DataTable和OrgNo和资源的各个类型判断记录个数
        /// <summary>
        /// 根据DataTable和OrgNo和资源类型判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <param name="DICTVALUE">资源的各个类型</param>
        /// /// <param name="TYPE">确定统计那个资源的类型</param>
        /// <returns>记录个数</returns>
        public static string getCountarmyByOrgNo(DataTable dt, string orgNo, string DICTVALUE, string TYPE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            if (TYPE == "1")//统计资源类型
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and RESOURCETYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and RESOURCETYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and RESOURCETYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "2")//统计林龄类别
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and AGETYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and AGETYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and AGETYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "3")//统计起源类型
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and ORIGINTYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and ORIGINTYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and ORIGINTYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "4")//统计可燃类型
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and BURNTYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and BURNTYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and BURNTYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "5")//统计林木类型
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and TREETYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and TREETYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and TREETYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            else
                return "";
        }
        #endregion

        #region 根据DataTable和资源各类型判断县市的记录个数
        /// <summary>
        /// 根据DataTable和资源各类型判断县市的记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <param name="DICTVALUE">资源的各个类型</param>
        /// <param name="TYPE">确定统计那个资源的类型</param>
        /// <returns>记录个数</returns>
        public static string getCountXSByOrgNo(DataTable dt, string orgNo, string DICTVALUE, string TYPE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            if (TYPE == "1")//统计资源类型
            {
                
                 if (PublicCls.OrgIsZhen(orgNo)==false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "' and RESOURCETYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "2")//统计林龄类别
            {
                 if (PublicCls.OrgIsZhen(orgNo)==false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "' and AGETYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "3")//统计起源类型
            {
                 if (PublicCls.OrgIsZhen(orgNo)==false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "' and ORIGINTYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "4")//统计可燃类型
            {
                 if (PublicCls.OrgIsZhen(orgNo)==false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "' and BURNTYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "5")//统计林木类型
            {
                 if (PublicCls.OrgIsZhen(orgNo)==false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("count(DC_RESOURCE_NEW_ID)", "ORGNOS='" + orgNo + "' and TREETYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            else
                return "";
        }
        #endregion

        #region 根据DataTable和OrgNo获取面积统计
        /// <summary>
        /// 统计面积
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="orgNo"></param>
        /// <param name="DICTVALUE"></param>
        /// <param name="TYPE"></param>
        /// <returns></returns>
        public static string getAREACount(DataTable dt, string orgNo, string DICTVALUE, string TYPE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            if (TYPE == "1")//统计资源类型
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and RESOURCETYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and RESOURCETYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and RESOURCETYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "2")//统计林龄类别
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and AGETYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and AGETYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and AGETYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "3")//统计起源类型
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and ORIGINTYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and ORIGINTYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and ORIGINTYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "4")//统计可燃类型
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and BURNTYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and BURNTYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and BURNTYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "5")//统计林木类型
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and TREETYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "substring(ORGNOS,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and TREETYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and TREETYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            else
                return "";

        }
        #endregion

        #region 根据DataTable和资源各类型判断县市的面积统计
        /// <summary>
        /// 根据DataTable和资源各类型判断县市的面积统计
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <param name="DICTVALUE">资源的各个类型</param>
        /// <param name="TYPE">确定统计那个资源的类型</param>
        /// <returns>记录个数</returns>
        public static string getAREACountXSByOrgNo(DataTable dt, string orgNo, string DICTVALUE, string TYPE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            if (TYPE == "1")//统计资源类型
            {

                if (PublicCls.OrgIsZhen(orgNo) == false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + orgNo + "' and RESOURCETYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "2")//统计林龄类别
            {
                if (PublicCls.OrgIsZhen(orgNo) == false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + orgNo + "' and AGETYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "3")//统计起源类型
            {
                if (PublicCls.OrgIsZhen(orgNo) == false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + orgNo + "' and ORIGINTYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "4")//统计可燃类型
            {
                if (PublicCls.OrgIsZhen(orgNo) == false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + orgNo + "' and BURNTYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "5")//统计林木类型
            {
                if (PublicCls.OrgIsZhen(orgNo) == false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("Sum(AREA)", "ORGNOS='" + orgNo + "' and TREETYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            else
                return "";
        }
        #endregion

        #region 获取数据字典类型多少条记录
        /// <summary>
        /// 获取数据字典类型多少条记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getCount(T_SYS_DICTSW sw)
        {
            string total = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    T_SYS_DICT a ");
            sb.AppendFormat("where ");
            sb.AppendFormat("DICTTYPEID =  '{0}'", ClsSql.EncodeSql(sw.DICTTYPEID));
            string sqlC = "select count(1) " + sb.ToString();
            //total = DataBaseClass.ReturnSqlField(sqlC);
            total = (DataBaseClass.ReturnSqlField(sqlC)).ToString();
            return total;
        }
        #endregion

        #region 统计当前用户下资源的数量
        /// <summary>
        /// 统计当前用户下资源的记录数量
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getNum(DC_RESOURCE_NEW_SW sw)
        {
            string total = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    DC_RESOURCE_NEW a ");
            sb.AppendFormat("where 1 = 1 ");
            if (sw.ORGNOS.Substring(4, 5) == "00000")//获取所有市的
                sb.AppendFormat(" AND (SUBSTRING(ORGNOS,1,4) = '{0}' or ORGNOS is null or ORGNOS='')", ClsSql.EncodeSql(sw.ORGNOS.Substring(0, 4)));
            else if (sw.ORGNOS.Substring(6, 3) == "000")//获取所有县的
                sb.AppendFormat(" AND (SUBSTRING(ORGNOS,1,6) = '{0}' or ORGNOS is null or ORGNOS='')", ClsSql.EncodeSql(sw.ORGNOS.Substring(0, 6)));
            else
                sb.AppendFormat(" AND ORGNOS = '{0}'", ClsSql.EncodeSql(sw.ORGNOS));
            string sqlC = "select count(1) " + sb.ToString();
            total = (DataBaseClass.ReturnSqlField(sqlC)).ToString();
            return total;
        }
        #endregion
       
    }
}
