using ChecklistAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChecklistAPI.Models.Dtos
{
    public class RegistroDTO
    {
        [Required]
        public decimal CondominioId { get; set; }
        [Required]
        public decimal TorreId { get; set; }
        [Required]
        public string FotoPath { get; set; }
        [Required]
        public DateTime DataDoRegistro { get; set; }
        [Required]
        public string DescricaoProblema { get; set; }
        [Required]
        public ProblemaEnum TipoProblema { get; set; }
    }
}
