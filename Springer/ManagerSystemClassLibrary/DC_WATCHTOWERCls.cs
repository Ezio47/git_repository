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
    /// 数据中心_瞭望塔
    /// </summary>
   public  class DC_WATCHTOWERCls
   {
       #region 数据中心_瞭望塔树形菜单
       /// <summary>
       /// 数据中心_瞭望塔树形菜单
       /// </summary>
       /// <param name="dtWATCHTOWER">瞭望表</param>
       /// <param name="dtOrg">组织机构表</param>
       /// <returns></returns>
       public static JArray getWATCHTOWERRORGChild(DataTable dtOrg, DataTable dtWATCHTOWER)
       {
           JArray childArray = new JArray();
           for (int i = 0; i < dtOrg.Rows.Count; i++)
           {
               if (dtOrg.Rows[i]["ORGNO"].ToString().Substring(6, 3) == "000")
               {
                   JObject root = new JObject
                {
                    {"id",dtOrg.Rows[i]["ORGNO"].ToString()},
                    {"text",dtOrg.Rows[i]["ORGNAME"].ToString()},
                    {"type","4"},
                    {"flag","0"}
                };
                   var watchtower = dtWATCHTOWER.Select("BYORGNO='" + dtOrg.Rows[i]["ORGNO"].ToString() + "'", "");
                   root.Add("children", getWATCHTOWERChild(watchtower));
                   childArray.Add(root);
               }
           }
           return childArray;
       }
       /// <summary>
       /// 数据中心_瞭望塔树形子菜单
       /// </summary>
       /// <param name="watchtower"></param>
       /// <returns></returns>
       public static JArray getWATCHTOWERChild(DataRow[] watchtower)
       {
           JArray childArray = new JArray();
           for (int i = 0; i < watchtower.Length; i++)
           {
               JObject root = new JObject
                {
                    {"id",watchtower[i]["DC_WATCHTOWERID"].ToString()},
                    {"text",watchtower[i]["WATCHNAME"].ToString()},
                    {"type","4"},
                    {"flag","1"}
                };
               childArray.Add(root);
           }
           return childArray;
       }
       #endregion
       #region 获取单条瞭望塔
       /// <summary>
       /// 获取单条瞭望塔
       /// </summary>
       /// <param name="sw"></param>
       /// <returns></returns>
       public static DC_WATCHTOWER_Model getModel(DC_WATCHTOWER_SW sw)
       {
           DataTable dt = BaseDT.DC_WATCHTOWER.getDT(sw);
           DC_WATCHTOWER_Model m = new DC_WATCHTOWER_Model();
           if (dt.Rows.Count > 0)
           {
               int i = 0;
               m.DC_WATCHTOWERID = dt.Rows[i]["DC_WATCHTOWERID"].ToString();
               m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
               m.WATCHNAME = dt.Rows[i]["WATCHNAME"].ToString();
               m.BASICS = dt.Rows[i]["BASICS"].ToString();
               m.BUILDTIME = dt.Rows[i]["BUILDTIME"].ToString();
               m.BULIDAREA = dt.Rows[i]["BULIDAREA"].ToString();
               m.USAGE = dt.Rows[i]["USAGE"].ToString();
               m.LINKWAY = dt.Rows[i]["LINKWAY"].ToString();
               m.PHOTO = dt.Rows[i]["PHOTO"].ToString();
               m.JD = dt.Rows[i]["JD"].ToString();
               m.WD = dt.Rows[i]["WD"].ToString();
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
       public static IEnumerable<DC_WATCHTOWER_Model> getListModel(DC_WATCHTOWER_SW sw)
       {
           DataTable dt = BaseDT.DC_WATCHTOWER.getDT(sw);
           var result = new List<DC_WATCHTOWER_Model>();
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               DC_WATCHTOWER_Model m = new DC_WATCHTOWER_Model();
               m.DC_WATCHTOWERID = dt.Rows[i]["DC_WATCHTOWERID"].ToString();
               m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
               m.WATCHNAME = dt.Rows[i]["WATCHNAME"].ToString();
               m.BASICS = dt.Rows[i]["BASICS"].ToString();
               m.PHOTO = dt.Rows[i]["PHOTO"].ToString();
               m.BUILDTIME = dt.Rows[i]["BUILDTIME"].ToString();
               m.BULIDAREA = dt.Rows[i]["BULIDAREA"].ToString();
               m.USAGE = dt.Rows[i]["USAGE"].ToString();
               m.JD = dt.Rows[i]["JD"].ToString();
               m.WD = dt.Rows[i]["WD"].ToString();
               m.LINKWAY = dt.Rows[i]["LINKWAY"].ToString();
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
