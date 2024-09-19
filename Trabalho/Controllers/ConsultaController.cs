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
    public class ConsultaController : ControllerBase
    {
        private readonly ConsultaService _consultaService;

        public ConsultaController(ConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        // GET: api/Consulta
        [HttpGet]
        public ActionResult<IEnumerable<ConsultaDTO>> GetConsultas()
        {
            var consultas = _consultaService.GetAllConsultas();
            return Ok(consultas);
        }

        // GET: api/Consulta/1
        [HttpGet("{id}")]
        public ActionResult<ConsultaDTO> GetConsulta(int id)
        {
            var consulta = _consultaService.GetConsultaById(id);
            if (consulta == null)
                return NotFound();

            return Ok(consulta);
        }

        // POST: api/Consulta
        [HttpPost]
        public ActionResult AddConsulta([FromBody] ConsultaDTO consulta)
        {
            try
            {
                _consultaService.AddConsulta(consulta);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Consulta/1
        [HttpPut("{id}")]
        public ActionResult UpdateConsulta(int id, [FromBody] ConsultaDTO consulta)
        {
            if (id != consulta.Id)
                return BadRequest("ID da consulta não corresponde");

            _consultaService.UpdateConsulta(consulta);
            return Ok();
        }

        // DELETE: api/Consulta/1
        [HttpDelete("{id}")]
        public ActionResult DeleteConsulta(int id)
        {
            _consultaService.DeleteConsulta(id);
            return Ok();
        }
    }
}
