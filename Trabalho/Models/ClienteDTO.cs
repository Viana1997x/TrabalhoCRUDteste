using Dapper;
using Dapper.Contrib;
using System.Data.SQLite;
using AutoMapper;

namespace Trabalho.Models
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public DateTime DataRegistro { get; set; }
    }

}
