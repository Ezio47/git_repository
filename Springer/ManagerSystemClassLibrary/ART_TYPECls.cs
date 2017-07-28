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

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 文档类别
    /// </summary>
   public class ART_TYPECls
   {
       #region 获取模型
       /// <summary>
       /// 获取模型
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <returns>参见模型</returns>
       public static ART_TYPE_Model getModel(ART_TYPE_SW sw)
       {
           DataTable dt = BaseDT.ART_TYPE.getDT(sw);
           ART_TYPE_Model m = new ART_TYPE_Model();
           if(dt!=null)
           {
               if (dt.Rows.Count > 0)
               {
                   int i = 0;
                   m.ARTTYPEID = dt.Rows[i]["ARTTYPEID"].ToString();
                   m.ARTTYPENAME = dt.Rows[i]["ARTTYPENAME"].ToString();
                   m.ARTTYPERID = dt.Rows[i]["ARTTYPERID"].ToString();
                   m.ISCHECKED = dt.Rows[i]["ISCHECKED"].ToString();
                   m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
               }
           }
              

         
           dt.Clear();
           dt.Dispose();

           return m;
       }
       #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<ART_TYPE_Model> getListModel(ART_TYPE_SW sw)
        {
            var result = new List<ART_TYPE_Model>();
            DataTable dt = BaseDT.ART_TYPE.getDT(sw);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ART_TYPE_Model m = new ART_TYPE_Model();
                m.ARTTYPEID = dt.Rows[i]["ARTTYPEID"].ToString();
                m.ARTTYPENAME = dt.Rows[i]["ARTTYPENAME"].ToString();
                m.ARTTYPERID = dt.Rows[i]["ARTTYPERID"].ToString();
                m.ISCHECKED = dt.Rows[i]["ISCHECKED"].ToString();
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
