using Dapper;
using Dapper.Contrib;
using System.Data.SQLite;
using AutoMapper;

using Trabalho.Models;

namespace Trabalho.Repositories
{
    public interface IMedicoRepository
    {
        IEnumerable<MedicoDTO> GetAll();
        MedicoDTO GetById(int id);
        void Add(MedicoDTO medico);
        void Update(MedicoDTO medico);
        void Delete(int id);
    }

}
