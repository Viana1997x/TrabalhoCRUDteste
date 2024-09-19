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
    public class MedicoController : ControllerBase
    {
        private readonly MedicoService _medicoService;

        public MedicoController(MedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        // GET: api/Medico
        [HttpGet]
        public ActionResult<IEnumerable<MedicoDTO>> GetMedicos()
        {
            var medicos = _medicoService.GetAllMedicos();
            return Ok(medicos);
        }

        // GET: api/Medico/1
        [HttpGet("{id}")]
        public ActionResult<MedicoDTO> GetMedico(int id)
        {
            var medico = _medicoService.GetMedicoById(id);
            if (medico == null)
                return NotFound();

            return Ok(medico);
        }

        // POST: api/Medico
        [HttpPost]
        public ActionResult AddMedico([FromBody] MedicoDTO medico)
        {
            _medicoService.AddMedico(medico);
            return Ok();
        }

        // PUT: api/Medico/1
        [HttpPut("{id}")]
        public ActionResult UpdateMedico(int id, [FromBody] MedicoDTO medico)
        {
            if (id != medico.Id)
                return BadRequest("ID do médico não corresponde");

            _medicoService.UpdateMedico(medico);
            return Ok();
        }

        // DELETE: api/Medico/1
        [HttpDelete("{id}")]
        public ActionResult DeleteMedico(int id)
        {
            _medicoService.DeleteMedico(id);
            return Ok();
        }
    }
}

