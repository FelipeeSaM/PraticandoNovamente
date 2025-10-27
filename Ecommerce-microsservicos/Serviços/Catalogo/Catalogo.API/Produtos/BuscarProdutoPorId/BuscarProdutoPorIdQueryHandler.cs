namespace Catalogo.API.Produtos.BuscarProdutoPorId
{
    public record BuscarProdutoPorIdQuery(Guid Id) : IQuery<BuscarProdutoPorIdResult>;
    public record BuscarProdutoPorIdResult(Produto Produto);

    public class BuscarProdutoPorIdQueryHandler(IDocumentSession session)
        : IQueryHandler<BuscarProdutoPorIdQuery, BuscarProdutoPorIdResult>
    {
        public async Task<BuscarProdutoPorIdResult> Handle(BuscarProdutoPorIdQuery request, CancellationToken cancellationToken)
        {
            var resultado = await session.LoadAsync<Produto>(request.Id, cancellationToken);
            if(resultado is null)
            {
                throw new ProdutoNaoEncontradoException(request.Id);
            }

            return new BuscarProdutoPorIdResult(resultado);
        }
    }
}
