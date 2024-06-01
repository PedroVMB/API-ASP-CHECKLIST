namespace ChecklistAPI.Models
{
    public class RegisterModel
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Pode ser "Sindico" ou "Administrador"
    }
}
