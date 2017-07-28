using System;
using ManagerSystemModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerSystemSearchWhereModel;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// WeatherCls
    /// </summary>
    public class WeatherCls
    {
        /// <summary>
        /// 获取天气情况
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<Weather_Model> getWeatherData(YJ_WEATHER_SW sw) 
        {
            DataTable dt = BaseDT.YJ_WEATHER.getNewDT(sw);
            var result = new List<Weather_Model>();
            for (int i = 0, len = dt.Rows.Count; i < len; i++)
            {
                Weather_Model m = new Weather_Model();
                m.TOWNNAME = dt.Rows[i]["TOWNNAME"].ToString();
                m.WEATHERDATE = dt.Rows[i]["WEATHERDATE"].ToString();
                m.P = dt.Rows[i]["P"].ToString();
                m.TCUR = dt.Rows[i]["TCUR"].ToString();
                m.THIGH = dt.Rows[i]["THIGH"].ToString();
                m.TLOWER = dt.Rows[i]["TLOWER"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
    }

    
}
