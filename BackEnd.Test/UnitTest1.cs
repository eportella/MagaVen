using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace BackEnd.Test
{
    public class UnitTest1
    {
        ServiceProvider ServiceProvider { get; }
        public UnitTest1()
        {
            var services = new ServiceCollection();
            (_ = new Startup(default)).ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }
        [Fact]
        public void TestaIntegridadeNaInjecaoDeDependencia()
        {

            {
                var implementation = ServiceProvider.GetService<Catalogo.Service>();
                Assert.NotNull(implementation);
                Assert.IsType<Catalogo.MongoDb.Service>(implementation);
            }
            {
                var implementation = ServiceProvider.GetService<Cliente.Service>();
                Assert.NotNull(implementation);
                Assert.IsType<Cliente.MongoDb.Service>(implementation);
            }
            {
                var implementation = ServiceProvider.GetService<Armazem.Service>();
                Assert.NotNull(implementation);
                Assert.IsType<Armazem.MongoDb.Service>(implementation);
            }
            {
                var implementation = ServiceProvider.GetService<Produto.Service>();
                Assert.NotNull(implementation);
                Assert.IsType<Produto.MongoDb.Service>(implementation);
            }
            {
                var implementation = ServiceProvider.GetService<SeuCliente.Service>();
                Assert.NotNull(implementation);
                Assert.IsType<SeuCliente.MongoDb.Service>(implementation);
            }
        }
        [Fact]
        public async void TestaCrudEmCatalogo()
        {
            const string NOME = "MOCK_Fake_Test";
            const string NOVO_NOME = "MOCK_Fake_edited_Test";
            var service = ServiceProvider.GetService<Catalogo.Service>();

            Assert.False(service.Read().Any(w => w.Nome == NOME), $"inviável a continuidade com o catálogo {NOME}");

            var catalogo = new Catalogo.Model { Nome = NOME };
            await service.CreateAsync(catalogo);

            catalogo = service.Read().Single(w => w.Nome == NOME);
            catalogo = catalogo with { Nome = NOVO_NOME };
            await service.UpdateAsync(NOME, catalogo);

            Assert.True(service.Read().Any(e => e.Nome == NOME), $"não deveria existir o catalogo {NOME}");
            Assert.True(!service.Read().Any(e => e.Nome == NOVO_NOME), $"deveria existir o catalogo {NOVO_NOME}");

            await service.DeleteAsync();
            Assert.True(service.Read().Any(e => e.Nome == NOME), $"não deveria existir o catalogo {NOME}");
            Assert.True(service.Read().Any(e => e.Nome == NOVO_NOME), $"não deveria existir o catalogo {NOVO_NOME}");
        }
    }
}