using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Springer.EntityModel.Entity
{
    [DataContract]
    public class HLYTXLModel
    {
        [DataMember]
        public string HID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public string HNAME { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [DataMember]
        public string PHONE { get; set; }

        /// <summary>
        /// 机构
        /// </summary>
        [DataMember]
        public string ORGNO { get; set; }
    }
}
