using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 监测_群众报警表
    /// </summary>
    public class JC_PERALARM_Model
    {
        /// <summary>
        /// 报警序号
        /// </summary>
        public string PERALARMID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 火情名称
        /// </summary>
        public string FIRENAME { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PERALARMPHONE { get; set; }
        /// <summary>
        /// 报警人
        /// </summary>
        public string PERALARMNAME { get; set; }
        /// <summary>
        /// 报警时间
        /// </summary>
        public string PERALARMTIME { get; set; }
        /// <summary>
        /// 报警发生地
        /// </summary>
        public string PERALARMADDRESS { get; set; }
        /// <summary>
        /// 报警内容
        /// </summary>
        public string PERALARMCONTENT { get; set; }
        /// <summary>
        /// 是否处理 0未处理 1已处理
        /// </summary>
        public string MANSTATE { get; set; }
        /// <summary>
        /// 反馈结果
        /// </summary>
        public string MANRESULT { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public string MANTIME { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public string MANUSERID { get; set; }
        
        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 报警摘要
        /// </summary>
        public string PEARLARMPRE { get; set; }
        /// <summary>
        /// 是否下发
        /// </summary>
        public string PEARLARMISSUED { get; set; }


        /// <summary>
        /// 所属单位名称
        /// </summary>
        public string ORGNAME { get; set; }
        /// <summary>
        /// 处理人用户名
        /// </summary>
        public string ManUserName { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }


        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 多选组织机构
        /// </summary>
        public string BYORGNOLIST { get; set; }
    }
}
