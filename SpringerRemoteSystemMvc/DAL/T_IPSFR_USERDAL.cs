using log4net;
using Springer.Common.Utils;
using Springer.DBUtility;
using Springer.EntityModel;
using Springer.EntityModel.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Springer.DAL
{
    /// <summary>
    /// 护林员
    /// </summary>
    public partial class T_IPSFR_USERDAL
    {
        private readonly ILog logs = LogHelper.GetInstance();
        public T_IPSFR_USERDAL() { }

        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("HID", "T_IPSFR_USER");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int HID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_IPSFR_USER");
            strSql.Append(" where HID=@HID");
            SqlParameter[] parameters = {
					new SqlParameter("@HID", SqlDbType.Int,4)
			};
            parameters[0].Value = HID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(T_IPSFR_USERModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_IPSFR_USER(");
            strSql.Append("HNAME,SN,PHONE,SEX,BIRTH,ONSTATE,BYORGNO)");
            strSql.Append(" values (");
            strSql.Append("@HNAME,@SN,@PHONE,@SEX,@BIRTH,@ONSTATE,@BYORGNO)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@HNAME", SqlDbType.VarChar,20),
					new SqlParameter("@SN", SqlDbType.VarChar,20),
					new SqlParameter("@PHONE", SqlDbType.VarChar,15),
					new SqlParameter("@SEX", SqlDbType.SmallInt,2),
					new SqlParameter("@BIRTH", SqlDbType.DateTime),
					new SqlParameter("@ONSTATE", SqlDbType.SmallInt,2),
					new SqlParameter("@BYORGNO", SqlDbType.VarChar,15)};
            parameters[0].Value = model.HNAME;
            parameters[1].Value = model.SN;
            parameters[2].Value = model.PHONE;
            parameters[3].Value = model.SEX;
            parameters[4].Value = model.BIRTH;
            parameters[5].Value = model.ONSTATE;
            parameters[6].Value = model.BYORGNO;

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
        public bool Update(T_IPSFR_USERModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_IPSFR_USER set ");
            strSql.Append("HNAME=@HNAME,");
            strSql.Append("SN=@SN,");
            strSql.Append("PHONE=@PHONE,");
            strSql.Append("SEX=@SEX,");
            strSql.Append("BIRTH=@BIRTH,");
            strSql.Append("ONSTATE=@ONSTATE,");
            strSql.Append("BYORGNO=@BYORGNO");
            strSql.Append(" where HID=@HID");
            SqlParameter[] parameters = {
					new SqlParameter("@HNAME", SqlDbType.VarChar,20),
					new SqlParameter("@SN", SqlDbType.VarChar,20),
					new SqlParameter("@PHONE", SqlDbType.VarChar,15),
					new SqlParameter("@SEX", SqlDbType.SmallInt,2),
					new SqlParameter("@BIRTH", SqlDbType.DateTime),
					new SqlParameter("@ONSTATE", SqlDbType.SmallInt,2),
					new SqlParameter("@BYORGNO", SqlDbType.VarChar,15),
					new SqlParameter("@HID", SqlDbType.Int,4)};
            parameters[0].Value = model.HNAME;
            parameters[1].Value = model.SN;
            parameters[2].Value = model.PHONE;
            parameters[3].Value = model.SEX;
            parameters[4].Value = model.BIRTH;
            parameters[5].Value = model.ONSTATE;
            parameters[6].Value = model.BYORGNO;
            parameters[7].Value = model.HID;

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
        public bool Delete(int HID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSFR_USER ");
            strSql.Append(" where HID=@HID");
            SqlParameter[] parameters = {
					new SqlParameter("@HID", SqlDbType.Int,4)
			};
            parameters[0].Value = HID;

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
        public bool DeleteList(string HIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_IPSFR_USER ");
            strSql.Append(" where HID in (" + HIDlist + ")  ");
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
        public T_IPSFR_USERModel GetModel(int HID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 HID,HNAME,SN,PHONE,SEX,BIRTH,ONSTATE,BYORGNO,MOBILEPARAMLIST from T_IPSFR_USER ");
            strSql.Append(" where HID=@HID");
            SqlParameter[] parameters = {
					new SqlParameter("@HID", SqlDbType.Int,4)
			};
            parameters[0].Value = HID;

            T_IPSFR_USERModel model = new T_IPSFR_USERModel();
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
        public T_IPSFR_USERModel DataRowToModel(DataRow row)
        {
            T_IPSFR_USERModel model = new T_IPSFR_USERModel();
            if (row != null)
            {
                if (row["HID"] != null && row["HID"].ToString() != "")
                {
                    model.HID = int.Parse(row["HID"].ToString());
                }
                if (row["HNAME"] != null)
                {
                    model.HNAME = row["HNAME"].ToString();
                }
                if (row["SN"] != null)
                {
                    model.SN = row["SN"].ToString();
                }
                if (row["PHONE"] != null)
                {
                    model.PHONE = row["PHONE"].ToString();
                }
                if (row["SEX"] != null && row["SEX"].ToString() != "")
                {
                    model.SEX = int.Parse(row["SEX"].ToString());
                }
                if (row["BIRTH"] != null && row["BIRTH"].ToString() != "")
                {
                    model.BIRTH = DateTime.Parse(row["BIRTH"].ToString());
                }
                if (row["ONSTATE"] != null && row["ONSTATE"].ToString() != "")
                {
                    model.ONSTATE = int.Parse(row["ONSTATE"].ToString());
                }
                if (row["BYORGNO"] != null)
                {
                    model.BYORGNO = row["BYORGNO"].ToString();
                }
                if (row["MOBILEPARAMLIST"] != null)
                {
                    model.MOBILEPARAMLIST = row["MOBILEPARAMLIST"].ToString();
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
            strSql.Append("select HID,HNAME,SN,PHONE,SEX,BIRTH,ONSTATE,BYORGNO,MOBILEPARAMLIST ");
            strSql.Append(" FROM T_IPSFR_USER ");
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
            strSql.Append(" HID,HNAME,SN,PHONE,SEX,BIRTH,ONSTATE,BYORGNO,MOBILEPARAMLIST");
            strSql.Append(" FROM T_IPSFR_USER ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM T_IPSFR_USER ");
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
                strSql.Append("order by T.HID desc");
            }
            strSql.Append(")AS Row, T.*  from T_IPSFR_USER T ");
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
             parameters[0].Value = "T_IPSFR_USER";
             parameters[1].Value = "HID";
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
        /// 判断是SN\PHONE是否存在记录
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool ExistHUser(string sn, string phone)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_IPSFR_USER");
            strSql.Append(" where 1=1 ");
            if (!string.IsNullOrEmpty(sn) && !string.IsNullOrEmpty(phone))
            {
                strSql.Append(" And SN=@SN");
                strSql.Append(" And PHONE=@PHONE");
                SqlParameter[] parameters = {
					new SqlParameter("@SN", SqlDbType.VarChar,20),
                    new SqlParameter("@PHONE", SqlDbType.VarChar,15)};
                parameters[0].Value = sn.Trim();
                parameters[1].Value = phone.Trim();
                return DbHelperSQL.Exists(strSql.ToString(), parameters);
            }
            else if (!string.IsNullOrEmpty(sn))
            {
                strSql.Append(" And SN=@SN");
                SqlParameter[] parameters = {
                    new SqlParameter("@SN", SqlDbType.VarChar,20)};
                parameters[0].Value = sn.Trim();
                return DbHelperSQL.Exists(strSql.ToString(), parameters);
            }
            else if (!string.IsNullOrEmpty(phone))
            {
                strSql.Append(" And PHONE=@PHONE");
                SqlParameter[] parameters = {
                    new SqlParameter("@PHONE", SqlDbType.VarChar,15)};
                parameters[0].Value = phone.Trim();
                return DbHelperSQL.Exists(strSql.ToString(), parameters);
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// 设备号检索Huser
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public T_IPSFR_USERModel GetModelBySn(string sn)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 HID,HNAME,SN,PHONE,SEX,BIRTH,ONSTATE,BYORGNO,MOBILEPARAMLIST from T_IPSFR_USER ");
            strSql.Append(" where SN=@SN");
            SqlParameter[] parameters = {
						new SqlParameter("@SN", SqlDbType.VarChar,20),
			};
            parameters[0].Value = sn;
            T_IPSFR_USERModel model = new T_IPSFR_USERModel();
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
        /// 电话号码检索Huser
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public T_IPSFR_USERModel GetModelByPhone(string phone)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 HID,HNAME,SN,PHONE,SEX,BIRTH,ONSTATE,BYORGNO,MOBILEPARAMLIST from T_IPSFR_USER ");
            strSql.Append(" where PHONE=@PHONE");
            SqlParameter[] parameters = {
						new SqlParameter("@PHONE", SqlDbType.VarChar,20),
			};
            parameters[0].Value = phone;
            T_IPSFR_USERModel model = new T_IPSFR_USERModel();
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
        /// 返回号码状态
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public string ReturnPhoneStatus(string sn)
        {
            if (string.IsNullOrEmpty(sn))
            {
                return StringEnum.Error.ToString();//错误
            }
            else
            {
                bool bo = this.ExistHUser(sn, "");
                if (bo == false)
                {
                    return StringEnum.NoSN.ToString();//没有对应的设备记录
                }
                else if (bo == true)
                {
                    var model = this.GetModelBySn(sn);
                    return model.PHONE;
                }
                else
                {
                    return StringEnum.Error.ToString();//错误
                }

            }
        }


        /// <summary>
        /// 注册护林员
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool RegisterHUser(string sn, string phone)
        {
            if (string.IsNullOrEmpty(sn) || string.IsNullOrEmpty(phone))
            {
                return false;
            }
            else
            {
                bool bo = ExistHUser("", phone);
                if (bo == false)
                {
                    return false;
                }
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update T_IPSFR_USER set ");
                strSql.Append("SN=@SN");
                strSql.Append(" where PHONE=@PHONE");
                SqlParameter[] parameters = {		 
					new SqlParameter("@SN", SqlDbType.VarChar,20),
					new SqlParameter("@PHONE", SqlDbType.VarChar,15)};
                parameters[0].Value = sn.Trim();
                parameters[1].Value = phone.Trim();
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
        }


        /// <summary>
        /// 注册护林员(string)
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public string RegisterHUserStr(string sn, string phone)
        {
            if (string.IsNullOrEmpty(sn) || string.IsNullOrEmpty(phone))
            {
                return StringEnum.Error.ToString();
            }
            else
            {
                bool bo = ExistHUser("", phone);
                if (bo == false)//没有电话号码
                {
                    logs.Info(string.Format("电话号码在系统不存在：{0}", phone.Trim()));
                    return StringEnum.NoData.ToString();
                }
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update T_IPSFR_USER set ");
                strSql.Append("SN=@SN");
                strSql.Append(" where PHONE=@PHONE");
                SqlParameter[] parameters = {		 
					new SqlParameter("@SN", SqlDbType.VarChar,20),
					new SqlParameter("@PHONE", SqlDbType.VarChar,15)};
                parameters[0].Value = sn.Trim();
                parameters[1].Value = phone.Trim();
                int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return StringEnum.Success.ToString();
                }
                else
                {
                    logs.Info(string.Format("电话号码注册出错：{0}", phone.Trim()));
                    return StringEnum.Fail.ToString();
                }
            }
        }


        /// <summary>
        /// 获取护林员信息list
        /// </summary>
        /// <param name="top"></param>
        /// <param name="strwhere"></param>
        /// <param name="ordercloum"></param>
        /// <returns></returns>
        public List<T_IPSFR_USERModel> GetHuserDataList(int top, string strwhere, string ordercloum)
        {
            var list = new List<T_IPSFR_USERModel>();
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
        #endregion  ExtensionMethod
    }
}
