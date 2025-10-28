namespace Catalogo.API.Produtos.BuscarProdutos
{
    public record BuscarProdutosRequest(int? NumeroPagina = 1, int? TamanhoPagina = 10);
    public record BuscarProdutosResponse(IEnumerable<Produto> Produtos);
    public class BuscarProdutosEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/produtos", async ([AsParameters] BuscarProdutosRequest request, ISender sender) => {
                var query = request.Adapt<BuscarProdutosQuery>();
                var resultado = await sender.Send(query);
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
