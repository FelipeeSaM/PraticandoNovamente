using Blocos.Exceptions;

namespace Catalogo.API.Exceptions
{
    public class ProdutoNaoEncontradoException : NotFoundException
    {
        public ProdutoNaoEncontradoException(Guid id) : base("Produto", id)
        {
        }
    }
}
