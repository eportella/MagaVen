using System;
using System.Collections.Generic;

namespace BackEnd.Produto
{
    public class Model
    {
        public Guid Id { get; init; }
        public Propriedades.Model Propriedades { get; init; }
        public ICollection<string> Tipos { get; init; }
        public IDictionary<string, string> Variacoes { get; init; }
        public Catalogo.Model Catalogo { get; set; }
    }
}