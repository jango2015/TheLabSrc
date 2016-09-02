using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.SubSystems.Configuration;
using Jango.Lib.CastleWindsor.MVC;

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

            var controllerFactory = new CastleControllerFactory(CastleContainer.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }

    public class BusinessInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .Where(x => x.Name.EndsWith("Controller"))
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

            //container.Register(Classes.FromAssemblyNamed("Jango.Lab.Repositories")
            //.InNamespace("Jango.Lab.Repositories.Core", true)
            //.LifestyleTransient()
            //);

        }
    }


}