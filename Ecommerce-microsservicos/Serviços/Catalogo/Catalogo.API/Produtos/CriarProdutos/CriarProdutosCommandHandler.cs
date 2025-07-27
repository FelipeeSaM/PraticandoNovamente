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

    internal class CriarProdutosCommandHandler(IDocumentSession sessão) : ICommandHandler<CriarProdutosCommand, CriarProdutosResult>
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

            sessão.Store(produto);
            await sessão.SaveChangesAsync(cancellationToken);

            return new CriarProdutosResult(produto.Id);
        }
    }
}
