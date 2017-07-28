using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PublicClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 排班日期表方法封装
    /// </summary>
    public class OD_DATECls
    {
        ///// <summary>
        ///// 获取排班日期表的日期与星期数据集合
        ///// </summary>
        ///// <returns></returns>
        //public static IEnumerable<OD_DATE_Model> GetModelList()
        //{

        //    var result = new List<OD_DATE_Model>();

        //    var dt = BaseDT.OD_DATE.GetModelList();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        OD_DATE_Model m = new OD_DATE_Model();
        //        m.WEEK = dt.Rows[i]["WEEK"].ToString();
        //        m.ONDUTYDATE = ClsSwitch.SwitDate(dt.Rows[i]["ONDUTYDATE"].ToString());
        //        result.Add(m);
        //    }
        //    return result;
        //}
        //public static IEnumerable<OD_DATE_Model> GetDateModelList(OD_DATE_Model dm)
        //{

        //    var result = new List<OD_DATE_Model>();

        //    var dt = BaseDT.OD_DATE.GetModelList(dm);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        OD_DATE_Model m = new OD_DATE_Model();
        //        m.WEEK = dt.Rows[i]["WEEK"].ToString();
        //        m.ONDUTYDATE = ClsSwitch.SwitDate(dt.Rows[i]["ONDUTYDATE"].ToString());
        //        result.Add(m);
        //    }
        //    return result;
        //}

        /// <summary>
        /// 日期排班表的增加，删除，修改
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static Message Manager(OD_ODTYPE_Model o)
        {
            if (o.op_Method == "Add")
            {
                return BaseDT.OD_DATE.Add(o);
            }
            return new Message(false, "无效操作", "");
        }




        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <returns></returns>
        public static OD_DATE_Model getModel(OD_DATE_SW sw)
        {
            OD_DATE_Model m = new OD_DATE_Model();
            DataTable dt = BaseDT.OD_DATE.getDT(sw);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.ONDUTYID = dt.Rows[i]["ONDUTYID"].ToString();
                m.OD_TYPEID = dt.Rows[i]["OD_TYPEID"].ToString();
                m.ONDUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["ONDUTYDATE"].ToString());
                m.WEEK = dt.Rows[i]["WEEK"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<OD_DATE_Model> getListModel(OD_DATE_SW sw)
        {
            var result = new List<OD_DATE_Model>();
            DataTable dt = BaseDT.OD_DATE.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OD_DATE_Model m = new OD_DATE_Model();
                m.ONDUTYID = dt.Rows[i]["ONDUTYID"].ToString();
                m.OD_TYPEID = dt.Rows[i]["OD_TYPEID"].ToString();
                m.ONDUTYDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["ONDUTYDATE"].ToString());
                m.WEEK = dt.Rows[i]["WEEK"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
    }
}
