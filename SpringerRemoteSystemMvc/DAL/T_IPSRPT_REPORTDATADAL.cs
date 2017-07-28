using log4net;
using Springer.Common.Utils;
using Springer.DBUtility;
using Springer.EntityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Springer.DAL
{
    /// <summary>
    /// 数据访问类:T_IPSRPT_REPORTDATA
    /// </summary>
    public partial class T_IPSRPT_REPORTDATADAL
    {
        private readonly ILog logs = LogHelper.GetInstance();
        public T_IPSRPT_REPORTDATADAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("HID", "T_IPSRPT_REPORTDATA");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int HID, long REPORTID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_IPSRPT_REPORTDATA");
            strSql.Append(" where HID=@HID and REPORTID=@REPORTID ");
            SqlParameter[] parameters = {
					new SqlParameter("@HID", SqlDbType.Int),
					new SqlParameter("@REPORTID", SqlDbType.BigInt)			};
            parameters[0].Value = HID;
            parameters[1].Value = REPORTID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(T_IPSRPT_REPORTDATAModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_IPSRPT_REPORTDATA(");
            strSql.Append("HID,SYSTYPEVALUE,ADDRESS,REPORTTIME,COLLECTNAME)");
            strSql.Append(" values (");
            strSql.Append("@HID,@SYSTYPEVALUE,@ADDRESS,@REPORTTIME,@COLLECTNAME)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@HID", SqlDbType.Int),
					new SqlParameter("@SYSTYPEVALUE", SqlDbType.VarChar,10),
					new SqlParameter("@ADDRESS", SqlDbType.VarChar,20),
					new SqlParameter("@REPORTTIME", SqlDbType.DateTime),
					new SqlParameter("@COLLECTNAME", SqlDbType.VarChar,100)};
            parameters[0].Value = model.HID;
            parameters[1].Value = model.SYSTYPEVALUE;
            parameters[2].Value = model.ADDRESS;
            parameters[3].Value = model.REPORTTIME;
            parameters[4].Value = model.COLLECTNAME;

            try
            {
                object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(obj);
                }

            }
            catch (Exception ex)
            {
                logs.Error(ex.Message);
                throw;
            }

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long REPORTID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSRPT_REPORTDATA ");
            strSql.Append(" where REPORTID=@REPORTID");
            SqlParameter[] parameters = {
					new SqlParameter("@REPORTID", SqlDbType.BigInt)
			};
            parameters[0].Value = REPORTID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int HID, long REPORTID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSRPT_REPORTDATA ");
            strSql.Append(" where HID=@HID and REPORTID=@REPORTID ");
            SqlParameter[] parameters = {
					new SqlParameter("@HID", SqlDbType.Int),
					new SqlParameter("@REPORTID", SqlDbType.BigInt)			};
            parameters[0].Value = HID;
            parameters[1].Value = REPORTID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string REPORTIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSRPT_REPORTDATA ");
            strSql.Append(" where REPORTID in (" + REPORTIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_IPSRPT_REPORTDATAModel GetModel(long REPORTID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 REPORTID,HID,SYSTYPEVALUE,ADDRESS,REPORTTIME,COLLECTNAME,ISDEAL,REPORTRESULT,NOTE from T_IPSRPT_REPORTDATA ");
            strSql.Append(" where REPORTID=@REPORTID");
            SqlParameter[] parameters = {
					new SqlParameter("@REPORTID", SqlDbType.BigInt)
			};
            parameters[0].Value = REPORTID;

            T_IPSRPT_REPORTDATAModel model = new T_IPSRPT_REPORTDATAModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_IPSRPT_REPORTDATAModel DataRowToModel(DataRow row)
        {
            T_IPSRPT_REPORTDATAModel model = new T_IPSRPT_REPORTDATAModel();
            if (row != null)
            {
                if (row["REPORTID"] != null && row["REPORTID"].ToString() != "")
                {
                    model.REPORTID = long.Parse(row["REPORTID"].ToString());
                }
                if (row["HID"] != null && row["HID"].ToString() != "")
                {
                    model.HID = int.Parse(row["HID"].ToString());
                }
                if (row["SYSTYPEVALUE"] != null)
                {
                    model.SYSTYPEVALUE = row["SYSTYPEVALUE"].ToString();
                }
                if (row["ADDRESS"] != null)
                {
                    model.ADDRESS = row["ADDRESS"].ToString();
                }
                if (row["REPORTTIME"] != null && row["REPORTTIME"].ToString() != "")
                {
                    model.REPORTTIME = DateTime.Parse(row["REPORTTIME"].ToString());
                }
                if (row["COLLECTNAME"] != null)
                {
                    model.COLLECTNAME = row["COLLECTNAME"].ToString();
                }
                if (row["ISDEAL"] != null && row["ISDEAL"].ToString() != "")
                {
                    model.ISDEAL = int.Parse(row["ISDEAL"].ToString());
                }
                if (row["REPORTRESULT"] != null)
                {
                    model.REPORTRESULT = row["REPORTRESULT"].ToString();
                }
                if (row["NOTE"] != null)
                {
                    model.NOTE = row["NOTE"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select REPORTID,HID,SYSTYPEVALUE,ADDRESS,REPORTTIME,COLLECTNAME,ISDEAL,REPORTRESULT,NOTE ");
            strSql.Append(" FROM T_IPSRPT_REPORTDATA ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" REPORTID,HID,SYSTYPEVALUE,ADDRESS,REPORTTIME,COLLECTNAME,ISDEAL,REPORTRESULT,NOTE ");
            strSql.Append(" FROM T_IPSRPT_REPORTDATA ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM T_IPSRPT_REPORTDATA ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.REPORTID desc");
            }
            strSql.Append(")AS Row, T.*  from T_IPSRPT_REPORTDATA T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "T_IPSRPT_REPORTDATA";
            parameters[1].Value = "REPORTID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
