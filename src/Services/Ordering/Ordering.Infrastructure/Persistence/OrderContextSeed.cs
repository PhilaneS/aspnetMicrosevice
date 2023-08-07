using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, Microsoft.Extensions.Logging.ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static Order[] GetPreconfiguredOrders()
        {
            return new Order[]
            {
                new Order() {UserName = "swn", FirstName = "Philane", LastName = "Sigwebela", EmailAddress = "", TotalPrice=20}
            };
        }
    }
}
