using System.Collections.Generic;

namespace BackEnd.Armazem
{
    public class Model
    {
        public string Nome { get; init; }
        public ICollection<Produto.Propriedades.Nome.Model> Produtos { get; init; }
    }
}