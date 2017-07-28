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
    /// 数据访问类:T_IPSCOL_DATAUPLOAD
    /// </summary>
    public partial class T_IPSCOL_DATAUPLOADDAL
    {
        public T_IPSCOL_DATAUPLOADDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("COLLECTUPLOADID", "T_IPSCOL_DATAUPLOAD");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long COLLECTID, int COLLECTUPLOADID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_IPSCOL_DATAUPLOAD");
            strSql.Append(" where COLLECTID=@COLLECTID and COLLECTUPLOADID=@COLLECTUPLOADID ");
            SqlParameter[] parameters = {
					new SqlParameter("@COLLECTID", SqlDbType.BigInt,8),
					new SqlParameter("@COLLECTUPLOADID", SqlDbType.Int,4)			};
            parameters[0].Value = COLLECTID;
            parameters[1].Value = COLLECTUPLOADID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(T_IPSCOL_DATAUPLOADModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_IPSCOL_DATAUPLOAD(");
            strSql.Append("COLLECTID,UPLOADURL,UPLOADNAME,UPLOADDESCRIBE,UPLOADTYPE)");
            strSql.Append(" values (");
            strSql.Append("@COLLECTID,@UPLOADURL,@UPLOADNAME,@UPLOADDESCRIBE,@UPLOADTYPE)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@COLLECTID", SqlDbType.BigInt,8),
					new SqlParameter("@UPLOADURL", SqlDbType.VarChar,20),
					new SqlParameter("@UPLOADNAME", SqlDbType.VarChar,15),
					new SqlParameter("@UPLOADDESCRIBE", SqlDbType.VarChar,30),
                    new SqlParameter("@UPLOADTYPE", SqlDbType.VarChar,10)};
            parameters[0].Value = model.COLLECTID;
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
        public bool Update(T_IPSCOL_DATAUPLOADModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_IPSCOL_DATAUPLOAD set ");
            strSql.Append("UPLOADURL=@UPLOADURL,");
            strSql.Append("UPLOADNAME=@UPLOADNAME,");
            strSql.Append("UPLOADDESCRIBE=@UPLOADDESCRIBE");
            strSql.Append(" where COLLECTUPLOADID=@COLLECTUPLOADID");
            SqlParameter[] parameters = {
					new SqlParameter("@UPLOADURL", SqlDbType.VarChar,20),
					new SqlParameter("@UPLOADNAME", SqlDbType.VarChar,15),
					new SqlParameter("@UPLOADDESCRIBE", SqlDbType.VarChar,30),
					new SqlParameter("@COLLECTUPLOADID", SqlDbType.Int,4),
					new SqlParameter("@COLLECTID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.UPLOADURL;
            parameters[1].Value = model.UPLOADNAME;
            parameters[2].Value = model.UPLOADDESCRIBE;
            parameters[3].Value = model.COLLECTUPLOADID;
            parameters[4].Value = model.COLLECTID;

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
        public bool Delete(int COLLECTUPLOADID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSCOL_DATAUPLOAD ");
            strSql.Append(" where COLLECTUPLOADID=@COLLECTUPLOADID");
            SqlParameter[] parameters = {
					new SqlParameter("@COLLECTUPLOADID", SqlDbType.Int,4)
			};
            parameters[0].Value = COLLECTUPLOADID;

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
        public bool Delete(long COLLECTID, int COLLECTUPLOADID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSCOL_DATAUPLOAD ");
            strSql.Append(" where COLLECTID=@COLLECTID and COLLECTUPLOADID=@COLLECTUPLOADID ");
            SqlParameter[] parameters = {
					new SqlParameter("@COLLECTID", SqlDbType.BigInt,8),
					new SqlParameter("@COLLECTUPLOADID", SqlDbType.Int,4)			};
            parameters[0].Value = COLLECTID;
            parameters[1].Value = COLLECTUPLOADID;

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
        public bool DeleteList(string COLLECTUPLOADIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSCOL_DATAUPLOAD ");
            strSql.Append(" where COLLECTUPLOADID in (" + COLLECTUPLOADIDlist + ")  ");
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
        public T_IPSCOL_DATAUPLOADModel GetModel(int COLLECTUPLOADID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 COLLECTUPLOADID,COLLECTID,UPLOADURL,UPLOADNAME,UPLOADDESCRIBE from T_IPSCOL_DATAUPLOAD ");
            strSql.Append(" where COLLECTUPLOADID=@COLLECTUPLOADID");
            SqlParameter[] parameters = {
					new SqlParameter("@COLLECTUPLOADID", SqlDbType.Int,4)
			};
            parameters[0].Value = COLLECTUPLOADID;

            T_IPSCOL_DATAUPLOADModel model = new T_IPSCOL_DATAUPLOADModel();
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
        public T_IPSCOL_DATAUPLOADModel DataRowToModel(DataRow row)
        {
            T_IPSCOL_DATAUPLOADModel model = new T_IPSCOL_DATAUPLOADModel();
            if (row != null)
            {
                if (row["COLLECTUPLOADID"] != null && row["COLLECTUPLOADID"].ToString() != "")
                {
                    model.COLLECTUPLOADID = int.Parse(row["COLLECTUPLOADID"].ToString());
                }
                if (row["COLLECTID"] != null && row["COLLECTID"].ToString() != "")
                {
                    model.COLLECTID = long.Parse(row["COLLECTID"].ToString());
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
            strSql.Append("select COLLECTUPLOADID,COLLECTID,UPLOADURL,UPLOADNAME,UPLOADDESCRIBE ");
            strSql.Append(" FROM T_IPSCOL_DATAUPLOAD ");
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
            strSql.Append(" COLLECTUPLOADID,COLLECTID,UPLOADURL,UPLOADNAME,UPLOADDESCRIBE ");
            strSql.Append(" FROM T_IPSCOL_DATAUPLOAD ");
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
            strSql.Append("select count(1) FROM T_IPSCOL_DATAUPLOAD ");
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
                strSql.Append("order by T.COLLECTUPLOADID desc");
            }
            strSql.Append(")AS Row, T.*  from T_IPSCOL_DATAUPLOAD T ");
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
            parameters[0].Value = "T_IPSCOL_DATAUPLOAD";
            parameters[1].Value = "COLLECTUPLOADID";
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
