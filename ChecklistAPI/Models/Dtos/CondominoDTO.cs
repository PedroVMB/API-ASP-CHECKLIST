using System.ComponentModel.DataAnnotations;

namespace ChecklistAPI.Models.Dtos
{
    public class CondominoDTO
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cnpj { get; set; }
        [Required]
        public int Quantidade_de_torres { get; set; }
    }
}
