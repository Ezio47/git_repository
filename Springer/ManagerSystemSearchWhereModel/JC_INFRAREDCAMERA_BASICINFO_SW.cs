using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 预警监测-红外相机
    /// </summary>
    public class JC_INFRAREDCAMERA_BASICINFO_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string INFRAREDCAMERAID { get; set; }
        /// <summary>
        /// 所属单位编号
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int curPage { get; set; }
    }
    /// <summary>
    /// 预警监测-红外相机-彩信图片
    /// </summary>
    public class JC_INFRAREDCAMERA_PHOTO_SW
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
        /// 是否处理
        /// </summary>
        public string MANSTATE { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string DateBegin { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string DateEnd { get; set; }
        /// <summary>
        /// 获取最新记录个数
        /// </summary>
        public string TopCount { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }

    }
}
