using Blocos.Exceptions;

namespace Catalogo.API.Exceptions
{
    public class ProdutoNaoEncontradoException : NaoEncontradoExcecao
    {
        public ProdutoNaoEncontradoException(Guid id) : base("Produto", id)
        {
        }
    }
}
