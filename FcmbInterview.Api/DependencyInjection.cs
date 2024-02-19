using System.Reflection;

namespace FcmbInterview.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationService(this IServiceCollection services)
        {

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {

                //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
               // options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            return services;
        }
    }
}
