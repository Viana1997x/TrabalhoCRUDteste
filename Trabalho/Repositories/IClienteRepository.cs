using Dapper;
using Dapper.Contrib;
using System.Data.SQLite;
using AutoMapper;

using Trabalho.Models;

namespace Trabalho.Repositories
{
    public interface IClienteRepository
    {
        IEnumerable<ClienteDTO> GetAll();
        ClienteDTO GetById(int id);
        void Add(ClienteDTO cliente);
        void Update(ClienteDTO cliente);
        void Delete(int id);
    }
}
