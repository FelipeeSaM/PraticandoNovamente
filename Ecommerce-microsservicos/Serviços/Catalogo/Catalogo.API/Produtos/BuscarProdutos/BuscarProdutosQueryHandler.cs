namespace Catalogo.API.Produtos.BuscarProdutos
{
    public record BuscarProdutosQuery(int? NumeroPagina = 1, int? TamanhoPagina = 10) : IQuery<BuscarProdutosResult>;
    public record BuscarProdutosResult(IEnumerable<Produto> Produtos);

    public class BuscarProdutosQueryHandler(IDocumentSession session)
        : IQueryHandler<BuscarProdutosQuery, BuscarProdutosResult>
    {
        public async Task<BuscarProdutosResult> Handle(BuscarProdutosQuery query, CancellationToken cancellationToken)
        {
            var produtos = await session.Query<Produto>().ToPagedListAsync(query.NumeroPagina ?? 1, query.TamanhoPagina ?? 10, cancellationToken);
            return new BuscarProdutosResult(produtos);
        }
    }
}
