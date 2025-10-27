
namespace Catalogo.API.Produtos.DeletarProduto
{
    public record DeletarProdutoCommand(Guid Id) : ICommand<DeletarProdutoResult>;
    public record DeletarProdutoResult(bool Sucesso);
    internal class DeletarProdutoCommandHandler(IDocumentSession session)
        : ICommandHandler<DeletarProdutoCommand, DeletarProdutoResult>
    {
        public async Task<DeletarProdutoResult> Handle(DeletarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = await session.LoadAsync<Produto>(request.Id, cancellationToken);

            if(produto is null)
            {
                throw new ProdutoNaoEncontradoException(request.Id);
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
