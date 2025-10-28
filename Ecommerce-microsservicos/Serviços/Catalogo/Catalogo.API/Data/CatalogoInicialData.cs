using Marten.Schema;

namespace Catalogo.API.Data
{
    public class CatalogoInicialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if(await session.Query<Produto>().AnyAsync())
            {
                return;
            }

            session.Store<Produto>(GetPreconfiguredProducts());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Produto> GetPreconfiguredProducts() => new List<Produto>
        {
            new Produto() {
                Id = new Guid("80931625-e42e-4a18-a329-df1f13e17646"),
                Nome = "Ifome x",
                Descricao = "Celular muito caro",
                ArquivoImagem = "produto-1.png",
                Preco = 950,
                Categorias = new List<string> { "Smart fome" }
            },

            new Produto() {
                Id = new Guid("168e5350-0feb-4df3-a333-650d8a1dd278"),
                Nome = "Ifome 12",
                Descricao = "Celular muito 2",
                ArquivoImagem = "produto-2.png",
                Preco = 950,
                Categorias = new List<string> { "Smart fome2" }
            }
        };
    }
}
