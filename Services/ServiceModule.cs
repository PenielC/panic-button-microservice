using Microsoft.Extensions.DependencyInjection;
using Services.Account.implementation;
using Services.Account.interfaces;
using Services.Panic.implementation;
using Services.Panic.interfaces;
using Services.Support.implementation;
using Services.Support.interfaces;

namespace Services
{
    public class ServiceModule
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ISupportService, SupportService>();
            services.AddTransient<IPanicService, PanicService>();
        }
    }
}
