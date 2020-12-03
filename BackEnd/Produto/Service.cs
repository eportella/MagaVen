using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Produto
{
    public interface Service
    {
        IAsyncEnumerable<Model> ReadAsync();
        Task CreateAsync(Lote.Model model);
        Task DeleteAsync(Guid id);
    }
}