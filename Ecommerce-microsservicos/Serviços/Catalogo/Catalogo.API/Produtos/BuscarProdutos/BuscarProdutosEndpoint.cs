namespace Catalogo.API.Produtos.BuscarProdutos
{
    // public record BuscarProdutosRequest();
    public record BuscarProdutosResponse(IEnumerable<Produto> Produtos);
    public class BuscarProdutosEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/produtos", async (ISender sender) => {
                var resultado = await sender.Send(new BuscarProdutosQuery());
                var resposta = resultado.Adapt<BuscarProdutosResponse>();
                return Results.Ok(resposta);
            })
            .WithName("BuscarProdutos")
            .Produces<BuscarProdutosResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Buscar produtos")
            .WithDescription("Buscar produtos")
            ;
        }
    }
}
