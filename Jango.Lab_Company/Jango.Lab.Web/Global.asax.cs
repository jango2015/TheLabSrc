using Castle.MicroKernel.Registration;
using Jango.Lib.CastleWindsor.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Jango.Lab.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BootStrapper.InstallBootStrapperContainer(Classes.FromThisAssembly(), new List<string>() { "Jango.Lab.Models", "Jango.Lab.Services", "Jango.Lab.Repositories" });
        }


        private void Application_Error(object sender, EventArgs e)
        {
            var lastError = Server.GetLastError();
            if (lastError != null)
            {
                var httpError = lastError as HttpException;
                if (httpError != null)
                {
                    var httpCode = httpError.GetHttpCode().ToString();
                    var httpResutWrapper = new HttpRequestWrapper(System.Web.HttpContext.Current.Request);
                    if (httpResutWrapper.IsAjaxRequest() && httpResutWrapper.HttpMethod.ToLower() == "post")
                    {
                        httpCode = "AjaxPost";
                    }
                }
            }
        }

        protected void Application_End()
        {
            CastleIOC.Container.Dispose();
        }
    }
}
