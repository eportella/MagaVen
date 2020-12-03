using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Armazem
{
    public interface Service
    {
        IAsyncEnumerable<Model> ReadAsync();
        Task CreateAsync(Model model);
        Task UpdateAsync(string nome, Model model);
        Task DeleteAsync(string nome);
        Task<bool> TemQualquerProduto(string nome);
    }
}