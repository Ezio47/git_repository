using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Springer.EntityModel.Entity
{

    [Serializable]
    [DataContract]
    public class TXLModel
    {
        /// <summary>
        /// 通讯录主键
        /// </summary>
        [DataMember]
        public string ADID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [DataMember]
        public string Phone { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [DataMember]
        public string DepName { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [DataMember]
        public string DepJob { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [DataMember]
        public string Address { get; set; }
    }
}
