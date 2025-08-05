
namespace Catalogo.API.Produtos.BuscarProdutoPorId
{
    //public record BuscarProdutoPorIdRequest();
    public record BuscarProdutoPorIdResponse(Produto produto);

    public class BuscarProdutoPorIdEndpoint : ICarterModule
    {
        public async void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/produtos/{id}", async (Guid id, ISender sender) => {
                var resultado = await sender.Send(new BuscarProdutoPorIdQuery(id));
                var response = resultado.Adapt<BuscarProdutoPorIdResponse>();
                return Results.Ok(response);
            })
            .WithName("Buscar produto por id")
            .Produces<BuscarProdutoPorIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Buscar produto por id sumário")
            .WithDescription("Buscar produto por id descricao");
        }
    }
}
