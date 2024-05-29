using ChecklistAPI.Models.Enums;

namespace ChecklistAPI.Models
{
    public class Registro
    {
        public decimal Id { get; set; }
        public decimal CondominioId { get; set; }
        public decimal TorreId { get; set; }
        public string FotoPath { get; set; }
        public DateTime DataDoRegistro { get; set; }
        public string DescricaoProblema { get; set; }
        public ProblemaEnum TipoProblema { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
