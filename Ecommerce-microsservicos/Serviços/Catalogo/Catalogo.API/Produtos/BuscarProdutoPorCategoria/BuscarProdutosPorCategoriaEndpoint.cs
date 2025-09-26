
namespace Catalogo.API.Produtos.BuscarProdutoPorCategoria
{
    //public record BuscarProdutosPorCategoriaRequest();
    public record BuscarProdutosPorCategoriaResponse(IEnumerable<Produto> produtos);

    public class BuscarProdutosPorCategoriaEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/produtos/categoria/{categoria}",
                async (string categoria, ISender sender) => {
                    var result = await sender.Send(new BuscarProdutosPorCategoriaQuery(categoria));
                    var response = result.Adapt<BuscarProdutosPorCategoriaResponse>();
                    return Results.Ok(response);
                })
                .WithName("BuscarProdutosPorCategoria")
                .Produces<BuscarProdutosPorCategoriaResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Buscar produtos por categoria")
                .WithDescription("Buscar produtos pro categoria");
        }
    }
}
