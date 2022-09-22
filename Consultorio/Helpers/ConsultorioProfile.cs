using AutoMapper;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Consultorio.Helpers
{
    public class ConsultorioProfile : Profile
    {
        public ConsultorioProfile()
        {
            CreateMap<Paciente, PacienteDetalhesDto>()
                .ReverseMap(); // Faz o mapeamento de modo reverso também.
                               // ForMember(dest => dest.Email, opt => opt.Ignore()); // retornará o email null

            CreateMap<ConsultaDto, Consulta>()
                .ForMember(dest => dest.Profissional, opt => opt.Ignore())
                .ForMember(dest => dest.Especialidade, opt => opt.Ignore());

            CreateMap<Consulta, ConsultaDto>()
                .ForMember(dest => dest.Especialidade, opt => opt.MapFrom(src => src.Especialidade.Nome))
                .ForMember(dest => dest.Profissional, opt => opt.MapFrom(src => src.Profissional.Nome));

            CreateMap<PacienteAdicionarDto, Paciente>();
        }
    }
}
