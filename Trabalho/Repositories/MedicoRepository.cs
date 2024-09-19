using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using AutoMapper;
using Trabalho.Models;
using System.Collections.Generic;

namespace Trabalho.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public MedicoRepository(IDbConnection dbConnection, IMapper mapper)
        {
            _dbConnection = dbConnection;
            _mapper = mapper;
        }

        public IEnumerable<MedicoDTO> GetAll()
        {
            var medicos = _dbConnection.GetAll<Medico>();
            return _mapper.Map<IEnumerable<MedicoDTO>>(medicos);
        }

        public MedicoDTO GetById(int id)
        {
            var query = "SELECT * FROM Medicos WHERE Id = @Id";
            var medico = _dbConnection.QuerySingleOrDefault<MedicoDTO>(query, new { Id = id });

            if (medico == null)
            {
                throw new KeyNotFoundException("Médico não encontrado.");
            }

            return medico;
        }

        public void Add(MedicoDTO medicoDTO)
        {
            var medico = _mapper.Map<Medico>(medicoDTO);
            _dbConnection.Insert(medico);
        }

        public void Update(MedicoDTO medicoDTO)
        {
            var medico = _mapper.Map<Medico>(medicoDTO);
            _dbConnection.Update(medico);
        }

        public void Delete(int id)
        {
            var medico = _dbConnection.Get<Medico>(id);
            if (medico == null)
            {
                throw new KeyNotFoundException("Medico não encontrado.");
            }
            _dbConnection.Delete(medico);
        }
    }
}
