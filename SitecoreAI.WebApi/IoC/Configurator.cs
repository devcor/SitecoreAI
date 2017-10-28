using SitecoreAI.BusinessRules;
using SitecoreAI.Interfaces.BusinessRules;
using SitecoreAI.Interfaces.DAO;
using SitecoreAI.MongoDB;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace SitecoreAI.WebApi.IoC
{
    public class Configurator
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IContactFacetDAO, ContactFacetDAO>(new HierarchicalLifetimeManager());
            container.RegisterType<IContactFacet, ContactFacet>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}