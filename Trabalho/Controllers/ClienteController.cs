using Dapper;
using Dapper.Contrib;
using System.Data.SQLite;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Trabalho.Models;
using Trabalho.Services;

namespace Trabalho.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClienteDTO>> GetClientes() => Ok(_clienteService.GetAllClientes());

        [HttpGet("{id}")]
        public ActionResult<ClienteDTO> GetCliente(int id)
        {
            var cliente = _clienteService.GetClienteById(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public ActionResult AddCliente(ClienteDTO cliente)
        {
            _clienteService.AddCliente(cliente);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCliente(int id, ClienteDTO cliente)
        {
            if (id != cliente.Id) return BadRequest();
            _clienteService.UpdateCliente(cliente);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCliente(int id)
        {
            _clienteService.DeleteCliente(id);
            return Ok();
        }
    }

}
