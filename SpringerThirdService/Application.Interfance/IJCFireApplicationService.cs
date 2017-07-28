using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW.AH.Business.DBUtility;

namespace TLW.AH.Application.Interfance
{
    public interface IJCFireApplicationService
    {

        /// <summary>
        /// 由Orgno获取组织机构
        /// </summary>
        /// <param name="orgno"></param>
        /// <returns></returns>
        T_SYS_ORG GetSysOrgByOrgNOData(string orgno);

        /// <summary>
        /// 设备id检索视频设备信息
        /// </summary>
        /// <param name="devid"></param>
        /// <returns></returns>
        JC_MONITOR_BASICINFO GetMonitorBasicInfoData(string devid);

        /// <summary>
        /// 增加火情
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool AddJcFireData(JC_FIRE info);

        /// <summary>
        /// 主键获取火情信息
        /// </summary>
        /// <param name="jcfid">火情id</param>
        /// <returns></returns>
        JC_FIRE GetFireInfoByJCFID(string jcfid);


        string GetStr(string str);
    }
}
