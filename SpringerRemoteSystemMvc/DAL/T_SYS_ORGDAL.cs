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
    public class T_SYS_ORGDAL
    {
        public bool Exists(string ORGNO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SYS_ORG");
            strSql.Append(" where ");
            strSql.Append(" ORGNO = @ORGNO  ");
            SqlParameter[] parameters = {
					new SqlParameter("@ORGNO", SqlDbType.VarChar,15)			};
            parameters[0].Value = ORGNO;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(T_SYS_ORGModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SYS_ORG(");
            strSql.Append("ORGNO,WD,COMMANDNAME,CITYID,WEATHERJC,POSTCODE,DUTYTELL,FAX,MOBILEPARAMLIST,ADDRESS,ORGNAME,ORGDUTY,LEADER,AREACODE,ORGJC,WXJC,SYSFLAG,JD");
            strSql.Append(") values (");
            strSql.Append("@ORGNO,@WD,@COMMANDNAME,@CITYID,@WEATHERJC,@POSTCODE,@DUTYTELL,@FAX,@MOBILEPARAMLIST,@ADDRESS,@ORGNAME,@ORGDUTY,@LEADER,@AREACODE,@ORGJC,@WXJC,@SYSFLAG,@JD");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ORGNO", SqlDbType.VarChar,15) ,            
                        new SqlParameter("@WD", SqlDbType.Float,8) ,            
                        new SqlParameter("@COMMANDNAME", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CITYID", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@WEATHERJC", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@POSTCODE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DUTYTELL", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FAX", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MOBILEPARAMLIST", SqlDbType.VarChar,2000) ,            
                        new SqlParameter("@ADDRESS", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@ORGNAME", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ORGDUTY", SqlDbType.Text) ,            
                        new SqlParameter("@LEADER", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@AREACODE", SqlDbType.VarChar,15) ,            
                        new SqlParameter("@ORGJC", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@WXJC", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@SYSFLAG", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@JD", SqlDbType.Float,8)             
              
            };

            parameters[0].Value = model.ORGNO;
            parameters[1].Value = model.WD;
            parameters[2].Value = model.COMMANDNAME;
            parameters[3].Value = model.CITYID;
            parameters[4].Value = model.WEATHERJC;
            parameters[5].Value = model.POSTCODE;
            parameters[6].Value = model.DUTYTELL;
            parameters[7].Value = model.FAX;
            parameters[8].Value = model.MOBILEPARAMLIST;
            parameters[9].Value = model.ADDRESS;
            parameters[10].Value = model.ORGNAME;
            parameters[11].Value = model.ORGDUTY;
            parameters[12].Value = model.LEADER;
            parameters[13].Value = model.AREACODE;
            parameters[14].Value = model.ORGJC;
            parameters[15].Value = model.WXJC;
            parameters[16].Value = model.SYSFLAG;
            parameters[17].Value = model.JD;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(T_SYS_ORGModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SYS_ORG set ");

            strSql.Append(" ORGNO = @ORGNO , ");
            strSql.Append(" WD = @WD , ");
            strSql.Append(" COMMANDNAME = @COMMANDNAME , ");
            strSql.Append(" CITYID = @CITYID , ");
            strSql.Append(" WEATHERJC = @WEATHERJC , ");
            strSql.Append(" POSTCODE = @POSTCODE , ");
            strSql.Append(" DUTYTELL = @DUTYTELL , ");
            strSql.Append(" FAX = @FAX , ");
            strSql.Append(" MOBILEPARAMLIST = @MOBILEPARAMLIST , ");
            strSql.Append(" ADDRESS = @ADDRESS , ");
            strSql.Append(" ORGNAME = @ORGNAME , ");
            strSql.Append(" ORGDUTY = @ORGDUTY , ");
            strSql.Append(" LEADER = @LEADER , ");
            strSql.Append(" AREACODE = @AREACODE , ");
            strSql.Append(" ORGJC = @ORGJC , ");
            strSql.Append(" WXJC = @WXJC , ");
            strSql.Append(" SYSFLAG = @SYSFLAG , ");
            strSql.Append(" JD = @JD  ");
            strSql.Append(" where ORGNO=@ORGNO  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ORGNO", SqlDbType.VarChar,15) ,            
                        new SqlParameter("@WD", SqlDbType.Float,8) ,            
                        new SqlParameter("@COMMANDNAME", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CITYID", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@WEATHERJC", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@POSTCODE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DUTYTELL", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FAX", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MOBILEPARAMLIST", SqlDbType.VarChar,2000) ,            
                        new SqlParameter("@ADDRESS", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@ORGNAME", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ORGDUTY", SqlDbType.Text) ,            
                        new SqlParameter("@LEADER", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@AREACODE", SqlDbType.VarChar,15) ,            
                        new SqlParameter("@ORGJC", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@WXJC", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@SYSFLAG", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@JD", SqlDbType.Float,8)             
              
            };

            parameters[0].Value = model.ORGNO;
            parameters[1].Value = model.WD;
            parameters[2].Value = model.COMMANDNAME;
            parameters[3].Value = model.CITYID;
            parameters[4].Value = model.WEATHERJC;
            parameters[5].Value = model.POSTCODE;
            parameters[6].Value = model.DUTYTELL;
            parameters[7].Value = model.FAX;
            parameters[8].Value = model.MOBILEPARAMLIST;
            parameters[9].Value = model.ADDRESS;
            parameters[10].Value = model.ORGNAME;
            parameters[11].Value = model.ORGDUTY;
            parameters[12].Value = model.LEADER;
            parameters[13].Value = model.AREACODE;
            parameters[14].Value = model.ORGJC;
            parameters[15].Value = model.WXJC;
            parameters[16].Value = model.SYSFLAG;
            parameters[17].Value = model.JD;
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
        public bool Delete(string ORGNO)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SYS_ORG ");
            strSql.Append(" where ORGNO=@ORGNO ");
            SqlParameter[] parameters = {
					new SqlParameter("@ORGNO", SqlDbType.VarChar,15)			};
            parameters[0].Value = ORGNO;


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
        /// 得到一个对象实体
        /// </summary>
        public T_SYS_ORGModel GetModel(string ORGNO)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ORGNO, WD, COMMANDNAME, CITYID, WEATHERJC, POSTCODE, DUTYTELL, FAX, MOBILEPARAMLIST, ADDRESS, ORGNAME, ORGDUTY, LEADER, AREACODE, ORGJC, WXJC, SYSFLAG, JD  ");
            strSql.Append("  from T_SYS_ORG ");
            strSql.Append(" where ORGNO=@ORGNO ");
            SqlParameter[] parameters = {
					new SqlParameter("@ORGNO", SqlDbType.VarChar,15)			};
            parameters[0].Value = ORGNO;


            T_SYS_ORGModel model = new T_SYS_ORGModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ORGNO = ds.Tables[0].Rows[0]["ORGNO"].ToString();
                if (ds.Tables[0].Rows[0]["WD"].ToString() != "")
                {
                    model.WD = decimal.Parse(ds.Tables[0].Rows[0]["WD"].ToString());
                }
                model.COMMANDNAME = ds.Tables[0].Rows[0]["COMMANDNAME"].ToString();
                model.CITYID = ds.Tables[0].Rows[0]["CITYID"].ToString();
                model.WEATHERJC = ds.Tables[0].Rows[0]["WEATHERJC"].ToString();
                model.POSTCODE = ds.Tables[0].Rows[0]["POSTCODE"].ToString();
                model.DUTYTELL = ds.Tables[0].Rows[0]["DUTYTELL"].ToString();
                model.FAX = ds.Tables[0].Rows[0]["FAX"].ToString();
                model.MOBILEPARAMLIST = ds.Tables[0].Rows[0]["MOBILEPARAMLIST"].ToString();
                model.ADDRESS = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                model.ORGNAME = ds.Tables[0].Rows[0]["ORGNAME"].ToString();
                model.ORGDUTY = ds.Tables[0].Rows[0]["ORGDUTY"].ToString();
                model.LEADER = ds.Tables[0].Rows[0]["LEADER"].ToString();
                model.AREACODE = ds.Tables[0].Rows[0]["AREACODE"].ToString();
                model.ORGJC = ds.Tables[0].Rows[0]["ORGJC"].ToString();
                model.WXJC = ds.Tables[0].Rows[0]["WXJC"].ToString();
                model.SYSFLAG = ds.Tables[0].Rows[0]["SYSFLAG"].ToString();
                if (ds.Tables[0].Rows[0]["JD"].ToString() != "")
                {
                    model.JD = decimal.Parse(ds.Tables[0].Rows[0]["JD"].ToString());
                }

                return model;
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
            strSql.Append(" FROM T_SYS_ORG ");
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
            strSql.Append(" FROM T_SYS_ORG ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

    }
}
