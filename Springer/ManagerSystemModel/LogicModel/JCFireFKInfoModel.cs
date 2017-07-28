using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel.LogicModel
{
    /// <summary>
    /// 监测火情 与火情反馈信息
    /// </summary>
    public class JCFireFKInfoModel
    {
        /// <summary>
        /// 火情反馈信息
        /// </summary>
        public JCFireFKInfoModel()
        {
            this.JC_FireData = new JC_FIRE_Model();
            this.JC_FireFKData = new JC_FIRETICKLING_Model();
        }
        /// <summary>
        /// 监测火情数据
        /// </summary>
        public JC_FIRE_Model JC_FireData { get; set; }

        /// <summary>
        /// 火情反馈数据
        /// </summary>
        public JC_FIRETICKLING_Model JC_FireFKData { get; set; }

        /// <summary>
        /// 反馈情况
        /// </summary>
        public string FKNAME { get; set; }
        /// <summary>
        /// 热点类别
        /// </summary>
        public string HOTETYPENAME { get; set; }
        /// <summary>
        /// 连续性
        /// </summary>
        public string LXNAME { get; set; }
        /// <summary>
        /// 火情来源
        /// </summary>
        public string FIRESOURCENAME { get; set; }
        /// <summary>
        /// 机构名
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string UserName { get; set; }
    }
}
