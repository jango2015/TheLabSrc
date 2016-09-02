using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.MicroKernel.SubSystems.Configuration;
using System.Web.Http.Controllers;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Jango.Lib.CastleWindsor.WebAPI;

namespace Jango.Lab.Web.Infrastructure
{
    public class BootStrapper
    {

        public static IWindsorContainer CastleContainer
        {
            get { return CastleIOC.Container; }
        }

        public static void InstallBootStrapperContainer()
        {
            CastleContainer.Install(FromAssembly.This());

            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator), new WindsorAPIActivator(CastleContainer)
                );
        }
    }

    public class BusinessInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(Classes.FromThisAssembly()
                .BasedOn<IHttpController>()
                .LifestyleTransient()
                );
            container.Register(Classes.FromAssemblyNamed("Jango.Lab.Services")
           .Where(type => type.Name.EndsWith("Service"))
           .LifestylePerWebRequest()
           .WithService.AllInterfaces());
            container.Register(Classes.FromAssemblyNamed("Jango.Lab.Repositories")
            .InNamespace("Jango.Lab.Repositories.Lab", true)
             .LifestylePerWebRequest()
             .WithService.AllInterfaces()
             );
            container.Register(Classes.FromAssemblyNamed("Jango.Lab.Repositories")
                .InNamespace("Jango.Lab.Repositories.Core")
             .LifestylePerWebRequest()
             .WithService.AllInterfaces()
            );
            container.Register(Classes.FromAssemblyNamed("Jango.Lab.Repositories")
             .InNamespace("Jango.Lab.Repositories", true)
             .LifestylePerWebRequest()
             .WithService.AllInterfaces());
            //Repository Inject
            container.Register(Classes.FromAssemblyNamed("Jango.Lab.Models")
            .InNamespace("Jango.Lab.Models", true)
            .LifestyleTransient()
            );

        }
    }


}