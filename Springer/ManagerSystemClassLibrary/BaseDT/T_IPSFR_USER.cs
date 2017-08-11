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

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 护林员表管理
    /// </summary>
    public class T_IPSFR_USER
    {
        #region 增加

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_IPSFR_USER_Model m)
        {
            if (isExists(new T_IPSFR_USER_SW { PHONE = m.PHONE }))
                return new Message(false, "添加失败，电话号码重复！", "");
            if (isExists(new T_IPSFR_USER_SW { SN = m.SN }))
                return new Message(false, "添加失败，终端编号重复！", "");
            //if (PublicCls.OrgIsZhen(m.BYORGNO )==false)
            //    return new Message(false, "添加失败，所属单位必须为乡镇！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  T_IPSFR_USER(HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO,ISENABLE)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.HNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SN));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PHONE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SEX));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BIRTH));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ONSTATE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ISENABLE));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message PATROLLENGTHMdy(T_IPSFR_USER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE T_IPSFR_USER");
            sb.AppendFormat(" set ");
            sb.AppendFormat("PATROLLENGTH='{0}'", ClsSql.EncodeSql(m.PATROLLENGTH));
            sb.AppendFormat(" where HID= '{0}'", ClsSql.EncodeSql(m.HID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }


        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(T_IPSFR_USER_Model m)
        {
            //if (PublicCls.OrgIsZhen(m.BYORGNO) == false)
            //    return new Message(false, "修改失败，所属单位必须为乡镇！", "");
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE T_IPSFR_USER");
            sb.AppendFormat(" set ");
            sb.AppendFormat("HNAME='{0}'", ClsSql.EncodeSql(m.HNAME));
            sb.AppendFormat(",SN='{0}'", ClsSql.EncodeSql(m.SN));
            sb.AppendFormat(",PHONE='{0}'", ClsSql.EncodeSql(m.PHONE));
            sb.AppendFormat(",SEX='{0}'", ClsSql.EncodeSql(m.SEX));
            sb.AppendFormat(",BIRTH='{0}'", ClsSql.EncodeSql(m.BIRTH));
            sb.AppendFormat(",ONSTATE='{0}'", ClsSql.EncodeSql(m.ONSTATE));
            sb.AppendFormat(",BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",ISENABLE='{0}'", ClsSql.EncodeSql(m.ISENABLE));
            sb.AppendFormat(" where HID= '{0}'", ClsSql.EncodeSql(m.HID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="m">对象</param>
        /// <returns></returns>
        public static Message UpdateHlyParameter(T_IPSFR_USER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE T_IPSFR_USER");
            sb.AppendFormat(" set ");
            sb.AppendFormat("MOBILEPARAMLIST='{0}'", ClsSql.EncodeSql(m.MOBILEPARAMLIST));
            sb.AppendFormat(" where HID= '{0}'", ClsSql.EncodeSql(m.HID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "设置成功！", "");
            else
                return new Message(false, "设置失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_IPSFR_USER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete T_IPSFR_USER");
            sb.AppendFormat(" where HID= '{0}'", ClsSql.EncodeSql(m.HID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 启用
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Enable(T_IPSFR_USER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update T_IPSFR_USER set ISENABLE=1");
            sb.AppendFormat(" where HID in({0})", ClsSql.EncodeSql(m.HID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "启用成功！", "");
            else
                return new Message(false, "启用失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 禁用
        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message UnEnable(T_IPSFR_USER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update T_IPSFR_USER set ISENABLE=0");
            sb.AppendFormat(" where HID in({0})", ClsSql.EncodeSql(m.HID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "禁用成功！", "");
            else
                return new Message(false, "禁用失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false 不存在 </returns>
        public static bool isExists(T_IPSFR_USER_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_IPSFR_USER where 1=1");
            if (string.IsNullOrEmpty(sw.HID) == false)
                sb.AppendFormat(" and HID='{0}'", ClsSql.EncodeSql(sw.HID));
            if (string.IsNullOrEmpty(sw.SN) == false)
                sb.AppendFormat(" and SN='{0}'", ClsSql.EncodeSql(sw.SN));
            if (string.IsNullOrEmpty(sw.PHONE) == false)
                sb.AppendFormat(" and PHONE='{0}'", ClsSql.EncodeSql(sw.PHONE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取分页数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_IPSFR_USER_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      T_IPSFR_USER a");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.ISENABLE) == false)
                sb.AppendFormat(" AND ISENABLE = '{0}'", ClsSql.EncodeSql(sw.ISENABLE));
            if (string.IsNullOrEmpty(sw.HID) == false)
                sb.AppendFormat(" AND HID = '{0}'", ClsSql.EncodeSql(sw.HID));
            if (string.IsNullOrEmpty(sw.HNAME) == false)
                sb.AppendFormat(" AND HNAME like '%{0}%'", ClsSql.EncodeSql(sw.HNAME));
            if (string.IsNullOrEmpty(sw.SN) == false)
                sb.AppendFormat(" AND SN like '%{0}%'", ClsSql.EncodeSql(sw.SN));
            if (string.IsNullOrEmpty(sw.PHONE) == false)
                sb.AppendFormat(" AND PHONE '%{0}%'", ClsSql.EncodeSql(sw.PHONE));
            if (string.IsNullOrEmpty(sw.SEX) == false)
                sb.AppendFormat(" AND SEX = '{0}'", ClsSql.EncodeSql(sw.SEX));
            if (string.IsNullOrEmpty(sw.ONSTATE) == false)
                sb.AppendFormat(" AND ONSTATE = '{0}'", ClsSql.EncodeSql(sw.ONSTATE));
            if (string.IsNullOrEmpty(sw.MOBILEPARAMLIST) == false)
                sb.AppendFormat(" AND MOBILEPARAMLIST = '{0}'", ClsSql.EncodeSql(sw.MOBILEPARAMLIST));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {

                if (PublicCls.OrgIsShi(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like '{0}%'", PublicCls.getShiIncOrgNo(sw.BYORGNO));
                }
                else if (PublicCls.OrgIsXian(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like  '{0}%'", PublicCls.getXianIncOrgNo(sw.BYORGNO));
                }
                else if (PublicCls.OrgIsZhen(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like  '{0}%'", PublicCls.getZhenIncOrgNo(sw.BYORGNO));
                }
                else
                {
                    sb.AppendFormat(" and BYORGNO =  '{0}'", sw.BYORGNO);
                }
            }
            if (string.IsNullOrEmpty(sw.PhoneHname) == false)
            {

                sb.AppendFormat(" AND (a.PHONE  like '%{0}%' or a.HNAME like '%{0}%')", ClsSql.EncodeSql(sw.PhoneHname));
            }
            string sql = "SELECT    HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO,ISENABLE,MOBILEPARAMLIST"
                + sb.ToString()
                + " order by BYORGNO,HNAME";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion

        #region 判断护林员线路、围栏是否存在
        /// <summary>
        /// 判断护林员线路、围栏是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isRouteRailExists(T_IPSFR_USER_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_IPSFR_USER where 1=1");
            if (string.IsNullOrEmpty(sw.HID) == false)
                sb.AppendFormat(" and HID='{0}'", ClsSql.EncodeSql(sw.HID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取数据 getDT
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_IPSFR_USER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT a.HID, a.HNAME, a.SN, a.PHONE, a.SEX, a.BIRTH, a.ONSTATE, a.BYORGNO,a.MOBILEPARAMLIST, a.ISENABLE, b.ORGNAME");
            sb.AppendFormat(" FROM      T_IPSFR_USER  AS a LEFT OUTER JOIN T_SYS_ORG AS b ON a.BYORGNO = b.ORGNO");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.HID) == false)
            {
                if (sw.HID.Split(',').Length > 1)
                    sb.AppendFormat(" AND a.HID in({0})", ClsSql.EncodeSql(sw.HID));
                else
                    sb.AppendFormat(" AND a.HID ='{0}'", ClsSql.EncodeSql(sw.HID));
            }
            if (string.IsNullOrEmpty(sw.HNAME) == false)
                sb.AppendFormat(" AND a.HNAME like '%{0}%'", ClsSql.EncodeSql(sw.HNAME));
            if (string.IsNullOrEmpty(sw.SN) == false)
                sb.AppendFormat(" AND a.SN like '%{0}%'", ClsSql.EncodeSql(sw.SN));
            if (string.IsNullOrEmpty(sw.PHONE) == false)
            {
                if (sw.PHONE.Length == 11)//精确查询
                    sb.AppendFormat(" AND a.PHONE= '{0}'", ClsSql.EncodeSql(sw.PHONE));
                else    //模糊查询
                    sb.AppendFormat(" AND a.PHONE like '%{0}%'", ClsSql.EncodeSql(sw.PHONE));
            }
            if (string.IsNullOrEmpty(sw.PHONELIST) == false)
            {
                if (sw.PHONELIST.Split(',').Length > 1)
                    sb.AppendFormat(" AND PHONE in({0})", ClsSql.SwitchStrToSqlIn(sw.PHONELIST));
                else
                    sb.AppendFormat(" AND PHONE ='{0}'", ClsSql.EncodeSql(sw.PHONELIST));
            }
            if (string.IsNullOrEmpty(sw.PATROLLENGTH) == false)
                sb.AppendFormat(" AND a.PATROLLENGTH = '{0}'", ClsSql.EncodeSql(sw.PATROLLENGTH));
            if (string.IsNullOrEmpty(sw.SEX) == false)
                sb.AppendFormat(" AND a.SEX = '{0}'", ClsSql.EncodeSql(sw.SEX));
            if (string.IsNullOrEmpty(sw.ONSTATE) == false)
                sb.AppendFormat(" AND a.ONSTATE = '{0}'", ClsSql.EncodeSql(sw.ONSTATE));
            if (string.IsNullOrEmpty(sw.ISENABLE) == false)//默认取有效用户
                sb.AppendFormat(" AND a.ISENABLE = '{0}'", ClsSql.EncodeSql(sw.ISENABLE));
            if (string.IsNullOrEmpty(sw.PhoneHname) == false)
                sb.AppendFormat(" AND (a.PHONE  like '%{0}%' or a.HNAME like '%{0}%')", ClsSql.EncodeSql(sw.PhoneHname));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                if (PublicCls.OrgIsShi(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like '{0}%'", PublicCls.getShiIncOrgNo(sw.BYORGNO));
                }
                else if (PublicCls.OrgIsXian(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like  '{0}%'", PublicCls.getXianIncOrgNo(sw.BYORGNO));
                }
                else if (PublicCls.OrgIsZhen(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like  '{0}%'", PublicCls.getZhenIncOrgNo(sw.BYORGNO));
                }
                else if (PublicCls.OrgIsCun(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like '{0}%'", PublicCls.getCunIncOrgNo(sw.BYORGNO));
                }
            }
            if (string.IsNullOrEmpty(sw.Orgs) == false)
            {
                string[] arr = sw.Orgs.Split(',');
                string tmpOrg = "";
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].Length == 15)
                    {
                        if (tmpOrg != "")
                            tmpOrg += ",";
                        tmpOrg += arr[i];
                    }
                }
                if (tmpOrg != "")
                {
                    string[] arr1 = tmpOrg.Split(',');//循环每个单位
                    sb.AppendFormat(" and (");
                    for (int i = 0; i < arr1.Length; i++)
                    {
                        if (i > 0)
                        {
                            sb.AppendFormat("  or");
                        }
                        if (PublicCls.OrgIsShi(arr1[i]))
                        {
                            sb.AppendFormat("  BYORGNO like '{0}%'", PublicCls.getShiIncOrgNo(arr1[i]));
                        }
                        else if (PublicCls.OrgIsXian(arr1[i]))
                        {
                            sb.AppendFormat("  BYORGNO like  '{0}%'", PublicCls.getXianIncOrgNo(arr1[i]));
                        }
                        else if (PublicCls.OrgIsZhen(arr1[i]))
                        {
                            sb.AppendFormat("  BYORGNO like  '{0}%'", PublicCls.getZhenIncOrgNo(arr1[i]));
                        }
                        else if (PublicCls.OrgIsCun(arr1[i]))
                        {
                            sb.AppendFormat("  BYORGNO like '{0}%'", PublicCls.getCunIncOrgNo(arr1[i]));
                        }
                    }
                    sb.AppendFormat(" )");
                }
            }
            sb.AppendFormat(" ORDER BY a.BYORGNO,a.HNAME ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        #endregion

        #region 根据DataTable和OrgNo判断记录个数
        /// <summary>
        /// 根据DataTable和OrgNo判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <returns>记录个数</returns>
        public static string getCountByOrgNo(DataTable dt, string orgNo)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            if (PublicCls.OrgIsShi(orgNo))//市
            {
                return dt.Compute("count(HID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
            }
            else if (PublicCls.OrgIsXian(orgNo))//县
            {
                return dt.Compute("count(HID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
            }
            else if (PublicCls.OrgIsZhen(orgNo))
            {
                return dt.Compute("count(HID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
            }
            else //机构编码可能不正确
                return "";
        }
        #endregion

        #region 获取护林员姓名
        /// <summary>
        /// 根据护林员ID获取姓名
        /// </summary>
        /// <param name="value">编码</param>
        /// <returns></returns>
        public static string getName(string value)
        {
            string sql = " Select HNAME from T_IPSFR_USER where HID='" + value + "'";
            return DataBaseClass.ReturnSqlField(sql);
        }
        #endregion

        #region 返回各县护林员个数 用于统计优化
        /// <summary>
        /// 返回各县护林员个数
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns></returns>
        public static DataTable getDTByOrgSum(T_IPSFR_USER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            if (PublicCls.OrgIsShi(sw.BYORGNO))
            {
                sb.AppendFormat(" select count(substring(BYORGNO,1,6)+'000000000') as C,substring(BYORGNO,1,6)+'000000000' as BYORGNO");
                sb.AppendFormat(" FROM      T_IPSFR_USER");
                sb.AppendFormat(" where ISENABLE = '1'");
                sb.AppendFormat(" group by substring(BYORGNO,1,6)+'000000000'");
            }
            else if (PublicCls.OrgIsXian(sw.BYORGNO))
            {
                sb.AppendFormat(" select count(BYORGNO) as C, BYORGNO");
                sb.AppendFormat(" FROM      T_IPSFR_USER");
                sb.AppendFormat(" where ISENABLE = '1'");
                sb.AppendFormat(" and substring(BYORGNO,1,6)+'000000000'='{0}'", sw.BYORGNO);
                sb.AppendFormat(" group by BYORGNO");
            }
            else
            {

                sb.AppendFormat(" select hid,hname");
                sb.AppendFormat(" FROM      T_IPSFR_USER");
                sb.AppendFormat(" where ISENABLE = '1'");
                sb.AppendFormat(" and BYORGNO='{0}'", sw.BYORGNO);
            }
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 返回各县护林员个数 用于统计优化
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDTShi(T_IPSFR_USER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" select count(substring(BYORGNO,1,6)+'000000000') as C,substring(BYORGNO,1,6)+'000000000' as BYORGNO");
            sb.AppendFormat(" FROM      T_IPSFR_USER");
            sb.AppendFormat(" where ISENABLE = '1'");
            sb.AppendFormat(" group by substring(BYORGNO,1,6)+'000000000'");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        #endregion

        #region 返回某县各镇护林员个数 用于统计优化
        /// <summary>
        /// 返回某县各镇护林员个数 用于统计优化
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDTXain(T_IPSFR_USER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" select count(BYORGNO) as C, BYORGNO");
            sb.AppendFormat(" FROM      T_IPSFR_USER");
            sb.AppendFormat(" where ISENABLE = '1'");
            sb.AppendFormat(" and substring(BYORGNO,1,6)+'000000000'='{0}'", sw.BYORGNO);
            sb.AppendFormat(" group by BYORGNO");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        #endregion

        #region 获取分组统计后的统计SUM
        /// <summary>
        /// 获取分组统计后的统计SUM
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="orgNo"></param>
        /// <returns></returns>
        public static string getSumByOrgNo(DataTable dt, string orgNo)
        {
            string str = "0";
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
            {
                str = dt.Compute("sum(C)", "").ToString();
            }
            else
            {
                str = dt.Compute("sum(C)", "BYORGNO=" + orgNo + "").ToString();
            }
            if (string.IsNullOrEmpty(str))
                str = "0";
            return str;
        }

        #endregion
    }
}
