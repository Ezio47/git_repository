using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{

    /// <summary>
    /// 数据中心_车辆
    /// </summary>
    public class DC_CAR_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_CAR_ID { get; set; }
        /// <summary>
        /// 车辆类型
        /// </summary>
        public string CARTYPE { get; set; }
        /// <summary>
        /// 车辆类型名称
        /// </summary>
        public string CARTYPEName { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string NUMBER { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 购买年份
        /// </summary>
        public string BUYYEAR { get; set; }
        /// <summary>
        /// 购买价格
        /// </summary>
        public string BUYPRICE { get; set; }
        /// <summary>
        /// 号牌
        /// </summary>
        public string PLATENUM { get; set; }
        /// <summary>
        /// 驾驶员
        /// </summary>
        public string DRIVER { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string CONTACTS { get; set; }
        /// <summary>
        /// GPS设备型号
        /// </summary>
        public string GPSEQUIP { get; set; }
        /// <summary>
        /// GPS号码
        /// </summary>
        public string GPSTELL { get; set; }
        /// <summary>
        /// 储存地点
        /// </summary>
        public string STOREADDR { get; set; }
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
        /// 所属单位名称
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 所属县市名称
        /// </summary>
        public string ORGXSName { get; set; }
    }
}
