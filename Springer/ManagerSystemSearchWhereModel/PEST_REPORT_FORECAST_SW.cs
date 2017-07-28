using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物_报表_预测表
    /// </summary>
    public class PEST_REPORT_FORECAST_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_REPORT_FORECASTID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 有害生物编码
        /// </summary>
        public string PESTBYCODE { get; set; }
        /// <summary>
        /// 预测年份
        /// </summary>
        public string FORECASTYEAR { get; set; }
        /// <summary>
        /// 预测阶段编码
        /// </summary>
        public string FORECASTSTAGECODE { get; set; }
        /// <summary>
        /// 预测面积
        /// </summary>
        public string FORECASTAREA { get; set; }
    }
}
