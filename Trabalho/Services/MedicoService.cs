using Dapper;
using Dapper.Contrib;
using System.Data.SQLite;
using AutoMapper;
using Trabalho.Models;
using Trabalho.Repositories;

namespace Trabalho.Services
{
    public class MedicoService
    {
        private readonly IMedicoRepository _medicoRepository;

        public MedicoService(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public IEnumerable<MedicoDTO> GetAllMedicos() => _medicoRepository.GetAll();

        public MedicoDTO GetMedicoById(int id) => _medicoRepository.GetById(id);

        public void AddMedico(MedicoDTO medico)
        {
            medico.DataRegistro = DateTime.Now;
            _medicoRepository.Add(medico);
        }

        public void UpdateMedico(MedicoDTO medico) => _medicoRepository.Update(medico);

        public void DeleteMedico(int id) => _medicoRepository.Delete(id);
    }

}
