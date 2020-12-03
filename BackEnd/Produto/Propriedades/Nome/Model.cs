namespace BackEnd.Produto.Propriedades.Nome
{
    public class Model
    {
        string Value { get; init; }
        public static implicit operator string(Model model) => model?.Value;
        public static implicit operator Model(string value) => new Model { Value = value };
    }
}
