using AutoMapper;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using Consultorio.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Controllers
{    
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProfissionalController : ControllerBase
    {
        private readonly IProfissionalRepository _repository;
        private readonly IMapper _mapper;

        public ProfissionalController(IProfissionalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var profissionais = await _repository.GetProfissionais();

            return profissionais.Any()
                ? Ok(profissionais)
                : NotFound("Profissionais não encontrados");
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("Profissional inválido");

            var profissional = await _repository.GetProfissionalById(id);

            var profissionalRetorno = _mapper.Map<ProfissionalDetalhesDto>(profissional);
  
            return profissionalRetorno != null
                ? Ok(profissionalRetorno)
                : NotFound("Profissional não encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProfissionalAdicionarDto profissional)
        {
            if (string.IsNullOrEmpty(profissional.Nome)) return BadRequest("Dados inválidos");

            var profissionalAdicionar = _mapper.Map<Profissional>(profissional);

            _repository.Add(profissionalAdicionar);

            var profissionalRetorno = _mapper.Map<ProfissionalDetalhesDto>(profissionalAdicionar);

            return await _repository.SaveChangesAsync()
                ? Ok(profissionalRetorno)
                : BadRequest("Erro ao adicionar profissional");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ProfissionalAtualizarDto profissional)
        {
            if (id <= 0) return BadRequest("Profissional inválido");

            var profissionalBanco = await _repository.GetProfissionalById(id);
            if (profissionalBanco == null) return NotFound("Profissional não encontrado");

            var profissionalAtualizar = _mapper.Map(profissional, profissionalBanco);

            _repository.Update(profissionalAtualizar);

            var profissionalRetorno = _mapper.Map<ProfissionalDetalhesDto>(profissionalAtualizar);

            return await _repository.SaveChangesAsync()
                ? Ok(profissionalRetorno)
                : BadRequest("Erro ao atualizar profissional");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Profissional inválido");

            var profissionalBanco = await _repository.GetProfissionalById(id);
            if (profissionalBanco == null) return NotFound("Profissional não encontrado");

            _repository.Delete(profissionalBanco);

            return await _repository.SaveChangesAsync()
                ? Ok("Profissional removido com sucesso")
                : BadRequest("Erro ao remover profissional");
        }

        [HttpPost("adicionar-especialidade")]
        public async Task<IActionResult> PostProfissionalEspecialidade(ProfissionalEspecialidadeAdicionarDto profissional)
        {
            int profissionalId = profissional.ProfissionalId;
            int especialidadeId = profissional.EspecialidadeId;

            if (profissionalId <= 0 || especialidadeId <= 0) return BadRequest("Dados inválidos");

            var profissionalEspecialidade = await _repository.GetProfissionalEspecialidade(profissionalId, especialidadeId);

            if (profissionalEspecialidade != null) return Ok("Especialidade já cadastrada para o profissional");

            var especialidadeAdicionar = new ProfissionalEspecialidade
            {
                ProfissionalId = profissionalId,
                EspecialidadeId = especialidadeId,
            };

            _repository.Add(especialidadeAdicionar);

            return await _repository.SaveChangesAsync()
               ? Ok("Especialidade adicionada com sucesso")
               : BadRequest("Erro ao adicionar especialidade ao profissional");
        }

        [HttpDelete("{profissionalId}/deletar-especialidade/{especialidadeId}")]
        public async Task<IActionResult> DeleteProfissionalEspecialidade(int profissionalId, int especialidadeId)
        {
            if (profissionalId <= 0 || especialidadeId <= 0) return BadRequest("Dados inválidos");

            var profissionalEspecialidade = await _repository.GetProfissionalEspecialidade(profissionalId, especialidadeId);

            if (profissionalEspecialidade == null) return NotFound("Especialidade não encontrada para o profissional selecionado");

            _repository.Delete(profissionalEspecialidade);

            return await _repository.SaveChangesAsync()
               ? Ok("Especialidade removida do profissional")
               : BadRequest("Erro ao remover especialidade do profissional");
        }
    }
}
