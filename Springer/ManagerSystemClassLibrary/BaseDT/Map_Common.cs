using DataBaseClassLibrary;
using ManagerSystemModel;
using PublicClassLibrary;
using PublicClassLibrary.PublicCom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 地图
    /// </summary>
    public class Map_Common
    {
        /// <summary>
        /// 更新指定表中的经度纬度
        /// </summary>
        /// <param name="jd"></param>
        /// <param name="wd"></param>
        /// <param name="tablename"></param>
        /// <param name="id"></param>
        /// <param name="key">主键名</param>
        /// <returns></returns>
        public static Message updatePoint(string jd, string wd, string tablename, string id, string key)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE " + tablename + "");
            sb.AppendFormat(" set ");
            sb.AppendFormat("JD={0}", ClsSql.saveNullField(jd));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(wd));
            sb.AppendFormat(" where " + key + " = '{0}'", id);
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "保存成功！", "");
            else
                return new Message(false, "保存失败！", "");
        }

        /// <summary>
        /// 更新坐标点
        /// </summary>
        /// <param name="jwdlist"></param>
        /// <param name="tablename"></param>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Message updateLine(string jwdlist, string tablename, string id, string key)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE " + tablename + "");
            sb.AppendFormat(" set ");
            sb.AppendFormat("JWDLIST={0}", ClsSql.saveNullField(jwdlist));
            //sb.AppendFormat(",WDBEGIN={0}", ClsSql.saveNullField(wd));
            //sb.AppendFormat(",JDEND={0}", ClsSql.saveNullField(jd1));
            //sb.AppendFormat(",WDEND={0}", ClsSql.saveNullField(wd1));
            sb.AppendFormat(" where " + key + " = '{0}'", id);
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "保存成功！", "");
            else
                return new Message(false, "保存失败！", "");
        }


        /// <summary>
        /// 由主键获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="key"></param>
        /// <param name="dbid"></param>
        /// <returns></returns>
        public static T GetTModel<T>(string tableName, string key, string dbid)
        {
            T t1 = default(T);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" Select * from {0} ", tableName);
            sb.AppendFormat(" where {0} = '{1}'", key, dbid);
            var ds = DataBaseClass.FullDataSet(sb.ToString());
            if (ds.Tables.Count > 0)
            {
                t1 = PublicTools.ConvertTo<T>(ds.Tables[0]).FirstOrDefault();
                //t1 = (T)Convert.ChangeType(model, t1.GetType());
            }
            //   T t1 = default(T);

            return t1;
            //return default(T);
        }

    }
}
