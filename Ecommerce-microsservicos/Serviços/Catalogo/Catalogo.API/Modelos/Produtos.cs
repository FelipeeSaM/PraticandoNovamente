namespace Catalogo.API.Modelos
{
    public class Produtos
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<string> Categorias { get; set; } = new();
        public string Descricao { get; set; }
        public string ArquivoImagem { get; set; }
        public decimal Preco { get; set; }
    }
}
