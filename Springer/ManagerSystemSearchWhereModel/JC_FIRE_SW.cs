using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 监测火情
    /// </summary>
    public class JC_FIRE_SW
    {

        /// <summary>
        /// 序号
        /// </summary>
        public string JCFID { get; set; }

        /// <summary>
        /// 序号str
        /// </summary>
        public string JCFIDSTR { get; set; }

        /// <summary>
        /// 火情名称
        /// </summary>
        public string FIRENAME { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
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
        /// 热点类别
        /// </summary>
        public string HOTTYPE { get; set; }
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
        /// 来源气象局
        /// </summary>
        public string FIREFROMWEATHER { get; set; }

        /// <summary>
        /// 处理状态str
        /// </summary>
        public string MANSTATESTR { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 结束时间时间
        /// </summary>
        public string EndTime { get; set; }


        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
        /// <summary>
        /// 火灾级别
        /// </summary>
        public string FIRELEVEL { get; set; }
        /// <summary>
        /// 火灾等级名称
        /// </summary>
        public string FIRELEVELname { get; set; }
        /// <summary>
        /// 需要包含下级单位的单位编码
        /// </summary>
        public string TopORGNO { get; set; }

        /// <summary>
        /// 用于首页统计
        /// </summary>
        public string isCountIndex { get; set; }
        /// <summary>
        /// 是否选中热点类别
        /// </summary>
        public string type1 { get; set; }
        /// <summary>
        /// 是否选中火险等级
        /// </summary>
        public string type2 { get; set; }
        /// <summary>
        /// 是否选中火情来源
        /// </summary>
        public string type3 { get; set; }
        /// <summary>
        /// type1+type2+type3组合
        /// </summary>
        public string TYPE { get; set; }

        /// <summary>
        /// 单独取市县镇
        /// </summary>
        public string ORGNOSXZ { get; set; }

    }
}
