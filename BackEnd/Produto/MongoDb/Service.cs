using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Produto.MongoDb
{
    public class Service : Produto.Service
    {
        MongoClient Client { get; }
        string Database { get; }
        public Service(MongoClient client, string database)
        {
            Client = client;
            Database = database;
        }
        public async Task CreateAsync(Lote.Model model)
        {
            await Client
                .GetDatabase(Database)
                .GetCollection<Model>(nameof(Produto))
                .InsertManyAsync(model.Produtos);
        }

        public async Task DeleteAsync(Guid id)
        {
            var sameNames = await Client
                .GetDatabase(Database)
                .GetCollection<Model>(nameof(Produto))
                .DeleteOneAsync(
                    Builders<Model>.Filter.Eq(
                        e => e.Id,
                        id
                    )
                );
        }

        public async IAsyncEnumerable<Model> ReadAsync()
        {
            foreach (var item in await (await Client
                    .GetDatabase(Database)
                    .GetCollection<Model>(nameof(Produto))
                    .FindAsync(Builders<Model>.Filter.Empty)
                )
                .ToListAsync()
            )
                yield return item;

        }

        //public async Task<bool> TemQualquerProduto(string nome)
        //{
        //    return await
        //        (
        //            await Client
        //                .GetDatabase(Database)
        //                .GetCollection<Model>(nameof(Armazem))
        //                .FindAsync(Builders<Model>.Filter.And(
        //                    Builders<Model>.Filter.Eq(e => e.Nome, nome),
        //                    Builders<Model>.Filter.Eq(e => e.Produtos.Any(), true)
        //                )
        //            )
        //        )
        //        .AnyAsync();
        //}
    }
}