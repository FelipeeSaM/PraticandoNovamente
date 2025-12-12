using Discount.Grpc.Data;
using Discount.Grpc.Models;
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

        public override async Task<CupomModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var cupom = request.Cupom.Adapt<Cupom>();
            if(cupom == null)
            {
                logger.LogError("Invalid cupom data received.");
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid cupom data."));
            }
            dbContext.Cupons.Add(cupom);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("cupom with ProductName={ProductName} created successfully.", cupom.ProductName);
            var adapt = cupom.Adapt<CupomModel>();
            return adapt;
        }

        public override async Task<CupomModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var cupom = request.Cupom.Adapt<Cupom>();
            if(cupom == null)
            {
                logger.LogError("Invalid cupom data received.");
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid cupom data."));
            }
            dbContext.Cupons.Update(cupom);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("cupom with ProductName={ProductName} updated successfully.", cupom.ProductName);
            var adapt = cupom.Adapt<CupomModel>();
            return adapt;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var cupom = await dbContext.Cupons
                .FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
            if (cupom == null)
            {
                logger.LogError("Invalid cupom data received.");
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount can't be found {request.ProductName}."));
            }
            dbContext.Cupons.Remove(cupom);
            var deleted = await dbContext.SaveChangesAsync();
            logger.LogInformation("cupom with ProductName={ProductName} deleted successfully.", cupom.ProductName);
            return new DeleteDiscountResponse { Success = true };
        }
    }
}
