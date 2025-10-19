
namespace Catalogo.API.Produtos.DeletarProduto
{
    public record DeletarProdutoCommand(Guid Id) : ICommand<DeletarProdutoResult>;
    public record DeletarProdutoResult(bool Sucesso);
    internal class DeletarProdutoCommandHandler(IDocumentSession session, ILogger<DeletarProdutoCommandHandler> logger)
        : ICommandHandler<DeletarProdutoCommand, DeletarProdutoResult>
    {
        public async Task<DeletarProdutoResult> Handle(DeletarProdutoCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeletarProdutoCommandHandler.Handle chamado com {@Query}", request);
            var produto = await session.LoadAsync<Produto>(request.Id, cancellationToken);

            if(produto is null)
            {
                throw new ProdutoNaoEncontradoException();
            }

            session.Delete(produto);
            await session.SaveChangesAsync();

            return new DeletarProdutoResult(true);
        }
    }

    public class DeletarProdutoCommandValidator : AbstractValidator<DeletarProdutoCommand> {
        public DeletarProdutoCommandValidator()
        {
            RuleFor(c => c.Id).NotNull().WithMessage("Id é necessário");
        }
    }
}
