
namespace Catalogo.API.Produtos.BuscarProdutoPorCategoria
{
    public record BuscarProdutosPorCategoriaQuery(string Categoria) : IQuery<BuscarProdutoPorCategoriaResult>;
    public record BuscarProdutoPorCategoriaResult(IEnumerable<Produto> Produtos);

    internal class BuscarProdutosPorCategoriaQueryHandler
        (IDocumentSession session, ILogger<BuscarProdutosPorCategoriaQueryHandler> logger)
        :
        IQueryHandler<BuscarProdutosPorCategoriaQuery, BuscarProdutoPorCategoriaResult>
    {
        public async Task<BuscarProdutoPorCategoriaResult> Handle(BuscarProdutosPorCategoriaQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("BuscarProdutoPorCategoriaQueryHandler.Handle chamado com {@Query}", query);

            var produtos = await session.Query<Produto>()
                .Where(c => c.Categorias.Contains(query.Categoria))
                .ToListAsync();

            return new BuscarProdutoPorCategoriaResult(produtos);
        }
    }
}
