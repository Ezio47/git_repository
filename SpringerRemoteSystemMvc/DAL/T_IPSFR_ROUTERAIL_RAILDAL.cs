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
    public partial class T_IPSFR_ROUTERAIL_RAILDAL
    {
        /// <summary>
        /// 增加一条出围数据
        /// </summary>
        public int Add(T_IPSFR_ROUTERAIL_RAILModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_IPSFR_ROUTERAIL_RAIL(");
            strSql.Append("HID,LONGITUDE,LATITUDE,SBTIME)");
            strSql.Append(" values (");
            strSql.Append("@HID,@LONGITUDE,@LATITUDE,@SBTIME)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@HID", SqlDbType.Int),
					new SqlParameter("@LONGITUDE", SqlDbType.Float),
					new SqlParameter("@LATITUDE", SqlDbType.Float),
					new SqlParameter("@SBTIME", SqlDbType.DateTime)};
            parameters[0].Value = model.HID;
            parameters[1].Value = model.LONGITUDE;
            parameters[2].Value = model.LATITUDE;
            parameters[3].Value = model.SBTIME;

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
        /// 检索是否存在围栏数据 ROADTYPE =1 为围栏
        /// </summary>
        /// <param name="hid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool ExistROUTERAIL(int hid, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_IPSFR_ROUTERAIL");
            strSql.Append(" where ROADTYPE=@ROADTYPE and HID=@HID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ROADTYPE", SqlDbType.Int),
					new SqlParameter("@HID", SqlDbType.Int)			};
            parameters[0].Value = type;
            parameters[1].Value = hid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获取围栏数据
        /// </summary>
        /// <param name="top"></param>
        /// <param name="strwhere"></param>
        /// <param name="ordercloum"></param>
        /// <returns></returns>
        public List<T_IPSFR_ROUTERAILModel> GetROUTERAILDataList(int top, string strwhere, string ordercloum)
        {
            var list = new List<T_IPSFR_ROUTERAILModel>();
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


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_IPSFR_ROUTERAILModel DataRowToModel(DataRow row)
        {
            T_IPSFR_ROUTERAILModel model = new T_IPSFR_ROUTERAILModel();
            if (row != null)
            {
                if (row["HID"] != null && row["HID"].ToString() != "")
                {
                    model.HID = int.Parse(row["HID"].ToString());
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
                if (row["ORDERBY"] != null && row["ORDERBY"].ToString() != "")
                {
                    model.ORDERBY = int.Parse(row["ORDERBY"].ToString());
                }
                if (row["ROADTYPE"] != null && row["ROADTYPE"].ToString() != "")
                {
                    model.ROADTYPE = int.Parse(row["ROADTYPE"].ToString());
                }
            }
            return model;
        }
        /// <summary>
        /// 获得围栏前几行数据
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
            strSql.Append(" FROM T_IPSFR_ROUTERAIL ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

    }
}
