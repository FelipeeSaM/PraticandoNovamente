namespace Catalogo.API.Produtos.BuscarProdutoPorId
{
    public record BuscarProdutoPorIdQuery(Guid Id) : IQuery<BuscarProdutoPorIdResult>;
    public record BuscarProdutoPorIdResult(Produto Produto);

    public class BuscarProdutoPorIdQueryHandler(IDocumentSession session, ILogger<BuscarProdutoPorIdQueryHandler> logger)
        : IQueryHandler<BuscarProdutoPorIdQuery, BuscarProdutoPorIdResult>
    {
        public async Task<BuscarProdutoPorIdResult> Handle(BuscarProdutoPorIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("BuscarProdutoPorIdQueryHandler.Handle chamado com a query {@request}", request);
            var resultado = await session.LoadAsync<Produto>(request.Id, cancellationToken);
            if(resultado is null)
            {
                throw new ProdutoNaoEncontradoException();
            }

            return new BuscarProdutoPorIdResult(resultado);
        }
    }
}
