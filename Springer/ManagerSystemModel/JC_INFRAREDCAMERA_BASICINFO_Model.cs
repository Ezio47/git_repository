using ManagerSystemModel.ExtenAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 预警监测-红外相机
    /// </summary>
    public class JC_INFRAREDCAMERA_BASICINFO_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string INFRAREDCAMERAID { get; set; }
        /// <summary>
        /// 所属单位编号
        /// </summary>
        [DisplayName("所属单位名称")]
        [DicType("OrgNO")]
        public string BYORGNO { get; set; }
        /// <summary>
        /// 相机名称
        /// </summary>
        [DisplayName("相机名称")]
        public string INFRAREDCAMERANAME { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string PHONE { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        [DisplayName("经度")]
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        [DisplayName("纬度")]
        public string WD { get; set; }
        /// <summary>
        /// 高程
        /// </summary>
        [DisplayName("高程")]
        public string GC { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [DisplayName("地址")]
        public string ADDRESS { get; set; }

        /// <summary>
        /// 空间数据Shape字段
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
        /// 页面数量
        /// </summary>
        public string pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public string curPage { get; set; }


        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
    }
    /// <summary>
    /// 预警监测-红外相机-彩信图片
    /// </summary>
    public class JC_INFRAREDCAMERA_PHOTO_Model
    {
        /// <summary>
        /// 回传照片序号
        /// </summary>
        public string JC_INFRAREDCAMERA_PHOTOID { get; set; }

        /// <summary>
        /// 相机序号
        /// </summary>
        public string INFRAREDCAMERAID { get; set; }

        /// <summary>
        /// 照片时间
        /// </summary>
        public string PHOTOTIME { get; set; }

        /// <summary>
        /// 照片名称
        /// </summary>
        public string PHOTOTITLE { get; set; }


        /// <summary>
        /// 序号
        /// </summary>
        public string smid { get; set; }
        /// <summary>
        /// 发送号码
        /// </summary>
        public string tpa { get; set; }
        /// <summary>
        /// 接收日期
        /// </summary>
        public string recvdatetime { get; set; }

        /// <summary>
        /// 图片存储编号
        /// </summary>
        public string mmsfilesid { get; set; }
        /// <summary>
        /// 彩信类型
        /// </summary>
        public string filetype { get; set; }
        /// <summary>
        /// 图片存储地址 注：绝对路径 D:\photo\201512011559490575791\image1448956524445.jpg.JPG
        /// </summary>
        public string filename { get; set; }


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
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 相机基本信息
        /// </summary>
        public JC_INFRAREDCAMERA_BASICINFO_Model BasicInfoModel { get; set; }
    }
}
