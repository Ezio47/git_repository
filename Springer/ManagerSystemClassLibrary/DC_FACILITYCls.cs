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
    /// 获取
    /// </summary>
    public class DC_FACILITYCls
    {
        # region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DC_FACILITY_Model> getListModel(DC_FACILITY_SW sw) 
        {
            DataTable dt = BaseDT.DC_FACILITY.getDT(sw);
            var result = new List<DC_FACILITY_Model>();
            for (int i = 0; i < dt.Rows.Count; i++) 
            {
                DC_FACILITY_Model m = new DC_FACILITY_Model();
                m.DC_FACILITYID = dt.Rows[i]["DC_FACILITYID"].ToString();
                m.TYPEID = dt.Rows[i]["TYPEID"].ToString();
                m.FACINAME = dt.Rows[i]["FACINAME"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD =dt.Rows[i]["WD"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
        # region 获取单条设施信息
        /// <summary>
        /// 单条设施信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DC_FACILITY_Model getModel(DC_FACILITY_SW sw )
        {
            DataTable dt = BaseDT.DC_FACILITY.getDT(sw);
            DC_FACILITY_Model m = new DC_FACILITY_Model();
            if(dt.Rows.Count>0)
            {
                int i = 0;
                m.DC_FACILITYID = dt.Rows[i]["DC_FACILITYID"].ToString();
                m.TYPEID = dt.Rows[i]["TYPEID"].ToString();
                m.FACINAME = dt.Rows[i]["FACINAME"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
            }
            
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion
    }
}
