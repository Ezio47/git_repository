using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 监测点
    /// </summary>
    public class PEST_MONITORINGSTATION_Model
    {
        /// <summary>
        /// 监测点序号
        /// </summary>
        public string PEST_MONITORINGSTATIONID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string NUMBER { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string ADDRESS { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string MODEL { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 无线电传输方式
        /// </summary>
        public string TRANSFERMODETYPE { get; set; }
        /// <summary>
        /// 监测内容（多选）
        /// </summary>
        public string MONICONTENT { get; set; }
        /// <summary>
        /// 建设日期
        /// </summary>
        public string BUILDDATE { get; set; }
        /// <summary>
        /// 使用现状类型
        /// </summary>
        public string USESTATE { get; set; }
        /// <summary>
        /// 维护管理类型
        /// </summary>
        public string MANAGERSTATE { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 价值
        /// </summary>
        public string WORTH { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 所属单位名称
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 所属县市名称
        /// </summary>
        public string ORGXSName { get; set; }
        /// <summary>
        /// 无线电传输方式名称
        /// </summary>
        public string TRANSFERMODETYPEName { get; set; }
        /// <summary>
        /// 使用现状类型名称
        /// </summary>
        public string USESTATEName { get; set; }
        /// <summary>
        /// 维护管理类型名称
        /// </summary>
        public string MANAGERSTATEName { get; set; }
    }
}
