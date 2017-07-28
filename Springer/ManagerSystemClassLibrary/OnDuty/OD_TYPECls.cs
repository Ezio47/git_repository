
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 值班_排班类别表基本操作
    /// </summary>
    public class OD_TYPECls
    {
        ///// <summary>
        ///// 添加返回刚插入数据的id
        ///// </summary>
        ///// <param name="o"></param>
        ///// <returns></returns>
        //public static string ADD(OD_ODTYPE_Model o)
        //{
        //    return BaseDT.OD_TYPE.ADD(o);
        //}
        #region 对类别表进行管理 Message Manager(OD_ODTYPE_Model od)
        /// <summary>
        /// 根据标识op_Method执行修改操作
        /// </summary>
        /// <param name="od"></param>
        /// <returns></returns>
        public static Message Manager(OD_ODTYPE_Model od)
        {
            if (od.op_Method == "Mdy")
            {
                return BaseDT.OD_TYPE.Mdy(od);
            }
            if (od.op_Method == "Add")
                return BaseDT.OD_TYPE.Add(od);
            return new Message(false, "无效操作", "");
        }

        #endregion

        #region 获取单条记录 OD_ODTYPE_Model getModel(OD_ODTYPE_SW sw)
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <returns></returns>
        public static OD_ODTYPE_Model getModel(OD_ODTYPE_SW sw)
        {
            OD_ODTYPE_Model m = new OD_ODTYPE_Model();
            DataTable dt = BaseDT.OD_TYPE.getDT(sw);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.OD_TYPEID = dt.Rows[i]["OD_TYPEID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.OD_TYPENAME = dt.Rows[i]["OD_TYPENAME"].ToString();
                m.OD_DATEBEGIN =PublicClassLibrary.ClsSwitch.SwitDate( dt.Rows[i]["OD_DATEBEGIN"].ToString());
                m.OD_DATEEND = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["OD_DATEEND"].ToString());
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }

        #endregion

        #region 获取列表 IEnumerable<OD_ODTYPE_Model> getListModel(OD_ODTYPE_SW sw)
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<OD_ODTYPE_Model> getListModel(OD_ODTYPE_SW sw)
        {
            var result = new List<OD_ODTYPE_Model>();
            DataTable dt = BaseDT.OD_TYPE.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OD_ODTYPE_Model m = new OD_ODTYPE_Model();
                m.OD_TYPEID = dt.Rows[i]["OD_TYPEID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.OD_TYPENAME = dt.Rows[i]["OD_TYPENAME"].ToString();
                m.OD_DATEBEGIN = dt.Rows[i]["OD_DATEBEGIN"].ToString();
                m.OD_DATEEND = dt.Rows[i]["OD_DATEEND"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion
        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<OD_ODTYPE_Model>GetOneData()
        {
            //var result = new List<O_ODUSER_Model>();
            //var dt = BaseDT.O_OD_USER.GetModelList(od);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    O_ODUSER_Model ood = new O_ODUSER_Model();
            //    ood.BYORGNO = BaseDT.T_SYS_ORG.getName(dt, dt.Rows[i]["ORGNO"].ToString());
            //    ood.ONDUTYUSERTYPE = dt.Rows[i]["ONDUTYUSERTYPE"].ToString();
            //    ood.USERNAME = dt.Rows[i]["username"].ToString();
            //    ood.ISATTENDED = dt.Rows[i]["ISATTENDED"].ToString();
            //    ood.ONDUTYDATE = ClsSwitch.SwitDate(dt.Rows[i]["ONDUTYDATE"].ToString());
            //    result.Add(ood);
            //}

            //dt.Clear();
            //dt.Dispose();
            //return result;
            var result=new List<OD_ODTYPE_Model>();
            var dt = BaseDT.OD_TYPE.GetOneData();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OD_ODTYPE_Model om = new OD_ODTYPE_Model();
                om.OD_TYPEID = dt.Rows[i]["OD_TYPEID"].ToString();
                om.OD_TYPENAME = dt.Rows[i]["OD_TYPENAME"].ToString();
                om.OD_DATEBEGIN = ClsSwitch.SwitDate(dt.Rows[i]["OD_DATEBEGIN"].ToString());
                om.OD_DATEEND = ClsSwitch.SwitDate(dt.Rows[i]["OD_DATEEND"].ToString());
                result.Add(om);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static IEnumerable<OD_ODTYPE_Model> GetModelList(OD_ODTYPE_Model o)
        {
            var result = new List<OD_ODTYPE_Model>();
            var dt = BaseDT.OD_TYPE.GetModelList(o);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OD_ODTYPE_Model om = new OD_ODTYPE_Model();
                om.OD_TYPEID = dt.Rows[i]["OD_TYPEID"].ToString();
                om.OD_TYPENAME = dt.Rows[i]["OD_TYPENAME"].ToString();
                om.OD_DATEBEGIN = ClsSwitch.SwitDate(dt.Rows[i]["OD_DATEBEGIN"].ToString());
                om.OD_DATEEND = ClsSwitch.SwitDate(dt.Rows[i]["OD_DATEEND"].ToString());
                result.Add(om);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

    }
}
