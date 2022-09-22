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
        private readonly IMapper _mapper;

        public PacienteController(IPacienteRepository repository, IMapper mapper)   
        {
            _repository = repository;
            _mapper = mapper;
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

            var pacienteRetorno = _mapper.Map<PacienteDetalhesDto>(paciente);

            // Mapeamento reverso
            var pacienteTeste = _mapper.Map<Paciente>(pacienteRetorno);

            return pacienteRetorno != null
                ? Ok(pacienteRetorno)
                : BadRequest("Paciente não encontrado.");
        }

        [HttpPost]
        public async Task<IActionResult> Post(PacienteAdicionarDto paciente)
        {
            if (paciente == null) return BadRequest("Dados Inválidos");

            var pacienteAdicionar = _mapper.Map<Paciente>(paciente);

            _repository.Add(pacienteAdicionar);

            return await _repository.SaveChangesAsync()
                ? Ok(paciente)
                : BadRequest("Erro ao salvar o paciente");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PacienteAtualizarDto paciente)
        {
            if (id <= 0) return BadRequest("Paciente não informado");

            var pacienteBanco = await _repository.GetPacientesByIdAsync(id);
            if (pacienteBanco == null) return NotFound("Paciente não encontrado");

            var pacienteAtualizar = _mapper.Map(paciente, pacienteBanco);

            _repository.Update(pacienteAtualizar);

            var pacienteRetorno = _mapper.Map<PacienteDetalhesDto>(pacienteAtualizar);

            return await _repository.SaveChangesAsync()
                ? Ok(pacienteRetorno)
                : BadRequest("Erro ao atualizar o paciente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Paciente não informado");

            var pacienteBanco = await _repository.GetPacientesByIdAsync(id);
            if (pacienteBanco == null) return NotFound("Paciente não encontrado");

            _repository.Delete(pacienteBanco);

            return await _repository.SaveChangesAsync()
                ? Ok("Paciente removido com sucesso")
                : BadRequest("Erro ao remover o paciente");
        }

    }
}
