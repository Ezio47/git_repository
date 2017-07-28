using ManagerSystemModel.SDEModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel.LogicModel
{
    /// <summary>
    /// 图层周边分析结果
    /// </summary>
    public class SDE_QueyLayerResultModel
    {
        /// <summary>
        /// 
        /// </summary>
        public SDE_QueyLayerResultModel()
        {
            this.DataList = new List<SDE_QueyLayerModel>();
        }
        /// <summary>
        /// 图层id
        /// </summary>
        public string LayerId { get; set; }

        /// <summary>
        /// 图层名字
        /// </summary>
        public string LayerName { get; set; }

        /// <summary>
        /// 图层信息
        /// </summary>
        public IList<SDE_QueyLayerModel> DataList { get; set; }
    }
}
