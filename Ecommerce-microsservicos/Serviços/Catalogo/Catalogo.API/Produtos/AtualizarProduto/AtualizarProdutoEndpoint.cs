
namespace Catalogo.API.Produtos.AtualizarProduto
{
    public record AtualizarProdutoRequest(Guid Id,
        string Nome,
        List<string> Categorias,
        string Descricao,
        string ArquivoImagem,
        decimal Preco);

    public record AtualizarProdutoResponse(bool Sucesso);
    public class AtualizarProdutoEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/produtos", async (AtualizarProdutoRequest request, ISender sender) => {
                var comando = request.Adapt<AtualizarProdutoComando>();
                var result = sender.Send(comando);
                var resposta = result.Adapt<AtualizarProdutoResponse>();

                return Results.Ok(resposta);
            })
                .WithName("AtualizarProduto")
                .Produces<AtualizarProdutoResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Atualizar produto")
                .WithDescription("Atualizar produto!");
        }
    }
}
