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
    /// 行政区划
    /// </summary>
    public class T_ALL_AREA
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_ALL_AREA_Model m)
        {
            if (isExists(new T_ALL_AREA_SW { AREACODE = m.AREACODE, }) == true)
                return new Message(false, "添加失败，该区划编码已存在！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  T_ALL_AREA(AREACODE, AREANAME, AREAJC, JD, WD ) OUTPUT INSERTED.AREAID ");
            sb.AppendFormat(" VALUES(");
            sb.AppendFormat(" '{0}'", ClsSql.EncodeSql(m.AREACODE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.AREANAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.AREAJC));
            sb.AppendFormat(" ,{0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(" ,{0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(")");
            try
            {
                string sId = DataBaseClass.ReturnSqlField(sb.ToString());
                if (sId != "")
                    return new Message(true, "添加成功!", m.returnUrl + "," + sId);
                else
                    return new Message(false, "添加失败!", m.returnUrl);
            }
            catch
            {
                return new Message(false, "添加失败!", m.returnUrl);
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(T_ALL_AREA_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE T_ALL_AREA SET ");
            sb.AppendFormat(" AREANAME='{0}'", ClsSql.EncodeSql(m.AREANAME));
            sb.AppendFormat(",AREACODE='{0}'", ClsSql.EncodeSql(m.AREACODE));
            sb.AppendFormat(",AREAJC='{0}'", ClsSql.EncodeSql(m.AREAJC));
            sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(" where AREAID= '{0}'", ClsSql.EncodeSql(m.AREAID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.returnUrl + "," + m.AREAID);
            else
                return new Message(false, "修改失败，请检查各输入框是否正确!", m.returnUrl);
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_ALL_AREA_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete T_ALL_AREA");
            if (m.AREACODE.Substring(2, 7) == "0000000")
            {
                sb.AppendFormat(" where substring(AREACODE,1,2)= '{0}'", ClsSql.EncodeSql(m.AREACODE.Substring(0, 2)));
            }
            else if (m.AREACODE.Substring(4, 5) == "00000")
            {
                sb.AppendFormat(" where substring(AREACODE,1,4)= '{0}'", ClsSql.EncodeSql(m.AREACODE.Substring(0, 4)));
            }
            else if (m.AREACODE.Substring(6, 3) == "000")
            {
                sb.AppendFormat(" where substring(AREACODE,1,6)= '{0}'", ClsSql.EncodeSql(m.AREACODE.Substring(0, 6)));
            }
            else
            {
                sb.AppendFormat(" where substring(AREACODE,1,{0})= '{1}'", ClsSql.EncodeSql(m.AREACODE).Length.ToString(), ClsSql.EncodeSql(m.AREACODE));
            }
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", m.returnUrl);
            else
                return new Message(false, "删除失败，请检查各输入框是否正确!", m.returnUrl);
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExists(T_ALL_AREA_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_ALL_AREA where 1=1");
            if (string.IsNullOrEmpty(sw.AREACODE) == false)
                sb.AppendFormat(" and AREACODE='{0}'", ClsSql.EncodeSql(sw.AREACODE));

            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 根据编码获取名称
        /// <summary>
        /// 根据编码获取名称
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="value"></param>
        /// <returns>参见模型</returns>
        public static string getName(DataTable dt, string value)
        {
            if (dt == null)
                return "";
            if (string.IsNullOrEmpty(value))
                return "";
            string str = "";
            DataRow[] dr = dt.Select("AREACODE='" + value + "'");
            if (dr.Length > 0)
                str = dr[0]["AREANAME"].ToString();
            return str;
        }

        #endregion

        #region 获取数据列表(根据系统配置，获取顶层行政区划下面所有区划 常用于下拉框)
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT()
        {
            string topArea = ConfigCls.getTopAreaCode();
            string sql = "SELECT AREACODE, AREANAME FROM  T_ALL_AREA WHERE  (SUBSTRING(AREACODE, 1, " + topArea.Length + ") = '" + topArea + "') ORDER BY AREACODE";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_ALL_AREA_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT     AREAID, AREACODE, AREANAME, AREAJC, JD, WD");
            sb.AppendFormat(" FROM      T_ALL_AREA");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.AREAID) == false)
                sb.AppendFormat(" AND AREAID = '{0}'", ClsSql.EncodeSql(sw.AREAID));
            if (string.IsNullOrEmpty(sw.AREACODE) == false)
                sb.AppendFormat(" AND AREACODE = '{0}'", ClsSql.EncodeSql(sw.AREACODE));
            if (!string.IsNullOrEmpty(sw.AREAJC))
            {
                sb.AppendFormat(" AND AREAJC = '{0}'", ClsSql.EncodeSql(sw.AREAJC));
            }
            if (string.IsNullOrEmpty(sw.SubAREACODE) == false)
            {
                if (sw.SubAREACODE == "0")
                    sb.AppendFormat(" AND Substring(AREACODE,3, 13) ='0000000000000'");
                else if (sw.SubAREACODE.Substring(2, 13) == "0000000000000")
                    sb.AppendFormat(" AND Substring(AREACODE,5, 11) = '00000000000' AND Substring(AREACODE,3, 2) != '00' AND Substring(AREACODE,1, 2)='{0}'", sw.SubAREACODE.Substring(0, 2));
                else if (sw.SubAREACODE.Substring(4, 11) == "00000000000" && sw.SubAREACODE.Substring(2, 2) != "00")
                    sb.AppendFormat(" AND Substring(AREACODE,7, 9) = '000000000' AND Substring(AREACODE,5, 2) != '00'AND Substring(AREACODE,1,4)='{0}'", sw.SubAREACODE.Substring(0, 4));
                else if (sw.SubAREACODE.Substring(6, 9) == "000000000" && sw.SubAREACODE.Substring(4, 2) != "00")
                    sb.AppendFormat("AND Substring(AREACODE,10, 6) = '000000' AND Substring(AREACODE,7, 3) != '000' AND Substring(AREACODE,1,6)='{0}'", sw.SubAREACODE.Substring(0, 6));
                else if (sw.SubAREACODE.Substring(9, 6) == "000000" && sw.SubAREACODE.Substring(6, 3) != "000")
                    sb.AppendFormat("AND Substring(AREACODE,13, 3) = '000' AND Substring(AREACODE,10, 6) != '000' AND Substring(AREACODE,1,9)='{0}'", sw.SubAREACODE.Substring(0, 6));
                else
                    sb.AppendFormat(" AND Substring(AREACODE,1, 15) = '{0}' ", sw.SubAREACODE.Substring(0, 15));
            }
            if (!string.IsNullOrEmpty(sw.GetContyORGNOByCity))//市获取所有县
            {
                sb.AppendFormat(" AND (SUBSTRING(AREACODE,1,4)='{0}' and SUBSTRING(AREACODE,5,2)<>'00' and SUBSTRING(AREACODE,7,9) = '000000000' )", ClsSql.EncodeSql(sw.GetContyORGNOByCity.Substring(0, 4)));
            }
            if (!string.IsNullOrEmpty(sw.GetXZOrgNOByConty))
            {
                sb.AppendFormat(" AND (SUBSTRING(AREACODE,1,6)='{0}' and SUBSTRING(AREACODE,7,3) != '000'and SUBSTRING(AREACODE,10,6) = '000000')", ClsSql.EncodeSql(sw.GetXZOrgNOByConty.Substring(0, 6)));
            }
            if (string.IsNullOrEmpty(sw.OnlyGetShiXian) == false)
            {
                sb.AppendFormat(" AND (SUBSTRING(AREACODE,1,4)='{0}' and SUBSTRING(AREACODE,7,3) = '000' and  SUBSTRING(AREACODE,5,11)<>'00000000000' )", ClsSql.EncodeSql(sw.TOPAREACODE.Substring(0, 4)));
            }
            sb.AppendFormat(" ORDER BY AREACODE ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        #endregion

    }
}
