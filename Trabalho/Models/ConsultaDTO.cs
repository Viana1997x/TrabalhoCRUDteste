using Dapper;
using Dapper.Contrib;
using System.Data.SQLite;
using AutoMapper;

namespace Trabalho.Models
{
    public class ConsultaDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int MedicoId { get; set; }
        public DateTime DataConsulta { get; set; }
    }

}
