using FcmbInterview.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FcmbInterview.Application.Common.Interfaces.Persistence;
using FcmbInterview.Infrastructure.Persistence;
using FcmbInterview.Application.Common.Interfaces.Services;
using FcmbInterview.Infrastructure.Services;

namespace FcmbInterview.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, builder =>
                {
                    builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    builder.EnableRetryOnFailure(maxRetryCount: 5,
                                                     maxRetryDelay: TimeSpan.FromSeconds(10),
                                                     errorNumbersToAdd: null);
                    builder.CommandTimeout(15);
                });

                options.EnableDetailedErrors(detailedErrorsEnabled: true);
                //options.EnableSensitiveDataLogging();
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IReferenceNumberGenerator, ReferenceNumberGenerator>();

            return services;
        }
    }
}
