using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 档案统计Echarts
    /// </summary>
    public class ArchvalCount_Model
    {
        /// <summary>
        /// 组织机构编码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 组织机构名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 林火数统计
        /// </summary>
        public string Hotetype1Count { get; set; }
        /// <summary>
        /// 草原火数统计
        /// </summary>
        public string Hotetype2Count { get; set; }
        /// <summary>
        /// 计划烧除数统计
        /// </summary>
        public string Hotetype3Count { get; set; }
        /// <summary>
        /// 农用火数统计
        /// </summary>
        public string Hotetype4Count { get; set; }
        /// <summary>
        /// 炼山数统计
        /// </summary>
        public string Hotetype5Count { get; set; }
        /// <summary>
        /// 灌木火数统计
        /// </summary>
        public string Hotetype6Count { get; set; }
        /// <summary>
        /// 工矿用火数统计
        /// </summary>
        public string Hotetype7Count { get; set; }
        /// <summary>
        /// 境外火数统计
        /// </summary>
        public string Hotetype8Count { get; set; }
        /// <summary>
        /// 未找到数统计
        /// </summary>
        public string Hotetype9Count { get; set; }
        /// <summary>
        /// 核查中数统计
        /// </summary>
        public string Hotetype10Count { get; set; }
        /// <summary>
        /// 荒火数统计
        /// </summary>
        public string Hotetype11Count { get; set; }
        /// <summary>
        /// 其他数统计
        /// </summary>
        public string Hotetype12Count { get; set; }
        /// <summary>
        /// 一般(VI级)
        /// </summary>
        public string FireLevelCount { get; set; }
        /// <summary>
        /// 较大(III级)
        /// </summary>
        public string FireLevel2Count { get; set; }
        /// <summary>
        /// 重大(II级)
        /// </summary>
        public string FireLevel3Count { get; set; }
        /// <summary>
        /// 特别重大(I级)
        /// </summary>
        public string FireLevel4Count { get; set; }
        /// <summary>
        /// 红外相机
        /// </summary>
        public string FireFrom1Count { get; set; }
        /// <summary>
        /// 电话报警
        /// </summary>
        public string FireFrom2Count { get; set; }
        /// <summary>
        /// 卫星热点
        /// </summary>
        public string FireFrom3Count { get; set; }
        /// <summary>
        /// 电子监控
        /// </summary>
        public string FireFrom4Count { get; set; }
        /// <summary>
        /// 护林员火情
        /// </summary>
        public string FireFrom5Count { get; set; }
        /// <summary>
        /// 无人机巡护
        /// </summary>
        public string FireFrom6Count { get; set; }
        /// <summary>
        /// 历史补录
        /// </summary>
        public string FireFrom7Count { get; set; }
    }
}
