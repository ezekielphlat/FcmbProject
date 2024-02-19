
using FcmbInterview.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FcmbInterview.Infrastructure;
using FcmbInterview.Infrastructure.Middlewares;

namespace FcmbInterview.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddPresentationService().AddInfrastructureService(builder.Configuration);

            // Add services to the container.

            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
          app.useInfrastruactureMiddleware();
            app.Run();
        }
    }
}