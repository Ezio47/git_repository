using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 数据中心_瞭望塔人员表
    /// </summary>
    public class DC_WATCHTOWERUSERCls
    {
        #region 获取瞭望塔人员
        /// <summary>
        /// 单条瞭望塔人员
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DC_WATCHTOWERUSER_Model getModel(DC_WATCHTOWERUSER_SW sw)
        {
            DataTable dt = BaseDT.DC_WATCHTOWERUSER.getDT(sw);
            DC_WATCHTOWERUSER_Model m = new DC_WATCHTOWERUSER_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DC_WATCHTOWERUSERID = dt.Rows[i]["DC_WATCHTOWERUSERID"].ToString();
                m.WATCHTOWERID = dt.Rows[i]["WATCHTOWERID"].ToString();
                m.FTNAME = dt.Rows[i]["FTNAME"].ToString();
                m.BIRTH = dt.Rows[i]["BIRTH"].ToString();
                m.SEX = dt.Rows[i]["SEX"].ToString();
                m.NATION = dt.Rows[i]["NATION"].ToString();
                m.USERJOB = dt.Rows[i]["USERJOB"].ToString();
                m.LINKWAY = dt.Rows[i]["LINKWAY"].ToString();
                m.PHOTO = dt.Rows[i]["PHOTO"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
            }
            dt.Dispose();
            dt.Clear();
            return m;
        }
        #endregion
        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DC_WATCHTOWERUSER_Model> getListModel(DC_WATCHTOWERUSER_SW sw)
       {
           DataTable dt = BaseDT.DC_WATCHTOWERUSER.getDT(sw);
           var result = new List<DC_WATCHTOWERUSER_Model>();
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               DC_WATCHTOWERUSER_Model m = new DC_WATCHTOWERUSER_Model();
               m.DC_WATCHTOWERUSERID = dt.Rows[i]["DC_WATCHTOWERUSERID"].ToString();
               m.WATCHTOWERID = dt.Rows[i]["WATCHTOWERID"].ToString();
               m.FTNAME = dt.Rows[i]["FTNAME"].ToString();
               m.BIRTH = dt.Rows[i]["BIRTH"].ToString();
               m.SEX = dt.Rows[i]["SEX"].ToString();
               m.NATION = dt.Rows[i]["NATION"].ToString();
               m.USERJOB = dt.Rows[i]["USERJOB"].ToString();
               m.LINKWAY = dt.Rows[i]["LINKWAY"].ToString();
               m.PHOTO = dt.Rows[i]["PHOTO"].ToString();
               m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
               result.Add(m);
           }
           dt.Clear();
           dt.Dispose();
           return result;
        }
        #endregion
    }
}
