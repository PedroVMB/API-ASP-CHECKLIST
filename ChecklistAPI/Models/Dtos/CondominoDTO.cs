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
        public string Bairro { get; set; }
        [Required]
        public string Cep { get; set; }
        [Required]
        public string Complemento { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        public string Uf { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public int Quantidade_de_torres { get; set; }
    }
}
