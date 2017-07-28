using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using DataBaseClassLibrary;
using System.Data;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 值班_排班类别表基本操作
    /// </summary>
    public class OD_TYPE
    {
        /// <summary>
        /// 添加返回当前记录的id
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static Message Add(OD_ODTYPE_Model o)
        {//增加判断该值班日期是否已存在

            StringBuilder sbExits = new StringBuilder();
            sbExits.AppendFormat("select ONDUTYID,ONDUTYDATE from OD_DATE where OD_TYPEID in(select OD_TYPEID from OD_TYPE where byorgno='{0}')", SystemCls.getCurUserOrgNo());
            sbExits.AppendFormat("and( ONDUTYDATE>='{0}' and ONDUTYDATE<='{1}')", o.OD_DATEBEGIN, o.OD_DATEEND);

            if (DataBaseClass.JudgeRecordExists(sbExits.ToString()) == true)

                return new Message(false, "新建失败,在该值班日期范围内已存在历史值班日期！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert into OD_TYPE(OD_TYPENAME,OD_DATEBEGIN,OD_DATEEND,BYORGNO) output inserted.OD_TYPEID values(");
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(o.OD_TYPENAME));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(o.OD_DATEBEGIN));
            sb.AppendFormat("'{0}',", ClsSql.EncodeSql(o.OD_DATEEND));
            sb.AppendFormat("'{0}')", ClsSql.EncodeSql(SystemCls.getCurUserOrgNo()));
            try
            {
                string strid = DataBaseClass.ReturnSqlField(sb.ToString());
                return new Message(true, "新建成功",strid);
            }
            catch (Exception)
            {                
                throw;
            }            
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns></returns>
        public static Message Mdy(OD_ODTYPE_Model o)
        {
            if (string.IsNullOrEmpty(o.OD_TYPEID))
                return new Message(false, "修改ID不能为空", "");
            //增加判断该值班日期是否已存在

            StringBuilder sbExits = new StringBuilder();
            sbExits.AppendFormat("select ONDUTYID,ONDUTYDATE from OD_DATE where OD_TYPEID in(select OD_TYPEID from OD_TYPE where byorgno='{0}')", SystemCls.getCurUserOrgNo());
            sbExits.AppendFormat("and( ONDUTYDATE>='{0}' and ONDUTYDATE<='{1}')", o.OD_DATEBEGIN, o.OD_DATEEND);
            sbExits.AppendFormat("and OD_TYPEID<>'{0}'", o.OD_TYPEID);
            if (DataBaseClass.JudgeRecordExists(sbExits.ToString()) == true)
                return new Message(false, "重新生成失败,在该值班日期范围内已存在历史值班日期！", "");

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update OD_TYPE set");
            sb.AppendFormat("  OD_TYPENAME='{0}',", ClsSql.EncodeSql(o.OD_TYPENAME));
            sb.AppendFormat("OD_DATEBEGIN='{0}',", ClsSwitch.SwitDate(o.OD_DATEBEGIN));
            sb.AppendFormat("OD_DATEEND='{0}'", ClsSwitch.SwitDate(o.OD_DATEEND));
            if (!string.IsNullOrEmpty(o.OD_TYPEID))
                sb.AppendFormat(" where OD_TYPEID={0}", ClsSql.EncodeSql(o.OD_TYPEID));
            bool b = DataBaseClass.ExeSql(sb.ToString());
            if (b)
            {
                return new Message(true, "修改成功", o.OD_TYPEID);
            }
            else
            {
                return new Message(false, "修改失败", "");
            }

        }
        /// <summary>
        /// 根据查询条件获取DataTable
        /// </summary>
        /// <param name="sw">参见OD_ODTYPE_SW</param>
        /// <returns>DataTable</returns>
        public static DataTable getDT(OD_ODTYPE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    ");
            if (sw.isTopOne == "1")
                sb.AppendFormat(" TOP 1");
            sb.AppendFormat(" OD_TYPEID, BYORGNO, OD_TYPENAME, OD_DATEBEGIN, OD_DATEEND");
            sb.AppendFormat(" FROM      OD_TYPE");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" AND BYORGNO='{0}'",sw.BYORGNO);
            sb.AppendFormat(" order by OD_TYPEID DESC");
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];
        }
        
        /// <summary>
        ///  获取一条数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetOneData()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select top 1 OD_TYPEID,OD_TYPENAME,OD_DATEBEGIN,OD_DATEEND from OD_TYPE order by OD_TYPEID desc");
             DataSet ds=DataBaseClass.FullDataSet(sb.ToString());
             return ds.Tables[0];
        }
        /// <summary>
        /// 获取带参数的数据集合
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DataTable GetModelList(OD_ODTYPE_Model o)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select  OD_TYPEID,OD_TYPENAME,OD_DATEBEGIN,OD_DATEEND from OD_TYPE where 1=1");
            if (!string.IsNullOrEmpty(o.OD_TYPEID))
                sb.AppendFormat(" and OD_TYPEID={0}", o.OD_TYPEID);
            if (!string.IsNullOrEmpty(o.OD_TYPENAME))
                sb.AppendFormat("  and OD_TYPENAME='{0}'", o.OD_TYPENAME);
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }


    }
}
