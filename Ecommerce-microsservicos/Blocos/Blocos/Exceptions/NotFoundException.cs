namespace Blocos.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string mensagem) : base(mensagem)
        {
        }

        public NotFoundException(string nome, object chave) : base($"Entidade: \"{nome}\" ({chave}) não encontrada") 
        {
            
        }
    }
}
