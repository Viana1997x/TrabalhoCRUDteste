using Dapper;
using Dapper.Contrib;
using System.Data.SQLite;
using AutoMapper;

using Trabalho.Models;

namespace Trabalho.Repositories
{
    public interface IConsultaRepository
    {
        IEnumerable<ConsultaDTO> GetAll();
        ConsultaDTO GetById(int id);
        void Add(ConsultaDTO consulta);
        void Update(ConsultaDTO consulta);
        void Delete(int id);
        bool IsMedicoDisponivel(int medicoId, DateTime dataConsulta);
    }

}
