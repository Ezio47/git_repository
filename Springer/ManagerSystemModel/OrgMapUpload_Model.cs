using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 
    /// </summary>
    public class OrgMapUpload_Model
    { 
        /// <summary>
        /// 县名称
        /// </summary>
        public string 县名称 { get; set; }
        /// <summary>
        /// 乡镇名称
        /// </summary>
        public string 乡镇名称 { get; set; }
        /// <summary>
        /// 村委会名称(为空代表乡镇领导)
        /// </summary>
        public string 村委会名称 { get; set; }
        /// <summary>
        /// 自然村名称(空代表村委员会人员）
        /// </summary>
        public string 自然村名称 { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string 联系人 { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string 职位 { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string 手机 { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string 电话 { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string 排序号 { get; set; }
    }
}
