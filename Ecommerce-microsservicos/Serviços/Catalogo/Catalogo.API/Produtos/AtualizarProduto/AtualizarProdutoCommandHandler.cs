
namespace Catalogo.API.Produtos.AtualizarProduto
{
    public record AtualizarProdutoComando(
        Guid Id,
        string Nome,
        List<string> Categorias,
        string Descricao,
        string ArquivoImagem,
        decimal Preco) :
    ICommand<AtualizarProdutoResult>;

    public record AtualizarProdutoResult(bool Sucesso);

    public class AtualizarProdutoCommandHandler(IDocumentSession session, ILogger<AtualizarProdutoCommandHandler> logger)
        : ICommandHandler<AtualizarProdutoComando, AtualizarProdutoResult>
    {
        public async Task<AtualizarProdutoResult> Handle(AtualizarProdutoComando request, CancellationToken cancellationToken)
        {
            logger.LogInformation("AtualizarProdutoCommandHandler.Handle chamado com {@Query}", request);

            var produto = await session.LoadAsync<Produto>(request.Id, cancellationToken);

            if(produto is null)
            {
                throw new ProdutoNaoEncontradoException();
            }

            produto.Nome = request.Nome;
            produto.Categorias = request.Categorias;
            produto.Descricao = request.Descricao;
            produto.ArquivoImagem = request.ArquivoImagem;
            produto.Preco = request.Preco;

            session.Update(produto);
            await session.SaveChangesAsync();

            return new AtualizarProdutoResult(true);
        }
    }
}
