using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Catalogo
{
    public interface Service
    {
        IQueryable<Model> Read();
        Task CreateAsync(Model model);
        Task UpdateAsync(string nome, Model model);
        Task DeleteAsync();
        Task<bool> EstaNoCliente();
    }
}
