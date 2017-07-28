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
    /// 数据访问类:T_IPS_REALDATADAL
    /// </summary>
    public partial class T_IPS_REALDATADAL
    {
        private readonly ILog logs = LogHelper.GetInstance();//log 
        public T_IPS_REALDATADAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long REALDATAID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_IPS_REALDATA");
            strSql.Append(" where REALDATAID=@REALDATAID");
            SqlParameter[] parameters = {
					new SqlParameter("@REALDATAID", SqlDbType.BigInt)
			};
            parameters[0].Value = REALDATAID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(T_IPS_REALDATAModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_IPS_REALDATA(");
            strSql.Append("PHONE,LONGITUDE,LATITUDE,HEIGHT,ELECTRIC,SPEED,DIRECTION,SBTIME,NOTE,ISOUTRAIL)");
            strSql.Append(" values (");
            strSql.Append("@PHONE,@LONGITUDE,@LATITUDE,@HEIGHT,@ELECTRIC,@SPEED,@DIRECTION,@SBTIME,@NOTE,@ISOUTRAIL)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@PHONE", SqlDbType.VarChar,15),
					new SqlParameter("@LONGITUDE", SqlDbType.Float,16),
					new SqlParameter("@LATITUDE", SqlDbType.Float,16),
					new SqlParameter("@HEIGHT", SqlDbType.Float,8),
					new SqlParameter("@ELECTRIC", SqlDbType.Float,8),
					new SqlParameter("@SPEED", SqlDbType.Float,8),
					new SqlParameter("@DIRECTION", SqlDbType.Float,8),
					new SqlParameter("@SBTIME", SqlDbType.DateTime),
					new SqlParameter("@NOTE", SqlDbType.Text),
                    new SqlParameter("@ISOUTRAIL",SqlDbType.Int)};
            parameters[0].Value = model.PHONE;
            parameters[1].Value = model.LONGITUDE;
            parameters[2].Value = model.LATITUDE;
            parameters[3].Value = model.HEIGHT;
            parameters[4].Value = model.ELECTRIC;
            parameters[5].Value = model.SPEED;
            parameters[6].Value = model.DIRECTION;
            parameters[7].Value = model.SBTIME;
            parameters[8].Value = model.NOTE;
            parameters[9].Value = model.ISOUTRAIL;
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
        public bool Update(T_IPS_REALDATAModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_IPS_REALDATA set ");
            strSql.Append("PHONE=@PHONE,");
            strSql.Append("LONGITUDE=@LONGITUDE,");
            strSql.Append("LATITUDE=@LATITUDE,");
            strSql.Append("HEIGHT=@HEIGHT,");
            strSql.Append("ELECTRIC=@ELECTRIC,");
            strSql.Append("SPEED=@SPEED,");
            strSql.Append("DIRECTION=@DIRECTION,");
            strSql.Append("SBTIME=@SBTIME,");
            strSql.Append("NOTE=@NOTE");
            strSql.Append(" where REALDATAID=@REALDATAID");
            SqlParameter[] parameters = {
					new SqlParameter("@PHONE", SqlDbType.VarChar,15),
					new SqlParameter("@LONGITUDE", SqlDbType.Float,16),
					new SqlParameter("@LATITUDE", SqlDbType.Float,16),
					new SqlParameter("@HEIGHT", SqlDbType.Float,8),
					new SqlParameter("@ELECTRIC", SqlDbType.Float,8),
					new SqlParameter("@SPEED", SqlDbType.Float,8),
					new SqlParameter("@DIRECTION", SqlDbType.Float,8),
					new SqlParameter("@SBTIME", SqlDbType.DateTime),
					new SqlParameter("@NOTE", SqlDbType.Text),
					new SqlParameter("@REALDATAID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.PHONE;
            parameters[1].Value = model.LONGITUDE;
            parameters[2].Value = model.LATITUDE;
            parameters[3].Value = model.HEIGHT;
            parameters[4].Value = model.ELECTRIC;
            parameters[5].Value = model.SPEED;
            parameters[6].Value = model.DIRECTION;
            parameters[7].Value = model.SBTIME;
            parameters[8].Value = model.NOTE;
            parameters[9].Value = model.REALDATAID;
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
        public bool Delete(long REALDATAID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPS_REALDATA ");
            strSql.Append(" where REALDATAID=@REALDATAID");
            SqlParameter[] parameters = {
					new SqlParameter("@REALDATAID", SqlDbType.BigInt)
			};
            parameters[0].Value = REALDATAID;

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
        public bool DeleteList(string REALDATAIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPS_REALDATA ");
            strSql.Append(" where REALDATAID in (" + REALDATAIDlist + ")  ");
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
        public T_IPS_REALDATAModel GetModel(long REALDATAID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 REALDATAID,PHONE,LONGITUDE,LATITUDE,HEIGHT,ELECTRIC,SPEED,DIRECTION,SBTIME,NOTE from T_IPS_REALDATA ");
            strSql.Append(" where REALDATAID=@REALDATAID");
            SqlParameter[] parameters = {
					new SqlParameter("@REALDATAID", SqlDbType.BigInt)
			};
            parameters[0].Value = REALDATAID;

            T_IPS_REALDATAModel model = new T_IPS_REALDATAModel();
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
        public T_IPS_REALDATAModel DataRowToModel(DataRow row)
        {
            T_IPS_REALDATAModel model = new T_IPS_REALDATAModel();
            if (row != null)
            {
                if (row["REALDATAID"] != null && row["REALDATAID"].ToString() != "")
                {
                    model.REALDATAID = long.Parse(row["REALDATAID"].ToString());
                }
                if (row["PHONE"] != null)
                {
                    model.PHONE = row["PHONE"].ToString();
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
                if (row["ELECTRIC"] != null && row["ELECTRIC"].ToString() != "")
                {
                    model.ELECTRIC = decimal.Parse(row["ELECTRIC"].ToString());
                }
                if (row["SPEED"] != null && row["SPEED"].ToString() != "")
                {
                    model.SPEED = decimal.Parse(row["SPEED"].ToString());
                }
                if (row["DIRECTION"] != null && row["DIRECTION"].ToString() != "")
                {
                    model.DIRECTION = decimal.Parse(row["DIRECTION"].ToString());
                }
                if (row["SBTIME"] != null && row["SBTIME"].ToString() != "")
                {
                    model.SBTIME = DateTime.Parse(row["SBTIME"].ToString());
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
            strSql.Append("select REALDATAID,PHONE,LONGITUDE,LATITUDE,HEIGHT,ELECTRIC,SPEED,DIRECTION,SBTIME,NOTE ");
            strSql.Append(" FROM T_IPS_REALDATA ");
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
            strSql.Append(" REALDATAID,PHONE,LONGITUDE,LATITUDE,HEIGHT,ELECTRIC,SPEED,DIRECTION,SBTIME,NOTE ");
            strSql.Append(" FROM T_IPS_REALDATA ");
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
            strSql.Append("select count(1) FROM T_IPS_REALDATA ");
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
                strSql.Append("order by T.REALDATAID desc");
            }
            strSql.Append(")AS Row, T.*  from T_IPS_REALDATA T ");
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
            parameters[0].Value = "T_IPS_REALDATA";
            parameters[1].Value = "REALDATAID";
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
