using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TLW.AH.Application.Interfance;
using TLW.AH.Application.Service;
using TLW.AH.Business.ServiceDal;
using TLW.AH.Business.ServiceIDal;
namespace TLW.Project.ThirdVideoServiceWebApi.Unitys
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));//MVC注入 
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);//WebAPi注入
        }


        /// <summary>
        /// Builds the unity container.
        /// </summary>
        /// <returns></returns>
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            //container.RegisterType<INodeBiz, NodeBiz>(); 
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = HttpContext.Current.Server.MapPath("~/CfgFiles/ConfigUnity.Config") };
            Configuration configuration =
                ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var unitySection = (UnityConfigurationSection)configuration.GetSection("unity");
            container.LoadConfiguration(unitySection);
            //container.RegisterType<ITestIoc, TestIo>();
            return container;
        }
    }

}