using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 通知公告Model
    /// </summary>
    public class T_SYS_NOTICE_Model
    {
        /// <summary>
        /// 信息序号
        /// </summary>
        public string INFOID { get; set; }
        /// <summary>
        /// 信息标题
        /// </summary>
        public string INFOTITLE { get; set; }
        /// <summary>
        /// 信息内容
        /// </summary>
        public string INFOCONTENT { get; set; }
        /// <summary>
        /// 信息图片URL
        /// </summary>
        public string INFOURL { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string FBTIME { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string LABLE { get; set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public string NUM { get; set; }
        /// <summary>
        /// 信息类别
        /// </summary>
        public string INFOTYPE { get; set; }
        /// <summary>
        /// 发布人ID
        /// </summary>
        public string INFOUSERID { get; set; }
        /// <summary>
        /// 所属系统标识
        /// </summary>
        public string SYSFLAG { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public string LOGINUSERNAME { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 发布人名称
        /// </summary>
        public string FBUserName { get; set; }
    }
}
