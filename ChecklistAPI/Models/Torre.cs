namespace ChecklistAPI.Models
{
    public class Torre
    {
        public decimal Id { get; set; }
        public decimal Condominio_id { get; set; }
        public int Numero_torre { get; set; }
        public int Quantidade_andares { get; set; }
        public int Quantidade_garagens { get; set; }
        public int Quantidade_salao_de_festas { get; set; }
        public int Quantidade_guaritas { get; set; }
        public int Quantidade_terracos { get; set; }

    }
}
