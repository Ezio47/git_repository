using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Springer.EntityModel.Entity
{
    [DataContract]
    public class DataInfoModel
    {
        /// <summary>
        /// 经度
        /// </summary>
        [DataMember]
        public decimal lon { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        [DataMember]
        public decimal lat { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        [DataMember]
        public decimal height { get; set; }

        /// <summary>
        /// 方位
        /// </summary>
        [DataMember]
        public decimal dir { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        [DataMember]
        public string cjtime { get; set; }

    }
}
