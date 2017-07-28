using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TLW.Project.ThirdVideoServiceWebApi.Unitys;
namespace TLW.Project.ThirdVideoServiceWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            // UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());//设置ioc工厂
            ////启用压缩
            //BundleTable.EnableOptimizations = true; 
            // 注入 Ioc
            // var container = new UnityContainer();
            //DependencyRegisterType.Container_Sys(ref container);
            // container.RegisterType<IControllerFactory, UnityControllerFactory>();
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));//MVC注入
            // GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);//WebAPi注入 
            //注入方法
            Bootstrapper.Initialise();
            // 使api返回为json 
            // GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }


    }
}
