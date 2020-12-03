using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.SeuCliente.MongoDb
{
    public class Service : SeuCliente.Service
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
                .GetCollection<Model>(nameof(SeuCliente))
                .InsertOneAsync(model);
        }

        public async Task DeleteAsync(string nome)
        {
            await Client
                .GetDatabase(Database)
                .GetCollection<Model>(nameof(SeuCliente))
                .DeleteOneAsync(Builders<Model>.Filter.Eq
                    (
                        e => e.Nome,
                        nome
                    ));
        }

        public async IAsyncEnumerable<Model> ReadAsync()
        {
            foreach (var item in await (await Client
                    .GetDatabase(Database)
                    .GetCollection<Model>(nameof(SeuCliente))
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
                .GetCollection<Model>(nameof(SeuCliente))
                .UpdateOneAsync(
                    Builders<Model>.Filter.Eq(
                        e => e.Nome,
                        nome
                    ),
                    Builders<Model>.Update.Set(
                        e => e.Nome,
                        model.Nome
                    )
                );
        }
    }
}