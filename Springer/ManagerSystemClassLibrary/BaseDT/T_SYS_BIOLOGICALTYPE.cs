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
    /// 公用_生物分类表
    /// </summary>
    public class T_SYS_BIOLOGICALTYPE
    {
        #region 增加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYS_BIOLOGICALTYPE_Model m)
        {
            if (isExists(new T_SYS_BIOLOGICALTYPE_SW { BIOLOCODE = m.BIOLOCODE, }) == true)
                return new Message(false, "添加失败，该分类编码已存在!", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO T_SYS_BIOLOGICALTYPE(BIOLOCODE, BIOLONAME, BIOLOENNAME, ORDERBY)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat(" '{0}'", ClsSql.EncodeSql(m.BIOLOCODE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BIOLONAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BIOLOENNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功!", "");
            else
                return new Message(false, "添加失败,请检查各输入框是否正确!", "");
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(T_SYS_BIOLOGICALTYPE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE T_SYS_BIOLOGICALTYPE SET");
            sb.AppendFormat(" BIOLONAME='{0}'", ClsSql.EncodeSql(m.BIOLONAME));
            sb.AppendFormat(",BIOLOENNAME='{0}'", ClsSql.EncodeSql(m.BIOLOENNAME));
            sb.AppendFormat(",ORDERBY='{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(" WHERE BIOLOCODE= '{0}'", ClsSql.EncodeSql(m.BIOLOCODE));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", "");
            else
                return new Message(false, "修改失败,请检查各输入框是否正确!", "");
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_SYS_BIOLOGICALTYPE_Model m)
        {
            if (isExistsChild(new T_SYS_BIOLOGICALTYPE_SW { BIOLOCODE = m.BIOLOCODE }))
                return new Message(false, "存在下级分类，暂无法删除!", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Delete T_SYS_BIOLOGICALTYPE");
            sb.AppendFormat(" where BIOLOCODE= '{0}'", ClsSql.EncodeSql(m.BIOLOCODE));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败!", "");
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExists(T_SYS_BIOLOGICALTYPE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_BIOLOGICALTYPE where 1=1");
            if (string.IsNullOrEmpty(sw.BIOLOCODE) == false)
                sb.AppendFormat(" and BIOLOCODE='{0}'", ClsSql.EncodeSql(sw.BIOLOCODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 判断是否有下属分类
        /// <summary>
        /// 判断记录是否存在下级
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExistsChild(T_SYS_BIOLOGICALTYPE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Select 1 from T_SYS_BIOLOGICALTYPE Where 1=1");
            if (string.IsNullOrEmpty(sw.BIOLOCODE) == false)
            {
                string code = "";
                if (PublicCls.BioCodeIsJie(sw.BIOLOCODE))
                    code = PublicCls.GetJieBioCode(sw.BIOLOCODE);
                if (PublicCls.BioCodeIsMen(sw.BIOLOCODE))
                    code = PublicCls.GetMenBioCode(sw.BIOLOCODE);
                if (PublicCls.BioCodeIsGang(sw.BIOLOCODE))
                    code = PublicCls.GetGangBioCode(sw.BIOLOCODE);
                if (PublicCls.BioCodeIsMu(sw.BIOLOCODE))
                    code = PublicCls.GetMuBioCode(sw.BIOLOCODE);
                if (PublicCls.BioCodeIsKe(sw.BIOLOCODE))
                    code = PublicCls.GetKeBioCode(sw.BIOLOCODE);
                if (PublicCls.BioCodeIsShu(sw.BIOLOCODE))
                    code = PublicCls.GetShuBioCode(sw.BIOLOCODE);
                if (PublicCls.BioCodeIsZHong(sw.BIOLOCODE))
                    code = PublicCls.GetZhongBioCode(sw.BIOLOCODE);
                sb.AppendFormat(" AND Substring(BIOLOCODE,1,{0})= '{1}' AND BIOLOCODE <> '{2}'", ClsSql.EncodeSql(code).Length, ClsSql.EncodeSql(code), ClsSql.EncodeSql(sw.BIOLOCODE));
            }
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取数据模型
        /// <summary>
        /// 获取数据模型
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getModel(T_SYS_BIOLOGICALTYPE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT BIOLOCODE, BIOLONAME, BIOLOENNAME, ORDERBY FROM T_SYS_BIOLOGICALTYPE WHERE 1=1");
            if (!string.IsNullOrEmpty(sw.BIOLOCODE))
                sb.AppendFormat(" AND BIOLOCODE = '{0}'", ClsSql.EncodeSql(sw.BIOLOCODE));
            if (!string.IsNullOrEmpty(sw.BIOLONAME))
                sb.AppendFormat(" AND BIOLONAME = '{0}'", ClsSql.EncodeSql(sw.BIOLONAME));
            if (!string.IsNullOrEmpty(sw.BIOLOENNAME))
                sb.AppendFormat(" AND BIOLOENNAME = '{0}'", ClsSql.EncodeSql(sw.BIOLOENNAME));
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns></returns>
        public static DataTable getDT(T_SYS_BIOLOGICALTYPE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  T_SYS_BIOLOGICALTYPE ");
            sb.AppendFormat(" WHERE  1=1");
            if (string.IsNullOrEmpty(sw.BIOLOCODE) == false)
            {
                string code = "";
                if (PublicCls.BioCodeIsJie(sw.BIOLOCODE))
                {
                    code = PublicCls.GetJieBioCode(sw.BIOLOCODE);
                    sb.AppendFormat(" AND Substring(BIOLOCODE,1,2) = '{0}'", code);
                    if (sw.IsOnlyGetChild)
                        sb.AppendFormat(" AND Substring(BIOLOCODE,3,2) <> '00' AND Substring(BIOLOCODE,5,10)='0000000000'");
                }
                else if (PublicCls.BioCodeIsMen(sw.BIOLOCODE))
                {
                    code = PublicCls.GetMenBioCode(sw.BIOLOCODE);
                    sb.AppendFormat(" AND Substring(BIOLOCODE,1,4) = '{0}'", code);
                    if (sw.IsOnlyGetChild)
                        sb.AppendFormat(" AND Substring(BIOLOCODE,5,2) <> '00' AND Substring(BIOLOCODE,7,8)='00000000'");
                }
                else if (PublicCls.BioCodeIsGang(sw.BIOLOCODE))
                {
                    code = PublicCls.GetGangBioCode(sw.BIOLOCODE);
                    sb.AppendFormat(" AND Substring(BIOLOCODE,1,6) = '{0}'", code);
                    if (sw.IsOnlyGetChild)
                        sb.AppendFormat(" AND Substring(BIOLOCODE,7,2) <> '00' AND Substring(BIOLOCODE,9,6)='000000'");
                }
                else if (PublicCls.BioCodeIsMu(sw.BIOLOCODE))
                {
                    code = PublicCls.GetMuBioCode(sw.BIOLOCODE);
                    sb.AppendFormat(" AND Substring(BIOLOCODE,1,8) = '{0}'", code);
                    if (sw.IsOnlyGetChild)
                        sb.AppendFormat(" AND Substring(BIOLOCODE,9,2) <> '00' AND Substring(BIOLOCODE,11,4)='0000'");
                }
                else if (PublicCls.BioCodeIsKe(sw.BIOLOCODE))
                {
                    code = PublicCls.GetKeBioCode(sw.BIOLOCODE);
                    sb.AppendFormat(" AND Substring(BIOLOCODE,1,10) = '{0}'", code);
                    if (sw.IsOnlyGetChild)
                        sb.AppendFormat(" AND Substring(BIOLOCODE,11,2) <> '00' AND Substring(BIOLOCODE,13,2)='00'");
                }
                else if (PublicCls.BioCodeIsShu(sw.BIOLOCODE))
                {
                    code = PublicCls.GetShuBioCode(sw.BIOLOCODE);
                    sb.AppendFormat(" AND Substring(BIOLOCODE,1,12) = '{0}'", code);
                    if (sw.IsOnlyGetChild)
                        sb.AppendFormat(" AND Substring(BIOLOCODE,13,2) <> '00'");
                }
                else
                {
                    code = PublicCls.GetShuBioCode(sw.BIOLOCODE);
                    if (sw.IsOnlyGetZhong)
                        sb.AppendFormat(" AND Substring(BIOLOCODE,1,12) = '{0}' AND Substring(BIOLOCODE,13,2)<>'00'",code.Substring(0,12));
                    else
                    sb.AppendFormat(" AND  BIOLOCODE = '{0}'", ClsSql.EncodeSql(sw.BIOLOCODE));
                }
            }
            if (sw.IsOnlyGetKe)
                sb.AppendFormat(" AND Substring(BIOLOCODE,1,8) <> '00000000' AND Substring(BIOLOCODE,9,2) <> '00' ");
            if (sw.IsOnlyGetZhong)
                sb.AppendFormat(" AND Substring(BIOLOCODE,13,2) <> '00'");
            if (string.IsNullOrEmpty(sw.BIOLONAME) == false)
                sb.AppendFormat(" AND BIOLONAME like '%{0}%'", ClsSql.EncodeSql(sw.BIOLONAME));
            if (string.IsNullOrEmpty(sw.BIOLOENNAME) == false)
                sb.AppendFormat(" AND BIOLOENNAME like '%{0}%'", ClsSql.EncodeSql(sw.BIOLOENNAME));          
            string sql = "SELECT BIOLOCODE, BIOLONAME, BIOLOENNAME, ORDERBY " + sb.ToString() + " order by BIOLOCODE";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion

        #region 获取分页数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYS_BIOLOGICALTYPE_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  T_SYS_BIOLOGICALTYPE ");
            sb.AppendFormat(" WHERE  1=1");
            if (string.IsNullOrEmpty(sw.BIOLOCODE) == false)
                sb.AppendFormat(" AND BIOLOCODE = '{0}'", ClsSql.EncodeSql(sw.BIOLOCODE));
            if (string.IsNullOrEmpty(sw.BIOLONAME) == false)
                sb.AppendFormat(" AND BIOLONAME like '%{0}%'", ClsSql.EncodeSql(sw.BIOLONAME));
            if (string.IsNullOrEmpty(sw.BIOLOENNAME) == false)
                sb.AppendFormat(" AND BIOLOENNAME like '%{0}%'", ClsSql.EncodeSql(sw.BIOLOENNAME));
            string sql = "SELECT BIOLOCODE, BIOLONAME, BIOLOENNAME, ORDERBY " + sb.ToString() + " order by ORDERBY";
            string sqlC = "SELECT Count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }
        #endregion

        #region 根据生物编码获取生物名称
        /// <summary>
        /// 根据分类编码获取分类名称
        /// </summary>
        /// <param name="BIOLOCODE">编码</param>
        /// <returns>名称</returns>
        public static string getName(string BIOLOCODE)
        {
            if (string.IsNullOrEmpty(BIOLOCODE))
                return "";
            string str = DataBaseClass.ReturnSqlField("SELECT BIOLONAME FROM T_SYS_BIOLOGICALTYPE WHERE BIOLOCODE='" + BIOLOCODE + "'");
            return str;
        }

        /// <summary>
        /// 根据生物编码获取生物名称
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="value">编码</param>
        /// <returns>名称</returns>
        public static string getName(DataTable dt, string value)
        {
            if (dt == null)
                return "";
            if (string.IsNullOrEmpty(value))
                return "";
            string str = "";
            DataRow[] dr = dt.Select("BIOLOCODE='" + value + "'");
            if (dr.Length > 0)
                str = dr[0]["BIOLONAME"].ToString();
            return str;
        }
        #endregion
    }
}
