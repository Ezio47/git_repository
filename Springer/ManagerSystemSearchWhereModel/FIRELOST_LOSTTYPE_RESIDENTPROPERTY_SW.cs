using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 灾损_损失分类_居民财产损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_RESIDENTPROPERTY_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string RESIDENTPROPERTYID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 居民财产名称
        /// </summary>
        public string RESIDENTPROPERTYNAME { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
        /// <summary>
        /// 居民财产数量
        /// </summary>
        public string RESIDENTPROPERTYCOUNT { get; set; }
        /// <summary>
        /// 居民财产单位
        /// </summary>
        public string RESIDENTPROPERTYUNIT { get; set; }
        /// <summary>
        /// 居民财产购入价
        /// </summary>
        public string RESIDENTPROPERTYPRICE { get; set; }
        /// <summary>
        /// 年平均折旧率
        /// </summary>
        public string YEARAVGDEPRECIATIONRATE { get; set; }
        /// <summary>
        /// 已使用年限
        /// </summary>
        public string HAVEUSEYEAR { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
    }
}
