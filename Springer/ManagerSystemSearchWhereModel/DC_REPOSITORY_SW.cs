using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 数据中心_仓库表
    /// </summary>
    public class DC_REPOSITORY_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DCREPOSITORYID { get; set; }
        /// <summary>
        /// 所属仓库序号
        /// </summary>
        public string REPTYPEID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 仓库地址
        /// </summary>
        public string ADDRESS { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string RESPONSIBLEMAN { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string LINKWAY { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int curPage { get; set; }
        /// <summary>
        /// 页面大小
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
         /// 经度
         /// </summary>
         public string JD { get; set; }
         /// <summary>
         /// 纬度
         /// </summary>
         public string WD { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
         public string Other { get; set; }
    

    }
}
