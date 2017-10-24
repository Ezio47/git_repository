using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物_采集数据模型
    /// </summary>
    public class PEST_COLLECTDATA_Model
    {
        #region 基本字段
        /// <summary>
        /// 序号
        /// </summary>
        public string PESTCOLLDATAID { get; set; }
        /// <summary>
        /// 护林员ID
        /// </summary>
        public string HID { get; set; }
        /// <summary>
        /// 采集来源编号 1：护林员上传 2：系统录入
        /// </summary>
        public string COLLECTRESOURCE { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 村名
        /// </summary>
        public string VILLAGENAME { get; set; }
        /// <summary>
        /// 小地名
        /// </summary>
        public string SMALLADDRESS { get; set; }
        /// <summary>
        /// 小班号
        /// </summary>
        public string SMALLCLASSCODE { get; set; }
        /// <summary>
        /// 小班面积
        /// </summary>
        public string SMALLCLASSAREA { get; set; }
        /// <summary>
        /// 寄主树种
        /// </summary>
        public string HOSTTREESPECIESCODE { get; set; }
        /// <summary>
        /// 调查类型
        /// </summary>
        public string SEARCHTYPE { get; set; }
        /// <summary>
        /// 病虫名称编号
        /// </summary>
        public string COLLECTPESTCODE { get; set; }
        /// <summary>
        /// 危害部位编号
        /// </summary>
        public string HARMPOSITION { get; set; }
        /// <summary>
        /// 危害程度编号
        /// </summary>
        public string HARMLEVEL { get; set; }
        /// <summary>
        /// 疑似病死株数
        /// </summary>
        public string DEADCOUNT { get; set; }
        /// <summary>
        /// 不明枯死株数
        /// </summary>
        public string UNKNOWNDIEOFFCOUNT { get; set; }
        /// <summary>
        /// 其他枯死株数
        /// </summary>
        public string ELSEDIEOFFCOUNT { get; set; }
        /// <summary>
        /// 取样株数
        /// </summary>
        public string SAMPLECOUNT { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public string UPLOADTIME { get; set; }
        /// <summary>
        /// 处理状态
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
        /// 处理人ID
        /// </summary>
        public string MANUSERID { get; set; }
        /// <summary>
        /// 空间库对应序号
        /// </summary>
        public string   KID { get; set; }
        /// <summary>
        /// 经纬度集合
        /// </summary>
        public string JWDLIST { get; set; } 
        #endregion

        #region 扩展字段
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string BYORGNONAME { get; set; }
        /// <summary>
        /// 寄主树种名称
        /// </summary>
        public string HOSTTREESPECIESNAME { get; set; }
        /// <summary>
        /// 调查类型名称
        /// </summary>
        public string SEARCHTYPENAME { get; set; }
        /// <summary>
        /// 病虫名称
        /// </summary>
        public string COLLECTPESTNAME { get; set; }
        /// <summary>
        /// 危害部位名称
        /// </summary>
        public string HARMPOSITIONNAME { get; set; }
        /// <summary>
        /// 危害程度名称
        /// </summary>
        public string HARMLEVELNAME { get; set; }
        /// <summary>
        /// 处理状态名称
        /// </summary>
        public string MANSTATENAME { get; set; }
        #endregion
    }
}
