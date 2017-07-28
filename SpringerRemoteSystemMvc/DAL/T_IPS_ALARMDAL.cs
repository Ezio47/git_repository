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
    /// 数据访问类:T_IPS_ALARM
    /// </summary>
    public partial class T_IPS_ALARMDAL
    {
        public T_IPS_ALARMDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ALARMID", "T_IPS_ALARM");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ALARMID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_IPS_ALARM");
            strSql.Append(" where ALARMID=@ALARMID");
            SqlParameter[] parameters = {
					new SqlParameter("@ALARMID", SqlDbType.Int,4)
			};
            parameters[0].Value = ALARMID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(T_IPS_ALARMModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_IPS_ALARM(");
            strSql.Append("LONGITUDE,LATITUDE,HEIGHT,PHONE,ADDRESS,ALARMTIME,ALARMCONTENT)");
            strSql.Append(" values (");
            strSql.Append("@LONGITUDE,@LATITUDE,@HEIGHT,@PHONE,@ADDRESS,@ALARMTIME,@ALARMCONTENT)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@LONGITUDE", SqlDbType.Float,16),
					new SqlParameter("@LATITUDE", SqlDbType.Float,16),
					new SqlParameter("@HEIGHT", SqlDbType.Float,8),
					new SqlParameter("@PHONE", SqlDbType.VarChar,15),
					new SqlParameter("@ADDRESS", SqlDbType.VarChar,20),
					new SqlParameter("@ALARMTIME", SqlDbType.DateTime),
					new SqlParameter("@ALARMCONTENT", SqlDbType.Text)
					 };
            parameters[0].Value = model.LONGITUDE;
            parameters[1].Value = model.LATITUDE;
            parameters[2].Value = model.HEIGHT;
            parameters[3].Value = model.PHONE;
            parameters[4].Value = model.ADDRESS;
            parameters[5].Value = model.ALARMTIME;
            parameters[6].Value = model.ALARMCONTENT;
         

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
        public bool Update(T_IPS_ALARMModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_IPS_ALARM set ");
            strSql.Append("LONGITUDE=@LONGITUDE,");
            strSql.Append("LATITUDE=@LATITUDE,");
            strSql.Append("HEIGHT=@HEIGHT,");
            strSql.Append("PHONE=@PHONE,");
            strSql.Append("ADDRESS=@ADDRESS,");
            strSql.Append("ALARMTIME=@ALARMTIME,");
            strSql.Append("ALARMCONTENT=@ALARMCONTENT,");
            strSql.Append("ALARMSTATE=@ALARMSTATE,");
            strSql.Append("ALARMRESULT=@ALARMRESULT,");
            strSql.Append("ALARMUSERID=@ALARMUSERID");
            strSql.Append(" where ALARMID=@ALARMID");
            SqlParameter[] parameters = {
					new SqlParameter("@LONGITUDE", SqlDbType.Float,16),
					new SqlParameter("@LATITUDE", SqlDbType.Float,16),
					new SqlParameter("@HEIGHT", SqlDbType.Float,8),
					new SqlParameter("@PHONE", SqlDbType.VarChar,15),
					new SqlParameter("@ADDRESS", SqlDbType.VarChar,20),
					new SqlParameter("@ALARMTIME", SqlDbType.DateTime),
					new SqlParameter("@ALARMCONTENT", SqlDbType.Text),
					new SqlParameter("@ALARMSTATE", SqlDbType.SmallInt,2),
					new SqlParameter("@ALARMRESULT", SqlDbType.VarChar,100),
					new SqlParameter("@ALARMUSERID", SqlDbType.Int,4),
					new SqlParameter("@ALARMID", SqlDbType.Int,4)};
            parameters[0].Value = model.LONGITUDE;
            parameters[1].Value = model.LATITUDE;
            parameters[2].Value = model.HEIGHT;
            parameters[3].Value = model.PHONE;
            parameters[4].Value = model.ADDRESS;
            parameters[5].Value = model.ALARMTIME;
            parameters[6].Value = model.ALARMCONTENT;
            parameters[7].Value = model.ALARMSTATE;
            parameters[8].Value = model.ALARMRESULT;
            parameters[9].Value = model.ALARMUSERID;
            parameters[10].Value = model.ALARMID;

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
        public bool Delete(int ALARMID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPS_ALARM ");
            strSql.Append(" where ALARMID=@ALARMID");
            SqlParameter[] parameters = {
					new SqlParameter("@ALARMID", SqlDbType.Int,4)
			};
            parameters[0].Value = ALARMID;

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
        public bool DeleteList(string ALARMIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPS_ALARM ");
            strSql.Append(" where ALARMID in (" + ALARMIDlist + ")  ");
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
        public T_IPS_ALARMModel GetModel(int ALARMID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ALARMID,LONGITUDE,LATITUDE,HEIGHT,PHONE,ADDRESS,ALARMTIME,ALARMCONTENT,ALARMSTATE,ALARMRESULT,ALARMUSERID from T_IPS_ALARM ");
            strSql.Append(" where ALARMID=@ALARMID");
            SqlParameter[] parameters = {
					new SqlParameter("@ALARMID", SqlDbType.Int,4)
			};
            parameters[0].Value = ALARMID;

            T_IPS_ALARMModel model = new T_IPS_ALARMModel();
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
        public T_IPS_ALARMModel DataRowToModel(DataRow row)
        {
            T_IPS_ALARMModel model = new T_IPS_ALARMModel();
            if (row != null)
            {
                if (row["ALARMID"] != null && row["ALARMID"].ToString() != "")
                {
                    model.ALARMID = int.Parse(row["ALARMID"].ToString());
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
                if (row["PHONE"] != null)
                {
                    model.PHONE = row["PHONE"].ToString();
                }
                if (row["ADDRESS"] != null)
                {
                    model.ADDRESS = row["ADDRESS"].ToString();
                }
                if (row["ALARMTIME"] != null && row["ALARMTIME"].ToString() != "")
                {
                    model.ALARMTIME = DateTime.Parse(row["ALARMTIME"].ToString());
                }
                if (row["ALARMCONTENT"] != null)
                {
                    model.ALARMCONTENT = row["ALARMCONTENT"].ToString();
                }
                if (row["ALARMSTATE"] != null && row["ALARMSTATE"].ToString() != "")
                {
                    model.ALARMSTATE = int.Parse(row["ALARMSTATE"].ToString());
                }
                if (row["ALARMRESULT"] != null)
                {
                    model.ALARMRESULT = row["ALARMRESULT"].ToString();
                }
                if (row["ALARMUSERID"] != null && row["ALARMUSERID"].ToString() != "")
                {
                    model.ALARMUSERID = int.Parse(row["ALARMUSERID"].ToString());
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
            strSql.Append("select ALARMID,LONGITUDE,LATITUDE,HEIGHT,PHONE,ADDRESS,ALARMTIME,ALARMCONTENT,ALARMSTATE,ALARMRESULT,ALARMUSERID ");
            strSql.Append(" FROM T_IPS_ALARM ");
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
            strSql.Append(" ALARMID,LONGITUDE,LATITUDE,HEIGHT,PHONE,ADDRESS,ALARMTIME,ALARMCONTENT,ALARMSTATE,ALARMRESULT,ALARMUSERID ");
            strSql.Append(" FROM T_IPS_ALARM ");
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
            strSql.Append("select count(1) FROM T_IPS_ALARM ");
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
                strSql.Append("order by T.ALARMID desc");
            }
            strSql.Append(")AS Row, T.*  from T_IPS_ALARM T ");
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
            parameters[0].Value = "T_IPS_ALARM";
            parameters[1].Value = "ALARMID";
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
