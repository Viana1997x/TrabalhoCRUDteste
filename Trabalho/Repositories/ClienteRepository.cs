using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using AutoMapper;
using Trabalho.Models;
using System.Collections.Generic;

namespace Trabalho.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public ClienteRepository(IDbConnection dbConnection, IMapper mapper)
        {
            _dbConnection = dbConnection;
            _mapper = mapper;
        }

        public IEnumerable<ClienteDTO> GetAll()
        {
            var clientes = _dbConnection.GetAll<Cliente>();
            return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
        }

        public ClienteDTO GetById(int id)
        {
            var query = "SELECT * FROM Clientes WHERE Id = @Id";
            var cliente = _dbConnection.QuerySingleOrDefault<ClienteDTO>(query, new { Id = id });

            if (cliente == null)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }

            return cliente;
        }

        public void Add(ClienteDTO clienteDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            _dbConnection.Insert(cliente);
        }

        public void Update(ClienteDTO clienteDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            _dbConnection.Update(cliente);
        }

        public void Delete(int id)
        {
            var query = "DELETE FROM Clientes WHERE Id = @Id";
            var rowsAffected = _dbConnection.Execute(query, new { Id = id });

            if (rowsAffected == 0)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }
        }
    }
}