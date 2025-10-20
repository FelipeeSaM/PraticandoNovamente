namespace Blocos.Exceptions
{
    public class NaoEncontradoExcecao : Exception
    {
        public NaoEncontradoExcecao(string mensagem) : base(mensagem)
        {
        }

        public NaoEncontradoExcecao(string nome, object chave) : base($"Entidade: \"{nome}\" ({chave}) não encontrada") 
        {
            
        }
    }
}
