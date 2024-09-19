using AutoMapper;
using Trabalho.Models;

namespace Trabalho
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento entre Cliente e ClienteDTO
            CreateMap<Cliente, ClienteDTO>().ReverseMap();

            // Mapeamento entre Medico e MedicoDTO
            CreateMap<Medico, MedicoDTO>().ReverseMap();

            // Mapeamento entre Consulta e ConsultaDTO
            CreateMap<Consulta, ConsultaDTO>().ReverseMap();
        }
    }
}
