using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 热点Model
    /// </summary>
  public   class T_IPS_HOTS_Model
    {
      /// <summary>
        /// 热点ID
      /// </summary>
        public string HOTSID { get; set; }
      /// <summary>
        /// 编号
      /// </summary>
        public string BH { get; set; }
      /// <summary>
        /// 卫星编号
      /// </summary>
        public string WXBH { get; set; }
      /// <summary>
        /// 热点编号
      /// </summary>
        public string DQRDBH { get; set; }
      /// <summary>
        /// 护林员
      /// </summary>
        public string HLY { get; set; }
      /// <summary>
        /// 火灾发生地
      /// </summary>
        public string ZQWZ { get; set; }
      /// <summary>
        /// 经度
      /// </summary>
        public string JD { get; set; }
      /// <summary>
        /// 纬度
      /// </summary>
        public string WD { get; set; }
      /// <summary>
      /// 像素
      /// </summary>
        public string XS { get; set; }
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
        /// 接收时间
      /// </summary>
        public string FXSJ { get; set; }
      /// <summary>
        /// 上报时间
      /// </summary>
        public string SBSJ { get; set; }
      /// <summary>
        /// 卫星图片存储编号
      /// </summary>
        public string BZW { get; set; }
      /// <summary>
        /// 文件编号
      /// </summary>
        public string FJBH { get; set; }
      /// <summary>
        /// MMddHHyy+区划编码
      /// </summary>
        public string WLBH { get; set; }
      /// <summary>
        /// 区划编码
      /// </summary>
        public string XZQH { get; set; }
      /// <summary>
        /// 
      /// </summary>
        public string CZW { get; set; }
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
      /// 方法
      /// </summary>
        public string opMethod { get; set; }

      /// <summary>
      /// 权限
      /// </summary>
        public string Rights { get; set; }
    }
}
