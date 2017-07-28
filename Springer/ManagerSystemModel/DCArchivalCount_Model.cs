using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 档案统计模型
    /// </summary>
    public class DCArchivalCount_Model
    {
        /// <summary>
        /// 火情监测id
        /// </summary>
        public string JCFID { get; set; }
        /// <summary>
        /// 组织机构码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 组织机构名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 热点类别
        /// </summary>
        public string HOTTYPE { get; set; }
        /// <summary>
        /// 热点名称
        /// </summary>
        public string HOTTYPECount { get; set; }
        /// <summary>
        /// 热点统计
        /// </summary>
        public IEnumerable<HOTTYPECountModel> HOTTYPECountModel { get; set; }
        /// <summary>
        /// 火情等级统计
        /// </summary>
        public IEnumerable<FIRELEVELCountModel> FIRELEVELCountModel { get; set; }
        /// <summary>
        /// 火情来源
        /// </summary>
        public string FIREFROM { get; set; }
        /// <summary>
        /// 红外相机统计
        /// </summary>
        public string FIREFROMCount1 { get; set; }
        /// <summary>
        /// 电话报警统计
        /// </summary>
        public string FIREFROMCount2 { get; set; }
        /// <summary>
        /// 卫星热点统计
        /// </summary>
        public string FIREFROMCount3 { get; set; }
        /// <summary>
        /// 电子报警统计
        /// </summary>
        public string FIREFROMCount4 { get; set; }
        /// <summary>
        /// 护林员火情统计
        /// </summary>
        public string FIREFROMCount5 { get; set; }
        /// <summary>
        /// 飞机巡护统计
        /// </summary>
        public string FIREFROMCount6 { get; set; }
        /// <summary>
        /// 历史补录统计
        /// </summary>
        public string FIREFROMCount7 { get; set; }
        /// <summary>
        /// 火情来源总数统计
        /// </summary>
        public string FIREFROMCount { get; set; }
        /// <summary>
        /// 火线等级
        /// </summary>
        public string FIRELEVEL { get; set; }
        /// <summary>
        /// 火线等级统计
        /// </summary>
        public string FIRELEVELCount { get; set; }
    }
    /// <summary>
    /// 热点类别统计模型
    /// </summary>
    public class HOTTYPECountModel 
    {
        /// <summary>
        /// 热点值
        /// </summary>
        public string HOTTYPEvalue { get; set; }
        /// <summary>
        /// 热点名称
        /// </summary>
        public string HOTTYPEname { get; set; }
        /// <summary>
        /// 热点统计
        /// </summary>
        public string DictHOTTYPECount { get; set; }
    }
    /// <summary>
    /// 火灾等级统计模型
    /// </summary>
    public class FIRELEVELCountModel
    {
        /// <summary>
        /// 火情等级
        /// </summary>
        public string FIRELEVELvalue { get; set; }
        /// <summary>
        /// 火情等级名称
        /// </summary>
        public string FIRELEVELname { get; set; }
        /// <summary>
        /// 火情等级统计
        /// </summary>
        public string DictFIRELEVELCount { get; set; }
    }
}
