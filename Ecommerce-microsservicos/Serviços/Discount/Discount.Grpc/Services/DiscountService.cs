using Discount.Grpc.Data;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService 
        (DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CupomModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var cupom = await dbContext.Cupons
                .FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

            if (cupom == null)
                {
                logger.LogError("cupom with ProductName={ProductName} not found.", request.ProductName);
                return new CupomModel { Id = 0, ProductName = "", Description = "No Discount", Amount = 0.0 };
            }

            logger.LogInformation("cupom with ProductName={ProductName} retrieved successfully.", request.ProductName);

            var adapt = cupom.Adapt<CupomModel>();
            return adapt;
        }

        public override Task<CupomModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            return base.CreateDiscount(request, context);
        }

        public override Task<CupomModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            return base.UpdateDiscount(request, context);
        }

        public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            return base.DeleteDiscount(request, context);
        }
    }
}
