using Dapper;
using Dapper.Contrib;
using System.Data.SQLite;
using AutoMapper;
using Trabalho.Models;
using Trabalho.Repositories;

namespace Trabalho.Services
{
    public class ConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;

        public ConsultaService(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public IEnumerable<ConsultaDTO> GetAllConsultas() => _consultaRepository.GetAll();

        public ConsultaDTO GetConsultaById(int id) => _consultaRepository.GetById(id);

        public void AddConsulta(ConsultaDTO consulta)
        {
            if (_consultaRepository.IsMedicoDisponivel(consulta.MedicoId, consulta.DataConsulta))
            {
                _consultaRepository.Add(consulta);
            }
            else
            {
                throw new Exception("Médico não disponível nesta data.");
            }
        }

        public void UpdateConsulta(ConsultaDTO consulta) => _consultaRepository.Update(consulta);

        public void DeleteConsulta(int id) => _consultaRepository.Delete(id);
    }

}
