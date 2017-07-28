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
    /// 数据访问类:T_SYS_DICT
    /// </summary>
    public partial class T_SYS_DICTDAL
    {
        public T_SYS_DICTDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("DICTID", "T_SYS_DICT");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DICTID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SYS_DICT");
            strSql.Append(" where DICTID=@DICTID");
            SqlParameter[] parameters = {
					new SqlParameter("@DICTID", SqlDbType.Int,4)
			};
            parameters[0].Value = DICTID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(T_SYS_DICTModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SYS_DICT(");
            strSql.Append("DICTTYPEID,DICTNAME,DICTVALUE,STANDBY1)");
            strSql.Append(" values (");
            strSql.Append("@DICTTYPEID,@DICTNAME,@DICTVALUE,@STANDBY1)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DICTTYPEID", SqlDbType.VarChar,20),
					new SqlParameter("@DICTNAME", SqlDbType.VarChar,20),
					new SqlParameter("@DICTVALUE", SqlDbType.VarChar,20),
					new SqlParameter("@STANDBY1", SqlDbType.VarChar,20)};
            parameters[0].Value = model.DICTTYPEID;
            parameters[1].Value = model.DICTNAME;
            parameters[2].Value = model.DICTVALUE;
            parameters[3].Value = model.STANDBY1;

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
        public bool Update(T_SYS_DICTModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SYS_DICT set ");
            strSql.Append("DICTTYPEID=@DICTTYPEID,");
            strSql.Append("DICTNAME=@DICTNAME,");
            strSql.Append("DICTVALUE=@DICTVALUE,");
            strSql.Append("STANDBY1=@STANDBY1");
            strSql.Append(" where DICTID=@DICTID");
            SqlParameter[] parameters = {
					new SqlParameter("@DICTTYPEID", SqlDbType.VarChar,20),
					new SqlParameter("@DICTNAME", SqlDbType.VarChar,20),
					new SqlParameter("@DICTVALUE", SqlDbType.VarChar,20),
					new SqlParameter("@STANDBY1", SqlDbType.VarChar,20),
					new SqlParameter("@DICTID", SqlDbType.Int,4)};
            parameters[0].Value = model.DICTTYPEID;
            parameters[1].Value = model.DICTNAME;
            parameters[2].Value = model.DICTVALUE;
            parameters[3].Value = model.STANDBY1;
            parameters[4].Value = model.DICTID;

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
        public bool Delete(int DICTID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SYS_DICT ");
            strSql.Append(" where DICTID=@DICTID");
            SqlParameter[] parameters = {
					new SqlParameter("@DICTID", SqlDbType.Int,4)
			};
            parameters[0].Value = DICTID;

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
        public bool DeleteList(string DICTIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SYS_DICT ");
            strSql.Append(" where DICTID in (" + DICTIDlist + ")  ");
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
        public T_SYS_DICTModel GetModel(int DICTID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 DICTID,DICTTYPEID,DICTNAME,DICTVALUE  from T_SYS_DICT ");
            strSql.Append(" where DICTID=@DICTID");
            SqlParameter[] parameters = {
					new SqlParameter("@DICTID", SqlDbType.Int,4)
			};
            parameters[0].Value = DICTID;

            T_SYS_DICTModel model = new T_SYS_DICTModel();
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
        public T_SYS_DICTModel DataRowToModel(DataRow row)
        {
            T_SYS_DICTModel model = new T_SYS_DICTModel();
            if (row != null)
            {
                if (row["DICTID"] != null && row["DICTID"].ToString() != "")
                {
                    model.DICTID = int.Parse(row["DICTID"].ToString());
                }
                if (row["DICTTYPEID"] != null)
                {
                    model.DICTTYPEID = row["DICTTYPEID"].ToString();
                }
                if (row["DICTNAME"] != null)
                {
                    model.DICTNAME = row["DICTNAME"].ToString();
                }
                if (row["DICTVALUE"] != null)
                {
                    model.DICTVALUE = row["DICTVALUE"].ToString();
                }

                if (row["STANDBY1"] != null)
                {
                    model.STANDBY1 = row["STANDBY1"].ToString();
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
            strSql.Append("select *  ");
            strSql.Append(" FROM T_SYS_DICT  A left join T_SYS_DICTTYPE B   ON A. DICTTYPEID = B.DICTTYPEID ");
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
            strSql.Append(" DICTID,DICTTYPEID,DICTNAME,DICTVALUE,STANDBY1 ");
            strSql.Append(" FROM T_SYS_DICT ");
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
            strSql.Append("select count(1) FROM T_SYS_DICT ");
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
                strSql.Append("order by T.DICTID desc");
            }
            strSql.Append(")AS Row, T.*  from T_SYS_DICT T ");
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
            parameters[0].Value = "T_SYS_DICT";
            parameters[1].Value = "DICTID";
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
