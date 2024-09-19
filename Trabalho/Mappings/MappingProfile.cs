using Dapper;
using Dapper.Contrib;
using System.Data.SQLite;
using AutoMapper;
using Trabalho.Models;

namespace Trabalho.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClienteDTO, Cliente>();
            CreateMap<MedicoDTO, Medico>();
            CreateMap<ConsultaDTO, Consulta>();
        }
    }
}
