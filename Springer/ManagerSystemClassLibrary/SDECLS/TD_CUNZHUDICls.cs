using ManagerSystemClassLibrary.EnumCls;
using ManagerSystemModel.SDEModel;
using ManagerSystemSearchWhereModel.LogicModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.SDECLS
{
    /// <summary>
    /// 村之地
    /// </summary>
    public class TD_CUNZHUDICls
    {

        /// <summary>
        /// 图层查询 获取联表结果
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<SDE_QueyLayerModel> GetQueryLayerUnionResult(QueryLayerDataSW sw)
        {
            var result = new List<SDE_QueyLayerModel>();
            DataTable dt = BaseDT.SDE.TD_CUNZHUDI.getUnionDT(sw);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SDE_QueyLayerModel m = new SDE_QueyLayerModel();
                m.ID = dt.Rows[i]["ID"].ToString();
                m.Name = dt.Rows[i]["NAME"].ToString();
                m.Display_X = dt.Rows[i]["DISPLAY_X"].ToString();
                m.Display_Y = dt.Rows[i]["DISPLAY_Y"].ToString();
                m.Flag = dt.Rows[i]["FLAG"].ToString();
                m.LayerName = Enum.GetName(typeof(LayerType), int.Parse(m.Flag));
                m.LNGLATSTRS = dt.Rows[i]["LNGLATSTRS"] == null ? null : dt.Rows[i]["LNGLATSTRS"].ToString();
                m.DBTYPE = dt.Rows[i]["DBTYPE"] == null ? null : dt.Rows[i]["DBTYPE"].ToString();
                m.TYPE = dt.Rows[i]["TYPE"].ToString();
                m.CATEGORY = dt.Rows[i]["category"].ToString();
                m.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();

            return result;
        }
    }
}
