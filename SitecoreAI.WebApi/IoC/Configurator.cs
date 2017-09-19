using Microsoft.Practices.Unity;
using SitecoreAI.BusinessRules;
using SitecoreAI.Interfaces.BusinessRules;
using SitecoreAI.Interfaces.DAO;
using SitecoreAI.MongoDB;
using System.Web.Http;

namespace SitecoreAI.WebApi.IoC
{
    public class Configurator
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IContactDAO, ContactDAO>(new HierarchicalLifetimeManager());
            container.RegisterType<IContact, Contact>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}