namespace Catalogo.API.Produtos.CriarProdutos
{
    public record CriarProdutosRequest(string Nome,
        List<string> Categorias,
        string Descricao,
        string ArquivoImagem,
        decimal Preco);

    public record CriarProdutosResponse(Guid id);
    public class CriarProdutosEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/produtos",
                    async (CriarProdutosRequest request, ISender sender) => {
                        var command = request.Adapt<CriarProdutosCommand>();
                        var result = await sender.Send(command);
                        var response = result.Adapt<CriarProdutosResponse>();
                        return Results.Created($"/produtos/{response.id}", response);
                    }
            )
            .WithName("CriarProdutos")
            .Produces<CriarProdutosResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Criar Produt")
            .WithDescription("Criar produto");
        }
    }
}
