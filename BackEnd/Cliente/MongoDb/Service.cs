using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Cliente.MongoDb
{
    public class Service : Cliente.Service
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
                .GetCollection<Model>(nameof(Cliente))
                .InsertOneAsync(model);
        }

        public async Task DeleteAsync()
        {
            await Client
                .GetDatabase(Database)
                .GetCollection<Model>(nameof(Cliente))
                .DeleteOneAsync(Builders<Model>.Filter.Empty);
        }

        public async IAsyncEnumerable<Model> ReadAsync()
        {
            foreach (var item in await (await Client
                    .GetDatabase(Database)
                    .GetCollection<Model>(nameof(Cliente))
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
                .GetCollection<Model>(nameof(Cliente))
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
    }
}