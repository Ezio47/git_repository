using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{

    /// <summary>
    /// 预警_火险等级表
    /// </summary>
    public class YJ_DANGERCLASS_Model
    {
        /// <summary>
        /// 火险等级序号
        /// </summary>
        public string DANGERID { get; set; }
        /// <summary>
        /// 等级时间
        /// </summary>
        public string DCDATE { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 乡镇名称
        /// </summary>
        public string TOWNNAME { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 火险等级
        /// </summary>
        public string DANGERCLASS { get; set; }
        /// <summary>
        /// 上级单位名称
        /// </summary>
        public string TOPTOWNNAME { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }

        /// <summary>
        /// 天气
        /// </summary>
        public string WEATHER { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public string TEMPREATURE { get; set; }

        /// <summary>
        /// 风向风速
        /// </summary>
        public string WINDYSPEED { get; set; }

        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }

        #region 三维火险等级
        /// <summary>
        /// 区域名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public string DValue { get; set; }


        /// <summary>
        /// 县代码
        /// </summary>
        public string XIANDAIMA { get; set; }
        #endregion
        
    }
}