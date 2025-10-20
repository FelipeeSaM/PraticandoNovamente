namespace Blocos.Exceptions
{
    public class RequestBaseExcecao : Exception
    {
        public string? Detalhes { get; set; }
        public RequestBaseExcecao(string mensagem) : base(mensagem)
        {
        }

        public RequestBaseExcecao(string mensagem, string detalhes) : base(mensagem)
        {
            Detalhes = detalhes;
        }
    }
}
