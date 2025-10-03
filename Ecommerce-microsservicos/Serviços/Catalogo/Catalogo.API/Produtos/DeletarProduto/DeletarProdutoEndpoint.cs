
using Catalogo.API.Produtos.AtualizarProduto;

namespace Catalogo.API.Produtos.DeletarProduto
{
    public record DeletarProdutoRequest(Guid Id);
    public record DeletarProdutoResponse(bool Sucesso);
    public class DeletarProdutoEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/produtos/excluir/{id:guid}", async (Guid id, ISender sender) => {
                var comando = new DeletarProdutoCommand(id);
                var result = await sender.Send(comando);
                var resposta = result.Adapt<DeletarProdutoResponse>();

                return Results.Ok(resposta);
            })
                .WithName("DeletarProduto")
                .Produces<DeletarProdutoResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Deletar produto")
                .WithDescription("Deletar produto!");
        }
    }
}
