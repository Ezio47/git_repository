using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物_危害等级表
    /// </summary>
    public class PEST_HARMCLASS_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_HARMCLASSID { get; set; }
        /// <summary>
        /// 等级时间
        /// </summary>
        public string DCDATE { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 乡镇名称
        /// </summary>
        public string TOWNNAME { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 危害等级
        /// </summary>
        public string HARMCLASS { get; set; }
    }
}
