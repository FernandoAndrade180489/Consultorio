using AutoMapper;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using Consultorio.Repository.Interfaces;
using Consultorio.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase    
    {
        private readonly IPacienteRepository _repository;

        public PacienteController(IPacienteRepository repository)   
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pacientes = await _repository.GetPacientesAsync();

            List<PacienteDto> pacientesRetorno = new List<PacienteDto>();

            foreach (var paciente in pacientes)
            {
                pacientesRetorno.Add(new PacienteDto { Id = paciente.Id, Nome = paciente.Nome });
            }

            return pacientesRetorno.Any() 
                ? Ok(pacientesRetorno)
                : BadRequest("Paciente não encontrado.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var paciente = await _repository.GetPacientesByIdAsync(id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Paciente, PacienteDetalhesDto>());

            var mapper = new Mapper(config);

            var pacienteRetorno = mapper.Map<PacienteDetalhesDto>(paciente);

            return pacienteRetorno != null
                ? Ok(pacienteRetorno)
                : BadRequest("Paciente não encontrado.");
        }

    }
}
