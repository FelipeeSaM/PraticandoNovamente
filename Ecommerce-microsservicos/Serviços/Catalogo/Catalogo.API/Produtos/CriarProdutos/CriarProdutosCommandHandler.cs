using MediatR;

namespace Catalogo.API.Produtos.CriarProdutos
{

    public record CriarProdutosCommand(
        string nome,
        List<string> categorias, 
        string descricao, 
        string arquivoImagem,
        decimal preco
        ) : IRequest<CriarProdutosResult>;

    public record CriarProdutosResult(
        Guid id
        );

    internal class CriarProdutosCommandHandler : IRequestHandler<CriarProdutosCommand, CriarProdutosResult>
    {
        public Task<CriarProdutosResult> Handle(CriarProdutosCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
