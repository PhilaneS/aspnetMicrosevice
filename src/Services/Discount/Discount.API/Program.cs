using Discount.API.Extensions;
using Discount.API.Repository;
using Microsoft.OpenApi.Models;

namespace Discount.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Discount.API", Version = "v1" }); });


            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();



            var app = builder.Build();

            app.MigrateDatabase<Program>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                //app.UseSwaggerUI();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Discount.API"));
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}