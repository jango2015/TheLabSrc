using Castle.MicroKernel.Registration;
using Jango.Lib.CastleWindsor.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Jango.Lab.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsHanddler());
            BootStrapper.InstallBootStrapperContainer(Classes.FromThisAssembly(), new List<string>() { "Jango.Lab.Models", "Jango.Lab.Services", "Jango.Lab.Repositories" });
        }
    }
}
