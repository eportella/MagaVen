using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Armazem.MongoDb
{
    public class Service : Armazem.Service
    {
        MongoClient Client { get; }
        string Database { get; }
        public Service(MongoClient client, string database)
        {
            Client = client;
            Database = database;
        }
        public async Task CreateAsync(Model model)
        {
            await Client
                .GetDatabase(Database)
                .GetCollection<Model>(nameof(Armazem))
                .InsertOneAsync(model);
        }

        public async Task DeleteAsync(string nome)
        {
            await Client
                .GetDatabase(Database)
                .GetCollection<Model>(nameof(Armazem))
                .DeleteOneAsync(
                    Builders<Model>.Filter.Eq(
                        nameof(Model.Nome),
                        nome
                    )
                );
        }

        public async IAsyncEnumerable<Model> ReadAsync()
        {
            foreach (var item in await (await Client
                    .GetDatabase(Database)
                    .GetCollection<Model>(nameof(Armazem))
                    .FindAsync(Builders<Model>.Filter.Empty)
                )
                .ToListAsync()
            )
                yield return item;

        }

        public async Task UpdateAsync(string nome, Model model)
        {
            await Client
                .GetDatabase(Database)
                .GetCollection<Model>(nameof(Armazem))
                .UpdateOneAsync(
                    Builders<Model>.Filter.Eq(
                        nameof(Model.Nome),
                        nome
                    ),
                    Builders<Model>.Update.Set(
                        nameof(Model.Nome),
                        model.Nome
                    )
                );
        }

        public async Task<bool> TemQualquerProduto(string nome)
        {
            return await
                (
                    await Client
                        .GetDatabase(Database)
                        .GetCollection<Model>(nameof(Armazem))
                        .FindAsync(Builders<Model>.Filter.And(
                            Builders<Model>.Filter.Eq(e => e.Nome, nome),
                            Builders<Model>.Filter.Eq(e => e.Produtos.Any(), true)
                        )
                    )
                )
                .AnyAsync();
        }
    }
}