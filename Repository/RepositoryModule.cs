using Microsoft.Extensions.DependencyInjection;
using Repository.Account.implementation;
using Repository.Account.interfaces;
using Repository.Panic.implementation;
using Repository.Panic.interfaces;
using Repository.Support.implementation;
using Repository.Support.interfaces;

namespace Repository
{
    public class RepositoryModule
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ISupportRepository, SupportRepository>();
            services.AddTransient<IPanicRepository, PanicRepository>();
        }
    }
}
