using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel.SDEModel
{
    /// <summary>
    /// 地图查询模型
    /// </summary>
    public class SDE_QueyLayerModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Display_X { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Display_Y { get; set; }

        /// <summary>
        /// shape 线面
        /// </summary>
        public string LNGLATSTRS { get; set; }

        /// <summary>
        /// null 则无需弹出框
        /// </summary>
        public string DBTYPE { get; set; }

        /// <summary>
        /// 0 点 1 线 2 面
        /// </summary>
        public string TYPE { get; set; }

        /// <summary>
        /// 图层类型
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// 图层Name
        /// </summary>
        public string LayerName { get; set; }
        /// <summary>
        /// 图层类别
        /// </summary>
        public string CATEGORY { get; set; }
        /// <summary>
        /// 图标路径
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
