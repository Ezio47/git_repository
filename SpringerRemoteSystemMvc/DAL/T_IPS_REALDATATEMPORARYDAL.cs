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
    /// 实时数据最新记录表
    /// </summary>
    public class T_IPS_REALDATATEMPORARYDAL
    {
        private readonly ILog logs = LogHelper.GetInstance();//log 

        public bool Exists(long REALDATAID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_IPS_REALDATATEMPORARY");
            strSql.Append(" where ");
            strSql.Append(" REALDATAID = @REALDATAID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@REALDATAID", SqlDbType.BigInt)
			};
            parameters[0].Value = REALDATAID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(T_IPS_REALDATATEMPORARYModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_IPS_REALDATATEMPORARY(");
            strSql.Append("NOTE,ORGNO,SBDATE,SBTIMEBEGIN,PATROLLENGTH,ISOUTRAIL,USERID,LONGITUDE,LATITUDE,HEIGHT,ELECTRIC,SPEED,DIRECTION,SBTIME");
            strSql.Append(") values (");
            strSql.Append("@NOTE,@ORGNO,@SBDATE,@SBTIMEBEGIN,@PATROLLENGTH,@ISOUTRAIL,@USERID,@LONGITUDE,@LATITUDE,@HEIGHT,@ELECTRIC,@SPEED,@DIRECTION,@SBTIME");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@NOTE", SqlDbType.Text) ,            
                        new SqlParameter("@ORGNO", SqlDbType.VarChar,15) ,            
                        new SqlParameter("@SBDATE", SqlDbType.Date) ,            
                        new SqlParameter("@SBTIMEBEGIN", SqlDbType.DateTime) ,            
                        new SqlParameter("@PATROLLENGTH", SqlDbType.Float,8) ,            
                        new SqlParameter("@ISOUTRAIL", SqlDbType.Int,4) ,            
                        new SqlParameter("@USERID", SqlDbType.Int,4) ,            
                        new SqlParameter("@LONGITUDE", SqlDbType.Float,8) ,            
                        new SqlParameter("@LATITUDE", SqlDbType.Float,8) ,            
                        new SqlParameter("@HEIGHT", SqlDbType.Float,8) ,            
                        new SqlParameter("@ELECTRIC", SqlDbType.Float,8) ,            
                        new SqlParameter("@SPEED", SqlDbType.Float,8) ,            
                        new SqlParameter("@DIRECTION", SqlDbType.Float,8) ,            
                        new SqlParameter("@SBTIME", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.NOTE;
            parameters[1].Value = model.ORGNO;
            parameters[2].Value = model.SBDATE;
            parameters[3].Value = model.SBTIMEBEGIN;
            parameters[4].Value = model.PATROLLENGTH;
            parameters[5].Value = model.ISOUTRAIL;
            parameters[6].Value = model.USERID;
            parameters[7].Value = model.LONGITUDE;
            parameters[8].Value = model.LATITUDE;
            parameters[9].Value = model.HEIGHT;
            parameters[10].Value = model.ELECTRIC;
            parameters[11].Value = model.SPEED;
            parameters[12].Value = model.DIRECTION;
            parameters[13].Value = model.SBTIME;

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
        public bool Update(T_IPS_REALDATATEMPORARYModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_IPS_REALDATATEMPORARY set ");

            strSql.Append(" NOTE = @NOTE , ");
            strSql.Append(" ORGNO = @ORGNO , ");
            strSql.Append(" SBDATE = @SBDATE , ");
            strSql.Append(" SBTIMEBEGIN = @SBTIMEBEGIN , ");
            strSql.Append(" PATROLLENGTH = @PATROLLENGTH , ");
            strSql.Append(" ISOUTRAIL = @ISOUTRAIL , ");
            strSql.Append(" USERID = @USERID , ");
            strSql.Append(" LONGITUDE = @LONGITUDE , ");
            strSql.Append(" LATITUDE = @LATITUDE , ");
            strSql.Append(" HEIGHT = @HEIGHT , ");
            strSql.Append(" ELECTRIC = @ELECTRIC , ");
            strSql.Append(" SPEED = @SPEED , ");
            strSql.Append(" DIRECTION = @DIRECTION , ");
            strSql.Append(" SBTIME = @SBTIME  ");
            strSql.Append(" where REALDATAID=@REALDATAID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@REALDATAID", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@NOTE", SqlDbType.Text) ,            
                        new SqlParameter("@ORGNO", SqlDbType.VarChar,15) ,            
                        new SqlParameter("@SBDATE", SqlDbType.Date) ,            
                        new SqlParameter("@SBTIMEBEGIN", SqlDbType.DateTime) ,            
                        new SqlParameter("@PATROLLENGTH", SqlDbType.Float,8) ,            
                        new SqlParameter("@ISOUTRAIL", SqlDbType.Int,4) ,            
                        new SqlParameter("@USERID", SqlDbType.Int,4) ,            
                        new SqlParameter("@LONGITUDE", SqlDbType.Float,8) ,            
                        new SqlParameter("@LATITUDE", SqlDbType.Float,8) ,            
                        new SqlParameter("@HEIGHT", SqlDbType.Float,8) ,            
                        new SqlParameter("@ELECTRIC", SqlDbType.Float,8) ,            
                        new SqlParameter("@SPEED", SqlDbType.Float,8) ,            
                        new SqlParameter("@DIRECTION", SqlDbType.Float,8) ,            
                        new SqlParameter("@SBTIME", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.REALDATAID;
            parameters[1].Value = model.NOTE;
            parameters[2].Value = model.ORGNO;
            parameters[3].Value = model.SBDATE;
            parameters[4].Value = model.SBTIMEBEGIN;
            parameters[5].Value = model.PATROLLENGTH;
            parameters[6].Value = model.ISOUTRAIL;
            parameters[7].Value = model.USERID;
            parameters[8].Value = model.LONGITUDE;
            parameters[9].Value = model.LATITUDE;
            parameters[10].Value = model.HEIGHT;
            parameters[11].Value = model.ELECTRIC;
            parameters[12].Value = model.SPEED;
            parameters[13].Value = model.DIRECTION;
            parameters[14].Value = model.SBTIME;
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
            strSql.Append("delete from T_IPS_REALDATATEMPORARY ");
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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string REALDATAIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPS_REALDATATEMPORARY ");
            strSql.Append(" where ID in (" + REALDATAIDlist + ")  ");
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
        public T_IPS_REALDATATEMPORARYModel GetModel(long REALDATAID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select REALDATAID, NOTE, ORGNO, SBDATE, SBTIMEBEGIN, PATROLLENGTH, ISOUTRAIL, USERID, LONGITUDE, LATITUDE, HEIGHT, ELECTRIC, SPEED, DIRECTION, SBTIME  ");
            strSql.Append("  from T_IPS_REALDATATEMPORARY ");
            strSql.Append(" where REALDATAID=@REALDATAID");
            SqlParameter[] parameters = {
					new SqlParameter("@REALDATAID", SqlDbType.BigInt)
			};
            parameters[0].Value = REALDATAID;


            T_IPS_REALDATATEMPORARYModel model = new T_IPS_REALDATATEMPORARYModel();
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_IPS_REALDATATEMPORARY ");
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
            strSql.Append(" * ");
            strSql.Append(" FROM T_IPS_REALDATATEMPORARY ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


        #region ExtensionMethod


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_IPS_REALDATATEMPORARYModel DataRowToModel(DataRow row)
        {
            T_IPS_REALDATATEMPORARYModel model = new T_IPS_REALDATATEMPORARYModel();
            if (row != null)
            {
                if (row["REALDATAID"].ToString() != "")
                {
                    model.REALDATAID = long.Parse(row["REALDATAID"].ToString());
                }
                model.NOTE = row["NOTE"].ToString();
                model.ORGNO = row["ORGNO"].ToString();
                if (row["SBDATE"].ToString() != "")
                {
                    model.SBDATE = DateTime.Parse(row["SBDATE"].ToString());
                }
                if (row["SBTIMEBEGIN"].ToString() != "")
                {
                    model.SBTIMEBEGIN = DateTime.Parse(row["SBTIMEBEGIN"].ToString());
                }
                if (row["PATROLLENGTH"].ToString() != "")
                {
                    model.PATROLLENGTH = decimal.Parse(row["PATROLLENGTH"].ToString());
                }
                if (row["ISOUTRAIL"].ToString() != "")
                {
                    model.ISOUTRAIL = int.Parse(row["ISOUTRAIL"].ToString());
                }
                if (row["USERID"].ToString() != "")
                {
                    model.USERID = int.Parse(row["USERID"].ToString());
                }
                if (row["LONGITUDE"].ToString() != "")
                {
                    model.LONGITUDE = decimal.Parse(row["LONGITUDE"].ToString());
                }
                if (row["LATITUDE"].ToString() != "")
                {
                    model.LATITUDE = decimal.Parse(row["LATITUDE"].ToString());
                }
                if (row["HEIGHT"].ToString() != "")
                {
                    model.HEIGHT = decimal.Parse(row["HEIGHT"].ToString());
                }
                if (row["ELECTRIC"].ToString() != "")
                {
                    model.ELECTRIC = decimal.Parse(row["ELECTRIC"].ToString());
                }
                if (row["SPEED"].ToString() != "")
                {
                    model.SPEED = decimal.Parse(row["SPEED"].ToString());
                }
                if (row["DIRECTION"].ToString() != "")
                {
                    model.DIRECTION = decimal.Parse(row["DIRECTION"].ToString());
                }
                if (row["SBTIME"].ToString() != "")
                {
                    model.SBTIME = DateTime.Parse(row["SBTIME"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获取多条记录
        /// </summary>
        /// <param name="top"></param>
        /// <param name="strwhere"></param>
        /// <param name="ordercloum"></param>
        /// <returns></returns>
        public List<T_IPS_REALDATATEMPORARYModel> GetRealDataTmpList(int top, string strwhere, string ordercloum)
        {
            var list = new List<T_IPS_REALDATATEMPORARYModel>();
            var ds = this.GetList(top, strwhere, ordercloum);
            if (ds.Tables.Count > 0)
            {
                //遍历一个表多行多列
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var data = this.DataRowToModel(dr);
                    list.Add(data);
                }
            }
            return list;
        }
        #endregion
    }
}
