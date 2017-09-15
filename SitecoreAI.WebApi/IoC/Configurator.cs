using Microsoft.Practices.Unity;
using SitecoreAI.MongoDB;
using System.Web.Http;

namespace SitecoreAI.WebApi.IoC
{
    public class Configurator
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IContacts, ContactDAO>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}