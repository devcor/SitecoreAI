using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using SitecoreAI.BusinessRules;
using SitecoreAI.Interfaces.BusinessRules;
using SitecoreAI.Interfaces.DAO;
using SitecoreAI.MongoDB;

namespace SitecoreAI.Website.IoC
{
    public class Configurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {            
            serviceCollection.AddScoped(typeof(IContactFacetDAO), typeof(ContactFacetDAO));
            serviceCollection.AddScoped(typeof(IContactFacet), typeof(ContactFacet));
            serviceCollection.AddMvcControllersInCurrentAssembly();
        }
    }
}