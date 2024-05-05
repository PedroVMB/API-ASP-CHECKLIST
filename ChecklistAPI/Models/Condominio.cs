using System.ComponentModel.DataAnnotations;

namespace ChecklistAPI.Models
{
    public class Condominio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public int Quantidade_de_torres { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
