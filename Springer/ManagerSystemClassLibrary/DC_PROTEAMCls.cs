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
    /// 数据中心_专业队
    /// </summary>
    public class DC_PROTEAMCls
    {
        #region  专业队树形菜单
        /// <summary>
        /// 专业队树形菜单
        /// </summary>
        /// <param name="dtTYPE">数据中心类别表</param>
        /// <param name="dtPROTEAM">专业队表</param>
        /// <param name="dctypetopid">父序号</param>
        /// <returns></returns>
        public static JArray getPROTEAMchild(DataTable dtTYPE, DataTable dtPROTEAM, string dctypetopid)
        {
            JArray childobjArray = new JArray();
            DataRow[] drTYPE = dtTYPE.Select("DCTYPETOPID = '" + dctypetopid + "'", "");
            for (int i = 0; i < drTYPE.Length; i++)
            {
                JObject root = new JObject
                        {
                        {"id", drTYPE[i]["DCTYPEID"].ToString()},
                        {"text",drTYPE[i]["DCTYPENAME"].ToString()},
                        {"type","3"},
                        {"flag", "0"}
                        };
                root.Add("children", getPROTEAMchild(dtTYPE, dtPROTEAM, drTYPE[i]["DCTYPEID"].ToString()));
                childobjArray.Add(root);
            }
            DataRow[] drPROTEAM = dtPROTEAM.Select("TYPEID = '" + dctypetopid + "'");
            for (int i = 0; i < drPROTEAM.Length; i++)
            {
                JObject root = new JObject
                        {
                        {"id", drPROTEAM[i]["DC_PROTEAMID"].ToString()},
                        {"text",drPROTEAM[i]["PROTEAMNAME"].ToString()},
                        {"type","3"},
                        {"flag", "1"}
                        };
                childobjArray.Add(root);
            }
            return childobjArray;
        }
        #endregion
        # region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DC_PROTEAM_Model> getListModel(DC_PROTEAM_SW sw)
        {
            DataTable dt = BaseDT.DC_PROTEAM.getDT(sw);
            var result = new List<DC_PROTEAM_Model>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_PROTEAM_Model m = new DC_PROTEAM_Model();
                m.DC_PROTEAMID = dt.Rows[i]["DC_PROTEAMID"].ToString();
                m.TYPEID = dt.Rows[i]["TYPEID"].ToString();
                m.PROTEAMNAME = dt.Rows[i]["PROTEAMNAME"].ToString();
                m.EQUIP = dt.Rows[i]["EQUIP"].ToString();
                m.CAPACITY = dt.Rows[i]["CAPACITY"].ToString();
                m.LEADER = dt.Rows[i]["LEADER"].ToString();
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
        #region 获取专业队单条信息
        /// <summary>
        /// 专业队单条信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DC_PROTEAM_Model getModel(DC_PROTEAM_SW sw) 
        {
            DataTable dt = BaseDT.DC_PROTEAM.getDT(sw);
            DC_PROTEAM_Model m = new DC_PROTEAM_Model();
            if( dt.Rows.Count>0)
            {
                int i = 0;
                m.DC_PROTEAMID = dt.Rows[i]["DC_PROTEAMID"].ToString();
                m.TYPEID = dt.Rows[i]["TYPEID"].ToString();
                m.PROTEAMNAME = dt.Rows[i]["PROTEAMNAME"].ToString();
                m.EQUIP = dt.Rows[i]["EQUIP"].ToString();
                m.CAPACITY = dt.Rows[i]["CAPACITY"].ToString();
                m.LEADER = dt.Rows[i]["LEADER"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.LINKWAY = dt.Rows[i]["LINKWAY"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion
    }
}
