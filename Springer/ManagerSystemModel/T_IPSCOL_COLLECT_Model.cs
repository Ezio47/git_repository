using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{

    /// <summary>
    /// 上报单位统计Model
    /// </summary>
    public class T_IPSCOL_COLLECT_OrgCountModel
    {
        /// <summary>
        /// 护林员ID
        /// </summary>
        public string HID { get; set; }
        /// <summary>
        /// 护林员姓名
        /// </summary>
        public string HName { get; set; }
        /// <summary>
        /// 组织机构码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 组织机构名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 上报总数量
        /// </summary>
        public string CollectCount { get; set; }
        /// <summary>
        /// 分类统计
        /// </summary>
        public IEnumerable<T_IPSCOL_COLLECT_TypeCountModel> TypeCountModel { get; set; }

    }
    /// <summary>
    /// 类型统计Model
    /// </summary>
    public class T_IPSCOL_COLLECT_TypeCountModel
    {
        /// <summary>
        /// 类别ID
        /// </summary>
        public string typeID { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string typeName { get; set; }
        /// <summary>
        /// 上报数量
        /// </summary>
        public string typeCount { get; set; }
    }

    /// <summary>
    /// 数据采集Model
    /// </summary>
    public class T_IPSCOL_COLLECT_Model
    {
        /// <summary>
        /// 采集ID
        /// </summary>
        public string COLLECTID { get; set; }
        /// <summary>
        /// 护林员ID
        /// </summary>
        public string HID { get; set; }
        /// <summary>
        /// 类型值
        /// </summary>
        public string SYSTYPEVALUE { get; set; }
        /// <summary>
        /// 发生地
        /// </summary>
        public string ADDRESS { get; set; }
        /// <summary>
        /// 采集时间
        /// </summary>
        public string COLLECTTIME { get; set; }
        /// <summary>
        /// 采集标题
        /// </summary>
        public string COLLECTNAME { get; set; }
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
        /// 数据ID
        /// </summary>
        public string COLLECTDETAILID { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string LONGITUDE { get; set; }

        /// <summary>
        /// 原始经度
        /// </summary>
        public string ORILONGITUDE { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string LATITUDE { get; set; }

        /// <summary>
        /// 原始纬度
        /// </summary>
        public string ORILATITUDE { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public string HEIGHT { get; set; }
        /// <summary>
        /// 方位
        /// </summary>
        public string DIRECTION { get; set; }
        /// <summary>
        /// 数据采集上传ID
        /// </summary>
        public string COLLECTUPLOADID { get; set; }
        /// <summary>
        /// 上传URL
        /// </summary>
        public string UPLOADURL { get; set; }
        /// <summary>
        /// 上传名
        /// </summary>
        public string UPLOADNAME { get; set; }
        /// <summary>
        /// 上传描述
        /// </summary>
        public string UPLOADDESCRIBE { get; set; }



        /// <summary>
        /// 处理人用户名
        /// </summary>
        public string ManUserName { get; set; }
        /// <summary>
        /// 护林员名称
        /// </summary>
        public string HName { get; set; }

        /// <summary>
        /// 护林员机构编码
        /// </summary>
        public string OrgNo { get; set; }
        /// <summary>
        /// 护林员机构名称
        /// </summary>
        public string OrgNoName { get; set; }


        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string opMethod { get; set; }

        /// <summary>
        /// 采集类别名称
        /// </summary>
        public string SYSTYPEName { get; set; }

        /// <summary>
        /// 采集点经纬度
        /// </summary>
        public IList<T_IPSCOL_COLLECTDETAIL_SW> DataList { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string Rights { get; set; }
    }
}
