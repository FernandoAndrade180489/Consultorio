using AutoMapper;
using Consultorio.Models.Dtos;
using Consultorio.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Controllers
{
    [ApiController]
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var profissionais = await _repository.GetProfissionais();

            return profissionais.Any()
                ? Ok(profissionais)
                : NotFound("Profissionais não encontrados");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("Profissional inválido");

            var profissional = await _repository.GetProfissionalById(id);

            var profissionalRetorno = _mapper.Map<ProfissionalDetalhesDto>(profissional);
  
            return profissionalRetorno != null
                ? Ok(profissionalRetorno)
                : NotFound("Profissional não encontrado");
        }
    }
}
