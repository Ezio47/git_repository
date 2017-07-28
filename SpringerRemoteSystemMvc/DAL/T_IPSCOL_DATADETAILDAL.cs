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
    /// 数据访问类:T_IPSCOL_DATADETAIL
    /// </summary>
    public partial class T_IPSCOL_DATADETAILDAL
    {
        public T_IPSCOL_DATADETAILDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long COLLECTID, long COLLECTDETAILID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_IPSCOL_DATADETAIL");
            strSql.Append(" where COLLECTID=@COLLECTID and COLLECTDETAILID=@COLLECTDETAILID ");
            SqlParameter[] parameters = {
					new SqlParameter("@COLLECTID", SqlDbType.BigInt,8),
					new SqlParameter("@COLLECTDETAILID", SqlDbType.BigInt,8)			};
            parameters[0].Value = COLLECTID;
            parameters[1].Value = COLLECTDETAILID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(T_IPSCOL_DATADETAILModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_IPSCOL_DATADETAIL(");
            strSql.Append("COLLECTID,LONGITUDE,LATITUDE,HEIGHT,DIRECTION,COLLECTTIME)");
            strSql.Append(" values (");
            strSql.Append("@COLLECTID,@LONGITUDE,@LATITUDE,@HEIGHT,@DIRECTION,@COLLECTTIME)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@COLLECTID", SqlDbType.BigInt,8),
					new SqlParameter("@LONGITUDE", SqlDbType.Float,16),
					new SqlParameter("@LATITUDE", SqlDbType.Float,16),
					new SqlParameter("@HEIGHT", SqlDbType.Float,8),
					new SqlParameter("@DIRECTION", SqlDbType.Float,8),
					new SqlParameter("@COLLECTTIME", SqlDbType.DateTime)};
            parameters[0].Value = model.COLLECTID;
            parameters[1].Value = model.LONGITUDE;
            parameters[2].Value = model.LATITUDE;
            parameters[3].Value = model.HEIGHT;
            parameters[4].Value = model.DIRECTION;
            parameters[5].Value = model.COLLECTTIME;

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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(T_IPSCOL_DATADETAILModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_IPSCOL_DATADETAIL set ");
            strSql.Append("LONGITUDE=@LONGITUDE,");
            strSql.Append("LATITUDE=@LATITUDE,");
            strSql.Append("HEIGHT=@HEIGHT,");
            strSql.Append("DIRECTION=@DIRECTION,");
            strSql.Append("COLLECTTIME=@COLLECTTIME");
            strSql.Append(" where COLLECTDETAILID=@COLLECTDETAILID");
            SqlParameter[] parameters = {
					new SqlParameter("@LONGITUDE", SqlDbType.Float,16),
					new SqlParameter("@LATITUDE", SqlDbType.Float,16),
					new SqlParameter("@HEIGHT", SqlDbType.Float,8),
					new SqlParameter("@DIRECTION", SqlDbType.Float,8),
					new SqlParameter("@COLLECTTIME", SqlDbType.DateTime),
					new SqlParameter("@COLLECTDETAILID", SqlDbType.BigInt,8),
					new SqlParameter("@COLLECTID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.LONGITUDE;
            parameters[1].Value = model.LATITUDE;
            parameters[2].Value = model.HEIGHT;
            parameters[3].Value = model.DIRECTION;
            parameters[4].Value = model.COLLECTTIME;
            parameters[5].Value = model.COLLECTDETAILID;
            parameters[6].Value = model.COLLECTID;

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
        public bool Delete(long COLLECTDETAILID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSCOL_DATADETAIL ");
            strSql.Append(" where COLLECTDETAILID=@COLLECTDETAILID");
            SqlParameter[] parameters = {
					new SqlParameter("@COLLECTDETAILID", SqlDbType.BigInt)
			};
            parameters[0].Value = COLLECTDETAILID;

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
        public bool Delete(long COLLECTID, long COLLECTDETAILID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSCOL_DATADETAIL ");
            strSql.Append(" where COLLECTID=@COLLECTID and COLLECTDETAILID=@COLLECTDETAILID ");
            SqlParameter[] parameters = {
					new SqlParameter("@COLLECTID", SqlDbType.BigInt,8),
					new SqlParameter("@COLLECTDETAILID", SqlDbType.BigInt,8)			};
            parameters[0].Value = COLLECTID;
            parameters[1].Value = COLLECTDETAILID;

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
        public bool DeleteList(string COLLECTDETAILIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSCOL_DATADETAIL ");
            strSql.Append(" where COLLECTDETAILID in (" + COLLECTDETAILIDlist + ")  ");
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
        public T_IPSCOL_DATADETAILModel GetModel(long COLLECTDETAILID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 COLLECTDETAILID,COLLECTID,LONGITUDE,LATITUDE,HEIGHT,DIRECTION,COLLECTTIME from T_IPSCOL_DATADETAIL ");
            strSql.Append(" where COLLECTDETAILID=@COLLECTDETAILID");
            SqlParameter[] parameters = {
					new SqlParameter("@COLLECTDETAILID", SqlDbType.BigInt)
			};
            parameters[0].Value = COLLECTDETAILID;

            T_IPSCOL_DATADETAILModel model = new T_IPSCOL_DATADETAILModel();
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
        public T_IPSCOL_DATADETAILModel DataRowToModel(DataRow row)
        {
            T_IPSCOL_DATADETAILModel model = new T_IPSCOL_DATADETAILModel();
            if (row != null)
            {
                if (row["COLLECTDETAILID"] != null && row["COLLECTDETAILID"].ToString() != "")
                {
                    model.COLLECTDETAILID = long.Parse(row["COLLECTDETAILID"].ToString());
                }
                if (row["COLLECTID"] != null && row["COLLECTID"].ToString() != "")
                {
                    model.COLLECTID = long.Parse(row["COLLECTID"].ToString());
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
                if (row["COLLECTTIME"] != null && row["COLLECTTIME"].ToString() != "")
                {
                    model.COLLECTTIME = DateTime.Parse(row["COLLECTTIME"].ToString());
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
            strSql.Append("select COLLECTDETAILID,COLLECTID,LONGITUDE,LATITUDE,HEIGHT,DIRECTION,COLLECTTIME ");
            strSql.Append(" FROM T_IPSCOL_DATADETAIL ");
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
            strSql.Append(" COLLECTDETAILID,COLLECTID,LONGITUDE,LATITUDE,HEIGHT,DIRECTION,COLLECTTIME ");
            strSql.Append(" FROM T_IPSCOL_DATADETAIL ");
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
            strSql.Append("select count(1) FROM T_IPSCOL_DATADETAIL ");
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
                strSql.Append("order by T.COLLECTDETAILID desc");
            }
            strSql.Append(")AS Row, T.*  from T_IPSCOL_DATADETAIL T ");
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
            parameters[0].Value = "T_IPSCOL_DATADETAIL";
            parameters[1].Value = "COLLECTDETAILID";
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
