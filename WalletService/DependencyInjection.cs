using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletService.Abstraction;
using WalletService.Common;
using WalletService.Repositories;

namespace WalletService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWalletService(this IServiceCollection services, IConfiguration config)
        {
            Action<DbOptions> dbConfig = opt => config.GetSection("DbOptions").Bind(opt);
            return services.
                Configure(dbConfig)
                .AddScoped<IWalletRepository, WalletRepository>();
        }
    }
}
