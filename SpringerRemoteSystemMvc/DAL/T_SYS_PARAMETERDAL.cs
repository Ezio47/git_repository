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
    /// 数据访问类:T_SYS_PARAMETERDAL
    /// </summary>
    public partial class T_SYS_PARAMETERDAL
    {
        public T_SYS_PARAMETERDAL()
        { }

        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("PARAMID", "T_SYS_PARAMETER");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PARAMID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SYS_PARAMETER");
            strSql.Append(" where PARAMID=@PARAMID");
            SqlParameter[] parameters = {
					new SqlParameter("@PARAMID", SqlDbType.Int,4)
			};
            parameters[0].Value = PARAMID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(T_SYS_PARAMETERModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SYS_PARAMETER(");
            strSql.Append("PARAMFLAG,PARAMNAME,PARAMVALUE,PARAMMARK,SYSFLAG)");
            strSql.Append(" values (");
            strSql.Append("@PARAMFLAG,@PARAMNAME,@PARAMVALUE,@PARAMMARK,@SYSFLAG)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PARAMFLAG", SqlDbType.VarChar,20),
					new SqlParameter("@PARAMNAME", SqlDbType.VarChar,20),
					new SqlParameter("@PARAMVALUE", SqlDbType.VarChar,20),
					new SqlParameter("@PARAMMARK", SqlDbType.VarChar,100),
					new SqlParameter("@SYSFLAG", SqlDbType.VarChar,20)};
            parameters[0].Value = model.PARAMFLAG;
            parameters[1].Value = model.PARAMNAME;
            parameters[2].Value = model.PARAMVALUE;
            parameters[3].Value = model.PARAMMARK;
            parameters[4].Value = model.SYSFLAG;

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
        public bool Update(T_SYS_PARAMETERModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SYS_PARAMETER set ");
            strSql.Append("PARAMFLAG=@PARAMFLAG,");
            strSql.Append("PARAMNAME=@PARAMNAME,");
            strSql.Append("PARAMVALUE=@PARAMVALUE,");
            strSql.Append("PARAMMARK=@PARAMMARK,");
            strSql.Append("SYSFLAG=@SYSFLAG");
            strSql.Append(" where PARAMID=@PARAMID");
            SqlParameter[] parameters = {
					new SqlParameter("@PARAMFLAG", SqlDbType.VarChar,20),
					new SqlParameter("@PARAMNAME", SqlDbType.VarChar,20),
					new SqlParameter("@PARAMVALUE", SqlDbType.VarChar,20),
					new SqlParameter("@PARAMMARK", SqlDbType.VarChar,100),
					new SqlParameter("@SYSFLAG", SqlDbType.VarChar,20),
					new SqlParameter("@PARAMID", SqlDbType.Int,4)};
            parameters[0].Value = model.PARAMFLAG;
            parameters[1].Value = model.PARAMNAME;
            parameters[2].Value = model.PARAMVALUE;
            parameters[3].Value = model.PARAMMARK;
            parameters[4].Value = model.SYSFLAG;
            parameters[5].Value = model.PARAMID;

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
        public bool Delete(int PARAMID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SYS_PARAMETER ");
            strSql.Append(" where PARAMID=@PARAMID");
            SqlParameter[] parameters = {
					new SqlParameter("@PARAMID", SqlDbType.Int,4)
			};
            parameters[0].Value = PARAMID;

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
        public bool DeleteList(string PARAMIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SYS_PARAMETER ");
            strSql.Append(" where PARAMID in (" + PARAMIDlist + ")  ");
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
        public T_SYS_PARAMETERModel GetModel(int PARAMID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 PARAMID,PARAMFLAG,PARAMNAME,PARAMVALUE,PARAMMARK,SYSFLAG from T_SYS_PARAMETER ");
            strSql.Append(" where PARAMID=@PARAMID");
            SqlParameter[] parameters = {
					new SqlParameter("@PARAMID", SqlDbType.Int,4)
			};
            parameters[0].Value = PARAMID;

            T_SYS_PARAMETERModel model = new T_SYS_PARAMETERModel();
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
        public T_SYS_PARAMETERModel DataRowToModel(DataRow row)
        {
            T_SYS_PARAMETERModel model = new T_SYS_PARAMETERModel();
            if (row != null)
            {
                if (row["PARAMID"] != null && row["PARAMID"].ToString() != "")
                {
                    model.PARAMID = int.Parse(row["PARAMID"].ToString());
                }
                if (row["PARAMFLAG"] != null)
                {
                    model.PARAMFLAG = row["PARAMFLAG"].ToString();
                }
                if (row["PARAMNAME"] != null)
                {
                    model.PARAMNAME = row["PARAMNAME"].ToString();
                }
                if (row["PARAMVALUE"] != null)
                {
                    model.PARAMVALUE = row["PARAMVALUE"].ToString();
                }
                if (row["PARAMMARK"] != null)
                {
                    model.PARAMMARK = row["PARAMMARK"].ToString();
                }
                if (row["SYSFLAG"] != null)
                {
                    model.SYSFLAG = row["SYSFLAG"].ToString();
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
            strSql.Append("select PARAMID,PARAMFLAG,PARAMNAME,PARAMVALUE,PARAMMARK,SYSFLAG ");
            strSql.Append(" FROM T_SYS_PARAMETER ");
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
            strSql.Append(" PARAMID,PARAMFLAG,PARAMNAME,PARAMVALUE,PARAMMARK,SYSFLAG ");
            strSql.Append(" FROM T_SYS_PARAMETER ");
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
            strSql.Append("select count(1) FROM T_SYS_PARAMETER ");
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
                strSql.Append("order by T.PARAMID desc");
            }
            strSql.Append(")AS Row, T.*  from T_SYS_PARAMETER T ");
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
            parameters[0].Value = "T_SYS_PARAMETER";
            parameters[1].Value = "PARAMID";
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

