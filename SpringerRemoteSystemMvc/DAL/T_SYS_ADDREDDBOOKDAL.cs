using log4net;
using Springer.Common.Utils;
using Springer.DBUtility;
using Springer.EntityModel;
using Springer.EntityModel.Entity;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace Springer.DAL
{
    /// <summary>
    /// 数据访问类:T_SYS_ADDREDDBOOK
    /// </summary>
    public partial class T_SYS_ADDREDDBOOKDAL
    {
        private readonly ILog logs = LogHelper.GetInstance();//log 
        public T_SYS_ADDREDDBOOKDAL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ADID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SYS_ADDREDDBOOK");
            strSql.Append(" where ADID=@ADID");
            SqlParameter[] parameters = {
					new SqlParameter("@ADID", SqlDbType.Int,4)
			};
            parameters[0].Value = ADID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(T_SYS_ADDREDDBOOK model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SYS_ADDREDDBOOK(");
            strSql.Append("ATID,ORGNO,ADNAME,USERJOB,PHONE,Tell,ORDERBY)");
            strSql.Append(" values (");
            strSql.Append("@ATID,@ORGNO,@ADNAME,@USERJOB,@PHONE,@Tell,@ORDERBY)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ATID", SqlDbType.Int,4),
					new SqlParameter("@ORGNO", SqlDbType.VarChar,15),
					new SqlParameter("@ADNAME", SqlDbType.VarChar,15),
					new SqlParameter("@USERJOB", SqlDbType.VarChar,20),
					new SqlParameter("@PHONE", SqlDbType.VarChar,15),
					new SqlParameter("@Tell", SqlDbType.VarChar,20),
					new SqlParameter("@ORDERBY", SqlDbType.Int,4)};
            parameters[0].Value = model.ATID;
            parameters[1].Value = model.ORGNO;
            parameters[2].Value = model.ADNAME;
            parameters[3].Value = model.USERJOB;
            parameters[4].Value = model.PHONE;
            parameters[5].Value = model.Tell;
            parameters[6].Value = model.ORDERBY;

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
        public bool Update(T_SYS_ADDREDDBOOK model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SYS_ADDREDDBOOK set ");
            strSql.Append("ATID=@ATID,");
            strSql.Append("ORGNO=@ORGNO,");
            strSql.Append("ADNAME=@ADNAME,");
            strSql.Append("USERJOB=@USERJOB,");
            strSql.Append("PHONE=@PHONE,");
            strSql.Append("Tell=@Tell,");
            strSql.Append("ORDERBY=@ORDERBY");
            strSql.Append(" where ADID=@ADID");
            SqlParameter[] parameters = {
					new SqlParameter("@ATID", SqlDbType.Int,4),
					new SqlParameter("@ORGNO", SqlDbType.VarChar,15),
					new SqlParameter("@ADNAME", SqlDbType.VarChar,15),
					new SqlParameter("@USERJOB", SqlDbType.VarChar,20),
					new SqlParameter("@PHONE", SqlDbType.VarChar,15),
					new SqlParameter("@Tell", SqlDbType.VarChar,20),
					new SqlParameter("@ORDERBY", SqlDbType.Int,4),
					new SqlParameter("@ADID", SqlDbType.Int,4)};
            parameters[0].Value = model.ATID;
            parameters[1].Value = model.ORGNO;
            parameters[2].Value = model.ADNAME;
            parameters[3].Value = model.USERJOB;
            parameters[4].Value = model.PHONE;
            parameters[5].Value = model.Tell;
            parameters[6].Value = model.ORDERBY;
            parameters[7].Value = model.ADID;

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
        public bool Delete(int ADID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SYS_ADDREDDBOOK ");
            strSql.Append(" where ADID=@ADID");
            SqlParameter[] parameters = {
					new SqlParameter("@ADID", SqlDbType.Int,4)
			};
            parameters[0].Value = ADID;

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
        public bool DeleteList(string ADIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SYS_ADDREDDBOOK ");
            strSql.Append(" where ADID in (" + ADIDlist + ")  ");
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
        public T_SYS_ADDREDDBOOK GetModel(int ADID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ADID,ATID,ORGNO,ADNAME,USERJOB,PHONE,Tell,ORDERBY from T_SYS_ADDREDDBOOK ");
            strSql.Append(" where ADID=@ADID");
            SqlParameter[] parameters = {
					new SqlParameter("@ADID", SqlDbType.Int,4)
			};
            parameters[0].Value = ADID;

            T_SYS_ADDREDDBOOK model = new T_SYS_ADDREDDBOOK();
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
        public T_SYS_ADDREDDBOOK DataRowToModel(DataRow row)
        {
            T_SYS_ADDREDDBOOK model = new T_SYS_ADDREDDBOOK();
            if (row != null)
            {
                if (row["ADID"] != null && row["ADID"].ToString() != "")
                {
                    model.ADID = int.Parse(row["ADID"].ToString());
                }
                if (row["ATID"] != null && row["ATID"].ToString() != "")
                {
                    model.ATID = int.Parse(row["ATID"].ToString());
                }
                if (row["ORGNO"] != null)
                {
                    model.ORGNO = row["ORGNO"].ToString();
                }
                if (row["ADNAME"] != null)
                {
                    model.ADNAME = row["ADNAME"].ToString();
                }
                if (row["USERJOB"] != null)
                {
                    model.USERJOB = row["USERJOB"].ToString();
                }
                if (row["PHONE"] != null)
                {
                    model.PHONE = row["PHONE"].ToString();
                }
                if (row["Tell"] != null)
                {
                    model.Tell = row["Tell"].ToString();
                }
                if (row["ORDERBY"] != null && row["ORDERBY"].ToString() != "")
                {
                    model.ORDERBY = int.Parse(row["ORDERBY"].ToString());
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
            strSql.Append("select ADID,ATID,ORGNO,ADNAME,USERJOB,PHONE,Tell,ORDERBY ");
            strSql.Append(" FROM T_SYS_ADDREDDBOOK ");
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
            strSql.Append(" ADID,ATID,ORGNO,ADNAME,USERJOB,PHONE,Tell,ORDERBY ");
            strSql.Append(" FROM T_SYS_ADDREDDBOOK ");
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
            strSql.Append("select count(1) FROM T_SYS_ADDREDDBOOK ");
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
                strSql.Append("order by T.ADID desc");
            }
            strSql.Append(")AS Row, T.*  from T_SYS_ADDREDDBOOK T ");
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
            parameters[0].Value = "T_SYS_ADDREDDBOOK";
            parameters[1].Value = "ADID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 通讯录
        /// </summary>
        /// <returns></returns>
        public DataSet GetTXLDt(string top)
        {
            StringBuilder strSql = new StringBuilder();
            if (string.IsNullOrEmpty(top))
            {
                strSql.Append("select ADID, ADNAME,USERJOB,PHONE, RTNAME ");
            }
            else
            {
                strSql.AppendFormat("select top {0} ADID, ADNAME,USERJOB,PHONE, RTNAME ", top);
            }
            strSql.Append(" FROM T_SYS_ADDREDDBOOK  a  left join T_SYS_ADDREDDTYPE b on a.ATID=b.ATID");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取通讯录模型
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public TXLModel DataRowToTXLModel(DataRow row)
        {
            TXLModel model = new TXLModel();
            if (row != null)
            {
                if (row["ADID"] != null && row["ADID"].ToString() != "")
                {
                    model.ADID = row["ADID"].ToString();
                }
                if (row["ADNAME"] != null)
                {
                    model.Name = row["ADNAME"].ToString();
                }
                if (row["USERJOB"] != null)
                {
                    model.DepJob = row["USERJOB"].ToString();
                }
                if (row["PHONE"] != null)
                {
                    model.Phone = row["PHONE"].ToString();
                }
                if (row["RTNAME"] != null)
                {
                    model.DepName = row["RTNAME"].ToString();
                }

            }
            return model;
        }
        #endregion  ExtensionMethod
    }
}

