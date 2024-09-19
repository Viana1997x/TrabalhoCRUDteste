namespace Trabalho.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int MedicoId { get; set; }
        public DateTime DataConsulta { get; set; }
    }

}
