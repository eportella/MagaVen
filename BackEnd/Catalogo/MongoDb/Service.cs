using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Catalogo.MongoDb
{
    public class Service : Catalogo.Service
    {
        MongoClient Client { get; }
        string Database { get; }
        public Service(MongoClient client, string database)
        {
            Client = client;
            Database = database;
        }
        public async Task CreateAsync(Model model) =>
            await Client
                .GetDatabase(Database)
                .GetCollection<Model>(nameof(Catalogo))
                .InsertOneAsync(model);

        public async Task DeleteAsync() =>
            await Client
                .GetDatabase(Database)
                .GetCollection<Model>(nameof(Catalogo))
                .DeleteOneAsync(Builders<Model>.Filter.Empty);

        public IQueryable<Model> Read() =>
            Client
                .GetDatabase(Database)
                .GetCollection<Model>(nameof(Catalogo))
                .AsQueryable();
        

        public async Task UpdateAsync(string nome, Model model)
        {
            await Client
                .GetDatabase(Database)
                .GetCollection<Model>(nameof(Catalogo))
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

        public async Task<bool> EstaNoCliente()
        {
            return await
                (
                    await Client
                        .GetDatabase(Database)
                        .GetCollection<Cliente.Model>(nameof(Cliente))
                        .FindAsync(Builders<Cliente.Model>.Filter.Eq(e => e.Catalogo, null))
                )
                .AnyAsync();
        }
    }
}
