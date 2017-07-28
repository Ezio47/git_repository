using Microsoft.Practices.Unity;
using TLW.AH.Application.Interfance;
using TLW.AH.Application.Service;
using TLW.AH.Business.ServiceDal;
using TLW.AH.Business.ServiceIDal;

namespace TLW.Project.ThirdVideoServiceWebApi.Unitys
{
    public class DependencyRegisterType
    {
        //系统注入
        public static void Container_Sys(ref UnityContainer container)
        {
            container.RegisterType<IVideoOriginalService, VideoOriginalService>();
            container.RegisterType<IVideoOriginalApplicationService, VideoOriginalApplicationService>();
            container.RegisterType<IJCFireApplicationService, JCFireApplicationService>();
            container.RegisterType<IJCFireService, JCFireService>();//样例
            //container.RegisterType<IBaseService, BaseService>();
        }

    }
}