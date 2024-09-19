using Dapper;
using Dapper.Contrib;
using System.Data.SQLite;
using AutoMapper;
using Trabalho.Models;
using Trabalho.Repositories;

namespace Trabalho.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IEnumerable<ClienteDTO> GetAllClientes() => _clienteRepository.GetAll();

        public ClienteDTO GetClienteById(int id) => _clienteRepository.GetById(id);

        public void AddCliente(ClienteDTO cliente)
        {
            cliente.DataRegistro = DateTime.Now;
            _clienteRepository.Add(cliente);
        }

        public void UpdateCliente(ClienteDTO cliente) => _clienteRepository.Update(cliente);

        public void DeleteCliente(int id) => _clienteRepository.Delete(id);
    }

}
