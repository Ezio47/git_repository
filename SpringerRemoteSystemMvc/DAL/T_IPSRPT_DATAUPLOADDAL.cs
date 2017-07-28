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
    /// 数据访问类:T_IPSRPT_DATAUPLOAD
    /// </summary>
    public partial class T_IPSRPT_DATAUPLOADDAL
    {
        public T_IPSRPT_DATAUPLOADDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("REPORTUPLOADID", "T_IPSRPT_DATAUPLOAD");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long REPORTID, int REPORTUPLOADID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_IPSRPT_DATAUPLOAD");
            strSql.Append(" where REPORTID=@REPORTID and REPORTUPLOADID=@REPORTUPLOADID ");
            SqlParameter[] parameters = {
					new SqlParameter("@REPORTID", SqlDbType.BigInt,8),
					new SqlParameter("@REPORTUPLOADID", SqlDbType.Int,4)			};
            parameters[0].Value = REPORTID;
            parameters[1].Value = REPORTUPLOADID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(T_IPSRPT_DATAUPLOADModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_IPSRPT_DATAUPLOAD(");
            strSql.Append("REPORTID,UPLOADURL,UPLOADNAME,UPLOADDESCRIBE,UPLOADTYPE)");
            strSql.Append(" values (");
            strSql.Append("@REPORTID,@UPLOADURL,@UPLOADNAME,@UPLOADDESCRIBE,@UPLOADTYPE)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@REPORTID", SqlDbType.BigInt,8),
					new SqlParameter("@UPLOADURL", SqlDbType.VarChar,100),
					new SqlParameter("@UPLOADNAME", SqlDbType.VarChar,50),
					new SqlParameter("@UPLOADDESCRIBE", SqlDbType.VarChar,100),
					new SqlParameter("@UPLOADTYPE", SqlDbType.VarChar,10)};
            parameters[0].Value = model.REPORTID;
            parameters[1].Value = model.UPLOADURL;
            parameters[2].Value = model.UPLOADNAME;
            parameters[3].Value = model.UPLOADDESCRIBE;
            parameters[4].Value = model.UPLOADTYPE;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(T_IPSRPT_DATAUPLOADModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_IPSRPT_DATAUPLOAD set ");
            strSql.Append("UPLOADURL=@UPLOADURL,");
            strSql.Append("UPLOADNAME=@UPLOADNAME,");
            strSql.Append("UPLOADDESCRIBE=@UPLOADDESCRIBE");
            strSql.Append(" where REPORTUPLOADID=@REPORTUPLOADID");
            SqlParameter[] parameters = {
					new SqlParameter("@UPLOADURL", SqlDbType.VarChar,100),
					new SqlParameter("@UPLOADNAME", SqlDbType.VarChar,50),
					new SqlParameter("@UPLOADDESCRIBE", SqlDbType.VarChar,100),
					new SqlParameter("@REPORTUPLOADID", SqlDbType.Int,4),
					new SqlParameter("@REPORTID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.UPLOADURL;
            parameters[1].Value = model.UPLOADNAME;
            parameters[2].Value = model.UPLOADDESCRIBE;
            parameters[3].Value = model.REPORTUPLOADID;
            parameters[4].Value = model.REPORTID;

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
        public bool Delete(int REPORTUPLOADID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSRPT_DATAUPLOAD ");
            strSql.Append(" where REPORTUPLOADID=@REPORTUPLOADID");
            SqlParameter[] parameters = {
					new SqlParameter("@REPORTUPLOADID", SqlDbType.Int,4)
			};
            parameters[0].Value = REPORTUPLOADID;

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
        public bool Delete(long REPORTID, int REPORTUPLOADID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSRPT_DATAUPLOAD ");
            strSql.Append(" where REPORTID=@REPORTID and REPORTUPLOADID=@REPORTUPLOADID ");
            SqlParameter[] parameters = {
					new SqlParameter("@REPORTID", SqlDbType.BigInt,8),
					new SqlParameter("@REPORTUPLOADID", SqlDbType.Int,4)			};
            parameters[0].Value = REPORTID;
            parameters[1].Value = REPORTUPLOADID;

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
        public bool DeleteList(string REPORTUPLOADIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSRPT_DATAUPLOAD ");
            strSql.Append(" where REPORTUPLOADID in (" + REPORTUPLOADIDlist + ")  ");
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
        public T_IPSRPT_DATAUPLOADModel GetModel(int REPORTUPLOADID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 REPORTUPLOADID,REPORTID,UPLOADURL,UPLOADNAME,UPLOADDESCRIBE from T_IPSRPT_DATAUPLOAD ");
            strSql.Append(" where REPORTUPLOADID=@REPORTUPLOADID");
            SqlParameter[] parameters = {
					new SqlParameter("@REPORTUPLOADID", SqlDbType.Int,4)
			};
            parameters[0].Value = REPORTUPLOADID;

            T_IPSRPT_DATAUPLOADModel model = new T_IPSRPT_DATAUPLOADModel();
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
        public T_IPSRPT_DATAUPLOADModel DataRowToModel(DataRow row)
        {
            T_IPSRPT_DATAUPLOADModel model = new T_IPSRPT_DATAUPLOADModel();
            if (row != null)
            {
                if (row["REPORTUPLOADID"] != null && row["REPORTUPLOADID"].ToString() != "")
                {
                    model.REPORTUPLOADID = int.Parse(row["REPORTUPLOADID"].ToString());
                }
                if (row["REPORTID"] != null && row["REPORTID"].ToString() != "")
                {
                    model.REPORTID = long.Parse(row["REPORTID"].ToString());
                }
                if (row["UPLOADURL"] != null)
                {
                    model.UPLOADURL = row["UPLOADURL"].ToString();
                }
                if (row["UPLOADNAME"] != null)
                {
                    model.UPLOADNAME = row["UPLOADNAME"].ToString();
                }
                if (row["UPLOADDESCRIBE"] != null)
                {
                    model.UPLOADDESCRIBE = row["UPLOADDESCRIBE"].ToString();
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
            strSql.Append("select REPORTUPLOADID,REPORTID,UPLOADURL,UPLOADNAME,UPLOADDESCRIBE ");
            strSql.Append(" FROM T_IPSRPT_DATAUPLOAD ");
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
            strSql.Append(" REPORTUPLOADID,REPORTID,UPLOADURL,UPLOADNAME,UPLOADDESCRIBE ");
            strSql.Append(" FROM T_IPSRPT_DATAUPLOAD ");
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
            strSql.Append("select count(1) FROM T_IPSRPT_DATAUPLOAD ");
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
                strSql.Append("order by T.REPORTUPLOADID desc");
            }
            strSql.Append(")AS Row, T.*  from T_IPSRPT_DATAUPLOAD T ");
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
            parameters[0].Value = "T_IPSRPT_DATAUPLOAD";
            parameters[1].Value = "REPORTUPLOADID";
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
