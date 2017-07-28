using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 交接班记录表
    /// </summary>
   public class OD_HANDOVER_SW
    {
       /// <summary>
       /// 值班交班序号
       /// </summary>
        public string ODHID { get; set; }
       /// <summary>
       /// 值班日期
       /// </summary>
        public string ONDUTYDATE { get; set; }
       /// <summary>
       /// 所属机构编码
       /// </summary>
        public string BYORGNO { get; set; }
       /// <summary>
       /// 人员值班类别
       /// </summary>
        public string ONDUTYUSERTYPE { get; set; }
       /// <summary>
       /// 值班类别
       /// </summary>
        public string ONDUTYTYPE { get; set; }
       /// <summary>
       /// 值班人序号
       /// </summary>
        public string ONDUTYUSERID { get; set; }
       /// <summary>
       /// 获取上一班次信息 1为是 其他为否 比如根据当前班次获取上一班次交班内容
       /// </summary>
        public string isGetUPOne { get; set; }
    }
}
