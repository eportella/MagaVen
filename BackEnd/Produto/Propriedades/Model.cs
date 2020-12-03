namespace BackEnd.Produto.Propriedades
{
    public record Model
    {
        public Nome.Model Nome { get; init; }
        public decimal Preco { get; init; }
        public string Descricao { get; init; }
        public string[] Imagens { get; init; }
    }
}