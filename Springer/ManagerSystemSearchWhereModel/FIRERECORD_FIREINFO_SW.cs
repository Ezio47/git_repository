using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManagerSystemClassLibrary
{    
    /// <summary>
    /// 火情档案
    /// </summary>
    public class FIRERECORD_FIREINFO_SW
   {
       /// <summary>
       /// 序号
       /// </summary>
        public string FRFIID { get; set; }
        /// <summary>
        /// 序号2
        /// </summary>
        public string JCFID { get; set; }
       /// <summary>
       /// 所属机构编码
       /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 火灾编号
        /// </summary>
        public string FIRECODE { get; set; }
       /// <summary>
       /// 起火地点县
       /// </summary>
        public string FIREADDRESSCOUNTY { get; set; }
       /// <summary>
       /// 起火地点乡
       /// </summary>
        public string FIREADDRESSTOWNS { get; set; }
       /// <summary>
       /// 起火地点村
       /// </summary>
        public string FIREADDRESSVILLAGES { get; set; }
        /// <summary>
        /// 起火地点详细地 
        /// </summary>
        public string FIREADDRESS { get; set; }
        /// <summary>
        /// 起火时间
        /// </summary>
        public string FIRETIME { get; set; }
        /// <summary>
        /// 灭火时间
        /// </summary>
        public string FIREENDTIME { get; set; }
        /// <summary>
        /// 火灾种类编号
        /// </summary>
        public string FIRERECINFO000 { get; set; }
        /// <summary>
        /// 火灾等级编号
        /// </summary>
        public string FIRERECINFO001 { get; set; }
        /// <summary>
        /// 火场总面积
        /// </summary>
        public string FIRERECINFO020 { get; set; }
        /// <summary>
        /// 有林地面积
        /// </summary>
        public string FIRERECINFO021 { get; set; }
        /// <summary>
        /// 原始林成灾面积
        /// </summary>
        public string FIRERECINFO030 { get; set; }
        /// <summary>
        /// 次生林成灾面积
        /// </summary>
        public string FIRERECINFO031 { get; set; }
        /// <summary>
        /// 人工林成灾面积
        /// </summary>
        public string FIRERECINFO032 { get; set; }
        /// <summary>
        /// 成林蓄积损失
        /// </summary>
        public string FIRERECINFO040 { get; set; }
        /// <summary>
        /// 幼林株数损失
        /// </summary>
        public string FIRERECINFO041 { get; set; }
        /// <summary>
        /// 林分组成
        /// </summary>
        public string FIRERECINFO050 { get; set; }
        /// <summary>
        /// 林龄
        /// </summary>
        public string FIRERECINFO051 { get; set; }
        /// <summary>
        /// 火场指挥员姓名
        /// </summary>
        public string FIRERECINFO060 { get; set; }
        /// <summary>
        /// 火场指挥员职务
        /// </summary>
        public string FIRERECINFO061 { get; set; }
         /// <summary>
        /// 人员轻伤人数
        /// </summary>
        public string FIRERECINFO070{ get; set; }
        /// <summary>
        /// 人员重伤人数
        /// </summary>
        public string FIRERECINFO071 { get; set; }
        /// <summary>
        /// 人员死亡人数
        /// </summary>
        public string FIRERECINFO072{ get; set; }
        /// <summary>
        /// 火案查处是否已处理
        /// </summary>
        public string FIRERECINFO080 { get; set; }
        /// <summary>
        /// 林政处罚人数
        /// </summary>
        public string FIRERECINFO081 { get; set; }
        /// <summary>
        /// 刑事处罚人数
        /// </summary>
        public string FIRERECINFO082 { get; set; }
        /// <summary>
        /// 其他损失折款
        /// </summary>
        public string FIRERECINFO090 { get; set; }
        /// <summary>
        /// 出动扑火人工
        /// </summary>
        public string FIRERECINFO100 { get; set; }
        /// <summary>
        /// 其中汽车
        /// </summary>
        public string FIRERECINFO110{ get; set; }
        /// <summary>
        /// 出动车辆合计
        /// </summary>
        public string FIRERECINFO111 { get; set; }
        /// <summary>
        /// 出动飞机
        /// </summary>
        public string FIRERECINFO120 { get; set; }
        /// <summary>
        /// 扑火经费
        /// </summary>
        public string FIRERECINFO130 { get; set; }
        /// <summary>
        /// 查明火源是否已经查明
        /// </summary>
        public string FIRERECINFO140 { get; set; }
        /// <summary>
        /// 查明火源起火原因
        /// </summary>
        public string FIRERECINFO150 { get; set; }
        /// <summary>
        /// 火源
        /// </summary>
        public string FIRERECINFO160 { get; set; }
        /// <summary>
        /// 受害森林面积
        /// </summary>
        public string FIRELOSEAREA { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurPage { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }


        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }
    }
}
