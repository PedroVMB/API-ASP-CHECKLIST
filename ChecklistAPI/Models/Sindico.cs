namespace ChecklistAPI.Models
{
    public class Sindico : ApplicationUser
    {
        public decimal Id { get; set; }
        public string Nome { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Cpf { get; set; } = String.Empty;
    }
}
