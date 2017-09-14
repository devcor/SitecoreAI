using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using SitecoreAI.MongoDB;

namespace SitecoreAI.Website.IoC
{
    public class Configurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {            
            serviceCollection.AddScoped(typeof(IContacts), typeof(ContactDAO));            
            serviceCollection.AddMvcControllersInCurrentAssembly();
        }
    }
}