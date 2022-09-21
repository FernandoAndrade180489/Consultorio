using Consultorio.Models.Entities;
using Consultorio.Services;
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
        private readonly IEmailService _emailService;   // Adicionando dependencia

        List<Agendamento> agendamentos = new List<Agendamento>();

        public AgendamentoController(IEmailService emailService)    // Registrando a dependencia no controller
        {
            agendamentos.Add(new Agendamento 
            { 
                Id = 1, 
                NomePaciente = "Fernando", 
                Horario = new DateTime(2022, 09, 21) 
            });
            _emailService = emailService;
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

        [HttpPost]
        public IActionResult Post()
        {
            var pacienteAgendado = true;

            // EmailService emailService = new EmailService(); - Errado instanciar a classe aqui

            if (pacienteAgendado)
            {
                _emailService.EnviarEmail("fernando@gmail.com");
            }

            return Ok();
        }
    }
}
