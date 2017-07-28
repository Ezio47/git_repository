using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 灾损_损失分类_农牧产品损失表
    /// </summary>
    public class FIRELOST_LOSTTYPE_FARMPASTUREPRODUCT_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string FIRELOST_LOSTTYPE_FARMPASTUREPRODUCTID { get; set; }
        /// <summary>
        /// 灾损火情基本信息序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 农牧产品名称
        /// </summary>
        public string FARMPASTUREPRODUCNAME { get; set; }
        /// <summary>
        /// 农牧产品损失类别编号
        /// </summary>
        public string FARMPASTUREPRODUCCODE { get; set; }
        /// <summary>
        /// 损失金额
        /// </summary>
        public string LOSEMONEYCOUNT { get; set; }
        /// <summary>
        /// 损失数量
        /// </summary>
        public string LOSECOUNT { get; set; }
        /// <summary>
        /// 原有价格(成本)
        /// </summary>
        public string BASEPRICE { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
    }
}                                                
