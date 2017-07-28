using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物_采集数据表
    /// </summary>
    public class PEST_COLLECTDATA_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string PESTCOLLDATAID { get; set; }
        /// <summary>
        /// 护林员ID
        /// </summary>
        public string HID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 采集来源编号
        /// </summary>
        public string COLLECTRESOURCE { get; set; }
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
        public float SMALLCLASSAREA { get; set; }
        /// <summary>
        /// 寄主树种
        /// </summary>
        public string HOSTTREESPECIESCODE { get; set; }
        /// <summary>
        /// 调查类型编号
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
        /// 备注
        /// </summary>
        public string MARK { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UPLOADTIME { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public int MANSTATE { get; set; }
        /// <summary>
        /// 反馈结果
        /// </summary>
        public string MANRESULT { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime MANTIME { get; set; }
        /// <summary>
        /// 处理人ID
        /// </summary>
        public string MANUSERID { get; set; }
        /// <summary>
        /// 空间库对应序号
        /// </summary>
        public string KID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 每页数据条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurPage { get; set; }
    }
}
