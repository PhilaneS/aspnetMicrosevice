using EventBus.Messages.Common;
using MassTransit;
using Microsoft.OpenApi.Models;
using Ordering.API.EventBusConsumer;
using Ordering.API.Extensions;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;

namespace Ordering.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddMassTransit(config => {

                config.AddConsumer<BasketCheckoutConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
                    {
                        c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
                    });
                });
            });

            builder.Services.AddScoped<BasketCheckoutConsumer>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering.API", Version = "v1" });
            });

            var app = builder.Build();

            app.MigrateDatabase<OrderContext>((context,service) => 
            {
                var logger = service.GetRequiredService<ILogger<OrderContextSeed>>();
                OrderContextSeed.SeedAsync(context, logger).Wait();
            });



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering.API"));
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}