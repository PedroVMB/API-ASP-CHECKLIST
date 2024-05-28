using System.ComponentModel.DataAnnotations;

namespace ChecklistAPI.Models.Dtos
{
    public class TorreDTO
    {
        [Required]
        public decimal Condominio_id { get; set; }
        [Required]
        public int Numero_torre { get; set; }
        [Required]
        public int Quantidade_andares { get; set; }
        [Required]
        public int Quantidade_garagens { get; set; }
        [Required]
        public int Quantidade_salao_de_festas { get; set; }
        [Required]
        public int Quantidade_guaritas { get; set; }
        [Required]
        public int Quantidade_terracos { get; set; }
    }
}
