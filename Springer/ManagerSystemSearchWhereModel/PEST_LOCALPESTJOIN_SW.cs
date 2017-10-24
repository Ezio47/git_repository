using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物_本地化生物关联表
    /// </summary>
    public class PEST_LOCALPESTJOIN_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PEST_LOCALPESTJOINID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 有害生物编码
        /// </summary>
        public string PESTBYCODE { get; set; }
        /// <summary>
        /// 调查类型编号
        /// </summary>
        public string SEARCHTYPE { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurPage { get; set; }
        /// <summary>
        /// 只获取本地区的生物
        /// </summary>
        public bool IsOnlyGetORGNO { get; set; }
        /// <summary>
        /// 根据生物编码去重
        /// </summary>
        public bool IsDistinctByPestCode { get; set; }
    }
}
