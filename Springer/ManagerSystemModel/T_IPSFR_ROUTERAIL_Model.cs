using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 路线/围栏Model
    /// </summary>
   public class T_IPSFR_ROUTERAIL_Model
   {
       /// <summary>
       /// 路线/围栏序号
       /// </summary>
       public string ROADID { get; set; }
       /// <summary>
       /// 护林员序号
       /// </summary>
       public string HID { get; set; }
       /// <summary>
       /// 经度
       /// </summary>
       public string LONGITUDE { get; set; }
       /// <summary>
       /// 纬度
       /// </summary>
       public string LATITUDE { get; set; }
       /// <summary>
       /// 高度
       /// </summary>
       public string HEIGHT { get; set; }
       /// <summary>
       /// 排序号
       /// </summary>
       public string ORDERBY { get; set; }
       /// <summary>
       /// 类型 0路线1围栏
       /// </summary>
       public string ROADTYPE { get; set; }
       /// <summary>
       /// 经纬度列表，用于对多个经纬度进行管理 
       /// 约定：
       /// 多个经纬度以｜分隔
       /// 参数之间以，分隔，LONGITUDE, LATITUDE, HEIGHT, ORDERBY
       /// </summary>
       public string longitLatitList { get; set; }
       /// <summary>
       /// 在多线，多面采集中，判断死第几条线，从1开始
       /// </summary>
       public string LINEARAEID { get; set; }

       /// <summary>
       /// 护林员名称
       /// </summary>
       public string HName { get; set; }
       /// <summary>
       /// 电话号码
       /// </summary>
       public string Phone { get; set; }

       /// <summary>
       /// 操作方法
       /// </summary>
       public string opMethod { get; set; }
    }
}
