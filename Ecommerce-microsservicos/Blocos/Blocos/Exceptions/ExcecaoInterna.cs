namespace Blocos.Exceptions
{
    public class ExcecaoInterna : Exception
    {
        public string? Detalhes { get; set; }
        public ExcecaoInterna(string mensagem) : base(mensagem)
        {            
        }

        public ExcecaoInterna(string mensagem, string detalhes) : base(mensagem)
        {
            Detalhes = detalhes;
        }
    }
}
