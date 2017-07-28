using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW.AH.Application.Interfance;
using TLW.AH.Business.DBUtility;
using TLW.AH.Business.ServiceIDal;

namespace TLW.AH.Application.Service
{
    public class JCFireApplicationService : IJCFireApplicationService
    {

        #region Identity
        //private IJCFireService iJCFireService = null;

        //public JCFireApplicationService(IJCFireService jcfireService)
        //{
        //    this.iJCFireService = jcfireService;
        //}
        [Dependency]
        public IJCFireService iJCFireService { get; set; }//火情信息 
        #endregion Identity

        #region 方法主体

        /// <summary>
        /// 由Orgno获取组织机构
        /// </summary>
        /// <param name="orgno"></param>
        /// <returns></returns>
        public T_SYS_ORG GetSysOrgByOrgNOData(string orgno)
        {
            return iJCFireService.GetSysOrgByOrgNO(orgno);
        }

        /// <summary>
        /// 设备id检索视频设备信息
        /// </summary>
        /// <param name="devid"></param>
        /// <returns></returns>
        public JC_MONITOR_BASICINFO GetMonitorBasicInfoData(string devid)
        {
            return iJCFireService.GetMonitorBasicInfoByDevid(devid);
        }

        /// <summary>
        /// 增加火情
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool AddJcFireData(JC_FIRE info)
        {
            var model = iJCFireService.AddJcFire(info);
            if (model != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// 主键获取火情信息
        /// </summary>
        /// <param name="jcfid">火情ID</param>
        /// <returns></returns>
        public JC_FIRE GetFireInfoByJCFID(string jcfid)
        {
            return iJCFireService.GetFireInfoByJCFID(jcfid);
        }

        public string GetStr(string str)
        {
            return iJCFireService.GetBussinessStr(str);
        }

        #endregion

    }
}
