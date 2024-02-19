using FcmbInterview.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Infrastructure.Middlewares
{
    public static class UseInfrastructureMiddleware
    {
        public static IApplicationBuilder useInfrastruactureMiddleware(this IApplicationBuilder app) {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.MigrateAsync().Wait();
                    
                }catch(Exception ex)
                {
                    Console.Out.WriteLineAsync(ex.ToString());
                    throw new Exception("cannot seed data");
                }
            }

             return app;
        
        }
    }
}
