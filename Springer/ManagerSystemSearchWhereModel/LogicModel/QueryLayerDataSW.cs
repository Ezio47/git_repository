using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel.LogicModel
{
    /// <summary>
    /// 图层查询检索模型
    /// </summary>
    public class QueryLayerDataSW
    {
        /// <summary>
        /// 检索名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图层类型
        /// </summary>
        public string FlagStr { get; set; }

        /// <summary>
        /// 周边距离
        /// </summary>
        public string AroundValue { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }

        /// <summary>
        /// 位度
        /// </summary>
        public string WD { get; set; }
    }
}
