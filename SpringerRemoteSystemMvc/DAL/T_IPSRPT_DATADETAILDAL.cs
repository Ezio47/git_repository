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
    /// 数据访问类:T_IPSRPT_DATADETAIL
    /// </summary>
    public partial class T_IPSRPT_DATADETAILDAL
    {
        private readonly ILog logs = LogHelper.GetInstance();
        public T_IPSRPT_DATADETAILDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long REPORTID, long REPORTDETAILID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_IPSRPT_DATADETAIL");
            strSql.Append(" where REPORTID=@REPORTID and REPORTDETAILID=@REPORTDETAILID ");
            SqlParameter[] parameters = {
					new SqlParameter("@REPORTID", SqlDbType.BigInt),
					new SqlParameter("@REPORTDETAILID", SqlDbType.BigInt)			};
            parameters[0].Value = REPORTID;
            parameters[1].Value = REPORTDETAILID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(T_IPSRPT_DATADETAILModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_IPSRPT_DATADETAIL(");
            strSql.Append("REPORTID,LONGITUDE,LATITUDE,HEIGHT,DIRECTION,SBTIME)");
            strSql.Append(" values (");
            strSql.Append("@REPORTID,@LONGITUDE,@LATITUDE,@HEIGHT,@DIRECTION,@SBTIME)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@REPORTID", SqlDbType.BigInt),
					new SqlParameter("@LONGITUDE", SqlDbType.Float,16),
					new SqlParameter("@LATITUDE", SqlDbType.Float,16),
					new SqlParameter("@HEIGHT", SqlDbType.Float,8),
					new SqlParameter("@DIRECTION", SqlDbType.Float,8),
					new SqlParameter("@SBTIME", SqlDbType.DateTime)};
            parameters[0].Value = model.REPORTID;
            parameters[1].Value = model.LONGITUDE;
            parameters[2].Value = model.LATITUDE;
            parameters[3].Value = model.HEIGHT;
            parameters[4].Value = model.DIRECTION;
            parameters[5].Value = model.SBTIME;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(T_IPSRPT_DATADETAILModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_IPSRPT_DATADETAIL set ");
            strSql.Append("LONGITUDE=@LONGITUDE,");
            strSql.Append("LATITUDE=@LATITUDE,");
            strSql.Append("HEIGHT=@HEIGHT,");
            strSql.Append("DIRECTION=@DIRECTION,");
            strSql.Append("SBTIME=@SBTIME");
            strSql.Append(" where REPORTDETAILID=@REPORTDETAILID");
            SqlParameter[] parameters = {
					new SqlParameter("@LONGITUDE", SqlDbType.Float,16),
					new SqlParameter("@LATITUDE", SqlDbType.Float,16),
					new SqlParameter("@HEIGHT", SqlDbType.Float,8),
					new SqlParameter("@DIRECTION", SqlDbType.Float,8),
					new SqlParameter("@SBTIME", SqlDbType.DateTime),
					new SqlParameter("@REPORTDETAILID", SqlDbType.BigInt),
					new SqlParameter("@REPORTID", SqlDbType.BigInt)};
            parameters[0].Value = model.LONGITUDE;
            parameters[1].Value = model.LATITUDE;
            parameters[2].Value = model.HEIGHT;
            parameters[3].Value = model.DIRECTION;
            parameters[4].Value = model.SBTIME;
            parameters[5].Value = model.REPORTDETAILID;
            parameters[6].Value = model.REPORTID;

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
        public bool Delete(long REPORTDETAILID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSRPT_DATADETAIL ");
            strSql.Append(" where REPORTDETAILID=@REPORTDETAILID");
            SqlParameter[] parameters = {
					new SqlParameter("@REPORTDETAILID", SqlDbType.BigInt)
			};
            parameters[0].Value = REPORTDETAILID;

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
        public bool Delete(long REPORTID, long REPORTDETAILID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSRPT_DATADETAIL ");
            strSql.Append(" where REPORTID=@REPORTID and REPORTDETAILID=@REPORTDETAILID ");
            SqlParameter[] parameters = {
					new SqlParameter("@REPORTID", SqlDbType.BigInt),
					new SqlParameter("@REPORTDETAILID", SqlDbType.BigInt)			};
            parameters[0].Value = REPORTID;
            parameters[1].Value = REPORTDETAILID;

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
        public bool DeleteList(string REPORTDETAILIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSRPT_DATADETAIL ");
            strSql.Append(" where REPORTDETAILID in (" + REPORTDETAILIDlist + ")  ");
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
        public T_IPSRPT_DATADETAILModel GetModel(long REPORTDETAILID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 REPORTDETAILID,REPORTID,LONGITUDE,LATITUDE,HEIGHT,DIRECTION,SBTIME from T_IPSRPT_DATADETAIL ");
            strSql.Append(" where REPORTDETAILID=@REPORTDETAILID");
            SqlParameter[] parameters = {
					new SqlParameter("@REPORTDETAILID", SqlDbType.BigInt)
			};
            parameters[0].Value = REPORTDETAILID;

            T_IPSRPT_DATADETAILModel model = new T_IPSRPT_DATADETAILModel();
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
        public T_IPSRPT_DATADETAILModel DataRowToModel(DataRow row)
        {
            T_IPSRPT_DATADETAILModel model = new T_IPSRPT_DATADETAILModel();
            if (row != null)
            {
                if (row["REPORTDETAILID"] != null && row["REPORTDETAILID"].ToString() != "")
                {
                    model.REPORTDETAILID = long.Parse(row["REPORTDETAILID"].ToString());
                }
                if (row["REPORTID"] != null && row["REPORTID"].ToString() != "")
                {
                    model.REPORTID = long.Parse(row["REPORTID"].ToString());
                }
                if (row["LONGITUDE"] != null && row["LONGITUDE"].ToString() != "")
                {
                    model.LONGITUDE = decimal.Parse(row["LONGITUDE"].ToString());
                }
                if (row["LATITUDE"] != null && row["LATITUDE"].ToString() != "")
                {
                    model.LATITUDE = decimal.Parse(row["LATITUDE"].ToString());
                }
                if (row["HEIGHT"] != null && row["HEIGHT"].ToString() != "")
                {
                    model.HEIGHT = decimal.Parse(row["HEIGHT"].ToString());
                }
                if (row["DIRECTION"] != null && row["DIRECTION"].ToString() != "")
                {
                    model.DIRECTION = decimal.Parse(row["DIRECTION"].ToString());
                }
                if (row["SBTIME"] != null && row["SBTIME"].ToString() != "")
                {
                    model.SBTIME = DateTime.Parse(row["SBTIME"].ToString());
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
            strSql.Append("select REPORTDETAILID,REPORTID,LONGITUDE,LATITUDE,HEIGHT,DIRECTION,SBTIME ");
            strSql.Append(" FROM T_IPSRPT_DATADETAIL ");
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
            strSql.Append(" REPORTDETAILID,REPORTID,LONGITUDE,LATITUDE,HEIGHT,DIRECTION,SBTIME ");
            strSql.Append(" FROM T_IPSRPT_DATADETAIL ");
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
            strSql.Append("select count(1) FROM T_IPSRPT_DATADETAIL ");
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
                strSql.Append("order by T.REPORTDETAILID desc");
            }
            strSql.Append(")AS Row, T.*  from T_IPSRPT_DATADETAIL T ");
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
            parameters[0].Value = "T_IPSRPT_DATADETAIL";
            parameters[1].Value = "REPORTDETAILID";
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
