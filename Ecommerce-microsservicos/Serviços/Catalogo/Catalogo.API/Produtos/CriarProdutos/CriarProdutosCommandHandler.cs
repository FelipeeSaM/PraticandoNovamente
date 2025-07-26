using Blocos.CQRS;
using Catalogo.API.Modelos;

namespace Catalogo.API.Produtos.CriarProdutos
{

    public record CriarProdutosCommand(
        string Nome,
        List<string> Categorias, 
        string Descricao, 
        string ArquivoImagem,
        decimal Preco
        ) : ICommand<CriarProdutosResult>;

    public record CriarProdutosResult(
        Guid Id
        );

    internal class CriarProdutosCommandHandler : ICommandHandler<CriarProdutosCommand, CriarProdutosResult>
    {
        public async Task<CriarProdutosResult> Handle(CriarProdutosCommand commnad, CancellationToken cancellationToken)
        {
            var produto = new Produto {
                Nome = commnad.Nome,
                Categorias = commnad.Categorias,
                Descricao = commnad.Descricao,
                ArquivoImagem = commnad.ArquivoImagem,
                Preco = commnad.Preco
            };

            return new CriarProdutosResult(Guid.NewGuid());
        }
    }
}
