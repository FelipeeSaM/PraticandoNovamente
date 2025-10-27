
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

    internal class AtualizarProdutoCommandHandler(IDocumentSession session)
        : ICommandHandler<AtualizarProdutoComando, AtualizarProdutoResult>
    {
        public async Task<AtualizarProdutoResult> Handle(AtualizarProdutoComando request, CancellationToken cancellationToken)
        {
            var produto = await session.LoadAsync<Produto>(request.Id, cancellationToken);

            if(produto is null)
            {
                throw new ProdutoNaoEncontradoException(request.Id);
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

    public class AtualizarProdutoComandoValidator : AbstractValidator<AtualizarProdutoComando>
    {
        public AtualizarProdutoComandoValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id necessário");
            RuleFor(c => c.Nome).NotNull();
            RuleFor(c => c.Categorias);
            RuleFor(c => c.Preco).GreaterThanOrEqualTo(0);
        }
    }
}
