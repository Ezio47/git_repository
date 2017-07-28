using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 火情反馈
    /// </summary>
    public class JC_FIRETICKLING_SW
    {

        /// <summary>
        /// 反馈序号
        /// </summary>
        public string FKID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JCFID { get; set; }
        /// <summary>
        /// 地类
        /// </summary>
        public string DL { get; set; }
        /// <summary>
        /// 林区名称
        /// </summary>
        public string FORESTNAME { get; set; }
        /// <summary>
        /// 林火类别
        /// </summary>
        public string FORESTFIRETYPE { get; set; }
        /// <summary>
        /// 可燃物类型
        /// </summary>
        public string FUELTYPE { get; set; }
        /// <summary>
        /// 热点类别
        /// </summary>
        public string HOTTYPE { get; set; }
        /// <summary>
        /// 核查时间
        /// </summary>
        public string CHECKTIME { get; set; }
        /// <summary>
        /// 是否烟云
        /// </summary>
        public string YY { get; set; }
        /// <summary>
        /// 是否连续火
        /// </summary>
        public string JXHQSJ { get; set; }
        /// <summary>
        /// 起火时间
        /// </summary>
        public string FIREBEGINTIME { get; set; }
        /// <summary>
        /// 灭火时间
        /// </summary>
        public string FIREENDTIME { get; set; }
        /// <summary>
        /// 是否已灭
        /// </summary>
        public string ISOUTFIRE { get; set; }
        /// <summary>
        /// 过火面积
        /// </summary>
        public string BURNEDAREA { get; set; }
        /// <summary>
        /// 过火林地面积
        /// </summary>
        public string OVERDOAREA { get; set; }
        /// <summary>
        /// 受害森林面积
        /// </summary>
        public string LOSTFORESTAREA { get; set; }
        /// <summary>
        /// 其他损失情况
        /// </summary>
        public string ELSELOSSINTRO { get; set; }
        /// <summary>
        /// 情况简介
        /// </summary>
        public string FIREINTRO { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public string MANUSERID { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public string MANTIME { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public string MANSTATE { get; set; }

        /// <summary>
        /// 审核不通过理由
        /// </summary>
        public string AUDITREASON { get; set; }

        /// <summary>
        /// 火情实际发生地
        /// </summary>
        public string ADDRESS { get; set; }
        /// <summary>
        /// 火情实际经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 火情实际纬度
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
    }
}
