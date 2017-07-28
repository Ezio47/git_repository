using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{

    /// <summary>
    /// 监测_火情表
    /// </summary>
    public class JC_FIRE_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string JCFID { get; set; }
        /// <summary>
        /// 序号2
        /// </summary>
        public string FRFIID { get; set; }
        /// <summary>
        /// 所属单位编号
        /// </summary>
        public string BYORGNO { get; set; }

        /// <summary>
        /// 所属单位名称
        /// </summary>
        public string ORGNAME { get; set; }
        /// <summary>
        /// 火情名称
        /// </summary>
        public string FIRENAME { get; set; }
        /// <summary>
        /// 火情来源
        /// </summary>
        public string FIREFROM { get; set; }
        /// <summary>
        /// 原始记录序号
        /// </summary>
        public string FIREFROMID { get; set; }
        /// <summary>
        /// 起火时间
        /// </summary>
        public string FIRETIME { get; set; }
        /// <summary>
        /// 起火结束时间
        /// </summary>
        public string FIREENDTIME { get; set; }
        /// <summary>
        /// 是否已灭 1为已灭
        /// </summary>
        public string ISOUTFIRE { get; set; }

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
        /// 火灾发生地
        /// </summary>
        public string ZQWZ { get; set; }
        /// <summary>
        /// 卫星编号
        /// </summary>
        public string WXBH { get; set; }
        /// <summary>
        /// 热点编号
        /// </summary>
        public string DQRDBH { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public string RSMJ { get; set; }
        /// <summary>
        /// 地类
        /// </summary>
        public string DL { get; set; }
        /// <summary>
        /// 是否烟云
        /// </summary>
        public string YY { get; set; }
        /// <summary>
        /// 是否连续火
        /// </summary>
        public string JXHQSJ { get; set; }
        /// <summary>
        /// 监测火情状态ID
        /// </summary>
        public string JCMANSTATE { get; set; }
        /// <summary>
        /// 所属主火情id
        /// </summary>
        public string OWERJCFID { get; set; }
        /// <summary>
        /// 派发人userid
        /// </summary>
        public string PFUSERID { get; set; }
        /// <summary>
        /// 派发人单位
        /// </summary>
        public string PFORGNO { get; set; }
        /// <summary>
        /// 派发时间
        /// </summary>
        public string PFTIME { get; set; }
        /// <summary>
        /// 派发标志 0 为非本单位处理 1 为市局本单位处理 2 为县局本单位处理 3 为乡镇局本单位处理
        /// </summary>
        public string PFFLAG { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        public string RECEIVETIME { get; set; }
        /// <summary>
        /// 下发时间
        /// </summary>
        public string ISSUEDTIME { get; set; }
        /// <summary>
        /// 最后处理时间
        /// </summary>
        public string LASTPROCESSTIME { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public string MANSTATE { get; set; }

        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }

        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 火灾级别
        /// </summary>
        public string FIRELEVEL { get; set; }
        /// <summary>
        /// 火灾属性相关数据
        /// </summary>
        public JC_FIRE_PROP_Model FirePropModel { get; set; }
        /// <summary>
        /// 派发人名
        /// </summary>
        public string PFNAME { get; set; }
        /// <summary>
        /// 火情来源名称
        /// </summary>
        public string FIREFROMName { get; set; }
        /// <summary>
        /// 派发单位名称
        /// </summary>
        public string PFORGNOName { get; set; }
        /// <summary>
        /// 热点类别
        /// </summary>
        public string HOTTYPE { get; set; }
        /// <summary>
        /// 热点类别名称
        /// </summary>
        public string HOTTYPEName { get; set; }
        /// <summary>
        /// 火灾等级名称
        /// </summary>
        public string FIRELEVELName { get; set; }

        /// <summary>
        /// 火情来源气象卫星 1 气象卫星 2 人工补录
        /// </summary>
        public string FIREFROMWEATHER { get; set; }
       
    }
}