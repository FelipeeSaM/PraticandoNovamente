namespace Catalogo.API.Produtos.CriarProdutos
{
    public record CriarProdutosRequest(string nome,
        List<string> categorias,
        string descricao,
        string arquivoImagem,
        decimal preco);

    public record CriarProdutosResponse(Guid id);
    public class CriarProdutosEndpoint
    {
    }
}
