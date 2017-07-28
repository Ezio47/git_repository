using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 灾损_火情基本信息表
    /// </summary>
    public class FIRELOST_FIREINFO_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string FIRELOST_FIREINFOID { get; set; }
        /// <summary>
        /// 火情序号
        /// </summary>
        public string JCFID { get; set; }
        /// <summary>
        /// 森林总面积
        /// </summary>
        public string TOTALAREA { get; set; }
        /// <summary>
        /// 灾区总人口数
        /// </summary>
        public string TOTALPERSON { get; set; }
        /// <summary>
        /// 灾区森林总蓄积量
        /// </summary>
        public string TOTALXJL { get; set; }
        /// <summary>
        /// 火场面积
        /// </summary>
        public string FIREAREA { get; set; }
        /// <summary>
        /// 森林火灾受害面积
        /// </summary>
        public string FIRELOSEAREA { get; set; }
        /// <summary>
        /// 森林蓄积损失量
        /// </summary>
        public string XJLLOSE { get; set; }
        /// <summary>
        /// 伤亡人数
        /// </summary>
        public string CASUALTYCOUNT { get; set; }
        /// <summary>
        /// 建筑物损失量
        /// </summary>
        public string BUILDINGLOSECOUNT { get; set; }
        /// <summary>
        /// 机械设备损失量
        /// </summary>
        public string MACHINERYLOSECOUNT { get; set; }
        /// <summary>
        /// 森林总面积坐标列表
        /// </summary>
        public string TOTALAREAJWDLIST { get; set; }
        /// <summary>
        /// 火场面积坐标列表
        /// </summary>
        public string FIREAREAJWDLIST { get; set; }
        /// <summary>
        /// 森林火灾受害面积坐标列表
        /// </summary>
        public string FIRELOSEAREAJWDLIST { get; set; }
        /// <summary>
        /// 损失总计
        /// </summary>
        public string LOSSCOUNT { get; set; }
        /// <summary>
        /// 森林资源损失率
        /// </summary>
        public string FORESTRESOURCELOSSRATIO { get; set; }
        /// <summary>
        /// 人均损失平均价值量
        /// </summary>
        public string AVGLOSSPERCATITAVALUE { get; set; }
        /// <summary>
        /// 林地损失平均价值量
        /// </summary>
        public string WOODLANDLOSSAVGVALUE { get; set; }
        /// <summary>
        /// 扑火成效比
        /// </summary>
        public string FIRESUPPEFFECTTHAN { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
    }
}
