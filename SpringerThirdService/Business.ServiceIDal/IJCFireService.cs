using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW.AH.Business.DBUtility;

namespace TLW.AH.Business.ServiceIDal
{
    public interface IJCFireService : IBaseService<JC_FIRE>
    {

        /// <summary>
        /// 由Orgno查询组织机构
        /// </summary>
        /// <param name="orgno"></param>
        /// <returns></returns>
        T_SYS_ORG GetSysOrgByOrgNO(string orgno);



        /// <summary>
        /// 设备编号id检索设备信息
        /// </summary>
        /// <param name="devid"></param>
        /// <returns></returns>
        JC_MONITOR_BASICINFO GetMonitorBasicInfoByDevid(string devid);

        /// <summary>
        /// 增加火情
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        JC_FIRE AddJcFire(JC_FIRE info);

        /// <summary>
        /// 主键获取火情信息
        /// </summary>
        /// <param name="jcfid">火情id</param>
        /// <returns></returns>
        JC_FIRE GetFireInfoByJCFID(string jcfid);

        string GetBussinessStr(string str);
    }
}
