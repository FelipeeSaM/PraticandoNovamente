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

    internal class CriarProdutosCommandHandler(IDocumentSession sessão, ILogger<CriarProdutosCommandHandler> logger) : ICommandHandler<CriarProdutosCommand, CriarProdutosResult>
    {
        public async Task<CriarProdutosResult> Handle(CriarProdutosCommand commnad, CancellationToken cancellationToken)
        {
            logger.LogInformation("CriarProdutosCommandHandler.Handle: {@Command}", commnad);

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

    public class CriarProdutoCommandValidator : AbstractValidator<CriarProdutosCommand>
    {
        public CriarProdutoCommandValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome inválido");
            RuleFor(x => x.Categorias).NotEmpty().WithMessage("Categoria inválida");
            RuleFor(x => x.ArquivoImagem).NotEmpty().WithMessage("Arquivo inválido");
            RuleFor(x => x.Preco).GreaterThan(0).WithMessage("Preço precisa ser maior que 0");
        }
    }
}
