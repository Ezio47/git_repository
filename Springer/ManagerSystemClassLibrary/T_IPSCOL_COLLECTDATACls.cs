using ManagerSystemClassLibrary.BaseDT;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 数据采集类库
    /// </summary>
    public class T_IPSCOL_COLLECTDATACls
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见条件模型CollectDataSW</param>
        /// <returns>参见模型CollectDataListModel</returns>
        public static IEnumerable<CollectDataListModel> get_CollectDataModelList(CollectDataSW sw)
        {
            var result = new List<CollectDataListModel>();
            DataTable dt = T_IPSCOL_COLLECTDATA.getUnionDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                CollectDataListModel m = new CollectDataListModel();
                m.COLLECTID = long.Parse(dt.Rows[i]["COLLECTID"].ToString());
                m.HID = int.Parse(dt.Rows[i]["HID"].ToString());
                m.LONGITUDE = (dt.Rows[i]["LONGITUDE"] == null) ? 0 : decimal.Parse(dt.Rows[i]["LONGITUDE"].ToString());
                m.LATITUDE = (dt.Rows[i]["LATITUDE"] == null) ? 0 : decimal.Parse(dt.Rows[i]["LATITUDE"].ToString());
                //******************计算坐标偏移量
                string[] arr = PublicCls.switJWD(m.LATITUDE.ToString(), m.LONGITUDE.ToString());
                m.LATITUDE = Convert.ToDecimal(arr[0]);
                m.LONGITUDE = Convert.ToDecimal(arr[1]);
                //******************计算坐标偏移量 End
                m.SYSTYPEVALUE = dt.Rows[i]["SYSTYPEVALUE"].ToString();
                m.COLLECTNAME = dt.Rows[i]["COLLECTNAME"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                //if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["HEIGHT"])))//判断是否是空值
                //{
                //    dt.Rows[i]["HEIGHT"] = 0;
                //}
                m.COLLECTTIME = Convert.ToDateTime(dt.Rows[i]["COLLECTTIME"].ToString());

                result.Add(m);
            }
            return result;
        }
    }
}
