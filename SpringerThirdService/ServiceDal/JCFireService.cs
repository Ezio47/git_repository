using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW.AH.Business.DBUtility;
using TLW.AH.Business.ServiceDal;
using TLW.AH.Business.ServiceIDal;

namespace TLW.AH.Business.ServiceDal
{
    public class JCFireService : BaseService<JC_FIRE>, IJCFireService
    {
        #region Identity
        private DbSet<JC_FIRE> JCFireDb;
        private DbSet<T_SYS_ORG> SYSOrgDb;//组织机构
        private DbSet<JC_MONITOR_BASICINFO> JCMonitorBasicDb;//视频基本信息
        public JCFireService(DbContext jdContext)
            : base(jdContext)
        {
            this.JCFireDb = base.Context.Set<JC_FIRE>();
            this.SYSOrgDb = base.Context.Set<T_SYS_ORG>();
            this.JCMonitorBasicDb = base.Context.Set<JC_MONITOR_BASICINFO>();
        }
        #endregion Identity

        #region 方法主体

        /// <summary>
        /// 由Orgno查询组织机构
        /// </summary>
        /// <param name="orgno"></param>
        /// <returns></returns>
        public T_SYS_ORG GetSysOrgByOrgNO(string orgno)
        {
            return SYSOrgDb.Find(orgno);
        }

        /// <summary>
        /// 设备编号id检索设备信息
        /// </summary>
        /// <param name="devid"></param>
        /// <returns></returns>
        public JC_MONITOR_BASICINFO GetMonitorBasicInfoByDevid(string devid)
        {
            return JCMonitorBasicDb.FirstOrDefault(p => p.TTBH == devid);
        }

        /// <summary>
        /// 增加火情
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public JC_FIRE AddJcFire(JC_FIRE info)
        {
            return this.Insert(info);

        }

        /// <summary>
        /// 主键获取火情信息
        /// </summary>
        /// <param name="jcfid">火情id</param>
        /// <returns></returns>
        public JC_FIRE GetFireInfoByJCFID(string jcfid)
        {
            int id = int.Parse(jcfid);
            return JCFireDb.FirstOrDefault(p => p.JCFID.Equals(id));
        }

        public string GetBussinessStr(string str)
        {
            return str;
        }

        #endregion

    }
}
