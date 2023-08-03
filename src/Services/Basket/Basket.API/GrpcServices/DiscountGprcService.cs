using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGprcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGprcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoService = discountProtoServiceClient ?? throw new ArgumentNullException(nameof(discountProtoServiceClient));
        }
        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}
