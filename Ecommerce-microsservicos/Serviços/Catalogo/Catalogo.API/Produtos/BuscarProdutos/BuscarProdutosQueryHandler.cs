
namespace Catalogo.API.Produtos.BuscarProdutos
{
    public record BuscarProdutosQuery() : IQuery<BuscarProdutosResult>;
    public record BuscarProdutosResult(IEnumerable<Produto> Produtos);

    public class BuscarProdutosQueryHandler(IDocumentSession session)
        : IQueryHandler<BuscarProdutosQuery, BuscarProdutosResult>
    {
        public async Task<BuscarProdutosResult> Handle(BuscarProdutosQuery query, CancellationToken cancellationToken)
        {
            var produtos = await session.Query<Produto>().ToListAsync(cancellationToken);
            return new BuscarProdutosResult(produtos);
        }
    }
}
