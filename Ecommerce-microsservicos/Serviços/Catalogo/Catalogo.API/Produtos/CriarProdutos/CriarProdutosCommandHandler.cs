using Blocos.CQRS;
using Catalogo.API.Modelos;

namespace Catalogo.API.Produtos.CriarProdutos
{

    public record CriarProdutosCommand(
        string nome,
        List<string> categorias, 
        string descricao, 
        string arquivoImagem,
        decimal preco
        ) : ICommand<CriarProdutosResult>;

    public record CriarProdutosResult(
        Guid id
        );

    internal class CriarProdutosCommandHandler : ICommandHandler<CriarProdutosCommand, CriarProdutosResult>
    {
        public async Task<CriarProdutosResult> Handle(CriarProdutosCommand commnad, CancellationToken cancellationToken)
        {
            var produto = new Produto {
                Nome = commnad.nome,
                Categorias = commnad.categorias,
                Descricao = commnad.descricao,
                ArquivoImagem = commnad.arquivoImagem,
                Preco = commnad.preco
            };

            return new CriarProdutosResult(Guid.NewGuid());
        }
    }
}
