using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using AutoMapper;
using Trabalho.Models;
using System.Collections.Generic;

namespace Trabalho.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public ConsultaRepository(IDbConnection dbConnection, IMapper mapper)
        {
            _dbConnection = dbConnection;
            _mapper = mapper;
        }

        public IEnumerable<ConsultaDTO> GetAll()
        {
            var consultas = _dbConnection.GetAll<Consulta>();
            return _mapper.Map<IEnumerable<ConsultaDTO>>(consultas);
        }

        public ConsultaDTO GetById(int id)
        {
            var query = "SELECT * FROM Consultas WHERE Id = @Id";
            var consulta = _dbConnection.QuerySingleOrDefault<ConsultaDTO>(query, new { Id = id });

            if (consulta == null)
            {
                throw new KeyNotFoundException("Consulta não encontrada.");
            }

            return consulta;
        }

        public void Add(ConsultaDTO consultaDTO)
        {
            var consulta = _mapper.Map<Consulta>(consultaDTO);
            _dbConnection.Insert(consulta);
        }

        public void Update(ConsultaDTO consultaDTO)
        {
            var consulta = _mapper.Map<Consulta>(consultaDTO);
            _dbConnection.Update(consulta);
        }

        public void Delete(int id)
        {
            var consulta = _dbConnection.Get<Consulta>(id);
            if (consulta == null)
            {
                throw new KeyNotFoundException("Consulta não encontrada.");
            }
            _dbConnection.Delete(consulta);
        }

        public bool IsMedicoDisponivel(int medicoId, DateTime dataConsulta)
        {
            var consultaExistente = _dbConnection.QuerySingleOrDefault<Consulta>(
                "SELECT * FROM Consultas WHERE MedicoId = @MedicoId AND DataConsulta = @DataConsulta",
                new { MedicoId = medicoId, DataConsulta = dataConsulta });

            return consultaExistente == null;
        }
    }
}
