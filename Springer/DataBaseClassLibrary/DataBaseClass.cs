using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
namespace DataBaseClassLibrary
{
    /// <summary>
    /// 数据库操作类
    /// 创建人：叶磊
    /// 创建时间：20150824
    /// 说明：数据库操作基本类，包含执行SQL执行语句、SQL查询语句
    /// </summary>
    public class DataBaseClass
    {
        /// <summary>
        /// 字义数据库连接字符串
        /// </summary>
        private string connStr;
        /// <summary>
        /// 定义SqlConnection
        /// </summary>
        private SqlConnection conn = new SqlConnection();
        /// <summary>
        /// 打开数据库连接 2007-3-28 10:48:29
        /// </summary>
        private void OpenConn()
        {
            if (conn.State != ConnectionState.Open)
            {
                connStr = ConfigurationManager.AppSettings["SpringerDBConnection"].ToString();
                conn = new SqlConnection(connStr);
                conn.Open();
            }
        }
        /// <summary>
        /// 关闭数据库连接 2007-3-23
        /// </summary>
        private void CloseConn()
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 根据SQL语句和表名返回DATASE
        /// </summary>
        /// <param name="sql">查询的语句</param>         
        /// <returns>返回 DataSet</returns>
        public static DataSet FullDataSet(string sql)
        {
            DataBaseClass sc = new DataBaseClass();
            sc.OpenConn();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, sc.conn);
            try
            {
                da.Fill(ds);
            }
            catch
            {
            }
            sc.CloseConn();
            return ds;
        }
        /// <summary>
        /// 返回分页的DataSet
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="startRecord">开始记录数</param>
        /// <param name="maxRecords">最大（结束）记录数</param>
        /// <param name="srcTables">表名称</param>
        /// <returns>参见模型</returns>
        public static DataSet FullDataSet(string sql, int startRecord, int maxRecords, string srcTables)
        {
            if (startRecord < 0)
                startRecord = 0;
            DataBaseClass sc = new DataBaseClass();
            sc.OpenConn();
            SqlCommand comm = new SqlCommand(sql, sc.conn);
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(comm);
            adp.Fill(ds, startRecord, maxRecords, srcTables);
            sc.CloseConn();
            return ds;
        }
        /// <summary>
        /// 得到SQL语句中第一个字段的值，只获取第一条记录第一个字段的值
        /// </summary>
        /// <param name="sql">要查询的SQL语句</param>
        /// <returns>返回值</returns>
        public static string ReturnSqlField(string sql)
        {
            string tempStr = "";
            try
            {
                DataSet ds = FullDataSet(sql);
                tempStr = ds.Tables[0].Rows[0][0].ToString();
                ds.Clear();
            }
            catch
            {
            }
            return tempStr;
        }
        /// <summary>
        /// 执行不返回值的SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>false/true 失败/成功</returns>
        public static bool ExeSql(string sql)
        {
            DataBaseClass sc = new DataBaseClass();
            sc.OpenConn();
            bool bln = false;
            try
            {
                SqlCommand comm = new SqlCommand(sql, sc.conn);
                comm.ExecuteNonQuery();
                bln = true;
            }
            catch
            {
                bln = false;
            }
            sc.CloseConn();
            return bln;
        }

        /// <summary>
        /// 判断SQL语句是否存在多条记录
        /// </summary>
        /// <param name="sql">要判断的SQL语句</param>
        /// <returns>false/true 不存在/存在多条记录</returns>
        public static bool JudgeRecordExists(string sql)
        {
            bool bln = false;
            DataSet ds = FullDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
                bln = true;
            ds.Clear();
            return bln;
        }
        /// <summary>
        /// 执行无返回值的存储过程
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="paramlist">参数数组</param>
        public static void ExeNoProc(string proc, SqlParameter[] paramlist)
        {
            DataSet ds = new DataSet();
            DataBaseClass sc = new DataBaseClass();
            sc.OpenConn();
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = sc.conn;
                comm.CommandTimeout = 20;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = proc;
                if (paramlist != null && paramlist.Length > 0)
                {
                    foreach (SqlParameter param in paramlist)
                        comm.Parameters.Add(param);
                }
                comm.ExecuteNonQuery();
            }
            catch
            {
            }
            sc.CloseConn();
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["SpringerDBConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch
                {
                    tx.Rollback();
                    return 0;
                }
            }
        }
    }
}
