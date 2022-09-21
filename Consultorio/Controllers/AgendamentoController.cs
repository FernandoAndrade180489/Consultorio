using Consultorio.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Consultorio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        List<Agendamento> agendamentos = new List<Agendamento>();
        public AgendamentoController()
        {
            agendamentos.Add(new Agendamento 
            { 
                Id = 1, 
                NomePaciente = "Fernando", 
                Horario = new DateTime(2022, 09, 21) 
            });
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(agendamentos);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var agendamentoSelecionado = agendamentos.FirstOrDefault(x => x.Id == id);

            return agendamentoSelecionado != null 
                ? Ok(agendamentos)
                : BadRequest("Erro ao buscar o agendamento");
        }
    }
}
