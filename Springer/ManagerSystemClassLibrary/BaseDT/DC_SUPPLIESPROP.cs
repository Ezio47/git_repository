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
    ///数据中心_物资属性表
    /// </summary>
    public class DC_SUPPLIESPROP
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(DC_SUPPLIESPROP_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  DC_SUPPLIESPROP(DC_SUPPLIESPROP_ID,DCSUPPROPNAME, DCSUPPROPMODEL, DCSUPPROPUNIT, DCSUPPROPFACTORY, MARK) ");
            sb.AppendFormat(" VALUES(");
            sb.AppendFormat("{0}", ClsSql.saveNullField(m.DC_SUPPLIESPROP_ID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DCSUPPROPNAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DCSUPPROPMODEL));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DCSUPPROPUNIT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DCSUPPROPFACTORY));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MARK));
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
        public static Message Mdy(DC_SUPPLIESPROP_Model m)
        {
            if (DC_SUPPLIESPROP.isExists(new DC_SUPPLIESPROP_Model { DC_SUPPLIESPROP_ID = m.DC_SUPPLIESPROP_ID }) == false)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("INSERT INTO  DC_SUPPLIESPROP(DC_SUPPLIESPROP_ID,DCSUPPROPNAME, DCSUPPROPMODEL, DCSUPPROPUNIT, DCSUPPROPFACTORY, MARK)");
                sb.AppendFormat("VALUES(");
                sb.AppendFormat("{0}", ClsSql.saveNullField(m.DC_SUPPLIESPROP_ID));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DCSUPPROPNAME));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DCSUPPROPMODEL));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DCSUPPROPUNIT));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DCSUPPROPFACTORY));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MARK));

                sb.AppendFormat(")");
                bool bln = DataBaseClass.ExeSql(sb.ToString());
                if (bln == true)
                    return new Message(true, "添加成功！", m.DC_SUPPLIESPROP_ID);
                else
                    return new Message(false, "添加失败，请检查各输入框是否正确！", "");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("UPDATE DC_SUPPLIESPROP");
                sb.AppendFormat(" set ");
                sb.AppendFormat("DCSUPPROPNAME={0}", ClsSql.saveNullField(m.DCSUPPROPNAME));
                sb.AppendFormat(",DCSUPPROPMODEL={0}", ClsSql.saveNullField(m.DCSUPPROPMODEL));
                sb.AppendFormat(",DCSUPPROPUNIT={0}", ClsSql.saveNullField(m.DCSUPPROPUNIT));
                sb.AppendFormat(",DCSUPPROPFACTORY={0}", ClsSql.saveNullField(m.DCSUPPROPFACTORY));
                sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
                sb.AppendFormat(" where DC_SUPPLIESPROP_ID= '{0}'", ClsSql.EncodeSql(m.DC_SUPPLIESPROP_ID));
                bool bln = DataBaseClass.ExeSql(sb.ToString());
                if (bln == true)
                    return new Message(true, "修改成功！", m.DC_SUPPLIESPROP_ID);
                else
                    return new Message(false, "修改失败，请检查各输入框是否正确！", "");
            }
        }

        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdyunit(DC_SUPPLIESPROP_Model m)
        {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("UPDATE DC_SUPPLIESPROP");
                sb.AppendFormat(" set ");
                sb.AppendFormat("DCSUPPROPUNIT={0}", ClsSql.saveNullField(m.DCSUPPROPUNIT));
                sb.AppendFormat(" where DC_SUPPLIESPROP_ID= '{0}'", ClsSql.EncodeSql(m.DC_SUPPLIESPROP_ID));
                bool bln = DataBaseClass.ExeSql(sb.ToString());
                if (bln == true)
                    return new Message(true, "修改成功！", m.DC_SUPPLIESPROP_ID);
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
        public static Message Del(DC_SUPPLIESPROP_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete DC_SUPPLIESPROP");
            sb.AppendFormat(" where DC_SUPPLIESPROP_ID= '{0}'", ClsSql.EncodeSql(m.DC_SUPPLIESPROP_ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", m.DC_SUPPLIESPROP_ID);
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        # region 判断是否存在
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool isExists(DC_SUPPLIESPROP_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from DC_SUPPLIESPROP where 1=1");
            if (string.IsNullOrEmpty(m.DC_SUPPLIESPROP_ID) == false)
                sb.AppendFormat(" and DC_SUPPLIESPROP_ID='{0}'", ClsSql.EncodeSql(m.DC_SUPPLIESPROP_ID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_SUPPLIESPROP_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      DC_SUPPLIESPROP");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DC_SUPPLIESPROP_ID) == false)
                sb.AppendFormat(" AND DC_SUPPLIESPROP_ID = '{0}'", ClsSql.EncodeSql(sw.DC_SUPPLIESPROP_ID));
            if (string.IsNullOrEmpty(sw.DCSUPPROPNAME) == false)
                sb.AppendFormat(" AND DCSUPPROPNAME = '{0}'", ClsSql.EncodeSql(sw.DCSUPPROPNAME));
            if (string.IsNullOrEmpty(sw.DCSUPPROPMODEL) == false)
                sb.AppendFormat(" AND DCSUPPROPMODEL = '{0}'", ClsSql.EncodeSql(sw.DCSUPPROPMODEL));
            if (string.IsNullOrEmpty(sw.DCSUPPROPUNIT) == false)
                sb.AppendFormat(" AND DCSUPPROPUNIT = '{0}'", ClsSql.EncodeSql(sw.DCSUPPROPUNIT));
            if (string.IsNullOrEmpty(sw.DCSUPPROPFACTORY) == false)
                sb.AppendFormat(" AND DCSUPPROPFACTORY = '{0}'", ClsSql.EncodeSql(sw.DCSUPPROPFACTORY));
            if (string.IsNullOrEmpty(sw.MARK) == false)
                sb.AppendFormat(" AND MARK = '{0}'", ClsSql.EncodeSql(sw.MARK));
            string sql = "";
            sql = "SELECT DC_SUPPLIESPROP_ID, DCSUPPROPNAME, DCSUPPROPMODEL, DCSUPPROPUNIT, DCSUPPROPFACTORY, MARK"
            + sb.ToString()
            + " order by DC_SUPPLIESPROP_ID";

            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion

        #region 通过物资id获取物资名称
        /// <summary>
        /// 通过物资id获取物资名称
        /// </summary>
        /// <param name="sw">sw</param>
        /// <returns></returns>
        public static string getname(DC_SUPPLIESPROP_SW sw)
        {
            string name = "";
            DataTable dt = getDT(sw);
            if (dt.Rows.Count > 0)
            {
                name = dt.Rows[0]["DCSUPPROPNAME"].ToString();
            }
            return name;
        }
        #endregion

        #region 通过物资id获取物资型号
        /// <summary>
        /// 通过物资id获取物资型号
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getmodel(DC_SUPPLIESPROP_SW sw)
        {
            string MODEL = "";
            DataTable dt = getDT(sw);
            if (dt.Rows.Count > 0)
            {
                MODEL = dt.Rows[0]["DCSUPPROPMODEL"].ToString();
            }
            return MODEL;
        }
        #endregion

        #region 通过物资id获取物资型号
        /// <summary>
        /// 通过物资id获取物资型号
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getunit(DC_SUPPLIESPROP_SW sw)
        {
            string unit = "";
            DataTable dt = getDT(sw);
            if (dt.Rows.Count > 0)
            {
                unit = dt.Rows[0]["DCSUPPROPUNIT"].ToString();
            }
            return unit;
        }
        #endregion

    }
}
