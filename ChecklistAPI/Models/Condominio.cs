using System.ComponentModel.DataAnnotations;

namespace ChecklistAPI.Models
{
    public class Condominio
    {
        public decimal Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Uf {  get; set; }
        public string Cidade { get; set; }
        public int Quantidade_de_torres { get; set; }
    }
}
