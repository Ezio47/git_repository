using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 公用_生物分类表
    /// </summary>
    public class T_SYS_BIOLOGICALTYPE_Model
    {
        /// <summary>
        /// 类别
        /// </summary>
        public string BIOLOTYPE { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary>
        public string BIOLOCODE { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string BIOLONAME { get; set; }
        /// <summary>
        /// 分类科名称
        /// </summary>
        public string BIOLOKENAME { get; set; }
        /// <summary>
        /// 分类科名称
        /// </summary>
        public string BIOLOSHUNAME { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string BIOLOENNAME { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
    }

    /// <summary>
    /// 本地关联的生物分类模型
    /// </summary>
    public class T_SYS_BIOLOGICALTYPE_BY_PESTJOIN_Model
    {
        /// <summary>
        /// 生物编码
        /// </summary>
        public string BIOLOCODE { get; set; }
        /// <summary>
        /// 生物名称
        /// </summary>
        public string BIOLONAME { get; set; }
        /// <summary>
        /// 本地是否拥有
        /// </summary>
        public string isCheck { get; set; }
        /// <summary>
        /// 子生物
        /// </summary>
        public IEnumerable<T_SYS_BIOLOGICALTYPE_BY_PESTJOIN_Model> subModel { get; set; }
    }

    /// <summary>
    /// 本地关联的动物分类模型
    /// </summary>
    public class T_SYS_BIOLOGICALTYPE_WILD_LOCALANIMAL_Model
    {
        /// <summary>
        /// 生物编码
        /// </summary>
        public string BIOLOCODE { get; set; }
        /// <summary>
        /// 生物名称
        /// </summary>
        public string BIOLONAME { get; set; }
        /// <summary>
        /// 本地是否拥有
        /// </summary>
        public string isCheck { get; set; }
        /// <summary>
        /// 子生物
        /// </summary>
        public IEnumerable<T_SYS_BIOLOGICALTYPE_WILD_LOCALANIMAL_Model> subModel { get; set; }
    }
    /// <summary>
    /// 本地关联的植物模型
    /// </summary>

    public class T_SYS_BIOLOGICALTYPE_WILD_LOCALBOTANY_Model
    {
        /// <summary>
        /// 生物编码
        /// </summary>
        public string BIOLOCODE { get; set; }
        /// <summary>
        /// 生物名称
        /// </summary>
        public string BIOLONAME { get; set; }
        /// <summary>
        /// 本地是否拥有
        /// </summary>
        public string isCheck { get; set; }
        /// <summary>
        /// 子生物
        /// </summary>
        public IEnumerable<T_SYS_BIOLOGICALTYPE_WILD_LOCALBOTANY_Model> subModel { get; set; }
    }
}
