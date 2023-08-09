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
                new Order()
                {
                    UserName = "swn",
                    FirstName = "Philane",
                    LastName = "Sigwebela",
                    EmailAddress = "kizer.p@gmail.com",
                    AddressLine = "5611 Manqele St",
                    Country = "South Africa",
                    State = "KwaZulu Natal",
                    ZipCode = "4001",
                    TotalPrice=50,
                     CardName="Philane Sigwebela",
                     CardNumber="1234567891234567",
                     Expiration="12/24",
                     CVV="123",
                     PaymentMethod=1
                }
            };
        }
    }
}
