using System.ComponentModel.DataAnnotations;

namespace ChecklistAPI.Models.Dtos
{
    public class SindicoDTO
    {
        [Required]
        public string Nome { get; set; } = String.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string Cpf { get; set; } = String.Empty;
    }
}
