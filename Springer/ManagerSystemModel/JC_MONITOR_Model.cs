using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 监测_电子监控基本信息表
    /// </summary>
    public class JC_MONITOR_BASICINFO_Model
    {
        /// <summary>
        /// 监控序号
        /// </summary>
        public string EMID { get; set; }
        /// <summary>
        /// 塔台编码
        /// </summary>
        public string TTBH { get; set; }
        /// <summary>
        /// 监控名称
        /// </summary>
        public string EMNAME { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 高程
        /// </summary>
        public string GC { get; set; }
        /// <summary>
        /// 发生地 地址
        /// </summary>
        public string ADDRESS { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public string  PORT { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string  TYPE { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LOGINUSERNAME { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string USERPWD { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string XH { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string PP { get; set; }
        /// <summary>
        /// 设备高度
        /// </summary>
        public string GD { get; set; }
        /// <summary>
        /// 最大监控距离
        /// </summary>
        public string JCJL { get; set; }

        /// <summary>
        /// 空间数据shape字段
        /// </summary>
        public string Shape { get; set; }

       
        /// <summary>
        /// 所属单位名称
        /// </summary>
        public string ORGNAME { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string OBJID { get; set; }
        /// <summary>
        /// 模板号
        /// </summary>
        public string TEMPLATEDID { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
    }
    /// <summary>
    /// 监测_电子监控表
    /// </summary>
    public class JC_MONITOR_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string IMBID { get; set; }
        /// <summary>
        /// 塔台编码
        /// </summary>
        public string TTBH { get; set; }
        /// <summary>
        /// 报警时间
        /// </summary>
        public string IMBTIME { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 水平角
        /// </summary>
        public string SPJ { get; set; }
        /// <summary>
        /// 俯仰角
        /// </summary>
        public string FYJ { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string IMBIMGURL { get; set; }


        /// <summary>
        /// 是否处理
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
        /// 监控基本信息
        /// </summary>
        public JC_MONITOR_BASICINFO_Model BasicInfoModel { get; set; }
    }
}
