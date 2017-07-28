using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 护林员巡检路线/围栏管理
    /// </summary>
    public class T_IPSFR_ROUTERAIL
    {
        #region 添加

        /// <summary>
        /// 单条添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_IPSFR_ROUTERAIL_Model m)
        {
            if (string.IsNullOrEmpty(m.ROADTYPE))
                return new Message(false, "添加失败，类别为空！", "");
            if (string.IsNullOrEmpty(m.HID))
                return new Message(false, "添加失败，护林员序号为空！", "");
            if (string.IsNullOrEmpty(m.LONGITUDE))
                return new Message(false, "修改失败，经度为空！", "");
            if (string.IsNullOrEmpty(m.LATITUDE))
                return new Message(false, "修改失败，纬度为空！", "");

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  T_IPSFR_ROUTERAIL(HID, LONGITUDE, LATITUDE, HEIGHT, ORDERBY, ROADTYPE)");
            sb.AppendFormat(" VALUES");


            sb.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}')"
                , m.HID//护林员序号
                , m.LONGITUDE//经度
                , m.LATITUDE//纬度
                , string.IsNullOrEmpty(m.HEIGHT) ? "0" : m.HEIGHT//高度
                , string.IsNullOrEmpty(m.ORDERBY) ? "0" : m.ORDERBY//排序号
                , m.ROADTYPE//类型
                );

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message AddBatch(T_IPSFR_ROUTERAIL_Model m)
        {
            if (string.IsNullOrEmpty(m.ROADTYPE))
                return new Message(false, "添加失败，类别为空！", "");
            if (string.IsNullOrEmpty(m.HID))
                return new Message(false, "添加失败，护林员序号为空！", "");
            if (string.IsNullOrEmpty(m.longitLatitList))
                return new Message(false, "添加失败，经纬度列表为空！", "");
            DelBatch(m);//先批量删除，再批量添加
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  T_IPSFR_ROUTERAIL(HID, LONGITUDE, LATITUDE, HEIGHT, ORDERBY, ROADTYPE,LINEARAEID)");
            sb.AppendFormat(" VALUES");

            //INSERT INTO MyTable(ID,NAME)VALUES(7,'003'),(8,'004'),(9,'005')
            var LINEARAEID = 1;
            string[] arr1 = m.longitLatitList.Split(';');
            for (int j = 0; j < arr1.Length; j++)
            {
                if (string.IsNullOrEmpty(arr1[j]))
                {
                    continue;
                }
                string[] arr = arr1[j].Split('|');
                for (int i = 0; i < arr.Length; i++)
                {
                    if (string.IsNullOrEmpty(arr[i]) == false)
                    {
                        string[] arrV = arr[i].Split(',');
                        //if (i > 0)
                        //    sb.AppendFormat(",");//多条记录，以逗号分隔

                        sb.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}','{6}'),"
                            , m.HID//护林员序号
                            , arrV[0].Trim()//经度
                            , arrV[1].Trim()//纬度
                            , string.IsNullOrEmpty(arrV[2].Trim()) ? "0" : arrV[2].Trim()//高度
                            , string.IsNullOrEmpty(arrV[3].Trim()) ? i.ToString() : arrV[3].Trim()//排序号
                            , m.ROADTYPE//类型
                            , LINEARAEID
                            );
                    }
                }
                LINEARAEID++;
            }
            bool bln = DataBaseClass.ExeSql(sb.ToString().Substring(0, sb.ToString().LastIndexOf(",")));
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }
        #endregion


        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(T_IPSFR_ROUTERAIL_Model m)
        {
            if (string.IsNullOrEmpty(m.ROADID))
                return new Message(false, "修改失败，修改序号为空！", "");
            if (string.IsNullOrEmpty(m.LONGITUDE))
                return new Message(false, "修改失败，经度为空！", "");
            if (string.IsNullOrEmpty(m.LATITUDE))
                return new Message(false, "修改失败，纬度为空！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE T_IPSFR_ROUTERAIL set ");
            sb.AppendFormat(" LONGITUDE='{0}'", ClsSql.EncodeSql(m.LONGITUDE));
            sb.AppendFormat(",LATITUDE='{0}'", ClsSql.EncodeSql(m.LATITUDE));
            if (string.IsNullOrEmpty(m.HEIGHT) == false)
                sb.AppendFormat(",HEIGHT='{0}'", ClsSql.EncodeSql(m.HEIGHT));
            if (string.IsNullOrEmpty(m.ORDERBY) == false)
                sb.AppendFormat(",ORDERBY='{0}'", ClsSql.EncodeSql(m.ORDERBY));

            sb.AppendFormat(" where ROADID= '{0}'", ClsSql.EncodeSql(m.ROADID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        #endregion


        #region 删除
        /// <summary>
        /// 单条删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_IPSFR_ROUTERAIL_Model m)
        {
            if (string.IsNullOrEmpty(m.ROADID))
                return new Message(false, "删除失败，记录序号为空！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete T_IPSFR_ROUTERAIL");
            sb.AppendFormat(" where  ROADID= '{0}'", ClsSql.EncodeSql(m.ROADID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message DelBatch(T_IPSFR_ROUTERAIL_Model m)
        {
            if (string.IsNullOrEmpty(m.HID))
                return new Message(false, "删除失败，护林员序号为空！", "");
            if (string.IsNullOrEmpty(m.ROADTYPE))
                return new Message(false, "删除失败，类别为空！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete T_IPSFR_ROUTERAIL");
            sb.AppendFormat(" where  HID= '{0}'", ClsSql.EncodeSql(m.HID));

            sb.AppendFormat(" AND ROADTYPE= '{0}'", ClsSql.EncodeSql(m.ROADTYPE));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion


        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_IPSFR_ROUTERAIL_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT a.ROADID, a.HID, a.LONGITUDE, a.LATITUDE, a.HEIGHT, a.ORDERBY, a.ROADTYPE,a.LINEARAEID");
            sb.AppendFormat(" FROM T_IPSFR_ROUTERAIL AS a");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.HID) == false)
            {
                if (sw.HID.Split(',').Length > 1)
                    sb.AppendFormat(" AND a.HID in({0})", ClsSql.EncodeSql(sw.HID));
                else
                    sb.AppendFormat(" AND a.HID ='{0}'", ClsSql.EncodeSql(sw.HID));
            }
            if (string.IsNullOrEmpty(sw.ROADID) == false)
                sb.AppendFormat(" AND a.ROADID= '{0}'", ClsSql.EncodeSql(sw.ROADID));
            if (string.IsNullOrEmpty(sw.ROADTYPE) == false)
                sb.AppendFormat(" AND a.ROADTYPE = '{0}'", ClsSql.EncodeSql(sw.ROADTYPE));
            sb.AppendFormat(" ORDER BY a.LINEARAEID ,a.ORDERBY ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        #endregion

        #region 根据DataTable、HID、ROADTYPE判断记录个数
        /// <summary>
        /// 根据DataTable、HID、ROADTYPE判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="HID">护林员序号</param>
        /// <param name="ROADTYPE">路线类型</param>
        /// <returns>记录个数</returns>
        public static string getCountByHidRoadtype(DataTable dt, string HID, string ROADTYPE)
        {
            if (dt == null) return "";
            if (string.IsNullOrEmpty(HID)) return "";
            if (string.IsNullOrEmpty(ROADTYPE)) return "";
            return dt.Compute("count(ROADID)", "HID='" + HID + "' and ROADTYPE='"+ROADTYPE+"'").ToString();
            
        }
        #endregion
    }
}
