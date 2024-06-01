using Microsoft.AspNetCore.Identity;

namespace ChecklistAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
    }
}
